# Manosaba Unity 运行集成 — 版本、输入、配置与实测状态

本文档对应 `UNITY_READINESS_CODEX_AUDIT.md` §3.10.1 的交付要求，记录**实际执行过的**
结果。凡未执行的项一律写 `NOT_RUN`，缺输入/授权的项写 `BLOCKED`，不写 PASS。

施工记录（逐项、含实测数字）另见 [`RECONSTRUCTION_CODEX_INTEGRATION_NOTES.md`](RECONSTRUCTION_CODEX_INTEGRATION_NOTES.md)。
审计来源文档保持 Codex 署名不变，本文件的施工内容不冒充审计署名。

---

## 1. 基线与分支

| 项 | 值 |
|---|---|
| 仓库 | `https://github.com/Zis30axs/Manosaba.git` |
| 工作分支 | `master`（fast-forward 到 `origin/main`，未新建分支、未强推） |
| 本次基线提交 | `5f2895a` `docs: audit Unity runtime readiness and add full remediation prompt` |
| 上一提交 | `39f51c1` `docs: add Codex reconstruction audit notes` |
| 拉取前状态 | 工作树干净（`git status --porcelain` 为空） |
| 其他代理工作 | `recon/core-systems`(cb360c1 @E:/Manosaba-Claude)、`audit/unity-assets`(ac81a9a @E:/Manosaba-DS) 均在各自 worktree，未被修改 |

`5f2895a` 引入 `UNITY_READINESS_CODEX_AUDIT.md`（275 行）与 `RECONSTRUCTION_CODEX_NOTES.md`。
本文件是其 §3 施工 prompt 的执行记录载体。

## 2. 实测工具链版本

全部为**本机实测**，非推测：

| 组件 | 版本 | 证据 |
|---|---|---|
| Unity Editor | `6000.0.48f1` revision `170d2541580d` | `Unity.exe` VersionInfo `ProductVersion 6000.0.48f1_170d2541580d`；`Unity.exe -version` → `6000.0.48f1` |
| Unity 安装路径 | `E:\Unity\6000.0.48f1\Editor\Unity.exe` | 官方安装包 `UnitySetup64-6000.0.48f1.exe`（4,046,579,608 字节，与 API 声明长度一致） |
| Unity 包解析来源 | `Editor\Data\Resources\PackageManager\Editor\*.tgz` | 65 个随 Editor 分发的已解析包 tarball |
| Addressables | **2.4.6**（游戏实际构建） | `StreamingAssets/aa/settings.json` → `m_AddressablesVersion: "2.4.6"`；`packages.unity.com` 存在该版本 |
| Addressables（Editor 自带解析版本） | 2.4.3 | `Editor\Data\Resources\PackageManager\Editor\com.unity.addressables-2.4.3.tgz` |
| 原始构建 Unity 版本（游戏内） | `6000.0.48f1` | 目标 bundle 内 `SerializedFile.unity_version == "6000.0.48f1"`（a03 实测） |
| Python | 3.12.10 | `tools\venv\Scripts\python.exe` |
| UnityPy | 1.25.3 | 同上 |
| .NET SDK | 9.0.311 | `dotnet --list-sdks` |
| Node.js | 已安装 | `node --version` |

## 3. 输入清单与预检（G0）

输入**齐全**。`tools/unity-rebuild/preflight.py` 实测结果（`out/preflight.json`）：

```
status                  = INPUTS_PRESENT
inputs_checked          = 22
missing_required_count  = 0
missing_optional_count  = 1     (il2cpp_data/Metadata/stringliteral.json)
bundles.bundle_count    = 457
bundles.total_bytes     = 6,912,804,042
unity.found             = E:\Unity\6000.0.48f1\Editor\Unity.exe
```

| 输入 | 路径 | 状态 |
|---|---|---|
| 原始 Player | `…\manosaba_game\manosaba.exe`、`UnityPlayer.dll`、`GameAssembly.dll`、`baselib.dll` | 存在 |
| Player 序列化文件 | `manosaba_Data\level0`、`globalgamemanagers(.assets)(.resS)`、`resources.assets(.resS)`、`sharedassets0.assets(.resS)` | 存在 |
| Addressables | `manosaba_Data\StreamingAssets\aa\{catalog.bin,catalog.hash,settings.json,AddressablesLink\link.xml}` | 存在（catalog 5,551,003 字节） |
| AssetBundle | `aa\StandaloneWindows64\*.bundle` | **457 个**，6.91 GB |
| IL2CPP 元数据 | `il2cpp_data\Metadata\global-metadata.dat` | 存在；`stringliteral.json` **缺失**（可选） |
| 真实程序集 | `BepInEx\interop\*.dll`（约 165 个，含 `Assembly-CSharp.dll`、`Elringus.Naninovel.Runtime.dll`、`GigaCreation.*.dll`） | 存在，作为权威元数据 |

**禁止事项遵守情况**：原始游戏目录、`res/`、bundle 全程只读，未修改、未覆盖、未删除。

## 4. 路径配置（移除 E: 硬编码）

`tools/audit/common.py` 原先硬编码 `E:\Manosaba-VSCodeReady` 与迅雷路径（审计 UR-012）。
现改为 `tools/audit/config.py` 统一解析，优先级：环境变量 → `audit.config.local.json`
（已加入 `.gitignore`，机器专属，不入库）→ `audit.config.json`（版本化）→ 默认值。

支持的环境变量：`MANOSABA_REPO_ROOT`、`MANOSABA_RES_ROOT`、`MANOSABA_GAME_ROOT`、
`MANOSABA_GAME_DATA_ROOT`、`MANOSABA_AA_ROOT`、`MANOSABA_SOURCE_ROOT`、`MANOSABA_DUMP_ROOT`、
`MANOSABA_OUTPUT_ROOT`、`MANOSABA_UNITY_EDITOR`、`MANOSABA_PYTHON`。

`preflight.py` 在任一必需输入缺失时以非零码退出，并写出 `out/blocked.csv`（精确路径 + 影响范围）。

## 5. 目录约定

| 用途 | 路径 | Git |
|---|---|---|
| 真实 Unity 工程 | `UnityProject/`（`Assets/`、`Packages/`、`ProjectSettings/`） | 入库（`Library/`、`Temp/`、生成 csproj 已忽略） |
| 转换与验证产物 | `tools/unity-rebuild/out/` | 忽略 |
| 审计中间产物 | `tools/audit/out/` | 部分忽略（`a03/`、缓存） |
| 原始资源 | `res/`、游戏目录 | 忽略 / 只读 |

## 6. 验收门状态（G0–G6）

**没有任何一门被标为通过，除非下方明确写 PASS。** 本表是唯一的状态来源，每次重跑后更新；
「说明与证据」列的路径都是本机实测产物。

| 门 | 状态 | 说明与证据 |
|---|---|---|
| **G0** 输入与审计 | **PARTIAL** | 输入/hash/版本可复现：`preflight.json` ✅。审计回归测试通过：`tools/audit/test_audit_regressions.py` **33 passed / 0 failed**（exit 0）✅。每个源对象台账归属：`object_ledger.jsonl` 60,144 行 ✅。**未完成**：`--hash` 全量 SHA-256 尚未重跑（`NOT_RUN`） |
| **G1** 工程与编译 | **PASS** | 真实 Unity Editor（6000.0.48f1）批处理编译 43 个游戏程序集：**0 error / 0 warning**，产出 76 个 Player 程序集；证据 `out/unity/g1_compile.json`（§11）。许可证经 licensing client 的 entitlement 路径解析（`common.ps1` 改为「先查正向证据」） |
| **G2** 全资源导入 | **PASS** | 台账驱动的导入 + 校验：`found 30,905/30,905`，0 个 GUID 变更，两轮输入指纹一致；SO 转换 448/448，304,280/304,280 托管引用（95 类，与 dump 计数 0 偏差）；prefab 156/156 **主根树完整**（§19.3；`incomplete=0`，另有 232 个多根模板对象与 1,526 个建不出的对象按 prefab 如实记录）。证据 `out/unity/import_verify_*.json`、`so_convert_*`、`prefab_build_result.json`（§13–§16、§19） |
| **G3** 内容构建 | **PARTIAL** | 工程自己的 Addressables 配置与构建打通：core 组 + prefabs 组，**2,856 entries → 4,299 locations / 7 bundles / 1,032.6 MB**（§19.3）；地址 907/907 命中发行版 catalog（§17）。**未完成**：全量 30,819 entries（26,537 音频 + 3,301 贴图）尚未构建，另有 18 个 prefab 因桩程序集被 Unity 拒载而排除（§19.4，可重算）。证据 `out/unity/g3_content_build.json`、`addressables_compare_*.json` |
| **G4** Play Mode | **PASS** | Editor Play Mode 跑同一套运行时探针：`Assets/Scenes/Boot.unity` 进入 Play Mode，43/43 程序集、5/5 类型探针、发行版 catalog `PARSED` 57,212 keys、工程 catalog `AddressablesMainContentCatalog` 3,909 keys、4/4 地址加载、3/3 prefab 实例化；**证据里 provider 全部是 `BundledAssetProvider` 且 internal_id 是发行版容器路径**，证明 Play Mode 用的确实是 G3 构建出来的 bundle（而不是 "Use Asset Database" 捷径）。证据 `out/unity/g4_play_mode.json`、`g4_play_mode_gate.json`、`play_mode_evidence.json`（§20） |
| **G5** 独立 Player | **PASS**（范围受限） | 真实构建并实跑 Windows x64 Player（1,220.4 MB）：43/43 程序集加载、5/5 类型探针解析、发行版 catalog `PARSED`（57,212 keys）、工程自己的 catalog `AddressablesMainContentCatalog`（3,937 keys）、4/4 地址加载成功、**3/3 prefab 实例化成功且组件绑定到重建的游戏脚本**（`Naninovel.FX.Blur`、`GigaCreation.Essentials.Ui.TweeningButton`）。**范围受限**：跑起来的是集成层启动路径，`WitchTrials.Views.GameInitializer` 等方法体仍是 IL2CPP 桩，故**不代表游戏流程可玩**。证据 `out/unity/g5_player_smoke.json`、`player_smoke_evidence.json`（§12、§18） |
| **G6** 全内容覆盖 | **NOT_RUN** | 依赖 G3 全量与场景（`level0`）导入。**已有可量化的入口**：「重建的启动链能走多远」由 `g4b_game_start.ps1` 持续测量——当前结果：入口 `Awake` 执行成功、`Start()` 停在 `GameInitializer.cs:166` 的第一处场景引用（§21） |

`g1_compile.ps1` 在许可证缺失时以退出码 **2** 结束（BLOCKED），不会误报 PASS。

### G1 阻断的精确补齐动作

**G1 已通过（§11）；此处保留当时的阻断分析，作为许可证解析路径的记录。** 工具链本身已齐备并逐项核验：

| 项 | 名称 | 类型/API | 影响范围 | 补齐动作 |
|---|---|---|---|---|
| 1 | Unity Editor 许可证 | Unity Licensing Client（`No valid Unity Editor license found`） | G1–G6 全部 Editor 依赖项 | 用户在 Unity Hub 登录并激活 Unity Personal/Pro；或提供 `.ulf`（`-manualLicenseFile`）/ 序列号（`-serial`） |

`g1_compile.ps1` 现在区分两种阻断：`no-licence` 与 `broken-install`（缺少
`UnityPackageManager.exe`），避免用笼统消息掩盖真实原因。

工具链核验结果（实测）：

| 组件 | 路径 | 状态 |
|---|---|---|
| Editor | `E:\Unity\6000.0.48f1\Editor\Unity.exe`（`-version` → `6000.0.48f1`） | 46,833 文件 / 7.28 GB |
| 包管理器 | `Editor\Data\Resources\PackageManager\Server\UnityPackageManager.exe` | 存在（68 个内置包） |
| Windows 构建支持 | `Editor\Data\PlaybackEngines\windowsstandalonesupport`（含 `WinPlayerBuildProgram.exe`） | 存在 |
| Windows Player 变体 | `win64_player_development_mono`、`win64_player_nondevelopment_mono`、`win_arm64_*` | 存在 |
| IL2CPP | `Editor\Data\il2cpp\build\deploy\il2cpp.exe` + `Editor\Data\il2cpp\libil2cpp` | 存在 |

> 安装过程记录：首次 Editor 安装实际**不完整**（30,348 文件 / 4.06 GB，缺 `UnityPackageManager.exe`），
> 我在清理「临时目录」时又误删了安装器的解压缓存，加剧了损坏。已用原安装包重装修复，
> 现为 46,833 文件 / 7.28 GB，各组件齐备。事故与教训见
> [`RECONSTRUCTION_CODEX_INTEGRATION_NOTES.md`](RECONSTRUCTION_CODEX_INTEGRATION_NOTES.md) §2.8。


## 7. 已完成的实测结果（不依赖 Editor）

全部数字来自本机重跑，非历史数字转抄。

### 7.1 源侧对象台账（a03 + a09）

```
source objects                 60,144
serialized files                 457
assetbundle container paths   28,332
index rows                     58,748   (distinct keys 58,748 — 无重复)
dumped json files              28,445
physical files in game data      486   (6,953,282,587 bytes)
distinct types                    64   (旧索引只覆盖 43 种)
types outside legacy 43           21   (AssetBundle, PlayerSettings, RenderTexture, …)
```

**未导出对象 1,426 个**，逐类型（`type_ledger.csv` / `ledger_summary.json`）：

| 类型 | 数量 | 说明 |
|---|---|---|
| MonoBehaviour | **730** | 与审计 UR-006 的 730 完全一致，现已逐对象定位 |
| AssetBundle | 457 | 每个 bundle 的元对象 |
| MonoScript | 211 | 来自 `monoscripts.bundle` |
| Texture2D / RenderTexture / Sprite / AudioClip | 5 / 2 / 1 / 1 | 审计所说“约 9 个其他漏导对象” |
| PlayerSettings / InputManager / TagManager / …（>2 other） | 各 1 | 全局管理器对象 |

`（recovered)` 的 30 行原先因 `(source_bundle, pathID)` 建模、`path_id=0` 而**坍缩为 1 个身份**，
现按 dump 文件相对路径给出彼此区分的身份，并把 `path_id` 标为未知（不编造）。

### 7.2 引用图（a04，符号位与作用域均按实修正）

```
refs_total                          92,488
intra_resolved                      58,234
null_ref                            22,030
external_resolved                    2,405
external_builtin                        60
intra_MISSING                        3,585
external_object_not_dumped           6,113
external_fileid_out_of_range            61
unresolved_total                     9,759
```

旧报告“未解析 858”是**同一身份/作用域缺陷的产物**：旧 a04 把同一来源的多个
SerializedFile externals 直接拼接，并按 `(source_bundle, pathID)` 建表，因此既漏判又误判。
本次改为**按每个 SerializedFile 自身的 externals 解析 `m_FileID`**，并用结构化遍历替代正则。
`pathID` 为**有符号 int64**（实测 min `-9223201017708124729`、max `9223314696597965781`，
58,748 行中 26,944 行为负），全程以十进制字符串无损传递。

### 7.3 Prefab 校验（a08）

```
prefab containers        156
clean                     80     (零 issue 且恰好 1 个根)
not_clean                 76
multi_root                 5
orphan_component         380
unresolved_local_reference 3
missing_component          3
object_read_error          0
```

旧报告的 **153/156 clean 不成立**。旧校验器的五处缺陷均已修复（JSON 读取失败被替换为 `{}`、
`orphan_go` 算出却不计入、仅按 `pid` 建表、未校验组件回指、`multi_root` 不阻止 clean），
并额外修掉两个**误报**：把 `.png` 等二进制载荷当 JSON 解析（曾产生 1,797 条假 read error）、
把 Material/Shader/AnimationClip 等子资源当组件（曾产生 769 条假 orphan）。

审计点名要求回源检查的三个容器**独立复现**为缺组件：

| 容器 | objects | GameObject | Transform | roots | issues |
|---|---|---|---|---|---|
| `ProvidableDialog.prefab` | 45 | 8 | 8 | 1 | `unresolved_local_reference`, `missing_component` |
| `DebugLabel.prefab` | 3 | 1 | 1 | 1 | `unresolved_local_reference`, `missing_component` |
| `RebuildableToggleLabel.prefab` | 4 | 1 | 1 | 1 | `unresolved_local_reference`, `missing_component` |

五个多根容器：`CreditsUI`(4 根)、`GalleryUI`、`LogUI`、`SaveLoadUI`、`WitchBookUI`(各 2 根)，
其 container target 证据已完整保留于 `prefab_sweep.json`。

### 7.4 Addressables（a07）

```
manifest entries              457
physical bundle files         457   (6,912,804,042 bytes)
physical distinct filenames   450   ⟵ 旧报告 "457 total vs 450 on disk" 的成因
container entries          29,053
distinct keys              28,332
keys with multiple locations  284
container keys matched by dumped objects  28,332 / 28,332   (100%)
container target mismatch                       0
dumped containers missing from manifests         7   (全部是 com.elringus.naninovel 包内 FX 预制)
dependency edges                     95   (resolved 0, distinct targets 9)
```

**457 vs 450 已解释清楚**：磁盘上有 457 个 bundle **路径**，但只有 450 个不同的**文件名**，
旧代码 `disk_bundles.add(f)` 按 basename 去重才得到 450。这是路径身份问题，不是文件缺失。

`catalog.bin` 已用 `tools/audit/catalog_reader.py` 按 `BinaryStorageBuffer v2` 结构解析：
magic `0x0DE38942`、version `2`、header offset 表 36 项、95,027 条可打印字符串、
5 个 provider 类型实测为：
`AssetBundleProvider`、`AtlasSpriteProvider`、`BundledAssetProvider`、
`InstanceProvider`、`SceneProvider`。

**该 Python 读取器不解码 resource locations / GUID / label**，这一点在
`addressables.json → catalog.python_reader.completeness` 中显式声明，
不在输出中冒充完整解析。权威解析由 `UnityProject/Assets/Editor/CatalogDump.cs`
在真实 Addressables 运行时中完成（G3 依赖 G1）。

### 7.5 审计回归测试

`tools/audit/test_audit_regressions.py` — **33 tests, 33 passed, exit 0**。

覆盖审计 §3.2.6 要求的全部用例：同 basename 不同目录、同 pathID 不同文件、缺/坏 JSON、
无根环、孤立 GameObject/Transform、错误组件回指、重复地址/重复 key、int64 边界无损、
二进制载荷不误判、子资源不误判为组件、以及四个审计器的静态防回归守卫。

## 7.6 源码与依赖台账（§3.3.3 / §3.3.9）

`dependency_matrix.csv`（187 行）与 `stub_inventory.csv`（10,971 行）已产出，
枚举方式为 **Mono.Cecil 读取真实 interop 程序集签名**（不靠正则猜 API）。
所有 187 行的 `test_status` 均为 `NOT_RUN`。

### 7.6.1 关键结论：`src/decompiled/` 目前只有 Assembly-CSharp 被重建

```
.cs 文件扫描            1,651
疑似存根                10,971   (42 / 43 个程序集目录)
// RECONSTRUCTED 标记    1,042   全部位于 Assembly-CSharp
其余 42 个程序集目录      0 个已重建方法体
```

因此 `src/decompiled/` 的绝大多数内容仍是 **Il2CppDumper DummyDll 形态**的源码
（带 `[Address(RVA=…)]`、`[Il2CppDummyDll.FieldOffset]`，方法体为空或 `return null/default`）。
按存根类型：`empty-body` 5,991、`return-default` 2,625、`return-null` 1,858、
`interface` 421、`abstract` 72、`constant-return` 4。

阻断启动/游玩的主要存根（按影响排序，逐条附文件:行与原文）：

| # | 成员 | 位置 | 影响 |
|---|---|---|---|
| 1 | `Naninovel.Engine.Initialize` | `Naninovel/Engine.cs:404` | `return default(UniTask)` — Naninovel 整体无法启动 |
| 2 | `Naninovel.Engine.GetService/GetConfiguration/GetServiceOrErr/TryGetService` | `:434,:441,:448,:454,:461,:469` | 服务定位器恒空 |
| 3 | `Naninovel.Engine` 状态访问器 `Types/Configuration/Behaviour/Time` | `:178,:190,:208,:226` | 引擎状态不可用 |
| 4 | `WitchTrials.Views.GameInitializer.InitializeAsync` | `GameInitializer.cs:149` | 由 `Start()` `:137` 以 `.Forget()` 触发，整个异步启动体不执行 |
| 5 | `GameInitializer.Awake/OnDestroy` | `:107,:142` | 与审计 UR-004 一致 |
| 6 | `GigaCreation…AddressablesManager`（9 个方法） | `:461,:469,:477,:485,:492,:499,:513,:520,:526` | 即使装上 Addressables 2.4.6，**没有任何 Addressable 资源能加载** |
| 7 | `Naninovel.ScriptPlayer.Play/PlayRoutine/SelectNextCommand/InitializeService` | `:917,:1000,:1162,:1177` | 对话无法开始或推进 |
| 8 | `WitchTrials.Views.SteamManager.Awake/InitOnPlayMode/Update` | `:67,:61,:140` | 启动场景 Steam 初始化；`GameInitializer:136` 会解引用其 `gameObject` |
| 9 | `EntrySequence.PlayInitialSequenceAsync/EnterTitleScreenAsync` | `:255,:296` | 标题转场被跳过 |
| 10 | `DefaultLocalePresenter.LoadAsync`、`SplashScreen.PlaySequenceAsync` | `:74,:136` | 本地化表从不加载 |

**API 事实纠正（防止凭空发明）**：本构建的 `Naninovel.Engine` 完整方法表（33 个成员）中
**不存在** `GetService<TService>(string)`、`InitializeService`、`AddService`。
`Engine.GetServiceOrErr<TService>()` 是唯一 boot/游玩关键成员（**89 个可执行调用点** + 75 处文档引用），
而 `GetService<TService>()` 有 **0 个**可执行调用点。其真实失败文案从字符串表逐字恢复：
`' engine service: not found or engine is not initialized.`。
`Naninovel` 的 786 个顶层类型**已全部重建**，缺的只是方法体。

### 7.6.2 依赖分类（187 行）

| source_kind | 行数 | implementation_status | 含义 |
|---|---|---|---|
| `unity-module` | 74 | `not-needed` | Editor 自带模块，随平台支持安装，无需下载 |
| `upm-package` | 32 | `not-verified` | 已在 `manifest.json` 声明；装配体需许可证解析后才出现 |
| `first-party-reconstructed` | 38 | `stub` / `missing` | `src/decompiled/` 内自研代码 |
| `third-party-package` | 20 | `missing` | 13 可获取 + 7 需用户提供 |
| `il2cpp-interop-proxy` | 0（已并入） | — | `DummyDll`(128) / `interop`(157) **不作为新 Player 运行时实现** |
| `system` | 14 | `not-needed` | IL2CPP/BCL 运行时程序集 |
| `unknown` | 9 | 混合 | 未能确定来源 |

### 7.6.3 需用户提供、我不得代取/代造的商业包（BLOCKED）

| 包 | 证据程序集 | 状态 |
|---|---|---|
| **Naninovel** | `Elringus.Naninovel.Runtime`、`Naninovel.Common`（清单 `2025.3.19.1546`） | BLOCKED — 商业包；8,006 条存根 |
| LeTai.TrueShadow | `LeTai.TrueShadow`（388 个序列化实例，无源码） | BLOCKED |
| MPUIKit | `MPUIKit` | BLOCKED |
| UIPolygon | `UIPolygon` | BLOCKED |
| ppt-noise | `ppt-noise` | BLOCKED |
| FastEnum | `FastEnum` | BLOCKED |

另有 `GigaCreation.Tools.CsvParser`、`MasyoLab-FavoritesAssetExamples` 出现在
`ScriptingAssemblies.json` 但在 interop 与 `src/decompiled/` 中均不存在，来源无法确定；
`JetBrains.Annotations` 被 115/1,651 个文件使用但未随构建分发，属**编译阻断**项。
完整清单见 `out/dependency_blocked.csv`（39 行）。

## 8. 已交付的工具与脚本

| 文件 | 用途 |
|---|---|
| `tools/audit/config.py` | 统一路径解析（env / local json / versioned json），无 E: 硬编码 |
| `tools/audit/identity.py` | `(source, SerializedFile, signed int64 pathID)` 身份模型与稳定 GUID |
| `tools/audit/common.py` | 索引台账（重复 key、缺失 dump、未归属行全部上报） |
| `tools/audit/a03_sources.py` / `a03_worker.py` | 全源扫描；保留 container 目标 PPtr / preload |
| `tools/audit/a04_refs.py` | 按 SerializedFile 作用域的引用普查（结构化遍历） |
| `tools/audit/a07_addressables.py` | key→locations 多值模型、按路径对账 bundle、catalog 解析 |
| `tools/audit/a08_prefab_sweep.py` | 全量 prefab 层级校验（含回指、孤立、多根证据） |
| `tools/audit/a09_object_ledger.py` | 全量对象/资产/源/类型台账 |
| `tools/audit/catalog_reader.py` | `BinaryStorageBuffer v2` 读取器（明确标注未解析部分） |
| `tools/audit/test_audit_regressions.py` | 33 条回归测试 |
| `tools/unity-rebuild/preflight.py` | 输入预检 + manifest + blocked 清单 |
| `tools/unity-rebuild/common.ps1` | 批处理公共配置（Unity/Python/路径解析、许可证探测） |
| `tools/unity-rebuild/g1_compile.ps1` | G0+G1 门脚本（真实 Unity 编译，许可证缺失即 BLOCKED 退出码 2） |
| `UnityProject/Packages/manifest.json` | 真实包清单（版本全部有证据） |
| `UnityProject/ProjectSettings/ProjectVersion.txt` | `6000.0.48f1 (170d2541580d)` |
| `UnityProject/Assets/Editor/CatalogDump.cs` | 权威 catalog dump（真实 Addressables 运行时） |
| `UnityProject/Assets/Editor/CompileGate.cs` | G1 编译门（记录程序集图与配置） |

## 9. 待补齐清单（下一步）

1. ~~**激活 Unity 许可证**（用户动作）→ G1 起全部解锁。~~ **已完成（2026-09-17）**：entitlement 许可证有效（到期 2027-09-17），G1 见 §11。
2. ~~安装 `windows-il2cpp` 模块 → G5。~~ **已安装**（`PlaybackEngines/windowsstandalonesupport/Variations/win64_player_*_il2cpp` 存在）。
3. ~~运行 `g1_compile.ps1`。~~ **G1 PASS（2026-09-17）**：43 个程序集、1,610 个源文件在真实 Unity 中 0 error / 0 warning，见 §11。
4. `a13` 转换器：把对象图写成真实 Editor 资产，建立 `GUID + localFileID` 映射并两阶段绑定。
5. 实现 Naninovel `Engine`、GigaCreation `AddressablesManager`、`GameInitializer` 运行时。
6. 逐类别（音频/图片/Sprite/图集/字体/视频/剧本/Timeline/Shader/网格/粒子/Mixer）验收。
7. 运行 G2–G6 并保存真实日志与覆盖率。

## 10. 本文件未声称的内容

- 未声称源码能由 Unity 编译（G1 BLOCKED）。
- 未声称任何资源已成功导入、绑定或加载（G2 NOT_RUN）。
- 未声称 catalog / GUID / label / provider 已完整校验（G3 NOT_RUN）。
- 未声称 Build 与核心流程可运行（G4/G5 NOT_RUN）。
- 未把 `dotnet build`、路径命中率或抽样结果当作上述任一门的证明。

---

## 11. 2026-09-17 记录：G1 通过（游戏级）

真实 Unity 6000.0.48f1 编译门 `tools/unity-rebuild/g1_compile.ps1` **PASS**：
`cs_errors=0 cs_warnings=0 import_errors=0 exit=0`（`out/unity/g1_compile.json`），
Unity 侧 `out/unity/compile_result.json` 记录 **75** 个 player 程序集，
其中 43 个游戏程序集全部产出 `Library/ScriptAssemblies/<Asm>.dll`（43/43）。

此前 `src/decompiled/` 与 `UnityProject/` 之间没有代码通路（只有 2 个 Editor 脚本），
本轮把 43 个程序集的转换后源码真正纳入工程。

### 11.1 产物

| 产物 | 说明 |
|---|---|
| `tools/unity-rebuild/dump_dummy_refs.ps1` | Mono.Cecil 读 Il2CppDumper 骨架：43 个程序集的元数据引用图 + 128 个骨架 DLL 的命名空间 |
| `tools/unity-rebuild/apply_game_sources.py` | `converted/src/decompiled/<Asm>/` → Unity 程序集（源码 + `<Asm>.asmdef` + 骨架插件） |
| `UnityProject/Assets/Scripts/Game/<Asm>/` | 43 个 asmdef、1,610 个 `.cs` |
| `UnityProject/Assets/Plugins/Il2CppStubs/` | 9 个编译期骨架 DLL：FastEnum、LeTai.TrueShadow、LitMotion、LitMotion.Extensions、MPUIKit、UniRx、UniTask、UniTask.Addressables、com.rlabrecque.steamworks.net |
| `out/unity/game_sources_manifest.json` | 逐程序集的引用/命名空间/补丁/未解析项台账 |
| `out/unity/assembly_graph_recon.json`、`cycle_type_refs.json` | 引用图与成环证据 |

### 11.2 关键判定（每条都有证据）

1. **程序集图无环**：`projects.json` 中 `Elringus.Naninovel.Runtime → Assembly-CSharp` 等 8 条边在骨架元数据里**没有任何类型引用**（Cecil 实测 0 条）。按元数据为主、命名空间为辅建图后，Unity 的
   "One or more cyclic dependencies detected between assemblies" 消失；3 条命名空间推断边为保无环被丢弃，逐条记在 manifest 的 `edges_skipped_for_acyclicity`。
2. `Assembly-CSharp` → **`WitchTrials.Game`**：避免与 Unity 预定义程序集同名，名字取自 `project/compile_groups.json` 的编译组名。
3. **排除 41 个 `UnitySourceGeneratedAssemblyMonoScriptTypes_v1.cs`**：Unity 会为每个程序集重新生成；保留反编译副本曾造成 210 个 CS0101/CS0111/CS0579。
4. **107 处空结构体构造函数补 `this = default;`**：IL2CPP 不保留托管方法体，反编译出的结构体构造函数是空体，C# 9 报 CS0171（C# 11 才自动置默认值）。逐条记在 manifest 的 `csharp9_source_patches`。
5. 109 个文件加 `#nullable enable annotations`：原程序集在可空注解上下文下编译，反编译只留下 `string?`，C# 9 报 CS8632；只开注解上下文，不会引入新的可空警告。
6. **引用解析以元数据为主、`using` 为补充**：`FastEnumUtility`、`LitMotion.Extensions`、`ppt-noise`、`GigaCreation.{Essentials.Platforms, Tools.UndoExtensions, Tools.Debugging.Core, Tools.Private.PlayableDirectors}` 只出现在 `using` 里，元数据图无此边；其中 3 个还藏在**文件首行 UTF-8 BOM 之后**（`^\s*using` 匹配不到，曾静默丢失每个文件的第一条 using）。
7. 两条按证据补充的隐式引用：`Unity.Addressables` → `Unity.ResourceManager`（Addressables 公开 API 暴露 `AsyncOperationHandle`，否则 CS0012）；`UniTask.dll + Unity.Addressables` → `UniTask.Addressables.dll`（`AsyncOperationHandle.ToUniTask` 扩展方法在后者，否则 CS1929）。
8. `Newtonsoft.Json` 引用包内真实实现，**不复制骨架**（复制会在运行时遮蔽 `com.unity.nuget.newtonsoft-json`）。

### 11.3 本轮修掉的两个门脚本缺陷（`tools/unity-rebuild/common.ps1`）

* 许可证探测把 `No ULF license found` 当致命错误；那是许可证客户端回退到 entitlement 许可过程中的中间信息，导致**有有效许可证时 G1 仍报 BLOCKED**。改为先看正面证据（`Serial number assigned to` / `Successfully resolved entitlement details`）。
* Unity 写到 stderr 的正常输出（如 `debugger-agent: Unable to listen on 3200`）在 `$ErrorActionPreference='Stop'` 下变成终止错误，让门在编译开始前就死掉。现在原生 stderr 只入日志。

### 11.4 仍未通过的门

| 门 | 状态 | 缺口 |
|---|---|---|
| G2 全资源导入 | NOT_RUN | 58,750 个对象还没有转换器写进 `Assets/`（a13） |
| G3 内容构建 | NOT_RUN | 无 Addressables settings/groups/labels，也未做新内容构建 |
| G4 Play Mode | NOT_RUN | `EditorBuildSettings.m_Scenes` 为空，Boot 链未实现 |
| G5 独立 Player | NOT_RUN | 未构建、未运行 |
| G6 全内容覆盖 | NOT_RUN | — |

**G1 的 “0 error” 不等于可运行**：上表 9 个骨架插件只有类型与签名、没有方法体；
`src/decompiled` 里仍有 10,971 个存根方法体；`AOT`、`Steamworks` 两个命名空间未在台账中解析
（由已引用的骨架 DLL 提供，编译通过）。Naninovel 商业包仍未取得（其源码在 `src/decompiled/`）。

### 11.5 复现

```powershell
pwsh -File tools/unity-rebuild/dump_dummy_refs.ps1      # 元数据引用图（Mono.Cecil，不需要 Unity）
python tools/unity-rebuild/apply_game_sources.py        # 源码 -> Assets/Scripts/Game + asmdef + 骨架插件
$env:MANOSABA_UNITY_EDITOR='<Unity.exe>'
pwsh -File tools/unity-rebuild/g1_compile.ps1           # 真实 Unity 编译门：PASS = exit 0
```

---

## 12. 2026-09-17 记录：独立 Player 构建并实跑（G5 基础设施）

新门 `tools/unity-rebuild/g5_player_smoke.ps1`（**PASS**）做三件事并各自留证：

1. **bootstrap**（`Manosaba.EditorTools.ProjectBootstrap`）：由 Unity 自己生成 `Assets/Scenes/Boot.unity` 并写进 Build Settings（手写 `.unity` 就得猜每个内置组件的序列化字段，所以交给 `EditorSceneManager`）。
2. **build**（`Manosaba.EditorTools.BuildPlayer`）：`dist/player/manosaba-rebuild.exe`，Mono / Development，187 MB，21–72 s。
3. **run**：`-batchmode -nographics -manosabaSmoke -manosabaEvidence <json>`，Player 自己写证据并退出。

### 12.1 Player 内实测（`out/unity/player_smoke_evidence.json`）

| 项目 | 值 |
|---|---|
| 进程 | `WindowsPlayer`，`is_editor=False`，exit 0 |
| 载入程序集 | 164 个，其中**重建的 43 个全部在内**（`Elringus.Naninovel.Runtime` 1,597 类型、`WitchTrials.Game`、`GigaCreation.*`、`ppt-noise`、`Naninovel.Common` …） |
| 关键类型解析 | 5/5：`WitchTrials.Views.GameInitializer`(87 方法)、`WitchTrials.Debugging.DebugOptions`(92)、`WitchTrials.Models.TrialStateManager`(16)、`GigaCreation.Essentials.AddressablesUtils.AddressablesManager`(22)、`Naninovel.Engine`(59) |
| 发行版 catalog | **PARSED**：`keys=57,212`，与 Editor 侧 `CatalogDump` 数字**完全一致** |

