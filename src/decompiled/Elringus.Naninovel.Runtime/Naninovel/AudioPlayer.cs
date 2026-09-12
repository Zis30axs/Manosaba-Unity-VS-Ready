using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Audio;

namespace Naninovel;

[Token(Token = "0x2000109")]
public class AudioPlayer : IAudioPlayer, IDisposable
{
	[Token(Token = "0x400041D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private readonly AudioController controller;

	[Token(Token = "0x170000BD")]
	public float Volume
	{
		[Token(Token = "0x600049D")]
		[Address(RVA = "0x484420", Offset = "0x482E20", VA = "0x180484420", Slot = "4")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x600049E")]
		[Address(RVA = "0x484480", Offset = "0x482E80", VA = "0x180484480", Slot = "5")]
		set
		{
		}
	}

	[Token(Token = "0x600049F")]
	[Address(RVA = "0x4844F0", Offset = "0x482EF0", VA = "0x1804844F0", Slot = "14")]
	public void Dispose()
	{
	}

	[Token(Token = "0x60004A0")]
	[Address(RVA = "0x484530", Offset = "0x482F30", VA = "0x180484530", Slot = "6")]
	public bool IsPlaying(AudioClip clip)
	{
		return default(bool);
	}

	[Token(Token = "0x60004A1")]
	[Address(RVA = "0x484550", Offset = "0x482F50", VA = "0x180484550", Slot = "7")]
	public void Play(AudioClip clip, AudioSource audioSource = default, float volume = 1f, bool loop = false, AudioMixerGroup mixerGroup = default, AudioClip introClip = default, bool additive = false)
	{
	}

	[Token(Token = "0x60004A2")]
	[Address(RVA = "0x4845A0", Offset = "0x482FA0", VA = "0x1804845A0", Slot = "8")]
	public UniTask Play(AudioClip clip, float fadeInTime, AudioSource audioSource = default, float volume = 1f, bool loop = false, AudioMixerGroup mixerGroup = default, AudioClip introClip = default, bool additive = false, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60004A3")]
	[Address(RVA = "0x484630", Offset = "0x483030", VA = "0x180484630", Slot = "9")]
	public void Stop(AudioClip clip)
	{
	}

	[Token(Token = "0x60004A4")]
	[Address(RVA = "0x484650", Offset = "0x483050", VA = "0x180484650", Slot = "10")]
	public void StopAll()
	{
	}

	[Token(Token = "0x60004A5")]
	[Address(RVA = "0x484670", Offset = "0x483070", VA = "0x180484670", Slot = "11")]
	public UniTask Stop(AudioClip clip, float fadeOutTime, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60004A6")]
	[Address(RVA = "0x4846B0", Offset = "0x4830B0", VA = "0x1804846B0", Slot = "12")]
	public UniTask StopAll(float fadeOutTime, AsyncToken token = default)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60004A7")]
	[Address(RVA = "0x484880", Offset = "0x483280", VA = "0x180484880", Slot = "13")]
	public IReadOnlyCollection<IAudioTrack> GetTracks(AudioClip clip)
	{
		return null;
	}

	[Token(Token = "0x60004A8")]
	[Address(RVA = "0x4848A0", Offset = "0x4832A0", VA = "0x1804848A0")]
	public AudioPlayer()
	{
	}
}
