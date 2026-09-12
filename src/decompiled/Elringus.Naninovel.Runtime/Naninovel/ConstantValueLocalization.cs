using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003DE")]
public struct ConstantValueLocalization
{
	[Token(Token = "0x4000E95")]
	[FieldOffset(Offset = "0x0")]
	[Tooltip("Name of the C# enum value baking the constant value.")]
	public string Value;

	[Token(Token = "0x4000E96")]
	[FieldOffset(Offset = "0x8")]
	[Tooltip("Locale-specific alias to use for the value.")]
	public string Alias;
}