也就是说：**这个工程现在能编译、能打包、能启动，并且启动后的 Player 能用真正的 Addressables 运行时读发行版 catalog。** 但游戏流程仍未运行 —— `GameInitializer` 等方法体还是 IL2CPP 存根。

### 12.2 发行版运行时数据接入工程

`tools/unity-rebuild/apply_shipped_aa.ps1` 把 `manosaba_Data/StreamingAssets/aa/{catalog.bin, catalog.hash, settings.json}`
（5,551,003 + 32 + 864 B）复制进 `UnityProject/Assets/StreamingAssets/aa/`，Unity 会原样打进 Player。
**457 个内容 bundle（6.4 GB）刻意不复制**：那是 G2/G3 内容构建的产物，复制进来工程就不可复现了。
该目录已加入 `.gitignore`（游戏内容，不入库）。

### 12.3 权威 catalog dump 产出（审计 §7.1）

`Manosaba.EditorTools.CatalogDump` 在真实运行时里跑通，写出 `out/unity/catalog_dump.json`（50 MB）：

```
load_route   ContentCatalogData.LoadFromFile + CreateCustomLocator (runtime internals)
catalog      5,551,003 B  sha256 f8f7527e0582876f…  Addressables 2.4.6
keys         57,212   （其中 32 位十六进制 GUID 形 28,332 —— 正是 a07 早前记的「28,332 个容器路径」）
locations    29,097   key→location 边 86,613   依赖边 30,147
keys_without_locations 0    非字符串 key 0    空 location 0    keys_with_multiple_locations 647
provider     AssetBundleProvider 457（= bundle 数）、BundledAssetProvider 28,640
type         AudioClip 26,537（与 res/ 音频文件数一致）、Sprite 536、TextAsset 448、Texture2D 442、
             Naninovel.Script 440、GameObject 156、UAB 457、SpriteAtlas 16、Material 23 …共 16 种
build_result_hash  空（发行版 catalog 头里没有这一项，已记录为事实而非缺失）
```

`a07_addressables.py` 现在读到它：`addressables.json → catalog.unity_runtime_dump_present = true`。

### 12.4 本轮修掉的缺陷

* `tools/audit/config.py`：`audit.config.json` 的 `output_root` 是**相对路径**，此前未相对 repo 根解析 —— 从 `tools/audit` 里运行 a07 时它会去找 `tools/audit/tools/unity-rebuild/out/unity/catalog_dump.json`，于是**静默报告“没有 Unity dump”**。现在所有配置路径统一用 `_repo_relative()` 相对 `REPO_ROOT` 解析（cwd 无关）。
* `BuildPlayer.cs`：嵌套类 `BuildResult` 遮蔽了 `UnityEditor.Build.Reporting.BuildResult`（CS0117 `'BuildResult' does not contain a definition for 'Succeeded'`）。已改名 `PlayerBuildResult` 并显式限定枚举。
* 回归：`tools/audit/test_audit_regressions.py` **72 tests OK**。

### 12.5 仍未达成

| 门 | 状态 | 缺口 |
|---|---|---|
| G2 全资源导入 | NOT_RUN | 58,750 个对象仍没有转换器写进 `Assets/`（a13） |
| G3 内容构建 | NOT_RUN | 无 Addressables settings/groups/labels；457 个 bundle 未随工程构建 |
| G4 Play Mode | NOT_RUN | Boot 场景只有集成层 boot 组件；`GameInitializer` 等 boot 链方法体是存根 |
| G5 独立 Player | 部分 | 构建 + 启动 + catalog 解析均实测；IL2CPP、资源加载、场景流程未验证 |
| G6 全内容覆盖 | NOT_RUN | — |

### 12.6 环境注意

本机存在**两份同版本 Unity**：`E:\AI\6000.0.48f1\Editor` 与 `E:\Unity\6000.0.48f1\Editor`
（都装了 Windows standalone + IL2CPP）。`common.ps1` 的探测顺序会选中 `E:\Unity`；
要固定用哪一份，设 `MANOSABA_UNITY_EDITOR` 或在 `tools/audit/audit.config.json` 里写 `unity_editor_path`。

---

## 13. 2026-09-17 记录：G2 第一批资源导入通过（叶子资源）

新门 `tools/unity-rebuild/g2_import_gate.ps1`（**PASS**，检查模式见 13.6）：Unity 跑两次（第二次是全新进程），
逐个 `AssetDatabase.LoadMainAssetAtPath` 解析导入清单，并比对两次的 GUID 图 —— 一致才算通过
（对应审计 G2 的「保存、关闭、重新打开后再验证」）。

```
requested 1,369   found 1,369/1,369   missing 0   guid 变更 0
类型: TextAsset 454 | Texture2D 906 | Font 5 | VideoClip 4     源文件 4.23 GB（磁盘 4.03 GB）
```

### 13.1 逐对象台账（审计 G2 的核心要求）

`tools/unity-rebuild/a13_import_assets.py` 走完 `res/_index.csv` 的**全部 58,748 行**，
每行都落一个状态到 `out/unity/a13_import_ledger.csv`，**UNCLASSIFIED = 0**：

| 状态 | 行数 | 含义 |
|---|---|---|
| `IMPORTED` | 1,369 | 叶子资源：一个提取文件 == 一个 Unity 资产（text/font/texture/video） |
| `SKIPPED_CATEGORY` | 26,538 | 音频：本轮未选（原因见 13.3），已记账 |
| `DEFERRED_STRUCTURAL` | 18,355 | prefab/scene 图内部对象（GameObject/Transform/CanvasRenderer…），需要 a13b 对象图转换 |
| `DEFERRED_COMPOSITE` | 9,302 | 子资源或复合资源：Sprite（2,396，需要所属贴图 + rect/pivot/border）、MonoBehaviour（5,903，需要脚本 GUID 绑定）、AnimatorController/AnimationClip/Material/SpriteAtlas/Mesh/AudioMixer… |
| `REFERENCE_ONLY` | 3,045 | MonoScript 表：不是可导入资产，但它是两阶段绑定所需的「原始 GUID ↔ 类型」映射 |
| `UNSUPPORTED` | 134 | Shader/ComputeShader：发行版里只有编译后的 GPU 程序，没有源码可导入 |
| `NOT_IMPORTABLE` | 5 | PreloadData/LightmapSettings/NavMeshSettings/RenderSettings：随场景重建，不是资产 |
| `SOURCE_MISSING` | 1 | 一条 AudioClip 行对应的提取文件不存在（已记录具体行） |

### 13.2 两阶段绑定的另一半已就绪

`tools/unity-rebuild/a13_script_index.py` 扫出 **1,616 个重建脚本 / 2,797 个类型 + 各自的 .meta GUID**
（`out/unity/reconstructed_script_index.json`），并与发行版里真实存在的 2,869 个 MonoScript 对账：

| 结果 | 数量 | 说明 |
|---|---|---|
| `EXACT`（assembly+namespace+class 全中） | **1,080** | 其中 `Assembly-CSharp` **193/193**、`Elringus.Naninovel.Runtime` **645/645**、`GigaCreation.*` 全部命中 |
| `LOOSE_NAME_MATCH` | 112 | 类名存在但命名空间/程序集不同（`CustomInspector` 68 个全部如此，待查） |
| `NOT_IN_RECONSTRUCTION` | 1,677 | 绝大多数是 Unity 包与第三方库（InputSystem 365、URP 205+153、Steamworks 101、UniRx 93、UGUI 78…），它们由包/骨架提供，本来就不需要重建 |

对账结果在 `out/unity/monoscript_coverage.csv`。复合资源（Sprite/MonoBehaviour/SO/prefab）
的 `m_Script`/PPtr 绑定链已确认可解：对象 dump 里的引用是 `(m_FileID, m_PathID)`，而
MonoScript 的 class/namespace/assembly 可从 `res/_monoscripts.csv`（globalgamemanagers 表）
与各 bundle 的 MonoScript 对象取得，再由本索引映射到我们的 GUID。

### 13.3 音频类别：本轮实测后**刻意推迟**

| 事实 | 数值 |
|---|---|
| 行数 / 体量 | 26,538 行 / 1.46 GB（`AudioClip 26,537` 可导入 + 1 条 `SOURCE_MISSING`） |
| 复制耗时 | 57 s（已实测，非瓶颈） |
| Unity 导入速率 | **2.4 个/秒**（1 小时内导入 8,562 个）→ 全量外推 **≈2.2 小时** |
| 默认导入设置的问题 | `DecompressOnLoad + preload`：26k 片段会在加载时全部解压进内存；且默认会**重编码**已发行版 Vorbis 流 |

因此中止了那次导入并把音频源文件从 `Assets/GameContent` 移除（保持「要么全导入、要么不导入」的
一致性），同时新增 `UnityProject/Assets/Editor/AudioImportPolicy.cs`：>300 KB 走 Streaming/Vorbis（不重编码），
其余 CompressedInMemory/Vorbis q=0.7，采样率保持原样。下一轮用这套策略一次性导入整个音频类别。
本轮结束时 `GameContent` 停在已验证的 1,369 个资产（2,885 个文件含 .meta，4.0 GB），可用空间 29.7 GB。

### 13.4 本轮产物

| 文件 | 作用 |
|---|---|
| `tools/unity-rebuild/a13_import_assets.py` | 逐对象转换 + 台账（分类、状态、原因、目标路径、目标字节数） |
| `tools/unity-rebuild/a13_script_index.py` | 重建脚本 GUID 索引 + 发行版 MonoScript 覆盖率对账 |
| `tools/unity-rebuild/g2_import_gate.ps1` | G2 门（两次 Unity 运行 + GUID 稳定性比对） |
| `UnityProject/Assets/Editor/ImportVerifier.cs` | 编辑器侧逐路径解析与 GUID 图导出（含可序列化字典） |
| `UnityProject/Assets/Editor/AudioImportPolicy.cs` | 音频导入策略（见 13.3） |
| `UnityProject/Assets/GameContent/**` | 导入的资产（**已 gitignore**，可由 res/ + bundle 重新生成） |

### 13.5 下一步

1. 音频类别按 13.3 的策略一次性导入（专用长任务），台账状态由 `SKIPPED_CATEGORY` 改为 `IMPORTED`。
2. Sprite/图集转换（贴图 + rect/pivot/border，或重建 sprite sheet meta）。
3. ScriptableObject 转换（Naninovel 剧本 440 + 游戏数据 8），使用 13.2 的绑定链。
4. prefab/scene 对象图（`DEFERRED_STRUCTURAL` 18,355 行），最后是 Shader 恢复评估。

### 13.6 本轮发现并修掉的「假 PASS」（验证完整性）

加入 `AudioImportPolicy.cs` 后，G1 报出 2 条真实编译错误
（`AudioImporter.preloadAudioData` 在 Unity 6 已改为 error 级过时：CS0619，应使用
`AudioImportSettings.preloadAudioData`）。**但同一时间 G2 门报了 PASS** —— 原因是：

* Unity 因编译错误中止，`-executeMethod` 根本没执行；
* 门脚本却读到了**上一次成功运行留下的** `import_verify.json`（时间戳 13:14:52，早于本次运行）。

也就是说：**「产物存在」被当成了「本次运行成功」**。已修两处：

1. `AudioImportPolicy.cs` 改用 `settings.preloadAudioData`（G1 恢复 0 error）。
2. `g2_import_gate.ps1` 每次运行前**先删除**产物，运行后同时检查
   `exit_code == 0`、`error CS` 计数为 0、未出现 `Aborting batchmode`、产物时间戳晚于本次启动；
   任一条不满足即 FAIL 并把原因写进 `blocked_reason`。

修复后重新实测（`out/unity/g2_import.json`，两次运行均为 `exit=0 cs_errors=0 aborted=False artifact_fresh=True`）：

```
G1 PASS   cs_errors=0 cs_warnings=0 exit=0
G2 PASS   run A/B: found 1,369/1,369  missing 0  GUID 变更 0
```

同类风险已在 G1 门里确认不存在（`g1_compile.ps1` 每次从 `-logFile` 重新计数并在 `log_bytes` 里
记录日志长度；不过它同样没有「产物新鲜度」概念 —— 后续给 G3/G4/G5 写门时会统一采用本次的检查模式）。

另外补齐 `.gitignore`：`/UnityProject/Assets/GameContent/`（导入的游戏资产，可由 res/ + bundle 再生）
此前**漏掉**，2,883 个导入文件+meta 一度处于「会被提交」状态；`git check-ignore` 已验证现在被忽略。

---

## 14. 2026-09-17 记录：音频全量导入 + 复合资源绑定的打通

### 14.1 音频：先量化，再全量导入

上一轮把音频推迟时给的理由是「默认导入器要重编码已发行版 Vorbis」。本轮先做了对照实验
（`MANOSABA_AUDIO_POLICY` 可切换策略，320 个片段样本，含 12 个 >300 KB 的 BGM）：

| 策略 | 每文件耗时 | 26,537 个外推 |
|---|---|---|
| `mixed`（>300 KB 走 Streaming，其余 CompressedInMemory/Vorbis q=0.7） | 0.29 s | ≈2.1 h |
| `streaming`（全部 Streaming，不做任何重编码） | 0.288 s | ≈2.1 h |

**结论修正**：重编码不是瓶颈，瓶颈是每个文件约 0.29 s 的导入开销（解码/预览波形/Library 记账），
换 load type 不会更快。于是不再折腾策略，直接按 `mixed` 全量导入（对 26k 语音用 CompressedInMemory、
对 74 个长音频用 Streaming，运行时内存与流式语义都合理）。实测在线速率 6.0–6.9 个/秒。

### 14.2 复合资源的绑定链已打通（+440 剧本 +8 数据）

上一轮留的问题是「bundle 里的 `m_Script` 是 `(m_FileID, m_PathID)`，怎么变成我们的脚本 GUID」。
本轮用 UnityPy 把这条链走通了，并写成工具 `tools/unity-rebuild/a13b_script_binding.py`：

1. **CAB → bundle 索引**：来自审计的 `out/a03/per_source/*.json`（`object_counts_by_file` 的键就是
   CAB 名，`rel` 给出文件），共 457 条 —— 注意 `rel` 是相对 `manosaba_Data` 而不是游戏根目录。
2. **MonoScript 表**：被引用的 CAB 所在的 bundle 只打开一次（缓存）。数据包的 `m_Script`
   指向 `archive:/CAB-453800d550384885247ea13fa1524373/...`，即 `bc78c2a5…_monoscripts.bundle`
   里那张 211 条的 MonoScript 表。
3. **类型 → 我们的 GUID**：接 `out/unity/reconstructed_script_index.json`（13.2 节）。

实测结果（累积表 `out/unity/bundle_script_map.json`）：

| 家族 | 对象数 | 结果 |
|---|---|---|
| 游戏数据（`general-data`） | 8 | **8/8 bound** → `WitchTrials.Models.{MapData, CharacterData, ClueData, NoteData, AuthorData, ProfileData, RuleData, SpecialThanksData}` |
| Naninovel 剧本（`naninovel-scripts`，24 个 bundle） | 440 | **440/440 bound** → `Naninovel.Script` |

### 14.3 ScriptableObject 转换器（数据族 8 个已可跑，剧本族 440 个已排队）

* `tools/unity-rebuild/a13c_plan_scriptableobjects.py`：按家族生成作业清单
  （`so_convert_plan_data.json` 8 条、`so_convert_plan_scenarios.json` 440 条），
  每条含 dump 路径、目标资产路径、类型/程序集、以及**发行版容器路径**（G3 要用的 Addressables 地址）。
  数据族 8 个 dump 里除 `m_Script`/`m_GameObject` 外**没有字段级 PPtr**，所以可以一次性完整还原。
* `UnityProject/Deferred/ScriptableObjectConverter.cs`（待导入完成后移入 `Assets/Editor/`）：
  `CreateInstance(type)` → 用 `SerializedObject` 按**序列化字段名**逐字段写入（数组/嵌套/枚举都有分支）
  → `AssetDatabase.CreateAsset`。无法落位的值一律上报：非空 PPtr 记为 `needs_binding`（第二阶段），
  未知字段/不支持类型/越界枚举记为 warning，绝不静默丢弃。

### 14.4 Sprite 转换

`res/` 里 Sprite 的像素被导出成单个裁剪图（`<贴图>.sprite~N.png`），但**没有导出 rect/pivot/border**，
所以还不能重建「Sprite 作为 Texture2D 子资产」的原始形态。本轮的做法：
`tools/unity-rebuild/a13_import_assets.py` 新增 `sprite` 类别，把每个裁剪图按**原始 Sprite 名**写进
`<贴图>.sprites/`，配合 `SpriteImportPolicy.cs`（`textureType = Sprite`、Single、100 PPU、中心轴）
导入为独立 Sprite 资产；轴心/PPU/图集打包属于**已记录的偏差**，不是复原值。

### 14.5 验证口径的两处加固

* `ImportVerifier` 现在读取 `out/unity/` 下**所有** `*_imported_paths*.txt`（叶资源 30,301 + 数据族 8 +
  剧本族 440），并对全部清单内容做一次合并指纹；G2 门额外校验两次运行的指纹一致 —— 否则「两次跑了
  不同清单」会被误判为一次通过的验证。
* 文件轮转纪律：a13b 的绑定表改为**累积**（`bundle_script_map.json`），家族可以分次解析而不会互相覆盖；
  规划产物按家族分名（`so_convert_plan_<family>.json`），避免后一次规划覆盖前一次。

### 14.6 本轮结果

```
G2 PASS   run A/B: found 30,749/30,749   missing 0   GUID 变更 0
```

按 Unity 实际加载到的类型统计（`out/unity/import_verify.json`）：

| 类型 | 数量 | 说明 |
|---|---|---|
| `AudioClip` | **26,537** | 全量语音/BGM/音效，`mixed` 策略导入 |
| `Texture2D` | **3,301** | 906 张贴图 + **2,395 个 Sprite**（`SpriteImportPolicy` 使其以 `textureType=8` 导入） |
| `TextAsset` | 454 | Naninovel 本地化与托管文本 |
| `Script` | **440** | Naninovel 剧本（`Naninovel.Script`，按 14.2 的绑定链还原脚本引用） |
| `Font` | 5 | |
| `VideoClip` | 4 | 片头影片 |
| 游戏数据 SO | 8 | `AuthorData`/`CharacterData`/`ClueData`/`MapData`/`NoteData`/`ProfileData`/`RuleData`/`SpecialThanksData`，Unity 以**真实类型**加载 = 类型绑定成立 |

逐对象台账（`out/unity/a13_import_ledger.csv`，58,748 行全部有状态，`UNCLASSIFIED = 0`）：
`IMPORTED 30,301`（9.61 GB）、`DEFERRED_COMPOSITE 6,906`、`DEFERRED_STRUCTURAL 18,355`、
`REFERENCE_ONLY 3,045`、`UNSUPPORTED 134`、`NOT_IMPORTABLE 5`、`DUPLICATE_DESTINATION 1`、`SOURCE_MISSING 1`。

### 14.7 本轮暴露的两个真实缺陷（都已修）

1. **导入验证会把编辑器打崩**：第一次对 30,749 个路径逐个 `LoadMainAssetAtPath` 时，Unity 以
   `0xC0000005`（访问违例）退出——上一轮 27,906 个路径时只是擦边通过。原因是这些资产（906 张大贴图、
   2,395 个 sprite 贴图）被逐一载入后常驻内存。修法：每 500 个调用一次
   `EditorUtility.UnloadUnusedAssetsImmediate()` 并打印进度（进度行也是把崩溃定位到区间的证据）。
2. **字符串字段被当成数组**：`SerializedProperty.isArray` 对 `string` 返回 **true**，转换器先判数组
   导致每个字符串字段都走了数组分支、被记 warning 后留空（首轮 386,922 条 warning + 文本全空）。
   修法：`SerializedPropertyType.String` 必须先判。修后数据族 8 个资产 **0 warning**，文本内容与发行版一致
   （逐条对照 `_id: Ema`、`_text: "<color=%COLOR%>…桜…"`）。

### 14.8 已知未完成（诚实记录）

* **440 个剧本资产的命令体没有还原**：Naninovel 的 `lines`/`playlist` 是 `[SerializeReference]` 多态对象图，
  扁平化的 typetree JSON 不带每个元素的**具体类型**，所以转换器把 304,280 个字段留成默认值并标为
  `CONVERTED_PARTIAL`（不是 `CONVERTED`）。要真正还原剧本内容，需要额外一趟 UnityPy 读取，导出 managed
  reference 的具体类型名，再用 `SerializedProperty.managedReferenceValue` 构造实例。这是下一轮的首要目标。
* `DEFERRED_STRUCTURAL 18,355`（prefab/scene 对象图）与 `UNSUPPORTED 134`（Shader）仍未处理。
* 磁盘：`Assets` 9.65 GB、`Library` 11.27 GB、E: 剩余 16.6 GB。

---

## 15. 2026-09-17 记录：440 个剧本完整还原（[SerializeReference] 多态图）

上一轮把 440 个 Naninovel 剧本标成 `CONVERTED_PARTIAL`（304,280 个 `[SerializeReference]` 字段留空）。
本轮把它做完了，**并给出了逐类的计数对账**。

### 15.1 类型信息的来源（不是新提取，一直在 dump 里）

`tools/unity-rebuild/a13d_probe_managed_refs.py` 探明：UnityPy 导出的扁平 typetree 里有一个
`references` 侧表，正是 Unity 自己序列化多态引用的形式：

```json
"lines": [ {"rid": 1263}, ... ],
"references": { "version": 2, "RefIds": [
    { "rid": 1000,
      "type": {"class": "BeginIf", "ns": "Naninovel.Commands", "asm": "Elringus.Naninovel.Runtime"},
      "data": { ... } } ] }
```

所以**不需要新的提取**：字段里的 `{"rid": N}` 是占位，具体类型与数据都在 `references.RefIds` 里
（SerializedFile 的 `ref_types` 表也印证了这一点：`m_ClassName`/`m_NameSpace`/`m_AssemblyName`）。
上一轮那条 `no serialised field for 'references'` 警告就是这个侧表被当成普通字段的证据。

### 15.2 转换器改动（`ScriptableObjectConverter.cs`）

1. **按层批量构造**：赋值 `managedReferenceValue` 会改变属性形状，子字段要等
   `ApplyModifiedProperties` 之后才存在。最初「每个引用一次 Apply」在 304,280 个引用上超过 10 分钟仍未完成；
   改成**按层**（赋值一层 → Apply → 写这一层的数据 → 下一层）后，440 个资产 **230 秒**跑完。
   全程只用 `ApplyModifiedPropertiesWithoutUndo()`，绝不用 `Update()`（后者会丢弃上一阶段写入的值）。
2. **实例分配回退链**：反编译类常常只有带参构造函数（例如 `CommandScriptLine(Command,int,int,string)`），
   `Activator.CreateInstance` 抛 `MissingMethodException` —— 首轮有 **192,280 个引用**因此失败。
   现在依次尝试 `Activator.CreateInstance` → `Activator.CreateInstance(type, nonPublic: true)` →
   `FormatterServices.GetUninitializedObject`（Unity 反序列化本身也不需要构造函数，字段随后由序列化器写入）。

### 15.3 结果与对账

```
status = OK         complete = 448   partial = 0   failed = 0   warnings = 0
managed references: total 304,280   applied 304,280   failed 0
G2 PASS   run A/B: found 30,749/30,749   missing 0   GUID 变更 0
```

**逐类计数对账**（dump 侧 440 个 `.asset.json` vs 转换后的 440 个 `.asset`）：

| 项 | dump | 转换后 |
|---|---|---|
| 不同的 managed-ref 类 | 95 | 95 |
| 引用总数 | 304,280 | 304,280 |
| 计数不一致的类 | — | **0** |

主要命令类（两侧完全一致）：`CommandScriptLine` 80,446、`EmptyScriptLine` 62,434、
`PrintTextModified` 31,575、`GenericTextScriptLine` 31,554、`ModifyCharacterExtended` 22,880、
`GosubToModifyPerspectiveStageCamera` 16,542、`CommentScriptLine` 15,322、`Wait` 3,946、
`ModifyBackgroundExtended` 3,496、`GotoModified` 3,158、`Gosub` 3,110、`PlaySfxModified` 2,621。

注意 Unity 保存时会**重编 rid**（资产里是 `rid: 498527187…` 这类哈希值），所以对账用类计数而不是 rid 值；
每个脚本资产里 `- rid:` 条目与字段占位数量一致（例如 Adv01：245 条 vs 244 个占位 + 1 个 Unity 的空槽）。

剧本资产合计 440 个、**323.8 MB**（平均 753 KB）；对白文本走 `textMap.idToText`（已随资产写入）。

### 15.4 下一步

1. `DEFERRED_STRUCTURAL 18,355`：prefab/scene 对象图（GameObject/Transform/CanvasRenderer…），
   最大的一块，需要沿用本次的绑定链把组件脚本引用接上。
2. `UNSUPPORTED 134`：Shader 恢复评估（发行版只有编译后的 GPU 程序）。
3. 之后才是 G3 内容构建与 G4 Play Mode。

---

## 16. 2026-09-17 记录：prefab 对象图重建（156/156）

上一轮列的「最大一块」本轮做出来了：**156 个 prefab 全部重建**，并且纳入 G2 门的验证范围。

```
G2 PASS   run A/B: found 30,905/30,905   missing 0   GUID 变更 0
          （30,301 叶子/精灵 + 448 ScriptableObject + 156 prefab）
a13e:     built 39（零告警）   partial 117   failed 0
          对象 22,209   字段 41,802   外部引用绑定 3,588 / 未绑定 469
```

### 16.1 新增管线

| 文件 | 作用 |
|---|---|
| `tools/unity-rebuild/a13e_plan_prefabs.py` | 规划器：按 `asset_path` 归组对象 dump、接 1.3b 的脚本绑定、**在进入 Unity 之前**把每个 PPtr 解析完（同文件 `m_FileID=0` 交给 builder；跨文件 `m_FileID>0` 经 externals → CAB → bundle → `(bundle,pid)` → 我们的资产路径），并给出逐 prefab 的解析覆盖 |
| `UnityProject/Assets/Editor/PrefabBuilder.cs` | 建 GameObject / Transform / RectTransform / 组件（MonoBehaviour 行按绑定类型 `AddComponent`）→ 按 `m_Father` 组层级 → 用共享 applier 写字段 → `PrefabUtility.SaveAsPrefabAsset` |
| `UnityProject/Assets/Editor/JsonToSerializedApplier.cs` | 从 SO 转换器抽出的共享 applier（字符串/isArray 陷阱、枚举索引、PPtr、managed reference 都在这一处），本轮补上 `Vector2/3/4`、`Quaternion`、`Color`、`Rect`、`LayerMask` |

### 16.2 本轮修掉的两个规模性缺陷

1. **绑定表只保留「我们重建了的」类型**：`bundle_script_map.json` 原先只收 `bound` 记录，
   于是 `UnityEngine.UI.Image`(1,419)、`TMPro.TextMeshProUGUI`(544)、`Unity.Timeline.*`(376)、
   `Unity.2D.Animation.SpriteSkin`(343)、`LeTai.TrueShadow`(388) 等**包/第三方组件全部无法创建**
   —— 3,378 个组件缺失、连带 1,527 个引用悬空。现在绑定表同时保留
   `type_known_not_reconstructed` 记录并带 `bound` 标志，builder 用 `Type.GetType(fullName, assembly)`
   从项目里的真实包解析它们。修后：缺失组件 3,378 → **281**，悬空引用 1,527 → **469**，零告警 prefab 7 → **39**。
2. **applier 跳过结构体字段**：`Vector3/Quaternion/Color/Rect` 等此前只记一条 "unsupported property type"
   就留默认值（`Court.prefab` 一个资产就有 2,355 条）——现在按 JSON 子字段实际写入。

### 16.3 仍未完成（诚实记录）

> **修正（§18.5、§19）**：本节所说的「156/156」是**构建器报告**的口径。后续与磁盘上的 prefab 文件对账发现
> 只有 13,737/22,870 个计划对象真正写进了 `.prefab`；根因是 `GetComponent<T>() ?? AddComponent<T>()`
> 这个 Unity fake-null 陷阱，修好后 **156/156 个 prefab 的主根树完整**（§19.1/§19.3）。

* **469 个悬空引用**：多数指向「没有被导入的跨文件对象」——图集内部的 sprite 子资产、未纳入计划的
  其它 bundle、以及 65 个规划期就无法解析的 PID（`cab-6fab7e…`）。按 prefab 分布看，96 个 prefab 只有
  ≤10 条告警，7 个 >100 条。
* **AnimationCurve 字段**（粒子系统，70 处）仍留默认值。
* **场景（level0）尚未处理**：规划器目前只做 `.prefab`；`level0` 的 typetree 还有 695 个
  `ValueError: Expected to read N bytes…`（UnityPy 读取问题），Boot 场景需要单独一趟。
* 345 个 `AnimatorController`、188 个 `PlayableDirector` 目标资产、17 个 `SpriteAtlas` 仍未转换。

---

## 17. 2026-09-17 记录：G3 内容构建打通（工程自己的 Addressables catalog）

工程现在有自己的 Addressables 配置与内容构建，并且**用发行版 catalog 当地址权威**：

```
setup : 922 entries（core 组）→ 4 个组，labels 440 条，missing assets 0
build : 946 locations，5 bundles（362 MB），catalog.bin 170 KB，6 分钟（增量 11.7 s）
compare: 我们的地址 907/907 命中发行版 catalog（100%）
         provider 种类 907/907 一致；resource type 893 相同 + 14 条比发行版更精确
```

### 17.1 一个被纠正的错误假设（重要）

最初我把「容器路径 = Addressables 地址」当成前提（沿用审计里 `_index.csv` 的 `asset_path` 列）。
构建后与发行版 catalog 对比，**地址命中率是 0** —— 说明假设错了。把发行版 catalog 反查一遍才看清真实结构：

| 发行版 catalog 里的东西 | 实际含义 |
|---|---|
| location 的 `internal_id` | **资产容器路径**（`Assets/#WitchTrials/Data/ScriptableObjects/AuthorData.asset`）—— 也就是 ledger 的 `asset_path` 列 |
| location 的 `primary_key` | **地址**（Naninovel 风格：`General/Data/AuthorData`、`Naninovel/Audio/Bgm_1_1`、`Naninovel/Scripts/Act01_Chapter01/…`） |
| 其它非 32 位十六进制 key | **label**（260 条，例如 `Audio-Bgm_001`、`Scripts-Act01_Chapter01_AdvBad`） |
| 28,332 个 32 位十六进制 key | 每个 entry 的隐式 GUID key |

所以 `catalog_dump_shipped.json`（30,819 个容器的 `internal_id → primary_key → labels`）就是地址与 label 的权威表，
规划器（`tools/unity-rebuild/a13f_plan_addressables.py`）现在按它生成：**30,819 个地址全部来自发行版 catalog，0 个回退**，
595 个 entry 带 label。分组布局仍是**重建决策**（每个家族一组，发行版不含 Editor 分组结构，审计禁止编造）。

### 17.2 新增工具

| 文件 | 作用 |
|---|---|
| `tools/unity-rebuild/a13f_plan_addressables.py` | 由 ledger + 发行版 dump 生成 entry 计划（地址/label/分组），`--group core|all` |
| `UnityProject/Assets/Editor/AddressablesSetup.cs` | 建 settings/groups/entries，写地址与 label，产出 `addressables_setup_result.json` |
| `UnityProject/Assets/Editor/AddressablesBuild.cs` | `AddressableAssetSettings.BuildPlayerContent`，记录 bundle 数/字节/catalog 路径/耗时 |
| `tools/unity-rebuild/a13g_compare_catalog.py` | 把两份 catalog dump 做地址覆盖 + 逐地址 resource type / provider 种类对比 |
| `CatalogDump.cs` 增补 | `MANOSABA_CATALOG_PATH` / `MANOSABA_CATALOG_OUT`：让同一套运行时解析路径也能 dump **工程自己**的 catalog（否则会覆盖发行版 dump） |

### 17.3 结果细节

我们这份 core catalog 的实际类型分布（真实运行时解析）：

```
TextAsset 448 | Naninovel.Script 440 | Texture2D 24 | Sprite 11 | Font 4 | Material 4
VideoClip 2 | 游戏数据 SO 8（AuthorData/CharacterData/…）| IAssetBundleResource 5（自己的 bundle）
provider: BundledAssetProvider 941 + AssetBundleProvider 5
```

* **907/907** 个真实地址命中发行版 catalog；另有 5 个「未命中」其实是我们自己生成的 bundle 隐式 key
  （`manosaba.data_assets_all_*.bundle` 等），发行版里当然没有。
* **14 条 resource type 差异全部是「我们比发行版更精确」**：发行版对那 8 个数据 SO 的 location 记的是 `null`，
  我们记到了具体类（`WitchTrials.Models.AuthorData` 等）——这是发行版构建时的既有现象，不是我们的偏差。
* 全量（30,819 entries，含 26,537 音频 + 3,301 贴图）的计划已生成（`addressables_plan_all.json`），
  但**尚未做全量构建**：贴图压缩 + 音频打包预计数小时，留作独立作业。
* 磁盘：`Assets` 9.97 GB、`Library` 12.51 GB（含内容构建缓存）、E: 剩 14.9 GB。

### 17.4 下一步

1. 全量内容构建（长作业）→ 用 `a13g` 对比全量地址覆盖（预期 ~30,819 命中）。
2. 把 G5 的 Player 冒烟改成**加载工程自己内容里的 prefab**（而不是只解析 catalog），
   这样"能运行"就有真实内容支撑。
3. 场景（level0）与剩余复合资产（AnimatorController / SpriteAtlas / Timeline）。


## 18. 2026-09-17 记录：G3 prefabs 内容构建通过 + Player 内加载真实内容并实例化重建 prefab

本轮目标：修掉 §17 遗留的内容构建回归，让 Player **加载工程自己构建的内容**、并**实例化重建出来的
prefab**（第 17.4 节第 2 条），同时把过程中发现的缺陷按证据记录下来。

```
G3  content build : 2,870 entries → 4,313 locations / 7 bundles / 1,032.5 MB，238.8 s（增量 16.9 s）
                    catalog.bin 482,774 B；player 侧内容 11 files / 1,032.9 MB
G5  player smoke  : Player 1,220.4 MB，PASS
                    43/43 程序集 · 5/5 类型探针 · 发行版 catalog PARSED 57,212 keys
                    工程 catalog `AddressablesMainContentCatalog` 3,937 keys · 4/4 地址加载
                    3/3 prefab 实例化，且组件绑定到重建的游戏脚本
```

### 18.1 Player 内实测（`out/unity/player_smoke_evidence.json`）

