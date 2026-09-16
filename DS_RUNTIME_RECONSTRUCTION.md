# DS 原生方法体重建进度 — Boot Runtime Gate

作者：DS runtime reconstruction agent（本文件由本次施工会话产出，非 Codex/Claude 审计署名）。
基线 commit：`5f2895a`（`master`，工作区 dirty）。
本轮范围：**仅 C# 方法体恢复**。未触碰 Unity 资源转换、Prefab/Scene、Addressables 工程配置、包安装、GUID/localFileID、Editor 自动化、资源导入。

---

## 0. 边界文件：先缺失、后被仓库作者补上（已复核）

任务书要求把 `UNITY_REBUILD_AGENT_BOUNDARIES.md` 当作最高优先级规则并「开始前先读取」。

**开工时该文件不存在**：全仓 `*BOUNDARIES*` / `*AGENT*.md` 搜索 0 命中。当时记录为 BLOCKED 输入，
预期路径 `E:\Manosaba-VSCodeReady\UNITY_REBUILD_AGENT_BOUNDARIES.md`，并改用两条既有依据执行边界判定：

1. 任务书正文 §1–§13 本身（routing 分类、A/B/C/D 证据纪律、禁止触碰清单）；
2. 仓库自带的、有外部证据支撑的边界文件 `RECONSTRUCTION.md` §0.5（`RECONSTRUCTION.md:38-101`）。

**该文件随后由仓库作者提交到 `main`**：`38b2ddc docs: define DS vs Claude Unity rebuild ownership`
（115 行，作者 Zis30axs，2026-09-16 13:03 +0800）。本节已按它复核，结论如下。

### 0.0 复核结论（对照 `UNITY_REBUILD_AGENT_BOUNDARIES.md`）

| 该文件条款 | 本轮做法 | 判定 |
|---|---|---|
| L7-18 DS 负责 `src/decompiled/**` 的批量/机械骨架工作 | 本轮正是在该目录做方法体恢复 | ✅ 属本 agent 职权 |
| L36-50 集成期 `src/decompiled/**` read-mostly、禁止 mass-edit | 只改 3 个文件、10 行真实代码、共 127 行插入（其余为证据注释），无任何批量模式 | ✅ 非 mass-edit |
| L65-80 stub 先分类再路由；禁止 bulk-fill | 10,971 行全部路由，0 UNKNOWN，未批量补 `return null/default` | ✅ |
| L80 Naninovel / `GigaCreation.*` / Boot/Init 先做 dependency·stub inventory；如需恢复方法体则交给 DS | Boot/Init 方法体由本 agent（DS）恢复；`GigaCreation.*`、Naninovel 已产出 inventory | ✅ |
| L84-95 blocker 需含 assembly/file/type/method/symptom/机械或语义/所需变换/证据/优先级/验收 | 已在 `ds_runtime_blockers.csv` 补上 4 个新列（见 §6） | ✅ 本轮补正 |
| L99-109 与旧审计提示冲突时以本文件为准 | 采用本文件判定，ownership 无冲突 | ✅ |

**两处据该文件做的调整：**

1. **routing 词表对齐**：该文件 L69-76 定义 `REAL_NOOP` / `ALREADY_RECONSTRUCTED` /
   `DS_RECONSTRUCTION_REQUIRED` / `INTEGRATION_SHIM_REQUIRED` / `BLOCKED_EXTERNAL_DEPENDENCY` / `UNKNOWN`，
   与任务书 §1 的 8 类名不同。`ds_runtime_blockers.csv` 现同时携带两套：原 `routing` 列（任务书词表）
   与新增 `boundaries_routing` 列（该文件词表）。
   映射：`EXTERNAL_PACKAGE_REQUIRED → BLOCKED_EXTERNAL_DEPENDENCY`；
   `EDITOR_OR_DEV_ONLY → REAL_NOOP`（该文件无 editor-only 类别，而这些成员在发布版 Player 中不执行，
   对运行时等价于无操作——**此映射是本 agent 的解释**，在此注明）。
2. **blocker 字段补全**：按 L84-95 增加 `mechanical_or_semantic`、`symptom`、
   `required_transformation`、`acceptance_test` 四列。

