#nullable enable annotations
﻿using GigaCreation.Tools.UndoExtensions;
using GigaCreation.Tools.Service;
using System.Runtime.InteropServices;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x200002E")]
    public class RebuildableLabel : MonoBehaviour
    {
    	[Token(Token = "0x40000A8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private TextMeshProUGUI _uiLabel;

    	[Token(Token = "0x40000A9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[Header("Parameters")]
    	[SerializeField]
    	private LabelData _labelData;

    	[Token(Token = "0x40000AA")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[Space]
    	[SerializeField]
    	private float _defaultFontSize;

    	[Token(Token = "0x40000AB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
    	[SerializeField]
    	private Color _defaultLabelColor;

    	[Token(Token = "0x40000AC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
    	[SerializeField]
    	private float _defaultCharacterSpacing;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0x311810 (shared with WitchTrialsButtonLabel): mov [rcx+0x48], 1
    	[Token(Token = "0x40000AD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[SerializeField]
    	private HorizontalAlignmentOptions _defaultHorizontalAlignment = HorizontalAlignmentOptions.Left;

    	[Token(Token = "0x40000AE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[Space]
    	[SerializeField]
    	private string _labelId;

    	[Token(Token = "0x40000AF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private ILocalizationProvider _localizationProvider;

    	[Token(Token = "0x17000027")]
    	public float PreferredWidth
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCE42D0, 42 B -- _uiLabel (+0x20) virtual slot 84 (vtable +0x678),
    		//   TMP_Text.preferredWidth
    		// Annotated disassembly: work/title-path/disas/rl_0xCE42D0.txt
    		[Token(Token = "0x60000CE")]
    		[Address(RVA = "0xCE42D0", Offset = "0xCE2CD0", VA = "0x180CE42D0")]
    		get
    		{
    			return _uiLabel.preferredWidth;
    		}
    	}

    	[Token(Token = "0x17000028")]
    	public float PreferredHeight
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCE4300, 42 B -- _uiLabel (+0x20) virtual slot 85 (vtable +0x688),
    		//   TMP_Text.preferredHeight
    		// Annotated disassembly: work/title-path/disas/rl_0xCE4300.txt
    		[Token(Token = "0x60000CF")]
    		[Address(RVA = "0xCE4300", Offset = "0xCE2D00", VA = "0x180CE4300")]
    		get
    		{
    			return _uiLabel.preferredHeight;
    		}
    	}

    	[Token(Token = "0x17000029")]
    	public string LabelId
    	{
    		[Token(Token = "0x60000D0")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x453480, 5 B -- `mov rax, qword ptr [rcx + 0x50]; ret`: the backing field _labelId (+0x50 in dump.cs)
    		[Address(RVA = "0x453480", Offset = "0x451E80", VA = "0x180453480")]
    		get
    		{
    			return _labelId;
    		}
    	}

    	[Token(Token = "0x1700002A")]
    	protected TextMeshProUGUI UiLabel
    	{
    		[Token(Token = "0x60000D1")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x377910, 5 B -- `mov rax, qword ptr [rcx + 0x20]; ret`: the backing field _uiLabel (+0x20 in dump.cs)
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		get
    		{
    			return _uiLabel;
    		}
    	}

    	[Token(Token = "0x1700002B")]
    	protected virtual Color DefaultLabelColor
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCE4330, 11 B -- copies _defaultLabelColor (+0x34)
    		// Annotated disassembly: work/title-path/disas/rl_0xCE4330.txt
    		[Token(Token = "0x60000D2")]
    		[Address(RVA = "0xCE4330", Offset = "0xCE2D30", VA = "0x180CE4330", Slot = "4")]
    		get
    		{
    			return _defaultLabelColor;
    		}
    	}

    	[Token(Token = "0x1700002C")]
    	private ILocalizationProvider LocalizationProvider
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCE4340, 186 B -- lazy: [this+0x58], else
    		//   ServiceLocator.Get<ILocalizationManager>(true) stored there
    		// Annotated disassembly: work/title-path/disas/rl_0xCE4340.txt
    		[Token(Token = "0x60000D3")]
    		[Address(RVA = "0xCE4340", Offset = "0xCE2D40", VA = "0x180CE4340")]
    		get
    		{
    			if (_localizationProvider == null)
    			{
    				_localizationProvider = ServiceLocator.Get<ILocalizationManager>(true);
    			}
    			return _localizationProvider;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE4400, 151 B -- GetComponentInChildren<TextMeshProUGUI>() -> +0x20
    	// Annotated disassembly: work/title-path/disas/rl_0xCE4400.txt
    	[Token(Token = "0x60000D4")]
    	[Address(RVA = "0xCE4400", Offset = "0xCE2E00", VA = "0x180CE4400")]
    	private void Reset()
    	{
    		_uiLabel = GetComponentInChildren<TextMeshProUGUI>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE44A0, 360 B -- a null labelData falls back to _labelData (+0x28)
    	//   (0x180CE44B8: the binary's own test); atRuntime and labelData.LocalizableText.ChangeFont (both getters
    	//   inlined, +0x10 / +0x20) -> ChangeFont inlined (_uiLabel.font = LocalizationProvider.CurrentFontAsset,
    	//   ILocalizationProvider slot 2 through the 0x7FA00 interface thunk); tail jump to RebuildElements
    	// Annotated disassembly: work/title-path/disas/rl_0xCE44A0.txt
    	[Token(Token = "0x60000D5")]
    	[Address(RVA = "0xCE44A0", Offset = "0xCE2EA0", VA = "0x180CE44A0")]
    	public void Rebuild(LabelData labelData, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    		// 0x180CE44B8: the shipped code substitutes the serialized label data for a null argument
    		if (labelData == null)
    		{
    			labelData = _labelData;
    		}
    		if (atRuntime && labelData.LocalizableText.ChangeFont)
    		{
    			ChangeFont();
    		}
    		RebuildElements(labelData, locale);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE4610, 808 B -- SetInternalData and ModifyLabel, both inlined
    	//   (the two DoActionAsUndoable closures <>c__DisplayClass23_0 / 25_0)
    	// Annotated disassembly: work/title-path/disas/rl_0xCE4610.txt
    	[Token(Token = "0x60000D6")]
    	[Address(RVA = "0xCE4610", Offset = "0xCE3010", VA = "0x180CE4610")]
    	private void RebuildElements(LabelData labelData, [Optional] LocaleKind? locale)
    	{
    		SetInternalData(labelData);
    		ModifyLabel(labelData, locale);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE4940, 411 B -- this.DoActionAsUndoable("Set SetInternal Data",
    	//   <>c__DisplayClass23_0.b__0 (0xCE5230): _labelId (+0x50) = labelData.LabelId (+0x48))
    	// Annotated disassembly: work/title-path/disas/rl_0xCE4940.txt
    	[Token(Token = "0x60000D7")]
    	[Address(RVA = "0xCE4940", Offset = "0xCE3340", VA = "0x180CE4940")]
    	private void SetInternalData(LabelData labelData)
    	{
    		this.DoActionAsUndoable("Set SetInternal Data", delegate
    		{
    			_labelId = labelData.LabelId;
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE4AE0, 269 B -- _uiLabel.font = LocalizationProvider.CurrentFontAsset
    	//   (the getter inlined; ILocalizationProvider slot 2)
    	// Annotated disassembly: work/title-path/disas/rl_0xCE4AE0.txt
    	[Token(Token = "0x60000D8")]
    	[Address(RVA = "0xCE4AE0", Offset = "0xCE34E0", VA = "0x180CE4AE0")]
    	private void ChangeFont()
    	{
    		_uiLabel.font = LocalizationProvider.CurrentFontAsset;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE4BF0, 437 B / closure <>c__DisplayClass25_0.b__0 0xCE52A0, 1888 B --
    	//   _uiLabel.DoActionAsUndoable("Modify Label", label => ...): SetText(labelData.LocalizableText.GetText(locale));
    	//   fontSize = _defaultFontSize (+0x30) * (Scale ?? 1f, data 0x2AE2820 = 1.0); color = Color ?? DefaultLabelColor
    	//   (virtual slot 4); characterSpacing = CharacterSpacing ?? _defaultCharacterSpacing (+0x44);
    	//   horizontalAlignment = Alignment ?? _defaultHorizontalAlignment (+0x48) -- each ToggleableStruct converted
    	//   with its implicit operator to a Nullable (the TMP setters are inlined in the binary)
    	// Annotated disassembly: work/title-path/disas/rl_0xCE4BF0.txt, rl_ModifyLabel_b0.txt
    	[Token(Token = "0x60000D9")]
    	[Address(RVA = "0xCE4BF0", Offset = "0xCE35F0", VA = "0x180CE4BF0")]
    	private void ModifyLabel(LabelData labelData, [Optional] LocaleKind? locale)
    	{
    		_uiLabel.DoActionAsUndoable("Modify Label", delegate(TextMeshProUGUI label)
    		{
    			label.SetText(labelData.LocalizableText.GetText(locale));
    			float? scale = labelData.Scale;
    			label.fontSize = _defaultFontSize * (scale ?? 1f);
    			Color? color = labelData.Color;
    			label.color = color ?? DefaultLabelColor;
    			float? characterSpacing = labelData.CharacterSpacing;
    			label.characterSpacing = characterSpacing ?? _defaultCharacterSpacing;
    			HorizontalAlignmentOptions? alignment = labelData.Alignment;
    			label.horizontalAlignment = alignment ?? _defaultHorizontalAlignment;
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE4DB0, 598 B / closure <>c__DisplayClass26_1.b__0 0xCE5A10, 328 B --
    	//   for each index j: _uiLabel.DoActionAsUndoable("Set Text", label => label.SetText(
    	//   _uiLabel.text.Replace(string.Format("{{{0}}}", j), texts[j])))
    	// Annotated disassembly: work/title-path/disas/rl_0xCE4DB0.txt, rl_InsertTexts_b0.txt
    	[Token(Token = "0x60000DA")]
    	[Address(RVA = "0xCE4DB0", Offset = "0xCE37B0", VA = "0x180CE4DB0")]
    	public void InsertTexts(params string[] texts)
    	{
    		for (int j = 0; j < texts.Length; j++)
    		{
    			_uiLabel.DoActionAsUndoable("Set Text", delegate(TextMeshProUGUI label)
    			{
    				label.SetText(_uiLabel.text.Replace(string.Format("{{{0}}}", j), texts[j]));
    			});
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE5010, 190 B / b__27_0 0xCE5190 -- this.DoActionAsUndoable(
    	//   "Store Label Parameters as Default", _ => the label's fontSize, color (slot 22), characterSpacing and
    	//   horizontalAlignment into +0x30 / +0x34 / +0x44 / +0x48)
    	// Annotated disassembly: work/title-path/disas/rl_0xCE5010.txt, rl_0xCE5190.txt
    	[Token(Token = "0x60000DB")]
    	[Address(RVA = "0xCE5010", Offset = "0xCE3A10", VA = "0x180CE5010")]
    	public void StoreLabelParametersAsDefault()
    	{
    		this.DoActionAsUndoable("Store Label Parameters as Default", delegate
    		{
    			_defaultFontSize = _uiLabel.fontSize;
    			_defaultLabelColor = _uiLabel.color;
    			_defaultCharacterSpacing = _uiLabel.characterSpacing;
    			_defaultHorizontalAlignment = _uiLabel.horizontalAlignment;
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE50D0, 190 B / b__28_0 0xCE5210 -- this.DoActionAsUndoable(
    	//   "Reset Default Parameters", _ => zero +0x30..+0x4B: font size, colour, spacing and alignment to default)
    	// Annotated disassembly: work/title-path/disas/rl_0xCE50D0.txt, rl_0xCE5210.txt
    	[Token(Token = "0x60000DC")]
    	[Address(RVA = "0xCE50D0", Offset = "0xCE3AD0", VA = "0x180CE50D0")]
    	public void ResetDefaultParameters()
    	{
    		this.DoActionAsUndoable("Reset Default Parameters", delegate
    		{
    			_defaultFontSize = 0f;
    			_defaultLabelColor = default(Color);
    			_defaultCharacterSpacing = 0f;
    			_defaultHorizontalAlignment = (HorizontalAlignmentOptions)0;
    		});
    	}

    	[Token(Token = "0x60000DD")]
    	[Address(RVA = "0x311810", Offset = "0x310210", VA = "0x180311810")]
    	public RebuildableLabel()
    	{
    	}
    }
}
