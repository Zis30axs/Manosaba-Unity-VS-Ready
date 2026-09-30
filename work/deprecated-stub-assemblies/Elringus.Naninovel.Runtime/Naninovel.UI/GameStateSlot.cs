using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Naninovel.UI
{
    [Token(Token = "0x200056A")]
    public class GameStateSlot : ScriptableGridSlot
    {
    	[Serializable]
    	[Token(Token = "0x200056B")]
    	private class OnTitleTextChangedEvent : UnityEvent<string>
    	{
    		[Token(Token = "0x6001AEC")]
    		[Address(RVA = "0x631070", Offset = "0x62FA70", VA = "0x180631070")]
    		public OnTitleTextChangedEvent()
    		{
    		}
    	}

    	[Token(Token = "0x400149F")]
    	[FieldOffset(Offset = "0x0")]
    	[ManagedText("DefaultUI")]
    	protected static string EmptySlotLabel;

    	[Token(Token = "0x40014A0")]
    	[FieldOffset(Offset = "0xC8")]
    	[Tooltip("Format of the date set in the title. For available options see C# docs for date and time format strings.")]
    	[SerializeField]
    	private string dateFormat;

    	[Token(Token = "0x40014A1")]
    	[FieldOffset(Offset = "0xD0")]
    	[Tooltip("Title template. `{N}` is replaced with the slot number, `{D}` — with the date (or empty label when the slot is empty).")]
    	[SerializeField]
    	private string titleTemplate;

    	[Token(Token = "0x40014A2")]
    	[FieldOffset(Offset = "0xD8")]
    	[SerializeField]
    	private Button deleteButton;

    	[Token(Token = "0x40014A3")]
    	[FieldOffset(Offset = "0xE0")]
    	[SerializeField]
    	private RawImage thumbnailImage;

    	[Token(Token = "0x40014A4")]
    	[FieldOffset(Offset = "0xE8")]
    	[SerializeField]
    	private Texture2D emptySlotThumbnail;

    	[Token(Token = "0x40014A5")]
    	[FieldOffset(Offset = "0xF0")]
    	[SerializeField]
    	private OnTitleTextChangedEvent onTitleTextChanged;

    	[Token(Token = "0x40014A6")]
    	[FieldOffset(Offset = "0xF8")]
    	private Action<int> onClicked;

    	[Token(Token = "0x40014A7")]
    	[FieldOffset(Offset = "0x100")]
    	private Action<int> onDeleteClicked;

    	[Token(Token = "0x40014A8")]
    	[FieldOffset(Offset = "0x108")]
    	private ScriptableUIBehaviour deleteButtonBehaviour;

    	[Token(Token = "0x40014A9")]
    	[FieldOffset(Offset = "0x110")]
    	private ISaveLoadUI saveLoadUI;

    	[Token(Token = "0x40014AA")]
    	[FieldOffset(Offset = "0x118")]
    	private ILocalizationManager l10n;

    	[Token(Token = "0x170003C8")]
    	public override string Id
    	{
    		[Token(Token = "0x6001ACF")]
    		[Address(RVA = "0x62FBB0", Offset = "0x62E5B0", VA = "0x18062FBB0", Slot = "67")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003C9")]
    	public virtual int SlotNumber
    	{
    		[Token(Token = "0x6001AD0")]
    		[Address(RVA = "0x62FC60", Offset = "0x62E660", VA = "0x18062FC60", Slot = "75")]
    		[CompilerGenerated]
    		get
    		{
    			return default(int);
    		}
    		[Token(Token = "0x6001AD1")]
    		[Address(RVA = "0x62FC70", Offset = "0x62E670", VA = "0x18062FC70")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170003CA")]
    	public virtual GameStateMap State
    	{
    		[Token(Token = "0x6001AD2")]
    		[Address(RVA = "0x492CE0", Offset = "0x4916E0", VA = "0x180492CE0", Slot = "76")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001AD3")]
    		[Address(RVA = "0x62FC80", Offset = "0x62E680", VA = "0x18062FC80")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170003CB")]
    	public virtual bool Empty
    	{
    		[Token(Token = "0x6001AD4")]
    		[Address(RVA = "0x62FCE0", Offset = "0x62E6E0", VA = "0x18062FCE0", Slot = "77")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170003CC")]
    	public virtual float LastSelectTime
    	{
    		[Token(Token = "0x6001AD5")]
    		[Address(RVA = "0x62FD10", Offset = "0x62E710", VA = "0x18062FD10", Slot = "78")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001AD6")]
    		[Address(RVA = "0x62FD20", Offset = "0x62E720", VA = "0x18062FD20")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170003CD")]
    	protected virtual Button DeleteButton
    	{
    		[Token(Token = "0x6001AD7")]
    		[Address(RVA = "0x5F73F0", Offset = "0x5F5DF0", VA = "0x1805F73F0", Slot = "79")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003CE")]
    	protected virtual RawImage ThumbnailImage
    	{
    		[Token(Token = "0x6001AD8")]
    		[Address(RVA = "0x33E0B0", Offset = "0x33CAB0", VA = "0x18033E0B0", Slot = "80")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003CF")]
    	protected virtual Texture2D EmptySlotThumbnail
    	{
    		[Token(Token = "0x6001AD9")]
    		[Address(RVA = "0x49D2A0", Offset = "0x49BCA0", VA = "0x18049D2A0", Slot = "81")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003D0")]
    	protected virtual string DateFormat
    	{
    		[Token(Token = "0x6001ADA")]
    		[Address(RVA = "0x5F6B60", Offset = "0x5F5560", VA = "0x1805F6B60", Slot = "82")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003D1")]
    	protected virtual string TitleTemplate
    	{
    		[Token(Token = "0x6001ADB")]
    		[Address(RVA = "0x5F7400", Offset = "0x5F5E00", VA = "0x1805F7400", Slot = "83")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6001ADC")]
    	[Address(RVA = "0x62FD30", Offset = "0x62E730", VA = "0x18062FD30", Slot = "84")]
    	public virtual void Initialize(Action<int> onClicked, Action<int> onDeleteClicked)
    	{
    	}

    	[Token(Token = "0x6001ADD")]
    	[Address(RVA = "0x6300C0", Offset = "0x62EAC0", VA = "0x1806300C0", Slot = "85")]
    	public virtual void Bind(int slotNumber, GameStateMap state)
    	{
    	}

    	[Token(Token = "0x6001ADE")]
    	[Address(RVA = "0x6301A0", Offset = "0x62EBA0", VA = "0x1806301A0", Slot = "69")]
    	public override void OnPointerEnter(PointerEventData eventData)
    	{
    	}

    	[Token(Token = "0x6001ADF")]
    	[Address(RVA = "0x6302B0", Offset = "0x62ECB0", VA = "0x1806302B0", Slot = "70")]
    	public override void OnPointerExit(PointerEventData eventData)
    	{
    	}

    	[Token(Token = "0x6001AE0")]
    	[Address(RVA = "0x6303C0", Offset = "0x62EDC0", VA = "0x1806303C0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001AE1")]
    	[Address(RVA = "0x6306B0", Offset = "0x62F0B0", VA = "0x1806306B0", Slot = "8")]
    	protected override void OnDestroy()
    	{
    	}

    	[Token(Token = "0x6001AE2")]
    	[Address(RVA = "0x630800", Offset = "0x62F200", VA = "0x180630800", Slot = "86")]
    	protected virtual void SetEmptyState(int slotNumber)
    	{
    	}

    	[Token(Token = "0x6001AE3")]
    	[Address(RVA = "0x630990", Offset = "0x62F390", VA = "0x180630990", Slot = "87")]
    	protected virtual void SetNonEmptyState(int slotNumber, GameStateMap state)
    	{
    	}

    	[Token(Token = "0x6001AE4")]
    	[Address(RVA = "0x630B80", Offset = "0x62F580", VA = "0x180630B80", Slot = "88")]
    	protected virtual void SetTitleText(string value)
    	{
    	}

    	[Token(Token = "0x6001AE5")]
    	[Address(RVA = "0x630BE0", Offset = "0x62F5E0", VA = "0x180630BE0", Slot = "62")]
    	protected override void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x6001AE6")]
    	[Address(RVA = "0x630C40", Offset = "0x62F640", VA = "0x180630C40", Slot = "89")]
    	protected virtual void HandleDeleteButtonClicked()
    	{
    	}

    	[Token(Token = "0x6001AE7")]
    	[Address(RVA = "0x630C90", Offset = "0x62F690", VA = "0x180630C90", Slot = "90")]
    	protected virtual void HandleDeleteInputActivated()
    	{
    	}

    	[Token(Token = "0x6001AE8")]
    	[Address(RVA = "0x630D70", Offset = "0x62F770", VA = "0x180630D70", Slot = "91")]
    	protected virtual void HandleLocaleChanged(LocaleChangedArgs _)
    	{
    	}

    	[Token(Token = "0x6001AE9")]
    	[Address(RVA = "0x630DD0", Offset = "0x62F7D0", VA = "0x180630DD0", Slot = "71")]
    	public override void OnSelect(BaseEventData eventData)
    	{
    	}

    	[Token(Token = "0x6001AEA")]
    	[Address(RVA = "0x630EB0", Offset = "0x62F8B0", VA = "0x180630EB0")]
    	public GameStateSlot()
    	{
    	}
    }
}
