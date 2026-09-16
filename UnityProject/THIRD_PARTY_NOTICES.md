# Third-party notices and reconstruction boundaries

This file records **who owns what** in this reconstruction, so that third-party code is
never presented as the game's own code and never mistaken for something that may be
redistributed freely. It exists because a boundary error here changes what may legally be
reconstructed at all.

## Evidence

Everything below is taken from the licence file that ships **inside the game itself**:
`manosaba_game/LICENSE.md` (18,654 bytes, 206 lines).

| Line | Text |
|---|---|
| 21-23 | `MIT License` / `Copyright (c) 2023 Gigacee / GIGA CREATION` |
| 55-59 | `■Naninovel Extender` / `MIT License` / `Copyright (c) 2025 GIGA CREATION` |
| 69-71 | `MIT License` / `Copyright (c) 2024 GIGA CREATION` |
| 133-135 | `MIT License` / `Copyright (c) 2018 Artyom Sovetnikov (Elringus)` |

Independently corroborated by source inspection: no source under any `GigaCreation.*`
directory references game-specific concepts. A search for `trial`, `debate`, `witch` and
`evidence` across all of `GigaCreation.*` returns **zero** matches, i.e. these are generic
utility packages, not game code.

## Classification

### FOREIGN_ASSEMBLY — MIT, generic utility packages (GIGA CREATION / Gigacee)

These are third-party Unity tool packages that the game happens to ship. They are **not**
part of the game's own codebase, and the shipped `LICENSE.md` grants MIT terms for them.

`GigaCreation.Essentials.*` (AddressablesUtils, Dialogs, Input, Localization, MasterData,
Overlays, Platforms, SaveLoad, Toggleables, UI, UniTasks, WebRequestUtils),
`GigaCreation.NaninovelExtender.*`, `GigaCreation.Tools.*`.

Consequences:

* A behavioural reconstruction here is a **derivative of MIT-licensed third-party code**,
  not a recovery of the game's own work. It must be attributed to GIGA CREATION and the
  MIT notice kept with it.
* If the user can obtain the original packages, using them is strictly preferable to a
  reconstruction. Reconstruction is the fallback, not the goal.

The reconstruction of `AddressablesManager` in
`Assets/Scripts/Runtime/GigaCreation.Essentials.AddressablesUtils/` falls under this
classification and carries the MIT notice below.

### COMMERCIAL_ASSET — must be supplied by the user

| Package | Evidence | Status |
|---|---|---|
| **Naninovel** (`Elringus.Naninovel.Runtime`, `Naninovel.Common`) | `LICENSE.md:133-135` carries an Elringus/Naninovel MIT notice, **but** Naninovel is a commercial Unity Asset Store product and this reconstruction has no purchased entitlement. Its MIT notice covers the licence file's own scope, which this project cannot verify on the user's behalf. | **BLOCKED** — ask the user; do not fetch, do not vendor, do not substitute an equivalent |
| LeTai.TrueShadow | 388 serialized instances, no source shipped | BLOCKED |
| MPUIKit, UIPolygon, ppt-noise, FastEnum | interop assemblies only, no source | BLOCKED |

> Note on the Naninovel MIT notice: it is recorded here as evidence, not as an entitlement.
> Redistribution of Naninovel source or binaries is not assumed. Do not treat the presence
> of that notice as permission to reconstruct or redistribute Naninovel.

### FIRST_PARTY — the game's own code

`Assembly-CSharp`, `CustomInspector` (+ `.Helpers`) and the game-specific `WitchTrials.*`
namespaces. Only here is "reconstruct the original implementation" the correct goal.

### NOT_A_RUNTIME_DEPENDENCY

`src/il2cpp/DummyDll/` (128 reference DLLs) and `BepInEx/interop/` (157 Il2CppInterop
proxies) are **analysis inputs**. They must never become the new Player's runtime
implementation, and must not be introduced as duplicate same-named types/assemblies.

## MIT notice retained for the reconstructed third-party component

```
MIT License

Copyright (c) 2023 Gigacee / GIGA CREATION

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```
