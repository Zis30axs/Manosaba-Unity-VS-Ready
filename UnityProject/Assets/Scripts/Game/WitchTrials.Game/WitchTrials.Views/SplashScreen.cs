using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Input;
using GigaCreation.Essentials.Overlays.Models;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;
using WitchTrials.Debugging;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000146")]
    public class SplashScreen : MonoBehaviour
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000147")]
    	[CompilerGenerated]
    	private struct _003CPlaySequenceAsync_003Ed__12
    {
    		[Token(Token = "0x400064E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400064F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000650")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public SplashScreen _003C_003E4__this;

    		[Token(Token = "0x4000651")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000652")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x4000653")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000654")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask<int>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000576")]
    		[Address(RVA = "0x38C070", Offset = "0x38AA70", VA = "0x18038C070", Slot = "4")]
    		private void MoveNext()
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x38C070 (3871 bytes)
    			//
    			// This state machine is *compiler-generated* from the async method below; its recovered
    			// semantics now live in `PlaySequenceAsyncCore`, which the C# compiler turns back into an
    			// equivalent state machine.  Keeping a hand-written body here as well would duplicate the
    			// logic (and the compiler would emit a second nested state machine with this name), so the
    			// recovered sequence is documented on `PlaySequenceAsyncCore` instead.  Native evidence for
    			// this exact function (annotated disassembly, in state order) is in
    			// tools/unity-rebuild/out/logs/SplashScreen_MoveNext_0x38C070.txt.
    		}

    		[Token(Token = "0x6000577")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000648")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	[Token(Token = "0x4000649")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Button _wholeScreenButton;

    	[Token(Token = "0x400064A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[Header("Parameters")]
    	[SerializeField]
    	private float _fadeDuration;

    	[Token(Token = "0x400064B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
    	[SerializeField]
    	private float _displayDuration;

    	[Token(Token = "0x400064C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private IInputManager _inputManager;

    	[Token(Token = "0x400064D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	private IOverlayFader _overlayFader;

    	[Token(Token = "0x170000B3")]
    	private IInputManager InputManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x38BC10, 186 B
    		//
    		// Native evidence (same shape as get_OverlayFader below, and as the `??=` service getters already
    		// reconstructed elsewhere in this project):
    		//   0x18038BC16  mov  rax,[rbx+0x38]              -> the cached field (_inputManager, dump.cs +0x38)
    		//   0x18038BC1A  test rax,rax / jne <return>      -> `??` short-circuit
    		//   0x18038BC1C  METHODINFO ServiceLocator.Get<IInputManager>()
    		//   0x18038BC39  mov  cl,1                        -> includeDerivedType: true
    		//   0x18038BC3B  call ServiceLocator$$Get<object>
    		//   0x18038BC40  mov  [rbx+0x38],rax              -> store back into the field (`??=`)
    		// The log for this RVA is tools/unity-rebuild/out/logs/SplashScreen_get_InputManager_0x38BC10.txt.
    		[Token(Token = "0x6000570")]
    		[Address(RVA = "0x38BC10", Offset = "0x38A610", VA = "0x18038BC10")]
    		get
    		{
    			return _inputManager ??= ServiceLocator.Get<IInputManager>(true);
    		}
    	}

    	[Token(Token = "0x170000B4")]
    	private IOverlayFader OverlayFader
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x38BCD0, 186 B
    		//
    		// Native evidence:
    		//   0x18038BD0B  mov  rax,[rbx+0x40]              -> _overlayFader (dump.cs +0x40)
    		//   0x18038BD12  jne  <return>                    -> `??` short-circuit
    		//   0x18038BD28  METHODINFO ServiceLocator.Get<IOverlayFader>()
    		//   0x18038BD2F  mov  cl,1                        -> includeDerivedType: true
    		//   0x18038BD31  call ServiceLocator$$Get<object>
    		//   0x18038BD40  mov  [rbx+0x40],rax              -> store back (`??=`)
    		// The remaining tail is the write barrier for the interface field.
    		[Token(Token = "0x6000571")]
    		[Address(RVA = "0x38BCD0", Offset = "0x38A6D0", VA = "0x18038BCD0")]
    		get
    		{
    			return _overlayFader ??= ServiceLocator.Get<IOverlayFader>(true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36E750 (identical-code-folded with MapAreaLabel.Show)
    	// 
    	// `_canvasGroup` (+0x20) -> set_alpha(1f).
    	[Token(Token = "0x6000572")]
    	[Address(RVA = "0x36E750", Offset = "0x36D150", VA = "0x18036E750")]
    	private void Awake()
    	{
    		_canvasGroup.alpha = 1f;
    	}

    	[Token(Token = "0x6000573")]
    	[Address(RVA = "0x38BD90", Offset = "0x38A790", VA = "0x18038BD90")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x6000574")]
    	[Address(RVA = "0x38BEC0", Offset = "0x38A8C0", VA = "0x18038BEC0")]
    	[AsyncStateMachine(typeof(_003CPlaySequenceAsync_003Ed__12))]
    	public UniTask PlaySequenceAsync([Optional] CancellationToken ct)
    	{
    		// The recovered body lives in PlaySequenceAsyncCore: writing it *here* as an async method would
    		// make the compiler emit a nested state machine whose name collides with the decompiled
    		// `_003CPlaySequenceAsync_003Ed__12` retained above as evidence-bearing metadata.
    		return PlaySequenceAsyncCore(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38BEC0 (kickoff, 420 B) / 0x38C070 (state machine MoveNext, 3871 B)
    	//
    	// Native evidence (annotated disassembly produced by work/reversing/disas.py:
    	// tools/unity-rebuild/out/logs/SplashScreen_PlaySequenceAsync_0x38BEC0.txt and
    	// tools/unity-rebuild/out/logs/SplashScreen_MoveNext_0x38C070.txt):
    	//
    	// Kickoff 0x38BEC0 -- zeroed <PlaySequenceAsync>d__12 with <>4__this = this (+0x18) and ct (+0x20),
    	//   then METHODINFO AsyncUniTaskMethodBuilder.Start<SplashScreen.<PlaySequenceAsync>d__12>() (0x331870)
    	//   and a UniTask returned by value out of the builder: an ordinary `async UniTask` method.  Its three
    	//   hoisted locals are <linkedCts>5__2 (+0x28), <>u__1 (UniTask.Awaiter, +0x30) and
    	//   <>u__2 (UniTask.Awaiter<int>, +0x40) -- i.e. exactly one `UniTask<int>` await exists.
    	//
    	// MoveNext, in state order (0 = first await, 1 = WhenAny, 2 = last await):
    	//   1. state -1 only: DebugOptionsBase.get_IsDebugMode(), and when it is true the DebugOptions
    	//      static-field block ([DebugOptions_TypeInfo + 0xB8]) byte +0x8 = s_skipSplashScreen.  That pair
    	//      is the inlined `DebugOptions.SkipSplashScreen` getter (DebugOptions.cs:138).  Taken branch ->
    	//      UnityEngine.Component.get_gameObject + GameObject.SetActive(false) + SetResult: return.
    	//   2. SplashScreen.get_OverlayFader (RVA 0x38BCD0, lazy cache in +0x40) then an IOverlayFader call
    	//      through the interface-slot thunk sub_4160 with slot index 4 = `FadeInAsync` (dump.cs:744205, Slot 4);
    	//      arguments: duration = _fadeDuration (this+0x30), color = a zeroed Nullable<Color> (hasValue 0),
    	//      ct = d__12+0x20.  Awaited as state 0.
    	//   3. CancellationTokenSource.CreateLinkedTokenSource (0x1810A39D0) on a 1-element
    	//      CancellationToken[] (sub_297400 with edx = 1: the `params` overload) -> <linkedCts>5__2.
    	//   4. il2cpp_codegen_object_new List<UniTask> + inlined List<UniTask>..ctor, whose body allocates a
    	//      3-element backer array ([list+0x10] = sub_297400(klass, 3)) -> `new List<UniTask>(3)`, filled by
    	//      three conditional-Add sites, each METHODINFO List<UniTask>.Add() + List<...>$$Add (0x331F40):
    	//        a. TimeSpan.FromSeconds(_displayDuration: this+0x34) + UniTask.Delay (0x18180D710) with
    	//           ignoreTimeScale 0, r9d = 8 = PlayerLoopTiming.Update, cancellationToken = linkedCts.Token,
    	//           cancelImmediately 0 -- all the overload's defaults.
    	//        b. _wholeScreenButton (this+0x28).m_OnClick (UnityEngine.UI.Button+0x100) + object_new
    	//           AsyncUnityEventHandler + .ctor(onClick, linkedCts.Token, true /*callOnce*/) + OnInvokeAsync
    	//           (0x18183BD10) -- verbatim Cysharp `UnityAsyncExtensions.OnClickAsync(Button, CancellationToken)`
    	//           (Packages/com.cysharp.unitask/UnityAsyncExtensions.uGUI.cs:58).
    	//        c. guarded by `test al, al`: IInputManager slot-2 thunk sub_8090 = `TryGetAction("Submit",
    	//           out action)` (string constant 'Submit' at 0x2ACF14C, out slot reused from [rbp+0x30]); when it
    	//           returns true, InputSystemExtensions.OnPerformAsync (0xCD0800, this InputAction + token) is
    	//           added.  The receiver is the inlined `InputManager` property: _inputManager (this+0x38) is
    	//           null-checked, loaded from ServiceLocator.Get<IInputManager> (0x751910 with cl = 1 =
    	//           includeDerivedType) and cached back in +0x38 -- the same one-time type-init slot
    	//           (data_30D94CE) and shape as the real getter at 0x38BC10.
    	//   5. await state 1: UniTask.WhenAny (0x18180FC80) over the List (the IEnumerable<UniTask> overload,
    	//      UniTask.WhenAny.cs:37) -> UniTask<int> in <>u__2; GetResult writes the winning index to a stack
    	//      local that is never read again -> the result is discarded.
    	//   6. linkedCts.Cancel(): inlined ThrowIfDisposed (cmp byte [cts+0x28]) + NotifyCancellation(false)
    	//      (0x1810A2FB0) -- what `CancellationTokenSource.Cancel()` compiles to.
    	//   7. await state 2: SplashScreen.get_OverlayFader again, IOverlayFader slot 2 = `FadeOutAsync`
    	//      (dump.cs:744199) resolved through 0x243A20 (r8d = 2) with the same three arguments as step 2.
    	//   8. <linkedCts>5__2 is disposed through the IDisposable thunk at every exit of the region that
    	//      starts right after step 3: normal completion (0x38CCE2) and the unwind path (0x38CDCA), plus
    	//      Roslyn's fast-completion guard `if (<>1__state >= 0) linkedCts.Dispose()` after the two suspends
    	//      inside the region (0x38CC7A, 0x38CD92) -- the `using` statement below.
    	//   9. after the region: this.gameObject.SetActive(false) (0x1820617A0 + 0x182067310), clear
    	//      <linkedCts>5__2, <>1__state = -2, AsyncUniTaskMethodBuilder.SetResult().
    	//
    	// TODO(evidence): the `color` argument of both IOverlayFader calls is a zeroed Nullable<Color>
    	//   (hasValue = 0).  An omitted `[Optional]` argument and an explicit `null` compile to the same code,
    	//   so the explicit `null` written below is a choice, not a recovered fact.
    	// TODO(evidence): `new List<UniTask>(3)` is inferred from the inlined ctor allocating exactly three
    	//   elements; the metadata MethodInfo only names `List<UniTask>..ctor()` without its signature.
    	private async UniTask PlaySequenceAsyncCore(CancellationToken ct)
    	{
    		if (DebugOptions.SkipSplashScreen)
    		{
    			gameObject.SetActive(false);
    			return;
    		}

    		await OverlayFader.FadeInAsync(_fadeDuration, null, ct);

    		using (CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct))
    		{
    			List<UniTask> tasks = new List<UniTask>(3);
    			tasks.Add(UniTask.Delay(TimeSpan.FromSeconds(_displayDuration), cancellationToken: linkedCts.Token));
    			tasks.Add(_wholeScreenButton.OnClickAsync(linkedCts.Token));

    			if (InputManager.TryGetAction("Submit", out var action))
    			{
    				tasks.Add(action.OnPerformAsync(linkedCts.Token));
    			}

    			await UniTask.WhenAny(tasks);

    			linkedCts.Cancel();

    			await OverlayFader.FadeOutAsync(_fadeDuration, null, ct);
    		}

    		gameObject.SetActive(false);
    	}

    	[Token(Token = "0x6000575")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public SplashScreen()
    	{
    	}
    }
}
