using System;
using System.Threading;
using GigaCreation.Essentials.AddressablesUtils;
using Il2CppDummyDll;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

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
		[Token(Token = "0x6000474")]
		[Address(RVA = "0x377920", Offset = "0x376320", VA = "0x180377920")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000475")]
	[Address(RVA = "0x3779E0", Offset = "0x3763E0", VA = "0x1803779E0")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000476")]
	[Address(RVA = "0x377A50", Offset = "0x376450", VA = "0x180377A50")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000477")]
	[Address(RVA = "0x377A70", Offset = "0x376470", VA = "0x180377A70")]
	private void Reset()
	{
	}

	[Token(Token = "0x6000478")]
	[Address(RVA = "0x377C20", Offset = "0x376620", VA = "0x180377C20")]
	public void SetupWithAddressableTexture(string address)
	{
	}

	[Token(Token = "0x6000479")]
	[Address(RVA = "0x378070", Offset = "0x376A70", VA = "0x180378070")]
	public void SetupWithText(string text, int backgroundIndex)
	{
	}

	[Token(Token = "0x600047A")]
	[Address(RVA = "0x3781B0", Offset = "0x376BB0", VA = "0x1803781B0")]
	private void StateLoading()
	{
	}

	[Token(Token = "0x600047B")]
	[Address(RVA = "0x378200", Offset = "0x376C00", VA = "0x180378200")]
	private void SetTexture(Texture2D tex)
	{
	}

	[Token(Token = "0x600047C")]
	[Address(RVA = "0x3783C0", Offset = "0x376DC0", VA = "0x1803783C0")]
	public void ClearContent()
	{
	}

	[Token(Token = "0x600047D")]
	[Address(RVA = "0x378610", Offset = "0x377010", VA = "0x180378610")]
	public IObservable<Unit> OnChoiceAsObservable()
	{
		return null;
	}

	[Token(Token = "0x600047E")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public WitchBookItemButton()
	{
	}
}
