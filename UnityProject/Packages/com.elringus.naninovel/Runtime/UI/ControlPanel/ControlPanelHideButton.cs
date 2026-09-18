
namespace Naninovel.UI
{
    public class ControlPanelHideButton : ScriptableLabeledButton
    {
        private IUIManager uiManager;

        protected override void Awake ()
        {
            base.Awake();

            uiManager = Engine.GetServiceOrErr<IUIManager>();
        }

        protected override void OnButtonClick () => uiManager.SetUIVisibleWithToggle(false);
    } 
}