| 探测项 | 结果 | 说明 |
|---|---|---|
| 地址 `Naninovel/Scripts/Act01_Chapter01/Act01_Chapter01_Adv01` | `Naninovel.Script`，`lines = 84` | 剧本 SO 的 `[SerializeReference]` 多态图在 Player 里可用 |
| 地址 `General/Data/AuthorData` | `WitchTrials.Models.AuthorData`，`_items = 31` | 游戏数据 SO |
| 地址 `Naninovel/Localization/zh-Hans/Text/CharacterNames` | `UnityEngine.TextAsset`，1088 字符 | 本地化文本 |
| 地址 `Naninovel/Fonts/SourceHanSansKR/...` | `UnityEngine.Font` | 字体 |
| prefab `General/Prefabs/Debug/DebugLabel` | `DebugLabel(Clone)`，1 transform / 2 components | 该 prefab 发行版对象集就是 {GameObject, RectTransform, CanvasRenderer}，实例化后完全对应 |
| prefab `Naninovel/Spawn/Blur` | `Blur(Clone)`，组件 `[Transform, **Naninovel.FX.Blur**]` | **重建脚本绑定成功**（不只是资源加载） |
| prefab `General/Prefabs/UI/Buttons/SimpleCloseButton` | `SimpleCloseButton(Clone)`，2 transforms / 8 components，含 **`GigaCreation.Essentials.Ui.TweeningButton`** | 同上，且 10/10 对象齐全 |

选 prefab 的依据是脚本化的，不是凭感觉：`tools/unity-rebuild/pick_smoke_prefabs.py` 从
`prefab_plan_all.json` 里筛「引用完全绑定 + 带重建游戏程序集组件」的 prefab（共 71 个候选）。

### 18.2 4 个 prefab 的排除表，以及被推翻的第一个结论（重要）

最初的现象：prefabs 组的内容构建在 `GenerateLocationListsTask` 崩掉：

```
Build Task GenerateLocationListsTask failed with exception:
Invalid type UniRx.ReactiveProperty`1<int> for instance field WitchTrials.Views.SaveLoadUi:_loadingSlotCount
  at AddressableAssetEntry.GatherMainAndReferencedSerializedTypes ... AddressableAssetEntry.cs:762
```

我**第一版结论是错的**：我按「哪些组件类型声明了 `ReactiveProperty` 字段」静态筛出 4 个 prefab，
把理由写成「UniRx 桩类型不可序列化」。为了拿到可保留的证据，本轮特意做了两次对照实验：

| 实验 | 配置 | 结果 | 证据 |
|---|---|---|---|
| A | 4 个全部**不排除**（1952 buildable） | FAIL：`Invalid type UniRx.ReactiveProperty`1<int> ... SaveLoadUi:_loadingSlotCount` | `out/logs/addressables_build_prefabs_probe_noexclusions.log` |
| B | 只排除 `SaveLoadUI.prefab`（1951） | 仍 FAIL，但**换了错误**：`TypeLoadException: VTable setup of type UniRx.CompositeDisposable failed`（`ContentBuildInterface.WriteSerializedFile`） | `out/logs/addressables_build_prefabs_probe_saveloadonly.log` |
| C | 把 98 个桩类型字段全部标 `[NonSerialized]`（78 个文件）后再试 | 仍 FAIL，两条错误都在 → **该修法无效，已回退**（`annotate_nonserialized_stub_fields.py --revert`） | `out/logs/addressables_build_prefabs_nonserialized.log` |

同时用静态证据核对了「UniRx 字段」这个前提本身，结论与第一版推想不同：

* 发行版序列化数据（`res/_serialized` 的 dump，即"真游戏到底序列化了什么"的权威）**不含**这些字段：
  `SaveLoadUi` 的序列化键是 `_deleteSaveSlotDialogData, _fadeColorOnLoad, _fadeInLoadDurationOnLoad,
  _loadDelay, _loadSfxPath, _naninovelScreen, _overwriteSaveSlotDialogData, _panelReferences,
  _saveGameTimeout, _saveSfxPath, adaptToInputMode, allowedSamplers, blockInputWhenVisible`——没有
  `_loadingSlotCount`；`Pagination` 的是 `_currentPageLabel, _indicatorStyle, _indicators,
  _nextPageButton, _originalDot, _previousPageButton, _totalPageLabel, _wrapped`——没有 `_currentPageIndex`。
* 所以 **prefab 本身没问题**，问题在**它们的组件类型上有 UniRx 桩类型字段**：Unity 在构建期解析
  这些组件的类型时就会碰到 `UniRx.ReactiveProperty<int>` / `UniRx.CompositeDisposable`，而 UniRx
  在本工程里只有 Il2CppDumper 桩程序集（`Assets/Plugins/Il2CppStubs/UniRx.dll`，元数据不完整，
  Mono 连 vtable 都建不起来）。
* 两者的差别用**排除表**而不是"我猜"来落地：见 `tools/unity-rebuild/content_build_exclusions.json`，
  每条都带 `reason` + `evidence`，并显式标注哪条是 **OBSERVED**（被日志点名过的 `SaveLoadUI.prefab`）、
  哪条是 **INFERRED**（同因推断，因为构建在第一个错误处即中止，永远不会点名其余三个）。

排除表是**机制**而不是手改计划：`a13f_plan_addressables.py` 读它，把命中的 entry 标成
`status: "excluded"` 并写进 `excluded[]` 摘要；`AddressablesSetup.cs` 见到 `excluded` 就跳过，
并且**会把已不在计划里的旧 entry 删掉**（`pruned`，计划对所点名的组是权威）——
这正是第二次运行时 `excluded=4 pruned=4`、第三次 `excluded=4 pruned=0`（幂等）的由来。

未解路径：真实的 UniRx 依赖（游戏本身就是真的 UniRx）。这属于「需用户提供/不得代造」的第三方包，
与 `UNITY_READINESS_CODEX_AUDIT.md` §7.6.3 的 BLOCKED 清单同类。

### 18.3 一个被纠正的错误心智模型：内容**不需要**我们拷进 StreamingAssets（重要）

§12 之后我一直以为「Player 不自动带上 Addressables 内容，所以要手动把
`Library/com.unity.addressables/aa/<platform>` 拷到 `Assets/StreamingAssets/aa`」。读了包源码后确认**这是错的**：

```csharp
// com.unity.addressables/Editor/Build/AddressablesPlayerBuildProcessor.cs
internal static List<KeyValuePair<string, string>> GetStreamingAssetPaths()
{
    if (Directory.Exists(Addressables.BuildPath) ...)
        pairs.Add(new KeyValuePair<string, string>(Addressables.BuildPath, "aa"));   // → StreamingAssets/aa
}
```

`PrepareForPlayerbuild` **每次** Player 构建都会把 `Addressables.BuildPath` 注册成
StreamingAssets 的附加路径，与 `BuildAddressablesWithPlayerBuild` 无关（那个开关只管「要不要顺便重建内容」）。
本工程该开关是 `DoNotBuildWithPlayer`（0），所以流程是：

```
G3 内容构建 → Library/com.unity.addressables/aa/Windows → （Player 构建自动注入）→ Player_Data/StreamingAssets/aa
```

实测印证：Player 的 `manosaba-rebuild_Data/StreamingAssets/aa` = **11 files / 1,032.9 MB**（catalog.bin +
7 bundles + settings.json + hash + link.xml），而我们**没有**做任何拷贝；`AddressablesBuild.cs` 里的
手动 staging 已默认关闭（`MANOSABA_STAGE_STREAMINGASSETS=1` 才会执行）。

这个机制还解释了一个此前的"灵异现象"：**内容构建失败会把 `Library/.../aa/Windows` 清空**
（ScriptableBuildPipeline 在构建开始时清理输出目录），于是失败之后那次 Player 构建就"静默地"产出一个
**不含任何 Addressables 内容**的 Player——上一次 G5 的 4/4 地址全部 `InvalidKeyException` 就是这么来的。
所以 `g3_content_build.ps1` 现在把「构建根目录里真的有 catalog + bundle」当作 PASS 条件之一
（该检查一度因 `Addressables.BuildPath` 是**相对路径**而误报 0 文件，也已修正为按工程根解析）。

### 18.4 本轮新增/加固的工具

| 文件 | 作用 |
|---|---|
| `tools/unity-rebuild/g3_content_build.ps1` | G3 门：跑 setup + BuildPlayerContent，删除旧产物 + 校验**新鲜度/退出码/状态**，再核对构建根目录里确实有 catalog 与 bundle；产物 `out/unity/g3_content_build.json` |
| `tools/unity-rebuild/content_build_exclusions.json` | 内容构建排除表（带 reason/evidence/OBSERVED-INFERRED 标注），规划器与 Editor 侧都消费它 |
| `a13f_plan_addressables.py`（改） | 读排除表 → `status: "excluded"`；摘要写 `entry_count` / `excluded_count` / `excluded[]`；按 **asset_path** 匹配（发行版 catalog 有 84/156 个容器路径被多个资产共用，按容器路径匹配会误伤） |
| `AddressablesSetup.cs`（改） | 跳过 `excluded`；**prune** 计划外 entry（`MANOSABA_ADDRESSABLES_PRUNE`，默认开）；结果里新增 `entries_excluded` / `entries_pruned` / `excluded_samples` / `pruned_samples` |
| `AddressablesBuild.cs`（改） | 记录 `player_streaming_source` / `player_streaming_source_ready`；手动 staging 改为默认关闭（见 §18.3） |
| `UnityProject/Assets/Editor/ContentBuildProbe.cs` | 用 `ContentBuildInterface.GetPlayerObjectIdentifiersInAsset` + `GetTypeForObjects` **逐资产**预测构建能否序列化，避免"每次 4 分钟、且只在第一个错误处停下"的试错；同时列出「序列化字段里来自桩程序集的那些」。结果：156/156 prefab 都不抛异常，只有 2 个引用 `MPUIKIT.*` 桩类型——**说明这条 API 路径不足以复现真实失败**，这一点也照实记录在工具注释里 |
| `tools/unity-rebuild/annotate_nonserialized_stub_fields.py` | 把桩类型字段标 `[NonSerialized]`（98 字段 / 78 文件），带 `--revert`；本轮验证其**无效**并已回退，工具保留供未来尝试 |
| `tools/unity-rebuild/print_smoke_evidence.py` | 把 `player_smoke_evidence.json` 打成可读摘要（省得每次手工挖 JSON） |
| `pick_smoke_prefabs.py` / `prefab_fidelity.py` / `prefab_written_objects.py` / `prefab_shortfall.py` / `prefab_script_refs.py` | prefab 保真度与脚本引用的一次性核算工具（见 §18.5） |
| `ManosabaBoot.cs`（改） | 内容探针从「实例化 1 个 Unity 内置组件 prefab」升级为「实例化 3 个 prefab 并记录**组件类型全名**」；`content.status = LOADED` 现在**要求**至少一个实例带重建游戏程序集的组件——只有资源加载、没有脚本绑定只能算 `PARTIAL` |

### 18.5 新发现的缺陷：prefab 保真度远低于 §16 的表述（诚实记录）

§16 写的是「prefab 对象图重建 156/156（39 clean / 117 partial）」，那是**构建器报告**的口径。
本轮把报告与**磁盘上真正的 prefab 文件**对账，发现两者不一致：

```
计划对象总数        22,870
构建器报告已创建    22,209   (97.1%)
prefab 文件里实有   13,737   (61.9% of planned)   ← 58 个 prefab 少写了对象
举例：CreditsUI 8/3189 · WitchBookUI 9/2065 · OptionsUI 11/1219 · SaveLoadUI 9/148 · GalleryUI 5/119
```

也就是说：**大 prefab 的层级在保存时丢了**——构建器把对象建出来了（`objects_created` 与计划一致），
但写进 `.prefab` 的只有根对象及其组件（`m_Children: []`），子层级没有成为 prefab 的一部分。
小 prefab 不受影响（DebugLabel 3/3、SimpleCloseButton 10/10，实例化结果与计划逐项吻合）。

结论要按这个口径修正：**G2 的 prefab 部分是"对象图按计划创建，但 58 个 prefab 落盘层级不完整"**，
不是"156 个都重建好了"。这是下一步的独立缺陷项（`tools/unity-rebuild/prefab_written_objects.py`
可随时重算），也解释了为什么本轮 G5 只能挑小 prefab 来证明"实例化 + 脚本绑定"。

### 18.6 磁盘：一次真实的构建失败（教训）

本轮中途内容构建以 `IOException: 磁盘空间不足` 失败——`Library/BuildCache`（SBP 内容构建缓存）在几次
构建后涨到 **14.72 GB**，E: 只剩 0.35 GB。删除该缓存后恢复到 15.08 GB（缓存可再生，代价是下次内容
构建变慢）。当前：内容构建 + Player 各一份 1 GB 级产物，E: 剩 **6.5 GB**。
长作业（全量 30,819 entries）之前必须先算磁盘，不能想当然。

### 18.7 下一步

1. **全量内容构建**（26,537 音频 + 3,301 贴图，预估数小时）：先清磁盘，再跑 `--group all`，
   然后用 `a13g` 对账全量地址覆盖。
2. **prefab 落盘层级**（§18.5）：修 `PrefabBuilder` 的父子关系/保存路径，让 58 个 prefab 的对象数
   与计划一致，然后重跑 G3/G5 并更新 §16 的口径。
3. **UniRx**：这是 4 个 prefab 与内容完整性的硬阻断；需要真实 UniRx（用户提供或确认可引入）。
4. 场景（`level0`，695 个 `Expected to read N bytes` 错误的 UnityPy 读取问题）、AnimatorController 345、
   SpriteAtlas 17、Timeline 188、Shader 134，以及 10,971 个 IL2CPP 桩方法体（决定"能不能真的玩"）。


## 19. 2026-09-17 记录：prefab 层级缺陷根因（一个 Unity API 陷阱）+ 内容构建阻塞改为「算出来」

§18.5 记录的现象（13,737/22,870 个对象才真正写进 `.prefab`，58 个大 prefab 只剩根对象）本轮定位到根因并修掉，
最终 G3/G5 重新通过。这一节按"现象 → 排错 → 根因 → 修法与复验 → 仍未完成"记录。

```
修前：58/156 个 prefab 层级不完整，22,209 个已创建对象里只有 13,737 个落盘（61.9%）
修后：156/156 个 prefab 主根树完整
      CreditsUI  8 → 2,996 对象   WitchBookUI 9 → 1,997   SaveLoadUI 9 → 106   OptionsUI 11 → 1,220
G3  content build : 2,856 entries → 4,299 locations / 7 bundles / 1,032.6 MB（18 个 prefab 排除，见 §19.4）
G5  player smoke  : Player 1,220.5 MB，PASS；43/43 程序集 · 5/5 探针 · 发行版 catalog 57,212 keys
                    工程 catalog `AddressablesMainContentCatalog` 3,909 keys · 4/4 地址 · 3/3 prefab 实例化
```

### 19.1 根因一（关键）：`GetComponent<T>() ?? AddComponent<T>()` 是错的

`PrefabBuilder` 里建 RectTransform 的写法原本是：

```csharp
RectTransform rt = owner.GetComponent<RectTransform>() ?? owner.AddComponent<RectTransform>();
```

**`??` 只判断"真正的 null"，而 Unity 的 `GetComponent<T>()` 在组件不存在时会返回一个 fake-null 包装对象**
（C# 对象存在、native 指针为空）。于是 `??` 认为左边"非空"，`AddComponent` 永远不会被调用，`rt` 是一个
fake-null 引用 → `node.component == null` → 该 transform 节点无法加入层级 → **它的整棵子树挂不上主根** →
`SaveAsPrefabAsset(root)` 只写出根对象。

排错路径（每一步都有产物，不是猜的）：

| 步骤 | 做法 | 结果 |
|---|---|---|
| 1 | 给 builder 加"保存后回读并计数"（`objects_written` / `saved_transforms` / 状态 `BUILT_INCOMPLETE`） | 首次把"报告说建好了、文件里没有"变成可测量的事实 |
| 2 | 逐 prefab 探针计划（`make_prefab_probe_plan.py` + `MANOSABA_PREFAB_PLAN`/`MANOSABA_PREFAB_OUT`） | 不必跑全量 156 个，1.5 分钟一轮 |
| 3 | 在"层级拼装后/字段应用后/保存前"三处数 transform | 树在**层级循环结束时就已经是平的**（`after_hierarchy=1`） |
| 4 | 转储每个 transform 节点的 `origin/owner/component/father/nowUnder`（`graph_dump`） | 13 个节点里 3 个 `component=NULL`，其余 `origin=reused`（**新建的 GameObject 不可能 reused**） |
| 5 | `PrefabBuilderDiag` 用真实 dump 复现同样的调用序列 | `GetComponent` 返回 fake-null，`AddComponent` 因而从未被调用 |

修法：用 Unity 的 `==` 比较，不用 `??`：

```csharp
RectTransform existing = owner.GetComponent<RectTransform>();
bool hasExisting = existing != null;                       // Unity 重载的 == 才认 fake-null
RectTransform rt = hasExisting ? existing : owner.AddComponent<RectTransform>();
```

复验的直接证据：G5 的运行时探针里 `DebugLabel(Clone)` 的组件从 `[Transform, CanvasRenderer]` 变成
`[RectTransform, CanvasRenderer]` —— 根对象的 RectTransform 真的存在了（§19.3）。

### 19.2 根因二：发行版的 prefab **本来就是多根**的

修完 §19.1 后仍有 5 个 prefab 写不全。按 `m_Father == 0` 统计根对象，并与 `res/_index.csv` 的资产归属对账
（该 prefab 的 148 个对象与索引逐条一致，0 差异），确认这些**不是我们的规划错误**：

| prefab | 根对象 | 额外根（模板对象） | 额外根里的对象数 |
|---|---|---|---|
| `SaveLoadUI.prefab` | `SaveLoadUI` | `SaveLoadSlot` | 43 |
| `GalleryUI.prefab` | `GalleryStillSlot` | `GalleryUI` | 103 |
| `LogUI.prefab` | `LogUI` | `LogMessage` | 38 |
| `WitchBookUI.prefab` | `WitchBookUI` | `WitchBookItemButton` | 35 |
| `CreditsUI.prefab` | `CreditsUI` | `SpecialThanksCreditLabel_Large/Normal/Small` | 13 |

实验结论（两次尝试都留了工具与注释）：

* **把所有根挂到一个自建 wrapper 下再保存** → 全部对象都能落盘，但实例化出来的根变成 wrapper
  （名字不对、组件下沉一层），运行时代码要找的根身份丢了；
* **保存后把 wrapper 两个文档从文本里剪掉、把子节点的 `m_Father` 改回 0**（`SpliceRoots`，已删除）→
  Unity 的 prefab 导入器只会保留"从文件主对象可达"的那一棵，其余根**全部被丢掉**。

所以 Unity 的 prefab 资产**无法表达多根**，而运行时要的是主根身份。最终选择：**保存主根树**（保身份），
并把额外根按 prefab 逐条记录成"已文档化的偏差"（`extra_root_names` / `objects_in_extra_roots`，
共 232 个对象），不再静默丢失。这些模板对象（存档槽、画廊槽、日志行……）要还原需要各自成为独立资产，
已列入下一步。

### 19.3 门与产物：现在的实测状态

| 门 | 结果 | 数字 |
|---|---|---|
| prefab 重建 | **156/156 主根树完整** | 创建 21,344 / 落盘 21,783；`incomplete=0`；34 built / 122 partial / 0 failed |
| **G3 内容构建** | **PASS** | 2,856 entries → 4,299 locations / 7 bundles / 1,032.6 MB（222.9 s）；catalog 与 bundle 都在构建根目录里 |
| **G5 独立 Player** | **PASS** | Player 1,220.5 MB；43/43 程序集、5/5 类型探针；发行版 catalog `PARSED` 57,212 keys；工程 catalog 3,909 keys；4/4 地址加载；3/3 prefab 实例化且带重建脚本组件（`Naninovel.FX.Blur`、`GigaCreation.Essentials.Ui.TweeningButton`） |

同时 builder 现在会如实报告**建不出来的对象**：`objects_unbuildable = 1,526`（其中 `AddComponent` 被拒 865 ——
抽象/重复类型，未知类型 281，其余为 transform 添加被拒）。CreditsUI 有 351 个这样的对象，这是"计划里有、
Unity 里建不出"的真实差距，不再混进"已创建"。

### 19.4 内容构建的阻塞：从"4 分钟试一次"改成"算出来"

修好层级后，内容构建又失败了 —— 但**换了个对象**，因为现在组件真的存在了：

```
Invalid type GigaCreation.Essentials.Ui.SelectableUtility for instance field
             GigaCreation.Essentials.Ui.TweeningToggle:_selectableUtil
VTable setup of type LeTai.TrueShadow.TrueShadow failed
Unloading broken assembly Assets/Plugins/Il2CppStubs/UniTask.dll, this assembly can cause crashes in the runtime
```

最后一行是系统性的原因：**Unity 直接拒绝加载这些桩程序集**，所以任何引用它们类型的资产都无法做类型遍历。
每次构建都在第一个错误处中止，手改排除表就是"4 分钟一个对象"，因此改为**静态计算**
（`tools/unity-rebuild/plan_exclusions_by_stub_fields.py`）：

```
字段类型来自桩程序集的组件类型   ：30 个（直接） + 5 个（传递：字段引用了上述类型，如 TweeningToggle._selectableUtil）
自身类型就是桩类型的组件         ：MPUIImage / TrueShadow / PPT.Noise.Noise …
受影响的 prefab                  ：18 / 156   → 写入 content_build_exclusions.json（每条带字段级理由）
```

排除表因此从"手写 4 条 + OBSERVED/INFERRED 标注"升级为**可重算的 18 条**（重新运行脚本即可复现），
规划器与 Editor 侧照旧消费它（`status: "excluded"` + prune）。代价是 18 个 UI prefab 进不了内容构建
（`DebateUI`、`TitleUI`、`OptionsUI`、`WitchBookUI`、`CreditsUI`…），这是"桩依赖"的真实代价，
解封路径只有一个：**真实的 UniRx / UniTask / LitMotion / TrueShadow / MPUIKit / UIPolygon 依赖**。

### 19.5 新增/改动的工具

| 文件 | 作用 |
|---|---|
| `UnityProject/Assets/Editor/PrefabBuilder.cs`（改） | 修 `??` 陷阱；保存后回读自检（`objects_written`/`hierarchy_complete`/`BUILT_INCOMPLETE`）；记录根数、额外根、建不出的对象；`MANOSABA_PREFAB_OUT` 让探针不覆盖正式产物 |
| `UnityProject/Assets/Editor/PrefabBuilderDiag.cs` | 隔离验证 Unity 原语（AddComponent/SetParent/fake-null），并用真实 dump 复现 builder 的调用序列 |
| `tools/unity-rebuild/make_prefab_probe_plan.py` | 生成子集探针计划（1.5 分钟一轮，而不是全量） |
| `tools/unity-rebuild/prefab_probe_report.py` / `prefab_result_lines.py` / `prefab_fix_numbers.py` | 探针与正式产物的人类可读报告 |
| `tools/unity-rebuild/probe_prefab_tree.py` / `probe_prefab_roots.py` / `probe_prefab_hierarchy.py` | 从计划+dump 直视层级、根对象、父子链 |
| `tools/unity-rebuild/plan_exclusions_by_stub_fields.py` | 计算（不是手写）内容构建排除表 |
| `tools/unity-rebuild/prefab_written_objects.py` / `prefab_fidelity.py` | 计划/报告/磁盘三方对账 |

### 19.6 仍未完成（诚实记录）

1. **232 个多根模板对象**只记录未落地（§19.2）：需要为它们建立独立资产或等 UniRx 类依赖解决后再谈结构。
2. **1,526 个对象建不出来**（抽象/重复组件类型、未知类型）——按 prefab 分布见 `prefab_build_result.json`
   的 `warnings`。
3. **18 个 prefab 被排除**于内容构建之外（§19.4），根因是桩程序集被 Unity 拒载。
4. **磁盘**：修好层级后内容构建会明显更吃缓存 —— `Library/BuildCache` 两次涨到 14.7 GB 并把构建打断
   （`IOException: 磁盘空间不足`）。删缓存可回收（可再生），当前 E: 剩 6.5 GB；**全量 30,819 entries 的
   构建必须先做磁盘预算**。
5. 场景（`level0`）、AnimatorController 345、SpriteAtlas 17、Timeline 188、Shader 134，以及 10,971 个
   IL2CPP 桩方法体（决定"能不能真的玩"）。


## 20. 2026-09-17 记录：G4 Play Mode 打通（同一套探针，Editor 与 Player 结果逐项一致）

G0–G6 里最后一个"依赖 G1 但一直没跑"的门是 G4。本轮把它做成**可重复的门**，并让它和 G5 共用同一套
运行时探针，这样"能运行"在 Editor 侧和 Player 侧有同一份口径。

```
G4 Play Mode : PASS（Editor Play Mode 35.9 s）
               43/43 程序集 · 5/5 类型探针 · 发行版 catalog PARSED 57,212 keys
               工程 catalog `AddressablesMainContentCatalog` 3,909 keys · 4/4 地址 · 3/3 prefab 实例化
G5 Player    : PASS（1,220.5 MB）—— 与 G4 的下列字段**逐项相同**
               status / assemblies / probes / shipped catalog keys / 自身 catalog keys / loaded, failed
               / providers / game_script_components / 三个 prefab 的实例名与组件类型
```

### 20.1 门是怎么做的

| 关心的问题 | 做法 |
|---|---|
| Play Mode 在 batchmode 下怎么退出？ | `PlayModeGate.Run` 进入 Play Mode 后把控制权交给 `EditorApplication.update`：一旦证据文件出现（或 240 s 超时）就 `isPlaying = false` 并 `EditorApplication.Exit(0/1/3)` |
| 进入 Play Mode 会**域重载**，静态状态全丢 | 门的状态放 `SessionState`（跨域重载保留），并用 `[InitializeOnLoadMethod]` 重新挂 update 回调 |
| Editor 里怎么拿到和 Player 一样的证据？ | 同一个 `ManosabaBoot`：G5 用命令行 `-manosabaSmoke -manosabaContent -manosabaEvidence <path>` 驱动，Editor 侧加环境变量等价物（`MANOSABA_SMOKE` / `MANOSABA_CONTENT` / `MANOSABA_EVIDENCE`），两条路径落到同一份 `ContentProbe` |
| 怎么证明 Play Mode 用的是**构建出来的内容**，而不是 "Use Asset Database" 捷径？ | 探针现在记录每个地址的 provider 与 container：`BundledAssetProvider` + 发行版容器路径（`.../Act01_Chapter01_Adv01.asset`）= 真的走了 bundle；捷径会报 `AssetDatabaseProvider` + 工程文件路径。batchmode 下 `AddressableAssetSettings.DataBuilders` 读出来是空的，所以"用哪个 play mode 脚本"不靠它判断，改为**从 provider 反证**（工具里已写明这一点） |

### 20.2 本轮修掉的两个自身缺陷

1. **门脚本覆盖了工具产物**：`g4_play_mode.ps1` 起初把汇总写回 `g4_play_mode.json`，把 `PlayModeGate`
   写下的详细信息（builder 索引、scene、play 时长）冲掉了 —— 现在分成两个文件：工具写
   `g4_play_mode.json`，门写 `g4_play_mode_gate.json`，各自带新鲜度校验。
2. **证据不足以区分内容来源**：`ContentRecord` 新增 `provider` / `bundle` / `internal_id`
   （以及 `provider_error`），这是 §20.1 最后一行能成立的前提。

### 20.3 新增工具

| 文件 | 作用 |
|---|---|
| `UnityProject/Assets/Editor/PlayModeGate.cs` | G4 的 Editor 侧实现（SessionState + update 轮询 + 退出码 0/1/2/3） |
| `tools/unity-rebuild/g4_play_mode.ps1` | G4 门脚本：bootstrap → 进 Play Mode → 校验新鲜度/退出码/状态 |
| `tools/unity-rebuild/compare_g4_g5.py` | 把 G4 与 G5 的证据逐字段并排打印，防止两边口径漂移 |
| `ManosabaBoot.cs`（改） | 环境变量驱动 + provider/bundle/internal_id 证据 |

### 20.4 仍未完成（诚实记录）

1. **"能运行"与"能玩"之间的那道线还没跨**：G4/G5 都只跑到集成层启动 + 内容加载 + prefab 实例化，
   `WitchTrials.Views.GameInitializer` 的方法体仍是 IL2CPP 桩（10,971 个），还没有真的调用它。
   下一步应当是"在 Play Mode 里尝试启动游戏并记录它究竟走到哪一步"——这是把 §19 的"能跑"推进到
   "能玩"的关键实验，预期会在桩方法体处停下，但那正是需要量化的边界。
2. 场景（`level0`）仍未导入，所以还没有真实的游戏场景可跑；18 个 prefab 因桩程序集被排除
   （§19.4）；1,526 个对象建不出来（§19.3）。


## 21. 2026-09-17 记录：真的去启动游戏了 —— 重建的入口代码**执行了**，并在第一处场景引用处停下

§20.4 说"能运行"与"能玩"之间那道线还没跨。本轮跨了：在 Play Mode 里用反射把游戏的启动组件
`WitchTrials.Views.GameInitializer` 真正建出来并调用，把结果记录成证据（`g4b_game_start.ps1`）。

```
G4b PASS（Play Mode 43.7 s）
  entry type        : WitchTrials.Views.GameInitializer, WitchTrials.Game   resolved=True
  component created : True
  Awake 的效果      : _addressablesManager 字段存在=True  非空=True
                      → GigaCreation.Essentials.AddressablesUtils.AddressablesManager
  Start()（直接调用）: NullReferenceException @ GameInitializer.cs:166
  log               : errors=0 exceptions=1
```

### 21.1 两个边界，都是量出来的

| 边界 | 证据 | 性质 |
|---|---|---|
| **重建的代码真的在执行** | `Awake()` 的方法体只有一行会写 `_addressablesManager`（`= new AddressablesManager()` + `ServiceLocator.Register<IAddressablesManager>`）；探针在 `AddComponent` 之后把该私有字段读回来，值是 `AddressablesManager` 实例 | 说明 `// RECONSTRUCTED | Confidence: A` 的还原不是"能编译"，而是**能跑** |
| **启动链停在第一处序列化引用** | `Start()` 第 166 行 `_steamManager.gameObject.SetActive(...)` 抛 NRE；`_steamManager` 是 `[SerializeField]`，值来自游戏自己的场景/prefab，我们还没有它 | **数据缺口**，不是代码缺口：需要导入 `level0`（或从发行版 dump 里补出这些引用） |

再往后还有一处代码缺口：`Start()` 末尾 `InitializeAsync(destroyCancellationToken).Forget()` 指向
`GameInitializer.InitializeAsync` 的状态机，而它的 `MoveNext()` 目前是**空的 IL2CPP 桩**
（`GameInitializer.cs:66-68`）。所以完整的启动链需要：场景引用 + 这段异步状态机。

### 21.2 顺带纠正一个口径：桩方法体并不集中在游戏自己的程序集

早期轮次用"10,971 个 IL2CPP 桩方法体"概括阻塞，容易让人以为游戏逻辑全是空的。对**当前应用的源码**
做一次普查（`print_game_start.py`）：

```
1,610 个文件 · 8,972 个方法签名 · 1,044 个 RECONSTRUCTED 标记
  WitchTrials.Game（原 Assembly-CSharp）: 1,044 reconstructed / 1,182 methods   ← 约 88% 有还原出的方法体
  其余 42 个程序集                        : 0 markers（它们的源码是完整反编译，不是"从原生证据重建"）
```

也就是说：**游戏自己的逻辑大部分是有真实方法体的**（1,044 个从原生证据还原，且 `GameInitializer.Awake`
已被实测证明能执行）；那 10,971 条桩记录分布在 43 个程序集里，主体是第三方/Unity 侧的类型。
"能不能玩"的瓶颈因此更精确地落在：场景数据接线 + 少数异步状态机。

### 21.3 新增工具与产物

| 文件 | 作用 |
|---|---|
| `UnityProject/Assets/Scripts/Runtime/ManosabaGameStart.cs` | 游戏启动探针：反射建组件、读回 `Awake` 的效果字段、直接调用 `Start()` 抓住异常对象与栈、捕获窗口内 Unity 的全部日志（异常/错误/断言） |
| `tools/unity-rebuild/g4b_game_start.ps1` | G4b 门：bootstrap → 开 `MANOSABA_GAMESTART=1` 跑 Play Mode → 要求 `boot evidence` 与 `game_start_evidence` **两份**都新鲜 |
| `PlayModeGate.cs`（改） | 第二个证据文件存在前不退出 Play Mode（否则会在实验中途把会话拆掉） |
| `print_game_start.py` | 打印启动证据 + 重建方法体普查 |
| `out/unity/game_start_evidence.json`、`g4b_game_start.json`、`logs/game_start.log` | 本轮证据 |

状态语义（写在门脚本头部，避免误读）：`STARTED` = 入口组件建出来了**且** `Awake` 的重建方法体生效；
`COMPONENT_ONLY` = 组件在但 `Awake` 没生效；`BLOCKED_TYPE` = 类型没解析到；`ERROR` = 探针自身失败。
门在 `STARTED` 时 PASS，并把启动链的异常**单独报告**——它停在哪儿本身就是结论。

### 21.4 下一步（按证据排序）

1. **让启动链走过第 166 行**：导入 `level0` 场景（当前因 UnityPy 的 695 个 `Expected to read N bytes`
   错误未处理），或从发行版的场景/prefab dump 里把 `_steamManager` / `_splashScreen` / `_gamePrefab` /
   `_gamePrefabInstanceName` 这四个引用补到 Boot 场景的 `GameInitializer` 组件上。
2. **重建 `GameInitializer.InitializeAsync` 的状态机**（`MoveNext()` 目前为空），以及它调用的
   `EntrySequence` 等；这属于既有的"桩方法体补齐"工作，但现在有了明确的优先级：**启动链上的桩优先**。
3. 把 G4b 的口径接进 G6（全内容覆盖）——"启动链能走多远"应当作为覆盖度的一部分持续测量。


## 22. 2026-09-17 记录：把启动链的数据缺口补上 —— `Start()` 走通，边界只剩异步桩

§21 测出启动链停在 `GameInitializer.cs:166`（第一处序列化引用）。本轮把那四个引用补齐，于是边界从
"缺数据"推进到"缺代码"，并且这一步也是量出来的。

```
G4b PASS（wired，Play Mode 41.5 s）
  entry type          : WitchTrials.Views.GameInitializer, WitchTrials.Game  resolved=True
  Awake 的效果        : _addressablesManager 非空 → GigaCreation.Essentials.AddressablesUtils.AddressablesManager
  Start()（直接调用）  : OK            ← 上一轮是 NullReferenceException @ GameInitializer.cs:166
  wiring:
    - _steamManager           : WitchTrials.Views.SteamManager 建出并赋值
    - _splashScreen           : WitchTrials.Views.SplashScreen 建出并赋值
    - _gamePrefabInstanceName : 设为 'Game(Clone)'
    - _gamePrefab             : AssetReferenceGameObject，m_AssetGUID=8aab08517962f2842bed974981b26f8b
                                （该类型无默认构造，改用 GetUninitializedObject —— 与 applier 里的托管引用同一路径）
  log                 : errors=0 exceptions=1（唯一一条来自我们自己的接线，见下）
```

### 22.1 先说实话：场景里的值**读不出来**，所以引用是"合成"的

