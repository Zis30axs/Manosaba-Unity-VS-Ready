using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.Video;

namespace Naninovel
{
    [Token(Token = "0x20003B4")]
    [InitializeAtRuntime(0, null)]
    public class MoviePlayer : IMoviePlayer, IEngineService<MoviesConfiguration>, IEngineService
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20003B5")]
    	[CompilerGenerated]
    	private struct _003CHoldResources_003Ed__37
    {
    		[Token(Token = "0x4000DD3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000DD4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000DD5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public MoviePlayer _003C_003E4__this;

    		[Token(Token = "0x4000DD6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string movieName;

    		[Token(Token = "0x4000DD7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public object holder;

    		[Token(Token = "0x4000DD8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<Resource<VideoClip>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001207")]
    		[Address(RVA = "0x5981E0", Offset = "0x596BE0", VA = "0x1805981E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001208")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20003B6")]
    	[CompilerGenerated]
    	private struct _003CLoadMovieClip_003Ed__42
    {
    		[Token(Token = "0x4000DD9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000DDA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<VideoClip> _003C_003Et__builder;

    		[Token(Token = "0x4000DDB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public MoviePlayer _003C_003E4__this;

    		[Token(Token = "0x4000DDC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string movieName;

    		[Token(Token = "0x4000DDD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x4000DDE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask<Resource<VideoClip>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001209")]
    		[Address(RVA = "0x598430", Offset = "0x596E30", VA = "0x180598430", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600120A")]
    		[Address(RVA = "0x598710", Offset = "0x597110", VA = "0x180598710", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20003B7")]
    	[CompilerGenerated]
    	private struct _003CPlay_003Ed__35
    {
    		[Token(Token = "0x4000DDF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000DE0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<Texture> _003C_003Et__builder;

    		[Token(Token = "0x4000DE1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public MoviePlayer _003C_003E4__this;

    		[Token(Token = "0x4000DE2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string movieName;

    		[Token(Token = "0x4000DE3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x4000DE4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private VideoPlayer _003C_003E7__wrap1;

    		[Token(Token = "0x4000DE5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask<VideoClip>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000DE6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600120B")]
    		[Address(RVA = "0x598740", Offset = "0x597140", VA = "0x180598740", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600120C")]
    		[Address(RVA = "0x598F10", Offset = "0x597910", VA = "0x180598F10", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20003B8")]
    	[CompilerGenerated]
    	private struct _003CPreparePlayer_003Ed__43
    {
    		[Token(Token = "0x4000DE7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000DE8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000DE9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public MoviePlayer _003C_003E4__this;

    		[Token(Token = "0x4000DEA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4000DEB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600120D")]
    		[Address(RVA = "0x598F40", Offset = "0x597940", VA = "0x180598F40", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600120E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000DCB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	private readonly IInputManager input;

    	[Token(Token = "0x4000DCC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private readonly IResourceProviderManager resources;

    	[Token(Token = "0x4000DCD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private readonly ILocalizationManager l10n;

    	[Token(Token = "0x4000DCE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private readonly IAudioManager audio;

    	[Token(Token = "0x4000DCF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	private LocalizableResourceLoader<VideoClip> videoLoader;

    	[Token(Token = "0x4000DD0")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	private string playedMovieName;

    	[Token(Token = "0x4000DD1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	private IInputSampler skipInput;

    	[Token(Token = "0x4000DD2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    	private string streamExtension;

    	[Token(Token = "0x1700026D")]
    	public virtual MoviesConfiguration Configuration
    	{
    		[Token(Token = "0x60011EF")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "17")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700026E")]
    	public virtual bool Playing
    	{
    		[Token(Token = "0x60011F0")]
    		[Address(RVA = "0x456330", Offset = "0x454D30", VA = "0x180456330", Slot = "18")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x60011F1")]
    		[Address(RVA = "0x4B1840", Offset = "0x4B0240", VA = "0x1804B1840")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700026F")]
    	protected virtual VideoPlayer Player
    	{
    		[Token(Token = "0x60011F2")]
    		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0", Slot = "19")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60011F3")]
    		[Address(RVA = "0x58DF10", Offset = "0x58C910", VA = "0x18058DF10")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000270")]
    	protected virtual AudioSource AudioSource
    	{
    		[Token(Token = "0x60011F4")]
    		[Address(RVA = "0x347410", Offset = "0x345E10", VA = "0x180347410", Slot = "20")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60011F5")]
    		[Address(RVA = "0x30EBC0", Offset = "0x30D5C0", VA = "0x18030EBC0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000271")]
    	protected virtual bool UrlStreaming
    	{
    		[Token(Token = "0x60011F6")]
    		[Address(RVA = "0x596480", Offset = "0x594E80", VA = "0x180596480", Slot = "21")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x1400005F")]
    	public event Action OnMoviePlay
    	{
    		[Token(Token = "0x60011EB")]
    		[Address(RVA = "0x5960C0", Offset = "0x594AC0", VA = "0x1805960C0", Slot = "4")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60011EC")]
    		[Address(RVA = "0x5961B0", Offset = "0x594BB0", VA = "0x1805961B0", Slot = "5")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x14000060")]
    	public event Action OnMovieStop
    	{
    		[Token(Token = "0x60011ED")]
    		[Address(RVA = "0x5962A0", Offset = "0x594CA0", VA = "0x1805962A0", Slot = "6")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x60011EE")]
    		[Address(RVA = "0x596390", Offset = "0x594D90", VA = "0x180596390", Slot = "7")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x60011F7")]
    	[Address(RVA = "0x596520", Offset = "0x594F20", VA = "0x180596520")]
    	public MoviePlayer(MoviesConfiguration config = default, IResourceProviderManager resources = default, ILocalizationManager l10n = default, IInputManager input = default, IAudioManager audio = default)
    	{
    	}

    	[Token(Token = "0x60011F8")]
    	[Address(RVA = "0x5966D0", Offset = "0x5950D0", VA = "0x1805966D0", Slot = "22")]
    	public virtual UniTask InitializeService()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60011F9")]
    	[Address(RVA = "0x596A80", Offset = "0x595480", VA = "0x180596A80", Slot = "23")]
    	public virtual void ResetService()
    	{
    	}

    	[Token(Token = "0x60011FA")]
    	[Address(RVA = "0x596AF0", Offset = "0x5954F0", VA = "0x180596AF0", Slot = "24")]
    	public virtual void DestroyService()
    	{
    	}

    	[Token(Token = "0x60011FB")]
    	[Address(RVA = "0x596CD0", Offset = "0x5956D0", VA = "0x180596CD0", Slot = "25")]
    	[AsyncStateMachine(typeof(_003CPlay_003Ed__35))]
    	public virtual UniTask<Texture> Play(string movieName, [Optional] AsyncToken token)
    	{
    		return default(UniTask<Texture>);
    	}

    	[Token(Token = "0x60011FC")]
    	[Address(RVA = "0x596F20", Offset = "0x595920", VA = "0x180596F20", Slot = "26")]
    	public virtual void Stop()
    	{
    	}

    	[Token(Token = "0x60011FD")]
    	[Address(RVA = "0x5970E0", Offset = "0x595AE0", VA = "0x1805970E0", Slot = "27")]
    	[AsyncStateMachine(typeof(_003CHoldResources_003Ed__37))]
    	public virtual UniTask HoldResources(string movieName, object holder)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60011FE")]
    	[Address(RVA = "0x5972D0", Offset = "0x595CD0", VA = "0x1805972D0", Slot = "28")]
    	public virtual void ReleaseResources(string movieName, object holder)
    	{
    	}

    	[Token(Token = "0x60011FF")]
    	[Address(RVA = "0x597340", Offset = "0x595D40", VA = "0x180597340", Slot = "29")]
    	protected virtual VideoPlayer CreatePlayer()
    	{
    		return null;
    	}

    	[Token(Token = "0x6001200")]
    	[Address(RVA = "0x5978C0", Offset = "0x5962C0", VA = "0x1805978C0", Slot = "30")]
    	protected virtual AudioSource SetupAudioSource()
    	{
    		return null;
    	}

    	[Token(Token = "0x6001201")]
    	[Address(RVA = "0x597AC0", Offset = "0x5964C0", VA = "0x180597AC0", Slot = "31")]
    	protected virtual string BuildStreamUrl(string movieName)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001202")]
    	[Address(RVA = "0x597BD0", Offset = "0x5965D0", VA = "0x180597BD0", Slot = "32")]
    	[AsyncStateMachine(typeof(_003CLoadMovieClip_003Ed__42))]
    	protected virtual UniTask<VideoClip> LoadMovieClip(string movieName, AsyncToken token)
    	{
    		return default(UniTask<VideoClip>);
    	}

    	[Token(Token = "0x6001203")]
    	[Address(RVA = "0x597E30", Offset = "0x596830", VA = "0x180597E30", Slot = "33")]
    	[AsyncStateMachine(typeof(_003CPreparePlayer_003Ed__43))]
    	protected virtual UniTask PreparePlayer(AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001204")]
    	[Address(RVA = "0x597FC0", Offset = "0x5969C0", VA = "0x180597FC0", Slot = "34")]
    	protected virtual void SetIsPlaying(bool playing)
    	{
    	}

    	[Token(Token = "0x6001205")]
    	[Address(RVA = "0x597FF0", Offset = "0x5969F0", VA = "0x180597FF0", Slot = "35")]
    	protected virtual void HandlePlaybackError(VideoPlayer source, string message)
    	{
    	}
    }
}
