using GigaCreation.Tools.Service;
using GigaCreation.Tools.Debugging.Core;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.Debugging.General
{
    [Token(Token = "0x2000006")]
    public class DestroySelfIfReleaseBuild : MonoBehaviour
    {
    	// RECONSTRUCTED | Confidence: A | RVA: 0xD43200, 228 B -- ServiceLocator.IsRegistered<IDebugManager>() (0x180751D40) false
    	//   -> Object.Destroy(gameObject) (tail call).  Only DebugPresenter registers the manager, and only in a debug build:
    	//   the stub left the Game prefab's DebugCanvas (the debug command side bar) alive in every build.
    	// Annotated disassembly: work/title-path/disas/dsirb_01_Start.txt
    	[Token(Token = "0x600000C")]
    	[Address(RVA = "0xD43200", Offset = "0xD41C00", VA = "0x180D43200")]
    	private void Start()
    	{
    		if (!ServiceLocator.IsRegistered<IDebugManager>())
    		{
    			Destroy(gameObject);
    		}
    	}

    	[Token(Token = "0x600000D")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DestroySelfIfReleaseBuild()
    	{
    	}
    }
}
