using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x200068D")]
public abstract class AudioCommand : Command
{
	[Token(Token = "0x170004C2")]
	protected IAudioManager AudioManager
	{
		[Token(Token = "0x6002001")]
		[Address(RVA = "0x4DE6D0", Offset = "0x4DD0D0", VA = "0x1804DE6D0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6002002")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	protected AudioCommand()
	{
	}
}
