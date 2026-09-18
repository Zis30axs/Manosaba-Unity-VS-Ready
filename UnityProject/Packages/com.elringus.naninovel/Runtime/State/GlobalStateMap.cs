using System;
using System.Linq;

namespace Naninovel
{
    /// <summary>
    /// Represents serializable global state of the engine services.
    /// </summary>
    [Serializable]
    public class GlobalStateMap : StateMap
    {
        /// <inheritdoc cref="StateMap.With"/>
        public static GlobalStateMap With (params object[] records) =>
            StateMap.With<GlobalStateMap>(records.Select(r => (r, default(string))).ToArray());
    }
}
