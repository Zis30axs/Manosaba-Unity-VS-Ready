using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000328")]
    public struct CustomVariablePredefine : IEquatable<CustomVariablePredefine>
    {
    	[Token(Token = "0x4000BDA")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private string name;

    	[Token(Token = "0x4000BDB")]
    	[FieldOffset(Offset = "0x8")]
    	[SerializeField]
    	private string value;

    	[Token(Token = "0x17000205")]
    	public string Name
    	{
    		[Token(Token = "0x6000F23")]
    		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000206")]
    	public string Expression
    	{
    		[Token(Token = "0x6000F24")]
    		[Address(RVA = "0x457050", Offset = "0x455A50", VA = "0x180457050")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000F25")]
    	[Address(RVA = "0x42B240", Offset = "0x429C40", VA = "0x18042B240")]
    	public CustomVariablePredefine(string name, string expression)
    	{ this = default; }

    	[Token(Token = "0x6000F26")]
    	[Address(RVA = "0x457440", Offset = "0x455E40", VA = "0x180457440", Slot = "4")]
    	public bool Equals(CustomVariablePredefine other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000F27")]
    	[Address(RVA = "0x562770", Offset = "0x561170", VA = "0x180562770", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000F28")]
    	[Address(RVA = "0x530A60", Offset = "0x52F460", VA = "0x180530A60", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
