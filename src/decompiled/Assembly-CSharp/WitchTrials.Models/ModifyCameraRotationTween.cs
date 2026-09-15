using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using Il2CppDummyDll;
using LitMotion;

namespace WitchTrials.Models;

[Token(Token = "0x200018E")]
public record ModifyCameraRotationTween
{
	// RECONSTRUCTED | Confidence: A | RVA: 0x39FFE0 Equals(object): `Equals(obj as ModifyCameraRotationTween)` through vtable
	// slot 7 (+0x1A8), with the `as` cast inlined as the type-hierarchy depth test, 146 B
	// RECONSTRUCTED | Confidence: A | RVA: 0x3A0210 <Clone>$: `new ModifyCameraRotationTween(this)` with the copy constructor
	// inlined (field-by-field copy), 164 B. Neither member is expressible in C# source, so they are
	// credited here rather than declared.
	// RECONSTRUCTED | Confidence: A
	// Record property storage. dump.cs names these `<X>k__BackingField` and tags
	// them `readonly`: the properties are init-only, and the accessors below are
	// their `init` accessors rendered by ILSpy as `set`. This file keeps the
	// non-positional rendering, so the storage is declared as plain fields.
	// ScenarioInfo.cs shows the equivalent positional form.
	private float?[] _to;
	private float _duration;
	private Ease _ease;

