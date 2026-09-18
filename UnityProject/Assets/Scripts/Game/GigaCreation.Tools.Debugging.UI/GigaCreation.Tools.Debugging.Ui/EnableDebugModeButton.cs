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
    	private float _longPressDuration;

    	[Token(Token = "0x400001A")]
    	[FieldOffset(Offset = "0x40")]
    	private IDebugManager _debugManager;

    	[Token(Token = "0x400001B")]
    	[FieldOffset(Offset = "0x48")]
    	private bool _isPressed;

    	[Token(Token = "0x6000020")]
    	[Address(RVA = "0xD48D60", Offset = "0xD47760", VA = "0x180D48D60")]
    	private void Start()
    	{
    	}

    	[Token(Token = "0x6000021")]
    	[Address(RVA = "0xD49220", Offset = "0xD47C20", VA = "0x180D49220")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x6000022")]
    	[Address(RVA = "0xD493C0", Offset = "0xD47DC0", VA = "0x180D493C0")]
    	private void ResetToDefault()
    	{
    	}

    	[Token(Token = "0x6000023")]
    	[Address(RVA = "0xD493F0", Offset = "0xD47DF0", VA = "0x180D493F0")]
    	public EnableDebugModeButton()
    	{
    	}
    }
}
