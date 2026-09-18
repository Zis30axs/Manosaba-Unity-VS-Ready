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

    	[Token(Token = "0x6000003")]
    	[Address(RVA = "0xD46FD0", Offset = "0xD459D0", VA = "0x180D46FD0")]
    	private void Start()
    	{
    	}

    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xD47120", Offset = "0xD45B20", VA = "0x180D47120")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xD47230", Offset = "0xD45C30", VA = "0x180D47230")]
    	private void ChangeVisibility(bool visible)
    	{
    	}

    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public DebugDisplay()
    	{
    	}
    }
}
