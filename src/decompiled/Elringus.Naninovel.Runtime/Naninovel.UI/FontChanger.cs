using System.Collections.Generic;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x2000588")]
public class FontChanger : MonoBehaviour
{
	[Token(Token = "0x4001528")]
	[FieldOffset(Offset = "0x20")]
	[Tooltip("Setup which game objects should be affected by font and text size changes (set in game settings).")]
	[SerializeField]
	private CustomUI.FontChangeConfiguration[] configuration;

	[Token(Token = "0x4001529")]
	[FieldOffset(Offset = "0x28")]
	private IUIManager uiManager;

	[Token(Token = "0x400152A")]
	[FieldOffset(Offset = "0x30")]
	private string lastAppliedFontName;

	[Token(Token = "0x400152B")]
	[FieldOffset(Offset = "0x38")]
	private int lastAppliedFontSize;

	[Token(Token = "0x6001B60")]
	[Address(RVA = "0x63B3B0", Offset = "0x639DB0", VA = "0x18063B3B0")]
	public static void InitializeConfiguration(IReadOnlyList<CustomUI.FontChangeConfiguration> configuration)
	{
	}

	[Token(Token = "0x6001B61")]
	[Address(RVA = "0x63B750", Offset = "0x63A150", VA = "0x18063B750")]
	public static void ChangeFont(TMP_FontAsset font, IReadOnlyCollection<CustomUI.FontChangeConfiguration> configuration)
	{
	}

	[Token(Token = "0x6001B62")]
	[Address(RVA = "0x63BA90", Offset = "0x63A490", VA = "0x18063BA90")]
	public static void ChangeFontSize(int dropdownIndex, IReadOnlyCollection<CustomUI.FontChangeConfiguration> configuration)
	{
	}

	[Token(Token = "0x6001B63")]
	[Address(RVA = "0x63BD20", Offset = "0x63A720", VA = "0x18063BD20", Slot = "4")]
	public virtual void ApplySelectedFont()
	{
	}

	[Token(Token = "0x6001B64")]
	[Address(RVA = "0x63BE00", Offset = "0x63A800", VA = "0x18063BE00", Slot = "5")]
	protected virtual void Awake()
	{
	}

	[Token(Token = "0x6001B65")]
	[Address(RVA = "0x63BEC0", Offset = "0x63A8C0", VA = "0x18063BEC0", Slot = "6")]
	protected virtual void OnEnable()
	{
	}

	[Token(Token = "0x6001B66")]
	[Address(RVA = "0x63BFD0", Offset = "0x63A9D0", VA = "0x18063BFD0", Slot = "7")]
	protected virtual void OnDisable()
	{
	}

	[Token(Token = "0x6001B67")]
	[Address(RVA = "0x63C0D0", Offset = "0x63AAD0", VA = "0x18063C0D0", Slot = "8")]
	protected virtual void HandleFontNameChanged(string fontName)
	{
	}

	[Token(Token = "0x6001B68")]
	[Address(RVA = "0x63C280", Offset = "0x63AC80", VA = "0x18063C280", Slot = "9")]
	protected virtual void HandleFontSizeChanged(int sizeIndex)
	{
	}

	[Token(Token = "0x6001B69")]
	[Address(RVA = "0x63C2B0", Offset = "0x63ACB0", VA = "0x18063C2B0", Slot = "10")]
	protected virtual bool IsFontNameChangePending()
	{
		return default(bool);
	}

	[Token(Token = "0x6001B6A")]
	[Address(RVA = "0x63C440", Offset = "0x63AE40", VA = "0x18063C440", Slot = "11")]
	protected virtual bool IsFontSizeChangePending()
	{
		return default(bool);
	}

	[Token(Token = "0x6001B6B")]
	[Address(RVA = "0x63C4B0", Offset = "0x63AEB0", VA = "0x18063C4B0")]
	public FontChanger()
	{
	}
}
