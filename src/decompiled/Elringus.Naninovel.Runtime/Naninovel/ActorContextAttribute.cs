using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20000DA")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
public sealed class ActorContextAttribute : ParameterContextAttribute
{
	[Token(Token = "0x6000415")]
	[Address(RVA = "0x476F30", Offset = "0x475930", VA = "0x180476F30")]
	public ActorContextAttribute(string pathPrefix = "*", int index = -1, string paramId = default)
	{
	}
}
