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
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCE2F20 (get) / 0xCE2F30 (set): auto-property accessors over
    		//   <ItemFontAssets>k__BackingField (+0x1F0)
    		[Token(Token = "0x60000B2")]
    		[Address(RVA = "0xCE2F20", Offset = "0xCE1920", VA = "0x180CE2F20")]
    		[CompilerGenerated]
    		private get;
    		[Token(Token = "0x60000B3")]
    		[Address(RVA = "0xCE2F30", Offset = "0xCE1930", VA = "0x180CE2F30")]
    		[CompilerGenerated]
    		set;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE2F90, 441 B -- TweeningDropdown.CreateItem inlined (Instantiate + the
    	//   toggle click sfx subscription); the item's text (+0x20).font = ItemFontAssets[_currentItemIdx (+0x1E8)]; the index
    	//   advances and wraps to 0 at ItemFontAssets.Length (0x180CE311A)
    	// Annotated disassembly: work/title-path/disas/tddl_0xCE2F90.txt
    	[Token(Token = "0x60000B4")]
    	[Address(RVA = "0xCE2F90", Offset = "0xCE1990", VA = "0x180CE2F90", Slot = "49")]
    	protected override DropdownItem CreateItem(DropdownItem itemTemplate)
    	{
    		DropdownItem dropdownItem = base.CreateItem(itemTemplate);
    		dropdownItem.text.font = ItemFontAssets[_currentItemIdx];
    		_currentItemIdx++;
    		if (_currentItemIdx >= ItemFontAssets.Length)
    		{
    			_currentItemIdx = 0;
    		}
    		return dropdownItem;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE3150, 43 B -- tail jump _captionCanvasGroup (+0x1E0).alpha = visible ? 1 : 0
    	//   (movzx + cvtdq2ps)
    	// Annotated disassembly: work/title-path/disas/tddl_0xCE3150.txt
    	[Token(Token = "0x60000B5")]
    	[Address(RVA = "0xCE3150", Offset = "0xCE1B50", VA = "0x180CE3150")]
    	public void ChangeCaptionVisibility(bool visible)
    	{
    		_captionCanvasGroup.alpha = (visible ? 1 : 0);
    	}

    	[Token(Token = "0x60000B6")]
    	[Address(RVA = "0xCE2CE0", Offset = "0xCE16E0", VA = "0x180CE2CE0")]
    	public TweeningDropdownLocalized()
    	{
    	}
    }
}
