using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000052")]
public interface IChoiceHandlerActor : IActor
{
	[Token(Token = "0x1700002D")]
	IReadOnlyList<ChoiceState> Choices
	{
		[Token(Token = "0x600013A")]
		get;
	}

	[Token(Token = "0x600013B")]
	void AddChoice(ChoiceState choice);

	[Token(Token = "0x600013C")]
	void RemoveChoice(string id);

	[Token(Token = "0x600013D")]
	void HandleChoice(string id);
}
