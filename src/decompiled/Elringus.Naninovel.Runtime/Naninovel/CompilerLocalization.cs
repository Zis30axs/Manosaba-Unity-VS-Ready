using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using Naninovel.Parsing;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20003DB")]
[CreateAssetMenu]
public class CompilerLocalization : ScriptableObject
{
	[Token(Token = "0x4000E75")]
	[FieldOffset(Offset = "0x18")]
	[Tooltip("Marks beginning of comment lines. ';' by default.")]
	public string CommentLine;

	[Token(Token = "0x4000E76")]
	[FieldOffset(Offset = "0x20")]
	[Tooltip("Marks beginning of label lines. '#' by default.")]
	public string LabelLine;

	[Token(Token = "0x4000E77")]
	[FieldOffset(Offset = "0x28")]
	[Tooltip("Marks beginning of the command lines. '@' by default.")]
	public string CommandLine;

	[Token(Token = "0x4000E78")]
	[FieldOffset(Offset = "0x30")]
	[Tooltip("Delimits author prefix from generic text content. ': ' by default.")]
	public string AuthorAssign;

	[Token(Token = "0x4000E79")]
	[FieldOffset(Offset = "0x38")]
	[Tooltip("Delimits author appearance from author identifier in generic text line prefix. '.' by default.")]
	public string AuthorAppearance;

	[Token(Token = "0x4000E7A")]
	[FieldOffset(Offset = "0x40")]
	[Tooltip("Marks beginning of script expression. '{' by default.")]
	public string ExpressionOpen;

	[Token(Token = "0x4000E7B")]
	[FieldOffset(Offset = "0x48")]
	[Tooltip("Marks end of script expression. '}' by default.")]
	public string ExpressionClose;

	[Token(Token = "0x4000E7C")]
	[FieldOffset(Offset = "0x50")]
	[Tooltip("Marks beginning of inlined command in generic text line. '[' by default.")]
	public string InlinedOpen;

	[Token(Token = "0x4000E7D")]
	[FieldOffset(Offset = "0x58")]
	[Tooltip("Marks end of inlined command in generic text line. ']' by default.")]
	public string InlinedClose;

	[Token(Token = "0x4000E7E")]
	[FieldOffset(Offset = "0x60")]
	[Tooltip("Delimits command parameter value from parameter identifier. ':' by default.")]
	public string ParameterAssign;

	[Token(Token = "0x4000E7F")]
	[FieldOffset(Offset = "0x68")]
	[Tooltip("Delimits items in list parameter value. ',' by default.")]
	public string ListDelimiter;

	[Token(Token = "0x4000E80")]
	[FieldOffset(Offset = "0x70")]
	[Tooltip("Delimits value from name in named parameter value. '.' by default.")]
	public string NamedDelimiter;

	[Token(Token = "0x4000E81")]
	[FieldOffset(Offset = "0x78")]
	[Tooltip("Marks beginning of text identifier in localizable text parameter value and generic text line. '|#' by default.")]
	public string TextIdOpen;

	[Token(Token = "0x4000E82")]
	[FieldOffset(Offset = "0x80")]
	[Tooltip("Marks end of text identifier in localizable text parameter value and generic text line. '|' by default.")]
	public string TextIdClose;

	[Token(Token = "0x4000E83")]
	[FieldOffset(Offset = "0x88")]
	[Tooltip("The flag placed before/after identifier of boolean command parameter to represent negative/positive value. '!' by default.")]
	public string BooleanFlag;

	[Token(Token = "0x4000E84")]
	[FieldOffset(Offset = "0x90")]
	[Tooltip("Constant representing positive boolean value. 'true' by default.")]
	public string True;

	[Token(Token = "0x4000E85")]
	[FieldOffset(Offset = "0x98")]
	[Tooltip("Constant representing negative boolean value. 'false' by default.")]
	public string False;

	[Token(Token = "0x4000E86")]
	[FieldOffset(Offset = "0xA0")]
	[Tooltip("Prefix to identify global script variables. 'g_' by default.")]
	public string GlobalVariablePrefix;

	[Token(Token = "0x4000E87")]
	[FieldOffset(Offset = "0xA8")]
	[Tooltip("Prefix to identify script constants pulled from managed text. 't_' by default.")]
	public string ScriptConstantPrefix;

	[Token(Token = "0x4000E88")]
	[FieldOffset(Offset = "0xB0")]
	[Tooltip("Locale-specific command aliases.")]
	public List<CommandLocalization> Commands;

	[Token(Token = "0x4000E89")]
	[FieldOffset(Offset = "0xB8")]
	[Tooltip("Locale-specific expression function aliases.")]
	public List<FunctionLocalization> Functions;

	[Token(Token = "0x4000E8A")]
	[FieldOffset(Offset = "0xC0")]
	[Tooltip("Locale-specific constant aliases.")]
	public List<ConstantLocalization> Constants;

	[Token(Token = "0x60012AA")]
	[Address(RVA = "0x5A2AA0", Offset = "0x5A14A0", VA = "0x1805A2AA0")]
	public Syntax GetSyntax()
	{
		return null;
	}

	[Token(Token = "0x60012AB")]
	[Address(RVA = "0x5A2C60", Offset = "0x5A1660", VA = "0x1805A2C60")]
	[ContextMenu("Add Existing Commands")]
	private void AddExistingCommands()
	{
	}

	[Token(Token = "0x60012AC")]
	[Address(RVA = "0x5A32F0", Offset = "0x5A1CF0", VA = "0x1805A32F0")]
	[ContextMenu("Add Existing Functions")]
	private void AddExistingFunctions()
	{
	}

	[Token(Token = "0x60012AD")]
	[Address(RVA = "0x5A3A20", Offset = "0x5A2420", VA = "0x1805A3A20")]
	private List<ParameterLocalization> AddExistingParameters(Type commandType)
	{
		return null;
	}

	[Token(Token = "0x60012AE")]
	[Address(RVA = "0x5A3D80", Offset = "0x5A2780", VA = "0x1805A3D80")]
	public CompilerLocalization()
	{
	}
}
