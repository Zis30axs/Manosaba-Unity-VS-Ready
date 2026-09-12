using System;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector;

[Serializable]
[Token(Token = "0x2000065")]
public struct SerializableNullable<T> where T : struct
{
	[Token(Token = "0x2000066")]
	public class NullType
	{
		[Token(Token = "0x6000147")]
		public NullType()
		{
		}
	}

	[Token(Token = "0x400016C")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private bool hasValue;

	[Token(Token = "0x400016D")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private T value;

	[Token(Token = "0x17000021")]
	public bool HasValue
	{
		[Token(Token = "0x6000132")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000133")]
		set
		{
		}
	}

	[Token(Token = "0x17000022")]
	public T Value
	{
		[Token(Token = "0x6000134")]
		get
		{
			return default(T);
		}
		[Token(Token = "0x6000135")]
		set
		{
		}
	}

	[Token(Token = "0x6000136")]
	public SerializableNullable(T value)
	{
	}

	[Token(Token = "0x6000137")]
	public SerializableNullable(NullType _)
	{
	}

	[Token(Token = "0x6000138")]
	public static explicit operator T(SerializableNullable<T> value)
	{
		return default(T);
	}

	[Token(Token = "0x6000139")]
	public static implicit operator SerializableNullable<T>(T value)
	{
		return default(SerializableNullable<T>);
	}

	[Token(Token = "0x600013A")]
	public static implicit operator SerializableNullable<T>(NullType value)
	{
		return default(SerializableNullable<T>);
	}

	[Token(Token = "0x600013B")]
	public static explicit operator SerializableNullable<T>(T? value)
	{
		return default(SerializableNullable<T>);
	}

	[Token(Token = "0x600013C")]
	public static explicit operator T?(SerializableNullable<T> value)
	{
		return null;
	}

	[Token(Token = "0x600013D")]
	public override readonly bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x600013E")]
	public readonly bool Equals(SerializableNullable<T> other)
	{
		return default(bool);
	}

	[Token(Token = "0x600013F")]
	public override readonly int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x6000140")]
	public override string ToString()
	{
		return null;
	}

	[Token(Token = "0x6000141")]
	public static bool operator ==(SerializableNullable<T> a, SerializableNullable<T> b)
	{
		return default(bool);
	}

	[Token(Token = "0x6000142")]
	public static bool operator !=(SerializableNullable<T> a, SerializableNullable<T> b)
	{
		return default(bool);
	}

	[Token(Token = "0x6000143")]
	public static bool operator ==(SerializableNullable<T> a, NullType b)
	{
		return default(bool);
	}

	[Token(Token = "0x6000144")]
	public static bool operator !=(SerializableNullable<T> a, NullType b)
	{
		return default(bool);
	}

	[Token(Token = "0x6000145")]
	public static bool operator ==(NullType b, SerializableNullable<T> a)
	{
		return default(bool);
	}

	[Token(Token = "0x6000146")]
	public static bool operator !=(NullType b, SerializableNullable<T> a)
	{
		return default(bool);
	}
}
