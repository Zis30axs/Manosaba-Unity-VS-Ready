#nullable enable annotations
﻿using System.Collections.Generic;
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
    		[Address(RVA = "0xCDD4B0", Offset = "0xCDBEB0", VA = "0x180CDD4B0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x17000011")]
    	protected IReadOnlyList<RebuildableLabel> MessageLabels
    	{
    		[Token(Token = "0x600006C")]
    		[Address(RVA = "0xCDD4C0", Offset = "0xCDBEC0", VA = "0x180CDD4C0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000012")]
    	protected IReadOnlyList<RebuildableButton> ResponseButtons
    	{
    		[Token(Token = "0x600006D")]
    		[Address(RVA = "0xCDD530", Offset = "0xCDBF30", VA = "0x180CDD530")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000013")]
    	private ILocalizationProvider LocalizationProvider
    	{
    		[Token(Token = "0x600006E")]
    		[Address(RVA = "0xCDD5A0", Offset = "0xCDBFA0", VA = "0x180CDD5A0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000014")]
    	private List<RebuildableLabel> ExistingMessageLabels
    	{
    		[Token(Token = "0x600006F")]
    		[Address(RVA = "0xCDD670", Offset = "0xCDC070", VA = "0x180CDD670")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000015")]
    	private List<RebuildableButton> ExistingResponseButtons
    	{
    		[Token(Token = "0x6000070")]
    		[Address(RVA = "0xCDD9E0", Offset = "0xCDC3E0", VA = "0x180CDD9E0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000071")]
    	[Address(RVA = "0xCDDD50", Offset = "0xCDC750", VA = "0x180CDDD50")]
    	[AsyncStateMachine(typeof(_003CRebuildAtRuntimeAsync_003Ed__27))]
    	protected UniTask<float> RebuildAtRuntimeAsync(DialogData dialogData, [Optional] CancellationToken ct)
    	{
    		return default(UniTask<float>);
    	}

    	[Token(Token = "0x6000072")]
    	[Address(RVA = "0xCDDFA0", Offset = "0xCDC9A0", VA = "0x180CDDFA0")]
    	public void RebuildInEditor()
    	{
    	}

    	[Token(Token = "0x6000073")]
    	[Address(RVA = "0xCDE010", Offset = "0xCDCA10", VA = "0x180CDE010")]
    	private void RebuildElements(DialogData dialogData, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    	}

    	[Token(Token = "0x6000074")]
    	[Address(RVA = "0xCDE4A0", Offset = "0xCDCEA0", VA = "0x180CDE4A0")]
    	private void SetInternalData(DialogData dialogData)
    	{
    	}

    	[Token(Token = "0x6000075")]
    	[Address(RVA = "0xCDE640", Offset = "0xCDD040", VA = "0x180CDE640")]
    	private void ModifyTitleLabel(LocalizableText localizableText, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    	}

    	[Token(Token = "0x6000076")]
    	[Address(RVA = "0xCDE920", Offset = "0xCDD320", VA = "0x180CDE920")]
    	private void ModifyMinWidth(float minWidth)
    	{
    	}

    	[Token(Token = "0x6000077")]
    	[Address(RVA = "0xCDEA40", Offset = "0xCDD440", VA = "0x180CDEA40")]
    	private void RebuildMessageLabels(IReadOnlyList<LabelData> labelsData, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    	}

    	[Token(Token = "0x6000078")]
    	[Address(RVA = "0xCDEEE0", Offset = "0xCDD8E0", VA = "0x180CDEEE0")]
    	private void RebuildResponseButtons(IReadOnlyList<ButtonData> buttonsData, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    	}

    	[Token(Token = "0x6000079")]
    	[Address(RVA = "0xCDF430", Offset = "0xCDDE30", VA = "0x180CDF430")]
    	public void Clear()
    	{
    	}

    	[Token(Token = "0x600007A")]
    	[Address(RVA = "0xCDFB00", Offset = "0xCDE500", VA = "0x180CDFB00")]
    	public void StoreDialogParametersAsDefault()
    	{
    	}

    	[Token(Token = "0x600007B")]
    	[Address(RVA = "0xCDFBC0", Offset = "0xCDE5C0", VA = "0x180CDFBC0")]
    	public void ResetDefaultParameters()
    	{
    	}

    	[Token(Token = "0x600007C")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public RebuildableDialog()
    	{
    	}
    }
}
