
namespace Naninovel.UI
{
    public class ControlPanelLogButton : ScriptableLabeledButton
    {
        private IUIManager uiManager;

        protected override void Awake ()
        {
            base.Awake();

            uiManager = Engine.GetServiceOrErr<IUIManager>();
        }

        protected override void OnButtonClick ()
        {
            uiManager.GetUI<IPauseUI>()?.Hide();
            uiManager.GetUI<IBacklogUI>()?.Show();
        }
    } 
}
