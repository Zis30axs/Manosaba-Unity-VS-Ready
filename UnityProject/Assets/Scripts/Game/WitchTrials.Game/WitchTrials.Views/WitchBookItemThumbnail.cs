using Cysharp.Threading.Tasks;
using GigaCreation.Tools.Service;
using System.Threading;
using GigaCreation.Essentials.AddressablesUtils;
using Il2CppDummyDll;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views
{
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
    		// RECONSTRUCTED | Confidence: A | RVA: 0x379080, 186 B
    		// Lazy cache: `_addressableAssetLoader (+0x30) ??= ServiceLocator.Get<IAddressablesManager>(true)`.
    		[Token(Token = "0x600048C")]
    		[Address(RVA = "0x379080", Offset = "0x377A80", VA = "0x180379080")]
    		get
    		{
    			return _addressableAssetLoader ??= ServiceLocator.Get<IAddressablesManager>(true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x379140, 107 B
    	// `_defaultTexture (+0x38) = _rawImage (+0x28).texture` (inlined `m_Texture` read).
    	[Token(Token = "0x600048D")]
    	[Address(RVA = "0x379140", Offset = "0x377B40", VA = "0x180379140")]
    	private void Awake()
    	{
    		_defaultTexture = _rawImage.texture;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3791B0, 292 B
    	// `_canvasGroup (+0x20) = GetComponent<CanvasGroup>(); _rawImage (+0x28) = GetComponent<RawImage>()`.
    	[Token(Token = "0x600048E")]
    	[Address(RVA = "0x3791B0", Offset = "0x377BB0", VA = "0x1803791B0")]
    	private void Reset()
    	{
    		_canvasGroup = GetComponent<CanvasGroup>();
    		_rawImage = GetComponent<RawImage>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3792E0, 17 B
    	// `_cts (+0x40)?.Dispose()`.
    	[Token(Token = "0x600048F")]
    	[Address(RVA = "0x3792E0", Offset = "0x377CE0", VA = "0x1803792E0")]
    	private void OnDestroy()
    	{
    		_cts?.Dispose();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x379300, 802 B
    	// Same flow as `WitchBookItemButton.SetupWithAddressableTexture` minus the label and backgrounds:
    	// an inlined `StateLoading()`, then `_cts?.Cancel()`, then a new CTS, then
    	// `AddressableAssetLoader.GetOrLoadAddressableAsset<Texture2D>(address, _cts.Token).ContinueWith(SetTexture).Forget()`.
    	[Token(Token = "0x6000490")]
    	[Address(RVA = "0x379300", Offset = "0x377D00", VA = "0x180379300")]
    	public void Setup(string address)
    	{
    		StateLoading();
    		_cts?.Cancel();
    		_cts = new CancellationTokenSource();
    		AddressableAssetLoader.GetOrLoadAddressableAsset<Texture2D>(address, _cts.Token).ContinueWith(SetTexture).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x379630, 79 B
    	// `_rawImage.texture = _defaultTexture; _canvasGroup.alpha = 0f; _canvasGroup.interactable = false`.
    	[Token(Token = "0x6000491")]
    	[Address(RVA = "0x379630", Offset = "0x378030", VA = "0x180379630")]
    	private void StateLoading()
    	{
    		_rawImage.texture = _defaultTexture;
    		_canvasGroup.alpha = 0f;
    		_canvasGroup.interactable = false;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x379680, 435 B
    	// Same as `WitchBookItemButton.SetTexture` (0x378200) with this class's field offsets: the texture is
    	// assigned, then `alpha == 0f` → 1f and interactable, then `activeSelf` → SetActive(true).
    	[Token(Token = "0x6000492")]
    	[Address(RVA = "0x379680", Offset = "0x378080", VA = "0x180379680")]
    	private void SetTexture(Texture2D tex)
    	{
    		_rawImage.texture = tex;
    		if (_canvasGroup.alpha == 0f)
    		{
    			_canvasGroup.alpha = 1f;
    			_canvasGroup.interactable = true;
    		}
    		if (!base.gameObject.activeSelf)
    		{
    			base.gameObject.SetActive(true);
    		}
    	}

    	[Token(Token = "0x6000493")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public WitchBookItemThumbnail()
    	{
    	}
    }
}
