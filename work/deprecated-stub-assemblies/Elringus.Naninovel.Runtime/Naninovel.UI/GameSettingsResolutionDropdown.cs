using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x2000550")]
    public class GameSettingsResolutionDropdown : ScriptableDropdown
    {
    	[Token(Token = "0x400146E")]
    	[FieldOffset(Offset = "0x90")]
    	private bool allowApplySettings;

    	[Token(Token = "0x6001A5F")]
    	[Address(RVA = "0x62A2B0", Offset = "0x628CB0", VA = "0x18062A2B0", Slot = "6")]
    	protected override void Start()
    	{
    	}

    	[Token(Token = "0x6001A60")]
    	[Address(RVA = "0x62A580", Offset = "0x628F80", VA = "0x18062A580", Slot = "62")]
    	protected override void OnValueChanged(int value)
    	{
    	}

    	[Token(Token = "0x6001A61")]
    	[Address(RVA = "0x62A640", Offset = "0x629040", VA = "0x18062A640")]
    	private void InitializeOptions(List<string> availableOptions)
    	{
    	}

    	[Token(Token = "0x6001A62")]
    	[Address(RVA = "0x62A720", Offset = "0x629120", VA = "0x18062A720")]
    	private int GetCurrentResolutionIndex()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6001A63")]
    	[Address(RVA = "0x53E9E0", Offset = "0x53D3E0", VA = "0x18053E9E0")]
    	public GameSettingsResolutionDropdown()
    	{
    	}
    }
}
