#nullable enable annotations
﻿using System;
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
    		[Address(RVA = "0x841E80", Offset = "0x840880", VA = "0x180841E80")]
    		get
    		{
    			return default(ResponseKind);
    		}
    	}

    	[Token(Token = "0x17000007")]
    	protected RebuildableLabel Label
    	{
    		[Token(Token = "0x6000022")]
    		[Address(RVA = "0x44CE10", Offset = "0x44B810", VA = "0x18044CE10")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000008")]
    	protected virtual Vector2 DefaultSize
    	{
    		[Token(Token = "0x6000023")]
    		[Address(RVA = "0xCD8150", Offset = "0xCD6B50", VA = "0x180CD8150", Slot = "4")]
    		get
    		{
    			return default(Vector2);
    		}
    	}

    	[Token(Token = "0x6000024")]
    	[Address(RVA = "0x538020", Offset = "0x536A20", VA = "0x180538020")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x6000025")]
    	[Address(RVA = "0xCD8170", Offset = "0xCD6B70", VA = "0x180CD8170")]
    	public void Rebuild(ButtonData buttonData, bool atRuntime, [Optional] bool? controlsItsOwnSize, [Optional] LocaleKind? locale)
    	{
    	}

    	[Token(Token = "0x6000026")]
    	[Address(RVA = "0xCD8370", Offset = "0xCD6D70", VA = "0x180CD8370", Slot = "5")]
    	protected virtual void RebuildElements(ButtonData buttonData, bool atRuntime, bool controlsItsOwnSize, [Optional] LocaleKind? locale)
    	{
    	}

    	[Token(Token = "0x6000027")]
    	[Address(RVA = "0xCD8910", Offset = "0xCD7310", VA = "0x180CD8910")]
    	private void SetInternalData(ButtonData buttonData)
    	{
    	}

    	[Token(Token = "0x6000028")]
    	[Address(RVA = "0xCD8AB0", Offset = "0xCD74B0", VA = "0x180CD8AB0")]
    	private void RebuildLabel(LabelData labelData, bool atRuntime, [Optional] LocaleKind? locale)
    	{
    	}

    	[Token(Token = "0x6000029")]
    	[Address(RVA = "0xCD8BD0", Offset = "0xCD75D0", VA = "0x180CD8BD0")]
    	private void ModifyWidth(SelectableSizeData widthData)
    	{
    	}

    	[Token(Token = "0x600002A")]
    	[Address(RVA = "0xCD8D70", Offset = "0xCD7770", VA = "0x180CD8D70")]
    	private void ModifyHeight(SelectableSizeData heightData)
    	{
    	}

    	[Token(Token = "0x600002B")]
    	[Address(RVA = "0xCD8F10", Offset = "0xCD7910", VA = "0x180CD8F10")]
    	private float GetContentPreferredWidth()
    	{
    		return default(float);
    	}

    	[Token(Token = "0x600002C")]
    	[Address(RVA = "0xCD9140", Offset = "0xCD7B40", VA = "0x180CD9140")]
    	private float GetContentPreferredHeight()
    	{
    		return default(float);
    	}

    	[Token(Token = "0x600002D")]
    	[Address(RVA = "0xCD9370", Offset = "0xCD7D70", VA = "0x180CD9370")]
    	private void RebuildLayoutController(bool controlsItsOwnSize)
    	{
    	}

    	[Token(Token = "0x600002E")]
    	[Address(RVA = "0xCD9B30", Offset = "0xCD8530", VA = "0x180CD9B30")]
    	public IObservable<ResponseKind> OnClickAsObservable()
    	{
    		return null;
    	}

    	[Token(Token = "0x600002F")]
    	[Address(RVA = "0xCD9C20", Offset = "0xCD8620", VA = "0x180CD9C20")]
    	[AsyncStateMachine(typeof(_003COnClickAsync_003Ed__27))]
    	public UniTask<ResponseKind> OnClickAsync([Optional] CancellationToken ct)
    	{
    		return default(UniTask<ResponseKind>);
    	}

    	[Token(Token = "0x6000030")]
    	[Address(RVA = "0xCD9E10", Offset = "0xCD8810", VA = "0x180CD9E10", Slot = "6")]
    	protected virtual void GetReferences()
    	{
    	}

    	[Token(Token = "0x6000031")]
    	[Address(RVA = "0xCDA1A0", Offset = "0xCD8BA0", VA = "0x180CDA1A0", Slot = "7")]
    	protected virtual void PurgeReferences()
    	{
    	}

    	[Token(Token = "0x6000032")]
    	[Address(RVA = "0xCDA3E0", Offset = "0xCD8DE0", VA = "0x180CDA3E0")]
    	public void GetReferencesInEditor()
    	{
    	}

    	[Token(Token = "0x6000033")]
    	[Address(RVA = "0xCDA4A0", Offset = "0xCD8EA0", VA = "0x180CDA4A0")]
    	public void PurgeReferencesInEditor()
    	{
    	}

    	[Token(Token = "0x6000034")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public RebuildableButton()
    	{
    	}
    }
}
