using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Async;

[Token(Token = "0x200060A")]
public static class AwaiterStatusExtensions
{
	[MethodImpl(256)]
	[Token(Token = "0x6001E55")]
	[Address(RVA = "0x4C7850", Offset = "0x4C6250", VA = "0x1804C7850")]
	public static bool IsCompleted(this AwaiterStatus status)
	{
		return default(bool);
	}

	[MethodImpl(256)]
	[Token(Token = "0x6001E56")]
	[Address(RVA = "0x4C7860", Offset = "0x4C6260", VA = "0x1804C7860")]
	public static bool IsCompletedSuccessfully(this AwaiterStatus status)
	{
		return default(bool);
	}

	[MethodImpl(256)]
	[Token(Token = "0x6001E57")]
	[Address(RVA = "0x4C7870", Offset = "0x4C6270", VA = "0x1804C7870")]
	public static bool IsCanceled(this AwaiterStatus status)
	{
		return default(bool);
	}

	[MethodImpl(256)]
	[Token(Token = "0x6001E58")]
	[Address(RVA = "0x4C7880", Offset = "0x4C6280", VA = "0x1804C7880")]
	public static bool IsFaulted(this AwaiterStatus status)
	{
		return default(bool);
	}
}
