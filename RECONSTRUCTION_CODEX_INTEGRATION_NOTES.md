# Manosaba 集成施工记录（Codex 审计 §3 执行）

> 本文档记录**实际执行**的动作与结果。审计来源文档 `UNITY_READINESS_CODEX_AUDIT.md`
> 的 Codex 署名保持不变；本文件不冒充审计署名。
> 状态词只用 `PASS` / `FAIL` / `BLOCKED` / `NOT_RUN`，且只在真正执行后使用 `PASS`。

---

## 1. 起点

| 项 | 值 |
|---|---|
| 起始提交 | `5f2895a`（`origin/main`，即引入审计文档的提交） |
| 起始分支 | `master`，fast-forward 自 `33cd707`；未新建分支、未强推 |
| 起始工作树 | 干净（`git status --porcelain` 为空） |
| 其他代理 | `recon/core-systems`(cb360c1 @ `E:/Manosaba-Claude`)、`audit/unity-assets`(ac81a9a @ `E:/Manosaba-DS`) 均未触碰 |
| 只读边界 | 游戏目录、`res/`、457 个 bundle 全程只读；`src/` 一个字节未改（`git status -- src/` 为 0 行） |

## 2. 逐项执行记录

### 2.1 审计器修复（§3.2）— PASS

| 缺陷 | 修复 | 证据 |
|---|---|---|
| a03 丢弃 container 目标 | 保留完整 `AssetInfo`（含 `m_FileID`/`m_PathID`）与 `preloadIndex`/`preloadSize`，并保留 `m_PreloadTable`、`m_Dependencies` | `a03_worker.py`；实测 container 目标可解析 4/4 |
| a03 对象身份 | 按 `obj.assets_file.name` 归属到具体 SerializedFile | `a03_worker.py` |
| a04 拼接 externals | 改为按**每个 SerializedFile 自身**的 externals 解析 `m_FileID` | `a04_refs.py` |
| a04 正则找引用 | 改为结构化遍历并记录字段路径 | `a04_refs.py: iter_pptrs` |
| a07 `setdefault` 丢来源 | key → locations 多值模型，重复/别名全部保留 | 实测 284 个 key 有多 location |
| a07 basename 去重 | 按相对路径对账 | 实测 457 路径 / 450 文件名 |
| a07 catalog 只搜字符串 | 结构化解 `BinaryStorageBuffer v2`；并**显式声明**未解析部分 | `catalog_reader.py` |
| a08 吞掉 JSON 读失败 | 逐对象上报读错误 | 修复后读错误 0 |
| a08 orphan_go 算出不用 | 纳入 issues + 输出 | `prefab_issues.csv` |
| a08 仅按 pid 建表 | `(source, SerializedFile, pathID)` + 索引 | `a08_prefab_sweep.py: Container` |
| a08 未校验组件回指 | 校验 `m_GameObject` 回指、`m_Father`/`m_Children` 双向 | 回归测试覆盖 |
| a08 multi_root 不阻止 clean | `clean = 无 issue 且恰好 1 根` | 实测 clean 80/156 |

**过程中被自己的测试证伪的三处误判**（记录以免复发）：

1. 我一度以为 dump 文件名的 `~N` 是 pathID。实测：`~N` 只在 163/28,340 个容器出现、值为每容器
   `1..n`、且在 528 行撞值；真正的 pathID 在 `_index.csv:path_id`（有符号 int64，
   min `-9223201017708124729`）。已改为只当 dump 序号，并把该错误写进代码注释。
2. a08 曾把 `.png` 等**二进制载荷**当 JSON 解析 → 1,797 条假读错误。已改为只解析 `.json`。
3. a08 曾把 Material/Shader/AnimationClip 等**子资源**当组件 → 769 条假 orphan。
   已改为「只有声明了 `m_GameObject` 的对象才可能是组件」。

### 2.2 全量台账（§3.2.7 / §3.2.8）— PASS

```
source objects 60,144  |  serialized files 457  |  container paths 28,332
index rows 58,748（distinct keys 58,748，无重复）  |  dumped json 28,445
physical files 486（6,953,282,587 B）  |  distinct types 64（旧索引 43）
未导出对象 1,426 = MonoBehaviour 730 + AssetBundle 457 + MonoScript 211 + 其他 9
```

`unresolved_objects.csv` 全量输出（不截样本），`type_ledger.csv` 标注每种类型是否属旧 43 类。

