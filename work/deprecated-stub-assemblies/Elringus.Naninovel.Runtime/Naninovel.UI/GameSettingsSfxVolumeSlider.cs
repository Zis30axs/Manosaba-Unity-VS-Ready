using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x2000555")]
    public class GameSettingsSfxVolumeSlider : ScriptableSlider
    {
    	[Token(Token = "0x400147A")]
    	[FieldOffset(Offset = "0x90")]
    	private IAudioManager audioManager;

    	[Token(Token = "0x6001A77")]
    	[Address(RVA = "0x62BAC0", Offset = "0x62A4C0", VA = "0x18062BAC0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001A78")]
    	[Address(RVA = "0x62BB80", Offset = "0x62A580", VA = "0x18062BB80", Slot = "6")]
    	protected override void Start()
    	{
    	}

    	[Token(Token = "0x6001A79")]
    	[Address(RVA = "0x62BC20", Offset = "0x62A620", VA = "0x18062BC20", Slot = "62")]
    	protected override void OnValueChanged(float value)
    	{
    	}

    	[Token(Token = "0x6001A7A")]
    	[Address(RVA = "0x540350", Offset = "0x53ED50", VA = "0x180540350")]
    	public GameSettingsSfxVolumeSlider()
    	{
    	}
    }
}
