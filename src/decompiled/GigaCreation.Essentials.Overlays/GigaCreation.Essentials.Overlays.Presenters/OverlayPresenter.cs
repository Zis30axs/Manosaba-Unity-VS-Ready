using GigaCreation.Essentials.Overlays.Models;
using GigaCreation.Essentials.Overlays.Views;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Overlays.Presenters;

[Token(Token = "0x2000006")]
public class OverlayPresenter : MonoBehaviour
{
	[Token(Token = "0x400000D")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private Overlay _overlay;

	[Token(Token = "0x400000E")]
	[FieldOffset(Offset = "0x28")]
	private IOverlayFader _overlayFader;

	[Token(Token = "0x6000008")]
	[Address(RVA = "0xCD46B0", Offset = "0xCD30B0", VA = "0x180CD46B0")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000009")]
	[Address(RVA = "0xCD4920", Offset = "0xCD3320", VA = "0x180CD4920")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x600000A")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public OverlayPresenter()
	{
	}
}
