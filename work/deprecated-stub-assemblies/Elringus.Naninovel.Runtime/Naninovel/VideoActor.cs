#nullable enable annotations
﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.FX;
using UnityEngine;
using UnityEngine.Video;

namespace Naninovel
{
    [Token(Token = "0x20000C7")]
    public abstract class VideoActor<TMeta> : MonoBehaviourActor<TMeta>, Blur.IBlurable where TMeta : OrthoActorMetadata
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20000C8")]
    	[CompilerGenerated]
    	private struct _003CChangeAppearance_003Ed__27
    {
    		[Token(Token = "0x4000308")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000309")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400030A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public VideoActor<TMeta> _003C_003E4__this;

    		[Token(Token = "0x400030B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string appearance;

    		[Token(Token = "0x400030C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncToken token;

    		[Token(Token = "0x400030D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Tween tween;

    		[Token(Token = "0x400030E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Transition? transition;

    		[Token(Token = "0x400030F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private string _003CpreviousAppearance_003E5__2;

    		[Token(Token = "0x4000310")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private VideoAppearance _003CvideoAppearance_003E5__3;

    		[Token(Token = "0x4000311")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private VideoPlayer _003Cvideo_003E5__4;

    		[Token(Token = "0x4000312")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private RenderTexture _003CpreviousTexture_003E5__5;

    		[Token(Token = "0x4000313")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<VideoAppearance>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000314")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private YieldAwaitable.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x4000315")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__3;

    		[Token(Token = "0x60003F1")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60003F2")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000C9")]
    	[CompilerGenerated]
    	private struct _003CChangeVisibility_003Ed__28
    {
    		[Token(Token = "0x4000316")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000317")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000318")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public VideoActor<TMeta> _003C_003E4__this;

    		[Token(Token = "0x4000319")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public bool visible;

    		[Token(Token = "0x400031A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Tween tween;

    		[Token(Token = "0x400031B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncToken token;

    		[Token(Token = "0x400031C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60003F3")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60003F4")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000CA")]
    	[CompilerGenerated]
    	private struct _003CGetAppearance_003Ed__34
    {
    		[Token(Token = "0x400031D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400031E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<VideoAppearance> _003C_003Et__builder;

    		[Token(Token = "0x400031F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public VideoActor<TMeta> _003C_003E4__this;

    		[Token(Token = "0x4000320")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string videoName;

    		[Token(Token = "0x4000321")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncToken token;

    		[Token(Token = "0x4000322")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private VideoPlayer _003CvideoPlayer_003E5__2;

    		[Token(Token = "0x4000323")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000324")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<Resource<VideoClip>>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60003F5")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60003F6")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20000CB")]
    	[CompilerGenerated]
    	private struct _003CInitialize_003Ed__25
    {
    		[Token(Token = "0x4000325")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000326")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000327")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public VideoActor<TMeta> _003C_003E4__this;

    		[Token(Token = "0x4000328")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60003F7")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60003F8")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000303")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly Tweener<FloatTween> volumeTweener;

    	[Token(Token = "0x4000304")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly StandaloneAppearanceLoader<VideoClip> videoLoader;

    	[Token(Token = "0x4000305")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly string streamExtension;

    	[Token(Token = "0x4000306")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private string appearance;

    	[Token(Token = "0x4000307")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private bool visible;

    	[Token(Token = "0x170000A1")]
    	public override string Appearance
    	{
    		[Token(Token = "0x60003D6")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60003D7")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x170000A2")]
    	public override bool Visible
    	{
    		[Token(Token = "0x60003D8")]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x60003D9")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x170000A3")]
    	protected virtual TransitionalRenderer TransitionalRenderer
    	{
    		[Token(Token = "0x60003DA")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60003DB")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170000A4")]
    	protected virtual Dictionary<string, VideoAppearance> Appearances
    	{
    		[Token(Token = "0x60003DC")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170000A5")]
    	protected virtual int TextureDepthBuffer
    	{
    		[Token(Token = "0x60003DD")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x170000A6")]
    	protected virtual RenderTextureFormat TextureFormat
    	{
    		[Token(Token = "0x60003DE")]
    		get
    		{
    			return default(RenderTextureFormat);
    		}
    	}

    	[Token(Token = "0x170000A7")]
    	protected virtual string MixerGroup
    	{
    		[Token(Token = "0x60003DF")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60003E0")]
    	protected VideoActor(string id = default, TMeta meta = default, StandaloneAppearanceLoader<VideoClip> loader = default)
    	{
    	}

    	[Token(Token = "0x60003E1")]
    	[AsyncStateMachine(typeof(VideoActor<>._003CInitialize_003Ed__25))]
    	public override UniTask Initialize()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60003E2")]
    	public virtual UniTask Blur(float intensity, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60003E3")]
    	[AsyncStateMachine(typeof(VideoActor<>._003CChangeAppearance_003Ed__27))]
    	public override UniTask ChangeAppearance(string appearance, Tween tween, [Optional] Transition? transition, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60003E4")]
    	[AsyncStateMachine(typeof(VideoActor<>._003CChangeVisibility_003Ed__28))]
    	public override UniTask ChangeVisibility(bool visible, Tween tween, [Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60003E5")]
    	public override void Dispose()
    	{
    	}

    	[Token(Token = "0x60003E6")]
    	protected virtual void SetAppearance(string appearance)
    	{
    	}

    	[Token(Token = "0x60003E7")]
    	protected virtual void SetVisibility(bool visible)
    	{
    	}

    	[Token(Token = "0x60003E8")]
    	protected override Color GetBehaviourTintColor()
    	{
    		return default(Color);
    	}

    	[Token(Token = "0x60003E9")]
    	protected override void SetBehaviourTintColor(Color tintColor)
    	{
    	}

    	[Token(Token = "0x60003EA")]
    	[AsyncStateMachine(typeof(VideoActor<>._003CGetAppearance_003Ed__34))]
    	protected virtual UniTask<VideoAppearance> GetAppearance(string videoName, [Optional] AsyncToken token)
    	{
    		return default(UniTask<VideoAppearance>);
    	}

    	[Token(Token = "0x60003EB")]
    	protected virtual bool ShouldLoopAppearance(string appearance)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60003EC")]
    	protected virtual AudioSource SetupAudioSource(VideoPlayer player)
    	{
    		return null;
    	}

    	[Token(Token = "0x60003ED")]
    	protected virtual void ReleaseAppearance(string appearance)
    	{
    	}

    	[Token(Token = "0x60003EE")]
    	protected virtual void DisposeAppearancePlayer(VideoAppearance player)
    	{
    	}

    	[Token(Token = "0x60003EF")]
    	protected virtual void HandleAppearanceLocalized(Resource<VideoClip> resource)
    	{
    	}
    }
}
