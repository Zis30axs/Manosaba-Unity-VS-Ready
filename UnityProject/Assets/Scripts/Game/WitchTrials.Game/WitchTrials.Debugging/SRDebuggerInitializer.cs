using GigaCreation.Tools.Debugging.Core;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Debugging
{
    [Token(Token = "0x20001C3")]
    public class SRDebuggerInitializer : MonoBehaviour
    {
    	[Token(Token = "0x400085E")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private bool _enableInEditor;

    	// RECONSTRUCTED | Confidence: A (bytes), B (source intent) | RVA: 0x3B34A0, 263 B
    	// 
    	// - `_enableInEditor` (+0x20) is tested and the Application class is initialized, but
    	//   `Application.get_isEditor` is the 5-byte `xor al, al; ret` in the player build (0x74B0C0).
    	//   The editor early-return therefore folds away.
    	// - `ServiceLocator.TryGet<IDebugManager>(out, includeDerivedType: false)`, then slot 0 `IsDebugMode` and
    	//   IReactiveProperty<bool> slot 0 `get_Value`. The result is never tested, which is what an `if`
    	//   whose body was compiled out (e.g. a `[Conditional]` SRDebugger init call) leaves behind.
    	[Token(Token = "0x60007AD")]
    	[Address(RVA = "0x3B34A0", Offset = "0x3B1EA0", VA = "0x1803B34A0")]
    	private void Awake()
    	{
    		if (!_enableInEditor && Application.isEditor)
    		{
    			return;
    		}
    		if (ServiceLocator.TryGet(out IDebugManager debugManager) && debugManager.IsDebugMode.Value)
    		{
    		}
    	}

    	[Token(Token = "0x60007AE")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public SRDebuggerInitializer()
    	{
    	}
    }
}
