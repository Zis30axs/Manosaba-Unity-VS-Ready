using Il2CppDummyDll;

namespace GigaCreation.Tools.Debugging.TextDisplays
{
    [Token(Token = "0x200000A")]
    public class DebugTextPreferencePlatform : DebugTextPreferenceBase
    {
    	[Token(Token = "0x17000004")]
    	protected override string LabelText
    	{
    		[Token(Token = "0x6000020")]
    		[Address(RVA = "0xD46950", Offset = "0xD45350", VA = "0x180D46950", Slot = "4")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000021")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DebugTextPreferencePlatform()
    	{
    	}
    }
}
