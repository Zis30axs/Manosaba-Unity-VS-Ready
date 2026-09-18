
namespace Naninovel.UI
{
    public class ControlPanelQuickSaveButton : ScriptableButton
    {
        private IStateManager gameState;

        protected override void Awake ()
        {
            base.Awake();

            gameState = Engine.GetServiceOrErr<IStateManager>();
        }

        protected override void OnButtonClick () => QuickSave();

        private async void QuickSave ()
        {
            UIComponent.interactable = false;
            await gameState.QuickSave();
            UIComponent.interactable = true;
        }
    } 
}
