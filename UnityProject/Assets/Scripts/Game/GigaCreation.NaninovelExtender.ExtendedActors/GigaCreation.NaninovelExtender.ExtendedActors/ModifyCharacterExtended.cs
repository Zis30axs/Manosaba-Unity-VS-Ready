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

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x2000043")]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [CommandAlias("char")]
    public class ModifyCharacterExtended : ModifyCharacter
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000045")]
    	[CompilerGenerated]
    	private struct _003CApplyAppearanceModification_003Ed__16
    {
    		[Token(Token = "0x40000FB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000FC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000FD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ModifyCharacterExtended _003C_003E4__this;

    		[Token(Token = "0x40000FE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x40000FF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public ICharacterActor actor;

    		[Token(Token = "0x4000100")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		public Tween tween;

    		[Token(Token = "0x4000101")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask<Texture2D>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000102")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60000EE")]
    		[Address(RVA = "0xD242A0", Offset = "0xD22CA0", VA = "0x180D242A0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000EF")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000046")]
    	[CompilerGenerated]
    	private struct _003CModify_003Ed__15
    {
    		[Token(Token = "0x4000103")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000104")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000105")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ModifyCharacterExtended _003C_003E4__this;

    		[Token(Token = "0x4000106")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4000107")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private EasingType _003CeasingType_003E5__2;

    		[Token(Token = "0x4000108")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private PooledObject<List<UniTask>> _003C__003E5__3;

    		[Token(Token = "0x4000109")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400010A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private UniTask<ICharacterActor>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60000F0")]
    		[Address(RVA = "0xD24A70", Offset = "0xD23470", VA = "0x180D24A70", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000F1")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000047")]
    	[CompilerGenerated]
    	private struct _003CPreloadResources_003Ed__13
    {
    		[Token(Token = "0x400010B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400010C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400010D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ModifyCharacterExtended _003C_003E4__this;

    		[Token(Token = "0x400010E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask<Texture2D>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400010F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60000F2")]
    		[Address(RVA = "0xD25DF0", Offset = "0xD247F0", VA = "0x180D25DF0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000F3")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40000F4")]
    	private const bool DefaultMatchScreenWidth = true;

    	[Token(Token = "0x40000F5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	public StringParameter SubId;

    	[Token(Token = "0x40000F6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("match")]
    	public BooleanParameter MatchScreenWidth;

    	[Token(Token = "0x40000F7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
    	private ModifyActorUtility<ICharacterActor, IMultipliableCharacterManager> _actorUtility;

    	[Token(Token = "0x40000F8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
    	private Texture2D _preloadedDissolveTexture;

    	[Token(Token = "0x17000018")]
    	protected override float?[] AssignedPosition
    	{
    		[Token(Token = "0x60000E1")]
    		[Address(RVA = "0xD23750", Offset = "0xD22150", VA = "0x180D23750", Slot = "22")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000019")]
    	private string AssignedSubId
    	{
    		[Token(Token = "0x60000E2")]
    		[Address(RVA = "0xD23760", Offset = "0xD22160", VA = "0x180D23760")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700001A")]
    	private bool AssignedMatchScreenWidth
    	{
    		[Token(Token = "0x60000E3")]
    		[Address(RVA = "0xD237F0", Offset = "0xD221F0", VA = "0x180D237F0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1700001B")]
    	private ModifyActorUtility<ICharacterActor, IMultipliableCharacterManager> ActorUtility
    	{
    		[Token(Token = "0x60000E4")]
    		[Address(RVA = "0xD238A0", Offset = "0xD222A0", VA = "0x180D238A0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60000E5")]
    	[Address(RVA = "0xD23990", Offset = "0xD22390", VA = "0x180D23990", Slot = "32")]
    	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__13))]
    	public override UniTask PreloadResources()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60000E6")]
    	[Address(RVA = "0xD23AB0", Offset = "0xD224B0", VA = "0x180D23AB0", Slot = "33")]
    	public override void ReleaseResources()
    	{
    	}

    	[Token(Token = "0x60000E7")]
    	[Address(RVA = "0xD23BF0", Offset = "0xD225F0", VA = "0x180D23BF0", Slot = "34")]
    	[AsyncStateMachine(typeof(_003CModify_003Ed__15))]
    	protected override UniTask Modify(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60000E8")]
    	[Address(RVA = "0xD23D80", Offset = "0xD22780", VA = "0x180D23D80", Slot = "36")]
    	[AsyncStateMachine(typeof(_003CApplyAppearanceModification_003Ed__16))]
    	protected override UniTask ApplyAppearanceModification(ICharacterActor actor, Tween tween, AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60000E9")]
    	[Address(RVA = "0xD23F90", Offset = "0xD22990", VA = "0x180D23F90")]
    	private float?[] AttemptScenePosition()
    	{
    		return null;
    	}

    	[Token(Token = "0x60000EA")]
    	[Address(RVA = "0x4D9600", Offset = "0x4D8000", VA = "0x1804D9600")]
    	public ModifyCharacterExtended()
    	{
    	}
    }
}
