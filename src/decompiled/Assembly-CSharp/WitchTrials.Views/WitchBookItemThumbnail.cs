using System.Threading;
using GigaCreation.Essentials.AddressablesUtils;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x2000115")]
public class WitchBookItemThumbnail : MonoBehaviour
{
	[Token(Token = "0x4000528")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private CanvasGroup _canvasGroup;

	[Token(Token = "0x4000529")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private RawImage _rawImage;

	[Token(Token = "0x400052A")]
	[FieldOffset(Offset = "0x30")]
	private IAddressablesManager _addressableAssetLoader;

	[Token(Token = "0x400052B")]
	[FieldOffset(Offset = "0x38")]
	private Texture _defaultTexture;

	[Token(Token = "0x400052C")]
	[FieldOffset(Offset = "0x40")]
	private CancellationTokenSource _cts;

	[Token(Token = "0x17000097")]
	private IAddressableAssetLoader AddressableAssetLoader
	{
		[Token(Token = "0x600048C")]
		[Address(RVA = "0x379080", Offset = "0x377A80", VA = "0x180379080")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600048D")]
	[Address(RVA = "0x379140", Offset = "0x377B40", VA = "0x180379140")]
	private void Awake()
	{
	}

	[Token(Token = "0x600048E")]
	[Address(RVA = "0x3791B0", Offset = "0x377BB0", VA = "0x1803791B0")]
	private void Reset()
	{
	}

	[Token(Token = "0x600048F")]
	[Address(RVA = "0x3792E0", Offset = "0x377CE0", VA = "0x1803792E0")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000490")]
	[Address(RVA = "0x379300", Offset = "0x377D00", VA = "0x180379300")]
	public void Setup(string address)
	{
	}

	[Token(Token = "0x6000491")]
	[Address(RVA = "0x379630", Offset = "0x378030", VA = "0x180379630")]
	private void StateLoading()
	{
	}

	[Token(Token = "0x6000492")]
	[Address(RVA = "0x379680", Offset = "0x378080", VA = "0x180379680")]
	private void SetTexture(Texture2D tex)
	{
	}

	[Token(Token = "0x6000493")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public WitchBookItemThumbnail()
	{
	}
}
