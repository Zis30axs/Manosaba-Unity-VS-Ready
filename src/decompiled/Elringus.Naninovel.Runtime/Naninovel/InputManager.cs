using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.InputSystem.UI;

namespace Naninovel;

[Token(Token = "0x200036B")]
[InitializeAtRuntime(0, null)]
public class InputManager : IStatefulService<GameStateMap>, IEngineService, IInputManager, IEngineService<InputConfiguration>
{
	[Serializable]
	[Token(Token = "0x200036C")]
	public class GameState
	{
		[Token(Token = "0x4000CB0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public bool ProcessInput;

		[Token(Token = "0x4000CB1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public List<string> DisabledSamplers;

		[Token(Token = "0x600107B")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public GameState()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200036E")]
	[CompilerGenerated]
	private struct _003CInitializeService_003Ed__22
{
		[Token(Token = "0x4000CB5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000CB6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000CB7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public InputManager _003C_003E4__this;

		[Token(Token = "0x4000CB8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask<UnityEngine.EventSystems.EventSystem>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000CB9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<UnityEngine.EventSystems.BaseInputModule>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x4000CBA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private InputSystemUIInputModule _003CinputModule_003E5__2;

		[Token(Token = "0x4000CBB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private YieldAwaitable.Awaiter _003C_003Eu__3;

		[Token(Token = "0x6001080")]
		[Address(RVA = "0x576B10", Offset = "0x575510", VA = "0x180576B10", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001081")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200036F")]
	[CompilerGenerated]
	private struct _003CSampleInput_003Ed__34
{
		[Token(Token = "0x4000CBC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000CBD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000CBE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public CancellationToken cancellationToken;

		[Token(Token = "0x4000CBF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public InputManager _003C_003E4__this;

		[Token(Token = "0x4000CC0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001082")]
		[Address(RVA = "0x577A80", Offset = "0x576480", VA = "0x180577A80", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001083")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000CA9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private readonly Dictionary<string, InputSampler> samplersMap;

	[Token(Token = "0x4000CAA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private readonly Dictionary<IManagedUI, string[]> blockingUIs;

	[Token(Token = "0x4000CAB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private readonly HashSet<string> blockedSamplers;

	[Token(Token = "0x4000CAC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private readonly CancellationTokenSource sampleCTS;

	[Token(Token = "0x4000CAD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private readonly InputModeDetector inputModeDetector;

	[Token(Token = "0x4000CAE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private InputMode inputMode;

	[Token(Token = "0x4000CAF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private GameObject gameObject;

	[Token(Token = "0x17000234")]
	public virtual InputConfiguration Configuration
	{
		[Token(Token = "0x6001068")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "20")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000235")]
	public virtual bool ProcessInput
	{
		[Token(Token = "0x6001069")]
		[Address(RVA = "0x468AC0", Offset = "0x4674C0", VA = "0x180468AC0", Slot = "21")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x600106A")]
		[Address(RVA = "0x468AD0", Offset = "0x4674D0", VA = "0x180468AD0", Slot = "22")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x17000236")]
	public virtual InputMode InputMode
	{
		[Token(Token = "0x600106B")]
		[Address(RVA = "0x575390", Offset = "0x573D90", VA = "0x180575390", Slot = "23")]
		get
		{
			return default(InputMode);
		}
		[Token(Token = "0x600106C")]
		[Address(RVA = "0x5753A0", Offset = "0x573DA0", VA = "0x1805753A0", Slot = "24")]
		set
		{
		}
	}

	[Token(Token = "0x14000057")]
	public event Action<InputMode> OnInputModeChanged
	{
		[Token(Token = "0x6001066")]
		[Address(RVA = "0x575170", Offset = "0x573B70", VA = "0x180575170", Slot = "9")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6001067")]
		[Address(RVA = "0x575280", Offset = "0x573C80", VA = "0x180575280", Slot = "10")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x600106D")]
	[Address(RVA = "0x5753C0", Offset = "0x573DC0", VA = "0x1805753C0")]
	public InputManager(InputConfiguration config)
	{
	}

	[Token(Token = "0x600106E")]
	[Address(RVA = "0x5757C0", Offset = "0x5741C0", VA = "0x1805757C0", Slot = "25")]
	[AsyncStateMachine(typeof(_003CInitializeService_003Ed__22))]
	public virtual UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x600106F")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "26")]
	public virtual void ResetService()
	{
	}

	[Token(Token = "0x6001070")]
	[Address(RVA = "0x5758F0", Offset = "0x5742F0", VA = "0x1805758F0", Slot = "27")]
	public virtual void DestroyService()
	{
	}

	[Token(Token = "0x6001071")]
	[Address(RVA = "0x575A60", Offset = "0x574460", VA = "0x180575A60", Slot = "28")]
	public virtual void SaveServiceState(GameStateMap stateMap)
	{
	}

	[Token(Token = "0x6001072")]
	[Address(RVA = "0x575D90", Offset = "0x574790", VA = "0x180575D90", Slot = "29")]
	public virtual UniTask LoadServiceState(GameStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001073")]
	[Address(RVA = "0x576000", Offset = "0x574A00", VA = "0x180576000", Slot = "30")]
	public virtual IInputSampler GetSampler(string bindingName)
	{
		return null;
	}

	[Token(Token = "0x6001074")]
	[Address(RVA = "0x576060", Offset = "0x574A60", VA = "0x180576060", Slot = "31")]
	public virtual void AddBlockingUI(IManagedUI ui, params string[] allowedSamplers)
	{
	}

	[Token(Token = "0x6001075")]
	[Address(RVA = "0x576170", Offset = "0x574B70", VA = "0x180576170", Slot = "32")]
	public virtual void RemoveBlockingUI(IManagedUI ui)
	{
	}

	[Token(Token = "0x6001076")]
	[Address(RVA = "0x5762A0", Offset = "0x574CA0", VA = "0x1805762A0", Slot = "33")]
	public virtual bool IsSampling(string bindingName)
	{
		return default(bool);
	}

	[Token(Token = "0x6001077")]
	[Address(RVA = "0x5763C0", Offset = "0x574DC0", VA = "0x1805763C0", Slot = "34")]
	protected virtual void ChangeInputMode(InputMode mode)
	{
	}

	[Token(Token = "0x6001078")]
	[Address(RVA = "0x5763F0", Offset = "0x574DF0", VA = "0x1805763F0", Slot = "35")]
	protected virtual InputMode GetDefaultInputMode()
	{
		return default(InputMode);
	}

	[Token(Token = "0x6001079")]
	[Address(RVA = "0x5764F0", Offset = "0x574EF0", VA = "0x1805764F0", Slot = "36")]
	protected virtual void HandleBlockingUIVisibilityChanged(bool visible)
	{
	}

	[Token(Token = "0x600107A")]
	[Address(RVA = "0x576830", Offset = "0x575230", VA = "0x180576830", Slot = "37")]
	[AsyncStateMachine(typeof(_003CSampleInput_003Ed__34))]
	protected virtual UniTaskVoid SampleInput(CancellationToken cancellationToken)
	{
		return default(UniTaskVoid);
	}
}
