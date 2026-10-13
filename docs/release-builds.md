# Windows / Android 测试版构建与发布

源资源准备、正常同步和 Addressables 内容构建遵循 `DEVELOPMENT.md`。所有平台使用同一份已审核源码，但资源包必须针对各自平台构建，不能把 Windows bundle 放进 APK。

## Windows

先执行正常内容构建，再在关闭项目 Editor 后调用 `Manosaba.EditorTools.BuildPlayer.RunRelease`。产物为 `dist/player-release/manosaba-rebuild.exe` 及同目录数据；不能只分发 EXE。Release 使用非 Development 模式，游戏内调试面板不会作为发布界面显示。

Windows Player 入口在场景列表为空时使用已存在的 `Assets/Scenes/Boot.unity`，不改写用户场景列表。它消费前一步已构建的 Addressables，打包期间临时关闭重复内容构建，并恢复原内容构建选项及 managed stripping 设置。

```powershell
& $env:MANOSABA_UNITY_EDITOR -batchmode -nographics -quit -buildTarget Win64 `
  -projectPath "$PWD/UnityProject" -executeMethod Manosaba.EditorTools.BuildPlayer.RunRelease `
  -logFile "$PWD/windows-build.log"
& $env:MANOSABA_PYTHON tools/unity-rebuild/package_windows_player.py --out dist/releases/MY_TAG/windows
```

打包器生成若干独立 ZIP，每个低于 1.9 GB。全部解压至同一位置，合并 `Manosaba-Windows` 文件夹后运行 EXE。每个输入文件均记录 SHA256，ZIP 写完后检查 CRC 和大小；排除调试备份、PDB、运行日志及 Naninovel 测试存档，不跟随符号链接或 Windows reparse 路径。不覆盖既有发布目录。

## Windows 独立运行验证

现有 `-manosabaSmoke` / `-manosabaContent` 负责启动和实际资源加载证据。可同时传入 `-manosabaSaveRoot <绝对目录>`，将三种 Naninovel IO 存档（游戏、全局、设置）隔离到指定目录。只有同时显式启用 smoke 时允许覆盖；非法、重复、相对、真实用户目录内或经过 junction/symlink 的路径会失败，不能退回写真实存档。普通启动和 Editor 的保存路径保持不变。

设置 `MANOSABA_GAMESTART=1` 与 `MANOSABA_GAMESTART_EVIDENCE` 可运行已有游戏启动探针；其余运行参数见 `g5_player_smoke.ps1`。检查新报告、进程退出码、真实 `BundledAssetProvider` 加载、标题脚本以及用户存档前后文件身份。图形验收使用 D3D11；`-nographics` 的通过只代表无图形启动。测试产生的 `_Data/NaninovelData` 诊断目录不分发。

## Android

```powershell
pwsh -NoProfile -File tools/unity-rebuild/g3_android_content_build.ps1 -Group all
& $env:MANOSABA_UNITY_EDITOR -batchmode -nographics -quit -buildTarget Android `
  -projectPath "$PWD/UnityProject" -executeMethod Manosaba.EditorTools.BuildAndroidPlayer.RunRelease `
  -logFile "$PWD/android-build.log"
& $env:MANOSABA_PYTHON tools/unity-rebuild/package_android_content.py --out dist/releases/MY_TAG/android-content
```

新入口生成 `dist/player-android-release/manosaba-rebuild.apk` 和 `build_android_player_release.json`，保留原 Debug 构建入口。Release 不启用 Development / AllowDebugging，构建结束后恢复两个 Editor development 标志。当前仍使用本机 Unity debug keystore，供私有预发布侧载，不是生产发行签名。

Android 内容超过 OBB 限制时沿用已有外置资源方案：APK 内包含内容清单，ZIP 按清单打包，首次启动通过文件选择器导入所有 ZIP。APK 和 ZIP 必须来自同一次匹配内容构建，不混用旧资源。签名、包名、最低系统要求、CPU 架构以及是否 debuggable 应从新 APK 读回；有设备再运行真机门禁，不能沿用旧真机报告。

## GitHub Releases

提交经审核的源码、转换配方及正式 `content-overrides`，不提交生成树、用户存档或 `dist`。为通过构建的源码创建唯一 tag，先创建私有仓库中的 draft prerelease，上传 Windows 全部 ZIP、APK、Android 全部 ZIP、安装说明和 SHA256 清单；逐项比对服务器返回的大小／摘要后再发布。保留仓库原有可见性。

完整资源包通过 Release 附件分发；协作者从源码重建仍需自己的同版本已导入资源。GitHub 托管 CI 仅运行静态检查，没有本地资源与 Unity 许可证时不能代替实际构建和运行验收。
