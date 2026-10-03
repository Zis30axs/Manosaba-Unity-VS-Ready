using Il2CppDummyDll;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000034")]
    public enum PaginationIndicatorStyle
    {
    	[Token(Token = "0x40000BD")]
    	None,
    	[Token(Token = "0x40000BE")]
    	Dot,
    	[Token(Token = "0x40000BF")]
    	Number,
    	// RECONSTRUCTED | Confidence: A (name and value) | the [Token] attribute is deliberately omitted:
    	// src/il2cpp/dump.cs lists the constant as `public const PaginationIndicatorStyle Tab = 3;` and the
    	// shipped inline indicator data uses _style values 1/2/3 (res/.../SaveLoadUI~18.asset.json), but the
    	// stub stopped at Number and the shipped dummy-DLL token for this member was not recovered.
    	Tab
    }
}
