using System;
using System.Collections.Generic;
using UnityEngine;

namespace Naninovel
{
    [EditInProjectSettings]
    public class UIConfiguration : Configuration
    {
        [Serializable]
        public class FontOption
        {
            [Tooltip("Name of the font option. Will be displayed in the font settings dropdown list.")]
            public string FontName;
            [Tooltip("Resource path to the font asset to apply for the affected text components when the option is selected.")]
            public string FontResource;
            [LocalesPopup(true), Tooltip("When not empty, will auto-apply the font when the locale (language) is selected.")]
            public string ApplyOnLocale = string.Empty;
        }

        public const string DefaultUIPathPrefix = "UI";
        public const string DefaultFontPathPrefix = "Fonts";

        [Tooltip("Configuration of the resource loader used with UI resources.")]
        public ResourceLoaderConfiguration UILoader = new() { PathPrefix = DefaultUIPathPrefix };
        [Tooltip("Configuration of the resource loader used with font resources.")]
        public ResourceLoaderConfiguration FontLoader = new() { PathPrefix = DefaultFontPathPrefix };
        [Tooltip("Whether to assign a specific layer to all the UI objects managed by the engine. Required for some of the built-in features, eg `Toggle UI`.")]
        public bool OverrideObjectsLayer = true;
        [Tooltip("When `Override Objects Layer` is enabled, the specified layer will be assigned to all the managed UI objects.")]
        public int ObjectsLayer = 5;
        [Tooltip("Font options, that should be available in the game settings UI (in addition to `Default`) for the player to choose from.")]
        public List<FontOption> FontOptions = new();
        [Tooltip("Font name from `Font Options` to apply by default when the game is first started. When not specified, `Default` font is applied.")]
        public string DefaultFont;

        /// <summary>
        /// Returns a font option with the specified name or null, when not found.
        /// </summary>
        public FontOption GetFontOption (string fontName) => FontOptions?.Find(fo => fo.FontName == fontName);
    }
}
