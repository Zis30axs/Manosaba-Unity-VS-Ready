using System;
using Il2CppDummyDll;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003F0")]
public class DecimalParameter : CommandParameter<float>
{
	[Token(Token = "0x60012FA")]
	[Address(RVA = "0x5A6DD0", Offset = "0x5A57D0", VA = "0x1805A6DD0")]
	public static implicit operator DecimalParameter(float value)
	{
		return null;
	}

	[Token(Token = "0x60012FB")]
	[Address(RVA = "0x5A6E80", Offset = "0x5A5880", VA = "0x1805A6E80")]
	public static implicit operator float?(DecimalParameter param)
	{
		return null;
	}

	[Token(Token = "0x60012FC")]
	[Address(RVA = "0x5A6F40", Offset = "0x5A5940", VA = "0x1805A6F40")]
	public static implicit operator DecimalParameter(NullableFloat value)
	{
		return null;
	}

	[Token(Token = "0x60012FD")]
	[Address(RVA = "0x5A7010", Offset = "0x5A5A10", VA = "0x1805A7010")]
	public static implicit operator NullableFloat(DecimalParameter param)
	{
		return null;
	}

	[Token(Token = "0x60012FE")]
	[Address(RVA = "0x5A71F0", Offset = "0x5A5BF0", VA = "0x1805A71F0", Slot = "23")]
	protected override float ParseRaw(RawValue raw, out string errors)
	{
		errors = default;
		errors = default;
		return default(float);
	}

	[Token(Token = "0x60012FF")]
	[Address(RVA = "0x5A7290", Offset = "0x5A5C90", VA = "0x1805A7290")]
	public DecimalParameter()
	{
	}
}
