# manosaba (Re,AER) Unity VSCode Ready

<img width="1150" height="241" alt="QQ20261024-181441" src="https://github.com/user-attachments/assets/94b2d8b9-6583-4646-822e-d2fa12f91b3b" />

# 为什么不使用 PhantomShield 保护你的游戏工程呢？

> 我们反对任何人以任何理由传播、重编译后再分发本项目工程相关内容，也严禁任何人于本仓库 Release 处获得盗版《魔法少女的魔女裁判》游戏本体后倒卖或于视频网站大肆宣传。  
> 本仓库内一切美术资产、音乐资产、代码资产等归 Re,AER、Acacia 所有。

## 介绍

本项目旨在重建游戏的 Unity 工程结构与运行时，使资源能够重新导入 Unity、场景和剧本能够运行，并为后续的代码分析、汉化与 Mod 开发提供基础。

项目包含 Unity 6 重建工程、资源同步工具、IL2CPP 分析文件、IDE 工程和运行时恢复代码。游戏资源需要从用户自己持有的 Steam 原版导入；仓库本身不提供完整游戏资源或原始发行文件。

## 项目状态

目前，项目已完成完整资源树的接入与全量同步，并通过 Windows x64 Player 构建和运行测试。Unity 场景、Prefab、Addressables、Naninovel 及相关运行时恢复工作已进入实际集成阶段。

这不是一个只包含反编译结果的代码仓库。项目同时维护原始反编译骨架和实际使用的 Unity 重建工程，两者用途不同。

| 项目 | 当前情况 |
|---|---|
| Unity 版本 | Unity 6000.0.48f1（Unity 6） |
| 运行平台 | Windows x64，IL2CPP，Direct3D 12 |
| 资源系统 | Addressables，`catalog.bin` 与 169 个 AssetBundle |
| 剧本引擎 | Naninovel + `GigaCreation.*` 扩展 |
| IDE 工程 | 44 个 .NET 工程 |
| Unity 重建 | 已接入资源、场景、Prefab 与运行时恢复代码 |
| 资源工作流 | 已完成现有完整资源树的全量同步验证 |
| 独立源码包 | 已实测构建通过，0 error |
| 空环境首次导入 | 尚未完成端到端验证 |

### 运行验证

项目已完成以下阶段性验证：

- 成功构建 Windows x64 Player 并启动运行。
- 加载 43 个游戏程序集，解析 Addressables catalog 中的 57,212 个 key。
- `Game<Runtime>` 与 `Naninovel<Runtime>` 成功启动，Naninovel 初始化完成。
- 标题场景 `System/System_Title` 已实际运行至预定停止点。
- 后续验证已进入 New Game，开场 Stills 与中文台词正常显示。
- 原版游戏目录断开后，增量资源同步及 G1/G2/G3/G4/G5 验证通过。

详细的验证范围、测试结果与遗留问题见 [tools/content/VERIFICATION.md](tools/content/VERIFICATION.md) 及 `work/title-path/g5_current.console.txt`。

需要注意：以上结果不代表所有场景、剧情分支及游戏功能均已恢复，也不代表只克隆仓库就能直接获得完整游戏。

## 快速开始

### 1. 准备环境

根据开发目标准备相应工具：

- Windows x64
- Unity 6000.0.48f1
- .NET SDK
- Git
- 原版游戏的本地安装文件（资源导入所需）

具体版本要求、环境变量、依赖配置与排错方式以 [DEVELOPMENT.md](DEVELOPMENT.md) 为准。

### 2. 导入游戏资源

首次使用时，需要从本地原版游戏导入资源。

资源开发入口：[tools/content/README.md](tools/content/README.md)

在仓库根目录执行：

```powershell
# 首次导入：<game> 为本地游戏目录
.\tools\content\import.ps1 <game>
```

导入完成后，资源保存在被 Git 忽略的 `res/` 目录中。之后的常规开发不需要每次重新提取原版资源。

