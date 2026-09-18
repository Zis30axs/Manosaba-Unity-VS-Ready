namespace Naninovel.UI
{
    public class ControlPanelTipsButton : ScriptableLabeledButton
    {
        private IUIManager uiManager;

        protected override void Awake ()
        {
            base.Awake();

            uiManager = Engine.GetServiceOrErr<IUIManager>();
            if (Engine.Initialized) DisableIfNoTips();
            else Engine.OnInitializationFinished += DisableIfNoTips;
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();
            Engine.OnInitializationFinished -= DisableIfNoTips;
        }

        protected override void OnButtonClick ()
        {
            uiManager.GetUI<IPauseUI>()?.Hide();
            uiManager.GetUI<ITipsUI>()?.Show();
        }

        protected virtual void DisableIfNoTips ()
        {
            var ui = uiManager.GetUI<ITipsUI>();
            gameObject.SetActive(ui != null && ui.TipsCount > 0);
        }
    }
}
