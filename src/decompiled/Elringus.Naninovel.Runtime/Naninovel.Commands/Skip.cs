using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x20006D0")]
public class Skip : Command
{
	[Token(Token = "0x40019FD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[ParameterDefaultValue("true")]
	public BooleanParameter Enable;

	[Token(Token = "0x60020C7")]
	[Address(RVA = "0x4F4B50", Offset = "0x4F3550", VA = "0x1804F4B50", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60020C8")]
	[Address(RVA = "0x31D800", Offset = "0x31C200", VA = "0x18031D800")]
	public Skip()
	{
	}
}
