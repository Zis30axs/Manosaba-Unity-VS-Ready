using System;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000274")]
    public readonly struct ProvisionSource : IEquatable<ProvisionSource>
    {
    	[Token(Token = "0x4000983")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly IResourceProvider Provider;

    	[Token(Token = "0x4000984")]
    	[FieldOffset(Offset = "0x8")]
    	public readonly string PathPrefix;

    	[Token(Token = "0x6000ADE")]
    	[Address(RVA = "0x42B240", Offset = "0x429C40", VA = "0x18042B240")]
    	public ProvisionSource(IResourceProvider provider, string pathPrefix)
    	{ this = default; }

    	[Token(Token = "0x6000ADF")]
    	[Address(RVA = "0x534FB0", Offset = "0x5339B0", VA = "0x180534FB0")]
    	public static string BuildFullPath(string pathPrefix, string localPath)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000AE0")]
    	[Address(RVA = "0x5350F0", Offset = "0x533AF0", VA = "0x1805350F0")]
    	public static string BuildLocalPath(string pathPrefix, string fullPath)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000AE1")]
    	[Address(RVA = "0x535520", Offset = "0x533F20", VA = "0x180535520")]
    	public string BuildFullPath(string localPath)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000AE2")]
    	[Address(RVA = "0x535660", Offset = "0x534060", VA = "0x180535660")]
    	public string BuildLocalPath(string fullPath)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000AE3")]
    	[Address(RVA = "0x535670", Offset = "0x534070", VA = "0x180535670", Slot = "4")]
    	public bool Equals(ProvisionSource other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000AE4")]
    	[Address(RVA = "0x535710", Offset = "0x534110", VA = "0x180535710", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000AE5")]
    	[Address(RVA = "0x535670", Offset = "0x534070", VA = "0x180535670")]
    	public static bool operator ==(ProvisionSource left, ProvisionSource right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000AE6")]
    	[Address(RVA = "0x535820", Offset = "0x534220", VA = "0x180535820")]
    	public static bool operator !=(ProvisionSource left, ProvisionSource right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000AE7")]
    	[Address(RVA = "0x5358C0", Offset = "0x5342C0", VA = "0x1805358C0", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
