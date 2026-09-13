# 方法体恢复工程 — 第一阶段报告

目标：把当前"类型系统完整的可编译骨架"推进为"基于原生程序行为重建的等价高级语言表示"。

**证据优先于完整度。** 本文件里的每一条结论都标注了证据等级；C/D 级不会被写进源码。

---

## 0. TL;DR

| 项目 | 结果 |
|---|---|
| 静态分析通道 | 已打通：`GameAssembly.dll` → capstone → 带语义标注的反汇编（见 `work/reversing/`） |
| 运行时验证通道 | 已打通：BepInEx 6（Il2CppInterop）+ Harmony，能挂已复原方法读真实字段值（见 `work/reversing/README.md`）——**不是**免费反编译，interop 代理只是参数封送壳 |
| 二进制身份 | 已校验，并排除了一个同名的 demo 版误配风险 |
| 游戏自身代码规模 | `WitchTrials.*` 共 **224 个类型 / 1212 个方法**，其中 **929 个是独立函数体** |
| 试点子系统 | `TrialStateManager`（TrialState）— **已完成**，11/12 个成员达到 Confidence A |
| 编译状态 | `dotnet build Manosaba.sln` = **0 error / 0 warning** |

---

## 1. 关键校准发现（都会影响后续判断）

### 1.1 必须用哪个二进制

机器上有两个 `manosaba` 的 `GameAssembly.dll`：

| 路径 | PE timestamp | 是否匹配本工程 dump |
|---|---|---|
| `E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\` | `0x68B45C56` | ✅ **匹配** |
| `E:\SteamLibrary\steamapps\common\manosaba_demo\` | `0x684C4689` | ❌ 不匹配（另一个构建） |

校验方式：dump.cs 里的 `RVA 0x33A4E0 / Offset 0x338EE0`，用 PE 节表换算。
正式版算出 `0x338EE0`（吻合），demo 版算出 `0x3390E0`（不吻合）。
**用错二进制会让每一个 RVA 都落到错误的函数上，且不会报错。**

### 1.2 `RVA 0x30C000` 是共享空函数体

反汇编确认它就是 `c2 00 00` = `ret 0`。

所以 dump 里所有 `[Address(RVA = "0x30C000")]` 的方法**都是真的空方法**——
可以直接判 Confidence A，不需要任何进一步分析，也**不应该**被排进恢复队列。
`WitchTrials.*` 里有 10 个这样的方法。

### 1.3 IL2CPP 会合并相同的函数体（body folding）

内容相同的平凡访问器被编译成一份代码。例如 `0x33A4E0` 一个地址被 **68 个**不同的
getter 共用（`ClueData.get_Items`、`MapData.get_Items`、`AuthorDataItem.get_Id` …）。

两个后果：

* RVA → 托管方法是**一对多**。用 RVA 当字典键会静默丢方法。
* 朴素的调用入度会被严重放大，让排序被一堆一行 getter 占满（第一版排序就踩了这个坑，已修正）。

好消息是：这 205 个折叠的访问器可以**一次反汇编、批量定案**——
一条 `mov rax,[rcx+0x18]; ret` 就同时确定了共用它的所有 getter。

---

## 2. 工具链

`work/reversing/`，纯 Python + capstone，不需要 IDA / Ghidra。用法见该目录的 `README.md`。

能力：把 `script.json` 的四张表（`ScriptMethod` / `ScriptString` / `ScriptMetadata` /
`ScriptMetadataMethod`）全部接进反汇编输出，所以几乎每一条 rip 相对引用都能还原成真名：

```
0x18039D44E  mov  rdx, [rip + 0x2ab45e3]   ; METHODINFO Method$ServiceLocator.Get<IPerspectiveStageManager>()
0x18039D455  mov  cl, 1                    ; includeDerivedType: true
0x18039D457  call 0x180751910              ; -> GigaCreation.Tools.Service.ServiceLocator$$Get<object>
0x18039D46A  mov  [rdi + 0x10], rax        ; rdi->TrialStateManager._stageManager
```

字段偏移用**访问宽度**过滤，避免把 `[rax+0x10]` 错配到当前 `this` 的字段上
（这是最容易产生伪代码的地方：寄存器里往往是别的对象）。

---

## 3. 规模

`WitchTrials.*`（游戏自身代码，不含 Naninovel 中间件）：

| 子系统 | 独立函数体 | 原生代码量 |
|---|---|---|
| UI | 165 | 50,436 B |
| Other | 162 | 56,145 B |
| WitchBook/Data | 121 | 39,146 B |
| Debate/Court | 82 | 32,044 B |
| Credits/Gallery | 71 | 20,747 B |
| Naninovel-glue | 66 | 21,262 B |
| PerspectiveStage | 65 | 27,784 B |
| Debug | 46 | 20,437 B |
| Boot/Init | 32 | 10,899 B |
| SaveLoad | 31 | 10,941 B |
| Map | 27 | 9,894 B |
| Input | 25 | 5,482 B |
| TrialState | 18 | 6,293 B |
| Audio | 18 | 5,268 B |

难度分布：trivial 233 / easy 540 / medium 374 / hard 53 / very hard 2。

**Naninovel（789 个类型）暂不作为恢复目标**——那是有公开文档的商业中间件，
它的已知语义更适合当作交叉验证的旁证（B 级证据），而不是恢复对象。

---

## 4. 第一批值得恢复的 44 个方法

按子系统分组，取每组价值最高的若干个。
"被调用"= 调用图入度；"虚/接口"= 需要靠 metadata 解析的间接调用点数（难度的主要来源）。
**RVA 后带 ✅ 的已经恢复并通过编译**（#1、12、15、18、42、44——都是"惰性缓存的服务查找"
这一种模式：`_field ??= Engine.GetServiceOrErr<T>()` / `ServiceLocator.Get<T>(true)` /
`NaniUiManager.GetUI<T>()`，详见对应源文件里的 `// RECONSTRUCTED` 注释）。
**带 🎮 的额外经过运行时验证**（#6，游戏启动入口，见下方第 7 节）。

