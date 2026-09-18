using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Overlays.Models;
using Il2CppDummyDll;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Overlays.Views
{
    [Token(Token = "0x2000004")]
    public class Overlay : MonoBehaviour
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000005")]
    	[CompilerGenerated]
    	private struct _003CFadeAsync_003Ed__3
    {
    		[Token(Token = "0x4000008")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000009")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400000A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Overlay _003C_003E4__this;

    		[Token(Token = "0x400000B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public FadeOverlayRequest request;

    		[Token(Token = "0x400000C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000006")]
    		[Address(RVA = "0xCD41D0", Offset = "0xCD2BD0", VA = "0x180CD41D0", Slot = "4")]
    		private void MoveNext()
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0xCD41D0, 1235 B
    			//
    			// This state machine is *compiler-generated* from `FadeAsync` below; its recovered
    			// semantics live in `FadeAsyncCore`, which the C# compiler turns back into an equivalent
    			// state machine. Keeping a hand-written body here as well would duplicate the logic (and
    			// the compiler would emit a second nested state machine with this name), so the recovered
    			// sequence is documented on `FadeAsyncCore` instead. Native evidence for this exact
    			// function (annotated disassembly, in state order) is in
    			// tools\unity-rebuild\out\logs\disas_overlay_fadeasync_movenext_0xCD41D0.txt.
    		}

    		[Token(Token = "0x6000007")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000006")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private Image _image;

    	[Token(Token = "0x4000007")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private MotionHandle _motion;

    	[Token(Token = "0x6000003")]
    	[Address(RVA = "0xCD3F90", Offset = "0xCD2990", VA = "0x180CD3F90")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xCD4030", Offset = "0xCD2A30", VA = "0x180CD4030")]
    	[AsyncStateMachine(typeof(_003CFadeAsync_003Ed__3))]
    	public UniTask FadeAsync(FadeOverlayRequest request)
    	{
    		// The recovered body lives in FadeAsyncCore: writing it *here* as an async method would make
    		// the compiler emit a nested state machine whose name collides with the decompiled
    		// `_003CFadeAsync_003Ed__3` retained above as evidence-bearing metadata.
    		return FadeAsyncCore(request);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD4030 (kickoff, 401 B) / 0xCD41D0 (state machine
    	// MoveNext, 1235 B)
    	//
    	// Native evidence (annotated disassembly produced by work/reversing/disas.py:
    	// tools\unity-rebuild\out\logs\disas_overlay_fadeasync_0xCD4030.txt and
    	// tools\unity-rebuild\out\logs\disas_overlay_fadeasync_movenext_0xCD41D0.txt):
    	//
    	// Kickoff 0xCD4030 -- a zeroed <FadeAsync>d__3 with <>4__this = this (+0x18) and request (+0x20),
    	//   then METHODINFO `AsyncUniTaskMethodBuilder.Start<Overlay.<FadeAsync>d__3>()`: an ordinary
    	//   `async UniTask` method whose only await is <>u__1 (UniTask.Awaiter, +0x28).
    	//
    	// MoveNext, in state order:
    	//   1. `if (_motion.IsActive()) _motion.Cancel();` -- _motion (Overlay+0x28; MotionHandle =
    	//      StorageId/Index/Version) is copied to a local and passed to the body at 0xCD57C0; when
    	//      that returns true, the body at 0xCD58F0 is called with dl = 1. Neither RVA is attributed
    	//      to a method by dump.cs, but they are this assembly's copies of LitMotion's
    	//      `MotionManager.IsActive(MotionHandle)` and
    	//      `MotionManager.Cancel(MotionHandle, bool checkIsInSequence)`: the LitMotion wrappers
    	//      0xDA4B90 (`MotionHandleExtensions.IsActive`, dump.cs:642186) and 0xDA4E10
    	//      (`MotionHandleExtensions.Cancel`, dump.cs:642210) tail into them with exactly that
    	//      argument list -- 0xDA4B90 is `... call 0xCD57C0`, 0xDA4E10 is `mov dl,1 ... call
    	//      0xCD58F0` -- and the package sources define those wrappers as
    	//      `MotionManager.IsActive(handle)` / `MotionManager.Cancel(handle)`
    	//      (MotionHandleExtensions.cs:36/106) with `checkIsInSequence` left at its `true` default,
    	//      which is where that dl = 1 comes from. So MoveNext contains both extension methods
    	//      inlined. Nothing else touches _motion before the rebuild below.
    	//   2. `if (request.Color.HasValue) _image.color = request.Color.Value;` -- Nullable<Color>
    	//      (request+0x18) through `get_HasValue` (0x8290) and `get_Value` (0x88F360), then stored into
    	//      _image (Overlay+0x20) through the *virtual* setter at vtable +0x2A8, i.e.
    	//      `UnityEngine.UI.Graphic.set_color(Color)` (`public virtual void set_color(Color value)`;
    	//      it is the only Color property on Image/Graphic).
    	//   3. `Mathf.Approximately(request.Duration, 0f)` -- request.Duration (+0x14) is passed to
    	//      0x331670 with 0f as the second float; that body is Unity's
    	//      `Abs(b - a) < Max(1E-06f * Max(Abs(a), Abs(b)), Epsilon * 8f)`: 0x2AE3A00 is the 0x7FFFFFFF
    	//      abs mask, 0x2AE255C is 1e-06f, 0x2AE2AA4 is 8f and the value loaded from Mathf's static
    	//      block ([Mathf_TypeInfo+0xB8]) is Epsilon -- Mathf's only non-const static field.
    	//   4a. Approximately true (instant path): `Color color = _image.color;` -- virtual getter at
    	//       vtable +0x298 returning a 16-byte Color through an sret buffer -- then
    	//       `color.a = request.IsFadeOut ? 1f : 0f`: xmm6 is zeroed before the test and overwritten
    	//       with the constant at 0x2AE2820 (1.0f) only when request.IsFadeOut (+0x10) != 0, and the
    	//       store is `movss [rsp+0x5C], xmm6`, the `a` lane of that Color. Finally
    	//       `_image.color = color` through the same virtual setter at +0x2A8. _motion is untouched.
    	//   4b. Approximately false (animated path): the two alpha bounds are materialised as
    	//       from = (IsFadeOut ? 0f : 1f) at [rsp+0xC8] and to = (IsFadeOut ? 1f : 0f) at [rsp+0xC0]
    	//       (same 0x2AE2820 constant, so the opaque end agrees with the instant path above). The
    	//       register mapping rdx = &from, r8 = &to is pinned by the callee's own signature
    	//       `Create<TValue, TOptions, TAdapter>(in TValue from, in TValue to, float duration)`
    	//       (dump.cs:641222) and cross-checked against the non-generic forwarder in this very binary
    	//       (RVA 0xDA06B0), which spills its incoming from/to into the two slots it then passes as
    	//       rdx/r8. The call target is the shared generic body `LMotion$$Create<...>` (0x6EA5E0) with
    	//       METHODINFO `LitMotion.LMotion.Create<float, NoOptions, FloatMotionAdapter>()`, i.e. the
    	//       inlined body of the non-generic `LMotion.Create(float, float, float)` -- including the
    	//       *same* one-time-init guard address data_30DD628 and the same MethodInfo-on-the-stack
    	//       pattern, so the source is `LMotion.Create(from, to, request.Duration)`. The resulting
    	//       builder is chained through METHODINFO
    	//       `LitMotionUGUIExtensions.BindToColorA<NoOptions, FloatMotionAdapter>()` (0x708520,
    	//       body-folded with the R/G/B variants, hence the misleading `BindToColorR` alias in the
    	//       call annotation; the target name is the one in the METHODINFO) and then
    	//       `MotionHandleExtensions.AddTo(MotionHandle handle, MonoBehaviour target)` (0xDA53D0,
    	//       dump.cs:642238) with r8 = this; the AddTo result is stored into _motion (+0x28/+0x30).
    	//   4c. `await _motion.ToUniTask(request.Token);` -- _motion is re-read from +0x28, request.Token
    	//       (+0x30) is passed as the CancellationToken to
    	//       `LitMotionUniTaskExtensions.ToUniTask(MotionHandle, CancellationToken)` (0xD83990,
    	//       dump.cs:638495), and the returned UniTask goes through the ordinary UniTask async
    	//       machinery (AwaitUnsafeOnCompleted<UniTask.Awaiter, d__3> -> state 0 -> GetResult ->
    	//       SetResult at 0xCD45F4). Nothing follows the await.
    	//
    	// TODO(evidence): the two alpha values are written as two ternaries. Native evidence pins their
    	//   values and storage slots (from = IsFadeOut ? 0f : 1f, to = IsFadeOut ? 1f : 0f) but not the
    	//   source spelling -- e.g. two ternaries versus deriving one local from the other produces the
    	//   same registers here.
    	private async UniTask FadeAsyncCore(FadeOverlayRequest request)
    	{
    		if (_motion.IsActive())
    		{
    			_motion.Cancel();
    		}

    		if (request.Color.HasValue)
    		{
    			_image.color = request.Color.Value;
    		}

    		if (Mathf.Approximately(request.Duration, 0f))
    		{
    			Color color = _image.color;
    			color.a = request.IsFadeOut ? 1f : 0f;
    			_image.color = color;
    		}
    		else
    		{
    			float from = request.IsFadeOut ? 0f : 1f;
    			float to = request.IsFadeOut ? 1f : 0f;
    			_motion = LMotion.Create(from, to, request.Duration)
    				.BindToColorA(_image)
    				.AddTo(this);
    			await _motion.ToUniTask(request.Token);
    		}
    	}

    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public Overlay()
    	{
    	}
    }
}
