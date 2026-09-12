using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x2000111")]
public class WitchBookItemDescription : MonoBehaviour
{
	[Token(Token = "0x400051A")]
	[FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private TextMeshProUGUI _label;

	[Token(Token = "0x400051B")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private AutoLayoutSupporter _autoLayoutSupporter;

	[Token(Token = "0x600047F")]
	[Address(RVA = "0x378640", Offset = "0x377040", VA = "0x180378640")]
	public void SetText(string text)
	{
	}

	[Token(Token = "0x6000480")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public WitchBookItemDescription()
	{
	}
}