| # | 系统 | 类型::方法 | RVA | 大小 | 分支 | 被调用 | 调用游戏方法 | 字符串 | 虚/接口 | 难度 |
|---|---|---|---|---|---|---|---|---|---|---|
| 1 | Boot/Init | `DefaultLocaleSelector::set_AvailableTextLanguages` | `0x36C200` ✅ | 82 | 2 | 41 | 0 | 0 | 0 | easy |
| 2 | Boot/Init | `SteamManager::Awake` | `0x3891E0` | 1446 | 33 | 0 | 0 | 5 | 1 | hard |
| 3 | Boot/Init | `SteamManager::OnEnable` | `0x389790` | 841 | 20 | 0 | 0 | 0 | 1 | medium |
| 4 | Boot/Init | `SteamManager::OnDestroy` | `0x389AE0` | 1177 | 19 | 0 | 0 | 0 | 0 | hard |
| 5 | Boot/Init | `EntrySequence::ResetDebugVariables` | `0x38A660` ✅ | 571 | 8 | 2 | 1 | 5 | 1 | medium |
| 6 | Boot/Init | `GameInitializer::Start` | `0x30FB70` ✅🎮 | 560 | 11 | 0 | 0 | 0 | 1 | medium |
| 7 | TrialState | `TrialStateManager::RegisterCameraState` | `0x39DA50` ✅ | 1323 | 30 | 0 | 0 | 1 | 5 | hard |
| 8 | TrialState | `WitchTrialsNaninovelManager::ChangeTrialPauseMode` | `0x399BE0` | 626 | 21 | 0 | 1 | 1 | 1 | hard |
| 9 | TrialState | `TrialStateManager::SaveServiceState` | `0x39D5C0` ✅ | 746 | 14 | 0 | 0 | 2 | 4 | medium |
| 10 | TrialState | `TrialStateManager::InitializeService` | `0x39D3A0` ✅🎮 | 544 | 9 | 0 | 0 | 0 | 3 | medium |
| 11 | TrialState | `TrialStateManager::RegisterScreenSplitState` | `0x39E120` ✅ | 500 | 14 | 0 | 0 | 0 | 0 | medium |
| 12 | TrialState | `StageScreenController::get_TrialStateManager` | `0x387A50` ✅ | 186 | 5 | 2 | 0 | 0 | 1 | easy |
| 13 | SaveLoad | `SaveLoadUi::Awake` | `0x359AB0` | 1073 | 18 | 0 | 0 | 1 | 4 | hard |
| 14 | SaveLoad | `SaveLoadUi::FindFocusObject` | `0x35B0F0` | 1479 | 47 | 0 | 0 | 0 | 3 | hard |
| 15 | SaveLoad | `SaveLoadUi::get_NaniStateManager` | `0x3597E0` ✅ | 202 | 5 | 6 | 0 | 0 | 1 | easy |
| 16 | SaveLoad | `SaveLoadUi::OnEndToDismiss` | `0x35AE50` ✅ | 570 | 12 | 0 | 0 | 0 | 6 | medium |
| 17 | SaveLoad | `SaveLoadUi::OnShow` | `0x35ABB0` ✅ | 508 | 6 | 0 | 0 | 0 | 7 | medium |
| 18 | SaveLoad | `ControlPanel::get_SaveLoadUi` | `0x337590` ✅ | 191 | 5 | 2 | 1 | 0 | 1 | easy |
| 19 | Naninovel-glue | `ScriptHelper::TryParseScenarioName` | `0x39B040` | 1446 | 38 | 3 | 0 | 1 | 3 | hard |
| 20 | Naninovel-glue | `GosubResetWitchBook::TryGetScriptPathAndLabel` | `0x3286B0` | 1643 | 40 | 0 | 0 | 3 | 4 | hard |
| 21 | Naninovel-glue | `WitchTrialsNaninovelManager::StartTrial` | `0x39A1D0` | 574 | 6 | 0 | 3 | 4 | 2 | medium |
| 22 | Naninovel-glue | `GosubResetWitchBook::CheckEvidenceState` | `0x328D20` | 1103 | 17 | 1 | 0 | 0 | 8 | hard |
| 23 | Naninovel-glue | `GosubToObjectionCutIn::Execute` | `0x325750` | 736 | 7 | 0 | 0 | 5 | 5 | medium |
| 24 | Naninovel-glue | `WitchTrialsNaninovelPresenter::Awake` | `0x38EE30` ✅ | 227 | 4 | 0 | 1 | 0 | 1 | easy |
| 25 | PerspectiveStage | `GosubToModifyPerspectiveStageCamera::Execute` | `0x31D870` | 2520 | 48 | 0 | 0 | 11 | 8 | hard |
| 26 | PerspectiveStage | `GosubToSplitPerspectiveStageScreens::Execute` | `0x31E250` | 2338 | 44 | 0 | 0 | 10 | 7 | hard |
| 27 | PerspectiveStage | `PerspectiveStagePresenter::Awake` | `0x38D8D0` | 1558 | 33 | 0 | 1 | 0 | 5 | hard |
| 28 | PerspectiveStage | `StageCameraController::Awake` | `0x3846C0` | 840 | 24 | 0 | 0 | 0 | 0 | hard |
| 29 | WitchBook/Data | `ClueDataItem::.ctor` | `0x36C0D0` ✅ | 170 | 4 | 72 | 0 | 0 | 0 | easy |
| 30 | WitchBook/Data | `MapDataItem::.ctor` | `0x30C0D0` ✅ | 82 | 2 | 56 | 0 | 0 | 0 | easy |
| 31 | WitchBook/Data | `WitchBookScreen::InitializePages` | `0x37A880` | 1408 | 31 | 0 | 5 | 0 | 5 | hard |
| 32 | WitchBook/Data | `WitchBookScreen::get_PageData` | `0x3799B0` ✅ | 807 | 15 | 14 | 0 | 0 | 3 | medium |
| 33 | Debate/Court | `DebateTextPrinterPanel::AddText` | `0x367E20` | 1322 | 36 | 1 | 0 | 3 | 5 | hard |
| 34 | Debate/Court | `DebateUi::OnVisibilityChangedByCustomUi` | `0x36A100` | 1435 | 19 | 1 | 2 | 0 | 10 | hard |
| 35 | Debate/Court | `PrintDebateText::ModifyDebatePrinter` | `0x322B40` | 1590 | 34 | 1 | 0 | 1 | 7 | hard |
| 36 | Debate/Court | `DebateUi::TimeSpanToText` | `0x36A900` | 1156 | 29 | 1 | 1 | 3 | 1 | hard |
| 37 | Map | `ChoiceMapArea::GetRestrictionAreas` | `0x327030` | 1029 | 25 | 1 | 1 | 1 | 8 | hard |
| 38 | Map | `MapPage::InitializeAreas` | `0x36FB30` | 1092 | 21 | 1 | 0 | 1 | 6 | hard |
| 39 | Map | `MapFloor::Show` | `0x36F100` | 784 | 21 | 1 | 1 | 0 | 2 | hard |
| 40 | Map | `MapFloor::get_Areas` | `0x36EC40` ✅ | 791 | 15 | 2 | 0 | 0 | 3 | medium |
| 41 | Input | `DebugTextPreferenceNaninovelInputState::BuildText` | `0x3B43B0` | 899 | 22 | 0 | 1 | 7 | 0 | hard |
| 42 | Input | `OptionsUi::get_InputManager` | `0x352860` ✅ | 202 | 5 | 13 | 0 | 0 | 1 | easy |
| 43 | Input | `WitchTrialsInputPresenter::Awake` | `0x38EC50` ✅ | 368 | 7 | 0 | 0 | 0 | 2 | medium |
| 44 | Input | `DebugCommandButtons::get_InputManager` | `0x3A7BD0` ✅ | 186 | 5 | 7 | 0 | 0 | 1 | easy |

