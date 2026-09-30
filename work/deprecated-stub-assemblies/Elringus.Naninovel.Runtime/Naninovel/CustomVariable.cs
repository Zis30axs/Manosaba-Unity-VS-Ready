using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x200031F")]
    public struct CustomVariable : IEquatable<CustomVariable>
    {
    	[Token(Token = "0x4000BC3")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private string name;

    	[Token(Token = "0x4000BC4")]
    	[FieldOffset(Offset = "0x8")]
    	[SerializeField]
    	private CustomVariableScope scope;

    	[Token(Token = "0x4000BC5")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private CustomVariableValue value;

    	[Token(Token = "0x17000200")]
    	public string Name
    	{
    		[Token(Token = "0x6000EF6")]
    		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000201")]
    	public CustomVariableScope Scope
    	{
    		[Token(Token = "0x6000EF7")]
    		[Address(RVA = "0x55F3F0", Offset = "0x55DDF0", VA = "0x18055F3F0")]
    		get
    		{
    			return default(CustomVariableScope);
    		}
    	}

    	[Token(Token = "0x17000202")]
    	public CustomVariableValue Value
    	{
    		[Token(Token = "0x6000EF8")]
    		[Address(RVA = "0x55F400", Offset = "0x55DE00", VA = "0x18055F400")]
    		get
    		{
    			return default(CustomVariableValue);
    		}
    	}

    	[Token(Token = "0x6000EF9")]
    	[Address(RVA = "0x55F420", Offset = "0x55DE20", VA = "0x18055F420")]
    	public CustomVariable(string name, CustomVariableScope scope, CustomVariableValue value)
    	{ this = default; }

    	[Token(Token = "0x6000EFA")]
    	[Address(RVA = "0x55F4F0", Offset = "0x55DEF0", VA = "0x18055F4F0", Slot = "4")]
    	public bool Equals(CustomVariable other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000EFB")]
    	[Address(RVA = "0x55F630", Offset = "0x55E030", VA = "0x18055F630", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000EFC")]
    	[Address(RVA = "0x55F6E0", Offset = "0x55E0E0", VA = "0x18055F6E0", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6000EFD")]
    	[Address(RVA = "0x55F7D0", Offset = "0x55E1D0", VA = "0x18055F7D0")]
    	public static bool operator ==(CustomVariable left, CustomVariable right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000EFE")]
    	[Address(RVA = "0x55F810", Offset = "0x55E210", VA = "0x18055F810")]
    	public static bool operator !=(CustomVariable left, CustomVariable right)
    	{
    		return default(bool);
    	}
    }
}
