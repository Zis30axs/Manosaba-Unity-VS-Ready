using System;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.General;

[Token(Token = "0x2000008")]
public class SceneVisibilitySwitcher : MonoBehaviour
{
	[Token(Token = "0x2000009")]
	private enum TargetKind
	{
		[Token(Token = "0x4000012")]
		Self,
		[Token(Token = "0x4000013")]
		ChildrenOneLevelDown,
		[Token(Token = "0x4000014")]
		Selections
	}

	[Serializable]
	[Token(Token = "0x200000A")]
	private class Target
	{
		[Token(Token = "0x4000015")]
		[FieldOffset(Offset = "0x10")]
		public GameObject GameObject;

		[Token(Token = "0x4000016")]
		[FieldOffset(Offset = "0x18")]
		public bool Visible;

		[Token(Token = "0x6000011")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public Target()
		{
		}
	}

	[Token(Token = "0x400000E")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private TargetKind _targetKind;

	[Token(Token = "0x400000F")]
	[FieldOffset(Offset = "0x24")]
	[SerializeField]
	private bool _visible;

	[Token(Token = "0x4000010")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Target[] _selections;

	[Token(Token = "0x600000E")]
	[Address(RVA = "0xD4A4B0", Offset = "0xD48EB0", VA = "0x180D4A4B0")]
	private void OnValidate()
	{
	}

	[Token(Token = "0x600000F")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public SceneVisibilitySwitcher()
	{
	}
}
