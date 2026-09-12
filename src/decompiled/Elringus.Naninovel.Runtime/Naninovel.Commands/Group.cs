using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Metadata;

namespace Naninovel.Commands;

[Token(Token = "0x20006CB")]
[RequireNested]
[Branch(BranchTraits.Nest | BranchTraits.Return, null, null)]
public class Group : Command, Command.INestedHost
{
	[Token(Token = "0x60020B7")]
	[Address(RVA = "0x4F37A0", Offset = "0x4F21A0", VA = "0x1804F37A0", Slot = "17")]
	public virtual int GetNextPlaybackIndex(ScriptPlaylist playlist, int playedIndex)
	{
		return default(int);
	}

	[Token(Token = "0x60020B8")]
	[Address(RVA = "0x4F3880", Offset = "0x4F2280", VA = "0x1804F3880", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60020B9")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public Group()
	{
	}
}
