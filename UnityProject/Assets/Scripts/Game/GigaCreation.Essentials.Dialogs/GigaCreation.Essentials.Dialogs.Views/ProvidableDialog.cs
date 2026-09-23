using UnityEngine.InputSystem;
using LitMotion.Extensions;
using GigaCreation.Tools.Service;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Dialogs.Models;
using GigaCreation.Essentials.Input;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using LitMotion;
using UnityEngine;

namespace GigaCreation.Essentials.Dialogs.Views
{
    [Token(Token = "0x2000007")]
    [RequireComponent(typeof(CanvasGroup), typeof(GeneralScreen))]
    public class ProvidableDialog : RebuildableDialog, IDisposable, IUniTaskAsyncDisposable
    {
    	[Token(Token = "0x2000009")]
    	[CompilerGenerated]
    	private sealed class _003C_003Ec__DisplayClass26_0
    	{
    		[Token(Token = "0x4000011")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public CancellationTokenSource linkedCts;

    		[Token(Token = "0x600001C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public _003C_003Ec__DisplayClass26_0()
    		{
    		}

    		// RECONSTRUCTED | Confidence: A | RVA: 0xCC9AC0 -- button.OnClickAsync(linkedCts (+0x10).Token)
    		// Annotated disassembly: work/title-path/disas/pd_0xCC9AC0.txt
    		[Token(Token = "0x600001D")]
    		[Address(RVA = "0xCC9AC0", Offset = "0xCC84C0", VA = "0x180CC9AC0")]
    		internal UniTask<ResponseKind> _003COnResponseAsync_003Eb__0(RebuildableButton button)
    		{
    			return button.OnClickAsync(linkedCts.Token);
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000A")]
    	[CompilerGenerated]
    	private struct _003CConfirmAsync_003Ed__19
    {
    		[Token(Token = "0x4000012")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000013")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<ResponseKind> _003C_003Et__builder;

    		[Token(Token = "0x4000014")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public ProvidableDialog _003C_003E4__this;

    		[Token(Token = "0x4000015")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public DialogData dialogData;

    		[Token(Token = "0x4000016")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public TextInsertData textInsertData;

    		[Token(Token = "0x4000017")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000018")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000019")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask<ResponseKind>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600001E")]
    		[Address(RVA = "0xCC9B20", Offset = "0xCC8520", VA = "0x180CC9B20", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600001F")]
    		[Address(RVA = "0xCCA120", Offset = "0xCC8B20", VA = "0x180CCA120", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000B")]
    	[CompilerGenerated]
    	private struct _003CDismissAndReleaseAsync_003Ed__27
    {
    		[Token(Token = "0x400001A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400001B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400001C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ProvidableDialog _003C_003E4__this;

    		[Token(Token = "0x400001D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x400001E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000020")]
    		[Address(RVA = "0xCCA150", Offset = "0xCC8B50", VA = "0x180CCA150", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000021")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000C")]
    	[CompilerGenerated]
    	private struct _003CDismissAsync_003Ed__23
    {
    		[Token(Token = "0x400001F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000020")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000021")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ProvidableDialog _003C_003E4__this;

    		[Token(Token = "0x4000022")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000023")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x4000024")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000022")]
    		[Address(RVA = "0xCCA5B0", Offset = "0xCC8FB0", VA = "0x180CCA5B0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000023")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000D")]
    	[CompilerGenerated]
    	private struct _003CDisposeAsync_003Ed__18
    {
    		[Token(Token = "0x4000025")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000026")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000027")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ProvidableDialog _003C_003E4__this;

    		[Token(Token = "0x4000028")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000024")]
    		[Address(RVA = "0xCCAE70", Offset = "0xCC9870", VA = "0x180CCAE70", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000025")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000E")]
    	[CompilerGenerated]
    	private struct _003COnResponseAsync_003Ed__26
    {
    		[Token(Token = "0x4000029")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400002A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<ResponseKind> _003C_003Et__builder;

    		[Token(Token = "0x400002B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x400002C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public ProvidableDialog _003C_003E4__this;

    		[Token(Token = "0x400002D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private _003C_003Ec__DisplayClass26_0 _003C_003E8__1;

    		[Token(Token = "0x400002E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<(int winArgumentIndex, ResponseKind result)>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000026")]
    		[Address(RVA = "0xCCB140", Offset = "0xCC9B40", VA = "0x180CCB140", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000027")]
    		[Address(RVA = "0xCCBBA0", Offset = "0xCCA5A0", VA = "0x180CCBBA0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000F")]
    	[CompilerGenerated]
    	private struct _003CPlayDismissAnimationAsync_003Ed__24
    {
    		[Token(Token = "0x400002F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000030")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000031")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ProvidableDialog _003C_003E4__this;

    		[Token(Token = "0x4000032")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000033")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000028")]
    		[Address(RVA = "0xCCBBD0", Offset = "0xCCA5D0", VA = "0x180CCBBD0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000029")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000010")]
    	[CompilerGenerated]
    	private struct _003CPlayPresentAnimationAsync_003Ed__22
    {
    		[Token(Token = "0x4000034")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000035")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000036")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ProvidableDialog _003C_003E4__this;

    		[Token(Token = "0x4000037")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000038")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public float timePosition;

    		[Token(Token = "0x4000039")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private IManualMotionSkipper _003CmanualMotionSkipper_003E5__2;

    		[Token(Token = "0x400003A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600002A")]
    		[Address(RVA = "0xCCC020", Offset = "0xCCAA20", VA = "0x180CCC020", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600002B")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000011")]
    	[CompilerGenerated]
    	private struct _003CRebuildAndPresentAsync_003Ed__20
    {
    		[Token(Token = "0x400003B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400003C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400003D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ProvidableDialog _003C_003E4__this;

    		[Token(Token = "0x400003E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x400003F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public DialogData dialogData;

    		[Token(Token = "0x4000040")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public TextInsertData textInsertData;

    		[Token(Token = "0x4000041")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x4000042")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask<float>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000043")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600002C")]
    		[Address(RVA = "0xCCC8E0", Offset = "0xCCB2E0", VA = "0x180CCC8E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600002D")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000012")]
    	[CompilerGenerated]
    	private struct _003CScaleAsync_003Ed__25
    {
    		[Token(Token = "0x4000044")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000045")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000046")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public float duration;

    		[Token(Token = "0x4000047")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
    		public float? from;

    		[Token(Token = "0x4000048")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public ProvidableDialog _003C_003E4__this;

    		[Token(Token = "0x4000049")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public float to;

    		[Token(Token = "0x400004A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
    		public Ease ease;

    		[Token(Token = "0x400004B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public bool updateMotionManually;

    		[Token(Token = "0x400004C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		public CancellationToken ct;

    		[Token(Token = "0x400004D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600002E")]
    		[Address(RVA = "0xCCD400", Offset = "0xCCBE00", VA = "0x180CCD400", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600002F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000008")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
    	[Header("References for Providable")]
    	[SerializeField]
    	private GeneralScreen _generalScreen;

    	[Token(Token = "0x4000009")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
    	[SerializeField]
    	private Transform _viewRootTransform;

    	[Token(Token = "0x400000A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
    	[Header("Parameters for Providable")]
    	[SerializeField]
    	private float _scaleUpDuration;

    	[Token(Token = "0x400000B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA4")]
    	[SerializeField]
    	private float _scaleDownDuration;

    	[Token(Token = "0x400000C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
    	private IInputManager _inputManager;

    	[Token(Token = "0x400000D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
    	private CancellationTokenSource _presentCts;

    	[Token(Token = "0x400000E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
    	private CancellationTokenSource _dismissCts;

    	[Token(Token = "0x17000001")]
    	public bool InUse
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xC54930 (shared 1-byte getter) / 0xCC8360 (set): auto-property
    		//   accessors over <InUse>k__BackingField (+0xC0)
    		[Token(Token = "0x6000007")]
    		[Address(RVA = "0xC54930", Offset = "0xC53330", VA = "0x180C54930")]
    		[CompilerGenerated]
    		get;
    		[Token(Token = "0x6000008")]
    		[Address(RVA = "0xCC8360", Offset = "0xCC6D60", VA = "0x180CC8360")]
    		[CompilerGenerated]
    		set;
    	}

    	[Token(Token = "0x17000002")]
    	private IInputManager InputManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCC8370 -- lazy: _inputManager (+0xA8), else
    		//   ServiceLocator.Get<IInputManager>(true) (mov cl, 1) stored there
    		// Annotated disassembly: work/title-path/disas/pd_0xCC8370.txt
    		[Token(Token = "0x6000009")]
    		[Address(RVA = "0xCC8370", Offset = "0xCC6D70", VA = "0x180CC8370")]
    		get
    		{
    			if (_inputManager == null)
    			{
    				_inputManager = ServiceLocator.Get<IInputManager>(true);
    			}
    			return _inputManager;
    		}
    	}

    	[Token(Token = "0x17000003")]
    	public int SortingOrder
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCC8440 -- _generalScreen (+0x90).SortingOrder (GeneralScreen getter
    		//   inlined: its _canvas (+0x20).sortingOrder icall)
    		// Annotated disassembly: work/title-path/disas/pd_0xCC8440.txt
    		[Token(Token = "0x600000A")]
    		[Address(RVA = "0xCC8440", Offset = "0xCC6E40", VA = "0x180CC8440")]
    		get
    		{
    			return _generalScreen.SortingOrder;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCC84F0 -- _generalScreen.SortingOrder = value (inlined: tail jump
    		//   to Canvas.set_sortingOrder on its _canvas)
    		// Annotated disassembly: work/title-path/disas/pd_0xCC84F0.txt
    		[Token(Token = "0x600000B")]
    		[Address(RVA = "0xCC84F0", Offset = "0xCC6EF0", VA = "0x180CC84F0")]
    		set
    		{
    			_generalScreen.SortingOrder = value;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCC8520 -- GetComponent<GeneralScreen>() -> _generalScreen (+0x90)
    	// Annotated disassembly: work/title-path/disas/pd_0xCC8520.txt
    	[Token(Token = "0x600000C")]
    	[Address(RVA = "0xCC8520", Offset = "0xCC6F20", VA = "0x180CC8520")]
    	private void Reset()
    	{
    		_generalScreen = GetComponent<GeneralScreen>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCC85C0 -- DismissAndReleaseAsync(destroyCancellationToken).Forget()
    	// Annotated disassembly: work/title-path/disas/pd_0xCC85C0.txt
    	[Token(Token = "0x600000D")]
    	[Address(RVA = "0xCC85C0", Offset = "0xCC6FC0", VA = "0x180CC85C0", Slot = "4")]
    	public void Dispose()
    	{
    		DismissAndReleaseAsync(base.destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCC8600 (kickoff) / 0xCCAE70 (<DisposeAsync>d__18.MoveNext): await
    	//   DismissAndReleaseAsync(destroyCancellationToken). Body in DisposeAsyncCore.
    	// Annotated disassembly: work/title-path/disas/pd_0xCCAE70.txt
    	[Token(Token = "0x600000E")]
    	[Address(RVA = "0xCC8600", Offset = "0xCC7000", VA = "0x180CC8600", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CDisposeAsync_003Ed__18))]
    	public UniTask DisposeAsync()
    	{
    		return DisposeAsyncCore();
    	}

    	private async UniTask DisposeAsyncCore()
    	{
    		await DismissAndReleaseAsync(base.destroyCancellationToken);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCC8740 (kickoff) / 0xCC9B20 (<ConfirmAsync>d__19.MoveNext): await
    	//   RebuildAndPresentAsync(dialogData, textInsertData, ct) (<>u__1), then the result of await OnResponseAsync(ct)
    	//   (<>u__2). Body in ConfirmAsyncCore.
    	// Annotated disassembly: work/title-path/disas/pd_0xCC9B20.txt
    	[Token(Token = "0x600000F")]
    	[Address(RVA = "0xCC8740", Offset = "0xCC7140", VA = "0x180CC8740")]
    	[AsyncStateMachine(typeof(_003CConfirmAsync_003Ed__19))]
    	public UniTask<ResponseKind> ConfirmAsync(DialogData dialogData, TextInsertData textInsertData = default, CancellationToken ct = default)
    	{
    		return ConfirmAsyncCore(dialogData, textInsertData, ct);
    	}

    	private async UniTask<ResponseKind> ConfirmAsyncCore(DialogData dialogData, TextInsertData textInsertData, CancellationToken ct)
    	{
    		await RebuildAndPresentAsync(dialogData, textInsertData, ct);
    		return await OnResponseAsync(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCC89E0 (kickoff) / 0xCCC8E0 (<RebuildAndPresentAsync>d__20.MoveNext, 2816 B):
    	//   nothing while a presentation is running (_presentCts (+0xB0) set, 0x180CCC9E3); a running dismissal
    	//   (_dismissCts +0xB8) is cancelled, disposed and cleared; _presentCts = new CTS; in a using over a token source
    	//   linked to it and ct (<linkedCts>5__2): _generalScreen.BeginToPresent(); RebuildAtRuntimeAsync(dialogData,
    	//   linked) is started, and a non-null textInsertData (0x180CCCC5A) is inserted before its float result is
    	//   awaited (<>u__1); await PlayPresentAnimationAsync(that time, linked) (<>u__2); _generalScreen.EndToPresent();
    	//   _presentCts disposed and cleared. Body in RebuildAndPresentAsyncCore.
    	// Annotated disassembly: work/title-path/disas/pd_0xCCC8E0.txt
    	[Token(Token = "0x6000010")]
    	[Address(RVA = "0xCC89E0", Offset = "0xCC73E0", VA = "0x180CC89E0")]
    	[AsyncStateMachine(typeof(_003CRebuildAndPresentAsync_003Ed__20))]
    	public UniTask RebuildAndPresentAsync(DialogData dialogData, TextInsertData textInsertData = default, CancellationToken ct = default)
    	{
    		return RebuildAndPresentAsyncCore(dialogData, textInsertData, ct);
    	}

    	private async UniTask RebuildAndPresentAsyncCore(DialogData dialogData, TextInsertData textInsertData, CancellationToken ct)
    	{
    		// 0x180CCC9E3: the shipped code ignores the request while a presentation is running
    		if (_presentCts != null)
    		{
    			return;
    		}
    		if (_dismissCts != null)
    		{
    			_dismissCts.Cancel();
    			_dismissCts.Dispose();
    			_dismissCts = null;
    		}
    		_presentCts = new CancellationTokenSource();
    		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_presentCts.Token, ct);
    		_generalScreen.BeginToPresent();
    		UniTask<float> rebuildTask = RebuildAtRuntimeAsync(dialogData, linkedCts.Token);
    		// 0x180CCCC5A: the shipped code tests the insert data before inserting it
    		if (textInsertData != null)
    		{
    			InsertTexts(textInsertData);
    		}
    		float timePosition = await rebuildTask;
    		await PlayPresentAnimationAsync(timePosition, linkedCts.Token);
    		_generalScreen.EndToPresent();
    		_presentCts.Dispose();
    		_presentCts = null;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCC8C50 / closure <>c__DisplayClass21_0.b__0 0xCC9A50 --
    	//   MessageLabels.FirstOrDefault(label => label.LabelId == textInsertData.LabelId) (string equality); a match
    	//   gets InsertTexts(textInsertData.Texts), otherwise Debug.LogWarning("ラベル ID が見つかりません：" + id)
    	// Annotated disassembly: work/title-path/disas/pd_0xCC8C50.txt, pd_0xCC9A50.txt
    	[Token(Token = "0x6000011")]
    	[Address(RVA = "0xCC8C50", Offset = "0xCC7650", VA = "0x180CC8C50")]
    	private void InsertTexts(TextInsertData textInsertData)
    	{
    		RebuildableLabel rebuildableLabel = base.MessageLabels.FirstOrDefault((RebuildableLabel label) => label.LabelId == textInsertData.LabelId);
    		if (rebuildableLabel != null)
    		{
    			rebuildableLabel.InsertTexts(textInsertData.Texts);
    			return;
    		}
    		UnityEngine.Debug.LogWarning("ラベル ID が見つかりません：" + textInsertData.LabelId);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCC8F30 (kickoff) / 0xCCC020 (<PlayPresentAnimationAsync>d__22.MoveNext, 2240 B):
    	//   using <manualMotionSkipper>5__2 = new ManualMotionSkipper() (ctor inlined: new CTS, ManualMotionDispatcher
    	//   .Default.Reset()); ScaleAsync(1f (data 0x2AE2820), _scaleUpDuration (+0xA0), Ease 0x1A = OutBack, from 0.5f
    	//   (0x3F000000), updateMotionManually true, ct); _generalScreen.FadeInAsync(false, true, ct); then
    	//   manualMotionSkipper.StartUpdate(timePosition, true) (IManualMotionSkipper slot 0); await UniTask.WhenAll of the
    	//   two tasks. Body in PlayPresentAnimationAsyncCore.
    	// Annotated disassembly: work/title-path/disas/pd_0xCCC020.txt
    	[Token(Token = "0x6000012")]
    	[Address(RVA = "0xCC8F30", Offset = "0xCC7930", VA = "0x180CC8F30")]
    	[AsyncStateMachine(typeof(_003CPlayPresentAnimationAsync_003Ed__22))]
    	private UniTask PlayPresentAnimationAsync(float timePosition = 0f, CancellationToken ct = default)
    	{
    		return PlayPresentAnimationAsyncCore(timePosition, ct);
    	}

    	private async UniTask PlayPresentAnimationAsyncCore(float timePosition, CancellationToken ct)
    	{
    		using IManualMotionSkipper manualMotionSkipper = new ManualMotionSkipper();
    		UniTask scaleTask = ScaleAsync(1f, _scaleUpDuration, Ease.OutBack, 0.5f, true, ct);
    		UniTask fadeTask = _generalScreen.FadeInAsync(false, true, ct);
    		manualMotionSkipper.StartUpdate(timePosition, true);
    		await UniTask.WhenAll(scaleTask, fadeTask);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCC9100 (kickoff) / 0xCCA5B0 (<DismissAsync>d__23.MoveNext, 2234 B): nothing
    	//   while a dismissal is running (_dismissCts (+0xB8) set, 0x180CCA666); a running presentation (_presentCts
    	//   +0xB0) is cancelled, disposed and cleared; _dismissCts = new CTS; in a using over a token source linked to it
    	//   and ct (<linkedCts>5__2): _generalScreen.BeginToDismiss(); await PlayDismissAnimationAsync(linked);
    	//   _generalScreen.EndToDismiss(); _dismissCts disposed and cleared. Body in DismissAsyncCore.
    	// Annotated disassembly: work/title-path/disas/pd_0xCCA5B0.txt
    	[Token(Token = "0x6000013")]
    	[Address(RVA = "0xCC9100", Offset = "0xCC7B00", VA = "0x180CC9100")]
    	[AsyncStateMachine(typeof(_003CDismissAsync_003Ed__23))]
    	public UniTask DismissAsync(CancellationToken ct = default)
    	{
    		return DismissAsyncCore(ct);
    	}

    	private async UniTask DismissAsyncCore(CancellationToken ct)
    	{
    		// 0x180CCA666: the shipped code ignores the request while a dismissal is running
    		if (_dismissCts != null)
    		{
    			return;
    		}
    		if (_presentCts != null)
    		{
    			_presentCts.Cancel();
    			_presentCts.Dispose();
    			_presentCts = null;
    		}
    		_dismissCts = new CancellationTokenSource();
    		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_dismissCts.Token, ct);
    		_generalScreen.BeginToDismiss();
    		await PlayDismissAnimationAsync(linkedCts.Token);
    		_generalScreen.EndToDismiss();
    		_dismissCts.Dispose();
    		_dismissCts = null;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCC92B0 (kickoff) / 0xCCBBD0 (<PlayDismissAnimationAsync>d__24.MoveNext): await
    	//   UniTask.WhenAll(ScaleAsync(0.75f (data 0x2AE2780), _scaleDownDuration (+0xA4), Ease 5 = OutQuad, null, false,
    	//   ct), _generalScreen.FadeOutAsync(false, false, ct)). Body in PlayDismissAnimationAsyncCore.
    	// Annotated disassembly: work/title-path/disas/pd_0xCCBBD0.txt
    	[Token(Token = "0x6000014")]
    	[Address(RVA = "0xCC92B0", Offset = "0xCC7CB0", VA = "0x180CC92B0")]
    	[AsyncStateMachine(typeof(_003CPlayDismissAnimationAsync_003Ed__24))]
    	private UniTask PlayDismissAnimationAsync(CancellationToken ct = default)
    	{
    		return PlayDismissAnimationAsyncCore(ct);
    	}

    	private async UniTask PlayDismissAnimationAsyncCore(CancellationToken ct)
    	{
    		UniTask scaleTask = ScaleAsync(0.75f, _scaleDownDuration, Ease.OutQuad, null, false, ct);
    		UniTask fadeTask = _generalScreen.FadeOutAsync(false, false, ct);
    		await UniTask.WhenAll(scaleTask, fadeTask);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCC9450 (kickoff) / 0xCCD400 (<ScaleAsync>d__25.MoveNext, 1690 B): a negative
    	//   duration (0x180CCD512) only logs Debug.LogWarning(string.Format("時間は 0 以上でなければいけません：{0}",
    	//   duration)); a from value first sets _viewRootTransform (+0x98).localScale = (from, from, 1); then await
    	//   LMotion.Create(localScale, (to, to, 1), duration).WithEase(ease).WithScheduler(updateMotionManually ?
    	//   MotionScheduler.Manual : MotionScheduler.UpdateIgnoreTimeScale (static +0x60)).BindToLocalScale(
    	//   _viewRootTransform).AddTo(_viewRootTransform (Component overload 0xDA5150)).ToUniTask(ct).
    	//   Body in ScaleAsyncCore.
    	// Annotated disassembly: work/title-path/disas/pd_0xCCD400.txt
    	[Token(Token = "0x6000015")]
    	[Address(RVA = "0xCC9450", Offset = "0xCC7E50", VA = "0x180CC9450")]
    	[AsyncStateMachine(typeof(_003CScaleAsync_003Ed__25))]
    	private UniTask ScaleAsync(float to, float duration, Ease ease, float? from = default, bool updateMotionManually = false, CancellationToken ct = default)
    	{
    		return ScaleAsyncCore(to, duration, ease, from, updateMotionManually, ct);
    	}

    	private async UniTask ScaleAsyncCore(float to, float duration, Ease ease, float? from, bool updateMotionManually, CancellationToken ct)
    	{
    		if (duration < 0f)
    		{
    			UnityEngine.Debug.LogWarning(string.Format("時間は 0 以上でなければいけません：{0}", duration));
    			return;
    		}
    		if (from.HasValue)
    		{
    			_viewRootTransform.localScale = new Vector3(from.Value, from.Value, 1f);
    		}
    		await LMotion.Create(_viewRootTransform.localScale, new Vector3(to, to, 1f), duration).WithEase(ease)
    			.WithScheduler(updateMotionManually ? MotionScheduler.Manual : MotionScheduler.UpdateIgnoreTimeScale)
    			.BindToLocalScale(_viewRootTransform)
    			.AddTo(_viewRootTransform)
    			.ToUniTask(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCC9650 (kickoff) / 0xCCB140 (<OnResponseAsync>d__26.MoveNext, 2654 B;
    	//   finally funclet 0xCCBB50): using <>8__1.linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct)
    	//   (params overload, 1-element array); the tasks are ResponseButtons (lazy +0x78) Select b__0 ToList; with
    	//   CancelableByAction (+0x6C): InputManager.TryGetAction("Cancel", out action) adds
    	//   action.OnPerformAsync(linked).ToCancel(), and _generalScreen.TryGetClickUnderlayTask(out task, linked)
    	//   (inlined onto its _underlay +0x30) adds task.ToCancel(); (_, result) = await UniTask.WhenAny(tasks);
    	//   linkedCts.Cancel(); PlayResponseSfx(result) (inlined: IInputManager slot 0 with result != Cancel); return
    	//   result. Body in OnResponseAsyncCore.
    	// Annotated disassembly: work/title-path/disas/pd_0xCCB140.txt
    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0xCC9650", Offset = "0xCC8050", VA = "0x180CC9650")]
    	[AsyncStateMachine(typeof(_003COnResponseAsync_003Ed__26))]
    	public UniTask<ResponseKind> OnResponseAsync(CancellationToken ct = default)
    	{
    		return OnResponseAsyncCore(ct);
    	}

    	private async UniTask<ResponseKind> OnResponseAsyncCore(CancellationToken ct)
    	{
    		using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
    		List<UniTask<ResponseKind>> list = base.ResponseButtons.Select((RebuildableButton button) => button.OnClickAsync(linkedCts.Token)).ToList();
    		if (base.CancelableByAction)
    		{
    			if (InputManager.TryGetAction("Cancel", out InputAction action))
    			{
    				list.Add(action.OnPerformAsync(linkedCts.Token).ToCancel());
    			}
    			if (_generalScreen.TryGetClickUnderlayTask(out UniTask task, linkedCts.Token))
    			{
    				list.Add(task.ToCancel());
    			}
    		}
    		(int winArgumentIndex, ResponseKind result) = await UniTask.WhenAny(list);
    		linkedCts.Cancel();
    		PlayResponseSfx(result);
    		return result;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCC9840 (kickoff) / 0xCCA150 (<DismissAndReleaseAsync>d__27.MoveNext): await
    	//   DismissAsync(ct); InUse = false (0x180CCA512: byte [this+0xC0] = 0). Body in DismissAndReleaseAsyncCore.
    	// Annotated disassembly: work/title-path/disas/pd_0xCCA150.txt
    	[Token(Token = "0x6000017")]
    	[Address(RVA = "0xCC9840", Offset = "0xCC8240", VA = "0x180CC9840")]
    	[AsyncStateMachine(typeof(_003CDismissAndReleaseAsync_003Ed__27))]
    	public UniTask DismissAndReleaseAsync(CancellationToken ct = default)
    	{
    		return DismissAndReleaseAsyncCore(ct);
    	}

    	private async UniTask DismissAndReleaseAsyncCore(CancellationToken ct)
    	{
    		await DismissAsync(ct);
    		InUse = false;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCC99E0, 104 B -- InputManager.PlayResponseSfx(kind != Cancel ? Submit
    	//   : Cancel) (IInputManager slot 0, setne)
    	// Annotated disassembly: work/title-path/disas/pd_0xCC99E0.txt
    	[Token(Token = "0x6000018")]
    	[Address(RVA = "0xCC99E0", Offset = "0xCC83E0", VA = "0x180CC99E0")]
    	private void PlayResponseSfx(ResponseKind kind)
    	{
    		InputManager.PlayResponseSfx((kind != ResponseKind.Cancel) ? ResponseKind.Submit : ResponseKind.Cancel);
    	}

    	[Token(Token = "0x6000019")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public ProvidableDialog()
    	{
    	}
    }
}
