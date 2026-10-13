using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace Naninovel
{
    /// <inheritdoc cref="IAudioManager"/>
    [InitializeAtRuntime]
    public class AudioManager : IStatefulService<SettingsStateMap>, IStatefulService<GameStateMap>, IAudioManager
    {
        [Serializable]
        public class Settings
        {
            public float MasterVolume;
            public float BgmVolume;
            public float SfxVolume;
            public float VoiceVolume;
            public string VoiceLocale;
            public List<NamedFloat> AuthorVolume;
        }

        [Serializable]
        public class GameState
        {
            public List<AudioClipState> BgmClips;
            public List<AudioClipState> SfxClips;
        }

        private class AuthorSource
        {
            public CharacterMetadata Metadata;
            public AudioSource Source;
        }

        private sealed class VoiceGroup
        {
            public readonly LocalizableResourceLoader<AudioClip> Loader;
            public readonly List<VoiceGroupTrack> Tracks = new();
            public bool Released;
            public VoiceGroup(LocalizableResourceLoader<AudioClip> loader) => Loader = loader;
        }

        private sealed class VoiceGroupTrack
        {
            public string Path;
            public IAudioTrack Track;
        }

        public virtual AudioConfiguration Configuration { get; }
        public virtual AudioMixer AudioMixer { get; }
        public virtual float MasterVolume { get => GetMixerVolume(Configuration.MasterVolumeHandleName); set => SetMixerVolume(Configuration.MasterVolumeHandleName, value); }
        public virtual float BgmVolume
        {
            get => GetMixerVolume(Configuration.BgmVolumeHandleName);
            set
            {
                if (BgmGroupAvailable) SetMixerVolume(Configuration.BgmVolumeHandleName, value);
            }
        }
        public virtual float SfxVolume
        {
            get => GetMixerVolume(Configuration.SfxVolumeHandleName);
            set
            {
                if (SfxGroupAvailable) SetMixerVolume(Configuration.SfxVolumeHandleName, value);
            }
        }
        public virtual float VoiceVolume
        {
            get => GetMixerVolume(Configuration.VoiceVolumeHandleName);
            set
            {
                if (VoiceGroupAvailable) SetMixerVolume(Configuration.VoiceVolumeHandleName, value);
            }
        }
        public virtual string VoiceLocale { get => voiceLoader.OverrideLocale; set => voiceLoader.OverrideLocale = value; }
        public virtual IResourceLoader AudioLoader => audioLoader;
        public virtual IResourceLoader VoiceLoader => voiceLoader;

        protected virtual bool BgmGroupAvailable => bgmGroup;
        protected virtual bool SfxGroupAvailable => sfxGroup;
        protected virtual bool VoiceGroupAvailable => voiceGroup;

        private readonly IResourceProviderManager resources;
        private readonly ILocalizationManager l10n;
        private readonly ICharacterManager chars;
        private readonly Dictionary<string, AudioClipState> bgmMap = new();
        private readonly Dictionary<string, AudioClipState> sfxMap = new();
        private readonly Dictionary<string, float> authorVolume = new();
        private readonly Dictionary<string, AuthorSource> authorSources = new();
        private AudioMixerGroup bgmGroup, sfxGroup, voiceGroup;
        private LocalizableResourceLoader<AudioClip> audioLoader, voiceLoader;
        private IAudioPlayer audioPlayer;
        private AudioClipState? voiceClip;
        private VoiceGroup activeVoiceGroup;
        private int voiceRequestVersion, voiceStopVersion;
        private readonly System.Threading.SemaphoreSlim voiceLoadGate = new(1, 1);

        public AudioManager (AudioConfiguration config, IResourceProviderManager resources,
            ILocalizationManager l10n, ICharacterManager chars)
        {
            Configuration = config;
            this.resources = resources;
            this.l10n = l10n;
            this.chars = chars;
            AudioMixer = config.CustomAudioMixer ? config.CustomAudioMixer : Engine.LoadInternalResource<AudioMixer>("DefaultMixer");
        }

        public virtual UniTask InitializeService ()
        {
            if (AudioMixer)
            {
                bgmGroup = AudioMixer.FindMatchingGroups(Configuration.BgmGroupPath)?.FirstOrDefault();
                sfxGroup = AudioMixer.FindMatchingGroups(Configuration.SfxGroupPath)?.FirstOrDefault();
                voiceGroup = AudioMixer.FindMatchingGroups(Configuration.VoiceGroupPath)?.FirstOrDefault();
            }

            audioLoader = Configuration.AudioLoader.CreateLocalizableFor<AudioClip>(resources, l10n);
            voiceLoader = Configuration.VoiceLoader.CreateLocalizableFor<AudioClip>(resources, l10n);
            var playerType = Type.GetType(Configuration.AudioPlayer);
            if (playerType is null) throw new Error($"Failed to get type of '{Configuration.AudioPlayer}' audio player.");
            audioPlayer = (IAudioPlayer)Activator.CreateInstance(playerType);

            return UniTask.CompletedTask;
        }

        public virtual void ResetService ()
        {
            StopVoice();
            audioPlayer.StopAll();
            bgmMap.Clear();
            sfxMap.Clear();
            voiceClip = null;

            audioLoader?.ReleaseAll(this);
            voiceLoader?.ReleaseAll(this);
        }

        public virtual void DestroyService ()
        {
            StopVoice();
            if (audioPlayer is IDisposable disposable)
                disposable.Dispose();
            audioLoader?.ReleaseAll(this);
            voiceLoader?.ReleaseAll(this);
        }

        public virtual void SaveServiceState (SettingsStateMap stateMap)
        {
            var settings = new Settings {
                MasterVolume = MasterVolume,
                BgmVolume = BgmVolume,
                SfxVolume = SfxVolume,
                VoiceVolume = VoiceVolume,
                VoiceLocale = VoiceLocale,
                AuthorVolume = authorVolume.Select(kv => new NamedFloat(kv.Key, kv.Value)).ToList()
            };
            stateMap.SetState(settings);
        }

        public virtual UniTask LoadServiceState (SettingsStateMap stateMap)
        {
            var settings = stateMap.GetState<Settings>();

            authorVolume.Clear();

            if (settings is null) // Apply default settings.
            {
                MasterVolume = Configuration.DefaultMasterVolume;
                BgmVolume = Configuration.DefaultBgmVolume;
                SfxVolume = Configuration.DefaultSfxVolume;
                VoiceVolume = Configuration.DefaultVoiceVolume;
                VoiceLocale = Configuration.VoiceLocales?.FirstOrDefault();
                return UniTask.CompletedTask;
            }

            MasterVolume = settings.MasterVolume;
            BgmVolume = settings.BgmVolume;
            SfxVolume = settings.SfxVolume;
            VoiceVolume = settings.VoiceVolume;
            VoiceLocale = Configuration.VoiceLocales?.Count > 0 ? settings.VoiceLocale ?? Configuration.VoiceLocales.First() : null;

            foreach (var item in settings.AuthorVolume)
                authorVolume[item.Name] = item.Value;

            return UniTask.CompletedTask;
        }

        public virtual void SaveServiceState (GameStateMap stateMap)
        {
            var state = new GameState { // Save only looped audio to prevent playing multiple clips at once when the game is (auto) saved in skip mode.
                BgmClips = bgmMap.Values.Where(s => IsBgmPlaying(s.Path) && s.Looped).ToList(),
                SfxClips = sfxMap.Values.Where(s => IsSfxPlaying(s.Path) && s.Looped).ToList()
            };
            stateMap.SetState(state);
        }

        public virtual async UniTask LoadServiceState (GameStateMap stateMap)
        {
            var state = stateMap.GetState<GameState>() ?? new GameState();
            using var _ = ListPool<UniTask>.Rent(out var tasks);

            StopVoice();

            if (state.BgmClips != null && state.BgmClips.Count > 0)
            {
                foreach (var bgmPath in bgmMap.Keys.ToList())
                    if (!state.BgmClips.Exists(c => c.Path.EqualsFast(bgmPath)))
                        tasks.Add(StopBgm(bgmPath));
                foreach (var clipState in state.BgmClips)
                    if (IsBgmPlaying(clipState.Path))
                        tasks.Add(ModifyBgm(clipState.Path, clipState.Volume, clipState.Looped, 0));
                    else tasks.Add(PlayBgm(clipState.Path, clipState.Volume, 0, clipState.Looped));
            }
            else tasks.Add(StopAllBgm());

            if (state.SfxClips != null && state.SfxClips.Count > 0)
            {
                foreach (var sfxPath in sfxMap.Keys.ToList())
                    if (!state.SfxClips.Exists(c => c.Path.EqualsFast(sfxPath)))
                        tasks.Add(StopSfx(sfxPath));
                foreach (var clipState in state.SfxClips)
                    if (IsSfxPlaying(clipState.Path))
                        tasks.Add(ModifySfx(clipState.Path, clipState.Volume, clipState.Looped, 0));
                    else tasks.Add(PlaySfx(clipState.Path, clipState.Volume, 0, clipState.Looped));
            }
            else tasks.Add(StopAllSfx());

            await UniTask.WhenAll(tasks);
        }

        public virtual void GetPlayedBgm (ICollection<string> paths)
        {
            foreach (var path in bgmMap.Keys)
                if (IsBgmPlaying(path))
                    paths.Add(path);
        }

        public virtual void GetPlayedSfx (ICollection<string> paths)
        {
            foreach (var path in sfxMap.Keys)
                if (IsSfxPlaying(path))
                    paths.Add(path);
        }

        public virtual string GetPlayedVoice ()
        {
            if (activeVoiceGroup != null)
                return activeVoiceGroup.Tracks.FirstOrDefault(v => IsCurrentVoiceTrack(v.Track))?.Path;
            return IsVoicePlaying(voiceClip?.Path) ? voiceClip?.Path : null;
        }

        public virtual async UniTask<bool> AudioExists (string path)
        {
            return await audioLoader.Exists(path);
        }

        public virtual async UniTask<bool> VoiceExists (string path)
        {
            return await voiceLoader.Exists(path);
        }

        public virtual async UniTask ModifyBgm (string path, float volume, bool loop, float time, AsyncToken token = default)
        {
            if (!bgmMap.ContainsKey(path)) return;

            bgmMap[path] = new(path, volume, loop);
            await ModifyAudio(path, volume, loop, time, token);
        }

        public virtual async UniTask ModifySfx (string path, float volume, bool loop, float time, AsyncToken token = default)
        {
            if (!sfxMap.ContainsKey(path)) return;

            sfxMap[path] = new(path, volume, loop);
            await ModifyAudio(path, volume, loop, time, token);
        }

        public virtual async UniTask PlaySfxFast (string path, float volume = 1f, string group = default, bool restart = true, bool additive = true)
        {
            if (!audioLoader.IsLoaded(path)) await AudioLoader.LoadOrErr(path);
            var clip = audioLoader.GetLoaded(path);
            if (audioPlayer.IsPlaying(clip) && !restart && !additive) return;
            if (audioPlayer.IsPlaying(clip) && restart) audioPlayer.Stop(clip);
            audioPlayer.Play(clip, null, volume, false, FindAudioGroupOrDefault(group, sfxGroup), null, additive);
        }

        public virtual async UniTask PlayBgm (string path, float volume = 1f, float fadeTime = 0f, bool loop = true, string introPath = null, string group = default, AsyncToken token = default)
        {
            var clipResource = await audioLoader.LoadOrErr(path, this);
            token.ThrowIfCanceled();

            bgmMap[path] = new(path, volume, loop);

            var introClip = default(AudioClip);
            if (!string.IsNullOrEmpty(introPath))
            {
                var introClipResource = await audioLoader.LoadOrErr(introPath, this);
                token.ThrowIfCanceled();
                introClip = introClipResource.Object;
            }

            if (fadeTime <= 0) audioPlayer.Play(clipResource, null, volume, loop, FindAudioGroupOrDefault(group, bgmGroup), introClip);
            else await audioPlayer.Play(clipResource, fadeTime, null, volume, loop, FindAudioGroupOrDefault(group, bgmGroup), introClip, token: token);
        }

        public virtual async UniTask StopBgm (string path, float fadeTime = 0f, AsyncToken token = default)
        {
            if (string.IsNullOrWhiteSpace(path)) return;
            if (bgmMap.ContainsKey(path))
                bgmMap.Remove(path);

            if (!audioLoader.IsLoaded(path)) return;
            var clipResource = audioLoader.GetLoaded(path);
            if (fadeTime <= 0) audioPlayer.Stop(clipResource);
            else await audioPlayer.Stop(clipResource, fadeTime, token);

            if (!IsBgmPlaying(path))
                audioLoader?.Release(path, this);
        }

        public virtual async UniTask StopAllBgm (float fadeTime = 0f, AsyncToken token = default)
        {
            await UniTask.WhenAll(bgmMap.Keys.ToList().Select(p => StopBgm(p, fadeTime, token)));
        }

        public virtual async UniTask PlaySfx (string path, float volume = 1f, float fadeTime = 0f, bool loop = false, string group = default, AsyncToken token = default)
        {
            var clipResource = await audioLoader.LoadOrErr(path, this);
            token.ThrowIfCanceled();

            sfxMap[path] = new(path, volume, loop);

            if (fadeTime <= 0) audioPlayer.Play(clipResource, null, volume, loop, FindAudioGroupOrDefault(group, sfxGroup));
            else await audioPlayer.Play(clipResource, fadeTime, null, volume, loop, FindAudioGroupOrDefault(group, sfxGroup), token: token);
        }

        public virtual async UniTask StopSfx (string path, float fadeTime = 0f, AsyncToken token = default)
        {
            if (string.IsNullOrWhiteSpace(path)) return;
            if (sfxMap.ContainsKey(path))
                sfxMap.Remove(path);

            if (!audioLoader.IsLoaded(path)) return;
            var clipResource = audioLoader.GetLoaded(path);
            if (fadeTime <= 0) audioPlayer.Stop(clipResource);
            else await audioPlayer.Stop(clipResource, fadeTime, token);

            if (!IsSfxPlaying(path))
                audioLoader?.Release(path, this);
        }

        public virtual async UniTask StopAllSfx (float fadeTime = 0f, AsyncToken token = default)
        {
            await UniTask.WhenAll(sfxMap.Keys.ToList().Select(p => StopSfx(p, fadeTime, token)));
        }

        public virtual async UniTask PlayVoice (string path, float volume = 1f, string group = default, string authorId = default, AsyncToken token = default)
        {
            token.ThrowIfCanceled();
            var request = ++voiceRequestVersion;
            var stopVersion = voiceStopVersion;
            var loader = voiceLoader;
            // A separate loading holder protects a same-path replacement from StopVoice releasing
            // its predecessor's manager holder. It also gives canceled loads independent cleanup.
            var loadingHolder = new object();
            try
            {
                var clipResource = await LoadVoiceForPlayback(loader, path, loadingHolder, token);
                CheckVoiceRequest(request, stopVersion, token,
                    Configuration.VoiceOverlapPolicy == VoiceOverlapPolicy.PreventOverlap);
                var audioSource = !string.IsNullOrEmpty(authorId) ? await GetOrInstantiateAuthorSource(authorId) : null;
                CheckVoiceRequest(request, stopVersion, token,
                    Configuration.VoiceOverlapPolicy == VoiceOverlapPolicy.PreventOverlap);

                if (activeVoiceGroup != null || Configuration.VoiceOverlapPolicy == VoiceOverlapPolicy.PreventOverlap)
                {
                    ++voiceStopVersion;
                    StopVoicePlayback();
                }

                volume = ApplyAuthorVolume(volume, authorId);
                loader.Hold(path, this);
                voiceClip = new AudioClipState(path, volume, false);
                audioPlayer.Play(clipResource, audioSource, volume, false, FindAudioGroupOrDefault(group, voiceGroup));
            }
            finally { loader.ReleaseAll(loadingHolder); }
        }

        public virtual async UniTask PlayVoiceGroup (IReadOnlyList<VoicePlayback> voices, AsyncToken token = default)
        {
            if (voices is null || voices.Count == 0) throw new ArgumentException("A voice group cannot be empty.", nameof(voices));
            var entries = voices.ToArray();
            var paths = new HashSet<string>(StringComparer.Ordinal);
            foreach (var entry in entries)
                if (string.IsNullOrWhiteSpace(entry.Path) || !paths.Add(entry.Path))
                    throw new ArgumentException("Voice group paths must be non-empty and distinct.", nameof(voices));

            token.ThrowIfCanceled();
            var request = ++voiceRequestVersion;
            var stopVersion = voiceStopVersion;
            var owner = new VoiceGroup(voiceLoader);
            var clips = new AudioClip[entries.Length];
            var sources = new AudioSource[entries.Length];
            var groups = new AudioMixerGroup[entries.Length];
            var clipIdentities = new HashSet<AudioClip>();
            var sourceIdentities = new HashSet<AudioSource>();
            var committed = false;
            try
            {
                for (var i = 0; i < entries.Length; i++)
                {
                    clips[i] = await LoadVoiceForPlayback(owner.Loader, entries[i].Path, owner, token);
                    CheckVoiceRequest(request, stopVersion, token);
                    if (!clipIdentities.Add(clips[i]))
                        throw new ArgumentException("Voice group paths must identify distinct clips.", nameof(voices));
                    if (!string.IsNullOrEmpty(entries[i].AuthorId))
                        sources[i] = await GetOrInstantiateAuthorSource(entries[i].AuthorId);
                    CheckVoiceRequest(request, stopVersion, token);
                    // Two entries must not overwrite the same character AudioSource.
                    if (sources[i] && !sourceIdentities.Add(sources[i])) sources[i] = null;
                    groups[i] = FindAudioGroupOrDefault(entries[i].Group, voiceGroup);
                }

                ++voiceStopVersion;
                StopVoicePlayback();
                activeVoiceGroup = owner;
                // There are no awaits in this loop: every prepared clip starts in the same frame.
                for (var i = 0; i < entries.Length; i++)
                {
                    audioPlayer.Play(clips[i], sources[i], ApplyAuthorVolume(entries[i].Volume, entries[i].AuthorId),
                        false, groups[i]);
                    var track = audioPlayer.GetTracks(clips[i])?.LastOrDefault();
                    if (track is null) throw new Error($"Voice player did not create a track for '{entries[i].Path}'.");
                    var item = new VoiceGroupTrack { Path = entries[i].Path, Track = track };
                    owner.Tracks.Add(item);
                }
                committed = true;
                foreach (var item in owner.Tracks.ToArray()) ReleaseWhenVoiceEnds(owner, item).Forget();
            }
            finally
            {
                if (!committed) ReleaseVoiceGroup(owner, true);
            }
        }

        private async UniTask<Resource<AudioClip>> LoadVoiceForPlayback(LocalizableResourceLoader<AudioClip> loader,
            string path, object holder, AsyncToken token)
        {
            // ResourceLoader does not coalesce cold same-path loads. Serialize playback acquisitions
            // so a late provider result cannot replace a newer request's holder registry.
            try { await voiceLoadGate.WaitAsync(token.CancellationToken); }
            catch (OperationCanceledException) when (token.Canceled)
            {
                token.ThrowIfCanceled();
                throw;
            }
            try
            {
                token.ThrowIfCanceled();
                return await loader.LoadOrErr(path, holder);
            }
            finally { voiceLoadGate.Release(); }
        }

        private void CheckVoiceRequest(int request, int stopVersion, AsyncToken token, bool requireLatest = true)
        {
            token.ThrowIfCanceled();
            if (stopVersion != voiceStopVersion || requireLatest && request != voiceRequestVersion)
                throw new AsyncOperationCanceledException(new AsyncToken(new System.Threading.CancellationToken(true)));
        }

        private float ApplyAuthorVolume(float volume, string authorId)
        {
            if (string.IsNullOrEmpty(authorId)) return volume;
            var authorLevel = GetAuthorVolume(authorId);
            return Mathf.Approximately(authorLevel, -1) ? volume : volume * authorLevel;
        }

        private static bool IsCurrentVoiceTrack(IAudioTrack track)
        {
            if (track is AudioTrack concrete && (!concrete.Source || concrete.Source.clip != concrete.Clip)) return false;
            return track?.Playing == true;
        }

        private async UniTask ReleaseWhenVoiceEnds(VoiceGroup owner, VoiceGroupTrack item)
        {
            // AudioTrack's OnStop timer uses game time, while AudioSource playback uses DSP time.
            // Fast-forward must not release a still-playing clip merely because that timer elapsed.
            while (!owner.Released && IsCurrentVoiceTrack(item.Track)) await UniTask.Yield();
            if (!owner.Released) ReleaseVoiceGroupTrack(owner, item);
        }

        private void ReleaseVoiceGroupTrack(VoiceGroup owner, VoiceGroupTrack item)
        {
            if (owner.Released || !owner.Tracks.Remove(item)) return;
            owner.Loader.Release(item.Path, owner);
            if (owner.Tracks.Count == 0)
            {
                owner.Released = true;
                if (ReferenceEquals(activeVoiceGroup, owner)) activeVoiceGroup = null;
            }
        }

        private void ReleaseVoiceGroup(VoiceGroup owner, bool stop)
        {
            if (owner is null || owner.Released) return;
            owner.Released = true;
            if (ReferenceEquals(activeVoiceGroup, owner)) activeVoiceGroup = null;
            foreach (var item in owner.Tracks)
            {
                if (stop && IsCurrentVoiceTrack(item.Track)) item.Track.Stop();
            }
            owner.Tracks.Clear();
            owner.Loader.ReleaseAll(owner);
        }

        public virtual bool IsBgmPlaying (string path)
        {
            if (string.IsNullOrEmpty(path) || !bgmMap.ContainsKey(path)) return false;
            return audioLoader.TryGetLoaded(path, out var clip) && audioPlayer.IsPlaying(clip);
        }

        public virtual bool IsSfxPlaying (string path)
        {
            if (string.IsNullOrEmpty(path) || !sfxMap.ContainsKey(path)) return false;
            return audioLoader.TryGetLoaded(path, out var clip) && audioPlayer.IsPlaying(clip);
        }

        public virtual bool IsVoicePlaying (string path)
        {
            if (activeVoiceGroup != null)
                return activeVoiceGroup.Tracks.Any(v => v.Path == path && IsCurrentVoiceTrack(v.Track));
            if (!voiceClip.HasValue || voiceClip.Value.Path != path) return false;
            return voiceLoader.TryGetLoaded(path, out var clip) && audioPlayer.IsPlaying(clip);
        }

        public virtual void StopVoice ()
        {
            ++voiceRequestVersion;
            ++voiceStopVersion;
            StopVoicePlayback();
        }

        private void StopVoicePlayback()
        {
            ReleaseVoiceGroup(activeVoiceGroup, true);
            if (!voiceClip.HasValue) return;
            var path = voiceClip.Value.Path;
            voiceClip = null;
            var clipResource = voiceLoader.GetLoaded(path);
            if (clipResource != null && clipResource.Valid) audioPlayer.Stop(clipResource);
            voiceLoader.Release(path, this);
        }

        public virtual IAudioTrack GetAudioTrack (string path)
        {
            var clipResource = audioLoader.GetLoaded(path);
            if (clipResource is null || !clipResource.Valid) return null;
            return audioPlayer.GetTracks(clipResource.Object)?.FirstOrDefault();
        }

        public virtual IAudioTrack GetVoiceTrack (string path)
        {
            if (activeVoiceGroup != null)
                return activeVoiceGroup.Tracks.FirstOrDefault(v => v.Path == path && IsCurrentVoiceTrack(v.Track))?.Track;
            var clipResource = voiceLoader.GetLoaded(path);
            if (clipResource is null || !clipResource.Valid) return null;
            return audioPlayer.GetTracks(clipResource.Object)?.FirstOrDefault();
        }

        public virtual float GetAuthorVolume (string authorId)
        {
            if (string.IsNullOrEmpty(authorId)) return -1;
            return authorVolume.GetValueOrDefault(authorId, -1);
        }

        public virtual void SetAuthorVolume (string authorId, float volume)
        {
            if (string.IsNullOrEmpty(authorId)) return;
            authorVolume[authorId] = volume;
        }

        protected virtual async UniTask ModifyAudio (string path, float volume, bool loop, float time, AsyncToken token = default)
        {
            if (!audioLoader.TryGetLoaded(path, out var clip)) return;
            var track = audioPlayer.GetTracks(clip)?.FirstOrDefault();
            if (track is null) return;
            track.Loop = loop;
            if (time <= 0) track.Volume = volume;
            else await track.Fade(volume, time, token);
        }

        protected virtual float GetMixerVolume (string handleName)
        {
            if (AudioMixer)
            {
                AudioMixer.GetFloat(handleName, out var value);
                return MathUtils.DecibelToLinear(value);
            }
            return audioPlayer.Volume;
        }

        protected virtual void SetMixerVolume (string handleName, float value)
        {
            if (AudioMixer) AudioMixer.SetFloat(handleName, MathUtils.LinearToDecibel(value));
            else audioPlayer.Volume = value;
        }

        protected virtual AudioMixerGroup FindAudioGroupOrDefault (string path, AudioMixerGroup defaultGroup)
        {
            if (string.IsNullOrEmpty(path)) return defaultGroup;
            var group = AudioMixer.FindMatchingGroups(path)?.FirstOrDefault();
            return group ? group : defaultGroup;
        }

        protected virtual async UniTask<AudioSource> GetOrInstantiateAuthorSource (string authorId)
        {
            if (authorSources.TryGetValue(authorId, out var authorSource))
            {
                if (!authorSource.Metadata.VoiceSource) return null;
                if (authorSource.Source) return authorSource.Source;
                return await Instantiate();
            }
            return await Instantiate();

            async UniTask<AudioSource> Instantiate ()
            {
                if (!chars.ActorExists(authorId)) return null;

                var metadata = chars.Configuration.GetMetadataOrDefault(authorId);
                var character = chars.GetActor(authorId) as MonoBehaviourActor<CharacterMetadata>;
                if (!metadata.VoiceSource || character is null)
                {
                    authorSources[authorId] = new() { Metadata = metadata };
                    return null;
                }

                var source = await Engine.Instantiate<AudioSource>(metadata.VoiceSource, parent: character.GameObject.transform);
                authorSources[authorId] = new() { Metadata = metadata, Source = source };
                return source;
            }
        }
    }
}
