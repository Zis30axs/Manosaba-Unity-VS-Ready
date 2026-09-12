using System.Collections.Generic;
using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace GigaCreation.Tools.Debugging.TextDisplays;

[Token(Token = "0x2000004")]
[RequireComponent(typeof(RectTransform))]
public class DebugLabelManager : MonoBehaviour
{
	[Token(Token = "0x4000006")]
	[FieldOffset(Offset = "0x20")]
	[Header("Assets")]
	[SerializeField]
	private TextMeshProUGUI _labelPrefab;

	[Token(Token = "0x4000007")]
	[FieldOffset(Offset = "0x28")]
	[Header("References")]
	[SerializeField]
	private Transform _transform;

	[Token(Token = "0x4000008")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private AutoLayoutSupporter _autoLayoutSupporter;

	[Token(Token = "0x4000009")]
	[FieldOffset(Offset = "0x38")]
	private readonly Dictionary<int, TextMeshProUGUI> _labels;

	[Token(Token = "0x6000003")]
	[Address(RVA = "0xD44890", Offset = "0xD43290", VA = "0x180D44890")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000004")]
	[Address(RVA = "0xD449A0", Offset = "0xD433A0", VA = "0x180D449A0")]
	public TextMeshProUGUI Add(int priority)
	{
		return null;
	}

	[Token(Token = "0x6000005")]
	[Address(RVA = "0xD45290", Offset = "0xD43C90", VA = "0x180D45290")]
	public void Remove(int priority)
	{
	}

	[Token(Token = "0x6000006")]
	[Address(RVA = "0xD455A0", Offset = "0xD43FA0", VA = "0x180D455A0")]
	public DebugLabelManager()
	{
	}
}
