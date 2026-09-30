using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20000E3")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public sealed class ResourceContextAttribute : ParameterContextAttribute
    {
    	[Token(Token = "0x600041F")]
    	[Address(RVA = "0x477350", Offset = "0x475D50", VA = "0x180477350")]
    	public ResourceContextAttribute(string pathPrefix, int index = -1, string paramId = default)
    	{
    	}
    }
}