**一处需要你裁决的范围问题**：该文件 L80 的默认规则是「`GigaCreation.*` 若需恢复方法体则交给 DS」，
而本轮把全部 `GigaCreation.*` 路由为外部依赖（理由见 §0.1：LICENSE 证明其为第三方 MIT UnityTools，
上游源码可获得）。即本轮的处置**比该文件更保守**。若上游源码实际拿不到，8,684 行中的
Addressables 最小闭包（10 个方法，见 §5.2）可按该文件 L80 改派给 DS——需要明确指示。

### 0.1 与任务书 §3 的一处实质冲突（已按证据裁决）

任务书 §3 把 `GigaCreation.Essentials.AddressablesUtils` 当作「自研层」并要求恢复其最小闭包。
`RECONSTRUCTION.md:64-66` 用**游戏自己随正式版发布的 `manosaba_game/LICENSE.md`**（206 行 MIT 署名）证明：

> GIGA CREATION（含个人身份 Gigacee）是**第三方 Unity 工具作者**，不是 Manosaba 的开发商。
> `GigaCreation.Essentials.*` / `GigaCreation.Tools.*` 对应 LICENSE 条目 **UnityTools**（MIT），
> `GigaCreation.NaninovelExtender.*`（10 个子程序集）对应 **Naninovel Extender**（MIT）。

并用代码独立复核（`RECONSTRUCTION.md:74-78`）：全部 `GigaCreation.*` 源码里 grep
`trial/debate/witch/evidence` 等 Manosaba 专属词汇**零命中**，类名全是通用能力
（`ServiceLocator`、`MasterDataProvider`、`DialogManager`、`TweeningButton`、`CameraManagerExtended`）。

`RECONSTRUCTION.md:92-100` 的结论：恢复范围**仅限 `WitchTrials.*`（Assembly-CSharp）**；
引用 GigaCreation/Naninovel 的 API 时按其公开签名当黑盒使用；
唯一例外是「某个 GigaCreation/Naninovel 方法的原生行为与其公开文档/开源版本明显不一致（怀疑被魔改）」——
**目前没有发现任何此类证据**。

因此本轮把全部 `GigaCreation.*` 路由为 `EXTERNAL_PACKAGE_REQUIRED`（8,684 行中的绝大部分），
**没有对它做任何方法体恢复**。这不影响 Boot Gate：`GameInitializer::Awake` 与
`DefaultLocalePresenter::.ctor` 对 GigaCreation 的调用是**黑盒 API 调用**（`ServiceLocator.Register<T>`），
属于边界内的正常引用，已在 §4 恢复。

---

## 1. Task 1 — shared-RVA heuristic 重新验证（已完成，结论被推翻并修正）

### 1.1 方法

`tools/unity-rebuild/out/stub_inventory.csv` 原先用「同 RVA 被 ≥5 个成员共享 ⇒ `confirmed-noop`」。
本轮对该判定所依赖的**全部 82 个 RVA** 逐个反汇编一次（不是逐方法），
工具为新增的 `work/reversing/rva_classify.py`（复用既有 `work/reversing/il2cpp_pe.py` +
`il2cpp_db.py` 的函数边界，剔除 IL2CPP 样板与 `int3` 对齐填充）。

判定规则（把同 RVA 成员**批量继承**这一条实现证据，并注明是 folding 而非逐方法猜测）：

| native_verdict | 判定依据 | 含义 |
|---|---|---|
| `real-noop` | 去样板后只剩 `ret` | 原生确实为空 |
| `trivial-getter` | `mov <返回寄存器>,[rcx+off]; ret` | 返回一个实例字段 |
| `trivial-setter` | `mov [rcx+off],<实参>; ret` | 写入一个实例字段 |
| `const-return` | `xor eax,eax; ret` / `mov al,1; ret` | 返回常量 |
| `guard-only` | 仅 IL2CPP 类型初始化守卫 + 尾跳运行时 helper，无实例写、无托管调用 | 平凡构造/无副作用 |
| `has-behavior` | 有内存写入 / 托管调用 / 内联 NRE 抛出 | 非空实现 |

### 1.2 结果：82 个 RVA 中**只有 1 个**是真 no-op

| verdict | RVA 数 | 涉及 stub 行 |
|---|---:|---:|
| `real-noop` | **1** | 740 |
| `trivial-getter` | 43 | — |
| `trivial-setter` | 4 | — |
| `const-return` | 3 | — |
| `guard-only` | 2 | — |
| `has-behavior` | 29 | — |

