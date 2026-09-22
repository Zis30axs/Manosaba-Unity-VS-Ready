using GigaCreation.Tools.Service;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.Debugging.Core;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.Debugging.General
{
    [Token(Token = "0x2000004")]
    public abstract class DebugOptionsBase : MonoBehaviour
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000005")]
    	[CompilerGenerated]
    	private struct _003CInitializeAsync_003Ed__6
    {
    		[Token(Token = "0x4000008")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000009")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400000A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CancellationToken ct;

    		[Token(Token = "0x400000B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public DebugOptionsBase _003C_003E4__this;

    		[Token(Token = "0x400000C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600000A")]
    		[Address(RVA = "0xD42E90", Offset = "0xD41890", VA = "0x180D42E90", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600000B")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000006")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private static bool s_isInitialized;

    	[Token(Token = "0x4000007")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    	private static IDebugManager s_debugManager;

    	[Token(Token = "0x17000001")]
    	public static bool IsDebugMode
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD429F0, 220 B -- static +0x0 `s_isInitialized` false -> LogWarning("DebugOptions not
    		//   initialized.") and false; static +0x8 `s_debugManager` null -> false; else IDebugManager slot 0 IsDebugMode,
    		//   IReactiveProperty<bool> slot 0 get_Value (tail call)
    		// Annotated disassembly: work/title-path/disas/dob_01_get_IsDebugMode.txt
    		[Token(Token = "0x6000003")]
    		[Address(RVA = "0xD429F0", Offset = "0xD413F0", VA = "0x180D429F0")]
    		get
    		{
    			if (!s_isInitialized)
    			{
    				UnityEngine.Debug.LogWarning("DebugOptions not initialized.");
    				return false;
    			}
    			// 0x180D42A70: the shipped binary tests the manager before reading it (no manager = release build)
    			if (s_debugManager == null)
    			{
    				return false;
    			}
    			return s_debugManager.IsDebugMode.Value;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD42AD0, 437 B -- InitializeAsync(destroyCancellationToken) kickoff inlined, Forget()
    	// Annotated disassembly: work/title-path/disas/dob_02_Awake.txt
    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xD42AD0", Offset = "0xD414D0", VA = "0x180D42AD0")]
    	private void Awake()
    	{
    		InitializeAsync(destroyCancellationToken).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD42C90, 18 B -- tail call through vtable +0x178 (slot 4 = UpdateStaticFields)
    	// Annotated disassembly: work/title-path/disas/dob_06_OnValidate.txt
    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xD42C90", Offset = "0xD41690", VA = "0x180D42C90")]
    	private void OnValidate()
    	{
    		UpdateStaticFields();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD42CB0 (kickoff) / 0xD42E90 (<InitializeAsync>d__6.MoveNext, 877 B)
    	// The body lives in InitializeAsyncCore (XxxCore convention; the <InitializeAsync>d__6 metadata stays).
    	//   await UniTask.Yield(timing 2 = EarlyUpdate, ct) (0x18180C850) -- one frame, so DebugPresenter.Awake has run;
    	//   ServiceLocator.TryGet<IDebugManager>(out static +0x8) -> UpdateStaticFields (vtable +0x178); static +0x0 = true.
    	//   A release build keeps s_debugManager null, so every DebugOptions switch reads as off there.
    	// Annotated disassembly: work/title-path/disas/dob_03_InitializeAsync, dob_04_InitializeAsync_MoveNext.txt
    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0xD42CB0", Offset = "0xD416B0", VA = "0x180D42CB0")]
    	[AsyncStateMachine(typeof(_003CInitializeAsync_003Ed__6))]
    	private UniTask InitializeAsync([Optional] CancellationToken ct)
    	{
    		return InitializeAsyncCore(ct);
    	}

    	private async UniTask InitializeAsyncCore(CancellationToken ct)
    	{
    		await UniTask.Yield(PlayerLoopTiming.EarlyUpdate, ct);
    		if (ServiceLocator.TryGet(out s_debugManager))
    		{
    			UpdateStaticFields();
    		}
    		s_isInitialized = true;
    	}

    	[Token(Token = "0x6000007")]
    	protected abstract void UpdateStaticFields();

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD42E50, 59 B -- static +0x0 = 0 (SubsystemRegistration: domain-reload-free play mode)
    	// Annotated disassembly: work/title-path/disas/dob_05_ResetInitializationFlag.txt
    	[Token(Token = "0x6000008")]
    	[Address(RVA = "0xD42E50", Offset = "0xD41850", VA = "0x180D42E50")]
    	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    	private static void ResetInitializationFlag()
    	{
    		s_isInitialized = false;
    	}

    	[Token(Token = "0x6000009")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	protected DebugOptionsBase()
    	{
    	}
    }
}