完整排序在 `work/reversing/ranked.json`（929 条）。

---

## 5. 试点：`TrialStateManager` — 已完成

选它的理由：是最小的完整子系统（18 个方法 / 6,293 B），但覆盖了后面每一种机制——
字段读写、接口分派、LINQ 内联、闭包/lambda、嵌套类、async 状态机。

结果写在 `src/decompiled/Assembly-CSharp/WitchTrials.Models/TrialStateManager.cs`，
每个成员都带 `// RECONSTRUCTED | Confidence | RVA` 和关键证据行。

| 成员 | RVA | 结论 | 等级 |
|---|---|---|---|
| `RegisterCurrentStage` | `0x39DA30` | `_currentStageKind = kind; _isLoaded = true;` | **A** |
| `UnregisterCurrentStage` | `0x39DA40` | `_currentStageKind = default; _isLoaded = false;` | **A** |
| `InitializeService` | `0x39D3A0` | 取 `IPerspectiveStageManager`，建两个 List，返回空 UniTask | **A + 运行时确认** |
| `SaveServiceState` | `0x39D5C0` | 打包 `GameState` 四个字段 → `stateMap.SetState` | **A** |
| `RegisterScreenVisibilityState` | `0x39DF80` | 补齐列表长度后写 `Visible` | **A** |
| `RegisterScreenSplitState` | `0x39E120` | 补齐后写三个字段（每次重新取元素） | **A** |
| `RegisterCameraState` | `0x39DA50` | 补齐 + 三个 `Nullable` 分支 + `Range(0,3).Select(λ).ToArray()` | **A**（`??` 写法为 B） |
| `<RegisterCameraState>b__0` | `0x39E330` | `rotation.ElementAtOrDefault(i) ?? _cameraStates[index].Rotation[i]` | **A** |
| `ScreenState..ctor` | `0x39E320` | 字段初始值 `Visible = true; Direction = Right;` | **A** |
| `ResetService` / `DestroyService` / 三个 `.ctor` | `0x30C000` | 真空方法 | **A** |
| `LoadServiceState` | `0x39D8B0` | 仅外壳（async trampoline）已定案 | A（外壳） |
| `<LoadServiceState>d__9.MoveNext` | `0x39E510` | **未恢复** | D |

