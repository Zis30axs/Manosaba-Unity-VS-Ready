using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20002AA")]
public abstract class TransientSaveSlotManager : ISaveSlotManager
{
	[Token(Token = "0x170001B2")]
	public virtual bool Loading
	{
		[Token(Token = "0x6000C64")]
		[Address(RVA = "0x456370", Offset = "0x454D70", VA = "0x180456370", Slot = "26")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000C65")]
		[Address(RVA = "0x539F80", Offset = "0x538980", VA = "0x180539F80")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x170001B3")]
	public virtual bool Saving
	{
		[Token(Token = "0x6000C66")]
		[Address(RVA = "0x539F90", Offset = "0x538990", VA = "0x180539F90", Slot = "27")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000C67")]
		[Address(RVA = "0x539FA0", Offset = "0x5389A0", VA = "0x180539FA0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x14000038")]
	public event Action<string> OnBeforeSave
	{
		[Token(Token = "0x6000C54")]
		[Address(RVA = "0x53B1B0", Offset = "0x539BB0", VA = "0x18053B1B0", Slot = "4")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000C55")]
		[Address(RVA = "0x53B2C0", Offset = "0x539CC0", VA = "0x18053B2C0", Slot = "5")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x14000039")]
	public event Action<string> OnSaved
	{
		[Token(Token = "0x6000C56")]
		[Address(RVA = "0x53B3D0", Offset = "0x539DD0", VA = "0x18053B3D0", Slot = "6")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000C57")]
		[Address(RVA = "0x53B4E0", Offset = "0x539EE0", VA = "0x18053B4E0", Slot = "7")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400003A")]
	public event Action<string> OnBeforeLoad
	{
		[Token(Token = "0x6000C58")]
		[Address(RVA = "0x53B5F0", Offset = "0x539FF0", VA = "0x18053B5F0", Slot = "8")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000C59")]
		[Address(RVA = "0x53B700", Offset = "0x53A100", VA = "0x18053B700", Slot = "9")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400003B")]
	public event Action<string> OnLoaded
	{
		[Token(Token = "0x6000C5A")]
		[Address(RVA = "0x53B810", Offset = "0x53A210", VA = "0x18053B810", Slot = "10")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000C5B")]
		[Address(RVA = "0x53B920", Offset = "0x53A320", VA = "0x18053B920", Slot = "11")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400003C")]
	public event Action<string> OnBeforeDelete
	{
		[Token(Token = "0x6000C5C")]
		[Address(RVA = "0x53BA30", Offset = "0x53A430", VA = "0x18053BA30", Slot = "12")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000C5D")]
		[Address(RVA = "0x53BB40", Offset = "0x53A540", VA = "0x18053BB40", Slot = "13")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400003D")]
	public event Action<string> OnDeleted
	{
		[Token(Token = "0x6000C5E")]
		[Address(RVA = "0x53BC50", Offset = "0x53A650", VA = "0x18053BC50", Slot = "14")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000C5F")]
		[Address(RVA = "0x53BD60", Offset = "0x53A760", VA = "0x18053BD60", Slot = "15")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400003E")]
	public event Action<string, string> OnBeforeRename
	{
		[Token(Token = "0x6000C60")]
		[Address(RVA = "0x53BE70", Offset = "0x53A870", VA = "0x18053BE70", Slot = "16")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000C61")]
		[Address(RVA = "0x53BF80", Offset = "0x53A980", VA = "0x18053BF80", Slot = "17")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x1400003F")]
	public event Action<string, string> OnRenamed
	{
		[Token(Token = "0x6000C62")]
		[Address(RVA = "0x53C090", Offset = "0x53AA90", VA = "0x18053C090", Slot = "18")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6000C63")]
		[Address(RVA = "0x53C1A0", Offset = "0x53ABA0", VA = "0x18053C1A0", Slot = "19")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6000C68")]
	public abstract bool SaveSlotExists(string slotId);

	[Token(Token = "0x6000C69")]
	public abstract bool AnySaveExists();

	[Token(Token = "0x6000C6A")]
	public abstract void DeleteSaveSlot(string slotId);

	[Token(Token = "0x6000C6B")]
	public abstract void RenameSaveSlot(string sourceSlotId, string destSlotId);

	[Token(Token = "0x6000C6C")]
	[Address(RVA = "0x539FB0", Offset = "0x5389B0", VA = "0x180539FB0", Slot = "32")]
	protected virtual void InvokeOnBeforeSave(string slotId)
	{
	}

	[Token(Token = "0x6000C6D")]
	[Address(RVA = "0x539FD0", Offset = "0x5389D0", VA = "0x180539FD0", Slot = "33")]
	protected virtual void InvokeOnSaved(string slotId)
	{
	}

	[Token(Token = "0x6000C6E")]
	[Address(RVA = "0x539FF0", Offset = "0x5389F0", VA = "0x180539FF0", Slot = "34")]
	protected virtual void InvokeOnBeforeLoad(string slotId)
	{
	}

	[Token(Token = "0x6000C6F")]
	[Address(RVA = "0x53A010", Offset = "0x538A10", VA = "0x18053A010", Slot = "35")]
	protected virtual void InvokeOnLoaded(string slotId)
	{
	}

	[Token(Token = "0x6000C70")]
	[Address(RVA = "0x53A030", Offset = "0x538A30", VA = "0x18053A030", Slot = "36")]
	protected virtual void InvokeOnBeforeDelete(string slotId)
	{
	}

	[Token(Token = "0x6000C71")]
	[Address(RVA = "0x53A050", Offset = "0x538A50", VA = "0x18053A050", Slot = "37")]
	protected virtual void InvokeOnDeleted(string slotId)
	{
	}

	[Token(Token = "0x6000C72")]
	[Address(RVA = "0x53A070", Offset = "0x538A70", VA = "0x18053A070", Slot = "38")]
	protected virtual void InvokeOnBeforeRename(string sourceSlotId, string destSlotId)
	{
	}

	[Token(Token = "0x6000C73")]
	[Address(RVA = "0x53A090", Offset = "0x538A90", VA = "0x18053A090", Slot = "39")]
	protected virtual void InvokeOnRenamed(string sourceSlotId, string destSlotId)
	{
	}

	[Token(Token = "0x6000C74")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	protected TransientSaveSlotManager()
	{
	}
}
[Token(Token = "0x20002AB")]
public class TransientSaveSlotManager<TData> : TransientSaveSlotManager, ISaveSlotManager<TData>, ISaveSlotManager where TData : new()
{
	[Token(Token = "0x170001B4")]
	public static Func<TData> DefaultFactory
	{
		[Token(Token = "0x6000C75")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000C76")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170001B5")]
	public virtual IReadOnlyCollection<string> ExistingSlotIds
	{
		[Token(Token = "0x6000C77")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170001B6")]
	protected virtual Dictionary<string, TData> SlotToData
	{
		[Token(Token = "0x6000C78")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000C79")]
	public virtual void SaveBlocking(string slotId, TData data)
	{
	}

	[Token(Token = "0x6000C7A")]
	public virtual TData LoadBlocking(string slotId)
	{
		return default(TData);
	}

	[Token(Token = "0x6000C7B")]
	public virtual TData LoadOrDefaultBlocking(string slotId)
	{
		return default(TData);
	}

	[Token(Token = "0x6000C7C")]
	public virtual UniTask Save(string slotId, TData data)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000C7D")]
	public virtual UniTask<TData> Load(string slotId)
	{
		return default(UniTask<TData>);
	}

	[Token(Token = "0x6000C7E")]
	public virtual UniTask<TData> LoadOrDefault(string slotId)
	{
		return default(UniTask<TData>);
	}

	[Token(Token = "0x6000C7F")]
	public override bool SaveSlotExists(string slotId)
	{
		return default(bool);
	}

	[Token(Token = "0x6000C80")]
	public override bool AnySaveExists()
	{
		return default(bool);
	}

	[Token(Token = "0x6000C81")]
	public override void DeleteSaveSlot(string slotId)
	{
	}

	[Token(Token = "0x6000C82")]
	public override void RenameSaveSlot(string sourceSlotId, string destSlotId)
	{
	}

	[Token(Token = "0x6000C83")]
	public TransientSaveSlotManager()
	{
	}
}
