using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Metadata;

namespace Naninovel.Commands;

[Token(Token = "0x20006C2")]
[Branch(BranchTraits.Nest | BranchTraits.Return, "BeginIf", null)]
public class Else : Command, Command.INestedHost
{
	[Token(Token = "0x170004C5")]
	public override bool ShouldExecute
	{
		[Token(Token = "0x6002092")]
		[Address(RVA = "0x42F6D0", Offset = "0x42E0D0", VA = "0x18042F6D0", Slot = "4")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6002093")]
	[Address(RVA = "0x4EDCB0", Offset = "0x4EC6B0", VA = "0x1804EDCB0", Slot = "17")]
	public virtual int GetNextPlaybackIndex(ScriptPlaylist playlist, int playedIndex)
	{
		return default(int);
	}

	[Token(Token = "0x6002094")]
	[Address(RVA = "0x4EFB10", Offset = "0x4EE510", VA = "0x1804EFB10", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002095")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public Else()
	{
	}
}
