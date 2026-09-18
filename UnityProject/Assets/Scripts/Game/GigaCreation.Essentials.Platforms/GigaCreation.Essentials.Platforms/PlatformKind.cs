using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Platforms
{
    [Token(Token = "0x2000005")]
    public enum PlatformKind
    {
    	[Token(Token = "0x4000009")]
    	[InspectorName("macOS")]
    	MacOs,
    	[Token(Token = "0x400000A")]
    	Windows,
    	[Token(Token = "0x400000B")]
    	[InspectorName("iOS")]
    	Ios,
    	[Token(Token = "0x400000C")]
    	Android,
    	[Token(Token = "0x400000D")]
    	NintendoSwitch,
    	[Token(Token = "0x400000E")]
    	[InspectorName("PlayStation 4")]
    	PlayStation4,
    	[Token(Token = "0x400000F")]
    	Unsupported
    }
}
