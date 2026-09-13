using Il2CppDummyDll;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000130")]
public class Court : MonoBehaviour, ICourtModifier
{
	[Token(Token = "0x40005B8")]
	[FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private GameObject _fireVfx;

	[Token(Token = "0x40005B9")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Light _light;

	[Token(Token = "0x40005BA")]
	[FieldOffset(Offset = "0x30")]
	[Header("Parameter")]
	[SerializeField]
	private Color _fireLightColor;

	[Token(Token = "0x40005BB")]
	[FieldOffset(Offset = "0x40")]
	private Color _initialFireLightColor;

	// RECONSTRUCTED | Confidence: A | RVA: 0x380DE0
	// Fire starts inactive; the light's original color is captured for
	// ChangeFireFxActivity to restore later.
	[Token(Token = "0x600051C")]
	[Address(RVA = "0x380DE0", Offset = "0x37F7E0", VA = "0x180380DE0")]
	private void Awake()
	{
		_fireVfx.SetActive(false);
		_initialFireLightColor = _light.color;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x380EC0
	[Token(Token = "0x600051D")]
	[Address(RVA = "0x380EC0", Offset = "0x37F8C0", VA = "0x180380EC0", Slot = "4")]
	public void ChangeFireFxActivity(bool active)
	{
		_fireVfx.SetActive(active);
		_light.color = active ? _fireLightColor : _initialFireLightColor;
	}

	[Token(Token = "0x600051E")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public Court()
	{
	}
}
