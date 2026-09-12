using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000460")]
public struct SpawnedObjectState : IEquatable<SpawnedObjectState>
{
	[Token(Token = "0x400104C")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private string path;

	[Token(Token = "0x400104D")]
	[FieldOffset(Offset = "0x8")]
	[SerializeField]
	private NullableString[] parameters;

	[Token(Token = "0x400104E")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private Vector3 position;

	[Token(Token = "0x400104F")]
	[FieldOffset(Offset = "0x1C")]
	[SerializeField]
	private Quaternion rotation;

	[Token(Token = "0x4001050")]
	[FieldOffset(Offset = "0x2C")]
	[SerializeField]
	private Vector3 scale;

	[Token(Token = "0x17000301")]
	public string Path
	{
		[Token(Token = "0x6001599")]
		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000302")]
	public IReadOnlyList<string> Parameters
	{
		[Token(Token = "0x600159A")]
		[Address(RVA = "0x5D4A90", Offset = "0x5D3490", VA = "0x1805D4A90")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000303")]
	public Vector3 Position
	{
		[Token(Token = "0x600159B")]
		[Address(RVA = "0x5D4CA0", Offset = "0x5D36A0", VA = "0x1805D4CA0")]
		get
		{
			return default(Vector3);
		}
	}

	[Token(Token = "0x17000304")]
	public Quaternion Rotation
	{
		[Token(Token = "0x600159C")]
		[Address(RVA = "0x5D4CC0", Offset = "0x5D36C0", VA = "0x1805D4CC0")]
		get
		{
			return default(Quaternion);
		}
	}

	[Token(Token = "0x17000305")]
	public Vector3 Scale
	{
		[Token(Token = "0x600159D")]
		[Address(RVA = "0x5D4CD0", Offset = "0x5D36D0", VA = "0x1805D4CD0")]
		get
		{
			return default(Vector3);
		}
	}

	[Token(Token = "0x600159E")]
	[Address(RVA = "0x5D4CF0", Offset = "0x5D36F0", VA = "0x1805D4CF0")]
	public SpawnedObjectState(SpawnedObject spawnedObject)
	{
	}

	[Token(Token = "0x600159F")]
	[Address(RVA = "0x5D5200", Offset = "0x5D3C00", VA = "0x1805D5200")]
	public void ApplyTo(SpawnedObject spawnedObject)
	{
	}

	[Token(Token = "0x60015A0")]
	[Address(RVA = "0x456740", Offset = "0x455140", VA = "0x180456740", Slot = "4")]
	public bool Equals(SpawnedObjectState other)
	{
		return default(bool);
	}

	[Token(Token = "0x60015A1")]
	[Address(RVA = "0x5D5650", Offset = "0x5D4050", VA = "0x1805D5650", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x60015A2")]
	[Address(RVA = "0x477700", Offset = "0x476100", VA = "0x180477700", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x60015A3")]
	[Address(RVA = "0x5D5710", Offset = "0x5D4110", VA = "0x1805D5710")]
	public static bool operator ==(SpawnedObjectState left, SpawnedObjectState right)
	{
		return default(bool);
	}

	[Token(Token = "0x60015A4")]
	[Address(RVA = "0x5D5760", Offset = "0x5D4160", VA = "0x1805D5760")]
	public static bool operator !=(SpawnedObjectState left, SpawnedObjectState right)
	{
		return default(bool);
	}
}
