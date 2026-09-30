using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x2000545")]
    public class GameSettingsBgmVolumeSlider : ScriptableSlider
    {
    	[Token(Token = "0x4001448")]
    	[FieldOffset(Offset = "0x90")]
    	private IAudioManager audioManager;

    	[Token(Token = "0x6001A1D")]
    	[Address(RVA = "0x625A20", Offset = "0x624420", VA = "0x180625A20", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001A1E")]
    	[Address(RVA = "0x625AE0", Offset = "0x6244E0", VA = "0x180625AE0", Slot = "6")]
    	protected override void Start()
    	{
    	}

    	[Token(Token = "0x6001A1F")]
    	[Address(RVA = "0x625B80", Offset = "0x624580", VA = "0x180625B80", Slot = "62")]
    	protected override void OnValueChanged(float value)
    	{
    	}

    	[Token(Token = "0x6001A20")]
    	[Address(RVA = "0x540350", Offset = "0x53ED50", VA = "0x180540350")]
    	public GameSettingsBgmVolumeSlider()
    	{
    	}
    }
}
