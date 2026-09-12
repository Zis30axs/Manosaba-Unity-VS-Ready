using System.Text.RegularExpressions;
using Il2CppDummyDll;

namespace WitchTrials.Models;

[Token(Token = "0x200017C")]
public static class ScriptHelper
{
	[Token(Token = "0x4000719")]
	[FieldOffset(Offset = "0x0")]
	private static readonly Regex s_scenarioNamePattern;

	[Token(Token = "0x600067B")]
	[Address(RVA = "0x39B040", Offset = "0x399A40", VA = "0x18039B040")]
	public static bool TryParseScenarioName(string scenarioName, out ScenarioInfo info)
	{
		info = default;
		info = default;
		return default(bool);
	}
}
