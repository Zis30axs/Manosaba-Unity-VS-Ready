using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x20003DD")]
    public struct ConstantLocalization
    {
    	[Token(Token = "0x4000E93")]
    	[FieldOffset(Offset = "0x0")]
    	[Tooltip("Name of the C# enum type baking the constant.")]
    	public string TypeName;

    	[Token(Token = "0x4000E94")]
    	[FieldOffset(Offset = "0x8")]
    	[Tooltip("Locale-specific values to use for the constant.")]
    	public List<ConstantValueLocalization> Values;
    }
}
