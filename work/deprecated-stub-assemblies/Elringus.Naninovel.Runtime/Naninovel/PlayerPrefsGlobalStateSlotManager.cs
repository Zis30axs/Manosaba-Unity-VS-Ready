using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000479")]
    public class PlayerPrefsGlobalStateSlotManager : PlayerPrefsSaveSlotManager<GlobalStateMap>
    {
    	[Token(Token = "0x40010A9")]
    	[FieldOffset(Offset = "0x60")]
    	private readonly string savesFolderPath;

    	[Token(Token = "0x40010AA")]
    	[FieldOffset(Offset = "0x68")]
    	private readonly StateConfiguration config;

    	[Token(Token = "0x17000321")]
    	protected override string KeyPrefix
    	{
    		[Token(Token = "0x600162B")]
    		[Address(RVA = "0x5DB8B0", Offset = "0x5DA2B0", VA = "0x1805DB8B0", Slot = "35")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000322")]
    	protected override bool Binary
    	{
    		[Token(Token = "0x600162C")]
    		[Address(RVA = "0x5DB790", Offset = "0x5DA190", VA = "0x1805DB790", Slot = "31")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x600162D")]
    	[Address(RVA = "0x5DB930", Offset = "0x5DA330", VA = "0x1805DB930")]
    	public PlayerPrefsGlobalStateSlotManager(StateConfiguration config, string savesFolderPath)
    	{
    	}
    }
}
