using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Toggleables;

[Serializable]
[Token(Token = "0x2000004")]
public class ToggleableClass<TClass> where TClass : class
{
	[Token(Token = "0x4000006")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private bool _enabled;

	[Token(Token = "0x4000007")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private TClass _value;

	[Token(Token = "0x17000001")]
	private TClass Value
	{
		[Token(Token = "0x6000003")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000004")]
	public static implicit operator ToggleableClass<TClass>(TClass value)
	{
		return null;
	}

	[Token(Token = "0x6000005")]
	public static implicit operator TClass(ToggleableClass<TClass> value)
	{
		return null;
	}

	[Token(Token = "0x6000006")]
	public ToggleableClass()
	{
	}
}
