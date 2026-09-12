using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;
using UnityEngine.Playables;

namespace WitchTrials.Views;

[Token(Token = "0x2000073")]
public class ObjectionCutIn : SpawnableWithAudio, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IAwaitable
{
	[Serializable]
	[Token(Token = "0x2000074")]
	public class ActivationGameObjects
	{
		[Token(Token = "0x4000196")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public GameObject[] Objects;

		[Token(Token = "0x6000176")]
		[Address(RVA = "0x32D0C0", Offset = "0x32BAC0", VA = "0x18032D0C0")]
		public void SetActive(bool value)
		{
		}

		[Token(Token = "0x6000177")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public ActivationGameObjects()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000075")]
	[CompilerGenerated]
	private struct _003CAwaitDestroy_003Ed__6
{
		[Token(Token = "0x4000197")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000198")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000199")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ObjectionCutIn _003C_003E4__this;

		[Token(Token = "0x400019A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000178")]
		[Address(RVA = "0x32D1C0", Offset = "0x32BBC0", VA = "0x18032D1C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000179")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000076")]
	[CompilerGenerated]
	private struct _003CAwaitSpawn_003Ed__5
{
		[Token(Token = "0x400019B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400019C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400019D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ObjectionCutIn _003C_003E4__this;

		[Token(Token = "0x400019E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x400019F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x40001A0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600017A")]
		[Address(RVA = "0x32D490", Offset = "0x32BE90", VA = "0x18032D490", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600017B")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000192")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[Space]
	[SerializeField]
	private PlayableDirector _playableDirector;

	[Token(Token = "0x4000193")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private ActivationGameObjects[] _objectLibrary;

	[Token(Token = "0x4000194")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private int _currentIndex;

	[Token(Token = "0x4000195")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private CancellationTokenSource _cts;

	[Token(Token = "0x6000172")]
	[Address(RVA = "0x32CCC0", Offset = "0x32B6C0", VA = "0x18032CCC0", Slot = "4")]
	public void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
	{
	}

	[Token(Token = "0x6000173")]
	[Address(RVA = "0x32CE10", Offset = "0x32B810", VA = "0x18032CE10", Slot = "5")]
	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__5))]
	public Naninovel.UniTask AwaitSpawn([Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000174")]
	[Address(RVA = "0x32CFA0", Offset = "0x32B9A0", VA = "0x18032CFA0", Slot = "6")]
	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__6))]
	public Naninovel.UniTask AwaitDestroy([Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000175")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public ObjectionCutIn()
	{
	}
}
