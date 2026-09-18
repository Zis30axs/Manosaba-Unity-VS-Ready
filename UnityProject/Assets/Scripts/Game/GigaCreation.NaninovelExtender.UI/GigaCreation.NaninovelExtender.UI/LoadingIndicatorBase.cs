using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Token(Token = "0x200001C")]
    public abstract class LoadingIndicatorBase : MonoBehaviour
    {
    	[Token(Token = "0x4000068")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private CanvasGroup _canvasGroup;

    	[Token(Token = "0x600005A")]
    	[Address(RVA = "0xD3BB40", Offset = "0xD3A540", VA = "0x180D3BB40", Slot = "4")]
    	protected virtual void Reset()
    	{
    	}

    	[Token(Token = "0x600005B")]
    	[Address(RVA = "0xD3BBE0", Offset = "0xD3A5E0", VA = "0x180D3BBE0")]
    	public void SetOpacity(float opacity)
    	{
    	}

    	[Token(Token = "0x600005C")]
    	public abstract void OnShow();

    	[Token(Token = "0x600005D")]
    	public abstract void OnHide();

    	[Token(Token = "0x600005E")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	protected LoadingIndicatorBase()
    	{
    	}
    }
}
