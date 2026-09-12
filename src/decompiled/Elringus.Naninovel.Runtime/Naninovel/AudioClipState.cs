using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x20000E9")]
public struct AudioClipState : IEquatable<AudioClipState>
{
	[Token(Token = "0x4000352")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private string path;

	[Token(Token = "0x4000353")]
	[FieldOffset(Offset = "0x8")]
	[SerializeField]
	private float volume;

	[Token(Token = "0x4000354")]
	[FieldOffset(Offset = "0xC")]
	[SerializeField]
	private bool looped;

	[Token(Token = "0x170000AE")]
	public string Path
	{
		[Token(Token = "0x6000426")]
		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000AF")]
	public float Volume
	{
		[Token(Token = "0x6000427")]
		[Address(RVA = "0x4775C0", Offset = "0x475FC0", VA = "0x1804775C0")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x170000B0")]
	public bool Looped
	{
		[Token(Token = "0x6000428")]
		[Address(RVA = "0x4775D0", Offset = "0x475FD0", VA = "0x1804775D0")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6000429")]
	[Address(RVA = "0x4775E0", Offset = "0x475FE0", VA = "0x1804775E0")]
	public AudioClipState(string path, float volume, bool looped)
	{
	}

	[Token(Token = "0x600042A")]
	[Address(RVA = "0x456740", Offset = "0x455140", VA = "0x180456740", Slot = "4")]
	public bool Equals(AudioClipState other)
	{
		return default(bool);
	}

	[Token(Token = "0x600042B")]
	[Address(RVA = "0x477640", Offset = "0x476040", VA = "0x180477640", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x600042C")]
	[Address(RVA = "0x477700", Offset = "0x476100", VA = "0x180477700", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x600042D")]
	[Address(RVA = "0x456740", Offset = "0x455140", VA = "0x180456740")]
	public static bool operator ==(AudioClipState left, AudioClipState right)
	{
		return default(bool);
	}

	[Token(Token = "0x600042E")]
	[Address(RVA = "0x477720", Offset = "0x476120", VA = "0x180477720")]
	public static bool operator !=(AudioClipState left, AudioClipState right)
	{
		return default(bool);
	}
}
