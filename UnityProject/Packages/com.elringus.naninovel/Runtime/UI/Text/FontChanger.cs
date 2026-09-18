using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Naninovel.UI
{
    public class FontChanger : MonoBehaviour
    {
        [Tooltip("Setup which game objects should be affected by font and text size changes (set in game settings).")]
        [SerializeField] private CustomUI.FontChangeConfiguration[] configuration;

        private IUIManager uiManager;
        private string lastAppliedFontName;
        private int lastAppliedFontSize = -1;

        public static void InitializeConfiguration (IReadOnlyList<CustomUI.FontChangeConfiguration> configuration)
        {
            if (configuration is null) return;

            for (int i = 0; i < configuration.Count; i++) // Store default fonts and sizes.
            {
                var config = configuration[i];
                if (!config.Object) throw new Error("Failed to initialize font size list: game object is invalid.");
                if (config.IncludeChildren)
                    foreach (var text in config.Object.GetComponentsInChildren<TMP_Text>(true))
                        AddComponent(config, text);
                else if (config.Object.TryGetComponent<TMP_Text>(out var text))
                    AddComponent(config, text);
            }

            void AddComponent (CustomUI.FontChangeConfiguration config, TMP_Text text)
            {
                config.Components.Add(text);
                config.DefaultSizes[text] = (int)text.fontSize;
                config.DefaultFonts[text] = text.font;
            }
        }

        public static void ChangeFont (TMP_FontAsset font,
            IReadOnlyCollection<CustomUI.FontChangeConfiguration> configuration)
        {
            if (configuration is null || configuration.Count == 0) return;

            foreach (var config in configuration)
                if (config.AllowFontChange)
                    foreach (var text in config.Components)
                        text.font = font ? font : config.DefaultFonts[text];
        }

        public static void ChangeFontSize (int dropdownIndex,
            IReadOnlyCollection<CustomUI.FontChangeConfiguration> configuration)
        {
            if (configuration is null || configuration.Count == 0) return;

            foreach (var config in configuration)
                if (config.AllowFontSizeChange)
                    foreach (var text in config.Components)
                        ApplySize(config, text);

            void ApplySize (CustomUI.FontChangeConfiguration config, TMP_Text text)
            {
                if (!config.FontSizes)
                    throw new Error($"Failed to apply font size to {ComponentUtils.FindTopmostComponent<CustomUI>(text).gameObject.name}: " +
                                    "font sizes are not set up in 'Font Change Configuration'.");
                if (dropdownIndex != -1 && !config.FontSizes.Sizes.IsIndexValid(dropdownIndex))
                    throw new Error($"Failed to apply selected font size dropdown index (`{dropdownIndex}`) " +
                                    $"to `{config.Object.name}` UI: index is not available in `Font Sizes` list.");
                text.fontSize = dropdownIndex == -1 ? config.DefaultSizes[text] : config.FontSizes.Sizes[dropdownIndex];
            }
        }

        public virtual void ApplySelectedFont ()
        {
            if (IsFontNameChangePending())
                HandleFontNameChanged(uiManager.FontName);
            if (IsFontSizeChangePending())
                HandleFontSizeChanged(uiManager.FontSize);
        }

        protected virtual void Awake ()
        {
            uiManager = Engine.GetServiceOrErr<IUIManager>();
            InitializeConfiguration(configuration);
        }

        protected virtual void OnEnable ()
        {
            uiManager.OnFontNameChanged += HandleFontNameChanged;
            uiManager.OnFontSizeChanged += HandleFontSizeChanged;
            ApplySelectedFont();
        }

        protected virtual void OnDisable ()
        {
            if (uiManager is null) return;
            uiManager.OnFontNameChanged -= HandleFontNameChanged;
            uiManager.OnFontSizeChanged -= HandleFontSizeChanged;
        }

        protected virtual void HandleFontNameChanged (string fontName)
        {
            var font = string.IsNullOrWhiteSpace(fontName) ? null : uiManager.GetFontAsset(fontName);
            ChangeFont(font, configuration);
            lastAppliedFontName = fontName;
        }

        protected virtual void HandleFontSizeChanged (int sizeIndex)
        {
            ChangeFontSize(sizeIndex, configuration);
            lastAppliedFontSize = sizeIndex;
        }

        protected virtual bool IsFontNameChangePending ()
        {
            if (string.IsNullOrWhiteSpace(lastAppliedFontName))
                return !string.IsNullOrWhiteSpace(uiManager.FontName);
            return lastAppliedFontName != uiManager.FontName;
        }

        protected virtual bool IsFontSizeChangePending ()
        {
            return lastAppliedFontSize != uiManager.FontSize;
        }
    }
}
