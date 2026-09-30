using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20000A6")]
    public class LayeredCameraLayer : ILayeredRendererLayer, IDisposable
    {
    	[Token(Token = "0x4000256")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly GameObject go;

    	[Token(Token = "0x4000257")]
    	[FieldOffset(Offset = "0x28")]
    	private readonly LayeredActorLayer layer;

    	[Token(Token = "0x17000080")]
    	public string Name
    	{
    		[Token(Token = "0x600030B")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000081")]
    	public string Group
    	{
    		[Token(Token = "0x600030C")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "5")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000082")]
    	public bool Enabled
    	{
    		[Token(Token = "0x600030D")]
    		[Address(RVA = "0x4686E0", Offset = "0x4670E0", VA = "0x1804686E0", Slot = "6")]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x600030E")]
    		[Address(RVA = "0x3407A0", Offset = "0x33F1A0", VA = "0x1803407A0", Slot = "7")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000083")]
    	public LayeredActorLayer Layer
    	{
    		[Token(Token = "0x600030F")]
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060", Slot = "8")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000310")]
    	[Address(RVA = "0x468780", Offset = "0x467180", VA = "0x180468780")]
    	public LayeredCameraLayer(GameObject go)
    	{
    	}

    	[Token(Token = "0x6000311")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "9")]
    	public void Dispose()
    	{
    	}
    }
}
