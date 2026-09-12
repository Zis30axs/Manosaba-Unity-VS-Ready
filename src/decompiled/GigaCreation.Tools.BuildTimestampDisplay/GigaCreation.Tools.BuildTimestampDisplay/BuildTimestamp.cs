using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.BuildTimestampDisplay;

[Token(Token = "0x2000004")]
public class BuildTimestamp : ScriptableObject
{
	[Token(Token = "0x4000006")]
	[FieldOffset(Offset = "0x18")]
	public int UtcYear;

	[Token(Token = "0x4000007")]
	[FieldOffset(Offset = "0x1C")]
	public int UtcMonth;

	[Token(Token = "0x4000008")]
	[FieldOffset(Offset = "0x20")]
	public int UtcDay;

	[Token(Token = "0x4000009")]
	[FieldOffset(Offset = "0x24")]
	public int UtcHour;

	[Token(Token = "0x400000A")]
	[FieldOffset(Offset = "0x28")]
	public int UtcMinute;

	[Token(Token = "0x400000B")]
	[FieldOffset(Offset = "0x2C")]
	public int UtcSecond;

	[Token(Token = "0x6000003")]
	[Address(RVA = "0xD41670", Offset = "0xD40070", VA = "0x180D41670", Slot = "3")]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x6000004")]
	[Address(RVA = "0xD41800", Offset = "0xD40200", VA = "0x180D41800")]
	public string ToString(string format, double utcOffsetHours)
	{
		return null;
	}

	[Token(Token = "0x6000005")]
	[Address(RVA = "0x398D20", Offset = "0x397720", VA = "0x180398D20")]
	public BuildTimestamp()
	{
	}
}
