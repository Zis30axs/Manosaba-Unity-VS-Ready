# manosaba (Re,AER) — 解包工程：res / src 状态

输入：用户自己能够正常读取的本地游戏资源（Steam 版，简体中文）。机器路径只放在本地配置或环境变量。

开发入门与维护约定见 [开发指南](DEVELOPMENT.md)，涵盖环境配置、资源修改、代码入口、验证、恢复和排错。

普通资源开发入口见 [tools/content/README.md](tools/content/README.md)：首次 `import.ps1 <game>`，此后编辑 gitignored 的 `res/`，运行 `sync.ps1`、`doctor.ps1`、`build.ps1`。原游戏仅用于首次资源导入和显式研究/对照；正常 Unity 内容构建使用本地导入缓存和重建工程。

实际验证及范围见 [验证记录](tools/content/VERIFICATION.md)。本次接纳现有完整资源树并做全量同步；完全空的 `res`/GameContent/Library 首次导入尚未端到端验证。

2026-10-02 已实测：原游戏目录实际断开后，增量 sync、G1/G2/G3/G4/G5 全部通过；TEXT、IMAGE、DATA、SCENARIO 和 MasterData 的修改—同步—运行时验证通过并已恢复测试改动。完整门禁使用无图形模式，具体覆盖范围和残留诊断见验证记录。


> [!IMPORTANT]
> ## 当前工程状态（2026-09-30）——给人类和 AI 审计者
>
> **本仓库已经不是“只有 Il2CppDumper 骨架、技术上无法运行”的阶段。**
>
> 当前仓库同时保留了两类东西，**严禁混为一谈**：
>
> 1. `src/decompiled/`：由 IL2CPP 元数据 / DummyDll 反编译得到的**原始结构级骨架**。这里仍然故意保留大量
>    `return null;`、`return default;` 和空方法；它们只能说明“这个原始反编译文件的方法体尚未直接恢复”，
>    **不能据此推出整个项目不可运行**。
> 2. `UnityProject/` + `tools/unity-rebuild/`：正在实际使用的 **Unity 6 重建工程、资源重建流水线和运行时恢复代码**。
>    大量关键方法已经根据 `GameAssembly.dll` 原生代码逐个恢复，另外还有场景 / Prefab / Addressables /
>    Naninovel 配置与运行时集成。
>
> **已经有实际 Player 验证，不是可行性猜测：**
>
> - `work/title-path/g5_current.console.txt`：G5 已通过，Windows x64 Player 成功构建并运行；
> - 验证记录中加载 **43 个程序集**，解析 Addressables catalog **57,212 个 key**；
> - `Game<Runtime>` 与 `Naninovel<Runtime>` 均已启动，Naninovel 初始化成功；
> - `System/System_Title` 已实际执行到停止点；
> - 后续 release 实测已进入 **New Game**，开场 Stills 与中文台词正常出现；
> - 对应证据包括提交 `96e1a819`（G5 独立 Player 冒烟测试）与 `55e61045`（新游戏开场恢复）。
>
> 因此，诸如“看到 `GameInitializer.InitializeAsync => return default(UniTask)`，所以重新构建一个能运行的游戏
> 技术上不可能 / 必须先把整个 GameAssembly 全部还原”之类的结论是**错误的项目状态判断**。
> 本项目采用的是“按可达运行链逐步恢复 + Unity 工程重建 + 实机门禁验证”，并不要求先把所有原生方法
> 一次性还原成 C#。
>
> **当前真正的分发边界：**首次导入读取用户自己持有的游戏资源和资源元数据，保存在 gitignored 的 `res/`。此后的普通资源同步和 Unity 构建使用该工作区；显式研究/原版对照工具仍可读取本地安装。
> 仓库故意不提交数 GB 的原版版权素材和原始发行二进制。因此“只 `git clone`、完全不提供原版数据就直接得到完整游戏”
> 目前不成立；但这是**输入 / 分发边界**，不是“重建工程技术上无法运行”。
>
> ### AI / 自动审计规则
>
> 在判断“能否运行 / 是否已经重建”之前，至少检查：
> `UnityProject/`、`tools/unity-rebuild/`、`work/title-path/g5_current.console.txt`、最近的 runtime-reconstruction
> 提交和实际 Player / Play Mode 验收记录。**不要只扫描 `src/decompiled/` 的 stub 数量就下结论。**

