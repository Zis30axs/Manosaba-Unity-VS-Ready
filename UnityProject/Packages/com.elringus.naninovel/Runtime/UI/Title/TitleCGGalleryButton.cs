namespace Naninovel.UI
{
    public class TitleCGGalleryButton : ScriptableButton
    {
        protected override void OnButtonClick ()
        {
            Engine.GetServiceOrErr<IUIManager>().GetUI<ICGGalleryUI>()?.Show();
        }

        protected override void Awake ()
        {
            base.Awake();
            if (Engine.Initialized) DisableIfNoCG();
            else Engine.OnInitializationFinished += DisableIfNoCG;
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();
            Engine.OnInitializationFinished -= DisableIfNoCG;
        }

        protected virtual void DisableIfNoCG ()
        {
            var ui = Engine.GetServiceOrErr<IUIManager>().GetUI<ICGGalleryUI>();
            gameObject.SetActive(ui != null && ui.CGCount > 0);
        }
    }
}
