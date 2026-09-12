using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Toggleables;

[Serializable]
[Token(Token = "0x2000005")]
public struct ToggleableStruct<TStruct> where TStruct : struct
{
	[Token(Token = "0x4000008")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private bool _enabled;

	[Token(Token = "0x4000009")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private TStruct _value;

	[Token(Token = "0x17000002")]
	private readonly TStruct? ValueOrNull
	{
		[Token(Token = "0x6000007")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000008")]
	public static implicit operator ToggleableStruct<TStruct>(TStruct value)
	{
		return default(ToggleableStruct<TStruct>);
	}

	[Token(Token = "0x6000009")]
	public static implicit operator TStruct?(ToggleableStruct<TStruct> value)
	{
		return null;
	}
}
