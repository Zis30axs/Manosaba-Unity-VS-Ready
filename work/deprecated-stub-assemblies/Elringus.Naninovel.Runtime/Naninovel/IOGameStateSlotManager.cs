using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000474")]
    public class IOGameStateSlotManager : IOSaveSlotManager<GameStateMap>
    {
    	[Token(Token = "0x400109E")]
    	[FieldOffset(Offset = "0x60")]
    	private readonly string savePattern;

    	[Token(Token = "0x400109F")]
    	[FieldOffset(Offset = "0x68")]
    	private readonly string quickSavePattern;

    	[Token(Token = "0x40010A0")]
    	[FieldOffset(Offset = "0x70")]
    	private readonly string savesFolderPath;

    	[Token(Token = "0x40010A1")]
    	[FieldOffset(Offset = "0x78")]
    	private readonly StateConfiguration config;

    	[Token(Token = "0x1700030E")]
    	protected override string SaveDataPath
    	{
    		[Token(Token = "0x60015EF")]
    		[Address(RVA = "0x5DAC80", Offset = "0x5D9680", VA = "0x1805DAC80", Slot = "37")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700030F")]
    	protected sealed override string Extension
    	{
    		[Token(Token = "0x60015F0")]
    		[Address(RVA = "0x5DACE0", Offset = "0x5D96E0", VA = "0x1805DACE0", Slot = "32")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000310")]
    	protected override bool Binary
    	{
    		[Token(Token = "0x60015F1")]
    		[Address(RVA = "0x5DAD50", Offset = "0x5D9750", VA = "0x1805DAD50", Slot = "31")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x60015F2")]
    	[Address(RVA = "0x5DAD70", Offset = "0x5D9770", VA = "0x1805DAD70")]
    	public IOGameStateSlotManager(StateConfiguration config = default, string savesFolderPath = default)
    	{
    	}

    	[Token(Token = "0x60015F3")]
    	[Address(RVA = "0x5DB060", Offset = "0x5D9A60", VA = "0x1805DB060", Slot = "27")]
    	public override bool AnySaveExists()
    	{
    		return default(bool);
    	}
    }
}
