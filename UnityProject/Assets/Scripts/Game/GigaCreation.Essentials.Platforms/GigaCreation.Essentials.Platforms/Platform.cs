using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Platforms
{
    [Token(Token = "0x2000006")]
    public static class Platform
    {
    	[Token(Token = "0x17000003")]
    	public static PlatformKind CurrentPlatform
    	{
    		[Token(Token = "0x6000006")]
    		[Address(RVA = "0xCD5B70", Offset = "0xCD4570", VA = "0x180CD5B70")]
    		get
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0xCD5B70
    			//   switch on UnityEngine.Device.Application.platform (0x180CD5BA8):
    			//   > 11: 25 (PS4) -> 5, 32 (Switch) -> 4; <= 8: jump table at 0xCD5C44 =
    			//   {0,1 -> MacOs; 2,7 -> Windows; 8 -> Ios; 3..6 -> default}; 11 (Android) -> 3;
    			//   default: Debug.LogError("Unsupported platform.") and Unsupported (6).
    			// Annotated disassembly: work/title-path/disas/plat_01_CurrentPlatform.txt
    			switch (UnityEngine.Device.Application.platform)
    			{
    				case RuntimePlatform.OSXEditor:
    				case RuntimePlatform.OSXPlayer:
    					return PlatformKind.MacOs;
    				case RuntimePlatform.WindowsPlayer:
    				case RuntimePlatform.WindowsEditor:
    					return PlatformKind.Windows;
    				case RuntimePlatform.IPhonePlayer:
    					return PlatformKind.Ios;
    				case RuntimePlatform.Android:
    					return PlatformKind.Android;
    				case RuntimePlatform.Switch:
    					return PlatformKind.NintendoSwitch;
    				case RuntimePlatform.PS4:
    					return PlatformKind.PlayStation4;
    				default:
    					Debug.LogError("Unsupported platform.");
    					return PlatformKind.Unsupported;
    			}
    		}
    	}

    	[Token(Token = "0x17000004")]
    	public static bool IsPc
    	{
    		[Token(Token = "0x6000007")]
    		[Address(RVA = "0xCD5C70", Offset = "0xCD4670", VA = "0x180CD5C70")]
    		get
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0xCD5C70 -- CurrentPlatform == MacOs (0) || == Windows (1)
    			PlatformKind platform = CurrentPlatform;
    			return platform == PlatformKind.MacOs || platform == PlatformKind.Windows;
    		}
    	}

    	[Token(Token = "0x17000005")]
    	public static bool IsMobile
    	{
    		[Token(Token = "0x6000008")]
    		[Address(RVA = "0xCD5CA0", Offset = "0xCD46A0", VA = "0x180CD5CA0")]
    		get
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0xCD5CA0 -- CurrentPlatform == Ios (2) || == Android (3)
    			PlatformKind platform = CurrentPlatform;
    			return platform == PlatformKind.Ios || platform == PlatformKind.Android;
    		}
    	}

    	[Token(Token = "0x17000006")]
    	public static bool IsConsole
    	{
    		[Token(Token = "0x6000009")]
    		[Address(RVA = "0xCD5CD0", Offset = "0xCD46D0", VA = "0x180CD5CD0")]
    		get
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0xCD5CD0 -- CurrentPlatform == NintendoSwitch (4) || == PlayStation4 (5)
    			PlatformKind platform = CurrentPlatform;
    			return platform == PlatformKind.NintendoSwitch || platform == PlatformKind.PlayStation4;
    		}
    	}
    }
}
