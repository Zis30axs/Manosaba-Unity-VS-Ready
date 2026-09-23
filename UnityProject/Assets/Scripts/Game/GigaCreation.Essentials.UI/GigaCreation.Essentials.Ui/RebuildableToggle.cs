#nullable enable annotations
﻿using UniRx;
using GigaCreation.Tools.UndoExtensions;
using System.Linq;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Localization;
using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000048")]
    public class RebuildableToggle : MonoBehaviour
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200004D")]
    	[CompilerGenerated]
    	private struct _003COnValueChangedAsync_003Ed__31
    {
    		[Token(Token = "0x400013F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000140")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000141")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public RebuildableToggle _003C_003E4__this;

    		[Token(Token = "0x4000142")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000143")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask<bool>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600018B")]
    		[Address(RVA = "0xCEFC60", Offset = "0xCEE660", VA = "0x180CEFC60", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600018C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400012A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("Assets")]
    	[SerializeField]
    	private RebuildableLabel _labelPrefab;

    	[Token(Token = "0x400012B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[Header("References")]
    	[SerializeField]
    	private GameObject _gameObject;

    	[Token(Token = "0x400012C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private RectTransform _rectTransform;

    	[Token(Token = "0x400012D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private LayoutElement _layoutElement;

    	[Token(Token = "0x400012E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[SerializeField]
    	private ContentSizeFitter _contentSizeFitter;

    	[Token(Token = "0x400012F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[SerializeField]
    	private AutoLayoutSupporter _autoLayoutSupporter;

    	[Token(Token = "0x4000130")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[SerializeField]
    	private Toggle _uiToggle;

    	[Token(Token = "0x4000131")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	[Space]
    	[SerializeField]
    	private Transform _labelParent;

    	[Token(Token = "0x4000132")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	[SerializeField]
    	private RebuildableLabel _label;

    	[Token(Token = "0x4000133")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	[Header("Parameters")]
    	[SerializeField]
    	private bool _dontRegenerateLabel;

    	[Token(Token = "0x4000134")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	[SerializeField]
    	private ToggleData _toggleData;

    	[Token(Token = "0x1700003E")]
    	public bool IsOn
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCECBC0, 30 B -- _uiToggle (+0x50).isOn (m_IsOn +0x120)
    		// Annotated disassembly: work/title-path/disas/rtg_0xCECBC0.txt
    		[Token(Token = "0x6000160")]
    		[Address(RVA = "0xCECBC0", Offset = "0xCEB5C0", VA = "0x180CECBC0")]
    		get
    		{
    			return _uiToggle.isOn;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCECBE0, 33 B -- _uiToggle.isOn = value (setter inlined: tail jump Toggle.Set(value, true))
    		// Annotated disassembly: work/title-path/disas/rtg_0xCECBE0.txt
    		[Token(Token = "0x6000161")]
    		[Address(RVA = "0xCECBE0", Offset = "0xCEB5E0", VA = "0x180CECBE0")]
    		set
    		{
    			_uiToggle.isOn = value;
    		}
    	}

    	[Token(Token = "0x1700003F")]
    	private Vector2 DefaultSize
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCECC10, 29 B -- tail jump _rectTransform (+0x30).sizeDelta
    		// Annotated disassembly: work/title-path/disas/rtg_0xCECC10.txt
    		[Token(Token = "0x6000162")]
    		[Address(RVA = "0xCECC10", Offset = "0xCEB610", VA = "0x180CECC10")]
    		get
    		{
    			return _rectTransform.sizeDelta;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCECC30, 7 B -- tail jump GetReferences() (the body is shared with
    	//   <GetReferencesInEditor>b__34_0)
    	// Annotated disassembly: work/title-path/disas/rtg_0xCECC30.txt
    	[Token(Token = "0x6000163")]
    	[Address(RVA = "0xCECC30", Offset = "0xCEB630", VA = "0x180CECC30")]
    	private void Reset()
    	{
    		GetReferences();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCECC40, 8 B -- tail jump RebuildElements(atRuntime, locale)
    	// Annotated disassembly: work/title-path/disas/rtg_0xCECC40.txt
    	[Token(Token = "0x6000164")]
    	[Address(RVA = "0xCECC40", Offset = "0xCEB640", VA = "0x180CECC40")]
    	public void Rebuild(bool atRuntime, [Optional] LocaleKind? locale)
    	{
    		RebuildElements(atRuntime, locale);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCECC50, 91 B -- RebuildElements(false, null), then
    	//   _autoLayoutSupporter (+0x48).ExecuteRebuildingAsync(its destroyCancellationToken).Forget()
    	// Annotated disassembly: work/title-path/disas/rtg_0xCECC50.txt
    	[Token(Token = "0x6000165")]
    	[Address(RVA = "0xCECC50", Offset = "0xCEB650", VA = "0x180CECC50")]
    	public void RebuildInEditor()
    	{
    		RebuildElements(false);
    		_autoLayoutSupporter.ExecuteRebuildingAsync(_autoLayoutSupporter.destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCECCB0, 136 B -- RebuildContentSizeFitter(); RebuildLabel(_toggleData (+0x70)
    	//   .LabelData (+0x20), atRuntime, locale); UpdateWidth(WidthData (+0x10)); tail jump UpdateHeight(HeightData (+0x18))
    	// Annotated disassembly: work/title-path/disas/rtg_0xCECCB0.txt
    	[Token(Token = "0x6000166")]
    	[Address(RVA = "0xCECCB0", Offset = "0xCEB6B0", VA = "0x180CECCB0")]
    	private void RebuildElements(bool atRuntime, [Optional] LocaleKind? locale)
    	{
    		RebuildContentSizeFitter();
    		RebuildLabel(_toggleData.LabelData, atRuntime, locale);
    		UpdateWidth(_toggleData.WidthData);
    		UpdateHeight(_toggleData.HeightData);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCECD40, 1583 B / b__20_0 0xCEF0E0, b__20_1 0xCEF140, <>c.b__20_2 0xCDAA10
    	//   (folded with RebuildableButton's) -- the fitter is _contentSizeFitter (+0x40) while alive, else
    	//   GetComponent<ContentSizeFitter>(); under a parent whose ILayoutGroup is a Behaviour a live fitter (op_Implicit
    	//   0x180CED0F8) is destroyed and the field cleared ("Set Null to Content Size Fitter Field"); otherwise, only when
    	//   there is no fitter (0x180CED1F7), one is added ("Set Added Content Size Fitter to Field") and set to
    	//   PreferredSize both ways ("Update Content Size Fitter")
    	// Annotated disassembly: work/title-path/disas/rtg_0xCECD40.txt
    	[Token(Token = "0x6000167")]
    	[Address(RVA = "0xCECD40", Offset = "0xCEB740", VA = "0x180CECD40")]
    	private void RebuildContentSizeFitter()
    	{
    		ContentSizeFitter contentSizeFitter = ((_contentSizeFitter != null) ? _contentSizeFitter : GetComponent<ContentSizeFitter>());
    		Behaviour behaviour = ((base.transform.parent != null) ? (base.transform.parent.GetComponent<ILayoutGroup>() as Behaviour) : null);
    		if (behaviour != null)
    		{
    			// 0x180CED0F8: the shipped code tests the fitter before destroying it
    			if ((bool)contentSizeFitter)
    			{
    				contentSizeFitter.DestroyAsUndoable();
    				this.DoActionAsUndoable("Set Null to Content Size Fitter Field", delegate
    				{
    					_contentSizeFitter = null;
    				});
    			}
    		}
    		// 0x180CED1F7: an existing fitter is left as it is
    		else if (contentSizeFitter == null)
    		{
    			this.DoActionAsUndoable("Set Added Content Size Fitter to Field", delegate
    			{
    				_contentSizeFitter = _gameObject.AddComponentAsUndoable<ContentSizeFitter>();
    			});
    			_contentSizeFitter.DoActionAsUndoable("Update Content Size Fitter", delegate(ContentSizeFitter fitter)
    			{
    				fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
    				fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    			});
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCED370, 523 B -- an empty labelData.LocalizableText (IsEmpty inlined) only
    	//   destroys the held label; otherwise, unless _dontRegenerateLabel (+0x68), the held label is destroyed and a new
    	//   one generated, and with it set an existing child label is held (a new one generated when none is found); then
    	//   _label (+0x60).Rebuild(labelData, atRuntime, locale)
    	// Annotated disassembly: work/title-path/disas/rtg_0xCED370.txt
    	[Token(Token = "0x6000168")]
    	[Address(RVA = "0xCED370", Offset = "0xCEBD70", VA = "0x180CED370")]
    	private void RebuildLabel(LabelData labelData, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    		if (labelData.LocalizableText.IsEmpty())
    		{
    			DestroyHoldenLabel();
    			return;
    		}
    		if (!_dontRegenerateLabel)
    		{
    			DestroyHoldenLabel();
    			GenerateAndHoldLabel();
    		}
    		else if (!HoldExistingLabel())
    		{
    			GenerateAndHoldLabel();
    		}
    		_label.Rebuild(labelData, atRuntime, locale);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCED580, 382 B / b__22_0 0x50DA80 -- a live _label (0x180CED685): its GameObject
    	//   DestroyAsUndoable, then "Set Null to Label Field" (_label = null)
    	// Annotated disassembly: work/title-path/disas/rtg_0xCED580.txt
    	[Token(Token = "0x6000169")]
    	[Address(RVA = "0xCED580", Offset = "0xCEBF80", VA = "0x180CED580")]
    	private void DestroyHoldenLabel()
    	{
    		// 0x180CED685: the shipped code tests the held label before destroying it
    		if (_label != null)
    		{
    			_label.gameObject.DestroyAsUndoable();
    			this.DoActionAsUndoable("Set Null to Label Field", delegate
    			{
    				_label = null;
    			});
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCED700, 1445 B / <>c.b__23_0 0xCEF6E0, <>c.b__23_1 0xCEF740, DisplayClass23_0.b__2
    	//   0xCEF810 -- false without a live _labelParent (+0x58, 0x180CED8FC); existingLabel = _labelParent.Cast<Transform>()
    	//   .Select(GetComponent<RebuildableLabel>).FirstOrDefault(label => label != null); false when none (0x180CEDC0D);
    	//   else "Hold Existing Label" (_label = existingLabel) and true
    	// Annotated disassembly: work/title-path/disas/rtg_0xCED700.txt
    	[Token(Token = "0x600016A")]
    	[Address(RVA = "0xCED700", Offset = "0xCEC100", VA = "0x180CED700")]
    	private bool HoldExistingLabel()
    	{
    		// 0x180CED8FC: without a label parent there is nothing to hold
    		if (_labelParent == null)
    		{
    			return false;
    		}
    		RebuildableLabel existingLabel = _labelParent.Cast<Transform>().Select((Transform trans) => trans.GetComponent<RebuildableLabel>())
    			.FirstOrDefault((RebuildableLabel label) => label != null);
    		// 0x180CEDC0D: no existing label found
    		if (existingLabel == null)
    		{
    			return false;
    		}
    		this.DoActionAsUndoable("Hold Existing Label", delegate
    		{
    			_label = existingLabel;
    		});
    		return true;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEDCB0, 622 B / b__24_0 0xCEF210 -- a missing _labelPrefab (+0x20) or _labelParent
    	//   (+0x58) only logs Debug.LogError ("ラベルのプレハブが設定されていません。" / "ラベルの親が設定されていません。");
    	//   then "Generate and Hold Label": _label = UnityEngine.Object.Instantiate(_labelPrefab, _labelParent)
    	// Annotated disassembly: work/title-path/disas/rtg_0xCEDCB0.txt, rtg_0xCEF210.txt
    	[Token(Token = "0x600016B")]
    	[Address(RVA = "0xCEDCB0", Offset = "0xCEC6B0", VA = "0x180CEDCB0")]
    	private void GenerateAndHoldLabel()
    	{
    		if (_labelPrefab == null)
    		{
    			UnityEngine.Debug.LogError("ラベルのプレハブが設定されていません。");
    		}
    		if (_labelParent == null)
    		{
    			UnityEngine.Debug.LogError("ラベルの親が設定されていません。");
    		}
    		this.DoActionAsUndoable("Generate and Hold Label", delegate
    		{
    			_label = UnityEngine.Object.Instantiate(_labelPrefab, _labelParent);
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEDF20, 36 B -- tail jump _label.Rebuild(labelData, atRuntime, locale)
    	// Annotated disassembly: work/title-path/disas/rtg_0xCEDF20.txt
    	[Token(Token = "0x600016C")]
    	[Address(RVA = "0xCEDF20", Offset = "0xCEC920", VA = "0x180CEDF20")]
    	private void RebuildLabelElements(LabelData labelData, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    		_label.Rebuild(labelData, atRuntime, locale);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEDF50, 591 B / DisplayClass26_0.b__0 0xCEF880 -- with a live _layoutElement (+0x38)
    	//   and widthData.Kind != None: "Update Width": preferredWidth = -1 (data 0x2AE2D98) when the parent's
    	//   HorizontalOrVerticalLayoutGroup controls (+0x66) and force-expands (+0x64) the width; otherwise the
    	//   switch expression: Fixed (1) -> FixedSize (+0x14); Flexible (2) -> GetContentPreferredWidth() + Padding (+0x18) * 2; otherwise the element's current value
    	// Annotated disassembly: work/title-path/disas/rtg_0xCEDF50.txt, rtg_0xCEF880.txt
    	[Token(Token = "0x600016D")]
    	[Address(RVA = "0xCEDF50", Offset = "0xCEC950", VA = "0x180CEDF50")]
    	private void UpdateWidth(SelectableSizeData widthData)
    	{
    		if (_layoutElement == null || widthData.Kind == SelectableSizeData.OverrideKind.None)
    		{
    			return;
    		}
    		_layoutElement.DoActionAsUndoable("Update Width", delegate(LayoutElement element)
    		{
    			HorizontalOrVerticalLayoutGroup component = _rectTransform.parent.GetComponent<HorizontalOrVerticalLayoutGroup>();
    			if (component != null && component.childControlWidth && component.childForceExpandWidth)
    			{
    				element.preferredWidth = -1f;
    				return;
    			}
    			element.preferredWidth = widthData.Kind switch
    			{
    				SelectableSizeData.OverrideKind.Fixed => widthData.FixedSize,
    				SelectableSizeData.OverrideKind.Flexible => GetContentPreferredWidth() + widthData.Padding * 2f,
    				_ => element.preferredWidth,
    			};
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEE1A0, 591 B / DisplayClass27_0.b__0 0xCEFA70 -- UpdateWidth for the height
    	//   ("Update Height"; childControlHeight +0x67, childForceExpandHeight +0x65);
    	//   switch expression: Fixed (1) -> FixedSize (+0x14); Flexible (2) -> GetContentPreferredHeight() + Padding (+0x18) * 2; otherwise the element's current value
    	// Annotated disassembly: work/title-path/disas/rtg_0xCEE1A0.txt, rtg_0xCEFA70.txt
    	[Token(Token = "0x600016E")]
    	[Address(RVA = "0xCEE1A0", Offset = "0xCECBA0", VA = "0x180CEE1A0")]
    	private void UpdateHeight(SelectableSizeData heightData)
    	{
    		if (_layoutElement == null || heightData.Kind == SelectableSizeData.OverrideKind.None)
    		{
    			return;
    		}
    		_layoutElement.DoActionAsUndoable("Update Height", delegate(LayoutElement element)
    		{
    			HorizontalOrVerticalLayoutGroup component = _rectTransform.parent.GetComponent<HorizontalOrVerticalLayoutGroup>();
    			if (component != null && component.childControlHeight && component.childForceExpandHeight)
    			{
    				element.preferredHeight = -1f;
    				return;
    			}
    			element.preferredHeight = heightData.Kind switch
    			{
    				SelectableSizeData.OverrideKind.Fixed => heightData.FixedSize,
    				SelectableSizeData.OverrideKind.Flexible => GetContentPreferredHeight() + heightData.Padding * 2f,
    				_ => element.preferredHeight,
    			};
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEE3F0, 548 B / b__28_0 0xCEF2F0 -- as RebuildableButton: a live _label's
    	//   PreferredWidth, else the first child ILayoutElement that is a Behaviour on another GameObject than _gameObject,
    	//   or Debug.LogError("ILayoutElement を持った子がありません。") and 0
    	// Annotated disassembly: work/title-path/disas/rtg_0xCEE3F0.txt
    	[Token(Token = "0x600016F")]
    	[Address(RVA = "0xCEE3F0", Offset = "0xCECDF0", VA = "0x180CEE3F0")]
    	private float GetContentPreferredWidth()
    	{
    		if (_label != null)
    		{
    			return _label.PreferredWidth;
    		}
    		ILayoutElement layoutElement = GetComponentsInChildren<ILayoutElement>().FirstOrDefault((ILayoutElement le) => le is Behaviour behaviour && behaviour.gameObject != _gameObject);
    		if (layoutElement == null)
    		{
    			UnityEngine.Debug.LogError("ILayoutElement を持った子がありません。");
    			return 0f;
    		}
    		return layoutElement.preferredWidth;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEE620, 548 B / b__29_0 0xCEF490 -- GetContentPreferredWidth for the height
    	// Annotated disassembly: work/title-path/disas/rtg_0xCEE620.txt
    	[Token(Token = "0x6000170")]
    	[Address(RVA = "0xCEE620", Offset = "0xCED020", VA = "0x180CEE620")]
    	private float GetContentPreferredHeight()
    	{
    		if (_label != null)
    		{
    			return _label.PreferredHeight;
    		}
    		ILayoutElement layoutElement = GetComponentsInChildren<ILayoutElement>().FirstOrDefault((ILayoutElement le) => le is Behaviour behaviour && behaviour.gameObject != _gameObject);
    		if (layoutElement == null)
    		{
    			UnityEngine.Debug.LogError("ILayoutElement を持った子がありません。");
    			return 0f;
    		}
    		return layoutElement.preferredHeight;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEE850, 11 B -- tail jump UniRx UnityUIComponentExtensions
    	//   .OnValueChangedAsObservable(_uiToggle (+0x50))
    	// Annotated disassembly: work/title-path/disas/rtg_0xCEE850.txt
    	[Token(Token = "0x6000171")]
    	[Address(RVA = "0xCEE850", Offset = "0xCED250", VA = "0x180CEE850")]
    	public IObservable<bool> OnValueChangedAsObservable()
    	{
    		return _uiToggle.OnValueChangedAsObservable();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEE860 (kickoff) / 0xCEFC60 (<OnValueChangedAsync>d__31.MoveNext, 883 B): await
    	//   _uiToggle.OnValueChangedAsync(ct) (UniTask's extension inlined: new AsyncUnityEventHandler<bool>(
    	//   onValueChanged, ct, callOnce: true).OnInvokeAsync()). Body in OnValueChangedAsyncCore.
    	// Annotated disassembly: work/title-path/disas/rtg_0xCEFC60.txt
    	[Token(Token = "0x6000172")]
    	[Address(RVA = "0xCEE860", Offset = "0xCED260", VA = "0x180CEE860")]
    	[AsyncStateMachine(typeof(_003COnValueChangedAsync_003Ed__31))]
    	public UniTask OnValueChangedAsync([Optional] CancellationToken ct)
    	{
    		return OnValueChangedAsyncCore(ct);
    	}

    	private async UniTask OnValueChangedAsyncCore(CancellationToken ct)
    	{
    		await _uiToggle.OnValueChangedAsync(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEEA00, 773 B -- gameObject -> +0x28; transform as RectTransform -> +0x30;
    	//   GetComponent<LayoutElement / ContentSizeFitter / AutoLayoutSupporter>() -> +0x38 / +0x40 / +0x48;
    	//   GetComponentInChildren<Toggle>() -> +0x50
    	// Annotated disassembly: work/title-path/disas/rtg_0xCEEA00.txt
    	[Token(Token = "0x6000173")]
    	[Address(RVA = "0xCEEA00", Offset = "0xCED400", VA = "0x180CEEA00")]
    	private void GetReferences()
    	{
    		_gameObject = base.gameObject;
    		_rectTransform = base.transform as RectTransform;
    		_layoutElement = GetComponent<LayoutElement>();
    		_contentSizeFitter = GetComponent<ContentSizeFitter>();
    		_autoLayoutSupporter = GetComponent<AutoLayoutSupporter>();
    		_uiToggle = GetComponentInChildren<Toggle>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEED10, 570 B -- +0x28 / +0x30 / +0x38 / +0x40 / +0x48 / +0x50 / +0x60 = null
    	//   (_labelParent and the serialized fields are kept)
    	// Annotated disassembly: work/title-path/disas/rtg_0xCEED10.txt
    	[Token(Token = "0x6000174")]
    	[Address(RVA = "0xCEED10", Offset = "0xCED710", VA = "0x180CEED10")]
    	private void PurgeReferences()
    	{
    		_gameObject = null;
    		_rectTransform = null;
    		_layoutElement = null;
    		_contentSizeFitter = null;
    		_autoLayoutSupporter = null;
    		_uiToggle = null;
    		_label = null;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEEF50, 199 B / b__34_0 0xCECC30 -- "Get References" (GetReferences), then
    	//   tail jump HoldExistingLabel() (its result dropped)
    	// Annotated disassembly: work/title-path/disas/rtg_0xCEEF50.txt
    	[Token(Token = "0x6000175")]
    	[Address(RVA = "0xCEEF50", Offset = "0xCED950", VA = "0x180CEEF50")]
    	public void GetReferencesInEditor()
    	{
    		this.DoActionAsUndoable("Get References", delegate
    		{
    			GetReferences();
    		});
    		HoldExistingLabel();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEF020, 190 B / b__35_0 0xCEF630 -- "Purge References" (PurgeReferences)
    	// Annotated disassembly: work/title-path/disas/rtg_0xCEF020.txt
    	[Token(Token = "0x6000176")]
    	[Address(RVA = "0xCEF020", Offset = "0xCEDA20", VA = "0x180CEF020")]
    	public void PurgeReferencesInEditor()
    	{
    		this.DoActionAsUndoable("Purge References", delegate
    		{
    			PurgeReferences();
    		});
    	}

    	[Token(Token = "0x6000177")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public RebuildableToggle()
    	{
    	}
    }
}
