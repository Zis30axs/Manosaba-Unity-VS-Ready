using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000332")]
    public struct CustomVariableValue : IEquatable<CustomVariableValue>
    {
    	[Token(Token = "0x4000BEC")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private CustomVariableValueType type;

    	[Token(Token = "0x4000BED")]
    	[FieldOffset(Offset = "0x8")]
    	[SerializeField]
    	private string stringValue;

    	[Token(Token = "0x4000BEE")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private float numericValue;

    	[Token(Token = "0x4000BEF")]
    	[FieldOffset(Offset = "0x14")]
    	[SerializeField]
    	private bool booleanValue;

    	[Token(Token = "0x17000209")]
    	public CustomVariableValueType Type
    	{
    		[Token(Token = "0x6000F41")]
    		[Address(RVA = "0x437AC0", Offset = "0x4364C0", VA = "0x180437AC0")]
    		get
    		{
    			return default(CustomVariableValueType);
    		}
    	}

    	[Token(Token = "0x1700020A")]
    	public string String
    	{
    		[Token(Token = "0x6000F42")]
    		[Address(RVA = "0x563AB0", Offset = "0x5624B0", VA = "0x180563AB0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700020B")]
    	public float Number
    	{
    		[Token(Token = "0x6000F43")]
    		[Address(RVA = "0x563B20", Offset = "0x562520", VA = "0x180563B20")]
    		get
    		{
    			return default(float);
    		}
    	}

    	[Token(Token = "0x1700020C")]
    	public bool Boolean
    	{
    		[Token(Token = "0x6000F44")]
    		[Address(RVA = "0x563B90", Offset = "0x562590", VA = "0x180563B90")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x6000F45")]
    	[Address(RVA = "0x563C00", Offset = "0x562600", VA = "0x180563C00")]
    	public CustomVariableValue(string value)
    	{ this = default; }

    	[Token(Token = "0x6000F46")]
    	[Address(RVA = "0x563C70", Offset = "0x562670", VA = "0x180563C70")]
    	public CustomVariableValue(float value)
    	{ this = default; }

    	[Token(Token = "0x6000F47")]
    	[Address(RVA = "0x563CE0", Offset = "0x5626E0", VA = "0x180563CE0")]
    	public CustomVariableValue(bool value)
    	{ this = default; }

    	[Token(Token = "0x6000F48")]
    	[Address(RVA = "0x563D50", Offset = "0x562750", VA = "0x180563D50", Slot = "4")]
    	public bool Equals(CustomVariableValue other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000F49")]
    	[Address(RVA = "0x563E10", Offset = "0x562810", VA = "0x180563E10", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000F4A")]
    	[Address(RVA = "0x563EB0", Offset = "0x5628B0", VA = "0x180563EB0", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6000F4B")]
    	[Address(RVA = "0x563F60", Offset = "0x562960", VA = "0x180563F60")]
    	public static bool operator ==(CustomVariableValue left, CustomVariableValue right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000F4C")]
    	[Address(RVA = "0x563F90", Offset = "0x562990", VA = "0x180563F90")]
    	public static bool operator !=(CustomVariableValue left, CustomVariableValue right)
    	{
    		return default(bool);
    	}
    }
}
