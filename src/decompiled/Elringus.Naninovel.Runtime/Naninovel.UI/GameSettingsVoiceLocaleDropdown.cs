using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.UI;

[Token(Token = "0x2000557")]
public class GameSettingsVoiceLocaleDropdown : ScriptableDropdown
{
	[Token(Token = "0x400147E")]
	[FieldOffset(Offset = "0x90")]
	private readonly List<string> options;

	[Token(Token = "0x400147F")]
	[FieldOffset(Offset = "0x98")]
	private IAudioManager audios;

	[Token(Token = "0x4001480")]
	[FieldOffset(Offset = "0xA0")]
	private ITextManager docs;

	[Token(Token = "0x4001481")]
	[FieldOffset(Offset = "0xA8")]
	private ILocalizationManager l10n;

	[Token(Token = "0x6001A83")]
	[Address(RVA = "0x62C440", Offset = "0x62AE40", VA = "0x18062C440", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001A84")]
	[Address(RVA = "0x62C700", Offset = "0x62B100", VA = "0x18062C700", Slot = "62")]
	protected override void OnValueChanged(int value)
	{
	}

	[Token(Token = "0x6001A85")]
	[Address(RVA = "0x62C7A0", Offset = "0x62B1A0", VA = "0x18062C7A0", Slot = "63")]
	protected virtual string GetLabelForLocale(string locale)
	{
		return null;
	}

	[Token(Token = "0x6001A86")]
	[Address(RVA = "0x62C910", Offset = "0x62B310", VA = "0x18062C910", Slot = "64")]
	protected virtual void InitializeOptions()
	{
	}

	[Token(Token = "0x6001A87")]
	[Address(RVA = "0x628AB0", Offset = "0x6274B0", VA = "0x180628AB0", Slot = "65")]
	protected virtual void HandleLocaleChanged(LocaleChangedArgs _)
	{
	}

	[Token(Token = "0x6001A88")]
	[Address(RVA = "0x62CCD0", Offset = "0x62B6D0", VA = "0x18062CCD0")]
	public GameSettingsVoiceLocaleDropdown()
	{
	}
}
