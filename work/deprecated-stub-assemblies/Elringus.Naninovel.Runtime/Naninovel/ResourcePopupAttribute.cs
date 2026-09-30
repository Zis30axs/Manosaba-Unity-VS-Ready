using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20000E6")]
    public class ResourcePopupAttribute : PropertyAttribute
    {
    	[Token(Token = "0x400034E")]
    	public const string EmptyValue = "None (disabled)";

    	[Token(Token = "0x400034F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	public readonly string Category;

    	[Token(Token = "0x4000350")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	public readonly string PathPrefix;

    	[Token(Token = "0x4000351")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	public readonly string EmptyOption;

    	[Token(Token = "0x6000422")]
    	[Address(RVA = "0x4773B0", Offset = "0x475DB0", VA = "0x1804773B0")]
    	public ResourcePopupAttribute([Optional] string category, [Optional] string pathPrefix, [Optional] string emptyOption)
    	{
    	}

    	[Token(Token = "0x6000423")]
    	[Address(RVA = "0x4774C0", Offset = "0x475EC0", VA = "0x1804774C0")]
    	public ResourcePopupAttribute(string category)
    	{
    	}
    }
}
