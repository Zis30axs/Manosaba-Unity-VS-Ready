using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x200002C")]
    [UsedImplicitly]
    [ActorResources(typeof(LayeredCharacterBehaviour), false)]
    public class LayeredCharacterExtended : LayeredCharacter, IMultipliableActor, IPivotChangeableActor, IMaskabilityChangeableActor
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200002D")]
    	[CompilerGenerated]
    	private struct _003CInitialize_003Ed__13
    {
    		[Token(Token = "0x4000099")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400009A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400009B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public LayeredCharacterExtended _003C_003E4__this;

    		[Token(Token = "0x400009C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000095")]
    		[Address(RVA = "0xD1D690", Offset = "0xD1C090", VA = "0x180D1D690", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000096")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1CE80, 8 B
    	// Get-only auto-property (`IMultipliableActor.SubId`, dump.cs Slot 74): the getter is the trivial
    	// `[CompilerGenerated]` read `mov rax,[rcx+0xC0]; ret` of the backing field
    	// `<SubId>k__BackingField` (+0xC0). `get;` instead of the stub `return null;` so the recovered
    	// 4-argument constructor can assign the backing field.
    	[Token(Token = "0x1700000D")]
    	public string SubId
    	{
    		[Token(Token = "0x6000089")]
    		[Address(RVA = "0xD1CE80", Offset = "0xD1B880", VA = "0x180D1CE80", Slot = "74")]
    		[CompilerGenerated]
    		get;
    	}

    	[Token(Token = "0x1700000E")]
    	public Vector2 Pivot
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD1CE90, 60 B (folded with SpriteBackgroundExtended.Pivot's getter)
    		// TransitionalRenderer (vtable +0x538 = slot 64, LayeredActor.get_TransitionalRenderer), then a tail jump to its slot 23 = get_Pivot.
    		// Annotated disassembly: work/title-path/disas/lce_03_get_Pivot.txt
    		[Token(Token = "0x600008A")]
    		[Address(RVA = "0xD1CE90", Offset = "0xD1B890", VA = "0x180D1CE90", Slot = "75")]
    		get
    		{
    			return TransitionalRenderer.Pivot;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD1CED0, 80 B (folded with SpriteBackgroundExtended.Pivot's setter)
    		// TransitionalRenderer (slot 64), then a tail jump to its slot 24 = set_Pivot with the Vector2 in xmm6 -> rdx.
    		// Annotated disassembly: work/title-path/disas/lce_04_set_Pivot.txt
    		[Token(Token = "0x600008B")]
    		[Address(RVA = "0xD1CED0", Offset = "0xD1B8D0", VA = "0x180D1CED0", Slot = "76")]
    		set
    		{
    			TransitionalRenderer.Pivot = value;
    		}
    	}

    	[Token(Token = "0x1700000F")]
    	public bool Masked
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD1CF30, 243 B (folded with IsMasked: the getter is IsMasked inlined)
    		// Annotated disassembly: work/title-path/disas/lce_05_get_Masked_IsMasked.txt
    		[Token(Token = "0x600008C")]
    		[Address(RVA = "0xD1CF30", Offset = "0xD1B930", VA = "0x180D1CF30", Slot = "78")]
    		get
    		{
    			return IsMasked();
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD1D030, 258 B (folded with ChangeMaskability)
    		// Annotated disassembly: work/title-path/disas/lce_06_set_Masked_ChangeMaskability.txt
    		[Token(Token = "0x600008D")]
    		[Address(RVA = "0xD1D030", Offset = "0xD1BA30", VA = "0x180D1D030", Slot = "79")]
    		set
    		{
    			ChangeMaskability(value);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x461C60, 114 B
    	// ABI: rcx=__this, rdx=id, r8=meta, r9=loader (+ MethodInfo*).
    	// The whole body is a tail call: `mov [rsp+0x60],MethodInfo; jmp 0x1808489a0` ->
    	// Naninovel.LayeredActor<LayeredCharacterBehaviour, CharacterMetadata>$$.ctor with rdx=id, r8=meta,
    	// r9=loader, i.e. the inlined `Naninovel.LayeredCharacter..ctor(id, meta, loader)` (dump.cs RVA
    	// 0x461C60 -- the same address, because that base ctor is itself nothing but this base call, so the
    	// two bodies are fold-identical) == base(id, meta, loader). No other statement is emitted.
    	[Token(Token = "0x600008E")]
    	[Address(RVA = "0x461C60", Offset = "0x460660", VA = "0x180461C60")]
    	public LayeredCharacterExtended(string id, CharacterMetadata meta, EmbeddedAppearanceLoader<GameObject> loader)
    		: base(id, meta, loader)
    	{
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1D140, 262 B
    	// ABI: rcx=__this, rdx=actorId, r8=subId, r9=meta, 5th (loader) = [rsp+0x60] (after `push rdi` +
    	// `sub rsp,0x30`). rdi=__this, rsi=actorId, rbx=subId, rbp=meta.
    	// 1. The first base argument is the id derived from (actorId, subId): `test rbx,rbx` / `je L`, then
    	//    `cmp dword ptr [rbx+0x10],0` / `jbe L` (that is `string.IsNullOrEmpty(subId)`, string length at
    	//    +0x10), then `System.String$$Concat(actorId, "-", subId)`; otherwise actorId is kept as-is.
    	//    This is byte-for-byte the inlined body of `IMultipliableActor.BuildId(string, string)`
    	//    (RVA 0xD18B00, 105 B: `if (string.IsNullOrEmpty(subId)) return actorId;
    	//    return String.Concat(actorId, "-", subId);`). BuildId has NO direct call site anywhere in
    	//    .text (every `E8 rel32` resolving to 0x180D18B00 was scanned: 0 hits), i.e. it is inlined at
    	//    all of its call sites -- this constructor included.
    	//    The expansion is written inline rather than as `IMultipliableActor.BuildId(actorId, subId)`
    	//    only because the reconstructed BuildId is still a stub that returns null, which would pass a
    	//    null id to the base ctor at runtime. The behaviour is identical.
    	//    TODO(evidence): the source spelling (call vs. inline) is not discriminable from these bytes.
    	// 2. `call 0x1808489a0` -> LayeredActor<LayeredCharacterBehaviour, CharacterMetadata>$$.ctor with
    	//    rdx=id, r8=rbp=meta, r9=[rsp+0x60]=loader, [rsp+0x20]=MethodInfo*, i.e. the inlined
    	//    `LayeredCharacter(id, meta, loader)` == base(id, meta, loader).
    	// 3. `mov [rdi+0xC0], rbx` (+ GC write barrier) -> this.<SubId>k__BackingField (+0xC0) = subId.
    	[Token(Token = "0x600008F")]
    	[Address(RVA = "0xD1D140", Offset = "0xD1BB40", VA = "0x180D1D140")]
    	public LayeredCharacterExtended(string actorId, string subId, CharacterMetadata meta, EmbeddedAppearanceLoader<GameObject> loader)
    		: base(string.IsNullOrEmpty(subId) ? actorId : actorId + "-" + subId, meta, loader)
    	{
    		SubId = subId;
    	}

    	// RECONSTRUCTED | Confidence: B | RVA: 0xD1D250, 791 B (folded with DicedSpriteCharacterExtended.ChangePivot; renderer slot 64 here)
    	// Each coordinate is x.GetValueOrDefault() when HasValue, otherwise the coordinate of the vtable +0x328 = slot 31 getter, i.e.
    	// Position (recorded as shipped, see DicedSpriteCharacterExtended.ChangePivot); the result goes to the renderer's slot 24 set_Pivot.
    	// Annotated disassembly: work/title-path/disas/lce_07_ChangePivot.txt
    	[Token(Token = "0x6000090")]
    	[Address(RVA = "0xD1D250", Offset = "0xD1BC50", VA = "0x180D1D250", Slot = "77")]
    	public void ChangePivot(float? x, float? y)
    	{
    		TransitionalRenderer.Pivot = new Vector2(x ?? Position.x, y ?? Position.y);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1D030, 258 B
    	// Identical (folded) to DicedSpriteCharacterExtended's 0xD191E0 / 0xD192E0 with the LayeredActor renderer accessor (vtable +0x538 = slot 64):
    	// TransitionalRenderer `as TransitionalSpriteRenderer`, its SpriteMaterial (vtable +0x3E8, slot 43) and the StencilComp property id:
    	// masked is Stencil Comparison 3 (Equal, 0x2AE29B8), unmasked 8 (Always, 0x2AE2AA4).
    	[Token(Token = "0x6000091")]
    	[Address(RVA = "0xD1D030", Offset = "0xD1BA30", VA = "0x180D1D030", Slot = "80")]
    	public void ChangeMaskability(bool enable)
    	{
    		if (TransitionalRenderer is TransitionalSpriteRenderer spriteRenderer)
    		{
    			spriteRenderer.SpriteMaterial.SetFloat(IMaskabilityChangeableActor.ShaderPropertyIds.StencilComp, enable ? 3f : 8f);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1D570 (kickoff) / 0xD1D690 (<Initialize>d__13.MoveNext, 1116 B)
    	// The body lives in InitializeCore (XxxCore convention; the <Initialize>d__13 metadata stays).  The stub returned
    	// default(UniTask): LayeredCharacter.Initialize (behaviour prefab instantiation, lip syncer) never ran, so every layered
    	// character (Ema, Hiro, ... 13 of them) was an empty shell after CharacterManagerExtended.ConstructActor.
    	//   state 0   await base.Initialize() -- the MoveNext starts LayeredCharacter.<Initialize>d__5 inline (0x180D1D812) and awaits
    	//             its task (0x180D1D81E)
    	//   0x180D1DA3C  Behaviour (vtable +0x528 = slot 63, LayeredActor.get_Behaviour) .transform (Component.get_transform) is re-parented
    	//             with Transform.SetParent(GetOrCreateParent() (vtable +0x508 = slot 61, MonoBehaviourActor), worldPositionStays: true)
    	//             (0x180D1DA7C)
    	// Annotated disassembly: work/title-path/disas/lce_01_Initialize, lce_02_Initialize_MoveNext.txt
    	[Token(Token = "0x6000092")]
    	[Address(RVA = "0xD1D570", Offset = "0xD1BF70", VA = "0x180D1D570", Slot = "41")]
    	[AsyncStateMachine(typeof(_003CInitialize_003Ed__13))]
    	public override UniTask Initialize()
    	{
    		return InitializeCore();
    	}

    	private async UniTask InitializeCore()
    	{
    		await base.Initialize();
    		Behaviour.transform.SetParent(GetOrCreateParent(), true);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1CF30, 243 B
    	// Identical (folded) to DicedSpriteCharacterExtended's 0xD191E0 / 0xD192E0 with the LayeredActor renderer accessor (vtable +0x538 = slot 64):
    	// TransitionalRenderer `as TransitionalSpriteRenderer`, its SpriteMaterial (vtable +0x3E8, slot 43) and the StencilComp property id:
    	// masked is Stencil Comparison 3 (Equal, 0x2AE29B8), unmasked 8 (Always, 0x2AE2AA4).
    	[Token(Token = "0x6000093")]
    	[Address(RVA = "0xD1CF30", Offset = "0xD1B930", VA = "0x180D1CF30")]
    	private bool IsMasked()
    	{
    		if (TransitionalRenderer is TransitionalSpriteRenderer spriteRenderer)
    		{
    			return Mathf.Approximately(spriteRenderer.SpriteMaterial.GetFloat(IMaskabilityChangeableActor.ShaderPropertyIds.StencilComp), 3f);
    		}
    		return false;
    	}
    }
}
