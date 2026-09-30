using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x200054F")]
    public class GameSettingsMessageSpeedSlider : ScriptableSlider
    {
    	[Token(Token = "0x400146C")]
    	[FieldOffset(Offset = "0x90")]
    	[SerializeField]
    	private GameSettingsPreviewPrinter previewPrinter;

    	[Token(Token = "0x400146D")]
    	[FieldOffset(Offset = "0x98")]
    	private ITextPrinterManager printerManager;

    	[Token(Token = "0x170003BA")]
    	protected virtual bool PreviewPrinterAvailable
    	{
    		[Token(Token = "0x6001A58")]
    		[Address(RVA = "0x625810", Offset = "0x624210", VA = "0x180625810", Slot = "63")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170003BB")]
    	protected GameSettingsPreviewPrinter PreviewPrinter
    	{
    		[Token(Token = "0x6001A59")]
    		[Address(RVA = "0x468E20", Offset = "0x467820", VA = "0x180468E20")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6001A5A")]
    	[Address(RVA = "0x629FC0", Offset = "0x6289C0", VA = "0x180629FC0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001A5B")]
    	[Address(RVA = "0x62A0F0", Offset = "0x628AF0", VA = "0x18062A0F0", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x6001A5C")]
    	[Address(RVA = "0x62A180", Offset = "0x628B80", VA = "0x18062A180", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x6001A5D")]
    	[Address(RVA = "0x62A210", Offset = "0x628C10", VA = "0x18062A210", Slot = "62")]
    	protected override void OnValueChanged(float value)
    	{
    	}

    	[Token(Token = "0x6001A5E")]
    	[Address(RVA = "0x540350", Offset = "0x53ED50", VA = "0x180540350")]
    	public GameSettingsMessageSpeedSlider()
    	{
    	}
    }
}