### 几个有代表性的恢复片段

`RegisterCurrentStage` 的原生体只有三条指令，字段偏移与 C# 侧完全吻合：

```
mov dword ptr [rcx+0x18], edx   ; _currentStageKind (+0x18) = kind
mov byte  ptr [rcx+0x1C], 1     ; _isLoaded         (+0x1C) = true
ret
```

`ScreenState` 的构造函数暴露了 C# 字段初始值——这类信息在托管骨架里是看不到的：

```
mov byte  ptr [rcx+0x10], 1     ; Visible   = true
mov dword ptr [rcx+0x1C], 1     ; Direction = 1 = ScreenSplitDirection.Right
ret
```

`RegisterCameraState` 的闭包 lambda（`0x39E330`）还原为：

```csharp
rotation.ElementAtOrDefault(i) ?? _cameraStates[index].Rotation[i]
```

依据是 `Enumerable.ElementAtOrDefault<float?>` → `HasValue` 分支 →
`GetValueOrDefault()`，否则走 `Vector3.get_Item`（作用在 `CameraState+0x14`）。

### 一个可复用的判别法：`for` 还是 `while`

三个 `Register*State` 都有"把列表补齐到 `index + 1` 长"的循环。
第一版写成了 `while (_screenStates.Count < index + 1)`，**是错的**。

