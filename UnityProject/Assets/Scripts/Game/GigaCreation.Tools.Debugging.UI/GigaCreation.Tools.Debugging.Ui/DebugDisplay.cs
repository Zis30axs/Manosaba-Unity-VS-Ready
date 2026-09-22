using UniRx;
using GigaCreation.Tools.Service;
using GigaCreation.Tools.Debugging.Core;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.Debugging.Ui
{
    [Token(Token = "0x2000004")]
    public class DebugDisplay : MonoBehaviour
    {
    	[Token(Token = "0x4000006")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private Transform _transform;

    	[Token(Token = "0x4000007")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Canvas _canvas;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD46FD0, 332 B -- ServiceLocator.TryGet<IDebugManager>; when present its IsDebugMode
    	//   (slot 0) is subscribed with ChangeVisibility (Action<bool> on this) and AddTo(this).  The ReactiveProperty pushes
    	//   its current value on subscribe, so a debug build whose presenter starts with _isDebugMode = false hides the panel.
    	// Annotated disassembly: work/title-path/disas/dd_01_Start.txt
    	[Token(Token = "0x6000003")]
    	[Address(RVA = "0xD46FD0", Offset = "0xD459D0", VA = "0x180D46FD0")]
    	private void Start()
    	{
    		if (ServiceLocator.TryGet(out IDebugManager debugManager))
    		{
    			debugManager.IsDebugMode.Subscribe(ChangeVisibility).AddTo(this);
    		}
    	}

    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xD47120", Offset = "0xD45B20", VA = "0x180D47120")]
    	private void Reset()
    	{
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD47230, 490 B -- `_canvas` (+0x28) alive (Object implicit bool, m_CachedPtr) ->
    	//   Behaviour.set_enabled(visible) (tail call); otherwise `_transform` (+0x20).localScale = Vector3.one (static +0xC) or
    	//   Vector3.zero (static +0x0)
    	// Annotated disassembly: work/title-path/disas/dd_03_ChangeVisibility.txt
    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xD47230", Offset = "0xD45C30", VA = "0x180D47230")]
    	private void ChangeVisibility(bool visible)
    	{
    		if (_canvas)
    		{
    			_canvas.enabled = visible;
    			return;
    		}
    		_transform.localScale = (visible ? Vector3.one : Vector3.zero);
    	}

    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DebugDisplay()
    	{
    	}
    }
}
