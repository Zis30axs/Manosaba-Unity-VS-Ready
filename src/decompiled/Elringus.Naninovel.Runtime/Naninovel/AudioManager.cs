using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

namespace Naninovel;

[Token(Token = "0x20000EB")]
[InitializeAtRuntime(0, null)]
public class AudioManager : IStatefulService<SettingsStateMap>, IEngineService, IStatefulService<GameStateMap>, IAudioManager, IEngineService<AudioConfiguration>
{
	[Serializable]
	[Token(Token = "0x20000EC")]
	public class Settings
	{
		[Token(Token = "0x400037C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public float MasterVolume;

		[Token(Token = "0x400037D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
		public float BgmVolume;

		[Token(Token = "0x400037E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public float SfxVolume;

		[Token(Token = "0x400037F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		public float VoiceVolume;

		[Token(Token = "0x4000380")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string VoiceLocale;

		[Token(Token = "0x4000381")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public List<NamedFloat> AuthorVolume;

		[Token(Token = "0x6000467")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public Settings()
		{
		}
	}

	[Serializable]
	[Token(Token = "0x20000ED")]
	public class GameState
	{
		[Token(Token = "0x4000382")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public List<AudioClipState> BgmClips;

		[Token(Token = "0x4000383")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public List<AudioClipState> SfxClips;

		[Token(Token = "0x6000468")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public GameState()
		{
		}
	}

	[Token(Token = "0x20000EE")]
	private class AuthorSource
	{
		[Token(Token = "0x4000384")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public CharacterMetadata Metadata;

		[Token(Token = "0x4000385")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AudioSource Source;

		[Token(Token = "0x6000469")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public AuthorSource()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000F6")]
	[CompilerGenerated]
	private struct _003CAudioExists_003Ed__59
{
		[Token(Token = "0x4000397")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000398")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

		[Token(Token = "0x4000399")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x400039A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string path;

		[Token(Token = "0x400039B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<bool>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000479")]
		[Address(RVA = "0x47DB50", Offset = "0x47C550", VA = "0x18047DB50", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600047A")]
		[Address(RVA = "0x47DDE0", Offset = "0x47C7E0", VA = "0x18047DDE0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000F7")]
	[CompilerGenerated]
	private struct _003CGetOrInstantiateAuthorSource_003Ed__83
{
		[Token(Token = "0x400039C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400039D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<AudioSource> _003C_003Et__builder;

		[Token(Token = "0x400039E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x400039F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string authorId;

		[Token(Token = "0x40003A0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<AudioSource>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600047B")]
		[Address(RVA = "0x47DE10", Offset = "0x47C810", VA = "0x18047DE10", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600047C")]
		[Address(RVA = "0x47E350", Offset = "0x47CD50", VA = "0x18047E350", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000F8")]
	[CompilerGenerated]
	private struct _003CLoadServiceState_003Ed__55
{
		[Token(Token = "0x40003A1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003A2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003A3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GameStateMap stateMap;

		[Token(Token = "0x40003A4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x40003A5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private PooledObject<List<UniTask>> _003C__003E5__2;

		[Token(Token = "0x40003A6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600047D")]
		[Address(RVA = "0x47E380", Offset = "0x47CD80", VA = "0x18047E380", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600047E")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000F9")]
	[CompilerGenerated]
	private struct _003CModifyAudio_003Ed__79
{
		[Token(Token = "0x40003A7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003A8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003A9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x40003AA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string path;

		[Token(Token = "0x40003AB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public bool loop;

		[Token(Token = "0x40003AC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public float time;

		[Token(Token = "0x40003AD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public float volume;

		[Token(Token = "0x40003AE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public AsyncToken token;

		[Token(Token = "0x40003AF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600047F")]
		[Address(RVA = "0x47F400", Offset = "0x47DE00", VA = "0x18047F400", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000480")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000FA")]
	[CompilerGenerated]
	private struct _003CModifyBgm_003Ed__61
{
		[Token(Token = "0x40003B0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003B1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003B2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x40003B3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string path;

		[Token(Token = "0x40003B4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float volume;

		[Token(Token = "0x40003B5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public bool loop;

		[Token(Token = "0x40003B6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public float time;

		[Token(Token = "0x40003B7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public AsyncToken token;

		[Token(Token = "0x40003B8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000481")]
		[Address(RVA = "0x47F7C0", Offset = "0x47E1C0", VA = "0x18047F7C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000482")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000FB")]
	[CompilerGenerated]
	private struct _003CModifySfx_003Ed__62
{
		[Token(Token = "0x40003B9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003BA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003BB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x40003BC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string path;

		[Token(Token = "0x40003BD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float volume;

		[Token(Token = "0x40003BE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public bool loop;

		[Token(Token = "0x40003BF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public float time;

		[Token(Token = "0x40003C0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public AsyncToken token;

		[Token(Token = "0x40003C1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000483")]
		[Address(RVA = "0x47FBB0", Offset = "0x47E5B0", VA = "0x18047FBB0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000484")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000FC")]
	[CompilerGenerated]
	private struct _003CPlayBgm_003Ed__64
{
		[Token(Token = "0x40003C2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003C3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003C4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x40003C5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string path;

		[Token(Token = "0x40003C6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x40003C7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public float volume;

		[Token(Token = "0x40003C8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
		public bool loop;

		[Token(Token = "0x40003C9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public string introPath;

		[Token(Token = "0x40003CA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		public float fadeTime;

		[Token(Token = "0x40003CB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		public string group;

		[Token(Token = "0x40003CC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private Resource<AudioClip> _003CclipResource_003E5__2;

		[Token(Token = "0x40003CD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
		private UniTask<Resource<AudioClip>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40003CE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000485")]
		[Address(RVA = "0x47FFA0", Offset = "0x47E9A0", VA = "0x18047FFA0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000486")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000FD")]
	[CompilerGenerated]
	private struct _003CPlaySfx_003Ed__67
{
		[Token(Token = "0x40003CF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003D0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003D1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x40003D2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string path;

		[Token(Token = "0x40003D3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x40003D4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public float volume;

		[Token(Token = "0x40003D5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
		public bool loop;

		[Token(Token = "0x40003D6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public float fadeTime;

		[Token(Token = "0x40003D7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		public string group;

		[Token(Token = "0x40003D8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private UniTask<Resource<AudioClip>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40003D9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000487")]
		[Address(RVA = "0x480A10", Offset = "0x47F410", VA = "0x180480A10", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000488")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000FE")]
	[CompilerGenerated]
	private struct _003CPlaySfxFast_003Ed__63
{
		[Token(Token = "0x40003DA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003DB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003DC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x40003DD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string path;

		[Token(Token = "0x40003DE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public bool restart;

		[Token(Token = "0x40003DF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x29")]
		public bool additive;

		[Token(Token = "0x40003E0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public float volume;

		[Token(Token = "0x40003E1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string group;

		[Token(Token = "0x40003E2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<Resource>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000489")]
		[Address(RVA = "0x4811C0", Offset = "0x47FBC0", VA = "0x1804811C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600048A")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000FF")]
	[CompilerGenerated]
	private struct _003CPlayVoice_003Ed__70
{
		[Token(Token = "0x40003E3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003E4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003E5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x40003E6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string path;

		[Token(Token = "0x40003E7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x40003E8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public string authorId;

		[Token(Token = "0x40003E9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public float volume;

		[Token(Token = "0x40003EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		public string group;

		[Token(Token = "0x40003EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private Resource<AudioClip> _003CclipResource_003E5__2;

		[Token(Token = "0x40003EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private UniTask<Resource<AudioClip>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40003ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
		private UniTask<AudioSource>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600048B")]
		[Address(RVA = "0x481700", Offset = "0x480100", VA = "0x180481700", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600048C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000100")]
	[CompilerGenerated]
	private struct _003CStopAllBgm_003Ed__66
{
		[Token(Token = "0x40003EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x40003F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float fadeTime;

		[Token(Token = "0x40003F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x40003F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600048D")]
		[Address(RVA = "0x481F20", Offset = "0x480920", VA = "0x180481F20", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600048E")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000101")]
	[CompilerGenerated]
	private struct _003CStopAllSfx_003Ed__69
{
		[Token(Token = "0x40003F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003F5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003F6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x40003F7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float fadeTime;

		[Token(Token = "0x40003F8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x40003F9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600048F")]
		[Address(RVA = "0x4823F0", Offset = "0x480DF0", VA = "0x1804823F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000490")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000102")]
	[CompilerGenerated]
	private struct _003CStopBgm_003Ed__65
{
		[Token(Token = "0x40003FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40003FB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40003FC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public string path;

		[Token(Token = "0x40003FD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x40003FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float fadeTime;

		[Token(Token = "0x40003FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken token;

		[Token(Token = "0x4000400")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000491")]
		[Address(RVA = "0x4828C0", Offset = "0x4812C0", VA = "0x1804828C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000492")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000103")]
	[CompilerGenerated]
	private struct _003CStopSfx_003Ed__68
{
		[Token(Token = "0x4000401")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000402")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000403")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public string path;

		[Token(Token = "0x4000404")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x4000405")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float fadeTime;

		[Token(Token = "0x4000406")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken token;

		[Token(Token = "0x4000407")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000493")]
		[Address(RVA = "0x482D50", Offset = "0x481750", VA = "0x180482D50", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000494")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000104")]
	[CompilerGenerated]
	private struct _003CVoiceExists_003Ed__60
{
		[Token(Token = "0x4000408")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000409")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<bool> _003C_003Et__builder;

		[Token(Token = "0x400040A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AudioManager _003C_003E4__this;

		[Token(Token = "0x400040B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string path;

		[Token(Token = "0x400040C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<bool>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000495")]
		[Address(RVA = "0x4831E0", Offset = "0x481BE0", VA = "0x1804831E0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000496")]
		[Address(RVA = "0x483470", Offset = "0x481E70", VA = "0x180483470", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400036E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private readonly IResourceProviderManager resources;

	[Token(Token = "0x400036F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private readonly ILocalizationManager l10n;

	[Token(Token = "0x4000370")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private readonly ICharacterManager chars;

	[Token(Token = "0x4000371")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private readonly Dictionary<string, AudioClipState> bgmMap;

	[Token(Token = "0x4000372")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private readonly Dictionary<string, AudioClipState> sfxMap;

	[Token(Token = "0x4000373")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private readonly Dictionary<string, float> authorVolume;

	[Token(Token = "0x4000374")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private readonly Dictionary<string, AuthorSource> authorSources;

	[Token(Token = "0x4000375")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private AudioMixerGroup bgmGroup;

	[Token(Token = "0x4000376")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private AudioMixerGroup sfxGroup;

	[Token(Token = "0x4000377")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private AudioMixerGroup voiceGroup;

	[Token(Token = "0x4000378")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	private LocalizableResourceLoader<AudioClip> audioLoader;

	[Token(Token = "0x4000379")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	private LocalizableResourceLoader<AudioClip> voiceLoader;

	[Token(Token = "0x400037A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	private IAudioPlayer audioPlayer;

	[Token(Token = "0x400037B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private AudioClipState? voiceClip;

	[Token(Token = "0x170000B1")]
	public virtual AudioConfiguration Configuration
	{
		[Token(Token = "0x6000430")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "46")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000B2")]
	public virtual AudioMixer AudioMixer
	{
		[Token(Token = "0x6000431")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "47")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000B3")]
	public virtual float MasterVolume
	{
		[Token(Token = "0x6000432")]
		[Address(RVA = "0x477E20", Offset = "0x476820", VA = "0x180477E20", Slot = "48")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6000433")]
		[Address(RVA = "0x477E70", Offset = "0x476870", VA = "0x180477E70", Slot = "49")]
		set
		{
		}
	}

	[Token(Token = "0x170000B4")]
	public virtual float BgmVolume
	{
		[Token(Token = "0x6000434")]
		[Address(RVA = "0x477ED0", Offset = "0x4768D0", VA = "0x180477ED0", Slot = "50")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6000435")]
		[Address(RVA = "0x477F20", Offset = "0x476920", VA = "0x180477F20", Slot = "51")]
		set
		{
		}
	}

	[Token(Token = "0x170000B5")]
	public virtual float SfxVolume
	{
		[Token(Token = "0x6000436")]
		[Address(RVA = "0x477FA0", Offset = "0x4769A0", VA = "0x180477FA0", Slot = "52")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6000437")]
		[Address(RVA = "0x477FF0", Offset = "0x4769F0", VA = "0x180477FF0", Slot = "53")]
		set
		{
		}
	}

	[Token(Token = "0x170000B6")]
	public virtual float VoiceVolume
	{
		[Token(Token = "0x6000438")]
		[Address(RVA = "0x478070", Offset = "0x476A70", VA = "0x180478070", Slot = "54")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6000439")]
		[Address(RVA = "0x4780C0", Offset = "0x476AC0", VA = "0x1804780C0", Slot = "55")]
		set
		{
		}
	}

	[Token(Token = "0x170000B7")]
	public virtual string VoiceLocale
	{
		[Token(Token = "0x600043A")]
		[Address(RVA = "0x478140", Offset = "0x476B40", VA = "0x180478140", Slot = "56")]
		get
		{
			return null;
		}
		[Token(Token = "0x600043B")]
		[Address(RVA = "0x478190", Offset = "0x476B90", VA = "0x180478190", Slot = "57")]
		set
		{
		}
	}

	[Token(Token = "0x170000B8")]
	public virtual IResourceLoader AudioLoader
	{
		[Token(Token = "0x600043C")]
		[Address(RVA = "0x478200", Offset = "0x476C00", VA = "0x180478200", Slot = "58")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000B9")]
	public virtual IResourceLoader VoiceLoader
	{
		[Token(Token = "0x600043D")]
		[Address(RVA = "0x478210", Offset = "0x476C10", VA = "0x180478210", Slot = "59")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000BA")]
	protected virtual bool BgmGroupAvailable
	{
		[Token(Token = "0x600043E")]
		[Address(RVA = "0x478220", Offset = "0x476C20", VA = "0x180478220", Slot = "60")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x170000BB")]
	protected virtual bool SfxGroupAvailable
	{
		[Token(Token = "0x600043F")]
		[Address(RVA = "0x4782F0", Offset = "0x476CF0", VA = "0x1804782F0", Slot = "61")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x170000BC")]
	protected virtual bool VoiceGroupAvailable
	{
		[Token(Token = "0x6000440")]
		[Address(RVA = "0x4783C0", Offset = "0x476DC0", VA = "0x1804783C0", Slot = "62")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6000441")]
	[Address(RVA = "0x478490", Offset = "0x476E90", VA = "0x180478490")]
	public AudioManager(AudioConfiguration config = default, IResourceProviderManager resources = default, ILocalizationManager l10n = default, ICharacterManager chars = default)
	{
	}

	[Token(Token = "0x6000442")]
	[Address(RVA = "0x4789C0", Offset = "0x4773C0", VA = "0x1804789C0", Slot = "63")]
	public virtual UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000443")]
	[Address(RVA = "0x479090", Offset = "0x477A90", VA = "0x180479090", Slot = "64")]
	public virtual void ResetService()
	{
	}

	[Token(Token = "0x6000444")]
	[Address(RVA = "0x479190", Offset = "0x477B90", VA = "0x180479190", Slot = "65")]
	public virtual void DestroyService()
	{
	}

	[Token(Token = "0x6000445")]
	[Address(RVA = "0x479240", Offset = "0x477C40", VA = "0x180479240", Slot = "66")]
	public virtual void SaveServiceState(SettingsStateMap stateMap)
	{
	}

	[Token(Token = "0x6000446")]
	[Address(RVA = "0x4795B0", Offset = "0x477FB0", VA = "0x1804795B0", Slot = "67")]
	public virtual UniTask LoadServiceState(SettingsStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000447")]
	[Address(RVA = "0x479AB0", Offset = "0x4784B0", VA = "0x180479AB0", Slot = "68")]
	public virtual void SaveServiceState(GameStateMap stateMap)
	{
	}

	[Token(Token = "0x6000448")]
	[Address(RVA = "0x479E70", Offset = "0x478870", VA = "0x180479E70", Slot = "69")]
	[AsyncStateMachine(typeof(_003CLoadServiceState_003Ed__55))]
	public virtual UniTask LoadServiceState(GameStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000449")]
	[Address(RVA = "0x479FF0", Offset = "0x4789F0", VA = "0x180479FF0", Slot = "70")]
	public virtual void GetPlayedBgm(ICollection<string> paths)
	{
	}

	[Token(Token = "0x600044A")]
	[Address(RVA = "0x47A180", Offset = "0x478B80", VA = "0x18047A180", Slot = "71")]
	public virtual void GetPlayedSfx(ICollection<string> paths)
	{
	}

	[Token(Token = "0x600044B")]
	[Address(RVA = "0x47A310", Offset = "0x478D10", VA = "0x18047A310", Slot = "72")]
	public virtual string GetPlayedVoice()
	{
		return null;
	}

	[Token(Token = "0x600044C")]
	[Address(RVA = "0x47A3F0", Offset = "0x478DF0", VA = "0x18047A3F0", Slot = "73")]
	[AsyncStateMachine(typeof(_003CAudioExists_003Ed__59))]
	public virtual UniTask<bool> AudioExists(string path)
	{
		return default(UniTask<bool>);
	}

	[Token(Token = "0x600044D")]
	[Address(RVA = "0x47A5E0", Offset = "0x478FE0", VA = "0x18047A5E0", Slot = "74")]
	[AsyncStateMachine(typeof(_003CVoiceExists_003Ed__60))]
	public virtual UniTask<bool> VoiceExists(string path)
	{
		return default(UniTask<bool>);
	}

	[Token(Token = "0x600044E")]
	[Address(RVA = "0x47A7D0", Offset = "0x4791D0", VA = "0x18047A7D0", Slot = "75")]
	[AsyncStateMachine(typeof(_003CModifyBgm_003Ed__61))]
	public virtual UniTask ModifyBgm(string path, float volume, bool loop, float time, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600044F")]
	[Address(RVA = "0x47AA00", Offset = "0x479400", VA = "0x18047AA00", Slot = "76")]
	[AsyncStateMachine(typeof(_003CModifySfx_003Ed__62))]
	public virtual UniTask ModifySfx(string path, float volume, bool loop, float time, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000450")]
	[Address(RVA = "0x47AC30", Offset = "0x479630", VA = "0x18047AC30", Slot = "77")]
	[AsyncStateMachine(typeof(_003CPlaySfxFast_003Ed__63))]
	public virtual UniTask PlaySfxFast(string path, float volume = 1f, string group = default, bool restart = true, bool additive = true)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000451")]
	[Address(RVA = "0x47AE50", Offset = "0x479850", VA = "0x18047AE50", Slot = "78")]
	[AsyncStateMachine(typeof(_003CPlayBgm_003Ed__64))]
	public virtual UniTask PlayBgm(string path, float volume = 1f, float fadeTime = 0f, bool loop = true, string introPath = default, string group = default, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000452")]
	[Address(RVA = "0x47B110", Offset = "0x479B10", VA = "0x18047B110", Slot = "79")]
	[AsyncStateMachine(typeof(_003CStopBgm_003Ed__65))]
	public virtual UniTask StopBgm(string path, float fadeTime = 0f, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000453")]
	[Address(RVA = "0x47B310", Offset = "0x479D10", VA = "0x18047B310", Slot = "80")]
	[AsyncStateMachine(typeof(_003CStopAllBgm_003Ed__66))]
	public virtual UniTask StopAllBgm(float fadeTime = 0f, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000454")]
	[Address(RVA = "0x47B4B0", Offset = "0x479EB0", VA = "0x18047B4B0", Slot = "81")]
	[AsyncStateMachine(typeof(_003CPlaySfx_003Ed__67))]
	public virtual UniTask PlaySfx(string path, float volume = 1f, float fadeTime = 0f, bool loop = false, string group = default, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000455")]
	[Address(RVA = "0x47B720", Offset = "0x47A120", VA = "0x18047B720", Slot = "82")]
	[AsyncStateMachine(typeof(_003CStopSfx_003Ed__68))]
	public virtual UniTask StopSfx(string path, float fadeTime = 0f, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000456")]
	[Address(RVA = "0x47B920", Offset = "0x47A320", VA = "0x18047B920", Slot = "83")]
	[AsyncStateMachine(typeof(_003CStopAllSfx_003Ed__69))]
	public virtual UniTask StopAllSfx(float fadeTime = 0f, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000457")]
	[Address(RVA = "0x47BAC0", Offset = "0x47A4C0", VA = "0x18047BAC0", Slot = "84")]
	[AsyncStateMachine(typeof(_003CPlayVoice_003Ed__70))]
	public virtual UniTask PlayVoice(string path, float volume = 1f, string group = default, string authorId = default, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000458")]
	[Address(RVA = "0x47BD70", Offset = "0x47A770", VA = "0x18047BD70", Slot = "85")]
	public virtual bool IsBgmPlaying(string path)
	{
		return default(bool);
	}

	[Token(Token = "0x6000459")]
	[Address(RVA = "0x47BE50", Offset = "0x47A850", VA = "0x18047BE50", Slot = "86")]
	public virtual bool IsSfxPlaying(string path)
	{
		return default(bool);
	}

	[Token(Token = "0x600045A")]
	[Address(RVA = "0x47BF30", Offset = "0x47A930", VA = "0x18047BF30", Slot = "87")]
	public virtual bool IsVoicePlaying(string path)
	{
		return default(bool);
	}

	[Token(Token = "0x600045B")]
	[Address(RVA = "0x47C050", Offset = "0x47AA50", VA = "0x18047C050", Slot = "88")]
	public virtual void StopVoice()
	{
	}

	[Token(Token = "0x600045C")]
	[Address(RVA = "0x47C210", Offset = "0x47AC10", VA = "0x18047C210", Slot = "89")]
	public virtual IAudioTrack GetAudioTrack(string path)
	{
		return null;
	}

	[Token(Token = "0x600045D")]
	[Address(RVA = "0x47C320", Offset = "0x47AD20", VA = "0x18047C320", Slot = "90")]
	public virtual IAudioTrack GetVoiceTrack(string path)
	{
		return null;
	}

	[Token(Token = "0x600045E")]
	[Address(RVA = "0x47C430", Offset = "0x47AE30", VA = "0x18047C430", Slot = "91")]
	public virtual float GetAuthorVolume(string authorId)
	{
		return default(float);
	}

	[Token(Token = "0x600045F")]
	[Address(RVA = "0x47C4C0", Offset = "0x47AEC0", VA = "0x18047C4C0", Slot = "92")]
	public virtual void SetAuthorVolume(string authorId, float volume)
	{
	}

	[Token(Token = "0x6000460")]
	[Address(RVA = "0x47C540", Offset = "0x47AF40", VA = "0x18047C540", Slot = "93")]
	[AsyncStateMachine(typeof(_003CModifyAudio_003Ed__79))]
	protected virtual UniTask ModifyAudio(string path, float volume, bool loop, float time, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000461")]
	[Address(RVA = "0x47C770", Offset = "0x47B170", VA = "0x18047C770", Slot = "94")]
	protected virtual float GetMixerVolume(string handleName)
	{
		return default(float);
	}

	[Token(Token = "0x6000462")]
	[Address(RVA = "0x47C8F0", Offset = "0x47B2F0", VA = "0x18047C8F0", Slot = "95")]
	protected virtual void SetMixerVolume(string handleName, float value)
	{
	}

	[Token(Token = "0x6000463")]
	[Address(RVA = "0x47CA80", Offset = "0x47B480", VA = "0x18047CA80", Slot = "96")]
	protected virtual AudioMixerGroup FindAudioGroupOrDefault(string path, AudioMixerGroup defaultGroup)
	{
		return null;
	}

	[Token(Token = "0x6000464")]
	[Address(RVA = "0x47CBE0", Offset = "0x47B5E0", VA = "0x18047CBE0", Slot = "97")]
	[AsyncStateMachine(typeof(_003CGetOrInstantiateAuthorSource_003Ed__83))]
	protected virtual UniTask<AudioSource> GetOrInstantiateAuthorSource(string authorId)
	{
		return default(UniTask<AudioSource>);
	}
}
