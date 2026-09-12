using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI;

[Serializable]
[Token(Token = "0x20004F7")]
public struct BacklogMessage : IEquatable<BacklogMessage>
{
	[Token(Token = "0x400134F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[SerializeField]
	private LocalizableText text;

	[Token(Token = "0x4001350")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
	[SerializeField]
	private string author;

	[Token(Token = "0x4001351")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	[SerializeField]
	private PlaybackSpot spot;

	[Token(Token = "0x4001352")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[SerializeField]
	private string[] voice;

	[Token(Token = "0x1700037F")]
	public LocalizableText Text
	{
		[Token(Token = "0x60018BD")]
		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
		get
		{
			return default(LocalizableText);
		}
	}

	[Token(Token = "0x17000380")]
	public bool Authored
	{
		[Token(Token = "0x60018BE")]
		[Address(RVA = "0x60DBF0", Offset = "0x60C5F0", VA = "0x18060DBF0")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000381")]
	public string AuthorId
	{
		[Token(Token = "0x60018BF")]
		[Address(RVA = "0x457050", Offset = "0x455A50", VA = "0x180457050")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000382")]
	public bool HasRollback
	{
		[Token(Token = "0x60018C0")]
		[Address(RVA = "0x60DC10", Offset = "0x60C610", VA = "0x18060DC10")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000383")]
	public PlaybackSpot RollbackSpot
	{
		[Token(Token = "0x60018C1")]
		[Address(RVA = "0x60DD70", Offset = "0x60C770", VA = "0x18060DD70")]
		get
		{
			return default(PlaybackSpot);
		}
	}

	[Token(Token = "0x17000384")]
	public IReadOnlyList<string> Voice
	{
		[Token(Token = "0x60018C2")]
		[Address(RVA = "0x60DD80", Offset = "0x60C780", VA = "0x18060DD80")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60018C3")]
	[Address(RVA = "0x60DDD0", Offset = "0x60C7D0", VA = "0x18060DDD0")]
	public BacklogMessage(LocalizableText text, [Optional] string author, [Optional] PlaybackSpot? spot, [Optional] string[] voice)
	{
	}

	[Token(Token = "0x60018C4")]
	[Address(RVA = "0x60E0D0", Offset = "0x60CAD0", VA = "0x18060E0D0", Slot = "4")]
	public bool Equals(BacklogMessage other)
	{
		return default(bool);
	}

	[Token(Token = "0x60018C5")]
	[Address(RVA = "0x60E140", Offset = "0x60CB40", VA = "0x18060E140", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x60018C6")]
	[Address(RVA = "0x60E210", Offset = "0x60CC10", VA = "0x18060E210", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}
}
