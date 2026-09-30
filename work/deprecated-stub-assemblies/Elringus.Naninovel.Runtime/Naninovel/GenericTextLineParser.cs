using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Commands;
using Naninovel.Parsing;

namespace Naninovel
{
    [Token(Token = "0x200040D")]
    public class GenericTextLineParser : ScriptLineParser<GenericTextScriptLine, GenericLine>
    {
    	[Token(Token = "0x4000EDD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private readonly MixedValueParser mixedParser;

    	[Token(Token = "0x4000EDE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private readonly IErrorHandler errorHandler;

    	[Token(Token = "0x4000EDF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private readonly Type printCommandType;

    	[Token(Token = "0x170002B3")]
    	protected virtual CommandParser CommandParser
    	{
    		[Token(Token = "0x600139F")]
    		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0", Slot = "10")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002B4")]
    	protected virtual GenericLine Model
    	{
    		[Token(Token = "0x60013A0")]
    		[Address(RVA = "0x347410", Offset = "0x345E10", VA = "0x180347410", Slot = "11")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60013A1")]
    		[Address(RVA = "0x30EBC0", Offset = "0x30D5C0", VA = "0x18030EBC0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170002B5")]
    	protected virtual IList<Command> InlinedCommands
    	{
    		[Token(Token = "0x60013A2")]
    		[Address(RVA = "0x44CE10", Offset = "0x44B810", VA = "0x18044CE10", Slot = "12")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002B6")]
    	protected virtual string AuthorId
    	{
    		[Token(Token = "0x60013A3")]
    		[Address(RVA = "0x5AF1B0", Offset = "0x5ADBB0", VA = "0x1805AF1B0", Slot = "13")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002B7")]
    	protected virtual string AuthorAppearance
    	{
    		[Token(Token = "0x60013A4")]
    		[Address(RVA = "0x5AF270", Offset = "0x5ADC70", VA = "0x1805AF270", Slot = "14")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002B8")]
    	protected virtual PlaybackSpot Spot
    	{
    		[Token(Token = "0x60013A5")]
    		[Address(RVA = "0x5AF330", Offset = "0x5ADD30", VA = "0x1805AF330", Slot = "15")]
    		get
    		{
    			return default(PlaybackSpot);
    		}
    	}

    	[Token(Token = "0x60013A6")]
    	[Address(RVA = "0x5AF450", Offset = "0x5ADE50", VA = "0x1805AF450")]
    	public GenericTextLineParser(ITextIdentifier identifier, [Optional] IErrorHandler errorHandler)
    	{
    	}

    	[Token(Token = "0x60013A7")]
    	[Address(RVA = "0x5AF7F0", Offset = "0x5AE1F0", VA = "0x1805AF7F0", Slot = "9")]
    	protected override GenericTextScriptLine Parse(GenericLine lineModel)
    	{
    		return null;
    	}

    	[Token(Token = "0x60013A8")]
    	[Address(RVA = "0x5AFA70", Offset = "0x5AE470", VA = "0x1805AFA70", Slot = "16")]
    	protected virtual void ResetState(GenericLine model)
    	{
    	}

    	[Token(Token = "0x60013A9")]
    	[Address(RVA = "0x5AFB30", Offset = "0x5AE530", VA = "0x1805AFB30", Slot = "17")]
    	protected virtual void AddAppearanceChange()
    	{
    	}

    	[Token(Token = "0x60013AA")]
    	[Address(RVA = "0x5AFE70", Offset = "0x5AE870", VA = "0x1805AFE70", Slot = "18")]
    	protected virtual void AddContent()
    	{
    	}

    	[Token(Token = "0x60013AB")]
    	[Address(RVA = "0x5B0170", Offset = "0x5AEB70", VA = "0x1805B0170", Slot = "19")]
    	protected virtual void AddCommand(Naninovel.Parsing.Command commandModel)
    	{
    	}

    	[Token(Token = "0x60013AC")]
    	[Address(RVA = "0x5B0480", Offset = "0x5AEE80", VA = "0x1805B0480", Slot = "20")]
    	protected virtual void AddCommand(Command command)
    	{
    	}

    	[Token(Token = "0x60013AD")]
    	[Address(RVA = "0x5B06F0", Offset = "0x5AF0F0", VA = "0x1805B06F0", Slot = "21")]
    	protected virtual void ParameterizeLastPrint(ParametrizeGeneric p)
    	{
    	}

    	[Token(Token = "0x60013AE")]
    	[Address(RVA = "0x5B0F80", Offset = "0x5AF980", VA = "0x1805B0F80", Slot = "22")]
    	protected virtual void AddGenericText(MixedValue genericText)
    	{
    	}

    	[Token(Token = "0x60013AF")]
    	[Address(RVA = "0x5B16C0", Offset = "0x5B00C0", VA = "0x1805B16C0", Slot = "23")]
    	protected virtual void AddLastWaitInput()
    	{
    	}
    }
}