```powershell
# 同步资源修改
.\tools\content\sync.ps1

# 检查资源与工程状态
.\tools\content\doctor.ps1

# 执行内容构建
.\tools\content\build.ps1
```

首次导入的完整流程尚未在完全空的 `res/`、`GameContent/` 与 `Library/` 环境下完成端到端验证。使用前请阅读工作流文档中的前置条件和限制。

### 3. 打开 Unity 工程

使用 Unity Hub 打开仓库中的 `UnityProject/`，并选择 Unity 6000.0.48f1。

导入资源后，即可在重建工程中继续进行场景、Prefab、Addressables、Naninovel 配置及运行时开发。

### 4. 编译 IDE 工程

仓库同时提供用于代码浏览、编译和插件开发的 .NET 解决方案。

默认工作区路径示例：

```powershell
E:\manosaba_re\project\Manosaba.sln
```

设置 NuGet 包缓存并构建：

```powershell
$env:NUGET_PACKAGES = 'E:\manosaba_re\tools\nuget'

dotnet build E:\manosaba_re\project\Manosaba.sln
```

也可以使用仓库根目录提供的独立构建脚本：

```powershell
# 构建全部程序集
.\build.ps1

# 清理 bin/obj 后重新构建
.\build.ps1 -Clean

# 打包源码与依赖
.\build.ps1 -Pack
```

构建完成后，程序集输出至 `dist/`。独立源码包为 `dist/Manosaba.Sources.zip`，不依赖原开发机器的绝对路径，已实测构建通过。

### 5. 使用 VS Code 调试

仓库提供 BepInEx 插件工程及相应的启动、安装脚本。

```powershell
# 构建插件
dotnet build E:\manosaba_re\project\src\Manosaba.Plugin\Manosaba.Plugin.csproj

# 安装 BepInEx
.\install_bepinex.ps1

# 启动游戏与插件
.\tools\launch_game.ps1

# 卸载 BepInEx
.\install_bepinex.ps1 -Uninstall
```

在 VS Code 中，也可以选择 `Launch game (BepInEx + plugin)` 配置并按 F5 启动调试。

以上路径仅为示例。请根据实际克隆目录调整路径，并在启动前确认游戏安装、资源导入及插件部署已完成。

## 目录结构

```text
Manosaba-Unity-VS-Ready/
├── res/                     # 本地资源工作区，不提交至 Git
├── src/
│   ├── il2cpp/              # IL2CPP 分析文件、DummyDll 与分析脚本
│   ├── decompiled/          # 游戏及相关库的反编译结构骨架
│   ├── decompiled_external/ # Unity、System 与第三方库参考代码
│   └── type_index.csv       # 类型索引
├── project/
│   ├── Manosaba.sln         # .NET 解决方案
│   └── src/
│       └── Manosaba.Plugin/ # BepInEx 插件工程
├── UnityProject/            # Unity 6 重建工程
├── tools/
│   ├── content/             # 资源导入、同步、检查与构建
│   └── unity-rebuild/       # Unity 工程重建与运行时恢复工具
├── work/                    # 分析脚本、日志及验证记录
├── build.ps1                # 独立构建入口
├── launch_game.ps1          # 游戏启动脚本
├── install_bepinex.ps1      # BepInEx 安装与卸载
├── DEVELOPMENT.md           # 开发指南
└── README.md
```

## 技术实现

### IL2CPP 分析与代码恢复

原版游戏使用 IL2CPP。托管程序集中的方法经过转换后，其实际实现位于原生二进制中。因此，DummyDll 和基于它生成的反编译代码主要用于恢复类型、字段、属性、方法签名等结构信息，不能直接视为完整的原始 C# 源码。

`src/il2cpp/` 保存了相关分析资料：