`level0` 就在 `manosaba_Data/level0`（18,804 字节、142 个对象），提取索引也能定位到
`GameInitializer` GameObject（path_id 17，组件 37=Transform、**116=MonoBehaviour**）。但 UnityPy 读不出
这个 MonoBehaviour：

```
=== path_id 116  type=MonoBehaviour
  read() failed: ValueError: Expected to read 120 bytes, but only read 32 bytes
```

原因是玩家版场景里**没有 MonoBehaviour 的 TypeTree**（那 695 个 `Expected to read N bytes` 是同一类问题）。
所以本轮**没有**从发行版场景里"恢复"出这四个引用的真实取值，而是：
SteamManager / SplashScreen 用重建类型现建、实例名用占位串、`_gamePrefab` 用**我们自己导入的**
`Game.prefab`（`Assets/GameContent/# WitchTrials/Prefabs/General/Core/Game.prefab`，guid `8aab08…`）的
GUID 填 `m_AssetGUID`。这一点在证据里逐条记为 `wiring`，不会被误读成"场景已还原"。
工具 `tools/unity-rebuild/read_level0_gameinitializer.py` 记录了这个限制本身。

### 22.2 边界因此更清楚了

| 层 | 状态 | 证据 |
|---|---|---|
| 重建代码执行 | ✅ | `Awake` 写入的 `_addressablesManager` 读回非空 |
| 启动组件的数据接线 | ✅（合成值） | 四个引用全部就位，`Start()` 返回 OK |
| 下一层数据缺口 | ⚠️ 已定位到行 | 我们新建 `SplashScreen` 时它自己的 `Awake()` 在 `SplashScreen.cs:124` 抛 NRE —— **每往下一层，就还有一组场景引用**，这是场景未还原的必然结果 |
| 启动链的代码缺口 | ❌ 未完成 | `Start()` 末尾进入 `InitializeAsync(...)`，其状态机 `MoveNext()` 仍是空桩（`GameInitializer.cs:66-68`）→ 链在这里**静默结束** |

一句话：**数据缺口可以用合成值临时绕过并逐层测量，代码缺口（异步启动链）只能靠继续重建**。

### 22.3 全门复验（都在本轮重跑，产物都是新的）

| 门 | 结果 |
|---|---|
| G1 编译 | **PASS** 0 error / 0 warning（13.3 s） |
| G2 导入 | **PASS** `found 30,905/30,905`、missing 0、两轮 GUID 变更 **0** |
| G3 内容构建 | **PASS** 2,856 entries → 4,299 locations / 7 bundles / 1,032.6 MB（增量 25.6 s） |
| G4 Play Mode | **PASS** 43/43 程序集 · 5/5 探针 · content LOADED（37.9 s） |
| G4b 启动游戏 | **PASS** `STARTED`，`Start()` OK（wired，41.5 s） |
| G5 独立 Player | **PASS** 1,220.5 MB · 43/43 · 5/5 · 发行版 catalog 57,212 keys · 4/4 地址 · 3/3 prefab 实例化 |

新增产物：`ManosabaGameStart.cs` 的接线段、`g4b_game_start.ps1 -Wire`、
`read_level0_gameinitializer.py`、`print_game_start_wired.py`、`game_start_evidence_wired.json`、
`g4b_game_start_wired.json`（未接线的基线证据仍保留为 `game_start_evidence.json` / `g4b_game_start.json`）。

### 22.4 目标判定：`让该工程能够编译运行` 已达成

按本轮证据，本目标的字面要求（该工程能编译、能运行）已经被实测满足：

* **能编译**：G1 PASS，0 error / 0 warning（真实 Unity Editor，43 个游戏程序集 → 76 个 Player 程序集）；
* **能运行**：G4（Editor Play Mode）与 G5（1,220.5 MB 独立 Player）都 PASS，且加载的是**工程自己构建的内容**
  （provider 为 `BundledAssetProvider`），4/4 地址、3/3 prefab 实例化并绑定到重建脚本；
* **游戏自己的入口代码在跑**：G4b `STARTED` —— `GameInitializer.Awake` 的重建方法体生效，`Start()` 在数据
  齐备时走完（`OK`）。

**仍未达成的是"游戏能玩"**，它属于另一个目标：补完启动链上的异步状态机与逐层场景引用（§22.2），
以及 G3 全量内容构建、`level0` 场景导入、18 个被排除的 prefab（需要真实的 UniRx/TrueShadow 等依赖）。
这些都已按证据列进 §21.4 / §22.2，随时可以接着做。


## 23. 2026-09-17 记录：第三方依赖改为**直接引用真实程序集**（不做反混淆）

按"商业包直接引用、不做 deobf"的要求，本轮把桩依赖换成真实程序集，并修掉了让桩无法加载的根因。
结果：内容构建的排除表从 18 个降到 13 个，而且**这 13 个全部只由两个商业包造成**。

### 23.1 根因：桩插件引用了只作为 asmdef 存在的 `Il2CppDummyDll`

每个 Il2CppDumper 桩插件（`Assets/Plugins/Il2CppStubs/*.dll`）都带一条程序集引用
`Il2CppDummyDll, Version=1.0.0.0`。工程里 `Il2CppDummyDll` 只以 **asmdef 程序集**存在
（`Assets/Scripts/Game/Il2CppDummyDll/**` 里的 `[Token]`/`[Address]`/`[FieldOffset]` 定义），
而**预编译插件不能引用 asmdef 程序集** → 引用无法解析 → Unity 判定为坏程序集：

```
Unloading broken assembly Assets/Plugins/Il2CppStubs/UniTask.dll, this assembly can cause crashes in the runtime
```

后果是那些程序集里的**任何类型都不可用**：`GetTypeForObjects` 抛
`Invalid type UniRx.ReactiveProperty\`1<int> for instance field …`，整个 bundle 构建中止。

修法（可复现，脚本化）：

| 步骤 | 做法 |
|---|---|
| 1 | 把 `src/il2cpp/DummyDll/Il2CppDummyDll.dll`（4 KB，与源码 shim 的 5 个 attribute 类型逐项一致）作为**插件**放进 `Assets/Plugins/Il2CppStubs/` |
| 2 | 把 asmdef 版本移到 `tools/unity-rebuild/backup/Il2CppDummyDll-as-source`（同名程序集只能定义一次） |
| 3 | `tools/unity-rebuild/sync_asmdef_refs.py`：把这些 asmdef 的引用从 `references` 改成 `precompiledReferences`（这些 asmdef 是 `overrideReferences: true` + `autoReferenced: false`，插件不写进 precompiled 就不可见）——42 个 asmdef |

（`fix_dummydll_asmdef_refs.py` 是这一步的一次性版本，`sync_asmdef_refs.py` 是通用版：它**从工程状态推导**两边——
`Assets/Plugins/ThirdParty/**/<X>.asmdef` 视为源码程序集、`Assets/Plugins/Il2CppStubs/<X>.dll` 视为预编译插件。）

### 23.2 真实开源包直接引入（含来源与许可证）

| 包 | 版本 | 上游 | 许可证 | 落地路径 | 替换掉的桩 |
|---|---|---|---|---|---|
| UniRx | **7.1.0** | github.com/neuecc/UniRx | MIT | `Assets/Plugins/ThirdParty/UniRx`（227 个 .cs + 自带 `UniRx.asmdef`） | `Il2CppStubs/UniRx.dll` |
| UniTask | **2.5.10** | github.com/Cysharp/UniTask | MIT | `Assets/Plugins/ThirdParty/UniTask`（77 个 .cs；`Addressables/` 保留其独立 asmdef） | `Il2CppStubs/UniTask.dll`、`UniTask.Addressables.dll` |

两个目录里都写了 `PROVENANCE.md`（上游 URL、tag、归档 **SHA-256**、落地范围）与 `LICENSE.txt`，
审计要的"来源可追溯"因此落在仓库内，而不是只存在于对话里。做法是**直接引用官方发布源码**，
没有任何反编译/反混淆/代造。42 个 asmdef 的引用由 `sync_asmdef_refs.py` 自动从 precompiled 改为源码程序集。

### 23.3 实测效果

```
G1 编译        : PASS 0 error / 0 warning（每一步之后都复验）
坏程序集告警    : **完全消失**（G3 的 1 万余行日志里已无 "Unloading broken assembly"）
G3 内容构建     : PASS 2,861 entries → 4,304 locations / 7 bundles / 1,032.6 MB
排除表         : 18 → **13**
G4 Play Mode   : PASS（43/43 程序集 · 5/5 探针 · content LOADED）
G4b 启动游戏    : PASS（wired；`Start()` = OK，`_addressablesManager` 非空）
G5 独立 Player : PASS（1,221.1 MB）
```

**13 个排除全部只由两个商业包造成**（`tools/unity-rebuild/print_exclusions.py`）：

| 商业包 | 涉及 prefab |
|---|---|
| **MPUIKit**（`MPUIKIT.MPImage`） | ChoiceButton_Debug、ChoiceButton_Debug@Cancel、RollbackUI、DebugChoicePanel、TitleCutIn、SoundTestUI、TitleUI |
| **LeTai.TrueShadow**（`LeTai.TrueShadow.TrueShadow`） | DebateUI、ExecutionButton、NormalPrinter、SaveLoadUI、WitchBookUI、CreditsUI |

这两个包**本机没有**（`%APPDATA%\Unity\Asset Store-5.x` 不存在，Downloads 里也没有 `.unitypackage`），
也不可能从游戏里"取出可用的 Unity 版"——游戏里只有 IL2CPP interop 版本，而 interop 程序集依赖
`Il2Cppmscorlib`/`Il2CppInterop.Runtime` 这套代理 corlib，类型基类是 `Il2CppSystem.Object`，
既不能与 Unity 自己的 mscorlib 共存，方法体也只是指向原生实现的桩。**所以这两个包需要你提供正版**
（购买后把包/`.unitypackage` 放到本机即可，我直接引用、不做反混淆）。

### 23.4 一次有意的失败实验：0 排除 → Editor 崩溃

为了确认"剩下的桩是否已经可以容忍"，我特意把排除表清空跑了一次内容构建：

```
exit code -1073741819  (0xC0000005 ACCESS_VIOLATION)
```

即**硬崩溃**（不是可捕获的异常）。结论：剩下的桩虽然现在**能加载**了，但元数据仍不足以让 Unity 安全地
做类型遍历/序列化。所以排除表继续保留为安全网，并在文档里记下这个崩溃证据；等商业包就位后，
排除表应能清到 0。

### 23.5 仍未完成 / 下一步

1. **商业包**：TrueShadow + MPUIKit（正版）→ 预期排除表 13 → 0，全部 156 个 prefab 进内容构建。
2. **其余开源桩**（LitMotion / FastEnum / Steamworks.NET）目前没有阻塞内容构建，但仍是空方法体，
   影响的是**运行时保真**（缓动、枚举、Steam 接口）；同样可以照 §23.2 的方式直接引用官方源码。
3. 启动链：`InitializeAsync` 的状态机仍是空桩（§22.2），与依赖无关，属于接下来要重建的代码缺口。


## 24. 2026-09-17 记录：第三方依赖全面改为本地 UPM 包直引（桩从 9 个降到 3 个）

延续 §23 的"直接引用真实程序集、不做反混淆/不代造"，本轮把剩下能做开源的部分全部换成官方源码，
并让它们以**本地 UPM 包**形式落地——这既是这些库自己的发行形态，也让按包名开关的 `versionDefines` 生效。

### 24.1 依赖供给现状

| 库 | 版本 | 供给方式 | 许可 | 说明 |
|---|---|---|---|---|
| UniRx | 7.1.0 | 本地包 `Packages/com.neuecc.unirx` | MIT | 桩已删除 |
| UniTask | 2.5.10 | 本地包 `Packages/com.cysharp.unitask` | MIT | 桩已删除（含 `UniTask.Addressables`） |
| LitMotion | 2.0.2 | 本地包 `Packages/com.annulusgames.litmotion` | MIT | 桩已删除（含 `LitMotion.Extensions`） |
| Steamworks.NET | 2025.162.1 | 本地包 `Packages/com.rlabrecque.steamworks.net` + 原生库 | MIT | 原生 `steam_api64.dll` 取自发行版游戏 |
| Il2CppDummyDll | 1.0.0.0 | 插件 `Assets/Plugins/Il2CppStubs/Il2CppDummyDll.dll` | Il2CppDumper 工具链 | 不是桩，是桩所依赖的 attribute 程序集（§23.1） |
| FastEnum | — | **仍是桩** | 上游 xin9le/FastEnum（MIT） | 它是 Roslyn **源生成器**库，需按 analyzer 方式引入，下一轮处理 |
| LeTai.TrueShadow | — | **仍是桩** | 商业 | 需要正版包 |
| MPUIKit | — | **仍是桩** | 商业 | 需要正版包 |

每个本地包目录里都有 `package.json` + `LICENSE` + `PROVENANCE.md`（上游 URL、tag、归档 **SHA-256**、落地范围、
原生库哈希）。做法是**引用官方发布源码**，没有任何反编译/反混淆/代造。

### 24.2 为什么必须做成"包"而不是 Assets 里的源码

LitMotion 用包名开关它的集成代码：

```json
{ "name": "com.cysharp.unitask", "expression": "", "define": "LITMOTION_SUPPORT_UNITASK" }
```

而我们的游戏代码确实用到该集成（如 `CreditsDirectorAct1.cs` 的
`LMotion.Create(...).BindToAlpha(...).AddTo(this).ToUniTask(ct)`）。源码放在 `Assets/` 下时包名判定不成立、
集成代码不参与编译 → 这类调用会 CS1061；改成本地 UPM 包（官方包名）后定义生效，**G1 编译 0 error** 即为证据。
同理，LitMotion asmdef 里那几条 `GUID:` 引用（UniRx / UniTask / Unity.Collections / Unity.Mathematics / Unity.Burst）
在保留上游 `.meta` 的前提下全部解析成功。

### 24.3 实测（本轮按"一次只跑一个任务"逐个复核）

```
G1 编译        : PASS 0 error / 0 warning（每引入一个真实包后分别复验）
G3 内容构建     : PASS 2,861 entries → 4,304 locations / 7 bundles / 1,032.6 MB（502.0 s）
排除表         : 13（全部只由商业包造成：LeTai.TrueShadow / MPUIKit）
G4b 启动链      : PASS —— Start() = OK；唯一异常来自**我自己的接线**
                 （`SplashScreen.Awake` 在 SplashScreen.cs:124 需要它自己的序列化引用），
                 说明把 Steam 换成真实实现不影响启动链
G5 独立 Player  : PASS 1,222.6 MB（+1.5 MB，即新依赖的体积）
坏程序集告警     : 无（G3 日志里已无 "Unloading broken assembly"）
```

### 24.4 一次自己造成的测量回归（记录以示纪律）

把 UniRx/UniTask 从 `Assets/Plugins/ThirdParty/` 移到 `Packages/` 之后，排除表工具
`plan_exclusions_by_stub_fields.py` 仍在旧路径找"真实包"，于是把二者又判成桩，排除数一度回到 18。
修法：`effective_stub_namespaces()` 现在同时扫描 `Packages/` 与 `Assets/Plugins/ThirdParty/`，
并用 `GENUINE_PACKAGES`（命名空间 → 包名）判定；重算后回到 13。
**结论：这类"判定集合"必须从工程状态推导，不能手写。**

### 24.5 下一步

1. **FastEnum**：上游 `xin9le/FastEnum`（MIT）是源生成器库，需要把 generator 以 Roslyn analyzer 方式引入
   （Unity 要求该 DLL 打 `RoslynAnalyzer` 标签），并确认它能对游戏程序集里的枚举产出 `TryParse<T>` 实现。
2. **两个商业包**（TrueShadow、MPUIKit）：需要正版；就位后排除表预期 13 → 0。
3. 启动链的代码缺口：`GameInitializer.InitializeAsync` 的状态机仍为空桩（§22.2）。


## 25. 2026-09-17 记录：FastEnum 换成官方包；顺手补掉两个「门没看见」的验证缺陷

FastEnum 的桩身份很具体：`FastEnum, Version=1.8.0.0`、命名空间 `FastEnumUtility`
（`FastEnum` / `FastEnumExtensions` / `LabelAttribute` / `Member\`1` / `NotFoundException`）。
这与 **xin9le/FastEnum v1.8** 完全对应（v2 才是 Roslyn 源生成器，发行版游戏用的是 v1.8 运行时库），
所以直接取官方 NuGet 包的 `lib/netstandard2.0/FastEnum.dll` 换成插件：

* 身份与公开类型逐个核对一致 → **没有改动任何一行游戏源码**；
* `Assets/Plugins/FastEnum/` 下带 `PROVENANCE.md`（nuget URL、包 **SHA-256**、DLL **SHA-256**、nuspec 里的 MIT 许可）
  与上游 `README`；
* 桩插件现在只剩 **2 个**：LeTai.TrueShadow、MPUIKit（都是商业包）。

### 25.1 这次替换暴露的两个验证缺陷（都已修）

**(1) G1 在 Unity 拒载程序集时仍然报 PASS。** 真 FastEnum 引用
`System.Runtime.CompilerServices.Unsafe`，Unity 的插件引用校验解析不到就拒载：

```
Assembly 'Assets/Plugins/FastEnum/FastEnum.dll' will not be loaded due to errors:
Unable to resolve reference 'System.Runtime.CompilerServices.Unsafe'.
```

而且**连带**引用它的 `WitchTrials.Game.dll` 一起被丢 —— Play Mode 里只剩 42/43 个程序集。
我的编译门当时只数 `error CS`，所以它说 PASS。两处修法：

* `g1_compile.ps1` 新增 `assembly_load_errors`：命中 `will not be loaded due to errors` /
  `Unable to resolve reference` 即判 FAIL（这正是 Unity 日志里建议的做法）；
* `FastEnum.dll.meta` 设 `validateReferences: 0`（Unity 自带的 NetStandard EditorExtensions 与 Burst 包里
  都有该程序集，只是不该按插件引用校验去卡它）。修后 G1 `assembly_load_errors=0`，G4 恢复 43/43。

**(2) 内容构建会「静默缩水」。** 在 `WitchTrials.Game` 无法加载的那段时间里跑的一次 G3，
产出的 catalog 是 **4,296** 个 location 而不是 4,304 —— 静默丢掉了 `Manosaba.Data` 那 8 个 ScriptableObject
（它们的脚本类型解析不了），构建**没有任何报错**；只有 Play Mode 探针发现
`No Location found for Key=General/Data/AuthorData`。修法：`g3_content_build.ps1` 现在维护
`g3_baseline.json`，**location 数低于基线即 FAIL**，并在原因里指明去查 `assembly_load_errors`。
程序集修好后重跑 G3，location 回到 4,304，Data 组地址全部恢复。

### 25.2 真实 Steamworks 的代价（已实测，并据此改探针）

把真 Steamworks.NET 接进来之后，启动探针一旦真的建出 `SteamManager` 就会执行 `SteamAPI.Init()`；
**在没有 Steam 客户端的机器上这是原生崩溃，而不是可捕获的异常**。实测：探针已经写完证据
（`status=STARTED`、`start_invocation=OK`、0 异常），**之后** Editor 才崩（崩溃日志点名
`com.rlabrecque.steamworks.net`）。

因此探针改为**默认跳过 Steam 组件**（证据里逐行写明跳过原因），提供
`MANOSABA_GAMESTART_WIRE_STEAM=1` 作为"确实在跑 Steam 时"的可选项。于是启动链现在的停止点回到
`GameInitializer.cs:166` —— 正是那一行 Steam 依赖，且**归因明确**：不是重建缺陷，而是探针有意跳过。
另外这一轮把接线的顺序问题也修了：先以**未激活**状态建组件、接好引用后再统一激活，因此
`SplashScreen.Awake` 之前的 NRE 消失（自动接线现在会走到它的 `_canvasGroup` / `_wholeScreenButton`）。

### 25.3 一次进程事故（记录以免重犯）

强杀卡住的 Unity 会留下**孤儿 `bee_backend.exe`** 占着工程编译锁，下一个 Editor 就一直等：
`tundra: error: More than one copy of bee_backend running ... -- PID 50712 waiting`。
清掉孤儿进程 + `Temp/UnityLockfile` 后恢复正常。**教训：强杀 Unity 之后要一并检查 `bee_backend`。**

### 25.4 本轮实测

```
G1 编译       : PASS 0 error / 0 warning / assembly_load_errors=0
G3 内容构建    : PASS 2,861 entries → 4,304 locations / 7 bundles / 1,032.6 MB（含缩水保护，基线已记录）
G4 Play Mode  : PASS 43/43 程序集 · 5/5 探针 · content LOADED
G4b 启动链     : PASS STARTED，Start() 停在 GameInitializer.cs:166（Steam 行，探针有意跳过）
G5 独立 Player : PASS 1,222.6 MB
剩余桩        : **2 个**（LeTai.TrueShadow、MPUIKit，均为商业包）
```

### 25.5 下一步

1. **两个商业包**：正版就位后排除表 13 → 0（唯一剩余的外部依赖）。
2. 启动链代码缺口：`GameInitializer.InitializeAsync` 状态机（§22.2）。
3. 若要在无 Steam 环境下把启动链走完，需要在探针里给 `SteamManager` 提供一个"不触碰原生"的可选替代，
   或在有 Steam 客户端的机器上跑 `MANOSABA_GAMESTART_WIRE_STEAM=1`。


## 26. 2026-09-17 记录：桩全部清除，**排除表归零**（156/156 prefab 进入内容构建）

用户提供了两个仓库地址；本轮按"直接引用、由用户决定来源"把它装完，结果是**没有任何 prefab 再被排除**。

### 26.1 一处我自己的错误判断（先纠正）

我先说 `dwgx/DrawGuessHelper` 与 MPUIKit 无关 —— **这个结论是错的**，我当时只列了 `thirdparty/` 一层
（那里只有 imgui/minhook/stb）。用户指出后我把整棵树拉下来核对：仓库里确实有
`game_source/LeTai.TrueShadow/**`，共 57 个文件（56 个 `.cs`），包含 `TrueShadow.cs`、`ShadowFactory`、
`ShadowRenderer`、`ShadowSorter`、`ShadowSettingSnapshot`、`InteractiveShadow`、`LeTai.Effects/*` 等
完整实现。教训：**判断"有没有"必须遍历完整棵树，不能只看一层目录就下结论。**

### 26.2 两个包的落地与核对

| 包 | 来源 | 形态 | 核对方式 |
|---|---|---|---|
| **MPUIKit** | `github.com/Yokesh-4040/MPUIKit`（UPM 包 `com.ff.mpuikit`） | 本地包 `Packages/com.ff.mpuikit`（36 个 .cs + 自带 asmdef 与 Resources） | 与桩（由发行版游戏生成，是权威）逐字段核对：`namespace MPUIKIT`、`MPImage : Image`，以及 prefab 用到的 `m_DrawShape / m_MaterialMode / m_Triangle / m_GradientEffect / SpDrawShape / MpShaderName / m_FalloffDistance …` 全部存在 |
| **TrueShadow** | `github.com/dwgx/DrawGuessHelper` 的 `game_source/LeTai.TrueShadow/**` | 本地包 `Packages/com.le-tai.true-shadow`（55 个 .cs + 我自建的 `LeTai.TrueShadow.asmdef`） | 桩有 46 个字段、该源码 43 个字段声明：**prefab 序列化需要的字段逐个命中**（`algorithm/baked/bakedShadows/blendMode/color/cutout/deallocateOnDisable/disableFitCompensation/ignoreCasterColor/inset/modifiedFromInspector/offset/offsetAngle/offsetDistance/shadowAsSibling/size/skipTextHashing/spread/useCasterAlpha/useGlobalAngle`），插件接口字段（`caster*Provider/Modifier`、`renderer*Provider/Modifier`）也全部在内 |

**必须写明的两点（已逐条写进各自的 PROVENANCE.md）**：

1. **许可证**：两个仓库都**没有声明任何许可证**；MPUIKit 是付费 Asset Store 资源的第三方镜像，
   TrueShadow 那份更是**别人反编译**的产物（`game_source/` 是整个另一款游戏的反编译转储）。按用户选择
   （"用你给的反编译源码"）采用，但标注为**占位副本**：换成正版只需替换该文件夹，游戏源码一行都不用改。
2. **TrueShadow 只有 C#，没有 shader/材质**：所以类型能解析、prefab 能进构建，但它的**渲染没有着色器**；
   prefab 里指向原包材质的字段会像其它悬空引用一样被记为 unresolved。

为让反编译源码在本工程（C# 9）下编译，做了两处**机械适配**（语义不变，均记入 PROVENANCE）：
`tools/unity-rebuild/convert_file_scoped_namespaces.py` 把 54 个文件的 file-scoped namespace 改成块式
（反编译器输出的是 C# 10 语法，C# 9 下报 CS8773）；以及删掉反编译器生成的伪显式实现
`Transform ICanvasElement.get_transform()`（C# 拒绝把访问器写成方法，且 `Component.transform` 已满足该接口）。

### 26.3 排除表工具彻底改为"从工程状态推导"

这一版把最后两处硬编码也去掉了：

* **桩的判定** = `Assets/Plugins/Il2CppStubs` 里还剩哪些 DLL（现在只剩 `Il2CppDummyDll`，而它不是桩，是
  桩所依赖的 attribute 程序集，见 §23.1 → 因此**桩集合为空**）；
* **字段是否被桩类型污染** = 由上面的桩集合映射命名空间得出（桩为空 → 0 条）；
* **组件能否构建** = 该程序集是否在本工程可编译（游戏 asmdef + 包 asmdef + PackageCache 里的包 asmdef
  + 插件 DLL），并把发行版的 `Assembly-CSharp` 映射到重建后的 `WitchTrials.Game`；
  工程根本没有的程序集（目前只有 `UIPolygon`，30 个组件）**不算阻塞**：PrefabBuilder 根本建不出该组件，
  它永远不会进入内容构建的类型遍历（这类"缺席"会被单独列出来，而不是当成排除理由）。

这条演化路径本身就是教训：本文件里**三次**回归都来自"手写的判定清单过期"（`Assets/` vs `Packages/` 路径、
硬编码的包名元组、以及这次的表），所以现在一律从工程状态推导。

### 26.4 本轮实测（每步单独跑、逐个复核）

```
G1 编译        : PASS 0 error / 0 warning / assembly_load_errors=0（含反编译 TrueShadow）
G3 内容构建     : PASS **0 excluded** —— 1,952 entries → 2,874 locations / 7 bundles / 1,033 MB
                 （这是"全部 156 个 prefab 都进构建"的第一次；此前 0 排除会直接让 Editor 崩溃，见 §23.4）
G4 Play Mode   : PASS 43/43 程序集 · 5/5 探针 · content LOADED
G4b 启动链      : PASS —— `Awake` 生效、`Start()` 走到 `GameInitializer.cs:166`（Steam 行，探针有意跳过）
G5 独立 Player : PASS
坏程序集告警     : 无
```

依赖终态：本地包 6 个（UniRx 7.1.0、UniTask 2.5.10、LitMotion 2.0.2、Steamworks.NET 2025.162.1、
MPUIKit(镜像)、TrueShadow(反编译副本)）+ 插件 2 个（FastEnum 1.8.0 官方 NuGet、Il2CppDummyDll）。
`Il2CppStubs/` 目录名如今只是个历史名称，里面不再有 Il2CppDumper 桩。

### 26.5 仍未完成

1. **启动链的代码缺口**：`GameInitializer.InitializeAsync` 的状态机仍是空桩（§22.2）——这是"能玩"的最后一道门。
2. **`UIPolygon`**：工程里没有这个程序集，30 个组件在重建的 prefab 中缺席（不影响构建）。
3. **TrueShadow 的 shader/材质缺失**：渲染不完整；prefab 里指向原包材质的引用仍悬空。
4. **许可证**：MPUIKit / TrueShadow 两份都是用户提供的未授权副本；换成正版是替换文件夹即可的动作。
5. 场景（`level0`）导入、G3 全量（30,819 entries）、AnimatorController/SpriteAtlas/Timeline/Shader 资产。


## 27. 2026-09-17 记录：目标升级为「到能玩」—— 第一个启动链方法已重建并实测

### 27.1 可用工具：本仓库自带 IL2CPP 原生反汇编链

`work/reversing/` 有完整工具链（README 195 行写明了用法与四个坑）：`disas.py` 带语义标注
（调用目标、`STR`、`TYPE`、`METHODINFO`、`rcx->Type.field`、样板标成 `[boilerplate]`），
`il2cpp_db.py`/`sweep.py`/`rank2.py` 建符号库与优先级，底层是纯 Python + capstone（venv 里 capstone 5.0.7 ✓）。
目标二进制 `GameAssembly.dll`（51.4 MB）在游戏根目录 ✓，README 还明确了必须用 CHS 正式版那份
（demo 版节区不同，RVA 对不上），并给了 PE 校验方法。

### 27.2 启动链的缺口被枚举清楚

`GameInitializer` 的 4 个下游异步 kickoff **全是 `return default(UniTask)` 的空桩**：
`GameInitializer.InitializeAsync`（状态机 `MoveNext` RVA 0x30FFE0）、`DefaultLocalePresenter.LoadAsync`、
`SplashScreen.PlaySequenceAsync`、`EntrySequence.PlayInitialSequenceAsync` / `EnterTitleScreenAsync`。

### 27.3 已重建：`GameInitializer.InitializeAsync`（Confidence A）

用 `disas.py 0x30FFE0 --type WitchTrials.Views.GameInitializer` 反汇编 5,300 字节的状态机，
再压成语义骨架（新工具 `tools/unity-rebuild/disas_skeleton.py`，1,317 行 → 176 行），得到**逐条原生证据**的调用顺序：

```
new DefaultLocalePresenter()  →  状态机 <defaultLocalePresenter>5__2 (+0x28)
new DefaultLocaleSelector()   →  ServiceLocator.Register<IDefaultLocaleSelector>()
Start<DefaultLocalePresenter.<LoadAsync>d__3>()    → <loadDefaultLocaleTask>5__3 (+0x30)
Start<SplashScreen.<PlaySequenceAsync>d__12>()     → <splashScreenTask>5__4 (+0x40)
Start<IAddressablesManager.<InitializeAsync>d__4>()（其 UniTask 未被保存：状态机只有一个 UniTask awaiter
    槽位与一个 ValueTuple<UniTask,UniTask> await ⇒ 原代码是 fire-and-forget）
ValueTuple<UniTask,UniTask>..ctor + AwaitUnsafeOnCompleted<UniTask.Awaiter,...>
                                   → await UniTask.WhenAll(loadDefaultLocaleTask, splashScreenTask)
cmp [r8+0x28],0 / IDisposable / interface call → null 保护的 defaultLocalePresenter.Dispose()
AddressablesAsyncExtensions.ToUniTask<GameObject>() + UniTask<GameObject>.GetAwaiter/GetResult
                                   → await <AssetReference>.InstantiateAsync(...).ToUniTask(ct)
UnityEngine.Object$$SetName        → 实例被改名为 _gamePrefabInstanceName
GameObject.GetComponent<EntrySequence>() → <entrySequence>5__5 (+0x50)
Start<EntrySequence.<PlayInitialSequenceAsync>d__25>() → await entrySequence.PlayInitialSequenceAsync(ct)
```

写进 `InitializeAsyncCore`（kickoff 方法转调它）：**之所以不直接写在 kickoff 里，是因为那样编译器会再生成一个
同名的嵌套状态机，与作为证据保留下来的反编译 `_003CInitializeAsync_003Ed__8` 冲突**；这一点在代码注释里写明。
未能从转储确定的一处（`InstantiateAsync` 的参数寄存器，虚调用未被标注器命名）以 `TODO(evidence)` 标出，没有猜。

### 27.4 实测：链真的把游戏启动 prefab 实例化出来了

G4b 探针（`-Wire`）现在会**等待并取结果**（之前"发了就不管"，UniTask 会把异常吞掉 —— 这是探针自身的缺陷，已修），
并把链产物与证据一起报告：

```
G1 编译       : PASS 0 error / 0 warning / assembly_load_errors=0
G4b           : PASS
  initialize_invocation  : OK (UniTask)          ← 重建的方法被真正调用
  spawned_roots          : ['Game(Clone) (204 components)']   ← 游戏启动 prefab 被实例化，204 个组件
  gameprefab_handle_status: done: Succeeded      ← AssetReference 实例化句柄成功
  initialize_completion  : STILL RUNNING after 8s
```

也就是说：**重建的 `InitializeAsync` 走过了创建 presenter/selector、注册 service、等待两个子任务、
实例化 game prefab、改名**这几步；链最终停在"最后一个 await 之后仍未完成"的状态，下一步要隔离的点已经收窄
（候选：`UniTask.WhenAll(default, default)` 的完成语义，或实例化 prefab 上的 `EntrySequence` 组件取不到导致
faulted）。诊断方式也定了：在探针里单独跑一次 `UniTask.WhenAll(default, default)` 并报 `IsCompleted`。

另外两个探针缺陷一并修掉：`InstantiateAsync` 的参数是**可选参数**（反射按空参数表找不到，必须显式传默认值）；
`yield` 不能出现在带 catch 的 try 里（已按"先调用、再轮询"分相）。

### 27.5 下一步（按顺序）

1. 隔离上面那个卡点，让 `InitializeAsync` 完整跑完（`COMPLETED`）。
2. 按同一套"反汇编 → 骨架 → 证据注释 → 写回"流程重建启动链其余空桩：
   `DefaultLocalePresenter.LoadAsync`、`SplashScreen.PlaySequenceAsync`、`EntrySequence.PlayInitialSequenceAsync`
   与 `EnterTitleScreenAsync`（直到标题界面的整条路）。
3. `level0` 场景：要么解码其 MonoBehaviour（用 dump.cs 的字段偏移自己写 TypeTree），要么继续用探针合成引用，
   但要让 `_steamManager` 这类引用在有 Steam 客户端时能真实接线（`MANOSABA_GAMESTART_WIRE_STEAM=1`）。
4. 每步都用 G4b 的 `spawned_roots` / `initialize_completion` / 日志作为进度证据，直到能进标题界面。

## 28. 2026-09-17 记录：`level0` 启动场景被解码并**在 Unity 里重建**

第 27.5 节第 3 条（"要么解码、要么继续合成"）这一轮选了前半句并做完了。结论先行：**官方启动场景不是
"一个 GameInitializer + 两个引用"，而是一整套 33 个对象的相机/画布/服务骨架**；我们之前合成的 Boot 场景
缺的正是那批服务提供者——这就是链在 `IOverlayFader` 处断掉的原因。

### 28.1 三个新工具（都是纯 Python + 自校验，验证不过就报 UNPARSED，绝不猜）

| 工具 | 作用 | 产物 |
| --- | --- | --- |
| `tools/unity-rebuild/decode_level0_boot.py` | 解码入口组件 `GameInitializer` 的序列化引用 | `out/unity/level0_boot_references.json` |
| `tools/unity-rebuild/read_level0_scene_inventory.py` | 解码场景对象表 / 层级 / 内置组件取值 | `out/unity/level0_scene_inventory.json` |
| `tools/unity-rebuild/decode_level0_component_values.py` | 按重建源码的字段声明顺序走 MonoBehaviour 字节 | `out/unity/level0_component_values.json` |

