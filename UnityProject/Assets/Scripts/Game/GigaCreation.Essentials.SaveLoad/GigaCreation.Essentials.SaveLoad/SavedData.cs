using Il2CppDummyDll;

namespace GigaCreation.Essentials.SaveLoad
{
    [Token(Token = "0x2000006")]
    public class SavedData
    {
    	[Token(Token = "0x4000006")]
    	[FieldOffset(Offset = "0x10")]
    	public VersionNumber VersionNumber;

    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0xCD5F80", Offset = "0xCD4980", VA = "0x180CD5F80", Slot = "4")]
    	public virtual void ResetToDefault()
    	{
    	}

    	[Token(Token = "0x6000007")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public SavedData()
    	{
    	}
    }
}
