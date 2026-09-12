using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace WitchTrials.Models;

[Token(Token = "0x2000182")]
[UsedImplicitly]
[InitializeAtRuntime(0, null)]
public class TrialStateManager : ITrialStateManager, IEngineService, IStatefulService<GameStateMap>
{
	[Serializable]
	[Token(Token = "0x2000183")]
	public class GameState
	{
		[Token(Token = "0x4000734")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public PerspectiveStageKind CurrentStageKind;

		[Token(Token = "0x4000735")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
		public bool IsLoaded;

		[Token(Token = "0x4000736")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CameraState[] CameraStates;

		[Token(Token = "0x4000737")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public ScreenState[] ScreenStates;

		[Token(Token = "0x60006AC")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public GameState()
		{
		}
	}

	[Serializable]
	[Token(Token = "0x2000184")]
	public class CameraState
	{
		[Token(Token = "0x4000738")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public float Height;

		[Token(Token = "0x4000739")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
		public Vector3 Rotation;

		[Token(Token = "0x400073A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float Distance;

		[Token(Token = "0x60006AD")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public CameraState()
		{
		}
	}

	[Serializable]
	[Token(Token = "0x2000185")]
	public class ScreenState
	{
		[Token(Token = "0x400073B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public bool Visible;

		[Token(Token = "0x400073C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
		public float NormalizedPosX;

		[Token(Token = "0x400073D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public float Roll;

		[Token(Token = "0x400073E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		public ScreenSplitDirection Direction;

		[Token(Token = "0x60006AE")]
		[Address(RVA = "0x39E320", Offset = "0x39CD20", VA = "0x18039E320")]
		public ScreenState()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000187")]
	[CompilerGenerated]
	private struct _003CLoadServiceState_003Ed__9
{
		[Token(Token = "0x4000742")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000743")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000744")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GameStateMap stateMap;

		[Token(Token = "0x4000745")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public TrialStateManager _003C_003E4__this;

		[Token(Token = "0x4000746")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60006B1")]
		[Address(RVA = "0x39E510", Offset = "0x39CF10", VA = "0x18039E510", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60006B2")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400072F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private IPerspectiveStageManager _stageManager;

	[Token(Token = "0x4000730")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private PerspectiveStageKind _currentStageKind;

	[Token(Token = "0x4000731")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
	private bool _isLoaded;

	[Token(Token = "0x4000732")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private IList<CameraState> _cameraStates;

	[Token(Token = "0x4000733")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private IList<ScreenState> _screenStates;

	[Token(Token = "0x60006A1")]
	[Address(RVA = "0x39D3A0", Offset = "0x39BDA0", VA = "0x18039D3A0", Slot = "9")]
	public Naninovel.UniTask InitializeService()
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x60006A2")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "10")]
	public void ResetService()
	{
	}

	[Token(Token = "0x60006A3")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "11")]
	public void DestroyService()
	{
	}

	[Token(Token = "0x60006A4")]
	[Address(RVA = "0x39D5C0", Offset = "0x39BFC0", VA = "0x18039D5C0", Slot = "12")]
	public void SaveServiceState(GameStateMap stateMap)
	{
	}

	[Token(Token = "0x60006A5")]
	[Address(RVA = "0x39D8B0", Offset = "0x39C2B0", VA = "0x18039D8B0", Slot = "13")]
	[AsyncStateMachine(typeof(_003CLoadServiceState_003Ed__9))]
	public Naninovel.UniTask LoadServiceState(GameStateMap stateMap)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x60006A6")]
	[Address(RVA = "0x39DA30", Offset = "0x39C430", VA = "0x18039DA30", Slot = "4")]
	public void RegisterCurrentStage(PerspectiveStageKind kind)
	{
	}

	[Token(Token = "0x60006A7")]
	[Address(RVA = "0x39DA40", Offset = "0x39C440", VA = "0x18039DA40", Slot = "5")]
	public void UnregisterCurrentStage()
	{
	}

	[Token(Token = "0x60006A8")]
	[Address(RVA = "0x39DA50", Offset = "0x39C450", VA = "0x18039DA50", Slot = "6")]
	public void RegisterCameraState(int index, float? height, float?[] rotation, float? distance)
	{
	}

	[Token(Token = "0x60006A9")]
	[Address(RVA = "0x39DF80", Offset = "0x39C980", VA = "0x18039DF80", Slot = "7")]
	public void RegisterScreenVisibilityState(int index, bool visible)
	{
	}

	[Token(Token = "0x60006AA")]
	[Address(RVA = "0x39E120", Offset = "0x39CB20", VA = "0x18039E120", Slot = "8")]
	public void RegisterScreenSplitState(int index, float normalizedPosX, float roll, ScreenSplitDirection direction)
	{
	}

	[Token(Token = "0x60006AB")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public TrialStateManager()
	{
	}
}
