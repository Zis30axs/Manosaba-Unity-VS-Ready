using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x2000148")]
public class VersionNumberDisplay : MonoBehaviour
{
	[Token(Token = "0x4000655")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private TextMeshProUGUI _label;

	[Token(Token = "0x6000578")]
	[Address(RVA = "0x38CFA0", Offset = "0x38B9A0", VA = "0x18038CFA0")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000579")]
	[Address(RVA = "0x38D040", Offset = "0x38BA40", VA = "0x18038D040")]
	private void Reset()
	{
	}

	[Token(Token = "0x600057A")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public VersionNumberDisplay()
	{
	}
}
