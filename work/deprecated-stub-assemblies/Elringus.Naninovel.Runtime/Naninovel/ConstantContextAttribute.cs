using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20000DF")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public class ConstantContextAttribute : ParameterContextAttribute
    {
    	[Token(Token = "0x4000349")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	public readonly Type EnumType;

    	[Token(Token = "0x600041A")]
    	[Address(RVA = "0x4770E0", Offset = "0x475AE0", VA = "0x1804770E0")]
    	public ConstantContextAttribute(Type enumType, int index = -1, string paramId = default)
    	{
    	}

    	[Token(Token = "0x600041B")]
    	[Address(RVA = "0x477210", Offset = "0x475C10", VA = "0x180477210")]
    	public ConstantContextAttribute(string nameExpression, int index = -1, string paramId = default)
    	{
    	}
    }
}
