using UnityEngine;

namespace Naninovel
{
    [EditInProjectSettings]
    public class ScriptPlayerConfiguration : Configuration
    {
        [Tooltip("Default skip mode to set when the game is first started.")]
        public PlayerSkipMode DefaultSkipMode = PlayerSkipMode.ReadOnly;
        [Tooltip("Time scale to use when in skip (fast-forward) mode."), Range(1f, 100f)]
        public float SkipTimeScale = 10f;
        [Tooltip("Minimum seconds to wait before executing next command while in auto play mode.")]
        public float MinAutoPlayDelay = 1f;
        [Tooltip("Whether to instantly complete blocking (`wait!`) commands performed over time (eg, animations, hide/reveal, tint changes, etc) when `Continue` input is activated.")]
        public bool CompleteOnContinue = true;
        [Tooltip("Whether to show player debug window on engine initialization.")]
        public bool ShowDebugOnInit;
        [Tooltip("Whether to wait the played commands when the `wait` parameter is not explicitly specified. Only applicable to the awaitable (asynchronous) commands." +
                 "\n\nWARNING: Don't enable in new projects, as this option is kept for backward compatibility and will be removed in the next release.")]
        public bool WaitByDefault;
        [Tooltip("Whether to show `ILoadingUI` while a script is pre-/loaded. Allows masking resource loading process with the loading screen.")]
        public bool ShowLoadingUI = true;
        [Tooltip("The mode in which script player handles missing playback spots when loading state. This may happen when player saved a game amidst a scenario script, which is then changed (eg, via a game update) and the saved playback spot (line and inline indexes) is no longer available." +
                 "\n • Nearest — Attempt to play from the nearest next spot; in case no next spots found (script was made shorter), start from nearest previous one. Most forgiving mode, but could cause an undefined behaviour." +
                 "\n • Restart — Start playing current script from start. Won't cause undefined behaviour in case all the state is local to scenario script, but player will have to re-play the script form the start." +
                 "\n • Error — Throw an error. Choose this option in case undefined behaviour is not acceptable.")]
        public PlayerResolveMode ResolveMode = PlayerResolveMode.Nearest;
    }
}
