using System.Collections.Generic;
using System.Linq;

namespace Naninovel.UI
{
    public class GameSettingsFontDropdown : ScriptableDropdown
    {
        [ManagedText("DefaultUI")]
        protected static string DefaultFontName = "Default";

        private IUIManager uiManager;
        private ICommunityLocalization communityL10n;

        protected override void Awake ()
        {
            base.Awake();

            uiManager = Engine.GetServiceOrErr<IUIManager>();
            communityL10n = Engine.GetServiceOrErr<ICommunityLocalization>();
        }

        protected override void OnEnable ()
        {
            base.OnEnable();

            InitializeOptions();

            if (Engine.TryGetService<ILocalizationManager>(out var localeManager))
                localeManager.OnLocaleChanged += HandleLocaleChanged;
        }

        protected override void OnDisable ()
        {
            base.OnDisable();

            if (Engine.TryGetService<ILocalizationManager>(out var localeManager))
                localeManager.OnLocaleChanged -= HandleLocaleChanged;
        }

        protected override void OnValueChanged (int value)
        {
            uiManager.FontName = value == 0 ? null : UIComponent.options[value].text;
        }

        private void InitializeOptions ()
        {
            var availableOptions = new List<string> { DefaultFontName };
            if (!communityL10n.Active && uiManager.Configuration.FontOptions?.Count > 0)
                availableOptions.AddRange(uiManager.Configuration.FontOptions.Select(o => o.FontName));
            else transform.parent.gameObject.SetActive(false);

            UIComponent.ClearOptions();
            UIComponent.AddOptions(availableOptions);
            UIComponent.value = GetCurrentIndex();
            UIComponent.RefreshShownValue();
        }

        private int GetCurrentIndex ()
        {
            if (string.IsNullOrEmpty(uiManager.FontName))
                return 0;
            var option = UIComponent.options.Find(o => o.text == uiManager.FontName);
            return UIComponent.options.IndexOf(option);
        }

        private void HandleLocaleChanged (LocaleChangedArgs _) => InitializeOptions();
    }
}
