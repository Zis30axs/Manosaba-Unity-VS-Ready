using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x200003E")]
    public class Underlay : MonoBehaviour
    {
    	[Token(Token = "0x4000105")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private Graphic _graphic;

    	[Token(Token = "0x4000106")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private Button _button;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEA0D0 -- GetComponent<Graphic> -> +0x20, GetComponent<Button> -> +0x28
    	// Annotated disassembly: work/title-path/disas/ul_CEA0D0.txt
    	[Token(Token = "0x6000120")]
    	[Address(RVA = "0xCEA0D0", Offset = "0xCE8AD0", VA = "0x180CEA0D0")]
    	private void Reset()
    	{
    		_graphic = GetComponent<Graphic>();
    		_button = GetComponent<Button>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEA200 -- `_graphic` (+0x20) alive -> virtual set_raycastTarget
    	//   (vtable +0x2C8)
    	// Annotated disassembly: work/title-path/disas/ul_CEA200.txt
    	[Token(Token = "0x6000121")]
    	[Address(RVA = "0xCEA200", Offset = "0xCE8C00", VA = "0x180CEA200")]
    	public void SetRaycastTargetEnabled(bool enable)
    	{
    		if (_graphic)
    		{
    			_graphic.raycastTarget = enable;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEA300 -- `_button` (+0x28) alive -> Selectable.set_interactable
    	// Annotated disassembly: work/title-path/disas/ul_CEA300.txt
    	[Token(Token = "0x6000122")]
    	[Address(RVA = "0xCEA300", Offset = "0xCE8D00", VA = "0x180CEA300")]
    	public void SetInteractable(bool enable)
    	{
    		if (_button)
    		{
    			_button.interactable = enable;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEA400 -- `_button` alive -> new AsyncUnityEventHandler(onClick, ct,
    	//   callOnce: true).OnInvokeAsync() (UniTask's Button.OnClickAsync inlined); else UniTask.CompletedTask
    	//   (static +0x10); returns `_button` alive again (setne on m_CachedPtr)
    	// Annotated disassembly: work/title-path/disas/ul_CEA400.txt
    	[Token(Token = "0x6000123")]
    	[Address(RVA = "0xCEA400", Offset = "0xCE8E00", VA = "0x180CEA400")]
    	public bool TryGetClickTask(out UniTask task, [Optional] CancellationToken ct)
    	{
    		task = (_button ? _button.OnClickAsync(ct) : UniTask.CompletedTask);
    		return _button;
    	}

    	[Token(Token = "0x6000124")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public Underlay()
    	{
    	}
    }
}
