using System;
using System.Collections.Generic;
using System.Linq;
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

	// RECONSTRUCTED | Confidence: A | RVA: 0xD4AA40, 1035 B; lambdas <Awake>b__3_0 0xD4AEA0 and b__3_1 0xD4AF70 are both
	//   `x => new Target { GameObject = x.gameObject, Active = _active }`.
	// - AllChildren: gameObject.GetComponentsInChildren<Transform>(true) (includes this object); ChildrenOneLevelDown:
	//   transform.Cast<Transform>(); Selections: _selections as is; any other kind returns.
	// - foreach target: Unity-null test on Target.GameObject (+0x10), then GameObject.SetActive(Target.Active (+0x18)).
	// OptionsUI's "Pages" uses ChildrenOneLevelDown/true: the 图形/声音 pages are authored inactive and only this Awake
	// activates them; OptionsUi.PageCanvas.SetActive then switches pages by Canvas.enabled alone.
	[Token(Token = "0x6000012")]
	[Address(RVA = "0xD4AA40", Offset = "0xD49440", VA = "0x180D4AA40")]
	private void Awake()
	{
		IEnumerable<Target> targets;
		switch (_targetKind)
		{
			case TargetKind.AllChildren:
				targets = gameObject.GetComponentsInChildren<Transform>(true)
					.Select(x => new Target { GameObject = x.gameObject, Active = _active });
				break;
			case TargetKind.ChildrenOneLevelDown:
				targets = transform.Cast<Transform>()
					.Select(x => new Target { GameObject = x.gameObject, Active = _active });
				break;
			case TargetKind.Selections:
				targets = _selections;
				break;
			default:
				return;
		}
		foreach (Target target in targets)
		{
			if (target.GameObject)
			{
				target.GameObject.SetActive(target.Active);
			}
		}
	}

	[Token(Token = "0x6000013")]
	[Address(RVA = "0xD4AE50", Offset = "0xD49850", VA = "0x180D4AE50")]
	public SetActiveOnAwake()
	{
	}
}
