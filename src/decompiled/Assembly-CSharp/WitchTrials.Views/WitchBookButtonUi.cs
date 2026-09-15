using Cysharp.Threading.Tasks;
using GigaCreation.NaninovelExtender.Ui;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x200010F")]
public class WitchBookButtonUi : CustomUI, IWitchBookButtonUi, IManagedUI
{
	[Token(Token = "0x400050C")]
	[FieldOffset(Offset = "0xE0")]
	[Header("References for WitchBook Button")]
	[SerializeField]
	private Button _button;

	[Token(Token = "0x400050D")]
	[FieldOffset(Offset = "0xE8")]
	[Header("Parameters for WitchBook Button")]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _openSfxPath;

	[Token(Token = "0x400050E")]
	[FieldOffset(Offset = "0xF0")]
	private IAudioManager _naniAudioManager;

	[Token(Token = "0x400050F")]
	[FieldOffset(Offset = "0xF8")]
	private IUIManager _naniUiManager;

	[Token(Token = "0x4000510")]
	[FieldOffset(Offset = "0x100")]
	private IWitchBookUi _witchBookUi;

	[Token(Token = "0x1700008F")]
	private IAudioManager NaniAudioManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x3772E0
		// Lazy cache in +0xF0 via `Engine.GetServiceOrErr<IAudioManager>()`.
		[Token(Token = "0x600046D")]
		[Address(RVA = "0x3772E0", Offset = "0x375CE0", VA = "0x1803772E0")]
		get
		{
			return _naniAudioManager ??= Engine.GetServiceOrErr<IAudioManager>();
		}
	}

	[Token(Token = "0x17000090")]
	private IUIManager NaniUiManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x3773B0
		// Lazy cache in +0xF8 via `Engine.GetServiceOrErr<IUIManager>()`.
		[Token(Token = "0x600046E")]
		[Address(RVA = "0x3773B0", Offset = "0x375DB0", VA = "0x1803773B0")]
		get
		{
			return _naniUiManager ??= Engine.GetServiceOrErr<IUIManager>();
		}
	}

	[Token(Token = "0x17000091")]
	private IWitchBookUi WitchBookUi
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x377480
		// Lazy cache in +0x100 via `NaniUiManager.GetUI<IWitchBookUi>()` (getter inlined).
		[Token(Token = "0x600046F")]
		[Address(RVA = "0x377480", Offset = "0x375E80", VA = "0x180377480")]
		get
		{
			return _witchBookUi ??= NaniUiManager.GetUI<IWitchBookUi>();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x377600, lambda <Start>b__11_0 0x3776F0
	// `_button` (+0xE0) `onClick` (+0x100, getter inlined) -> UniRx `AsObservable()` -> `Subscribe(Action<Unit>)`
	// with an instance lambda -> `AddTo(this)`. No base call is visible; neither CustomUI nor ScriptableUIBehaviour
	// declares Start, so the base is Unity's empty UIBehaviour.Start (a call to it would vanish either way).
	// Lambda: NaniAudioManager (inlined) PlaySfx(_openSfxPath +0xE8, 1f, 0f, false, null, default), result discarded;
	// IWitchBookUi slot 0 SetPresentationMode(0 = Local), slot 1 SetRestrictionCategories(null), slot 2
	// SetRestrictionMapAreas(null); then ManagedUiExtensions.PresentAsync(ui, null, destroyCancellationToken)
	// (CancellationToken overload) -> Cysharp Forget. WitchBookUi is re-fetched through its getter for each call.
	[Token(Token = "0x6000470")]
	[Address(RVA = "0x377600", Offset = "0x376000", VA = "0x180377600", Slot = "6")]
	protected override void Start()
	{
		_button.onClick.AsObservable()
			.Subscribe(_ =>
			{
				NaniAudioManager.PlaySfx(_openSfxPath);
				WitchBookUi.SetPresentationMode(WitchBookPresentationMode.Local);
				WitchBookUi.SetRestrictionCategories(null);
				WitchBookUi.SetRestrictionMapAreas(null);
				WitchBookUi.PresentAsync(null, destroyCancellationToken).Forget();
			})
			.AddTo(this);
	}

	[Token(Token = "0x6000471")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public WitchBookButtonUi()
	{
	}
}
