using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GigaCreation.NaninovelExtender.Cameras;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedSpawn
{
    [Token(Token = "0x2000006")]
    [UsedImplicitly]
    [CommandAlias("spawn")]
    public class SpawnExtended : Spawn
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000007")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__6
    {
    		[Token(Token = "0x4000010")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000011")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000012")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public SpawnExtended _003C_003E4__this;

    		[Token(Token = "0x4000013")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x4000014")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<SpawnedObject>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000015")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600000F")]
    		[Address(RVA = "0xD2BBD0", Offset = "0xD2A5D0", VA = "0x180D2BBD0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000010")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400000E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	[UsedImplicitly]
    	public StringParameter SubId;

    	[Token(Token = "0x400000F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	[UsedImplicitly]
    	[ParameterAlias("match")]
    	public BooleanParameter MatchScreenWidth;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2B440, 144 B
    	// `SubId is null || !SubId.HasValue ? null : SubId.Value` -- byte-for-byte the body of
    	// StringParameter.op_Implicit(StringParameter) inlined on the +0x68 field, i.e. `=> SubId;`.
    	// (DestroySpawnedExtended.get_AssignedSubId, 0x86C880, is the same shape and is folded with
    	// ModifyActor.get_AssignedTransition, whose package source is `=> Transition;`.)
    	// Annotated disassembly: work/spawn-nre/disas/30_get_AssignedSubId.txt
    	[Token(Token = "0x17000002")]
    	private string AssignedSubId
    	{
    		[Token(Token = "0x600000A")]
    		[Address(RVA = "0xD2B440", Offset = "0xD29E40", VA = "0x180D2B440")]
    		get
    		{
    			return SubId;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2B4D0, 96 B
    	//   0x180D2B4FA  virtual slot (0x258-0x138)/16 = 18 on this -> Spawn.get_SpawnManager (dump.cs Slot 18)
    	//   0x180D2B51A  tail call 0x298020 with IMultipliableSpawnManager_TypeInfo: returns the object when it
    	//                implements the interface, otherwise null -> the `as` operator (a cast would throw).
    	// Annotated disassembly: work/spawn-nre/disas/31_get_MultipliableManager.txt
    	[Token(Token = "0x17000003")]
    	private IMultipliableSpawnManager MultipliableManager
    	{
    		[Token(Token = "0x600000B")]
    		[Address(RVA = "0xD2B4D0", Offset = "0xD29ED0", VA = "0x180D2B4D0")]
    		get
    		{
    			return SpawnManager as IMultipliableSpawnManager;
    		}
    	}

    	[Token(Token = "0x600000C")]
    	[Address(RVA = "0xD2B530", Offset = "0xD29F30", VA = "0x180D2B530", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__6))]
    	public override UniTask Execute([Optional] AsyncToken asyncToken)
    	{
    		// The recovered body lives in ExecuteCore (see the project's async reconstruction convention in
    		// AddressableResourceProviderExtended / Overlay).
    		return ExecuteCore(asyncToken);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2B530 (kickoff, 400 B) / 0xD2BBD0 (<Execute>d__6.MoveNext, 1689 B)
    	// State -1, in order (this is the package's Spawn.Execute with the sub-id threaded through):
    	//   0x180D2BD04  virtual slot 18 on this (get_SpawnManager) + 0x298020 isinst IMultipliableSpawnManager
    	//                -> the MultipliableManager getter inlined -> r15
    	//   0x180D2BD2C  [rbx+0x30] = Path: HasValue/Value inline (= the string implicit operator) -> r14
    	//   0x180D2BDA5  [rbx+0x68] = SubId: same inline (= AssignedSubId) -> r9
    	//   0x180D2BE45  MultipliableSpawnManagerExtensions$$GetOrSpawnAsync(r15, path, subId, asyncToken (+0x20))
    	//   await (<>u__1); result -> rsi
    	//   0x180D2BF12  slot (0x288-0x138)/16 = 21 ApplyParameters(rsi)   (dump.cs Spawn Slot 21)
    	//   0x180D2BF33  slot 22 ApplyScenePosition(rsi)  -- the override below
    	//   0x180D2BF4C  slot 23 ApplyPosition(rsi)
    	//   0x180D2BF65  slot 24 ApplyRotation(rsi)
    	//   0x180D2BF7E  slot 25 ApplyScale(rsi)
    	//   0x180D2BF9B  new Func<AsyncToken, UniTask>(rsi, SpawnedObject.AwaitSpawn)
    	//   0x180D2BFBB  slot (0x208-0x138)/16 = 13 WaitOrForget(fn, Wait ([rbx+0x60]), asyncToken); await (<>u__2)
    	// Annotated disassembly: work/spawn-nre/disas/32_Execute.txt, 33_Execute_MoveNext.txt
    	private async UniTask ExecuteCore(AsyncToken asyncToken)
    	{
    		var manager = MultipliableManager;
    		var spawn = await manager.GetOrSpawnAsync(Path, AssignedSubId, asyncToken);
    		ApplyParameters(spawn);
    		ApplyScenePosition(spawn);
    		ApplyPosition(spawn);
    		ApplyRotation(spawn);
    		ApplyScale(spawn);
    		await WaitOrForget(spawn.AwaitSpawn, Wait, asyncToken);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2B6C0, 1290 B
    	// The package's Spawn.ApplyScenePosition, but the scene rect comes from
    	// CameraConfigurationUtility.EvaluateSceneRect(MatchScreenWidth) instead of the camera configuration:
    	//   0x180D2B73D  [rbx+0x40] = ScenePosition null? / ICommandParameter.get_HasValue (thunk 0x22E0) false -> return
    	//   0x180D2B785  BooleanParameter.op_Implicit(MatchScreenWidth [rbx+0x70]) -> System.Nullable<bool> (dx)
    	//   0x180D2B794  CameraConfigurationUtility$$EvaluateSceneRect(&rect, dx) -> xmm8 = (x, y, w, h)
    	//   X: 0x180D2B7CE ElementAtOrDefault<NullableFloat>(ScenePosition, 0) != null ->
    	//        0x180D2B7F0 ScenePosition[0] (ParameterList.get_Item + Nullable<float>.op_Implicit) -> xmm7
    	//        0x180D2B811 divss 100.0 (0x2AE2BC4); 0x180D2B819 shufps 0xAA (= rect.width); mulss; 0x180D2B822 addss rect.x
    	//      else 0x180D2B83B spawnedObject.Transform (GameObject$$get_transform) .position.x (icall 0x30E8D80)
    	//   Y: same with element 1, shufps 0xFF (= rect.height) and shufps 0x55 (= rect.y)
    	//   Z: 0x180D2B9D4 ElementAtOrDefault(2) ?? transform.position.z (NullableFloat$$op_Implicit(float) + Nullable<float>.op_Implicit)
    	//   0x180D2BB05  Transform.set_position(new Vector3(x, y, z)) (icall 0x30E8D88)
    	// `rect.x + v / 100f * rect.width` is CameraConfiguration.SceneToWorldSpace(new Vector2(v / 100f, 0), rect).x
    	// spelled out; the arithmetic is reproduced as emitted.
    	// Annotated disassembly: work/spawn-nre/disas/34_ApplyScenePosition.txt
    	[Token(Token = "0x600000D")]
    	[Address(RVA = "0xD2B6C0", Offset = "0xD2A0C0", VA = "0x180D2B6C0", Slot = "22")]
    	protected override void ApplyScenePosition(SpawnedObject spawnedObject)
    	{
    		if (!Assigned(ScenePosition)) return;
    		Rect sceneRect = CameraConfigurationUtility.EvaluateSceneRect(MatchScreenWidth);
    		spawnedObject.Transform.position = new Vector3(
    			ScenePosition.ElementAtOrDefault(0) != null
    				? sceneRect.x + ScenePosition[0] / 100f * sceneRect.width
    				: spawnedObject.Transform.position.x,
    			ScenePosition.ElementAtOrDefault(1) != null
    				? sceneRect.y + ScenePosition[1] / 100f * sceneRect.height
    				: spawnedObject.Transform.position.y,
    			ScenePosition.ElementAtOrDefault(2) ?? spawnedObject.Transform.position.z);
    	}

    	[Token(Token = "0x600000E")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public SpawnExtended()
    	{
    	}
    }
}
