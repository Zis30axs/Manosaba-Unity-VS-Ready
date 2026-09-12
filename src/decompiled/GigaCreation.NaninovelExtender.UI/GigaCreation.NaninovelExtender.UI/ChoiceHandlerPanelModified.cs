using System.Collections.Generic;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Ui;

[Token(Token = "0x2000007")]
public class ChoiceHandlerPanelModified : ChoiceHandlerPanel
{
	[Token(Token = "0x400000E")]
	[FieldOffset(Offset = "0x118")]
	[SerializeField]
	private CustomChoiceButtonData[] _customChoiceButtonData;

	[Token(Token = "0x400000F")]
	[FieldOffset(Offset = "0x120")]
	private IDictionary<string, ChoiceHandlerButton> _customButtons;

	[Token(Token = "0x6000014")]
	[Address(RVA = "0xD36180", Offset = "0xD34B80", VA = "0x180D36180", Slot = "83")]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000015")]
	[Address(RVA = "0xD364D0", Offset = "0xD34ED0", VA = "0x180D364D0", Slot = "104")]
	protected override ChoiceHandlerButton LoadCustomButtonPrefab(string path)
	{
		return null;
	}

	[Token(Token = "0x6000016")]
	[Address(RVA = "0x36BA60", Offset = "0x36A460", VA = "0x18036BA60")]
	public ChoiceHandlerPanelModified()
	{
	}
}
