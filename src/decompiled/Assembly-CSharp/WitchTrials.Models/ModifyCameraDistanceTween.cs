using System;
using System.Runtime.CompilerServices;
using System.Text;
using Il2CppDummyDll;
using LitMotion;

namespace WitchTrials.Models;

[Token(Token = "0x200018F")]
public record ModifyCameraDistanceTween
{
	// RECONSTRUCTED | Confidence: A
	// Record property storage. dump.cs names these `<X>k__BackingField` and tags
	// them `readonly`: the properties are init-only, and the accessors below are
	// their `init` accessors rendered by ILSpy as `set`. This file keeps the
	// non-positional rendering, so the storage is declared as plain fields.
	// ScenarioInfo.cs shows the equivalent positional form.
	private float _to;
	private float _duration;
	private Ease _ease;

	[Token(Token = "0x17000124")]
	[CompilerGenerated]
	protected virtual Type EqualityContract
	{
		[Token(Token = "0x60006E0")]
		[Address(RVA = "0x3A03B0", Offset = "0x39EDB0", VA = "0x1803A03B0", Slot = "5")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000125")]
	public float To
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x39F300 (shared with
		// ModifyCameraHeightTween.get_To -- `movss xmm0,[rcx+0x10]; ret`)
		[Token(Token = "0x60006E1")]
		[Address(RVA = "0x39F300", Offset = "0x39DD00", VA = "0x18039F300")]
		[CompilerGenerated]
		get
		{
			return _to;
		}
		// RECONSTRUCTED | Confidence: A | RVA: 0x39F310 (shared setter, +0x10)
		[Token(Token = "0x60006E2")]
		[Address(RVA = "0x39F310", Offset = "0x39DD10", VA = "0x18039F310")]
		[CompilerGenerated]
		set
		{
			_to = value;
		}
	}

	[Token(Token = "0x17000126")]
	public float Duration
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x3686E0 (shared getter, +0x14)
		[Token(Token = "0x60006E3")]
		[Address(RVA = "0x3686E0", Offset = "0x3670E0", VA = "0x1803686E0")]
		[CompilerGenerated]
		get
		{
			return _duration;
		}
		// RECONSTRUCTED | Confidence: A | RVA: 0x39F320 (shared setter, +0x14)
		[Token(Token = "0x60006E4")]
		[Address(RVA = "0x39F320", Offset = "0x39DD20", VA = "0x18039F320")]
		[CompilerGenerated]
		set
		{
			_duration = value;
		}
	}

	[Token(Token = "0x17000127")]
	public Ease Ease
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x38F590 (shared generic 4-byte getter, +0x18)
		[Token(Token = "0x60006E5")]
		[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590")]
		[CompilerGenerated]
		get
		{
			return _ease;
		}
		// RECONSTRUCTED | Confidence: A | RVA: 0x39B780 (shared generic 4-byte setter, +0x18)
		[Token(Token = "0x60006E6")]
		[Address(RVA = "0x39B780", Offset = "0x39A180", VA = "0x18039B780")]
		[CompilerGenerated]
		set
		{
			_ease = value;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39F270 (shared body with
	// ModifyCameraHeightTween's primary constructor -- identical field layout)
	[Token(Token = "0x60006DF")]
	[Address(RVA = "0x39F270", Offset = "0x39DC70", VA = "0x18039F270")]
	public ModifyCameraDistanceTween(float To, float Duration, Ease Ease)
	{
		_to = To;
		_duration = Duration;
		_ease = Ease;
	}

	[Token(Token = "0x60006E7")]
	[Address(RVA = "0x3A0430", Offset = "0x39EE30", VA = "0x1803A0430", Slot = "3")]
	[CompilerGenerated]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x60006E8")]
	[Address(RVA = "0x3A0570", Offset = "0x39EF70", VA = "0x1803A0570", Slot = "6")]
	[CompilerGenerated]
	protected virtual bool PrintMembers(StringBuilder builder)
	{
		return default(bool);
	}

	[Token(Token = "0x60006EB")]
	[Address(RVA = "0x3A0740", Offset = "0x39F140", VA = "0x1803A0740", Slot = "2")]
	[CompilerGenerated]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x60006ED")]
	[Address(RVA = "0x3A0950", Offset = "0x39F350", VA = "0x1803A0950", Slot = "7")]
	[CompilerGenerated]
	public virtual bool Equals(ModifyCameraDistanceTween? other)
	{
		return default(bool);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39FA30 (shared body with
	// ModifyCameraHeightTween's copy constructor -- identical field layout)
	[Token(Token = "0x60006EF")]
	[Address(RVA = "0x39FA30", Offset = "0x39E430", VA = "0x18039FA30")]
	[CompilerGenerated]
	protected ModifyCameraDistanceTween(ModifyCameraDistanceTween original)
	{
		_to = original._to;
		_duration = original._duration;
		_ease = original._ease;
	}

	[Token(Token = "0x60006F0")]
	[Address(RVA = "0x39FA60", Offset = "0x39E460", VA = "0x18039FA60")]
	[CompilerGenerated]
	public void Deconstruct(out float To, out float Duration, out Ease Ease)
	{
		To = default;
		Duration = default;
		Ease = default;
		To = default;
		Duration = default;
		Ease = default;
	}
}
