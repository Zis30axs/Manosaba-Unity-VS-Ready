using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.ManagedText;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI;

[Token(Token = "0x20005BA")]
[RequireComponent(typeof(CanvasGroup))]
public class TipsPanel : CustomUI, ITipsUI, IManagedUI
{
	[StructLayout(3)]
	[Token(Token = "0x20005BD")]
	[CompilerGenerated]
	private struct _003CChangeVisibility_003Ed__54
{
		[Token(Token = "0x4001605")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001606")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001607")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public bool visible;

		[Token(Token = "0x4001608")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public TipsPanel _003C_003E4__this;

		[Token(Token = "0x4001609")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float? duration;

		[Token(Token = "0x400160A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken token;

		[Token(Token = "0x400160B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private InteractionBlocker _003C_003E7__wrap1;

		[Token(Token = "0x400160C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001CD6")]
		[Address(RVA = "0x4A6D60", Offset = "0x4A5760", VA = "0x1804A6D60", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001CD7")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20005BE")]
	[CompilerGenerated]
	private struct _003CFillListItem_003Ed__42
{
		[Token(Token = "0x400160D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400160E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400160F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public TipsPanel _003C_003E4__this;

		[Token(Token = "0x4001610")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public ManagedTextRecord record;

		[Token(Token = "0x4001611")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<TipsListItem>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001CD8")]
		[Address(RVA = "0x4A7550", Offset = "0x4A5F50", VA = "0x1804A7550", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001CD9")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20005BF")]
	[CompilerGenerated]
	private struct _003CFillListItems_003Ed__41
{
		[Token(Token = "0x4001612")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001613")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001614")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public TipsPanel _003C_003E4__this;

		[Token(Token = "0x4001615")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private PooledObject<List<UniTask>> _003C__003E5__2;

		[Token(Token = "0x4001616")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001CDA")]
		[Address(RVA = "0x4A7B90", Offset = "0x4A6590", VA = "0x1804A7B90", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001CDB")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20005C0")]
	[CompilerGenerated]
	private struct _003CHandleLocaleChanged_003Ed__53
{
		[Token(Token = "0x4001617")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001618")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001619")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public TipsPanel _003C_003E4__this;

		[Token(Token = "0x400161A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001CDC")]
		[Address(RVA = "0x4A85C0", Offset = "0x4A6FC0", VA = "0x1804A85C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001CDD")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20005C1")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__35
{
		[Token(Token = "0x400161B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400161C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400161D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public TipsPanel _003C_003E4__this;

		[Token(Token = "0x400161E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001CDE")]
		[Address(RVA = "0x4A88A0", Offset = "0x4A72A0", VA = "0x1804A88A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001CDF")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40015F2")]
	public const string DefaultUnlockableIdPrefix = "Tips";

	[Token(Token = "0x40015F5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	[Header("Tips Setup")]
	[Tooltip("All the unlockable item IDs with the specified prefix will be considered Tips items.")]
	[SerializeField]
	private string unlockableIdPrefix;

	[Token(Token = "0x40015F6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	[Tooltip("Text character to separate title, category and tip text in managed text record.")]
	[SerializeField]
	private string separatorLiteral;

	[Token(Token = "0x40015F7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
	[Tooltip("Prefix to add to unlockable ID to indicate that it was selected (seen) at least once.")]
	[SerializeField]
	private string selectedPrefix;

	[Token(Token = "0x40015F8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
	[Header("UI Setup")]
	[SerializeField]
	private ScrollRect itemsScrollRect;

	[Token(Token = "0x40015F9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
	[SerializeField]
	private RectTransform itemsContainer;

	[Token(Token = "0x40015FA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x118")]
	[SerializeField]
	private TipsListItem itemPrefab;

	[Token(Token = "0x40015FB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x120")]
	[SerializeField]
	private StringUnityEvent onTitleChanged;

	[Token(Token = "0x40015FC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x128")]
	[SerializeField]
	private StringUnityEvent onNumberChanged;

	[Token(Token = "0x40015FD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x130")]
	[SerializeField]
	private StringUnityEvent onCategoryChanged;

	[Token(Token = "0x40015FE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x138")]
	[SerializeField]
	private StringUnityEvent onDescriptionChanged;

	[Token(Token = "0x40015FF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x140")]
	private readonly List<TipsListItem> listItems;

	[Token(Token = "0x4001600")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x148")]
	private IUnlockableManager unlockables;

	[Token(Token = "0x4001601")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x150")]
	private ILocalizationManager l10n;

	[Token(Token = "0x4001602")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x158")]
	private ITextManager docs;

	[Token(Token = "0x17000436")]
	public virtual int TipsCount
	{
		[Token(Token = "0x6001CA7")]
		[Address(RVA = "0x4A37B0", Offset = "0x4A21B0", VA = "0x1804A37B0", Slot = "97")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
		[Token(Token = "0x6001CA8")]
		[Address(RVA = "0x4A37C0", Offset = "0x4A21C0", VA = "0x1804A37C0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000437")]
	protected virtual string UnlockableIdPrefix
	{
		[Token(Token = "0x6001CA9")]
		[Address(RVA = "0x49D2B0", Offset = "0x49BCB0", VA = "0x18049D2B0", Slot = "98")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000438")]
	protected virtual RectTransform ItemsContainer
	{
		[Token(Token = "0x6001CAA")]
		[Address(RVA = "0x4A37D0", Offset = "0x4A21D0", VA = "0x1804A37D0", Slot = "99")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000439")]
	protected virtual TipsListItem ItemPrefab
	{
		[Token(Token = "0x6001CAB")]
		[Address(RVA = "0x34DB60", Offset = "0x34C560", VA = "0x18034DB60", Slot = "100")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700043A")]
	protected virtual string SeparatorLiteral
	{
		[Token(Token = "0x6001CAC")]
		[Address(RVA = "0x49D250", Offset = "0x49BC50", VA = "0x18049D250", Slot = "101")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700043B")]
	protected virtual string SelectedPrefix
	{
		[Token(Token = "0x6001CAD")]
		[Address(RVA = "0x37A040", Offset = "0x378A40", VA = "0x18037A040", Slot = "102")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700043C")]
	protected virtual IReadOnlyList<TipsListItem> ListItems
	{
		[Token(Token = "0x6001CAE")]
		[Address(RVA = "0x493370", Offset = "0x491D70", VA = "0x180493370", Slot = "103")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700043D")]
	protected virtual string SelectedItemId
	{
		[Token(Token = "0x6001CAF")]
		[Address(RVA = "0x49D2A0", Offset = "0x49BCA0", VA = "0x18049D2A0", Slot = "104")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6001CB0")]
		[Address(RVA = "0x4A37E0", Offset = "0x4A21E0", VA = "0x1804A37E0", Slot = "105")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x6001CB1")]
	[Address(RVA = "0x4A3840", Offset = "0x4A2240", VA = "0x1804A3840", Slot = "83")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__35))]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001CB2")]
	[Address(RVA = "0x4A3960", Offset = "0x4A2360", VA = "0x1804A3960", Slot = "106")]
	public virtual void SelectTipRecord(string tipId)
	{
	}

	[Token(Token = "0x6001CB3")]
	[Address(RVA = "0x4A3BA0", Offset = "0x4A25A0", VA = "0x1804A3BA0", Slot = "107")]
	public virtual bool HasUnselectedItem()
	{
		return default(bool);
	}

	[Token(Token = "0x6001CB4")]
	[Address(RVA = "0x4A3D70", Offset = "0x4A2770", VA = "0x1804A3D70", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001CB5")]
	[Address(RVA = "0x4A4080", Offset = "0x4A2A80", VA = "0x1804A4080", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x6001CB6")]
	[Address(RVA = "0x4A4190", Offset = "0x4A2B90", VA = "0x1804A4190", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x6001CB7")]
	[Address(RVA = "0x4A42A0", Offset = "0x4A2CA0", VA = "0x1804A42A0", Slot = "108")]
	[AsyncStateMachine(typeof(_003CFillListItems_003Ed__41))]
	protected virtual UniTask FillListItems()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001CB8")]
	[Address(RVA = "0x4A43C0", Offset = "0x4A2DC0", VA = "0x1804A43C0", Slot = "109")]
	[AsyncStateMachine(typeof(_003CFillListItem_003Ed__42))]
	protected virtual UniTask FillListItem(ManagedTextRecord record)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001CB9")]
	[Address(RVA = "0x4A4560", Offset = "0x4A2F60", VA = "0x1804A4560", Slot = "110")]
	protected virtual void ClearListItems()
	{
	}

	[Token(Token = "0x6001CBA")]
	[Address(RVA = "0x4A4890", Offset = "0x4A3290", VA = "0x1804A4890", Slot = "111")]
	protected virtual void ClearSelection()
	{
	}

	[Token(Token = "0x6001CBB")]
	[Address(RVA = "0x4A4A50", Offset = "0x4A3450", VA = "0x1804A4A50", Slot = "112")]
	protected virtual void SelectItem(TipsListItem item)
	{
	}

	[Token(Token = "0x6001CBC")]
	[Address(RVA = "0x4A56A0", Offset = "0x4A40A0", VA = "0x1804A56A0", Slot = "113")]
	protected virtual void HandleUnlockableItemUpdated(UnlockableItemUpdatedArgs args)
	{
	}

	[Token(Token = "0x6001CBD")]
	[Address(RVA = "0x4A59B0", Offset = "0x4A43B0", VA = "0x1804A59B0", Slot = "114")]
	protected virtual void SetTitle(string value)
	{
	}

	[Token(Token = "0x6001CBE")]
	[Address(RVA = "0x4A5A10", Offset = "0x4A4410", VA = "0x1804A5A10", Slot = "115")]
	protected virtual void SetNumber(string value)
	{
	}

	[Token(Token = "0x6001CBF")]
	[Address(RVA = "0x4A5A70", Offset = "0x4A4470", VA = "0x1804A5A70", Slot = "116")]
	protected virtual void SetCategory(string value)
	{
	}

	[Token(Token = "0x6001CC0")]
	[Address(RVA = "0x4A5AD0", Offset = "0x4A44D0", VA = "0x1804A5AD0", Slot = "117")]
	protected virtual void SetDescription(string value)
	{
	}

	[Token(Token = "0x6001CC1")]
	[Address(RVA = "0x4A5B30", Offset = "0x4A4530", VA = "0x1804A5B30", Slot = "118")]
	protected virtual bool WasItemSelectedOnce(string unlockableId)
	{
		return default(bool);
	}

	[Token(Token = "0x6001CC2")]
	[Address(RVA = "0x4A5BD0", Offset = "0x4A45D0", VA = "0x1804A5BD0", Slot = "119")]
	protected virtual void SetItemSelectedOnce(string unlockableId)
	{
	}

	[Token(Token = "0x6001CC3")]
	[Address(RVA = "0x4A5CD0", Offset = "0x4A46D0", VA = "0x1804A5CD0", Slot = "120")]
	[AsyncStateMachine(typeof(_003CHandleLocaleChanged_003Ed__53))]
	protected virtual UniTask HandleLocaleChanged(LocaleChangedArgs _)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001CC4")]
	[Address(RVA = "0x4A5DF0", Offset = "0x4A47F0", VA = "0x1804A5DF0", Slot = "44")]
	[AsyncStateMachine(typeof(_003CChangeVisibility_003Ed__54))]
	public override UniTask ChangeVisibility(bool visible, [Optional] float? duration, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001CC5")]
	[Address(RVA = "0x4A5FB0", Offset = "0x4A49B0", VA = "0x1804A5FB0", Slot = "55")]
	protected override void HandleVisibilityChanged(bool visible)
	{
	}

	[Token(Token = "0x6001CC6")]
	[Address(RVA = "0x4A6030", Offset = "0x4A4A30", VA = "0x1804A6030", Slot = "121")]
	protected virtual TipsListItem FindSelectedItem()
	{
		return null;
	}

	[Token(Token = "0x6001CC7")]
	[Address(RVA = "0x4A60E0", Offset = "0x4A4AE0", VA = "0x1804A60E0", Slot = "122")]
	protected virtual TipsListItem FindFirstUnlockedItem()
	{
		return null;
	}

	[Token(Token = "0x6001CC8")]
	[Address(RVA = "0x4A6190", Offset = "0x4A4B90", VA = "0x1804A6190", Slot = "123")]
	protected virtual TipsListItem FindLastUnlockedItem()
	{
		return null;
	}

	[Token(Token = "0x6001CC9")]
	[Address(RVA = "0x4A6240", Offset = "0x4A4C40", VA = "0x1804A6240", Slot = "124")]
	protected virtual void HandleNavigationInput(float value)
	{
	}

	[Token(Token = "0x6001CCA")]
	[Address(RVA = "0x4A62C0", Offset = "0x4A4CC0", VA = "0x1804A62C0", Slot = "125")]
	protected virtual void SelectPreviousUnlockedItem()
	{
	}

	[Token(Token = "0x6001CCB")]
	[Address(RVA = "0x4A6470", Offset = "0x4A4E70", VA = "0x1804A6470", Slot = "126")]
	protected virtual void SelectNextUnlockedItem()
	{
	}

	[Token(Token = "0x6001CCC")]
	[Address(RVA = "0x4A6650", Offset = "0x4A5050", VA = "0x1804A6650", Slot = "127")]
	protected virtual string GetRecordValueOrDefault(string key, string documentPath)
	{
		return null;
	}

	[Token(Token = "0x6001CCD")]
	[Address(RVA = "0x4A67D0", Offset = "0x4A51D0", VA = "0x1804A67D0")]
	public TipsPanel()
	{
	}
}
