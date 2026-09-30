using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x20003E0")]
    public struct ParameterLocalization
    {
    	[Token(Token = "0x4000E99")]
    	[FieldOffset(Offset = "0x0")]
    	[Tooltip("Identifier (field name) of the parameter to localize alias for.")]
    	public string Id;

    	[Token(Token = "0x4000E9A")]
    	[FieldOffset(Offset = "0x8")]
    	[Tooltip("Locale-specific alias of the parameter.")]
    	public string Alias;

    	[Token(Token = "0x4000E9B")]
    	[FieldOffset(Offset = "0x10")]
    	[Tooltip("Locale-specific documentation summary of the parameter.")]
    	public string Summary;
    }
}
