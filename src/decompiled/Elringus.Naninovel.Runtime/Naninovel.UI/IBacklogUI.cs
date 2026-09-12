using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x20004FF")]
public interface IBacklogUI : IManagedUI
{
	[Token(Token = "0x60018EF")]
	void AddMessage(LocalizableText text, [Optional] string authorId, [Optional] PlaybackSpot? rollbackSpot, [Optional] string voicePath);

	[Token(Token = "0x60018F0")]
	void AppendMessage(LocalizableText text, [Optional] string voicePath);

	[Token(Token = "0x60018F1")]
	void AddChoice(IReadOnlyList<BacklogChoice> choices);

	[Token(Token = "0x60018F2")]
	void Clear();
}