判别依据是 `get_Count` 被调用了几次：

```
0x39DFFD  call get_Count      ; 只在循环外调用一次
0x39E00F  mov  esi, eax       ; 存进局部变量
0x39E011  cmp  eax, r14d
0x39E014  jge  exit
0x39E020  loop: ... Add(new ScreenState())
0x39E0A5  inc  esi            ; 递增局部变量，Count 不再读
0x39E0AA  jl   0x39E020
```

`Count` 被提升到循环外，只读一次。Roslyn **不会**把一个接口属性调用提升到
`Add()` 之外（无法证明 `Add` 不改变 `Count`），所以这个提升来自源码结构本身：

```csharp
for (int i = _screenStates.Count; i < index + 1; i++)
    _screenStates.Add(new ScreenState());
```

两种写法在 `Add` 每次只加一个元素时行为等价——但这恰好是 Harmony patch
会打破的假设，而这几个方法被标为 Confidence A，所以必须写对。

三处（`0x39DFFD` / `0x39E1B0` / `0x39DC04`）都是同一形态，已全部修正。

> **教训**：`dotnet build` 通过不代表恢复正确——方法体写什么都能编译。
> 编译只保证不引入语法/类型错误，控制流的正确性只能靠反汇编本身交叉验证。

### 运行时验证：`InitializeService` 已经过双重确认

2026-09-13：把 `tools/bepinex.zip` 换成兼容 Unity 6 的新版（BepInEx 6.0.0-be.788，
Il2CppInterop 后端）后，游戏能正常启动、`project/src/Manosaba.Plugin` 能正常加载。
用 Harmony postfix 挂 `InitializeService`，游戏启动时自动触发（不需要玩家操作），
读到的真实字段值：

```
_stageManager=IPerspectiveStageManager  _cameraStates=non-null  _screenStates=non-null
```

