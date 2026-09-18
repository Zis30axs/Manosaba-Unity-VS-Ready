using System.Collections.Generic;
using System.Linq;

namespace Naninovel.UI
{
    public class GameSettingsLanguageDropdown : ScriptableDropdown
    {
        private IReadOnlyList<string> availableLocales;
        private ILocalizationManager l10n;
        private ICommunityLocalization communityL10n;
        private ITextManager docs;

        protected override void Awake ()
        {
            base.Awake();

            docs = Engine.GetServiceOrErr<ITextManager>();
            l10n = Engine.GetServiceOrErr<ILocalizationManager>();
            communityL10n = Engine.GetServiceOrErr<ICommunityLocalization>();
            l10n.OnLocaleChanged += HandleLocaleChanged;
            availableLocales = GetAvailableLocales();
            if (Engine.Initialized) InitializeOptions();
            else Engine.OnInitializationFinished += InitializeOptions;
        }

        protected virtual string[] GetAvailableLocales ()
        {
            if (communityL10n.Active) return new[] { communityL10n.Author };
            return l10n.AvailableLocales.ToArray();
        }

        protected virtual void InitializeOptions ()
        {
            Engine.OnInitializationFinished -= InitializeOptions;
            UIComponent.ClearOptions();
            UIComponent.AddOptions(availableLocales.Select(GetLabelForLocale).ToList());
            UpdateSelectedLocale(l10n.SelectedLocale);
        }

        protected virtual void UpdateSelectedLocale (string locale)
        {
            UIComponent.value = availableLocales.IndexOf(locale);
            UIComponent.RefreshShownValue();
        }

        protected virtual string GetLabelForLocale (string locale)
        {
            if (communityL10n.Active) return locale;
            var localized = docs.GetRecordValue(locale, ManagedTextPaths.Locales);
            if (!string.IsNullOrWhiteSpace(localized)) return localized;
            return Languages.GetNameByTag(locale);
        }

        protected override void OnValueChanged (int value)
        {
            var selectedLocale = availableLocales[value];
            l10n.SelectLocale(selectedLocale);
        }

        protected virtual void HandleLocaleChanged (LocaleChangedArgs _)
        {
            InitializeOptions();
        }
    }
}
