using System;
using Il2CppDummyDll;

namespace Naninovel.Async.Internal;

[Token(Token = "0x2000642")]
internal static class RuntimeHelpersAbstraction
{
	[Token(Token = "0x2000643")]
	private static class WellKnownNoReferenceContainsType<T>
	{
		[Token(Token = "0x40017E2")]
		[FieldOffset(Offset = "0x0")]
		public static readonly bool IsWellKnownType;

		[Token(Token = "0x6001EFC")]
		static WellKnownNoReferenceContainsType()
		{
		}
	}

	[Token(Token = "0x6001EFA")]
	public static bool IsWellKnownNoReferenceContainsType<T>()
	{
		return default(bool);
	}

	[Token(Token = "0x6001EFB")]
	[Address(RVA = "0x4CE610", Offset = "0x4CD010", VA = "0x1804CE610")]
	private static bool WellKnownNoReferenceContainsTypeInitialize(Type t)
	{
		return default(bool);
	}
}