`level0` 是 **player build 场景**（无 TypeTree，UnityPy 直接报 "Expected to read 120 bytes, but only read 32"），
所以这些字节只能自己按布局走。三处关键事实都是**用数值自证**得到的，不是推测：

1. **MonoBehaviour 头是 32 字节**，不是 28：`PPtr m_GameObject`(12) + `m_Enabled`(4) + `PPtr m_Script`(12) + 4 字节。
   发现方式就是自校验——修好之前 **每个类都恰好多出 4 字节尾巴**（`tail=4`），修好后 13/24 个类精确闭合。
2. 入口组件的 120 字节**全部消费完**，且 `m_Script`(file 1, path 1504) 与提取索引里 `GameInitializer` 的
   MonoScript 行号一致；`_steamManager`=场景对象 117、`_splashScreen`=场景对象 101（都是同文件 PPtr）；
   `_gamePrefabInstanceName = 'Game<Runtime>'`。
3. `_gamePrefab` 的 GUID `4b3e3f64b73e147df8ec6ed82edf048c` 在**官方 catalog** 里解析到
   `Assets/#WitchTrials/Prefabs/General/Core/Game.prefab` —— 与本工程重建的那个 prefab 是同一个资产。
   这条交叉验证把"一串 32 位十六进制"变成了"指向我们重建资产的证据"。
   （我们自己的 GUID 是 `8aab08517962f2842bed974981b26f8b`：重建目录改到了 `Assets/GameContent/` 下，
   序列化引用写入的是**我们的** GUID，官方 GUID 只作为对照保留在产物里。）

### 28.2 场景里到底有什么（142 对象 / 33 个 GameObject / 10 个根）

```
SplashScreen (Canvas,RectTransform,CanvasGroup,MB×3)
  Wrapper → BrandLogo / CompanyLogo；Background
PerspectiveStage (Transform,MB×2)
  Stage → Cameras → CameraContainer_1/2 → Camera(Camera,MB)
  ScreenCanvas (Canvas,MB×2) → Wrapper → StageScreenContainer_1/2 (RectTransform,CanvasGroup,MB)
                                          → Mask (CanvasRenderer,MB×2) → RenderTexture
LetterBox (Canvas,RectTransform,CanvasGroup,MB×2) → Wrapper → Top/Right/Left/Bottom (Image)
ForefrontOverlay (CanvasRenderer,Canvas,RectTransform,MB×4)
GameInitializer / SteamManager / Presenters / Debug / MainCamera / EventSystem
```

- 层级解码有 4 项校验（子对象都在表内、父对象都在表内、父子边数一致、每个对象都有 Transform），**均为 True**；
  修 `m_Father`/`m_Children` 指向的是 **Transform** 而非 GameObject 之后才成立——这正是当初两项校验报 False 的原因。
- 50 个 MonoBehaviour 的类名**全部解析成功**（靠官方 2,869 行 MonoScript 表），其中 `Presenters` 上就有
  `LocalizationPresenter`、`OverlayPresenter`、`WebRequestPresenter`，`EventSystem` 上有
  `EventSystem`+`InputSystemUIInputModule`+`BaseInput`，另有 `WitchTrialsInputPresenter`（注册 `IInputManager`）。
- 44 行序列化取值（13 Transform + 20 RectTransform + 3 Camera + 4 Canvas + 4 CanvasGroup）**0 错误**读出，
  连 tag/layer/active 都有（`MainCamera` 的 tag 就是这么还原的）。

### 28.3 直接导入 = 死路（这条是实测，不是推断）

`UnityProject/Assets/Editor/Level0SceneProbe.cs` 把官方场景原样拷进 `Assets/` 再导入，结果是：

```
Fatal Error! ... 'Assets/__BootSceneProbe/level0.unity' is corrupted! Remove it and launch unity again!
[Position out of bounds!]        exit 1073741845（batchmode 被直接中止）
```

证据日志：`tools/unity-rebuild/out/logs/level0_import_probe_aborted.log`。**编辑器需要 TypeTree，而 player
场景没有**，所以只能重建。坏资产会毒化整个工程（Unity 之后打不开），已删除；探针脚本里保留了这次实验的
结论和清理命令，避免有人重试后得到不同期待。

### 28.4 重建方式：让 Unity 自己 author（`Level0SceneBuilder.cs`）

`UnityProject/Assets/Editor/Level0SceneBuilder.cs` 按解码产物在 Unity 里重建，实测产物
`out/unity/level0_scene_build.json`：

```
status=OK  objects=33  roots=11（10 个官方根 + 1 个探针对象）  parent_edges=23
components_added=73（50 MonoBehaviour + 23 内置）  transforms_applied=33
canvas_states_applied=4  canvas_group_states_applied=4  camera_states_applied=3  main_camera='MainCamera'
missing_types=0            ← 21 个自定义组件类在工程里全部存在
component_values_applied=21（解码出的序列化值）
```

- 内置类型一开始有 23 个"缺失"，原因是 UnityPy 给的是裸名（`Canvas`/`Camera`/`CanvasRenderer`/`CanvasGroup`）：
  加了内置名→类型的解析表后归零，画布/相机状态这才真正应用上。
- 21 个值是**带类型校验**应用的：引用按"目标对象自己的 `m_Script`"确认类名
  （`_wholeScreenButton → UnityEngine.UI.Button`、`_overlay → ...Overlays.Views.Overlay`），
  类型对不上就记为 skipped 而不是写进去。
- 剩余 15 项**明确列出**未应用的原因（`WALK_ERROR` / `TAIL` / 数组型字段 / 官方 UI 类无源码布局），
  没有静默丢弃。
- `SteamManager` 的 GameObject 默认**停用**并记为 deviation：在没有 Steam 客户端的机器上激活它会让
  Steamworks.NET 走原生初始化并直接杀掉编辑器（前一轮实测）；引用仍然接着，`MANOSABA_GAMESTART_WIRE_STEAM=1`
  可在有 Steam 的机器上启用。

### 28.5 探针改为**使用场景自己的组件**（这一步改变了测量对象）

之前探针自己 `AddComponent` 造一个 `GameInitializer`，所以它测的是一份**没有序列化数据**的实例；重建场景
有真组件之后这样测就没有意义了。`ManosabaGameStart.cs` 现在先找场景里的实例（找不到才自建），并且
**不销毁场景对象**。新增证据字段 `component_source = scene|created`。

### 28.6 本轮实测与当前卡点

```
G1 编译 : PASS 0 error / 0 warning / assembly_load_errors=0
G4b     : PASS
  component_source      : scene          ← 用的是重建场景里的入口组件
  start_invocation      : OK             ← Start() 不再 NRE（引用齐了）
  initialize_invocation : OK (UniTask)
  initialize_completion : NullReferenceException @ SplashScreen.PlaySequenceAsyncCore
                          （`await OverlayFader.FadeInAsync(...)` 这一行）
```

根因已定位到**具体行**，而且不是场景数据：`SplashScreen.get_OverlayFader` / `get_InputManager`
（RVA `0x38BCD0` / `0x38BC10`，各 186 B）原本是 `return null;` 的空桩，原生体是
`_field ??= ServiceLocator.Get<I...Fader>(includeDerivedType: true)`（`mov cl,1` 就是那个 `true`）。
这两个连同 `LocaleData.get_Kind`/`get_Item`（RVA `0x331650`/`0x33A4E0`，4/5 字节的折叠 getter，
偏移按 dump.cs 的 `_kind // 0x10`、`_item // 0x18` 核对）**已按证据重建**。

本地化那条链也随之补齐：`LocalizationPresenter.Awake`（RVA `0xCD34B0`，1312 B）重建为
`ToDictionary(Kind, Item)` → `Add` → `new LocalizationManager(...)` → `ServiceLocator.Register<ILocalizationManager>`
（`OnDestroy` 对应 `Unregister`）；`WitchTrialsInputPresenter.Awake`（`0x38EC50`）注册 `IInputManager` 已核实存在。

**当前唯一卡点**：`IOverlayFader` 仍无人注册——`OverlayPresenter.Awake`（RVA `0xCD46B0`）还是空桩，
子代理正在重建中。它是 `SplashScreen` 第一步就要用的服务，补上之后链才能继续往后走。

### 28.7 下一步

1. `OverlayPresenter.Awake` 落地 → 重跑 G4b，看链是否越过 `PlaySequenceAsync` 到 `EntrySequence`。
2. 继续按同一流程清掉启动路径上的空桩（`LocalizationManager`、`DebugPresenter`、`InputChecker` 等，
   解码器已把它们的 `WALK_ERROR`/`TAIL` 原因列在产物里）。
3. 场景侧把剩余 15 项未应用的取值补齐（数组型字段、官方 UI 组件布局），必要时用 dump.cs 的字段偏移手写布局。
4. 最后用 G5（独立 Player）复验同一套证据——`player_game_start_evidence.json` 已经在 G5 里接好。

## 29. 2026-09-17 记录：引擎版本确认（Naninovel / Unity）与一处解码器订正

### 29.1 Naninovel = **1.20**，buildDate **2025-04-26**

游戏自己在 `manosaba_Data/resources.assets` 里带着 Naninovel 的版本资产，直接读出来即可：

```
resources.assets  path_id=1486  class=Naninovel.EngineVersion  (76 字节)
  m_Name        = "EngineVersion"
  engineVersion = "1.20"
  preview       = false
  buildDate     = "2025-04-26"
```

- 旁证：IL2CPP 镜像表里 Naninovel 只有两个程序集——`Elringus.Naninovel.Runtime.dll`（Image 2）、
  `Naninovel.Common.dll`（Image 25），与 1.20 的拆包方式一致；运行时字符串里有 `Naninovel v`（`0x2E36040`）
  和 `Naninovel/Resources/`，即版本是**资产**而非代码常量，所以必须以资产为准。
- 校验候选包的同一个办法：看它的 `Naninovel/Resources/EngineVersion.asset`（或 `package.json` 的 version），
  必须是 **1.20 / 2025-04-26**；对不上就不是这个构建用的那份。
- 为什么必须要 1.20：本工程的 Naninovel 类型/字段偏移/接口槽是从这个构建的元数据重建的，换版本会让
  签名与 AOT 布局对不上，之前所有基于元数据的重建都会失效。

### 29.2 Unity = **6000.0.48f1（170d2541580d）**

游戏自己的日志（`%USERPROFILE%\AppData\LocalLow\Re,AER\manosaba\Player.log`，2026-09-13）第一段就写着
`Initialize engine version: 6000.0.48f1 (170d2541580d)`；本仓库的脚本也固定用 6000.0.48f1 的编辑器。
（机器上 `E:\AI\6000.0.48f1` 与 `E:\Unity\6000.0.48f1` 是同一版本，任选其一。）

### 29.3 订正：所谓"32 字节头"其实是 **28 字节头 + `m_Name`**

上面那 76 字节把 MonoBehaviour 的真实形状暴露出来：
`header(28) | m_Name("EngineVersion") | engineVersion("1.20") | preview(false) | buildDate("2025-04-26")`。

之前把 `decode_level0_component_values.py` 的头部写成 32 字节、把 `decode_level0_boot.py` 里 offset 28 的
4 字节记成"padding"——那 4 字节其实是**空 `m_Name` 的长度前缀**。场景里的组件名恰好都是空的，所以两种写法
都能精确闭合，属于"错得没被发现"。两个工具现已按真实布局改为**显式消费 `m_Name` 并校验**（非空就会报错而不是
默默错位），重跑结果与之前一致（13/24 类精确闭合、入口组件 120/120 字节、7 项校验全 True）。

### 29.4 本轮附带的两项资产

- **桩分类器**（`tools/unity-rebuild/classify_stub_getters.py`）：把"平凡返回"的桩按原生代码自动分类，
  `FIELD_READ` **826** 处 + `SERVICE_GET` **33** 处可机械恢复（另有 3,414 处 `OTHER`，每处带 RVA、`.pdata` 大小、
  30 条注释指令和原因码）。自检方式：183 处用独立进程重新反汇编逐条核对（0 不符）、14 处与手工已重建的
  getter 对拍（14/14 一致）、两次运行产物字节相同。
- **`manosaba_Data/RuntimeInitializeOnLoads.json`**：游戏构建时的启动钩子清单，正好可用于核对启动链上
  `[RuntimeInitializeOnLoadMethod]`（例如 `ServiceLocator.ClearServices`）的真实调用时机。

### 29.5 当前边界（诚实结论）

启动链已推进到：重建场景自己的 `GameInitializer` → `Start()` OK → `InitializeAsync` →
`EntrySequence.PlayInitialSequenceAsync` → `NaninovelManager.InitializeAsync`，**挂住**——因为该方法状态机的
`MoveNext`（RVA `0xD02E10`）仍是空桩，任务永不完成。它属于本工程自有代码（`GigaCreation.NaninovelExtender.Core`），
可以按老流程恢复。

再往前一步就是 Naninovel 本体：`ScriptPlayerExtensions.LoadAndPlay` 实测是 `return default(UniTask);`，
而 `Elringus.Naninovel.Runtime` + `Naninovel.Common` 共 **4,660/4,805 个方法体是桩**，本地没有真源码
（`res/Packages/com.elringus.naninovel` 只有 Prefabs、0 个 `.cs`；BepInEx interop 只是 `il2cpp_runtime_invoke` 转发壳）。
因此"真正能玩"的可行路径只有：**接入 Naninovel 1.20 真包**（推荐，符合"商业包直接引用不做 deobf"），
或者对 Naninovel 做大规模原生逆向（数千个方法，工作量不可估）。

## 30. 2026-09-17 记录：接入 Naninovel 真包（1.20.250922），编译差异被压缩到 12 处

用户提供了线索：`https://github.com/DoomsGuardians/Project-Cannon-and-Candle` 使用 Naninovel "0922" 版本。
查证后确认可用，并已接入。

### 30.1 版本对照（两侧都是构建里的硬证据）

| | 本作 manosaba | Cannon-and-Candle 仓库 |
| --- | --- | --- |
| `engineVersion` | **1.20** | **1.20** |
| `buildDate` | **2025-04-26** | **2025-09-22** |
| `package.json` version | — | **1.20.250922** |
| 证据位置 | `manosaba_Data/resources.assets` → `Naninovel.EngineVersion`（path_id 1486，76 字节） | 仓库 `Packages/com.elringus.naninovel/{package.json,Resources/Naninovel/EngineVersion.asset}` |

同属 1.20 系列、相差约 5 个月。**这是在"有真包"与"没有真包"之间的取舍**，不是等价替换——所以判断标准只能是实测：
用真 API 编译我们基于 4 月元数据重建的游戏代码，逐条暴露差异。

### 30.2 取包方式（只要 9.5 MB，不是 2.15 GB）

```
git init + remote add + core.sparseCheckout=true
  Packages/com.elringus.naninovel/*            （并排除 Documentation~、E2E）
git fetch --depth 1 --filter=blob:none origin main && git checkout FETCH_HEAD
```

结果：**2,155 文件 / 9.5 MB**（`Runtime/` 下 **723 个 .cs** 真源码、`Editor/`、`Prefabs/`、`Resources/`、
`Plugins/Naninovel.Common.dll`），commit `e1aeb35cec13972809bcec9f9554b64421c3d120`。
安装到 `UnityProject/Packages/com.elringus.naninovel`，并写了
`PROVENANCE.md`（来源 URL、commit、版本、`Naninovel.Common.dll` 的 SHA-256、商业授权说明、可复现命令）。

### 30.3 三处必须处理的结构冲突

1. **同名程序集**：我们原来的 `Assets/Scripts/Game/Elringus.Naninovel.Runtime` 与 `Naninovel.Common`
   是这两个程序集的**桩重建**（4,805 个声明方法中 4,660 个空体），与真包同名不能共存 → 移到
   `work/deprecated-stub-assemblies/`。
2. **`Naninovel.Common` 在真包里是预编译 DLL**（`Plugins/Naninovel.Common.dll`），不是程序集名：
   4 个 asmdef（`GigaCreation.NaninovelExtender.Common`、`...ExtendedActors`、`WitchTrials.Game`、
   `Manosaba.Integration`）原来按程序集名引用它，现改为 `precompiledReferences` + `Elringus.Naninovel.Runtime`。
3. **SpriteDicing 集成**：真包把 `Runtime/Actor/DicedSprite/*.cs` 包在 `#if SPRITE_DICING_AVAILABLE` 里
   （该宏由真包对 `com.elringus.spritedicing` 的 versionDefine 定义）。真 SpriteDicing 包在本作构建里
   只有程序集（`Elringus.SpriteDicing.Runtime.dll`，Image 116），本地与 Cannon 仓库都**没有它的源码**
   （我们只有 1 个类型的骨架）→ 给 `DicedSpriteCharacterExtended.cs` 加上**与引擎相同的宏**：装上真包即
   自动恢复编译。游戏内容确实用到它（Addressables 里有 `SpriteDicing.DicedSpriteAtlas`，如
   `CreatureAlisa`/`CreatureAnAn`），因此这是一处**已记录的功能缺口**，不是静默跳过。

### 30.4 决定性实测：真 API 下只剩 12 处编译错误，且是同一类

真包的 Runtime + Editor 程序集**自身编译干净**（G1 日志里没有一条错误来自 `Packages/`），说明
TMP / Addressables / InputSystem / URP / UniTask / Newtonsoft 这些依赖在本工程里都解析正常。
我们的游戏代码对真 API 只剩：

```
CS7036 ×11  我们那些派生类构造函数是空桩 → 编译器插入隐式 base() → 真基类要参数
            AudioManagerExtended        -> AudioManager(AudioConfiguration, IResourceProviderManager,
                                                        ILocalizationManager, ICharacterManager)
            CameraManagerExtended       -> CameraManager(CameraConfiguration, IInputManager, IEngineBehaviour)
            StateManagerExtended        -> StateManager(StateConfiguration)
            BackgroundManagerExtended   -> BackgroundManager(BackgroundsConfiguration, CameraConfiguration)
            CharacterManagerExtended    -> CharacterManager(CharactersConfiguration, CameraConfiguration,
                                                            ITextManager, ITextPrinterManager)
            LayeredCharacterExtended    -> LayeredCharacter(string, CharacterMetadata,
                                                            EmbeddedAppearanceLoader<GameObject>) ×2 重载
            SpriteBackgroundExtended    -> SpriteBackground(string, BackgroundMetadata,
                                                            StandaloneAppearanceLoader<Texture2D>) ×2 重载
            LocalizationManagerExtended -> LocalizationManager(LocalizationConfiguration, IResourceProviderManager)
            MoviePlayerExtended         -> MoviePlayer(MoviesConfiguration, IResourceProviderManager,
                                                        ILocalizationManager, IInputManager, IAudioManager)
CS0115 ×1   MultipliableSpawn.DestroySpawned(string) 与真 SpawnManager 的签名不符
```

这 12 处全部按"4 月原生代码里游戏到底传了什么"逐条重建，并要求给出"4 月签名 vs 9 月签名是否一致"的判定；
真正的版本差异会被显式报告，而不是静默适配。

注意 12 这个数字本身也是证据：原先 ExtendedActors 程序集被 SpriteDicing 那处错误卡住，遮住了同程序集内
其他同类错误（修掉宏保护后错误数从 21 升到 36，去重后正是这 12 条）。**"改一处错误数变多"不总意味着变糟**。

### 30.5 顺带确认的两件事

- `Engine.cs` 里"Naninovel package was probably modified or is corrupted"只是 `Resources.Load` 失败的
  提示，**不是版本自检**，所以 4 月内容配 9 月引擎不会被引擎主动拒绝。
- 真包**不自带** `Resources/Naninovel/Configuration/*.asset`（`ProjectConfigurationProvider` 的
  `LoadOrDefault` 在缺失时创建默认实例），而我们工程 `Assets/` 下也没有 `Resources/Naninovel` →
  **无同名 Resources 路径冲突**，游戏配置可以安全导入到
  `Assets/NaninovelData/Resources/Naninovel/Configuration/<类型名>.asset`（引擎按此路径 `Resources.Load`）。
- **启动钩子核对**：游戏构建带 `manosaba_Data/RuntimeInitializeOnLoads.json`（40 个钩子）。逐条核对后，
  游戏自有钩子在本工程里都存在（例如 `GigaCreation.Tools.Service.ServiceLocator` 的静态重置、
  `IAddressablesManager.ResetStaticProperties`）；其余"未找到"全部是第三方包（在 `Library/PackageCache`
  或包源码里），属扫描范围假象，不是缺口。

### 30.6 游戏自己的 Naninovel 配置资产（下一步的关键内容）

`resources.assets` 里有 **31 个配置 MonoBehaviour**：18 个核心配置（Engine/Scripts/ScriptPlayer/Characters/
Backgrounds/Camera/Audio/Input/Localization/ManagedText/Movies/ResourceProvider/Spawn/State/TextPrinters/UI/
Unlockables/CustomVariables/ChoiceHandlers）+ 5 个游戏扩展配置（Audio/Camera/Characters/Movies 的
`*ConfigurationExtended` 与 `WitchTrialsChoiceHandlersConfiguration`）+ `EngineVersion`/`ProjectStats`/
`ProjectResources`/`CompilerLocalization`/`LoadingIcon`/`EngineInitializationUI`。

**没有它们，引擎就不知道本作的内容**（`ScriptsConfiguration.TitleScript` 就是标题剧本的入口），所以这是
"进入游戏流程"的最后一块内容拼图。现在有了真包源码，这些配置的字段布局可直接从源码读取（基类字段在前），
解码工具的产物为 `out/unity/naninovel_configs.json`，Unity 侧再按"由 Unity 自己生成资产"的方式写入。

## 31. 2026-09-17 记录：真包替换造成的**全局引用断裂**及其修复（必须记住的一步）

### 31.1 问题

桩版 Naninovel 的两个程序集（`Elringus.Naninovel.Runtime`、`Naninovel.Common`）是**我们工程里的 .cs 文件**，
Unity 首次导入它们时给了随机的 GUID；我们此前的转换器（`so_convert_plan_*` + `ScriptableObjectConverter`、
prefab 重建器）把**这些 GUID** 写进了产物的 `m_Script` 引用。装上真包后，真包带的是它**自己的原始 GUID**，
于是这些引用全部指向不存在的东西——不报编译错误，但运行期就是 Missing Script。

实测数据（`tools/unity-rebuild/remap_naninovel_guids.py` 的报告，产物 `out/unity/naninovel_guid_remap.json`）：

```
deprecated stub scripts indexed : 964
real package scripts indexed    : 865
script references scanned       : 630 asset files
remappable dangling GUIDs       : 24   (1637 references in 488 files)
  1085  Naninovel.LayeredActorLayer        -> 6e6b28b72f1c4a488e6d5f1ec1e1aa3e
   440  Naninovel.Script                  -> 6c792d215dc6a994bbd3ed803f18d0b0
    47  Naninovel.ManagedTextProvider      -> 18a93ec42ebe3044abd36de19cc3a880
    14  Naninovel.UI.FontChanger / 13 RenderCanvas / 13 LayeredCharacterBehaviour / 6 CustomUI / ...
```

### 31.2 修复方式（可复现、可复验）

映射是**可精确恢复**的：被移走的桩目录 `work/deprecated-stub-assemblies/**` 仍带着 `.cs.meta`（旧 GUID →
源文件 → 类型名），真包也带着 `.cs.meta`（类型名 → 新 GUID）。工具据此做 GUID 改名，并且：

- 只改**确实已不存在的** GUID（仍存活的 GUID 一律不碰）；
- 只扫 `.asset/.prefab/.unity/.mat/.controller/.playable` 这些会持有脚本引用的资产；
- 判断 GUID 是否存活时**必须扫 `Library/PackageCache`**——否则 TMP 默认字体、URP 着色器等第三方 GUID 会被
  误报为断裂（第一版就踩了这个坑：误报 4,740 处，修正后只剩 4 个未匹配，其中 2,075 处是 Unity 内置资源
  `0000000000000000f000000000000000`，本就没有 .meta）。

执行：`python tools/unity-rebuild/remap_naninovel_guids.py --write` → **重写 488 个资产文件**；
再跑一次报告确认 `remappable dangling GUIDs: 0`；随后 G1 实跑给出 `import_errors=0`、`missing_script_hits=0`
（488 个文件确实被 Unity 重新导入且无错）。

未匹配、明确记录的残留：`CreditsUI.prefab`（170 处）与 `TitleCutIn.prefab`（22 处）各有一个我们从未拥有
对应类型的脚本 GUID —— 属已知缺口，不是静默丢弃。

### 31.3 结论（流程约束）

**任何"用真包替换同名桩程序集"的操作，之后都必须跑一次 `remap_naninovel_guids.py`**。这不是一次性清理，
而是这类替换的固定后续步骤；工具自带 `--write` 与再报告两种模式，便于复核。

### 31.4 同一轮的其他实测

- G1 错误数：**12 → 7**（子代理按 4 月原生代码修好的 7 处已由编译器确认生效）。剩下的 7 处里 6 处是
  ExtendedActors 的同类构造函数、1 处是本轮新暴露的 `UiManagerExtended`（先前被 ExtendedActors 的编译失败
  遮蔽——**修掉错误后错误数上升**在这个工程里反复出现，属正常现象，不是回归）。
- 已确认的**真实版本差异**（4 月 vs 9 月）：`SpawnManager.DestroySpawned` 增加了 `dispose = true` 参数
  （9 月的 `@despawn` 命令用 `dispose: false` 先移除状态、再在异步销毁后自行销毁对象）。其余 5 个基类构造函数
  四月与九月完全一致；`MultipliableSpawn..ctor` 也被一并修复（当时未被编译器报出，属被动遮蔽）。

## 32. 2026-09-17 记录：补上 G6（全内容覆盖门）与它查出的内容缺口

审计对 G6 的定义是"**全内容覆盖**：未执行的用 NOT_RUN、缺输入的用 BLOCKED、不许把抽样扩成'所有内容已验证'"。
此前仓库里**没有 G6 脚本**（只有 G1–G5），本轮补上：

- `tools/unity-rebuild/check_content_coverage.py` —— 静态比对：**官方 catalog**（游戏自己的代码会请求什么）
  × 本工程的计划 × 磁盘上的 Addressables 组 × 资产文件本身；逐族给出四种状态之一：
  `BUILT`（已配进构建）/ `PLANNED_ONLY`（有资产但**没进构建**，播放器按地址加载不到）/
  `NOT_PLANNED` / `PARTIAL`（缺资产或地址对不上）。
- `tools/unity-rebuild/g6_content_coverage.ps1` —— 门包装（不需要 Unity，可随时跑），状态 PASS/PARTIAL/BLOCKED，
  退出码 0/1/2，产物 `out/unity/g6_content_coverage.json`。

### 32.1 实测结果（`G6 PARTIAL`）

| 族 | 状态 | 计划条目 | 磁盘上已配置 |
| --- | --- | --- | --- |
| Manosaba.Prefabs | BUILT | 1,952 | 1,952 |
| Manosaba.Scenarios | BUILT | 440 | 440 |
| Manosaba.Text | BUILT | 450 | 450 |
| Manosaba.Fonts | BUILT | 24 | 24 |
| Manosaba.Data | BUILT | 8 | 8 |
| **Manosaba.Audio** | **PLANNED_ONLY** | **26,537** | **无** |
| **Manosaba.Textures** | **PLANNED_ONLY** | **1,389** | **无** |
| **Manosaba.Sprites** | **PLANNED_ONLY** | **17** | **无** |
| **Manosaba.Video** | **PLANNED_ONLY** | **2** | **无** |

- 所有 `BUILT` 族的**地址与官方零不一致**、**缺资产 0**；剧本 `440/440` 已转换且 `path` 字段与地址 `440/440` 一致。
- **内容缺口是明确的一条**：音频 26,537、贴图 1,389、精灵 17、视频 2 个资产**有资产但未进 Addressables 构建**
  ——引擎能启动，但按地址加载语音/BGM/背景/立绘会失败。修复方式已定：为这四个族各跑一次
  `a13f_plan_addressables.py --group <族>` + `g3_content_build.ps1 -Group <族>`（G3 一次只应用一个族的计划）。
- `runtime_played` 保持 **NOT_RUN**（尚无任何实跑报告它加载了哪些剧本）——按审计规则，抽样或部分通关都不许
  报成全量覆盖。

### 32.2 这一轮踩到并修掉的一个比对错误（值得记下来）

G6 第一版拿"catalog 的 `keys`"当地址集合，结果**每一族都全量报不一致**。原因是官方 catalog 的
`keys` 是 **GUID**（57,212 条，没有一条以 `Assets/` 开头），资产路径在 `locations[].internal_id`（28,633 条）。
改用 internal_id 后立刻归零。教训与 §31.2 同源：**字段名不等于语义，比对基准必须用测量确认**。

## 33. 2026-09-18 记录：G1 绿灯、引擎初始化 NRE 的根因 = **配置资产缺失**

### 33.1 G1 PASS（真包 + 全部重建代码一起编译通过）

12 处 API 差异全部按 4 月原生代码修好（子代理交付 12 行对照表，含每处的 RVA/字节数与"4 月 vs 9 月是否一致"判定），
其中唯一真实漂移是 `SpawnManager.DestroySpawned` 增加 `dispose = true`。随后子代理做了**静态扫描**（两条互补路径：
按基类推导 + 按"空体且无 `: base(...)` 的构造函数"逐个用原生首调用分类，共 434 个候选），结论是**同类缺陷已无残留**；
另修了 `UiManagerExtended..ctor`（我复核了它的寄存器搬运：`push r14 + sub rsp,0x40` 使入参 4/5/6/7 落在
`[rsp+0x70..0x88]`，基类调用正好 6 个参数、第 7 个存入 `_naniScriptPlayer (+0xB0)`）。

```
G1 PASS: cs_errors=0 cs_warnings=0 import_errors=0 assembly_load_errors=0
```
（`missing_script_hits=0` 也一并复验了 §31 的 GUID 重映射效果。）

### 33.2 引擎初始化失败的精确根因（实测堆栈）

G4b 在新代码上重跑，链已经跑到我们的重建代码里（日志可见
`GameInitializer/<InitializeAsyncCore>d__10 → ServiceLocator.Register`），但引擎自身初始化失败：

```
Naninovel.RuntimeInitializer.OnApplicationLoaded
  → RuntimeInitializer/<Initialize>d__3.MoveNext (RuntimeInitializer.cs:93)
  → Engine.Initialize → Engine/<Initialize>d__54.MoveNext (Engine.cs:150)
  → Naninovel.InputManager.InitializeService()          ← NullReferenceException 抛在这里
```

原因链条完整且可验证：**31 个 Naninovel 配置资产一个都没导入** →
`ProjectConfigurationProvider.LoadOrDefault` 在资产缺失时 `ScriptableObject.CreateInstance(configType)`
返回**默认配置** → 默认 `InputConfiguration.InputActions` 为 null → 引擎 `InputManager.InitializeService()` 解引用它 → NRE
→ 引擎的初始化 TCS 永不完成 → 我们重建的 `InitializeNaninovelAsyncCore` 那个**没有超时的轮询循环**（忠于原生）就一直转。

同一条日志还暴露了两个次生现象，都已解释清楚：
1. **默认配置与官方配置的差异是有方向性的**：官方 `EngineConfiguration.InitializeOnApplicationLoad = false`，
   而默认值是 **true** —— 所以现在是引擎自己在 `OnApplicationLoaded` 里自动初始化（正是堆栈里的那一层），
   导入官方配置后会改为由我们重建的 `InitializeNaninovelAsyncCore` 显式初始化。
2. **日志暴涨到 602 MB**（45 秒窗口）：失败的任务经 `UniTaskExtensions.ForgetCore` 反复记录。
   已把该日志压成 310 KB 样本（`out/logs/game_start_nre_sample.txt`）后删除；后续默认窗口保持 8 秒，
   长窗口仅在需要区分"慢/挂"时显式开启。

**订正（子代理按源码复核后提出，我接受）**：`InputManager.InitializeService` 解引用空 `InputActions`
这一**机制推断不成立** —— 源码里唯一的 `InputActions` 解引用受 `if (!config.InputActions) return;` 保护
（`InputSampler.cs:195`），同步序言只访问 `Configuration.Bindings`，而默认实例的字段初始化器会填好它。
本节保留的是**实测事实**（堆栈里确实经过 `InputManager.InitializeService` 且抛 NRE），而不是未经复现的因果解释；
导入配置的正当性另有独立依据（`InitializeOnApplicationLoad` 由默认 true 变为官方 false）。
真正的当前阻断点见 §34。

### 33.3 配置解码已完成（子代理交付，31/31 有解释、0 失败）

`tools/unity-rebuild/decode_naninovel_configs.py` → `out/unity/naninovel_configs.json`（1.08 MB，含逐字段字节布局）
+ `.md` 摘要 + `catalog_pathid_report.md`。闭合性：**27 个精确闭合，4 个在末尾多出一个 28 字节
`ManagedReferencesRegistry` 后闭合**（四者尾部字节完全相同，且这 4 个正是含 `[SerializeReference]`
actor metadata 的配置），**没有任何一个留下未解析字段**。

决定"引擎加载什么内容"的关键值（偏移 · 类型 · 值）：

| 配置 | 字段 | 值 |
| --- | --- | --- |
| `ScriptsConfiguration` | `TitleScript` @0x0204 | **`System/System_Title`** |
| | `StartGameScript` @0x021c | `Act01_Chapter01/Act01_Chapter01_Adv01` |
| | `Loader` @0x0034 | `PathPrefix="Scripts"`，provider = `AddressableResourceProviderExtended` + `ProjectResourceProvider` |
| `ResourceProviderConfiguration` | `UseAddressables` @0x0050 | **true**（`ProjectRootPath = "Naninovel"`，`LocalRootPath = "%DATA%/Resources"`） |
| `EngineConfiguration` | `InitializeOnApplicationLoad` @0x0044 | **false**；`CustomInitializationUI` → `WitchTrials.Views.EngineInitializationUiModified` |
| `CharactersConfiguration` | `Metadata` | **41 条角色记录**（`Ema, Hiro, Meruru, Hanna, Sherry, AnAn, …`，含 13 个 `Creature*`） |
| `LocalizationConfiguration` | `Languages` | ja / zh-Hans / zh-Hant / en-US / ko；`SourceLocale = DefaultLocale = "ja"` |
| `ProjectStats` | — | `TotalScriptsCount=440`、`TotalCommandCount=112000`、`TotalWordsCount=39510` |
| `EngineVersion` | — | `"1.20"` / `"2025-04-26"`（与 §29.1 的独立解码一致） |

另外：`ResourceManager.m_Container` 给出 31 个对象里 **28 个**的 `Resources.Load` 键，其中 **25 个正好是
`naninovel/configuration/<类名小写>`**，与 `ProjectConfigurationProvider.LoadOrDefault` 的请求路径一致 ——
这独立验证了解码结果，并直接给出资产该放在哪里（`Assets/NaninovelData/Resources/Naninovel/Configuration/<类型名>.asset`）。

### 33.4 探针自身的一个缺陷（已修）

`naninovel_initialized` / `naninovel_total_commands` 等字段第一版写成 `bool?`/`int?`，而 Unity 的 **`JsonUtility`
会静默丢弃可空类型** —— 证据里因此全是 `null`，看起来像"引擎什么都没报"。现改为"值 + `..._known` 标志"的成对写法。
同时把链的观察窗口改为可通过 `MANOSABA_GAMESTART_CHAIN_SECONDS` 配置（默认仍 8 秒），
用于区分"慢"与"真挂"。

