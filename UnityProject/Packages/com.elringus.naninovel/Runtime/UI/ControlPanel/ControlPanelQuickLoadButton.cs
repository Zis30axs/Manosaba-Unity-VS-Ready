namespace Naninovel.UI
{
    public class ControlPanelQuickLoadButton : ScriptableButton
    {
        private IStateManager gameState;

        protected override void Awake ()
        {
            base.Awake();

            gameState = Engine.GetServiceOrErr<IStateManager>();
        }

        protected override void Start ()
        {
            base.Start();

            ControlInteractability(default);
        }

        protected override void OnEnable ()
        {
            base.OnEnable();

            gameState.GameSlotManager.OnBeforeLoad += ControlInteractability;
            gameState.GameSlotManager.OnLoaded += ControlInteractability;
            gameState.GameSlotManager.OnBeforeSave += ControlInteractability;
            gameState.GameSlotManager.OnSaved += ControlInteractability;
        }

        protected override void OnDisable ()
        {
            base.OnDisable();

            gameState.GameSlotManager.OnBeforeLoad -= ControlInteractability;
            gameState.GameSlotManager.OnLoaded -= ControlInteractability;
            gameState.GameSlotManager.OnBeforeSave -= ControlInteractability;
            gameState.GameSlotManager.OnSaved -= ControlInteractability;
        }

        protected override void OnButtonClick ()
        {
            UIComponent.interactable = false;
            QuickLoad();
        }

        private async void QuickLoad ()
        {
            using (await LoadingScreen.Show())
                await gameState.QuickLoad();
        }

        private void ControlInteractability (string _)
        {
            UIComponent.interactable = gameState.QuickLoadAvailable && !gameState.GameSlotManager.Loading && !gameState.GameSlotManager.Saving;
        }
    }
}
