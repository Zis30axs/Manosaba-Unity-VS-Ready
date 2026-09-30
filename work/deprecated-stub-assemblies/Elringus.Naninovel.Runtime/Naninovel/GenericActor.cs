#nullable enable annotations
﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000080")]
    public abstract class GenericActor<TBehaviour, TMeta> : MonoBehaviourActor<TMeta> where TBehaviour : GenericActorBehaviour where TMeta : ActorMetadata
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000081")]
    	[CompilerGenerated]
    	private struct _003CInitialize_003Ed__15
    {
    		[Token(Token = "0x40001CF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40001D0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40001D1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public GenericActor<TBehaviour, TMeta> _003C_003E4__this;

    		[Token(Token = "0x40001D2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40001D3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<Resource<GameObject>>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x40001D4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<GameObject>.Awaiter _003C_003Eu__3;

    		[Token(Token = "0x6000249")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600024A")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40001CB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly EmbeddedAppearanceLoader<GameObject> prefabLoader;

    	[Token(Token = "0x40001CC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private string appearance;

    	[Token(Token = "0x40001CD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private bool visible;

    	[Token(Token = "0x40001CE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private Color tintColor;

    	[Token(Token = "0x17000060")]
    	public virtual TBehaviour Behaviour
    	{
    		[Token(Token = "0x6000239")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x600023A")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000061")]
    	public override string Appearance
    	{
    		[Token(Token = "0x600023B")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x600023C")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000062")]
    	public override bool Visible
    	{
    		[Token(Token = "0x600023D")]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x600023E")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x600023F")]
    	protected GenericActor(string id = default, TMeta meta = default, EmbeddedAppearanceLoader<GameObject> loader = default)
    	{
    	}

    	[Token(Token = "0x6000240")]
    	[AsyncStateMachine(typeof(GenericActor<, >._003CInitialize_003Ed__15))]
    	public override UniTask Initialize()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000241")]
    	public override UniTask ChangeAppearance(string appearance, Tween tween, [Optional] Transition? transition, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000242")]
    	public override UniTask ChangeVisibility(bool visible, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000243")]
    	protected virtual void SetAppearance(string appearance)
    	{
    	}

    	[Token(Token = "0x6000244")]
    	protected virtual void SetVisibility(bool visible)
    	{
    	}

    	[Token(Token = "0x6000245")]
    	protected override Color GetBehaviourTintColor()
    	{
    		return default(Color);
    	}

    	[Token(Token = "0x6000246")]
    	protected override void SetBehaviourTintColor(Color tintColor)
    	{
    	}

    	[Token(Token = "0x6000247")]
    	public override void Dispose()
    	{
    	}
    }
}
