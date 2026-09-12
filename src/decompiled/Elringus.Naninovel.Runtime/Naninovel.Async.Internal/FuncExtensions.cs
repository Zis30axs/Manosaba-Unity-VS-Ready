using System;
using Il2CppDummyDll;

namespace Naninovel.Async.Internal;

[Token(Token = "0x2000638")]
internal static class FuncExtensions
{
	[Token(Token = "0x6001EA4")]
	internal static Action<T> AsFuncOfT<T>(this Action action)
	{
		return null;
	}

	[Token(Token = "0x6001EA5")]
	private static void Invoke<T>(this Action action, T unused)
	{
	}
}
