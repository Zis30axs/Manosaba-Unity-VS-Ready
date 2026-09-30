using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Naninovel
{
    [Token(Token = "0x20002B8")]
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(Button))]
    public class ScriptableGridSlot : ScriptableButton, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
    	[Serializable]
    	[Token(Token = "0x20002B9")]
    	private class OnSlotClickedEvent : UnityEvent<string>
    	{
    		[Token(Token = "0x6000CD8")]
    		[Address(RVA = "0x53F130", Offset = "0x53DB30", VA = "0x18053F130")]
    		public OnSlotClickedEvent()
    		{
    		}
    	}

    	[Token(Token = "0x4000AA2")]
    	[FieldOffset(Offset = "0x9C")]
    	[Tooltip("Opacity to fade to when the slot is hovered or selected; set to zero to disable the fade behaviour.")]
    	[SerializeField]
    	private float hoverOpacityFade;

    	[Token(Token = "0x4000AA3")]
    	[FieldOffset(Offset = "0xA0")]
    	[SerializeField]
    	private OnSlotClickedEvent onSlotClicked;

    	[Token(Token = "0x4000AA4")]
    	[FieldOffset(Offset = "0xA8")]
    	private readonly Tweener<FloatTween> fadeTweener;

    	[Token(Token = "0x170001C4")]
    	public virtual string Id
    	{
    		[Token(Token = "0x6000CCC")]
    		[Address(RVA = "0x468E20", Offset = "0x467820", VA = "0x180468E20", Slot = "67")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170001C5")]
    	public virtual bool Selected
    	{
    		[Token(Token = "0x6000CCD")]
    		[Address(RVA = "0x468E50", Offset = "0x467850", VA = "0x180468E50", Slot = "68")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000CCE")]
    		[Address(RVA = "0x53EA60", Offset = "0x53D460", VA = "0x18053EA60")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x6000CCF")]
    	[Address(RVA = "0x53EA70", Offset = "0x53D470", VA = "0x18053EA70", Slot = "69")]
    	public virtual void OnPointerEnter(PointerEventData eventData)
    	{
    	}

    	[Token(Token = "0x6000CD0")]
    	[Address(RVA = "0x53EA90", Offset = "0x53D490", VA = "0x18053EA90", Slot = "70")]
    	public virtual void OnPointerExit(PointerEventData eventData)
    	{
    	}

    	[Token(Token = "0x6000CD1")]
    	[Address(RVA = "0x53EAB0", Offset = "0x53D4B0", VA = "0x18053EAB0", Slot = "71")]
    	public virtual void OnSelect(BaseEventData eventData)
    	{
    	}

    	[Token(Token = "0x6000CD2")]
    	[Address(RVA = "0x53EAD0", Offset = "0x53D4D0", VA = "0x18053EAD0", Slot = "72")]
    	public virtual void OnDeselect(BaseEventData eventData)
    	{
    	}

    	[Token(Token = "0x6000CD3")]
    	[Address(RVA = "0x53EAF0", Offset = "0x53D4F0", VA = "0x18053EAF0", Slot = "6")]
    	protected override void Start()
    	{
    	}

    	[Token(Token = "0x6000CD4")]
    	[Address(RVA = "0x53EB30", Offset = "0x53D530", VA = "0x18053EB30", Slot = "62")]
    	protected override void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x6000CD5")]
    	[Address(RVA = "0x53EBA0", Offset = "0x53D5A0", VA = "0x18053EBA0", Slot = "73")]
    	protected virtual void FadeInSlot()
    	{
    	}

    	[Token(Token = "0x6000CD6")]
    	[Address(RVA = "0x53EDE0", Offset = "0x53D7E0", VA = "0x18053EDE0", Slot = "74")]
    	protected virtual void FadeOutSlot()
    	{
    	}

    	[Token(Token = "0x6000CD7")]
    	[Address(RVA = "0x53F050", Offset = "0x53DA50", VA = "0x18053F050")]
    	public ScriptableGridSlot()
    	{
    	}
    }
}
