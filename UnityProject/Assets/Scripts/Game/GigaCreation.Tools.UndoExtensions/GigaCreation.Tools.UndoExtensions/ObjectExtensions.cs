using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.UndoExtensions
{
    [Token(Token = "0x2000005")]
    public static class ObjectExtensions
    {
    	// RECONSTRUCTED | Confidence: A | RVA: 0xD5D180, 132 B -- tail jump to UnityEngine.Object.Destroy(self, 0f) (the
    	//   player build: the editor-only Undo.DestroyObjectImmediate path is compiled out)
    	// Annotated disassembly: work/title-path/disas/ue_01_DestroyAsUndoable.txt
    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xD5D180", Offset = "0xD5BB80", VA = "0x180D5D180")]
    	public static void DestroyAsUndoable(this UnityEngine.Object self)
    	{
    		UnityEngine.Object.Destroy(self);
    	}

    	// RECONSTRUCTED | Confidence: A | shared generic instance DoActionAsUndoable<object> RVA: 0x7239F0, 67 B -- invokes
    	//   undoableAction(self) (Action<T>.Invoke through the delegate's method pointer; a null delegate throws
    	//   NullReferenceException).  The player build records no Undo; every call site the reconstruction has met inlines
    	//   exactly this invoke (RebuildableLabel.SetInternalData 0x180CE4AD3, ModifyLabel, InsertTexts, ...).
    	//   The skeleton's empty body silently dropped every action routed through it (MapArea's focus button / label,
    	//   the rebuildable labels' text, size and colour).
    	// Annotated disassembly: work/title-path/disas/ue_02_DoActionAsUndoable.txt
    	[Token(Token = "0x6000005")]
    	public static void DoActionAsUndoable<T>(this T self, string name, Action<T> undoableAction) where T : UnityEngine.Object
    	{
    		undoableAction(self);
    	}
    }
}