唯一真 no-op：**`0x30C000`，3 字节，单条 `ret`，被 4,711 个托管方法共享**（`disas.py` 统计）。
逐指令输出：`work/reversing/rva_classify.py 0x30C000` → `ret 0`。

典型反例（原文引用，均来自本轮反汇编）：

- `0x33FBC0`（5 B）：`mov rax, qword ptr [rcx + 0x10]` / `ret` —— **返回实例字段的 getter**，不是空方法。
- `0x397FF0`（4 B）：`mov dword ptr [rcx + 0x10], edx` / `ret` —— **写入实参的 setter**。
- `0x42F6D0`（3 B）：`mov al, 1` / `ret` —— **返回 true 的常量方法**。
- `0x30C0D0`（82 B）：`mov qword ptr [rcx + 0x10], rdx` + GC 写屏障 —— 真构造（`RECONSTRUCTION.md` #30 记为
  `MapDataItem::.ctor` 0x30C0D0，82 B，两者一致）。
- `0x30D8F0`（67 B）：一次类型初始化守卫 + `jmp` 运行时 helper，**无实例写、无托管调用** ——
  即 MonoBehaviour 平凡构造（`RECONSTRUCTION.md:2606` 已独立记录过「0x30D8F0 尾跳 MonoBehaviour 构造」，
  与本轮反汇编一致）。

### 1.3 修正结果

`stub_inventory.csv` 的 `status` 列已就地修正：

| 旧值 | 新值 | 行数 |
|---|---|---:|
| `confirmed-noop` | `noop-verified-A`（RVA 0x30C000，反汇编确认） | 731 |
| `confirmed-noop` | `unreconstructed`（其余 81 个 RVA，原生确有行为） | 1,258 |

修正后分布：`unreconstructed` 9,680 / `noop-verified-A` 731 / `interface-or-abstract` 493 / `already-reconstructed` 67 = 10,971。

**即：原先把 1,258 行误标为「无需处理」，其中 107 行属于 Assembly-CSharp（DS 自有）。**
逐行修正记录：`tools/unity-rebuild/out/stub_status_corrections.csv`（1,258 行）。
RVA 级证据：`tools/unity-rebuild/out/shared_rva_classification.csv`（82 行，含 bytes / 指令数 / verdict / 前 6 条反汇编 / 受影响行数）。

> 诚实标注：`guard-only` 与 `trivial-*` 的判定基于**单次反汇编 + 去样板规则**，
> 属 Confidence A（原生行为完全确定）；但 `has-behavior` 只确定了「不是空方法」，
> **其具体方法体尚未恢复**。

---

## 2. Task 2 — Boot Gate 精确调用图

起点 `GameInitializer::Start`（RVA 0x30FB70，既有 Confidence A 复原，`GameInitializer.cs:132-138`）：

```
_steamManager.gameObject.SetActive(!DebugOptions.IsDebugMode || DebugOptions.EnableSteamworks);
InitializeAsync(destroyCancellationToken).Forget();      // fire-and-forget，Start 不 await
```

上游：Unity 消息 `Awake`（RVA 0x30FA80）→ 本轮已恢复（§4）。
下游（`InitializeAsync` RVA 0x30FE20 / `MoveNext` RVA 0x30FFE0，439 B + 状态机）：
状态机字段（dump.cs，`GameInitializer.cs:22-76`）点名了完整链路——
`<defaultLocalePresenter>5__2`、`<loadDefaultLocaleTask>5__3`、`<splashScreenTask>5__4`、
`<entrySequence>5__5`、`<>u__1`(UniTask.Awaiter)、`<>u__2`(UniTask<GameObject>.Awaiter)。

即 Boot → Game prefab → title 路径为：

