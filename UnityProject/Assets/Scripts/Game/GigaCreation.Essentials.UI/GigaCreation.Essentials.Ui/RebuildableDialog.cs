#nullable enable annotations
﻿using GigaCreation.Tools.UndoExtensions;
using GigaCreation.Tools.Service;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Localization;
using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x200001D")]
    public class RebuildableDialog : MonoBehaviour
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000022")]
    	[CompilerGenerated]
    	private struct _003CRebuildAtRuntimeAsync_003Ed__27
    {
    		[Token(Token = "0x4000075")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000076")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<float> _003C_003Et__builder;

    		[Token(Token = "0x4000077")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public RebuildableDialog _003C_003E4__this;

    		[Token(Token = "0x4000078")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000079")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public DialogData dialogData;

    		[Token(Token = "0x400007A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x400007B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private float _003CtimeRequestedToPresent_003E5__3;

    		[Token(Token = "0x400007C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600008B")]
    		[Address(RVA = "0xCE0070", Offset = "0xCDEA70", VA = "0x180CE0070", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600008C")]
    		[Address(RVA = "0xCE0B40", Offset = "0xCDF540", VA = "0x180CE0B40", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400005C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("Assets")]
    	[SerializeField]
    	private RebuildableLabel _messageLabelPrefab;

    	[Token(Token = "0x400005D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private RebuildableButton _responseButtonPrefab;

    	[Token(Token = "0x400005E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[Header("References")]
    	[SerializeField]
    	private LayoutElement _viewLayoutElement;

    	[Token(Token = "0x400005F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private AutoLayoutSupporter _viewRootAutoLayoutSupporter;

    	[Token(Token = "0x4000060")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[SerializeField]
    	private TextMeshProUGUI _titleLabel;

    	[Token(Token = "0x4000061")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[SerializeField]
    	private FontMaterialChanger _titleFontMaterialChanger;

    	[Token(Token = "0x4000062")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[SerializeField]
    	private Transform _messageLabelRoot;

    	[Token(Token = "0x4000063")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	[SerializeField]
    	private Transform _responseButtonRoot;

    	[Token(Token = "0x4000064")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	[Header("Parameters")]
    	[SerializeField]
    	private DialogData _dialogData;

    	[Token(Token = "0x4000065")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	[Space]
    	[SerializeField]
    	private float _defaultWidth;

    	[Token(Token = "0x4000066")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x6C")]
    	[SerializeField]
    	private bool _cancelableByAction;

    	[Token(Token = "0x4000067")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	[SerializeField]
    	private List<RebuildableLabel> _messageLabels;

    	[Token(Token = "0x4000068")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    	[SerializeField]
    	private List<RebuildableButton> _responseButtons;

    	[Token(Token = "0x4000069")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
    	private ILocalizationProvider _localizationProvider;

    	[Token(Token = "0x400006A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
    	private CancellationTokenSource _rebuildCts;

    	[Token(Token = "0x17000010")]
    	protected bool CancelableByAction
    	{
    		[Token(Token = "0x600006B")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCDD4B0, 5 B -- `movzx eax, byte ptr [rcx + 0x6c]; ret`: the backing field _cancelableByAction (+0x6C in dump.cs)
    		[Address(RVA = "0xCDD4B0", Offset = "0xCDBEB0", VA = "0x180CDD4B0")]
    		get
    		{
    			return _cancelableByAction;
    		}
    	}

    	[Token(Token = "0x17000011")]
    	protected IReadOnlyList<RebuildableLabel> MessageLabels
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCDD4C0, 107 B -- lazy: _messageLabels (+0x70), else ExistingMessageLabels
    		// Annotated disassembly: work/title-path/disas/rd_0xCDD4C0.txt
    		[Token(Token = "0x600006C")]
    		[Address(RVA = "0xCDD4C0", Offset = "0xCDBEC0", VA = "0x180CDD4C0")]
    		get
    		{
    			if (_messageLabels == null)
    			{
    				_messageLabels = ExistingMessageLabels;
    			}
    			return _messageLabels;
    		}
    	}

    	[Token(Token = "0x17000012")]
    	protected IReadOnlyList<RebuildableButton> ResponseButtons
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCDD530, 107 B -- lazy: _responseButtons (+0x78), else ExistingResponseButtons
    		// Annotated disassembly: work/title-path/disas/rd_0xCDD530.txt
    		[Token(Token = "0x600006D")]
    		[Address(RVA = "0xCDD530", Offset = "0xCDBF30", VA = "0x180CDD530")]
    		get
    		{
    			if (_responseButtons == null)
    			{
    				_responseButtons = ExistingResponseButtons;
    			}
    			return _responseButtons;
    		}
    	}

    	[Token(Token = "0x17000013")]
    	private ILocalizationProvider LocalizationProvider
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCDD5A0, 202 B -- lazy: +0x80, else ServiceLocator.Get<ILocalizationManager>(false)
    		//   (xor ecx, ecx: this call site passes includeDerivedType = false)
    		// Annotated disassembly: work/title-path/disas/rd_0xCDD5A0.txt
    		[Token(Token = "0x600006E")]
    		[Address(RVA = "0xCDD5A0", Offset = "0xCDBFA0", VA = "0x180CDD5A0")]
    		get
    		{
    			if (_localizationProvider == null)
    			{
    				_localizationProvider = ServiceLocator.Get<ILocalizationManager>(false);
    			}
    			return _localizationProvider;
    		}
    	}

    	[Token(Token = "0x17000014")]
    	private List<RebuildableLabel> ExistingMessageLabels
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCDD670, 873 B -- _messageLabelRoot (+0x50).Cast<Transform>()
    		//   .Select(<>c.b__24_0 0xCDFD70: GetComponent<RebuildableLabel>()).Where(<>c.b__24_1 0xCDFDD0: != null).ToList()
    		// Annotated disassembly: work/title-path/disas/rd_0xCDD670.txt
    		[Token(Token = "0x600006F")]
    		[Address(RVA = "0xCDD670", Offset = "0xCDC070", VA = "0x180CDD670")]
    		get
    		{
    			return _messageLabelRoot.Cast<Transform>().Select((Transform trans) => trans.GetComponent<RebuildableLabel>())
    				.Where((RebuildableLabel label) => label != null)
    				.ToList();
    		}
    	}

    	[Token(Token = "0x17000015")]
    	private List<RebuildableButton> ExistingResponseButtons
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCDD9E0, 873 B -- _responseButtonRoot (+0x58).Cast<Transform>()
    		//   .Select(<>c.b__26_0 0xCDFEA0: GetComponent<RebuildableButton>()).Where(<>c.b__26_1 0xCDFF00: != null).ToList()
    		// Annotated disassembly: work/title-path/disas/rd_0xCDD9E0.txt
    		[Token(Token = "0x6000070")]
    		[Address(RVA = "0xCDD9E0", Offset = "0xCDC3E0", VA = "0x180CDD9E0")]
    		get
    		{
    			return _responseButtonRoot.Cast<Transform>().Select((Transform trans) => trans.GetComponent<RebuildableButton>())
    				.Where((RebuildableButton go) => go != null)
    				.ToList();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDDD50 (kickoff) / 0xCE0070 (<RebuildAtRuntimeAsync>d__27.MoveNext, 2754 B):
    	//   cancel and dispose a running _rebuildCts (+0x88) and make a new one; using a token source linked to it and ct
    	//   (<linkedCts>5__2): <timeRequestedToPresent>5__3 = Time.realtimeSinceStartup; a null dialogData falls back to
    	//   _dialogData (+0x60, 0x180CE03A3: the binary's own test); RebuildElements(dialogData, true,
    	//   LocalizationProvider.CurrentLocale); await UniTask.NextFrame(linked); await
    	//   _viewRootAutoLayoutSupporter.ExecuteRebuildingAsync(linked); dispose and clear _rebuildCts; the result is the
    	//   elapsed realtime (icall UnityEngine.Time::get_realtimeSinceStartup, 0x180CE08B3). Body in RebuildAtRuntimeAsyncCore.
    	// Annotated disassembly: work/title-path/disas/rd_0xCDDD50.txt, rd_0xCE0070.txt
    	[Token(Token = "0x6000071")]
    	[Address(RVA = "0xCDDD50", Offset = "0xCDC750", VA = "0x180CDDD50")]
    	[AsyncStateMachine(typeof(_003CRebuildAtRuntimeAsync_003Ed__27))]
    	protected UniTask<float> RebuildAtRuntimeAsync(DialogData dialogData, [Optional] CancellationToken ct)
    	{
    		return RebuildAtRuntimeAsyncCore(dialogData, ct);
    	}

    	private async UniTask<float> RebuildAtRuntimeAsyncCore(DialogData dialogData, CancellationToken ct)
    	{
    		if (_rebuildCts != null)
    		{
    			_rebuildCts.Cancel();
    			_rebuildCts.Dispose();
    		}
    		_rebuildCts = new CancellationTokenSource();
    		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_rebuildCts.Token, ct);
    		float timeRequestedToPresent = Time.realtimeSinceStartup;
    		// 0x180CE03A3: the shipped code substitutes the serialized dialog data for a null argument
    		if (dialogData == null)
    		{
    			dialogData = _dialogData;
    		}
    		RebuildElements(dialogData, true, LocalizationProvider.CurrentLocale);
    		await UniTask.NextFrame(linkedCts.Token);
    		await _viewRootAutoLayoutSupporter.ExecuteRebuildingAsync(linkedCts.Token);
    		_rebuildCts.Dispose();
    		_rebuildCts = null;
    		return Time.realtimeSinceStartup - timeRequestedToPresent;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDDFA0, 102 B -- RebuildElements(_dialogData, false, null), then
    	//   _viewRootAutoLayoutSupporter.ExecuteRebuildingAsync(its destroyCancellationToken).Forget()
    	// Annotated disassembly: work/title-path/disas/rd_0xCDDFA0.txt
    	[Token(Token = "0x6000072")]
    	[Address(RVA = "0xCDDFA0", Offset = "0xCDC9A0", VA = "0x180CDDFA0")]
    	public void RebuildInEditor()
    	{
    		RebuildElements(_dialogData, false);
    		_viewRootAutoLayoutSupporter.ExecuteRebuildingAsync(_viewRootAutoLayoutSupporter.destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDE010, 1152 B -- SetInternalData (inlined); ModifyTitleLabel(TitleText);
    	//   ModifyMinWidth(MinWidth ?? _defaultWidth (+0x68), inlined); RebuildMessageLabels; RebuildResponseButtons
    	// Annotated disassembly: work/title-path/disas/rd_0xCDE010.txt
    	[Token(Token = "0x6000073")]
    	[Address(RVA = "0xCDE010", Offset = "0xCDCA10", VA = "0x180CDE010")]
    	private void RebuildElements(DialogData dialogData, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    		SetInternalData(dialogData);
    		ModifyTitleLabel(dialogData.TitleText, atRuntime, locale);
    		float? minWidth = dialogData.MinWidth;
    		ModifyMinWidth(minWidth ?? _defaultWidth);
    		RebuildMessageLabels(dialogData.MessageLabelsData, atRuntime, locale);
    		RebuildResponseButtons(dialogData.ButtonsData, atRuntime, locale);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDE4A0, 411 B / closure 0xCDFFD0 -- this.DoActionAsUndoable("Set Internal
    	//   Data", _ => _cancelableByAction (+0x6C) = dialogData.CancelableByAction (+0x20))
    	// Annotated disassembly: work/title-path/disas/rd_0xCDE4A0.txt, rd_0xCDFFD0.txt
    	[Token(Token = "0x6000074")]
    	[Address(RVA = "0xCDE4A0", Offset = "0xCDCEA0", VA = "0x180CDE4A0")]
    	private void SetInternalData(DialogData dialogData)
    	{
    		this.DoActionAsUndoable("Set Internal Data", delegate
    		{
    			_cancelableByAction = dialogData.CancelableByAction;
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDE640, 727 B / closure 0xCE0000 -- nothing without a live _titleLabel
    	//   (+0x40; 0x180CDE7C5, the binary's own test); at runtime with localizableText.ChangeFont: the title font becomes
    	//   LocalizationProvider.CurrentFontAsset and a live _titleFontMaterialChanger (+0x48, implicit bool) fires
    	//   ChangeFontMaterialAsync(_titleLabel).Forget(); then _titleLabel.DoActionAsUndoable("Modify Title Label",
    	//   label => label.SetText(localizableText.GetText(locale)))
    	// Annotated disassembly: work/title-path/disas/rd_0xCDE640.txt, rd_0xCE0000.txt
    	[Token(Token = "0x6000075")]
    	[Address(RVA = "0xCDE640", Offset = "0xCDD040", VA = "0x180CDE640")]
    	private void ModifyTitleLabel(LocalizableText localizableText, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    		// 0x180CDE7C5: the shipped code tests the title label before touching it
    		if (_titleLabel == null)
    		{
    			return;
    		}
    		if (atRuntime && localizableText.ChangeFont)
    		{
    			_titleLabel.font = LocalizationProvider.CurrentFontAsset;
    			if ((bool)_titleFontMaterialChanger)
    			{
    				_titleFontMaterialChanger.ChangeFontMaterialAsync(_titleLabel).Forget();
    			}
    		}
    		_titleLabel.DoActionAsUndoable("Modify Title Label", delegate(TextMeshProUGUI label)
    		{
    			label.SetText(localizableText.GetText(locale));
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDE920, 274 B / closure 0xCE0040 -- unless Mathf.Approximately(minWidth, 0)
    	//   (0x331670): _viewLayoutElement (+0x30).DoActionAsUndoable("Modify Min Width", element => element.minWidth =
    	//   minWidth (LayoutElement slot 32))
    	// Annotated disassembly: work/title-path/disas/rd_0xCDE920.txt, rd_0xCE0040.txt
    	[Token(Token = "0x6000076")]
    	[Address(RVA = "0xCDE920", Offset = "0xCDD320", VA = "0x180CDE920")]
    	private void ModifyMinWidth(float minWidth)
    	{
    		if (Mathf.Approximately(minWidth, 0f))
    		{
    			return;
    		}
    		_viewLayoutElement.DoActionAsUndoable("Modify Min Width", delegate(LayoutElement element)
    		{
    			element.minWidth = minWidth;
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDEA40, 1178 B -- _messageLabels = ExistingMessageLabels; from the last
    	//   index down to labelsData.Count the surplus label's GameObject is destroyed (DestroyAsUndoable inlined) and
    	//   removed; then for each label data: instantiate _messageLabelPrefab under _messageLabelRoot when the list is
    	//   short, and Rebuild(labelsData[i], atRuntime, locale)
    	// Annotated disassembly: work/title-path/disas/rd_0xCDEA40.txt
    	[Token(Token = "0x6000077")]
    	[Address(RVA = "0xCDEA40", Offset = "0xCDD440", VA = "0x180CDEA40")]
    	private void RebuildMessageLabels(IReadOnlyList<LabelData> labelsData, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    		_messageLabels = ExistingMessageLabels;
    		for (int i = _messageLabels.Count - 1; i >= labelsData.Count; i--)
    		{
    			_messageLabels[i].gameObject.DestroyAsUndoable();
    			_messageLabels.RemoveAt(i);
    		}
    		for (int j = 0; j < labelsData.Count; j++)
    		{
    			if (j >= _messageLabels.Count)
    			{
    				_messageLabels.Add(Object.Instantiate(_messageLabelPrefab, _messageLabelRoot));
    			}
    			_messageLabels[j].Rebuild(labelsData[j], atRuntime, locale);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDEEE0, 1348 B -- as RebuildMessageLabels for the response buttons; a
    	//   surplus button's GameObject is destroyed only while it is alive (0x180CDF1C3, the binary's own test); each
    	//   button is rebuilt with controlsItsOwnSize = false
    	// Annotated disassembly: work/title-path/disas/rd_0xCDEEE0.txt
    	[Token(Token = "0x6000078")]
    	[Address(RVA = "0xCDEEE0", Offset = "0xCDD8E0", VA = "0x180CDEEE0")]
    	private void RebuildResponseButtons(IReadOnlyList<ButtonData> buttonsData, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    		_responseButtons = ExistingResponseButtons;
    		for (int i = _responseButtons.Count - 1; i >= buttonsData.Count; i--)
    		{
    			// 0x180CDF1C3: the shipped code tests the button's GameObject before destroying it
    			if (_responseButtons[i].gameObject != null)
    			{
    				_responseButtons[i].gameObject.DestroyAsUndoable();
    			}
    			_responseButtons.RemoveAt(i);
    		}
    		for (int j = 0; j < buttonsData.Count; j++)
    		{
    			if (j >= _responseButtons.Count)
    			{
    				_responseButtons.Add(Object.Instantiate(_responseButtonPrefab, _responseButtonRoot));
    			}
    			_responseButtons[j].Rebuild(buttonsData[j], atRuntime, false, locale);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDF430, 1738 B -- cancel, dispose and clear a running _rebuildCts; empty a
    	//   live title; destroy every existing message label's and response button's GameObject; clear both lists; then
    	//   _viewRootAutoLayoutSupporter.ExecuteRebuildingAsync(its destroyCancellationToken).Forget()
    	// Annotated disassembly: work/title-path/disas/rd_0xCDF430.txt
    	[Token(Token = "0x6000079")]
    	[Address(RVA = "0xCDF430", Offset = "0xCDDE30", VA = "0x180CDF430")]
    	public void Clear()
    	{
    		if (_rebuildCts != null)
    		{
    			_rebuildCts.Cancel();
    			_rebuildCts.Dispose();
    			_rebuildCts = null;
    		}
    		// 0x180CDF644: the shipped code tests the title label before emptying it
    		if (_titleLabel != null)
    		{
    			_titleLabel.SetText(string.Empty);
    		}
    		foreach (RebuildableLabel existingMessageLabel in ExistingMessageLabels)
    		{
    			existingMessageLabel.gameObject.DestroyAsUndoable();
    		}
    		foreach (RebuildableButton existingResponseButton in ExistingResponseButtons)
    		{
    			existingResponseButton.gameObject.DestroyAsUndoable();
    		}
    		_messageLabels = null;
    		_responseButtons = null;
    		_viewRootAutoLayoutSupporter.ExecuteRebuildingAsync(_viewRootAutoLayoutSupporter.destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDFB00, 190 B / b__36_0 0xCDFC80 -- this.DoActionAsUndoable("Store Dialog
    	//   Parameters as Default", _ => _defaultWidth (+0x68) = _viewLayoutElement.minWidth (slot 31))
    	// Annotated disassembly: work/title-path/disas/rd_0xCDFB00.txt, rd_0xCDFC80.txt
    	[Token(Token = "0x600007A")]
    	[Address(RVA = "0xCDFB00", Offset = "0xCDE500", VA = "0x180CDFB00")]
    	public void StoreDialogParametersAsDefault()
    	{
    		this.DoActionAsUndoable("Store Dialog Parameters as Default", delegate
    		{
    			_defaultWidth = _viewLayoutElement.minWidth;
    		});
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDFBC0, 190 B / b__37_0 0xCDFCC0 -- this.DoActionAsUndoable("Reset Default
    	//   Parameters", _ => _defaultWidth = 0)
    	// Annotated disassembly: work/title-path/disas/rd_0xCDFBC0.txt, rd_0xCDFCC0.txt
    	[Token(Token = "0x600007B")]
    	[Address(RVA = "0xCDFBC0", Offset = "0xCDE5C0", VA = "0x180CDFBC0")]
    	public void ResetDefaultParameters()
    	{
    		this.DoActionAsUndoable("Reset Default Parameters", delegate
    		{
    			_defaultWidth = 0f;
    		});
    	}

    	[Token(Token = "0x600007C")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public RebuildableDialog()
    	{
    	}
    }
}
