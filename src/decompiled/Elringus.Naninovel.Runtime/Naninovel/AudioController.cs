using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

namespace Naninovel;

[Token(Token = "0x20001FD")]
public class AudioController : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x20001FF")]
	[CompilerGenerated]
	private struct _003CPlayClip_003Ed__17
{
		[Token(Token = "0x400086B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400086C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400086D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AudioClip clip;

		[Token(Token = "0x400086E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public bool additive;

		[Token(Token = "0x400086F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AudioController _003C_003E4__this;

		[Token(Token = "0x4000870")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AudioSource audioSource;

		[Token(Token = "0x4000871")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public float volume;

		[Token(Token = "0x4000872")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
		public bool loop;

		[Token(Token = "0x4000873")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public AudioMixerGroup mixerGroup;

		[Token(Token = "0x4000874")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		public AudioClip introClip;

		[Token(Token = "0x4000875")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		public float fadeInTime;

		[Token(Token = "0x4000876")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		public AsyncToken token;

		[Token(Token = "0x4000877")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60008EB")]
		[Address(RVA = "0x521F60", Offset = "0x520960", VA = "0x180521F60", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60008EC")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000200")]
	[CompilerGenerated]
	private struct _003CStopAllClips_003Ed__21
{
		[Token(Token = "0x4000878")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000879")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400087A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AudioController _003C_003E4__this;

		[Token(Token = "0x400087B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float fadeOutTime;

		[Token(Token = "0x400087C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x400087D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private PooledObject<List<UniTask>> _003C__003E5__2;

		[Token(Token = "0x400087E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60008ED")]
		[Address(RVA = "0x522620", Offset = "0x521020", VA = "0x180522620", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60008EE")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000201")]
	[CompilerGenerated]
	private struct _003CStopClip_003Ed__20
{
		[Token(Token = "0x400087F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000880")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000881")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AudioClip clip;

		[Token(Token = "0x4000882")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AudioController _003C_003E4__this;

		[Token(Token = "0x4000883")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float fadeOutTime;

		[Token(Token = "0x4000884")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken token;

		[Token(Token = "0x4000885")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private PooledObject<List<UniTask>> _003C__003E5__2;

		[Token(Token = "0x4000886")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60008EF")]
		[Address(RVA = "0x522D50", Offset = "0x521750", VA = "0x180522D50", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60008F0")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000866")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private readonly List<AudioTrack> audioTracks;

	[Token(Token = "0x4000867")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private readonly Stack<AudioSource> sourcesPool;

	[Token(Token = "0x4000868")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private AudioListener listenerCache;

	[Token(Token = "0x4000869")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private Tweener<FloatTween> listenerVolumeTweener;

	[Token(Token = "0x1700013D")]
	public AudioListener Listener
	{
		[Token(Token = "0x60008D2")]
		[Address(RVA = "0x51FC60", Offset = "0x51E660", VA = "0x18051FC60")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700013E")]
	public float Volume
	{
		[Token(Token = "0x60008D3")]
		[Address(RVA = "0x51FD50", Offset = "0x51E750", VA = "0x18051FD50")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x60008D4")]
		[Address(RVA = "0x51FDA0", Offset = "0x51E7A0", VA = "0x18051FDA0")]
		set
		{
		}
	}

	[Token(Token = "0x1700013F")]
	public bool Mute
	{
		[Token(Token = "0x60008D5")]
		[Address(RVA = "0x51FE00", Offset = "0x51E800", VA = "0x18051FE00")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x60008D6")]
		[Address(RVA = "0x51FE50", Offset = "0x51E850", VA = "0x18051FE50")]
		set
		{
		}
	}

	[Token(Token = "0x60008D7")]
	[Address(RVA = "0x51FEA0", Offset = "0x51E8A0", VA = "0x18051FEA0")]
	private void Awake()
	{
	}

	[Token(Token = "0x60008D8")]
	[Address(RVA = "0x51FF40", Offset = "0x51E940", VA = "0x18051FF40")]
	public void AttachListener(Transform target)
	{
	}

	[Token(Token = "0x60008D9")]
	[Address(RVA = "0x520080", Offset = "0x51EA80", VA = "0x180520080")]
	public void FadeVolume(float volume, float time)
	{
	}

	[Token(Token = "0x60008DA")]
	[Address(RVA = "0x5202D0", Offset = "0x51ECD0", VA = "0x1805202D0")]
	public bool ClipPlaying(AudioClip clip)
	{
		return default(bool);
	}

	[Token(Token = "0x60008DB")]
	[Address(RVA = "0x5205B0", Offset = "0x51EFB0", VA = "0x1805205B0")]
	public void PlayClip(AudioClip clip, AudioSource audioSource = default, float volume = 1f, bool loop = false, AudioMixerGroup mixerGroup = default, AudioClip introClip = default, bool additive = false)
	{
	}

	[Token(Token = "0x60008DC")]
	[Address(RVA = "0x5208B0", Offset = "0x51F2B0", VA = "0x1805208B0")]
	[AsyncStateMachine(typeof(_003CPlayClip_003Ed__17))]
	public UniTask PlayClip(AudioClip clip, float fadeInTime, AudioSource audioSource = default, float volume = 1f, bool loop = false, AudioMixerGroup mixerGroup = default, AudioClip introClip = default, bool additive = false, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60008DD")]
	[Address(RVA = "0x520BD0", Offset = "0x51F5D0", VA = "0x180520BD0")]
	public void StopClip(AudioClip clip)
	{
	}

	[Token(Token = "0x60008DE")]
	[Address(RVA = "0x520E60", Offset = "0x51F860", VA = "0x180520E60")]
	public void StopAllClips()
	{
	}

	[Token(Token = "0x60008DF")]
	[Address(RVA = "0x521060", Offset = "0x51FA60", VA = "0x180521060")]
	[AsyncStateMachine(typeof(_003CStopClip_003Ed__20))]
	public UniTask StopClip(AudioClip clip, float fadeOutTime, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60008E0")]
	[Address(RVA = "0x521270", Offset = "0x51FC70", VA = "0x180521270")]
	[AsyncStateMachine(typeof(_003CStopAllClips_003Ed__21))]
	public UniTask StopAllClips(float fadeOutTime, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60008E1")]
	[Address(RVA = "0x521420", Offset = "0x51FE20", VA = "0x180521420")]
	public IReadOnlyCollection<AudioTrack> GetTracks(AudioClip clip)
	{
		return null;
	}

	[Token(Token = "0x60008E2")]
	[Address(RVA = "0x521690", Offset = "0x520090", VA = "0x180521690")]
	public IReadOnlyCollection<AudioTrack> GetAllTracks()
	{
		return null;
	}

	[Token(Token = "0x60008E3")]
	[Address(RVA = "0x5216E0", Offset = "0x5200E0", VA = "0x1805216E0")]
	private AudioListener FindOrAddListener()
	{
		return null;
	}

	[Token(Token = "0x60008E4")]
	[Address(RVA = "0x5218C0", Offset = "0x5202C0", VA = "0x1805218C0")]
	private bool IsOwnedByController(AudioSource audioSource)
	{
		return default(bool);
	}

	[Token(Token = "0x60008E5")]
	[Address(RVA = "0x521AB0", Offset = "0x5204B0", VA = "0x180521AB0")]
	private AudioSource GetPooledSource()
	{
		return null;
	}

	[Token(Token = "0x60008E6")]
	[Address(RVA = "0x521B60", Offset = "0x520560", VA = "0x180521B60")]
	private void PoolUnusedSources()
	{
	}

	[Token(Token = "0x60008E7")]
	[Address(RVA = "0x521C80", Offset = "0x520680", VA = "0x180521C80")]
	public AudioController()
	{
	}
}