| 序 | 方法 | RVA | 归属 | 状态 |
|---|---|---|---|---|
| 1 | `GameInitializer::Awake` | 0x30FA80 | DS | ✅ 本轮 A |
| 2 | `GameInitializer::Start` | 0x30FB70 | DS | ✅ 既有 A（🎮 已运行时验证） |
| 3 | `GameInitializer::InitializeAsync` | 0x30FE20 | DS | ⏳ GATE_A_BOOT 剩余 |
| 4 | `GameInitializer.<InitializeAsync>d__8::MoveNext` | 0x30FFE0 | DS | ⏳ GATE_A_BOOT 剩余（实际 await 逻辑在此） |
| 5 | `DefaultLocalePresenter::.ctor` | 0x38D0E0 | DS | ✅ 本轮 A |
| 6 | `DefaultLocalePresenter::LoadAsync` | 0x38D220 | DS | ⏳ 剩余 |
| 7 | `DefaultLocaleSelector::LoadAsync` | 0x398000 | DS | ⏳ 剩余 |
| 8 | `SplashScreen::Reset` | 0x38BD90 | DS | ⏳ 剩余 |
| 9 | `SplashScreen::PlaySequenceAsync` | 0x38BEC0 | DS | ⏳ 剩余 |
| 10 | `EntrySequence::PlayInitialSequenceAsync` | 0x38A4B0 | DS | ⏳ 剩余 |
| 11 | `EntrySequence::EnterTitleScreenAsync` | 0x38A8A0 | DS | ⏳ 剩余 |
| 12 | `SteamManager::Awake` | 0x3891E0 | DS | ⏳ 剩余（1,446 B / 320 指令，最难） |
| 13 | `SteamManager::Update` | 0x389F80 | DS | ✅ 本轮 B |
| 14 | `SteamManager::InitOnPlayMode` | 0x389130 | DS | ⏳ 剩余 |
| 15 | `SteamManager::SteamAPIDebugTextHook` | 0x3890E0 | DS | ⏳ 剩余 |
| 16 | `EngineInitializationUiModified::OnEnable` | 0x389FE0 | DS | ⏳ 剩余（虚表槽 0x3c8 待解析） |
| — | Naninovel 外部方法 | — | 外部包 | `EXTERNAL_PACKAGE_REQUIRED` |
| — | Unity API（`Camera.main`、`MonoBehaviour.destroyCancellationToken`、`SetActive`） | — | Unity | `UNITY_PACKAGE_REQUIRED`（无需处理） |
| — | 第三方 API（`Steamworks.SteamAPI`、`ServiceLocator`） | — | 第三方/外部包 | 黑盒调用，`EXTERNAL_PACKAGE_REQUIRED` |
| — | C/D blocker | — | — | 见 §5.2 |

---

## 3. Task 3 — routing（全部 10,971 个候选，0 个 UNKNOWN）

机器可读结果：`tools/unity-rebuild/out/ds_runtime_blockers.csv`
（列：`assembly,file,type,method,rva,gate,routing,confidence,status,commit,evidence,notes`）

| routing | 行数 | 说明 |
|---|---:|---|
| `EXTERNAL_PACKAGE_REQUIRED` | 8,684 | Naninovel 1,073+281、GigaCreation.*、SpriteDicing、ppt-noise |
| `REAL_NOOP` | 1,281 | 731 反汇编确认 `ret` + 493 接口/抽象（无原生体）+ 57 平凡构造 |
| `EDITOR_OR_DEV_ONLY` | 550 | `CustomInspector*`（`[Conditional("UNITY_EDITOR")]`）、`GigaCreation.Tools.Debugging.*` |
| `DS_RECONSTRUCTION_REQUIRED` | 350 | **本 agent 队列**：GATE_A_BOOT 14 + GATE_B_OR_C 336 |
| `ALREADY_RECONSTRUCTED` | 72 | 67 既有 + 5 本轮 |
| `INTEGRATION_SHIM_REQUIRED` | 34 | `UnitySourceGeneratedAssemblyMonoScriptTypes_v1`（Unity 构建生成） |
| `UNKNOWN` | **0** | — |

`gate` 分布：`GATE_A_BOOT` 19、`GATE_B_OR_C` 336、`n/a` 10,616。

**没有批量修补任何 `return null/default`；没有按函数名猜行为。**
350 个 DS 行中，336 行被显式推迟到 GATE_B_OR_C，理由记录在 `notes` 列：
不在 Boot Gate 调用图上，按边界 §9「没有 runtime evidence / 没有调用到的 stub 不因看起来重要自动提优先级」。

---

## 4. Task 3b — 本轮恢复的方法体（逐方法记录，§10 格式）

### 4.1 `GameInitializer::Awake` — Confidence A

