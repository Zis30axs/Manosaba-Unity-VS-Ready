using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.UndoExtensions;

[Token(Token = "0x2000005")]
public static class ObjectExtensions
{
	[Token(Token = "0x6000004")]
	[Address(RVA = "0xD5D180", Offset = "0xD5BB80", VA = "0x180D5D180")]
	public static void DestroyAsUndoable(this UnityEngine.Object self)
	{
	}

	[Token(Token = "0x6000005")]
	public static void DoActionAsUndoable<T>(this T self, string name, Action<T> undoableAction) where T : UnityEngine.Object
	{
	}
}