跟上面的静态复原结论完全一致。这是本工程目前唯一一个"静态分析 + 真实运行时行为"
双重确认的方法，其余仍然只是静态分析（Confidence A 但未经运行时验证）。

详细的坑（尤其是 `Traverse.Field` 对 Il2CppInterop 类型会静默失效）记在
`work/reversing/README.md`「运行时验证通道」一节，不重复贴。

### 刻意没有做的事

`MoveNext`（3,420 B）没有恢复。从静态调用清单可以看出它"读回 GameState 并把
相机/屏幕状态作为一批 tween 重放"，但具体的顺序、空值处理和循环结构**没有验证**，
所以源码里保留原状，只在注释中记录已确证的调用清单。
按要求，不把 C/D 级推测写成确定事实。

---

## 6. 批量定案：205 个折叠访问器（进行中，已完成约 40 个）

`work/reversing/patch_accessor.py`：按 RVA 定位 `[Address(RVA="0x...")]` 标注的
accessor，替换方法体并插入置信度注释——用于批量处理"同一份原生代码被几十上百个
无关方法共用"的情形（body folding，见 `work/reversing/README.md`）。用法：

```powershell
# 先建 job 列表（每条 {path, rva, kind, body, comment}），再:
python patch_accessor.py jobs.json --dry   # 预览
python patch_accessor.py jobs.json         # 落盘
```

### 已完成（27 个文件，约 40 处方法体）

**普通字段 getter/setter**（偏移直接对应一个已声明字段，机械替换）：
`PerspectiveStageData`、`CharacterData(Item)`、`ClueData(Item)`、`MapData(Item)`、
`NoteData(Item)`、`ProfileData(Item)`、`RuleData(Item)`、`SpecialThanksData(Item)`、
`MasterData`、`AuthorData(Item)`、`WitchBookItemButton`、`WitchBookItemList`、
`TextSizeByCourse`——`get_Items`/`get_Name`/`get_Id`/`get_RectTransform` 等
18 组共 32 处，全部 Confidence A。

**Positional record 的定位属性**（`ScenarioInfo`、`ModifyCameraHeightTween`、
`ModifyCameraDistanceTween`、`ModifyCameraRotationTween`）——这类记录的属性
在 dump.cs 里只有一个形如 `<Act>k__BackingField` 的元数据名，**不是合法 C# 标识符**，
所以 ILSpy 把它们反编译成没有对应字段声明的裸 get/set 方法对。逐个补上私有字段
声明后，把 get/set 方法体、主构造函数、record 的拷贝构造函数（`with` 表达式用到的
那个）一并复原。全部 Confidence A，依据是反汇编直接确认的字段读写序列。

**一个需要小心的坑**：`dump.cs` 把这些字段标成 `readonly`，但反汇编证实每个属性
都有一个真正会写这个字段的 `set` 方法——C# 的 `readonly` 字段不允许在构造函数之外
被赋值，所以这个 `readonly` 标注本身就是不可信的（大概率是 Il2CppDumper 把 record
的 `init` 访问器误标成了字段级 readonly）。按证据（有 setter）而不是按 dump.cs 的
标签来定，字段声明成普通 `private`，不是 `readonly`。

**一个例外**：`WitchTrialsActions.asset` 的字段名是推测出来的（Confidence B，不是 A）——
这个类是 Unity Input System 从 `.inputactions` 资源自动生成的包装类（嵌套 struct 里的
`m_Wrapper` 反向引用是 Unity 生成器的标志性写法），不是手写游戏逻辑，`m_Asset` 这个
名字来自 Unity 生成器的通用命名习惯，不是反汇编直接给出的。

### 发现但刻意没有动的：record 的 `==`/`!=` 运算符整个不存在于骨架里