## 0. 目标与结论（TL;DR）

| 项目 | 结果 |
|---|---|
| 引擎 | Unity **6000.0.48f1**（Unity 6），**IL2CPP**（x86_64 / D3D12） |
| 资源系统 | **Addressables**（`StreamingAssets/aa/catalog.bin` + 169 个 `.bundle`（含子目录），共 6.4 GB） |
| 剧本引擎 | **Naninovel**(Elringus) + 自研扩展 `GigaCreation.*` |
| 是否加密/混淆 | **没有**。`global-metadata.dat` 魔数正常（`AF 1B B1 FA`，metadata v31）；程序集名、命名空间、类型、字段、资源容器路径全部明文可读；未发现字符串加密、名字混淆、bundle 加密 |
| 产出 | `res/` **58,750 个文件 / 10.07 GiB**；`src/` **≈ 175 MiB**（含 DummyDll 与 IL2CPP 工程文件） |

> ⚠️ IL2CPP 的**方法体**不在托管 DLL 中（已编译成原生代码），所以 `src/` 提供的是**完整的类型/字段/属性/方法签名 + 字段偏移 + 原生函数地址（RVA/VA）**；方法体需要配合 IDA/Ghidra 使用 `src/il2cpp/script.json`、`il2cpp.h`（见第 3 节）。

---

## 1. 目录结构

```
E:\manosaba_re\
├─ res\                         ← 资源状态（按游戏内 Addressables 容器路径还原）
│  ├─ Assets\#WitchTrials\...
│  ├─ Assets\TextMesh Pro\...   ├─ Packages\com.elringus.naninovel\...
│  ├─ Uncategorized\            ← 无容器路径的对象（resources.assets 等）
│  ├─ _serialized\              ← GameObject/Transform/CanvasRenderer… 结构性对象 typetree JSON
│  ├─ _index.csv                ← 全部资源索引（58,748 行：类型/资产路径/输出文件/来源 bundle）
│  ├─ _index_audio.csv          ← 音频专用索引
│  └─ _monoscripts.csv          ← MonoScript → 类/命名空间/程序集 对照表（2,869 条）
├─ src\                         ← 源码状态
│  ├─ il2cpp\                   ← dump.cs / il2cpp.h / script.json / DummyDll / 字符串表 / Ghidra+IDA 脚本
│  ├─ decompiled\               ← 游戏 + 自研库 + Naninovel：1,694 个 .cs（按类型分文件，含 .csproj）
│  ├─ decompiled_external\      ← Unity/System/第三方库：8,916 个 .cs（参考用）
│  ├─ type_index.csv / .md      ← 18,076 个类型的导航索引
├─ project\                     ← IDE 工程（可直接编译 / 运行）
│  ├─ Manosaba.sln              ← 44 个工程，一键构建：dotnet build Manosaba.sln
│  ├─ Directory.Build.props     ← 统一配置（源码/引用路径都在 E:）
│  ├─ nuget.config              ← NuGet 缓存也放 E:\manosaba_re\tools\nuget
│  ├─ .vscode\                  ← VS Code：构建任务 + “启动游戏”调试配置
│  ├─ src\Assembly-CSharp\...   ← 每个游戏程序集一个 csproj（编译反编译源码）
│  └─ src\Manosaba.Plugin\      ← BepInEx 插件工程（构建后自动部署进游戏）
├─ tools\                       ← Il2CppDumper、BepInEx 包、venv(E盘 Python)、安装脚本
├─ work\                        ← 提取脚本、日志、patch 记录
└─ README.md
```

