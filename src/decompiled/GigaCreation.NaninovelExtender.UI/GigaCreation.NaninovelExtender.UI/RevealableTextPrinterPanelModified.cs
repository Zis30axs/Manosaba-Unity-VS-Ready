using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using TMPro;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Ui;

[Token(Token = "0x2000027")]
public class RevealableTextPrinterPanelModified : RevealableTextPrinterPanel
{
	[Token(Token = "0x40000A1")]
	[FieldOffset(Offset = "0x1F0")]
	[Header("References for Modified Printer")]
	[SerializeField]
	private CanvasGroup _skipModeIndicatorCanvasGroup;

	[Token(Token = "0x40000A2")]
	[FieldOffset(Offset = "0x1F8")]
	[SerializeField]
	private FontMaterialChanger _fontMaterialChanger;

	[Token(Token = "0x40000A3")]
	[FieldOffset(Offset = "0x200")]
	private IScriptPlayer _naniScriptPlayer;

	[Token(Token = "0x40000A4")]
	[FieldOffset(Offset = "0x208")]
	private string _activeAppearance;

	[Token(Token = "0x17000018")]
	private IScriptPlayer NaniScriptPlayer
	{
		[Token(Token = "0x60000A5")]
		[Address(RVA = "0xD40520", Offset = "0xD3EF20", VA = "0x180D40520")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60000A6")]
	[Address(RVA = "0xD405F0", Offset = "0xD3EFF0", VA = "0x180D405F0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x60000A7")]
	[Address(RVA = "0xD40690", Offset = "0xD3F090", VA = "0x180D40690", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x60000A8")]
	[Address(RVA = "0xD40840", Offset = "0xD3F240", VA = "0x180D40840", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x60000A9")]
	[Address(RVA = "0xD40A10", Offset = "0xD3F410", VA = "0x180D40A10", Slot = "112")]
	public override void SetWaitForInputIndicatorVisible(bool visible)
	{
	}

	[Token(Token = "0x60000AA")]
	[Address(RVA = "0xD40B30", Offset = "0xD3F530", VA = "0x180D40B30", Slot = "84")]
	public override void SetFont(TMP_FontAsset font)
	{
	}

	[Token(Token = "0x60000AB")]
	[Address(RVA = "0xD40EF0", Offset = "0xD3F8F0", VA = "0x180D40EF0", Slot = "130")]
	protected override string GetActiveAppearance()
	{
		return null;
	}

	[Token(Token = "0x60000AC")]
	[Address(RVA = "0xD40F90", Offset = "0xD3F990", VA = "0x180D40F90", Slot = "131")]
	protected override void SetActiveAppearance(string appearance)
	{
	}

	[Token(Token = "0x60000AD")]
	[Address(RVA = "0xD41330", Offset = "0xD3FD30", VA = "0x180D41330")]
	private void HandleAutoModeChanged(bool enable)
	{
	}

	[Token(Token = "0x60000AE")]
	[Address(RVA = "0xD413E0", Offset = "0xD3FDE0", VA = "0x180D413E0")]
	private void HandleSkipModeChanged(bool enable)
	{
	}

	[Token(Token = "0x60000AF")]
	[Address(RVA = "0x33FA80", Offset = "0x33E480", VA = "0x18033FA80")]
	public RevealableTextPrinterPanelModified()
	{
	}
}