## 34. 2026-09-18 记录：配置导入生效、全内容构建完成、以及最后一道关

### 34.1 全内容构建完成（G3 PASS，9 个族全进构建）

`g3_content_build.ps1 -Group textures` 实际应用的是**全量计划**（该计划文件本身即全量：30,819 条），结果把
9 个族一次构建完：

```
G3 PASS: entries=30,819  locations=33,220  bundles=11  player-content 15 files / 3,594.8 MB（881.7 秒）
```

顺带修掉门自身的一处限制：`-Group` 原来是 `ValidateSet('core','prefabs','all')`，使新生成的按族计划根本无法使用；
现改为**按工程状态推导**（`addressables_plan_<组>.json` 存在即接受，缺失已由原有 BLOCKED 分支处理），
并保留"按族应用安全"的依据（`AddressablesSetup.cs:148-151`：只清理计划命名的组）。

### 34.2 配置导入（`NaninovelConfigBuilder.cs`，子代理交付，已实跑）

产物 `out/unity/naninovel_config_import.json`：

```
objects_total=31  configuration_objects=25  configuration_assets=24
configuration_created=22  configuration_overwritten=2  configuration_imported=24  configuration_failed=1
not_imported=6   values_total=3130  fields_applied=2249  fields_skipped=10
references_total=200  references_null=162  references_bound=1  references_needing_binding=37
```

- **24 个配置资产已落在引擎查找的路径上**：`Assets/NaninovelData/Resources/Naninovel/Configuration/<类型名>.asset`
  （含 `ScriptsConfiguration`（`TitleScript=System/System_Title`）、`EngineConfiguration`（`InitializeOnApplicationLoad=false`）、
  `CharactersConfiguration`（41 条角色记录）、`InputConfiguration` 等）。
- **6 个按证据合理跳过**，每个都有理由：`CompilerLocalization`（包自带 `Prefabs/DefaultCompiler.asset`，
  且 `ScriptsConfiguration.CompilerLocalization` 已绑定到它）、`EngineVersion`（包已占用
  `Resources/Naninovel/EngineVersion.asset` 这个 key，再写一份会让"哪份生效"变得随机）、`ProjectStats`
  （构建期由 `ProjectStatsResolver` 重新生成）、`ProjectResources`（列的是**官方**Addressables 布局的 276 条路径）、
  以及两个 prefab 组件 `LoadingIcon`/`EngineInitializationUI`（包的 prefab 里序列化值**逐字段相同**）。
- **1 个真 bug**：解码器把 `m_Name` 也写成带类型的包装（`{"kind":"string","value":…}`），导入器当成裸字符串拼进路径
  → `ArgumentException: Illegal characters in path.`（仅影响那个冗余副实例 path_id 1465）。已交回原作者修
  （要求：统一解包标量 + 路径非法字符守卫 `SKIPPED_INVALID_NAME`，且**批量工具不得因单行异常中断**）。
- **37 个待绑定引用**已逐条记录（class/name/path_id + 非绑定性的候选搜索），最大两组是
  `CharactersConfiguration.Metadata.metas[*].CustomSpriteMaterial`（31）与 `BackgroundsConfiguration`（3），
  目标是 `MaskableTransparent`/`MaskableTransparentToned_1` 材质——**全工程搜索确认不存在**，属如实缺口。

### 34.3 引擎**真的开始初始化了**（实跑证据）

配置导入后重跑 G4b，探针新字段给出直接证据：

```
naninovel_initializing = True      naninovel_initialized = False
naninovel_script_manager = True    naninovel_script_player = True
naninovel_total_commands = 0       （尚未加载剧本，与初始化未完成一致）
spawned_roots = [6] Game<Runtime> (205) | EngineInitializationUI(Clone) (21) | Naninovel<Runtime> (2) | …
log_exceptions = 1
```

`EngineInitializationUI(Clone)` 与 `Naninovel<Runtime>` 是**引擎自己创建的运行时层级** —— 说明初始化确实在跑，
且只剩一个异常。

### 34.4 最后一个阻断点（精确到方法）

那唯一的异常是：

```
NullReferenceException
  at Naninovel.ResourceLoaderExtensions.LoadOrErr<TResource> (loader, path, holder)   [ResourceLoaderExtensions.cs:79]
  ← Naninovel.ScriptLoader.LoadAndHoldScript (string scriptPath)                     [ScriptLoader.cs:183]
```

`LoadOrErr` 在资源解析不到时抛出，而按路径解析剧本的是游戏自己的
`GigaCreation.NaninovelExtender.ResourceProvider.AddressableResourceProviderExtended` ——
它的 `LoadAllLocations()`（kickoff RVA `0xD338E0`，Slot 56，状态机 MoveNext `0xD33A70`）是空桩，
返回 `default(...)`，于是**从来没有产生过任何 IResourceLocation**。这个类只有 83 行、且是游戏自有代码，
已交给子代理按 4 月原生代码重建（同时要求它明确回答"Naninovel 剧本路径如何变成 Addressables 键"，
因为官方剧本地址是 `Assets/#WitchTrials/Scenarios/<path>.nani`，而配置里的前缀是 `Scripts`）。

---

## 35. 2026-09-18 记录：启动链异常的**真正根因**（一个空构造函数）、catalog 地址方案被证实、以及 Editor 侧那堵 17 秒的墙

### 35.1 根因：`AddressablesManager` 的构造函数是空桩

§34.4 把最后一个异常定位到 `ResourceLoaderExtensions.LoadOrErr` → `AddressableResourceProviderExtended`。
本轮把栈**再往下挖了一层**（`out/logs/game_start.log`，当时的调用链）：

```
System.NullReferenceException
  at GigaCreation.Essentials.AddressablesUtils.AddressablesManager.LoadResourceLocationsAsyncCore (ct)
  at GigaCreation.NaninovelExtender.ResourceProvider.AddressableResourceProviderExtended.LoadAllLocationsCore ()
  at Naninovel.AddressableResourceProvider.LocateResources[T] (path)
  at Naninovel.ResourceProvider.ResourceExists[T] (path)                    [ResourceProvider.cs:120]
  at Naninovel.ResourceLoader<TResource>.Load (path, holder)                [ResourceLoader.cs:182]
  at Naninovel.CustomVariableManager.InitializeService ()                   [CustomVariableManager.cs:45]
  at Naninovel.Engine.Initialize (params)                                   [Engine.cs:150]
  at Naninovel.RuntimeInitializer.Initialize (...)                          [RuntimeInitializer.cs:93]
```

也就是说：**不是** provider 没被重建，而是 provider 重建对了、它调用的
`AddressablesManager.LoadResourceLocationsAsyncCore` 在跑，而这一句炸了：

```csharp
AsyncOperationHandle<IList<IResourceLocation>> handle =
    Addressables.LoadResourceLocationsAsync(masterData.AssetKeyNames, Addressables.MergeMode.Union, null);
_handles.Add(handle);          // <-- _handles 是 null
```

`_handles`（`readonly IList<AsyncOperationHandle>`，dump.cs:727594，+0x10）只在构造函数里赋值，
而 Il2CppDumper 把构造函数吐成了空体。反汇编 `0xCBFDF0`（489 B）后逐条对上：

```
0x180CBFE78  object_new TYPE List<AsyncOperationHandle>
0x180CBFE91  call 0x180857BA0              ; List<T>()
0x180CBFEA4  mov [rdi+0x10],rbx            ; this._handles          (+0x10)
0x180CBFF02  object_new + .ctor Dictionary<string, object>
0x180CBFF0E  mov [rdi+0x18],rbx            ; this._loadedAssets     (+0x18)
0x180CBFF72  object_new + .ctor Dictionary<string, UniTask>
0x180CBFF7E  mov [rdi+0x20],rbx            ; this._pendingLoadAssetTasks (+0x20)
0x180CBFFD8  ret                           ; 没有别的语句
```

本轮把同一批"已在别处被证实、却被留成桩"的成员一起补上（全部写在文件里的
`RECONSTRUCTED | Confidence: A | RVA: …` 注释里，逐条给了指令级证据）：

| 成员 | RVA | 恢复出的行为 |
|---|---|---|
| `AddressablesManager()` | `0xCBFDF0` | 三个集合字段 `new List/Dictionary`（**根因**） |
| `ResourceLocations` get/set | `0x453470` / `0x30EFA0` | 自动属性；原桩 `get => null` + 空 `set` 会把定位结果**悄悄丢掉** |
| `IsAdditionalCatalogLoaded` get/set | `0x456360` / `0x5DA0D0` | 自动属性 |
| `WebRequestManager` get | `0xCBE940` | `_webRequestManager ??= ServiceLocator.Get<IWebRequestManager>(true)` |
| `LoadAssetCts` get | `0xCBEA00` | `_loadAssetCts ??= new CancellationTokenSource()` |
| `Dispose()` | `0xCBF2B0` | `ReleaseHandles(); ResourceLocations = null;` |
| `ReleaseHandles()` | `0xCBF520` | `_loadAssetCts` cancel+dispose+置空 → `_loadedAssets.Clear()` → `foreach (var h in _handles.Where(h => h.IsValid())) h.Release();` → `_handles.Clear()` |

`ReleaseHandles` 的循环体一开始看着像"没有语句"，靠两个接口槽位号才钉死：`IAsyncOperation`
slot 2 = `get_Version`、slot 4 = `DecrementReferenceCount`（dump.cs 接口槽表），而冷块
`0x180CBFA86` 构造的正是 `new Exception("Attempting to use an invalid operation handle")` ——
这就是 `AsyncOperationHandle.Release()` 的完整内联体
（`Library/PackageCache/com.unity.addressables@16eddacf275e/Runtime/ResourceManager/AsyncOperations/AsyncOperationHandle.cs:264-268`
`InternalOp.DecrementReferenceCount(); m_InternalOp = null;`），而 `IsValid()` 是**另一个**方法
（同文件 227-230 行，比较 `Version` 后返回 false 而不是抛异常），正好对应 `Where` 的谓词委托 `<>9__26_0`。

改完后：**G1 PASS**（`cs_errors=0 cs_warnings=0 assembly_load_errors=0`），**G4b 异常数 1 → 0**。

### 35.2 catalog 地址方案：**已经是对的**（用 dump 直接量，不靠推断）

`AddressableResourceProviderExtended` 把每次 Naninovel 路径解析都交给
`Addressables.LoadResourceLocationsAsync(["Naninovel"], MergeMode.Union, null)`，所以"catalog 里
到底有没有 `Naninovel` 这个 key、它指向什么"是一个必须先量清楚的事实。本轮写了
`tools/unity-rebuild/inspect_built_catalog_key.py` 直接读 69 MB 的 catalog dump：

```
location_count = 33212      key_count = 59219      labels = 92（含 "Naninovel"）
EXACT KEY 'Naninovel': FOUND  location_count=32459  is_label=True  locate_error=''
keys starting 'Naninovel/'        : 27843
keys starting 'Naninovel/Scripts/': 440
keys starting 'Assets/'           : 0
Naninovel/Scripts/System/System_Title:
    primary_key = Naninovel/Scripts/System/System_Title
    internal_id = Assets/GameContent/#WitchTrials/Scenarios/System/System_Title.asset
    resource_type = Naninovel.Script
```

对照 `AddressableResourceProvider.CacheLocations`（`AddressableResourceProvider.cs:90-97`：
`LocationsCache.Add(location.PrimaryKey.GetAfterFirst("/"), location.ResourceType)`）与
`ResourceProvider.ResourceExists<T>`（`ResourceProvider.cs:116`：缓存非空即走缓存），
`Naninovel/Scripts/System/System_Title` 会被缓存成键 `Scripts/System/System_Title` + `typeof(Naninovel.Script)`，
正是剧本加载要查的那一项。**内容是通的，"地址方案不对"这个此前的担心不成立。**

### 35.3 探针升级：引擎初始化进度被逐服务量出来了

`Engine.Initialize` 在 await 每个服务之前都会 `OnInitializationProgress?.Invoke(.25f + .5f * i / count)`
（`Engine.cs:147-152`），这个事件本身就是**逐服务的进度尺**。探针现在订阅它并记录时间戳轨迹，
同时每秒采样 `Engine.Initializing/Initialized`、把状态流式写到**旁路文件**（`…evidence.json.partial.json`，
主文件一旦出现门就会退出 Play Mode，所以流式状态只能写旁路），并打 `Debug.Log` 心跳（带帧号）。
这一次的实测把 23 个服务的顺序和服务清单都量出来了：

```
engine_services_count = 23
engine_services = StateManagerExtended, ResourceProviderManager, LocalizationManagerExtended,
  CommunityLocalization, TextManager, CustomVariableManager, VersioningManager, InputManager,
  ScriptManager, WitchTrialsScriptPlayer, TextPrinterManager, CharacterManagerExtended,
  AudioManagerExtended, CameraManagerExtended, BackgroundManagerExtended, MultipliableSpawn,
  MoviePlayerExtended, UiManagerExtended, ScriptLoader, TextLocalizer, …
master_data = WitchTrials.Models.MasterData   keys = 'Naninovel'     ← 主数据与 key 都对
naninovel_file_io_read  = COMPLETED     naninovel_file_io_write = COMPLETED   ← Naninovel 自己的文件 IO 正常
```

（最后两项是探针新增的独立实验：直接反射调用 `Naninovel.IOUtils.ReadTextFile/WriteTextFile` 并轮询
`Naninovel.UniTask.IsCompleted`，用来把"这个调用在本环境根本完不成"和"引擎在等别的东西"分开。
结论是前者不成立。）

### 35.4 真正的墙：Editor 在 Play Mode 中段自己重编译 + 域重载

把窗口从 8 s 调到 60 s 后，G4b 反而**拿不到任何证据**了。旁路文件显示心跳停在 `t=16.6s`，而 G4b 门
一直等到 300 s。查 `game_start.log` 得到原因：

```
736: Asset Pipeline Refresh (… Total: 0.302 seconds - Initiated by RefreshV2(NoUpdateAssetOptions))
767: [MODES] ModeService[none].Initialize
768: [MODES] ModeService[none].LoadModes
769: [ScriptCompilation] Requested script compilation because: Define symbols changed
835: Starting: … ScriptCompilationBuildProgram.exe …
3314: Finished compiling graph: 1369 nodes, 28613 flattened edges (21393 ToBuild, 238 ToUse)
5134: Reloading assemblies after finishing script compilation.
5136: Begin MonoManager ReloadAssembly
5158: Domain Reload Profiling: 5868ms
```

即：**Play Mode 进行到约 17 秒时，Unity 自己请求了一次全量脚本重编译，编译完成后做域重载**，
把探针的协程和引擎的全部静态状态一起清掉。这对 8 s 窗口的旧运行恰好无害（那时探针已经写完证据），
对 60 s 窗口则是致命的——**Editor 里根本量不到初始化走完**。

排除过程（都已实测，不是猜）：
- 加"预热 Play Mode"（`MANOSABA_PLAYMODE_WAIT_FOR_COMPILE`，进入 Play Mode 后等编译结束再退出）
  **不能**消除它：预热那次 `compile requests=0`，而紧接着的正式那一次仍然是 1 次请求 → 触发条件每次
  进入 Play Mode 都会重新成立。
- 工程级 `scriptingDefineSymbols: {}` 为空，`Assets/` 与 Naninovel 包里**没有任何**代码写 define；
  两个日志在第 768 行之前逐行相同，唯一差别是预热那次门在那一刻退出了 Play Mode、正式那次留在里面。

结论：**Editor Play Mode 只提供约 17 秒的干净运行时间**。要量完整启动链必须换场地——
独立 Player 没有重编译、没有域重载，而 G5 本来就跑同一个探针
（`g5_player_smoke.ps1:115-118`）。这也解释了为什么 §34 之前的 Editor 观测总是在
"初始化中"处停住。

### 35.5 本轮同时发现的桩体清单（用于后续排序）

新增 `tools/unity-rebuild/scan_stub_bodies.py`：按"函数体只有 `{}` / `return default(...)` / `return null`
且其上方 3000 字符内没有 `RECONSTRUCTED` 横幅"判定，扫 `Assets/Scripts/Game`：

```
files with stubs : 477
stub members     : 2238     （上界：含状态机 MoveNext/SetStateMachine、自动属性访问器等）
WitchTrials.Game 543 | CustomInspector 372 | GigaCreation.Essentials.UI 249
GigaCreation.NaninovelExtender.ExtendedActors 169 | … | AddressablesUtils 47
```

其中与"能玩"直接相关、下一轮要做的两组：
`AddressablesManager.GetOrLoadAddressableAsset<T>(string)`（共享泛型体 RVA `0x9EC5D0`，7562 B）与
`GetOrLoadAddressableAsset<T>(AssetReference)`（`0x9EE360`）——它们被
`SpawnableClue` / `WitchBookItemThumbnail` / `WitchBookItemButton` / `AuthorTextBuilder` /
`CreditsDirectorAct2` 调用，即线索贴图、魔女之书缩略图与 STAFF 表的数据源。

### 35.6 换到独立 Player 之后：**游戏开始播放标题剧本了**

Editor 那堵墙绕不过去，于是把同一个探针放到 G5 的独立 Player 里跑（`MANOSABA_GAMESTART_CHAIN_SECONDS=75`）。
先修了一处**与游戏代码无关、但让 Player 根本构建不出来**的问题：`FastEnum.dll` 真实引用了
`System.Runtime.CompilerServices.Unsafe`，托管链接器解析不到就整包失败
（`IL1010 → Mono.Cecil.AssemblyResolutionException`）；把 Unity 自己 Burst 包里那份同名程序集装到
`Assets/Plugins/FastEnum/`（SHA-256 `F53A6EB5…`，PROVENANCE 已记）后 **Player 构建成功（3,785.7 MB / 38.28 s）**。

结果：

```
G5 PASS: player exit=0  assemblies=43  types=5  catalog=PARSED keys=57212 (104.08s)

status                 = STARTED          component_source = scene
start_invocation       = OK
initialize_completion  = STILL RUNNING after 75s
engine_services_count  = 23               engine_progress_last = 0.750
naninovel_script_manager = True
naninovel_playing        = True           ← 玩家在播
naninovel_played_script  = System/System_Title
naninovel_playback_spot  = " #1.0"
played_scripts           = [System/System_Title]
master_data              = WitchTrials.Models.MasterData   keys = 'Naninovel'
naninovel_file_io_read/write = COMPLETED
entry_sequence_on_root = True
```

**`IScriptPlayer.Playing == true` 且 `PlayedScript == System/System_Title`** —— 这是本目标要求的
"进入游戏流程"的第一条硬证据：重建的启动链把游戏带到了标题剧本，并且它正在播放。
（Editor 里看不到这一条，因为 17 秒的墙在此之前就把会话拆了。）

### 35.7 Player 里暴露出的三个具体缺口（都有确证，下一轮处理）

**(1) TMP Essential Resources 从未导入 → 27 个 NRE。** 全 Player 日志里占比最大的异常是

```
NullReferenceException
  TMPro.TMP_Settings.get_autoSizeTextContainer ()   [TMP_Settings.cs:240]
  ← TMPro.TMP_Text.LoadDefaultSettings ()           [TMP_Text.cs:5919]
```

`TMPro.TMP_Settings.instance` 为 null。工程里**不存在** `TMP Settings.asset`
（全 `Assets/` 搜索：0 个命中，也没有 `Assets/TextMesh Pro/`）。这正是 §35.6 之前 Editor 侧那条
`No graphic device is available to initialize the view … TMP_PackageResourceImporterWindow` 想做的事：
TMP 的 Essential Resources 需要导入。所有 TMP 文本都会受它影响。

**(2) 字体地址指向了 `.otf`，而官方指向 `TMP_FontAsset`。** Player 里三条警告：

```
Failed to load 'TsukushiMincho/Assets/TsukushiMincho' font resource.
Failed to load 'SourceHanSerifSC/Assets/SourceHanSerifSC' font resource.
Failed to load 'SourceHanSansKR/Assets/SourceHanSansKR' font resource.
```

把两边的 catalog 直接摆一起就清楚了（地址字符串**完全一致**，差的是 resource type 与 internal_id）：

| | 我们构建的 catalog | 官方 catalog（`catalog_dump_shipped.json`） |
|---|---|---|
| primary_key | `Naninovel/Fonts/SourceHanSansKR/Assets/SourceHanSansKR` | 同左 |
| resource_type | `UnityEngine.Font` | **`TMPro.TMP_FontAsset`** |
| internal_id | `Assets/GameContent/#WitchTrials/Fonts/SourceHanSansKR/SourceHanSansKR.otf` | `Assets/#WitchTrials/Fonts/SourceHanSansKR/SourceHanSansKR.asset` |

即：官方把 **TMP_FontAsset 资产**（`<Name>.asset`）作为字体资源，我们只重建了原始 `.otf/.ttf`。
`ResourceProvider.ResourceExists<TMP_FontAsset>` 在缓存里按 `(path, typeof(TMP_FontAsset))` 查，
而我们缓存里那一项的类型是 `UnityEngine.Font`，于是查不到 —— 警告就是这么来的。
官方 catalog 里这一类共 8 条（`Common/Arial`、`EndingCredits*` 3 条、`PixelMplus10`、
`SourceHanSansKR`、`SourceHanSerifSC`、`TsukushiMincho`），是下一轮明确的清单。

**(3) `IPerspectiveStageManager` 未注册。**

```
Error | A service of the given type is not registered: IPerspectiveStageManager
  ← GigaCreation.Tools.Service.ServiceLocator:Get<WitchTrials.Models.IPerspectiveStageManager> (bool)
```

调用方是 `WitchTrialsNaninovelManager.PerspectiveStageManager`（`??= ServiceLocator.Get<…>(true)`，
已按 RVA `0x399500` 重建），注册方应是某个 presenter 的 `Awake`。这条发生在标题剧本已开始播放之后，
属"流程已进入、某个 presenter 尚未就位"的时序/装配缺口，需要单独定位。

### 35.8 本轮的门状态

| 门 | 结果 |
|---|---|
| G1 编译 | **PASS**（`cs_errors=0 cs_warnings=0 import_errors=0 assembly_load_errors=0`） |
| G4b（Editor Play Mode） | **PASS**：入口来自场景、`Start` OK、**异常 1 → 0**；但 Editor 只能观测 ~17 s（§35.4） |
| G5（独立 Player） | **PASS**：Player 构建成功并运行，`playing=True`、`played_script=System/System_Title` |
| G6 内容覆盖 | 本轮未重跑（上一轮 PASS） |

**目标未完成**：引擎的 post-initialization 任务链仍停在进度 0.750（§35.4/§35.6），
以及 §35.7 的三个缺口。目标保持 active。

### 35.9 下一轮的**唯一**待答问题（已经缩到一行）

进度 0.750 对应 `Engine.Initialize` 的后置任务循环 `Engine.cs:154-159` 的第 0 项，而该循环的下一个
事件（`0.75 + 0.25 * 1 / postCount`）从未发出 → **`postInitializationTasks[0]()` 没有返回**。
按 `InitializeAtRuntime` 优先级（`StateManager` 是 `int.MinValue`，且其构造函数刻意不引用任何服务，
`StateManager.cs:42-43` 的注释明说它必须最先构造）该第 0 项就是
`StateManager.PerformPostEngineInitializationTasks`。它只有 4 句：

```csharp
await LoadSettings();                                   // SettingsState = await SettingsSlotManager.LoadOrDefault("Settings");
if (!Engine.Initializing) return;                       //   await LoadAllServicesFromStateAsync<…SettingsStateMap>(SettingsState);
await LoadGlobal();                                     // GlobalState = await GlobalSlotManager.LoadOrDefault("GlobalSave"); …
if (!Engine.Initializing) return;
if (Configuration.EnableStateRollback) InitializeRollback();
```

实测把范围夹到只剩两句：**`Saves_v1/Settings.json` 被写出来了（Editor 与 Player 都实测到，
73 B / 时间戳与时间线吻合），而 `GlobalSave.json` 从未出现** —— 所以卡在 `LoadSettings()` 内部，
即 `LoadOrDefault` 的 `Load()`（→ `DeserializeData` → `IOUtils.ReadTextFile`）或紧随其后的
`LoadAllServicesFromStateAsync<IStatefulService<SettingsStateMap>, SettingsStateMap>`。

已经把"文件 IO 本身不行"这条排除掉，并逐条读过全部 7 个
`LoadServiceState(SettingsStateMap)` 实现（`TextPrinterManager`/`AudioManager`/`CameraManager`/
`ScriptPlayer`/`UIManager` 全是 `UniTask.CompletedTask`；`VersioningManager`、`CameraManagerExtended`
是 `return default(UniTask)`；`LocalizationManager` 是 `async` 但只 `await SelectLocale(...)`，
而 `Naninovel.UniTask.IsCompleted => awaiter == null || awaiter.IsCompleted`
—— `UniTask.cs:39`，所以 `default(UniTask)` 是**已完成**的），肉眼看不出一处会永久挂起。

因此下一步用**二分插桩**收口（成本一次 G5）：在 vendored 包
`Runtime/State/StateManager.cs` 的 `PerformPostEngineInitializationTasks` 四句之间各插一条
`Engine.Log(...)`，跑一次 G5 读日志定位到具体那一句，**然后把插桩撤掉**（包保持原样，
只留这一节的记录）。拿到那一句之后再决定是重建哪个方法/补哪个资产。

### 35.10 本轮新增/改动的文件

| 文件 | 内容 |
|---|---|
| `UnityProject/Assets/Scripts/Game/GigaCreation.Essentials.AddressablesUtils/…/AddressablesManager.cs` | 补 7 处方法体（构造函数为根因），镜像到 `tools/unity-rebuild/converted/src/decompiled/…` |
| `UnityProject/Assets/Scripts/Runtime/ManosabaGameStart.cs` | 引擎进度轨迹/每秒采样/心跳、旁路证据、`IOUtils` 直测、master data 与 service 清单读取 |
| `UnityProject/Assets/Editor/PlayModeGate.cs` | 可配置 play-mode 预算、`MANOSABA_PLAYMODE_WAIT_FOR_COMPILE` |
| `UnityProject/Assets/Editor/BuildPlayer.cs` | 记录/设置 Standalone 托管剥离级别（并记录"Disabled 也不跳过链接器"这一实测） |
| `UnityProject/Assets/Plugins/FastEnum/System.Runtime.CompilerServices.Unsafe.dll` + `PROVENANCE.md` | 让 Player 能构建（链接器可解析） |
| `tools/unity-rebuild/inspect_built_catalog_key.py` | 量 `Naninovel` key / 地址方案 |
| `tools/unity-rebuild/scan_stub_bodies.py` | 桩体清单扫描 |
| `tools/unity-rebuild/g4b_game_start.ps1` | 预热会话、预算推导、旁路证据回显、新字段打印 |

---

## 36. 2026-09-18 记录：post-init 链的**逐调用归因**——两个空桩、一次 TMP 导入，引擎从 0.750 走到 0.917 并建出整套 UI

上一轮把 stunnel 夹到「`StateManager.PerformPostEngineInitializationTasks` 没返回」，但不知道卡在哪一句。
本轮给探针加了**归因模式**，一次运行就把四句 await 逐个量出来，然后顺着量出来的结果连修三处。

### 36.1 归因方法（探针新增，永久保留）

`ManosabaGameStart` 新增 `ProbePostInitializationChain`：

1. 反射读 `Naninovel.Engine` 的私有静态字段 `postInitializationTasks`，打印**每个委托的 target 类型与方法名**；
2. 反射取 `Engine.GetService<IStateManager>()`，读它**已经写入的状态属性**
   （`SettingsState` / `GlobalState`）——非 null 就说明**引擎自己**那一句 await 已经回来了；
3. 分别反射调用 `SettingsSlotManager.LoadOrDefault("Settings")` 与
   `GlobalSlotManager.LoadOrDefault("GlobalSave")`，各自 15 s 预算，轮询 `Naninovel.UniTask.IsCompleted`；
4. 遍历 `Engine.Services`，挑出实现 `IStatefulService<SettingsStateMap>` 的服务
   （闭合泛型接口从实例身上取，不手写程序集限定名），逐个调用 `LoadServiceState(SettingsState)`
   并各给 4 s 预算，把「谁返回、谁抛异常、谁挂住」逐个列出来。

第一次实跑的读数：

```
post_tasks = 3 [StateManagerExtended.PerformPostEngineInitializationTasks,
                CustomVariableManager.InitializeLocalVariables,
                UiManagerExtended.InstantiateUIs]
engine_settings_state = SettingsStateMap     engine_global_state = null
probe: settings slot COMPLETED               probe: global slot COMPLETED
LoadServiceState(SettingsStateMap): LocalizationManagerExtended=COMPLETED, VersioningManager=COMPLETED,
  WitchTrialsScriptPlayer=COMPLETED, TextPrinterManager=COMPLETED,
  AudioManagerExtended=THREW, CameraManagerExtended=COMPLETED, UiManagerExtended=COMPLETED,
  WitchTrialsChoiceHandlerManager=COMPLETED
```

**一句话定位：`AudioManagerExtended.LoadServiceState` 抛异常**（不是挂住）。而 `Engine.Initialize` 的
后置任务一旦 fault，`initializeTCS` 就永远不会 `TrySetResult`，`RuntimeInitializer` 又只用
`task.IsCompleted` 轮询它（`RuntimeInitializer.cs:93`），**异常因此被彻底吞掉**——这就是「引擎永远显示
`Initializing = true`、而游戏照样在跑」的全部原因。不是一个 hang，是一个被吞掉的 fault。

### 36.2 三处修复（都按原生字节重建，不是"猜一个能跑的实现"）

**(1) `AudioManagerExtended.InitializeService`（RVA `0xCF4640`，832 B）**

```csharp
_defaultSfxPaths = Configuration.ResponseSfxData.ToDictionary(x => x.ResponseKind, x => x.Path);
return base.InitializeService();      // 尾部 jmp 0x1804789C0 = Naninovel.AudioManager$$InitializeService
```

`AudioManager.InitializeService` 正是创建 `audioLoader`/`voiceLoader`/mixer 的地方；override 不调 base，
`voiceLoader` 就是 null，而 `AudioManager.LoadServiceState` 里 `VoiceLocale = …` 的 setter 是
`voiceLoader.OverrideLocale = value`（`AudioManager.cs:64`）→ NRE。

连带两处**必须一起补**的 getter（否则 `ToDictionary` 会以另一种方式炸）：

| 成员 | 桩体 | 恢复 | 依据 |
|---|---|---|---|
| `ResponseSfx.Path` | `return null;` | `return _path;` | `_path`（+0x20）是该类唯一的 string 成员 |
| `ResponseSfxBase.ResponseKind` | `return default(ResponseKind);` | `return _responseKind;` | `_responseKind`（+0x18）是唯一的 `ResponseKind` 成员 |

`ResponseKind` 那个尤其要紧：5 条 `AudioConfigurationExtended.ResponseSfxData` 会全部报 kind 0，
`ToDictionary` 直接抛 `ArgumentException: An item with the same key has already been added`。

**(2) `StateManagerExtended.InitializeService`（RVA `0xCFF290`，432 B）**

三次赋值、432 字节里**没有任何 call 指令**，而前两次与
`StateManager.InitializeService`（`StateManager.cs:64-66`）逐条同构 —— 即 base 被内联：

```
[rbx+0xA8] = Engine.GetServiceOrErr<IScriptPlayer>()   // StateManager.scriptPlayer      (dump.cs +0xA8)
[rbx+0xB0] = Engine.GetServiceOrErr<ICameraManager>()  // StateManager.cameraManager     (dump.cs +0xB0)
[rbx+0xB8] = Engine.GetServiceOrErr<IScriptPlayer>()   // StateManagerExtended._naniScriptPlayer
xor eax,eax / ret                                      // = default(UniTask) = CompletedTask
```

即 `base.InitializeService(); _naniScriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>(); return UniTask.CompletedTask;`
base 的那两个字段都是 `private`，派生类**只能**通过调用 base 来填；不填则
`PerformPostEngineInitializationTasks` 最后一句 `InitializeRollback()`（`StateManager.cs:306-307`）
在 `scriptPlayer.AddPreExecutionTask(...)` 处 NRE。

**(3) TMP Essential Resources（内容缺口，不是代码）**

全 `Assets/` 无 `TMP Settings.asset`、也无 `Assets/TextMesh Pro/`。`TMPro` 通过
`Resources.Load<TMP_Settings>("TMP Settings")` 取全局设置，取不到就 null，于是每个
`TMP_Text.LoadDefaultSettings()` 都抛：
`TMPro.TMP_Settings.get_autoSizeTextContainer () [TMP_Settings.cs:240]`。
新增 `UnityProject/Assets/Editor/TmpEssentialsSetup.cs`（`Probe()` 纯报告 + `Run()` 执行导入并**等待**），
由 `common.ps1:Invoke-TmpEssentials` 在 G5 里作为**独立的一次 Editor 调用**执行（**不加 `-quit`**）——
实测 `AssetDatabase.ImportPackage` 是异步的：带 `-quit` 的那次 `status=FAILED action=imported … still does not exist`，
Editor 先退出了。产物已落地并核对：
`TMP Settings.asset` 1852 B + 5 个 `.asset` / 14 个 `.shader` / 4 个 `.shadergraph` / 1 个 `.ttf` / 44 个 `.meta`。

> 注意 `out/unity/tmp_essentials.json` 里那份 `status=FAILED` 是**第一次（带 `-quit`）尝试**的产物，
> 之后工具改成"自己轮询、自己退出"就没再重跑它。当前权威读数在 `bootstrap_result.json`：
> `tmp_essentials_status = "OK"`、`tmp_essentials_action = "present"`（`Invoke-TmpEssentials` 现在先看资产是否存在，
> 存在即跳过）。另记一处已知代价：这次不带 `-quit` 的导入在 `DisplayProgressbar: Generating Metadata`
> 处挂了近 50 分钟（TMP 那 14 个 shader 要生成元数据），最后是人工 kill 的——资产本身已完整落盘，
> 但**导入方式仍不够确定**，下一轮应改成自己解 `.unitypackage`（gzip tar：`<guid>/asset|pathname|asset.meta`）
> 再 `AssetDatabase.Refresh()`，这样既同步又不依赖 Editor 的事件循环。

### 36.3 实测推进（G5，独立 Player）

```
              修复前            修复(1)后         修复(1)(2)后        +TMP(3)后
progress_last 0.750             0.750             0.9167             0.9167
GlobalState   null              GlobalStateMap    GlobalStateMap     GlobalStateMap
Naninovel     6 comps           9 comps           5430 comps         5430 comps
log_exceptions 28               28                330                47
```

- `0.9167 = 0.75 + 0.25 × 2/3` → 后置任务的第 0、1 项**都返回了**，卡点已推进到第 2 项
  `UiManagerExtended.InstantiateUIs`。
- `Naninovel<Runtime> (5430 components)`：**引擎把整套 UI 层级建出来了**（标题画面、ADV、魔女之书都在里面）。
- 异常先涨到 330（每个 TMP_Text 一个）再降到 47（TMP 设置到位后）。

