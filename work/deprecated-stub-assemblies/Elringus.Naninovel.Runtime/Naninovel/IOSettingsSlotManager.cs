using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000476")]
    public class IOSettingsSlotManager : IOSaveSlotManager<SettingsStateMap>
    {
    	[Token(Token = "0x40010A5")]
    	[FieldOffset(Offset = "0x60")]
    	private readonly string defaultSlotId;

    	[Token(Token = "0x40010A6")]
    	[FieldOffset(Offset = "0x68")]
    	private readonly string savesFolderPath;

    	[Token(Token = "0x17000314")]
    	protected override string SaveDataPath
    	{
    		[Token(Token = "0x60015F9")]
    		[Address(RVA = "0x5DB450", Offset = "0x5D9E50", VA = "0x1805DB450", Slot = "37")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000315")]
    	protected override string Extension
    	{
    		[Token(Token = "0x60015FA")]
    		[Address(RVA = "0x5DB4B0", Offset = "0x5D9EB0", VA = "0x1805DB4B0", Slot = "32")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000316")]
    	protected override bool Binary
    	{
    		[Token(Token = "0x60015FB")]
    		[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360", Slot = "31")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x60015FC")]
    	[Address(RVA = "0x5DB520", Offset = "0x5D9F20", VA = "0x1805DB520")]
    	public IOSettingsSlotManager(StateConfiguration config = default, string savesFolderPath = default)
    	{
    	}

    	[Token(Token = "0x60015FD")]
    	[Address(RVA = "0x5DB630", Offset = "0x5DA030", VA = "0x1805DB630", Slot = "27")]
    	public override bool AnySaveExists()
    	{
    		return default(bool);
    	}
    }
}
