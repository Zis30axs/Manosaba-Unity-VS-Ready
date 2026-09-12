using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000111")]
public struct CameraComponentState : IEquatable<CameraComponentState>
{
	[Token(Token = "0x400042A")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private string typeName;

	[Token(Token = "0x400042B")]
	[FieldOffset(Offset = "0x8")]
	[SerializeField]
	private bool enabled;

	[Token(Token = "0x170000C7")]
	public string TypeName
	{
		[Token(Token = "0x60004DB")]
		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000C8")]
	public bool Enabled
	{
		[Token(Token = "0x60004DC")]
		[Address(RVA = "0x485440", Offset = "0x483E40", VA = "0x180485440")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x60004DD")]
	[Address(RVA = "0x485450", Offset = "0x483E50", VA = "0x180485450")]
	public CameraComponentState(MonoBehaviour component)
	{
	}

	[Token(Token = "0x60004DE")]
	[Address(RVA = "0x456740", Offset = "0x455140", VA = "0x180456740", Slot = "4")]
	public bool Equals(CameraComponentState other)
	{
		return default(bool);
	}

	[Token(Token = "0x60004DF")]
	[Address(RVA = "0x485570", Offset = "0x483F70", VA = "0x180485570", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x60004E0")]
	[Address(RVA = "0x477700", Offset = "0x476100", VA = "0x180477700", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x60004E1")]
	[Address(RVA = "0x456740", Offset = "0x455140", VA = "0x180456740")]
	public static bool operator ==(CameraComponentState left, CameraComponentState right)
	{
		return default(bool);
	}

	[Token(Token = "0x60004E2")]
	[Address(RVA = "0x477720", Offset = "0x476120", VA = "0x180477720")]
	public static bool operator !=(CameraComponentState left, CameraComponentState right)
	{
		return default(bool);
	}
}