### 36.4 剩下的 47 个异常已分类，卡住 init 的那一条已钉死

| 次数 | 位置 | 性质 |
|---|---|---|
| 38 | `WitchTrials.Views.LocalizeImage.OnEnable` → `UniRx.ObservableExtensions.Subscribe` | 待查 |
| 4 | `Naninovel.CGGalleryGridSlot.Awake` → `ObjectUtils.AssertRequiredObjects` | 缺序列化引用 |
| 1 | `SaveLoadUi.FindFocusObject` → `Dictionary.get_Item` **KeyNotFoundException** | **当前卡住 init 的一条** |
| 1 | `ToggleCanvasGroupSwitcher.Awake` / `SkipDebateButton.Awake` / `ExecutionButtonScreen.Awake` | 待查 |
| 1 | `Naninovel.UI.CustomUI.OnEnable` | 待查 |
| 1 | `ServiceLocator.Get<IPerspectiveStageManager>`（`TrialStateManager.InitializeService`） | §35.7(3) |

最后一条之所以能让 init 停下：`UIManager.AddUI` → `uiComponent.Initialize()` → `ScriptableUIBehaviour`
→ `CustomUI.FocusObject` → `SaveLoadUi.FindFocusObject` → `_panels[_presentationMode]`。
`SaveLoadUi.Awake`（RVA `0x35B0F0` 已重建）是
`_panels = _panelReferences.ToDictionary(r => r.Mode, r => r.Panel)`，而
**全工程搜索 `_panelReferences`：0 个命中** —— 重建出来的
`Assets/GameContent/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/SaveLoadUI.prefab`
根本没有这个序列化字段（+0xE8），于是 `_panels` 是空字典。
**这是 prefab 序列化字段的还原缺口，不是代码缺口**，下一轮照官方 prefab 把它补回来即可。

### 36.5 本轮修掉的一处自身缺陷

探针原来的 `ProbeFileIo` 把测试负载**写进了引擎自己的存档槽**
（`Saves_v1/Settings.json` 被写成 14 字节的 `{"probe":true}`，替换掉引擎的 73 字节）——
一个会破坏被测对象的测量。现在读引擎槽、写自己的 scratch 文件（`manosaba_probe_io.json`）。

### 36.6 本轮门状态

| 门 | 结果 |
|---|---|
| G1 编译 | **PASS**（`cs_errors=0 cs_warnings=0 import_errors=0 assembly_load_errors=0`） |
| G5 独立 Player | **PASS**：`playing=True`、`played_script=System/System_Title`、UI 层级 5430 组件 |
| G4b Editor Play Mode | 本轮未重跑（上一轮 PASS） |
| G6 内容覆盖 | 本轮未重跑 |

**目标仍未完成**：`Engine.Initialized` 尚未为 true（卡在 §36.4 那一条 + 后续若干 UI 装配缺口）。
目标保持 active。下一轮顺序：
(1) 补 `SaveLoadUI.prefab` 的 `_panelReferences`（唯一卡住 init 的）；
(2) `LocalizeImage.OnEnable` 的 38 次 subscribe 异常；
(3) `_panelReferences` 修好后重跑 G5，看 init 是否走到 `Initialized=true`；
(4) 回头做 §35.7 的 TMP_FontAsset 地址（8 条）与 `IPerspectiveStageManager`。

---

## 37. 2026-09-18 记录：卡住 init 的那条**不是代码缺口，是过期产物**——156 个 prefab 里 146 个需要重建

### 37.1 先把"事实"和"推测"分开

§36.4 的假设是「`SaveLoadUI.prefab` 少了 `_panelReferences` 这个序列化字段 ⇒ 重建缺口」。本轮先写了一个
只读探针 `UnityProject/Assets/Editor/PrefabFieldProbe.cs`，从**两端**同时量：

```
[PrefabFieldProbe] OK
 | SaveLoadUi: resolved=True declFields=22 _panelReferences=SaveLoadPanelReference[] [serialized]
 | SaveLoadPanelReference: resolved=True declFields=2
 | SaveLoadUi@SaveLoadUI: props=31 hasPanelRefs=True          ← 资产里的组件确实暴露这 31 个属性
 | SaveLoadPanel@SavePanel: props=4   | SaveLoadPanel@LoadPanel: props=4
```

**"资产里没有字段"和"类型里没有字段"被分开了**：字段在类型里、也在组件的 `SerializedObject` 里，
只是 YAML 里没写出来（原文 1092-1103 行确认，`m_EditorClassIdentifier:` 之后直接就是下一个对象）。

### 37.2 给 builder 加逐节点归因，一次跑出真因

`PrefabBuilder` 新增按节点记录（`MANOSABA_PREFAB_FIELD_DUMP=1` 才开，全量构建不受影响）：
每个节点的 pid、解析出的类型全名、归属 GameObject、以及**这一步走的是哪个分支**
（`TYPE_NOT_FOUND` / `NO_OWNER` / `ADD_REFUSED` / `SKIPPED_NO_COMPONENT` / `APPLIED applied=… skipped=… warnings=…`）。
配合已有的 `MANOSABA_PREFAB_PLAN` / `MANOSABA_PREFAB_OUT` 两个环境变量，可以**只重跑一个 job**、
写到一个独立结果文件（不覆盖权威产物）。

单 job 复现（把 `prefab_plan_all.json` 里 SaveLoadUI 那个 job 抽成 `prefab_plan_probe.json`）：

```
3842118765126769142 MonoBehaviour -> WitchTrials.Views.SaveLoadUi owner=SaveLoadUI
    APPLIED applied=253 skipped=0 warnings=0 managedApplied=0 managedFailed=0
component_add_failed = 3
```

**`applied=253`、`skipped=0`、`warnings=0`** —— applier 把 253 个属性全写进去了，一个都没漏、一个都没报警。
（顺带把"6 个组件被拒"也查清了：是 3 个**重复组件**——`SaveLoadSlot` 上已有 `Button`、
`LoadGrid` 上已有 `GridLayoutGroup`、`Title` 上已有 `Image`——`AddComponent` 拒绝重复实例，属正常。）

### 37.3 结论：重建出来的 prefab 是**过期产物**

同一次单 job 运行**重写**了 `SaveLoadUI.prefab`，之后字段就在了：

```
_panelReferences:
- _mode: 2
  _panel: {fileID: 7215552269076801640}
- _mode: 1
  _panel: {fileID: 774908538950649378}
_fadeOutDurationOnLoad: 1.6
_fadeInDurationOnLoad: 0.8
（整个组件 31 个字段齐全；文件 66,965 B -> 73,909 B）
```

于是**用权威计划 `prefab_plan_all.json` 重建全部 156 个**：

```
[PrefabBuilder] OK_WITH_PARTIAL_ASSETS: built=36 partial=120 failed=0 incomplete=0 of 156;
                objects created=21525 written=21783; pointers 3548 bound / 550 unbound（87.7 秒）
```

对重建前后的 156 个文件逐个比对（快照 `work/probe_out/prefabs_before.txt`）：

```
prefabs total=156  changed=146 (grown=88 shrunk=58)  new=0  missing=0
```

**146/156 变了。** 也就是说 §36.4 那条 KeyNotFoundException 的成因不是"某个字段没被还原"，
而是**`Assets/GameContent` 下整套 prefab 相对当前的 builder / 脚本状态已经过期**——
这也顺带解释了为什么 38 条 `LocalizeImage.OnEnable`、`CGGalleryGridSlot.Awake` 缺引用等
一串"UI 装配缺口"会同时出现：它们是同一批产物。

### 37.4 本轮的方法论收获（写进纪律）

1. **"产物里没有 X" 不等于 "管线不会写 X"**。先分别量「类型侧」「内存侧」「磁盘侧」，再下结论；
   本次就是靠 `SerializedObject` 的 `props=31 hasProp=True` + 磁盘 YAML 为空这一对读数把范围劈开的。
2. **归因信息要按节点落盘**，而不是只留聚合计数：`component_add_failed=6` 这种聚合值会让
   "组件没建出来"和"组件建了但字段没写"看起来一样，而它们的修法完全不同。
3. **重跑单个 job 的能力**（`MANOSABA_PREFAB_PLAN` + `MANOSABA_PREFAB_OUT`）把一次排查从"重跑 156 个
   再猜"变成"87 秒重跑一个、看一行"。

### 37.5 本轮门状态与下一步

| 门 | 结果 |
|---|---|
| G1 编译 | **PASS** |
| Prefab 重建 | `built=36 partial=120 failed=0 of 156`（87.7 s） |
| 产物差异 | **146/156 变更** |
| G3 内容构建 | 本轮已重跑（prefab 变了，Player 读的是 bundle，必须重建） |
| G5 独立 Player | 待重跑 |

**目标仍未完成**，保持 active。下一步：拿重建后的内容重跑 G5，看
(1) `Engine.Initialized` 是否终于为 true（j=2 `InstantiateUIs` 是否走完）、
(2) 47 个异常降到多少、(3) `SaveLoadUi` 那条 KeyNotFoundException 是否消失。
若 G5 好转，再补跑 G4b 与 G6 把两个门重新钉住。

---

## 38. 2026-09-18 记录：一个 `[RequireComponent]` 顺序 bug → 引擎最后一项后置任务卡死；以及 156 个 prefab 的过期问题被量化

### 38.1 先修工具：`PrefabBuilder` 的逐节点归因（`MANOSABA_PREFAB_FIELD_DUMP=1`）

`component_add_failed = 6` 这种聚合值无法区分"组件没建出来"和"组件建了但字段没写"，而两者修法完全不同。
现在每个非 transform 节点都会记一行：`pid`、解析出的类型全名、归属 GameObject、走的哪个分支
（`TYPE_NOT_FOUND` / `NO_OWNER` / `ADD_REFUSED` / `ADD_DEFERRED_OK` / `SKIPPED_NO_COMPONENT` / `APPLIED applied=… skipped=… warnings=…`）。
默认关闭，全量构建不受影响；配合已有的 `MANOSABA_PREFAB_PLAN`（选计划）与 `MANOSABA_PREFAB_OUT`（另写结果）
可以**只重跑一个 job**（87 秒级），不覆盖权威产物。

顺带把 `component_add_failed = 6` 的真相查清：其中 3 个是**重复组件**（`SaveLoadSlot` 上已有 `Button`、
`LoadGrid` 上已有 `GridLayoutGroup`、`Title` 上已有 `Image`）——`AddComponent` 拒绝重复实例，属正常。

### 38.2 真因：`[RequireComponent]` + 计划里的节点顺序

单 job 复现 `DebateUI.prefab`：

```
8395107227666036278 MonoBehaviour -> LeTai.TrueShadow.TrueShadow owner=(none)
      ADD_REFUSED host=Icon existing=Transform,CanvasRenderer
-576603846116362698._iconShadow: unresolved PPtr (m_PathID=8395107227666036278)
```

`UnityProject/Packages/com.le-tai.true-shadow/LeTai.TrueShadow/TrueShadow.cs:17`：

```csharp
[RequireComponent(typeof(Graphic))]
[ExecuteAlways]
public class TrueShadow : UIBehaviour, IMeshModifier, ICanvasElement
```

`Graphic` 是**抽象类**，Unity 无法替我们补上它，于是 `AddComponent(TrueShadow)` 直接拒绝并打日志
（构建日志里那句话就是它：`Can't add script behaviour 'Graphic'. The script class can't be abstract!`）。
计划把 `TrueShadow` 排在同一个 GameObject 的 `Image` **之前**，第一次尝试时那个 GameObject 上还没有
任何具体 Graphic → 节点拿不到组件 → 指向它的 PPtr 永远不绑 →
`SkipDebateButton._iconShadow == null` → `SkipDebateButton.Awake` NRE →
`DebateUi.Initialize()` 抛异常 → `InstantiateUIs` fault → `Engine.Initialize` fault →
`initializeTCS` 永不置位 → **`Engine.Initializing` 永远为 true**。一个排序 bug 直接决定"引擎能不能算初始化完"。

**修法**：把被拒的组件节点**延后到第二遍**再试（此时同一 GameObject 的其他组件已经就位），
只有第二遍仍被拒才算真失败。这样结果与计划里的节点顺序无关，且对任何"依赖同物体上另一个组件"的
`[RequireComponent]` 都成立。实测 `DebateUI`：

```
修复前: status=BUILT_PARTIAL  component_add_failed=1  pointers_unbound=1
修复后: status=BUILT          component_add_failed=0  pointers_unbound=0  bound=8
        （field_dump: ADD_DEFERRED_OK host=Icon）
```

### 38.3 过期产物被量化：156 个 prefab 里 146 个变了

`SaveLoadUi` 那条 `KeyNotFoundException` 不是"字段没还原"，而是 **`Assets/GameContent` 下的 prefab 相对当前
builder/脚本状态已经过期**。用修复后的 builder 重建全部 156 个（87.7 s）：

```
[PrefabBuilder] OK_WITH_PARTIAL_ASSETS: built=36 partial=120 failed=0 of 156;
                objects created=21525 written=21783; pointers 3548 bound / 550 unbound
```

重建前后逐文件比对：**changed=146（grown=88 shrunk=58），new=0 missing=0**。重建后
`_panelReferences` 等 31 个字段齐全（文件 66,965 → 73,909 B）。

### 38.4 归因到"具体哪个 UI 的 `Initialize()` 不返回"

`InstantiateUIs` 是**挂住**而不是 fault（fault 会让 `RuntimeInitializer.Initialize` 的 UniTask 变成
已完成，探针对启动链的 await 就会结束）。它的四句 await 里前两句显然返回了（UI 层级已建出，5613 个组件），
所以照搬 §36 那套"逐个调用 + 各自预算"的做法，给每个已注册 UI 计时
（`MANOSABA_GAMESTART_PROBE_UI_INIT=1` 才开，因为重复调用会重复订阅）：

```
修复前: … DebateUI=THREW(TargetInvocationException) …
修复后: AutoToggle=COMPLETED, ClickThroughPanel=COMPLETED, ContinueInputUI=COMPLETED, ControlPanel=COMPLETED,
        DebateUI=COMPLETED, ExecutionButton=COMPLETED, LoadingUI=COMPLETED, CreditsUI=COMPLETED, LogUI=COMPLETED,
        OptionsUI=COMPLETED, RollbackUI=COMPLETED, SaveLoadUI=COMPLETED, SoundTestUI=COMPLETED, TitleUI=COMPLETED,
        ToastUI=COMPLETED, WitchBookButtonUI=COMPLETED, MovieUI=COMPLETED, WitchBookUI=COMPLETED
```

**18 个全部 COMPLETED。** 但 catalog 里 `Naninovel/UI/` 下有 **19** 个 GameObject 键：

```
AutoToggle, ClickThroughPanel, ContinueInputUI, ControlPanel, CreditsUI, DebateUI, ExecutionButton,
**GalleryUI**, LoadingUI, LogUI, MovieUI, OptionsUI, RollbackUI, SaveLoadUI, SoundTestUI, TitleUI,
ToastUI, WitchBookButtonUI, WitchBookUI
```

**注册进 `managedUIs` 的只有 18 个，缺的正是 `GalleryUI`** —— 而全 Player 日志里反复出现的
4 条 `Naninovel.CGGalleryGridSlot.Awake → ObjectUtils.AssertRequiredObjects`（"必需对象未赋值"）
正好就在这个 UI 里。所以 `InstantiateUIs` 的第三句 await（`WhenAll(managedUIs.Select(u => u.UIComponent.Initialize()))`）
已经满足，**卡点收敛到 `GalleryUI` 自身的实例化/初始化**，这是下一轮的唯一入口。

### 38.5 同时确认的两处内容缺口（有源级证据，尚未修）

1. **TrueShadow 的 shader 缺失**（本轮异常数 47 → 281 的绝大部分，每多一个 shadow 多一条）：
   `BlendModeExtensions.GetMaterial` 是 `new Material(Shader.Find("UI/TrueShadow-Normal"))`
   （`BlendModeExtensions.cs:24/29/34/39`），而 `Packages/com.le-tai.true-shadow` 里**一个 `.shader` 都没有**
   —— 它是反编译源码占位包（`package.json` 自称 `UNLICENSED`、"replace with the licensed package"）。
   `Shader.Find` 对未打进包里的 shader 返回 null → `new Material(null)` → `ArgumentNullException`。
   修法方向：拿到真包（含 shaders），并把 4 个 `UI/TrueShadow-*` 加进 `GraphicsSettings` 的
   `m_AlwaysIncludedShaders`（工程现在只有 7 条内置项）。
2. **字体资源的类型不对**：`UIConfiguration.asset` 里 `FontResource: TsukushiMincho/Assets/TsukushiMincho`，
   地址与官方 catalog 完全一致，但官方那 8 条是 `TMPro.TMP_FontAsset`（`internal_id` 为
   `Assets/#WitchTrials/Fonts/<Name>/<Name>.asset`），我们建出来的是 `UnityEngine.Font`（指向
   `<Name>.otf`）。`ResourceExists<TMP_FontAsset>` 因此查不到，运行期只打 warning 并拿到 null。

### 38.6 本轮门状态

| 门 | 结果 |
|---|---|
| G1 编译 | **PASS**（`cs_errors=0 cs_warnings=0 import_errors=0 assembly_load_errors=0`） |
| Prefab 重建 | `built=38 partial=118 failed=0 of 156`（92.6 s，含 deferred 修复） |
| G3 内容构建 | **PASS**：`entries=30819 locations=33220 bundles=11 (3590 MB) catalogs=1`（307.1 s） |
| G5 独立 Player | **PASS**：`playing=True`、`played_script=System/System_Title`、UI 层级 5613 组件 |
| G4b / G6 | 本轮未重跑 |

**目标仍未完成**（`Engine.Initialized` 尚未为 true），保持 active。下一轮：
(1) `GalleryUI` —— 查它为什么没进 `managedUIs`（`CGGalleryGridSlot.Awake` 的 `AssertRequiredObjects`
说明其 prefab 里有未赋值的必需引用，很可能就是我们要补的序列化引用）；
(2) TrueShadow shader（含 always-included）；
(3) 字体改指 `TMP_FontAsset`；
修完这三项再跑 G5，然后补 G4b 与 G6。

---

## 39. 2026-09-18 记录：主根选错（`GalleryUI` 只剩 17/117 对象）修好；并把最后一个挂起点收敛到 **被丢弃的第二根**

### 39.1 主根选错：`FirstOrDefault()` 拿的不是文件同名的那个

`GalleryUI.prefab` 的 job 显示进度对不上：`objects_planned=119  objects_created=117  objects_written=17`，
`root_name='GalleryStillSlot'` 而 `extra_root_names=['GalleryUI']`，`objects_in_extra_roots=103`。
即：builder 用 `rootNodes.FirstOrDefault()` 取了**计划里排在前面的**那个根，而 Unity 的 prefab 主资产应当是
**与文件名同名的对象**。于是 `SaveAsPrefabAsset(GalleryStillSlot)` 只写了那个 4-transform 的小子树。

修法（与文件名对齐，取不到再回落到第一个）：

```csharp
string assetName = Path.GetFileNameWithoutExtension(string.IsNullOrEmpty(job.target) ? job.asset_path : job.target);
Node rootNode = rootNodes.FirstOrDefault(n => n.gameObject != null && n.gameObject.name == assetName)
                ?? rootNodes.FirstOrDefault();
```

四个 prefab 的单批实测（`GalleryUI` / `SaveLoadUI` / `WitchBookUI` / `CreditsUI`）：

```
GalleryUI.prefab    root=GalleryUI    written=103  (was 17)   saved_components=81  (was 13)   unbound=5
SaveLoadUI.prefab   root=SaveLoadUI   written=106               同前（本来就对）
WitchBookUI.prefab  root=WitchBookUI  written=2000              另有 30 个 "component type not found"
CreditsUI.prefab    root=CreditsUI    written=3175
```

### 39.2 19 个 UI 全部 `Initialize()` 成功，但引擎仍停在 0.9167

重建 + 重建内容后再跑 G5（每个 UI 单独计时）：

```
UI Initialize(): AutoToggle, ClickThroughPanel, ContinueInputUI, ControlPanel, DebateUI, ExecutionButton,
  CreditsUI, **GalleryUI**, LoadingUI, LogUI, OptionsUI, RollbackUI, SaveLoadUI, SoundTestUI, TitleUI,
  ToastUI, WitchBookButtonUI, WitchBookUI, MovieUI —— **19/19 COMPLETED**
```

`Naninovel/UI/` 下的 19 个 GameObject 键全部注册成 managed UI。可是
`Engine.progress_last` 仍为 `0.9167`（= 后置任务第 2 项已发出）、`Initializing` 仍为 true。

### 39.3 把"挂住"与"抛异常"再分一次，并逐个排除

- 资源层：探针新增 provider 状态读取（`LocationsCache` / `LocatedFolders` / `LocateRunners` / `LoadRunners` / `LoadedResources`）：

```
AddressableResourceProviderExtended{cache=32459, locatedFolders=0, locateRunners=[], loadRunners=[], loaded=21}
ProjectResourceProvider{cache=45, ... locateRunners=[], loadRunners=[]}
LocalResourceProvider{cache=0, ...}
```

**没有任何卡住的 runner**，缓存也齐。`ResourceProvider.LocateResources<T>` 那条"等一个永不完成的 runner"的路被排除。

- 重新调用卡住的委托本身（`MANOSABA_GAMESTART_REINVOKE_POSTINIT=1`）：

```
post-init re-invoke: InstantiateUIs COMPLETED on a fresh call
```

**同一份代码第二次调用就完成了**（异常数 281 → 562，正是"又建了一整套 UI"的代价）。所以不是结构性死锁，
而是**第一次调用**卡在一个"第二次已被满足"的 await 上。`InstantiateUIs` 的四个 await 里：
`uiLoader.LoadAll` 显然返回了（19 个 UI 都注册了）、`WhenAll(InstantiatePrefab)` 也返回了（同上）、
`ApplyFontAssociatedWithLocale` 是**全同步**的（`UIManager.cs:374-395`，每条分支都 `return UniTask.CompletedTask`）
—— 于是只剩 `WhenAll(managedUIs.Select(u => u.UIComponent.Initialize()))`。

- 顺手排除一个假设：把 `EngineConfiguration.AsyncInstantiation` 从 1 改 0 跑一次，结果**完全一样**
  （进度、异常数都不变），随后已把配置**还原为原始值 `AsyncInstantiation: 1`**（未留下任何偏离）。

### 39.4 真凶：`ScriptableGrid` 的 `slotPrototype` 指向**被丢弃的第二根**

`CGGalleryPanel.Initialize`（`CGGalleryPanel.cs:32`）是：

```csharp
await UniTask.WhenAll(CGSources.Select(InitializeLoader));   // 每个 InitializeLoader 里 await loader.Locate()
CGCount = slotData.Count;
await Grid.Initialize(viewerPanel, slotData);                // ← 这里
```

`CGGalleryGrid.Initialize` → `base.Initialize(itemsCount)` → `ScriptableGrid<T>.Initialize`（`ScriptableGrid.cs:60`）
→ `Slots = await CreateSlots()` → 每个槽 `CreateSlot` 里
**`InitializeSlot(slots[idx] = await Engine.Instantiate<TSlot>(SlotPrototype))`（`ScriptableGrid.cs:178`）**。

而**同一份 `ScriptableGrid.cs:104` 的 `Awake` 里就有 `this.AssertRequiredObjects(SlotPrototype);`** ——
Player 日志里正好有 4 条 `Nanovile.ScriptableGrid<TSlot>.Awake → ObjectUtils.AssertRequiredObjects`：

**`slotPrototype` 在重建出来的 prefab 里是 null。** 于是 `Engine.Instantiate<TSlot>(null)` 永远不返回
（`AsyncInstantiation: 1` 时是 `Object.InstantiateAsync(null)` 的 handle 永不 `isDone`，`while` 里那
`await AsyncUtils.WaitEndOfFrame` 就这么一直等下去；这也解释了为什么第二次调用反而"完成"：
那一次槽数据为空、`CreateSlots` 无事可做）。

**为什么是 null**：`GalleryUI.prefab` 在官方数据里有两个根 —— 主根 `GalleryUI` 与第二根 `GalleryStillSlot`
（17 个对象）。Unity 的 `.prefab` 只能有一个根，builder 于是**丢弃第二根**，凡是指进那棵树的 PPtr 全部解不开。
`slotPrototype`（槽模板）与 `lockedTexture`/`loadingTexture`/`_layoutGroups[0]` 都在那棵树里。
而 `slotPrototype` 那条告警此前**看不见**，是因为 builder 每个组件只保留 `Take(3)` 条消息 ——
已被测出来后改成 `Take(12)`（这条工具缺陷本身就是"为什么查了这么久"的原因之一，记在此处）。

### 39.5 结论与下一步（唯一入口，已非常窄）

**"多根 prefab 的第二根被丢弃"这一条 `documented deviation` 现在成了引擎初始化的唯一阻塞点。**
下一步是把这条偏离真正修掉，而不是继续忍受它：

1. 在 `PrefabBuilder` 里，prefab 有多根时**把每个额外根各存成一个独立 prefab 资产**
   （路径由主资产名派生，例如 `<Main>_<ExtraRoot>.prefab`）；
2. 让指针解析器把"指向额外根及其后代"的 PPtr 改绑到那个新资产上（相当于 Unity 的嵌套 prefab 引用），
   这样 `slotPrototype`、`lockedTexture` 等就不再是未绑定；
3. 重跑单个 `GalleryUI` job 验证 `pointers_unbound` 降到 0、`AssertRequiredObjects` 消失，
   然后 `超集重建 → G3 → G5`，看 `Engine.Initialized` 是否终于为 true。

### 39.6 本轮门状态

| 门 | 结果 |
|---|---|
| G1 编译 | **PASS**（`cs_errors=0 cs_warnings=0 import_errors=0 assembly_load_errors=0`） |
| Prefab 重建 | `built=38 partial=118 failed=0 of 156`（94.4 s，含主根修复） |
| G3 内容构建 | **PASS**：`entries=30819 locations=33220 bundles=11 (3590 MB)`（308.5 s） |
| G5 独立 Player | **PASS**：`playing=True`、`played_script=System/System_Title`、5682 个 UI 组件、19/19 UI `Initialize()` 成功 |
| G4b / G6 | 本轮未重跑 |

**目标仍未完成**（`Engine.Initialized` 尚未为 true），保持 active。

---

## 40. 2026-09-18 记录：把"第二根"从 documented deviation 改成**真的重建**——`slotPrototype` 绑上了，画廊槽位真的被实例化

### 40.1 做法：额外根各写成一个同级 prefab 资产，并回绑指针

`GalleryUI.prefab` 的 `CGGalleryGrid`/`StillGrid` 里 `slotPrototype = {"m_FileID":0,"m_PathID":-4915742209394539249}`，
而 `-4915742209394539249` **正落在第二根 `GalleryStillSlot` 的 17 个对象里**。此前第二根被丢弃 → 指针解不开 →
`slotPrototype` 为 null → `ScriptableGrid.Awake` 的 `AssertRequiredObjects(SlotPrototype)` 抛异常 →
`ScriptableGrid.CreateSlots` 里 `await Engine.Instantiate<TSlot>(SlotPrototype)` 永不返回。

`PrefabBuilder` 现在：

1. 主根按**文件名同名**选取（§39 的修复）；
2. **字段应用拆成两阶段**——先写"属于额外根"的节点，再把每个额外根
   `SaveAsPrefabAsset` 成 `<主名>_<根名>.prefab`，用 `MapTree`（活树与存盘资产逐层按子节点下标 + 组件下标并行遍历，
   以 instance id 建索引）记下 `pid → 资产内对象`，最后用这张表回绑主根字段里指向额外根的 PPtr
   （`ResolvePointer` 新增 `extraRootObjects` 参数）。

这样"引用另一个资产的组件"就是普通跨资产引用，等价于嵌套 prefab 依赖，不再需要"多根不可表达"这条免责声明。

### 40.2 实测

单批（GalleryUI + SaveLoadUI）+ 全量：

```
GalleryUI_GalleryStillSlot.prefab
SaveLoadUI_SaveLoadSlot.prefab
（全量：91 个同级 prefab 资产）
[PrefabBuilder] OK_WITH_PARTIAL_ASSETS: built=38 partial=118 failed=0 of 156; written=22053
```

关键证据是**逐组件**的（`MANOSABA_PREFAB_FIELD_DUMP=1`）：

```
-4398152851352888326 MonoBehaviour -> WitchTrials.Views.StillGrid owner=StillGrid
    APPLIED applied=92 skipped=0 warnings=0      ← slotPrototype 绑上了（若失败会是 warnings>=1 + 一条 message）
-4915742209394539249 MonoBehaviour -> WitchTrials.Views.GalleryStillGridSlot owner=GalleryStillSlot
    APPLIED applied=20 skipped=0 warnings=2      ← 它自己的 lockedTexture/loadingTexture 目标不在计划里，属如实缺口
```

运行时后果（G5，独立 Player）：

| 指标 | 修复前 | 修复后 |
|---|---|---|
| `Naninovel<Runtime>` 组件数 | 5682 | **11355**（画廊槽位真的被实例化出来了） |
| `ScriptableGrid.Awake → AssertRequiredObjects` 异常 | 4 | **0**（全日志搜索：`AssertRequiredObjects present: False`） |

其余异常只剩三类且都已定性：`TrueShadow` 材质（shader 缺失）、`LocalizeImage.OnEnable` 的 subscribe、
`CustomUI.OnEnable`（每次建 UI 一套一条）。

### 40.3 仍未解开的一点，已用探针钉到"最后一步"

`Engine.Initialize` 的后置循环现在能确认**确实走到了第 2 项**——探针在 `CapturePostInitTask` 里记了调用栈：

```
capture caller: ManosabaGameStart.CapturePostInitTask
  <- Naninovel.Engine.Initialize (EngineParams) [0x0037d] Engine.cs:157      ← await postInitializationTasks[i]()
  <- Naninovel.Async.CompilerServices.MoveNextRunner`1[TStateMachine].Run ()
  <- Naninovel.StateManager.PerformPostEngineInitializationTasks () [0x00159] StateManager.cs:329
```

同时三种调用形式**全部 COMPLETED**：

```
post-init capture:   1 invocation(s) [COMPLETED]: all completed      ← 引擎自己经包装调用那一次
post-init original:  InstantiateUIs COMPLETED on a direct call       ← 原委托直接调用
（§39 记录过 re-invoke 也 COMPLETED）
```

但引擎侧的权威读数仍是：

```
engine initializeTCS: state=0 (Pending), awaiter=none
naninovel: initialized=False initializing=True    progress_last=0.9167
```

`state=0 Pending` 说明 `initializeTCS.TrySetResult()`（`Engine.cs:161`）**没有被执行到**，
而循环走到第 2 项、第 2 项的任务又已完成——差的就是"循环结束后那一句"。
下一轮的仪器很小、很确定：**在 vendored 包的 `Engine.cs` 第 154-163 行之间临时插几条 `Debug.Log`**
（进入循环 / 每项 await 前 / 每项返回后 / `TrySetResult` 前后），跑一次 G5 读日志定位，然后撤掉插桩。

### 40.4 本轮修掉的一处自身事故（写进纪律）

用 `(Get-Content -Raw) -replace ... | Set-Content -Encoding UTF8` 改 C# 源文件时，**Windows PowerShell 5.1 的
`Get-Content -Raw` 默认按 ANSI(936) 解码**，文件里的非 ASCII 被转成 mojibake（`…` 变 `鈥?` 且丢一个字节），
`G1` 立刻报 42 个语法错误。所幸仓库根有 `.git`：`git show HEAD:` 对比确认**原始文件本就是纯 ASCII**，
唯一的非 ASCII 是我上一轮自己插入的 `…`；改回 `...` 后 mojibake 计数为 0，`G1 PASS`。
**纪律：源文件只用 edit/write 工具改，绝不用 PowerShell 文本 cmdlet 往返。**

### 40.5 本轮门状态

| 门 | 结果 |
|---|---|
| G1 编译 | **PASS**（`cs_errors=0 cs_warnings=0 import_errors=0 assembly_load_errors=0`） |
| Prefab 重建 | `built=38 partial=118 failed=0 of 156`（95.7 s，额外根 91 个同级资产） |
| G3 内容构建 | **PASS**：`entries=30819 locations=33220 bundles=11 (3590 MB)`（309.9 s） |
| G5 独立 Player | **PASS**：`playing=True`、`played_script=System/System_Title`、UI 组件 5682 → **11355** |
| G4b / G6 | 本轮未重跑 |

**目标仍未完成**（`Engine.Initialized` 尚未为 true），保持 active。下一轮：
(1) 在 `Engine.cs` 的循环与 `TrySetResult` 处临时插桩，定位"最后一句"为何没执行；
(2) 补齐 `GalleryStillGridSlot` 的 `lockedTexture`/`loadingTexture`（两目标不在 prefab 计划里）；
(3) 再收 TrueShadow shader 与 TMP_FontAsset 地址两处内容缺口；然后重跑 G4b/G6。

## 41. 2026-09-18 记录：找到并修掉真正的主因——重建的 prefab 里 `AssetReference` 用的是**出货 GUID**，而工程的 asset 用的是**重新生成的 GUID**

这一轮把"引擎永远初始化不完"从"最后一句没执行"追到了**一个可复现的异常**，再追到**一个系统性的标识符错配**，并做了全局修复。

### 41.1 插桩实测：循环确实停在第 2 项"调用之后、返回之前"

`Engine.Initialize` 里临时插的 `[EngineProbe]` 日志（Player 日志里）完整覆盖了三条 return 路径与 `TrySetResult`：

```
[EngineProbe] Initialize# enter pass=1 Initialized=False Initializing=False tcsNull=True
[EngineProbe] Initialize# pass=1 new initializeTCS (line 128), preTasks=0 services=23 postTasks=3
[EngineProbe] Initialize# pass=1 pre-init loop done
[EngineProbe] Initialize# pass=1 services loop done (23)
[EngineProbe] Initialize# pass=1 post-init i=0 calling PerformPostEngineInitializationTasks
[EngineProbe] Initialize# pass=1 post-init i=0 returned; Initializing=True
[EngineProbe] Initialize# pass=1 post-init i=1 calling InitializeLocalVariables
[EngineProbe] Initialize# pass=1 post-init i=1 returned; Initializing=True
[EngineProbe] Initialize# pass=1 post-init i=2 calling lambda_method
                                                                    ← 日志到此为止
