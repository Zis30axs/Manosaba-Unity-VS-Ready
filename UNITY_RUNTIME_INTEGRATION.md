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

**没有任何一门被标为通过，除非下方明确写 PASS。**

| 门 | 状态 | 说明与证据 |
|---|---|---|
| **G0** 输入与审计 | **PARTIAL** | 输入/hash/版本可复现：`preflight.json` ✅。审计回归测试通过：`tools/audit/test_audit_regressions.py` **33 passed / 0 failed**（exit 0）✅。每个源对象台账归属：`object_ledger.jsonl` 60,144 行 ✅。**未完成**：`--hash` 全量 SHA-256 尚未重跑（`NOT_RUN`） |
| **G1** 工程与编译 | **BLOCKED** | `Unity.exe` 可执行，但**无有效许可证**：`No valid Unity Editor license found. Please activate your license.` 实测命令与返回码见 `out/unity/g1_compile.json`（`status=BLOCKED`，`blocked_reason` 已记录）。**不得**用 `dotnet build` 代替，故本门不通过 |
| **G2** 全资源导入 | **NOT_RUN** | 依赖 G1 |
| **G3** 内容构建 | **NOT_RUN** | 依赖 G1 |
| **G4** Play Mode | **NOT_RUN** | 依赖 G1 |
| **G5** 独立 Player | **NOT_RUN** | 依赖 G1；Windows IL2CPP 模块经 Hub 安装未完成 |
| **G6** 全内容覆盖 | **NOT_RUN** | 依赖 G1 |

`g1_compile.ps1` 在许可证缺失时以退出码 **2** 结束（BLOCKED），不会误报 PASS。

### G1 阻断的精确补齐动作

**修复后只剩一个阻断项：Unity 许可证。** 工具链本身已齐备并逐项核验：

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

1. **激活 Unity 许可证**（用户动作）→ G1 起全部解锁。
2. 安装 `windows-il2cpp` 模块 → G5。
3. 运行 `g1_compile.ps1`（若需先移除 DummyDll 运行时代码引用并完成 C# 9 兼容转换）。
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
