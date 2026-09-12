using System;
using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x200005D")]
[Flags]
public enum ErrorType
{
	[Token(Token = "0x40000FB")]
	SpaceInLabel = 1,
	[Token(Token = "0x40000FC")]
	MissingLabel = 2,
	[Token(Token = "0x40000FD")]
	MissingCommandId = 4,
	[Token(Token = "0x40000FE")]
	MissingParamId = 8,
	[Token(Token = "0x40000FF")]
	MissingParamValue = 0x10,
	[Token(Token = "0x4000100")]
	MultipleNameless = 0x20,
	[Token(Token = "0x4000101")]
	MissingAppearance = 0x40,
	[Token(Token = "0x4000102")]
	MissingExpressionBody = 0x80,
	[Token(Token = "0x4000103")]
	MissingTextIdBody = 0x100,
	[Token(Token = "0x4000104")]
	ExpressionInGenericPrefix = 0x200
}
