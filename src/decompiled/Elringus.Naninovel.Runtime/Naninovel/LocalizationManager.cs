using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel;

[Token(Token = "0x200038E")]
[InitializeAtRuntime(0, null)]
public class LocalizationManager
{
	[Serializable]
	[Token(Token = "0x200038F")]
	public class Settings
	{
		[Token(Token = "0x4000D3F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public string SelectedLocale;

		[Token(Token = "0x6001155")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public Settings()
		{
		}
	}

	[Token(Token = "0x2000390")]
	private readonly struct OnChangeTask
	{
		[Token(Token = "0x4000D40")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public readonly Func<LocaleChangedArgs, UniTask> Handler;

		[Token(Token = "0x4000D41")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public readonly int Priority;

		[Token(Token = "0x6001156")]
		[Address(RVA = "0x589040", Offset = "0x587A40", VA = "0x180589040")]
		public OnChangeTask(Func<LocaleChangedArgs, UniTask> handler, int priority)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000395")]
	[CompilerGenerated]
	private struct _003CInitializeService_003Ed__25
{
		[Token(Token = "0x4000D48")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000D49")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000D4A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public LocalizationManager _003C_003E4__this;

		[Token(Token = "0x4000D4B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600115F")]
		[Address(RVA = "0x5891A0", Offset = "0x587BA0", VA = "0x1805891A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001160")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000396")]
	[CompilerGenerated]
	private struct _003CLoadServiceState_003Ed__29
{
		[Token(Token = "0x4000D4C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000D4D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000D4E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SettingsStateMap stateMap;

		[Token(Token = "0x4000D4F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public LocalizationManager _003C_003E4__this;

		[Token(Token = "0x4000D50")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001161")]
		[Address(RVA = "0x589500", Offset = "0x587F00", VA = "0x180589500", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001162")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000397")]
	[CompilerGenerated]
	private struct _003CRetrieveAvailableLocales_003Ed__35
{
		[Token(Token = "0x4000D51")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000D52")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000D53")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public LocalizationManager _003C_003E4__this;

		[Token(Token = "0x4000D54")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask<IReadOnlyCollection<Folder>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001163")]
		[Address(RVA = "0x5898C0", Offset = "0x5882C0", VA = "0x1805898C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001164")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000398")]
	[CompilerGenerated]
	private struct _003CSelectLocale_003Ed__32
{
		[Token(Token = "0x4000D55")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000D56")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000D57")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public LocalizationManager _003C_003E4__this;

		[Token(Token = "0x4000D58")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string locale;

		[Token(Token = "0x4000D59")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private LocaleChangedArgs _003CeventArgs_003E5__2;

		[Token(Token = "0x4000D5A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private InteractionBlocker _003C_003E7__wrap2;

		[Token(Token = "0x4000D5B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private IEnumerator<OnChangeTask> _003C_003E7__wrap3;

		[Token(Token = "0x4000D5C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001165")]
		[Address(RVA = "0x589ED0", Offset = "0x5888D0", VA = "0x180589ED0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001166")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000D3D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private readonly IResourceProviderManager resources;

	[Token(Token = "0x4000D3E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private readonly List<OnChangeTask> changeTasks;

	[Token(Token = "0x17000256")]
	public virtual LocalizationConfiguration Configuration
	{
		[Token(Token = "0x6001140")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "19")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000257")]
	public virtual string SelectedLocale
	{
		[Token(Token = "0x6001141")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "20")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6001142")]
		[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000258")]
	private IReadOnlyCollection<string> Naninovel_002EILocalizationManager_002EAvailableLocales
	{
		[Token(Token = "0x6001143")]
		[Address(RVA = "0x587C60", Offset = "0x586660", VA = "0x180587C60", Slot = "12")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000259")]
	private IReadOnlyList<IResourceProvider> Naninovel_002EILocalizationManager_002EProviders
	{
		[Token(Token = "0x6001144")]
		[Address(RVA = "0x587C80", Offset = "0x586680", VA = "0x180587C80", Slot = "13")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700025A")]
	protected virtual List<string> AvailableLocales
	{
		[Token(Token = "0x6001145")]
		[Address(RVA = "0x38F060", Offset = "0x38DA60", VA = "0x18038F060", Slot = "21")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700025B")]
	protected virtual List<IResourceProvider> Providers
	{
		[Token(Token = "0x6001146")]
		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0", Slot = "22")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1400005C")]
	public event Action<LocaleChangedArgs> OnLocaleChanged
	{
		[Token(Token = "0x600113E")]
		[Address(RVA = "0x587A40", Offset = "0x586440", VA = "0x180587A40", Slot = "9")]
		[CompilerGenerated]
		add
		{
		}
		[Token(Token = "0x600113F")]
		[Address(RVA = "0x587B50", Offset = "0x586550", VA = "0x180587B50", Slot = "10")]
		[CompilerGenerated]
		remove
		{
		}
	}

	[Token(Token = "0x6001147")]
	[Address(RVA = "0x587CA0", Offset = "0x5866A0", VA = "0x180587CA0")]
	public LocalizationManager(LocalizationConfiguration cfg = default, IResourceProviderManager resources = default)
	{
	}

	[Token(Token = "0x6001148")]
	[Address(RVA = "0x587F50", Offset = "0x586950", VA = "0x180587F50", Slot = "23")]
	[AsyncStateMachine(typeof(_003CInitializeService_003Ed__25))]
	public virtual UniTask InitializeService()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001149")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "24")]
	public virtual void ResetService()
	{
	}

	[Token(Token = "0x600114A")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "25")]
	public virtual void DestroyService()
	{
	}

	[Token(Token = "0x600114B")]
	[Address(RVA = "0x588070", Offset = "0x586A70", VA = "0x180588070", Slot = "26")]
	public virtual void SaveServiceState(SettingsStateMap stateMap)
	{
	}

	[Token(Token = "0x600114C")]
	[Address(RVA = "0x5881D0", Offset = "0x586BD0", VA = "0x1805881D0", Slot = "27")]
	[AsyncStateMachine(typeof(_003CLoadServiceState_003Ed__29))]
	public virtual UniTask LoadServiceState(SettingsStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600114D")]
	[Address(RVA = "0x588350", Offset = "0x586D50", VA = "0x180588350", Slot = "28")]
	public virtual void GetAvailableLocales(ICollection<string> tags)
	{
	}

	[Token(Token = "0x600114E")]
	[Address(RVA = "0x588510", Offset = "0x586F10", VA = "0x180588510", Slot = "29")]
	public virtual bool LocaleAvailable(string locale)
	{
		return default(bool);
	}

	[Token(Token = "0x600114F")]
	[Address(RVA = "0x588580", Offset = "0x586F80", VA = "0x180588580", Slot = "30")]
	[AsyncStateMachine(typeof(_003CSelectLocale_003Ed__32))]
	public virtual UniTask SelectLocale(string locale)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001150")]
	[Address(RVA = "0x588710", Offset = "0x587110", VA = "0x180588710", Slot = "31")]
	public virtual void AddChangeLocaleTask(Func<LocaleChangedArgs, UniTask> handler, int priority)
	{
	}

	[Token(Token = "0x6001151")]
	[Address(RVA = "0x588900", Offset = "0x587300", VA = "0x180588900", Slot = "32")]
	public virtual void RemoveChangeLocaleTask(Func<LocaleChangedArgs, UniTask> handler)
	{
	}

	[Token(Token = "0x6001152")]
	[Address(RVA = "0x588A30", Offset = "0x587430", VA = "0x180588A30", Slot = "33")]
	[AsyncStateMachine(typeof(_003CRetrieveAvailableLocales_003Ed__35))]
	protected virtual UniTask RetrieveAvailableLocales()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001153")]
	[Address(RVA = "0x588B50", Offset = "0x587550", VA = "0x180588B50", Slot = "34")]
	protected virtual string GetDefaultLocale()
	{
		return null;
	}

	[Token(Token = "0x6001154")]
	[Address(RVA = "0x588C30", Offset = "0x587630", VA = "0x180588C30", Slot = "35")]
	protected virtual bool TryMapSystemLocale(out string locale)
	{
		locale = default;
		locale = default;
		return default(bool);
	}
}