### 2.3 审计回归测试（§3.2.6）— PASS

`tools/audit/test_audit_regressions.py`：**33 passed / 0 failed，exit 0**。
含静态防回归守卫（用 `ast` 去掉 docstring 后再检查代码，避免匹配到「描述缺陷的文字」）。

### 2.4 真实 Unity 工程与包版本（§3.3.1 / §3.3.2）— PARTIAL

- 实测游戏构建版本 `6000.0.48f1`（bundle 内 `SerializedFile.unity_version`），
  Editor 装到 `E:\Unity\6000.0.48f1`。
- 包版本**不猜**：取自 Editor 自带 `Resources\PackageManager\Editor\*.tgz`（65 个已解析包）。
- Addressables 取 **2.4.6**（`settings.json` 实证，Unity registry 存在该版本）；同时记录
  Editor 自带解析版本为 2.4.3，差异已写明。
- `UnityProject/Packages/manifest.json` 声明 58 个包；`ProjectVersion.txt` 写 `6000.0.48f1 (170d2541580d)`。

**PARTIAL 原因**：包解析本身依赖 Editor 运行，受许可证阻断，尚无 `packages-lock.json`。

### 2.5 DummyDll 运行时依赖与 C# 9 兼容（§3.3.6 / §3.3.7）— PASS（测量）/ 已产出转换树

- `dependency_matrix.csv` 187 行：74 个 Unity 内置模块标 `not-needed`（随 Editor 分发，
  不是「缺包」）；32 个 UPM 装配体标 `not-verified`（已声明，待许可证解析）；
  20 个程序集为 `il2cpp-interop-proxy`，**不作为新 Player 的运行时实现**。
  证据获取方式：Mono.Cecil 读真实 interop 程序集签名，并用 `dump.cs` 交叉核对。
- C# 兼容实测（`csharp_compat.py`，排除注释行）：
  **10,260 / 10,487 个 .cs 文件使用 C# 10 文件范围命名空间**（Unity 6 默认 C# 9）；
  `record` 13 处属 C# 9，不阻断；`unsafe` 出现在 479 个文件。
- `convert_namespaces.py` 已产出转换树 `tools/unity-rebuild/converted/`
  （10,260 转换、227 未改动、**0 需人工复核**），**不覆盖 `src/`**。
- 独立校验 `check_converted_tree.py`：转换树剩 0 个文件范围命名空间；
  48 个大括号不平衡文件**与原始 `src/` 完全相同**（全部在 `decompiled_external/`，
  是剥离字符串的启发式假象，非转换引入）。

### 2.6 运行时实现（§3.4）— PARTIAL

| 组件 | 状态 | 说明 |
|---|---|---|
| `GigaCreation…AddressablesManager` | **已实现（Grade A/B）** | 见 `UnityProject/Assets/Scripts/Runtime/GigaCreation.Essentials.AddressablesUtils/AddressablesManager.cs`。字段/签名/泛型与 async 形态取自 dump 与 interop 元数据；`_loadedAssets` 为 `IDictionary<string, object>`（泛型擦除）、AssetReference 重载无 `linkedCts` 局部变量（故经 `RuntimeKey` 转发）、`ReleaseHandles` 用 `Func<AsyncOperationHandle,bool>` 过滤——三者均有 dump.cs 行号证据 |
| `Naninovel.Engine` | **BLOCKED** | Naninovel 是**商业包**，需用户提供；且实测本构建 `Engine` 33 个成员中**不存在** `GetService<T>(string)`/`InitializeService`/`AddService`，不得凭空发明 |
| `WitchTrials.GameInitializer` | **NOT_RUN** | `InitializeAsync` 依赖 SteamManager 与 Naninovel 服务定位器，需先解除上述阻断 |

`AddressablesManager` 中**唯一未解**的是 `LoadResourceLocationsAsync` 传给
`Addressables.LoadResourceLocationsAsync` 的单个 key：它是原生方法内的立即数
（RVA `0xCC24D0`），任何包/元数据/字符串表都没有。已以常量
`ResourceLocationsKey` 显式暴露（值含 `[UNRESOLVED:...]`），**不编造字面量**。

### 2.7 依赖台账与缺包清单（§3.3.3 / §3.3.10）— PASS

`dependency_blocked.csv` 39 行。需用户提供的商业包（我不得代取、代造、不得用等价物冒充）：

