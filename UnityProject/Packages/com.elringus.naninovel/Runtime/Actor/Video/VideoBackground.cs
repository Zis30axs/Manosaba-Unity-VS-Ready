using UnityEngine.Video;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="IBackgroundActor"/> implementation using <see cref="VideoClip"/> to represent the actor.
    /// </summary>
    [ActorResources(typeof(VideoClip), true)]
    public class VideoBackground : VideoActor<BackgroundMetadata>, IBackgroundActor
    {
        protected override string MixerGroup => Configuration.GetOrDefault<AudioConfiguration>().BgmGroupPath;

        private BackgroundMatcher matcher;

        public VideoBackground (string id, BackgroundMetadata meta, StandaloneAppearanceLoader<VideoClip> loader)
            : base(id, meta, loader) { }

        public override async UniTask Initialize ()
        {
            await base.Initialize();
            matcher = BackgroundMatcher.CreateFor(ActorMeta, TransitionalRenderer);
        }

        public override void Dispose ()
        {
            base.Dispose();
            matcher?.Stop();
        }
    }
}
