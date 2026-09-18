using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.Overlays.Models;
using GigaCreation.Essentials.Overlays.Views;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;

namespace GigaCreation.Essentials.Overlays.Presenters
{
    [Token(Token = "0x2000006")]
    public class OverlayPresenter : MonoBehaviour
    {
    	[Token(Token = "0x400000D")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private Overlay _overlay;

    	[Token(Token = "0x400000E")]
    	[FieldOffset(Offset = "0x28")]
    	private IOverlayFader _overlayFader;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD46B0, 611 B
    	//
    	// Native evidence (log:
    	// tools\unity-rebuild\out\logs\disas_overlaypresenter_awake_0xCD46B0.txt):
    	//  1. `il2cpp_codegen_object_new(OverlayFader_TypeInfo)` (0x2980E0), then
    	//     `new Subject<FadeOverlayRequest>()` (0x9294D0, METHODINFO
    	//     `UniRx.Subject<FadeOverlayRequest>..ctor()`) stored straight into the fresh object at
    	//     +0x10 -- i.e. OverlayFader's own parameterless constructor (RVA 0xCD4FE0) inlined at the
    	//     call site: disassembling 0xCD4FE0 separately shows the same two steps *and* the same
    	//     one-time class-init guard address (data_30DD141), which is what identifies it as that
    	//     constructor rather than hand-written duplicate logic. So the source is `new OverlayFader()`.
    	//  2. `mov [rsi+0x28], rdi` -> OverlayPresenter._overlayFader (dump.cs:744166, +0x28) = that object.
    	//  3. null test on `_overlayFader`, then `mov ecx,5` + `rdx = IOverlayFader_TypeInfo` +
    	//     `r8 = object` -> interface-call thunk 0x22E0. Slot 5 of IOverlayFader is
    	//     `OnRequestToFadeOverlayAsObservable` (dump.cs:744207 "Slot: 5"); the interface's own method
    	//     table is exactly slots 0..5 as dumped because IService (dump.cs:744623) declares no members.
    	//     The IObservable<FadeOverlayRequest> it returns is what step 5 subscribes to.
    	//  4. `il2cpp_codegen_object_new(Action<FadeOverlayRequest>)` + `Action<T>..ctor` (0x974BB0) with
    	//     rdx = this and r8 = METHODINFO `<Awake>b__2_0` -> the lambda below. Its only capture is
    	//     `this` (it is emitted as an instance method on OverlayPresenter, not on a <>c display class),
    	//     so an inline lambda is the faithful shape.
    	//  5. `UniRx.ObservableExtensions.Subscribe<FadeOverlayRequest>` (0x735A00): rcx = the IObservable
    	//     from step 3, rdx = that Action, r8 = METHODINFO `Subscribe<FadeOverlayRequest>()`.
    	//  6. `UniRx.DisposableExtensions.AddTo<IDisposable>` (0x67F4A0): rcx = the subscription, rdx =
    	//     `this`. The callee's own signature is
    	//     `AddTo(T disposable, UnityEngine.Component* gameObjectComponent, MethodInfo*)`, i.e. the
    	//     `AddTo(Component)` overload (LifetimeDisposableExtensions.cs:58) -- the extra r8/r9 values at
    	//     the call site are the shared-generic ABI, not source arguments.
    	//  7. registration, proven directly: METHODINFO `ServiceLocator.Register<IOverlayFader>()` loaded
    	//     into rdx with rcx = `_overlayFader` ([rsi+0x28]), then a tail `jmp 0x752130` to
    	//     `ServiceLocator$$Register<object>` (`Register<TService>(TService service)`,
    	//     ServiceLocator.cs:62). This is the IOverlayFader service registration that the boot chain's
    	//     `ServiceLocator.Get<IOverlayFader>()` in SplashScreen.PlaySequenceAsyncCore needs.
    	//
    	// TODO(evidence): the lambda's parameter name and its statement layout are spelling choices. The
    	//   `request.WaitForCompletion` test, both call targets, and the fact that only the
    	//   WaitForCompletion branch stores into `FadeTask` are pinned by <Awake>b__2_0 (RVA 0xCD4990).
    	[Token(Token = "0x6000008")]
    	[Address(RVA = "0xCD46B0", Offset = "0xCD30B0", VA = "0x180CD46B0")]
    	private void Awake()
    	{
    		_overlayFader = new OverlayFader();

    		_overlayFader.OnRequestToFadeOverlayAsObservable()
    			.Subscribe(request =>
    			{
    				// <Awake>b__2_0, RVA 0xCD4990, 332 B (log:
    				// tools\unity-rebuild\out\logs\disas_overlaypresenter_awake_lambda_0xCD4990.txt):
    				//   `cmp byte ptr [request+0x2c], 0` = FadeOverlayRequest.WaitForCompletion
    				//   (dump.cs:744306, +0x2C). Both arms call `Overlay.FadeAsync(request)` (0xCD4030);
    				//   the taken arm resolves IOverlayFader in the receiver's interface table and calls
    				//   slot 0, `set_FadeTask(UniTask)` (dump.cs:744193 "Slot: 0"), with the returned
    				//   UniTask; the fall-through arm discards it through
    				//   `Cysharp.Threading.Tasks.UniTaskExtensions.Forget` (0x18181F300).
    				if (request.WaitForCompletion)
    				{
    					_overlayFader.FadeTask = _overlay.FadeAsync(request);
    				}
    				else
    				{
    					_overlay.FadeAsync(request).Forget();
    				}
    			})
    			.AddTo(this);

    		ServiceLocator.Register(_overlayFader);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD4920, 104 B
    	//
    	// Native evidence (log:
    	// tools\unity-rebuild\out\logs\disas_overlaypresenter_ondestroy_0xCD4920.txt):
    	//  1. `rcx = ServiceLocator_TypeInfo; cmp dword ptr [rcx+0xe4], 0` -> the IL2CPP one-time
    	//     class-init guard (0x24CE60 when uninitialized): boilerplate.
    	//  2. `mov rbx, [rbx+0x28]` -> OverlayPresenter._overlayFader (dump.cs:744166, +0x28).
    	//  3. METHODINFO `ServiceLocator.Unregister<IOverlayFader>()` loaded into rdx, rcx =
    	//     `_overlayFader`, then a tail `jmp 0x752790` to `ServiceLocator$$Unregister<object>`
    	//     (`Unregister<TService>(TService service)`, ServiceLocator.cs:67 -- the metadata declares no
    	//     parameterless overload, so this register really is the service argument).
    	// There is no null test, no Dispose call and no conditional around it: a single bare Unregister.
    	[Token(Token = "0x6000009")]
    	[Address(RVA = "0xCD4920", Offset = "0xCD3320", VA = "0x180CD4920")]
    	private void OnDestroy()
    	{
    		ServiceLocator.Unregister(_overlayFader);
    	}

    	[Token(Token = "0x600000A")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public OverlayPresenter()
    	{
    	}
    }
}
