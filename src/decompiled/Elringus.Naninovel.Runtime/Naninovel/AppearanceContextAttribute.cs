using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20000DB")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
public sealed class AppearanceContextAttribute : ParameterContextAttribute
{
	[Token(Token = "0x6000416")]
	[Address(RVA = "0x476F60", Offset = "0x475960", VA = "0x180476F60")]
	public AppearanceContextAttribute(int index = -1, string paramId = default, string actorId = default)
	{
	}
}
