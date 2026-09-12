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

[Token(Token = "0x200007C")]
public class SpawnableMap : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IAwaitable
{
	[StructLayout(3)]
	[Token(Token = "0x200007D")]
	[CompilerGenerated]
	private struct _003CAwaitDestroy_003Ed__12
{
		[Token(Token = "0x40001DA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40001DB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40001DC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnableMap _003C_003E4__this;

		[Token(Token = "0x40001DD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x40001DE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x40001DF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000197")]
		[Address(RVA = "0x332520", Offset = "0x330F20", VA = "0x180332520", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000198")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200007E")]
	[CompilerGenerated]
	private struct _003CAwaitSpawn_003Ed__11
{
		[Token(Token = "0x40001E0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40001E1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40001E2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnableMap _003C_003E4__this;

		[Token(Token = "0x40001E3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x40001E4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x40001E5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000199")]
		[Address(RVA = "0x332C60", Offset = "0x331660", VA = "0x180332C60", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600019A")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40001D2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private Canvas _canvas;

	[Token(Token = "0x40001D3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private PlayableDirector[] _playableDirectors;

	[Token(Token = "0x40001D4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private CanvasGroup _rootCanvasGroup;

	[Token(Token = "0x40001D5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[Header("Parameters")]
	[SerializeField]
	private float _hideDuration;

	[Token(Token = "0x40001D6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private ICameraManager _naniCameraManager;

	[Token(Token = "0x40001D7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private PlayableDirector _current;

	[Token(Token = "0x40001D8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private int _currenIndex;

	[Token(Token = "0x40001D9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private CancellationTokenSource _cts;

	[Token(Token = "0x17000026")]
	private ICameraManager NaniCameraManager
	{
		[Token(Token = "0x6000192")]
		[Address(RVA = "0x330D80", Offset = "0x32F780", VA = "0x180330D80")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000193")]
	[Address(RVA = "0x330E40", Offset = "0x32F840", VA = "0x180330E40", Slot = "4")]
	public void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
	{
	}

	[Token(Token = "0x6000194")]
	[Address(RVA = "0x331080", Offset = "0x32FA80", VA = "0x180331080", Slot = "5")]
	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__11))]
	public Naninovel.UniTask AwaitSpawn([Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000195")]
	[Address(RVA = "0x331210", Offset = "0x32FC10", VA = "0x180331210", Slot = "6")]
	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__12))]
	public Naninovel.UniTask AwaitDestroy([Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000196")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public SpawnableMap()
	{
	}
}