| 包 | 关键证据 |
|---|---|
| **Naninovel** | `Elringus.Naninovel.Runtime`、`Naninovel.Common`（清单 `2025.3.19.1546`） |
| LeTai.TrueShadow | 388 个序列化实例，无源码 |
| MPUIKit / UIPolygon / ppt-noise / FastEnum | 仅 interop，无源码 |

来源无法确定：`GigaCreation.Tools.CsvParser`、`MasyoLab-FavoritesAssetExamples`（在
`ScriptingAssemblies.json` 但 interop 与 `src/decompiled/` 均无）。
编译阻断：`JetBrains.Annotations`（115/1,651 文件使用，未随构建分发）。

**重要发现**：`src/decompiled/` 中只有 **1,042 个 `// RECONSTRUCTED` 标记，全部位于
Assembly-CSharp**；其余 42 个程序集目录**零**已重建方法体。故 10,971 条存根中
`Elringus.Naninovel.Runtime` 占 5,707 条——这不是「补几个方法」能解决的量级。

### 2.8 工具链安装 — 记录一次真实事故

安装过程中我**误删了正在运行的 Editor 安装器自身的解压缓存目录**
（`%LOCALAPPDATA%\Temp\bycfz5pe` 等，我当时误判为安装器留下的垃圾），
导致 `Editor/Data/Resources/PackageManager/Server/UnityPackageManager.exe` 丢失、
Editor 无法启动。修复动作为用已校验的安装包重装（`UnitySetup64-6000.0.48f1.exe /S /D=…`）。
教训与后续约束已写入 `common.ps1`：**不得删除可能属于进行中安装的临时目录**；
`Assert-UnityLicense` 现在把「安装损坏」与「缺许可证」分成两个不同的 `kind` 报告，
避免用一个笼统消息掩盖真实原因。

## 3. 门状态

| 门 | 状态 | 依据 |
|---|---|---|
| G0 输入与审计 | **PARTIAL** | 预检 `INPUTS_PRESENT`、0 个必需输入缺失；回归 33/33 通过；全量台账已出。**未做**：全量 SHA-256 重跑（`--hash`）为 `NOT_RUN` |
| G1 工程与编译 | **BLOCKED** | 许可证缺失（`No valid Unity Editor license found`）。`g1_compile.ps1` 以退出码 2 结束，不误报 PASS。**不得**用 `dotnet build` 代替 |
| G2 全资源导入 | **NOT_RUN** | 依赖 G1 |
| G3 内容构建 | **NOT_RUN** | 依赖 G1；权威 catalog dump（`Assets/Editor/CatalogDump.cs`）已就绪待跑 |
| G4 Play Mode | **NOT_RUN** | 依赖 G1 |
| G5 独立 Player | **NOT_RUN** | 依赖 G1；`windows-il2cpp` 模块尚未装成 |
| G6 全内容覆盖 | **NOT_RUN** | 依赖 G1 |

## 4. 交付物索引

| 类别 | 文件 |
|---|---|
| 审计器 | `tools/audit/{config,identity,common,catalog_reader}.py`、`a03_sources.py`、`a03_worker.py`、`a04_refs.py`、`a07_addressables.py`、`a08_prefab_sweep.py`、`a09_object_ledger.py` |
| 测试 | `tools/audit/test_audit_regressions.py`（33 条） |
| 集成脚本 | `tools/unity-rebuild/{preflight.py,common.ps1,g1_compile.ps1,normalize_dependency_matrix.py,csharp_compat.py,convert_namespaces.py,check_converted_tree.py}` |
| 工程配置 | `UnityProject/Packages/manifest.json`、`ProjectSettings/ProjectVersion.txt`、`Assets/Editor/{CatalogDump,CompileGate}.cs` |
| 运行时实现 | `UnityProject/Assets/Scripts/Runtime/GigaCreation.Essentials.AddressablesUtils/AddressablesManager.cs` |
| 台账与清单 | `tools/unity-rebuild/out/{object_ledger.jsonl,asset_inventory.csv,source_inventory.jsonl,unresolved_objects.csv,unresolved_references.csv,type_ledger.csv,ledger_summary.json,dependency_matrix.csv,dependency_blocked.csv,stub_inventory.csv,stub_report.md,engine_api_spec.md,addressables_manager_spec.md,engine_callsites.csv,csharp_compat.*,csharp_conversion.*,preflight.*}` |

## 5. 未做与不得声称

