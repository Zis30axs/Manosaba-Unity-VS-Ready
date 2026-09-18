
namespace Naninovel.UI
{
    public class TitleSettingsButton : ScriptableButton
    {
        private IUIManager uiManager;

        protected override void Awake ()
        {
            base.Awake();

            uiManager = Engine.GetServiceOrErr<IUIManager>();
        }

        protected override void OnButtonClick () => uiManager.GetUI<ISettingsUI>()?.Show();
    }
}
