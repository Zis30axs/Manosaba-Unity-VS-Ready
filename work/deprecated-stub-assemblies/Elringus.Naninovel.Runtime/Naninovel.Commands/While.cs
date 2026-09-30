using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Metadata;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006D9")]
    [RequireNested]
    [Branch(BranchTraits.Nest | BranchTraits.Return, null, null)]
    [IgnoreParameter("ConditionalExpression")]
    public class While : Command, Command.INestedHost
    {
    	[Token(Token = "0x4001A1E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	[ConditionContext(-1, null)]
    	public StringParameter Expression;

    	[Token(Token = "0x170004CD")]
    	public override bool ShouldExecute
    	{
    		[Token(Token = "0x60020DC")]
    		[Address(RVA = "0x42F6D0", Offset = "0x42E0D0", VA = "0x18042F6D0", Slot = "4")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x60020DD")]
    	[Address(RVA = "0x4F6FD0", Offset = "0x4F59D0", VA = "0x1804F6FD0", Slot = "17")]
    	public virtual int GetNextPlaybackIndex(ScriptPlaylist playlist, int playedIndex)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x60020DE")]
    	[Address(RVA = "0x4F71E0", Offset = "0x4F5BE0", VA = "0x1804F71E0", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60020DF")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public While()
    	{
    	}
    }
}
