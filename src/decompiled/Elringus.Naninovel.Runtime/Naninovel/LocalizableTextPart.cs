using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000387")]
public struct LocalizableTextPart : IEquatable<LocalizableTextPart>
{
	[Token(Token = "0x4000D1C")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private string id;

	[Token(Token = "0x4000D1D")]
	[FieldOffset(Offset = "0x8")]
	[SerializeField]
	private PlaybackSpot spot;

	[Token(Token = "0x4000D1E")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private string text;

	[Token(Token = "0x17000252")]
	public string Id
	{
		[Token(Token = "0x6001122")]
		[Address(RVA = "0x584C10", Offset = "0x583610", VA = "0x180584C10")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000253")]
	public PlaybackSpot Spot
	{
		[Token(Token = "0x6001123")]
		[Address(RVA = "0x584C80", Offset = "0x583680", VA = "0x180584C80")]
		get
		{
			return default(PlaybackSpot);
		}
	}

	[Token(Token = "0x17000254")]
	public string Text
	{
		[Token(Token = "0x6001124")]
		[Address(RVA = "0x584D00", Offset = "0x583700", VA = "0x180584D00")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000255")]
	public bool PlainText
	{
		[Token(Token = "0x6001125")]
		[Address(RVA = "0x584D70", Offset = "0x583770", VA = "0x180584D70")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6001126")]
	[Address(RVA = "0x584D90", Offset = "0x583790", VA = "0x180584D90")]
	public static LocalizableTextPart FromIdentified(string id, PlaybackSpot spot)
	{
		return default(LocalizableTextPart);
	}

	[Token(Token = "0x6001127")]
	[Address(RVA = "0x584EC0", Offset = "0x5838C0", VA = "0x180584EC0")]
	public static LocalizableTextPart FromPlainText(string text)
	{
		return default(LocalizableTextPart);
	}

	[Token(Token = "0x6001128")]
	[Address(RVA = "0x584F90", Offset = "0x583990", VA = "0x180584F90", Slot = "3")]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x6001129")]
	[Address(RVA = "0x585270", Offset = "0x583C70", VA = "0x180585270", Slot = "4")]
	public bool Equals(LocalizableTextPart other)
	{
		return default(bool);
	}

	[Token(Token = "0x600112A")]
	[Address(RVA = "0x585420", Offset = "0x583E20", VA = "0x180585420", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x600112B")]
	[Address(RVA = "0x5854C0", Offset = "0x583EC0", VA = "0x1805854C0", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}
}
