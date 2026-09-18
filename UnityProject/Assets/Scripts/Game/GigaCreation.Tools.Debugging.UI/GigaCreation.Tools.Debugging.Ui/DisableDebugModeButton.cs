using GigaCreation.Tools.Debugging.Core;
using Il2CppDummyDll;
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

    	[Token(Token = "0x600001B")]
    	[Address(RVA = "0xD48950", Offset = "0xD47350", VA = "0x180D48950")]
    	private void Start()
    	{
    	}

    	[Token(Token = "0x600001C")]
    	[Address(RVA = "0xD48B40", Offset = "0xD47540", VA = "0x180D48B40")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x600001D")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DisableDebugModeButton()
    	{
    	}
    }
}
