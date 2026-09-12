using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x20006A5")]
[CommandAlias("clearChoice")]
public class ClearChoiceHandler : Command
{
	[Token(Token = "0x4001979")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[ActorContext("ChoiceHandlers", -1, null)]
	public StringParameter HandlerId;

	[Token(Token = "0x400197A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterDefaultValue("true")]
	public BooleanParameter Hide;

	[Token(Token = "0x6002048")]
	[Address(RVA = "0x4E74C0", Offset = "0x4E5EC0", VA = "0x1804E74C0", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002049")]
	[Address(RVA = "0x4E7A30", Offset = "0x4E6430", VA = "0x1804E7A30")]
	private static void RemoveAllChoices(IChoiceHandlerActor handler)
	{
	}

	[Token(Token = "0x600204A")]
	[Address(RVA = "0x325690", Offset = "0x324090", VA = "0x180325690")]
	public ClearChoiceHandler()
	{
	}
}
