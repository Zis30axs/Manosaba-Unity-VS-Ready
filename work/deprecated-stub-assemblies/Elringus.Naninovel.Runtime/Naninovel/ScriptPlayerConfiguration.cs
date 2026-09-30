using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200044C")]
    [EditInProjectSettings]
    public class ScriptPlayerConfiguration : Configuration
    {
    	[Token(Token = "0x4000FF9")]
    	[FieldOffset(Offset = "0x18")]
    	[Tooltip("Default skip mode to set when the game is first started.")]
    	public PlayerSkipMode DefaultSkipMode;

    	[Token(Token = "0x4000FFA")]
    	[FieldOffset(Offset = "0x1C")]
    	[Tooltip("Time scale to use when in skip (fast-forward) mode.")]
    	[Range(1f, 100f)]
    	public float SkipTimeScale;

    	[Token(Token = "0x4000FFB")]
    	[FieldOffset(Offset = "0x20")]
    	[Tooltip("Minimum seconds to wait before executing next command while in auto play mode.")]
    	public float MinAutoPlayDelay;

    	[Token(Token = "0x4000FFC")]
    	[FieldOffset(Offset = "0x24")]
    	[Tooltip("Whether to instantly complete blocking (`wait!`) commands performed over time (eg, animations, hide/reveal, tint changes, etc) when `Continue` input is activated.")]
    	public bool CompleteOnContinue;

    	[Token(Token = "0x4000FFD")]
    	[FieldOffset(Offset = "0x25")]
    	[Tooltip("Whether to show player debug window on engine initialization.")]
    	public bool ShowDebugOnInit;

    	[Token(Token = "0x4000FFE")]
    	[FieldOffset(Offset = "0x26")]
    	[Tooltip("Whether to wait the played commands when the `wait` parameter is not explicitly specified. Only applicable to the awaitable (asynchronous) commands.\n\nWARNING: Don't enable in new projects, as this option is kept for backward compatibility and will be removed in the next release.")]
    	public bool WaitByDefault;

    	[Token(Token = "0x4000FFF")]
    	[FieldOffset(Offset = "0x27")]
    	[Tooltip("Whether to show `ILoadingUI` while a script is pre-/loaded. Allows masking resource loading process with the loading screen.")]
    	public bool ShowLoadingUI;

    	[Token(Token = "0x4001000")]
    	[FieldOffset(Offset = "0x28")]
    	[Tooltip("The mode in which script player handles missing playback spots when loading state. This may happen when player saved a game amidst a scenario script, which is then changed (eg, via a game update) and the saved playback spot (line and inline indexes) is no longer available.\n • Nearest — Attempt to play from the nearest next spot; in case no next spots found (script was made shorter), start from nearest previous one. Most forgiving mode, but could cause an undefined behaviour.\n • Restart — Start playing current script from start. Won't cause undefined behaviour in case all the state is local to scenario script, but player will have to re-play the script form the start.\n • Error — Throw an error. Choose this option in case undefined behaviour is not acceptable.")]
    	public PlayerResolveMode ResolveMode;

    	[Token(Token = "0x6001541")]
    	[Address(RVA = "0x5CE2A0", Offset = "0x5CCCA0", VA = "0x1805CE2A0")]
    	public ScriptPlayerConfiguration()
    	{
    	}
    }
}
