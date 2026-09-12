using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors;

[Token(Token = "0x200003D")]
[UsedImplicitly]
[CommandAlias("back")]
public class ModifyBackgroundExtended : ModifyBackground
{
	[StructLayout(3)]
	[Token(Token = "0x200003F")]
	[CompilerGenerated]
	private struct _003CApplyAppearanceModification_003Ed__18
{
		[Token(Token = "0x40000DA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000DB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000DC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ModifyBackgroundExtended _003C_003E4__this;

		[Token(Token = "0x40000DD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40000DE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public IBackgroundActor actor;

		[Token(Token = "0x40000DF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public Tween tween;

		[Token(Token = "0x40000E0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask<Texture2D>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40000E1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60000D9")]
		[Address(RVA = "0xD21330", Offset = "0xD1FD30", VA = "0x180D21330", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000DA")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000040")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__16
{
		[Token(Token = "0x40000E2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000E3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000E4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ModifyBackgroundExtended _003C_003E4__this;

		[Token(Token = "0x40000E5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40000E6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000DB")]
		[Address(RVA = "0xD21B00", Offset = "0xD20500", VA = "0x180D21B00", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000DC")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000041")]
	[CompilerGenerated]
	private struct _003CModify_003Ed__17
{
		[Token(Token = "0x40000E7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000E8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000E9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ModifyBackgroundExtended _003C_003E4__this;

		[Token(Token = "0x40000EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40000EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private EasingType _003CeasingType_003E5__2;

		[Token(Token = "0x40000EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private PooledObject<List<UniTask>> _003C__003E5__3;

		[Token(Token = "0x40000ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40000EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private UniTask<IBackgroundActor>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60000DD")]
		[Address(RVA = "0xD22230", Offset = "0xD20C30", VA = "0x180D22230", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000DE")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000042")]
	[CompilerGenerated]
	private struct _003CPreloadResources_003Ed__14
{
		[Token(Token = "0x40000EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ModifyBackgroundExtended _003C_003E4__this;

		[Token(Token = "0x40000F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask<Texture2D>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40000F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x60000DF")]
		[Address(RVA = "0xD23130", Offset = "0xD21B30", VA = "0x180D23130", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000E0")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40000D2")]
	private const bool DefaultMatchScreenWidth = true;

	[Token(Token = "0x40000D3")]
	private const string StillId = "Stills";

	[Token(Token = "0x40000D4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
	[UsedImplicitly]
	public StringParameter SubId;

	[Token(Token = "0x40000D5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
	[UsedImplicitly]
	[ParameterAlias("match")]
	public BooleanParameter MatchScreenWidth;

	[Token(Token = "0x40000D6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	private ModifyActorUtility<IBackgroundActor, IMultipliableBackgroundManager> _actorUtility;

	[Token(Token = "0x40000D7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	private Texture2D _preloadedDissolveTexture;

	[Token(Token = "0x17000014")]
	protected override float?[] AssignedPosition
	{
		[Token(Token = "0x60000CA")]
		[Address(RVA = "0xD204C0", Offset = "0xD1EEC0", VA = "0x180D204C0", Slot = "22")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000015")]
	private string AssignedSubId
	{
		[Token(Token = "0x60000CB")]
		[Address(RVA = "0xD204D0", Offset = "0xD1EED0", VA = "0x180D204D0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000016")]
	private bool AssignedMatchScreenWidth
	{
		[Token(Token = "0x60000CC")]
		[Address(RVA = "0xD20560", Offset = "0xD1EF60", VA = "0x180D20560")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000017")]
	private ModifyActorUtility<IBackgroundActor, IMultipliableBackgroundManager> ActorUtility
	{
		[Token(Token = "0x60000CD")]
		[Address(RVA = "0xD20610", Offset = "0xD1F010", VA = "0x180D20610")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60000CE")]
	[Address(RVA = "0xD20700", Offset = "0xD1F100", VA = "0x180D20700", Slot = "32")]
	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__14))]
	public override UniTask PreloadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000CF")]
	[Address(RVA = "0xD20820", Offset = "0xD1F220", VA = "0x180D20820", Slot = "33")]
	public override void ReleaseResources()
	{
	}

	[Token(Token = "0x60000D0")]
	[Address(RVA = "0xD20960", Offset = "0xD1F360", VA = "0x180D20960", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__16))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000D1")]
	[Address(RVA = "0xD20AE0", Offset = "0xD1F4E0", VA = "0x180D20AE0", Slot = "34")]
	[AsyncStateMachine(typeof(_003CModify_003Ed__17))]
	protected override UniTask Modify(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000D2")]
	[Address(RVA = "0xD20C70", Offset = "0xD1F670", VA = "0x180D20C70", Slot = "36")]
	[AsyncStateMachine(typeof(_003CApplyAppearanceModification_003Ed__18))]
	protected override UniTask ApplyAppearanceModification(IBackgroundActor actor, Tween tween, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60000D3")]
	[Address(RVA = "0xD20E80", Offset = "0xD1F880", VA = "0x180D20E80")]
	private float?[] AttemptScenePosition()
	{
		return null;
	}

	[Token(Token = "0x60000D4")]
	[Address(RVA = "0x4D8860", Offset = "0x4D7260", VA = "0x1804D8860")]
	public ModifyBackgroundExtended()
	{
	}
}
