using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20000DC")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = true)]
    public sealed class AssignmentContextAttribute : ParameterContextAttribute
    {
    	[Token(Token = "0x6000417")]
    	[Address(RVA = "0x476F90", Offset = "0x475990", VA = "0x180476F90")]
    	public AssignmentContextAttribute(int index = -1, string paramId = default)
    	{
    	}
    }
}
