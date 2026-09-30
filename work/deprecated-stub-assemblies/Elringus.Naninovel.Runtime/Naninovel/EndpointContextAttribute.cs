using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20000E0")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public sealed class EndpointContextAttribute : ParameterContextAttribute
    {
    	[Token(Token = "0x600041C")]
    	[Address(RVA = "0x477240", Offset = "0x475C40", VA = "0x180477240")]
    	public EndpointContextAttribute([Optional] string paramId)
    	{
    	}
    }
}
