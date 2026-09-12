using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.UndoExtensions;

[Token(Token = "0x2000004")]
public static class GameObjectExtensions
{
	[Token(Token = "0x6000003")]
	public static T AddComponentAsUndoable<T>(this GameObject self) where T : Component
	{
		return null;
	}
}