	[Token(Token = "0x17000120")]
	[CompilerGenerated]
	protected virtual Type EqualityContract
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x39FAF0, 125 B (`typeof` = ldtoken + Type.GetTypeFromHandle)
		[Token(Token = "0x60006CE")]
		[Address(RVA = "0x39FAF0", Offset = "0x39E4F0", VA = "0x18039FAF0", Slot = "5")]
		[CompilerGenerated]
		get
		{
			return typeof(ModifyCameraRotationTween);
		}
	}

	[Token(Token = "0x17000121")]
	public float?[] To
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 (shared generic
		// reference-field getter, +0x10 -- same thunk used by 460+ unrelated
		// methods project-wide; see work/reversing/README.md)
		[Token(Token = "0x60006CF")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return _to;
		}
		// RECONSTRUCTED | Confidence: A | RVA: 0x30C0D0 (generic reference-field
		// setter with GC write barrier, +0x10)
		[Token(Token = "0x60006D0")]
		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
		[CompilerGenerated]
		set
		{
			_to = value;
		}
	}

	[Token(Token = "0x17000122")]
	public float Duration
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x39FB70 (`movss xmm0,[rcx+0x18]; ret`)
		[Token(Token = "0x60006D1")]
		[Address(RVA = "0x39FB70", Offset = "0x39E570", VA = "0x18039FB70")]
		[CompilerGenerated]
		get
		{
			return _duration;
		}
		// RECONSTRUCTED | Confidence: A | RVA: 0x39FB80 (`movss [rcx+0x18],xmm1; ret`)
		[Token(Token = "0x60006D2")]
		[Address(RVA = "0x39FB80", Offset = "0x39E580", VA = "0x18039FB80")]
		[CompilerGenerated]
		set
		{
			_duration = value;
		}
	}

	[Token(Token = "0x17000123")]
	public Ease Ease
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x38F5A0 (shared generic 4-byte getter, +0x1C)
		[Token(Token = "0x60006D3")]
		[Address(RVA = "0x38F5A0", Offset = "0x38DFA0", VA = "0x18038F5A0")]
		[CompilerGenerated]
		get
		{
			return _ease;
		}
		// RECONSTRUCTED | Confidence: A | RVA: 0x39B790 (`mov [rcx+0x1C],edx; ret`)
		[Token(Token = "0x60006D4")]
		[Address(RVA = "0x39B790", Offset = "0x39A190", VA = "0x18039B790")]
		[CompilerGenerated]
		set
		{
			_ease = value;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39FA80
	// Positional record's primary constructor: stores all three params into the
	// matching backing fields (array param via the standard GC write barrier).
	[Token(Token = "0x60006CD")]
	[Address(RVA = "0x39FA80", Offset = "0x39E480", VA = "0x18039FA80")]
	public ModifyCameraRotationTween(float?[] To, float Duration, Ease Ease)
	{
		_to = To;
		_duration = Duration;
		_ease = Ease;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39FB90, 311 B
	// The compiler-generated record ToString: the type name, " { ", the members through the virtual
	// PrintMembers (slot 6), a space only when it returned true, then '}'.
	[Token(Token = "0x60006D5")]
	[Address(RVA = "0x39FB90", Offset = "0x39E590", VA = "0x18039FB90", Slot = "3")]
	[CompilerGenerated]
	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("ModifyCameraRotationTween");
		stringBuilder.Append(" { ");
		if (PrintMembers(stringBuilder))
		{
			stringBuilder.Append(' ');
		}
		stringBuilder.Append('}');
		return stringBuilder.ToString();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x39FCD0, 396 B
	// Compiler-generated: the array member goes through `StringBuilder.Append(object)` (inlined: null check, then the virtual ToString), the float and enum members through `<member>.ToString()`.
	// Fields are read directly (+0x10, +0x18, +0x1C) because the property getters are trivial and inlined.
	[Token(Token = "0x60006D6")]
	[Address(RVA = "0x39FCD0", Offset = "0x39E6D0", VA = "0x18039FCD0", Slot = "6")]
	[CompilerGenerated]
	protected virtual bool PrintMembers(StringBuilder builder)
	{
		builder.Append("To = ");
		builder.Append(To);
		builder.Append(", Duration = ");
		builder.Append(Duration.ToString());
		builder.Append(", Ease = ");
		builder.Append(Ease.ToString());
		return true;
	}

	// RECONSTRUCTED | Confidence: A (calls), B (the multiplier chain is written in canonical Roslyn form) | RVA: 0x39FE60, 371 B
	// The three `imul` constants are the folded powers of the record hash multiplier -1521134295:
	// 0x325D7C39 on the EqualityContract hash, 0x7C71BF6F on the To hash, 0x5AAAAAD7 (subtracted) on
	// the Duration hash, with the Ease hash added last -- i.e. the canonical Roslyn chain below.
	[Token(Token = "0x60006D9")]
	[Address(RVA = "0x39FE60", Offset = "0x39E860", VA = "0x18039FE60", Slot = "2")]
	[CompilerGenerated]
	public override int GetHashCode()
	{
		return ((EqualityComparer<Type>.Default.GetHashCode(EqualityContract) * -1521134295
			+ EqualityComparer<float?[]>.Default.GetHashCode(To)) * -1521134295
			+ EqualityComparer<float>.Default.GetHashCode(Duration)) * -1521134295
			+ EqualityComparer<Ease>.Default.GetHashCode(Ease);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3A0080, 395 B
	// Compiler-generated record equality: reference equality first, then a null test, then the virtual
	// EqualityContract of both sides (so a derived record never equals its base), then one
	// EqualityComparer<T>.Default.Equals per member.
	[Token(Token = "0x60006DB")]
	[Address(RVA = "0x3A0080", Offset = "0x39EA80", VA = "0x1803A0080", Slot = "7")]
	[CompilerGenerated]
	public virtual bool Equals(ModifyCameraRotationTween? other)
	{
		if ((object)this == other)
		{
			return true;
		}
		return other != null && EqualityContract == other.EqualityContract
			&& EqualityComparer<float?[]>.Default.Equals(To, other.To)
			&& EqualityComparer<float>.Default.Equals(Duration, other.Duration)
			&& EqualityComparer<Ease>.Default.Equals(Ease, other.Ease);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3A02C0
	// Compiler-generated record copy constructor: field-by-field copy (array
	// field via GC write barrier), null-checked (throws NullReferenceException
	// via the shared IL2CPP helper if `original` is null -- reproduced here by
	// the implicit null-deref on `original._to`).
	[Token(Token = "0x60006DD")]
	[Address(RVA = "0x3A02C0", Offset = "0x39ECC0", VA = "0x1803A02C0")]
	[CompilerGenerated]
	protected ModifyCameraRotationTween(ModifyCameraRotationTween original)
	{
		_to = original._to;
		_duration = original._duration;
		_ease = original._ease;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3A0340, 106 B
	// Plain out-parameter stores; the parameters shadow the property names, hence the `this.` qualifiers.
	[Token(Token = "0x60006DE")]
	[Address(RVA = "0x3A0340", Offset = "0x39ED40", VA = "0x1803A0340")]
	[CompilerGenerated]
	public void Deconstruct(out float?[] To, out float Duration, out Ease Ease)
	{
		To = this.To;
		Duration = this.Duration;
		Ease = this.Ease;
	}
}
