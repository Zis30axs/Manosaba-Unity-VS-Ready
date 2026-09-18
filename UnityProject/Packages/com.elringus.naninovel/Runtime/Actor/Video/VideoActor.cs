using System;
using System.Collections.Generic;
using System.Linq;
using Naninovel.FX;
using UnityEngine;
using UnityEngine.Video;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="IActor"/> implementation using <see cref="VideoClip"/> to represent the actor.
    /// </summary>
    public abstract class VideoActor<TMeta> : MonoBehaviourActor<TMeta>, Blur.IBlurable
        where TMeta : OrthoActorMetadata
    {
        public override string Appearance { get => appearance; set => SetAppearance(value); }
        public override bool Visible { get => visible; set => SetVisibility(value); }

        protected virtual TransitionalRenderer TransitionalRenderer { get; private set; }
        protected virtual Dictionary<string, VideoAppearance> Appearances { get; } = new();
        protected virtual int TextureDepthBuffer => 24;
        protected virtual RenderTextureFormat TextureFormat => RenderTextureFormat.ARGB32;
        protected virtual string MixerGroup => Configuration.GetOrDefault<AudioConfiguration>().MasterGroupPath;

        private readonly Tweener<FloatTween> volumeTweener = new();
        private readonly StandaloneAppearanceLoader<VideoClip> videoLoader;
        private readonly string streamExtension;
        private string appearance;
        private bool visible;

        protected VideoActor (string id, TMeta meta, StandaloneAppearanceLoader<VideoClip> loader)
            : base(id, meta)
        {
            videoLoader = loader;
            streamExtension = Engine.GetConfiguration<ResourceProviderConfiguration>().VideoStreamExtension;
        }

        public override async UniTask Initialize ()
        {
            await base.Initialize();

            videoLoader.OnLocalized += HandleAppearanceLocalized;
            TransitionalRenderer = TransitionalRenderer.CreateFor(ActorMeta, GameObject, false);
            SetVisibility(false);
        }

        public virtual UniTask Blur (float intensity, Tween tween, AsyncToken token = default)
        {
            return TransitionalRenderer.Blur(intensity, tween, token);
        }

        public override async UniTask ChangeAppearance (string appearance, Tween tween,
            Transition? transition = default, AsyncToken token = default)
        {
            var previousAppearance = this.appearance;
            this.appearance = appearance;

            if (string.IsNullOrEmpty(appearance))
            {
                foreach (var app in Appearances.Values)
                    app.Video.Stop();
                return;
            }

            foreach (var kv in Appearances)
                if (!kv.Key.EqualsFast(appearance) && !kv.Key.EqualsFast(previousAppearance))
                    kv.Value.Video.Stop();

            var videoAppearance = await GetAppearance(appearance, token);
            var video = videoAppearance.Video;

            if (!video.isPrepared)
            {
                video.Prepare();
                // Player could be invalid, as we're invoking this from sync version of change appearance.
                while (token.EnsureNotCanceled(video) && !video.isPrepared)
                    await AsyncUtils.WaitEndOfFrame();
                if (!video) return;
            }

            var previousTexture = video.targetTexture;
            videoAppearance.Video.targetTexture = RenderTexture.GetTemporary((int)video.width, (int)video.height, TextureDepthBuffer, TextureFormat);
            videoAppearance.Video.Play();

            foreach (var kv in Appearances)
                kv.Value.TweenVolume(kv.Key.EqualsFast(appearance) && visible ? 1 : 0, tween.Duration, token).Forget();
            await TransitionalRenderer.TransitionTo(video.targetTexture, tween, transition, token);
            if (!video) return;

            if (previousTexture)
                RenderTexture.ReleaseTemporary(previousTexture);
            if (previousAppearance != this.appearance)
                ReleaseAppearance(previousAppearance);
        }

        public override async UniTask ChangeVisibility (bool visible, Tween tween, AsyncToken token = default)
        {
            this.visible = visible;

            foreach (var appearance in Appearances.Values)
                appearance.TweenVolume(visible && appearance.Video.isPlaying ? 1 : 0, tween.Duration, token).Forget();
            await TransitionalRenderer.FadeTo(visible ? TintColor.a : 0, tween, token);
        }

        public override void Dispose ()
        {
            base.Dispose();

            foreach (var videoAppearance in Appearances.Values)
            {
                if (!videoAppearance.Video) continue;
                RenderTexture.ReleaseTemporary(videoAppearance.Video.targetTexture);
                ObjectUtils.DestroyOrImmediate(videoAppearance.GameObject);
            }

            Appearances.Clear();

            if (videoLoader != null)
            {
                videoLoader.OnLocalized -= HandleAppearanceLocalized;
                videoLoader.ReleaseAll(this);
            }
        }

        protected virtual void SetAppearance (string appearance) => ChangeAppearance(appearance, new(0)).Forget();

        protected virtual void SetVisibility (bool visible) => ChangeVisibility(visible, new(0)).Forget();

        protected override Color GetBehaviourTintColor () => TransitionalRenderer.TintColor;

        protected override void SetBehaviourTintColor (Color tintColor)
        {
            if (!Visible) tintColor.a = TransitionalRenderer.TintColor.a;
            TransitionalRenderer.TintColor = tintColor;
        }

        protected virtual async UniTask<VideoAppearance> GetAppearance (string videoName, AsyncToken token = default)
        {
            if (Appearances.TryGetValue(videoName, out var cached)) return cached;

            var videoPlayer = Engine.CreateObject<VideoPlayer>(videoName, parent: Transform);
            videoPlayer.playOnAwake = false;
            videoPlayer.isLooping = ShouldLoopAppearance(videoName);
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            if (Application.platform == RuntimePlatform.WebGLPlayer && !Application.isEditor)
            {
                videoPlayer.source = VideoSource.Url;
                videoPlayer.url = PathUtils.Combine(Application.streamingAssetsPath, $"{ActorMeta.Loader.PathPrefix}/{Id}/{videoName}") + streamExtension;
                await AsyncUtils.WaitEndOfFrame(token);
            }
            else
            {
                var videoClip = await videoLoader.LoadOrErr(videoName, this);
                token.ThrowIfCanceled();
                videoPlayer.source = VideoSource.VideoClip;
                videoPlayer.clip = videoClip;
            }

            var videoAppearance = new VideoAppearance(videoPlayer, SetupAudioSource(videoPlayer));
            Appearances[videoName] = videoAppearance;

            return videoAppearance;
        }

        protected virtual bool ShouldLoopAppearance (string appearance)
        {
            return !appearance.EndsWith("NoLoop", StringComparison.OrdinalIgnoreCase);
        }

        protected virtual AudioSource SetupAudioSource (VideoPlayer player)
        {
            var audioManager = Engine.GetServiceOrErr<IAudioManager>();
            var audioSource = player.gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.bypassReverbZones = true;
            audioSource.bypassEffects = true;
            if (audioManager.AudioMixer && !string.IsNullOrEmpty(MixerGroup))
                audioSource.outputAudioMixerGroup = audioManager.AudioMixer.FindMatchingGroups(MixerGroup)?.FirstOrDefault();
            player.audioOutputMode = VideoAudioOutputMode.AudioSource;
            player.SetTargetAudioSource(0, audioSource);
            return audioSource;
        }

        protected virtual void ReleaseAppearance (string appearance)
        {
            if (string.IsNullOrEmpty(appearance)) return;

            videoLoader.Release(appearance, this);

            if (videoLoader.CountHolders(appearance) == 0 &&
                Appearances.Remove(appearance, out var player))
                DisposeAppearancePlayer(player);
        }

        protected virtual void DisposeAppearancePlayer (VideoAppearance player)
        {
            player.Video.Stop();
            RenderTexture.ReleaseTemporary(player.Video.targetTexture);
            ObjectUtils.DestroyOrImmediate(player.GameObject);
        }

        protected virtual void HandleAppearanceLocalized (Resource<VideoClip> resource)
        {
            if (Appearance == videoLoader.GetLocalPath(resource))
                Appearance = Appearance;
        }
    }
}
