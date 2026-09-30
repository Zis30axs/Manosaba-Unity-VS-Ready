using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x200047A")]
    public class PlayerPrefsSettingsSlotManager : PlayerPrefsSaveSlotManager<SettingsStateMap>
    {
    	[Token(Token = "0x40010AB")]
    	[FieldOffset(Offset = "0x60")]
    	private readonly string savesFolderPath;

    	[Token(Token = "0x17000323")]
    	protected override string KeyPrefix
    	{
    		[Token(Token = "0x600162E")]
    		[Address(RVA = "0x5DBA30", Offset = "0x5DA430", VA = "0x1805DBA30", Slot = "35")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000324")]
    	protected override bool Binary
    	{
    		[Token(Token = "0x600162F")]
    		[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360", Slot = "31")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x6001630")]
    	[Address(RVA = "0x5DBAB0", Offset = "0x5DA4B0", VA = "0x1805DBAB0")]
    	public PlayerPrefsSettingsSlotManager(StateConfiguration config, string savesFolderPath)
    	{
    	}
    }
}
