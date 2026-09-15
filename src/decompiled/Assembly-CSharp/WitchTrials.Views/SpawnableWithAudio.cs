using Il2CppDummyDll;
using Naninovel;
using System.Linq;
using UnityEngine.Audio;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x2000086")]
[RequireComponent(typeof(AudioSource))]
public class SpawnableWithAudio : MonoBehaviour
{
	[Token(Token = "0x400020F")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private AudioSource _audioSource;

	[Token(Token = "0x4000210")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private string _audioGroupPath;

	// RECONSTRUCTED | Confidence: A | RVA: 0x335E00, 232 B
	// Engine.GetServiceOrErr<IAudioManager>() is resolved before `_audioSource` (+0x20) is loaded, so the group is a
	// local: IAudioManager AudioMixer (thunk 0x5E30), FindMatchingGroups(_audioGroupPath +0x28), null-conditional
	// FirstOrDefault, then AudioSource.outputAudioMixerGroup (tail jump).
	[Token(Token = "0x60001AF")]
	[Address(RVA = "0x335E00", Offset = "0x334800", VA = "0x180335E00")]
	private void Awake()
	{
		AudioMixerGroup group = Engine.GetServiceOrErr<IAudioManager>().AudioMixer.FindMatchingGroups(_audioGroupPath)?.FirstOrDefault();
		_audioSource.outputAudioMixerGroup = group;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x335EF0, 151 B
	// `_audioSource` (+0x20) = GetComponent<AudioSource>().
	[Token(Token = "0x60001B0")]
	[Address(RVA = "0x335EF0", Offset = "0x3348F0", VA = "0x180335EF0")]
	private void Reset()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	[Token(Token = "0x60001B1")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public SpawnableWithAudio()
	{
	}
}
