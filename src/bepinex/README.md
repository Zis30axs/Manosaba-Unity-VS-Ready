# BepInEx 引用程序集(随仓库分发)

这个目录让 `Manosaba.Plugin` 在**任何机器上都能编译**,不要求先安装游戏或 BepInEx。

## 内容

| 程序集 | 版本 | 许可 |
| --- | --- | --- |
| `BepInEx.Core.dll` | 6.0.0-be.788 (5b766a3b) | MIT |
| `BepInEx.Unity.IL2CPP.dll` | 6.0.0-be.788 (5b766a3b) | MIT |
| `BepInEx.Unity.Common.dll` | 6.0.0-be.788 (5b766a3b) | MIT |
| `0Harmony.dll` | 2.10.2 | MIT |
| `Il2CppInterop.Runtime.dll` | 1.5.3 (dbda1cb3) | MIT |

均取自 BepInEx 6.0.0-be.788 官方发行包(`BepInEx/core/`)。上游项目:

- BepInEx — https://github.com/BepInEx/BepInEx (MIT)
- Harmony — https://github.com/pardeike/Harmony (MIT)
- Il2CppInterop — https://github.com/BepInEx/Il2CppInterop (MIT)

## 为什么需要提交它们

`project/src/Manosaba.Plugin/Manosaba.Plugin.csproj` 原先只从 `$(GameDir)\BepInEx\core`
解析这些引用,而 `$(GameDir)` 默认写死为本机路径:

    E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game

干净机器上该路径不存在,于是引用被整组跳过,`Plugin.cs` 报 32 个 CS0246
(找不到 `BepInEx` / `HarmonyLib` 命名空间),整个解决方案编译失败。

现在 csproj 的解析顺序是:

1. `$(GameDir)\BepInEx\core` — 本机已装游戏时优先,保证与游戏实际加载的版本一致
2. **本目录** — 兜底,使全新克隆无需游戏即可编译

两者 API 面相同,所以编译产物一致。

## 注意

- 这些是**第三方二进制**,不是本项目代码。若本仓库将来公开,请自行确认再分发合规
  (上游均为 MIT,保留版权声明即可),或改用从 NuGet/官方发行包取件的方式。
- 升级 BepInEx 时需同步替换本目录,并核对 `Manosaba.Plugin.csproj` 里的文件名
  (BepInEx 6 曾把代理程序集从 `Il2CppAssemblies` 迁到 `interop`,核心库名也从
  `BepInEx.IL2CPP.dll` 改成 `BepInEx.Unity.IL2CPP.dll`)。
