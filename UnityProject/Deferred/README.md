# Deferred integration work

Files here are **intentionally outside `Assets/`** so Unity does not compile them yet.
`UnityProject/Deferred/` is not an asset folder, so nothing here can break a build.

Each entry records why it is parked rather than shipped.

---

## `AddressablesManager.cs` (reconstructed)

**Status: DEFERRED — blocked on a scope decision, then on a duplicate-type conflict.**

### Why it is not in `Assets/`

`UnityProject/Assets/Scripts/Runtime/GigaCreation.Essentials.AddressablesUtils/AddressablesManager.cs`
was the original location. It cannot stay there:

1. **Duplicate type.** The class
   `GigaCreation.Essentials.AddressablesUtils.AddressablesManager` is *also* declared at
   `src/decompiled/GigaCreation.Essentials.AddressablesUtils/GigaCreation.Essentials.AddressablesUtils/AddressablesManager.cs`.
   Whenever both trees are compiled into one assembly, Unity reports CS0101 (duplicate
   definition) and the project fails to compile. Exactly one of the two may exist.
2. **Ownership.** `UNITY_REBUILD_AGENT_BOUNDARIES.md:80` states that findings in
   `GigaCreation.*` should first become an inventory and that restoring original method
   bodies is handed to DS unless the user explicitly assigns them to the integration agent.
   That document landed at commit `38b2ddc` (13:03), *after* this reconstruction was written
   (this file was authored ~12:0x). The scope decision belongs to the user.
3. **Third party.** `GigaCreation.*` is GIGA CREATION's MIT-licensed tool suite, not the
   game's own code — see `UnityProject/THIRD_PARTY_NOTICES.md`. Reconstruction is a
   fallback; the original package is preferable if obtainable.

### What is actually recovered (for whoever owns it)

Grade A evidence, all from `src/il2cpp/dump.cs` and the shipped interop metadata:

* field layout — `_handles : IList<AsyncOperationHandle>`, `_loadedAssets : IDictionary<string, object>`,
  `_pendingLoadAssetTasks : IDictionary<string, UniTask>` (`dump.cs:727595-727596`), plus
  `_webRequestManager`, `_additionalCatalogLocationsWithoutExt`, `_loadAssetCts`;
* the type-erased `object` cache, and why;
* the AssetReference overload forwards via `AssetReference.RuntimeKey` rather than loading
  directly (its state machine has no `linkedCts` local, unlike the string overload);
* `ReleaseHandles` selects handles with a `Func<AsyncOperationHandle, bool>` filter rather
  than releasing blindly;
* `GameInitializer::Awake` (recovered, RVA `0x30FA80`) constructs exactly this type and
  registers it as `IAddressablesManager`, which is the boot-path consumer.

### The one genuine unknown

`LoadResourceLocationsAsync` passes a **single key** to `Addressables.LoadResourceLocationsAsync`.
That key is an immediate operand in the native method (RVA `0xCC24D0`); it appears in no
package, metadata dump or string table. The reconstruction exposes it as the named constant
`ResourceLocationsKey` whose value literally contains `[UNRESOLVED:...]`, so the gap is
visible and testable instead of hidden behind an invented literal.

### To adopt this file

Pick one, and say which:

* **A.** Keep it — delete or stub the `src/decompiled` copy, and treat this as an
  integration shim (`INTEGRATION_SHIM_REQUIRED` in the boundaries vocabulary). Move it back
  under `Assets/`.
* **B.** Hand the method bodies to DS (boundaries `L80`) — this file becomes DS input, not
  integration output.
* **C.** Use the real upstream `GigaCreation.Essentials` package instead — preferred if the
  user can supply it; this file is then deleted.

Until one is chosen, compiling anything that includes both trees will fail with CS0101.
