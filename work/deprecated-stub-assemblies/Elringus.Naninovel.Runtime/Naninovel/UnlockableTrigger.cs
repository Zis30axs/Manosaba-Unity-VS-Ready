using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;

namespace Naninovel
{
    [Token(Token = "0x20004E3")]
    public class UnlockableTrigger : MonoBehaviour
    {
    	[Serializable]
    	[Token(Token = "0x20004E4")]
    	private class UnlockedStateChangedEvent : UnityEvent<bool>
    	{
    		[Token(Token = "0x600187D")]
    		[Address(RVA = "0x6082B0", Offset = "0x606CB0", VA = "0x1806082B0")]
    		public UnlockedStateChangedEvent()
    		{
    		}
    	}

    	[Token(Token = "0x40012A1")]
    	[FieldOffset(Offset = "0x28")]
    	[Tooltip("ID of the unlockable item to listen for.")]
    	[SerializeField]
    	private string unlockableItemId;

    	[Token(Token = "0x40012A2")]
    	[FieldOffset(Offset = "0x30")]
    	[Tooltip("Invoked when unlocked state of the listened unlockable item is changed; also invoked when the component is started.")]
    	[SerializeField]
    	private UnlockedStateChangedEvent onUnlockedStateChanged;

    	[Token(Token = "0x40012A3")]
    	[FieldOffset(Offset = "0x38")]
    	[Tooltip("Invoked when the item is unlocked.")]
    	[SerializeField]
    	private UnityEvent onUnlocked;

    	[Token(Token = "0x40012A4")]
    	[FieldOffset(Offset = "0x40")]
    	[Tooltip("Invoked when the item is locked.")]
    	[SerializeField]
    	private UnityEvent onLocked;

    	[Token(Token = "0x17000379")]
    	public virtual string UnlockableItemId
    	{
    		[Token(Token = "0x6001874")]
    		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060", Slot = "4")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001875")]
    		[Address(RVA = "0x486D40", Offset = "0x485740", VA = "0x180486D40", Slot = "5")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700037A")]
    	protected IUnlockableManager UnlockableManager
    	{
    		[Token(Token = "0x6001876")]
    		[Address(RVA = "0x607C50", Offset = "0x606650", VA = "0x180607C50")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1400008A")]
    	public event Action<bool> OnUnlockedStateChanged
    	{
    		[Token(Token = "0x6001872")]
    		[Address(RVA = "0x607A30", Offset = "0x606430", VA = "0x180607A30")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6001873")]
    		[Address(RVA = "0x607B40", Offset = "0x606540", VA = "0x180607B40")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x6001877")]
    	[Address(RVA = "0x607CB0", Offset = "0x6066B0", VA = "0x180607CB0", Slot = "6")]
    	protected virtual void OnEnable()
    	{
    	}

    	[Token(Token = "0x6001878")]
    	[Address(RVA = "0x607D70", Offset = "0x606770", VA = "0x180607D70", Slot = "7")]
    	protected virtual void OnDisable()
    	{
    	}

    	[Token(Token = "0x6001879")]
    	[Address(RVA = "0x607F30", Offset = "0x606930", VA = "0x180607F30", Slot = "8")]
    	protected virtual void Initialize()
    	{
    	}

    	[Token(Token = "0x600187A")]
    	[Address(RVA = "0x608160", Offset = "0x606B60", VA = "0x180608160", Slot = "9")]
    	protected virtual void HandleItemUpdated(UnlockableItemUpdatedArgs args)
    	{
    	}

    	[Token(Token = "0x600187B")]
    	[Address(RVA = "0x608200", Offset = "0x606C00", VA = "0x180608200")]
    	private void InvokeEvents(bool unlocked)
    	{
    	}

    	[Token(Token = "0x600187C")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public UnlockableTrigger()
    	{
    	}
    }
}
