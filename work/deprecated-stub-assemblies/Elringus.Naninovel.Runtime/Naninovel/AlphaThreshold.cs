using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel
{
    [Token(Token = "0x20002AD")]
    public class AlphaThreshold : MonoBehaviour
    {
    	[Token(Token = "0x4000A76")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private Image graphic;

    	[Token(Token = "0x4000A77")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private float minimumThreshold;

    	[Token(Token = "0x6000C88")]
    	[Address(RVA = "0x53C2B0", Offset = "0x53ACB0", VA = "0x18053C2B0")]
    	private void Start()
    	{
    	}

    	[Token(Token = "0x6000C89")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public AlphaThreshold()
    	{
    	}
    }
}
