using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20002D3")]
    public static class ArrayUtils
    {
    	[Token(Token = "0x6000D95")]
    	[Address(RVA = "0x545F80", Offset = "0x544980", VA = "0x180545F80")]
    	public static Vector2 ToVector2(float?[] array, [Optional] Vector2 @default)
    	{
    		return default(Vector2);
    	}

    	[Token(Token = "0x6000D96")]
    	[Address(RVA = "0x546270", Offset = "0x544C70", VA = "0x180546270")]
    	public static Vector3 ToVector3(float?[] array, [Optional] Vector3 @default)
    	{
    		return default(Vector3);
    	}

    	[Token(Token = "0x6000D97")]
    	[Address(RVA = "0x5466C0", Offset = "0x5450C0", VA = "0x1805466C0")]
    	public static Vector4 ToVector4(float?[] array, [Optional] Vector4 @default)
    	{
    		return default(Vector4);
    	}

    	[Token(Token = "0x6000D98")]
    	[Address(RVA = "0x546C50", Offset = "0x545650", VA = "0x180546C50")]
    	public static float?[] ToNullableArray(this Vector2 vector)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000D99")]
    	[Address(RVA = "0x546D30", Offset = "0x545730", VA = "0x180546D30")]
    	public static float?[] ToNullableArray(this Vector3 vector)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000D9A")]
    	[Address(RVA = "0x546E60", Offset = "0x545860", VA = "0x180546E60")]
    	public static float?[] ToNullableArray(this Vector4 vector)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000D9B")]
    	[Address(RVA = "0x546FB0", Offset = "0x5459B0", VA = "0x180546FB0")]
    	public static float[] ToArray(this Vector2 vector)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000D9C")]
    	[Address(RVA = "0x547030", Offset = "0x545A30", VA = "0x180547030")]
    	public static float[] ToArray(this Vector3 vector)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000D9D")]
    	[Address(RVA = "0x5470B0", Offset = "0x545AB0", VA = "0x1805470B0")]
    	public static float[] ToArray(this Vector4 vector)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000D9E")]
    	public static void Add<T>(ref T[] array, T item)
    	{
    	}

    	[Token(Token = "0x6000D9F")]
    	public static bool ArrayEquals<T>(T[] lhs, T[] rhs)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000DA0")]
    	public static bool ArrayReferenceEquals<T>(T[] lhs, T[] rhs)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000DA1")]
    	public static void AddRange<T>(ref T[] array, T[] items)
    	{
    	}

    	[Token(Token = "0x6000DA2")]
    	public static void Insert<T>(ref T[] array, int index, T item)
    	{
    	}

    	[Token(Token = "0x6000DA3")]
    	public static void Remove<T>(ref T[] array, T item)
    	{
    	}

    	[Token(Token = "0x6000DA4")]
    	public static T[] FindAll<T>(T[] array, Predicate<T> match)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000DA5")]
    	public static T Find<T>(T[] array, Predicate<T> match)
    	{
    		return default(T);
    	}

    	[Token(Token = "0x6000DA6")]
    	public static int FindIndex<T>(T[] array, Predicate<T> match)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6000DA7")]
    	public static int IndexOf<T>(T[] array, T value)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6000DA8")]
    	public static int LastIndexOf<T>(T[] array, T value)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6000DA9")]
    	public static void RemoveAt<T>(ref T[] array, int index)
    	{
    	}

    	[Token(Token = "0x6000DAA")]
    	public static bool Contains<T>(T[] array, T item)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000DAB")]
    	public static void ClearAndResize<T>(ref T[] array)
    	{
    	}
    }
}
