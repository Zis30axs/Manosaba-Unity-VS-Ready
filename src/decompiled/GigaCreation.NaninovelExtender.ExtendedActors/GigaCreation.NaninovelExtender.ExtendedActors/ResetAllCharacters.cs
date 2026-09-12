using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x200004A")]
[UsedImplicitly]
[CommandAlias("resetChars")]
public class ResetAllCharacters : Command
{
	[Token(Token = "0x4000114")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	public BooleanParameter Wait;

	[Token(Token = "0x4000115")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private ICharacterManager _naniCharacterManager;

	[Token(Token = "0x1700001D")]
	private ICharacterManager NaniCharacterManager
	{
		[Token(Token = "0x60000FB")]
		[Address(RVA = "0xD26E00", Offset = "0xD25800", VA = "0x180D26E00")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60000FC")]
	[Address(RVA = "0xD26EC0", Offset = "0xD258C0", VA = "0x180D26EC0", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000FD")]
	[Address(RVA = "0xD26F70", Offset = "0xD25970", VA = "0x180D26F70")]
	private UniTask Reset(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000FE")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public ResetAllCharacters()
	{
	}
}
