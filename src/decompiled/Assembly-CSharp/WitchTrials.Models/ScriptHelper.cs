using System.Text.RegularExpressions;
using FastEnumUtility;
using Il2CppDummyDll;

namespace WitchTrials.Models;

[Token(Token = "0x200017C")]
public static class ScriptHelper
{
	// RECONSTRUCTED | Confidence: A | RVA: 0x39B5F0 (.cctor), 215 B
	// `new Regex(literal 0x2E5AE50, (RegexOptions)8 = Compiled)` stored to
	// static +0x0. The pattern matches names like "Act1_Chapter2_Trial3":
	// act number, chapter number, part-kind word and section number.
	[Token(Token = "0x4000719")]
	[FieldOffset(Offset = "0x0")]
	private static readonly Regex s_scenarioNamePattern = new Regex("^[A-Z][a-z]*(\\d+)_[A-Z][a-z]*(\\d+)_([A-Z][a-z]*)(\\d+)$", RegexOptions.Compiled);

	// RECONSTRUCTED | Confidence: A (behavior), B (`&` vs `&&` spelling) | RVA: 0x39B040, 1446 B
	// - `Regex.Match(scenarioName)` is inlined: ArgumentNullException("input"),
	//   then `Regex.Run`.
	// - `isMatched` = `Match.Success` (+0x28 != 0) and `Groups.Count` == 5
	//   (the inlined GroupCollection count: group array length).
	// - Groups 1..4 are then read without short-circuiting, even when the match
	//   failed; failed groups yield "".
	//   - `Groups[n].Value` is the inlined `GetGroup(n)` plus
	//     `Substring(Index, Length)`.
	//   - 1, 2 and 4 go through `int.TryParse(string, out int)`, inlined as
	//     `Number.TryParseInt32(span, NumberStyles.Integer (7), CurrentInfo)`.
	//   - 3 goes through `FastEnum.TryParse<ScenarioPartKind>(string, out)`,
	//     which becomes `TryParseInternal(value, ignoreCase: false, ...)`.
	// - The five bools are AND-ed without branches. That is consistent both
	//   with `&` on locals and with `&&` folded by the optimizer; since
	//   every TryParse ran first, `&` is the faithful spelling.
	// - When all five hold, `new ScenarioInfo(act, chapter, part, section)`
	//   (primary ctor inlined, fields +0x10..+0x1C); otherwise `info = null`.
	//   Returns the combined bool.
	[Token(Token = "0x600067B")]
	[Address(RVA = "0x39B040", Offset = "0x399A40", VA = "0x18039B040")]
	public static bool TryParseScenarioName(string scenarioName, out ScenarioInfo info)
	{
		Match match = s_scenarioNamePattern.Match(scenarioName);
		bool isMatched = match.Success && match.Groups.Count == 5;
		bool actParsed = int.TryParse(match.Groups[1].Value, out int act);
		bool chapterParsed = int.TryParse(match.Groups[2].Value, out int chapter);
		bool partParsed = FastEnum.TryParse<ScenarioPartKind>(match.Groups[3].Value, out ScenarioPartKind part);
		bool sectionParsed = int.TryParse(match.Groups[4].Value, out int section);
		bool result = isMatched & actParsed & chapterParsed & partParsed & sectionParsed;
		info = result ? new ScenarioInfo(act, chapter, part, section) : null;
		return result;
	}
}
