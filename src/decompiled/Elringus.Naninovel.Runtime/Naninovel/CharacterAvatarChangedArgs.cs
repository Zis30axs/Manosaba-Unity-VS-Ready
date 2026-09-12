using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000031")]
public readonly struct CharacterAvatarChangedArgs : IEquatable<CharacterAvatarChangedArgs>
{
	[Token(Token = "0x40000A5")]
	[FieldOffset(Offset = "0x0")]
	public readonly string CharacterId;

	[Token(Token = "0x40000A6")]
	[FieldOffset(Offset = "0x8")]
	public readonly Texture2D AvatarTexture;

	[Token(Token = "0x60000BC")]
	[Address(RVA = "0x42B240", Offset = "0x429C40", VA = "0x18042B240")]
	public CharacterAvatarChangedArgs(string characterId, Texture2D avatarTexture)
	{
	}

	[Token(Token = "0x60000BD")]
	[Address(RVA = "0x44D100", Offset = "0x44BB00", VA = "0x18044D100", Slot = "4")]
	public bool Equals(CharacterAvatarChangedArgs other)
	{
		return default(bool);
	}

	[Token(Token = "0x60000BE")]
	[Address(RVA = "0x44D1A0", Offset = "0x44BBA0", VA = "0x18044D1A0", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x60000BF")]
	[Address(RVA = "0x44D2A0", Offset = "0x44BCA0", VA = "0x18044D2A0", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}
}
