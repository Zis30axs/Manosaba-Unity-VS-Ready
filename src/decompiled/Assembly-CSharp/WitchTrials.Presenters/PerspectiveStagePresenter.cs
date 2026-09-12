using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.AddressableAssets;
using WitchTrials.Models;
using WitchTrials.Views;

namespace WitchTrials.Presenters;

[Token(Token = "0x200014B")]
public class PerspectiveStagePresenter : MonoBehaviour, IPerspectiveStageLoader, ICourtModifier
{
	[StructLayout(3)]
	[Token(Token = "0x200014D")]
	[CompilerGenerated]
	private struct _003CLoadStageAsync_003Ed__13
{
		[Token(Token = "0x4000667")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000668")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000669")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public PerspectiveStagePresenter _003C_003E4__this;

		[Token(Token = "0x400066A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public PerspectiveStageKind kind;

		[Token(Token = "0x400066B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public CancellationToken ct;

		[Token(Token = "0x400066C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<GameObject>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600058C")]
		[Address(RVA = "0x38E6A0", Offset = "0x38D0A0", VA = "0x18038E6A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600058D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400065C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("Assets")]
	[SerializeField]
	private PerspectiveStageData[] _stageData;

	[Token(Token = "0x400065D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[Header("References")]
	[SerializeField]
	private Transform _stageParent;

	[Token(Token = "0x400065E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private StageScreenController _screenController;

	[Token(Token = "0x400065F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private StageCameraController _cameraController;

	[Token(Token = "0x4000660")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private IPerspectiveStageManager _perspectiveStageManager;

	[Token(Token = "0x4000661")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private ITrialStateManager _trialStateManager;

	[Token(Token = "0x4000662")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private IReadOnlyDictionary<PerspectiveStageKind, AssetReferenceGameObject> _stageReferences;

	[Token(Token = "0x4000663")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private GameObject _currentStage;

	[Token(Token = "0x170000B5")]
	private ITrialStateManager TrialStateManager
	{
		[Token(Token = "0x6000580")]
		[Address(RVA = "0x38D810", Offset = "0x38C210", VA = "0x18038D810")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000581")]
	[Address(RVA = "0x38D8D0", Offset = "0x38C2D0", VA = "0x18038D8D0")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000582")]
	[Address(RVA = "0x38DEF0", Offset = "0x38C8F0", VA = "0x18038DEF0")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000583")]
	[Address(RVA = "0x38DF60", Offset = "0x38C960", VA = "0x18038DF60")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000584")]
	[Address(RVA = "0x38E0A0", Offset = "0x38CAA0", VA = "0x18038E0A0", Slot = "4")]
	[AsyncStateMachine(typeof(_003CLoadStageAsync_003Ed__13))]
	public UniTask LoadStageAsync(PerspectiveStageKind kind, [Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000585")]
	[Address(RVA = "0x38E260", Offset = "0x38CC60", VA = "0x18038E260", Slot = "5")]
	public void UnloadStage()
	{
	}

	[Token(Token = "0x6000586")]
	[Address(RVA = "0x38E470", Offset = "0x38CE70", VA = "0x18038E470", Slot = "6")]
	public void ChangeFireFxActivity(bool active)
	{
	}

	[Token(Token = "0x6000587")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public PerspectiveStagePresenter()
	{
	}
}
