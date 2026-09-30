using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000021")]
    public abstract class ActorState
    {
    	[Token(Token = "0x400008C")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private string appearance;

    	[Token(Token = "0x400008D")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private bool visible;

    	[Token(Token = "0x400008E")]
    	[FieldOffset(Offset = "0x1C")]
    	[ScenePosition]
    	[SerializeField]
    	private Vector3 position;

    	[Token(Token = "0x400008F")]
    	[FieldOffset(Offset = "0x28")]
    	[DrawAsEuler]
    	[SerializeField]
    	private Quaternion rotation;

    	[Token(Token = "0x4000090")]
    	[FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private Vector3 scale;

    	[Token(Token = "0x4000091")]
    	[FieldOffset(Offset = "0x44")]
    	[SerializeField]
    	private Color tintColor;

    	[Token(Token = "0x17000011")]
    	public string Appearance
    	{
    		[Token(Token = "0x600008E")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000012")]
    	public bool Visible
    	{
    		[Token(Token = "0x600008F")]
    		[Address(RVA = "0x44BDC0", Offset = "0x44A7C0", VA = "0x18044BDC0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000013")]
    	public Vector3 Position
    	{
    		[Token(Token = "0x6000090")]
    		[Address(RVA = "0x44BDD0", Offset = "0x44A7D0", VA = "0x18044BDD0")]
    		get
    		{
    			return default(Vector3);
    		}
    	}

    	[Token(Token = "0x17000014")]
    	public Quaternion Rotation
    	{
    		[Token(Token = "0x6000091")]
    		[Address(RVA = "0x44BDF0", Offset = "0x44A7F0", VA = "0x18044BDF0")]
    		get
    		{
    			return default(Quaternion);
    		}
    	}

    	[Token(Token = "0x17000015")]
    	public Vector3 Scale
    	{
    		[Token(Token = "0x6000092")]
    		[Address(RVA = "0x44BE00", Offset = "0x44A800", VA = "0x18044BE00")]
    		get
    		{
    			return default(Vector3);
    		}
    	}

    	[Token(Token = "0x17000016")]
    	public Color TintColor
    	{
    		[Token(Token = "0x6000093")]
    		[Address(RVA = "0x44BE20", Offset = "0x44A820", VA = "0x18044BE20")]
    		get
    		{
    			return default(Color);
    		}
    	}

    	[Token(Token = "0x6000094")]
    	[Address(RVA = "0x44BE30", Offset = "0x44A830", VA = "0x18044BE30")]
    	public string ToJson()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000095")]
    	[Address(RVA = "0x44BE40", Offset = "0x44A840", VA = "0x18044BE40")]
    	public void OverwriteFromJson(string json)
    	{
    	}

    	[Token(Token = "0x6000096")]
    	[Address(RVA = "0x44C000", Offset = "0x44AA00", VA = "0x18044C000")]
    	public void OverwriteFromActor(IActor actor)
    	{
    	}

    	[Token(Token = "0x6000097")]
    	[Address(RVA = "0x44C150", Offset = "0x44AB50", VA = "0x18044C150")]
    	public UniTask ApplyToActor(IActor actor)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000098")]
    	[Address(RVA = "0x44C460", Offset = "0x44AE60", VA = "0x18044C460")]
    	protected ActorState()
    	{
    	}
    }
    [Serializable]
    [Token(Token = "0x2000022")]
    public abstract class ActorState<TActor> : ActorState where TActor : IActor
    {
    	[Token(Token = "0x6000099")]
    	public virtual void OverwriteFromActor(TActor actor)
    	{
    	}

    	[Token(Token = "0x600009A")]
    	public virtual UniTask ApplyToActor(TActor actor)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600009B")]
    	protected ActorState()
    	{
    	}
    }
}
