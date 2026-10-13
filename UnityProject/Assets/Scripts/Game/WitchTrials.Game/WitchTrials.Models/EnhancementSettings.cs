using System;
using Naninovel;
using UnityEngine.Scripting;

namespace WitchTrials.Models
{
    /// <summary>User presentation preferences, persisted with settings rather than story saves.</summary>
    [Preserve, InitializeAtRuntime]
    public sealed class EnhancementSettings : IStatefulService<SettingsStateMap>
    {
        [Serializable, Preserve]
        public sealed class Settings
        {
            public bool UseCorrectedCutInPortraits;
            public bool FireworksEpilogueEnabled = true;
        }

        public event Action Changed;

        public bool UseCorrectedCutInPortraits
        {
            get => useCorrectedCutInPortraits;
            set => Apply(value, fireworksEpilogueEnabled);
        }

        public bool FireworksEpilogueEnabled
        {
            get => fireworksEpilogueEnabled;
            set => Apply(useCorrectedCutInPortraits, value);
        }

        private bool useCorrectedCutInPortraits;
        private bool fireworksEpilogueEnabled = true;

        public UniTask InitializeService() => UniTask.CompletedTask;

        // Engine resets accompany new games, loads and returning to title. Preferences survive them.
        public void ResetService() { }

        public void DestroyService() => Changed = null;

        public void ResetDefaults() => Apply(false, true);

        public void SaveServiceState(SettingsStateMap stateMap)
        {
            stateMap.SetState(new Settings {
                UseCorrectedCutInPortraits = useCorrectedCutInPortraits,
                FireworksEpilogueEnabled = fireworksEpilogueEnabled
            });
        }

        public UniTask LoadServiceState(SettingsStateMap stateMap)
        {
            var settings = stateMap.GetState<Settings>();
            Apply(settings?.UseCorrectedCutInPortraits ?? false, settings?.FireworksEpilogueEnabled ?? true);
            return UniTask.CompletedTask;
        }

        private void Apply(bool correctedPortraits, bool epilogueEnabled)
        {
            if (useCorrectedCutInPortraits == correctedPortraits && fireworksEpilogueEnabled == epilogueEnabled) return;
            useCorrectedCutInPortraits = correctedPortraits;
            fireworksEpilogueEnabled = epilogueEnabled;
            Changed?.Invoke();
        }
    }
}
