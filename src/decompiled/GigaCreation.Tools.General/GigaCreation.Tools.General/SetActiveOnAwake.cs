using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.General;

[Token(Token = "0x200000B")]
public class SetActiveOnAwake : MonoBehaviour
{
	[Token(Token = "0x200000C")]
	private enum TargetKind
	{
		[Token(Token = "0x400001B")]
		AllChildren,
		[Token(Token = "0x400001C")]
		ChildrenOneLevelDown,
		[Token(Token = "0x400001D")]
		Selections
	}

	[Serializable]
	[Token(Token = "0x200000D")]
	private class Target
	{
		[Token(Token = "0x400001E")]
		[FieldOffset(Offset = "0x10")]
		public GameObject GameObject;

		[Token(Token = "0x400001F")]
		[FieldOffset(Offset = "0x18")]
		public bool Active;

		[Token(Token = "0x6000016")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public Target()
		{
		}
	}

	[Token(Token = "0x4000017")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private TargetKind _targetKind;

	[Token(Token = "0x4000018")]
	[FieldOffset(Offset = "0x24")]
	[SerializeField]
	private bool _active;

	[Token(Token = "0x4000019")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Target[] _selections;

	[Token(Token = "0x6000012")]
	[Address(RVA = "0xD4AA40", Offset = "0xD49440", VA = "0x180D4AA40")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000013")]
	[Address(RVA = "0xD4AE50", Offset = "0xD49850", VA = "0x180D4AE50")]
	public SetActiveOnAwake()
	{
	}
}
