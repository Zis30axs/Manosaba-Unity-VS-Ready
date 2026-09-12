using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Metadata;

namespace Naninovel;

[Token(Token = "0x20000E2")]
public abstract class ParameterContextAttribute : Attribute
{
	[Token(Token = "0x400034A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	public readonly ValueContextType Type;

	[Token(Token = "0x400034B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	public readonly string SubType;

	[Token(Token = "0x400034C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	public readonly string ParameterId;

	[Token(Token = "0x400034D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	public readonly int Index;

	[Token(Token = "0x600041E")]
	[Address(RVA = "0x4772A0", Offset = "0x475CA0", VA = "0x1804772A0")]
	protected ParameterContextAttribute(ValueContextType type = default, string subType = default, int index = -1, string paramId = default)
	{
	}
}