- **Method**: `Awake`　**Assembly**: Assembly-CSharp
- **File**: `src/decompiled/Assembly-CSharp/WitchTrials.Views/GameInitializer.cs`（RVA 0x30FA80，227 B，51 指令）
- **Gate**: GATE_A_BOOT　**Caller**: Unity 消息（Boot 场景）
- **Native evidence**:
  - `0x30FAD7 call il2cpp_codegen_object_new`（`AddressablesManager_TypeInfo`）→ `0x30FAE4 call -> AddressablesManager$$.ctor`（无参）
  - `0x30FAF0 mov [rdi+0x40], rbx` → `_addressablesManager`（dump.cs 字段 +0x40），带 GC 写屏障 → 引用字段存
  - `0x30FB38` 重载该字段 → `0x30FB4A rdx = METHODINFO ServiceLocator.Register<IAddressablesManager>()`
    → `0x30FB5E jmp -> ServiceLocator$$Register<object>`（rcx = 字段）
  - `ServiceLocator.Register<TService>(TService service)`（`ServiceLocator.cs:62`）是唯一匹配重载 ⇒ 字段作实参
- **Recovered behavior**:
  ```csharp
  _addressablesManager = new AddressablesManager();
  GigaCreation.Tools.Service.ServiceLocator.Register<IAddressablesManager>(_addressablesManager);
  ```
- **Uncertain details**: 无（调用目标、字段偏移、分支、常量均明确）
- **Runtime significance**: 启动链第一环；`Start()` 依赖 Addressables 前端已注册。

### 4.2 `GameInitializer::OnDestroy` — Confidence A

- **RVA**: 0x30FDB0（104 B）　**Gate**: GATE_A_BOOT
- **Native evidence**: `0x30FDF2 rbx = [rbx+0x40]` → `0x30FE04 rdx = METHODINFO ServiceLocator.Unregister<IAddressablesManager>()`
  → `0x30FE0B rcx = rbx` → `0x30FE13 jmp -> ServiceLocator$$Unregister<object>`
  `Unregister<TService>(TService service)`（`ServiceLocator.cs:67`）⇒ **不是无参 unregister**
- **Recovered behavior**: `GigaCreation.Tools.Service.ServiceLocator.Unregister<IAddressablesManager>(_addressablesManager);`
- **Uncertain details**: 无　**Significance**: Awake 的对称拆卸；DontDestroyOnLoad 语义下防重复注册。

### 4.3 `DefaultLocalePresenter::.ctor` — Confidence A

- **RVA**: 0x38D0E0（206 B）　**Gate**: GATE_A_BOOT　**Caller**: `GameInitializer.InitializeAsync`
- **Native evidence**: `0x38D133 call il2cpp_codegen_object_new`（`DefaultLocaleSelector_TypeInfo`）
  → `0x38D13F mov [rbx+0x10], rax`（dump.cs：唯一实例字段 `readonly DefaultLocaleSelector _defaultLocaleSelector`，+0x10）
  → `0x38D188` 重载 → `0x38D19A rdx = METHODINFO ServiceLocator.Register<IDefaultLocaleSelector>()`
  → `0x38D1A9 jmp -> ServiceLocator$$Register<object>`
- **Recovered behavior**:
  ```csharp
  _defaultLocaleSelector = new DefaultLocaleSelector();
  GigaCreation.Tools.Service.ServiceLocator.Register<IDefaultLocaleSelector>(_defaultLocaleSelector);
  ```
- **Folded implementation**: 与 `GameInitializer::Awake` 同构（同一 `Register` 目标 0x180752130，字段偏移 0x10 vs 0x40）——
  这是**实现证据的批量继承**，不是按名字猜。
- **Uncertain details**: 无　**Significance**: locale 服务注册，`InitializeAsync` 第 5 步依赖。

### 4.4 `DefaultLocalePresenter::Dispose` — Confidence A

- **RVA**: 0x38D1B0（104 B）　**Native evidence**: `0x38D1F2 rbx = [rbx+0x10]` → METHODINFO `Unregister<IDefaultLocaleSelector>` → `0x38D213 jmp -> ServiceLocator$$Unregister<object>`（与 `GameInitializer::OnDestroy` 同一 0x180752790 目标）
- **Recovered behavior**: `GigaCreation.Tools.Service.ServiceLocator.Unregister<IDefaultLocaleSelector>(_defaultLocaleSelector);`

### 4.5 `SteamManager::Update` — Confidence B

- **RVA**: 0x389F80（82 B）　**Gate**: GATE_A_BOOT
- **Native evidence**:
  - `0x389F84 cmp byte ptr [rcx+0x20],0` → `_bInitialized`（dump.cs 实例字段 +0x20）
  - `0x389F88 je -> 0x180389FCD`（共享 `add rsp,0x28; ret` 尾巴）⇒ 未初始化即返回
  - `0x389FC8 jmp -> Steamworks.CallbackDispatcher$$RunFrame`
