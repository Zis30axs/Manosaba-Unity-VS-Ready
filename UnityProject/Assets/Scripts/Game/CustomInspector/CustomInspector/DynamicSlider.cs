using System;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Serializable]
    [Token(Token = "0x2000056")]
    public class DynamicSlider
    {
    	[Token(Token = "0x400014D")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	public float value;

    	[Token(Token = "0x400014E")]
    	[FieldOffset(Offset = "0x14")]
    	[SerializeField]
    	private float min;

    	[Token(Token = "0x400014F")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private float max;

    	[Token(Token = "0x4000150")]
    	[FieldOffset(Offset = "0x1C")]
    	private readonly float defaultMin;

    	[Token(Token = "0x4000151")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly float defaultMax;

    	[Token(Token = "0x4000152")]
    	[FieldOffset(Offset = "0x24")]
    	private readonly FixedSide fixedSide;

    	[Token(Token = "0x60000AB")]
    	[Address(RVA = "0x39F310", Offset = "0x39DD10", VA = "0x18039F310")]
    	public DynamicSlider(float value, float min, float max, FixedSide fixedSide = FixedSide.None)
    	{
    	}

    	[Token(Token = "0x60000AC")]
    	[Address(RVA = "0x43F710", Offset = "0x43E110", VA = "0x18043F710", Slot = "3")]
    	public override string ToString()
    	{
    		return null;
    	}

    	[Token(Token = "0x60000AD")]
    	[Address(RVA = "0x43F720", Offset = "0x43E120", VA = "0x18043F720")]
    	public static implicit operator float(DynamicSlider dynamicSlider)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x60000AE")]
    	[Address(RVA = "0x43F720", Offset = "0x43E120", VA = "0x18043F720")]
    	public static float operator +(DynamicSlider d)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x60000AF")]
    	[Address(RVA = "0x43F740", Offset = "0x43E140", VA = "0x18043F740")]
    	public static float operator -(DynamicSlider d)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x60000B0")]
    	[Address(RVA = "0x43F760", Offset = "0x43E160", VA = "0x18043F760")]
    	public static float operator +(DynamicSlider a, DynamicSlider b)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x60000B1")]
    	[Address(RVA = "0x43F790", Offset = "0x43E190", VA = "0x18043F790")]
    	public static float operator -(DynamicSlider a, DynamicSlider b)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x60000B2")]
    	[Address(RVA = "0x43F7C0", Offset = "0x43E1C0", VA = "0x18043F7C0")]
    	public static float operator *(DynamicSlider a, DynamicSlider b)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x60000B3")]
    	[Address(RVA = "0x43F7F0", Offset = "0x43E1F0", VA = "0x18043F7F0")]
    	public static float operator /(DynamicSlider a, DynamicSlider b)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x60000B4")]
    	[Address(RVA = "0x43F820", Offset = "0x43E220", VA = "0x18043F820")]
    	public static float operator +(float f, DynamicSlider d)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x60000B5")]
    	[Address(RVA = "0x43F840", Offset = "0x43E240", VA = "0x18043F840")]
    	public static float operator +(DynamicSlider d, float f)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x60000B6")]
    	[Address(RVA = "0x43F860", Offset = "0x43E260", VA = "0x18043F860")]
    	public static float operator -(float f, DynamicSlider d)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x60000B7")]
    	[Address(RVA = "0x43F880", Offset = "0x43E280", VA = "0x18043F880")]
    	public static float operator -(DynamicSlider d, float f)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x60000B8")]
    	[Address(RVA = "0x43F8A0", Offset = "0x43E2A0", VA = "0x18043F8A0")]
    	public static float operator *(float f, DynamicSlider d)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x60000B9")]
    	[Address(RVA = "0x43F8C0", Offset = "0x43E2C0", VA = "0x18043F8C0")]
    	public static float operator *(DynamicSlider d, float f)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x60000BA")]
    	[Address(RVA = "0x43F8E0", Offset = "0x43E2E0", VA = "0x18043F8E0")]
    	public static float operator /(float f, DynamicSlider d)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x60000BB")]
    	[Address(RVA = "0x43F900", Offset = "0x43E300", VA = "0x18043F900")]
    	public static float operator /(DynamicSlider d, float f)
    	{
    		return default(float);
    	}
    }
}
