using UnityEngine;

namespace Naninovel
{
    [EditInProjectSettings]
    public class MoviesConfiguration : Configuration
    {
        public const string DefaultPathPrefix = "Movies";

        [Tooltip("Configuration of the resource loader used with movie resources.")]
        public ResourceLoaderConfiguration Loader = new() { PathPrefix = DefaultPathPrefix };
        [Tooltip("Whether to skip movie playback when user activates `cancel` input keys.")]
        public bool SkipOnInput = true;
        [Tooltip("Whether to skip frames to catch up with current time.")]
        public bool SkipFrames = true;
        [Tooltip("Time in seconds to fade in/out before starting/finishing playing the movie.")]
        public float FadeDuration = 1f;
        [Tooltip("Texture to show while fading. Will use a simple black texture when not specified.")]
        public Texture2D CustomFadeTexture;
        [Tooltip ("Whether to automatically play a movie after engine initialization and before showing the main menu.")]
        public bool PlayIntroMovie;
        [Tooltip("Path to the intro movie resource.")]
        [ResourcePopup(DefaultPathPrefix, DefaultPathPrefix)]
        public string IntroMovieName;
    }
}
