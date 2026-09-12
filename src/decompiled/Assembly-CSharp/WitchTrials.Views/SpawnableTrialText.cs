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

[Token(Token = "0x2000083")]
public class SpawnableTrialText : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IAwaitable
{
	[StructLayout(3)]
	[Token(Token = "0x2000084")]
	[CompilerGenerated]
	private struct _003CAwaitDestroy_003Ed__10
{
		[Token(Token = "0x4000203")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000204")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000205")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnableTrialText _003C_003E4__this;

		[Token(Token = "0x4000206")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x4000207")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x4000208")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60001AB")]
		[Address(RVA = "0x334F30", Offset = "0x333930", VA = "0x180334F30", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60001AC")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000085")]
	[CompilerGenerated]
	private struct _003CAwaitSpawn_003Ed__9
{
		[Token(Token = "0x4000209")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400020A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400020B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnableTrialText _003C_003E4__this;

		[Token(Token = "0x400020C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x400020D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x400020E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60001AD")]
		[Address(RVA = "0x335670", Offset = "0x334070", VA = "0x180335670", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60001AE")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40001FD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private Canvas _canvas;

	[Token(Token = "0x40001FE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private PlayableDirector _playableDirector;

	[Token(Token = "0x40001FF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private CanvasGroup _rootCanvasGroup;

	[Token(Token = "0x4000200")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[Header("Parameters")]
	[SerializeField]
	private float _hideDuration;

	[Token(Token = "0x4000201")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private ICameraManager _naniCameraManager;

	[Token(Token = "0x4000202")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private CancellationTokenSource _cts;

	[Token(Token = "0x17000027")]
	private ICameraManager NaniCameraManager
	{
		[Token(Token = "0x60001A6")]
		[Address(RVA = "0x334A30", Offset = "0x333430", VA = "0x180334A30")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60001A7")]
	[Address(RVA = "0x334AF0", Offset = "0x3334F0", VA = "0x180334AF0", Slot = "4")]
	public void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
	{
	}

	[Token(Token = "0x60001A8")]
	[Address(RVA = "0x334C10", Offset = "0x333610", VA = "0x180334C10", Slot = "5")]
	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__9))]
	public Naninovel.UniTask AwaitSpawn([Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x60001A9")]
	[Address(RVA = "0x334DA0", Offset = "0x3337A0", VA = "0x180334DA0", Slot = "6")]
	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__10))]
	public Naninovel.UniTask AwaitDestroy([Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x60001AA")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public SpawnableTrialText()
	{
	}
}
