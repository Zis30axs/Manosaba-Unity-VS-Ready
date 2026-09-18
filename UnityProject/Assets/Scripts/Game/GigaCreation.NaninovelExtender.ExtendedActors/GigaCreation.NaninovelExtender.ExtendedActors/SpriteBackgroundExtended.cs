using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x2000030")]
    [UsedImplicitly]
    [ActorResources(typeof(Texture2D), true)]
    public class SpriteBackgroundExtended : SpriteBackground, IMultipliableActor, IPivotChangeableActor, IMaskabilityChangeableActor
    {
    	// RECONSTRUCTED | Confidence: A | RVA: 0x492CE0, 8 B (folded)
    	// Get-only auto-property (`IMultipliableActor.SubId`, dump.cs Slot 73): the getter is the trivial
    	// `[CompilerGenerated]` read `mov rax,[rcx+0xB8]; ret` of the backing field
    	// `<SubId>k__BackingField` (+0xB8). `get;` instead of the stub `return null;` so the recovered
    	// 4-argument constructor can assign the backing field.
    	[Token(Token = "0x17000010")]
    	public string SubId
    	{
    		[Token(Token = "0x600009F")]
    		[Address(RVA = "0x492CE0", Offset = "0x4916E0", VA = "0x180492CE0", Slot = "73")]
    		[CompilerGenerated]
    		get;
    	}

    	[Token(Token = "0x17000011")]
    	public Vector2 Pivot
    	{
    		[Token(Token = "0x60000A0")]
    		[Address(RVA = "0xD1CE90", Offset = "0xD1B890", VA = "0x180D1CE90", Slot = "74")]
    		get
    		{
    			return default(Vector2);
    		}
    		[Token(Token = "0x60000A1")]
    		[Address(RVA = "0xD1CED0", Offset = "0xD1B8D0", VA = "0x180D1CED0", Slot = "75")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000012")]
    	public bool Masked
    	{
    		[Token(Token = "0x60000A2")]
    		[Address(RVA = "0xD1E410", Offset = "0xD1CE10", VA = "0x180D1E410", Slot = "77")]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x60000A3")]
    		[Address(RVA = "0xD1E510", Offset = "0xD1CF10", VA = "0x180D1E510", Slot = "78")]
    		set
    		{
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x46C8C0, 114 B
    	// ABI: rcx=__this, rdx=id, r8=metadata, r9=loader (+ MethodInfo*).
    	// The whole body is a tail call: `mov [rsp+0x60],MethodInfo; jmp 0x18091f350` ->
    	// Naninovel.SpriteActor<BackgroundMetadata>$$.ctor with rdx=id, r8=metadata, r9=loader, i.e. the
    	// inlined `Naninovel.SpriteBackground..ctor(id, meta, loader)` (dump.cs RVA 0x46C8C0 -- the same
    	// address, because that base ctor is nothing but this base call, so the bodies are fold-identical)
    	// == base(id, metadata, loader). No other statement is emitted.
    	[Token(Token = "0x60000A4")]
    	[Address(RVA = "0x46C8C0", Offset = "0x46B2C0", VA = "0x18046C8C0")]
    	public SpriteBackgroundExtended(string id = default, BackgroundMetadata metadata = default, StandaloneAppearanceLoader<Texture2D> loader = default)
    		: base(id, metadata, loader)
    	{
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x313980, 230 B
    	// ABI: rcx=__this, rdx=actorId, r8=subId, r9=metadata, 5th (loader) = [rsp+0x60] (after `push rdi` +
    	// `sub rsp,0x30`). rdi=__this, rsi=actorId, rbx=subId, rbp=metadata.
    	// 1. First base argument: the id derived from (actorId, subId) -- `test rbx,rbx` / `je L`, then
    	//    `cmp dword ptr [rbx+0x10],0` / `jbe L` (`string.IsNullOrEmpty(subId)`) then
    	//    `System.String$$Concat(actorId, "-", subId)`, otherwise actorId unchanged. Same inlined code as
    	//    `IMultipliableActor.BuildId` (RVA 0xD18B00); see the identical note in LayeredCharacterExtended.
    	// 2. `call 0x18046c8c0` -> SpriteBackground..ctor with rdx=id, r8=rbp=metadata, r9=[rsp+0x60]=loader,
    	//    [rsp+0x20]=MethodInfo* == base(id, metadata, loader). NOTE: 0x18046C8C0 is simultaneously this
    	//    class's 3-argument ctor (fold-identical to the base ctor), so the alternative spelling
    	//    `: this(id, metadata, loader)` emits the very same call -- the two are equivalent and the bytes
    	//    do not discriminate. The sibling DicedSpriteCharacterExtended (same family, guarded out of the
    	//    build) proves the chaining spelling for itself: its 4-argument ctor calls its own 3-argument ctor
    	//    (`call 0x180D19540`, RVA 0xD19BC0) after the same inlined BuildId. `: base(...)` is kept here.
    	//    TODO(evidence): call-vs-inline spelling only.
    	// 3. `mov [rdi+0xB8], rbx` (+ GC write barrier) -> this.<SubId>k__BackingField (+0xB8) = subId.
    	[Token(Token = "0x60000A5")]
    	[Address(RVA = "0x313980", Offset = "0x312380", VA = "0x180313980")]
    	public SpriteBackgroundExtended(string actorId, string subId, BackgroundMetadata metadata, StandaloneAppearanceLoader<Texture2D> loader)
    		: base(string.IsNullOrEmpty(subId) ? actorId : actorId + "-" + subId, metadata, loader)
    	{
    		SubId = subId;
    	}

    	[Token(Token = "0x60000A6")]
    	[Address(RVA = "0xD1E620", Offset = "0xD1D020", VA = "0x180D1E620", Slot = "76")]
    	public void ChangePivot(float? x, float? y)
    	{
    	}

    	[Token(Token = "0x60000A7")]
    	[Address(RVA = "0xD1E510", Offset = "0xD1CF10", VA = "0x180D1E510", Slot = "79")]
    	public void ChangeMaskability(bool enable)
    	{
    	}

    	[Token(Token = "0x60000A8")]
    	[Address(RVA = "0xD1E410", Offset = "0xD1CE10", VA = "0x180D1E410")]
    	private bool IsMasked()
    	{
    		return default(bool);
    	}
    }
}
