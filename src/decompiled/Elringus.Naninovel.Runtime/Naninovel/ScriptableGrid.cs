using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel;

[Token(Token = "0x20002B4")]
[RequireComponent(typeof(GridLayoutGroup))]
public abstract class ScriptableGrid<TSlot> : ScriptableUIComponent<GridLayoutGroup> where TSlot : ScriptableGridSlot
{
	[StructLayout(3)]
	[Token(Token = "0x20002B5")]
	[CompilerGenerated]
	private struct _003CCreateSlot_003Ed__42
{
		[Token(Token = "0x4000A8D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000A8E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000A8F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public TSlot[] slots;

		[Token(Token = "0x4000A90")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int idx;

		[Token(Token = "0x4000A91")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ScriptableGrid<TSlot> _003C_003E4__this;

		[Token(Token = "0x4000A92")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private TSlot[] _003C_003E7__wrap1;

		[Token(Token = "0x4000A93")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private int _003C_003E7__wrap2;

		[Token(Token = "0x4000A94")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TSlot>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000CC6")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000CC7")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20002B6")]
	[CompilerGenerated]
	private struct _003CCreateSlots_003Ed__41
{
		[Token(Token = "0x4000A95")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000A96")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder<TSlot[]> _003C_003Et__builder;

		[Token(Token = "0x4000A97")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ScriptableGrid<TSlot> _003C_003E4__this;

		[Token(Token = "0x4000A98")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private PooledObject<List<UniTask>> _003C__003E5__2;

		[Token(Token = "0x4000A99")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private TSlot[] _003Cslots_003E5__3;

		[Token(Token = "0x4000A9A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000CC8")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000CC9")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20002B7")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__30
{
		[Token(Token = "0x4000A9B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000A9C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000A9D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public ScriptableGrid<TSlot> _003C_003E4__this;

		[Token(Token = "0x4000A9E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int itemsCount;

		[Token(Token = "0x4000A9F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private UniTask<TSlot[]>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000CCA")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000CCB")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000A87")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[Tooltip("Prefab representing grid slot.")]
	[SerializeField]
	private TSlot slotPrototype;

	[Token(Token = "0x4000A88")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[Tooltip("How many slots should be visible per page.")]
	[Range(1f, 99f)]
	[SerializeField]
	private int itemsPerPage;

	[Token(Token = "0x4000A89")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[Tooltip("Container for the page number controls (optional). Will be disabled when grid has only one page.")]
	[SerializeField]
	private GameObject paginationPanel;

	[Token(Token = "0x4000A8A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[Tooltip("Button inside pagination panel to select next grid page.")]
	[SerializeField]
	private Button previousPageButton;

	[Token(Token = "0x4000A8B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[Tooltip("Button inside pagination panel to select previous grid page.")]
	[SerializeField]
	private Button nextPageButton;

	[Token(Token = "0x4000A8C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	[Tooltip("Event invoked when grid page number changes.")]
	[SerializeField]
	private OnGridPageChangedEvent onPageChanged;

	[Token(Token = "0x170001BB")]
	public virtual int ItemsCount
	{
		[Token(Token = "0x6000CAC")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
		[Token(Token = "0x6000CAD")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x170001BC")]
	public virtual int ItemsPerPage
	{
		[Token(Token = "0x6000CAE")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x170001BD")]
	public virtual int CurrentPage
	{
		[Token(Token = "0x6000CAF")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
		[Token(Token = "0x6000CB0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x170001BE")]
	public virtual int PageCount
	{
		[Token(Token = "0x6000CB1")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x170001BF")]
	public virtual IReadOnlyList<TSlot> Slots
	{
		[Token(Token = "0x6000CB2")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000CB3")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x170001C0")]
	protected virtual TSlot SlotPrototype
	{
		[Token(Token = "0x6000CB4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170001C1")]
	protected virtual GameObject PaginationPanel
	{
		[Token(Token = "0x6000CB5")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170001C2")]
	protected virtual Button PreviousPageButton
	{
		[Token(Token = "0x6000CB6")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170001C3")]
	protected virtual Button NextPageButton
	{
		[Token(Token = "0x6000CB7")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000CB8")]
	[AsyncStateMachine(typeof(ScriptableGrid<>._003CInitialize_003Ed__30))]
	public virtual UniTask Initialize(int itemsCount)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000CB9")]
	public virtual void SelectPage(int pageNumber)
	{
	}

	[Token(Token = "0x6000CBA")]
	public virtual void SelectNextPage()
	{
	}

	[Token(Token = "0x6000CBB")]
	public virtual void SelectPreviousPage()
	{
	}

	[Token(Token = "0x6000CBC")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6000CBD")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x6000CBE")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x6000CBF")]
	protected virtual void InitializeSlot(TSlot slot)
	{
	}

	[Token(Token = "0x6000CC0")]
	protected abstract void BindSlot(TSlot slot, int itemIndex);

	[Token(Token = "0x6000CC1")]
	protected virtual void Paginate()
	{
	}

	[Token(Token = "0x6000CC2")]
	private void PaginateSlot(int slotIndex)
	{
	}

	[Token(Token = "0x6000CC3")]
	[AsyncStateMachine(typeof(ScriptableGrid<>._003CCreateSlots_003Ed__41))]
	private UniTask<TSlot[]> CreateSlots()
	{
		return default(UniTask<TSlot[]>);
	}

	[Token(Token = "0x6000CC4")]
	[AsyncStateMachine(typeof(ScriptableGrid<>._003CCreateSlot_003Ed__42))]
	private UniTask CreateSlot(TSlot[] slots, int idx)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000CC5")]
	protected ScriptableGrid()
	{
	}
}
