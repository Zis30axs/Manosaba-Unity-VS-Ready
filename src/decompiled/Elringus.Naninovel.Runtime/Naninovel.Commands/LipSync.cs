using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x20006AD")]
public class LipSync : Command
{
	[Token(Token = "0x20006AE")]
	public interface IReceiver
	{
		[Token(Token = "0x600205F")]
		void AllowLipSync(bool active);
	}

	[Token(Token = "0x4001994")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[RequiredParameter]
	[ActorContext("Characters", 0, null)]
	public NamedBooleanParameter CharIdAndAllow;

	[Token(Token = "0x600205D")]
	[Address(RVA = "0x4EA0C0", Offset = "0x4E8AC0", VA = "0x1804EA0C0", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600205E")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public LipSync()
	{
	}
}
