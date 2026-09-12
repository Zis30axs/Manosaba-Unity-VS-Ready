using System.Collections.Generic;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel.Metadata;

[Token(Token = "0x2000071")]
public interface IMetadata
{
	[Token(Token = "0x1700007D")]
	string? EntryScript
	{
		[Token(Token = "0x600025D")]
		get;
	}

	[Token(Token = "0x1700007E")]
	string? TitleScript
	{
		[Token(Token = "0x600025E")]
		get;
	}

	[Token(Token = "0x1700007F")]
	IReadOnlyCollection<Actor> Actors
	{
		[Token(Token = "0x600025F")]
		get;
	}

	[Token(Token = "0x17000080")]
	IReadOnlyCollection<Command> Commands
	{
		[Token(Token = "0x6000260")]
		get;
	}

	[Token(Token = "0x17000081")]
	IReadOnlyCollection<Constant> Constants
	{
		[Token(Token = "0x6000261")]
		get;
	}

	[Token(Token = "0x17000082")]
	IReadOnlyCollection<Resource> Resources
	{
		[Token(Token = "0x6000262")]
		get;
	}

	[Token(Token = "0x17000083")]
	IReadOnlyCollection<string> Variables
	{
		[Token(Token = "0x6000263")]
		get;
	}

	[Token(Token = "0x17000084")]
	IReadOnlyCollection<Function> Functions
	{
		[Token(Token = "0x6000264")]
		get;
	}

	[Token(Token = "0x17000085")]
	ISyntax Syntax
	{
		[Token(Token = "0x6000265")]
		get;
	}

	[Token(Token = "0x6000266")]
	Command FindCommand(string aliasOrId);

	[Token(Token = "0x6000267")]
	Parameter? FindParameter(string commandAliasOrId, string? paramAliasOrId);

	[Token(Token = "0x6000268")]
	bool FindFunctions(string name, ICollection<Function> result);
}
