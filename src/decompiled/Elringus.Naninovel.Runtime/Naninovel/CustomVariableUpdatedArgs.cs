using System;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000331")]
public readonly struct CustomVariableUpdatedArgs : IEquatable<CustomVariableUpdatedArgs>
{
	[Token(Token = "0x4000BE9")]
	[FieldOffset(Offset = "0x0")]
	public readonly string Name;

	[Token(Token = "0x4000BEA")]
	[FieldOffset(Offset = "0x8")]
	public readonly CustomVariableValue? Value;

	[Token(Token = "0x4000BEB")]
	[FieldOffset(Offset = "0x28")]
	public readonly CustomVariableValue? InitialValue;

	[Token(Token = "0x6000F3D")]
	[Address(RVA = "0x563680", Offset = "0x562080", VA = "0x180563680")]
	public CustomVariableUpdatedArgs(string name, CustomVariableValue? value, CustomVariableValue? initialValue)
	{
	}

	[Token(Token = "0x6000F3E")]
	[Address(RVA = "0x5637A0", Offset = "0x5621A0", VA = "0x1805637A0", Slot = "4")]
	public bool Equals(CustomVariableUpdatedArgs other)
	{
		return default(bool);
	}

	[Token(Token = "0x6000F3F")]
	[Address(RVA = "0x563910", Offset = "0x562310", VA = "0x180563910", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x6000F40")]
	[Address(RVA = "0x5639D0", Offset = "0x5623D0", VA = "0x1805639D0", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}
}