- **Recovered behavior**:
  ```csharp
  if (!_bInitialized) return;
  SteamAPI.RunCallbacks();
  ```
- **Confidence B 的理由（必须注明）**: 调用目标字面上是 `CallbackDispatcher.RunFrame`，
  但它是 `internal static`（`CallbackDispatcher.cs:95`），Assembly-CSharp 无法命名它。
  公开转发函数 `Steamworks.SteamAPI::RunCallbacks`（RVA 0x432EF0）的原生体与本尾巴**逐指令同构**：
  同一个一次性守卫变量 `data_30DB68D`、同一个 `CallbackDispatcher` 分派解析、
  同样的 `xor edx,edx / xor ecx,ecx / jmp RunFrame`。⇒ 唯一可编译且控制流一致的写法是内联后的 `RunCallbacks()`。
- **等价点**: `SteamAPI.RunCallbacks()` ≡ `CallbackDispatcher.RunFrame(isGameServer: false)`
- **Uncertain details**: 原作者写的是 `RunCallbacks()` 还是等价的公开包装器；运行语义两者相同。
- **Runtime significance**: Steam 回调泵；不跑则 Steam 回调/成就/overlay 全部失效（不阻断进标题）。

### 4.6 顺带修正的 4 个「假 DS 任务」

`EntrySequence::.ctor`、`GameInitializer::.ctor`、`SplashScreen::.ctor`、`SteamManager::.ctor`
全部指向共享体 `0x30D8F0`，反汇编判定 `guard-only`（无实例写、无托管调用）⇒
**平凡无参构造，「骨架里的空体本来就是对的」**，路由为 `REAL_NOOP`，不再计入 DS 队列。
（这与 `RECONSTRUCTION.md:2606` 的独立结论一致。）

---

## 5. Task 4 — 门禁状态

### 5.1 编译与进度（已实测）

| 项目 | 结果 |
|---|---|
| `dotnet build project\Manosaba.sln --no-incremental` | **0 error / 140 warning**（与既有基线一致，未新增） |
| 日志 | `work/ds_build_1.log`（恢复前）、`work/ds_build_2.log`（恢复后） |
| `work/reversing/progress.py` | **Boot/Init 12/32 函数体、4,216/10,899 B**（恢复前 7/32） |
| 全部 `WitchTrials.*` | **908/929 函数体、309,675/316,778 B**（恢复前 903/929、308,952 B） |
| 字节增量核对 | +723 B = 227+104+206+104+82（5 个方法）——**与 progress.py 完全吻合** |
| 审计基线（4 处） | **NOT RE-RUN**：`RECONSTRUCTION.md:2601` 说明该基线来自 scratchpad 脚本 `stub_audit.py`，**该脚本不在仓库中**（按 4 个命令类名全仓搜索为 0 命中）。需要该脚本才能复现基线。 |

**未做且不得宣称的**：Unity 编译、Play Mode、独立 Player 构建、任何运行时验证。
`dotnet build` 的 0 error **不等于** Unity Runtime PASS。

### 5.2 状态块（任务书 §11 要求格式）

```
Boot Gate:
  required:        32  （progress.py 的 Boot/Init 桶口径）
  reconstructed:   12  （7 既有 + 5 本轮：A×4、B×1）
  external:        0   （Boot 链上的 Naninovel 调用不计入 DS 队列）
  C/D blocked:     0
  本 agent 7 类型命名集口径：23 个成员 = 5 已恢复 + 4 REAL_NOOP(平凡构造) + 14 GATE_A_BOOT 待办

Addressables Gate:
  required:        10  （GigaCreation.Essentials.AddressablesUtils 中被 Boot 链调用的最小闭包）
  reconstructed:   0
  external:        10  （全部路由 EXTERNAL_PACKAGE_REQUIRED —— 见 §0.1）
  C/D blocked:     0
  说明：该程序集属于第三方 MIT「UnityTools」，不是 DS 自有代码；本 agent 未做任何恢复。

Next runtime blockers:
  - GameInitializer::InitializeAsync + <InitializeAsync>d__8::MoveNext（0x30FE20 / 0x30FFE0）
  - DefaultLocalePresenter::LoadAsync / DefaultLocaleSelector::LoadAsync（0x38D220 / 0x398000）
  - SplashScreen::Reset / PlaySequenceAsync（0x38BD90 / 0x38BEC0）
  - EntrySequence::PlayInitialSequenceAsync / EnterTitleScreenAsync（0x38A4B0 / 0x38A8A0）
  - SteamManager::Awake / InitOnPlayMode / SteamAPIDebugTextHook / Instance / Initialized
    （0x3891E0 / 0x389130 / 0x3890E0 / 0x388D80 / 0x388F30）
  - EngineInitializationUiModified::OnEnable（0x389FE0；虚表槽 0x3c8 待解析）
```

