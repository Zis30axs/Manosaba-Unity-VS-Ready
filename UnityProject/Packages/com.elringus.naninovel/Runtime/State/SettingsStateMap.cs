using System;
using System.Linq;

namespace Naninovel
{
    /// <summary>
    /// Serializable state of the engine service's settings.
    /// </summary>
    [Serializable]
    public class SettingsStateMap : StateMap
    {
        /// <inheritdoc cref="StateMap.With"/>
        public static SettingsStateMap With (params object[] records) =>
            StateMap.With<SettingsStateMap>(records.Select(r => (r, default(string))).ToArray());
    }
}
