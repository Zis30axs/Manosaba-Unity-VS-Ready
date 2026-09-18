using System.Collections.Generic;
using UnityEngine;

namespace Naninovel.UI
{
    public class GameSettingsScreenModeDropdown : ScriptableDropdown
    {
        [ManagedText("DefaultUI")]
        protected static string ExclusiveFullScreen = "Full Screen";
        [ManagedText("DefaultUI")]
        protected static string FullScreenWindow = "Full Screen Window";
        [ManagedText("DefaultUI")]
        protected static string MaximizedWindow = "Maximized Window";
        [ManagedText("DefaultUI")]
        protected static string Windowed = "Windowed";

        private readonly Dictionary<int, FullScreenMode> indexToMode = new();
        private readonly List<string> options = new();
        private bool allowApplySettings;

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

        protected override void Start ()
        {
            base.Start();

            allowApplySettings = true;
        }

        protected override void OnValueChanged (int value)
        {
            if (!allowApplySettings) return; // Prevent changing resolution when UI initializes.
            Screen.SetResolution(Screen.width, Screen.height, indexToMode[value], Screen.currentResolution.refreshRateRatio);
        }

        protected virtual void MapOptions ()
        {
            indexToMode.Clear();
            options.Clear();

            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
                MapOption(ExclusiveFullScreen, FullScreenMode.ExclusiveFullScreen);
            MapOption(FullScreenWindow, FullScreenMode.FullScreenWindow);
            if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor)
                MapOption(MaximizedWindow, FullScreenMode.MaximizedWindow);
            MapOption(Windowed, FullScreenMode.Windowed);
        }

        protected virtual void InitializeOptions ()
        {
            #if !UNITY_STANDALONE && !UNITY_EDITOR
            transform.parent.gameObject.SetActive(false);
            #else
            MapOptions();
            UIComponent.ClearOptions();
            UIComponent.AddOptions(options);
            UIComponent.value = (int)Screen.fullScreenMode;
            UIComponent.RefreshShownValue();
            #endif
        }

        protected virtual void HandleLocaleChanged (LocaleChangedArgs _) => InitializeOptions();

        protected virtual void MapOption (string option, FullScreenMode mode)
        {
            indexToMode[options.Count] = mode;
            options.Add(option);
        }
    }
}
