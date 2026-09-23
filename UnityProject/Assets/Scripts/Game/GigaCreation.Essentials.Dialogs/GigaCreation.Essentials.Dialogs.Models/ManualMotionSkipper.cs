using UnityEngine;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using LitMotion;

namespace GigaCreation.Essentials.Dialogs.Models
{
    [Token(Token = "0x200001B")]
    public class ManualMotionSkipper : IManualMotionSkipper, IDisposable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200001C")]
    	[CompilerGenerated]
    	private struct _003CUpdateAsync_003Ed__5
    {
    		[Token(Token = "0x4000060")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000061")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000062")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ManualMotionSkipper _003C_003E4__this;

    		[Token(Token = "0x4000063")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public bool ignoreTimeScale;

    		[Token(Token = "0x4000064")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000056")]
    		[Address(RVA = "0xCCF5D0", Offset = "0xCCDFD0", VA = "0x180CCF5D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000057")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCCF120: new CancellationTokenSource() (ctor inlined) -> +0x10
    	[Token(Token = "0x400005E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly CancellationTokenSource _cts = new CancellationTokenSource();

    	[Token(Token = "0x400005F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private bool _started;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCCF120 -- after the _cts initializer: ManualMotionDispatcher static +0x0
    	//   (Default), tail jump ManualMotionDispatcher.Reset
    	[Token(Token = "0x6000052")]
    	[Address(RVA = "0xCCF120", Offset = "0xCCDB20", VA = "0x180CCF120")]
    	public ManualMotionSkipper()
    	{
    		ManualMotionDispatcher.Default.Reset();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCCF220, 125 B -- _cts (+0x10).Cancel() (inlined: disposed check,
    	//   NotifyCancellation(false)), then tail jump _cts.Dispose()
    	// Annotated disassembly: work/title-path/disas/mms_0xCCF220.txt
    	[Token(Token = "0x6000053")]
    	[Address(RVA = "0xCCF220", Offset = "0xCCDC20", VA = "0x180CCF220", Slot = "5")]
    	public void Dispose()
    	{
    		_cts.Cancel();
    		_cts.Dispose();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCCF2A0, 476 B -- only the first call does anything (_started +0x18,
    	//   0x180CCF2E1): _started = true; ManualMotionDispatcher.Default.Update(startTime) (float widened to double);
    	//   UpdateAsync(ignoreTimeScale).Forget()
    	// Annotated disassembly: work/title-path/disas/mms_0xCCF2A0.txt
    	[Token(Token = "0x6000054")]
    	[Address(RVA = "0xCCF2A0", Offset = "0xCCDCA0", VA = "0x180CCF2A0", Slot = "4")]
    	public void StartUpdate(float startTime, bool ignoreTimeScale = false)
    	{
    		// 0x180CCF2E1: the shipped code starts the update loop once
    		if (_started)
    		{
    			return;
    		}
    		_started = true;
    		ManualMotionDispatcher.Default.Update(startTime);
    		UpdateAsync(ignoreTimeScale).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCCF480 (kickoff) / 0xCCF5D0 (<UpdateAsync>d__5.MoveNext, 953 B): until _cts
    	//   is cancelled (IsCancellationRequested inlined: state >= 2, 0x180CCF692): await UniTask.Yield(_cts.Token), then
    	//   ManualMotionDispatcher.Default.Update(ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime) (icalls
    	//   named at data 0x2608F48 / 0x2608950). Body in UpdateAsyncCore.
    	// Annotated disassembly: work/title-path/disas/mms_0xCCF5D0.txt
    	[Token(Token = "0x6000055")]
    	[Address(RVA = "0xCCF480", Offset = "0xCCDE80", VA = "0x180CCF480")]
    	[AsyncStateMachine(typeof(_003CUpdateAsync_003Ed__5))]
    	private UniTask UpdateAsync(bool ignoreTimeScale = false)
    	{
    		return UpdateAsyncCore(ignoreTimeScale);
    	}

    	private async UniTask UpdateAsyncCore(bool ignoreTimeScale)
    	{
    		while (!_cts.IsCancellationRequested)
    		{
    			await UniTask.Yield(_cts.Token);
    			ManualMotionDispatcher.Default.Update(ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime);
    		}
    	}
    }
}
