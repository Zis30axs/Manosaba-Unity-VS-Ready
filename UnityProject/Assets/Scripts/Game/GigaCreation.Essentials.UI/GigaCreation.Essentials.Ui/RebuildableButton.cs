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
using GigaCreation.Essentials.Input;
using GigaCreation.Essentials.Localization;
using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x200000B")]
    public class RebuildableButton : MonoBehaviour
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000012")]
    	[CompilerGenerated]
    	private struct _003COnClickAsync_003Ed__27
    {
    		[Token(Token = "0x4000034")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000035")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<ResponseKind> _003C_003Et__builder;

    		[Token(Token = "0x4000036")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public RebuildableButton _003C_003E4__this;

    		[Token(Token = "0x4000037")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000038")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000048")]
    		[Address(RVA = "0xCDB1A0", Offset = "0xCD9BA0", VA = "0x180CDB1A0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000049")]
    		[Address(RVA = "0xCDB500", Offset = "0xCD9F00", VA = "0x180CDB500", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400001E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private GameObject _gameObject;

    	[Token(Token = "0x400001F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private RectTransform _rectTransform;

    	[Token(Token = "0x4000020")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private LayoutElement _layoutElement;

    	[Token(Token = "0x4000021")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private Button _uiButton;

    	[Token(Token = "0x4000022")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[SerializeField]
    	private RebuildableLabel _label;

    	[Token(Token = "0x4000023")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[Space]
    	[SerializeField]
    	private ContentSizeFitter _contentSizeFitter;

    	[Token(Token = "0x4000024")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[SerializeField]
    	private AutoLayoutSupporter _autoLayoutSupporter;

    	[Token(Token = "0x4000025")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	[Header("Parameters")]
    	[SerializeField]
    	private bool _controlsItsOwnSize;

    	[Token(Token = "0x4000026")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	[SerializeField]
    	private ButtonData _buttonData;

    	[Token(Token = "0x4000027")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	[Space]
    	[SerializeField]
    	private ResponseKind _responseKind;

    	[Token(Token = "0x17000006")]
    	public ResponseKind ResponseKind
    	{
    		[Token(Token = "0x6000021")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x841E80, 4 B -- `mov eax, dword ptr [rcx + 0x68]; ret`: the backing field _responseKind (+0x68 in dump.cs)
    		[Address(RVA = "0x841E80", Offset = "0x840880", VA = "0x180841E80")]
    		get
    		{
    			return _responseKind;
    		}
    	}

    	[Token(Token = "0x17000007")]
    	protected RebuildableLabel Label
    	{
    		[Token(Token = "0x6000022")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x44CE10, 5 B -- `mov rax, qword ptr [rcx + 0x40]; ret`: the backing field _label (+0x40 in dump.cs)
    		[Address(RVA = "0x44CE10", Offset = "0x44B810", VA = "0x18044CE10")]
    		get
    		{
    			return _label;
    		}
    	}

    	[Token(Token = "0x17000008")]
    	protected virtual Vector2 DefaultSize
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCD8150, 29 B -- tail jump to _rectTransform (+0x28).sizeDelta
    		// Annotated disassembly: work/title-path/disas/rb_0xCD8150.txt
    		[Token(Token = "0x6000023")]
    		[Address(RVA = "0xCD8150", Offset = "0xCD6B50", VA = "0x180CD8150", Slot = "4")]
    		get
    		{
    			return _rectTransform.sizeDelta;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x538020 (shared, 20 B) -- tail call through vtable +0x198 = slot 6,
    	//   GetReferences
    	// Annotated disassembly: work/title-path/disas/rb_0x538020.txt
    	[Token(Token = "0x6000024")]
    	[Address(RVA = "0x538020", Offset = "0x536A20", VA = "0x180538020")]
    	private void Reset()
    	{
    		GetReferences();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD8170, 506 B -- a null buttonData falls back to _buttonData (+0x60)
    	//   (0x180CD81CA: the binary's own test); controlsItsOwnSize ?? _controlsItsOwnSize (+0x58); virtual
    	//   RebuildElements (slot 5); then, only when the size is its own and not at runtime,
    	//   _autoLayoutSupporter.ExecuteRebuildingAsync(_autoLayoutSupporter.destroyCancellationToken).Forget()
    	// Annotated disassembly: work/title-path/disas/rb_0xCD8170.txt
    	[Token(Token = "0x6000025")]
    	[Address(RVA = "0xCD8170", Offset = "0xCD6B70", VA = "0x180CD8170")]
    	public void Rebuild(ButtonData buttonData, bool atRuntime, [Optional] bool? controlsItsOwnSize, [Optional] LocaleKind? locale)
    	{
    		// 0x180CD81CA: the shipped code substitutes the serialized button data for a null argument
    		if (buttonData == null)
    		{
    			buttonData = _buttonData;
    		}
    		bool controls = controlsItsOwnSize ?? _controlsItsOwnSize;
    		RebuildElements(buttonData, atRuntime, controls, locale);
    		if (controls && !atRuntime)
    		{
    			_autoLayoutSupporter.ExecuteRebuildingAsync(_autoLayoutSupporter.destroyCancellationToken).Forget();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD8370, 1435 B -- SetInternalData, RebuildLabel(buttonData.LabelData),
    	//   ModifyWidth(WidthData), ModifyHeight(HeightData) - all four inlined - then a tail call to
    	//   RebuildLayoutController(controlsItsOwnSize)
    	// Annotated disassembly: work/title-path/disas/rb_0xCD8370.txt
    	[Token(Token = "0x6000026")]
    	[Address(RVA = "0xCD8370", Offset = "0xCD6D70", VA = "0x180CD8370", Slot = "5")]
    	protected virtual void RebuildElements(ButtonData buttonData, bool atRuntime, bool controlsItsOwnSize, [Optional] LocaleKind? locale)
    	{
    		SetInternalData(buttonData);
    		RebuildLabel(buttonData.LabelData, atRuntime, locale);
    		ModifyWidth(buttonData.WidthData);
    		ModifyHeight(buttonData.HeightData);
    		RebuildLayoutController(controlsItsOwnSize);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD8910, 411 B / closure 0xCDAAD0 -- this.DoActionAsUndoable(
    	//   "Set Internal Data", _ => _responseKind (+0x68) = buttonData.ResponseKind (+0x20))
    	// Annotated disassembly: work/title-path/disas/rb_0xCD8910.txt, rb_0xCDAAD0.txt
    	[Token(Token = "0x6000027")]
    	[Address(RVA = "0xCD8910", Offset = "0xCD7310", VA = "0x180CD8910")]
    	private void SetInternalData(ButtonData buttonData)
    	{
    		this.DoActionAsUndoable("Set Internal Data", delegate
    		{
    			_responseKind = buttonData.ResponseKind;
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD8AB0, 282 B -- `_label != null` (UnityEngine.Object inequality)
    	//   -> _label.Rebuild(labelData, atRuntime, locale)
    	// Annotated disassembly: work/title-path/disas/rb_0xCD8AB0.txt
    	[Token(Token = "0x6000028")]
    	[Address(RVA = "0xCD8AB0", Offset = "0xCD74B0", VA = "0x180CD8AB0")]
    	private void RebuildLabel(LabelData labelData, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    		if (_label != null)
    		{
    			_label.Rebuild(labelData, atRuntime, locale);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD8BD0, 416 B / closure 0xCDAB00, 723 B -- _layoutElement.DoActionAsUndoable(
    	//   "Modify Width", element => ...): under a HorizontalOrVerticalLayoutGroup parent that controls and
    	//   force-expands the width (+0x66, +0x64) the preferred width is -1 (data 0x2AE2D98); otherwise by Kind:
    	//   None -> DefaultSize.x (virtual slot 4), Fixed -> FixedSize (+0x14), Flexible -> content width + 2 * Padding
    	//   (+0x18), anything else keeps the element's own preferredWidth (LayoutElement slots 35 / 36)
    	// Annotated disassembly: work/title-path/disas/rb_0xCD8BD0.txt, rb_0xCDAB00.txt
    	[Token(Token = "0x6000029")]
    	[Address(RVA = "0xCD8BD0", Offset = "0xCD75D0", VA = "0x180CD8BD0")]
    	private void ModifyWidth(SelectableSizeData widthData)
    	{
    		_layoutElement.DoActionAsUndoable("Modify Width", delegate(LayoutElement element)
    		{
    			HorizontalOrVerticalLayoutGroup layoutGroup = (_rectTransform.parent != null) ? _rectTransform.parent.GetComponent<HorizontalOrVerticalLayoutGroup>() : null;
    			if (layoutGroup != null && layoutGroup.childControlWidth && layoutGroup.childForceExpandWidth)
    			{
    				element.preferredWidth = -1f;
    				return;
    			}
    			element.preferredWidth = widthData.Kind switch
    			{
    				SelectableSizeData.OverrideKind.None => DefaultSize.x,
    				SelectableSizeData.OverrideKind.Fixed => widthData.FixedSize,
    				SelectableSizeData.OverrideKind.Flexible => GetContentPreferredWidth() + widthData.Padding * 2f,
    				_ => element.preferredWidth,
    			};
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD8D70, 416 B / closure 0xCDADE0, 723 B -- as ModifyWidth for the height:
    	//   childControlHeight / childForceExpandHeight (+0x67, +0x65), DefaultSize.y, preferredHeight (slots 37 / 38)
    	// Annotated disassembly: work/title-path/disas/rb_0xCD8D70.txt, rb_0xCDADE0.txt
    	[Token(Token = "0x600002A")]
    	[Address(RVA = "0xCD8D70", Offset = "0xCD7770", VA = "0x180CD8D70")]
    	private void ModifyHeight(SelectableSizeData heightData)
    	{
    		_layoutElement.DoActionAsUndoable("Modify Height", delegate(LayoutElement element)
    		{
    			HorizontalOrVerticalLayoutGroup layoutGroup = (_rectTransform.parent != null) ? _rectTransform.parent.GetComponent<HorizontalOrVerticalLayoutGroup>() : null;
    			if (layoutGroup != null && layoutGroup.childControlHeight && layoutGroup.childForceExpandHeight)
    			{
    				element.preferredHeight = -1f;
    				return;
    			}
    			element.preferredHeight = heightData.Kind switch
    			{
    				SelectableSizeData.OverrideKind.None => DefaultSize.y,
    				SelectableSizeData.OverrideKind.Fixed => heightData.FixedSize,
    				SelectableSizeData.OverrideKind.Flexible => GetContentPreferredHeight() + heightData.Padding * 2f,
    				_ => element.preferredHeight,
    			};
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD8F10, 548 B -- a live _label (+0x40) -> its PreferredWidth (inlined);
    	//   else the first child ILayoutElement that is a Behaviour on another GameObject than _gameObject
    	//   (b__23_0 0xCDA560) -> its preferredWidth (interface slot 3); none -> Debug.LogError(literal 0x2E691C8), 0
    	// Annotated disassembly: work/title-path/disas/rb_0xCD8F10.txt, rb_0xCDA560.txt
    	[Token(Token = "0x600002B")]
    	[Address(RVA = "0xCD8F10", Offset = "0xCD7910", VA = "0x180CD8F10")]
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD9140, 548 B -- as GetContentPreferredWidth for the height
    	//   (b__24_0 0xCDA700; interface slot 6)
    	// Annotated disassembly: work/title-path/disas/rb_0xCD9140.txt, rb_0xCDA700.txt
    	[Token(Token = "0x600002C")]
    	[Address(RVA = "0xCD9140", Offset = "0xCD7B40", VA = "0x180CD9140")]
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD9370, 1982 B -- controlling its own size: add a ContentSizeFitter / an
    	//   AutoLayoutSupporter that is missing (AddComponentAsUndoable, the new component stored through
    	//   DoActionAsUndoable closures b__2 / b__3), then _contentSizeFitter.DoActionAsUndoable("Modify Layout
    	//   Controller", <>c.b__25_1 0xCDAA10: both fits PreferredSize); otherwise destroy both (DestroyAsUndoable) and
    	//   null the fields (b__25_0 0xCDA8A0)
    	// Annotated disassembly: work/title-path/disas/rb_0xCD9370.txt, rb_0xCDA8A0.txt, rb_0xCDAA10.txt
    	[Token(Token = "0x600002D")]
    	[Address(RVA = "0xCD9370", Offset = "0xCD7D70", VA = "0x180CD9370")]
    	private void RebuildLayoutController(bool controlsItsOwnSize)
    	{
    		if (controlsItsOwnSize)
    		{
    			if (_contentSizeFitter == null)
    			{
    				ContentSizeFitter fitter = _gameObject.AddComponentAsUndoable<ContentSizeFitter>();
    				this.DoActionAsUndoable("Set Added Content Size Fitter to Field", delegate
    				{
    					_contentSizeFitter = fitter;
    				});
    			}
    			if (_autoLayoutSupporter == null)
    			{
    				AutoLayoutSupporter supporter = _gameObject.AddComponentAsUndoable<AutoLayoutSupporter>();
    				this.DoActionAsUndoable("Set Added Auto Layout Supporter to Field", delegate
    				{
    					_autoLayoutSupporter = supporter;
    				});
    			}
    			_contentSizeFitter.DoActionAsUndoable("Modify Layout Controller", delegate(ContentSizeFitter fitter)
    			{
    				fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
    				fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    			});
    			return;
    		}
    		if (_contentSizeFitter != null)
    		{
    			_contentSizeFitter.DestroyAsUndoable();
    		}
    		if (_autoLayoutSupporter != null)
    		{
    			_autoLayoutSupporter.DestroyAsUndoable();
    		}
    		this.DoActionAsUndoable("Set Null to Fields of Layout Controllers", delegate
    		{
    			_contentSizeFitter = null;
    			_autoLayoutSupporter = null;
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD9B30, 224 B -- _uiButton (+0x38).onClick (+0x100).AsObservable()
    	//   .Select(b__26_0: _ => _responseKind (+0x68))
    	// Annotated disassembly: work/title-path/disas/rb_0xCD9B30.txt
    	[Token(Token = "0x600002E")]
    	[Address(RVA = "0xCD9B30", Offset = "0xCD8530", VA = "0x180CD9B30")]
    	public IObservable<ResponseKind> OnClickAsObservable()
    	{
    		return _uiButton.onClick.AsObservable().Select((Unit _) => _responseKind);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD9C20 (kickoff) / 0xCDB1A0 (<OnClickAsync>d__27.MoveNext, 852 B) --
    	//   await new AsyncUnityEventHandler(_uiButton.onClick, ct, callOnce: true).OnInvokeAsync() (UniTask's
    	//   Button.OnClickAsync(ct)), then the result is _responseKind (+0x68). Body in OnClickAsyncCore.
    	// Annotated disassembly: work/title-path/disas/rb_0xCD9C20.txt, rb_0xCDB1A0.txt
    	[Token(Token = "0x600002F")]
    	[Address(RVA = "0xCD9C20", Offset = "0xCD8620", VA = "0x180CD9C20")]
    	[AsyncStateMachine(typeof(_003COnClickAsync_003Ed__27))]
    	public UniTask<ResponseKind> OnClickAsync([Optional] CancellationToken ct)
    	{
    		return OnClickAsyncCore(ct);
    	}

    	private async UniTask<ResponseKind> OnClickAsyncCore(CancellationToken ct)
    	{
    		await _uiButton.OnClickAsync(ct);
    		return _responseKind;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD9E10, 901 B -- gameObject, transform as RectTransform,
    	//   GetComponent<LayoutElement>, GetComponentInChildren<Button>, GetComponentInChildren<RebuildableLabel>,
    	//   GetComponent<ContentSizeFitter>, GetComponent<AutoLayoutSupporter> into +0x20..+0x50
    	// Annotated disassembly: work/title-path/disas/rb_0xCD9E10.txt
    	[Token(Token = "0x6000030")]
    	[Address(RVA = "0xCD9E10", Offset = "0xCD8810", VA = "0x180CD9E10", Slot = "6")]
    	protected virtual void GetReferences()
    	{
    		_gameObject = base.gameObject;
    		_rectTransform = base.transform as RectTransform;
    		_layoutElement = GetComponent<LayoutElement>();
    		_uiButton = GetComponentInChildren<Button>();
    		_label = GetComponentInChildren<RebuildableLabel>();
    		_contentSizeFitter = GetComponent<ContentSizeFitter>();
    		_autoLayoutSupporter = GetComponent<AutoLayoutSupporter>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDA1A0, 570 B -- the same seven fields set to null
    	// Annotated disassembly: work/title-path/disas/rb_0xCDA1A0.txt
    	[Token(Token = "0x6000031")]
    	[Address(RVA = "0xCDA1A0", Offset = "0xCD8BA0", VA = "0x180CDA1A0", Slot = "7")]
    	protected virtual void PurgeReferences()
    	{
    		_gameObject = null;
    		_rectTransform = null;
    		_layoutElement = null;
    		_uiButton = null;
    		_label = null;
    		_contentSizeFitter = null;
    		_autoLayoutSupporter = null;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDA3E0, 190 B -- this.DoActionAsUndoable("Get References",
    	//   b__30_0 = virtual GetReferences)
    	// Annotated disassembly: work/title-path/disas/rb_0xCDA3E0.txt
    	[Token(Token = "0x6000032")]
    	[Address(RVA = "0xCDA3E0", Offset = "0xCD8DE0", VA = "0x180CDA3E0")]
    	public void GetReferencesInEditor()
    	{
    		this.DoActionAsUndoable("Get References", delegate
    		{
    			GetReferences();
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDA4A0, 190 B -- this.DoActionAsUndoable("Purge References",
    	//   b__31_0 = virtual PurgeReferences)
    	// Annotated disassembly: work/title-path/disas/rb_0xCDA4A0.txt
    	[Token(Token = "0x6000033")]
    	[Address(RVA = "0xCDA4A0", Offset = "0xCD8EA0", VA = "0x180CDA4A0")]
    	public void PurgeReferencesInEditor()
    	{
    		this.DoActionAsUndoable("Purge References", delegate
    		{
    			PurgeReferences();
    		});
    	}

    	[Token(Token = "0x6000034")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public RebuildableButton()
    	{
    	}
    }
}
