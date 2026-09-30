using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Parsing;

namespace Naninovel
{
    [Token(Token = "0x2000407")]
    public class CommandLineParser : ScriptLineParser<CommandScriptLine, CommandLine>
    {
    	[Token(Token = "0x170002AC")]
    	protected virtual CommandParser CommandParser
    	{
    		[Token(Token = "0x600137A")]
    		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0", Slot = "10")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x600137B")]
    	[Address(RVA = "0x5AC1F0", Offset = "0x5AABF0", VA = "0x1805AC1F0")]
    	public CommandLineParser(ITextIdentifier identifier, [Optional] IErrorHandler errorHandler)
    	{
    	}

    	[Token(Token = "0x600137C")]
    	[Address(RVA = "0x5AC2D0", Offset = "0x5AACD0", VA = "0x1805AC2D0", Slot = "9")]
    	protected override CommandScriptLine Parse(CommandLine lineModel)
    	{
    		return null;
    	}
    }
}
