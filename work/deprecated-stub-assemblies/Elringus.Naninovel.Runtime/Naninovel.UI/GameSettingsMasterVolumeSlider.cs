using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x200054B")]
    public class GameSettingsMasterVolumeSlider : ScriptableSlider
    {
    	[Token(Token = "0x400145F")]
    	[FieldOffset(Offset = "0x90")]
    	private IAudioManager audioManager;

    	[Token(Token = "0x6001A46")]
    	[Address(RVA = "0x628AD0", Offset = "0x6274D0", VA = "0x180628AD0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001A47")]
    	[Address(RVA = "0x628B90", Offset = "0x627590", VA = "0x180628B90", Slot = "6")]
    	protected override void Start()
    	{
    	}

    	[Token(Token = "0x6001A48")]
    	[Address(RVA = "0x628C30", Offset = "0x627630", VA = "0x180628C30", Slot = "62")]
    	protected override void OnValueChanged(float value)
    	{
    	}

    	[Token(Token = "0x6001A49")]
    	[Address(RVA = "0x540350", Offset = "0x53ED50", VA = "0x180540350")]
    	public GameSettingsMasterVolumeSlider()
    	{
    	}
    }
}
