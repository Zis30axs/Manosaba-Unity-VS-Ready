using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.AddressablesUtils;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x2000077")]
public class SpawnableClue : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IAwaitable
{
	[StructLayout(3)]
	[Token(Token = "0x2000078")]
	[CompilerGenerated]
	private struct _003CAwaitDestroy_003Ed__27
{
		[Token(Token = "0x40001B3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40001B4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40001B5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnableClue _003C_003E4__this;

		[Token(Token = "0x40001B6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x40001B7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x40001B8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600018A")]
		[Address(RVA = "0x32EAF0", Offset = "0x32D4F0", VA = "0x18032EAF0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600018B")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000079")]
	[CompilerGenerated]
	private struct _003CAwaitSpawn_003Ed__26
{
		[Token(Token = "0x40001B9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40001BA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40001BB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnableClue _003C_003E4__this;

		[Token(Token = "0x40001BC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x40001BD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private CancellationTokenSource _003ClinkedCts_003E5__2;

		[Token(Token = "0x40001BE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask<Texture2D>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40001BF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600018C")]
		[Address(RVA = "0x32F480", Offset = "0x32DE80", VA = "0x18032F480", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600018D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200007A")]
	[CompilerGenerated]
	private struct _003CFadeAsync_003Ed__30
{
		[Token(Token = "0x40001C0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40001C1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40001C2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public float from;

		[Token(Token = "0x40001C3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		public float to;

		[Token(Token = "0x40001C4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float time;

		[Token(Token = "0x40001C5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public EasingType easing;

		[Token(Token = "0x40001C6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public SpawnableClue _003C_003E4__this;

		[Token(Token = "0x40001C7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken asyncToken;

		[Token(Token = "0x40001C8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600018E")]
		[Address(RVA = "0x330490", Offset = "0x32EE90", VA = "0x180330490", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600018F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200007B")]
	[CompilerGenerated]
	private struct _003CMoveAsync_003Ed__29
{
		[Token(Token = "0x40001C9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40001CA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40001CB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public Vector3 from;

		[Token(Token = "0x40001CC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public Vector3 to;

		[Token(Token = "0x40001CD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public float time;

		[Token(Token = "0x40001CE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
		public EasingType easing;

		[Token(Token = "0x40001CF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public SpawnableClue _003C_003E4__this;

		[Token(Token = "0x40001D0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public AsyncToken asyncToken;

		[Token(Token = "0x40001D1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000190")]
		[Address(RVA = "0x3308F0", Offset = "0x32F2F0", VA = "0x1803308F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000191")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40001A1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private Canvas _canvas;

	[Token(Token = "0x40001A2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private RectTransform _contentRectTransform;

	[Token(Token = "0x40001A3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private CanvasGroup _contentCanvasGroup;

	[Token(Token = "0x40001A4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[Header("Parameters")]
	[SerializeField]
	private float _duration;

	[Token(Token = "0x40001A5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
	[SerializeField]
	private Vector2 _move;

	[Token(Token = "0x40001A6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[SerializeField]
	private RawImage _rawImage;

	[Token(Token = "0x40001A7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[SerializeField]
	private EasingType _spawnMoveEasing;

	[Token(Token = "0x40001A8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x54")]
	[SerializeField]
	private EasingType _spawnFadeEasing;

	[Token(Token = "0x40001A9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[SerializeField]
	private EasingType _despawnMoveEasing;

	[Token(Token = "0x40001AA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x5C")]
	[SerializeField]
	private EasingType _despawnFadeEasing;

	[Token(Token = "0x40001AB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private readonly Tweener<VectorTween> _moveTweener;

	[Token(Token = "0x40001AC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private readonly Tweener<FloatTween> _fadeTweener;

	[Token(Token = "0x40001AD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	private IAddressablesManager _addressablesManager;

	[Token(Token = "0x40001AE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	private Camera _camera;

	[Token(Token = "0x40001AF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	private string _clueId;

	[Token(Token = "0x40001B0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private float _time;

	[Token(Token = "0x40001B1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8C")]
	private Vector2 _initialPosition;

	[Token(Token = "0x40001B2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	private CancellationTokenSource _cts;

	[Token(Token = "0x17000023")]
	private IAddressablesManager AddressablesManager
	{
		[Token(Token = "0x600017C")]
		[Address(RVA = "0x32DD30", Offset = "0x32C730", VA = "0x18032DD30")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000024")]
	private Camera Camera
	{
		[Token(Token = "0x600017D")]
		[Address(RVA = "0x32DDF0", Offset = "0x32C7F0", VA = "0x18032DDF0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000025")]
	private string Address
	{
		[Token(Token = "0x600017E")]
		[Address(RVA = "0x32DEE0", Offset = "0x32C8E0", VA = "0x18032DEE0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600017F")]
	[Address(RVA = "0x32DF40", Offset = "0x32C940", VA = "0x18032DF40")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000180")]
	[Address(RVA = "0x32DFA0", Offset = "0x32C9A0", VA = "0x18032DFA0", Slot = "4")]
	public void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
	{
	}

	[Token(Token = "0x6000181")]
	[Address(RVA = "0x32E0E0", Offset = "0x32CAE0", VA = "0x18032E0E0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__26))]
	public Naninovel.UniTask AwaitSpawn([Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000182")]
	[Address(RVA = "0x32E280", Offset = "0x32CC80", VA = "0x18032E280", Slot = "6")]
	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__27))]
	public Naninovel.UniTask AwaitDestroy([Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000183")]
	[Address(RVA = "0x32E400", Offset = "0x32CE00", VA = "0x18032E400")]
	private void CompleteTweenersIfRunning()
	{
	}

	[Token(Token = "0x6000184")]
	[Address(RVA = "0x32E520", Offset = "0x32CF20", VA = "0x18032E520")]
	[AsyncStateMachine(typeof(_003CMoveAsync_003Ed__29))]
	private Naninovel.UniTask MoveAsync(Vector3 from, Vector3 to, float time, EasingType easing, [Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000185")]
	[Address(RVA = "0x32E710", Offset = "0x32D110", VA = "0x18032E710")]
	[AsyncStateMachine(typeof(_003CFadeAsync_003Ed__30))]
	private Naninovel.UniTask FadeAsync(float from, float to, float time, EasingType easing, [Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000186")]
	[Address(RVA = "0x32E900", Offset = "0x32D300", VA = "0x18032E900")]
	private void SetAnchoredPosition(Vector2 anchoredPosition)
	{
	}

	[Token(Token = "0x6000187")]
	[Address(RVA = "0x32E920", Offset = "0x32D320", VA = "0x18032E920")]
	private void SetAlpha(float a)
	{
	}

	[Token(Token = "0x6000188")]
	[Address(RVA = "0x32E940", Offset = "0x32D340", VA = "0x18032E940")]
	public SpawnableClue()
	{
	}
}