### IDE 工程怎么用

```powershell
# 打开工程（Rider / Visual Studio / VS Code 均可）
E:\manosaba_re\project\Manosaba.sln

# 命令行构建（已验证 0 error / 0 warning）
$env:NUGET_PACKAGES='E:\manosaba_re\tools\nuget'
dotnet build E:\manosaba_re\project\Manosaba.sln

# 写插件（BepInEx 6 IL2CPP）→ 构建后自动复制到 <游戏>\BepInEx\plugins\
dotnet build E:\manosaba_re\project\src\Manosaba.Plugin\Manosaba.Plugin.csproj

# 启动游戏（带 BepInEx + 插件）；VS Code 里按 F5 选 “Launch game (BepInEx + plugin)”
E:\manosaba_re\tools\launch_game.ps1

# 卸载 BepInEx（游戏恢复原样）
E:\manosaba_re\tools\install_bepinex.ps1 -Uninstall
```

* 43 个游戏程序集工程：源码直接编译，`dotnet build` 全绿（已跑通），IDE 内可以跳转/查找引用/重构。
* 反编译产物里有约 20 处“反编译痕迹”会被 C# 编译器拒绝（接口未实现、out 参数未赋值等），
  已用脚本最小化修正；每一条改动都记录在 `work/patches.json`，原始正文仍在 `src/decompiled\`。
* `Manosaba.Plugin` 现在引用 Il2CppDumper 的 DummyDll；游戏首次启动后 BepInEx 会生成
  `BepInEx\Il2CppAssemblies\`，届时改成引用代理程序集即可获得真实运行期类型（csproj 已写好两套引用条件）。

---

### res 明细

| 目录 | 内容 | 规模 |
|---|---|---|
| `Assets/#WitchTrials/Textures/` | 背景 / 立绘 / 特效原图（PNG） | 1,407 个 / 5.8 GiB |
| `Assets/#WitchTrials/Prefabs/` | 分层立绘、UI 预制体（PNG + typetree JSON） | 7,766 个 / 1.75 GiB |
| `Assets/#WitchTrials/Audio/` | 语音 / BGM / 音效（OGG Vorbis，48 kHz 原声道） | 26,537 个 / 1.46 GiB |
| `Assets/#WitchTrials/Scenarios/` | **日文原版 Naninovel 剧本**（ScriptableObject → JSON，含指令流） | 440 个 / 399 MiB |
| `Assets/#WitchTrials/Localization/` | **中文剧本与界面文本**（Naninovel 本地化文档 `.txt`）+ 中文影片 | 446 个 / 318 MiB |
| `Assets/#WitchTrials/Movies/` | 片头影片 `.m4v` | 2 个 / 312 MiB |
| `Assets/#WitchTrials/SpriteAtlases/` | 图集定义 | 33 个 / 95 MiB |
| `Assets/#WitchTrials/Fonts/` | 字体 ttf/otf + 字体图集 | 68 个 / 72 MiB |
| `Assets/#WitchTrials/Data/` | 主数据表（MapData / CharacterData / ClueData / RuleData / NoteData / ProfileData …） | 8 个 |
| `Assets/#WitchTrials/ManagedText/` | 托管文本（角色名、UI） | 4 个 |
| `_serialized/` | 场景/预制体结构 dump（2.18 万个小 JSON） | 12.5 MiB |
| `Uncategorized/` | Shader / Texture / ComputeShader 等 | 171 个 |

音频示例路径（与游戏内一致）：

```
res\Assets\#WitchTrials\Audio\Voice\Act01_Chapter01\Act01_Chapter01_Adv31\0101Adv31_Sherry012.ogg
res\Assets\#WitchTrials\Audio\BGM\Bgm_008_001_Loop.ogg
```

