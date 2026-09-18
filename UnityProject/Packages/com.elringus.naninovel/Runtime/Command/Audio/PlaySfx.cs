namespace Naninovel.Commands
{
    [Doc(
        @"
Plays or modifies currently played [SFX (sound effect)](/guide/audio#sound-effects) track with the specified name.",
        @"
Sound effect tracks are not looped by default.
When sfx track name (SfxPath) is not specified, will affect all the currently played tracks.
When invoked for a track that is already playing, the playback won't be affected (track won't start playing from the start),
but the specified parameters (volume and whether the track is looped) will be applied.",
        @"
; Plays an SFX with the name 'Explosion' once.
@sfx Explosion",
        @"
; Plays an SFX with the name 'Rain' in a loop and fades-in over 30 seconds.
@sfx Rain loop! fade:30",
        @"
; Changes volume of all the played SFX tracks to 75% over 2.5 seconds
; and disables looping for all of them.
@sfx volume:0.75 !loop time:2.5"
        )]
    [CommandAlias("sfx")]
    public class PlaySfx : AudioCommand, Command.IPreloadable
    {
        [Doc("Path to the sound effect asset to play.")]
        [ParameterAlias(NamelessParameterAlias), ResourceContext(AudioConfiguration.DefaultAudioPathPrefix)]
        public StringParameter SfxPath;
        [Doc("Volume of the sound effect.")]
        [ParameterDefaultValue("1")]
        public DecimalParameter Volume = 1f;
        [Doc("Whether to play the sound effect in a loop.")]
        [ParameterDefaultValue("false")]
        public BooleanParameter Loop = false;
        [Doc("Duration of the volume fade-in when starting playback, in seconds (0.0 by default); doesn't have effect when modifying a playing track.")]
        [ParameterAlias("fade"), ParameterDefaultValue("0")]
        public DecimalParameter FadeInDuration = 0f;
        [Doc("Audio mixer [group path](https://docs.unity3d.com/ScriptReference/Audio.AudioMixer.FindMatchingGroups) that should be used when playing the audio.")]
        [ParameterAlias("group")]
        public StringParameter GroupPath;
        [Doc("Duration (in seconds) of the modification.")]
        [ParameterAlias("time"), ParameterDefaultValue("0.35")]
        public DecimalParameter Duration;
        [Doc("Whether to wait for the SFX fade animation to finish before playing next command.")]
        public BooleanParameter Wait;

        public async UniTask PreloadResources ()
        {
            if (!Assigned(SfxPath) || SfxPath.DynamicValue) return;
            await AudioManager.AudioLoader.Load(SfxPath, this);
        }

        public void ReleaseResources ()
        {
            if (!Assigned(SfxPath) || SfxPath.DynamicValue) return;
            AudioManager?.AudioLoader?.Release(SfxPath, this);
        }

        public override UniTask Execute (AsyncToken token = default)
        {
            if (ShouldSkip()) return UniTask.CompletedTask;
            return WaitOrForget(Play, Wait, token);
        }

        protected virtual async UniTask Play (AsyncToken token)
        {
            var duration = Assigned(Duration) ? Duration.Value : AudioManager.Configuration.DefaultFadeDuration;
            if (Assigned(SfxPath)) await PlayOrModifyTrack(AudioManager, SfxPath, Volume, Loop, duration, FadeInDuration, GroupPath, token);
            else
            {
                using var _ = SetPool<string>.Rent(out var paths);
                using var __ = ListPool<UniTask>.Rent(out var tasks);
                AudioManager.GetPlayedSfx(paths);
                foreach (var path in paths)
                    tasks.Add(PlayOrModifyTrack(AudioManager, path, Volume, Loop, duration, FadeInDuration, null, token));
                await UniTask.WhenAll(tasks);
            }
        }

        protected virtual bool ShouldSkip ()
        {
            if (AudioManager.Configuration.PlaySfxWhileSkipping) return false;
            if (Assigned(Loop) && Loop) return false;
            return Engine.GetServiceOrErr<IScriptPlayer>().SkipActive;
        }

        private static UniTask PlayOrModifyTrack (IAudioManager manager, string path, float volume, bool loop, float time, float fade, string group, AsyncToken token)
        {
            if (manager.IsSfxPlaying(path)) return manager.ModifySfx(path, volume, loop, time, token);
            return manager.PlaySfx(path, volume, fade, loop, group, token);
        }
    }
}
