using System;
using Il2CppDummyDll;

namespace Naninovel.Parsing
{
    [Token(Token = "0x2000061")]
    [Flags]
    public enum TokenType
    {
    	[Token(Token = "0x400010E")]
    	Error = 1,
    	[Token(Token = "0x400010F")]
    	LineId = 2,
    	[Token(Token = "0x4000110")]
    	CommentText = 4,
    	[Token(Token = "0x4000111")]
    	LabelText = 8,
    	[Token(Token = "0x4000112")]
    	GenericText = 0x10,
    	[Token(Token = "0x4000113")]
    	CommandBody = 0x20,
    	[Token(Token = "0x4000114")]
    	CommandId = 0x40,
    	[Token(Token = "0x4000115")]
    	NamelessParam = 0x80,
    	[Token(Token = "0x4000116")]
    	NamedParam = 0x100,
    	[Token(Token = "0x4000117")]
    	ParamId = 0x200,
    	[Token(Token = "0x4000118")]
    	ParamValue = 0x400,
    	[Token(Token = "0x4000119")]
    	ParamAssign = 0x800,
    	[Token(Token = "0x400011A")]
    	Expression = 0x1000,
    	[Token(Token = "0x400011B")]
    	ExpressionBody = 0x2000,
    	[Token(Token = "0x400011C")]
    	ExpressionOpen = 0x4000,
    	[Token(Token = "0x400011D")]
    	ExpressionClose = 0x8000,
    	[Token(Token = "0x400011E")]
    	Inlined = 0x10000,
    	[Token(Token = "0x400011F")]
    	InlinedOpen = 0x20000,
    	[Token(Token = "0x4000120")]
    	InlinedClose = 0x40000,
    	[Token(Token = "0x4000121")]
    	AuthorId = 0x80000,
    	[Token(Token = "0x4000122")]
    	AuthorAppearance = 0x100000,
    	[Token(Token = "0x4000123")]
    	AuthorAssign = 0x200000,
    	[Token(Token = "0x4000124")]
    	AppearanceAssign = 0x400000,
    	[Token(Token = "0x4000125")]
    	TextId = 0x800000,
    	[Token(Token = "0x4000126")]
    	TextIdBody = 0x1000000,
    	[Token(Token = "0x4000127")]
    	TextIdOpen = 0x2000000,
    	[Token(Token = "0x4000128")]
    	TextIdClose = 0x4000000,
    	[Token(Token = "0x4000129")]
    	BoolFlag = 0x8000000,
    	[Token(Token = "0x400012A")]
    	Indent = 0x10000000
    }
}
