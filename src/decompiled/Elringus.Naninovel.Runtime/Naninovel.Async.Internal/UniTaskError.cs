using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Async.Internal;

[Token(Token = "0x2000637")]
internal static class UniTaskError
{
	[MethodImpl(256)]
	[Token(Token = "0x6001E9C")]
	public static void ThrowArgumentNullException<T>(T value, string paramName) where T : class
	{
	}

	[MethodImpl(8)]
	[Token(Token = "0x6001E9D")]
	[Address(RVA = "0x4CC960", Offset = "0x4CB360", VA = "0x1804CC960")]
	private static void ThrowArgumentNullExceptionCore(string paramName)
	{
	}

	[MethodImpl(8)]
	[Token(Token = "0x6001E9E")]
	public static void ThrowArgumentException<T>(string message)
	{
	}

	[MethodImpl(8)]
	[Token(Token = "0x6001E9F")]
	[Address(RVA = "0x4CC9B0", Offset = "0x4CB3B0", VA = "0x1804CC9B0")]
	public static void ThrowNotYetCompleted()
	{
	}

	[MethodImpl(8)]
	[Token(Token = "0x6001EA0")]
	public static T ThrowNotYetCompleted<T>()
	{
		return default(T);
	}

	[MethodImpl(256)]
	[Token(Token = "0x6001EA1")]
	public static void ThrowWhenContinuationIsAlreadyRegistered<T>(T continuationField) where T : class
	{
	}

	[MethodImpl(8)]
	[Token(Token = "0x6001EA2")]
	[Address(RVA = "0x4CCA00", Offset = "0x4CB400", VA = "0x1804CCA00")]
	private static void ThrowInvalidOperationExceptionCore(string message)
	{
	}

	[MethodImpl(8)]
	[Token(Token = "0x6001EA3")]
	[Address(RVA = "0x4CCA50", Offset = "0x4CB450", VA = "0x1804CCA50")]
	public static void ThrowOperationCanceledException()
	{
	}
}
