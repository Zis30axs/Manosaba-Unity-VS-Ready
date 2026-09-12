using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using UnityEngine.Audio;

namespace Naninovel;

[Token(Token = "0x200010C")]
public interface IAudioManager : IEngineService<AudioConfiguration>, IEngineService
{
	[Token(Token = "0x170000BE")]
	AudioMixer AudioMixer
	{
		[Token(Token = "0x60004AB")]
		get;
	}

	[Token(Token = "0x170000BF")]
	float MasterVolume
	{
		[Token(Token = "0x60004AC")]
		get;
		[Token(Token = "0x60004AD")]
		set;
	}

	[Token(Token = "0x170000C0")]
	float BgmVolume
	{
		[Token(Token = "0x60004AE")]
		get;
		[Token(Token = "0x60004AF")]
		set;
	}

	[Token(Token = "0x170000C1")]
	float SfxVolume
	{
		[Token(Token = "0x60004B0")]
		get;
		[Token(Token = "0x60004B1")]
		set;
	}

	[Token(Token = "0x170000C2")]
	float VoiceVolume
	{
		[Token(Token = "0x60004B2")]
		get;
		[Token(Token = "0x60004B3")]
		set;
	}

	[Token(Token = "0x170000C3")]
	string VoiceLocale
	{
		[Token(Token = "0x60004B4")]
		get;
		[Token(Token = "0x60004B5")]
		set;
	}

	[Token(Token = "0x170000C4")]
	IResourceLoader AudioLoader
	{
		[Token(Token = "0x60004B6")]
		get;
	}

	[Token(Token = "0x170000C5")]
	IResourceLoader VoiceLoader
	{
		[Token(Token = "0x60004B7")]
		get;
	}

	[Token(Token = "0x60004B8")]
	UniTask ModifyBgm(string path, float volume, bool loop, float time, AsyncToken token = default);

	[Token(Token = "0x60004B9")]
	UniTask ModifySfx(string path, float volume, bool loop, float time, AsyncToken token = default);

	[Token(Token = "0x60004BA")]
	UniTask PlaySfxFast(string path, float volume = 1f, string group = default, bool restart = true, bool additive = true);

	[Token(Token = "0x60004BB")]
	UniTask PlayBgm(string path, float volume = 1f, float fadeTime = 0f, bool loop = true, string introPath = default, string group = default, AsyncToken token = default);

	[Token(Token = "0x60004BC")]
	UniTask StopBgm(string path, float fadeTime = 0f, AsyncToken token = default);

	[Token(Token = "0x60004BD")]
	UniTask StopAllBgm(float fadeTime = 0f, AsyncToken token = default);

	[Token(Token = "0x60004BE")]
	UniTask PlaySfx(string path, float volume = 1f, float fadeTime = 0f, bool loop = false, string group = default, AsyncToken token = default);

	[Token(Token = "0x60004BF")]
	UniTask StopSfx(string path, float fadeTime = 0f, AsyncToken token = default);

	[Token(Token = "0x60004C0")]
	UniTask StopAllSfx(float fadeTime = 0f, AsyncToken token = default);

	[Token(Token = "0x60004C1")]
	UniTask PlayVoice(string path, float volume = 1f, string group = default, string authorId = default, AsyncToken token = default);

	[Token(Token = "0x60004C2")]
	void StopVoice();

	[Token(Token = "0x60004C3")]
	bool IsBgmPlaying(string path);

	[Token(Token = "0x60004C4")]
	bool IsSfxPlaying(string path);

	[Token(Token = "0x60004C5")]
	bool IsVoicePlaying(string path);

	[Token(Token = "0x60004C6")]
	void GetPlayedBgm(ICollection<string> paths);

	[Token(Token = "0x60004C7")]
	void GetPlayedSfx(ICollection<string> paths);

	[Token(Token = "0x60004C8")]
	[CanBeNull]
	string GetPlayedVoice();

	[Token(Token = "0x60004C9")]
	[CanBeNull]
	IAudioTrack GetAudioTrack(string path);

	[Token(Token = "0x60004CA")]
	[CanBeNull]
	IAudioTrack GetVoiceTrack(string path);

	[Token(Token = "0x60004CB")]
	float GetAuthorVolume(string authorId);

	[Token(Token = "0x60004CC")]
	void SetAuthorVolume(string authorId, float volume);
}
