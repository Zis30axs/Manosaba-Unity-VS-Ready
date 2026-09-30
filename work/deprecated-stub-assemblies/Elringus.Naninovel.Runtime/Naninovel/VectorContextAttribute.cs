using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20000E4")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public sealed class VectorContextAttribute : ParameterContextAttribute
    {
    	[Token(Token = "0x6000420")]
    	[Address(RVA = "0x477380", Offset = "0x475D80", VA = "0x180477380")]
    	public VectorContextAttribute(string components, int index = -1, string paramId = default)
    	{
    	}
    }
}
