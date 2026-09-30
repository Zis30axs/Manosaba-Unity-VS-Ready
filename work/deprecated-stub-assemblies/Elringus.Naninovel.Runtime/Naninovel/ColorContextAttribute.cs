using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20000DD")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public sealed class ColorContextAttribute : ParameterContextAttribute
    {
    	[Token(Token = "0x6000418")]
    	[Address(RVA = "0x477000", Offset = "0x475A00", VA = "0x180477000")]
    	public ColorContextAttribute(int index = -1, string paramId = default)
    	{
    	}
    }
}
