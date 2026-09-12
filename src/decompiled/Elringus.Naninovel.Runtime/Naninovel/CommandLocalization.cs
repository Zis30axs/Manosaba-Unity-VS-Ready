using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20003D8")]
public struct CommandLocalization
{
	[Token(Token = "0x4000E5E")]
	[FieldOffset(Offset = "0x0")]
	[Tooltip("Identifier (implementation type name) of the command to localize alias for.")]
	public string Id;

	[Token(Token = "0x4000E5F")]
	[FieldOffset(Offset = "0x8")]
	[Tooltip("Locale-specific alias of the command.")]
	public string Alias;

	[Token(Token = "0x4000E60")]
	[FieldOffset(Offset = "0x10")]
	[Tooltip("Locale-specific documentation summary of the command.")]
	public string Summary;

	[Token(Token = "0x4000E61")]
	[FieldOffset(Offset = "0x18")]
	[Tooltip("Locale-specific documentation remarks for the command.")]
	public string Remarks;

	[Token(Token = "0x4000E62")]
	[FieldOffset(Offset = "0x20")]
	[Tooltip("Locale-specific documentation examples for the command.")]
	public string[] Examples;

	[Token(Token = "0x4000E63")]
	[FieldOffset(Offset = "0x28")]
	[Tooltip("Locale-specific parameters of the command.")]
	public List<ParameterLocalization> Parameters;
}
