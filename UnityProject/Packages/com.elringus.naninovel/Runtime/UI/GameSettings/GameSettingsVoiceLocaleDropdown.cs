using System.Collections.Generic;
using System.Linq;

namespace Naninovel.UI
{
    public class GameSettingsVoiceLocaleDropdown : ScriptableDropdown
    {
        private readonly List<string> options = new();
        private IAudioManager audios;
        private ITextManager docs;
        private ILocalizationManager l10n;

        protected override void Awake ()
        {
            base.Awake();

            audios = Engine.GetServiceOrErr<IAudioManager>();
            docs = Engine.GetServiceOrErr<ITextManager>();
            l10n = Engine.GetServiceOrErr<ILocalizationManager>();
            l10n.OnLocaleChanged += HandleLocaleChanged;
            if (Engine.Initialized) InitializeOptions();
            else Engine.OnInitializationFinished += InitializeOptions;
        }

        protected override void OnValueChanged (int value)
        {
            var selectedLocale = options[value];
            audios.VoiceLocale = selectedLocale;
        }

        protected virtual string GetLabelForLocale (string locale)
        {
            var localized = docs.GetRecordValue(locale, ManagedTextPaths.Locales);
            if (!string.IsNullOrWhiteSpace(localized)) return localized;
            return Languages.GetNameByTag(locale);
        }

        protected virtual void InitializeOptions ()
        {
            Engine.OnInitializationFinished -= InitializeOptions;
            options.Clear();
            var voiceLocales = Engine.GetConfiguration<AudioConfiguration>().VoiceLocales;
            if (voiceLocales?.Count > 0) options.AddRange(voiceLocales);
            else
            {
                transform.parent.gameObject.SetActive(false);
                return;
            }

            UIComponent.ClearOptions();
            UIComponent.AddOptions(voiceLocales.Select(GetLabelForLocale).ToList());
            UIComponent.value = options.IndexOf(audios.VoiceLocale);
            UIComponent.RefreshShownValue();
        }

        protected virtual void HandleLocaleChanged (LocaleChangedArgs _)
        {
            InitializeOptions();
        }
    }
}
