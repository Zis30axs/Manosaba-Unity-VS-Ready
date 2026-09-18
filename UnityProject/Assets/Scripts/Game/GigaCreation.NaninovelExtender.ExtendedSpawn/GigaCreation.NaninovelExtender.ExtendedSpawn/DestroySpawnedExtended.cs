using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;

namespace GigaCreation.NaninovelExtender.ExtendedSpawn
{
    [Token(Token = "0x2000004")]
    [UsedImplicitly]
    [CommandAlias("despawn")]
    public class DestroySpawnedExtended : DestroySpawned
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000005")]
    	[CompilerGenerated]
    	private struct _003CDestroy_003Ed__4
    {
    		[Token(Token = "0x4000008")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000009")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400000A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public DestroySpawnedExtended _003C_003E4__this;

    		[Token(Token = "0x400000B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x400000C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private IMultipliableSpawnManager _003CmultipliableSpawnManager_003E5__2;

    		[Token(Token = "0x400000D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000008")]
    		[Address(RVA = "0xD2ABD0", Offset = "0xD295D0", VA = "0x180D2ABD0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000009")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000006")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[UsedImplicitly]
    	public StringParameter SubId;

    	[Token(Token = "0x4000007")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[UsedImplicitly]
    	[ParameterAlias("warn")]
    	[ParameterDefaultValue("true")]
    	public BooleanParameter LogWarning;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x86C880, 130 B (folded with ModifyActor.get_AssignedTransition)
    	// `SubId is null || !SubId.HasValue ? null : SubId.Value` on the +0x48 field == the StringParameter
    	// string implicit operator == `=> SubId;` (the folding partner's package source is `=> Transition;`).
    	// Annotated disassembly: work/spawn-nre/disas/50_get_AssignedSubId.txt
    	[Token(Token = "0x17000001")]
    	private string AssignedSubId
    	{
    		[Token(Token = "0x6000003")]
    		[Address(RVA = "0x86C880", Offset = "0x86B280", VA = "0x18086C880")]
    		get
    		{
    			return SubId;
    		}
    	}

    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xD2A950", Offset = "0xD29350", VA = "0x180D2A950", Slot = "17")]
    	[AsyncStateMachine(typeof(_003CDestroy_003Ed__4))]
    	protected override UniTask Destroy(AsyncToken token)
    	{
    		return DestroyCore(token);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2A950 (kickoff, 384 B) / 0xD2ABD0 (<Destroy>d__4.MoveNext, 2155 B)
    	// State -1, in order:
    	//   0x180D2ACE9  virtual slot (0x238-0x138)/16 = 16 on this -> DestroySpawned.get_SpawnManager (dump.cs Slot 16)
    	//   0x180D2AD09  0x298020 isinst IMultipliableSpawnManager -> <multipliableSpawnManager>5__2 (+0x30)
    	//   0x180D2AD79  null -> 0x180D2B0D0: Start<DestroySpawned.<Destroy>d__8>(this, token) and await it
    	//                == `await base.Destroy(token)`; then fall through to the shared epilogue.
    	//   0x180D2AD8E  StringParameter.op_Implicit(Path [rsi+0x30]); 0x180D2AD9B get_AssignedSubId
    	//   0x180D2ADB7  thunk 0x8000, ecx = 1 -> IMultipliableSpawnManager slot 1 = IsSpawned(path, subId); false ->
    	//     0x180D2B04C  AssignedSubId non-empty ? String.Format("{0}-{1}", Path, AssignedSubId) : String.Format("{0}", Path)
    	//     0x180D2B0AB  String.Concat("Failed to destroy spawned object '", name, "': the object is not found.")
    	//     0x180D2B0C7  virtual slot (0x1A8-0x138)/16 = 7 on this -> Command.Warn(message) (the override below); return
    	//   0x180D2ADF4  thunk 0x801B0 -> slot 2 = GetSpawned(path, subId) -> rbx
    	//   0x180D2AE07  Params ([rsi+0x38]) null ? null : StringListParameter.ToReadOnlyList()
    	//   0x180D2AE23  SpawnedObject$$SetDestroyParameters(rbx, that)
    	//   0x180D2AE3C  SpawnedObject$$AwaitDestroy(rbx, token (+0x20)); await (<>u__1)
    	//   0x180D2AFDB  Path (inline implicit) / get_AssignedSubId again; thunk 0x8000, ecx = 3 -> slot 3 =
    	//                DestroySpawned(path, subId) on the manager saved in +0x30
    	//   0x180D2B33C  <multipliableSpawnManager>5__2 = null (hoisted local cleared at the end); SetResult
    	// April semantics kept: the object is removed from the map (and destroyed by MultipliableSpawn)
    	// AFTER AwaitDestroy completes; the package's newer DestroySpawned removes it first with dispose: false.
    	// Annotated disassembly: work/spawn-nre/disas/51_Destroy.txt, 52_Destroy_MoveNext.txt
    	private async UniTask DestroyCore(AsyncToken token)
    	{
    		var multipliableSpawnManager = SpawnManager as IMultipliableSpawnManager;
    		if (multipliableSpawnManager == null)
    		{
    			await base.Destroy(token);
    			return;
    		}
    		if (!multipliableSpawnManager.IsSpawned(Path, AssignedSubId))
    		{
    			string name = !string.IsNullOrEmpty(AssignedSubId)
    				? string.Format("{0}-{1}", Path, AssignedSubId)
    				: string.Format("{0}", Path);
    			Warn("Failed to destroy spawned object '" + name + "': the object is not found.");
    			return;
    		}
    		var spawned = multipliableSpawnManager.GetSpawned(Path, AssignedSubId);
    		spawned.SetDestroyParameters(Params?.ToReadOnlyList());
    		await spawned.AwaitDestroy(token);
    		multipliableSpawnManager.DestroySpawned(Path, AssignedSubId);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD2AAD0, 247 B
    	//   0x180D2AB11  Naninovel.Nullable<bool>.op_Implicit(LogWarning [rbx+0x50]) -> bool (returned through the
    	//                hidden pointer [rsp+0x70]); 0x180D2AB16 cmp byte,0; je return   -> `if (LogWarning)`
    	//   0x180D2AB6E  movups [rbx+0x18] = Command.playbackSpot -> new System.Nullable<PlaybackSpot>(spot)
    	//   0x180D2ABB7  Naninovel.Engine$$Warn(message, spot)  -- the package's Command.Warn body inlined
    	// Annotated disassembly: work/spawn-nre/disas/53_Warn.txt
    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xD2AAD0", Offset = "0xD294D0", VA = "0x180D2AAD0", Slot = "7")]
    	public override void Warn(string message)
    	{
    		if (LogWarning) Engine.Warn(message, PlaybackSpot);
    	}

    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0x4E9A40", Offset = "0x4E8440", VA = "0x1804E9A40")]
    	public DestroySpawnedExtended()
    	{
    	}
    }
}
