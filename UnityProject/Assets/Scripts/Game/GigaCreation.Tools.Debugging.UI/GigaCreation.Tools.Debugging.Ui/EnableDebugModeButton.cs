using UnityEngine.EventSystems;
using UniRx.Triggers;
using UniRx;
using GigaCreation.Tools.Service;
using GigaCreation.Tools.Debugging.Core;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Tools.Debugging.Ui
{
    [Token(Token = "0x200000A")]
    [RequireComponent(typeof(CanvasGroup), typeof(Image), typeof(Selectable))]
    public class EnableDebugModeButton : MonoBehaviour
    {
    	[Token(Token = "0x4000016")]
    	[FieldOffset(Offset = "0x20")]
    	[Header("References")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	[Token(Token = "0x4000017")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Image _image;

    	[Token(Token = "0x4000018")]
    	[FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private Selectable _selectable;

    	[Token(Token = "0x4000019")]
    	[FieldOffset(Offset = "0x38")]
    	[Header("Parameters")]
    	[SerializeField]
    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xD493F0: mov dword [rcx+0x38], 0x3F800000 (1.0)
    	private float _longPressDuration = 1f;

    	[Token(Token = "0x400001A")]
    	[FieldOffset(Offset = "0x40")]
    	private IDebugManager _debugManager;

    	[Token(Token = "0x400001B")]
    	[FieldOffset(Offset = "0x48")]
    	private bool _isPressed;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD48D60, 1202 B -- closure <>c__DisplayClass6_0 {this, float pressedTime (+0x18)}:
    	// - ServiceLocator.TryGet<IDebugManager>(out +0x40) false -> done (0x180D48F9C).
    	// - pressedTime = 0, `_image` (+0x28).fillAmount = 0.
    	// - UpdateAsObservable().Where(b__0: `_isPressed` (+0x48)).Subscribe(b__1, 0xD49460): fillAmount = (Time.
    	//   realtimeSinceStartup (icall data_2608CF0) - pressedTime) / `_longPressDuration` (+0x38); once the elapsed time
    	//   reaches the duration (comiss/ja) -> _isPressed = 0, fillAmount = 0, IsDebugMode (slot 0).Value = true
    	//   (IReactiveProperty<bool> slot 1).
    	// - `_selectable` (+0x30).OnPointerDownAsObservable().Where(b__2: !IsDebugMode.Value).Subscribe(b__3: _isPressed
    	//   = 1, pressedTime = realtimeSinceStartup); OnPointerUpAsObservable().Where(b__4, same test).Subscribe(b__5:
    	//   _isPressed = 0, fillAmount = 0).
    	// - IsDebugMode.Subscribe(b__6, 0xD49780): `_canvasGroup` (+0x20) alpha = x ? 0 : 1 (data_2AE2820 = 1.0),
    	//   interactable = !x, blocksRaycasts = !x.  Each subscription AddTo(this).
    	//   So in a development build the button shows at the Image's own 25 % alpha until debug mode is on; a release
    	//   build destroys the whole DebugCanvas first (DestroySelfIfReleaseBuild).
    	// Annotated disassembly: work/title-path/disas/edmb_D48D60, edmb_D49440 .. edmb_D49780 (b__0 .. b__6).txt
    	[Token(Token = "0x6000020")]
    	[Address(RVA = "0xD48D60", Offset = "0xD47760", VA = "0x180D48D60")]
    	private void Start()
    	{
    		// 0x180D48F9C: no IDebugManager registered (a release build) -> the button stays inert
    		if (!ServiceLocator.TryGet(out _debugManager))
    		{
    			return;
    		}
    		float pressedTime = 0f;
    		_image.fillAmount = 0f;
    		this.UpdateAsObservable().Where((Unit _) => _isPressed).Subscribe(delegate
    		{
    			_image.fillAmount = (Time.realtimeSinceStartup - pressedTime) / _longPressDuration;
    			if (Time.realtimeSinceStartup - pressedTime >= _longPressDuration)
    			{
    				_isPressed = false;
    				_image.fillAmount = 0f;
    				_debugManager.IsDebugMode.Value = true;
    			}
    		}).AddTo(this);
    		_selectable.OnPointerDownAsObservable().Where((PointerEventData _) => !_debugManager.IsDebugMode.Value).Subscribe(delegate
    		{
    			_isPressed = true;
    			pressedTime = Time.realtimeSinceStartup;
    		}).AddTo(this);
    		_selectable.OnPointerUpAsObservable().Where((PointerEventData _) => !_debugManager.IsDebugMode.Value).Subscribe(delegate
    		{
    			_isPressed = false;
    			_image.fillAmount = 0f;
    		}).AddTo(this);
    		_debugManager.IsDebugMode.Subscribe(delegate(bool x)
    		{
    			_canvasGroup.alpha = (x ? 0f : 1f);
    			_canvasGroup.interactable = !x;
    			_canvasGroup.blocksRaycasts = !x;
    		}).AddTo(this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD49220, 405 B -- GetComponent<CanvasGroup> -> +0x20, <Image> -> +0x28,
    	//   <Selectable> -> +0x30
    	// Annotated disassembly: work/title-path/disas/edmb_D49220.txt
    	[Token(Token = "0x6000021")]
    	[Address(RVA = "0xD49220", Offset = "0xD47C20", VA = "0x180D49220")]
    	private void Reset()
    	{
    		_canvasGroup = GetComponent<CanvasGroup>();
    		_image = GetComponent<Image>();
    		_selectable = GetComponent<Selectable>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD493C0, 37 B -- +0x48 = 0, `_image`.fillAmount = 0 (tail jump)
    	// Annotated disassembly: work/title-path/disas/edmb_D493C0.txt
    	[Token(Token = "0x6000022")]
    	[Address(RVA = "0xD493C0", Offset = "0xD47DC0", VA = "0x180D493C0")]
    	private void ResetToDefault()
    	{
    		_isPressed = false;
    		_image.fillAmount = 0f;
    	}

    	[Token(Token = "0x6000023")]
    	[Address(RVA = "0xD493F0", Offset = "0xD47DF0", VA = "0x180D493F0")]
    	public EnableDebugModeButton()
    	{
    	}
    }
}
