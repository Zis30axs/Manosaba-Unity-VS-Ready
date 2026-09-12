using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using TMPro;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20004D0")]
[InitializeAtRuntime(0, null)]
public class UIManager : IUIManager, IEngineService<UIConfiguration>, IEngineService, IStatefulService<SettingsStateMap>
{
	[Serializable]
	[Token(Token = "0x20004D1")]
	public class Settings
	{
		[Token(Token = "0x4001260")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public string FontName;

		[Token(Token = "0x4001261")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public int FontSize;

		[Token(Token = "0x6001830")]
		[Address(RVA = "0x601AF0", Offset = "0x6004F0", VA = "0x180601AF0")]
		public Settings()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20004D4")]
	[CompilerGenerated]
	private struct _003CAddUI_003Ed__41
{
		[Token(Token = "0x400126B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400126C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<IManagedUI> _003C_003Et__builder;

		[Token(Token = "0x400126D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public UIManager _003C_003E4__this;

		[Token(Token = "0x400126E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public GameObject prefab;

		[Token(Token = "0x400126F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string name;

		[Token(Token = "0x4001270")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public string group;

		[Token(Token = "0x4001271")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private IManagedUI _003CuiComponent_003E5__2;

		[Token(Token = "0x4001272")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask<IManagedUI>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4001273")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6001838")]
		[Address(RVA = "0x602120", Offset = "0x600B20", VA = "0x180602120", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001839")]
		[Address(RVA = "0x602690", Offset = "0x601090", VA = "0x180602690", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20004D5")]
	[CompilerGenerated]
	private struct _003CInitializeFonts_003Ed__68
{
		[Token(Token = "0x4001274")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001275")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<IEnumerable<(string Name, TMP_FontAsset Asset)>> _003C_003Et__builder;

		[Token(Token = "0x4001276")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public UIManager _003C_003E4__this;

		[Token(Token = "0x4001277")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<TMP_FontAsset>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4001278")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<(string Name, TMP_FontAsset Asset)[]>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x600183A")]
		[Address(RVA = "0x6026C0", Offset = "0x6010C0", VA = "0x1806026C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600183B")]
		[Address(RVA = "0x602DC0", Offset = "0x6017C0", VA = "0x180602DC0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20004D6")]
	[CompilerGenerated]
	private struct _003CInitializeService_003Ed__36
{
		[Token(Token = "0x4001279")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400127A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400127B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public UIManager _003C_003E4__this;

		[Token(Token = "0x400127C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask<IEnumerable<(string Name, TMP_FontAsset Asset)>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600183C")]
		[Address(RVA = "0x602DF0", Offset = "0x6017F0", VA = "0x180602DF0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600183D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20004D7")]
	[CompilerGenerated]
	private struct _003CInstantiatePrefab_003Ed__58
{
		[Token(Token = "0x400127D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400127E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<IManagedUI> _003C_003Et__builder;

		[Token(Token = "0x400127F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public UIManager _003C_003E4__this;

		[Token(Token = "0x4001280")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string group;

		[Token(Token = "0x4001281")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public GameObject prefab;

		[Token(Token = "0x4001282")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public string name;

		[Token(Token = "0x4001283")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask<GameObject>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600183E")]
		[Address(RVA = "0x603700", Offset = "0x602100", VA = "0x180603700", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600183F")]
		[Address(RVA = "0x603F00", Offset = "0x602900", VA = "0x180603F00", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20004D8")]
	[CompilerGenerated]
	private struct _003CInstantiateUIs_003Ed__63
{
		[Token(Token = "0x4001284")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001285")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001286")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public UIManager _003C_003E4__this;

		[Token(Token = "0x4001287")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask<IReadOnlyCollection<Resource<GameObject>>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4001288")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<IManagedUI[]>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x4001289")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__3;

		[Token(Token = "0x6001840")]
		[Address(RVA = "0x603F30", Offset = "0x602930", VA = "0x180603F30", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001841")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400124F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private readonly List<ManagedUI> managedUIs;

	[Token(Token = "0x4001250")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private readonly Dictionary<Type, IManagedUI> cachedGetUIResults;

	[Token(Token = "0x4001251")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private readonly Dictionary<string, TMP_FontAsset> fontNameToAsset;

	[Token(Token = "0x4001252")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private readonly List<IManagedUI> modalUIs;

	[Token(Token = "0x4001253")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private readonly ICameraManager camera;

	[Token(Token = "0x4001254")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	private readonly IInputManager input;

	[Token(Token = "0x4001255")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private readonly IResourceProviderManager resources;

	[Token(Token = "0x4001256")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private readonly ILocalizationManager l10n;

	[Token(Token = "0x4001257")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private readonly ICommunityLocalization communityL10n;

	[Token(Token = "0x4001258")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	private LocalizableResourceLoader<GameObject> uiLoader;

	[Token(Token = "0x4001259")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	private LocalizableResourceLoader<TMP_FontAsset> fontLoader;

	[Token(Token = "0x400125A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	private GameObject container;

	[Token(Token = "0x400125B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private GameObject modalContainer;

	[Token(Token = "0x400125C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	private CanvasGroup containerGroup;

	[Token(Token = "0x400125D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	private IInputSampler toggleUIInput;

	[Token(Token = "0x400125E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
	private string fontName;

	[Token(Token = "0x400125F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
	private int fontSize;

	[Token(Token = "0x17000372")]
	public virtual UIConfiguration Configuration
	{
		[Token(Token = "0x6001806")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "32")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000373")]
	public virtual string FontName
	{
		[Token(Token = "0x6001807")]
		[Address(RVA = "0x33B470", Offset = "0x339E70", VA = "0x18033B470", Slot = "33")]
		get
		{
			return null;
		}
		[Token(Token = "0x6001808")]
		[Address(RVA = "0x5FD4D0", Offset = "0x5FBED0", VA = "0x1805FD4D0", Slot = "34")]
		set
		{
		}
	}

	[Token(Token = "0x17000374")]
	public virtual int FontSize
	{
		[Token(Token = "0x6001809")]
		[Address(RVA = "0x5FD4F0", Offset = "0x5FBEF0", VA = "0x1805FD4F0", Slot = "35")]
		get
		{
			return default(int);
		}
		[Token(Token = "0x600180A")]
		[Address(RVA = "0x5FD500", Offset = "0x5FBF00", VA = "0x1805FD500", Slot = "36")]
		set
		{
		}
	}

	[Token(Token = "0x17000375")]
	public virtual bool AnyModal
	{
		[Token(Token = "0x600180B")]
		[Address(RVA = "0x5FD520", Offset = "0x5FBF20", VA = "0x1805FD520", Slot = "37")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x14000086")]
	public event Action<string> OnFontNameChanged
	{
		[Token(Token = "0x6001802")]
		[Address(RVA = "0x5FD090", Offset = "0x5FBA90", VA = "0x1805FD090", Slot = "4")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6001803")]
		[Address(RVA = "0x5FD1A0", Offset = "0x5FBBA0", VA = "0x1805FD1A0", Slot = "5")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x14000087")]
	public event Action<int> OnFontSizeChanged
	{
		[Token(Token = "0x6001804")]
		[Address(RVA = "0x5FD2B0", Offset = "0x5FBCB0", VA = "0x1805FD2B0", Slot = "6")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x6001805")]
		[Address(RVA = "0x5FD3C0", Offset = "0x5FBDC0", VA = "0x1805FD3C0", Slot = "7")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x600180C")]
	[Address(RVA = "0x5FD570", Offset = "0x5FBF70", VA = "0x1805FD570")]
	public UIManager(UIConfiguration config = default, IResourceProviderManager resources = default, ILocalizationManager l10n = default, ICommunityLocalization communityL10n = default, ICameraManager camera = default, IInputManager input = default)
	{
	}

	[Token(Token = "0x600180D")]
	[Address(RVA = "0x5FDA80", Offset = "0x5FC480", VA = "0x1805FDA80", Slot = "38")]
	[AsyncStateMachine(typeof(_003CInitializeService_003Ed__36))]
	public virtual UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x600180E")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "39")]
	public virtual void ResetService()
	{
	}

	[Token(Token = "0x600180F")]
	[Address(RVA = "0x5FDBA0", Offset = "0x5FC5A0", VA = "0x1805FDBA0", Slot = "40")]
	public virtual void DestroyService()
	{
	}

	[Token(Token = "0x6001810")]
	[Address(RVA = "0x5FDFA0", Offset = "0x5FC9A0", VA = "0x1805FDFA0", Slot = "41")]
	public virtual void SaveServiceState(SettingsStateMap stateMap)
	{
	}

	[Token(Token = "0x6001811")]
	[Address(RVA = "0x5FE110", Offset = "0x5FCB10", VA = "0x1805FE110", Slot = "42")]
	public virtual UniTask LoadServiceState(SettingsStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001812")]
	[Address(RVA = "0x5FE280", Offset = "0x5FCC80", VA = "0x1805FE280", Slot = "43")]
	[AsyncStateMachine(typeof(_003CAddUI_003Ed__41))]
	public virtual UniTask<IManagedUI> AddUI(GameObject prefab, [Optional] string name, [Optional] string group)
	{
		return default(UniTask<IManagedUI>);
	}

	[Token(Token = "0x6001813")]
	[Address(RVA = "0x5FE520", Offset = "0x5FCF20", VA = "0x1805FE520", Slot = "44")]
	public virtual void GetManagedUIs(ICollection<IManagedUI> managedUIs)
	{
	}

	[Token(Token = "0x6001814")]
	public bool HasUI<T>() where T : class, IManagedUI
	{
		return default(bool);
	}

	[Token(Token = "0x6001815")]
	[Address(RVA = "0x5FE6F0", Offset = "0x5FD0F0", VA = "0x1805FE6F0", Slot = "16")]
	public bool HasUI(string name)
	{
		return default(bool);
	}

	[Token(Token = "0x6001816")]
	public virtual T GetUI<T>() where T : class, IManagedUI
	{
		return null;
	}

	[Token(Token = "0x6001817")]
	[Address(RVA = "0x5FE860", Offset = "0x5FD260", VA = "0x1805FE860", Slot = "46")]
	public virtual IManagedUI GetUI(Type type)
	{
		return null;
	}

	[Token(Token = "0x6001818")]
	[Address(RVA = "0x5FEA70", Offset = "0x5FD470", VA = "0x1805FEA70", Slot = "47")]
	public virtual IManagedUI GetUI(string name)
	{
		return null;
	}

	[Token(Token = "0x6001819")]
	[Address(RVA = "0x5FEBF0", Offset = "0x5FD5F0", VA = "0x1805FEBF0", Slot = "48")]
	public virtual bool RemoveUI(IManagedUI managedUI)
	{
		return default(bool);
	}

	[Token(Token = "0x600181A")]
	[Address(RVA = "0x5FEF00", Offset = "0x5FD900", VA = "0x1805FEF00", Slot = "49")]
	public virtual void SetUIVisibleWithToggle(bool visible, bool allowToggle = true)
	{
	}

	[Token(Token = "0x600181B")]
	[Address(RVA = "0x5FF0E0", Offset = "0x5FDAE0", VA = "0x1805FF0E0", Slot = "50")]
	public virtual void AddModalUI(IManagedUI managedUI)
	{
	}

	[Token(Token = "0x600181C")]
	[Address(RVA = "0x5FF400", Offset = "0x5FDE00", VA = "0x1805FF400", Slot = "51")]
	public virtual void RemoveModalUI(IManagedUI managedUI)
	{
	}

	[Token(Token = "0x600181D")]
	[Address(RVA = "0x5FF5E0", Offset = "0x5FDFE0", VA = "0x1805FF5E0", Slot = "52")]
	public virtual void GetModalUIs(ICollection<IManagedUI> modalUIs)
	{
	}

	[Token(Token = "0x600181E")]
	[Address(RVA = "0x5FF790", Offset = "0x5FE190", VA = "0x1805FF790", Slot = "53")]
	public virtual bool IsActiveModalUI(IManagedUI managedUI)
	{
		return default(bool);
	}

	[Token(Token = "0x600181F")]
	[Address(RVA = "0x5FF950", Offset = "0x5FE350", VA = "0x1805FF950", Slot = "25")]
	public TMP_FontAsset GetFontAsset(string fontName)
	{
		return null;
	}

	[Token(Token = "0x6001820")]
	[Address(RVA = "0x5FFA30", Offset = "0x5FE430", VA = "0x1805FFA30", Slot = "54")]
	protected virtual bool HasFontAsset(string fontName)
	{
		return default(bool);
	}

	[Token(Token = "0x6001821")]
	[Address(RVA = "0x5FFA90", Offset = "0x5FE490", VA = "0x1805FFA90", Slot = "55")]
	protected virtual bool IsModal(IManagedUI managedUI)
	{
		return default(bool);
	}

	[Token(Token = "0x6001822")]
	[Address(RVA = "0x5FFAF0", Offset = "0x5FE4F0", VA = "0x1805FFAF0", Slot = "56")]
	protected virtual bool ShouldYieldModal(IManagedUI existingModal, IManagedUI newModal)
	{
		return default(bool);
	}

	[Token(Token = "0x6001823")]
	[Address(RVA = "0x5FFC70", Offset = "0x5FE670", VA = "0x1805FFC70", Slot = "57")]
	[AsyncStateMachine(typeof(_003CInstantiatePrefab_003Ed__58))]
	protected virtual UniTask<IManagedUI> InstantiatePrefab(GameObject prefab, [Optional] string name, [Optional] string group)
	{
		return default(UniTask<IManagedUI>);
	}

	[Token(Token = "0x6001824")]
	[Address(RVA = "0x5FFF10", Offset = "0x5FE910", VA = "0x1805FFF10", Slot = "58")]
	protected virtual Transform GetOrCreateGroup(string group)
	{
		return null;
	}

	[Token(Token = "0x6001825")]
	[Address(RVA = "0x600120", Offset = "0x5FEB20", VA = "0x180600120", Slot = "59")]
	protected virtual void SetFontName(string fontName)
	{
	}

	[Token(Token = "0x6001826")]
	[Address(RVA = "0x600580", Offset = "0x5FEF80", VA = "0x180600580", Slot = "60")]
	protected virtual void SetFontSize(int size)
	{
	}

	[Token(Token = "0x6001827")]
	[Address(RVA = "0x600790", Offset = "0x5FF190", VA = "0x180600790", Slot = "61")]
	protected virtual void ToggleUI()
	{
	}

	[Token(Token = "0x6001828")]
	[Address(RVA = "0x600800", Offset = "0x5FF200", VA = "0x180600800", Slot = "62")]
	[AsyncStateMachine(typeof(_003CInstantiateUIs_003Ed__63))]
	protected virtual UniTask InstantiateUIs()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001829")]
	[Address(RVA = "0x600930", Offset = "0x5FF330", VA = "0x180600930", Slot = "63")]
	protected virtual void ShowVisibleOnAwake()
	{
	}

	[Token(Token = "0x600182A")]
	[Address(RVA = "0x600BD0", Offset = "0x5FF5D0", VA = "0x180600BD0", Slot = "64")]
	protected virtual bool TryGetManaged(IManagedUI ui, out ManagedUI managedUI)
	{
		managedUI = default;
		managedUI = default;
		return default(bool);
	}

	[Token(Token = "0x600182B")]
	[Address(RVA = "0x600D40", Offset = "0x5FF740", VA = "0x180600D40", Slot = "65")]
	protected virtual UniTask ApplyFontAssociatedWithLocale(LocaleChangedArgs _)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600182C")]
	[Address(RVA = "0x601260", Offset = "0x5FFC60", VA = "0x180601260", Slot = "66")]
	protected virtual UniTask NotifyLocaleChanged(LocaleChangedArgs _)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600182D")]
	[Address(RVA = "0x6015D0", Offset = "0x5FFFD0", VA = "0x1806015D0", Slot = "67")]
	[AsyncStateMachine(typeof(_003CInitializeFonts_003Ed__68))]
	protected virtual UniTask<IEnumerable<(string, TMP_FontAsset)>> InitializeFonts()
	{
		return default(UniTask<IEnumerable<(string, TMP_FontAsset)>>);
	}
}
