using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;
using UnityEngine.Playables;

namespace WitchTrials.Views;

[Token(Token = "0x200007F")]
public class SpawnableReenactment : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IAwaitable
{
	[StructLayout(3)]
	[Token(Token = "0x2000080")]
	[CompilerGenerated]
	private struct _003CAwaitDestroy_003Ed__8
{
		[Token(Token = "0x40001EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40001ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40001EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnableReenactment _003C_003E4__this;

		[Token(Token = "0x40001EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x40001F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private TaskAwaiter _003C_003Eu__1;

		[Token(Token = "0x60001A0")]
		[Address(RVA = "0x333AF0", Offset = "0x3324F0", VA = "0x180333AF0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60001A1")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000081")]
	[CompilerGenerated]
	private struct _003CAwaitSpawn_003Ed__7
{
		[Token(Token = "0x40001F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40001F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40001F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnableReenactment _003C_003E4__this;

		[Token(Token = "0x40001F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x40001F5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private TaskAwaiter _003C_003Eu__1;

		[Token(Token = "0x60001A2")]
		[Address(RVA = "0x333D60", Offset = "0x332760", VA = "0x180333D60", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60001A3")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000082")]
	[CompilerGenerated]
	private struct _003CRunTimelineAsync_003Ed__9
{
		[Token(Token = "0x40001F6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40001F7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40001F8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public SpawnableReenactment _003C_003E4__this;

		[Token(Token = "0x40001F9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public PlayableDirector director;

		[Token(Token = "0x40001FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken asyncToken;

		[Token(Token = "0x40001FB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x40001FC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60001A4")]
		[Address(RVA = "0x334150", Offset = "0x332B50", VA = "0x180334150", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60001A5")]
		[Address(RVA = "0x3349D0", Offset = "0x3333D0", VA = "0x1803349D0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40001E6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private PlayableDirector _openingDirector;

	[Token(Token = "0x40001E7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private PlayableDirector _closingDirector;

	[Token(Token = "0x40001E8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private PlayableDirector[] _storiesDirector;

	[Token(Token = "0x40001E9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private PlayableDirector _current;

	[Token(Token = "0x40001EA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private int _currenIndex;

	[Token(Token = "0x40001EB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private CancellationTokenSource _cts;

	[Token(Token = "0x600019B")]
	[Address(RVA = "0x333420", Offset = "0x331E20", VA = "0x180333420", Slot = "4")]
	public void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
	{
	}

	[Token(Token = "0x600019C")]
	[Address(RVA = "0x333570", Offset = "0x331F70", VA = "0x180333570", Slot = "5")]
	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__7))]
	public Naninovel.UniTask AwaitSpawn([Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x600019D")]
	[Address(RVA = "0x3336F0", Offset = "0x3320F0", VA = "0x1803336F0", Slot = "6")]
	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__8))]
	public Naninovel.UniTask AwaitDestroy([Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x600019E")]
	[Address(RVA = "0x333870", Offset = "0x332270", VA = "0x180333870")]
	[AsyncStateMachine(typeof(_003CRunTimelineAsync_003Ed__9))]
	private Task RunTimelineAsync(AsyncToken asyncToken, PlayableDirector director)
	{
		return null;
	}

	[Token(Token = "0x600019F")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public SpawnableReenactment()
	{
	}
}
