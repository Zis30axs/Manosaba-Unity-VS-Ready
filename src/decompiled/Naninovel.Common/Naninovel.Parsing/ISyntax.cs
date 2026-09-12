using Il2CppDummyDll;

namespace Naninovel.Parsing;

[Token(Token = "0x200002B")]
public interface ISyntax
{
	[Token(Token = "0x1700001D")]
	string CommentLine
	{
		[Token(Token = "0x60000B2")]
		get;
	}

	[Token(Token = "0x1700001E")]
	string LabelLine
	{
		[Token(Token = "0x60000B3")]
		get;
	}

	[Token(Token = "0x1700001F")]
	string CommandLine
	{
		[Token(Token = "0x60000B4")]
		get;
	}

	[Token(Token = "0x17000020")]
	string AuthorAssign
	{
		[Token(Token = "0x60000B5")]
		get;
	}

	[Token(Token = "0x17000021")]
	string AuthorAppearance
	{
		[Token(Token = "0x60000B6")]
		get;
	}

	[Token(Token = "0x17000022")]
	string ParametrizeGeneric
	{
		[Token(Token = "0x60000B7")]
		get;
	}

	[Token(Token = "0x17000023")]
	string ExpressionOpen
	{
		[Token(Token = "0x60000B8")]
		get;
	}

	[Token(Token = "0x17000024")]
	string ExpressionClose
	{
		[Token(Token = "0x60000B9")]
		get;
	}

	[Token(Token = "0x17000025")]
	string InlinedOpen
	{
		[Token(Token = "0x60000BA")]
		get;
	}

	[Token(Token = "0x17000026")]
	string InlinedClose
	{
		[Token(Token = "0x60000BB")]
		get;
	}

	[Token(Token = "0x17000027")]
	string ParameterAssign
	{
		[Token(Token = "0x60000BC")]
		get;
	}

	[Token(Token = "0x17000028")]
	string ListDelimiter
	{
		[Token(Token = "0x60000BD")]
		get;
	}

	[Token(Token = "0x17000029")]
	string NamedDelimiter
	{
		[Token(Token = "0x60000BE")]
		get;
	}

	[Token(Token = "0x1700002A")]
	string TextIdOpen
	{
		[Token(Token = "0x60000BF")]
		get;
	}

	[Token(Token = "0x1700002B")]
	string TextIdClose
	{
		[Token(Token = "0x60000C0")]
		get;
	}

	[Token(Token = "0x1700002C")]
	string BooleanFlag
	{
		[Token(Token = "0x60000C1")]
		get;
	}

	[Token(Token = "0x1700002D")]
	string True
	{
		[Token(Token = "0x60000C2")]
		get;
	}

	[Token(Token = "0x1700002E")]
	string False
	{
		[Token(Token = "0x60000C3")]
		get;
	}
}
