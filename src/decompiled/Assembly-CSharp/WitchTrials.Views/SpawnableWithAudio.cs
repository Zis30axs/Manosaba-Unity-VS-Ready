using Il2CppDummyDll;
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

	[Token(Token = "0x60001AF")]
	[Address(RVA = "0x335E00", Offset = "0x334800", VA = "0x180335E00")]
	private void Awake()
	{
	}

	[Token(Token = "0x60001B0")]
	[Address(RVA = "0x335EF0", Offset = "0x3348F0", VA = "0x180335EF0")]
	private void Reset()
	{
	}

	[Token(Token = "0x60001B1")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public SpawnableWithAudio()
	{
	}
}
