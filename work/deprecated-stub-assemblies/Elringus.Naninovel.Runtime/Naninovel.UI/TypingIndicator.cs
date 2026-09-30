using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x20005A9")]
    public class TypingIndicator : MonoBehaviour
    {
    	[Token(Token = "0x40015BF")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private float printDotDelay;

    	[Token(Token = "0x40015C0")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private string typeSymbol;

    	[Token(Token = "0x40015C1")]
    	[FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private int symbolCount;

    	[Token(Token = "0x40015C2")]
    	[FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private TMP_Text text;

    	[Token(Token = "0x40015C3")]
    	[FieldOffset(Offset = "0x40")]
    	private float lastPrintDotTime;

    	[Token(Token = "0x6001C27")]
    	[Address(RVA = "0x49CEF0", Offset = "0x49B8F0", VA = "0x18049CEF0")]
    	private void Awake()
    	{
    	}

    	[Token(Token = "0x6001C28")]
    	[Address(RVA = "0x49CFD0", Offset = "0x49B9D0", VA = "0x18049CFD0")]
    	private void Update()
    	{
    	}

    	[Token(Token = "0x6001C29")]
    	[Address(RVA = "0x49D180", Offset = "0x49BB80", VA = "0x18049D180")]
    	public TypingIndicator()
    	{
    	}
    }
}
