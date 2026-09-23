using GigaCreation.Essentials.Dialogs.Views;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.Dialogs.Models
{
    [Token(Token = "0x2000015")]
    public class AddDialogData
    {
    	[Token(Token = "0x4000054")]
    	[FieldOffset(Offset = "0x10")]
    	public ProvidableDialog ProvidableDialog;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xC1B290 (shared 82 B body) -- ProvidableDialog (+0x10) = null
    	// Annotated disassembly: work/title-path/disas/ad_0xC1B290.txt
    	[Token(Token = "0x6000044")]
    	[Address(RVA = "0xC1B290", Offset = "0xC19C90", VA = "0x180C1B290")]
    	public void Clear()
    	{
    		ProvidableDialog = null;
    	}

    	[Token(Token = "0x6000045")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public AddDialogData()
    	{
    	}
    }
}
