using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Audio;

namespace Naninovel;

[Token(Token = "0x20000EA")]
[EditInProjectSettings]
public class AudioConfiguration : Configuration
{
	[Token(Token = "0x4000355")]
	public const string DefaultAudioPathPrefix = "Audio";

	[Token(Token = "0x4000356")]
	public const string DefaultVoicePathPrefix = "Voice";

	[Token(Token = "0x4000357")]
	[FieldOffset(Offset = "0x18")]
	[Tooltip("Configuration of the resource loader used with audio (BGM and SFX) resources.")]
	public ResourceLoaderConfiguration AudioLoader;

	[Token(Token = "0x4000358")]
	[FieldOffset(Offset = "0x20")]
	[Tooltip("Configuration of the resource loader used with voice resources.")]
	public ResourceLoaderConfiguration VoiceLoader;

	[Token(Token = "0x4000359")]
	[FieldOffset(Offset = "0x28")]
	[Tooltip("IAudioPlayer implementation responsible for playing audio clips.")]
	public string AudioPlayer;

	[Token(Token = "0x400035A")]
	[FieldOffset(Offset = "0x30")]
	[Range(0f, 1f)]
	[Tooltip("Master volume to set when the game is first started.")]
	public float DefaultMasterVolume;

	[Token(Token = "0x400035B")]
	[FieldOffset(Offset = "0x34")]
	[Range(0f, 1f)]
	[Tooltip("BGM volume to set when the game is first started.")]
	public float DefaultBgmVolume;

	[Token(Token = "0x400035C")]
	[FieldOffset(Offset = "0x38")]
	[Range(0f, 1f)]
	[Tooltip("SFX volume to set when the game is first started.")]
	public float DefaultSfxVolume;

	[Token(Token = "0x400035D")]
	[FieldOffset(Offset = "0x3C")]
	[Range(0f, 1f)]
	[Tooltip("Voice volume to set when the game is first started.")]
	public float DefaultVoiceVolume;

	[Token(Token = "0x400035E")]
	[FieldOffset(Offset = "0x40")]
	[Tooltip("When enabled, each [@print] command will attempt to play an associated voice clip.")]
	public bool EnableAutoVoicing;

	[Token(Token = "0x400035F")]
	[FieldOffset(Offset = "0x44")]
	[Tooltip("Dictates how to handle concurrent voices playback:\n • Allow Overlap — Concurrent voices will be played without limitation.\n • Prevent Overlap — Prevent concurrent voices playback by stopping any played voice clip before playing a new one.\n • Prevent Character Overlap — Prevent concurrent voices playback per character; voices of different characters (auto voicing) and any number of [@voice] command are allowed to be played concurrently.")]
	public VoiceOverlapPolicy VoiceOverlapPolicy;

	[Token(Token = "0x4000360")]
	[FieldOffset(Offset = "0x48")]
	[Tooltip("Assign localization tags to allow selecting voice language in the game settings independently of the main localization.")]
	public List<string> VoiceLocales;

	[Token(Token = "0x4000361")]
	[FieldOffset(Offset = "0x50")]
	[Tooltip("Default duration of the volume fade in/out when starting or stopping playing audio.")]
	public float DefaultFadeDuration;

	[Token(Token = "0x4000362")]
	[FieldOffset(Offset = "0x54")]
	[Tooltip("Whether to play non-looped sound effects (SFX) while in skip mode. When disabled, will ignore [@sfx] commands without `loop!` while skipping.")]
	public bool PlaySfxWhileSkipping;

	[Token(Token = "0x4000363")]
	[FieldOffset(Offset = "0x58")]
	[Header("Audio Mixer")]
	[Tooltip("Audio mixer to control audio groups. When not specified, will use a default one.")]
	public AudioMixer CustomAudioMixer;

	[Token(Token = "0x4000364")]
	[FieldOffset(Offset = "0x60")]
	[Tooltip("Path of the mixer's group to control master volume.")]
	public string MasterGroupPath;

	[Token(Token = "0x4000365")]
	[FieldOffset(Offset = "0x68")]
	[Tooltip("Name of the mixer's handle (exposed parameter) to control master volume.")]
	public string MasterVolumeHandleName;

	[Token(Token = "0x4000366")]
	[FieldOffset(Offset = "0x70")]
	[Tooltip("Path of the mixer's group to control volume of background music.")]
	public string BgmGroupPath;

	[Token(Token = "0x4000367")]
	[FieldOffset(Offset = "0x78")]
	[Tooltip("Name of the mixer's handle (exposed parameter) to control background music volume.")]
	public string BgmVolumeHandleName;

	[Token(Token = "0x4000368")]
	[FieldOffset(Offset = "0x80")]
	[Tooltip("Path of the mixer's group to control sound effects music volume.")]
	public string SfxGroupPath;

	[Token(Token = "0x4000369")]
	[FieldOffset(Offset = "0x88")]
	[Tooltip("Name of the mixer's handle (exposed parameter) to control sound effects volume.")]
	public string SfxVolumeHandleName;

	[Token(Token = "0x400036A")]
	[FieldOffset(Offset = "0x90")]
	[Tooltip("Path of the mixer's group to control voice volume.")]
	public string VoiceGroupPath;

	[Token(Token = "0x400036B")]
	[FieldOffset(Offset = "0x98")]
	[Tooltip("Name of the mixer's handle (exposed parameter) to control voice volume.")]
	public string VoiceVolumeHandleName;

	[Token(Token = "0x600042F")]
	[Address(RVA = "0x477780", Offset = "0x476180", VA = "0x180477780")]
	public AudioConfiguration()
	{
	}
}