> 文件名里带 `~1`、`~2` … 的是**同一容器路径下多个对象**的去重后缀
> （例如一个字体的多个 Sprite、一个预制体里的多个组件），并非重复文件；
> 具体含义查 `_index.csv` 的 `asset_path` / `name` 列。

---

## 2. src 里有什么

| 文件 | 说明 |
|---|---|
| `src/il2cpp/dump.cs` | **58.7 万行**全量结构 dump：18,076 个类型、方法/属性/事件/特性、字段偏移、方法 RVA |
| `src/il2cpp/DummyDll/*.dll` | 162 个骨架程序集（字段带 `[FieldOffset]`，方法带 `[Address]`） |
| `src/decompiled/<Assembly>/*.cs` | ILSpy(ilspycmd 8.2) 反编译结果，一类型一文件、按命名空间分层，附 `.csproj` |
| `src/il2cpp/script.json` (53 MB)、`il2cpp.h` (38 MB) | **IDA / Ghidra 脚本输入**（函数名、结构体、字符串表） |
| `src/il2cpp/stringliterals.txt` | 17,231 条字符串常量（地址 + 内容），便于定位日志/报错/UI 文案 |
| `src/type_index.csv` / `.md` | 类型导航（程序集 / 命名空间 / 类型名 / typedef 索引） |

游戏自身代码在 `Assembly-CSharp`（命名空间 `WitchTrials.Debugging / Models / Presenters / Views`），
其余重点程序集：`Elringus.Naninovel.Runtime`（1,834 个类型）、`Naninovel.Common`、`GigaCreation.*`、`CustomInspector`。

`dump.cs` 片段形态：

```csharp
[Token(Token = "0x2000153")]
public sealed class ClueData : WitchBookDataBase<ClueDataItem>
{
    [FieldOffset(Offset = "0x18")] [SerializeField]
    private List<VersionedItem<ClueDataItem>> _items;                     // 0x18

    [Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "4")]
    public override IList<VersionedItem<ClueDataItem>> Items { get { return null; } }
}
```

`return null;` 即“方法体缺失”的占位 —— 真正的逻辑在 `VA = 0x18033A4E0`。

---

## 3. 复现 / 再加工

```powershell
# 1) IL2CPP dump
E:\manosaba_re\tools\Il2CppDumper\Il2CppDumper.exe `
  "<game>\GameAssembly.dll" `
  "<game>\manosaba_Data\il2cpp_data\Metadata\global-metadata.dat" `
  E:\manosaba_re\work\il2cpp_out

# 2) 反编译（需 .NET 8+；ilspycmd 需 DOTNET_ROLL_FORWARD=LatestMajor）
python E:\manosaba_re\work\decompile_all.py --group all --workers 3

