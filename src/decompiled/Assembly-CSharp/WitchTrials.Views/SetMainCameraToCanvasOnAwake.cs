using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x2000017")]
[RequireComponent(typeof(Canvas))]
public class SetMainCameraToCanvasOnAwake : MonoBehaviour
{
	[Token(Token = "0x400002B")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private Canvas _canvas;

	// RECONSTRUCTED | Confidence: A | RVA: 0x30F000, 46 B
	[Token(Token = "0x600004D")]
	[Address(RVA = "0x30F000", Offset = "0x30DA00", VA = "0x18030F000")]
	private void Awake()
	{
		_canvas.worldCamera = Camera.main;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x30F030, 151 B
	[Token(Token = "0x600004E")]
	[Address(RVA = "0x30F030", Offset = "0x30DA30", VA = "0x18030F030")]
	private void Reset()
	{
		_canvas = GetComponent<Canvas>();
	}

	[Token(Token = "0x600004F")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public SetMainCameraToCanvasOnAwake()
	{
	}
}
