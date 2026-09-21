// `Naninovel.DicedSpriteCharacter` exists because the embedded package `com.elringus.spritedicing` (Packages/, the runtime subset
// the shipped build still carries: SpriteDicing.DicedSpriteAtlas) defines SPRITE_DICING_AVAILABLE for Elringus.Naninovel.Runtime.
// Earlier revisions of this file were wrapped in `#if SPRITE_DICING_AVAILABLE` because that package was absent (recorded gap
// closed on 2026-09-22: Yuki, Warden, Jailer* and the 13 Creature* characters use this actor type).
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using SpriteDicing;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x2000027")]
    [UsedImplicitly]
    [ActorResources(typeof(DicedSpriteAtlas), false)]
    public class DicedSpriteCharacterExtended : DicedSpriteCharacter, IMultipliableActor, IPositionChangeableForEachCoordinateActor, IPivotChangeableActor, IMaskabilityChangeableActor
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000028")]
    	[CompilerGenerated]
    	private struct _003CChangePosition_003Ed__32
    {
    		[Token(Token = "0x4000079")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400007A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400007B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public DicedSpriteCharacterExtended _003C_003E4__this;

    		[Token(Token = "0x400007C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public Vector3 position;

    		[Token(Token = "0x400007D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
    		public Tween tween;

    		[Token(Token = "0x400007E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x400007F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000081")]
    		[Address(RVA = "0xD1BB10", Offset = "0xD1A510", VA = "0x180D1BB10", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000082")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000029")]
    	[CompilerGenerated]
    	private struct _003CChangePositionXAsync_003Ed__27
    {
    		[Token(Token = "0x4000080")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000081")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000082")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public DicedSpriteCharacterExtended _003C_003E4__this;

    		[Token(Token = "0x4000083")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float positionX;

    		[Token(Token = "0x4000084")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public float duration;

    		[Token(Token = "0x4000085")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public EasingType easingType;

    		[Token(Token = "0x4000086")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x4000087")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000083")]
    		[Address(RVA = "0xD1BFC0", Offset = "0xD1A9C0", VA = "0x180D1BFC0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000084")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200002A")]
    	[CompilerGenerated]
    	private struct _003CChangePositionYAsync_003Ed__28
    {
    		[Token(Token = "0x4000088")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000089")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400008A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public DicedSpriteCharacterExtended _003C_003E4__this;

    		[Token(Token = "0x400008B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float positionY;

    		[Token(Token = "0x400008C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public float duration;

    		[Token(Token = "0x400008D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public EasingType easingType;

    		[Token(Token = "0x400008E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x400008F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000085")]
    		[Address(RVA = "0xD1C4B0", Offset = "0xD1AEB0", VA = "0x180D1C4B0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000086")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200002B")]
    	[CompilerGenerated]
    	private struct _003CChangePositionZAsync_003Ed__29
    {
    		[Token(Token = "0x4000090")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000091")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000092")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public DicedSpriteCharacterExtended _003C_003E4__this;

    		[Token(Token = "0x4000093")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float positionZ;

    		[Token(Token = "0x4000094")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    		public float duration;

    		[Token(Token = "0x4000095")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public EasingType easingType;

    		[Token(Token = "0x4000096")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken asyncToken;

    		[Token(Token = "0x4000097")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000087")]
    		[Address(RVA = "0xD1C9A0", Offset = "0xD1B3A0", VA = "0x180D1C9A0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000088")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400006D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
    	// Field initializers of the shipped constructor (0x180D1965E ff.), before the base call: the four tweeners and `_vertices`
    	// (the Il2CppDumper skeleton had dropped them).
    	private readonly Tweener<FloatTween> _positionXTweener = new Tweener<FloatTween>();

    	[Token(Token = "0x400006E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
    	private readonly Tweener<VectorTween> _positionTweener = new Tweener<VectorTween>();

    	[Token(Token = "0x400006F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
    	private readonly Tweener<FloatTween> _positionYTweener = new Tweener<FloatTween>();

    	[Token(Token = "0x4000070")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
    	private readonly Tweener<FloatTween> _positionZTweener = new Tweener<FloatTween>();

    	[Token(Token = "0x4000071")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
    	private readonly OrthoActorMetadata _meta;

    	[Token(Token = "0x4000072")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
    	private readonly Material _renderMaterial;

    	[Token(Token = "0x4000073")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
    	private readonly Mesh _renderMesh;

    	[Token(Token = "0x4000074")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
    	private readonly List<Vector3> _vertices = new List<Vector3>();

    	[Token(Token = "0x4000075")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
    	private readonly float? _scaleCoefficient;

    	[Token(Token = "0x4000076")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
    	private Vector3 _position;

    	[Token(Token = "0x4000077")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x120")]
    	private ICharacterManagerExtended _naniCharacterManager;

    	[Token(Token = "0x17000008")]
    	public string SubId
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD19130, 8 B
    		// Get-only auto-property (`IMultipliableActor.SubId`): the trivial `mov rax,[rcx+0x128]; ret` of `<SubId>k__BackingField` (+0x128);
    		// `get;` so that the recovered 4-argument constructor can assign it.  Annotated disassembly: work/title-path/disas/dsce_01_get_SubId.txt
    		[Token(Token = "0x6000064")]
    		[Address(RVA = "0xD19130", Offset = "0xD17B30", VA = "0x180D19130", Slot = "75")]
    		[CompilerGenerated]
    		get;
    	}

    	[Token(Token = "0x17000009")]
    	public Vector2 Pivot
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD19140, 60 B
    		// TransitionalRenderer (vtable +0x528 = slot 63, DicedSpriteActor.get_TransitionalRenderer), tail jump to its slot 23 = get_Pivot.
    		// Annotated disassembly: work/title-path/disas/dsce_02_get_Pivot.txt
    		[Token(Token = "0x6000065")]
    		[Address(RVA = "0xD19140", Offset = "0xD17B40", VA = "0x180D19140", Slot = "79")]
    		get
    		{
    			return TransitionalRenderer.Pivot;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD19180, 80 B
    		// TransitionalRenderer (slot 63), tail jump to its slot 24 = set_Pivot.
    		// Annotated disassembly: work/title-path/disas/dsce_03_set_Pivot.txt
    		[Token(Token = "0x6000066")]
    		[Address(RVA = "0xD19180", Offset = "0xD17B80", VA = "0x180D19180", Slot = "80")]
    		set
    		{
    			TransitionalRenderer.Pivot = value;
    		}
    	}

    	[Token(Token = "0x1700000A")]
    	public bool Masked
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD191E0, 243 B (folded with IsMasked below: the getter is IsMasked inlined)
    		// Annotated disassembly: work/title-path/disas/dsce_04_get_Masked_IsMasked.txt
    		[Token(Token = "0x6000067")]
    		[Address(RVA = "0xD191E0", Offset = "0xD17BE0", VA = "0x180D191E0", Slot = "82")]
    		get
    		{
    			return IsMasked();
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD192E0, 258 B (folded with ChangeMaskability below)
    		// Annotated disassembly: work/title-path/disas/dsce_05_set_Masked_ChangeMaskability.txt
    		[Token(Token = "0x6000068")]
    		[Address(RVA = "0xD192E0", Offset = "0xD17CE0", VA = "0x180D192E0", Slot = "83")]
    		set
    		{
    			ChangeMaskability(value);
    		}
    	}

    	[Token(Token = "0x1700000B")]
    	public override Vector3 Position
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD193F0, 25 B -- the 12 bytes of `_position` (+0x110) copied to the return slot.
    		// Annotated disassembly: work/title-path/disas/dsce_06_get_Position.txt
    		[Token(Token = "0x6000069")]
    		[Address(RVA = "0xD193F0", Offset = "0xD17DF0", VA = "0x180D193F0", Slot = "31")]
    		get
    		{
    			return _position;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD19410, 92 B
    		// All four position tweeners are completed (0x180D1BA50), `_position` (+0x110) is stored, then the virtual SetBehaviourPosition
    		// (vtable +0x468 = slot 51) applies it -- MonoBehaviourActor.Position's setter with the extended tweeners.
    		// Annotated disassembly: work/title-path/disas/dsce_07_set_Position.txt
    		[Token(Token = "0x600006A")]
    		[Address(RVA = "0xD19410", Offset = "0xD17E10", VA = "0x180D19410", Slot = "32")]
    		set
    		{
    			CompleteAllPositionTweeners();
    			_position = value;
    			SetBehaviourPosition(value);
    		}
    	}

    	[Token(Token = "0x1700000C")]
    	private ICharacterManagerExtended NaniCharacterManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD19470, 202 B -- lazy: [this+0x120], else Engine.GetServiceOrErr<ICharacterManagerExtended>() (0x180682840)
    		// Annotated disassembly: work/title-path/disas/dsce_08_get_NaniCharacterManager.txt
    		[Token(Token = "0x600006B")]
    		[Address(RVA = "0xD19470", Offset = "0xD17E70", VA = "0x180D19470")]
    		get
    		{
    			return _naniCharacterManager ??= Engine.GetServiceOrErr<ICharacterManagerExtended>();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD19540, 1653 B
    	// Field initializers first (four Tweeners, `_vertices`), then the inlined base constructor chain DicedSpriteCharacter -> DicedSpriteActor<
    	// CharacterMetadata> (0x180C54940, the package's private renderMaterial / renderMesh are its own), then this class's body in this order:
    	//  +0xE8 _meta = meta;
    	//  `_scaleCoefficient` (+0x108): when it has no value yet (HasValue, 0x180D19941) and NaniCharacterManager.ScaleCoefficients (interface
    	//  slot 0 of ICharacterManagerExtended, IReadOnlyDictionary<string, float> slot 1 TryGetValue) knows the character id -> the coefficient;
    	//  +0xF0 _renderMaterial = new Material(Shader.Find("Sprites/Default")) { hideFlags = HideAndDontSave (0x3D) };
    	//  +0xF8 _renderMesh = new Mesh { hideFlags = 0x3D, name = id + " Mesh" }; _renderMesh.MarkDynamic();
    	// Annotated disassembly: work/title-path/disas/dsce_09_ctor3.txt
    	[Token(Token = "0x600006C")]
    	[Address(RVA = "0xD19540", Offset = "0xD17F40", VA = "0x180D19540")]
    	public DicedSpriteCharacterExtended(string id, CharacterMetadata meta, EmbeddedAppearanceLoader<DicedSpriteAtlas> loader)
    		: base(id, meta, loader)
    	{
    		_meta = meta;
    		if (!_scaleCoefficient.HasValue && NaniCharacterManager.ScaleCoefficients.TryGetValue(id, out float scaleCoefficient))
    		{
    			_scaleCoefficient = scaleCoefficient;
    		}
    		_renderMaterial = new Material(Shader.Find("Sprites/Default"));
    		_renderMaterial.hideFlags = HideFlags.HideAndDontSave;
    		_renderMesh = new Mesh();
    		_renderMesh.hideFlags = HideFlags.HideAndDontSave;
    		_renderMesh.name = id + " Mesh";
    		_renderMesh.MarkDynamic();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD19BC0, 663 B
    	// The id derived from (id, subId) -- IMultipliableActor.BuildId inlined (String.Concat(id, "-", subId) for a non-empty sub-id,
    	// 0x180D19C76) -- goes through the 3-argument constructor above (`call 0x180D19540`); then `<SubId>k__BackingField` (+0x128) = subId
    	// (0x180D19CA5) and the scale-coefficient lookup is repeated for the plain `id` when the composed id had none (HasValue test
    	// 0x180D19D49, TryGetValue(id) 0x180D19DE7).
    	// Annotated disassembly: work/title-path/disas/dsce_10_ctor4.txt
    	[Token(Token = "0x600006D")]
    	[Address(RVA = "0xD19BC0", Offset = "0xD185C0", VA = "0x180D19BC0")]
    	public DicedSpriteCharacterExtended(string id, string subId, CharacterMetadata meta, EmbeddedAppearanceLoader<DicedSpriteAtlas> loader)
    		: this(string.IsNullOrEmpty(subId) ? id : id + "-" + subId, meta, loader)
    	{
    		SubId = subId;
    		if (!_scaleCoefficient.HasValue && NaniCharacterManager.ScaleCoefficients.TryGetValue(id, out float scaleCoefficient))
    		{
    			_scaleCoefficient = scaleCoefficient;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD19E60 (kickoff) / 0xD1BFC0 (<ChangePositionXAsync>d__27.MoveNext, 1253 B)
    	// The body lives in ChangePositionXAsyncCore (XxxCore convention; the <ChangePositionXAsync>d__27 metadata stays).
    	// MonoBehaviourActor.ChangePosition for one coordinate: the X tweener (+0xC8) is completed (0x180D1B840), `_position`'s x (+0x110) is
    	// replaced, then RunAwaitable(new FloatTween(GetBehaviourPosition().x [vtable +0x458 = slot 50], positionX, new Tween(duration,
    	// easingType) {Scale = true, Complete = true = 0x101}, SetBehaviourPositionX), token, GameObject [vtable +0x3A8 = slot 39]).
    	// Annotated disassembly: work/title-path/disas/dsce_11_ChangePositionXAsync, dsce_12_ChangePositionXAsync_MoveNext.txt
    	[Token(Token = "0x600006E")]
    	[Address(RVA = "0xD19E60", Offset = "0xD18860", VA = "0x180D19E60", Slot = "76")]
    	[AsyncStateMachine(typeof(_003CChangePositionXAsync_003Ed__27))]
    	public UniTask ChangePositionXAsync(float positionX, float duration, EasingType easingType = EasingType.Linear, AsyncToken asyncToken = default)
    	{
    		return ChangePositionXAsyncCore(positionX, duration, easingType, asyncToken);
    	}

    	private async UniTask ChangePositionXAsyncCore(float positionX, float duration, EasingType easingType, AsyncToken asyncToken)
    	{
    		CompletePositionXTweener();
    		_position = new Vector3(positionX, _position.y, _position.z);
    		FloatTween tweenValue = new FloatTween(GetBehaviourPosition().x, positionX, new Tween(duration, easingType), SetBehaviourPositionX);
    		await _positionXTweener.RunAwaitable(tweenValue, asyncToken, GameObject);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1A040 (kickoff) / 0xD1C4B0 (<ChangePositionYAsync>d__28.MoveNext, 1253 B)
    	// The Y twin of ChangePositionXAsync (tweener +0xD8, CompletePositionYTweener 0x180D1B8F0, SetBehaviourPositionY 0x180D1B710).
    	// Annotated disassembly: work/title-path/disas/dsce_13_ChangePositionYAsync, dsce_14_ChangePositionYAsync_MoveNext.txt
    	[Token(Token = "0x600006F")]
    	[Address(RVA = "0xD1A040", Offset = "0xD18A40", VA = "0x180D1A040", Slot = "77")]
    	[AsyncStateMachine(typeof(_003CChangePositionYAsync_003Ed__28))]
    	public UniTask ChangePositionYAsync(float positionY, float duration, EasingType easingType = EasingType.Linear, AsyncToken asyncToken = default)
    	{
    		return ChangePositionYAsyncCore(positionY, duration, easingType, asyncToken);
    	}

    	private async UniTask ChangePositionYAsyncCore(float positionY, float duration, EasingType easingType, AsyncToken asyncToken)
    	{
    		CompletePositionYTweener();
    		_position = new Vector3(_position.x, positionY, _position.z);
    		FloatTween tweenValue = new FloatTween(GetBehaviourPosition().y, positionY, new Tween(duration, easingType), SetBehaviourPositionY);
    		await _positionYTweener.RunAwaitable(tweenValue, asyncToken, GameObject);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1A220 (kickoff) / 0xD1C9A0 (<ChangePositionZAsync>d__29.MoveNext, 1237 B)
    	// The Z twin (tweener +0xE0, CompletePositionZTweener 0x180D1B9A0, SetBehaviourPositionZ 0x180D1B750).
    	// Annotated disassembly: work/title-path/disas/dsce_15_ChangePositionZAsync, dsce_16_ChangePositionZAsync_MoveNext.txt
    	[Token(Token = "0x6000070")]
    	[Address(RVA = "0xD1A220", Offset = "0xD18C20", VA = "0x180D1A220", Slot = "78")]
    	[AsyncStateMachine(typeof(_003CChangePositionZAsync_003Ed__29))]
    	public UniTask ChangePositionZAsync(float positionZ, float duration, EasingType easingType = EasingType.Linear, AsyncToken asyncToken = default)
    	{
    		return ChangePositionZAsyncCore(positionZ, duration, easingType, asyncToken);
    	}

    	private async UniTask ChangePositionZAsyncCore(float positionZ, float duration, EasingType easingType, AsyncToken asyncToken)
    	{
    		CompletePositionZTweener();
    		_position = new Vector3(_position.x, _position.y, positionZ);
    		FloatTween tweenValue = new FloatTween(GetBehaviourPosition().z, positionZ, new Tween(duration, easingType), SetBehaviourPositionZ);
    		await _positionZTweener.RunAwaitable(tweenValue, asyncToken, GameObject);
    	}

    	// RECONSTRUCTED | Confidence: B | RVA: 0xD1A400, 791 B (folded with LayeredCharacterExtended.ChangePivot 0xD1D250 and SpriteBackgroundExtended's)
    	// TransitionalRenderer (slot 63); each coordinate is x.GetValueOrDefault() when HasValue (0x180D1A4D1 / 0x180D1A5FA), otherwise the
    	// coordinate of the vtable +0x328 = slot 31 getter, i.e. Position (0x180D1A584 / 0x180D1A6AF: `movsd xmm6,[rax]` = x, then
    	// `shufps 0x55` = y) -- the shipped fallback reads Position, not Pivot (recorded as shipped); the result goes to the renderer's
    	// slot 24 set_Pivot.
    	// Annotated disassembly: work/title-path/disas/dsce_17_ChangePivot.txt
    	[Token(Token = "0x6000071")]
    	[Address(RVA = "0xD1A400", Offset = "0xD18E00", VA = "0x180D1A400", Slot = "81")]
    	public void ChangePivot(float? x, float? y)
    	{
    		TransitionalRenderer.Pivot = new Vector2(x ?? Position.x, y ?? Position.y);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD192E0, 258 B
    	// RVA 0xD191E0 / 0xD192E0 are shared (identical code folded) with LayeredCharacterExtended's 0xD1CF30 / 0xD1D030 and
    	// SpriteBackgroundExtended's 0xD1E4xx: the renderer (vtable +0x528, slot 63 in DicedSpriteActor) is tested with `as TransitionalSpriteRenderer`
    	// (class-hierarchy walk at 0x180D19240), its SpriteMaterial (vtable +0x3E8, slot 43) is read / written through the
    	// IMaskabilityChangeableActor.ShaderPropertyIds.StencilComp id: masked is Stencil Comparison 3 (Equal, constant 0x2AE29B8),
    	// unmasked 8 (Always, 0x2AE2AA4).
    	[Token(Token = "0x6000072")]
    	[Address(RVA = "0xD192E0", Offset = "0xD17CE0", VA = "0x180D192E0", Slot = "84")]
    	public void ChangeMaskability(bool enable)
    	{
    		if (TransitionalRenderer is TransitionalSpriteRenderer spriteRenderer)
    		{
    			spriteRenderer.SpriteMaterial.SetFloat(IMaskabilityChangeableActor.ShaderPropertyIds.StencilComp, enable ? 3f : 8f);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1A720 (kickoff) / 0xD1BB10 (<ChangePosition>d__32.MoveNext, 1196 B)
    	// MonoBehaviourActor.ChangePosition with the extended state: all four tweeners completed (0x180D1BA50), `_position` (+0x110) stored,
    	// RunAwaitable(new VectorTween(GetBehaviourPosition() [slot 50], position, tween, SetBehaviourPosition [vtable +0x470 = slot 51]),
    	// token, GameObject) on the position tweener (+0xD0).
    	// Annotated disassembly: work/title-path/disas/dsce_18_ChangePosition, dsce_19_ChangePosition_MoveNext.txt
    	[Token(Token = "0x6000073")]
    	[Address(RVA = "0xD1A720", Offset = "0xD19120", VA = "0x180D1A720", Slot = "44")]
    	[AsyncStateMachine(typeof(_003CChangePosition_003Ed__32))]
    	public override UniTask ChangePosition(Vector3 position, Tween tween, AsyncToken asyncToken = default)
    	{
    		return ChangePositionCore(position, tween, asyncToken);
    	}

    	private async UniTask ChangePositionCore(Vector3 position, Tween tween, AsyncToken asyncToken)
    	{
    		CompleteAllPositionTweeners();
    		_position = position;
    		VectorTween tweenValue = new VectorTween(GetBehaviourPosition(), position, tween, SetBehaviourPosition);
    		await _positionTweener.RunAwaitable(tweenValue, asyncToken, GameObject);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1A8F0, 620 B
    	// The per-character scale coefficient (`_scaleCoefficient`, +0x108, HasValue at 0x180D1A9B0; constant 1.0 = 0x2AE2820 otherwise)
    	// multiplies the scale (mulss x3, 0x180D1AA9E) before Transform (vtable +0x3B8 = slot 40).localScale is set.
    	// Annotated disassembly: work/title-path/disas/dsce_20_SetBehaviourScale.txt
    	[Token(Token = "0x6000074")]
    	[Address(RVA = "0xD1A8F0", Offset = "0xD192F0", VA = "0x180D1A8F0", Slot = "56")]
    	protected override void SetBehaviourScale(Vector3 scale)
    	{
    		float coefficient = _scaleCoefficient.HasValue ? _scaleCoefficient.GetValueOrDefault() : 1f;
    		Transform.localScale = scale * coefficient;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1AB60, 90 B -- the extended copies of the render material (+0xF0) and mesh (+0xF8) are
    	// destroyed (ObjectUtils.DestroyOrImmediate 0x18054BE20), then DicedSpriteActor.Dispose (tail jump 0x180C54DB0).
    	// Annotated disassembly: work/title-path/disas/dsce_21_Dispose.txt
    	[Token(Token = "0x6000075")]
    	[Address(RVA = "0xD1AB60", Offset = "0xD19560", VA = "0x180D1AB60", Slot = "48")]
    	public override void Dispose()
    	{
    		ObjectUtils.DestroyOrImmediate(_renderMaterial);
    		ObjectUtils.DestroyOrImmediate(_renderMesh);
    		base.Dispose();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1ABC0, 1164 B -- DicedSpriteActor.RebuildRenderMesh over the extended fields: `_vertices` (+0x100)
    	// is refilled from Sprite.vertices, `_renderMesh` (+0xF8) is Clear()ed and gets SetVertices / SetUVs(0, sprite.uv) /
    	// SetTriangles(sprite.triangles, 0) (Mesh$$SetSizedArrayForChannel, SetUvsImpl, SetTrianglesImpl).
    	// Annotated disassembly: work/title-path/disas/dsce_22_RebuildRenderMesh.txt
    	[Token(Token = "0x6000076")]
    	[Address(RVA = "0xD1ABC0", Offset = "0xD195C0", VA = "0x180D1ABC0", Slot = "70")]
    	protected override void RebuildRenderMesh(Sprite dicedSprite)
    	{
    		_vertices.Clear();
    		foreach (Vector2 vertex in dicedSprite.vertices)
    		{
    			_vertices.Add(vertex);
    		}
    		_renderMesh.Clear();
    		_renderMesh.SetVertices(_vertices);
    		_renderMesh.SetUVs(0, dicedSprite.uv);
    		_renderMesh.SetTriangles(dicedSprite.triangles, 0);
    	}

    	// RECONSTRUCTED | Confidence: B | RVA: 0xD1B050, 1649 B -- DicedSpriteActor.RenderToTexture over the extended fields (`_meta` +0xE8,
    	// `_renderMaterial` +0xF0, `_renderMesh` +0xF8): RenderTexture.GetTemporary (0x182046980), Sprite.get_pivot (0x181FD5E60),
    	// Graphics.SetRenderTarget, GL.Clear / PushMatrix / LoadProjectionMatrix / PopMatrix, Material.set_mainTexture (Sprite.get_texture),
    	// SetPass(0), Graphics.DrawMeshNow (0x181FFAEB0).  The package's private GetSpriteRect / GetRenderTexture helpers are not
    	// members of this class in the shipped build, so they are spelled inline (the vertex min/max as a loop).  The arithmetic is the
    	// package's; TODO(evidence): the constants (ortho 0..100) were not compared byte for byte.
    	// Annotated disassembly: work/title-path/disas/dsce_23_RenderToTexture.txt
    	[Token(Token = "0x6000077")]
    	[Address(RVA = "0xD1B050", Offset = "0xD19A50", VA = "0x180D1B050", Slot = "71")]
    	protected override RenderTexture RenderToTexture(Sprite dicedSprite)
    	{
    		Vector2 minVertPos = new Vector2(float.MaxValue, float.MaxValue);
    		Vector2 maxVertPos = new Vector2(float.MinValue, float.MinValue);
    		foreach (Vector2 vertex in dicedSprite.vertices)
    		{
    			minVertPos = new Vector2(Mathf.Min(minVertPos.x, vertex.x), Mathf.Min(minVertPos.y, vertex.y));
    			maxVertPos = new Vector2(Mathf.Max(maxVertPos.x, vertex.x), Mathf.Max(maxVertPos.y, vertex.y));
    		}
    		Vector2 spriteSize = new Vector2(Mathf.Abs(maxVertPos.x - minVertPos.x), Mathf.Abs(maxVertPos.y - minVertPos.y));
    		RenderTexture renderTexture = RenderTexture.GetTemporary(Mathf.CeilToInt(spriteSize.x * _meta.PixelsPerUnit), Mathf.CeilToInt(spriteSize.y * _meta.PixelsPerUnit));
    		Vector2 pivot = dicedSprite.pivot / spriteSize / dicedSprite.pixelsPerUnit;
    		Vector2 drawPos = spriteSize * pivot - spriteSize / 2f;
    		Vector2 halfSize = spriteSize / 2f;
    		Matrix4x4 orthoMatrix = Matrix4x4.Ortho(0f - halfSize.x, halfSize.x, 0f - halfSize.y, halfSize.y, 0f, 100f);
    		Graphics.SetRenderTarget(renderTexture);
    		GL.Clear(true, true, Color.clear);
    		GL.PushMatrix();
    		GL.LoadProjectionMatrix(orthoMatrix);
    		_renderMaterial.mainTexture = dicedSprite.texture;
    		_renderMaterial.SetPass(0);
    		Graphics.DrawMeshNow(_renderMesh, drawPos, Quaternion.identity);
    		GL.PopMatrix();
    		return renderTexture;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1B6D0, 57 B -- Transform (vtable +0x3B8 = slot 40), tail jump TransformUtils.SetPosX (0x180554D80)
    	// Annotated disassembly: work/title-path/disas/dsce_24_SetBehaviourPositionX.txt
    	[Token(Token = "0x6000078")]
    	[Address(RVA = "0xD1B6D0", Offset = "0xD1A0D0", VA = "0x180D1B6D0")]
    	private void SetBehaviourPositionX(float positionX)
    	{
    		Transform.SetPosX(positionX);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1B710, 57 B -- tail jump TransformUtils.SetPosY (0x180555140)
    	// Annotated disassembly: work/title-path/disas/dsce_25_SetBehaviourPositionY.txt
    	[Token(Token = "0x6000079")]
    	[Address(RVA = "0xD1B710", Offset = "0xD1A110", VA = "0x180D1B710")]
    	private void SetBehaviourPositionY(float positionY)
    	{
    		Transform.SetPosY(positionY);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1B750, 57 B -- tail jump TransformUtils.SetPosZ (0x1805554F0)
    	// Annotated disassembly: work/title-path/disas/dsce_26_SetBehaviourPositionZ.txt
    	[Token(Token = "0x600007A")]
    	[Address(RVA = "0xD1B750", Offset = "0xD1A150", VA = "0x180D1B750")]
    	private void SetBehaviourPositionZ(float positionZ)
    	{
    		Transform.SetPosZ(positionZ);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD191E0, 243 B
    	// RVA 0xD191E0 / 0xD192E0 are shared (identical code folded) with LayeredCharacterExtended's 0xD1CF30 / 0xD1D030 and
    	// SpriteBackgroundExtended's 0xD1E4xx: the renderer (vtable +0x528, slot 63 in DicedSpriteActor) is tested with `as TransitionalSpriteRenderer`
    	// (class-hierarchy walk at 0x180D19240), its SpriteMaterial (vtable +0x3E8, slot 43) is read / written through the
    	// IMaskabilityChangeableActor.ShaderPropertyIds.StencilComp id: masked is Stencil Comparison 3 (Equal, constant 0x2AE29B8),
    	// unmasked 8 (Always, 0x2AE2AA4).
    	// Not the sprite renderer -> false (0x180D19016 `xor al, al`); GetFloat is Material$$GetFloatImpl (0x182010230), the
    	// comparison Mathf.Approximately (0x180331670) against 3.0.
    	[Token(Token = "0x600007B")]
    	[Address(RVA = "0xD191E0", Offset = "0xD17BE0", VA = "0x180D191E0")]
    	private bool IsMasked()
    	{
    		if (TransitionalRenderer is TransitionalSpriteRenderer spriteRenderer)
    		{
    			return Mathf.Approximately(spriteRenderer.SpriteMaterial.GetFloat(IMaskabilityChangeableActor.ShaderPropertyIds.StencilComp), 3f);
    		}
    		return false;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1B790, 162 B -- `_positionTweener` (+0xD0): Running (inlined getter) -> CompleteInstantly (0x180965910)
    	// Annotated disassembly: work/title-path/disas/dsce_27_CompletePositionTweener.txt
    	[Token(Token = "0x600007C")]
    	[Address(RVA = "0xD1B790", Offset = "0xD1A190", VA = "0x180D1B790")]
    	private void CompletePositionTweener()
    	{
    		if (_positionTweener.Running)
    		{
    			_positionTweener.CompleteInstantly();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1B840, 162 B -- `_positionXTweener` (+0xC8)
    	[Token(Token = "0x600007D")]
    	[Address(RVA = "0xD1B840", Offset = "0xD1A240", VA = "0x180D1B840")]
    	private void CompletePositionXTweener()
    	{
    		if (_positionXTweener.Running)
    		{
    			_positionXTweener.CompleteInstantly();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1B8F0, 162 B -- `_positionYTweener` (+0xD8)
    	[Token(Token = "0x600007E")]
    	[Address(RVA = "0xD1B8F0", Offset = "0xD1A2F0", VA = "0x180D1B8F0")]
    	private void CompletePositionYTweener()
    	{
    		if (_positionYTweener.Running)
    		{
    			_positionYTweener.CompleteInstantly();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1B9A0, 162 B -- `_positionZTweener` (+0xE0)
    	[Token(Token = "0x600007F")]
    	[Address(RVA = "0xD1B9A0", Offset = "0xD1A3A0", VA = "0x180D1B9A0")]
    	private void CompletePositionZTweener()
    	{
    		if (_positionZTweener.Running)
    		{
    			_positionZTweener.CompleteInstantly();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD1BA50, 186 B -- the position tweener check is inlined, then X, Y and (tail jump) Z
    	// Annotated disassembly: work/title-path/disas/dsce_31_CompleteAllPositionTweeners.txt
    	[Token(Token = "0x6000080")]
    	[Address(RVA = "0xD1BA50", Offset = "0xD1A450", VA = "0x180D1BA50")]
    	private void CompleteAllPositionTweeners()
    	{
    		CompletePositionTweener();
    		CompletePositionXTweener();
    		CompletePositionYTweener();
    		CompletePositionZTweener();
    	}
    }
}
