using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x2000019")]
public class ToggleCanvasGroupSwitcher : MonoBehaviour
{
	[Token(Token = "0x400002D")]
	[FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private Toggle _toggle;

	[Token(Token = "0x400002E")]
	[FieldOffset(Offset = "0x28")]
	[Header("Canvas Group")]
	[SerializeField]
	private CanvasGroup _onCanvasGroup;

	[Token(Token = "0x400002F")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private CanvasGroup _offCanvasGroup;

	// RECONSTRUCTED | Confidence: A | RVA: 0x30F220
	// `_toggle` (+0x20).onValueChanged (+0x118).AddListener(new UnityAction<bool>(OnValueChanged)); tail jump to
	// Change(_toggle.isOn) (+0x120 m_IsOn).
	[Token(Token = "0x6000053")]
	[Address(RVA = "0x30F220", Offset = "0x30DC20", VA = "0x18030F220")]
	private void Awake()
	{
		_toggle.onValueChanged.AddListener(OnValueChanged);
		Change(_toggle.isOn);
	}

	// RECONSTRUCTED | Confidence: A (null-test spelling: B) | RVA: 0x30F300
	// Inlined UnityEngine.Object liveness test on `_toggle` (non-null and m_CachedPtr != 0), then
	// UnityEvent<bool>.RemoveListener inlined (GetDelegate slot +0x1B8, InvokableCallList.RemoveListener(target, method)).
	[Token(Token = "0x6000054")]
	[Address(RVA = "0x30F300", Offset = "0x30DD00", VA = "0x18030F300")]
	private void OnDestroy()
	{
		if (_toggle != null)
		{
			_toggle.onValueChanged.RemoveListener(OnValueChanged);
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x30F480
	// GetComponent<Toggle>() into +0x20.
	[Token(Token = "0x6000055")]
	[Address(RVA = "0x30F480", Offset = "0x30DE80", VA = "0x18030F480")]
	private void Reset()
	{
		_toggle = GetComponent<Toggle>();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x30F520
	// Tail jump to Change(isOn).
	[Token(Token = "0x6000056")]
	[Address(RVA = "0x30F520", Offset = "0x30DF20", VA = "0x18030F520")]
	private void OnValueChanged(bool isOn)
	{
		Change(isOn);
	}

	// RECONSTRUCTED | Confidence: A (`!isOn` spelling: B) | RVA: 0x30F530
	// `_onCanvasGroup` (+0x28): alpha isOn ? 1f : 0f, interactable = isOn, blocksRaycasts = isOn; then `xor dil, 1` and
	// `_offCanvasGroup` (+0x30): alpha isOn ? 0f : 1f, interactable and blocksRaycasts = !isOn.
	[Token(Token = "0x6000057")]
	[Address(RVA = "0x30F530", Offset = "0x30DF30", VA = "0x18030F530")]
	private void Change(bool isOn)
	{
		_onCanvasGroup.alpha = isOn ? 1f : 0f;
		_onCanvasGroup.interactable = isOn;
		_onCanvasGroup.blocksRaycasts = isOn;
		_offCanvasGroup.alpha = isOn ? 0f : 1f;
		_offCanvasGroup.interactable = !isOn;
		_offCanvasGroup.blocksRaycasts = !isOn;
	}

	[Token(Token = "0x6000058")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public ToggleCanvasGroupSwitcher()
	{
	}
}
