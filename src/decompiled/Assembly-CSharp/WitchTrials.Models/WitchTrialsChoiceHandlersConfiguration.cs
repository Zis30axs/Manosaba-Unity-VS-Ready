using Il2CppDummyDll;
using Naninovel;

namespace WitchTrials.Models;

[Token(Token = "0x2000177")]
[EditInProjectSettings]
public class WitchTrialsChoiceHandlersConfiguration : Configuration
{
	[Token(Token = "0x4000704")]
	[FieldOffset(Offset = "0x18")]
	public bool DefaultCriticalChoiceHintEnabled;

	[Token(Token = "0x600064E")]
	[Address(RVA = "0x399370", Offset = "0x397D70", VA = "0x180399370")]
	public WitchTrialsChoiceHandlersConfiguration()
	{
	}
}
