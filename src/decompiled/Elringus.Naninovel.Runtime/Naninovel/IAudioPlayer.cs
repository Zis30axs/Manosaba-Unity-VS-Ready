using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Audio;

namespace Naninovel;

[Token(Token = "0x200010D")]
public interface IAudioPlayer
{
	[Token(Token = "0x170000C6")]
	float Volume
	{
		[Token(Token = "0x60004CD")]
		get;
		[Token(Token = "0x60004CE")]
		set;
	}

	[Token(Token = "0x60004CF")]
	bool IsPlaying(AudioClip clip);

	[Token(Token = "0x60004D0")]
	void Play(AudioClip clip, AudioSource audioSource = default, float volume = 1f, bool loop = false, AudioMixerGroup mixerGroup = default, AudioClip introClip = default, bool additive = false);

	[Token(Token = "0x60004D1")]
	UniTask Play(AudioClip clip, float fadeInTime, AudioSource audioSource = default, float volume = 1f, bool loop = false, AudioMixerGroup mixerGroup = default, AudioClip introClip = default, bool additive = false, AsyncToken token = default);

	[Token(Token = "0x60004D2")]
	void Stop(AudioClip clip);

	[Token(Token = "0x60004D3")]
	void StopAll();

	[Token(Token = "0x60004D4")]
	UniTask Stop(AudioClip clip, float fadeOutTime, AsyncToken token = default);

	[Token(Token = "0x60004D5")]
	UniTask StopAll(float fadeOutTime, AsyncToken token = default);

	[Token(Token = "0x60004D6")]
	IReadOnlyCollection<IAudioTrack> GetTracks(AudioClip clip);
}
