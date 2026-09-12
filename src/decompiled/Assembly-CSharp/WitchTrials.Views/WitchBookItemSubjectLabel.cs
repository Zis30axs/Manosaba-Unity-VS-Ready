using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x2000114")]
[RequireComponent(typeof(TextMeshProUGUI))]
public class WitchBookItemSubjectLabel : MonoBehaviour
{
	[Token(Token = "0x4000525")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private TextMeshProUGUI _label;

	[Token(Token = "0x4000526")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private float _fontSizeDiff;

	[Token(Token = "0x4000527")]
	[FieldOffset(Offset = "0x2C")]
	[SerializeField]
	private float _initialSpace;

	[Token(Token = "0x6000489")]
	[Address(RVA = "0x378C70", Offset = "0x377670", VA = "0x180378C70")]
	private void Reset()
	{
	}

	[Token(Token = "0x600048A")]
	[Address(RVA = "0x378D10", Offset = "0x377710", VA = "0x180378D10")]
	public void SetText(string subject)
	{
	}

	[Token(Token = "0x600048B")]
	[Address(RVA = "0x379030", Offset = "0x377A30", VA = "0x180379030")]
	public WitchBookItemSubjectLabel()
	{
	}
}
