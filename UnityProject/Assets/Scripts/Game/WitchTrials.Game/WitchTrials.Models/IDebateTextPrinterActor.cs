using Il2CppDummyDll;
using Naninovel;

namespace WitchTrials.Models
{
    [Token(Token = "0x2000173")]
    public interface IDebateTextPrinterActor : ITextPrinterActor, IActor
    {
    	[Token(Token = "0x17000107")]
    	DebateTextAnimationKind AnimationKind
    	{
    		[Token(Token = "0x600063E")]
    		get;
    		[Token(Token = "0x600063F")]
    		set;
    	}
    }
}
