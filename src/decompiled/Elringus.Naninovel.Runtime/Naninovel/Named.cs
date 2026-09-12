using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x200020E")]
public class Named<TValue> : INamed<TValue>, INamedValue
{
	[Token(Token = "0x40008AA")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private NullableString name;

	[Token(Token = "0x40008AB")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private TValue value;

	[Token(Token = "0x17000151")]
	public string Name
	{
		[Token(Token = "0x6000939")]
		get
		{
			return null;
		}
		[Token(Token = "0x600093A")]
		set
		{
		}
	}

	[Token(Token = "0x17000152")]
	public TValue Value
	{
		[Token(Token = "0x600093B")]
		get
		{
			return default(TValue);
		}
		[Token(Token = "0x600093C")]
		set
		{
		}
	}

	[Token(Token = "0x600093D")]
	public Named()
	{
	}

	[Token(Token = "0x600093E")]
	public Named(string name, TValue value)
	{
	}

	[Token(Token = "0x600093F")]
	public override string ToString()
	{
		return null;
	}
}
