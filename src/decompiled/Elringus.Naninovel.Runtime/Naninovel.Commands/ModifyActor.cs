using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel.Commands;

[Token(Token = "0x2000670")]
public abstract class ModifyActor<TActor, TState, TMeta, TConfig, TManager> : Command, Command.IPreloadable where TActor : class, IActor where TState : ActorState<TActor>, new() where TMeta : ActorMetadata where TConfig : ActorManagerConfiguration<TMeta>, new() where TManager : class, IActorManager<TActor, TState, TMeta, TConfig>
{
	[StructLayout(3)]
	[Token(Token = "0x2000671")]
	[CompilerGenerated]
	private struct _003CApplyAppearanceModification_003Ed__61
{
		[Token(Token = "0x400187D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400187E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400187F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ModifyActor<TActor, TState, TMeta, TConfig, TManager> _003C_003E4__this;

		[Token(Token = "0x4001880")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public TActor actor;

		[Token(Token = "0x4001881")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Tween tween;

		[Token(Token = "0x4001882")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x4001883")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001FAD")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001FAE")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000672")]
	[CompilerGenerated]
	private struct _003CApplyModifications_003Ed__60
{
		[Token(Token = "0x4001884")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001885")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001886")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public TActor actor;

		[Token(Token = "0x4001887")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ModifyActor<TActor, TState, TMeta, TConfig, TManager> _003C_003E4__this;

		[Token(Token = "0x4001888")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public EasingType easingType;

		[Token(Token = "0x4001889")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x400188A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001FAF")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001FB0")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000673")]
	[CompilerGenerated]
	private struct _003CApplyPositionModification_003Ed__63
{
		[Token(Token = "0x400188B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400188C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400188D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ModifyActor<TActor, TState, TMeta, TConfig, TManager> _003C_003E4__this;

		[Token(Token = "0x400188E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public TActor actor;

		[Token(Token = "0x400188F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Tween tween;

		[Token(Token = "0x4001890")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x4001891")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001FB1")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001FB2")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000674")]
	[CompilerGenerated]
	private struct _003CApplyRotationModification_003Ed__64
{
		[Token(Token = "0x4001892")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001893")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001894")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ModifyActor<TActor, TState, TMeta, TConfig, TManager> _003C_003E4__this;

		[Token(Token = "0x4001895")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public TActor actor;

		[Token(Token = "0x4001896")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Tween tween;

		[Token(Token = "0x4001897")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x4001898")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001FB3")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001FB4")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000675")]
	[CompilerGenerated]
	private struct _003CApplyScaleModification_003Ed__65
{
		[Token(Token = "0x4001899")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400189A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400189B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ModifyActor<TActor, TState, TMeta, TConfig, TManager> _003C_003E4__this;

		[Token(Token = "0x400189C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public TActor actor;

		[Token(Token = "0x400189D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Tween tween;

		[Token(Token = "0x400189E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x400189F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001FB5")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001FB6")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000676")]
	[CompilerGenerated]
	private struct _003CApplyTintColorModification_003Ed__66
{
		[Token(Token = "0x40018A0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40018A1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40018A2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ModifyActor<TActor, TState, TMeta, TConfig, TManager> _003C_003E4__this;

		[Token(Token = "0x40018A3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public TActor actor;

		[Token(Token = "0x40018A4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Tween tween;

		[Token(Token = "0x40018A5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x40018A6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001FB7")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001FB8")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000677")]
	[CompilerGenerated]
	private struct _003CApplyVisibilityModification_003Ed__62
{
		[Token(Token = "0x40018A7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40018A8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40018A9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ModifyActor<TActor, TState, TMeta, TConfig, TManager> _003C_003E4__this;

		[Token(Token = "0x40018AA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public TActor actor;

		[Token(Token = "0x40018AB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Tween tween;

		[Token(Token = "0x40018AC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x40018AD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001FB9")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001FBA")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000678")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__58
{
		[Token(Token = "0x40018AE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40018AF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40018B0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ModifyActor<TActor, TState, TMeta, TConfig, TManager> _003C_003E4__this;

		[Token(Token = "0x40018B1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x40018B2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask _003Ctasks_003E5__2;

		[Token(Token = "0x40018B3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private YieldAwaitable.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40018B4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6001FBB")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001FBC")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000679")]
	[CompilerGenerated]
	private struct _003CModify_003Ed__59
{
		[Token(Token = "0x40018B5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40018B6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40018B7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ModifyActor<TActor, TState, TMeta, TConfig, TManager> _003C_003E4__this;

		[Token(Token = "0x40018B8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x40018B9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private EasingType _003CeasingType_003E5__2;

		[Token(Token = "0x40018BA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private PooledObject<List<UniTask>> _003C__003E5__3;

		[Token(Token = "0x40018BB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40018BC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TActor>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6001FBD")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001FBE")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200067A")]
	[CompilerGenerated]
	private struct _003CPreloadResources_003Ed__56
{
		[Token(Token = "0x40018BD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40018BE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40018BF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ModifyActor<TActor, TState, TMeta, TConfig, TManager> _003C_003E4__this;

		[Token(Token = "0x40018C0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private ResourceRequest _003CloadTask_003E5__2;

		[Token(Token = "0x40018C1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UnityAsyncExtensions.ResourceRequestAwaiter _003C_003Eu__1;

		[Token(Token = "0x40018C2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TActor>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x40018C3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<Resource>.Awaiter _003C_003Eu__3;

		[Token(Token = "0x6001FBF")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001FC0")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400186D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	public StringParameter Id;

	[Token(Token = "0x400186E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[AppearanceContext(-1, null, null)]
	public StringParameter Appearance;

	[Token(Token = "0x400186F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	public StringParameter Pose;

	[Token(Token = "0x4001870")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[ConstantContext(typeof(TransitionType), -1, null)]
	public StringParameter Transition;

	[Token(Token = "0x4001871")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[ParameterAlias("params")]
	public DecimalListParameter TransitionParams;

	[Token(Token = "0x4001872")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[ParameterAlias("dissolve")]
	public StringParameter DissolveTexturePath;

	[Token(Token = "0x4001873")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	public BooleanParameter Visible;

	[Token(Token = "0x4001874")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[VectorContext("X,Y,Z", -1, null)]
	public DecimalListParameter Position;

	[Token(Token = "0x4001875")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[VectorContext("X,Y,Z", -1, null)]
	public DecimalListParameter Rotation;

	[Token(Token = "0x4001876")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[VectorContext("X,Y,Z", -1, null)]
	public DecimalListParameter Scale;

	[Token(Token = "0x4001877")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[ParameterAlias("tint")]
	[ColorContext(-1, null)]
	public StringParameter TintColor;

	[Token(Token = "0x4001878")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[ParameterAlias("easing")]
	[ConstantContext(typeof(EasingType), -1, null)]
	public StringParameter EasingTypeName;

	[Token(Token = "0x4001879")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[ParameterAlias("time")]
	[ParameterDefaultValue("0.35")]
	public DecimalParameter Duration;

	[Token(Token = "0x400187A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[ParameterDefaultValue("false")]
	public BooleanParameter Lazy;

	[Token(Token = "0x400187B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	public BooleanParameter Wait;

	[Token(Token = "0x400187C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private Texture2D preloadedDissolveTexture;

	[Token(Token = "0x170004A2")]
	protected virtual string AssignedId
	{
		[Token(Token = "0x6001F8A")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004A3")]
	protected virtual string AssignedTransition
	{
		[Token(Token = "0x6001F8B")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004A4")]
	protected virtual string AssignedAppearance
	{
		[Token(Token = "0x6001F8C")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004A5")]
	protected virtual bool? AssignedVisibility
	{
		[Token(Token = "0x6001F8D")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004A6")]
	protected virtual float?[] AssignedPosition
	{
		[Token(Token = "0x6001F8E")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004A7")]
	protected virtual float?[] AssignedRotation
	{
		[Token(Token = "0x6001F8F")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004A8")]
	protected virtual float?[] AssignedScale
	{
		[Token(Token = "0x6001F90")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004A9")]
	protected virtual Color? AssignedTintColor
	{
		[Token(Token = "0x6001F91")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004AA")]
	protected virtual float AssignedDuration
	{
		[Token(Token = "0x6001F92")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x170004AB")]
	protected virtual TManager ActorManager
	{
		[Token(Token = "0x6001F93")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004AC")]
	protected virtual TConfig Configuration
	{
		[Token(Token = "0x6001F94")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004AD")]
	protected virtual string AlternativeAppearance
	{
		[Token(Token = "0x6001F95")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004AE")]
	protected virtual bool AllowPreload
	{
		[Token(Token = "0x6001F96")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x170004AF")]
	protected virtual bool PosedViaAppearance
	{
		[Token(Token = "0x6001F97")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x170004B0")]
	protected string PosedAppearance
	{
		[Token(Token = "0x6001F98")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004B1")]
	protected bool? PosedVisibility
	{
		[Token(Token = "0x6001F99")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004B2")]
	protected float?[] PosedPosition
	{
		[Token(Token = "0x6001F9A")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004B3")]
	protected float?[] PosedRotation
	{
		[Token(Token = "0x6001F9B")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004B4")]
	protected float?[] PosedScale
	{
		[Token(Token = "0x6001F9C")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004B5")]
	protected Color? PosedTintColor
	{
		[Token(Token = "0x6001F9D")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001F9E")]
	[AsyncStateMachine(typeof(ModifyActor<, , , , >._003CPreloadResources_003Ed__56))]
	public virtual UniTask PreloadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001F9F")]
	public virtual void ReleaseResources()
	{
	}

	[Token(Token = "0x6001FA0")]
	[AsyncStateMachine(typeof(ModifyActor<, , , , >._003CExecute_003Ed__58))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001FA1")]
	[AsyncStateMachine(typeof(ModifyActor<, , , , >._003CModify_003Ed__59))]
	protected virtual UniTask Modify(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001FA2")]
	[AsyncStateMachine(typeof(ModifyActor<, , , , >._003CApplyModifications_003Ed__60))]
	protected virtual UniTask ApplyModifications(TActor actor, EasingType easingType, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001FA3")]
	[AsyncStateMachine(typeof(ModifyActor<, , , , >._003CApplyAppearanceModification_003Ed__61))]
	protected virtual UniTask ApplyAppearanceModification(TActor actor, Tween tween, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001FA4")]
	[AsyncStateMachine(typeof(ModifyActor<, , , , >._003CApplyVisibilityModification_003Ed__62))]
	protected virtual UniTask ApplyVisibilityModification(TActor actor, Tween tween, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001FA5")]
	[AsyncStateMachine(typeof(ModifyActor<, , , , >._003CApplyPositionModification_003Ed__63))]
	protected virtual UniTask ApplyPositionModification(TActor actor, Tween tween, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001FA6")]
	[AsyncStateMachine(typeof(ModifyActor<, , , , >._003CApplyRotationModification_003Ed__64))]
	protected virtual UniTask ApplyRotationModification(TActor actor, Tween tween, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001FA7")]
	[AsyncStateMachine(typeof(ModifyActor<, , , , >._003CApplyScaleModification_003Ed__65))]
	protected virtual UniTask ApplyScaleModification(TActor actor, Tween tween, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001FA8")]
	[AsyncStateMachine(typeof(ModifyActor<, , , , >._003CApplyTintColorModification_003Ed__66))]
	protected virtual UniTask ApplyTintColorModification(TActor actor, Tween tween, AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001FA9")]
	protected virtual Color? ParseColor(string color)
	{
		return null;
	}

	[Token(Token = "0x6001FAA")]
	protected virtual ActorPose<TState> GetPoseOrNull()
	{
		return null;
	}

	[Token(Token = "0x6001FAB")]
	protected virtual TState GetPosed(string propertyName)
	{
		return null;
	}

	[Token(Token = "0x6001FAC")]
	protected ModifyActor()
	{
	}
}
