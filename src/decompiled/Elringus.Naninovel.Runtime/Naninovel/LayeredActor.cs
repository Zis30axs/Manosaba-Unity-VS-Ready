using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.FX;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x200008D")]
public abstract class LayeredActor<TBehaviour, TMeta> : MonoBehaviourActor<TMeta>, Blur.IBlurable where TBehaviour : LayeredActorBehaviour where TMeta : OrthoActorMetadata
{
	[StructLayout(3)]
	[Token(Token = "0x200008E")]
	[CompilerGenerated]
	private struct _003CChangeAppearance_003Ed__23
{
		[Token(Token = "0x40001F7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40001F8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40001F9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public string appearance;

		[Token(Token = "0x40001FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public LayeredActor<TBehaviour, TMeta> _003C_003E4__this;

		[Token(Token = "0x40001FB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Tween tween;

		[Token(Token = "0x40001FC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Transition? transition;

		[Token(Token = "0x40001FD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x40001FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private RenderTexture _003CpreviousTexture_003E5__2;

		[Token(Token = "0x40001FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000287")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000288")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200008F")]
	[CompilerGenerated]
	private struct _003CChangeVisibility_003Ed__24
{
		[Token(Token = "0x4000200")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000201")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000202")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public LayeredActor<TBehaviour, TMeta> _003C_003E4__this;

		[Token(Token = "0x4000203")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public bool visible;

		[Token(Token = "0x4000204")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public Tween tween;

		[Token(Token = "0x4000205")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncToken token;

		[Token(Token = "0x4000206")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000289")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600028A")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000090")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__21
{
		[Token(Token = "0x4000207")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000208")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000209")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public LayeredActor<TBehaviour, TMeta> _003C_003E4__this;

		[Token(Token = "0x400020A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400020B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<Resource<GameObject>>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x400020C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<GameObject>.Awaiter _003C_003Eu__3;

		[Token(Token = "0x600028B")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600028C")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40001F1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly EmbeddedAppearanceLoader<GameObject> prefabLoader;

	[Token(Token = "0x40001F2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private RenderTexture appearanceTexture;

	[Token(Token = "0x40001F3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private string defaultAppearance;

	[Token(Token = "0x40001F4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private string appearance;

	[Token(Token = "0x40001F5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private bool visible;

	[Token(Token = "0x40001F6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private bool perceivedVisibility;

	[Token(Token = "0x17000066")]
	public virtual TBehaviour Behaviour
	{
		[Token(Token = "0x6000272")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000273")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000067")]
	public override string Appearance
	{
		[Token(Token = "0x6000274")]
		get
		{
			return null;
		}
		[Token(Token = "0x6000275")]
		set
		{
		}
	}

	[Token(Token = "0x17000068")]
	public override bool Visible
	{
		[Token(Token = "0x6000276")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000277")]
		set
		{
		}
	}

	[Token(Token = "0x17000069")]
	protected virtual TransitionalRenderer TransitionalRenderer
	{
		[Token(Token = "0x6000278")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000279")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x600027A")]
	protected LayeredActor(string id = default, TMeta meta = default, EmbeddedAppearanceLoader<GameObject> loader = default)
	{
	}

	[Token(Token = "0x600027B")]
	[AsyncStateMachine(typeof(LayeredActor<, >._003CInitialize_003Ed__21))]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x600027C")]
	public virtual UniTask Blur(float intensity, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600027D")]
	[AsyncStateMachine(typeof(LayeredActor<, >._003CChangeAppearance_003Ed__23))]
	public override UniTask ChangeAppearance(string appearance, Tween tween, [Optional] Transition? transition, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600027E")]
	[AsyncStateMachine(typeof(LayeredActor<, >._003CChangeVisibility_003Ed__24))]
	public override UniTask ChangeVisibility(bool visible, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600027F")]
	public override void Dispose()
	{
	}

	[Token(Token = "0x6000280")]
	protected virtual string GetAppearance()
	{
		return null;
	}

	[Token(Token = "0x6000281")]
	protected virtual void SetAppearance(string appearance)
	{
	}

	[Token(Token = "0x6000282")]
	protected virtual void SetVisibility(bool visible)
	{
	}

	[Token(Token = "0x6000283")]
	protected override Color GetBehaviourTintColor()
	{
		return default(Color);
	}

	[Token(Token = "0x6000284")]
	protected override void SetBehaviourTintColor(Color tintColor)
	{
	}

	[Token(Token = "0x6000285")]
	protected virtual void RenderAppearance()
	{
	}
}
