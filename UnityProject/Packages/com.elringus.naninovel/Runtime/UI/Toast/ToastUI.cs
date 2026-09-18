using System;
using System.Collections.Generic;
using UnityEngine;

namespace Naninovel.UI
{
    /// <inheritdoc cref="IToastUI"/>
    public class ToastUI : CustomUI, IToastUI, ILocalizableUI
    {
        [Tooltip("The appearance used by default, when 'appearance' parameter is not specified.")]
        [SerializeField] private ToastAppearance defaultAppearance;
        [Tooltip("Seconds to wait before hiding the toast; used by default, when 'duration' parameter is not specified.")]
        [SerializeField] private float defaultDuration = 5f;

        private readonly Dictionary<string, ToastAppearance> appearances = new(StringComparer.OrdinalIgnoreCase);
        private Timer hideTimer;

        public virtual void Show (LocalizableText text, string appearance = default, float? duration = default)
        {
            if (!TrySelectAppearance(appearance, out var selectedAppearance)) return;
            if (hideTimer.Running) hideTimer.Stop();
            selectedAppearance.SetText(text);
            hideTimer.Run(duration ?? defaultDuration, target: this);
            base.Show();
        }

        protected override void Awake ()
        {
            base.Awake();

            this.AssertRequiredObjects(defaultAppearance);

            hideTimer = new(ignoreTimeScale: true, onCompleted: Hide);

            foreach (var appearance in GetComponentsInChildren<ToastAppearance>(true))
                appearances[appearance.name] = appearance;
        }

        protected virtual bool TrySelectAppearance (string appearanceName, out ToastAppearance selectedAppearance)
        {
            var appearanceId = appearanceName ?? defaultAppearance.name;
            if (!appearances.TryGetValue(appearanceId, out selectedAppearance))
            {
                Engine.Err($"Failed to show toast with '{appearanceId}' appearance: the appearance game object is not found under the toast prefab.");
                selectedAppearance = null;
                return false;
            }

            foreach (var toastAppearance in appearances.Values)
                if (toastAppearance != selectedAppearance)
                    toastAppearance.SetSelected(false);
            selectedAppearance.SetSelected(true);

            return true;
        }
    }
}
