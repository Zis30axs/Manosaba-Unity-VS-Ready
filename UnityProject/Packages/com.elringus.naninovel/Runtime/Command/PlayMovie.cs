using Naninovel.UI;

namespace Naninovel.Commands
{
    [Doc(
        @"
Plays a movie with the specified name (path).",
        @"
Will fade-out the screen before playing the movie and fade back in after the play.
Playback can be canceled by activating a `cancel` input (`Esc` key by default).",
        @"
; Given an 'Opening' video clip is added to the movie resources, plays it.
@movie Opening"
    )]
    [CommandAlias("movie")]
    public class PlayMovie : Command, Command.IPreloadable
    {
        [Doc("Name of the movie resource to play.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter, ResourceContext(MoviesConfiguration.DefaultPathPrefix)]
        public StringParameter MovieName;
        [Doc("Duration (in seconds) of the fade animation. When not specified, will use fade duration set in the movie configuration.")]
        [ParameterAlias("time")]
        public DecimalParameter Duration;
        [Doc("Whether to block interaction with the game while the movie is playing, preventing the player from skipping it.")]
        [ParameterAlias("block")]
        public BooleanParameter BlockInteraction;
        [Doc(SharedDocs.WaitParameter)]
        public BooleanParameter Wait;

        protected virtual IMoviePlayer Player => Engine.GetServiceOrErr<IMoviePlayer>();

        public async UniTask PreloadResources ()
        {
            if (!Assigned(MovieName) || MovieName.DynamicValue) return;
            await Player.HoldResources(MovieName, this);
        }

        public void ReleaseResources ()
        {
            if (!Assigned(MovieName) || MovieName.DynamicValue) return;
            Player?.ReleaseResources(MovieName, this);
        }

        public override UniTask Execute (AsyncToken token = default)
        {
            return WaitOrForget(Play, Wait, token);
        }

        protected virtual async UniTask Play (AsyncToken token)
        {
            var movieUI = Engine.GetService<IUIManager>()?.GetUI<IMovieUI>();
            if (movieUI is null) return;

            var blocker = BlockInteraction ? new InteractionBlocker() : null;

            var fadeDuration = Assigned(Duration) ? Duration.Value : Player.Configuration.FadeDuration;
            await movieUI.ChangeVisibility(true, fadeDuration, token);

            var movieTexture = await Player.Play(MovieName, token);
            movieUI.SetMovieTexture(movieTexture);

            while (Player.Playing)
                await AsyncUtils.WaitEndOfFrame(token);

            blocker?.Dispose();

            await movieUI.ChangeVisibility(false, fadeDuration, token);
        }
    }
}
