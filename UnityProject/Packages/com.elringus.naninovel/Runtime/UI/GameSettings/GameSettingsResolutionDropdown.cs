using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel.UI
{
    public class GameSettingsResolutionDropdown : ScriptableDropdown
    {
        private bool allowApplySettings;

        protected override void Start ()
        {
            base.Start();

            #if !UNITY_STANDALONE && !UNITY_EDITOR
            transform.parent.gameObject.SetActive(false);
            #else
            InitializeOptions(Screen.resolutions.Select(r => r.ToString()).ToList());
            #endif
        }

        protected override void OnValueChanged (int value)
        {
            if (!allowApplySettings) return; // Prevent changing resolution when UI initializes.
            var resolution = Screen.resolutions[value];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio);
        }

        private void InitializeOptions (List<string> availableOptions)
        {
            UIComponent.ClearOptions();
            UIComponent.AddOptions(availableOptions);
            UIComponent.value = GetCurrentResolutionIndex();
            UIComponent.RefreshShownValue();
            allowApplySettings = true;
        }

        /// <summary>
        /// Finds index of the closest to the real (current) available (native to display) resolution.
        /// </summary>
        private int GetCurrentResolutionIndex ()
        {
            if (Screen.resolutions.Length == 0) return 0;

            var currentResolution = new Resolution { width = Screen.width, height = Screen.height, refreshRateRatio = Screen.currentResolution.refreshRateRatio };
            var closestResolution = Screen.resolutions.Aggregate((x, y) => ResolutionDiff(x, currentResolution) < ResolutionDiff(y, currentResolution) ? x : y);
            // ReSharper disable once UsageOfDefaultStructEquality
            return Array.IndexOf(Screen.resolutions, closestResolution);

            int ResolutionDiff (Resolution a, Resolution b) =>
                Mathf.Abs(a.width - b.width) + Mathf.Abs(a.height - b.height) +
                Mathf.Abs((int)a.refreshRateRatio.value - (int)b.refreshRateRatio.value);
        }
    }
}
