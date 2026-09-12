using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003DF")]
public struct FunctionLocalization
{
	[Token(Token = "0x4000E97")]
	[FieldOffset(Offset = "0x0")]
	[Tooltip("Name of the C# method baking the expression function.")]
	public string MethodName;

	[Token(Token = "0x4000E98")]
	[FieldOffset(Offset = "0x8")]
	[Tooltip("Locale-specific alias to use for the function.")]
	public string Alias;
}