### 5.3 未完成 / 需要额外证据（不允许写进方法体的部分）

1. **`SteamManager::Instance` 与 `Initialized` 两个 getter**（0x388D80 297 B、0x388F30 293 B）：
   反汇编显示它们读静态存储 `[rax+0xb8]` 后再取成员并做分派解析，属于「静态单例 + 成员读取」形状，
   但**具体表达式（`s_instance` 直返 vs null 检查三目）尚未逐分支解析**，暂不落笔。
2. **`EngineInitializationUiModified::OnEnable`（0x389FE0）**：`0x389FF9 mov r9,[r8+0x3c8]` 是一次
   **虚表槽 0x3c8 / 8 = 槽 121** 的虚调用（实参 `Camera.main`），随后尾跳 `base.OnEnable()`
   （`Naninovel.UI.EngineInitializationUI$$OnEnable`）。要命名这个虚方法必须先解析该类型虚表布局，
   本轮**未做**，因此不猜。
3. **`SteamManager::Awake`（0x3891E0，1,446 B / 320 指令）**：Boot 链上最大的一块，本轮未开工。
4. 审计基线 4 处无法复现（缺 `stub_audit.py`，见 §5.1）。

---

## 6. 本轮产出文件

| 文件 | 内容 |
|---|---|
| `tools/unity-rebuild/out/ds_runtime_blockers.csv` | **10,971 行** routing/门禁台账（任务书 §11 + `UNITY_REBUILD_AGENT_BOUNDARIES.md` §Output expected 的字段：`assembly,file,type,method,rva,gate,routing,boundaries_routing,mechanical_or_semantic,confidence,status,symptom,required_transformation,acceptance_test,commit,evidence,notes`） |
| `tools/unity-rebuild/out/shared_rva_classification.csv` | 82 个共享 RVA 的反汇编判定（新增，Task 1 证据） |
| `tools/unity-rebuild/out/stub_status_corrections.csv` | 1,258 行 `confirmed-noop → unreconstructed` 逐行修正记录（新增） |
| `tools/unity-rebuild/out/stub_inventory.csv` | `status` 列已修正（原地更新） |
| `work/reversing/rva_classify.py` | 新的 RVA 分类器（新增；复用既有 PE/symdb 工具） |
| `work/ds_build_1.log` / `work/ds_build_2.log` | 恢复前 / 恢复后的全量构建日志 |
| `src/decompiled/Assembly-CSharp/WitchTrials.Views/GameInitializer.cs` | `Awake`、`OnDestroy` 方法体 + 证据注释 |
| `src/decompiled/Assembly-CSharp/WitchTrials.Presenters/DefaultLocalePresenter.cs` | 构造函数、`Dispose` 方法体 + 证据注释（+1 个 using） |
| `src/decompiled/Assembly-CSharp/WitchTrials.Views/SteamManager.cs` | `Update` 方法体 + 证据注释 |

**未触碰**：`UnityProject/`、`tools/unity-rebuild/converted/`、任何资源文件、任何 `.csproj`、任何 GigaCreation/Naninovel 源码。

> 移交提示：本轮只改了 `src/decompiled/`。Unity integration agent 的
> `tools/unity-rebuild/converted/` 是从 `src/` 派生的副本，需要重新生成才能看到这 5 个方法体。

---

## 7. 沿调用图继续扩展的边界（本轮遵守情况）

- 只加入了 **Boot → Game prefab → title screen** 路径必需的方法（§2 表格）。
- 没有为了「Boot/Init 百分比」去补 320 指令的 `SteamManager::Awake` 之外的旁支。
- 没有进入 8,006 个 Naninovel stub。
- 没有因为「看起来重要」给 Map/Debate/Trial/WitchBook 等已 100% 完成的子系统加任务。
- 停下来等待 Unity integration agent 用真实 Unity compile / Play Mode 给出下一批 blocker。
