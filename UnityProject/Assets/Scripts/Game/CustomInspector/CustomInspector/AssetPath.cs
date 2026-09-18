using System;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Serializable]
    [Token(Token = "0x2000050")]
    public abstract class AssetPath
    {
    	[Token(Token = "0x4000145")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private string path;

    	[Token(Token = "0x4000146")]
    	protected const string invalidString = "<invalid>";

    	[Token(Token = "0x4000147")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly Type RequiredType;

    	[Token(Token = "0x4000148")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	[HideInInspector]
    	private UnityEngine.Object assetReference;

    	[Token(Token = "0x1700000D")]
    	public string Name
    	{
    		[Token(Token = "0x6000085")]
    		[Address(RVA = "0x43E960", Offset = "0x43D360", VA = "0x18043E960")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700000E")]
    	public Type AssetType
    	{
    		[Token(Token = "0x600008B")]
    		[Address(RVA = "0x43EE40", Offset = "0x43D840", VA = "0x18043EE40")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000086")]
    	[Address(RVA = "0x43E970", Offset = "0x43D370", VA = "0x18043E970")]
    	protected AssetPath(string defaultPath = "<invalid>")
    	{
    	}

    	[Token(Token = "0x6000087")]
    	[Address(RVA = "0x43EAB0", Offset = "0x43D4B0", VA = "0x18043EAB0")]
    	protected AssetPath(Type fileType)
    	{
    	}

    	[Token(Token = "0x6000088")]
    	[Address(RVA = "0x43EC60", Offset = "0x43D660", VA = "0x18043EC60")]
    	protected AssetPath(string defaultPath, Type fileType)
    	{
    	}

    	[Token(Token = "0x6000089")]
    	[Address(RVA = "0x43EE00", Offset = "0x43D800", VA = "0x18043EE00")]
    	public string GetPath()
    	{
    		return null;
    	}

    	[Token(Token = "0x600008A")]
    	[Address(RVA = "0x43EE10", Offset = "0x43D810", VA = "0x18043EE10")]
    	public string GetFileName()
    	{
    		return null;
    	}

    	[Token(Token = "0x600008C")]
    	[Address(RVA = "0x43EE90", Offset = "0x43D890", VA = "0x18043EE90")]
    	public string GetAbsolutePath()
    	{
    		return null;
    	}

    	[Token(Token = "0x600008D")]
    	[Address(RVA = "0x43EEC0", Offset = "0x43D8C0", VA = "0x18043EEC0")]
    	public bool SetPath(string path)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600008E")]
    	[Address(RVA = "0x43EEE0", Offset = "0x43D8E0", VA = "0x18043EEE0")]
    	public bool HasPath()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600008F")]
    	[Address(RVA = "0x43EF00", Offset = "0x43D900", VA = "0x18043EF00")]
    	public bool HasEnding(string ending)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000090")]
    	[Address(RVA = "0x43EF40", Offset = "0x43D940", VA = "0x18043EF40")]
    	public static bool IsValidPath(string path, Type fileType)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000091")]
    	[Address(RVA = "0x43EF90", Offset = "0x43D990", VA = "0x18043EF90", Slot = "3")]
    	public override string ToString()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000092")]
    	[Address(RVA = "0x43EFE0", Offset = "0x43D9E0", VA = "0x18043EFE0")]
    	public static string AbsoluteToRelativePath(string absolute)
    	{
    		return null;
    	}
    }
}