反汇编找到了 `op_Equality`/`op_Inequality` 的共享实现（标准的编译器生成 record
相等运算符：引用相等短路 → 空引用检查 → 调 `Equals`），Confidence A 级别的证据。
但搜索发现 `WitchTrials.Models` 下**没有任何一个** record 类型的 .cs 文件声明了
`operator ==`/`operator !=`——这不是"stub 写错了"，是 Il2CppDumper 的 DummyDll
生成器根本没有把这两个方法产出到骨架里。补上它们等于给类新增公开 API，
超出了"修复已声明成员的方法体"这个批次的范围，所以先记录在这里，不在本批次处理。

### 还没做的折叠组

`.ctor()` 空构造函数组（RVA `0x318720`/`0x336F70`/`0x3A22F0`，共 34 个 WitchTrials.*
方法）——反汇编确认是 `xor edx,edx; jmp 基类..ctor`，而 dump.cs 生成的骨架本来就是
`{ }`空方法体，这**已经是正确的**（C# 隐式默认构造函数就是"调用 base()，方法体为空"），
不需要改代码，只是还没有逐个补 Confidence 注释。

---

## 7. `GameInitializer::Start` — 游戏启动入口，已恢复并经运行时验证

原始任务说明把"游戏启动入口"列为第一优先级，这是它。

```csharp
private void Start()
{
    _steamManager.gameObject.SetActive(!DebugOptions.IsDebugMode || DebugOptions.EnableSteamworks);
    InitializeAsync(destroyCancellationToken).Forget();
}
```

依据（RVA `0x30FB70`）：`_steamManager` 判空 → 取其 `gameObject` → 调用
`DebugOptionsBase.get_IsDebugMode()` → 短路布尔判断（不在调试模式直接为 true，
在调试模式则读 `DebugOptions` 静态存储偏移 0 处的字段——对照 dump.cs 字段表，
偏移 0 正是 `s_enableSteamworks`，即公开属性 `EnableSteamworks` 的底层存储）
→ `SetActive` → 取 `this.destroyCancellationToken`（Unity 内置属性）→
启动 `InitializeAsync` 的编译器生成状态机 → 对返回的 `UniTask` 调用 `Forget()`
（fire-and-forget，`Start()` 本身不 await）。

**运行时验证**（Harmony postfix，`project/src/Manosaba.Plugin/Plugin.cs`
的 `GameInitializerVerification`）：游戏启动、不做任何操作，日志立即打印

```
[verify] GameInitializer.Start() -> IsDebugMode=False EnableSteamworks=True
         expectedSteamManagerActive=True actualSteamManagerActive=True match=True
```

`match=True`——静态复原的布尔表达式在真实运行时算出了跟 `SteamManager.gameObject`
实际的 `activeSelf` 完全一致的结果。这是继 `TrialStateManager.InitializeService`
之后第二个"静态 + 运行时"双重确认的方法。

---

## 8. 下一步

1. **继续批量定案剩余折叠组**——`.ctor()` 空构造确认（低优先级，代码已经对，只差注释）、
   `op_Equality`/`op_Inequality` 的缺失方法补齐（需要先决定是否值得新增 API 面）。
2. **恢复 `<LoadServiceState>d__9.MoveNext`**，把 async 状态机的工作流验证出来
   （这是唯一还没验证过的机制，之后 Naninovel-glue 和 PerspectiveStage 大量依赖它）。
3. 然后按第 4 节的表推进 Boot/Init → SaveLoad → Naninovel-glue。

---

## 约定

源码里的标记：

```csharp
// RECONSTRUCTED | Confidence: A | RVA: 0x39DA30    完整恢复
// PARTIALLY RECONSTRUCTED | Confidence: B          部分恢复
// NATIVE BODY NOT RECONSTRUCTED                    未恢复，保留占位
```

证据等级：**A** 原生代码直接确认 / **B** 多处交叉印证 / **C** 语义可能正确但有多解 /
**D** 无法可靠恢复。**C 和 D 不写进方法体。**

每次修改后必须保持 `dotnet build project\Manosaba.sln` 通过。
