using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x200005C")]
public abstract class MonoBehaviourActor<TMeta> : IActor, IDisposable where TMeta : ActorMetadata
{
	[StructLayout(3)]
	[Token(Token = "0x200005D")]
	[CompilerGenerated]
	private struct _003CChangePosition_003Ed__42
{
		[Token(Token = "0x400012A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400012B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400012C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public MonoBehaviourActor<TMeta> _003C_003E4__this;

		[Token(Token = "0x400012D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Vector3 position;

		[Token(Token = "0x400012E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Tween tween;

		[Token(Token = "0x400012F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x4000130")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000199")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600019A")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200005E")]
	[CompilerGenerated]
	private struct _003CChangeRotation_003Ed__43
{
		[Token(Token = "0x4000131")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000132")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000133")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public MonoBehaviourActor<TMeta> _003C_003E4__this;

		[Token(Token = "0x4000134")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Quaternion rotation;

		[Token(Token = "0x4000135")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Tween tween;

		[Token(Token = "0x4000136")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x4000137")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600019B")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600019C")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200005F")]
	[CompilerGenerated]
	private struct _003CChangeScale_003Ed__44
{
		[Token(Token = "0x4000138")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000139")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400013A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public MonoBehaviourActor<TMeta> _003C_003E4__this;

		[Token(Token = "0x400013B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Vector3 scale;

		[Token(Token = "0x400013C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Tween tween;

		[Token(Token = "0x400013D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x400013E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600019D")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600019E")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000060")]
	[CompilerGenerated]
	private struct _003CChangeTintColor_003Ed__45
{
		[Token(Token = "0x400013F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000140")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000141")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public MonoBehaviourActor<TMeta> _003C_003E4__this;

		[Token(Token = "0x4000142")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Color tintColor;

		[Token(Token = "0x4000143")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Tween tween;

		[Token(Token = "0x4000144")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x4000145")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600019F")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60001A0")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000122")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly Tweener<VectorTween> positionTweener;

	[Token(Token = "0x4000123")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly Tweener<VectorTween> rotationTweener;

	[Token(Token = "0x4000124")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly Tweener<VectorTween> scaleTweener;

	[Token(Token = "0x4000125")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly Tweener<ColorTween> tintColorTweener;

	[Token(Token = "0x4000126")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private Vector3 position;

	[Token(Token = "0x4000127")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private Vector3 scale;

	[Token(Token = "0x4000128")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private Quaternion rotation;

	[Token(Token = "0x4000129")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private Color tintColor;

	[Token(Token = "0x17000039")]
	public virtual string Id
	{
		[Token(Token = "0x600016E")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700003A")]
	public virtual TMeta ActorMeta
	{
		[Token(Token = "0x600016F")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700003B")]
	public abstract string Appearance
	{
		[Token(Token = "0x6000170")]
		get;
		[Token(Token = "0x6000171")]
		set;
	}

	[Token(Token = "0x1700003C")]
	public abstract bool Visible
	{
		[Token(Token = "0x6000172")]
		get;
		[Token(Token = "0x6000173")]
		set;
	}

	[Token(Token = "0x1700003D")]
	public virtual Vector3 Position
	{
		[Token(Token = "0x6000174")]
		get
		{
			return default(Vector3);
		}
		[Token(Token = "0x6000175")]
		set
		{
		}
	}

	[Token(Token = "0x1700003E")]
	public virtual Quaternion Rotation
	{
		[Token(Token = "0x6000176")]
		get
		{
			return default(Quaternion);
		}
		[Token(Token = "0x6000177")]
		set
		{
		}
	}

	[Token(Token = "0x1700003F")]
	public virtual Vector3 Scale
	{
		[Token(Token = "0x6000178")]
		get
		{
			return default(Vector3);
		}
		[Token(Token = "0x6000179")]
		set
		{
		}
	}

	[Token(Token = "0x17000040")]
	public virtual Color TintColor
	{
		[Token(Token = "0x600017A")]
		get
		{
			return default(Color);
		}
		[Token(Token = "0x600017B")]
		set
		{
		}
	}

	[Token(Token = "0x17000041")]
	public virtual GameObject GameObject
	{
		[Token(Token = "0x600017C")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x600017D")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000042")]
	public virtual Transform Transform
	{
		[Token(Token = "0x600017E")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600017F")]
	protected MonoBehaviourActor(string id = default, TMeta meta = default)
	{
	}

	[Token(Token = "0x6000180")]
	public virtual UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000181")]
	public abstract UniTask ChangeAppearance(string appearance, Tween tween, [Optional] Transition? transition, [Optional] AsyncToken token);

	[Token(Token = "0x6000182")]
	public abstract UniTask ChangeVisibility(bool isVisible, Tween tween, [Optional] AsyncToken token);

	[Token(Token = "0x6000183")]
	[AsyncStateMachine(typeof(MonoBehaviourActor<>._003CChangePosition_003Ed__42))]
	public virtual UniTask ChangePosition(Vector3 position, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000184")]
	[AsyncStateMachine(typeof(MonoBehaviourActor<>._003CChangeRotation_003Ed__43))]
	public virtual UniTask ChangeRotation(Quaternion rotation, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000185")]
	[AsyncStateMachine(typeof(MonoBehaviourActor<>._003CChangeScale_003Ed__44))]
	public virtual UniTask ChangeScale(Vector3 scale, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000186")]
	[AsyncStateMachine(typeof(MonoBehaviourActor<>._003CChangeTintColor_003Ed__45))]
	public virtual UniTask ChangeTintColor(Color tintColor, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000187")]
	public virtual void Dispose()
	{
	}

	[Token(Token = "0x6000188")]
	public virtual CancellationToken GetDestroyCancellationToken()
	{
		return default(CancellationToken);
	}

	[Token(Token = "0x6000189")]
	protected virtual Vector3 GetBehaviourPosition()
	{
		return default(Vector3);
	}

	[Token(Token = "0x600018A")]
	protected virtual void SetBehaviourPosition(Vector3 position)
	{
	}

	[Token(Token = "0x600018B")]
	protected virtual Quaternion GetBehaviourRotation()
	{
		return default(Quaternion);
	}

	[Token(Token = "0x600018C")]
	protected virtual void SetBehaviourRotation(Quaternion rotation)
	{
	}

	[Token(Token = "0x600018D")]
	protected virtual void SetBehaviourRotation(Vector3 rotation)
	{
	}

	[Token(Token = "0x600018E")]
	protected virtual Vector3 GetBehaviourScale()
	{
		return default(Vector3);
	}

	[Token(Token = "0x600018F")]
	protected virtual void SetBehaviourScale(Vector3 scale)
	{
	}

	[Token(Token = "0x6000190")]
	protected abstract Color GetBehaviourTintColor();

	[Token(Token = "0x6000191")]
	protected abstract void SetBehaviourTintColor(Color tintColor);

	[Token(Token = "0x6000192")]
	protected virtual GameObject CreateHostObject()
	{
		return null;
	}

	[Token(Token = "0x6000193")]
	protected virtual string BuildActorCategory()
	{
		return null;
	}

	[Token(Token = "0x6000194")]
	protected virtual Transform GetOrCreateParent()
	{
		return null;
	}

	[Token(Token = "0x6000195")]
	private void CompletePositionTween()
	{
	}

	[Token(Token = "0x6000196")]
	private void CompleteRotationTween()
	{
	}

	[Token(Token = "0x6000197")]
	private void CompleteScaleTween()
	{
	}

	[Token(Token = "0x6000198")]
	private void CompleteTintColorTween()
	{
	}
}
