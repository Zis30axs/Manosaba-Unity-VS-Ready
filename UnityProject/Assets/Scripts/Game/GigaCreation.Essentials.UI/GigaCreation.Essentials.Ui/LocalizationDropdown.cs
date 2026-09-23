using System.Collections.Generic;
using Il2CppDummyDll;
using TMPro;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000023")]
    public class LocalizationDropdown : OptionItemDropdown
    {
    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE0B70, 262 B -- Dropdown (+0x20, getter inlined).ClearOptions(); AddOptions(options);
    	//   when the dropdown is a TweeningDropdownLocalized (isinst) its ItemFontAssets = fontAssets
    	// Annotated disassembly: work/title-path/disas/ldd_0xCE0B70.txt
    	[Token(Token = "0x600008D")]
    	[Address(RVA = "0xCE0B70", Offset = "0xCDF570", VA = "0x180CE0B70")]
    	public void SetOptions(List<string> options, TMP_FontAsset[] fontAssets)
    	{
    		base.Dropdown.ClearOptions();
    		base.Dropdown.AddOptions(options);
    		if (base.Dropdown is TweeningDropdownLocalized tweeningDropdownLocalized)
    		{
    			tweeningDropdownLocalized.ItemFontAssets = fontAssets;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE0C80, 150 B -- when Dropdown is a TweeningDropdownLocalized (isinst):
    	//   its ChangeCaptionVisibility(visible) (inlined: _captionCanvasGroup.alpha)
    	// Annotated disassembly: work/title-path/disas/ldd_0xCE0C80.txt
    	[Token(Token = "0x600008E")]
    	[Address(RVA = "0xCE0C80", Offset = "0xCDF680", VA = "0x180CE0C80")]
    	public void ChangeCaptionVisibility(bool visible)
    	{
    		if (base.Dropdown is TweeningDropdownLocalized tweeningDropdownLocalized)
    		{
    			tweeningDropdownLocalized.ChangeCaptionVisibility(visible);
    		}
    	}

    	[Token(Token = "0x600008F")]
    	[Address(RVA = "0xCE0D20", Offset = "0xCDF720", VA = "0x180CE0D20")]
    	public LocalizationDropdown()
    	{
    	}
    }
}
