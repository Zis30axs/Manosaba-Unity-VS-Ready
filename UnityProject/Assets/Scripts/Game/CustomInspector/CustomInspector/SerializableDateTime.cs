using System;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Serializable]
    [Token(Token = "0x200005F")]
    public class SerializableDateTime
    {
    	[Token(Token = "0x2000060")]
    	public enum InspectorFormat
    	{
    		[Token(Token = "0x4000165")]
    		Default,
    		[Token(Token = "0x4000166")]
    		AddTextInput,
    		[Token(Token = "0x4000167")]
    		TextInput,
    		[Token(Token = "0x4000168")]
    		DateEnums
    	}

    	[Token(Token = "0x400015D")]
    	[FieldOffset(Offset = "0x10")]
    	[Min(1f)]
    	[SerializeField]
    	public int year;

    	[Token(Token = "0x400015E")]
    	[FieldOffset(Offset = "0x14")]
    	[Min(1f)]
    	[SerializeField]
    	public int month;

    	[Token(Token = "0x400015F")]
    	[FieldOffset(Offset = "0x18")]
    	[Min(1f)]
    	[SerializeField]
    	public int day;

    	[Token(Token = "0x4000160")]
    	[FieldOffset(Offset = "0x1C")]
    	[Min(0f)]
    	[SerializeField]
    	public int hour;

    	[Token(Token = "0x4000161")]
    	[FieldOffset(Offset = "0x20")]
    	[Min(0f)]
    	[SerializeField]
    	public int minute;

    	[Token(Token = "0x4000162")]
    	[FieldOffset(Offset = "0x24")]
    	[Min(0f)]
    	[SerializeField]
    	public int second;

    	[Token(Token = "0x4000163")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	public DateTimeKind kind;

    	[Token(Token = "0x17000019")]
    	public int Year
    	{
    		[Token(Token = "0x6000116")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x331650, 4 B -- `mov eax, dword ptr [rcx + 0x10]; ret`: the backing field year (+0x10 in dump.cs)
    		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
    		get
    		{
    			return year;
    		}
    		[Token(Token = "0x6000117")]
    		[Address(RVA = "0x4401B0", Offset = "0x43EBB0", VA = "0x1804401B0")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700001A")]
    	public int Month
    	{
    		[Token(Token = "0x6000118")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x38F580, 4 B -- `mov eax, dword ptr [rcx + 0x14]; ret`: the backing field month (+0x14 in dump.cs)
    		[Address(RVA = "0x38F580", Offset = "0x38DF80", VA = "0x18038F580")]
    		get
    		{
    			return month;
    		}
    		[Token(Token = "0x6000119")]
    		[Address(RVA = "0x4401E0", Offset = "0x43EBE0", VA = "0x1804401E0")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700001B")]
    	public int Day
    	{
    		[Token(Token = "0x600011A")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x38F590, 4 B -- `mov eax, dword ptr [rcx + 0x18]; ret`: the backing field day (+0x18 in dump.cs)
    		[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590")]
    		get
    		{
    			return day;
    		}
    		[Token(Token = "0x600011B")]
    		[Address(RVA = "0x440200", Offset = "0x43EC00", VA = "0x180440200")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700001C")]
    	public int Hour
    	{
    		[Token(Token = "0x600011D")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x38F5A0, 4 B -- `mov eax, dword ptr [rcx + 0x1c]; ret`: the backing field hour (+0x1C in dump.cs)
    		[Address(RVA = "0x38F5A0", Offset = "0x38DFA0", VA = "0x18038F5A0")]
    		get
    		{
    			return hour;
    		}
    		[Token(Token = "0x600011E")]
    		[Address(RVA = "0x440340", Offset = "0x43ED40", VA = "0x180440340")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700001D")]
    	public int Minute
    	{
    		[Token(Token = "0x600011F")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x440360, 4 B -- `mov eax, dword ptr [rcx + 0x20]; ret`: the backing field minute (+0x20 in dump.cs)
    		[Address(RVA = "0x440360", Offset = "0x43ED60", VA = "0x180440360")]
    		get
    		{
    			return minute;
    		}
    		[Token(Token = "0x6000120")]
    		[Address(RVA = "0x440370", Offset = "0x43ED70", VA = "0x180440370")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700001E")]
    	public int Second
    	{
    		[Token(Token = "0x6000121")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x440390, 4 B -- `mov eax, dword ptr [rcx + 0x24]; ret`: the backing field second (+0x24 in dump.cs)
    		[Address(RVA = "0x440390", Offset = "0x43ED90", VA = "0x180440390")]
    		get
    		{
    			return second;
    		}
    		[Token(Token = "0x6000122")]
    		[Address(RVA = "0x4403A0", Offset = "0x43EDA0", VA = "0x1804403A0")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700001F")]
    	public DateTimeKind Kind
    	{
    		[Token(Token = "0x6000123")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x4403C0, 4 B -- `mov eax, dword ptr [rcx + 0x28]; ret`: the backing field kind (+0x28 in dump.cs)
    		[Address(RVA = "0x4403C0", Offset = "0x43EDC0", VA = "0x1804403C0")]
    		get
    		{
    			return kind;
    		}
    		[Token(Token = "0x6000124")]
    		[Address(RVA = "0x4403D0", Offset = "0x43EDD0", VA = "0x1804403D0")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x600011C")]
    	[Address(RVA = "0x4402A0", Offset = "0x43ECA0", VA = "0x1804402A0")]
    	private void ClampDayValue()
    	{
    	}

    	[Token(Token = "0x6000125")]
    	[Address(RVA = "0x4403E0", Offset = "0x43EDE0", VA = "0x1804403E0")]
    	public SerializableDateTime()
    	{
    	}

    	[Token(Token = "0x6000126")]
    	[Address(RVA = "0x440450", Offset = "0x43EE50", VA = "0x180440450")]
    	public SerializableDateTime(DateTime dateTime)
    	{
    	}

    	[Token(Token = "0x6000127")]
    	[Address(RVA = "0x440620", Offset = "0x43F020", VA = "0x180440620")]
    	public static implicit operator SerializableDateTime(DateTime dateTime)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000128")]
    	[Address(RVA = "0x440680", Offset = "0x43F080", VA = "0x180440680")]
    	public static explicit operator DateTime(SerializableDateTime dateTime)
    	{
    		return default(DateTime);
    	}
    }
}
