using System;
using System.Runtime.CompilerServices;
using System.Text;
using Il2CppDummyDll;
using LitMotion;

namespace WitchTrials.Models;

[Token(Token = "0x200018D")]
public record ModifyCameraHeightTween
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

	[Token(Token = "0x1700011C")]
	[CompilerGenerated]
	protected virtual Type EqualityContract
	{
		[Token(Token = "0x60006BC")]
		[Address(RVA = "0x39F280", Offset = "0x39DC80", VA = "0x18039F280", Slot = "5")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700011D")]
	public float To
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x39F300 (`movss xmm0,[rcx+0x10]; ret`)
		[Token(Token = "0x60006BD")]
		[Address(RVA = "0x39F300", Offset = "0x39DD00", VA = "0x18039F300")]
		[CompilerGenerated]
		get
		{
			return _to;
		}
		// RECONSTRUCTED | Confidence: A | RVA: 0x39F310 (shared setter, +0x10)
		[Token(Token = "0x60006BE")]
		[Address(RVA = "0x39F310", Offset = "0x39DD10", VA = "0x18039F310")]
		[CompilerGenerated]
		set
		{
			_to = value;
		}
	}

	[Token(Token = "0x1700011E")]
	public float Duration
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x3686E0 (`movss xmm0,[rcx+0x14]; ret`)
		[Token(Token = "0x60006BF")]
		[Address(RVA = "0x3686E0", Offset = "0x3670E0", VA = "0x1803686E0")]
		[CompilerGenerated]
		get
		{
			return _duration;
		}
		// RECONSTRUCTED | Confidence: A | RVA: 0x39F320 (shared with Distance-tween's
		// set_Duration -- `movss [rcx+0x14],xmm1; ret`)
		[Token(Token = "0x60006C0")]
		[Address(RVA = "0x39F320", Offset = "0x39DD20", VA = "0x18039F320")]
		[CompilerGenerated]
		set
		{
			_duration = value;
		}
	}

	[Token(Token = "0x1700011F")]
	public Ease Ease
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x38F590 (shared generic 4-byte getter, +0x18)
		[Token(Token = "0x60006C1")]
		[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590")]
		[CompilerGenerated]
		get
		{
			return _ease;
		}
		// RECONSTRUCTED | Confidence: A | RVA: 0x39B780 (shared generic 4-byte setter, +0x18)
		[Token(Token = "0x60006C2")]
		[Address(RVA = "0x39B780", Offset = "0x39A180", VA = "0x18039B780")]
		[CompilerGenerated]
		set
		{
			_ease = value;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39F270
	// Positional record's primary constructor: stores all three params into the
	// matching backing fields, no other logic. (Shared body with
	// ModifyCameraDistanceTween's primary constructor -- same field layout.)
	[Token(Token = "0x60006BB")]
	[Address(RVA = "0x39F270", Offset = "0x39DC70", VA = "0x18039F270")]
	public ModifyCameraHeightTween(float To, float Duration, Ease Ease)
	{
		_to = To;
		_duration = Duration;
		_ease = Ease;
	}

	[Token(Token = "0x60006C3")]
	[Address(RVA = "0x39F330", Offset = "0x39DD30", VA = "0x18039F330", Slot = "3")]
	[CompilerGenerated]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x60006C4")]
	[Address(RVA = "0x39F470", Offset = "0x39DE70", VA = "0x18039F470", Slot = "6")]
	[CompilerGenerated]
	protected virtual bool PrintMembers(StringBuilder builder)
	{
		return default(bool);
	}

	[Token(Token = "0x60006C7")]
	[Address(RVA = "0x39F640", Offset = "0x39E040", VA = "0x18039F640", Slot = "2")]
	[CompilerGenerated]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x60006C9")]
	[Address(RVA = "0x39F850", Offset = "0x39E250", VA = "0x18039F850", Slot = "7")]
	[CompilerGenerated]
	public virtual bool Equals(ModifyCameraHeightTween? other)
	{
		return default(bool);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39FA30
	// Compiler-generated record copy constructor: field-by-field copy, null-checked
	// (throws NullReferenceException via the shared IL2CPP helper if `original` is
	// null -- reproduced here by the implicit null-deref on `original._to`).
	// Shared body with ModifyCameraDistanceTween's copy constructor.
	[Token(Token = "0x60006CB")]
	[Address(RVA = "0x39FA30", Offset = "0x39E430", VA = "0x18039FA30")]
	[CompilerGenerated]
	protected ModifyCameraHeightTween(ModifyCameraHeightTween original)
	{
		_to = original._to;
		_duration = original._duration;
		_ease = original._ease;
	}

	[Token(Token = "0x60006CC")]
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
