using GigaCreation.Tools.Debugging.Core;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Tools.Debugging.Ui
{
    [Token(Token = "0x2000009")]
    [RequireComponent(typeof(CanvasGroup), typeof(Button))]
    public class DisableDebugModeButton : MonoBehaviour
    {
    	[Token(Token = "0x4000013")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	[Token(Token = "0x4000014")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Button _button;

    	[Token(Token = "0x4000015")]
    	[FieldOffset(Offset = "0x30")]
    	private IDebugManager _debugManager;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD48950, 482 B; lambdas <Start>b__3_0 0xD48C70 and b__3_1 0xD48CF0.
    	// - ServiceLocator.TryGet<IDebugManager>(out +0x30) false -> inert.
    	// - `_button` (+0x28).onClick.AsObservable().Subscribe(b__3_0: IsDebugMode (slot 0).Value = false
    	//   (IReactiveProperty<bool> slot 1)).AddTo(this).
    	// - IsDebugMode.Subscribe(b__3_1: `_canvasGroup` (+0x20) alpha = x ? 1 : 0, interactable = x,
    	//   blocksRaycasts = x).AddTo(this) -- the inverse of EnableDebugModeButton.
    	// Annotated disassembly: work/title-path/disas/dbgp_*.txt (regenerate with work/reversing/disas.py).
    	[Token(Token = "0x600001B")]
    	[Address(RVA = "0xD48950", Offset = "0xD47350", VA = "0x180D48950")]
    	private void Start()
    	{
    		if (!ServiceLocator.TryGet(out _debugManager))
    		{
    			return;
    		}
    		_button.onClick.AsObservable().Subscribe(_ => _debugManager.IsDebugMode.Value = false).AddTo(this);
    		_debugManager.IsDebugMode.Subscribe(delegate(bool x)
    		{
    			_canvasGroup.alpha = (x ? 1f : 0f);
    			_canvasGroup.interactable = x;
    			_canvasGroup.blocksRaycasts = x;
    		}).AddTo(this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD48B40, 296 B -- GetComponent<CanvasGroup> -> +0x20, <Button> -> +0x28.
    	[Token(Token = "0x600001C")]
    	[Address(RVA = "0xD48B40", Offset = "0xD47540", VA = "0x180D48B40")]
    	private void Reset()
    	{
    		_canvasGroup = GetComponent<CanvasGroup>();
    		_button = GetComponent<Button>();
    	}

    	[Token(Token = "0x600001D")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DisableDebugModeButton()
    	{
    	}
    }
}