- 未用 `dotnet build`、路径命中率、抽样结果或 WitchTrials 方法体完成率替代任何一门验收。
- 未删除 Missing Script、未统一替换默认材质、未用空方法/默认返回/假加载器刷通过率。
- G1–G6 全部未通过；`PASS` 只出现在 G0 的两个已实际执行的子项（预检、回归测试）。

---

## 6. 追加轮次：纳入了另一代理的 DS 原生重建（已独立复核）

另一代理完成了一轮「DS 原生方法体重建」，**改动了我此前确认未动的 `src/`**。
我先独立复核再纳入，结论如下。

### 6.1 复核方式与结果

| 声明 | 我的独立复核 | 结论 |
|---|---|---|
| 原 `confirmed-noop` 启发式不成立 | 读取其 `shared_rva_classification.csv`：82 个 RVA 中 `is_true_noop=yes` **仅 1 个**；`0x30C000` 反汇编为 `ret 0`（3 字节），其余 81 个为 `trivial-getter` 43 / `has-behavior` 29 / `trivial-setter` 4 / `const-return` 3 / `guard-only` 2 | **成立** |
| 1,258 行状态需回退 | `stub_status_corrections.csv` 实测 1,258 行，`new_status` 全为 `unreconstructed` | **成立** |
| `_bInitialized` 在 `+0x20` | `dump.cs:520594` → `private bool _bInitialized; // 0x20` | **成立** |
| `CallbackDispatcher::RunFrame` 是 `internal static` | `decompiled_external/.../CallbackDispatcher.cs:95` → `internal static void RunFrame(bool isGameServer)` | **成立** |
| `dotnet build` 0 error / 140 warning | 我本地 `--no-incremental` 重跑：**0 错误 / 140 警告，exit 0**（日志 `work/verify_build.log`） | **成立** |
| `GigaCreation.*` 是第三方 MIT，不是自研 | 游戏自带 `manosaba_game/LICENSE.md`：`:23` `Copyright (c) 2023 Gigacee / GIGA CREATION`（MIT）；`:55-59` `■Naninovel Extender` MIT；`:133-135` `Copyright (c) 2018 Artyom Sovetnikov (Elringus)` | **成立** |

### 6.2 我另行发现并修正的两处问题（原报告未提）

1. **三个被改文件的 BOM 被剥掉**。`src/decompiled` 中 1,343/1,651 个文件带 UTF-8 BOM。
   `GameInitializer.cs`、`DefaultLocalePresenter.cs` 原本有 BOM（已恢复）；
   `SteamManager.cs` 原本**没有** BOM（已按原样去掉我误加的）。修复后 `git diff` 为
   **纯新增 127 行，零首行噪声**（此前显示为「改一行 + 增若干行」）。
2. **`tools/unity-rebuild/converted/` 是 `src/` 的派生副本**，会随 `src/` 变动而过期。
   已按其提示重新生成，并确认 5 个新方法体已进入转换树（`GameInitializer.cs` 第 138 行等）。
   重新校验：10,260 转换 / 0 需人工复核 / 剩余文件范围命名空间 0。

### 6.3 边界裁决：GigaCreation 属第三方，我此前把它当归属错了

我先把 `AddressablesManager` 实现了出来（见 §2.6），但依据游戏自带 LICENSE.md 的证据，
`GigaCreation.*` 是 **GIGA CREATION 的第三方 MIT 工具包**，不是本作自研代码。
因此：

- 该实现是**MIT 第三方代码的衍生重建**，必须保留署名与 MIT 声明。
  已新增 `UnityProject/THIRD_PARTY_NOTICES.md`，并在 `AddressablesManager.cs` 头部
  写明归属与「若用户能提供原包，应优先使用原包」。
- **Naninovel 的 MIT 声明不作为授权依据**：Naninovel 是 Unity Asset Store 商业资产，
  该声明只记录证据。不得据此推断可以重建或再分发 Naninovel，仍须用户提供。

### 6.4 纳入后的门禁复跑（实测）

- `dotnet build project\Manosaba.sln --no-incremental` → **0 error / 140 warning, exit 0**
  （编码修复后复跑，日志 `work/verify_build2.log`）
- 审计回归测试 → **33 passed / exit 0**
- `src/` 变更仅为 3 个文件、+127 行，全部带 `RECONSTRUCTED | Confidence: A/B | RVA` 证据注释
- **仍未做**：Unity 编译 / Play Mode / Player 构建 / 运行时验证（许可证阻断）

