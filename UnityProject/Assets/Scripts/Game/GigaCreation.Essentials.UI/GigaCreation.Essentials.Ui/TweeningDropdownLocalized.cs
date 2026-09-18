using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000028")]
    public class TweeningDropdownLocalized : TweeningDropdown
    {
    	[Token(Token = "0x400008F")]
    	[FieldOffset(Offset = "0x1E0")]
    	[Header("References for Localized")]
    	[SerializeField]
    	private CanvasGroup _captionCanvasGroup;

    	[Token(Token = "0x4000090")]
    	[FieldOffset(Offset = "0x1E8")]
    	private int _currentItemIdx;

    	[Token(Token = "0x1700001B")]
    	public TMP_FontAsset[] ItemFontAssets
    	{
    		[Token(Token = "0x60000B2")]
    		[Address(RVA = "0xCE2F20", Offset = "0xCE1920", VA = "0x180CE2F20")]
    		[CompilerGenerated]
    		private get
    		{
    			return null;
    		}
    		[Token(Token = "0x60000B3")]
    		[Address(RVA = "0xCE2F30", Offset = "0xCE1930", VA = "0x180CE2F30")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x60000B4")]
    	[Address(RVA = "0xCE2F90", Offset = "0xCE1990", VA = "0x180CE2F90", Slot = "49")]
    	protected override DropdownItem CreateItem(DropdownItem itemTemplate)
    	{
    		return null;
    	}

    	[Token(Token = "0x60000B5")]
    	[Address(RVA = "0xCE3150", Offset = "0xCE1B50", VA = "0x180CE3150")]
    	public void ChangeCaptionVisibility(bool visible)
    	{
    	}

    	[Token(Token = "0x60000B6")]
    	[Address(RVA = "0xCE2CE0", Offset = "0xCE16E0", VA = "0x180CE2CE0")]
    	public TweeningDropdownLocalized()
    	{
    	}
    }
}
