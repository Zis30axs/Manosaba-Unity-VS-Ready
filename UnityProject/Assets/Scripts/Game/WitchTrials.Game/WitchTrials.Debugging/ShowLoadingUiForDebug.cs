using FastEnumUtility;
using GigaCreation.Essentials.Ui;
using GigaCreation.NaninovelExtender.Common;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;
using YieldAwaitable = Naninovel.YieldAwaitable;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace WitchTrials.Debugging
{
    [Token(Token = "0x20001C1")]
    [UsedImplicitly]
    [CommandAlias("showLoading")]
    public class ShowLoadingUiForDebug : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20001C2")]
    	[CompilerGenerated]
    	private struct _003CShowLoadingUiAsync_003Ed__5
    {
    		[Token(Token = "0x4000856")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000857")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000858")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ShowLoadingUiForDebug _003C_003E4__this;

    		[Token(Token = "0x4000859")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x400085A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private IDisposable _003C__003E5__2;

    		[Token(Token = "0x400085B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private float _003CstartingTime_003E5__3;

    		[Token(Token = "0x400085C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
    		private float _003Cduration_003E5__4;

    		[Token(Token = "0x400085D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60007AB")]
    		[Address(RVA = "0x3B2D10", Offset = "0x3B1710", VA = "0x1803B2D10", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60007AC")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000852")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly]
    	[ParameterAlias("")]
    	public StringParameter LoadingMode;

    	[Token(Token = "0x4000853")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[UsedImplicitly]
    	[ParameterAlias("time")]
    	[ParameterDefaultValue("0.35")]
    	public DecimalParameter Duration;

    	[Token(Token = "0x4000854")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[UsedImplicitly]
    	[ParameterAlias("halt")]
    	public BooleanParameter HaltInput;

    	[Token(Token = "0x4000855")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[UsedImplicitly]
    	public BooleanParameter Wait;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3B2AC0, 171 B
    	// `WaitOrForget` (vtable +0x208, slot 13) over `ShowLoadingUiAsync` with `Wait` (+0x48).
    	[Token(Token = "0x60007A8")]
    	[Address(RVA = "0x3B2AC0", Offset = "0x3B14C0", VA = "0x1803B2AC0", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return WaitOrForget(ShowLoadingUiAsync, Wait, token);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3B2B70 (builder stub); MoveNext 0x3B2D10, 1928 B
    	// 
    	// - `FastEnum.TryParse<LoadingMode>((string)LoadingMode, ignoreCase: true (dl=1), out mode)`. On failure,
    	//   Command slot 7 `Warn` with the format argument being the parameter object; mode stays 0.
    	// - The hoisted unnamed `IDisposable` (+0x30) =
    	//   `NaninovelLoadingScreen.Show(mode, HaltInput, Array.Empty<ICancelEventBlockable>())`, disposed on
    	//   every exit.
    	// - `<startingTime>5__3` (+0x38) starts at 0f and accumulates `Time.deltaTime` (icall 0x2608950), so
    	//   despite its name it is elapsed time.
    	// - `<duration>5__4` (+0x3C) = Assigned(Duration) ? Duration.Value : 0.35f (rdata 0x2AE26D0).
    	// - The loop runs while `!token.Canceled` (inlined IsCancellationRequested) and `duration > startingTime`,
    	//   awaiting `UniTask.Yield(PlayerLoopTiming.Update (8), token)`.
    	// - The enum is qualified because the `LoadingMode` field shadows the type name.
    	[Token(Token = "0x60007A9")]
    	[Address(RVA = "0x3B2B70", Offset = "0x3B1570", VA = "0x1803B2B70")]
    	[AsyncStateMachine(typeof(_003CShowLoadingUiAsync_003Ed__5))]
    	private async UniTask ShowLoadingUiAsync(AsyncToken token)
    	{
    		if (!FastEnum.TryParse<GigaCreation.NaninovelExtender.Common.LoadingMode>(LoadingMode, ignoreCase: true, out GigaCreation.NaninovelExtender.Common.LoadingMode mode))
    		{
    			Warn(string.Format("Failed to parse `{0}` as loading mode.", LoadingMode));
    		}
    		using (NaninovelLoadingScreen.Show(mode, HaltInput, Array.Empty<ICancelEventBlockable>()))
    		{
    			float startingTime = 0f;
    			float duration = Assigned(Duration) ? Duration.Value : 0.35f;
    			while (!token.Canceled && duration > startingTime)
    			{
    				startingTime += Time.deltaTime;
    				await UniTask.Yield(PlayerLoopTiming.Update, token);
    			}
    		}
    	}

    	[Token(Token = "0x60007AA")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public ShowLoadingUiForDebug()
    	{
    	}
    }
}
