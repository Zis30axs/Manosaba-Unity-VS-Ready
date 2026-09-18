# Provenance: UniRx

* Upstream: https://github.com/neuecc/UniRx
* Version: 7.1.0 (release, published 2019-07-01)
* Archive: https://codeload.github.com/neuecc/UniRx/tar.gz/refs/tags/7.1.0
* Archive SHA-256: 570222E13D814484FE58A27FAF3345EC1E8835BA2BEC25F2DE884DB396653863
* Vendored path: UnityProject/Assets/Plugins/ThirdParty/UniRx (only `Assets/Plugins/UniRx/Scripts`)
* License: MIT (see LICENSE.txt next to this file)

Why it is vendored instead of the Il2CppDumper stub: the stub assembly
(`Assets/Plugins/Il2CppStubs/UniRx.dll`) is a metadata-only Il2CppDumper dummy. Unity cannot load it
("Unloading broken assembly ... this assembly can cause crashes in the runtime"), so every field whose
type came from it made the Addressables content build abort. The genuine package is referenced directly
(no decompilation, no re-implementation), which is also what makes the reconstructed game code actually
*execute* its reactive chains instead of calling empty stub bodies.

## License text

The MIT License (MIT)

Copyright (c) 2018 Yoshifumi Kawai

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

