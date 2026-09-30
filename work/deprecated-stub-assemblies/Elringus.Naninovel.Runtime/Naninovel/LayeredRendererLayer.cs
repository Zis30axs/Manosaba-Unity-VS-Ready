using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20000A3")]
    public class LayeredRendererLayer : ILayeredRendererLayer, IDisposable
    {
    	[Token(Token = "0x400024C")]
    	[FieldOffset(Offset = "0x20")]
    	public readonly Mesh Mesh;

    	[Token(Token = "0x400024D")]
    	[FieldOffset(Offset = "0x28")]
    	public readonly Renderer Renderer;

    	[Token(Token = "0x400024E")]
    	[FieldOffset(Offset = "0x30")]
    	public readonly Transform Transform;

    	[Token(Token = "0x400024F")]
    	[FieldOffset(Offset = "0x0")]
    	private static readonly int spriteColorId;

    	[Token(Token = "0x4000250")]
    	[FieldOffset(Offset = "0x38")]
    	private readonly SpriteRenderer spriteRenderer;

    	[Token(Token = "0x17000079")]
    	public string Name
    	{
    		[Token(Token = "0x60002F8")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700007A")]
    	public string Group
    	{
    		[Token(Token = "0x60002F9")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "5")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700007B")]
    	public LayeredActorLayer Layer
    	{
    		[Token(Token = "0x60002FA")]
    		[Address(RVA = "0x467250", Offset = "0x465C50", VA = "0x180467250", Slot = "8")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700007C")]
    	public bool Enabled
    	{
    		[Token(Token = "0x60002FB")]
    		[Address(RVA = "0x4672A0", Offset = "0x465CA0", VA = "0x1804672A0", Slot = "6")]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x60002FC")]
    		[Address(RVA = "0x467340", Offset = "0x465D40", VA = "0x180467340", Slot = "7")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700007D")]
    	public Vector2 Position
    	{
    		[Token(Token = "0x60002FD")]
    		[Address(RVA = "0x467360", Offset = "0x465D60", VA = "0x180467360")]
    		get
    		{
    			return default(Vector2);
    		}
    	}

    	[Token(Token = "0x1700007E")]
    	public Quaternion Rotation
    	{
    		[Token(Token = "0x60002FE")]
    		[Address(RVA = "0x467430", Offset = "0x465E30", VA = "0x180467430")]
    		get
    		{
    			return default(Quaternion);
    		}
    	}

    	[Token(Token = "0x1700007F")]
    	public Vector2 Scale
    	{
    		[Token(Token = "0x60002FF")]
    		[Address(RVA = "0x467500", Offset = "0x465F00", VA = "0x180467500")]
    		get
    		{
    			return default(Vector2);
    		}
    	}

    	[Token(Token = "0x6000300")]
    	[Address(RVA = "0x4675D0", Offset = "0x465FD0", VA = "0x1804675D0")]
    	public LayeredRendererLayer(Renderer renderer, Mesh mesh)
    	{
    	}

    	[Token(Token = "0x6000301")]
    	[Address(RVA = "0x467920", Offset = "0x466320", VA = "0x180467920")]
    	public LayeredRendererLayer(SpriteRenderer spriteRenderer)
    	{
    	}

    	[Token(Token = "0x6000302")]
    	[Address(RVA = "0x4679E0", Offset = "0x4663E0", VA = "0x1804679E0", Slot = "9")]
    	public void Dispose()
    	{
    	}

    	[Token(Token = "0x6000303")]
    	[Address(RVA = "0x467BB0", Offset = "0x4665B0", VA = "0x180467BB0")]
    	public MaterialPropertyBlock GetPropertyBlock(MaterialPropertyBlock block)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000304")]
    	[Address(RVA = "0x467ED0", Offset = "0x4668D0", VA = "0x180467ED0")]
    	private static Mesh BuildSpriteMesh(SpriteRenderer spriteRenderer)
    	{
    		return null;
    	}
    }
}
