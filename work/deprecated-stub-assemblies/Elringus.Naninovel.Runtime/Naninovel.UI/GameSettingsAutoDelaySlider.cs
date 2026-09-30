using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x2000544")]
    public class GameSettingsAutoDelaySlider : ScriptableSlider
    {
    	[Token(Token = "0x4001446")]
    	[FieldOffset(Offset = "0x90")]
    	[SerializeField]
    	private GameSettingsPreviewPrinter previewPrinter;

    	[Token(Token = "0x4001447")]
    	[FieldOffset(Offset = "0x98")]
    	private ITextPrinterManager printerManager;

    	[Token(Token = "0x170003B5")]
    	protected virtual bool PreviewPrinterAvailable
    	{
    		[Token(Token = "0x6001A18")]
    		[Address(RVA = "0x625810", Offset = "0x624210", VA = "0x180625810", Slot = "63")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170003B6")]
    	protected GameSettingsPreviewPrinter PreviewPrinter
    	{
    		[Token(Token = "0x6001A19")]
    		[Address(RVA = "0x468E20", Offset = "0x467820", VA = "0x180468E20")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6001A1A")]
    	[Address(RVA = "0x625850", Offset = "0x624250", VA = "0x180625850", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001A1B")]
    	[Address(RVA = "0x625980", Offset = "0x624380", VA = "0x180625980", Slot = "62")]
    	protected override void OnValueChanged(float value)
    	{
    	}

    	[Token(Token = "0x6001A1C")]
    	[Address(RVA = "0x540350", Offset = "0x53ED50", VA = "0x180540350")]
    	public GameSettingsAutoDelaySlider()
    	{
    	}
    }
}
