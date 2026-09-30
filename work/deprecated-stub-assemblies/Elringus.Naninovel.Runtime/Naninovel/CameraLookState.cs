using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000114")]
    public struct CameraLookState : IEquatable<CameraLookState>
    {
    	[Token(Token = "0x400043F")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private bool enabled;

    	[Token(Token = "0x4000440")]
    	[FieldOffset(Offset = "0x1")]
    	[SerializeField]
    	private bool gravity;

    	[Token(Token = "0x4000441")]
    	[FieldOffset(Offset = "0x4")]
    	[SerializeField]
    	private Vector2 zone;

    	[Token(Token = "0x4000442")]
    	[FieldOffset(Offset = "0xC")]
    	[SerializeField]
    	private Vector2 speed;

    	[Token(Token = "0x170000D0")]
    	public bool Enabled
    	{
    		[Token(Token = "0x60004FB")]
    		[Address(RVA = "0x486730", Offset = "0x485130", VA = "0x180486730")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170000D1")]
    	public bool Gravity
    	{
    		[Token(Token = "0x60004FC")]
    		[Address(RVA = "0x486740", Offset = "0x485140", VA = "0x180486740")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170000D2")]
    	public Vector2 Zone
    	{
    		[Token(Token = "0x60004FD")]
    		[Address(RVA = "0x486750", Offset = "0x485150", VA = "0x180486750")]
    		get
    		{
    			return default(Vector2);
    		}
    	}

    	[Token(Token = "0x170000D3")]
    	public Vector2 Speed
    	{
    		[Token(Token = "0x60004FE")]
    		[Address(RVA = "0x486770", Offset = "0x485170", VA = "0x180486770")]
    		get
    		{
    			return default(Vector2);
    		}
    	}

    	[Token(Token = "0x60004FF")]
    	[Address(RVA = "0x486790", Offset = "0x485190", VA = "0x180486790")]
    	public CameraLookState(bool enabled, bool gravity, Vector2 zone, Vector2 speed)
    	{ this = default; }

    	[Token(Token = "0x6000500")]
    	[Address(RVA = "0x4867B0", Offset = "0x4851B0", VA = "0x1804867B0", Slot = "4")]
    	public bool Equals(CameraLookState other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000501")]
    	[Address(RVA = "0x486820", Offset = "0x485220", VA = "0x180486820", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000502")]
    	[Address(RVA = "0x486900", Offset = "0x485300", VA = "0x180486900", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6000503")]
    	[Address(RVA = "0x4869E0", Offset = "0x4853E0", VA = "0x1804869E0")]
    	public static bool operator ==(CameraLookState left, CameraLookState right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000504")]
    	[Address(RVA = "0x486A50", Offset = "0x485450", VA = "0x180486A50")]
    	public static bool operator !=(CameraLookState left, CameraLookState right)
    	{
    		return default(bool);
    	}
    }
}
