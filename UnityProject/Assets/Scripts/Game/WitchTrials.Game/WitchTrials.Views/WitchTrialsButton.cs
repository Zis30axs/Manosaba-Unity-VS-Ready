#nullable enable annotations
using GigaCreation.Essentials.Input;
using System.Runtime.InteropServices;
using GigaCreation.Essentials.Localization;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views
{
    [Token(Token = "0x200000B")]
    public class WitchTrialsButton : RebuildableButton
    {
    	[Token(Token = "0x400000D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	[Space]
    	[SerializeField]
    	private Canvas _canvas;

    	[Token(Token = "0x400000E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    	[SerializeField]
    	private GraphicRaycaster _graphicRaycaster;

    	[Token(Token = "0x400000F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
    	[Space]
    	[SerializeField]
    	private Vector2 _defaultSize = new Vector2(505f, 120f);

    	[Token(Token = "0x17000001")]
    	protected override Vector2 DefaultSize
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x30CA20 (shared Vector2 field getter, +0x80)
    		[Token(Token = "0x6000017")]
    		[Address(RVA = "0x30CA20", Offset = "0x30B420", VA = "0x18030CA20", Slot = "4")]
    		get
    		{
    			return _defaultSize;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (local spelling: B) | RVA: 0x30CA40
    	// `Label` (0x44CE10 = +0x40) `as WitchTrialsButtonLabel` and `buttonData.ResponseKind` (+0x20) are both read before
    	// the real call to RebuildableButton.RebuildElements, so they are locals in the source. Afterwards ResponseKind == 1
    	// (Submit) and a live label -> ModifyEmphasizedText.
    	[Token(Token = "0x6000018")]
    	[Address(RVA = "0x30CA40", Offset = "0x30B440", VA = "0x18030CA40", Slot = "5")]
    	protected override void RebuildElements(ButtonData buttonData, bool atRuntime, bool controlsItsOwnSize, [Optional] LocaleKind? locale)
    	{
    		WitchTrialsButtonLabel label = Label as WitchTrialsButtonLabel;
    		ResponseKind responseKind = buttonData.ResponseKind;
    		base.RebuildElements(buttonData, atRuntime, controlsItsOwnSize, locale);
    		if (responseKind == ResponseKind.Submit && label != null)
    		{
    			label.ModifyEmphasizedText();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30CBE0
    	// RebuildableButton.GetReferences (real call); GetComponent<Canvas>() -> +0x70, GetComponent<GraphicRaycaster>() -> +0x78.
    	[Token(Token = "0x6000019")]
    	[Address(RVA = "0x30CBE0", Offset = "0x30B5E0", VA = "0x18030CBE0", Slot = "6")]
    	protected override void GetReferences()
    	{
    		base.GetReferences();
    		_canvas = GetComponent<Canvas>();
    		_graphicRaycaster = GetComponent<GraphicRaycaster>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30CD20
    	// `_canvas` (+0x70), `_graphicRaycaster` (+0x78) enabled = true.
    	[Token(Token = "0x600001A")]
    	[Address(RVA = "0x30CD20", Offset = "0x30B720", VA = "0x18030CD20")]
    	public void Show()
    	{
    		_canvas.enabled = true;
    		_graphicRaycaster.enabled = true;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30CD60
    	// `_canvas` (+0x70), `_graphicRaycaster` (+0x78) enabled = false.
    	[Token(Token = "0x600001B")]
    	[Address(RVA = "0x30CD60", Offset = "0x30B760", VA = "0x18030CD60")]
    	public void Hide()
    	{
    		_canvas.enabled = false;
    		_graphicRaycaster.enabled = false;
    	}

    	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x30CDA0, 87 B
    	// Only the field initializer `_defaultSize = new Vector2(505f, 120f)` (0x43FC8000 / 0x42F00000 at +0x80/+0x84); the
    	// rest is the empty base ctor chain inlined (UnityEngine.Object class-init check).
    	[Token(Token = "0x600001C")]
    	[Address(RVA = "0x30CDA0", Offset = "0x30B7A0", VA = "0x18030CDA0")]
    	public WitchTrialsButton()
    	{
    	}
    }
}
