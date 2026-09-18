
namespace Naninovel.UI
{
    public class TitleContinueButton : ScriptableButton
    {
        private IStateManager stateManager;
        private IUIManager uiManager;

        protected override void Awake ()
        {
            base.Awake();

            stateManager = Engine.GetServiceOrErr<IStateManager>();
            uiManager = Engine.GetServiceOrErr<IUIManager>();
        }

        protected override void Start ()
        {
            base.Start();

            ControlInteractability(default);
        }

        protected override void OnEnable ()
        {
            base.OnEnable();

            stateManager.GameSlotManager.OnSaved += ControlInteractability;
            stateManager.GameSlotManager.OnDeleted += ControlInteractability;
        }

        protected override void OnDisable ()
        {
            base.OnDisable();

            stateManager.GameSlotManager.OnSaved -= ControlInteractability;
            stateManager.GameSlotManager.OnDeleted -= ControlInteractability;
        }

        protected override void OnButtonClick ()
        {
            var saveLoadUI = uiManager.GetUI<ISaveLoadUI>();
            if (saveLoadUI is null) return;

            var lastLoadMode = saveLoadUI.GetLastLoadMode();
            saveLoadUI.PresentationMode = lastLoadMode;
            saveLoadUI.Show();
        }

        private void ControlInteractability (string _) => UIComponent.interactable = stateManager.AnyGameSaveExists;
    }
}
