using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x20005B2")]
    public abstract class TextRevealEffect : MonoBehaviour
    {
    	[Token(Token = "0x40015DF")]
    	[FieldOffset(Offset = "0x20")]
    	[Tooltip("Text component to apply the reveal effect for.")]
    	[SerializeField]
    	private RevealableText text;

    	[Token(Token = "0x17000422")]
    	protected virtual RevealableText Text
    	{
    		[Token(Token = "0x6001C65")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "4")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000423")]
    	protected virtual IRevealInfo Info
    	{
    		[Token(Token = "0x6001C66")]
    		[Address(RVA = "0x4A10E0", Offset = "0x49FAE0", VA = "0x1804A10E0", Slot = "5")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6001C67")]
    	[Address(RVA = "0x4A1120", Offset = "0x49FB20", VA = "0x1804A1120")]
    	private void Awake()
    	{
    	}

    	[Token(Token = "0x6001C68")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	protected TextRevealEffect()
    	{
    	}
    }
}
