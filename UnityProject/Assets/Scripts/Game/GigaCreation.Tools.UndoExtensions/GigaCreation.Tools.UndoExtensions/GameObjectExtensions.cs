using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.UndoExtensions
{
    [Token(Token = "0x2000004")]
    public static class GameObjectExtensions
    {
    	// RECONSTRUCTED | Confidence: A | shared generic instance AddComponentAsUndoable<object> RVA: 0x6B7E00 -- tail jump to
    	//   GameObject.AddComponent<T>(self) (0x6B68F0); the player build records no Undo (a null self throws
    	//   NullReferenceException at 0x1806B7E3D)
    	// Annotated disassembly: work/title-path/disas/ue_03_AddComponentAsUndoable.txt
    	[Token(Token = "0x6000003")]
    	public static T AddComponentAsUndoable<T>(this GameObject self) where T : Component
    	{
    		return self.AddComponent<T>();
    	}
    }
}