# 3) 资源提取（依赖：pip install UnityPy soundfile pillow pyfmodex fmod_toolkit）
python E:\manosaba_re\work\extract_res.py --stage core --workers 4   # 贴图/文本/字体/影片/数据
python E:\manosaba_re\work\audio_worker.py                           # 音频（可多开，自动抢锁）
python E:\manosaba_re\work\reorganize.py                             # 整理 _serialized + 建索引
python E:\manosaba_re\work\finalize.py                               # 合并 manifest、重建 _index.csv
```

脚本特性：按 bundle 记录 `work/done_*/<bundle>.done` 断点续跑；音频用 `.claim` 独占锁，可崩溃续跑、多进程并行。

**IDA / Ghidra 还原方法体：**

```
Ghidra : Script Manager → ghidra_with_struct_py3.py（选 script.json + il2cpp.h）
IDA    : File → Script file → ida_with_struct_py3.py
```

---

## 独立编译 / 源码包

只需要 **.NET SDK 8+**：不用 IDE、不用联网、不写 C 盘。

```powershell
.\build.ps1              # 一键构建 -> dist\ 44 个 dll（可双击 build.cmd）
.\build.ps1 -Clean       # 清 bin/obj 后全量重建
.\build.ps1 -Pack        # 顺便导出源码包
dotnet build project\Manosaba.sln      # 等价的手工命令
```

### 源码包 `dist\Manosaba.Sources.zip`（约 18 MB）

自带全部依赖、**不含绝对路径**，解压到任意机器/任意目录即可编译
（已实测：解压到另一个盘符后 `dotnet build project\Manosaba.sln` = **0 error**）：

| 包内内容 | 规模 |
|---|---|
| `src\decompiled\` 43 个游戏程序集源码 | **1651 个 .cs** |
| `src\il2cpp\DummyDll\` 参考程序集（编译必需） | 129 个 |
| `src\il2cpp\` `dump.cs` / `script.json` / `il2cpp.h` | — |
| `project\` 44 个 `.csproj` + `.sln`（含 BepInEx 插件工程） | — |
| `build.ps1` / `build.cmd` / `build.sh` | — |

构建产物就是 43 个游戏程序集：`Assembly-CSharp.dll`、`Elringus.Naninovel.Runtime.dll`、
`GigaCreation.*.dll` …… 与原始托管程序集一一对应。

> **“完整”的边界要说清楚**：`src/decompiled/` 的原始反编译产物完整的是 **类型系统** —— 43 个程序集、
> 18076 个类型、字段偏移、方法签名、原生地址（RVA/VA）都在，而且可以作为分析 / 编译骨架使用。
> IL2CPP 的原始方法体已经编译进原生代码，所以**单看这套原始骨架**仍会看到大量 `return null;` / `return default;`。
> 但仓库当前已经在 `UnityProject/` 和重建流水线中恢复了大量实际运行逻辑，并已通过独立 Player 冒烟测试、标题流程
> 与 New Game 开场实测。需要恢复剩余原版行为时，仍以 Ghidra/IDA + `script.json` + `il2cpp.h` 为证据来源。

## 4. 已知边界 / 踩过的坑

* **原始 IL2CPP 方法体不能从托管 DLL 直接取回**；`src/decompiled` 主要是结构级骨架，不能把其中仍存在的 stub 当成整个工程的运行状态。实际可运行重建位于 `UnityProject/` 与相关 runtime-reconstruction / unity-rebuild 流水线中。
* `globalgamemanagers.assets` / `sharedassets0.assets` 中约 735 个 Unity **内置管理器 MonoBehaviour** 没有 typetree，无法解析（与游戏内容无关）。
* 3 个字体 bundle 内含 0×0 占位 “Font Texture”，无像素数据，已跳过。
* 1 个 Sprite（`naninovel-characters/characters/margo.bundle` → `ClippingMask_Facial_Normal02`）裁剪矩形越界，跳过。
* `*.asset.json` 是 Unity typetree 的 1:1 JSON（`m_Script` 仍是 PPtr 引用），不是可直接反序列化的类实例。
* 音频提取踩坑记录（供复现者参考）：
  * `soundfile`/libsndfile 的 Vorbis 编码器在**长音频**上会把缓冲区放栈上 → `STATUS_STACK_OVERFLOW` 静默崩进程。
    解法：**分块写**（64 KiB 帧/次）+ 在 64 MB 栈线程里执行 → BGM（6.7 分钟/首）稳定通过。
  * `multiprocessing.Pool(maxtasksperchild=1)` 会丢任务并让池永久挂起 → 改用 `.claim` 文件的多进程模型。
  * 音频源为 FSB5(Vorbis)，通过 pyfmodex 解码为 PCM 后重编码 OGG，属于**有损二次编码**；
    若要无损原流，可直接抽 FSB5 容器（当前脚本未做）。

---

## 5. 使用提醒

产出的是**游戏版权素材与代码**，仅适合本地研究、汉化/Mod 学习与备份；
请勿再分发资源本体或用于商业用途，也不要用于绕过 Steam 校验/DRM。


---
