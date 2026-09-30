using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x200054A")]
    public class GameSettingsLanguageDropdown : ScriptableDropdown
    {
    	[Token(Token = "0x400145B")]
    	[FieldOffset(Offset = "0x90")]
    	private IReadOnlyList<string> availableLocales;

    	[Token(Token = "0x400145C")]
    	[FieldOffset(Offset = "0x98")]
    	private ILocalizationManager l10n;

    	[Token(Token = "0x400145D")]
    	[FieldOffset(Offset = "0xA0")]
    	private ICommunityLocalization communityL10n;

    	[Token(Token = "0x400145E")]
    	[FieldOffset(Offset = "0xA8")]
    	private ITextManager docs;

    	[Token(Token = "0x6001A3E")]
    	[Address(RVA = "0x6280F0", Offset = "0x626AF0", VA = "0x1806280F0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001A3F")]
    	[Address(RVA = "0x628430", Offset = "0x626E30", VA = "0x180628430", Slot = "63")]
    	protected virtual string[] GetAvailableLocales()
    	{
    		return null;
    	}

    	[Token(Token = "0x6001A40")]
    	[Address(RVA = "0x6285C0", Offset = "0x626FC0", VA = "0x1806285C0", Slot = "64")]
    	protected virtual void InitializeOptions()
    	{
    	}

    	[Token(Token = "0x6001A41")]
    	[Address(RVA = "0x628810", Offset = "0x627210", VA = "0x180628810", Slot = "65")]
    	protected virtual void UpdateSelectedLocale(string locale)
    	{
    	}

    	[Token(Token = "0x6001A42")]
    	[Address(RVA = "0x6288D0", Offset = "0x6272D0", VA = "0x1806288D0", Slot = "66")]
    	protected virtual string GetLabelForLocale(string locale)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001A43")]
    	[Address(RVA = "0x628A20", Offset = "0x627420", VA = "0x180628A20", Slot = "62")]
    	protected override void OnValueChanged(int value)
    	{
    	}

    	[Token(Token = "0x6001A44")]
    	[Address(RVA = "0x628AB0", Offset = "0x6274B0", VA = "0x180628AB0", Slot = "67")]
    	protected virtual void HandleLocaleChanged(LocaleChangedArgs _)
    	{
    	}

    	[Token(Token = "0x6001A45")]
    	[Address(RVA = "0x53E9E0", Offset = "0x53D3E0", VA = "0x18053E9E0")]
    	public GameSettingsLanguageDropdown()
    	{
    	}
    }
}
