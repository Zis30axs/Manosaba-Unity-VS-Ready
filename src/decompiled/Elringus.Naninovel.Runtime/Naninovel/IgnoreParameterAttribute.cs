using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20000D7")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true)]
public sealed class IgnoreParameterAttribute : Attribute
{
	[Token(Token = "0x4000347")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	public readonly string ParameterId;

	[Token(Token = "0x6000412")]
	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
	public IgnoreParameterAttribute([Optional] string paramId)
	{
	}
}
