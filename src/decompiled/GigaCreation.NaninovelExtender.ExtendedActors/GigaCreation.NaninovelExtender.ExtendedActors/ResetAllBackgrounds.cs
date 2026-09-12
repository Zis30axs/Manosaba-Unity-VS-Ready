using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x2000048")]
[UsedImplicitly]
[CommandAlias("resetBacks")]
public class ResetAllBackgrounds : Command
{
	[Token(Token = "0x4000110")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	public BooleanParameter Wait;

	[Token(Token = "0x4000111")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private IBackgroundManager _naniBackgroundManager;

	[Token(Token = "0x1700001C")]
	private IBackgroundManager NaniBackgroundManager
	{
		[Token(Token = "0x60000F4")]
		[Address(RVA = "0xD26410", Offset = "0xD24E10", VA = "0x180D26410")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60000F5")]
	[Address(RVA = "0xD264D0", Offset = "0xD24ED0", VA = "0x180D264D0", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000F6")]
	[Address(RVA = "0xD26580", Offset = "0xD24F80", VA = "0x180D26580")]
	private UniTask Reset(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000F7")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public ResetAllBackgrounds()
	{
	}
}
