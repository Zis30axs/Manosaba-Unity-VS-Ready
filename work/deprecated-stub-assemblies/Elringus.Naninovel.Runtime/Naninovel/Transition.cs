#nullable enable annotations
﻿using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200049F")]
    public readonly struct Transition : IEquatable<Transition>
    {
    	[Token(Token = "0x4001147")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	public readonly string Name;

    	[Token(Token = "0x4001148")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    	public readonly Texture DissolveTexture;

    	[Token(Token = "0x4001149")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly Vector4? parameters;

    	[Token(Token = "0x17000331")]
    	public Vector4 Parameters
    	{
    		[Token(Token = "0x60016CB")]
    		[Address(RVA = "0x5E8EA0", Offset = "0x5E78A0", VA = "0x1805E8EA0")]
    		get
    		{
    			return default(Vector4);
    		}
    	}

    	[Token(Token = "0x60016CC")]
    	[Address(RVA = "0x5E9060", Offset = "0x5E7A60", VA = "0x1805E9060")]
    	public Transition(string name, [Optional] Vector4? parameters, [Optional] Texture dissolveTexture)
    	{ this = default; }

    	[Token(Token = "0x60016CD")]
    	[Address(RVA = "0x5E9140", Offset = "0x5E7B40", VA = "0x1805E9140", Slot = "4")]
    	public bool Equals(Transition other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60016CE")]
    	[Address(RVA = "0x5E9270", Offset = "0x5E7C70", VA = "0x1805E9270", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60016CF")]
    	[Address(RVA = "0x5E9320", Offset = "0x5E7D20", VA = "0x1805E9320", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x60016D0")]
    	[Address(RVA = "0x5E9490", Offset = "0x5E7E90", VA = "0x1805E9490")]
    	public static bool operator ==(Transition left, Transition right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60016D1")]
    	[Address(RVA = "0x5E94D0", Offset = "0x5E7ED0", VA = "0x1805E94D0")]
    	public static bool operator !=(Transition left, Transition right)
    	{
    		return default(bool);
    	}
    }
}
