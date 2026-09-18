using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views
{
    [Token(Token = "0x200001A")]
    public class ToggleImageSwitcher : MonoBehaviour
    {
    	[Token(Token = "0x4000030")]
    	[FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private Toggle _toggle;

    	[Token(Token = "0x4000031")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Image _image;

    	[Token(Token = "0x4000032")]
    	[FieldOffset(Offset = "0x30")]
    	[Header("Sprites")]
    	[SerializeField]
    	private Sprite _onSprite;

    	[Token(Token = "0x4000033")]
    	[FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private Sprite _offSprite;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30F600
    	// GetComponent<Toggle>() into +0x20, then GetComponentInChildren<Image>() into +0x28.
    	[Token(Token = "0x6000059")]
    	[Address(RVA = "0x30F600", Offset = "0x30E000", VA = "0x18030F600")]
    	private void Reset()
    	{
    		_toggle = GetComponent<Toggle>();
    		_image = GetComponentInChildren<Image>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30F730
    	// AddListener(OnValueChanged) on `_toggle.onValueChanged`; then Change(_toggle.isOn) inlined:
    	// `_image` (+0x28).sprite = isOn ? `_onSprite` (+0x30) : `_offSprite` (+0x38).
    	[Token(Token = "0x600005A")]
    	[Address(RVA = "0x30F730", Offset = "0x30E130", VA = "0x18030F730")]
    	private void Awake()
    	{
    		_toggle.onValueChanged.AddListener(OnValueChanged);
    		Change(_toggle.isOn);
    	}

    	// RECONSTRUCTED | Confidence: A (null-test spelling: B) | RVA: 0x30F820
    	// Liveness test on `_image` -> sprite = null; liveness test on `_toggle` -> RemoveListener(OnValueChanged)
    	// (inlined as in ToggleCanvasGroupSwitcher.OnDestroy).
    	[Token(Token = "0x600005B")]
    	[Address(RVA = "0x30F820", Offset = "0x30E220", VA = "0x18030F820")]
    	private void OnDestroy()
    	{
    		if (_image != null)
    		{
    			_image.sprite = null;
    		}
    		if (_toggle != null)
    		{
    			_toggle.onValueChanged.RemoveListener(OnValueChanged);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A (call spelling: B) | RVA: 0x30FA50 (folded with Change)
    	// Byte-identical to Change: forwarding with Change inlined compiles to the same body.
    	[Token(Token = "0x600005C")]
    	[Address(RVA = "0x30FA50", Offset = "0x30E450", VA = "0x18030FA50")]
    	private void OnValueChanged(bool isOn)
    	{
    		Change(isOn);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30FA50 (folded with OnValueChanged)
    	// `_image` (+0x28).sprite = [this + (isOn ^ 1)*8 + 0x30]: `_onSprite` (+0x30) when on, `_offSprite` (+0x38) when off.
    	[Token(Token = "0x600005D")]
    	[Address(RVA = "0x30FA50", Offset = "0x30E450", VA = "0x18030FA50")]
    	private void Change(bool isOn)
    	{
    		_image.sprite = isOn ? _onSprite : _offSprite;
    	}

    	[Token(Token = "0x600005E")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public ToggleImageSwitcher()
    	{
    	}
    }
}
