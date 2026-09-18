
namespace Naninovel.UI
{
    public class ControlPanelSaveButton : ScriptableButton
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

            var saveLoadUI = uiManager.GetUI<ISaveLoadUI>();
            if (saveLoadUI is null) return;

            saveLoadUI.PresentationMode = SaveLoadUIPresentationMode.Save;
            saveLoadUI.Show();
        }
    } 
}
