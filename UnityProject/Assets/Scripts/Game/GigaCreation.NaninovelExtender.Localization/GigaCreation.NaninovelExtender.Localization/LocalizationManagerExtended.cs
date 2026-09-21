using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.Localization;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace GigaCreation.NaninovelExtender.Localization
{
    [Token(Token = "0x2000006")]
    [UsedImplicitly]
    [InitializeAtRuntime(0, typeof(Naninovel.LocalizationManager))]
    // Interface list as shipped (dump.cs TypeDefIndex 17666): without ILocalizationManagerExtended,
    // Engine.GetService<ILocalizationManagerExtended>() finds no service and OptionsUi.Initialize stalls engine init.
    public class LocalizationManagerExtended : Naninovel.LocalizationManager, ILocalizationManagerExtended
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000009")]
    	[CompilerGenerated]
    	private struct _003CGetAvailableLocaleTagsAsync_003Ed__14
    {
    		[Token(Token = "0x400000F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000010")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<IEnumerable<string>> _003C_003Et__builder;

    		[Token(Token = "0x4000011")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public LocalizationManagerExtended _003C_003E4__this;

    		[Token(Token = "0x4000012")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private Naninovel.UniTask<IReadOnlyCollection<Folder>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000015")]
    		[Address(RVA = "0xD319D0", Offset = "0xD303D0", VA = "0x180D319D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000016")]
    		[Address(RVA = "0xD31EF0", Offset = "0xD308F0", VA = "0x180D31EF0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000A")]
    	[CompilerGenerated]
    	private struct _003CRetrieveAvailableLocales_003Ed__12
    {
    		[Token(Token = "0x4000013")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000014")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000015")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public LocalizationManagerExtended _003C_003E4__this;

    		[Token(Token = "0x4000016")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private Naninovel.UniTask<IEnumerable<string>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000017")]
    		[Address(RVA = "0xD31F20", Offset = "0xD30920", VA = "0x180D31F20", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000018")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000B")]
    	[CompilerGenerated]
    	private struct _003CSelectLocale_003Ed__10
    {
    		[Token(Token = "0x4000017")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000018")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000019")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public LocalizationManagerExtended _003C_003E4__this;

    		[Token(Token = "0x400001A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public LocaleKind locale;

    		[Token(Token = "0x400001B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private Naninovel.UniTask _003Ctask_003E5__2;

    		[Token(Token = "0x400001C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400001D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000019")]
    		[Address(RVA = "0xD324F0", Offset = "0xD30EF0", VA = "0x180D324F0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600001A")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200000C")]
    	[CompilerGenerated]
    	private struct _003CSelectLocale_003Ed__11
    {
    		[Token(Token = "0x400001E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400001F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000020")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public LocalizationManagerExtended _003C_003E4__this;

    		[Token(Token = "0x4000021")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string locale;

    		[Token(Token = "0x4000022")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private Naninovel.UniTask _003Ctask_003E5__2;

    		[Token(Token = "0x4000023")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000024")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600001B")]
    		[Address(RVA = "0xD329E0", Offset = "0xD313E0", VA = "0x180D329E0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600001C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD31260 (field initializer; 230 B total)
    	// `_validLocales` (+0x48) = new List<string>(): the constructor emits
    	// `il2cpp_codegen_object_new(List<string>_TypeInfo)` + the list constructor and stores the
    	// result to +0x48 BEFORE the base constructor call -> a C# field initializer.
    	[Token(Token = "0x4000009")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private readonly List<string> _validLocales = new List<string>();

    	[Token(Token = "0x400000A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private GigaCreation.Essentials.Localization.ILocalizationManager _localizationManager;

    	[Token(Token = "0x400000B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private IDefaultLocaleSelector _defaultLocaleSelector;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x453470, 5 B (identical-code-folded with ~60 plain field getters)
    	//   0x180453470  mov rax, [rcx+0x48]  (_validLocales) ; ret
    	[Token(Token = "0x17000003")]
    	public IReadOnlyCollection<string> ValidLocales
    	{
    		[Token(Token = "0x6000008")]
    		[Address(RVA = "0x453470", Offset = "0x451E70", VA = "0x180453470", Slot = "36")]
    		get
    		{
    			return _validLocales;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD310E0, 186 B -- lazily cached service lookup
    	//   0x180D3111B  rax = [this+0x50] (_localizationManager); non-null -> return it
    	//   0x180D31141  ServiceLocator.Get<ILocalizationManager>(cl = 1: includeDerivedType) -> stored to +0x50, returned
    	// Annotated disassembly: work/title-path/disas/lme_02_get_LocalizationManager.txt
    	[Token(Token = "0x17000004")]
    	private GigaCreation.Essentials.Localization.ILocalizationManager LocalizationManager
    	{
    		[Token(Token = "0x6000009")]
    		[Address(RVA = "0xD310E0", Offset = "0xD2FAE0", VA = "0x180D310E0")]
    		get
    		{
    			if (_localizationManager == null)
    			{
    				_localizationManager = ServiceLocator.Get<GigaCreation.Essentials.Localization.ILocalizationManager>(true);
    			}
    			return _localizationManager;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD311A0 -- same shape as LocalizationManager on +0x58
    	//   ServiceLocator.Get<IDefaultLocaleSelector>(cl = 1: includeDerivedType), cached in _defaultLocaleSelector
    	// Annotated disassembly: work/title-path/disas/lme_03_get_DefaultLocaleSelector.txt
    	[Token(Token = "0x17000005")]
    	private IDefaultLocaleSelector DefaultLocaleSelector
    	{
    		[Token(Token = "0x600000A")]
    		[Address(RVA = "0xD311A0", Offset = "0xD2FBA0", VA = "0x180D311A0")]
    		get
    		{
    			if (_defaultLocaleSelector == null)
    			{
    				_defaultLocaleSelector = ServiceLocator.Get<IDefaultLocaleSelector>(true);
    			}
    			return _defaultLocaleSelector;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD31260, 230 B
    	// ABI: rcx=__this, rdx=cfg, r8=resources (two parameters + MethodInfo*).
    	// 1. field initializer (see `_validLocales` above): +0x48 = new List<string>().
    	// 2. tail call: rcx=__this (rdi), rdx=cfg (rbp), r8=resources (rsi), r9d=0 (MethodInfo*),
    	//    `jmp 0x180587ca0 Naninovel.LocalizationManager$$.ctor` == base(cfg, resources).
    	//    Nothing else is emitted (empty body).
    	[Token(Token = "0x600000B")]
    	[Address(RVA = "0xD31260", Offset = "0xD2FC60", VA = "0x180D31260")]
    	public LocalizationManagerExtended(LocalizationConfiguration cfg, IResourceProviderManager resources)
    		: base(cfg, resources)
    	{
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD31350 (kickoff) / 0xD324F0 (<SelectLocale>d__10.MoveNext)
    	//   0x180D325EB  <task>5__2 = base.SelectLocale(locale.ToTagName())   (direct call to Naninovel
    	//                LocalizationManager.SelectLocale 0x180588580 == the compiler's <>n__0 base accessor)
    	//   0x180D32677  await LocalizationManager.ChangeLocaleAsync(locale, default)   (GigaCreation ILocalizationManager,
    	//                a Cysharp UniTask awaited directly: <>u__1; the token argument is rbx == 0)
    	//   0x180D32836  await <task>5__2   (<>u__2)
    	// Annotated disassembly: work/title-path/disas/lme_05_SelectLocale_kind_MoveNext.txt
    	[Token(Token = "0x600000C")]
    	[Address(RVA = "0xD31350", Offset = "0xD2FD50", VA = "0x180D31350", Slot = "37")]
    	[AsyncStateMachine(typeof(_003CSelectLocale_003Ed__10))]
    	public Naninovel.UniTask SelectLocale(LocaleKind locale)
    	{
    		return SelectLocaleCore(locale);
    	}

    	private async Naninovel.UniTask SelectLocaleCore(LocaleKind locale)
    	{
    		Naninovel.UniTask task = base.SelectLocale(locale.ToTagName());
    		await LocalizationManager.ChangeLocaleAsync(locale);
    		await task;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD31490 (kickoff) / 0xD329E0 (<SelectLocale>d__11.MoveNext)
    	//   Same shape as SelectLocale(LocaleKind), keyed by the tag instead:
    	//   0x180D32AD2  <task>5__2 = base.SelectLocale(locale)
    	//   0x180D32B3C  await LocalizationManager.ChangeLocaleAsync(locale.ToLocaleKind(), default)
    	//   then await <task>5__2
    	// Annotated disassembly: work/title-path/disas/lme_07_SelectLocale_str_MoveNext.txt
    	[Token(Token = "0x600000D")]
    	[Address(RVA = "0xD31490", Offset = "0xD2FE90", VA = "0x180D31490", Slot = "30")]
    	[AsyncStateMachine(typeof(_003CSelectLocale_003Ed__11))]
    	public override Naninovel.UniTask SelectLocale(string locale)
    	{
    		return SelectLocaleCore(locale);
    	}

    	private async Naninovel.UniTask SelectLocaleCore(string locale)
    	{
    		Naninovel.UniTask task = base.SelectLocale(locale);
    		await LocalizationManager.ChangeLocaleAsync(locale.ToLocaleKind());
    		await task;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD31620 (kickoff) / 0xD31F20 (<RetrieveAvailableLocales>d__12.MoveNext)
    	//   await GetAvailableLocaleTagsAsync()                         (<>u__1, result kept at [rsp+0xf0])
    	//   0x180D3227D  AvailableLocales.Clear()                         (vtable 0x288 = slot 21 get_AvailableLocales)
    	//   0x180D322D2  AvailableLocales.Add(Configuration.SourceLocale)  (slot 19 get_Configuration, +0x28 SourceLocale)
    	//   0x180D3231D  AvailableLocales.AddRange(tags)                   (List.AddRange inlined as InsertRange(_size, ...))
    	//   0x180D3232F  _validLocales.Clear()                             (+0x48)
    	//   0x180D32356  DefaultLocaleSelector.AvailableTextLanguages (interface slot 1); when it is non-null
    	//                (0x180D3235B, the shipped null test) and its Count (ICollection<string> slot 0) is > 0, the
    	//                getter is called again and that list is used; otherwise AvailableLocales (0x180D323A1)
    	//   0x180D323E3  _validLocales.AddRange(<chosen list>)
    	// Annotated disassembly: work/title-path/disas/lme_09_RetrieveAvailableLocales_MoveNext.txt
    	[Token(Token = "0x600000E")]
    	[Address(RVA = "0xD31620", Offset = "0xD30020", VA = "0x180D31620", Slot = "33")]
    	[AsyncStateMachine(typeof(_003CRetrieveAvailableLocales_003Ed__12))]
    	protected override Naninovel.UniTask RetrieveAvailableLocales()
    	{
    		return RetrieveAvailableLocalesCore();
    	}

    	private async Naninovel.UniTask RetrieveAvailableLocalesCore()
    	{
    		IEnumerable<string> tags = await GetAvailableLocaleTagsAsync();
    		AvailableLocales.Clear();
    		AvailableLocales.Add(Configuration.SourceLocale);
    		AvailableLocales.AddRange(tags);
    		_validLocales.Clear();
    		IList<string> textLanguages = DefaultLocaleSelector.AvailableTextLanguages;
    		bool useTextLanguages = textLanguages != null && textLanguages.Count > 0;
    		_validLocales.AddRange(useTextLanguages ? DefaultLocaleSelector.AvailableTextLanguages : AvailableLocales);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD31740, 93 B
    	//   0x180D3176F  DefaultLocaleSelector (0xD311A0); 0x180D31785 IDefaultLocaleSelector slot 0 = DefaultLocale
    	//   0x180D31793  tail jump LocaleKindExtensions.ToTagName
    	// Annotated disassembly: work/title-path/disas/lme_10_GetDefaultLocale.txt
    	[Token(Token = "0x600000F")]
    	[Address(RVA = "0xD31740", Offset = "0xD30140", VA = "0x180D31740", Slot = "34")]
    	protected override string GetDefaultLocale()
    	{
    		return DefaultLocaleSelector.DefaultLocale.ToTagName();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD317A0 (kickoff) / 0xD319D0 (<GetAvailableLocaleTagsAsync>d__14.MoveNext)
    	//   0x180D31B61  await Providers.LocateFolders(Configuration.Loader.PathPrefix)
    	//                (vtable 0x298 = slot 22 get_Providers, 0x268 = slot 19 get_Configuration; +0x18 Loader, +0x10 PathPrefix)
    	//   0x180D31D24  .Select(<>c.<>9__14_0)  -- b__14_0 (0x589140) is code-folded with Naninovel's own
    	//                LocalizationManager `r => r.Name` lambda (<RetrieveAvailableLocales>b__35_0)
    	//   0x180D31DE6  .Where(<>O.<0>__ContainsTag)  -- method group Naninovel.Languages.ContainsTag
    	// Annotated disassembly: work/title-path/disas/lme_12_GetAvailableLocaleTagsAsync_MoveNext.txt
    	[Token(Token = "0x6000010")]
    	[Address(RVA = "0xD317A0", Offset = "0xD301A0", VA = "0x180D317A0")]
    	[AsyncStateMachine(typeof(_003CGetAvailableLocaleTagsAsync_003Ed__14))]
    	private Naninovel.UniTask<IEnumerable<string>> GetAvailableLocaleTagsAsync()
    	{
    		return GetAvailableLocaleTagsAsyncCore();
    	}

    	private async Naninovel.UniTask<IEnumerable<string>> GetAvailableLocaleTagsAsyncCore()
    	{
    		IReadOnlyCollection<Folder> folders = await Providers.LocateFolders(Configuration.Loader.PathPrefix);
    		return folders.Select(folder => folder.Name).Where(Naninovel.Languages.ContainsTag);
    	}
    }
}
