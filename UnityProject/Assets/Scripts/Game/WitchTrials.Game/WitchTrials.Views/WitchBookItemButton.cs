using Cysharp.Threading.Tasks;
using GigaCreation.Tools.Service;
using System;
using System.Threading;
using GigaCreation.Essentials.AddressablesUtils;
using Il2CppDummyDll;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000110")]
    public class WitchBookItemButton : MonoBehaviour
    {
    	[Token(Token = "0x4000511")]
    	[FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private RectTransform _rectTransform;

    	[Token(Token = "0x4000512")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	[Token(Token = "0x4000513")]
    	[FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private Button _button;

    	[Token(Token = "0x4000514")]
    	[FieldOffset(Offset = "0x38")]
    	[Space]
    	[SerializeField]
    	private Image[] _backgrounds;

    	[Token(Token = "0x4000515")]
    	[FieldOffset(Offset = "0x40")]
    	[SerializeField]
    	private RawImage _rawImage;

    	[Token(Token = "0x4000516")]
    	[FieldOffset(Offset = "0x48")]
    	[SerializeField]
    	private TMP_Text _label;

    	[Token(Token = "0x4000517")]
    	[FieldOffset(Offset = "0x50")]
    	private IAddressablesManager _addressableAssetLoader;

    	[Token(Token = "0x4000518")]
    	[FieldOffset(Offset = "0x58")]
    	private Texture _defaultTexture;

    	[Token(Token = "0x4000519")]
    	[FieldOffset(Offset = "0x60")]
    	private CancellationTokenSource _cts;

    	[Token(Token = "0x17000092")]
    	public RectTransform RectTransform
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x377910 (generic reference-field getter)
    		// 292 project-wide methods fold to this same native body; the offset determines
    		// which field each one reads. Here that is `_rectTransform` (+0x20, RectTransform).
    		[Token(Token = "0x6000473")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		get
    		{
    			return _rectTransform;
    		}
    	}

    	[Token(Token = "0x17000093")]
    	private IAddressableAssetLoader AddressableAssetLoader
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x377920, 186 B
    		// Lazy cache: `_addressableAssetLoader (+0x50) ??= ServiceLocator.Get<IAddressablesManager>(true)` (cl = 1).
    		[Token(Token = "0x6000474")]
    		[Address(RVA = "0x377920", Offset = "0x376320", VA = "0x180377920")]
    		get
    		{
    			return _addressableAssetLoader ??= ServiceLocator.Get<IAddressablesManager>(true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3779E0, 107 B
    	// `_defaultTexture (+0x58) = _rawImage (+0x40).texture`: the RawImage getter is inlined as a read of `m_Texture` (+0xE0).
    	[Token(Token = "0x6000475")]
    	[Address(RVA = "0x3779E0", Offset = "0x3763E0", VA = "0x1803779E0")]
    	private void Awake()
    	{
    		_defaultTexture = _rawImage.texture;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x377A50, 17 B
    	// `_cts (+0x60)?.Dispose()`, as a tail jump.
    	[Token(Token = "0x6000476")]
    	[Address(RVA = "0x377A50", Offset = "0x376450", VA = "0x180377A50")]
    	private void OnDestroy()
    	{
    		_cts?.Dispose();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x377A70, 421 B
    	// Unity editor Reset. `transform` is cast with an exact-class compare against RectTransform
    	// (cmove → null on mismatch, i.e. `as`), then `GetComponentInChildren<RawImage>()` and `GetComponent<Button>()`.
    	// `_canvasGroup`, `_backgrounds` and `_label` are left to the Inspector.
    	[Token(Token = "0x6000477")]
    	[Address(RVA = "0x377A70", Offset = "0x376470", VA = "0x180377A70")]
    	private void Reset()
    	{
    		_rectTransform = base.transform as RectTransform;
    		_rawImage = GetComponentInChildren<RawImage>();
    		_button = GetComponent<Button>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x377C20, 1092 B
    	// - `StateLoading()` is inlined (the same three stores as 0x3781B0).
    	// - `_cts?.Cancel()`: the inlined CTS.Cancel runs the disposed check, then `NotifyCancellation(false)`.
    	// - `_cts = new CancellationTokenSource()` (ctor inlined: state = 1, timer = -1).
    	// - `_label (+0x48).SetText(string.Empty)`.
    	// - An array foreach over `_backgrounds (+0x38)` sets `enabled = false` (icall Behaviour::set_enabled_Injected).
    	// - `AddressableAssetLoader` (getter inlined), then `.GetOrLoadAddressableAsset<Texture2D>(address, _cts.Token)`,
    	//   then `.ContinueWith(new Action<Texture2D>(SetTexture))`, then `.Forget()`.
    	[Token(Token = "0x6000478")]
    	[Address(RVA = "0x377C20", Offset = "0x376620", VA = "0x180377C20")]
    	public void SetupWithAddressableTexture(string address)
    	{
    		StateLoading();
    		_cts?.Cancel();
    		_cts = new CancellationTokenSource();
    		_label.SetText(string.Empty);
    		Image[] backgrounds = _backgrounds;
    		foreach (Image image in backgrounds)
    		{
    			image.enabled = false;
    		}
    		AddressableAssetLoader.GetOrLoadAddressableAsset<Texture2D>(address, _cts.Token).ContinueWith(SetTexture).Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x378070, 308 B
    	// `_label.SetText(text)`, then a `for` loop that re-reads `_backgrounds` on every iteration (not a
    	// foreach): `_backgrounds[i].enabled = i == backgroundIndex` (`cmp edi, r15d; sete bl`).
    	[Token(Token = "0x6000479")]
    	[Address(RVA = "0x378070", Offset = "0x376A70", VA = "0x180378070")]
    	public void SetupWithText(string text, int backgroundIndex)
    	{
    		_label.SetText(text);
    		for (int i = 0; i < _backgrounds.Length; i++)
    		{
    			_backgrounds[i].enabled = i == backgroundIndex;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3781B0, 79 B
    	// `_rawImage.texture = _defaultTexture; _canvasGroup (+0x28).alpha = 0f; _canvasGroup.interactable = false` (tail jump).
    	[Token(Token = "0x600047A")]
    	[Address(RVA = "0x3781B0", Offset = "0x376BB0", VA = "0x1803781B0")]
    	private void StateLoading()
    	{
    		_rawImage.texture = _defaultTexture;
    		_canvasGroup.alpha = 0f;
    		_canvasGroup.interactable = false;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x378200, 435 B
    	// - `_rawImage.texture = tex`.
    	// - If `_canvasGroup.alpha == 0f` (icall get_alpha_Injected; ucomiss, with NaN → false): `alpha = 1f`
    	//   (constant 0x2AE2820 = 1.0) and `interactable = true`.
    	// - If `!gameObject.activeSelf` (icall GameObject::get_activeSelf_Injected): `gameObject.SetActive(true)`.
    	// This is the `ContinueWith` callback of the texture load.
    	[Token(Token = "0x600047B")]
    	[Address(RVA = "0x378200", Offset = "0x376C00", VA = "0x180378200")]
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3783C0, 586 B
    	// The backgrounds foreach sets `enabled = false`, then `_rawImage.texture = _defaultTexture`, then
    	// `_label.SetText(string.Empty)`. The CanvasGroup setters are inlined here as direct icalls
    	// (set_alpha_Injected 0f, set_interactable_Injected false). The in-flight `_cts` is not cancelled.
    	[Token(Token = "0x600047C")]
    	[Address(RVA = "0x3783C0", Offset = "0x376DC0", VA = "0x1803783C0")]
    	public void ClearContent()
    	{
    		Image[] backgrounds = _backgrounds;
    		foreach (Image image in backgrounds)
    		{
    			image.enabled = false;
    		}
    		_rawImage.texture = _defaultTexture;
    		_label.SetText(string.Empty);
    		_canvasGroup.alpha = 0f;
    		_canvasGroup.interactable = false;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x378610, 36 B
    	// `_button (+0x30).onClick` (the `m_OnClick` +0x100 read is inlined), then a tail jump to UniRx `UnityEventExtensions.AsObservable`.
    	[Token(Token = "0x600047D")]
    	[Address(RVA = "0x378610", Offset = "0x377010", VA = "0x180378610")]
    	public IObservable<Unit> OnChoiceAsObservable()
    	{
    		return _button.onClick.AsObservable();
    	}

    	[Token(Token = "0x600047E")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public WitchBookItemButton()
    	{
    	}
    }
}
