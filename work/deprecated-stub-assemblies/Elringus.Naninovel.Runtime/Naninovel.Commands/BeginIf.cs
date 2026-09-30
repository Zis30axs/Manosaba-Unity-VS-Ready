using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Metadata;

namespace Naninovel.Commands
{
    [Token(Token = "0x20006BC")]
    [CommandAlias("if")]
    [Branch(BranchTraits.Nest | BranchTraits.Return | BranchTraits.Switch, null, null)]
    [IgnoreParameter("ConditionalExpression")]
    public class BeginIf : Command, Command.INestedHost
    {
    	[Token(Token = "0x40019C2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	[ConditionContext(-1, null)]
    	public StringParameter Expression;

    	[Token(Token = "0x170004C4")]
    	public override bool ShouldExecute
    	{
    		[Token(Token = "0x600207C")]
    		[Address(RVA = "0x42F6D0", Offset = "0x42E0D0", VA = "0x18042F6D0", Slot = "4")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x600207D")]
    	[Address(RVA = "0x4EDCB0", Offset = "0x4EC6B0", VA = "0x1804EDCB0", Slot = "17")]
    	public virtual int GetNextPlaybackIndex(ScriptPlaylist playlist, int playedIndex)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x600207E")]
    	[Address(RVA = "0x4EDD50", Offset = "0x4EC750", VA = "0x1804EDD50", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600207F")]
    	[Address(RVA = "0x4EDF20", Offset = "0x4EC920", VA = "0x1804EDF20")]
    	public static void HandleConditionalBlock(Command ifOrElse)
    	{
    	}

    	[Token(Token = "0x6002080")]
    	[Address(RVA = "0x4EE1A0", Offset = "0x4ECBA0", VA = "0x1804EE1A0")]
    	private static int GetNextIndexWithNested(IScriptPlayer player, Command ifOrElse)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6002081")]
    	[Address(RVA = "0x4EE450", Offset = "0x4ECE50", VA = "0x1804EE450")]
    	private static int GetNextIndexWithEndIf(IScriptPlayer player, bool gettingOut)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6002082")]
    	[Address(RVA = "0x4EE680", Offset = "0x4ED080", VA = "0x1804EE680")]
    	private static bool IsTruthyElse(Command cmd)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6002083")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public BeginIf()
    	{
    	}
    }
}
