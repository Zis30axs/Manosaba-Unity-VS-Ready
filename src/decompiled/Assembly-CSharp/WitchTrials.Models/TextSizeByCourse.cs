using System;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Serializable]
[Token(Token = "0x200015F")]
public class TextSizeByCourse
{
	[Token(Token = "0x40006A8")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private SpecialThanksCourse _course;

	[Token(Token = "0x40006A9")]
	[FieldOffset(Offset = "0x14")]
	[SerializeField]
	private int _phase;

	[Token(Token = "0x40006AA")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private int _level;

	[Token(Token = "0x40006AB")]
	[FieldOffset(Offset = "0x1C")]
	[SerializeField]
	private int _fee;

	[Token(Token = "0x170000CF")]
	public SpecialThanksCourse Course
	{
		[Token(Token = "0x60005BB")]
		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
		get
		{
			return default(SpecialThanksCourse);
		}
	}

	[Token(Token = "0x170000D0")]
	public int Phase
	{
		[Token(Token = "0x60005BC")]
		[Address(RVA = "0x38F580", Offset = "0x38DF80", VA = "0x18038F580")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x170000D1")]
	public int Level
	{
		[Token(Token = "0x60005BD")]
		[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x170000D2")]
	public int Fee
	{
		[Token(Token = "0x60005BE")]
		[Address(RVA = "0x38F5A0", Offset = "0x38DFA0", VA = "0x18038F5A0")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x60005BF")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public TextSizeByCourse()
	{
	}
}
