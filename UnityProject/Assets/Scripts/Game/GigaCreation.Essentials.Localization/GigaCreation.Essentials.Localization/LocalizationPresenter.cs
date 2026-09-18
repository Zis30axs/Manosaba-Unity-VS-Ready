using System.Collections.Generic;
using System.Linq;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.Localization
{
    [Token(Token = "0x2000013")]
    public class LocalizationPresenter : MonoBehaviour
    {
    	[Token(Token = "0x4000032")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private LocaleData _sourceLocaleData;

    	[Token(Token = "0x4000033")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private LocaleData[] _localizationLocaleData;

    	[Token(Token = "0x4000034")]
    	[FieldOffset(Offset = "0x30")]
    	private ILocalizationManager _localizationManager;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD34B0, 1312 B
    	//
    	// Numbered native evidence (log:
    	// tools\unity-rebuild\out\logs\localizationpresenter_awake_0xCD34B0.txt):
    	//  1. `rbp = __this`, then `r14 = [rbp+0x28]` = `_localizationLocaleData`
    	//     (dump.cs: `_localizationLocaleData // 0x28`, `LocaleData[]`).
    	//  2. `<>c` display-class singleton is fetched/built and its two *cached*
    	//     delegate fields `+0x8 <>9__3_0` and `+0x10 <>9__3_1` are created only
    	//     if null -- the standard `<>c.<>9__3_0 ??= ...` lowering for a
    	//     non-capturing lambda in `Awake`. The METHODINFO slots name them
    	//     explicitly: `LocalizationPresenter.<>c.<Awake>b__3_0` and
    	//     `...<Awake>b__3_1` (data 0x2E674A0 / 0x2E67498).
    	//     Their (folded, shared) bodies prove the selector bodies exactly
    	//     (log tools\unity-rebuild\out\logs\localizationpresenter_lambda_slots.txt):
    	//       b__3_0 -> RVA 0x312620: `test rdx,rdx; mov eax,[rdx+0x10]` =
    	//                 read `LocaleData._kind` (+0x10, LocaleKind = int32)
    	//                 => `data => data.Kind`
    	//       b__3_1 -> RVA 0x312640: `test rdx,rdx; mov rax,[rdx+0x18]` =
    	//                 read `LocaleData._item` (+0x18, LocaleDataItem ref)
    	//                 => `data => data.Item`
    	//     (dump.cs `LocaleData`: `_kind // 0x10`, `_item // 0x18`; both fields
    	//     are private, so from this class only the public `Kind`/`Item`
    	//     properties can be written -- each is a trivially-folded getter
    	//     (`get_Kind` RVA 0x331650 = `mov eax,[rcx+0x10]; ret`, `get_Item`
    	//     RVA 0x33A4E0 = `mov rax,[rcx+0x18]; ret`), so no call survives.)
    	//  3. `ToDictionary`: METHODINFO alias
    	//     `Method$System.Linq.Enumerable.ToDictionary<LocaleData, LocaleKind,
    	//     LocaleDataItem>()` (data 0x2E67470) and the call
    	//     `Enumerable$$ToDictionary<...>(rcx = _localizationLocaleData,
    	//     rdx = <>9__3_0, r8 = <>9__3_1, r9/rgctx = shared generic ctx)`.
    	//     Result kept in r14; the source array is NOT null-guarded inline
    	//     (it is handed to the BCL, which does its own ArgumentNullException).
    	//  4. `((IDictionary<LocaleKind, LocaleDataItem>)r14).Add(key, value)`:
    	//     an explicit interface-slot resolution against
    	//     `IDictionary<LocaleKind, LocaleDataItem>_TypeInfo` (0xCD37B2) with
    	//     slot index 5 (`mov r8d, 5` -> `sub_243A20`; the slow path at
    	//     0xCD39A9 computes `klass + 0x138 + (interfaceOffset + 5) * 16`),
    	//     then `rcx = r14 (dictionary)`, `edx = [sourceLocaleData+0x10]`
    	//     (LocaleKind), `r8 = [sourceLocaleData+0x18]` (LocaleDataItem),
    	//     `call r10`. dump.cs `IDictionary<TKey, TValue>` metadata slots:
    	//     `// Slot: 5 public abstract void Add(TKey key, TValue value);`
    	//     -- so this is `Add`, NOT the `set_Item` indexer (slot 1). Both
    	//     arguments are dereferenced with the standard `test/je` NRE check
    	//     on `_sourceLocaleData` (+0x20) and on the dictionary.
    	//     CONSEQUENCE (intended game behaviour, not a bug to paper over):
    	//     if `_sourceLocaleData`'s kind is already present in
    	//     `_localizationLocaleData`, this throws ArgumentException.
    	//  5. `new LocalizationManager(_sourceLocaleData.Kind, localeData)`:
    	//     `il2cpp_codegen_object_new(LocalizationManager_TypeInfo)` followed by
    	//     direct field stores `[+0x18] = new UniRx.Subject<LocaleKind>()`,
    	//     `[+0x28] = kind`, `[+0x2C] = kind`, `[+0x10] = dictionary` -- this is
    	//     the ctor at RVA 0xCD1DB0 inlined at the call site (verified by
    	//     disassembling it separately: identical store order and offsets, and
    	//     the same `Subject<LocaleKind>` + `data_30DD123` class-init guard),
    	//     matching dump.cs
    	//     `LocalizationManager(LocaleKind sourceLocale, IDictionary<LocaleKind,
    	//     LocaleDataItem> localeData)` with fields
    	//     `_localeData // 0x10`, `_localeChangedSubject // 0x18`,
    	//     `<SourceLocale>k__BackingField // 0x28`,
    	//     `<CurrentLocale>k__BackingField // 0x2C` (the ctor seeds
    	//     CurrentLocale from sourceLocale).
    	//  6. `[rbp+0x30] = rdi` = `_localizationManager` (dump.cs: +0x30).
    	//  7. `ServiceLocator.Register<ILocalizationManager>(_localizationManager)`:
    	//     METHODINFO `Method$GigaCreation.Tools.Service.ServiceLocator.
    	//     Register<ILocalizationManager>()` (0xCD3982) placed in rdx with the
    	//     field value in rcx, then a *tail* `jmp` to
    	//     `ServiceLocator$$Register<object>` -- a genuine tail call, consistent
    	//     with `Register` returning void and being the last statement.
    	//
    	// The local's declared type must be the interface (step 4 proves the call
    	// site is an interface dispatch, not `Dictionary<K,V>.Add`): B-level only
    	// in the *name* of the local, which is unknowable from native code.
    	//
    	// OUT-OF-SCOPE DEPENDENCIES (both in files this task did not authorise, so
    	// they are reported rather than fixed -- without them this body compiles and
    	// the registration itself is correct, but the values it feeds the manager are
    	// not the game's):
    	//   * `LocaleData.get_Kind` (RVA 0x331650 = `mov eax,[rcx+0x10]; ret`) and
    	//     `LocaleData.get_Item` (RVA 0x33A4E0 = `mov rax,[rcx+0x18]; ret`) are
    	//     still `return default(LocaleKind);` / `return null;` stubs in
    	//     LocaleData.cs, so every selector below would read 0 / null. Both are
    	//     Confidence A trivial shared bodies (the repo already reconstructs the
    	//     identical pair for PerspectiveStageData.Kind/Stage).
    	//   * `LocalizationManager..ctor` (RVA 0xCD1DB0) is still `{ }` in
    	//     LocalizationManager.cs, so the registered instance's `_localeData`,
    	//     `_localeChangedSubject`, `SourceLocale` and `CurrentLocale` stay
    	//     unset. Registering the service still makes
    	//     `ServiceLocator.Get<ILocalizationManager>()` succeed (which is what the
    	//     boot chain needs); using it does not.
    	[Token(Token = "0x6000033")]
    	[Address(RVA = "0xCD34B0", Offset = "0xCD1EB0", VA = "0x180CD34B0")]
    	private void Awake()
    	{
    		IDictionary<LocaleKind, LocaleDataItem> localeData = _localizationLocaleData.ToDictionary((LocaleData data) => data.Kind, (LocaleData data) => data.Item);
    		localeData.Add(_sourceLocaleData.Kind, _sourceLocaleData.Item);
    		_localizationManager = new LocalizationManager(_sourceLocaleData.Kind, localeData);
    		ServiceLocator.Register(_localizationManager);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCD39E0, 104 B
    	// A single tail `jmp` to `ServiceLocator$$Unregister<object>` after loading
    	// METHODINFO `...ServiceLocator.Unregister<ILocalizationManager>()`
    	// (0xCD3A34) into rdx and `_localizationManager` (+0x30) into rcx -- the
    	// exact mirror of Awake's `Register`. dump.cs lists the GigaCreation
    	// overload as `Unregister<TService>(TService service)`, service by value.
    	// (log: tools\unity-rebuild\out\logs\localizationpresenter_ondestroy_0xCD39E0.txt)
    	[Token(Token = "0x6000034")]
    	[Address(RVA = "0xCD39E0", Offset = "0xCD23E0", VA = "0x180CD39E0")]
    	private void OnDestroy()
    	{
    		ServiceLocator.Unregister(_localizationManager);
    	}

    	// Not reconstructed: RVA 0x30D8F0 is a *shared trivial* body (aliased by
    	// thousands of parameterless ctors, e.g. UnityEngine.MonoBehaviour/Component/
    	// Transform..ctor). Its whole body is the IL2CPP class-init guard plus a
    	// tail-jump into the runtime's ctor helper -- no field stores, no argument
    	// use, no game logic. Left empty deliberately.
    	[Token(Token = "0x6000035")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public LocalizationPresenter()
    	{
    	}
    }
}
