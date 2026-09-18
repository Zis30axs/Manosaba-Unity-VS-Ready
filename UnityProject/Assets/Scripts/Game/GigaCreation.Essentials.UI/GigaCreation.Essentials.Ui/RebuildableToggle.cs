#nullable enable annotations
﻿using System;
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
    		[Token(Token = "0x6000160")]
    		[Address(RVA = "0xCECBC0", Offset = "0xCEB5C0", VA = "0x180CECBC0")]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000161")]
    		[Address(RVA = "0xCECBE0", Offset = "0xCEB5E0", VA = "0x180CECBE0")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700003F")]
    	private Vector2 DefaultSize
    	{
    		[Token(Token = "0x6000162")]
    		[Address(RVA = "0xCECC10", Offset = "0xCEB610", VA = "0x180CECC10")]
    		get
    		{
    			return default(Vector2);
    		}
    	}

    	[Token(Token = "0x6000163")]
    	[Address(RVA = "0xCECC30", Offset = "0xCEB630", VA = "0x180CECC30")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x6000164")]
    	[Address(RVA = "0xCECC40", Offset = "0xCEB640", VA = "0x180CECC40")]
    	public void Rebuild(bool atRuntime, [Optional] LocaleKind? locale)
    	{
    	}

    	[Token(Token = "0x6000165")]
    	[Address(RVA = "0xCECC50", Offset = "0xCEB650", VA = "0x180CECC50")]
    	public void RebuildInEditor()
    	{
    	}

    	[Token(Token = "0x6000166")]
    	[Address(RVA = "0xCECCB0", Offset = "0xCEB6B0", VA = "0x180CECCB0")]
    	private void RebuildElements(bool atRuntime, [Optional] LocaleKind? locale)
    	{
    	}

    	[Token(Token = "0x6000167")]
    	[Address(RVA = "0xCECD40", Offset = "0xCEB740", VA = "0x180CECD40")]
    	private void RebuildContentSizeFitter()
    	{
    	}

    	[Token(Token = "0x6000168")]
    	[Address(RVA = "0xCED370", Offset = "0xCEBD70", VA = "0x180CED370")]
    	private void RebuildLabel(LabelData labelData, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    	}

    	[Token(Token = "0x6000169")]
    	[Address(RVA = "0xCED580", Offset = "0xCEBF80", VA = "0x180CED580")]
    	private void DestroyHoldenLabel()
    	{
    	}

    	[Token(Token = "0x600016A")]
    	[Address(RVA = "0xCED700", Offset = "0xCEC100", VA = "0x180CED700")]
    	private bool HoldExistingLabel()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600016B")]
    	[Address(RVA = "0xCEDCB0", Offset = "0xCEC6B0", VA = "0x180CEDCB0")]
    	private void GenerateAndHoldLabel()
    	{
    	}

    	[Token(Token = "0x600016C")]
    	[Address(RVA = "0xCEDF20", Offset = "0xCEC920", VA = "0x180CEDF20")]
    	private void RebuildLabelElements(LabelData labelData, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    	}

    	[Token(Token = "0x600016D")]
    	[Address(RVA = "0xCEDF50", Offset = "0xCEC950", VA = "0x180CEDF50")]
    	private void UpdateWidth(SelectableSizeData widthData)
    	{
    	}

    	[Token(Token = "0x600016E")]
    	[Address(RVA = "0xCEE1A0", Offset = "0xCECBA0", VA = "0x180CEE1A0")]
    	private void UpdateHeight(SelectableSizeData heightData)
    	{
    	}

    	[Token(Token = "0x600016F")]
    	[Address(RVA = "0xCEE3F0", Offset = "0xCECDF0", VA = "0x180CEE3F0")]
    	private float GetContentPreferredWidth()
    	{
    		return default(float);
    	}

    	[Token(Token = "0x6000170")]
    	[Address(RVA = "0xCEE620", Offset = "0xCED020", VA = "0x180CEE620")]
    	private float GetContentPreferredHeight()
    	{
    		return default(float);
    	}

    	[Token(Token = "0x6000171")]
    	[Address(RVA = "0xCEE850", Offset = "0xCED250", VA = "0x180CEE850")]
    	public IObservable<bool> OnValueChangedAsObservable()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000172")]
    	[Address(RVA = "0xCEE860", Offset = "0xCED260", VA = "0x180CEE860")]
    	[AsyncStateMachine(typeof(_003COnValueChangedAsync_003Ed__31))]
    	public UniTask OnValueChangedAsync([Optional] CancellationToken ct)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000173")]
    	[Address(RVA = "0xCEEA00", Offset = "0xCED400", VA = "0x180CEEA00")]
    	private void GetReferences()
    	{
    	}

    	[Token(Token = "0x6000174")]
    	[Address(RVA = "0xCEED10", Offset = "0xCED710", VA = "0x180CEED10")]
    	private void PurgeReferences()
    	{
    	}

    	[Token(Token = "0x6000175")]
    	[Address(RVA = "0xCEEF50", Offset = "0xCED950", VA = "0x180CEEF50")]
    	public void GetReferencesInEditor()
    	{
    	}

    	[Token(Token = "0x6000176")]
    	[Address(RVA = "0xCEF020", Offset = "0xCEDA20", VA = "0x180CEF020")]
    	public void PurgeReferencesInEditor()
    	{
    	}

    	[Token(Token = "0x6000177")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public RebuildableToggle()
    	{
    	}
    }
}
