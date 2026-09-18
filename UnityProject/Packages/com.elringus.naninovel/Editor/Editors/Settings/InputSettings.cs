using System;
using System.Collections.Generic;
using UnityEditor;

namespace Naninovel
{
    public class InputSettings : ConfigurationSettings<InputConfiguration>
    {
        #if ENABLE_INPUT_SYSTEM && INPUT_SYSTEM_AVAILABLE
        private bool newInput => true;
        #else
        private bool newInput => false;
        #endif

        protected override Dictionary<string, Action<SerializedProperty>> OverrideConfigurationDrawers ()
        {
            var drawers = base.OverrideConfigurationDrawers();
            drawers[nameof(InputConfiguration.CustomEventSystem)] = p => DrawWhen(Configuration.SpawnEventSystem, p);
            drawers[nameof(InputConfiguration.CustomInputModule)] = p => DrawWhen(Configuration.SpawnInputModule, p);
            drawers[nameof(InputConfiguration.DetectInputMode)] = p => DrawWhen(newInput, p);
            drawers[nameof(InputConfiguration.TouchFrequencyLimit)] = p => DrawWhen(Configuration.ProcessLegacyBindings, p);
            drawers[nameof(InputConfiguration.TouchDistanceLimit)] = p => DrawWhen(Configuration.ProcessLegacyBindings, p);
            return drawers;
        }
    }
}
