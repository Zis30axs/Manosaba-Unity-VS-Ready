namespace Naninovel.UI
{
    public class ControlPanelTitleButton : ScriptableButton
    {
        [ManagedText("DefaultUI")]
        protected static string ConfirmationMessage = "Are you sure you want to quit to the title screen?<br>Any unsaved game progress will be lost.";

        private IStateManager state;
        private IUIManager ui;

        protected override void Awake ()
        {
            base.Awake();

            state = Engine.GetServiceOrErr<IStateManager>();
            ui = Engine.GetServiceOrErr<IUIManager>();
        }

        protected override void OnButtonClick ()
        {
            ui.GetUI<IPauseUI>()?.Hide();

            ExitToTitle();
        }

        private async void ExitToTitle ()
        {
            if (ui.GetUI<IConfirmationUI>() is { } cui &&
                !await cui.Confirm(ConfirmationMessage)) return;

            using (await LoadingScreen.Show())
                await state.ResetState();
            ui.GetUI<ITitleUI>()?.Show();
        }
    }
}
