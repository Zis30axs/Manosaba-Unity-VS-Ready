using System;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
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
    		return null;
    	}

    	[Token(Token = "0x6000015")]
    	[Address(RVA = "0xCD0640", Offset = "0xCCF040", VA = "0x180CD0640")]
    	public static UniTask OnPressAsync(this InputAction self, [Optional] CancellationToken ct)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0xCD0720", Offset = "0xCCF120", VA = "0x180CD0720")]
    	public static UniTask OnReleaseAsync(this InputAction self, [Optional] CancellationToken ct)
    	{
    		return default(UniTask);
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
    		return default(UniTask);
    	}
    }
}
