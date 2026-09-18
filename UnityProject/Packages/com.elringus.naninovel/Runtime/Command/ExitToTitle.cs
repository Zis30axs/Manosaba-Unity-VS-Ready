using Naninovel.Metadata;

namespace Naninovel.Commands
{
    [Doc(
        @"
Resets engine state and shows `ITitleUI` UI (main menu).",
        null,
        @"
; Exit to title UI, no matter which script is playing.
@title"
    )]
    [CommandAlias("title"), Branch(BranchTraits.Endpoint, endpoint: "{" + Metadata.ExpressionEvaluator.TitleScript + "}")]
    public class ExitToTitle : Command
    {
        public override async UniTask Execute (AsyncToken token = default)
        {
            var gameState = Engine.GetServiceOrErr<IStateManager>();
            var uiManager = Engine.GetServiceOrErr<IUIManager>();

            using (await LoadingScreen.Show())
                await gameState.ResetState();
            // Don't check for the cancellation, as it's always cancelled after state reset.

            uiManager.GetUI<UI.ITitleUI>()?.Show();
        }
    }
}
