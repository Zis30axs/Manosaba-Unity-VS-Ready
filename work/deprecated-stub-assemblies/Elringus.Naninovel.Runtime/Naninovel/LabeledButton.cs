using Il2CppDummyDll;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel
{
    [Token(Token = "0x20002B0")]
    public class LabeledButton : Button
    {
    	[Token(Token = "0x4000A7E")]
    	[FieldOffset(Offset = "0x108")]
    	[SerializeField]
    	private TMP_Text labelText;

    	[Token(Token = "0x4000A7F")]
    	[FieldOffset(Offset = "0x110")]
    	[SerializeField]
    	private ColorBlock labelColors;

    	[Token(Token = "0x4000A80")]
    	[FieldOffset(Offset = "0x168")]
    	private Color labelColorMultiplier;

    	[Token(Token = "0x4000A81")]
    	[FieldOffset(Offset = "0x178")]
    	private Tweener<ColorTween> tintTweener;

    	[Token(Token = "0x170001B7")]
    	public virtual TMP_Text Label
    	{
    		[Token(Token = "0x6000C93")]
    		[Address(RVA = "0x53D0F0", Offset = "0x53BAF0", VA = "0x18053D0F0", Slot = "43")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170001B8")]
    	public virtual ColorBlock LabelColorBlock
    	{
    		[Token(Token = "0x6000C94")]
    		[Address(RVA = "0x53D250", Offset = "0x53BC50", VA = "0x18053D250", Slot = "44")]
    		get
    		{
    			return default(ColorBlock);
    		}
    	}

    	[Token(Token = "0x170001B9")]
    	public virtual Color LabelColorMultiplier
    	{
    		[Token(Token = "0x6000C95")]
    		[Address(RVA = "0x53D2A0", Offset = "0x53BCA0", VA = "0x18053D2A0", Slot = "45")]
    		get
    		{
    			return default(Color);
    		}
    		[Token(Token = "0x6000C96")]
    		[Address(RVA = "0x53D2B0", Offset = "0x53BCB0", VA = "0x18053D2B0", Slot = "46")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x6000C97")]
    	[Address(RVA = "0x53D330", Offset = "0x53BD30", VA = "0x18053D330", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6000C98")]
    	[Address(RVA = "0x53D3E0", Offset = "0x53BDE0", VA = "0x18053D3E0", Slot = "26")]
    	protected override void DoStateTransition(SelectionState state, bool instant)
    	{
    	}

    	[Token(Token = "0x6000C99")]
    	[Address(RVA = "0x53DC70", Offset = "0x53C670", VA = "0x18053DC70")]
    	public LabeledButton()
    	{
    	}
    }
}
