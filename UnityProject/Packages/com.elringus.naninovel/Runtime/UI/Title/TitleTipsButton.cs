namespace Naninovel.UI
{
    public class TitleTipsButton : ScriptableButton
    {
        protected override void Awake ()
        {
            base.Awake();

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
            Engine.GetServiceOrErr<IUIManager>().GetUI<ITipsUI>()?.Show();
        }

        protected virtual void DisableIfNoTips ()
        {
            var ui = Engine.GetServiceOrErr<IUIManager>().GetUI<ITipsUI>();
            gameObject.SetActive(ui != null && ui.TipsCount > 0);
        }
    }
}
