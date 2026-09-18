# Provenance: Steamworks.NET

* Upstream: https://github.com/rlabrecque/Steamworks.NET
* Version tag: 2025.162.1
* Archive: https://codeload.github.com/rlabrecque/Steamworks.NET/tar.gz/refs/tags/2025.162.1
* Archive SHA-256: 8E04594FBC9EFA7139561146A15225093B4F5629D04E870C295DD01927E3F86F
* Vendored path: UnityProject/Packages/com.rlabrecque.steamworks.net (the upstream package as-is:
  Runtime + Editor + its own .asmdefs and package.json)
* License: MIT (see LICENSE.md)

The shipped game's assembly reports version 0.0.0.0 (IL2CPP interop strips it), so the tag was chosen as
the release closest to the game's build date and then *verified by compiling* the reconstructed sources
against it (G1: 0 errors).

Native dependency: Steamworks.NET needs `steam_api64.dll`.  It is NOT in the upstream repository
(Valve's redistributable), so the copy that the shipped game carries was placed at
`UnityProject/Assets/Plugins/x86_64/steam_api64.dll` (source:
`<game>/manosaba_Data/Plugins/x86_64/steam_api64.dll`, 312 KB).  Without it the first Steam call would
throw DllNotFoundException instead of simply failing to initialise.

* steam_api64.dll SHA-256: 4586FB605DAF81EA86375D4FA79B7FC8A2D64A4B503536E8EE7D2D3BA20012CF
