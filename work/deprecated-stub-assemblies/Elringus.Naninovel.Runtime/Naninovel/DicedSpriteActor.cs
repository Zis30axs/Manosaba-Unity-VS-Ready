#nullable enable annotations
﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.FX;
using SpriteDicing;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000077")]
    public abstract class DicedSpriteActor<TMeta> : MonoBehaviourActor<TMeta>, Blur.IBlurable where TMeta : OrthoActorMetadata
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000079")]
    	[CompilerGenerated]
    	private struct _003CChangeAppearance_003Ed__21
    {
    		[Token(Token = "0x40001AB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40001AC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40001AD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public DicedSpriteActor<TMeta> _003C_003E4__this;

    		[Token(Token = "0x40001AE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string appearance;

    		[Token(Token = "0x40001AF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncToken token;

    		[Token(Token = "0x40001B0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Tween tween;

    		[Token(Token = "0x40001B1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Transition? transition;

    		[Token(Token = "0x40001B2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private RenderTexture _003CrenderTexture_003E5__2;

    		[Token(Token = "0x40001B3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<DicedSpriteAtlas>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40001B4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000227")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000228")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200007A")]
    	[CompilerGenerated]
    	private struct _003CChangeVisibility_003Ed__22
    {
    		[Token(Token = "0x40001B5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40001B6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40001B7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public DicedSpriteActor<TMeta> _003C_003E4__this;

    		[Token(Token = "0x40001B8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public bool visible;

    		[Token(Token = "0x40001B9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Tween tween;

    		[Token(Token = "0x40001BA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncToken token;

    		[Token(Token = "0x40001BB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000229")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600022A")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200007B")]
    	[CompilerGenerated]
    	private struct _003CGetOrLoadAtlas_003Ed__30
    {
    		[Token(Token = "0x40001BC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40001BD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<DicedSpriteAtlas> _003C_003Et__builder;

    		[Token(Token = "0x40001BE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public DicedSpriteActor<TMeta> _003C_003E4__this;

    		[Token(Token = "0x40001BF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncToken token;

    		[Token(Token = "0x40001C0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<Resource<DicedSpriteAtlas>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600022B")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600022C")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200007C")]
    	[CompilerGenerated]
    	private struct _003CInitialize_003Ed__20
    {
    		[Token(Token = "0x40001C1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40001C2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40001C3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public DicedSpriteActor<TMeta> _003C_003E4__this;

    		[Token(Token = "0x40001C4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600022D")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600022E")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400019D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly OrthoActorMetadata meta;

    	[Token(Token = "0x400019E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly Material renderMaterial;

    	[Token(Token = "0x400019F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly Mesh renderMesh;

    	[Token(Token = "0x40001A0")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly List<Vector3> vertices;

    	[Token(Token = "0x40001A1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly EmbeddedAppearanceLoader<DicedSpriteAtlas> atlasLoader;

    	[Token(Token = "0x40001A2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private RenderTexture appearanceTexture;

    	[Token(Token = "0x40001A3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private string appearance;

    	[Token(Token = "0x40001A4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private bool visible;

    	[Token(Token = "0x1700005C")]
    	public override string Appearance
    	{
    		[Token(Token = "0x6000208")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6000209")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700005D")]
    	public override bool Visible
    	{
    		[Token(Token = "0x600020A")]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x600020B")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700005E")]
    	protected virtual TransitionalRenderer TransitionalRenderer
    	{
    		[Token(Token = "0x600020C")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x600020D")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x600020E")]
    	protected DicedSpriteActor(string id = default, TMeta meta = default, EmbeddedAppearanceLoader<DicedSpriteAtlas> loader = default)
    	{
    	}

    	[Token(Token = "0x600020F")]
    	public virtual UniTask Blur(float intensity, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000210")]
    	[AsyncStateMachine(typeof(DicedSpriteActor<>._003CInitialize_003Ed__20))]
    	public override UniTask Initialize()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000211")]
    	[AsyncStateMachine(typeof(DicedSpriteActor<>._003CChangeAppearance_003Ed__21))]
    	public override UniTask ChangeAppearance(string appearance, Tween tween, [Optional] Transition? transition, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000212")]
    	[AsyncStateMachine(typeof(DicedSpriteActor<>._003CChangeVisibility_003Ed__22))]
    	public override UniTask ChangeVisibility(bool visible, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000213")]
    	public override void Dispose()
    	{
    	}

    	[Token(Token = "0x6000214")]
    	protected virtual void SetAppearance(string appearance)
    	{
    	}

    	[Token(Token = "0x6000215")]
    	protected virtual void SetVisibility(bool visible)
    	{
    	}

    	[Token(Token = "0x6000216")]
    	protected override Color GetBehaviourTintColor()
    	{
    		return default(Color);
    	}

    	[Token(Token = "0x6000217")]
    	protected override void SetBehaviourTintColor(Color tintColor)
    	{
    	}

    	[Token(Token = "0x6000218")]
    	protected virtual Sprite GetDefaultSprite(DicedSpriteAtlas atlas)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000219")]
    	protected virtual Sprite GetSprite(string appearance, DicedSpriteAtlas atlas)
    	{
    		return null;
    	}

    	[Token(Token = "0x600021A")]
    	[AsyncStateMachine(typeof(DicedSpriteActor<>._003CGetOrLoadAtlas_003Ed__30))]
    	protected virtual UniTask<DicedSpriteAtlas> GetOrLoadAtlas(AsyncToken token)
    	{
    		return default(UniTask<DicedSpriteAtlas>);
    	}

    	[Token(Token = "0x600021B")]
    	protected virtual void RebuildRenderMesh(Sprite dicedSprite)
    	{
    	}

    	[Token(Token = "0x600021C")]
    	protected virtual RenderTexture RenderToTexture(Sprite dicedSprite)
    	{
    		return null;
    	}

    	[Token(Token = "0x600021D")]
    	private static RenderTexture GetRenderTexture(Rect spriteRect, float ppu)
    	{
    		return null;
    	}

    	[Token(Token = "0x600021E")]
    	private static Rect GetSpriteRect(Sprite sprite)
    	{
    		return default(Rect);
    	}
    }
}