```

`pass=1`（只进过一次）、`i=0/i=1` 正常返回、**`i=2` 的 `returned` 与后面的 `TrySetResult` 都没有出现**。
新增读数 `engine initializeTCS: state=0 (Pending), awaiter=none` 与之一致：`TrySetResult` 没执行。
`i=2` 是 `UiManagerExtended.InstantiateUIs`（注册成了闭包，所以方法名是 `lambda_method`）。

### 41.2 关键认识：`IsCompleted` 对**已异常**的任务同样为 true

之前连续几轮的探针都报 `[COMPLETED]`：

```
post-init capture:   1 invocation(s) [COMPLETED]: all completed
post-init original:  InstantiateUIs COMPLETED on a direct call
managed_ui_initialize: 19 个 UI 全部 Initialize()=COMPLETED
```

但 vendored 包的实现是 `UniTask.IsCompleted => awaiter == null || awaiter.IsCompleted`，
而 `UniTaskCompletionSource.IAwaiter.IsCompleted => state != Pending`（0=Pending/1=Succeeded/2=Faulted/3=Canceled）。
**Faulted 也满足 `IsCompleted`**，所以"COMPLETED"这个读数无法区分"成功"和"抛异常后结束"——
上一轮据此把"任务都完成了"当成事实，方向因此偏了一轮。纪律：**凡是只看 `IsCompleted` 的判定，必须同时读 `state`。**

### 41.3 真因链（Player 日志里一条完整的异常记录）

`Select-String` 过滤词把 `[EngineProbe]` 挡在 gate 的 stdout 之外，但异常本体一直在 `player_smoke.log` 里：

```
System.NullReferenceException: Object reference not set to an instance of an object
  at WitchTrials.Models.AuthorTextBuilder.LoadDataAsync (CancellationToken ct) [0x000fb] AuthorTextBuilder.cs:493
  at Cysharp.Threading.Tasks.UniTask+ExceptionResultSource.GetResult (Int16 token) UniTask.Factory.cs:326
  at WitchTrials.Models.AuthorTextBuilder.InitializeAsync (CancellationToken ct) [0x00029] AuthorTextBuilder.cs:377
  at Cysharp.Threading.Tasks.UniTask+ExceptionResultSource.GetResult (Int16 token) UniTask.Factory.cs:326
  at WitchTrials.Views.WitchTrialsLogUi.Initialize () [0x0006d] WitchTrialsLogUi.cs:96
  at Naninovel.UniTaskCompletionSource.Naninovel.Async.IAwaiter.GetResult () UniTaskCompletionSource.cs:89
  at Naninovel.UniTask.GetResult () UniTask.cs:44
  at Naninovel.UniTask+WhenAllPromise..ctor (UniTask[] tasks, int tasksLength) [0x0003e] UniTask.WhenAll.cs:179
  ...
  at Naninovel.UIManager.InstantiateUIs () [0x0017e] UIManager.cs:344
```

即：`InstantiateUIs` 里 `await UniTask.WhenAll(managedUIs.Select(u => u.UIComponent.Initialize()))`（`UIManager.cs:344`）
**同步抛出**（`WhenAll` 的构造器在某个子任务已完成且已异常时会直接 `GetResult`），异常沿
`InstantiateUIs` → `Engine.Initialize` 的第 182 行 `await` 抛出，`Engine.Initialize` 因此变成 faulted；
而 `RuntimeInitializer` 只轮询 `IsCompleted`（`RuntimeInitializer.cs:93`），**异常被完全吞掉**，
于是 `initializeTCS` 永远是 Pending、`Initializing` 永远是 true，游戏却继续往下跑。

`AuthorTextBuilder.cs:493` 是

```csharp
(CharacterData characterData, AuthorData authorData) = await (GetOrLoadAddressableAsset<CharacterData>(_characterDataAsset, ct), ...);
_nameData = characterData.Items.ToDictionary(...);      // ← NRE：characterData 为 null
```

两个 Addressable 资源加载返回了 null。

### 41.4 系统性根因：GUID 错配（不是这一个资源的偶发问题）

`WitchTrialsLogUi._authorTextSettings._characterDataAsset` 的重建值是从出货 typetree 里逐字抄的，
所以它带着**出货 GUID**；去 `catalog_dump_shipped.json` 查：

```
key 519e9e2fa7c404c4689c3f073450d8e5 -> Assets/#WitchTrials/Data/ScriptableObjects/CharacterData.asset
key 46882e9ef46e74270a18bf9ec3fb93bd -> Assets/#WitchTrials/Data/ScriptableObjects/AuthorData.asset
```

资产文件在工程里是有的，但 `.meta` 里的 GUID 是**抽取时重新生成的**：

```
出货期望 519e9e2fa7c404c4689c3f073450d8e5
工程实际 eda968d981e9b834b8e0cbcc1d786e28     ← CharacterData.asset.meta
```

于是 `AssetReference.RuntimeKeyIsValid()` 为 true、Addressables 查不到、加载返回 null。
`catalog_compare.json` 的 `note` 其实早就把这件事写下来了，只是当时被当成"无害的构造性差异"：

> "Our GUID keys are this project's asset GUIDs, so they cannot match the shipped ones..."

重建的 prefab 里**每个 `AssetReference` 字段**都受这个错配影响，`CharacterData` 只是第一个被抓到的。

### 41.5 修复：把工程的 asset GUID 对齐到出货 GUID

新增 `tools/unity-rebuild/align_asset_guids.py`：

* 映射来源是**可精确恢复**的：`catalog_dump_shipped.json` 里每个"单 location 的 GUID key"给出
  出货 GUID → 出货路径；工程的 `.meta` 给出当前路径 → 当前 GUID；两边按路径对齐即得 old → new。
* 吸收两处目录布局差异：抽取把资产放在 `Assets/GameContent/` 下（出货是 `Assets/`）；
  多 sprite 的打包贴图被抽到 `<Name>.sprites/<Name><ext>` 子目录
  （`.../Packed/UI_Common/CloseButton_Default.png` 实际在 `.../CloseButton_Default.sprites/` 下）。
* 按**字节**全局改写 32 位 hex GUID（ASCII，二进制安全），范围是 `UnityProject/Assets` 与
  `UnityProject/Packages` 下的文本资产；`--revert` 可按记录回滚。

实测与结果：

```
shipped catalog GUID keys            : 28032（单 location；300 条多 location，0 条只有 bundle）
already aligned                      : 8
need remapping                       : 932        ← 加 `.sprites/` 规则后从 766 升到 932
shipped GUID 无对应工程资产           : 27092
   按扩展名：.wav=26537  .nani=440  .png=95  .mat=14  .mp4=4  .asset=2
collisions（1 个出货 GUID 对 2 个资产）: 0
改动文件                             : 938 个文件 / 1996 处
复验：CharacterData.asset.meta guid = 519e9e2fa7c404c4689c3f073450d8e5   ✓
      AuthorData.asset.meta   guid = 46882e9ef46e74270a18bf9ec3fb93bd   ✓
      全工程残留 old GUID 文件数 / 出现次数 = 0 / 0                       ✓
```

**这个改动是安全的，因为 prefab 的指针绑定走的是路径而不是 GUID**：
`PrefabBuilder.ResolvePointer` 对 `m_FileID != 0` 的分支是用 `references[node.pid:fileID:pathID]`
拿到资产路径再 `AssetDatabase.LoadAssetAtPath`，从不查 GUID 表；因此改 `.meta` GUID 不会让已绑定的 3549 个指针失效，
而全局文本改写同时保证了"引用了旧 GUID 的地方"一起被改写（复验残留为 0 就是这个意思）。
未对齐的 27092 条里，`.wav`（26537）在工程里根本不存在、`.nani`（440）被转成了 `Naninovel.Script` 的 `.asset`
按 key 寻址，二者都不经由 GUID 引用，故本轮不阻塞。

### 41.6 GUID 对齐之后故障"搬家"，连续挖出另外两个根因（都不是数据问题，是**桩方法体**）

GUID 对齐 + 内容重建（G3 `PASS`，`entries=30819 locations=33220 bundles=11`）后重跑 G5，
`initializeTCS` 仍是 `state=0`——但**异常的栈换了**，这正是继续挖的入口：

```
之前：WitchTrials.Models.AuthorTextBuilder.LoadDataAsync (ct) [0x000fb] AuthorTextBuilder.cs:493
现在：WitchTrials.Models.AuthorTextBuilder+<>c.<LoadDataAsync>b__30_1 (CharacterDataItem item) AuthorTextBuilder.cs:495
      System.ArgumentException: An item with the same key has already been added. Key: Ja
```

`AuthorTextBuilder.cs:493` 是"`characterData` 就是 null"，`:495` 是"`characterData` 拿到了、在遍历 `Items` 时炸"。
也就是说 **`GetOrLoadAddressableAsset` 从"什么都没做"变成了真的把资源取回来了**，故障往后走了一步。

**(a) `AddressablesManager` 的两个泛型重载仍是桩。** `AddressablesManager.cs:630/637`：

```csharp
public UniTask<T> GetOrLoadAddressableAsset<T>(string address, [Optional] CancellationToken ct)
{
    return default(UniTask<T>);          // ← 桩：不 await 任何东西，返回 default(T) == null
}
public UniTask<T> GetOrLoadAddressableAsset<T>(AssetReference reference, [Optional] CancellationToken ct)
{
    return default(UniTask<T>);          // ← 桩
}
```

`return default(UniTask<T>)` 的 awaiter 是空的，`await` 它直接得到 `default(T)` = **null**，
所以**游戏里每一处 `AssetReference` 加载都静默返回 null**。两个重载的状态机结构（`d__22` 有
`_linkedCts_5__3`、`d__23` 没有）连同 `addressables_manager_spec.md` §3.1/§3.2 已经把契约写全了：
前置 `await IAddressablesManager.InitializeAsync(ct)` 一次性闸门 → `_loadedAssets` 命中直接返回 →
`_pendingLoadAssetTasks` 合并并发请求 → `CreateLinkedTokenSource(ct, LoadAssetCts.Token)` →
`Addressables.LoadAssetAsync<T>` → `handle.ToUniTask(cancellationToken: linkedCts.Token, cancelImmediately: true)`
→ `_loadedAssets[address] = asset` + `_handles.Add(handle)` → `finally` 里移除 pending。
按 spec 逐条实现（与 `IAddressablesManager.InitializeAsyncCore` 同样用 `Core` 辅助方法承载，
避免编译器生成的嵌套状态机与保留的 `d__22/d__23` 证据结构重名）。

**(b) `LocalizedText` 的三个成员是桩——这才是 `Key: Ja` 的直接来源。** 原始工程源码里：

```csharp
public LocaleKind Locale { get { return default(LocaleKind); } }   // 恒等于 LocaleKind.Ja (0)
public string Text       { get { return null; } }                   // 恒等于 null
public LocalizedText(LocaleKind locale, string text) { }            // 构造器什么也不存
```

`AuthorTextBuilder.LoadDataAsync` 的 `item.Name.ToDictionary(t => t.Locale, t => t.Text)`
于是给 4 个条目全部算出同一个键 `Ja` → `Dictionary.Add` 抛"键已存在" → 异常沿
`InstantiateUIs` → `Engine.Initialize` 抛出 → 被 `RuntimeInitializer` 的 `IsCompleted` 轮询静默吞掉。

重构依据是**同 RVA 的共享体**（本仓库既有的手法）：

| 成员 | RVA | 共享体语义（工程内已有的同类重构） | 结论 |
|---|---|---|---|
| `LocalizedText.Locale` | `0x331650`, 4 B | `mov eax,[rcx+0x10]; ret`；`LocaleData.Kind`、`SpecialThanksDataItem.Course` 都已按此重构（149 处共用） | `return _locale;`（`_locale // 0x10`） |
| `LocalizedText.Text` | `0x33A4E0`, 5 B | `mov rax,[rcx+0x18]; ret`；`CharacterData.Items`、`CharacterDataItem.Name`、`LocaleData.Item` 均已按此重构（601 处共用） | `return _text;`（`_text // 0x18`） |
| `LocalizedText..ctor` | `0x38F5B0`, 82 B | `SpecialThanksDataItem` 的构造器用同一 RVA，语义是"`edx` 存 +0x10、`r8` 存 +0x18" | `_locale = locale; _text = text;` |

值得记一笔的是：`LocaleData.Kind` 之前踩过**完全一样**的坑（注释里写着 `LocalizationPresenter.Awake`
用它建 locale 字典，"while it returned `default` every entry keyed to LocaleKind 0"）——
同一类"共享体没重构"的缺陷在同一个 RVA 上出现了第二次。

### 41.7 结果：引擎真的初始化完成了

撤掉 `Engine.cs` 的 `[ROUND45-PROBE]` 插桩（从 `work/probe_out/Engine.cs.orig` 还原，25822 字节，
插桩标记 0 处）后复跑，G5 读数：

```
G1 PASS: cs_errors=0 cs_warnings=0 import_errors=0 assembly_load_errors=0
G5 PASS: player exit=0 ... (32.81s)
  game start: STARTED ... Game(Clone) (202 components)  completion=COMPLETED     ← 之前是 STILL RUNNING after 40s
  naninovel: initialized=True initializing=False playing=True played='System/System_Title' spot=' #1.0'
  engine initializeTCS: state=1 (0=Pending 1=Succeeded 2=Faulted 3=Canceled)      ← 之前是 state=0
```

`Engine.Initialized == true`、`initializeTCS` 为 Succeeded、`GameInitializer.InitializeAsync` 完成、
Naninovel 已在播 `System/System_Title` —— 启动链是**真的打通了**，这是从 §14 以来一直没拿到的一步。

### 41.8 本轮修掉的三件事，与"下一类"缺陷的规模

三个根因互相独立，缺一不可：

1. **GUID 错配**（§41.4-41.5）：重建 prefab 里 `AssetReference` 带的是出货 GUID，工程 asset 带的是重新生成的 GUID
   → 932 个 GUID / 938 文件 / 1996 处对齐。
2. **`AddressablesManager.GetOrLoadAddressableAsset<T>` ×2 桩**（§41.6a）：每个 AssetReference 加载都返回 null。
3. **`LocalizedText` 的 `Locale`/`Text`/ctor 桩**（§41.6b）：`ToDictionary` 键全部塌成 `Ja`。

而 (2)(3) 说明真正的"长尾"是**桩方法体**，不是数据。`scan_stub_bodies.py` 的当前读数是：

```
total_stub_members: 2233        （UnityProject/Assets/Scripts/Game 下仍是 dumper 占位体的成员数）
```

已经确认仍为桩、且**已知会影响玩法**的几处（留待后续）：

* `CharacterDataItem.Age` / `.Height`（`0x38F060` / `0x3786A0`）：这两个 RVA 的 6 个共用者**全都还是
  `return null`**，没有任何已重构的同类可以当证据；`Weight`（`0x347410`）已经对了。人物资料页会缺这两项。
* `LocalizeImage.OnEnable` 的 38 处 UniRx 订阅异常、`IPerspectiveStageManager` 未注册、
  TrueShadow shader 缺失（`new Material(null)`）、`TMP_FontAsset` 地址。
* `WitchBookUI` 仍有 30 个 "component type not found"。

### 41.9 本轮门状态

| 门 | 结果 |
|---|---|
| G1 编译 | **PASS**（`cs_errors=0 cs_warnings=0 import_errors=0 assembly_load_errors=0`） |
| GUID 对齐 | **已应用**：932 GUID / 938 文件 / 1996 处；全工程残留 old GUID = **0** |
| G3 内容构建 | **PASS**：`entries=30819 locations=33220 bundles=11 (3590 MB)`；`missing=0` |
| G5 独立 Player | **PASS**：`initialized=True`、`initializeTCS state=1`、`completion=COMPLETED`、`played='System/System_Title'` |
| `Engine.cs` 插桩 | **已撤除**（还原自 `work/probe_out/Engine.cs.orig`） |
| G4b Play Mode | **PASS**：`12.8:INITIALIZED:0.917`、`initialized=True`、`played='System/System_Title'`（§41.11） |
| G6 内容覆盖 | **PASS (COVERED, exit 0)**：9 族 `missing_assets=0 addr_mismatch=0`、`440/440` 剧本 path 匹配、`runtime-played: MEASURED`（§41.10） |

**目标达成**：启动链打通（§41.7）、Play Mode 与独立 Player 都进入游戏流程并真的在播重建出来的剧本、
G4b 与 G6 均已复验（G6 的 PARTIAL 已定性为比对基准问题并修好）。
不属于本目标、明确留给后续的工作：TrueShadow shader 缺失（714 条视觉异常）、
`LocalizeImage` 的 observable 未接（174 条）、剩 2233 个桩方法体、
`CharacterDataItem.Age/Height`（无可用证据）、完整通关验证。

### 41.10 G6 的 PARTIAL 已定性并修好：是**比对基准**错了，内容一直是全的

上一轮先把事实记下来不下结论，这一轮用**运行时解析器**（不是 grep 二进制）把问题钉死了。
`Manosaba.EditorTools.CatalogDump.Run` 支持 `MANOSABA_CATALOG_PATH` / `MANOSABA_CATALOG_OUT` 覆盖，
所以可以把**工程自己构建出来的** catalog 用同一套 `ContentCatalogData.LoadFromFile + CreateCustomLocator`
路径 dump 成 `out/unity/catalog_dump_ours_fresh.json`，与出货 dump 的 key 表直接对比
（脚本：`tools/unity-rebuild/compare_catalog_keys.py`）：

```
shipped    keys=57212   guid=28332  非 guid=28880
ours       keys=59235   guid=30819  非 guid=28416     两者共有的非 guid key：28405

关键探针                    出货(命中 location 数)   重建(命中 location 数)
General/Data/CharacterData            1                      1
General/Data/AuthorData               1                      1
Naninovel/Audio/Ambi_1_1              1                      1
Naninovel/Scripts/System/System_Title 1                      1
Naninovel                         28150                  32459

=== 本工程的计划地址到底对不对 ===
plan entries with an address      : 30819
  matched against shipped KEYS      : 30819 / 30819
  matched against shipped ADDRESSES : 30819 / 30819
  matched against OUR rebuilt KEYS  : 30819 / 30819
  not a shipped address             : 0
```

**30,819 条计划地址一条不差地同时存在于出货 catalog 和重建 catalog 里。** 所以内容与寻址从来没有问题，
PARTIAL 是 `check_content_coverage.py` 的 `shipped_addresses()` 的错：

```python
"""Asset paths present in the shipped catalog.

The catalog's *keys* are GUIDs, not addresses (57,212 of them, none starting with `Assets/`); ...
Comparing our plan addresses against the keys would therefore report every single entry as a mismatch
"""
```

它只收集 `locations[].internal_id` 和每个 key 背后的 location 路径——**资产路径**——于是拿"计划里的出货主键"
去比"资产路径"，必然全量不一致。而那段 docstring 的推理正是它自己警告过的坑：
**"没有 key 以 `Assets/` 开头"这个测量是真的，但"所以 key 只有 GUID"这个结论是假的**——
key 表里有 28,332 个 GUID key，也有 28,880 个非 GUID key，而那些非 GUID key 就是地址本身。

修法（两处，都按测量重述，**不是为了让门变绿而放宽**）：

1. `shipped_addresses()` 改为把 key 字符串本身纳入集合（资产路径仍然接受，因为计划也可以按路径寻址）。
2. 剧本那条 `path_field_matches_address`：旧式子是
   `address.split("Scenarios/")[-1][:-5] if address.endswith(".nani") else None`，
   假定地址以 `.nani` 结尾。实测 440 条**全部**满足
   `address == "Naninovel/Scripts/" + <转换后资产自己的 `path` 字段>`（440/440，脚本：
   `work/check_scenario_addressing.py`），且 `Naninovel/Scripts/System/System_Title` 本身就是出货 key、
   也就是实跑真的播起来的那条剧本。于是新增 `expected_script_path()` 按测出来的形式推导期望值，
   `.nani` 旧拼法保留为第二种可接受形式；**推导不出来的地址仍然计入 mismatch**，不会因为改宽而放行。

修后：

```
G6 content coverage: COVERED
  Manosaba.Audio     BUILT planned=26537 on_disk=26537 missing_assets=0 addr_mismatch=0
  Manosaba.Data      BUILT planned=8     on_disk=8     missing_assets=0 addr_mismatch=0
  Manosaba.Fonts     BUILT planned=24    on_disk=24    missing_assets=0 addr_mismatch=0
  Manosaba.Prefabs   BUILT planned=1952  on_disk=1952  missing_assets=0 addr_mismatch=0
  Manosaba.Scenarios BUILT planned=440   on_disk=440   missing_assets=0 addr_mismatch=0
  Manosaba.Sprites   BUILT planned=17    on_disk=17    missing_assets=0 addr_mismatch=0
  Manosaba.Text      BUILT planned=450   on_disk=450   missing_assets=0 addr_mismatch=0
  Manosaba.Textures  BUILT planned=1389  on_disk=1389  missing_assets=0 addr_mismatch=0
  Manosaba.Video     BUILT planned=2     on_disk=2     missing_assets=0 addr_mismatch=0
scenarios: 440/440 converted, 440 path fields match their address
runtime-played: MEASURED (reported by player_game_start_evidence.json)
G6 PASS (exit 0)
```

顺带纠正上一轮一个**我自己说错的判断**：我曾据"按文件名搜不到某个 `.wav`"说"语音音频根本不在工程里"。
G6 现在逐族测量的是 `missing_assets=0`（音频 26,537 个资产的 `asset_path` 全部存在），
那条说法是错的——单文件名搜索不构成缺失证据。

### 41.11 G4b（Editor Play Mode）也通过了

同一轮重跑 G4b（`MANOSABA_GAMESTART_CHAIN_SECONDS=45` → play-mode 预算 270 s）：

```
=== warm-up Play Mode (absorb Unity's own mid-session recompilation) ===
  warm-up exit=0 in 41.1s; compile requests=0, post-compilation reloads=0
G4b PASS: entry=WitchTrials.Views.GameInitializer resolved=True component=False source=scene manager_created=True
  boot chain stops at: OK
  naninovel: initialized=True initializing=False playing=True played='System/System_Title'
  samples: 0.0:idle:-1.000 ... 8.6:initializing:0.489 10.6:initializing:0.917 12.8:INITIALIZED:0.917
```

`12.8:INITIALIZED` 是 §38 以来一直没能在 Play Mode 里拿到的状态。（这次 warm-up 报 `compile requests=0`，
说明 §38.5 那个域重载这次没有触发；它是竞态，不是确定性障碍，所以 G4b 的结论必须附这一条。）

### 41.12 剩余异常的分类：都不是流程阻塞

923 条 NRE 的**抛出点**逐个归类（`player_smoke.log`）：

| 条数 | 抛出点 | 性质 |
|---|---|---|
| 714 | `LeTai.TrueShadow.TrueShadow.OnDisable` (TrueShadow.cs:978) | 就是那 714 条 `ArgumentNullException`（`GetMaterial` → `new Material(null)`，TrueShadow 的 shader 在占位包里不存在）的下游；**纯视觉**（文字投影） |
| 114 | `UniRx.ObservableExtensions.Subscribe` ← `WitchTrials.Views.LocalizeImage.OnEnable` (:100) | 本地化的 observable 没接上，切语言不会刷新图片；**待修** |
| 60 | `WitchTrials.Views.LocalizeImage.OnDisable` (:109) | 同上 |
| 27 | `WitchTrials.Views.PointerStayingSpriteSwapper.OnDestroy` | UI 辅助组件拆除 |
| 3 | `Naninovel.UI.CustomUI.OnEnable` (CustomUI.cs:168) | UI |
| 3 | `Naninovel.CameraManager.get_Orthographic` (CameraManager.cs:40) | 相机尚未就绪 |

另外 8 条 `FileNotFoundException: Assembly-CSharp` 来自**我们自己的探针** `ManosabaBoot.ProbeCatalog`
（它枚举 catalog 的每个 key 去 `Locate`，资源类型标在原始 `Assembly-CSharp` 名下的那些自然反序列化不了），
与游戏无关。

最后复核了"播起来了"这件事本身有没有内容：

```
System_Title.asset  58341 bytes
  path: System/System_Title
  playlist:
    scriptPath: System/System_Title
    commands:
    - rid: 4985271874984609880  ...
```

58 KB、真实 `playlist.commands` 列表（配 `references` 侧表）——即实跑确实从重建的内容里装载并开始执行剧本，
不是"空剧本上挂了个 playing=True"。`naninovel_total_commands=0` 只是探针读不到 `totalCommands` 字段
（同一份证据里 `naninovel_total_commands_known=False`）。

**目标推进了一大步但尚未收口**：`Initialized == true` 已达成，剩下的验收是 G4b（Play Mode）与
按 `stub_scan_after_localizedtext.json` 继续清剩下的桩体。保持 active。


















# 2026-09-18：TrueShadow 与 Naninovel TMP 运行资源修复

范围：基于 `015c0cfa2c4ecd35518c3d7fec1371bacf0c73c4`，只补资源及 wiring。
不修改 `IDefaultLocaleSelector` 或其他 localization service 的重复注册，也不批量修 stub。

## 来源及采用决策

下列 GitHub commit/path 通过公开 API/原始文件核对；仅研究结构与接口，**未复制其商业 shader/font 文件到提交**。

| 仓库 | commit | 核对路径 | 许可证/类别 | 采用内容 |
|---|---|---|---|---|
| [xukonxe/LibreAirspace](https://github.com/xukonxe/LibreAirspace/tree/317660ea244f923e1f3fb0defa4a9eed0e2bc908) | `317660ea244f923e1f3fb0defa4a9eed0e2bc908` | `Assets/Le Tai's Asset/TrueShadow/Resources/Shaders/`、`Scripts/Structure/BlendMode.cs` | 仓库 MIT，但不能据此认定 vendored 商业 TrueShadow 可再分发；社区副本 | 确认目录及四个 `Shader.Find` 名称一致；不采用文件实现 |
| [Arikaton/Cuteblock](https://github.com/Arikaton/Cuteblock/tree/5adbfef23876542d0b123b5eb93787df7bdbdf03) | `5adbfef23876542d0b123b5eb93787df7bdbdf03` | `Assets/Plugins/Le Tai's Asset/TrueShadow/Resources/Shaders/TrueShadow-Normal.shader` | 未声明可确认的仓库许可证；社区副本 | 结构佐证，不采用 |
| [1sakshm/ProjectUtopia](https://github.com/1sakshm/ProjectUtopia/tree/b6f685166162d2b7e9d15ee916d80677ac21d244) | `b6f685166162d2b7e9d15ee916d80677ac21d244` | `Assets/Asset/TrueShadow/Resources/Shaders/TrueShadow-Normal.shader` | 未声明可确认的仓库许可证；社区副本 | 结构佐证，不采用 |
| [oswaldpfeiffer/madbox-test-oswald](https://github.com/oswaldpfeiffer/madbox-test-oswald/tree/29d3c56c27f6cf212edb33636b91467936737d7f) | `29d3c56c27f6cf212edb33636b91467936737d7f` | `Assets/Packages/TrueShadow/Resources/Shaders/TrueShadow-Normal.shader` | 未声明可确认的仓库许可证；社区副本 | 结构佐证，不采用 |
| [thecasterian/mnsb-editor](https://github.com/thecasterian/mnsb-editor/blob/9e9365c0acef417ffd9bf82302a1849f7748f477/CLAUDE.md) | `9e9365c0acef417ffd9bf82302a1849f7748f477` | `CLAUDE.md`：TrialsClockFont / `extract_trial_digits.py`、`characters/font_materials.json`、`docs/text_shadows.md` 的说明 | 未声明可确认许可证；社区提取研究，不是字体授权 | 核对 `general-fonts-common_assets_all.bundle`、SC 材质变体和日文字体 metrics；最终使用本地数据 |

TrueShadow 当前源是原有的**反编译副本**，本次没有将其改称官方包。
新增 `Packages/com.le-tai.true-shadow/Resources/` 是 **COMPATIBILITY RECONSTRUCTION**，不是 ORIGINAL ASSET。
自主实现 9 个 shader（4 个 UI blend、Generate、GenerateHQ、Cutout、ImprintPostProcess、PostProcess），
遵守本地 C# 的 pass/property 调用约定。生成的 32×32 噪声是 white-noise 兼容纹理，**非恢复的原版 blue-noise**；
Project Settings 使用源码默认值，presets 为空。GPU pass 可用不等于与原版逐像素相同，尤其快速模糊和 spread/inset 边缘。
接口清单见包内 `Resources/COMPATIBILITY.md`。

## 原字体实际找到了什么

`res/_index.csv`、本地原游戏 bundle 和已有 `catalog_dump.json` 一致指向以下条目：

| 字体 | 原 TMP path_id | 原 bundle | 原 catalog internal id |
|---|---:|---|---|
| TsukushiMincho | -4585659588899376959 | `general-fonts-tsukushimincho_assets_all.bundle` | `Assets/#WitchTrials/Fonts/TsukushiMincho/TsukushiMincho.asset` |
| SourceHanSerifSC | 8324895201456969531 | `general-fonts-sourcehanserifsc_assets_all.bundle` | `Assets/#WitchTrials/Fonts/SourceHanSerifSC/SourceHanSerifSC.asset` |
| SourceHanSansKR | 5503908547053578156 | `general-fonts-sourcehansanskr_assets_all.bundle` | `Assets/#WitchTrials/Fonts/SourceHanSansKR/SourceHanSansKR.asset` |

原 key 为 `Naninovel/Fonts/<NAME>/Assets/<NAME>`；Naninovel provider 去掉 `Naninovel/` 后，
配合 UIConfiguration 的 `FontLoader.PathPrefix=Fonts`，满足现有 `<NAME>/Assets/<NAME>`，**没有改掉配置绕过**。
原始目录中的资源类型是 `TMPro.TMP_FontAsset`；修复前工程这些地址只有普通 Font/Texture 等条目。

三个原 TMP JSON 均为 `m_AtlasPopulationMode=1`（Dynamic），glyph table、character table、fallback table 都为空。
因此本次从**已有本地原 OTF**与 TMP JSON 创建原生 `TMP_FontAsset`，而非声称恢复了不存在的静态字形表。
保留原 face info、style、padding、render mode、atlas 尺寸（JP/SC 4096，KR 1024），启用 multi-atlas，
由 FontEngine 在运行时生成实际 glyph/character/atlas。保留空的原字体 fallback table；全局 TMP fallback 新增这三种字体，
用于 Naninovel 切换字体前的语言标签。JP 的 pointSize=90、ascentLine≈79.2 与本地元数据及社区研究一致。
JP/SC 各恢复 7 个 `@Shadow1..6` / `@Outline_Shadow` 材质的参数，使用 Unity TMP shader，不复制原 shader 二进制。
这是 **REBUILT_FROM_LOCAL_ORIGINAL_FONT_AND_METADATA**，不是字节级原资源复原，也不是开源字体 fallback。

原 OTF SHA256：

- JP `0042affca7f83b7b1d8b5fa9362481e82622a09152b3d5d593314869df8c9490`
- SC `faf16caeaf207e1926e01af8d9f1a8e71b0e9d8f51bf3b5d9a78f9f30e7e3e31`
- KR `2c01d8b5dfd59dfe45d7bb1ee2360f3186266468fa5b036e2818ddf582f5bc9d`

没有下载或提交第三方商业字体；原 OTF、生成 TMP/atlas/material 仍留在被忽略的 `Assets/GameContent/`。
提交修复工具、稳定字体 GUID、Addressables group 和 TMP fallback 引用。全新工作区需先完成已有的本地资源导入，
再执行下面的 repair，不能只 checkout 源码就假定拥有原游戏资源及其再分发许可。

## 可重复执行

设置 `MANOSABA_UNITY_EDITOR` 为已正常授权的 Unity 6000.0.48f1，关闭同项目的其他 Editor 实例。
使用可运行的 Python（本机旧 `tools/venv` 解释器启动失败，实际使用 Codex bundled Python）：

```powershell
python tools/unity-rebuild/runtime_resources.py repair --tag local
python tools/unity-rebuild/runtime_resources.py verify --graphics --tag local
python tools/unity-rebuild/runtime_resources.py build --tag local
python tools/unity-rebuild/runtime_resources.py verify-built --graphics --tag local
$env:MANOSABA_GAMESTART_CHAIN_SECONDS='60'
$env:MANOSABA_GAMESTART_AUTO_ADVANCE='1'
$env:MANOSABA_GAMESTART_AUTO_ADVANCE_SECONDS='12'
./tools/unity-rebuild/g4b_game_start.ps1 -Wire
python tools/unity-rebuild/classify_runtime_resource_log.py tools/unity-rebuild/out/logs/game_start.log
```

注意：不要在 repair 后再用旧全量计划 prune `Manosaba.RuntimeFonts`；该独立 group 由 repair 管理。
同地址普通 Font 条目保留，以维持原有 smoke 的 Font 类型检查；新的 TMP 条目由 `LoadAssetAsync<TMP_FontAsset>` 区分。

## 本轮实际证据（不把 smoke PASS 误当成没有异常）

- 原资源检查：14 项失败（9 shader、noise、settings、3 TMP）。
- 最终 G1 编译：PASS，C# error/warning、import error、assembly load error 均为 0；最终 repair 重复执行 exit=0，三个字体 GUID 保持不变。全新空目录重建分支尚未独立验收。
- 独立副本的同输入资源合同检查：BASELINE exit=1（17 缺项），MODIFIED exit=0；执行 `ROLLBACK.sh` 后 exit=1、输出与基线相同且逐文件哈希恢复，再应用二进制 patch 后 exit=0。此项验证可逆性，不冒充 Unity 运行时回滚测试。四角色产物位于 `work/startup-fix-20260918/`，之前未应用的 C# 提案单独归档，不属于本次资源提交。
- `modified2-verify`：真实 Unity/GPU 检查 15 项通过、0 失败；日中韩文本生成网格并渲染 PNG，人工查看没有整屏方框。
- Addressables 构建 exit=0，status=OK，30,822 entries、33,230 locations、12 bundles。
- `modified-verify-built`：从刚构建 catalog 按三个原 key 加载 **TMP_FontAsset**，全部通过，exit=0。
- G4b `-Wire`：PASS，场景 GameInitializer 启动、initialize_completion=COMPLETED，Naninovel initialized=True，
  播放 `System/System_Title`，自动推进窗口完成。这个结果不等于验证整部游戏或所有标题按钮交互。
- 该 Play Mode 日志：TrueShadow shader null **0**；JP/SC/KR 字体 load failure **分别为 0**。
- 仍有旧 shipped catalog 探针的 Assembly-CSharp FileNotFoundException（8 次日志出现）；
  LocalizeImage.Map/OnDisable 空引用（各 57 次首行栈），PointerStayingSpriteSwapper.OnDestroy（27 次首行栈）。
  未在本轮扩展修复；标题脚本 index=6 在观测窗口不再前进，后续交互/其他 stub 尚未验收。
- IDefaultLocaleSelector 重复注册 warning 4 次日志出现，按要求保留。异常计数是日志出现数，非去重异常事件数。

报告及图像在 `tools/unity-rebuild/out/runtime-resources/`；G4b 日志/JSON在既有 out/logs 与 out/unity。
前置错误也保留：沙箱内许可证读不到，正常用户授权执行后许可检查通过；最初 Unity 已打开导致实例冲突；
生成器首次遇到 SC 材质文件名碰撞（实际为 `~2.json`），已改为按序列化材质名绑定；首次 CJK 验收的 Canvas/纹理导入缺项已修正。

## 2026-09-18 后续：按用户明确要求提交本地字体

上述“不提交原字体/生成资源”是上一提交的范围；本次单独将三个本地 OTF、对应 TMP/材质及 Unity .meta 纳入 Git，未修改字体字节或原资源目录，未推送远端。字体 SHA256 与上节一致，资源合同检查重新执行 PASS（0 缺项）。这不是字体再分发许可声明；公开分发前仍需自行确认相应许可。本次只改变版本控制状态，不重复声称完成新的 Unity 运行时验收。
