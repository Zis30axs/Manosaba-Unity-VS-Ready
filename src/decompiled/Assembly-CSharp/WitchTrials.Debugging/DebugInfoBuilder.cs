using Il2CppDummyDll;
using Naninovel;
using System.Linq;
using System;

namespace WitchTrials.Debugging;

[Token(Token = "0x20001AD")]
public static class DebugInfoBuilder
{
	// RECONSTRUCTED | Confidence: A | RVA: 0x3A95E0, 112 B
	// `text.Contains(Environment.NewLine)` (NRE on null) -> three-string Concat with '"' on both sides.
	[Token(Token = "0x600076A")]
	[Address(RVA = "0x3A95E0", Offset = "0x3A7FE0", VA = "0x1803A95E0")]
	public static string Quote(string text)
	{
		return text.Contains(Environment.NewLine) ? "\"" + text + "\"" : text;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3A9660, 791 B
	// 
	// - `Engine.TryGetService<IScriptPlayer>` is inlined (GetService + null test).
	// - IScriptPlayer slot 24 `PlaybackSpot`, with `Valid` inlined (`lineIndex >= 0 && inlineIndex >= 0`)
	//   and the result kept in bpl.
	// - The spot is fetched a second time for `ToString()` only when valid.
	[Token(Token = "0x600076B")]
	[Address(RVA = "0x3A9660", Offset = "0x3A8060", VA = "0x1803A9660")]
	public static bool TryGetPlaybackSpot(out string spot)
	{
		bool valid = Engine.TryGetService(out IScriptPlayer player) && player.PlaybackSpot.Valid;
		spot = valid ? player.PlaybackSpot.ToString() : null;
		return valid;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3A9980, 1150 B; predicate <>c.b__2_0 0x3AA1E0 (IActor slot 3 `Visible`)
	// 
	// - `TryGetService<ITextPrinterManager>` (inlined); IActorManager<ITextPrinterActor, ...> slot 0 `Actors`;
	//   `FirstOrDefault(actor => actor.Visible)`.
	// - `string.Join(Environment.NewLine, printer.Messages (slot 0).Select(BuildMessage))`, with the
	//   method-group delegate cached in `<>O`.
	// - A missing service or printer stores null and returns false.
	[Token(Token = "0x600076C")]
	[Address(RVA = "0x3A9980", Offset = "0x3A8380", VA = "0x1803A9980")]
	public static bool TryGetPrintedMessage(out string message)
	{
		if (!Engine.TryGetService(out ITextPrinterManager printerManager))
		{
			message = null;
			return false;
		}
		ITextPrinterActor printer = printerManager.Actors.FirstOrDefault((ITextPrinterActor actor) => actor.Visible);
		if (printer == null)
		{
			message = null;
			return false;
		}
		message = string.Join(Environment.NewLine, printer.Messages.Select(BuildMessage));
		return true;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3A9E00, 818 B
	// 
	// - `(string)printed.Text`, then `.Replace("<br>", Environment.NewLine)`.
	// - `Author.HasValue` and `TryGetService<ICharacterManager>` (inlined).
	// - The author name is the label when `!Label.IsEmpty` (inlined `parts != null && parts.Length != 0`);
	//   otherwise ICharacterManager slot 7 `GetAuthorName(Id)`.
	// - A non-blank name gives the four-string Concat `"＠" + name + NewLine + text`.
	[Token(Token = "0x600076D")]
	[Address(RVA = "0x3A9E00", Offset = "0x3A8800", VA = "0x1803A9E00")]
	private static string BuildMessage(PrintedMessage printed)
	{
		string text = ((string)printed.Text).Replace("<br>", Environment.NewLine);
		if (printed.Author.HasValue && Engine.TryGetService(out ICharacterManager characterManager))
		{
			MessageAuthor author = printed.Author.Value;
			string authorName = !author.Label.IsEmpty ? (string)author.Label : characterManager.GetAuthorName(author.Id);
			if (!string.IsNullOrWhiteSpace(authorName))
			{
				return "＠" + authorName + Environment.NewLine + text;
			}
		}
		return text;
	}
}
