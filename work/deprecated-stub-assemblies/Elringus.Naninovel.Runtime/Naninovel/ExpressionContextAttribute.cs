using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20000E1")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true)]
    public class ExpressionContextAttribute : ParameterContextAttribute
    {
    	[Token(Token = "0x600041D")]
    	[Address(RVA = "0x477270", Offset = "0x475C70", VA = "0x180477270")]
    	public ExpressionContextAttribute(int index = -1, string paramId = default)
    	{
    	}
    }
}
