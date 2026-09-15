using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using Il2CppDummyDll;
using LitMotion;

namespace WitchTrials.Models;

[Token(Token = "0x200018F")]
public record ModifyCameraDistanceTween
{
	// RECONSTRUCTED | Confidence: A | RVA: 0x3A08B0 Equals(object): `Equals(obj as ModifyCameraDistanceTween)` through vtable
	// slot 7 (+0x1A8), with the `as` cast inlined as the type-hierarchy depth test, 146 B
	// RECONSTRUCTED | Confidence: A | RVA: 0x3A0AD0 <Clone>$: `new ModifyCameraDistanceTween(this)` with the copy constructor
	// inlined (field-by-field copy), 88 B. Neither member is expressible in C# source, so they are
	// credited here rather than declared.
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
		// RECONSTRUCTED | Confidence: A | RVA: 0x3A03B0, 125 B (`typeof` = ldtoken + Type.GetTypeFromHandle)
		[Token(Token = "0x60006E0")]
		[Address(RVA = "0x3A03B0", Offset = "0x39EDB0", VA = "0x1803A03B0", Slot = "5")]
		[CompilerGenerated]
		get
		{
			return typeof(ModifyCameraDistanceTween);
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

	// RECONSTRUCTED | Confidence: A | RVA: 0x3A0430, 311 B
	// The compiler-generated record ToString: the type name, " { ", the members through the virtual
	// PrintMembers (slot 6), a space only when it returned true, then '}'.
	[Token(Token = "0x60006E7")]
	[Address(RVA = "0x3A0430", Offset = "0x39EE30", VA = "0x1803A0430", Slot = "3")]
	[CompilerGenerated]
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("ModifyCameraDistanceTween");
		stringBuilder.Append(" { ");
		if (PrintMembers(stringBuilder))
		{
			stringBuilder.Append(' ');
		}
		stringBuilder.Append('}');
		return stringBuilder.ToString();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3A0570, 453 B
	// Compiler-generated: each value-typed member is appended as `<member>.ToString()` (System.Number.FormatSingle with the current NumberFormatInfo for the floats, Enum.ToString for Ease).
	// Fields are read directly (+0x10, +0x14, +0x18) because the property getters are trivial and inlined.
	[Token(Token = "0x60006E8")]
	[Address(RVA = "0x3A0570", Offset = "0x39EF70", VA = "0x1803A0570", Slot = "6")]
	[CompilerGenerated]
	protected virtual bool PrintMembers(StringBuilder builder)
	{
		builder.Append("To = ");
		builder.Append(To.ToString());
		builder.Append(", Duration = ");
		builder.Append(Duration.ToString());
		builder.Append(", Ease = ");
		builder.Append(Ease.ToString());
		return true;
	}

	// RECONSTRUCTED | Confidence: A (calls), B (the multiplier chain is written in canonical Roslyn form) | RVA: 0x3A0740, 354 B
	// The three `imul` constants are the folded powers of the record hash multiplier -1521134295:
	// 0x325D7C39 on the EqualityContract hash, 0x7C71BF6F on the To hash, 0x5AAAAAD7 (subtracted) on
	// the Duration hash, with the Ease hash added last -- i.e. the canonical Roslyn chain below.
	[Token(Token = "0x60006EB")]
	[Address(RVA = "0x3A0740", Offset = "0x39F140", VA = "0x1803A0740", Slot = "2")]
	[CompilerGenerated]
	public override int GetHashCode()
	{
		return ((EqualityComparer<Type>.Default.GetHashCode(EqualityContract) * -1521134295
			+ EqualityComparer<float>.Default.GetHashCode(To)) * -1521134295
			+ EqualityComparer<float>.Default.GetHashCode(Duration)) * -1521134295
			+ EqualityComparer<Ease>.Default.GetHashCode(Ease);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3A0950, 379 B
	// Compiler-generated record equality: reference equality first, then a null test, then the virtual
	// EqualityContract of both sides (so a derived record never equals its base), then one
	// EqualityComparer<T>.Default.Equals per member.
	[Token(Token = "0x60006ED")]
	[Address(RVA = "0x3A0950", Offset = "0x39F350", VA = "0x1803A0950", Slot = "7")]
	[CompilerGenerated]
	public virtual bool Equals(ModifyCameraDistanceTween? other)
	{
		if ((object)this == other)
		{
			return true;
		}
		return other != null && EqualityContract == other.EqualityContract
			&& EqualityComparer<float>.Default.Equals(To, other.To)
			&& EqualityComparer<float>.Default.Equals(Duration, other.Duration)
			&& EqualityComparer<Ease>.Default.Equals(Ease, other.Ease);
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

	// RECONSTRUCTED | Confidence: A | RVA: 0x39FA60, 18 B (shared body with ModifyCameraHeightTween.Deconstruct)
	// Plain out-parameter stores; the parameters shadow the property names, hence the `this.` qualifiers.
	[Token(Token = "0x60006F0")]
	[Address(RVA = "0x39FA60", Offset = "0x39E460", VA = "0x18039FA60")]
	[CompilerGenerated]
	public void Deconstruct(out float To, out float Duration, out Ease Ease)
	{
		To = this.To;
		Duration = this.Duration;
		Ease = this.Ease;
	}
}
