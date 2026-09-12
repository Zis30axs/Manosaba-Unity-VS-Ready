using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000051")]
public struct ChoiceState : IEquatable<ChoiceState>
{
	[Token(Token = "0x400010F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[SerializeField]
	private string id;

	[Token(Token = "0x4000110")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
	[SerializeField]
	private PlaybackSpot hostedAt;

	[Token(Token = "0x4000111")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	[SerializeField]
	private bool nested;

	[Token(Token = "0x4000112")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[SerializeField]
	private LocalizableText summary;

	[Token(Token = "0x4000113")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private bool locked;

	[Token(Token = "0x4000114")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private string buttonPath;

	[Token(Token = "0x4000115")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private Vector2 buttonPosition;

	[Token(Token = "0x4000116")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[SerializeField]
	private bool overwriteButtonPosition;

	[Token(Token = "0x4000117")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[SerializeField]
	private string onSelectScript;

	[Token(Token = "0x4000118")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[SerializeField]
	private bool autoPlay;

	[Token(Token = "0x17000023")]
	public string Id
	{
		[Token(Token = "0x600012A")]
		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000024")]
	public PlaybackSpot HostedAt
	{
		[Token(Token = "0x600012B")]
		[Address(RVA = "0x456320", Offset = "0x454D20", VA = "0x180456320")]
		get
		{
			return default(PlaybackSpot);
		}
	}

	[Token(Token = "0x17000025")]
	public bool Nested
	{
		[Token(Token = "0x600012C")]
		[Address(RVA = "0x44BDC0", Offset = "0x44A7C0", VA = "0x18044BDC0")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000026")]
	public LocalizableText Summary
	{
		[Token(Token = "0x600012D")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		get
		{
			return default(LocalizableText);
		}
	}

	[Token(Token = "0x17000027")]
	public bool Locked
	{
		[Token(Token = "0x600012E")]
		[Address(RVA = "0x456330", Offset = "0x454D30", VA = "0x180456330")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000028")]
	public string ButtonPath
	{
		[Token(Token = "0x600012F")]
		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000029")]
	public Vector2 ButtonPosition
	{
		[Token(Token = "0x6000130")]
		[Address(RVA = "0x456340", Offset = "0x454D40", VA = "0x180456340")]
		get
		{
			return default(Vector2);
		}
	}

	[Token(Token = "0x1700002A")]
	public bool OverwriteButtonPosition
	{
		[Token(Token = "0x6000131")]
		[Address(RVA = "0x456360", Offset = "0x454D60", VA = "0x180456360")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x1700002B")]
	public string OnSelectScript
	{
		[Token(Token = "0x6000132")]
		[Address(RVA = "0x453470", Offset = "0x451E70", VA = "0x180453470")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700002C")]
	public bool AutoPlay
	{
		[Token(Token = "0x6000133")]
		[Address(RVA = "0x456370", Offset = "0x454D70", VA = "0x180456370")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6000134")]
	[Address(RVA = "0x456380", Offset = "0x454D80", VA = "0x180456380")]
	public ChoiceState(PlaybackSpot hostedAt, bool nested, LocalizableText summary = default, bool locked = false, string buttonPath = default, Vector2? buttonPosition = default, string onSelectScript = default, bool autoPlay = false)
	{
	}

	[Token(Token = "0x6000135")]
	[Address(RVA = "0x456670", Offset = "0x455070", VA = "0x180456670", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x6000136")]
	[Address(RVA = "0x456740", Offset = "0x455140", VA = "0x180456740", Slot = "4")]
	public bool Equals(ChoiceState other)
	{
		return default(bool);
	}

	[Token(Token = "0x6000137")]
	[Address(RVA = "0x456780", Offset = "0x455180", VA = "0x180456780", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x6000138")]
	[Address(RVA = "0x4567F0", Offset = "0x4551F0", VA = "0x1804567F0")]
	public static bool operator ==(ChoiceState left, ChoiceState right)
	{
		return default(bool);
	}

	[Token(Token = "0x6000139")]
	[Address(RVA = "0x456850", Offset = "0x455250", VA = "0x180456850")]
	public static bool operator !=(ChoiceState left, ChoiceState right)
	{
		return default(bool);
	}
}
