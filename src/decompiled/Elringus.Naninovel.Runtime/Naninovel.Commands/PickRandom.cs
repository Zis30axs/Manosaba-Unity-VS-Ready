using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Metadata;

namespace Naninovel.Commands;

[Token(Token = "0x20006CC")]
[CommandAlias("random")]
[RequireNested]
[Branch(BranchTraits.Nest | BranchTraits.Return, null, null)]
public class PickRandom : Command, Command.INestedHost
{
	[Token(Token = "0x40019F3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	public DecimalListParameter Weight;

	[Token(Token = "0x60020BA")]
	[Address(RVA = "0x4F38D0", Offset = "0x4F22D0", VA = "0x1804F38D0", Slot = "17")]
	public virtual int GetNextPlaybackIndex(ScriptPlaylist playlist, int playedIndex)
	{
		return default(int);
	}

	[Token(Token = "0x60020BB")]
	[Address(RVA = "0x4F3990", Offset = "0x4F2390", VA = "0x1804F3990", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60020BC")]
	[Address(RVA = "0x4F39E0", Offset = "0x4F23E0", VA = "0x1804F39E0", Slot = "18")]
	protected virtual int PickRandomNested(ScriptPlaylist playlist, int hostIndex)
	{
		return default(int);
	}

	[Token(Token = "0x60020BD")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public PickRandom()
	{
	}
}
