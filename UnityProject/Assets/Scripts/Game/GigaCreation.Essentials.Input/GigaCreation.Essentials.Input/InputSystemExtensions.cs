using System;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using UniRx;
using UnityEngine.InputSystem;

namespace GigaCreation.Essentials.Input
{
    [Token(Token = "0x2000008")]
    public static class InputSystemExtensions
    {
    	[Token(Token = "0x6000014")]
    	[Address(RVA = "0xCD03C0", Offset = "0xCCEDC0", VA = "0x180CD03C0")]
    	public static IObservable<InputAction.CallbackContext> AsObservable(this InputAction self)
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCD03C0 -- Observable.FromEvent<CallbackContext>(add, remove) over a closure holding
    		//   `self`; b__0 0xCD09C0 / b__1 0xCD0A20 are AddCallback / RemoveCallback on InputAction +0x170 = m_OnPerformed.
    		return Observable.FromEvent<InputAction.CallbackContext>(
    			action => self.performed += action,
    			action => self.performed -= action);
    	}

    	[Token(Token = "0x6000015")]
    	[Address(RVA = "0xCD0640", Offset = "0xCCF040", VA = "0x180CD0640")]
    	public static UniTask OnPressAsync(this InputAction self, [Optional] CancellationToken ct)
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCD0640 -- UniTask.WaitUntil(self.WasPressedThisFrame, cancellationToken: ct) (219 B).
    		return UniTask.WaitUntil(self.WasPressedThisFrame, PlayerLoopTiming.Update, ct);
    	}

    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0xCD0720", Offset = "0xCCF120", VA = "0x180CD0720")]
    	public static UniTask OnReleaseAsync(this InputAction self, [Optional] CancellationToken ct)
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCD0720 -- UniTask.WaitUntil(self.WasReleasedThisFrame, cancellationToken: ct) (219 B).
    		return UniTask.WaitUntil(self.WasReleasedThisFrame, PlayerLoopTiming.Update, ct);
    	}

    	[Token(Token = "0x6000017")]
    	[Address(RVA = "0xCD0800", Offset = "0xCCF200", VA = "0x180CD0800")]
        public static async UniTask OnPerformAsync(this InputAction self, [Optional] CancellationToken ct)
        {
            if (self == null)
                return;

            bool performed = false;
            void OnPerformed(InputAction.CallbackContext _) => performed = true;

            self.performed += OnPerformed;
            try
            {
                // A default(UniTask) is already completed and made every cancelable
                // ProvidableDialog close immediately. Wait for the actual Input System event.
                await UniTask.WaitUntil(() => performed, cancellationToken: ct, cancelImmediately: true);
            }
            finally
            {
                self.performed -= OnPerformed;
            }
    	}

    	[Token(Token = "0x6000018")]
    	[Address(RVA = "0xCD08E0", Offset = "0xCCF2E0", VA = "0x180CD08E0")]
    	public static UniTask OnCompleteAsync(this InputAction self, [Optional] CancellationToken ct)
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCD08E0 -- UniTask.WaitUntil(self.WasCompletedThisFrame, cancellationToken: ct) (219 B).
    		return UniTask.WaitUntil(self.WasCompletedThisFrame, PlayerLoopTiming.Update, ct);
    	}
    }
}
