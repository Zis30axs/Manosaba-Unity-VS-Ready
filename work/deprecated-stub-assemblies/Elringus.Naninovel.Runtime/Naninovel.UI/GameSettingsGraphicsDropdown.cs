using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x2000549")]
    public class GameSettingsGraphicsDropdown : ScriptableDropdown
    {
    	[Token(Token = "0x4001454")]
    	[FieldOffset(Offset = "0x0")]
    	[ManagedText("DefaultUI")]
    	protected static string GraphicOption1;

    	[Token(Token = "0x4001455")]
    	[FieldOffset(Offset = "0x8")]
    	[ManagedText("DefaultUI")]
    	protected static string GraphicOption2;

    	[Token(Token = "0x4001456")]
    	[FieldOffset(Offset = "0x10")]
    	[ManagedText("DefaultUI")]
    	protected static string GraphicOption3;

    	[Token(Token = "0x4001457")]
    	[FieldOffset(Offset = "0x18")]
    	[ManagedText("DefaultUI")]
    	protected static string GraphicOption4;

    	[Token(Token = "0x4001458")]
    	[FieldOffset(Offset = "0x20")]
    	[ManagedText("DefaultUI")]
    	protected static string GraphicOption5;

    	[Token(Token = "0x4001459")]
    	[FieldOffset(Offset = "0x28")]
    	[ManagedText("DefaultUI")]
    	protected static string GraphicOption6;

    	[Token(Token = "0x400145A")]
    	[FieldOffset(Offset = "0x90")]
    	private ICameraManager cameraManager;

    	[Token(Token = "0x6001A36")]
    	[Address(RVA = "0x6274D0", Offset = "0x625ED0", VA = "0x1806274D0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001A37")]
    	[Address(RVA = "0x627590", Offset = "0x625F90", VA = "0x180627590", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x6001A38")]
    	[Address(RVA = "0x627750", Offset = "0x626150", VA = "0x180627750", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x6001A39")]
    	[Address(RVA = "0x627910", Offset = "0x626310", VA = "0x180627910", Slot = "62")]
    	protected override void OnValueChanged(int value)
    	{
    	}

    	[Token(Token = "0x6001A3A")]
    	[Address(RVA = "0x627970", Offset = "0x626370", VA = "0x180627970")]
    	private void InitializeOptions()
    	{
    	}

    	[Token(Token = "0x6001A3B")]
    	[Address(RVA = "0x627DB0", Offset = "0x6267B0", VA = "0x180627DB0")]
    	private void HandleLocaleChanged(LocaleChangedArgs _)
    	{
    	}

    	[Token(Token = "0x6001A3C")]
    	[Address(RVA = "0x53E9E0", Offset = "0x53D3E0", VA = "0x18053E9E0")]
    	public GameSettingsGraphicsDropdown()
    	{
    	}
    }
}
