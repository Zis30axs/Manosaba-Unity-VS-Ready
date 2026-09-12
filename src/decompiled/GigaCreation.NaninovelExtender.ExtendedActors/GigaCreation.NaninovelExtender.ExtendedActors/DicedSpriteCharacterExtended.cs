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

namespace GigaCreation.NaninovelExtender.ExtendedActors;

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
	private readonly Tweener<FloatTween> _positionXTweener;

	[Token(Token = "0x400006E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
	private readonly Tweener<VectorTween> _positionTweener;

	[Token(Token = "0x400006F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
	private readonly Tweener<FloatTween> _positionYTweener;

	[Token(Token = "0x4000070")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	private readonly Tweener<FloatTween> _positionZTweener;

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
	private readonly List<Vector3> _vertices;

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
		[Token(Token = "0x6000064")]
		[Address(RVA = "0xD19130", Offset = "0xD17B30", VA = "0x180D19130", Slot = "75")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000009")]
	public Vector2 Pivot
	{
		[Token(Token = "0x6000065")]
		[Address(RVA = "0xD19140", Offset = "0xD17B40", VA = "0x180D19140", Slot = "79")]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x6000066")]
		[Address(RVA = "0xD19180", Offset = "0xD17B80", VA = "0x180D19180", Slot = "80")]
		set
		{
		}
	}

	[Token(Token = "0x1700000A")]
	public bool Masked
	{
		[Token(Token = "0x6000067")]
		[Address(RVA = "0xD191E0", Offset = "0xD17BE0", VA = "0x180D191E0", Slot = "82")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000068")]
		[Address(RVA = "0xD192E0", Offset = "0xD17CE0", VA = "0x180D192E0", Slot = "83")]
		set
		{
		}
	}

	[Token(Token = "0x1700000B")]
	public override Vector3 Position
	{
		[Token(Token = "0x6000069")]
		[Address(RVA = "0xD193F0", Offset = "0xD17DF0", VA = "0x180D193F0", Slot = "31")]
		get
		{
			return default(Vector3);
		}
		[Token(Token = "0x600006A")]
		[Address(RVA = "0xD19410", Offset = "0xD17E10", VA = "0x180D19410", Slot = "32")]
		set
		{
		}
	}

	[Token(Token = "0x1700000C")]
	private ICharacterManagerExtended NaniCharacterManager
	{
		[Token(Token = "0x600006B")]
		[Address(RVA = "0xD19470", Offset = "0xD17E70", VA = "0x180D19470")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600006C")]
	[Address(RVA = "0xD19540", Offset = "0xD17F40", VA = "0x180D19540")]
	public DicedSpriteCharacterExtended(string id, CharacterMetadata meta, EmbeddedAppearanceLoader<DicedSpriteAtlas> loader)
	{
	}

	[Token(Token = "0x600006D")]
	[Address(RVA = "0xD19BC0", Offset = "0xD185C0", VA = "0x180D19BC0")]
	public DicedSpriteCharacterExtended(string id, string subId, CharacterMetadata meta, EmbeddedAppearanceLoader<DicedSpriteAtlas> loader)
	{
	}

	[Token(Token = "0x600006E")]
	[Address(RVA = "0xD19E60", Offset = "0xD18860", VA = "0x180D19E60", Slot = "76")]
	[AsyncStateMachine(typeof(_003CChangePositionXAsync_003Ed__27))]
	public UniTask ChangePositionXAsync(float positionX, float duration, EasingType easingType = EasingType.Linear, AsyncToken asyncToken = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600006F")]
	[Address(RVA = "0xD1A040", Offset = "0xD18A40", VA = "0x180D1A040", Slot = "77")]
	[AsyncStateMachine(typeof(_003CChangePositionYAsync_003Ed__28))]
	public UniTask ChangePositionYAsync(float positionY, float duration, EasingType easingType = EasingType.Linear, AsyncToken asyncToken = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000070")]
	[Address(RVA = "0xD1A220", Offset = "0xD18C20", VA = "0x180D1A220", Slot = "78")]
	[AsyncStateMachine(typeof(_003CChangePositionZAsync_003Ed__29))]
	public UniTask ChangePositionZAsync(float positionZ, float duration, EasingType easingType = EasingType.Linear, AsyncToken asyncToken = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000071")]
	[Address(RVA = "0xD1A400", Offset = "0xD18E00", VA = "0x180D1A400", Slot = "81")]
	public void ChangePivot(float? x, float? y)
	{
	}

	[Token(Token = "0x6000072")]
	[Address(RVA = "0xD192E0", Offset = "0xD17CE0", VA = "0x180D192E0", Slot = "84")]
	public void ChangeMaskability(bool enable)
	{
	}

	[Token(Token = "0x6000073")]
	[Address(RVA = "0xD1A720", Offset = "0xD19120", VA = "0x180D1A720", Slot = "44")]
	[AsyncStateMachine(typeof(_003CChangePosition_003Ed__32))]
	public override UniTask ChangePosition(Vector3 position, Tween tween, AsyncToken asyncToken = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000074")]
	[Address(RVA = "0xD1A8F0", Offset = "0xD192F0", VA = "0x180D1A8F0", Slot = "56")]
	protected override void SetBehaviourScale(Vector3 scale)
	{
	}

	[Token(Token = "0x6000075")]
	[Address(RVA = "0xD1AB60", Offset = "0xD19560", VA = "0x180D1AB60", Slot = "48")]
	public override void Dispose()
	{
	}

	[Token(Token = "0x6000076")]
	[Address(RVA = "0xD1ABC0", Offset = "0xD195C0", VA = "0x180D1ABC0", Slot = "70")]
	protected override void RebuildRenderMesh(Sprite dicedSprite)
	{
	}

	[Token(Token = "0x6000077")]
	[Address(RVA = "0xD1B050", Offset = "0xD19A50", VA = "0x180D1B050", Slot = "71")]
	protected override RenderTexture RenderToTexture(Sprite dicedSprite)
	{
		return null;
	}

	[Token(Token = "0x6000078")]
	[Address(RVA = "0xD1B6D0", Offset = "0xD1A0D0", VA = "0x180D1B6D0")]
	private void SetBehaviourPositionX(float positionX)
	{
	}

	[Token(Token = "0x6000079")]
	[Address(RVA = "0xD1B710", Offset = "0xD1A110", VA = "0x180D1B710")]
	private void SetBehaviourPositionY(float positionY)
	{
	}

	[Token(Token = "0x600007A")]
	[Address(RVA = "0xD1B750", Offset = "0xD1A150", VA = "0x180D1B750")]
	private void SetBehaviourPositionZ(float positionZ)
	{
	}

	[Token(Token = "0x600007B")]
	[Address(RVA = "0xD191E0", Offset = "0xD17BE0", VA = "0x180D191E0")]
	private bool IsMasked()
	{
		return default(bool);
	}

	[Token(Token = "0x600007C")]
	[Address(RVA = "0xD1B790", Offset = "0xD1A190", VA = "0x180D1B790")]
	private void CompletePositionTweener()
	{
	}

	[Token(Token = "0x600007D")]
	[Address(RVA = "0xD1B840", Offset = "0xD1A240", VA = "0x180D1B840")]
	private void CompletePositionXTweener()
	{
	}

	[Token(Token = "0x600007E")]
	[Address(RVA = "0xD1B8F0", Offset = "0xD1A2F0", VA = "0x180D1B8F0")]
	private void CompletePositionYTweener()
	{
	}

	[Token(Token = "0x600007F")]
	[Address(RVA = "0xD1B9A0", Offset = "0xD1A3A0", VA = "0x180D1B9A0")]
	private void CompletePositionZTweener()
	{
	}

	[Token(Token = "0x6000080")]
	[Address(RVA = "0xD1BA50", Offset = "0xD1A450", VA = "0x180D1BA50")]
	private void CompleteAllPositionTweeners()
	{
	}
}
