using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x20006B9")]
public class OpenURL : Command
{
	[Token(Token = "0x40019B9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[RequiredParameter]
	public StringParameter URL;

	[Token(Token = "0x40019BA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterDefaultValue("_self")]
	public StringParameter Target;

	[Token(Token = "0x6002075")]
	[Address(RVA = "0x4ED270", Offset = "0x4EBC70", VA = "0x1804ED270", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002076")]
	[Address(RVA = "0x4ED3F0", Offset = "0x4EBDF0", VA = "0x1804ED3F0")]
	public OpenURL()
	{
	}
}
