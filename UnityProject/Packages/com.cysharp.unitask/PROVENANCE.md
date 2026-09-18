# Provenance: UniTask

* Upstream: https://github.com/Cysharp/UniTask
* Version tag: 2.5.10
* Archive: https://codeload.github.com/Cysharp/UniTask/tar.gz/refs/tags/2.5.10
* Archive SHA-256: D5EFBB1D36E90EE12484C36087E1058823DA95EE7624356329915C9BB3D552E2
* Vendored path: UnityProject/Assets/Plugins/ThirdParty/UniTask
  (`Runtime/**` without `External/` and without `Linq/`, plus `Runtime/External/Addressables` moved
  to `Addressables/` so its own asmdef still scopes it)
* License: MIT (see LICENSE.txt)

Vendored because the Il2CppDumper stub (`Assets/Plugins/Il2CppStubs/UniTask.dll`) is a metadata-only
dummy that Unity refuses to load, which made every UniTask-typed field fatal for the Addressables content
build.  The genuine package is referenced directly; it also means the reconstructed game code executes
real async scheduling instead of calling empty stub bodies.