- `dump.cs`：类型、字段与方法信息。
- `il2cpp.h`：原生结构及类型定义。
- `script.json`：供反编译与分析工具使用的映射信息。
- DummyDll：用于结构分析和 IDE 工程构建的程序集。
- IDA / Ghidra 脚本：辅助定位原生函数和分析方法实现。

方法体的恢复需要结合原生代码分析、运行时行为和实际测试逐步完成。

### Unity 工程重建

`UnityProject/` 与 `tools/unity-rebuild/` 是实际的工程重建与运行时开发入口，负责资源重新组织、场景及 Prefab 重建、Addressables 配置、Naninovel 集成和必要的运行时逻辑恢复。

项目采用按实际运行链逐步恢复的方式：优先修复启动流程和可达功能，再根据运行结果继续处理尚未恢复的方法，而不是要求所有原生方法都完成还原后才尝试运行。

### 资源工作流

原版资源通过 Addressables 容器及相关资源文件导入本地工作区。目前资源工作区约有 58,750 个文件，占用约 10.07 GiB；`src/` 约 175 MiB。

资源系统包含 `catalog.bin`、169 个 AssetBundle 及相应的资源索引。常规开发以 `res/` 为工作区，通过同步工具将修改应用到重建工程。

## 已知限制

- **资源依赖：** 仓库不包含完整原版资源和原始发行二进制。仅克隆仓库无法直接得到完整游戏。
- **反编译结果：** `src/decompiled/` 中仍有大量未恢复的方法体。它们不等于 Unity 重建工程中对应功能的最终实现状态。
- **首次导入：** 现有完整资源树已完成全量同步，但完全空白环境下的首次导入尚未完成端到端验证。
- **Unity 内置对象：** `globalgamemanagers.assets` 与 `sharedassets0.assets` 中约 735 个内置管理器 MonoBehaviour 缺少 typetree，无法按当前流程解析。
- **字体与 Sprite：** 3 个字体 bundle 包含尺寸为 0×0 的占位 Font Texture，已跳过；另有 1 个 Sprite 的裁剪矩形越界，已跳过。
- **序列化数据：** `*.asset.json` 是 Unity typetree 的 JSON 表示，不是可以直接反序列化为完整 Unity 类实例的数据。
- **音频质量：** 当前音频流程会将 FSB5（Vorbis）音频解码后重新编码为 OGG，存在二次有损编码。若需要保留原始音频流，需要直接提取 FSB5 容器；当前流程尚未实现这一功能。

上述限制会随开发进展变化。涉及具体功能时，请优先参考最新提交、验证记录及实际运行结果。

## 参与开发

欢迎围绕工程重建、运行时恢复、资源工作流、文档和构建工具提交 Issue 或 Pull Request。

开始修改前，建议先阅读：

- [DEVELOPMENT.md](DEVELOPMENT.md)：环境配置、开发约定与排错。
- [tools/content/README.md](tools/content/README.md)：资源导入、同步及构建流程。
- [tools/content/VERIFICATION.md](tools/content/VERIFICATION.md)：验证记录与已知问题。

提交涉及运行时恢复的改动时，尽量附上对应的构建结果、日志或 Player / Play Mode 验证记录，以便确认修改的实际效果。

## 版权与使用范围

本项目是面向本地研究的工程重建工具，不是游戏本体的再发行版本。

- 请使用自己合法持有、能够正常访问的原版游戏资源。
- 原版游戏的代码、图像、音频、剧本及其他版权素材不随本仓库分发。
- 本项目的公开代码、工具与文档不代表原版游戏版权素材获得了额外授权。使用、修改或分发时，应分别遵守适用的许可证及原权利人的要求。
- 请勿将原版游戏资源或原始发行文件重新打包上传、用于未经授权的商业用途，或利用本项目绕过 Steam 校验及 DRM。

《魔法少女的魔女审判》及其相关内容的版权归相应权利人所有。本项目与原游戏发行方不存在当然的官方关联或授权关系。
