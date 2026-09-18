namespace Naninovel.UI
{
    public class GameSettingsReturnButton : ScriptableButton
    {
        private GameSettingsMenu menu;

        protected override void Awake ()
        {
            base.Awake();

            menu = GetComponentInParent<GameSettingsMenu>();
        }

        protected override void OnButtonClick ()
        {
            menu.SaveSettingsAndHide().Forget();
        }
    }
}
