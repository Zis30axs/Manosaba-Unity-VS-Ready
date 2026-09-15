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
	private float _fontSizeDiff = 30f;

	[Token(Token = "0x4000527")]
	[FieldOffset(Offset = "0x2C")]
	[SerializeField]
	private float _initialSpace;

	// RECONSTRUCTED | Confidence: A | RVA: 0x378C70, 151 B
	// `_label (+0x20) = GetComponent<TextMeshProUGUI>()`.
	[Token(Token = "0x6000489")]
	[Address(RVA = "0x378C70", Offset = "0x377670", VA = "0x180378C70")]
	private void Reset()
	{
		_label = GetComponent<TextMeshProUGUI>();
	}

	// RECONSTRUCTED | Confidence: A (behavior), B (reassign spelling) | RVA: 0x378D10, 791 B
	// The first character is enlarged. `subject.Length` is read first (NRE on null):
	// - `> 1`: `string.Format("<size=+{0}>{1}<space={2}><size=+0>{3}", params object[4])` with the boxed float
	//   `_fontSizeDiff` (+0x28), the boxed char `subject[0]`, the boxed float `_initialSpace` (+0x2C) and
	//   `subject.Substring(1, Length - 1)`.
	// - `== 1`: `string.Format("<size=+{0}>{1}", _fontSizeDiff, subject[0])` (two-object ParamsArray overload).
	// - `0`: the subject is used unchanged.
	// The result goes to `_label.text` (vtable +0x558 = slot 66).
	[Token(Token = "0x600048A")]
	[Address(RVA = "0x378D10", Offset = "0x377710", VA = "0x180378D10")]
	public void SetText(string subject)
	{
		if (subject.Length > 1)
		{
			subject = string.Format("<size=+{0}>{1}<space={2}><size=+0>{3}", _fontSizeDiff, subject[0], _initialSpace, subject.Substring(1, subject.Length - 1));
		}
		else if (subject.Length == 1)
		{
			subject = string.Format("<size=+{0}>{1}", _fontSizeDiff, subject[0]);
		}
		_label.text = subject;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x379030, 74 B
	// Only the field initializer `_fontSizeDiff (+0x28) = 30f` (0x41F00000), then the inlined MonoBehaviour ctor.
	// `_initialSpace` has no initializer.
	[Token(Token = "0x600048B")]
	[Address(RVA = "0x379030", Offset = "0x377A30", VA = "0x180379030")]
	public WitchBookItemSubjectLabel()
	{
	}
}
