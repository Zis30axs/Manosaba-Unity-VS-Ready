#nullable enable annotations
﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20000A8")]
    [ActorResources(null, false)]
    public class NarratorCharacter : ICharacterActor, IActor
    {
    	[Token(Token = "0x17000084")]
    	public string Id
    	{
    		[Token(Token = "0x6000313")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "7")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000085")]
    	public string Appearance
    	{
    		[Token(Token = "0x6000314")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "8")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6000315")]
    		[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200", Slot = "9")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000086")]
    	public bool Visible
    	{
    		[Token(Token = "0x6000316")]
    		[Address(RVA = "0x468AC0", Offset = "0x4674C0", VA = "0x180468AC0", Slot = "10")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000317")]
    		[Address(RVA = "0x468AD0", Offset = "0x4674D0", VA = "0x180468AD0", Slot = "11")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000087")]
    	public Vector3 Position
    	{
    		[Token(Token = "0x6000318")]
    		[Address(RVA = "0x468AE0", Offset = "0x4674E0", VA = "0x180468AE0", Slot = "12")]
    		[CompilerGenerated]
    		get
    		{
    			return default(Vector3);
    		}
    		[Token(Token = "0x6000319")]
    		[Address(RVA = "0x468B00", Offset = "0x467500", VA = "0x180468B00", Slot = "13")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000088")]
    	public Quaternion Rotation
    	{
    		[Token(Token = "0x600031A")]
    		[Address(RVA = "0x468B10", Offset = "0x467510", VA = "0x180468B10", Slot = "14")]
    		[CompilerGenerated]
    		get
    		{
    			return default(Quaternion);
    		}
    		[Token(Token = "0x600031B")]
    		[Address(RVA = "0x468B20", Offset = "0x467520", VA = "0x180468B20", Slot = "15")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000089")]
    	public Vector3 Scale
    	{
    		[Token(Token = "0x600031C")]
    		[Address(RVA = "0x468B30", Offset = "0x467530", VA = "0x180468B30", Slot = "16")]
    		[CompilerGenerated]
    		get
    		{
    			return default(Vector3);
    		}
    		[Token(Token = "0x600031D")]
    		[Address(RVA = "0x468B50", Offset = "0x467550", VA = "0x180468B50", Slot = "17")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700008A")]
    	public Color TintColor
    	{
    		[Token(Token = "0x600031E")]
    		[Address(RVA = "0x468B60", Offset = "0x467560", VA = "0x180468B60", Slot = "18")]
    		[CompilerGenerated]
    		get
    		{
    			return default(Color);
    		}
    		[Token(Token = "0x600031F")]
    		[Address(RVA = "0x468B70", Offset = "0x467570", VA = "0x180468B70", Slot = "19")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700008B")]
    	public CharacterLookDirection LookDirection
    	{
    		[Token(Token = "0x6000320")]
    		[Address(RVA = "0x468B80", Offset = "0x467580", VA = "0x180468B80", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return default(CharacterLookDirection);
    		}
    		[Token(Token = "0x6000321")]
    		[Address(RVA = "0x468B90", Offset = "0x467590", VA = "0x180468B90", Slot = "5")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x6000322")]
    	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    	public NarratorCharacter(string id, CharacterMetadata metadata)
    	{
    	}

    	[Token(Token = "0x6000323")]
    	[Address(RVA = "0x468BA0", Offset = "0x4675A0", VA = "0x180468BA0", Slot = "20")]
    	public UniTask Initialize()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000324")]
    	[Address(RVA = "0x468BF0", Offset = "0x4675F0", VA = "0x180468BF0", Slot = "21")]
    	public UniTask ChangeAppearance(string appearance, Tween tween, [Optional] Transition? transition, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000325")]
    	[Address(RVA = "0x468C40", Offset = "0x467640", VA = "0x180468C40", Slot = "22")]
    	public UniTask ChangeVisibility(bool visible, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000326")]
    	[Address(RVA = "0x468C90", Offset = "0x467690", VA = "0x180468C90", Slot = "23")]
    	public UniTask ChangePosition(Vector3 position, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000327")]
    	[Address(RVA = "0x468CE0", Offset = "0x4676E0", VA = "0x180468CE0", Slot = "24")]
    	public UniTask ChangeRotation(Quaternion rotation, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000328")]
    	[Address(RVA = "0x468D30", Offset = "0x467730", VA = "0x180468D30", Slot = "25")]
    	public UniTask ChangeScale(Vector3 scale, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000329")]
    	[Address(RVA = "0x468D80", Offset = "0x467780", VA = "0x180468D80", Slot = "26")]
    	public UniTask ChangeTintColor(Color tintColor, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600032A")]
    	[Address(RVA = "0x468DD0", Offset = "0x4677D0", VA = "0x180468DD0", Slot = "6")]
    	public UniTask ChangeLookDirection(CharacterLookDirection lookDirection, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}
    }
}
