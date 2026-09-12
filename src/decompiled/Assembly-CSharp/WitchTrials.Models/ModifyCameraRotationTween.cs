using System;
using System.Runtime.CompilerServices;
using System.Text;
using Il2CppDummyDll;
using LitMotion;

namespace WitchTrials.Models;

[Token(Token = "0x200018E")]
public record ModifyCameraRotationTween
{
	[Token(Token = "0x17000120")]
	[CompilerGenerated]
	protected virtual Type EqualityContract
	{
		[Token(Token = "0x60006CE")]
		[Address(RVA = "0x39FAF0", Offset = "0x39E4F0", VA = "0x18039FAF0", Slot = "5")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000121")]
	public float?[] To
	{
		[Token(Token = "0x60006CF")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x60006D0")]
		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x17000122")]
	public float Duration
	{
		[Token(Token = "0x60006D1")]
		[Address(RVA = "0x39FB70", Offset = "0x39E570", VA = "0x18039FB70")]
		[CompilerGenerated]
		get
		{
			return default(float);
		}
		[Token(Token = "0x60006D2")]
		[Address(RVA = "0x39FB80", Offset = "0x39E580", VA = "0x18039FB80")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x17000123")]
	public Ease Ease
	{
		[Token(Token = "0x60006D3")]
		[Address(RVA = "0x38F5A0", Offset = "0x38DFA0", VA = "0x18038F5A0")]
		[CompilerGenerated]
		get
		{
			return default(Ease);
		}
		[Token(Token = "0x60006D4")]
		[Address(RVA = "0x39B790", Offset = "0x39A190", VA = "0x18039B790")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x60006CD")]
	[Address(RVA = "0x39FA80", Offset = "0x39E480", VA = "0x18039FA80")]
	public ModifyCameraRotationTween(float?[] To, float Duration, Ease Ease)
	{
	}

	[Token(Token = "0x60006D5")]
	[Address(RVA = "0x39FB90", Offset = "0x39E590", VA = "0x18039FB90", Slot = "3")]
	[CompilerGenerated]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x60006D6")]
	[Address(RVA = "0x39FCD0", Offset = "0x39E6D0", VA = "0x18039FCD0", Slot = "6")]
	[CompilerGenerated]
	protected virtual bool PrintMembers(StringBuilder builder)
	{
		return default(bool);
	}

	[Token(Token = "0x60006D9")]
	[Address(RVA = "0x39FE60", Offset = "0x39E860", VA = "0x18039FE60", Slot = "2")]
	[CompilerGenerated]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x60006DB")]
	[Address(RVA = "0x3A0080", Offset = "0x39EA80", VA = "0x1803A0080", Slot = "7")]
	[CompilerGenerated]
	public virtual bool Equals(ModifyCameraRotationTween? other)
	{
		return default(bool);
	}

	[Token(Token = "0x60006DD")]
	[Address(RVA = "0x3A02C0", Offset = "0x39ECC0", VA = "0x1803A02C0")]
	[CompilerGenerated]
	protected ModifyCameraRotationTween(ModifyCameraRotationTween original)
	{
	}

	[Token(Token = "0x60006DE")]
	[Address(RVA = "0x3A0340", Offset = "0x39ED40", VA = "0x1803A0340")]
	[CompilerGenerated]
	public void Deconstruct(out float?[] To, out float Duration, out Ease Ease)
	{
		To = default;
		Duration = default;
		Ease = default;
		To = default;
		Duration = default;
		Ease = default;
	}
}
