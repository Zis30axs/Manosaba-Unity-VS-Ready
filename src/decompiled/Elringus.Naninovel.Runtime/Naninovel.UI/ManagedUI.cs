using System;
using Il2CppDummyDll;
using JetBrains.Annotations;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x2000563")]
public readonly struct ManagedUI : IEquatable<ManagedUI>
{
	[Token(Token = "0x4001492")]
	[FieldOffset(Offset = "0x0")]
	public readonly string Name;

	[Token(Token = "0x4001493")]
	[FieldOffset(Offset = "0x8")]
	public readonly GameObject GameObject;

	[Token(Token = "0x4001494")]
	[FieldOffset(Offset = "0x10")]
	public readonly Transform Parent;

	[Token(Token = "0x4001495")]
	[FieldOffset(Offset = "0x18")]
	public readonly IManagedUI UIComponent;

	[Token(Token = "0x4001496")]
	[FieldOffset(Offset = "0x20")]
	public readonly Type ComponentType;

	[Token(Token = "0x6001AB9")]
	[Address(RVA = "0x62ED10", Offset = "0x62D710", VA = "0x18062ED10")]
	public ManagedUI([NotNull] string name, [NotNull] GameObject gameObject, [NotNull] IManagedUI uiComponent)
	{
	}

	[Token(Token = "0x6001ABA")]
	[Address(RVA = "0x62EF00", Offset = "0x62D900", VA = "0x18062EF00", Slot = "4")]
	public bool Equals(ManagedUI other)
	{
		return default(bool);
	}

	[Token(Token = "0x6001ABB")]
	[Address(RVA = "0x62EF40", Offset = "0x62D940", VA = "0x18062EF40", Slot = "0")]
	public override bool Equals(object obj)
	{
		return default(bool);
	}

	[Token(Token = "0x6001ABC")]
	[Address(RVA = "0x62EFF0", Offset = "0x62D9F0", VA = "0x18062EFF0", Slot = "2")]
	public override int GetHashCode()
	{
		return default(int);
	}

	[Token(Token = "0x6001ABD")]
	[Address(RVA = "0x62F020", Offset = "0x62DA20", VA = "0x18062F020")]
	public static bool operator ==(ManagedUI left, ManagedUI right)
	{
		return default(bool);
	}

	[Token(Token = "0x6001ABE")]
	[Address(RVA = "0x62F070", Offset = "0x62DA70", VA = "0x18062F070")]
	public static bool operator !=(ManagedUI left, ManagedUI right)
	{
		return default(bool);
	}
}
