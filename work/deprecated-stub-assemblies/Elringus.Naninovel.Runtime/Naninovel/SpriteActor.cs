#nullable enable annotations
﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.FX;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20000B0")]
    public abstract class SpriteActor<TMeta> : MonoBehaviourActor<TMeta>, Blur.IBlurable where TMeta : OrthoActorMetadata
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20000B2")]
    	[CompilerGenerated]
    	private struct _003CChangeAppearance_003Ed__20
    {
    		[Token(Token = "0x4000297")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000298")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000299")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public SpriteActor<TMeta> _003C_003E4__this;

    		[Token(Token = "0x400029A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string appearance;

    		[Token(Token = "0x400029B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncToken token;

    		[Token(Token = "0x400029C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Tween tween;

    		[Token(Token = "0x400029D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Transition? transition;

    		[Token(Token = "0x400029E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private string _003CpreviousAppearance_003E5__2;

    		[Token(Token = "0x400029F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<Resource<Texture2D>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40002A0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600036C")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600036D")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000B3")]
    	[CompilerGenerated]
    	private struct _003CChangeVisibility_003Ed__21
    {
    		[Token(Token = "0x40002A1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40002A2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40002A3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public SpriteActor<TMeta> _003C_003E4__this;

    		[Token(Token = "0x40002A4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public bool visible;

    		[Token(Token = "0x40002A5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncToken token;

    		[Token(Token = "0x40002A6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Tween tween;

    		[Token(Token = "0x40002A7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600036E")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600036F")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000B4")]
    	[CompilerGenerated]
    	private struct _003CInitialize_003Ed__18
    {
    		[Token(Token = "0x40002A8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40002A9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40002AA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public SpriteActor<TMeta> _003C_003E4__this;

    		[Token(Token = "0x40002AB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000370")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000371")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000B5")]
    	[CompilerGenerated]
    	private struct _003CLoadAppearance_003Ed__27
    {
    		[Token(Token = "0x40002AC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40002AD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<Resource<Texture2D>> _003C_003Et__builder;

    		[Token(Token = "0x40002AE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public SpriteActor<TMeta> _003C_003E4__this;

    		[Token(Token = "0x40002AF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string appearance;

    		[Token(Token = "0x40002B0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncToken token;

    		[Token(Token = "0x40002B1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<Resource<Texture2D>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000372")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000373")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000B6")]
    	[CompilerGenerated]
    	private struct _003CLoadDefaultAppearance_003Ed__28
    {
    		[Token(Token = "0x40002B2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40002B3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<Resource<Texture2D>> _003C_003Et__builder;

    		[Token(Token = "0x40002B4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public SpriteActor<TMeta> _003C_003E4__this;

    		[Token(Token = "0x40002B5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncToken token;

    		[Token(Token = "0x40002B6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<string>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40002B7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<Resource<Texture2D>>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000374")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000375")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000B7")]
    	[CompilerGenerated]
    	private struct _003CLocateDefaultAppearance_003Ed__29
    {
    		[Token(Token = "0x40002B8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40002B9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<string> _003C_003Et__builder;

    		[Token(Token = "0x40002BA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public SpriteActor<TMeta> _003C_003E4__this;

    		[Token(Token = "0x40002BB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncToken token;

    		[Token(Token = "0x40002BC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<System.Collections.Generic.IReadOnlyCollection<string>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000376")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000377")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000290")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private string appearance;

    	[Token(Token = "0x4000291")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private bool visible;

    	[Token(Token = "0x4000292")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private string defaultAppearancePath;

    	[Token(Token = "0x4000293")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private Resource<Texture2D> defaultAppearance;

    	[Token(Token = "0x1700008F")]
    	public override string Appearance
    	{
    		[Token(Token = "0x600034F")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6000350")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000090")]
    	public override bool Visible
    	{
    		[Token(Token = "0x6000351")]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000352")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000091")]
    	protected virtual StandaloneAppearanceLoader<Texture2D> AppearanceLoader
    	{
    		[Token(Token = "0x6000353")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000092")]
    	protected virtual TransitionalRenderer TransitionalRenderer
    	{
    		[Token(Token = "0x6000354")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6000355")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x6000356")]
    	protected SpriteActor(string id = default, TMeta meta = default, StandaloneAppearanceLoader<Texture2D> loader = default)
    	{
    	}

    	[Token(Token = "0x6000357")]
    	[AsyncStateMachine(typeof(SpriteActor<>._003CInitialize_003Ed__18))]
    	public override UniTask Initialize()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000358")]
    	public virtual UniTask Blur(float intensity, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000359")]
    	[AsyncStateMachine(typeof(SpriteActor<>._003CChangeAppearance_003Ed__20))]
    	public override UniTask ChangeAppearance(string appearance, Tween tween, [Optional] Transition? transition, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600035A")]
    	[AsyncStateMachine(typeof(SpriteActor<>._003CChangeVisibility_003Ed__21))]
    	public override UniTask ChangeVisibility(bool visible, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600035B")]
    	public override void Dispose()
    	{
    	}

    	[Token(Token = "0x600035C")]
    	protected virtual void SetAppearance(string appearance)
    	{
    	}

    	[Token(Token = "0x600035D")]
    	protected virtual void SetVisibility(bool visible)
    	{
    	}

    	[Token(Token = "0x600035E")]
    	protected override Color GetBehaviourTintColor()
    	{
    		return default(Color);
    	}

    	[Token(Token = "0x600035F")]
    	protected override void SetBehaviourTintColor(Color tintColor)
    	{
    	}

    	[Token(Token = "0x6000360")]
    	[AsyncStateMachine(typeof(SpriteActor<>._003CLoadAppearance_003Ed__27))]
    	protected virtual UniTask<Resource<Texture2D>> LoadAppearance(string appearance, AsyncToken token)
    	{
    		return default(UniTask<Resource<Texture2D>>);
    	}

    	[Token(Token = "0x6000361")]
    	[AsyncStateMachine(typeof(SpriteActor<>._003CLoadDefaultAppearance_003Ed__28))]
    	protected virtual UniTask<Resource<Texture2D>> LoadDefaultAppearance(AsyncToken token)
    	{
    		return default(UniTask<Resource<Texture2D>>);
    	}

    	[Token(Token = "0x6000362")]
    	[AsyncStateMachine(typeof(SpriteActor<>._003CLocateDefaultAppearance_003Ed__29))]
    	protected virtual UniTask<string> LocateDefaultAppearance(AsyncToken token)
    	{
    		return default(UniTask<string>);
    	}

    	[Token(Token = "0x6000363")]
    	protected virtual void ApplyTextureSettings(Texture2D texture)
    	{
    	}

    	[Token(Token = "0x6000364")]
    	protected virtual void HandleAppearanceLocalized(Resource<Texture2D> resource)
    	{
    	}
    }
}
