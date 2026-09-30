using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20000DE")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true)]
    public sealed class ConditionContextAttribute : ParameterContextAttribute
    {
    	[Token(Token = "0x6000419")]
    	[Address(RVA = "0x477070", Offset = "0x475A70", VA = "0x180477070")]
    	public ConditionContextAttribute(int index = -1, string paramId = default)
    	{
    	}
    }
}
