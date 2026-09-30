using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    [Token(Token = "0x2000526")]
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollController : MonoBehaviour
    {
    	[Token(Token = "0x40013F6")]
    	[FieldOffset(Offset = "0x20")]
    	private ScrollRect rect;

    	[Token(Token = "0x40013F7")]
    	[FieldOffset(Offset = "0x28")]
    	private IInputSampler input;

    	[Token(Token = "0x600199D")]
    	[Address(RVA = "0x61CAC0", Offset = "0x61B4C0", VA = "0x18061CAC0")]
    	private void Awake()
    	{
    	}

    	[Token(Token = "0x600199E")]
    	[Address(RVA = "0x61CC60", Offset = "0x61B660", VA = "0x18061CC60")]
    	private void Update()
    	{
    	}

    	[Token(Token = "0x600199F")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public ScrollController()
    	{
    	}
    }
}
