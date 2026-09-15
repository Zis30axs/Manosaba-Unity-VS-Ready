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
| 第一批 44 个方法 | **27/44 已完成**（Boot/Init、TrialState 两个子系统全部完成；SaveLoad/Input/Naninovel-glue/WitchBook-Data/Map 部分完成） |
| 编译状态 | `dotnet build Manosaba.sln --no-incremental` = **0 error**（历史 `CS8632` 警告仅存在于未触碰的其他程序集） |
| 目标架构 | 独立 Unity Editor 工程 + 合法外部依赖 → Play Mode（**不是**塞回原版 `GameAssembly.dll`，Harmony 仅作恢复验证工具，见 §0.6） |
| Naninovel 兼容层可行性 | 已审计（§0.6，含一轮复核修正）：脚本是预编译 JSON（不用重写 `.nani` 解析器），条件分支语义 0 使用，表达式函数 15 个里只用到 1 个（`max`）；GigaCreation.NaninovelExtender 对 Naninovel actor/manager 基类有硬依赖，范围比初估大但仍可枚举；**独立风险**：`res/Assets` 下无 `.prefab`/`.unity`/`.meta`，场景/预制体还没有 Editor 可用形式，可能比兼容层本身更大——不需要先转 Yarn Spinner |
| Debate/Court 子系统 | 本轮攻坚（独立分支 `recon/core-systems`）：**已完成**：82/82 个函数体、32,044/32,044 B（100%，`work/reversing/progress.py` 测得；最近几批补完 `ExecutionButtonScreen`/`SkipDebateButton`/`ObjectionCutIn` 三个整类、`ChoiceEvidence::Execute`（`@evidence`）、`Damage::Execute`（原生为空操作）、`DebugOptions.AllowContinueDebate`，以及收尾的 `DebateText::ProcessText`/`ModifyLinkedTextColor`（链接悬停高亮）；此前手工累加的"49/82、27,405 B、85.5%"口径错误已作废，见 §7.5 更正），行为全部 Confidence A；最近几批补完 `DebateTextPrinterPanel` 显示管线（`SetText`/`AppendText`/`SetMessages`/`AddMessage`/`RevealMessages`/`WatAndRevealAsync`/`Gosub`/`Awake`/`Initialize`/`SetFont`/`OnDestroy`/3 个 getter）与 `DebateText` 链接点击链与 `PrintDebateText` 的 4 个尾巴，查出三处字节确认的行为事实（`RevealMessages` 不读自己的 `delay` 参数、`AppendText` 丢弃消息作者、`SetText`/`AddText` 下标约定互不兼容），并补上缺失的 `LitMotion.Extensions` 引用与 `PooledObject<T>` 桩缺失的 `IDisposable` 声明，见 §7.5；`DebateUi.cs` 整个文件完成；`WaitForDebate`/`Print()`/`Execute()` 均已复原成真正的 `async`/`await`（`Print()` 3 个 await 点、`Execute()` 2 个 await 点），不是 MoveNext 逐状态翻译；`ModifyDebatePrinter`（此前判定"very hard"暂缓的场景位置/旋转/动画种类配置）已复原，`Execute → GetOrAddPrinter → ModifyDebatePrinter → Print → WaitForDebate` 入口链现已完全闭合；`DebateTextPrinterPanel::AddText`（优先级表 #33）及其 LINQ 谓词 lambda 也已复原，五处非显然的第三方 API 识别（`IsNullOrWhiteSpace`/`Split`/`Behaviour.get_enabled`/`Behaviour.set_enabled`/`TMP_Text.set_alpha`）全部逐字节独立验证，并诚实记录了一处字节确认的行为疑点（循环边界用"空闲标签数"但写入目标是 `_labels[i]` 而非 `availableLabels[i]`）；外部复核发现并已修复 4 处提交后才暴露的错误（2 处虚表槽位号、1 处遗漏的 disposal 守卫、1 处多余语句），另有 2 处提交前发现并订正的注释错误（`set_Position`/`set_Rotation` 分派机制混淆；`AddText` 未填充槽位回退常量的错误识别），详见 §7.5"本轮修正"；顺带修复了两处阻塞性第三方/工程配置缺口（`Naninovel.UniTask.Awaiter` 缺失基接口列表；`Assembly-CSharp.csproj` 完全没有引用 `FastEnum.dll`） |
| PerspectiveStage 子系统 | 本轮启动（用户优先级第 2 位，此前完全未触碰）：**已完成**：65/65 个函数体、27,784/27,784 B（100%，`progress.py` 测得；收尾覆盖全部舞台命令、`StageCameraContainer`/`StageScreenContainer`/`StageScreenController` 全部方法与 `PerspectiveStagePresenter`，见 §7.6；此前手工累加的"19/65、23,203 B、83.5%"口径错误已作废）——`GosubToModifyPerspectiveStageCamera::Execute`/`GosubToSplitPerspectiveStageScreens::Execute`/`PerspectiveStagePresenter::Awake`/`StageCameraController::Awake`/`StageCameraController::get_TrialStateManager`/`StageCameraController::ModifyCameraAsync`/`StageCameraController::ShakeCameraAsync`/`PerspectiveStageManager`整个类（构造函数+属性+全部 7 个接口方法）/`StageScreenController::Awake`/`StageCameraController::OnDestroy`与`StageScreenController::OnDestroy`（同一原生函数的折叠体），全部 Confidence A（三个异步方法收尾的 using-vs-try/finally 写法为 B，见 §7.6）。`PerspectiveStageManager` 的每个方法都是单跳转发到对应子接口字段（`_loader`/`_screenModifier`/`_cameraModifier`/`_courtModifier`），逐一独立追踪参数在共享"接口槽缓存"跳板里的寄存器/栈来源验证，而不是按方法名相似度假设；`PerspectiveStagePresenter::Awake` 的构造调用把 `_cameraModifier` 钉死为 `_cameraController`（`StageCameraController`），使得 `ModifyCameraAsync`/`ShakeCameraAsync` 两条调用链现在从 `PerspectiveStageManager` 一路追踪到 `StageCameraController` 的真实实现，构成完整闭合的调用链而非假设。`ModifyCameraAsync`（RVA `0x384B50` 入口 + `0x3850A0` MoveNext，共 4,999 B，单一 await 点）复原成真正的 `async`/`await`，途中独立反汇编确认 `Cysharp.Threading.Tasks.UniTaskExtensions.GetAwaiter(UniTask[])`（RVA `0x18203F0`）——这是 `await _modifyCameraTasks[cameraIndex];` 之所以能编译的原因，也解开了 `Awake()` 留白的"4 行任务槽表索引什么"：槽 0/1/2 = height/rotation/distance tween 任务；副作用是发现并修复了一处工程配置缺口（`Assembly-CSharp.csproj` 缺 `UnityEngine.AssetBundleModule` 引用——`Cysharp.Threading.Tasks` 命名空间下同时可见的 `UnityAsyncExtensions.GetAwaiter(AssetBundleRequest/AssetBundleCreateRequest)` 使扩展方法查找必须能解析该类型，即使从未选中这些重载，已用 `MetadataLoadContext` 反射验证）。`ShakeCameraAsync`（RVA `0x384E00` 入口 + `0x386190` MoveNext，共 4,465 B）结构与 `ModifyCameraAsync` 同构，把 `_shakeCameraTasks` 的 3 槽解释为 X/Y/Z 三轴（`Vector3 force` 是必填结构体，三路无条件全跑，跟三个可选 tween 不同），并顺带用 grep 穷尽排查确认 `_modifyCameraTasks` 空闲的第 4 槽再无第三处读写——从"未知"降级为"证据已穷尽"。**`GosubToModifyPerspectiveStageCamera` 在脚本里出现 16,542 次——是全部 43 个 `WitchTrials.*` 自定义命令类里频次第二高的（仅次于 `PrintTextModified` 的 31,575 次），比本轮之前的主攻目标 `PrintDebateText`（2,003 次）高一个数量级**，所以这不只是"打开了一个新子系统"，按调用频次算是本轮杠杆最高的单个复原目标。前两个都不是 async 状态机（同步方法，尾调用返回 `gosub.Execute(default(AsyncToken))`，自身收到的 `token` 被静默丢弃不转发，字节确认 `AsyncToken` 恰好 16 字节）；揭示并在第二个目标上验证确认了一种"读每角色命名变量 → 拼 subroutine 标签后缀 → 重发布固定名变量 → Gosub 到 System_Subroutine"的通用调用约定；发现两者对同一个 `look:` 角色 id 会算出不同的变量名（`ToLowerInvariant` vs `FirstToLower`），查过脚本语料后确认这处不一致在实际内容里是惰性的（用到的 `look:` 值都是单大写字母开头）。`PerspectiveStagePresenter::Awake` 是这个子系统"常驻状态"一侧的入口，构造并注册唯一的 `PerspectiveStageManager` 服务，是整个子系统的接线图；复原过程中识别出两处看似直接跨类访问私有成员的调用，实际都是被内联的公开包装方法（`StageScreenController.Dismiss()`/`StageCameraController.Dismiss()`），各自独立反汇编验证；顺带修复第三处阻塞性第三方引用桩缺口（`Naninovel.ParameterList<TItem>` 缺失 `IEnumerable<TItem>`/`IEnumerable` 基接口，与 `UniTask.Awaiter` 同类问题）；本轮新发现五处无条件解引用的 `[SerializeField]` 字段已记入 `RECONSTRUCTION_CLAUDE_NOTES.md`，详见 §7.6 |
| Naninovel-glue 子系统 | 本轮推进（用户优先级第 3 位）：**已完成**：66/66 个函数体、21,262/21,262 B（100%，`progress.py` 测得）——`WitchTrialsScriptPlayer`（软重置保留 skip/auto、`GetSkipAllowed`、`ProcessAutoObjectionAsync`）、`WitchTrialsChoiceHandlerManager`、`WitchTrialsNaninovelManager` 余下成员、`ScriptHelper`、`GosubResetWitchBook` 整类、`ScenarioInfo` 改回位置记录，见 §7.7；全部 `WitchTrials.*` 合计 252/929 个函数体、95,243/316,778 B |
| Save/Load 子系统 | 本轮推进（用户优先级第 4 位）：**已完成**：31/31 个函数体、10,941/10,941 B（100%，`progress.py` 测得）——`QuickSave`/`QuickLoad`/`OpenLoadUi` 三个命令、`SaveLoadUi` 整类（存/读/删三条 async 流程、分页加载遮罩计数、焦点槽位选择）、`TitleUi.SaveLoadUi`，见 §7.8；全部 `WitchTrials.*` 合计 278/929 个函数体、104,318/316,778 B |
| WitchBook/Data 子系统 | 已完成（用户优先级第 5 位）：121/121 个函数体、39,146/39,146 B。模型层、五个 Page、列表项控件、WitchBookScreen、WitchBookUi、按钮与三个命令，见 §7.9；警告基线降为 140 |
| Audio 子系统 | 已完成：18/18 个函数体、5,268/5,268 B。收尾的 14 个包括：`sfx` 命令（PlaySfxModified）、SoundTestUi 播放器（含 UpdatePlayerAsync 状态机）、SpawnableWithAudio、`audioPlayer` 命令，以及两个 IAudioManager 懒加载访问器，见 §7.13 |
| Input 子系统 | 已完成：25/25 个函数体、5,482/5,482 B。收尾的 17 个包括：WitchTrialsActions 的 Unity 生成包装器（bindingMask/devices/FindBinding/5 个 Scheme）、WitchTrialsInputManager（ctor/PlayResponseSfx/BuildActionNameOrId）、DebugTextPreferenceNaninovelInputState，见 §7.12 |
| Map 子系统 | 进行中：第 A 组（叶子组件）完成，17/27 个函数体、5,094/9,894 B。MapArea、MapAreaFocusButton、MapAreaLabel、MapAreaModifier、MapFloor 全部方法，含 `OnClickAsync` 状态机与两个编辑器用 `ModifyInEditor`；为此给 Assembly-CSharp 补上 `GigaCreation.Tools.UndoExtensions` 工程引用，见 §7.14。第 B 组（MapPage、ChoiceMapArea、SpawnableMap）待做 |
| TrialState 子系统 | 已完成：18/18 个函数体、6,293/6,293 B。最后 6 个为 WitchTrialsNaninovelManager 的 ChangeTrialMode/SetTrialTimer，以及三个 Naninovel 命令 `trialMode`/`pauseTrial`/`timer`，见 §7.11 |
| UI 子系统 | 已完成（用户优先级第 6 位）：165/165 个函数体、50,436/50,436 B。共七组（第 4 组为 HintDisplay，第 5 组为 ControlPanel，第 6 组为 TitleUi，第 7 组为 OptionsUi），见 §7.10：<br>第 1 组：小型控件（TabToggle、两个 Toggle 切换器、CanvasGroupButton、VersionNumberDisplay、WitchTrialsButton/Label）与四个 Page 构造函数。<br>第 2 组：AutoToggleUi、AdvChoiceHandlerButton、MPImageAppearanceChanger 及其 Labeled 子类、TrialChoiceHandlerPanel 构造函数。<br>第 3 组：回想面板 WitchTrialsLogUi/LogMessageUi 与 WitchTrialsTextPrinterPanel。<br>警告基线仍为 140 |

---

## 0.5 恢复范围边界：43 个程序集里哪些是"游戏本体"，哪些是"引擎/中间件"

**结论先行**：43 个反编译程序集里，**只有 `Assembly-CSharp`（`WitchTrials.*`）是这个游戏自己的代码**。
其余全部是第三方中间件——跟 Minecraft 和 LWJGL 的关系完全一致：LWJGL 提供窗口/OpenGL/输入
绑定，Mojang 自己的 `net.minecraft.*` 才是模组关心的对象，MCP 只反编译后者。这里也一样：
其余程序集提供服务定位器/存档抽象/UI 组件/Naninovel 扩展等能力，但**不编码任何"审判/辩论/
证据"这些 Manosaba 特有的游戏规则**——那些规则 100% 在 `WitchTrials.*` 里，靠调用这些中间件的
公开 API 实现。

**证据不是靠代码风格猜的，是游戏自带的第三方授权文件说的**（`<游戏目录>\LICENSE.md`，
206 行，游戏自己随正式版一起发布，用于满足 MIT 协议的署名要求）：

| 授权文件里的条目 | 版权方 | 对应到反编译出的程序集 |
|---|---|---|
| Naninovel Extender | GIGA CREATION (2025) | `GigaCreation.NaninovelExtender.*`（10 个子程序集） |
| Service Locator for Unity | GIGA CREATION (2024) | `GigaCreation.Tools.Service` |
| Unity Undo Extensions | Gigacee / GIGA CREATION (2023) | `GigaCreation.Tools.UndoExtensions` |
| UnityTools | Gigacee / GIGA CREATION (2023) | `GigaCreation.Essentials.*`、`GigaCreation.Tools.*` 其余部分 |
| Build Timestamp Display for Unity | Gigacee / GIGA CREATION (2023) | `GigaCreation.Tools.BuildTimestampDisplay` |
| SpriteDicing | Artyom Sovetnikov / Elringus (2018) | `Elringus.SpriteDicing.Runtime` |
| Steamworks.NET | Riley Labrecque (2013-2022) | 纯引用 DLL，未反编译 |
| UniRx | Yoshifumi Kawai (2018) | 纯引用 DLL，未反编译 |
| UniTask | Yoshifumi Kawai / Cysharp, Inc. (2019) | 纯引用 DLL，未反编译 |
| LitMotion | Yusuke Nakada (2023) | 纯引用 DLL，未反编译 |
| FastEnum | Takaaki Suzuki (2019) | （被上述库内部引用） |

**关键发现：`GIGA CREATION`（含"Gigacee"个人身份）是第三方 Unity 工具作者，不是 Manosaba
的开发商。** 这几个库全部 MIT 协议、免费开源——意味着它们的真实源码大概率能直接在
Gigacee 的 GitHub 上找到，逆向它们不仅没必要，还不如直接找原始仓库（如果需要对照）。

**`Naninovel`（`Elringus.Naninovel.Runtime` + `Naninovel.Common`，789+176=965 个文件，
是整个工程里最大的一块）没有出现在这份 MIT 授权列表里**——因为它是付费商业资产
（Unity Asset Store 在售，naninovel.com 有完整公开文档），走的是商业 EULA 而不是 MIT 署名。
但结论一样：**第三方、有公开文档、不需要逆向**。

**用代码本身也能独立验证这个结论**（不止依赖授权文件）：
- 在全部 `GigaCreation.*` 源码里 grep "trial/debate/witch/evidence" 等 Manosaba 专属词汇，
  零命中（两个看似命中的全是"Switch"/"Industrial"之类的子串误报）。
- 类名全部是通用能力：`ServiceLocator`、`MasterDataProvider`、`DialogManager`、
  `InputManager`、`TweeningButton`、`Pagination`、`CameraManagerExtended`——没有一个
  和"witch trial"的具体规则相关，都是"提供能力"而不是"实现规则"。
- `CustomInspector` / `CustomInspector.Helpers`（114 个文件）全部类都标了
  `[Conditional("UNITY_EDITOR")]`——**发布版游戏里这些代码根本不会执行**，比"低优先级"
  更进一步：是完全无关。
- `ppt-noise`（`Noise.cs`/`Perlin.cs`）是通用 Perlin 噪声算法，公开算法，逆向价值也低。

**项目结构本身也已经体现了这条边界**：`project/src/*/*.csproj` 里，UniRx/UniTask/
Steamworks.NET/LitMotion/Newtonsoft.Json/MPUIKit/LeTai.TrueShadow 以及全部 `UnityEngine.*`
官方模块，从一开始就只以编译好的 DummyDll 二进制形式被引用（`<Reference HintPath=...>`），
**从未被反编译成源码**——工程本身早就没有把这些当"游戏代码"处理。真正被反编译成 43 个
可编译源码工程的，只是因为 IL2CPP 编译时会把所有静态链接的程序集（包括第三方资产）一起
打包进 `GameAssembly.dll`，Il2CppDumper 没法在"反编译"阶段区分"谁的代码"——这个区分
只能靠上面这些外部证据（授权文件、公开文档、代码语义）来做。

**对本工程恢复范围的实际结论**：
1. 本文件第 4/6/7 节列的优先级表、以及后续所有方法体恢复工作，**范围仅限
   `WitchTrials.*`（Assembly-CSharp）**——这从一开始就是对的，不需要调整。
2. 引用 Naninovel / GigaCreation.* 的 API 时（`Engine.GetServiceOrErr<T>()`、
   `ServiceLocator.Get<T>()`、`NaniVarManager.TrySetVariableValue(...)` 等），
   按其**公开签名语义**当已知黑盒使用即可，不需要也不应该去逆向这些库自己的方法体。
3. 唯一的例外情形：如果发现某个 `GigaCreation.*` 或 `Naninovel` 方法在这份游戏里的
   RVA 反汇编出来的行为，跟其公开文档/开源版本明显不一致（怀疑被 Manosaba 开发商
   自己打了补丁/魔改），那一处才值得单独逆向确认——目前没有发现任何这种情况的证据。

---

## 0.6 Naninovel 兼容层可行性审计（脚本数据面 + 代码 API 面）

**目标架构澄清（本轮审计的前提）**：本工程的目标**不是**把恢复后的 C# 重新塞回原版
`GameAssembly.dll`，也**不是**把 BepInEx/Harmony runtime patch 当成最终架构——Harmony
从始至终只是"拿真实运行时行为核对恢复是否正确"的验证工具。真正的目标管线是：

```
恢复的 WitchTrials.* C# + 合法可得的外部依赖（Unity/UniTask/UniRx/Steamworks.NET/
LitMotion/SpriteDicing/GigaCreation.* 等）+ 现有工程资产 + 重建的工程配置
  → Unity Editor 编译 → Play Mode
```

Unity 本身（引擎/渲染/物理/IL2CPP 运行时）在这个框架里跟 MCP 场景下的 JVM/LWJGL
地位一样：是正常安装的开发平台，不是需要逆向或重新实现的对象。原版 `GameAssembly.dll`
的角色也降级为"恢复证据来源"，不是最终运行时的必需依赖。

**唯一的真实缺口**：`Elringus.Naninovel.Runtime` + `Naninovel.Common`（965 个文件）虽然
按 §0.5 的结论"不需要逆向"，但它是**商业付费资产**，本工程没有采购许可证、也不打算购买，
所以不能把它的真实实现塞进 Unity 工程——`src/decompiled/` 下现有的 965 个文件全部是
IL2CPP 反编译出来的**空壳**（方法体是 `return null;`），无法直接编译运行。本节审计的目的
就是精确回答：**WitchTrials.\* 实际用到了 Naninovel 的哪一小部分 API/语义，够不够写一个
"最小兼容层"来替代整个商业包**，而不是先估算"多少文件耦合"就下结论要不要全量重写。

方法论：**一律用真实数据源逐字段解析，不按文件大小/文件数估算**——脚本数据面直接解析
440 个 `.asset.json` 的 `references.RefIds`（每条都带精确的 `{asm, ns, class}` 出处标签，
零歧义）；代码 API 面以本地已反编译的 Naninovel 源码树（965 个文件，含完整类型签名/基类/
接口/特性声明，只是方法体是空的）作为**真值字典**，再去匹配 `WitchTrials.*` 227 个源文件的
实际引用，而不是单纯数 `using` 语句或裸词 grep 命中数（后者已发现多个假阳性，见 0.6.2）。

### 0.6.1 脚本数据面（对 440 个 `.asset.json` 的精确统计）

**文件格式**：每个 `.asset.json` 是 Unity `[SerializeReference]` 序列化出的
`ScriptableObject`，不是 Naninovel 的 `.nani` 纯文本脚本格式。关键字段：
`textMap.idToText`（可本地化文本，按行 ID 索引）、`playlist.commands`（顺序命令流，
`{"rid": N}` 引用指针）、`references.RefIds`（真正的命令对象数组，每条形如
`{"rid", "type": {"class","ns","asm"}, "data": {...}}`）。**这意味着游戏出货时脚本已经是
"编译产物"，不是需要重新解析的源文本**——兼容层不需要重新实现 Naninovel 的 `.nani`
词法/语法分析器，只需要一个能把这份 JSON 反序列化成命令对象、并按 `playlist.commands`
顺序执行的播放器。

| 统计项 | 结果 |
|---|---|
| 脚本文件数 | 440 个（`Act01_Chapter01` ~ `Act02_Chapter06` + `Common`/`Debug`/`System`） |
| 命令实例总数 | 304,280 条 |
| 命令实例的 distinct (asm, ns, class) 类型 | 95 种 |
| 平均每文件命令数 | 254.5 |
| 可本地化文本行数 | 34,823 行，共 ~1,004 KiB 日文文本 |
| `Scenarios/` 目录总大小 | 398.9 MiB（440 个 `.asset.json`） |
| 目录内是否嵌有 base64/二进制数据 | **否**——抽样文件里最长字符串字段只有 93 字符（带
  `<ruby>` 标记的日文台词），398.9 MiB 几乎全部是**结构化 JSON 本身的冗余**（每个参数
  不论是否被使用，都固定携带 `value`/`hasValue`/`raw`/`spot` 四个子字段），跟"剧本复杂度"
  没有正比关系 |

**命令来源占比**（按 asm 分组，95 个 distinct 类型分布）：

| 程序集 | 命令实例数 | 占比 | distinct 类型数 |
|---|---:|---:|---:|
| `Elringus.Naninovel.Runtime`（Naninovel 内置） | 212,631 | 69.9% | 38 |
| `Assembly-CSharp`（`WitchTrials.*` 自定义） | 59,869 | 19.7% | 43 |
| `GigaCreation.NaninovelExtender.ExtendedActors` | 27,304 | 9.0% | 5 |
| `GigaCreation.NaninovelExtender.Common` | 3,464 | 1.1% | 4 |
| `GigaCreation.NaninovelExtender.ExtendedSpawn` | 994 | 0.3% | 2 |
| `GigaCreation.NaninovelExtender.Effects` / `.Localization` | 18 | ~0% | 3 |

注意：`Naninovel.CommandScriptLine`(80,446)/`EmptyScriptLine`(62,434)/
`GenericTextScriptLine`(31,554)/`CommentScriptLine`(15,322)/`LabelScriptLine`(2,524)
这 5 种"脚本行包装类型"占了 Naninovel 内置命令数的大头——它们对应原始 `.nani` 源文本里
**每一行**（含空行、纯注释行）被编译后留下的占位记录，不是"游戏逻辑"，兼容层里可以用
一个统一的行结构表示，不需要 5 个独立类。真正代表游戏逻辑密度的是下面这张表。

**`WitchTrials.*` 全部 43 个自定义命令类**（按脚本内出现频次）：

| 频次 | 类名 | 频次 | 类名 |
|---:|---|---:|---|
| 31,575 | `PrintTextModified` | 22 | `SetBadId` |
| 16,542 | `GosubToModifyPerspectiveStageCamera` | 19 | `EmoteJumpDouble` |
| 2,621 | `PlaySfxModified` | 16 | `ReturnToTitle` |
| 2,159 | `ChangeTrialPauseMode` | 14 | `EmoteJump` |
| 2,003 | `PrintDebateText` | 13 | `ChoiceMapArea` |
| 963 | `UpdateWitchBook` | 11 | `ClearWitchBook` |
| 698 | `Damage` | 6 | `SplitPerspectiveStageScreen` |
| 693 | `StopForChoice` | 5 | `ChangeTrialMode` |
| 677 | `CancelGosub` | 4 | `ShowCreditsUi` / `HideCreditsUi` / `ChangeCourtFireFxActivity` |
| 461 | `HidePerspectiveStageScreen` | 3 | `EmoteShake` / `ChangeDebateMode` / `ShowLoadingUiForDebug` |
| 373 | `GosubResetWitchBook` | 2 | `EmoteNod` / `UnloadPerspectiveStage` / `BlockInteraction` |
| 213 | `ModifyPerspectiveStageCamera` | 1 | `EmoteFall` / `ShowWitchBookUi` / `QuickLoad` / `LoadPerspectiveStage` |
| 194 | `GosubToObjectionCutIn` | | |
| 148 | `SetTrialTimer` | | |
| 101 | `GosubToSplitPerspectiveStageScreens` | | |
| 89 | `ChoiceEvidence` | | |
| 59 | `UpdateAudioPlayerUi` | | |
| 46 | `ShowPerspectiveStageScreen` | | |
| 41 | `ShakePerspectiveStageCamera` | | |
| 26 | `QuickSave` | | |
| 25 | `RecordCorrectChoice` / `ProcessBadEnd` | | |

**控制流**：`Gosub`(3,110 次)/`Return`(752 次) 是子程序跳转的主力，目标绝大多数集中在
9 个 `System/System_Subroutine.*` 固定入口（`EndDebate-Last/First`、`EndDebate-Effect`、
`BeginDebate-Last/First/Effect`、`BeginAdv`、`EndAdv`、`BeginTrial`、`EndTrial`）——
这是一个**很规整的状态机骨架**，不是任意跳转的意大利面代码。此外
`GigaCreation.NaninovelExtender.Common.GotoModified`（3,158 次）是 GigaCreation 扩展的
无返回跳转（对应标准 Naninovel `Goto`/`@goto` 语义的自定义替代实现）。

**条件分支——一个反直觉但确定的发现**：Naninovel 给几乎每个命令都预留了
`ConditionalExpression` 参数槽位（用于内联 `@if` 条件执行单条命令），语料库里这个字段
**出现了 112,000 次，但全部 440 个文件里没有一次被实际赋值**（`hasValue` 全部为 0，
已对全部 112,000 处做了逐条验证，非抽样）。**这说明 Manosaba 的分支/审判逻辑完全不依赖
Naninovel 内置的行级 `@if` 语义**，而是通过"自定义 Command 类 + `SetCustomVariable` 写状态
变量 + Gosub 按变量值跳转不同子程序"这条路径实现的——对兼容层来说是好消息：**行级条件
执行语义可以完全不实现**，选择/分支相关的真正逻辑全部封装在上面表格里的
`ChoiceEvidence`/`ChoiceMapArea`/`RecordCorrectChoice`/`StopForChoice`/`AddChoice` 等
命令类本身，只要那些类实现对，条件分支就跟着对。

**变量**：`SetCustomVariable` 共 917 次赋值，语法是 Naninovel 的裸标识符表达式
（**不是** `{var}` 插值），例如：
```
g_gameProgress = max(1, g_gameProgress)
hasWitchBook = false
chosenAreaId=="Library"
chosenAreaFlag == 15
```
高频变量：`nextScenario`(410)、`chosenEvidenceId`(200)、`isDebugDirectJump`(175)、
`courtStandCount`(83)、`chosenAreaId`(58)/`chosenAreaFlag`(36)、
`characterDistance`(33)/`cameraYaw`(19)（运镜相关）、`g_gameProgress`/`g_moviePlayed`/
`g_pi`（全局存档进度）、以及一批 `xxxIdx` 后缀的角色索引变量（`emaIdx`/`ananIdx`/
`miriaIdx`/`hiroIdx`/`meruruIdx`/`hannaIdx`/`sherryIdx`/`leiaIdx`/`cocoIdx`/`margoIdx`/
`noahIdx`/`nanokaIdx`/`alisaIdx`/`yukiIdx`/`wardenIdx`/`jailerIdx`，各 16 次，明显是给
16 个固定角色分配舞台位置索引的机制）。表达式里出现的函数目前确认至少有 `max`；
Naninovel 内置表达式函数的完整清单见 0.6.3。

**本地化结构**：`res/Assets/#WitchTrials/Localization/zh-Hans/` 下只有一种语言（简中），
分两块：
- `Text/Scripts/`：440 个 `.txt`，与 440 个 `.asset.json` **一一对应**，格式是 Naninovel
  标准的"managed text document"——`# 行ID` + `; 原文注释` + 译文，纯文本、按行 ID 对齐，
  跟 `.asset.json` 里预编译的命令流是两条独立的数据轨（脚本结构 vs 台词翻译），互不耦合。
  这部分文本一共只有 6.6 MiB。
- `Text/`：另外 4 个全局文档——`CharacterNames.txt`（角色名对照）、`CustomUI.txt`、
  `DefaultUI.txt`、`Locales.txt`（语言列表）。
- `Movies/`：2 个 `.m4v`（`Act01_Opening.m4v`、`Act02_Opening.m4v`），**311.8 MiB**，
  占 `Localization/` 目录总大小（318.5 MiB）的 **98%**。**这是之前"717 MiB 文件体积"
  印象的真正来源之一**——真正的翻译文本只有 6.6 MiB，剩下几乎全是两个片头动画。

### 0.6.2 代码 API 面（对 227 个 `Assembly-CSharp` 源文件的精确统计）

以本地反编译的 `Elringus.Naninovel.Runtime`(789 文件) + `Naninovel.Common`(176 文件) +
10 个 `GigaCreation.NaninovelExtender.*` 子程序集作为真值类型字典（共 1,782 个已知类型名），
扫描 227 个 `WitchTrials.*` 源文件里对这些类型的实际引用。

**方法论修正（重要）**：纯按裸标识符做正则匹配会产生假阳性——已发现并排除 5 个：
`Token`（其实是 `Il2CppDummyDll.Token` 反射令牌特性，跟 Naninovel 自己名为 `Token` 的
表达式词法单元类型撞名）、`Dictionary`/`Nullable`（`System.Collections.Generic`/
`System` 的裸标识符，跟 Naninovel 同名类型撞名）、`Compiler`（`System.CodeDom.Compiler`
命名空间引用 + "Compiler-generated" 注释文本撞词）、`Wait`（`public BooleanParameter Wait;`
是字段名撞了 `Naninovel.Commands.Wait` 类名，逐处核对后确认 227 个文件里**零处**真正
`new Wait`/`: Wait`/`<Wait>` 用法）。修正后：

| 统计项 | 结果 |
|---|---|
| 有 `Naninovel*`/`GigaCreation.NaninovelExtender*` using 指令的文件 | 114 / 227 |
| 修正后 distinct 引用类型数 | ~151（原始裸词命中 156，减去上述 5 个确认假阳性） |
| 按种类分布 | class 78 / interface 37 / struct 31 / enum 5 |
| `Engine.GetServiceOrErr<T>`/`GetService<T>` 请求的 distinct 服务接口数 | **仅 6 个** |

**Engine 服务面**（`Engine.GetServiceOrErr<T>()` 请求的全部 6 个服务接口）：
`ICustomVariableManager`(6)、`IScriptPlayer`(3)、`ITrialStateManager`(3，WitchTrials 自定义
接口)、`IAudioManager`(2)、`IUIManager`(2)、`IStateManager`(2)。**这是整个引擎级 API 面里
真正被直接调用的部分，规模很小。**

**类型引用里排名最高的其实不是"Naninovel 业务 API"，而是异步基础设施**：`UniTask`(103
文件)/`AsyncUniTaskMethodBuilder`(76)/`Awaiter`(75)/`AsyncToken`(52) 排在裸引用频次最前面，
但这是 Cysharp `UniTask` 库的异步编译器基础设施（`Naninovel` 项目里重新导出了 UniTask 的
命名空间别名），本来就已经在 §0.5 里被归类为"正常免费外部依赖"，兼容层不需要为此单独
做任何事——直接引用真正的 UniTask 包即可。

**真正的 Naninovel 业务类型**（排除异步基础设施后）：
`BooleanParameter`/`IntegerParameter`/`DecimalParameter`/`StringParameter`/
`StringListParameter`/`DecimalListParameter`（18/14/13/12/5/3 个文件）—— Naninovel 的
"脚本参数包装类型"家族，全部是简单的值+`hasValue`可空封装，机械易实现；
`IManagedUI`(17)/`CustomUI`(10)——UI 生命周期契约；
`IInputManager`/`IScriptPlayer`/`IAudioManager`/`ICustomVariableManager`——引擎服务接口
（即上面 Engine 服务表的另一侧）。

**继承关系（`class X : Y` 精确匹配，无歧义，这是本次审计里置信度最高的一组数字）**：

| 基类 | 派生的 WitchTrials 类数 |
|---|---:|
| `Command`（`Naninovel.Commands.Command`） | 29 |
| `CustomUI` | 10 |
| `AnimateActorExtended`（GigaCreation） | 5 |
| `ChoiceHandlerButton` / `PrintText` / `SetCustomVariableAsFlag` / `Stop` | 各 2 |
| `Gosub`、`ScriptPlayer`、`InputManager`、`Configuration`、`NaninovelManager`、
  `NaninovelPresenter`、`BackgroundMatcher`、`NaninovelTMProText`、`UITextPrinter`、
  `UITextPrinterPanel`、`EngineInitializationUI`、`CGGalleryGridSlot/Panel/Grid`、
  `LoadingIndicatorBase`、`PlaySfx`、`ChoiceHandlerPanelModified`、`NaninovelScreen`、
  `SpriteBackgroundExtended`、`BacklogMessageUI`、`BacklogPanelModified`、
  `ChoiceHandlerManager` | 各 1 |

跟 0.6.1 的"43 个自定义命令类"基本对得上——大部分命令类直接 `: Command`，少数（如
`GosubResetWitchBook : Gosub`）直接继承某个具体内置命令类而不是抽象基类，
`GosubToObjectionCutIn`/`GosubToModifyPerspectiveStageCamera`/
`GosubToSplitPerspectiveStageScreens` 虽然类名带 "Gosub" 但实际都 `: Command`（自己实现
逻辑，内部再 `new Gosub{...}.Execute()`，属于"调用" Gosub 而非"继承" Gosub）。

**特性（attribute）使用面**：Naninovel 自己一共声明了 35 种可用特性（`ActorContext`、
`Branch`、`CommandAlias`、`ConditionContext`、`ExpressionFunction` 等），`WitchTrials.*`
只用到其中 **11 种**：`ParameterAlias`(52)、`CommandAlias`(31)、
`ParameterDefaultValue`(20)、`RequiredParameter`(20)、`ResourcePopup`(13)、
`InitializeAtRuntime`(3)、`ActorResources`(2)、`Branch`(2)、`VectorContext`(2)、
`EditInProjectSettings`(1)、`ConstantContext`(1)——不到三分之一。

**尚未完成的子项（本轮审计的已知空白）**：
- 序列化在场景/预制体里的 Naninovel 组件类型（第 7 项）——Unity 的 `.prefab`/`.unity`
  YAML 靠脚本 GUID 而非类名引用 `MonoBehaviour`，要精确回答这一项需要额外做
  "`.meta` GUID ↔ 类名"的交叉比对，本轮未做，留作后续。
- ~~Naninovel 内置表达式函数的实际使用频率~~——已补充，见 0.6.3：15 个内置函数里
  **语料库实际只用到 1 个**（`max`，28 次）。

### 0.6.3 综合结论：最小兼容层范围，直接回答"够不够"

**Naninovel 内置表达式函数完整清单**（来自 `ExpressionFunctions.cs` 的
`[ExpressionFunction("...")]` 声明，共 15 个函数名/18 个重载）：
`random`(×3 重载)、`calculateProgress`、`isUnlocked`、`hasPlayed`(×2)、`getName`、
`pow`、`sqrt`、`cos`、`sin`、`log`、`abs`、`max`、`min`、`round`、`approx`。
这是 Naninovel 表达式求值器的**全部**内置函数——不是一个大型脚本语言，是一个只有
15 个数学/存档相关函数的表达式求值器，加变量读写、`==`/`!=`/`&&`/`||` 等运算符。

**实测使用频率**（对全部 440 个 `.asset.json` 按函数名逐一 grep 计数）：15 个内置函数
**语料库里只用到 1 个**——`max`，28 次（即 §0.6.1 样例里的
`g_gameProgress = max(1, g_gameProgress)` 这类"进度只增不减"写法）。其余 14 个
（`random`/`calculateProgress`/`isUnlocked`/`hasPlayed`/`getName`/`pow`/`sqrt`/`cos`/
`sin`/`log`/`abs`/`min`/`round`/`approx`）**零使用**。这进一步收窄了兼容层的表达式求值器
范围：变量读写 + `==`/`&&` 等基本运算符 + 一个 `max` 函数，就能跑通语料库里全部 917 处
`SetCustomVariable` 表达式——不需要实现完整的 15 函数表。

**结论**：为了让 `WitchTrials.*` 在没有商业 Naninovel package 的情况下运行，需要实现的
最小兼容层范围是**可枚举、可界定的**，不需要重写整个 Naninovel 引擎，也不需要先把内容
转换成 Yarn Spinner 这类新格式：

1. **脚本加载器**：反序列化现有 440 个 `.asset.json`（`playlist`/`references.RefIds`/
   `textMap` 三个字段），不需要重新实现 `.nani` 纯文本编译器——出货的脚本已经是编译产物。
2. **~38 个 Naninovel 内置命令类**（`Elringus.Naninovel.Runtime` 语料库实际用到的
   distinct 类型数）+ 5 种脚本行包装类型（可以合并成 1 种通用行结构）。
3. **6 个 Engine 服务接口**及其被实际调用的成员（`ICustomVariableManager`/
   `IScriptPlayer`/`IAudioManager`/`IUIManager`/`IStateManager` + WitchTrials 自定义的
   `ITrialStateManager`）——不是整个 Naninovel 引擎服务定位器体系。
4. **6 个参数包装类型**（`BooleanParameter` 等）+ `Named<T>`/`NamedString` 家族——纯数据
   结构，机械可实现。
5. **表达式求值器**：变量读写 + 15 个内置函数（实测只用到 1 个，`max`）+ 基本比较/逻辑
   运算符——规模很小，**完全不需要实现行级 `@if` 条件执行语义**（语料库证实零使用）。
   **但求值器必须能在任意参数字段上被调用**，不能只挂在 `Expression`/
   `ConditionalExpression` 这两个具名字段上——见 0.6.4 第 2 条修正。
6. **`IManagedUI`/`CustomUI` UI 生命周期契约**，支撑 10 个自定义 UI 类。
7. **11 个特性**（`CommandAlias`/`ParameterAlias`/`RequiredParameter` 等）——主要影响
   编辑器序列化/校验，不影响运行时语义，可以先做最简实现（甚至先忽略部分编辑器专用行为）。
8. `GigaCreation.NaninovelExtender.*` 的 5+4+2+3=14 个 distinct 命令类型，**外加它们
   直接派生的一批 Naninovel 内置 actor/manager/state 基类**——已确认（见 0.6.4 第 3 条
   修正），不是独立库，必须折算进兼容层契约。

**明确拒绝的方向**：现在就把全部内容转成 Yarn Spinner。语料库已经完全绑定在 Naninovel
的命令词汇表/序列化契约上（304,280 条命令实例、43 个自定义命令类都是照着 Naninovel 的
`Command`/`Engine.GetServiceOrErr<T>()` API 写的），实现一个范围明确的兼容层（above 1-8）
可以**零改动**复用现有全部内容；换成 Yarn Spinner 意味着要重新编写或者机械转换全部
304,280 条命令实例——工作量和风险都严格大于实现这个兼容层。**优先级：先把兼容层范围
钉死到可执行的清单（本节 1-8），有余力再评估是否要往 Yarn Spinner 迁移，而不是反过来。**

### 0.6.4 复核修正（针对 0.6.1/0.6.2 的三处更正 + 一个新发现的独立风险）

本节初稿提交后做了一轮复核（用真实数据反查，而不是相信第一遍脚本的输出）。发现三处
需要更正的具体结论，以及一个跟 Naninovel 完全无关、但直接影响"→ Unity Editor → Play
Mode"这条目标管线能否走通的独立风险。按本工程一贯的证据纪律，更正记录在这里，不覆盖
上面的原始数字（表格数字本身没错，是"解读"错了）。

**修正 1：`nextScenario`(410 次) 不是生产关卡的分支变量，是调试菜单专用**。
逐文件定位后发现，`nextScenario` 和它对应的 `.GoToChosenScenario` 标签**100% 只出现在
一个文件**——`Scenarios/Debug/Debug_Home.asset.json`（一个 3.3 MB 的调试用"跳转到任意
关卡"菜单，410 大概率对应 440 个正式关卡里可供调试直跳的那一部分）。**真正的生产关卡
分支变量**是 `chosenEvidenceId`（400 次出现，分布在 59 个文件）、
`isDebugDirectJump`(546 次，368 个文件——这个虽然名字也带 debug，但它是嵌在几乎所有
正式关卡文件开头的一个调试直跳放行判断，本身是发布内容的一部分，不是纯调试专属文件）、
`courtStandCount`(253 次，54 个文件)、`chosenAreaFlag`/`chosenAreaId`(212+116 次，
24+12 个文件)——这些才是 0.6.1 表格里应该被当作"高频生产变量"读的条目。

**修正 2：表达式插值不止发生在 `Expression`/`ConditionalExpression` 两个具名字段上**。
0.6.1 最初只扫了这两个字段，遗漏了 Naninovel "任意参数值都可以内嵌表达式"这条通用
语法——同一个 `raw.parts[].expression` 结构在其他参数字段里同样出现，逐字段复核后
统计到 **26 组 (命令类, 参数名) 组合、约 3,400+ 处**非空表达式插值，例如：

| 命令.参数 | 次数 | 表达式样例 |
|---|---:|---|
| `ModifyBackgroundExtended.Scale` | 736 | `g_backgroundDefaultScale`、`g_backgroundDefaultScale * 3` |
| `ModifyCharacterExtended.Position` | 706 | `hiddenCharacterPositionY` |
| `Wait.WaitMode` | 649 | `debateWaitTime` |
| `ModifyCharacterExtended.Scale` | 402 | `characterScale` |
| `ModifyCharacterExtended.Rotation` | 201 | `hiroRotationY` |
| `ModifyPerspectiveStageCamera.Rotation` | 199 | `(2.2 / courtStandCount * 360)` |
| `GosubResetWitchBook.IsDebugDirectJump` | 196 | `isDebugDirectJump` |
| `AddChoice.ChoiceSummary` / `UpdateAudioPlayerUi.TrackName` | 各 58 | `bgm_1_1`（动态拼出资源名，不是台词文本） |
| `GotoModified.Path` | 3 | `nextScenario`、`commonBadEndId`（Debug 菜单专用的动态跳转，见修正 1） |

**这直接改变兼容层的设计约束**：表达式求值器不能只挂在两个具名字段的反序列化路径上，
必须作为**所有参数类型通用的反序列化步骤**（每个参数值在读出时都要检查
`raw.parts[].expression` 是否非空，非空则求值后替换），因为真正大量使用插值的地方
（背景缩放、角色位置/旋转、等待时长）全部是数值/资源名参数，不是 `Expression` 那种
"整段就是一句赋值语句"的字段。好消息是：0.6.3 里"15 个内置函数只用到 `max`"的统计
是对全文件文本做的正则扫描（不区分字段来源），这条结论本身不受这处遗漏影响，仍然成立。

**修正 3：`GigaCreation.NaninovelExtender.*` 对 Naninovel 有硬编译期继承依赖，不是独立库**。
0.6.3 原文把这个留作"待确认"，现已用源码直接确认——**确认为真**：
`AnimateActorExtended : AnimateActor`、`ModifyCharacterExtended : ModifyCharacter`、
`ModifyBackgroundExtended : ModifyBackground`、`BackgroundManagerExtended :
BackgroundManager`、`BackgroundStateExtended : BackgroundState`、
`CharacterManagerExtended : CharacterManager`、`CharacterStateExtended : CharacterState`、
`DicedSpriteCharacterExtended : DicedSpriteCharacter`、`LayeredCharacterExtended :
LayeredCharacter`、`SpriteBackgroundExtended : SpriteBackground`、`GotoModified : Goto`、
`Quit : Stop`、`GameStateMapExtended : GameStateMap`、`StateManagerExtended :
StateManager`、`DestroySpawnedExtended : DestroySpawned`、`MultipliableSpawn :
SpawnManager`、`SpawnExtended : Spawn`——**16 个 GigaCreation 类直接继承 Naninovel 自己
的 actor/manager/state 基类**，还实现了 `IEngineService<T>`/`IStatefulService<T>`/
`IMultipliableActor`/`IPivotChangeableActor`/`IMaskabilityChangeableActor` 等 Naninovel
接口。**结论**：兼容层不能只实现 0.6.3 第 2 条里"脚本里直接出现的 ~38 个命令类"，还要
连带实现这 16 个基类代表的 actor/manager 子系统（角色/背景渲染抽象、状态管理、生成物
管理）——范围比最初估计的大，但依然是**可枚举**的（上面这 16 个类名就是完整列表），
没有变成"重写整个引擎"。

**独立风险（跟 Naninovel 无关，但同样卡在"→ Unity Editor → Play Mode"这条目标管线上）**：
`res/Assets/` 下**没有任何 `.prefab`/`.unity`/`.meta` 文件**（全部搜索命中 0）。目录结构
是 `res/Uncategorized/<UnityObjectType>/`（`GameObject`/`MonoBehaviour`/`Transform`/
`Canvas`/`Material`/`Shader`/...）+ `res/_serialized/`，这是 AssetRipper 一类工具"按
Unity 对象类型平铺导出"的中间产物特征，**不是** Unity Editor 能直接识别、带 GUID 引用
关系的工程结构（Editor 需要 `.meta` 文件的 GUID 才能把 `MonoBehaviour` 序列化数据跟
具体脚本类型、把 `Transform` 挂回具体 `GameObject`、把场景/预制体重新拼成层级树）。
**这意味着 0.6.2 第 7 项（"序列化在场景/预制体里的 Naninovel 组件类型"）目前不是"需要
额外交叉比对"这么简单，而是连场景/预制体本身的 Editor 可用形式都还没有生成**——这可能
是比"实现 Naninovel 兼容层"更大、更优先的一块工作（AssetRipper 通常有场景/预制体重建
功能，需要确认是否跑过、跑到哪一步、产物在哪），**留作独立的下一轮审计题目，本轮不
展开**。

**本轮审计的诚实边界**：用户要求的 10 项（Part A）里，第 3 项（唯一方法/属性计数）和
第 10 项（机械 vs. 独特调用的精细分类）**没有做**——审计脚本声明了 `member_calls`
计数器但从未填充，属于遗留的未完成项，不是"数据显示为零"。第 7 项如上，因为场景/预制体
本身不是 Editor 可用格式而被阻塞，不是简单的遗漏。

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

各桶当前复原进度用 `work/reversing/progress.py` 测量（与本表同口径；计数规则见 §7.6
"进度计数约定"），不要手工累加。

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
| 3 | Boot/Init | `SteamManager::OnEnable` | `0x389790` ✅ | 841 | 20 | 0 | 0 | 0 | 1 | medium |
| 4 | Boot/Init | `SteamManager::OnDestroy` | `0x389AE0` ✅ | 1177 | 19 | 0 | 0 | 0 | 0 | hard |
| 5 | Boot/Init | `EntrySequence::ResetDebugVariables` | `0x38A660` ✅ | 571 | 8 | 2 | 1 | 5 | 1 | medium |
| 6 | Boot/Init | `GameInitializer::Start` | `0x30FB70` ✅🎮 | 560 | 11 | 0 | 0 | 0 | 1 | medium |
| 7 | TrialState | `TrialStateManager::RegisterCameraState` | `0x39DA50` ✅ | 1323 | 30 | 0 | 0 | 1 | 5 | hard |
| 8 | TrialState | `WitchTrialsNaninovelManager::ChangeTrialPauseMode` | `0x399BE0` ✅ | 626 | 21 | 0 | 1 | 1 | 1 | hard |
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
| 21 | Naninovel-glue | `WitchTrialsNaninovelManager::StartTrial` | `0x39A1D0` ✅ | 574 | 6 | 0 | 3 | 4 | 2 | medium |
| 22 | Naninovel-glue | `GosubResetWitchBook::CheckEvidenceState` | `0x328D20` | 1103 | 17 | 1 | 0 | 0 | 8 | hard |
| 23 | Naninovel-glue | `GosubToObjectionCutIn::Execute` | `0x325750` ✅ | 736 | 7 | 0 | 0 | 5 | 5 | medium |
| 24 | Naninovel-glue | `WitchTrialsNaninovelPresenter::Awake` | `0x38EE30` ✅ | 227 | 4 | 0 | 1 | 0 | 1 | easy |
| 25 | PerspectiveStage | `GosubToModifyPerspectiveStageCamera::Execute` | `0x31D870` ✅ | 2520 | 48 | 0 | 0 | 11 | 8 | hard |
| 26 | PerspectiveStage | `GosubToSplitPerspectiveStageScreens::Execute` | `0x31E250` ✅ | 2338 | 44 | 0 | 0 | 10 | 7 | hard |
| 27 | PerspectiveStage | `PerspectiveStagePresenter::Awake` | `0x38D8D0` ✅ | 1558 | 33 | 0 | 1 | 0 | 5 | hard |
| 28 | PerspectiveStage | `StageCameraController::Awake` | `0x3846C0` ✅ | 840 | 24 | 0 | 0 | 0 | 0 | hard |
| 29 | WitchBook/Data | `ClueDataItem::.ctor` | `0x36C0D0` ✅ | 170 | 4 | 72 | 0 | 0 | 0 | easy |
| 30 | WitchBook/Data | `MapDataItem::.ctor` | `0x30C0D0` ✅ | 82 | 2 | 56 | 0 | 0 | 0 | easy |
| 31 | WitchBook/Data | `WitchBookScreen::InitializePages` | `0x37A880` | 1408 | 31 | 0 | 5 | 0 | 5 | hard |
| 32 | WitchBook/Data | `WitchBookScreen::get_PageData` | `0x3799B0` ✅ | 807 | 15 | 14 | 0 | 0 | 3 | medium |
| 33 | Debate/Court | `DebateTextPrinterPanel::AddText` | `0x367E20` ✅ | 1322 | 36 | 1 | 0 | 3 | 5 | hard |
| 34 | Debate/Court | `DebateUi::OnVisibilityChangedByCustomUi` | `0x36A100` ✅ | 1435 | 19 | 1 | 2 | 0 | 10 | hard |
| 35 | Debate/Court | `PrintDebateText::ModifyDebatePrinter` | `0x322B40` ✅ | 1590 | 34 | 1 | 0 | 1 | 7 | hard |
| 36 | Debate/Court | `DebateUi::TimeSpanToText` | `0x36A900` ✅ | 1156 | 29 | 1 | 1 | 3 | 1 | hard |
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

## 7.5 `Debate/Court` 子系统：已完成，82/82 个函数体 / 32,044/32,044 B（独立 worktree
`recon/core-systems`，无资源审计工作，见并行的 `audit/unity-assets`）

**进度（`work/reversing/progress.py` 测得，口径与 §3 桶总数一致）**：`Debate/Court` 桶
82 个独立函数体 / 32,044 B，**全部复原（100%）**。收尾一批：`DebateText::ProcessText`
（不调用基类实现，自行串起 6 个 Naninovel 标签处理虚方法——vtable 槽 162/159/163/160/161/165
= `ProcessExpressionTags`/`ProcessLinkTags`/`ProcessTipTags`/`ProcessRubyTags`/`ProcessEventTags`/
`FixArabic`；非空白文本时订阅 GigaCreation `IInputManager.GetAction("Point", "UI")`（接口槽 1）
驱动悬停高亮）与 `DebateText::ModifyLinkedTextColor`（`FindIntersectingLink` 命中链接时按
0.75 调暗、离开时按 1.33333f 还原，两常量从 PE 读出，1.33333f 不是精确倒数，反复悬停会有
微小色差漂移；每轮以 `UpdateVertexData(All)` 结束）。再上一批：`ChoiceEvidence::Execute`（`@evidence` 命令，3 个 await 复原为真正的
`async`/`await`；`correct:` 指定的证据不在图鉴当前显示范围时走 `Command.Err`（vtable 槽 8）
报错而非警告；调试跳过时不等玩家选择、直接取正解或 `"*"`，结束后
`IScriptPlayer.SetSkipEnabled(true)`（接口槽 34）），`DebateTextPrinter` 构造函数（纯基类调用）。
最新一批 +7 个 / +1,283 B：`ObjectionCutIn`（`SetSpawnParameters` 与两个 async 方法；
`AwaitSpawn` 按参数只显示一组对象，`PlayableGraph::IsValid()` 由 icall 名字符串锚定——
已有图则 `time = 0` 后 `ResumeAsync`，否则 `PlayAsync`；`AwaitDestroy` 只取消 `_cts`，
不隐藏任何东西），`Damage::Execute`（原生代码里是空操作，同步完成），
`DebugOptions.AllowContinueDebate`（`IsDebugMode && s_allowContinueDebate`），
`ChangeCourtFireFxActivity` 构造函数（字段初始化 `Enable = true`），`PrintDebateText.<>n__0`
（`base.Print` 的编译器生成访问器）。顺带补上 `Assembly-CSharp.csproj` 缺失的
`GigaCreation.Tools.Private.PlayableDirectors` 项目引用。
再上一批 +20 个 / +5,099 B：`ExecutionButtonScreen` 整个类（处刑按钮：按住蓄血槽，
满槽后播完成音效、等 `_delayAfterFullFill` 秒、`ResumeAtLabel("ExecutionConfirmed")`；
3 个 async 方法复原为真正的 `async`/`await`，2 个 `IAudioManager` 接口槽 19/20 =
`PlaySfx`/`StopSfx` 逐一从跳板里的槽号常量锚定；字节确认填充速度按帧计、没有乘
`Time.deltaTime`），`SkipDebateButton` 整个类，`DebateTextPrinterPanel::set_Appearance`
（自动属性，dump.cs 的 `<Appearance>k__BackingField; // 0x188` 直接锚定）。
行为全部 Confidence A（个别无法从字节区分的 C# 写法选择记为 B，逐处在源码注释里标明）。
**更正**：本节此前写的"49 个 / 27,405 B（约 85.5%）"是手工累加的，与桶总数口径不一致，
已作废——它把异步方法的 `MoveNext` 字节计入了分子（`MoveNext` 所在的 `<X>d__N` 嵌套类型
在 dump.cs 里命名空间为空，`rank2.py` 按 `WitchTrials.` 前缀过滤时就不在任何桶里），把按
`rank2.py` 规则归入 Naninovel-glue 的 `DebateTextPrinterPanel::Gosub` 算进了本桶，还把 7 B
的折叠平凡构造体（`ranked.json` 的 `aliased`，不在任何桶里）算作一个体。以后进度一律以
`progress.py` 为准，不再手工累加。（初次
提交时有 2 处被误判为需要 Confidence B 的调用目标，实际是虚表基址算错导致的——
已在外部复核后重新独立锚定虚表基址并修正为 Confidence A；另有 1 处 disposal 守卫
遗漏和 1 处多余语句，详见下方"本轮修正"。）最近几批：`DebateTextPrinterPanel` 的
`SetText`/`AppendText`/`RevealMessages`（含其谓词 lambda）/`WatAndRevealAsync`/`Gosub`/
`OnDestroy`/`SetMessages`/`AddMessage`/`Awake`/`Initialize`/`SetFont`/构造函数及 3 个属性
getter，`PrintDebateText` 的 `CompleteOnContinue`/`ShouldPlayAutoVoice`/`WaitAutoVoice`/
构造函数，`DebateText` 的链接点击链，见下方"显示管线"小节。

| 文件 | 完成 / 总数 | 说明 |
|---|---|---|
| `DebateUi.cs` | **14 / 14（整个文件）** | 辩论 UI：倒计时文本、跳过按钮、HP 变化钩子 |
| `Court.cs` | 2 / 3（ctor 是已知的共享空构造体） | 法庭火焰特效 |
| `ChangeCourtFireFxActivity.cs` | 2 / 3（同上） | 火焰开关 Command |
| `ChangeDebateMode.cs` | 1 / 2（同上） | 辩论模式开关 Command |
| `PrintDebateText.cs` | 18（原"~15"总数估计偏低；逐地址核对未做，见"尚未解决"第 3 条） | 辩论文字打印 Command（脚本内 2,003 次调用）——`Execute()`/`Print()` 均已复原成结构化 `async`/`await`（见下方详述）；`ModifyDebatePrinter`（场景位置/旋转/动画种类配置）已复原，**`Execute → GetOrAddPrinter → ModifyDebatePrinter → Print → WaitForDebate` 入口链现已完全闭合**；本轮补完 `CompleteOnContinue`（按 "Continue" 输入提前完成，经 `ref` 改写传入的 token）/`ShouldPlayAutoVoice`/`WaitAutoVoice`（立即完成，不等语音）/构造函数（与 `PrintTextModified` 折叠） |
| `DebateTextPrinterPanel.cs` | 12 / ~23 | 辩论打字机效果：`SetText`（重置整个 `_labels`）/`AddText`（优先级表 #33，只填空闲标签）按 `<br>` 拆行填字；`AppendText` 把文字拼到最后一条消息，**并丢弃其作者**；`RevealMessages` 按当前语言设行距、等布局重建，再对每个已启用标签错开 0.2 s 发起淡入（**不读传入的 `delay`**；`enableDebugSkip` 打开时不错开）；`WatAndRevealAsync` 等待后 0.4 s alpha 0→1；`Gosub`；`OnDestroy` 取消并释放 `_revealCts`；3 个属性 getter。`Awake`/`Initialize`/`SetMessages`/`AddMessage`/`SetFont`/构造函数等仍是空桩 |

### 系统模型：`DebateUi` 完整生命周期（可连续阅读）

```
Initialize()
  -> _skipDebateButton.SetIconShadowEnabled(false)      // 初始外观：跳过图标暗淡

ChangeVisibility(visible, duration, token)               // CustomUI 显示/隐藏总入口
  -> OnVisibilityChangedByCustomUi(visible)
  -> base.ChangeVisibility(...)                          // 交给 Naninovel 处理淡入淡出动画

OnVisibilityChangedByCustomUi(true)                      // 辩论 UI 出现时
  -> _disposables = new CompositeDisposable()
  -> WitchTrialsNaninovelManager.OnRemainingTimeChangedAsObservable()
       .Subscribe(b__18_0).AddTo(_disposables)           // 每次倒计时变化：刷新文本 + 归零时触发 Goto("TimeIsUp")
  -> WitchTrialsNaninovelManager.OnDebateSkippableChangedAsObservable()
       .Subscribe(b__18_1).AddTo(_disposables)           // 是否可跳过变化：跳过按钮 Present()/Dismiss()
  -> _skipDebateButton.OnPointerDownAsObservable()
       .Subscribe(b__18_2).AddTo(_disposables)           // 按下：NaniScriptPlayer.SetSkipEnabled(true)
  -> _skipDebateButton.OnPointerUpAsObservable()
       .Subscribe(b__18_3).AddTo(_disposables)           // 松开：SetSkipEnabled(false)（长按跳过）
  -> NaniScriptPlayer.OnSkip += _skipDebateButton.SetIconShadowEnabled   // 跳过状态 <-> 图标外观双向同步

OnVisibilityChangedByCustomUi(false)                      // 辩论 UI 隐藏时
  -> _disposables.Clear(); _disposables.Dispose(); _disposables = null   // 全部取消订阅
  -> NaniScriptPlayer.OnSkip -= _skipDebateButton.SetIconShadowEnabled

b__18_0(TimeSpan remaining)                               // 倒计时归零时的关键发现
  -> _label.SetText(TimeSpanToText(remaining))
  -> if (remaining > TimeSpan.Zero) return;
  -> new Goto { Path = "System/System_Subroutine.TimeIsUp", ResetState = ["-"] }.Execute().Forget();
     // 计时器不是纯 UI 效果：倒计时归零会直接跳转执行脚本的 TimeIsUp 分支
```

这条链路证实：**辩论倒计时是真正的游戏规则耦合点**，不是装饰性 UI——`TimeIsUp`
标签的具体内容（大概率是"举证失败/超时惩罚"分支）还没有单独反查，列进下面的
下一步。

### `PrintDebateText::CalculateWaitTime` — 一个此前完全不透明的调用链，现已打通

```
CalculateWaitTime()
  -> IsEnabledDebugSkip ? return 0.1f
  -> IsPlayingAutoVoice()?
       -> AudioManager.GetVoiceTrack(AutoVoicePath)            // IAudioManager dump.cs Slot 31
       -> is AudioTrack audioTrack ? return audioTrack.Clip.length   // 有真实语音：等待时长=语音时长
  -> 否则 CalculateWaitTimeFromMessage(Text)
       -> 剥离空白/日文括号引号标点 + 富文本标签（两个静态 Regex，原文本已从数据段
          直接读出，不是猜的）
       -> return cleanedLength * 0.15f + 0.6f                  // 阅读速度估算公式
```

（此调用链最初误判为 `ShouldPlayAutoVoice()` + `BuildAutoVoicePath()`；见下方
"本轮修正" 一节——由外部复核发现并经反汇编+dump.cs 独立核实后更正为
`IsPlayingAutoVoice()`/`get_AutoVoicePath()`，现为 Confidence A。）

**`PrintDebateText` 的静态构造函数（`.cctor`）是原始 ILSpy 桩代码里完全缺失的一个
真实原生方法**——它有自己的 RVA（`0x3236A0`），但 ILSpy 生成 `.cs` 桩文件时把它
漏掉了（大概率认为这类"纯字段初始化"构造函数不值得单独渲染）。这是本轮流程上的
一个提醒：**符号库（`il2cpp_db.py`/`sweep.py`）比 ILSpy 桩文件更权威**，遇到
"这个类应该有 cctor 但 `.cs` 里没有" 的情况，直接查符号库能找到。

### `WaitForDebate` — 完整恢复成 `async`/`await`/`while`，不是 MoveNext 逐状态翻译

已按用户要求把 `<WaitForDebate>d__25.MoveNext`（824 B 状态机）**结构化复原**成：

```csharp
private async UniTask WaitForDebate(float time, [Optional] AsyncToken token)
{
    float elapsedTime = 0f;
    while (elapsedTime < time)
    {
        token.ThrowIfCanceled();
        if (token.Completed) break;
        NaniVarManager.TrySetVariableValue(DebateWaitTimeVarName, time - elapsedTime);
        await AsyncUtils.WaitEndOfFrame(token);
        token.ThrowIfCanceled();
        elapsedTime += Time.deltaTime;
    }
}
```

确认技巧记录下来供后续状态机复原复用：
1. **`sub_XXXXX(cached icall)` 模式可以直接读出 icall 名字符串**——被调用地址附近
   的数据段里存着形如 `"UnityEngine.Time::get_deltaTime()"` 的纯 ASCII 字符串，
   `il2cpp_pe.PE().read(rva, n)` 直接读出来即可，不需要靠猜测/模式匹配。
2. **`AsyncUtils.WaitEndOfFrame(token)` 的识别靠独立反汇编逐字节对比**——常量
   `PlayerLoopTiming.PostLateUpdate`（`0xc`）和构造出的结构体形状，在调用点内联的
   版本和这个方法自己反汇编出来的版本完全一致，才敢确认成具名调用而不是保留在
   raw 状态机层面。
3. **虚方法调用点的 `[klass+base+16*slot]`/`[klass+base+16*slot+8]` 双读取模式，
   读的是同一个 `VirtualInvokeData{ methodPtr; method; }`（16 字节）的两个字段，不是
   相邻两个槽位**——`method`（`MethodInfo*`）在需要按共享泛型方式分派时会被塞进
   调用约定的下一个空闲寄存器（可能是 `rdx`、也可能是 `r8`，取决于该虚方法本身有
   几个显式参数），本轮在 `ModifyDebatePrinter`（`set_Position`/Slot 6）和
   `AddText`（`Graphic.SetVerticesDirty`/Slot 28、`text` setter/Slot 66）里各遇到
   一次，混淆过一次（见"本轮修正"）。这也是"虚表基址 `0x138` 对全二进制通用"这一
   经验结论背后真正的结构性原因，不只是"两次独立锚定恰好给出同一个数"的巧合。
4. **`describe_data` 返回的字符串字面量是已经正确解码的 Python `str`，但这个环境的
   终端/文件重定向会把非 ASCII 字符显示成乱码（`repr()` 也一样会乱码）**——正确
   读法是 `sys.stdout.buffer.write(s.encode('utf-8'))` 直接写字节流，绕过终端的
   错误编码假设；已在 `AddText` 的日语警告字符串上验证过。

### `Print()` — 3 个 await 点，第一次打通"调试跳过自动举证"分支

`<Print>d__21.MoveNext`（RVA `0x324180`，3290 B，本轮单个最大的复原目标）**结构化
复原**成：

```csharp
protected override async UniTask Print(ITextPrinterActor printer, AsyncToken token)
{
    await base.Print(printer, token);
    if (IsEnabledDebugSkip)
    {
        string[] linkIds = s_linkIdRegex.Matches(Text)
            .Where((Match match) => match.Groups.Count == 2)
            .Select((Match match) => match.Groups[1].Value)
            .ToArray();
        if (linkIds.Length != 0)
        {
            NaniVarManager.TrySetVariableValue(DebateWaitTimeVarName, 0f);
            await WitchTrialsScriptPlayer.ProcessAutoObjectionAsync(linkIds);
            return;
        }
    }
    await WaitForDebate(CalculateWaitTime(), token);
}
```

关键发现：**`enableDebugSkip` 打开时，`Print()` 完全跳过正常的计时等待**，改成用
`s_linkIdRegex`（`<link="(\w+?)">`）从台词里抠出所有 `<link="id">` 标记的 id，一次性
交给 `IWitchTrialsScriptPlayer.ProcessAutoObjectionAsync(string[] objectionIds)`（dump.cs
确认为该接口 Slot 0，和该接口仅有的另一成员 `CancelGosub()` 放在一起看，读作"QA/调试
模式下自动化处理证据链接式举证，不必等真人逐个点击"）。这条分支被证实**立即
`return`**（state==1 的 resume label 直接跳到 `SetResult` 收尾，不会落入第 4 步）——
不是靠语义推测，是靠追踪 resume label 的跳转目标验证的。三个 await 点分别在状态机
里对应 state 0/1/2，全部通过"resume 路径落到与同步完成路径相同的合并点"这一方式
交叉验证了控制流，不是逐状态直译。

两个被 ILSpy 完全省略的编译器缓存 lambda（`PrintDebateText.<>c.<Print>b__21_0`/
`b__21_1`，和本文件的 `.cctor` 一样属于"ILSpy 认为不值得渲染的纯样板代码"）逐字节
比对确认为 `match.Groups.Count == 2`（`GroupCollection.get_Count()` 的 35 字节体
完全相同）和 `match.Groups[1].Value`（`Capture.get_Value()` 的 40 字节体完全相同）——
由于 `s_linkIdRegex` 只有一个捕获组，前者对这个正则永远为真，是"忠实但语义上多余"
的防御性代码，如实保留，不做"优化"。

**副产品：修了一个阻塞性的第三方桩代码缺口**——`Naninovel.UniTask.Awaiter`（编译期
引用桩 `src/decompiled/Elringus.Naninovel.Runtime/Naninovel/UniTask.cs`）完全没有声明
基接口列表（应为 `: IAwaiter, ICriticalNotifyCompletion, INotifyCompletion`，dump.cs
和二进制 DummyDll 都确认了这一点），导致任何对裸 `UniTask` 表达式的 `await`
都会报 `CS4027`。这是本轮第一次真正尝试在别处 `await` 一个 `UniTask`（`WaitForDebate`
自己等待的是另一种 `YieldAwaitable.Awaiter`），所以之前没暴露。已按 dump.cs 逐字段
核实后补回接口列表；顺带检查了同一参考树里所有其他 `struct Awaiter` 声明（含
`<Execute>d__19` 会用到的泛型 `UniTask<T>.Awaiter`），确认只有这一处缺失。

### `Execute()` — 2 个 await 点，纠正了一次"逐字节折叠"的过度断言

`<Execute>d__19.MoveNext`（RVA `0x323A90`，1762 B）**结构化复原**成：

```csharp
public override async UniTask Execute(AsyncToken token)
{
    ITextPrinterActor printer = await GetOrAddPrinter(token);
    if (printer is IDebateTextPrinterActor debateTextPrinterActor)
    {
        ModifyDebatePrinter(debateTextPrinterActor);
    }
    await WaitOrForget(new Func<AsyncToken, UniTask>(displayClass.b__0), true, token);
}
```

其中 `GetOrAddPrinter`（dump.cs `PrinterCommand` Slot 20）和 `WaitOrForget`（dump.cs
`Command` Slot 13）都是继承自基类、本类型未重写的虚方法。闭包 lambda
（`_003CExecute_003Eb__0`，RVA `0x323A50`）确认与 `Naninovel.Commands.PrintText` 自己的
`<>c__DisplayClass31_0.<Execute>b__0` **完全共享同一地址**（dump.cs 对这一个地址列出了
两个不同的托管名字）。

**记录一次自我纠正**：第一版注释把这条"lambda 共享地址"的事实过度推广成了
"整个 `Execute()` 方法体和 `PrintText.Execute()` 逐字节折叠、行为完全一样"——这是
外部复核直接指出的错误，凭证据一查就能证伪：`PrintText.<Execute>d__31.MoveNext`
在 dump.cs 里是**另一个地址**（`0x4FF740`），根本没有被折叠。独立反汇编这个地址后
确认：`PrintText` 自己的 `Execute()` 也是同样的"`GetOrAddPrinter` → await →
`WaitOrForget(lambda, true, token)` → await"两段式结构，但**完全没有** isinst 检查、
完全没有调用任何 actor 类型相关的东西。所以准确的结论是：`PrintDebateText.Execute()`
不是"照抄基类"，而是在基类同一个两段式骨架里**插入了恰好一步**辩论专属逻辑
（`IDebateTextPrinterActor` 配置钩子）；`PrintText` 自己 Slot 27–41 的
`ShowPrinter`/`PlayAutoVoice`/`WaitInput`/`AddBacklog` 等等既不在这个 `Execute()`
里，也不在 `PrintText` 自己的 `Execute()` 里被调用——那些是 `Print()`
（Slot 26/35）内部的事，正好就是这个文件里 `Print()` 覆写要接管的那一层。

### `ModifyDebatePrinter` — 此前判定"very hard"暂缓的目标，现已复原

`PrintDebateText::ModifyDebatePrinter`（RVA `0x322B40`，1590 B）**结构化复原**成：

```csharp
private void ModifyDebatePrinter(IDebateTextPrinterActor printer)
{
    if (Assigned(ScenePosition))
    {
        Rect sceneRect = CameraConfigurationUtility.EvaluateSceneRect(true);
        float x = (ScenePosition.ElementAtOrNull(0) != null)
            ? sceneRect.x + ((float?)ScenePosition[0]).Value / 100f * sceneRect.width
            : printer.Position.x;
        float y = (ScenePosition.ElementAtOrNull(1) != null)
            ? sceneRect.y + ((float?)ScenePosition[1]).Value / 100f * sceneRect.height
            : printer.Position.y;
        float z = ((float?)(ScenePosition.ElementAtOrNull(2) ?? (NullableFloat)printer.Position.z)).Value;
        printer.Position = new Vector3(x, y, z);
    }
    if (Assigned(Roll))
    {
        printer.Rotation = Quaternion.Euler(0f, 0f, Roll);
    }
    if (Assigned(AnimationKind))
    {
        if (FastEnum.TryParse<DebateTextAnimationKind>(AnimationKind, true, out DebateTextAnimationKind animationKind))
        {
            printer.AnimationKind = animationKind;
        }
        else
        {
            Warn(string.Format("Invalid animation kind: {0}", AnimationKind));
        }
    }
}
```

三个独立的 `if (Assigned(...))` 块，每处守卫都逐字节比对 `Command.Assigned`
（`parameter != null && parameter.HasValue`）的独立反汇编，并且共享同一个
`data_30DC164` 类型初始化标志——证实这三处确实是同一个内联静态调用，不是
三段各写各的检查：

- **`ScenePosition`**（`DecimalListParameter` = `ParameterList<NullableFloat>`）：
  X/Y 是当前场景矩形的百分比（显式给出时），否则退回 printer 当前位置对应分量；
  Z 是不带缩放的原始值。X/Y 各自编译成**两次独立调用**——`ElementAtOrNull(i)`
  只做判空，真正取值另有一次单独的 `get_Item(i)` 调用；只有 Z 才是真正复用同一次
  `ElementAtOrNull(2)` 结果（经 `??` 直接送进 `Nullable<float>.op_Implicit`，没有
  第二次 `get_Item`）——这个不对称是靠反汇编字节确认的，不是凭源码风格猜的。场景
  矩形来自 `CameraConfigurationUtility.EvaluateSceneRect(true)`（dump.cs 确认的单参
  `Nullable<bool>` 重载，调用点内联构造了一个 `Nullable<bool>(true)`）。`printer.
  Position` 的读（回退分支）走 `IActor.get_Position`（Slot 5）经通用 getter 蹦床
  `sub_39F0`（槽位号经 `edx` 动态传入，不是 `IActor` 专属蹦床）；最终 `printer.
  Position = new Vector3(x, y, z)` 的写走 `IActor.set_Position`（Slot 6），但这次
  是**完全内联在调用点自身**的接口偏移缓存扫描（先查 `[r10+0x12e]` 处缓存的接口
  偏移表，未命中才 `mov r8d, 6` + `call sub_243A20` 解出槽位，再 `call` 解出的函数
  指针）——**没有经过任何共享蹦床**，这一点和下面的 `Roll` 不同。
- **`Roll`**：`printer.Rotation = Quaternion.Euler(0f, 0f, Roll)`——靠读出解析目标
  地址处缓存的 icall 名字符串 `"UnityEngine.Quaternion::Internal_FromEulerRad_
  Injected(...)"` 确认，乘的常量读回来正好是 `Mathf.Deg2Rad`（`0.017453292...`）。
  和 `set_Position` 不同，这次写**确实**经过一个共享运行时蹦床（`call sub_38B0`，
  RVA `0x38B0`）——独立反汇编这个蹦床本体，确认它把 `IActor.set_Rotation` 的槽位号
  （dump.cs Slot 8）硬编码进了自己的机器码（`mov r8d, 8`），而不是像 `sub_39F0` 那样
  作为运行时参数动态传入。**`Position` 和 `Rotation` 这两次写用的是两种完全不同的
  分派机制，不是同一机制换了个槽位常量**——初稿注释曾把两者的蹦床归属写反（说
  `Position` 也走 `sub_38B0`），提交前由外部复核比对反汇编原始记录后指出并订正，
  未进入已提交历史。
- **`AnimationKind`**：`FastEnum.TryParse<DebateTextAnimationKind>(AnimationKind,
  true, out var animationKind)`（dump.cs 确认存在这个确切的 `TryParse<T>(string,
  bool, out T)` 重载）成功则设置 `printer.AnimationKind`（dump.cs
  `IDebateTextPrinterActor.set_AnimationKind`，Slot 1，经通用 setter 蹦床
  `sub_36F0`，槽位号同样是运行时参数动态传入），失败则调用 `Warn(...)`（dump.cs
  `Command.Warn`，Slot 7），格式字符串 `"Invalid animation kind: {0}"` 直接从数据段
  读出——解析结果没有被无检查地使用过。

**编译期额外需要的两处基础设施修复**：
- `NullableFloat -> float` 的隐式转换在三处触发 CS0457（`NullableFloat.implicit
  operator float?(NullableFloat)` 和 `Nullable<float>.implicit operator
  float(Nullable<float>)` 两个候选歧义）——沿用本项目已有的两步转换写法
  `((float?)x).Value`，与反汇编里真实的两步展开（先 `NullableFloat` 自己的转换，
  再一次独立的 `Nullable<float>.op_Implicit`/`.Value`）完全对应。
- `Assembly-CSharp.csproj` 完全没有 `FastEnum.dll` 的 `<Reference>` 条目——DLL 本身
  一直存在于 `src/il2cpp/DummyDll/FastEnum.dll`，只是此前没有任何已复原代码用到过
  它，所以从未暴露。已按现有 `<Reference>` 条目的写法补一条。

**`Execute → GetOrAddPrinter → ModifyDebatePrinter → Print → WaitForDebate` 入口链
现已完全闭合**——这是本轮以"系统级连贯性"而非孤立方法数为目标的一个完整闭环。

### `DebateTextPrinterPanel::AddText` — 优先级表 #33，五处第三方 API 逐字节验证

`DebateTextPrinterPanel::AddText`（RVA `0x367E20`，1322 B）**结构化复原**成：

```csharp
private void AddText(string text)
{
    string[] lines = string.IsNullOrWhiteSpace(text) ? Array.Empty<string>() : text.Split("<br>");
    DebateText[] availableLabels = _labels.Where((DebateText label) => !label.enabled).ToArray();
    if (lines.Length > availableLabels.Length)
    {
        UnityEngine.Debug.LogWarning("渡されたメッセージの行数が未使用ラベル数を超えています。");
        return;
    }
    for (int i = 0; i < availableLabels.Length; i++)
    {
        bool hasLine = i < lines.Length;
        _labels[i].enabled = hasLine;
        _labels[i].alpha = 0f;
        _labels[i].text = hasLine ? lines[i] : string.Empty;
    }
}
```

这是本轮验证密度最高的一个方法——五处非显然识别，每一处都靠独立反汇编对应的
真实方法逐字节比对，而不是靠名字/签名猜测：

- `string.IsNullOrWhiteSpace(text)`：开头的判空 + 逐字符 `Char.IsWhiteSpace` 循环
  和独立反汇编出来的 `System.String.IsNullOrWhiteSpace`（RVA `0xE25060`）结构完全
  相同，连共享的 `data_30D76D8` 类初始化标志位都一致。
- `text.Split("<br>")`：不只是"dump.cs 里唯一匹配的重载"这种签名推断——调用点是
  `String.Split(string, StringSplitOptions)`（RVA `0xE29360`）整个方法体的完整
  内联，连它自己内部（这里恒为死代码，因为 `"<br>"` 是非空字面量）的分隔符判空
  兜底逻辑都原样内联进来了。**这个兜底逻辑正是解开 `data_30D76E0 + 0xb8` 真实
  身份的关键**：它是 `String.Split` 自己用来替换 `null` 分隔符的通用"空字符串"
  静态引用，不是某个具体字面量的缓存槽——纠正了下面会提到的一次提交前误判。
- `.Where(label => !label.enabled)` 的谓词 lambda `<AddText>b__34_0`（RVA
  `0x368A20`）经独立反汇编，与 `UnityEngine.Behaviour.get_enabled`（RVA
  `0x2060B80`）逐字节相同（含共享的 `data_2604C08` 缓存图标符描述符），后面接一个
  `sete al` 取反——即这个 LINQ 过滤收集的是**当前被禁用（"空闲"）的标签**，不是
  当前可见的标签。
- `_labels[i].enabled = hasLine` 与独立反汇编的 `Behaviour.set_enabled(bool)`
  （RVA `0x2060C20`）逐字节相同。
- `_labels[i].alpha = 0f` 与独立反汇编的 `TMP_Text.set_alpha(float)`（RVA
  `0x1EFFDE0`）逐字节相同；这个 setter 自己内部的"新旧值相同就跳过"空操作守卫，
  正是调用点看起来像有一个 `if` 的原因——**不需要在源码层面另写一个 `if`**，那个
  分支完全是内联进来的 setter 自身逻辑。
- `_labels[i].text` 的写入走 vtable Slot 66，对应本文件同目录
  `NaninovelTMProText.cs` 里它自己重写的 `text` setter（同一个 Slot 66）。

**诚实记录一处字节确认、但没有解释掉的行为疑点**：循环的边界来自
`availableLabels.Length`（当前"空闲"标签的数量），但循环体内每次都直接写
`_labels[i]`，不是 `availableLabels[i]`——反汇编在 `0x36813F`/`0x368221` 都是
重新从 `this._labels` 取值，从未使用 `[rsp+0x98]`（`ToArray()` 结果）本身来索引，
只有循环上界用了它。这意味着"有多少空闲槽位"和"写的是哪些槽位"是两件不一定
对齐的事——如果空闲（禁用）的标签不是 `_labels[0..count)` 这样一个前缀，这次
调用就会静默覆盖一个当前正在显示文字的标签。这是否会真的发生，取决于所有调用方
是否都保证调用前先把整个 `_labels` 重置为禁用（这个类自己的 `SetText` 很可能就是
干这个的，但它还是空桩，未证实）。记录为一个字节确认的行为发现，而不是含糊带过。

### 本轮修正（外部复核发现，已核实并修复——见 commit `d457070`）

在把这批代码标为"完成"之后，外部复核（advisor）指出并经独立反汇编+dump.cs
交叉验证确认了四处问题；记录在这里而不是悄悄改写历史，因为这正是"B/C 不能
冒充 A"纪律应该覆盖、但这次在提交前没做到的地方：

1. **`CalculateWaitTime` 的两处虚表槽位号原本都错了**——最初的算法是"假设其中
   一次调用是 `ShouldPlayAutoVoice`（Slot 33），再反推另一次的槽位差"，这是循环
   论证。改为从同一文件里另外两处调用独立锚定虚表基址：`Naninovel.Commands.Goto.Execute`
   （dump.cs Slot 5）在 `[klass+0x188]`被分派，`StringBuilder.ToString()`（Object
   的 Slot 3）在 `TimeSpanToText` 里于 `[klass+0x168]` 被分派——两者都给出基址
   `0x138`（`offset = 0x138 + 16*slot`）。代入后：`[klass+0x3d8]` 实际是 Slot 42
   = `IsPlayingAutoVoice()`，`[klass+0x2b8]` 实际是 Slot 24 = `get_AutoVoicePath()`。
   `ShouldPlayAutoVoice()`（Slot 33）和原先标 Confidence B 的 `BuildAutoVoicePath()`
   （Slot 29）都不是真正调用目标——两者分别偏差 9 个和 5 个槽位。已改为
   `IsPlayingAutoVoice()`/`AutoVoicePath` 属性，Confidence A。
2. **`DebateUi::OnVisibilityChangedByCustomUi` 丢了两处守卫**——`visible=false`
   分支原来无条件执行 `NaniScriptPlayer.OnSkip -=`，而反汇编里 `_disposables==null`
   时会直接跳到函数尾（整个 teardown including `-=` 都跳过）；`visible=true` 分支
   原来无条件 `new CompositeDisposable()` 再订阅四个流，反汇编里其实有一个
   `_disposables != null` 时直接返回的幂等守卫——没有它，第二次 `visible=true`
   会重复订阅全部四个流并泄漏上一个 `CompositeDisposable`。这是一个已提交为
   "14/14 完成"的文件里的真实行为偏差，`dotnet build` 完全测不出来。已按反汇编
   补回两处 `if` 守卫。
3. **`WaitForDebate` 里多写了一行不存在的 `token.ThrowIfCanceled()`**——`await`
   之后那次调用实际操作的是 `[rsp+0x48]`（本地恢复出来的 awaiter 副本内部），不是
   `this.token`（`[rbx+0x28]`，循环顶部那次真正的调用用的是这个）；前者就是
   `await` 自带的 `awaiter.GetResult()`，不是源码里第二次显式调用。已删除多余行。
4. **`TimeSpanToText` 的分钟计算和分隔符写法都不精确**——分钟用的辅助函数
   （`sub_37E2B0`）经独立反汇编 `UnityEngine.Mathf.FloorToInt`（RVA `0x2053640`）
   逐字节比对，92/92 字节完全一致（含内部同一个 `Math.Floor` helper），应写成
   `Mathf.FloorToInt((float)timeSpan.TotalMinutes)` 而不是
   `(int)Math.Floor(timeSpan.TotalMinutes)`；两个 `:` 分隔符走的是
   `StringBuilder.Append(string)` 重载（调用点 `rdx` 里是 `STR ':'` 指针，不是裸
   char 值），应写成 `Append(":")` 而不是 `Append(':')`。

### `DebateTextPrinterPanel` 显示管线 + `PrintDebateText` 尾巴 — 三处字节确认的行为事实

本批 14 个函数体 / 9,664 B。`DebateTextPrinterPanel` 10 个：`SetText` 865 B、
`AppendText` 1,069 B、`RevealMessages` 383+2,411 B、其谓词 lambda 152 B、
`WatAndRevealAsync` 427+1,409 B、`Gosub` 281+953 B、`OnDestroy` 229 B、
`LocalizationProvider`/`NaniVarManager`/`IsEnabledDebugSkip` 三个 getter 202/202/275 B。
`PrintDebateText` 4 个：`CompleteOnContinue` 496 B、`ShouldPlayAutoVoice` 238 B、
`WaitAutoVoice` 65 B、构造函数 7 B。逐方法证据在源码注释里，这里只记结论：

- **`SetText` 与 `AddText` 的下标约定互不兼容。** 两者开头逐字节相同（内联的
  `IsNullOrWhiteSpace` + `Split("<br>")`），但 `SetText` 遍历整个 `_labels` 并按总标签数
  做越界检查，`AddText` 则先 `.Where(!enabled)` 过滤。两处警告字符串是不同的驻留对象
  （经 `il2cpp_db` 字符串表取出，不是看终端乱码）："…ラベル数を超えています。"与
  "…未使用ラベル数を超えています。"，少了"未使用"，独立印证两者比较的对象不同。
  `SetText` 之后空闲标签是数组后缀，而 `AddText` 从 `_labels[0]` 写起。
- **`AppendText` 丢弃作者。** 最后一条消息被替换为 `new PrintedMessage(last.Text + text)`，
  `author` 的两个字段显式清零（`Count == 0` 分支同样）。`Messages` getter 被派发 4 次，
  但它是 `[CompilerGenerated]` 字段读取，C++ 后端会合并冗余派发，调用次数不能当作源码
  结构的证据，所以没有据此编造局部变量。
- **`RevealMessages` 不读自己的 `delay` 参数。** 状态机只提升了 `this` 和 `token`；逐标签
  错开是硬编码的 `i * 0.2f`（常量从 PE 读出为 0.2），`enableDebugSkip` 打开时为 0。每个
  `WatAndRevealAsync` 都被 `Forget()`，所以该方法在所有淡入"开始"时就完成，不等淡入结束。
  上一个 `_revealCts` 被直接覆盖，未取消也未释放；已复原的方法里只有 `OnDestroy` 清理它。
  谓词 lambda 是 `label.enabled`，与 `AddText` 的 `!enabled` 相反。
- `WatAndRevealAsync`：`Naninovel.UniTask.Delay`（`delayTiming` 传 8 =
  `PlayerLoopTiming.Update`，其余为默认值）之后 `LMotion.Create(0f, 1f, 0.4f)
  .BindToColorA(label).ToUniTask(ct)`。
- `CompleteOnContinue` 经 `ref` 改写调用方的 token：保留 `CancellationToken`，把
  `CompletionToken` 链接到下一次 "Continue" 输入；没有该输入采样器时返回装箱的
  `EmptyDisposable`，不改 token。`ShouldPlayAutoVoice` 检查的是 `PlaybackSpot.ScriptPath`
  非空，不是 `PlaybackSpot.Valid`（后者检查行号下标，已独立反汇编）。`WaitAutoVoice`
  立即完成。构造函数与 `PrintTextModified` 折叠。

**工程/第三方桩修正**：`Naninovel.Common` 的 `PooledObject<T>` 有 `IDisposable.Dispose`
却没有声明 `: IDisposable`，导致 `using` 无法编译（与此前 `UniTask.Awaiter`/
`ParameterList<TItem>` 同类）；`Assembly-CSharp.csproj` 缺 `LitMotion.Extensions.dll`
引用（`BindToColorA` 在其中）。

**工具备忘**：`disas.py ... | tee 文件 | head -N` 会因 SIGPIPE 截断落盘内容
（`RevealMessages` 的 MoveNext 第一次只写入 335/617 行）。大函数一律直接重定向到文件。

### 尚未解决的问题（诚实记录，不是回避）

1. **`AddText` 与 `SetText` 的下标约定互不兼容（`SetText` 复原后，疑点更具体了，
   但仍未证实会实际触发）**——`SetText` 遍历整个 `_labels`，把
   `_labels[0..lines.Length)` 启用、其余禁用，所以它之后的"空闲"标签是数组**后缀**；
   `AddText` 用 `.Where(!enabled)` 选出这段后缀计数，却从 `_labels[0]` 开始写。若在
   部分填充的 `SetText` 之后调用 `AddText`，会覆盖正在显示的前缀。两者的调用方
   （`SetMessages`/`AddMessage`，仍是空桩）还没排查，所以是否真会发生仍未知。
2. **`DebateTextPrinterPanel.cs` 剩余尾巴**——`Awake`/`Initialize`/`SetMessages`/
   `AddMessage`/`SetFont`/构造函数/`LabelSpaceData` 两个 getter 仍是空桩（反汇编已
   存到本会话 scratchpad，未写入）。`RevealProgress` get 与 `Appearance` get/set 的
   RVA 解析到的是其它类型共享的折叠体（`VirtualResourceProvider.get_LoadProgress`
   等），不能直接照那几份反汇编写，需要单独处理。
3. **`PrintDebateText.cs` 逐地址核对未做**——本轮补完 `CompleteOnContinue`/
   `ShouldPlayAutoVoice`/`WaitAutoVoice`/构造函数后，文件内非共享空体地址 18 个、
   `RECONSTRUCTED` 标记 16 个（异步方法的 MoveNext 地址并入入口方法计），原表里
   "~15" 的总数估计偏低；是否已无空桩需要逐地址核对，暂不宣称整个文件完成。

（`ModifyDebatePrinter` 已复原，`Execute → GetOrAddPrinter → ModifyDebatePrinter →
Print → WaitForDebate` 入口链已完全闭合；`SetText`/`AppendText`/`RevealMessages`/
`WatAndRevealAsync`/`Gosub` 与 `PrintDebateText` 的四个尾巴本轮均已复原——以上
各条旧待办已解决，不是悄悄消失。）

### 下一批最高价值目标（按价值排序，供下一轮直接开始）

1. `DebateTextPrinterPanel::SetMessages`（1415 B）/`AddMessage`（191 B）——它们是
   `SetText`/`AddText` 的候选调用方，能直接回答上面第 1 条是否会实际触发
2. `DebateTextPrinterPanel` 其余：`Awake`（786 B）/`Initialize`（830 B）/`SetFont`（907 B）
3. `PrintTextModified::ShouldPlayAutoVoice`（RVA `0x3251A0`）——与本轮
   `PrintDebateText` 的同名重写对照；以及 `PrintDebateText.cs` 逐地址核对

（`StageCameraController::ShakeCameraAsync` 已复原——见下方 §7.6，`_shakeCameraTasks`
的 3 槽已完全解释清楚，`_modifyCameraTasks` 的空闲第 4 槽也已 grep 穷尽排查
（`private` 字段，除 `Awake`/`ModifyCameraAsync` 外无第三处读写），从"未知"降级为
"证据已穷尽、原意是否故意留白已不可考"，不再是待办项。`PerspectiveStageManager`
整个类——构造函数、`StageOffset` 属性、以及全部 7 个接口方法——本轮也已全部复原，
见下方 §7.6，不再是待办项；`PerspectiveStagePresenter::Awake` 早先揭示的"接线图"
现在有了完整的双端证据：`ModifyCameraAsync`/`ShakeCameraAsync` 这两条调用链已经
从 `PerspectiveStageManager` 一路追踪到 `StageCameraController` 的真实实现，
不是推测。`StageScreenController::Awake` 已复原——同一个 `sub_297400` 数组分配
helper，比 `StageCameraController::Awake` 简单（没有任务槽表要预分配）；
`StageCameraController::OnDestroy`/`StageScreenController::OnDestroy` 也已复原——
两个类共享同一个原生函数（folded body，同一 RVA，因为 `_tokenSources` 在两个类
里都在偏移 0x40），遍历 `_tokenSources` 逐个 `Cancel()`/`Dispose()`/置空，是
`Awake()` 分配那张表的生命周期另一端。`PerspectiveStage` 桶里"入口+接线"这一层
现在已经收尾。）

**约定备忘**：`work/reversing/` 下的 `disas.py` 等脚本依赖 `capstone`，这个环境的
系统 Python 没装；必须用 `E:/Manosaba-VSCodeReady/tools/venv/Scripts/python.exe`
调用，否则会报 `ModuleNotFoundError: No module named 'capstone'`。

## 7.6 `PerspectiveStage` 子系统：已完成，65/65 个函数体 / 27,784/27,784 B（用户优先级第 2 位）

**进度（`work/reversing/progress.py` 测得）**：`PerspectiveStage` 桶 65 个独立函数体 /
27,784 B，**全部复原（100%）**。收尾几批覆盖：全部 Naninovel 舞台命令
（`hideStageScreen`/`showStageScreen`/`stageCamera`/`shakeStageCamera`/`loadStage`/
`unloadStage`/`splitStageScreen`，均为 `WaitOrForget`(vtable 槽 13) + 真正的 `async`/`await`，
接口槽号逐一从跳板常量锚定；`loadStage` 解析失败静默不做事，`stageCamera` 缓动名解析失败
`Warn`），`StageCameraContainer` 全部补间（高度/距离的另两轴硬置 0、旋转拆成容器偏航 +
相机俯仰/横滚并以 `(UniTask, UniTask)` 元组 await、X/Y/Z 抖动/冲击）、`StageScreenContainer`
（分屏四边形：`tan(roll)/16:9*0.5` 倾斜，`tanf` 由级数项 1/3 与有理式路径识别）、
`StageScreenController`（可见性渐变前开相机、淡出完成后关相机；分屏登记到
`ITrialStateManager` 槽 4）、`PerspectiveStagePresenter`（加载时已有舞台则不重载、
`_stageReferences[kind]` 缺项抛异常、`ChangeFireFxActivity` 只查实例根上的 `Court`）。
补上 `Assembly-CSharp.csproj` 缺失的 `Unity.ResourceManager`（`AsyncOperationHandle`）与
`UniTask.Addressables`（`AddressablesAsyncExtensions.ToUniTask`）两个引用。
此前的中间进度（17 个 / 11,705 B，约 42%）为本节开头的测量，已被上述收尾覆盖。**更正**：
此前写的"19 个 / 23,203 B（约 83.5%）"作废——那是按下面已废弃的两条手工约定累加的：
把两个异步方法的 `MoveNext` 字节（不在桶里）计入分子，把 `get_TrialStateManager`
（`rank2.py` 归入 `TrialState` 桶）算进本桶，并把折叠的 `OnDestroy` 算作 2 个体。下面
列举的各条目及其字节数作为"原生代码规模"的叙述性数字保留，但**不能**与桶总数相加比较。
已复原条目：`GosubToModifyPerspectiveStageCamera::Execute`（RVA `0x31D870`，优先级表
#25）、`GosubToSplitPerspectiveStageScreens::Execute`（RVA `0x31E250`，优先级表
#26）、`PerspectiveStagePresenter::Awake`（RVA `0x38D8D0`，优先级表 #27）、
`StageCameraController::Awake`（RVA `0x3846C0`，优先级表 #28）、
`StageCameraController::get_TrialStateManager`（RVA `0x384600`，186 B，不在原始
44 个优先级表里，是跟随调用图顺带发现的）、`StageCameraController::ModifyCameraAsync`
（RVA `0x384B50` 入口 + `0x3850A0` MoveNext，4,999 B，同样不在原始表里）、
`StageCameraController::ShakeCameraAsync`（RVA `0x384E00` 入口 + `0x386190`
MoveNext，4,465 B，同样不在原始表里）、`PerspectiveStageManager` 整个类
（构造函数 358 B + `StageOffset` 属性 19 B + 7 个接口方法共 5,595 B，共 5,972 B，
全部不在原始表里）、`StageScreenController::Awake`（RVA `0x387B10`，156 B，不在
原始表里）、以及 `StageCameraController::OnDestroy`/`StageScreenController::OnDestroy`
（folded body，RVA `0x384A10`，169 B，同样不在原始表里），全部 Confidence A（三个
异步方法收尾的 `using`/`try-finally` 写法选择均为 B，见各自小节）。这是本轮第一次
真正进入这个子系统（此前完全未触碰）。

**进度计数约定（更正版，取代此前的两条手工约定）**：进度只以
`work/reversing/progress.py` 的输出为准，它与 §3 桶总数（`rank2.py` → `ranked.json`）
用同一口径：

- **异步方法只计入口壳**。`MoveNext` 属于编译器生成的嵌套类型 `<X>d__N`，dump.cs 里
  它的 `// Namespace:` 为空，全名不带 `WitchTrials.` 前缀，`rank2.py` 从不把它放进任何桶；
  静态 lambda 容器 `<>c` 同理。把 `MoveNext` 字节加进分子会让百分比虚高（此前
  PerspectiveStage 的 83.5% 实际为 42%）。
- **折叠体按 RVA 去重，只算一行**。`rank2.py` 遇到已见过的 RVA 直接跳过，所以
  `0x384A10`（两个 `OnDestroy` 共享）在 65 个体里只占 1 行，归属列出的第一个类。
- **别名数 > 1 且 ≤ 0x40 B 的共享平凡体不在任何桶里**（`ranked.json` 的 `aliased`），
  复原它们对进度贡献为 0（例如 `PrintDebateText`/`PrintTextModified` 共享的 7 B 构造函数）。
  超过 0x40 B 的共享体仍在 `rows` 里，照常计 1 行（例如 `0x366370`
  `DebateTextPrinterPanel::set_Appearance`，82 B，Debate/Court）。
- **归桶按 `rank2.py` 的首个匹配正则**，不按"它在哪个功能里被用到"：
  `DebateTextPrinterPanel::Gosub` 归 Naninovel-glue，`StageCameraController::get_TrialStateManager`
  归 TrialState。
- 一个体算"已完成"，当且仅当其 RVA 出现在 `src/decompiled/Assembly-CSharp` 某个
  `// RECONSTRUCTED` 标记行或其下一行。

**这不只是"打开了一个新子系统"**：第 4 节频次统计显示 `GosubToModifyPerspectiveStageCamera`
在脚本里出现 **16,542 次**——是全部 43 个 `WitchTrials.*` 自定义命令类里频次
**第二高**的（仅次于 `PrintTextModified` 的 31,575 次），比本轮之前的主攻目标
`PrintDebateText`（2,003 次）高一个数量级。按调用频次算，这是本轮杠杆最高的单个
复原目标，不是"顺手开个新桶"。

### `GosubToModifyPerspectiveStageCamera::Execute` — 揭示了 PerspectiveStage 的一种通用调用约定

```csharp
public override UniTask Execute([Optional] AsyncToken token)
{
    int index = ((int?)Index).Value;
    string targetCharIdxVarName = LookCharacterId.Value.ToLowerInvariant() + "Idx";
    ICustomVariableManager customVariableManager = Engine.GetServiceOrErr<ICustomVariableManager>();
    if (!customVariableManager.TryGetVariableValue<int>(targetCharIdxVarName, out int targetCharIdx))
    {
        Warn("Variable " + targetCharIdxVarName + " does not exist.");
        return default(UniTask);
    }
    StringBuilder stringBuilder = new StringBuilder("ModifyCamera");
    // ...(Composition/RollDirection/ZoomLevel/LabelNameParams 逐个 Append 到 stringBuilder)...
    customVariableManager.TrySetVariableValue("cameraIdx", index);
    customVariableManager.TrySetVariableValue("targetCharIdx", targetCharIdx);
    Gosub gosub = new Gosub
    {
        Path = new NamedString(SubroutineScriptPath, stringBuilder.ToString())
    };
    return gosub.Execute(default(AsyncToken));
}
```

**不是 async 状态机**——没有 `[AsyncStateMachine]`，没有嵌套 `<Execute>d__` 结构体；
反汇编直接证实了这一点：整个方法同步运行，以一次纯粹的尾调用
`return gosub.Execute(default(AsyncToken));` 结束——**自己收到的 `token` 参数在
函数体内完全没被读取过**，调用点现场清零构造了一个全新的 16 字节结构体传进去
（`xorps`+`movdqa` 清零的宽度，逐字节对照 dump.cs 里 `AsyncToken`（两个
`CancellationToken` 字段，各 8 字节，共 16 字节）的真实大小，不是"猜测清零宽度
等于整个结构体"）——也就是说这条 Command 自己的取消令牌被静默丢弃，不会传给它
派发出去的 `Gosub`。

**通用调用约定，现已在两个目标上验证确认（不再是单点推测）**：这类
`GosubTo*PerspectiveStage*` Command 不直接操作摄像机/舞台状态，而是：
1. 读一个"每角色命名"的脚本变量（这里是 `<小写look>Idx`）拿到目标索引；
2. 把参数拼成一段固定前缀+参数序列的字符串（这里是
   `"ModifyCamera-<comp>-<rollDir>-Lvl<n>[-<param>...]"`）；
3. 把索引以两个固定名字（`cameraIdx`/`targetCharIdx`）重新发布成脚本变量，供
   `System/System_Subroutine` 里的具体分支读取；
4. `new Gosub { Path = ... }` 跳转到 `"System/System_Subroutine"`，用第 2 步拼出的
   字符串作为标签后缀，`Execute()` 同步完成、`token` 不转发。

### `GosubToSplitPerspectiveStageScreens::Execute` — 验证了上面的约定，同时抓出一个真实的两命令行为分歧

`GosubToSplitPerspectiveStageScreens::Execute`（RVA `0x31E250`，2338 B）逐字节确认
是同一种骨架，三处真实差异（都独立验证过，不是"假设一致"）：
- 没有 `ZoomLevel` 字段（`LabelNameParams` 挪到了 `+0x50`，正好是
  `GosubToModifyPerspectiveStageCamera.ZoomLevel` 原来的位置——用字段偏移的
  变化本身当证据，不是"读了 dump.cs 类体确认"），所以拼出来的前缀是
  `"SplitScreen-<comp>-<rollDir>[-<param>...]"`，没有 `-Lvl<n>` 这一段。
- 前缀字符串是 `"SplitScreen"` 不是 `"ModifyCamera"`。
- 角色索引变量名用的是 `Naninovel.StringUtils.FirstToLower(LookCharacterId)`
  （只转小写首字母，"Ann" → "annIdx"、但 "AnnSmith" → "annSmithIdx"），不是
  `LookCharacterId.Value.ToLowerInvariant()`（转整个字符串，"AnnSmith" →
  "annsmithIdx"）。

**第一版复原直接照抄了 `LookCharacterId.Value`（沿用另一个类的读法），是错的**——
`FirstToLower` 是个接受 `null` 的普通静态方法，这条路径反汇编里根本没有
"未赋值就抛异常"的守卫（`LookCharacterId` 虽然两个类都标了 `[RequiredParameter]`，
但只有 `ModifyCamera` 的字节层面真的在这次读取上强制了它）；独立反汇编
`StringParameter.op_Implicit` 逐字节比对后，确认正确写法是让隐式转换
`StringUtils.FirstToLower(LookCharacterId)` 自己处理判空——这和本轮反复用到的
"调用点看起来像有守卫，其实是被内联的转换/setter 自身逻辑"是同一类陷阱。

**两个命令会对同一个 `look:` 角色 id 算出不同的变量名**（`ToLowerInvariant` vs
`FirstToLower` 在角色名有多个大写字母时不一致）——查过 `res/Assets/#WitchTrials/
Scenarios/**/*.asset.json` 全部脚本语料后确认：两个命令实际用到的 `look:` 值只有
`Ema`/`Sherry`，都是单大写字母开头，两种转小写方式结果相同——**这处不一致是真实
存在的实现分歧，但在这个游戏的实际内容里是惰性的，不是活跃的脚本 bug**。

**本轮第三处基础设施修复**：`Naninovel/ParameterList.cs`（第三方引用桩）的类声明
完全没有基接口列表（`ParameterList<TItem> : CommandParameter<List<TItem>>`），而
dump.cs 显示真实类型是 `..., IEnumerable<TItem>, IEnumerable`——和本轮已修复的
`UniTask.Awaiter` 缺失基接口列表是同一类问题。桩文件里已经有两个私有的
"乱码命名"占位方法（`System_002ECollections_002E...GetEnumerator`），只是没有
声明成真正的显式接口实现——补上接口列表 + 把这两个方法改写成
`IEnumerable.GetEnumerator()`/`IEnumerable<TItem>.GetEnumerator()` 的显式接口实现
语法即可，不用改动方法体。这一修复让 `foreach (NullableString x in
LabelNameParams)` 不再需要额外的 `(IEnumerable<NullableString>)` 强制转换，直接
匹配反汇编里朴素的 `IEnumerable<NullableString>.GetEnumerator()` 调用。

### `PerspectiveStagePresenter::Awake` — 子系统"常驻状态"一侧的接线图

前两个目标是"命令调用"一侧（脚本触发的 `GosubTo*` Command）；`Awake()`（RVA
`0x38D8D0`，1558 B）是"常驻状态"一侧——揭示了整个 `PerspectiveStage` 子系统在运行时
是怎么接线的：

```csharp
private void Awake()
{
    _screenController.Dismiss();
    _cameraController.Dismiss();
    _stageReferences = _stageData.ToDictionary((PerspectiveStageData data) => data.Kind, (PerspectiveStageData data) => data.Stage);
    _perspectiveStageManager = new PerspectiveStageManager(this, _screenController, _cameraController, this, _stageParent.position);
    ServiceLocator.Register(_perspectiveStageManager);
}
```

**子系统模型**：`PerspectiveStagePresenter` 自己同时实现 `IPerspectiveStageLoader`
和 `ICourtModifier` 两个接口，`_screenController`（`StageScreenController`）实现
`IPerspectiveStageScreenModifier`，`_cameraController`（`StageCameraController`）实现
`IPerspectiveStageCameraModifier`——`Awake()` 把这四个角色一起打包，构造出唯一一个
`PerspectiveStageManager`（同时实现 `IPerspectiveStageManager`/`IPerspectiveStageLoader`/
`IPerspectiveStageScreenModifier`/`IPerspectiveStageCameraModifier`/`ICourtModifier`/
`IService` 六个接口），注册进 `ServiceLocator`。也就是说：**`PerspectiveStagePresenter`
本身不是这个子系统的"大脑"，只是个组装 MonoBehaviour**——真正的状态和行为都封装在
`PerspectiveStageManager` 里，脚本层通过 `ServiceLocator`/`Engine.GetServiceOrErr<
IPerspectiveStageManager>()` 拿到的也是这一个实例。

**复原过程中抓到一次"看起来像跨类访问私有成员"的假象**：`_screenController.Dismiss()`/
`_cameraController.Dismiss()` 在反汇编里表现为直接读取 `StageScreenController.
_generalScreen` 字段、直接调用 `StageCameraController.SetCameraEnabled`——但这两个
成员在 dump.cs 里都标记为 `private`，跨类直接访问在 C# 里根本编译不过。独立反汇编
`StageScreenController.Dismiss()`（RVA `0x3881E0`）后确认它的方法体逐字节等于这里
内联的序列；`StageCameraController.Dismiss()`/`Present()`（RVA `0x385010`/
`0x385000`）是一对 10 字节的尾调用蹦床，分别把 `SetCameraEnabled(false)`/
`SetCameraEnabled(true)` 包成公开的 0 参数方法——所以源码层面写的是这两个公开的
`Dismiss()`，只是它们自己的小函数体又被内联回了调用点，不是真的绕过了访问控制。
`PerspectiveStageManager` 的构造同理：`il2cpp_codegen_object_new` 后直接摆字段，
不是漏看了构造函数调用——独立反汇编它自己的 `.ctor`（RVA `0x3A0B50`）逐字节确认
字段写入顺序和偏移完全一致，构造函数体本身就小到会被内联。

**新发现的 `[SerializeField]` 无守卫解引用**：`_stageParent`/`_screenController`/
`_cameraController`/`_stageData`（`PerspectiveStagePresenter`）和 `_generalScreen`
（`StageScreenController`）共五处，已记入 `RECONSTRUCTION_CLAUDE_NOTES.md` 供并行的
`audit/unity-assets` agent 参考。

### `StageCameraController::Awake` — 预分配任务槽表，留下一个记录在案的未知数

`Awake()`（RVA `0x3846C0`，840 B）**结构化复原**成：

```csharp
private void Awake()
{
    _modifyCameraTasks = new UniTask[_cameraContainers.Length][];
    for (int i = 0; i < _modifyCameraTasks.Length; i++)
    {
        _modifyCameraTasks[i] = new UniTask[4];
    }
    _shakeCameraTasks = new UniTask[_cameraContainers.Length][];
    for (int i = 0; i < _shakeCameraTasks.Length; i++)
    {
        _shakeCameraTasks[i] = new UniTask[3];
    }
    _tokenSources = new CancellationTokenSource[_cameraContainers.Length];
}
```

五处 `new T[n]` 分配全部落到同一个反汇编确认的通用数组分配 helper
（`sub_297400(TYPE, length)`：算 `元素大小*长度+头部`、走 GC 分配器、写回 TypeInfo
和长度——是 IL2CPP 对 `newarr`/`new T[n]` IL 指令的共享 codegen，不是五个不同的
具名 API），`_cameraContainers`（`[SerializeField]`）在这个方法里被独立重新读取
并各自单独判空**三次**（不是缓存进局部变量后复用一次判空）——反汇编逐字确认，
如实保留这个"重复读取"的写法，不做"看起来能简化"的整理。

**当时诚实记录的未解开问题，本轮已解开一半**：`_modifyCameraTasks`/
`_shakeCameraTasks` 每行的长度是字面常量 `4`/`3`，当时既不匹配
`ModifyCameraAsync`（三个 tween 参数）也不匹配 `ShakeCameraAsync`（没有这种参数
结构），留白未猜。下面的 `ModifyCameraAsync` 小节把 `_modifyCameraTasks` 这一半
解开了；`_shakeCameraTasks` 的 3 槽仍未验证。

### `StageCameraController::get_TrialStateManager` — 同一个惰性缓存模式的第三个实例

RVA `0x384600`（186 B）逐字节确认，与 `StageScreenController::get_TrialStateManager`
（RVA `0x387A50`，优先级表 #12）完全同构：

```csharp
private ITrialStateManager TrialStateManager
{
    get
    {
        return _trialStateManager ??= Naninovel.Engine.GetServiceOrErr<ITrialStateManager>();
    }
}
```

`[this+0x28]` 判空、METHODINFO 槽解析到
`Method$Naninovel.Engine.GetServiceOrErr<ITrialStateManager>()`、结果写回字段——
跟第 0 节 TL;DR 里提到的"惰性缓存的服务查找"这一种模式（`_field ??=
Engine.GetServiceOrErr<T>()`）第三次重复出现，没有任何变体。

### `StageCameraController::ModifyCameraAsync` — 单一 await 点，把"4 行任务槽"之谜解开一半

RVA `0x384B50`（680 B，纯 `AsyncUniTaskMethodBuilder.Start<>()` 入口壳，没有自己
的逻辑）+ `0x3850A0`（`<ModifyCameraAsync>d__10.MoveNext()`，4,319 B，真正的方法
体）。初始化块只声明了**一个** `AwaitUnsafeOnCompleted<UniTask.Awaiter,
...d__10>()`——单一 await 点，意味着这 4,319 B 大部分是直线执行的赋值/分支逻辑，
不是真正复杂的状态机，字节数比看起来的更"便宜"。复原结果：

```csharp
public async UniTask ModifyCameraAsync(int cameraIndex, ModifyCameraHeightTween heightTween, ModifyCameraRotationTween rotationTween, ModifyCameraDistanceTween distanceTween, [Optional] CancellationToken ct)
{
    if (cameraIndex < 0 || cameraIndex >= _cameraContainers.Length)
    {
        UnityEngine.Debug.LogWarning($"Camera index {cameraIndex} is out of range.");
        return;
    }

    TrialStateManager.RegisterCameraState(cameraIndex, heightTween?.To, rotationTween?.To, distanceTween?.To);

    if (_tokenSources[cameraIndex] != null)
    {
        _tokenSources[cameraIndex].Cancel();
        _tokenSources[cameraIndex].Dispose();
    }
    _tokenSources[cameraIndex] = new CancellationTokenSource();
    CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_tokenSources[cameraIndex].Token, ct);

    _modifyCameraTasks[cameraIndex][0] = (heightTween != null) ? _cameraContainers[cameraIndex].ModifyHeightAsync(heightTween.To, heightTween.Ease, heightTween.Duration, linkedCts.Token) : default(UniTask);
    _modifyCameraTasks[cameraIndex][1] = (rotationTween != null) ? _cameraContainers[cameraIndex].ModifyRotationAsync(rotationTween.To, rotationTween.Ease, rotationTween.Duration, linkedCts.Token) : default(UniTask);
    _modifyCameraTasks[cameraIndex][2] = (distanceTween != null) ? _cameraContainers[cameraIndex].ModifyDistanceAsync(distanceTween.To, distanceTween.Ease, distanceTween.Duration, linkedCts.Token) : default(UniTask);

    using (linkedCts)
    {
        await _modifyCameraTasks[cameraIndex];
        _tokenSources[cameraIndex].Dispose();
        _tokenSources[cameraIndex] = null;
    }
}
```

**关键发现：`UniTaskExtensions.GetAwaiter(UniTask[] tasks)` 是一个真实存在的
API，不是猜测**——独立反汇编 RVA `0x18203F0`，`dump.cs` 签名确认
`public static UniTask.Awaiter GetAwaiter(UniTask[] tasks)`，只接收数组本身、
没有下标参数。这就是 `await _modifyCameraTasks[cameraIndex];` 这行为什么能通过
编译：数组通过这个扩展方法直接变成可等待对象。这解开了 `Awake()` 留白的"4 行
任务槽表索引什么"的**大半**：三个独立的边界检查字面量（`cmp [row+0x18], 0/1/2;
jbe <越界处理>`）配合每次 16 字节（`UniTask` 结构体大小）的写入偏移
（`row+0x20/+0x30/+0x40`）确认——槽 0 = height tween 任务，槽 1 = rotation，
槽 2 = distance；槽 3（`row+0x50`）在这个方法里**从未被写入**，永远是
`default(UniTask)`，在 `GetAwaiter(UniTask[])` 这种数组整体等待里是无害的填充。
**为什么分配长度是 4 而不是恰好 3，仍然是未解开的尾巴**——`_shakeCameraTasks`
的 3 槽还没有对应的证据（需要反汇编 `<ShakeCameraAsync>d__11.MoveNext()`，RVA
`0x386190`）。

`RegisterCameraState(int, float?, float?[], float?)` 的识别用了两条独立证据
交叉验证：一是跟 `ITrialStateManager` 接口定义逐字段类型比对，是唯一一个形状
匹配的成员；二是虚表槽算术（`mov r8d, 2` 选中接口第 3 个声明成员，0 起始，
正好是 `RegisterCameraState`）。这个接口方法的真实实现
（`TrialStateManager::RegisterCameraState`，RVA `0x39DA50`，见第 5 节试点小节）
本轮之前就已经复原，两端可以互相印证。

三个 tween 分支的参数（`To`/`Ease`/`Duration`）逐一比对了各自 record 类型
（`ModifyCameraHeightTween`/`ModifyCameraRotationTween`/`ModifyCameraDistanceTween`，
均已在更早的批量定案里复原）自己的字段偏移，调用参数顺序则比对了
`StageCameraContainer::ModifyHeightAsync`/`ModifyRotationAsync`/
`ModifyDistanceAsync` 各自的真实签名——height/distance 两路在这个调用点是被
内联的 `Start<>()` kickoff（跟自己的状态机结构体字段偏移逐一核对），rotation
那一路是普通具名调用，同一个类的三个姊妹异步方法在同一个调用点上被内联的
方式不一致，是本轮反复见到的"trivial wrapper 是否内联取决于具体实例化"现象
的又一例。

**收尾部分的证据等级降级说明**：`_tokenSources[cameraIndex]` 的 dispose+置空
在 `linkedCts` 的 dispose 之前执行——这一点是指令顺序直接观察到的，Confidence A。
但 `linkedCts` 的 dispose 每次都通过显式 `IDisposable` 接口调用桩派发（而不是
直接调用 `CancellationTokenSource.Dispose()`），并且在三处独立的退出路径上各自
重复了一份、每份都套了一层 `state < 0` 判断——这只能说明"这是一个跨多个入口点
的编译器生成 `finally`"，**不能**单独区分源码写的是 `using (linkedCts) { ... }`
还是手写的 `try { ... } finally { ((IDisposable)linkedCts).Dispose(); }`（
`CancellationTokenSource` 是非密封类型、`Dispose()` 是虚方法，两种写法在这个
粒度上编译结果相同）。选了 `using` 是因为它最惯用、且完全复现了观察到的顺序
和派发方式；这处收尾写法本身标记为 **Confidence B**，其余全部 A。

**顺带修复一处工程配置缺口**：加上 `await` 那一行以后，编译报
`CS0012`——`AssetBundleRequest`/`AssetBundleCreateRequest` 定义在未引用的程序集
里。用 `System.Reflection.MetadataLoadContext` 反射 `UniTask.dll`（在
`work/reversing/` 之外临时建的检查用小工程，未纳入正式代码）确认：真正的原因是
`Cysharp.Threading.Tasks.UnityAsyncExtensions`（跟已经 `using` 的
`UniTaskExtensions` 同一个命名空间）声明了
`GetAwaiter(AssetBundleRequest)`/`GetAwaiter(AssetBundleCreateRequest)`——C# 的
扩展方法查找要枚举这个命名空间下所有候选类的所有同名方法，即使最终选中的是
`UniTaskExtensions.GetAwaiter(UniTask[])`，光是"看一眼"那两个签名也需要能解析
`AssetBundleRequest` 类型。`Assembly-CSharp.csproj` 里补一条到
`UnityEngine.AssetBundleModule.dll`（`il2cpp/DummyDll` 下已存在，只是从未被引用
过）的引用即可，不是绕过问题的权宜之计。

### `StageCameraController::ShakeCameraAsync` — 与 `ModifyCameraAsync` 同构，顺带解释清楚 `_shakeCameraTasks` 的 3 槽

RVA `0x384E00`（505 B 入口壳）+ `0x386190`（`<ShakeCameraAsync>d__11.MoveNext()`，
3,960 B，单一 await 点）。控制流跟 `ModifyCameraAsync` 逐字节同构——边界检查、
`_tokenSources` 取消/释放/重建/链接、收尾 `using (linkedCts)` 全部照搬同一套
证据（只是状态机字段偏移不同，见方法自带的注释）：

```csharp
public async UniTask ShakeCameraAsync(int cameraIndex, Vector3 force, float duration, int frequency, float dampingRatio, bool randomly, [Optional] CancellationToken ct)
{
    if (cameraIndex < 0 || cameraIndex >= _cameraContainers.Length)
    {
        UnityEngine.Debug.LogWarning($"Camera index {cameraIndex} is out of range.");
        return;
    }

    if (_tokenSources[cameraIndex] != null)
    {
        _tokenSources[cameraIndex].Cancel();
        _tokenSources[cameraIndex].Dispose();
    }
    _tokenSources[cameraIndex] = new CancellationTokenSource();
    CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_tokenSources[cameraIndex].Token, ct);

    _shakeCameraTasks[cameraIndex][0] = _cameraContainers[cameraIndex].ShakeCameraXAsync(force.x, duration, frequency, dampingRatio, randomly, linkedCts.Token);
    _shakeCameraTasks[cameraIndex][1] = _cameraContainers[cameraIndex].ShakeCameraYAsync(force.y, duration, frequency, dampingRatio, randomly, linkedCts.Token);
    _shakeCameraTasks[cameraIndex][2] = _cameraContainers[cameraIndex].ShakeCameraZAsync(force.z, duration, frequency, dampingRatio, randomly, linkedCts.Token);

    using (linkedCts)
    {
        await _shakeCameraTasks[cameraIndex];
        _tokenSources[cameraIndex].Dispose();
        _tokenSources[cameraIndex] = null;
    }
}
```

两处跟 `ModifyCameraAsync`不同、都已验证的地方：一是一次性初始化块里完全没有
出现 `ITrialStateManager_TypeInfo`，说明摇晃相机从不写持久化的每相机试炼状态
（只有"相机在哪"的 height/rotation/distance 状态才写）；二是 `force` 是必填的
`Vector3` 结构体，不可能为 `null`，所以三条轴向调用无条件全部执行（没有
`ModifyCameraAsync`里那种按 tween 是否赋值决定要不要跑的三元表达式）。

**`_shakeCameraTasks` 的 3 槽现在完全解释清楚**：`force.x`/`.y`/`.z`（状态机偏移
`0x30`/`0x34`/`0x38`，正好是 `Vector3` 自己的内存布局）分别喂给
`StageCameraContainer.ShakeCameraXAsync`/`ShakeCameraYAsync`/`ShakeCameraZAsync`
（逐一比对过这三个方法各自的真实签名和状态机字段偏移），存进
`_shakeCameraTasks[cameraIndex][0]`/`[1]`/`[2]`——三个轴，三个槽，不多不少，
不需要填充位。

**这也让 `_modifyCameraTasks` 空闲第 4 槽的追问可以收尾了**：`_modifyCameraTasks`
是 `private` 字段，全仓库 grep 只有 `Awake()`（分配）和 `ModifyCameraAsync`
（写槽 0/1/2）两处读写，`ShakeCameraAsync` 只碰自己独立的 `_shakeCameraTasks`。
既然已经排查了这个字段在代码里**唯一**可能写它的两个方法，且都不写槽 3，就没有
第三个"漏查的消费者"可以再去找了。第 4 槽是否是原作者故意留的余量（比如曾经
计划过第 4 种 tween）还是单纯多分配了一格，从代码本身已经不可考——但"从这两个
方法的证据看，槽 3 永远是 `default(UniTask)`"这一点现在是完全确定的，不再是
"未排查"，是"排查完了、答案就是没有第三方写它"。

### `PerspectiveStageManager` 整个类 — 子系统"大脑"其实是一层薄转发

构造函数（358 B）、`StageOffset` 属性（19 B）、以及全部 7 个接口方法（共
5,595 B）本轮全部复原。结果是：这个被 `PerspectiveStagePresenter::Awake`
称为子系统"大脑"的类，其实每个方法都只做一件事——把调用原样转发给对应的
子接口字段：

```csharp
public PerspectiveStageManager(IPerspectiveStageLoader loader, IPerspectiveStageScreenModifier screenModifier, IPerspectiveStageCameraModifier cameraModifier, ICourtModifier courtModifier, Vector3 stageOffset)
{
    _loader = loader;
    _screenModifier = screenModifier;
    _cameraModifier = cameraModifier;
    _courtModifier = courtModifier;
    StageOffset = stageOffset;
}

public async UniTask LoadStageAsync(PerspectiveStageKind kind, [Optional] CancellationToken ct) => await _loader.LoadStageAsync(kind, ct);
public void UnloadStage() => _loader.UnloadStage();
public async UniTask ChangeScreenVisibilityAsync(int screenIndex, bool visible, float duration, [Optional] CancellationToken ct) => await _screenModifier.ChangeScreenVisibilityAsync(screenIndex, visible, duration, ct);
public void SplitScreen(int screenIndex, float normalizedPosX, float roll, ScreenSplitDirection direction) => _screenModifier.SplitScreen(screenIndex, normalizedPosX, roll, direction);
public async UniTask ModifyCameraAsync(int cameraIndex, ModifyCameraHeightTween heightTween, ModifyCameraRotationTween rotationTween, ModifyCameraDistanceTween distanceTween, [Optional] CancellationToken ct) => await _cameraModifier.ModifyCameraAsync(cameraIndex, heightTween, rotationTween, distanceTween, ct);
public async UniTask ShakeCameraAsync(int cameraIndex, Vector3 power, float duration, int frequency, float dampingRatio, bool randomly, [Optional] CancellationToken ct) => await _cameraModifier.ShakeCameraAsync(cameraIndex, power, duration, frequency, dampingRatio, randomly, ct);
public void ChangeFireFxActivity(bool active) => _courtModifier.ChangeFireFxActivity(active);
```

（实际源文件里每个方法保留展开的方法体和各自的证据注释，这里只是压缩展示
形状；`StageOffset` 是 get-only 自动属性，由构造函数直接赋值。）

**验证方法，不是按名字猜**：每一条转发都独立确认了两件事——(1) 被调用的
是接口的哪个成员，靠的是 IL2CPP 共享的"接口槽缓存"跳板（项目里反复出现的
"universal vtable base 0x138"手法）解析出的成员索引（`r8d`/`ecx` 的字面值），
逐一比对各接口自己声明顺序里第 N 个成员是谁；(2) 每个参数的寄存器/栈来源，
通过跳板自己的栈帧大小算术（`push rdi; sub rsp, N` ⇒ 跳板的 `[rsp+X]` 对应
调用者的 `[rsp+(X-栈帧总大小)]`）逐一映射回调用者传入的具体字段，逐字节确认
落点跟接口签名声明的参数顺序一致。`ModifyCameraAsync` 用的跳板
（`sub_3C80`）逐一核对到底；`ShakeCameraAsync` 用的跳板（`sub_3B50`）结构
不同（`Vector3 power` 按指针传递给本地副本），本轮也完整算过一遍栈帧偏移，
不是靠跟 `ModifyCameraAsync` 结构相似就假设过关。

**这条链现在完全闭合，不是假设**：`PerspectiveStagePresenter::Awake`
（本轮更早复原）里的构造调用是
`new PerspectiveStageManager(this, _screenController, _cameraController, this, _stageParent.position)`
——也就是说运行时 `_cameraModifier` 就是 `_cameraController`（一个
`StageCameraController` 实例），`_loader`/`_courtModifier` 都是
presenter 自己。所以 `PerspectiveStageManager.ModifyCameraAsync` →
`StageCameraController.ModifyCameraAsync`、
`PerspectiveStageManager.ShakeCameraAsync` →
`StageCameraController.ShakeCameraAsync` 这两条调用链，从脚本层一路到
相机控制器的真实实现，现在是逐字节追踪确认过的完整闭环，不是"结构相似所以
大概率是它"。

### `StageScreenController::Awake` — 同一个数组分配 helper，但没有任务槽表

`StageScreenController::Awake`（RVA `0x387B10`，156 B）比
`StageCameraController::Awake` 简单得多：同一个共享 `sub_297400(TYPE, length)`
数组分配 helper（调用形状完全一致），但这个控制器没有 `_modifyCameraTasks`/
`_shakeCameraTasks` 那种按屏幕预分配的任务槽表要建，只有：

```csharp
private void Awake()
{
    _tokenSources = new CancellationTokenSource[_screenContainers.Length];
}
```

`_screenContainers`（`[SerializeField]`）被无条件解引用取 `.Length`（内联的
NRE 抛出桩），已记入 `RECONSTRUCTION_CLAUDE_NOTES.md`。

### `StageCameraController::OnDestroy` / `StageScreenController::OnDestroy` — 折叠体，`Awake` 分配的表在这里被回收

两个类的 `OnDestroy` 是同一个原生函数（RVA `0x384A10`，169 B）——反汇编的
`managed:` 行同时列出两个类名，这是 §1.3 讲过的函数体折叠现象，成立的前提是
两个类在这个函数唯一访问的字段（`_tokenSources`）上共享同一个
`FieldOffset`（都是 `0x40`，已在两份 `FieldOffset` 表里逐一核对）。逐字节复原为：

```csharp
private void OnDestroy()
{
    for (int i = 0; i < _tokenSources.Length; i++)
    {
        if (_tokenSources[i] != null)
        {
            _tokenSources[i].Cancel();
            _tokenSources[i].Dispose();
            _tokenSources[i] = null;
        }
    }
}
```

判别式证据：循环边界（`_tokenSources.Length`）在每次迭代前都重新从
`this` 取字段并重新判空，不是循环外缓存到局部变量一次——这排除了
`int n = _tokenSources.Length;` 提到循环外的写法，坐实了"每次都走属性/索引器"
这一形状。`Cancel()`/`Dispose()` 是两次独立的索引器读取，不是缓存同一个引用两次
调用：两者之间反汇编重新加载并重新判空了一次该槽位（编译器无法证明
`Cancel()` 没有把槽位置空，所以必须再判一次）。清零槽位调用的
`sub_2390(array, index, value)`——这个引用数组元素带 GC 写屏障的存储 helper——
在 `ModifyCameraAsync` 自己的 MoveNext 里独立交叉确认了两次（`new
CancellationTokenSource()` 存入槽位、以及末尾存 `null`，两处的寄存器摆法都是
`rcx`=数组、`rdx`=下标、`r8`=值），不是凭印象假设的。这是 `Awake()` 分配
`_tokenSources` 表的生命周期另一端：组件销毁时把还没完成的每个 token source
取消、释放、清空引用。

## 7.7 `Naninovel-glue` 子系统：已完成，66/66 个函数体 / 21,262/21,262 B（用户优先级第 3 位）

`work/reversing/progress.py Naninovel-glue` 测得 66/66、21,262/21,262 B（100%）。全部
`WitchTrials.*` 合计 252/929 个函数体、95,243/316,778 B。本节只记本轮新增部分；此前已完成的
`StartTrial`/`ChangeTrialPauseMode`/`GosubToObjectionCutIn`/`WitchTrialsNaninovelPresenter`
等见前文各节。

**`WitchTrialsScriptPlayer`**
- `ResetService`（`0x39C1C0`）：中段与基类 `ScriptPlayer.ResetService`（`0x5C4AE0`）逐字节相同，
  即内联的 `base.ResetService()`；前后用 `_dontDisableAutoPlayAndSkip = IsSoftResetting` 包住，
  于是**软重置期间基类重置不会关掉 skip/auto play**（本类的 `DisableSkip`/`DisableAutoPlay`
  覆写在该标志为真时什么都不做）。
- `GetSkipAllowed`（`0x39C890`）：`DebugOptions.DebugSkip` → 辩论中（`IsInDebate`）→
  `SkipMode == Everything` → 否则只允许跳过已读行 `HasPlayed(PlayedScript.Path, PlayedIndex + 1)`。
- `ProcessAutoObjectionAsync`（`0x39C560` + MoveNext `0x39CAE0`）：先取消旧 `_cts`、新建 `_cts`；
  对每个异议 id 依次 `await new Gosub { Path = new NamedString(null, id) }.Execute(token)`，
  随后 `ThrowIfCanceled` 并 `await UniTask.Yield(PostLateUpdate, token)`；循环结束
  `_cts.Dispose(); _cts = null`（**无判空**）。没有 try/finally：被取消时异常直接进
  SetException，`_cts` 由取消方负责释放。

**`WitchTrialsChoiceHandlerManager`**：构造函数内联了基类 `ChoiceHandlerManager` 构造
（`PickedChoices` 字段初始化器 + `ActorManager` 构造 + `resources`/`l10n`）；
`SaveServiceState`/`LoadServiceState` 用 `SettingsWitchTrials` 存取"关键选项提示"开关，
读不到状态时回落到 `Configuration.DefaultCriticalChoiceHintEnabled`；
`SetCriticalChoiceHintEnabled` 写字段后同步触发 `OnCriticalChoiceHintChanged`。

**`WitchTrialsNaninovelManager` 余下成员**：`StartGameScenarioName` =
`DebugOptions.StartGameScenario ?? base.StartGameScenarioName`（尾段与基类 getter `0xD026C0`
逐字节相同）；`OnEnterTitleScreenAsObservable` =
`_isInTitleScreen.SkipLatestValueOnSubscribe().Where(x => x).AsUnitObservable()`；
构造函数只有四个字段初始化器（`_isInTitleScreen` 显式传 `false`，`_trialTimer = new Timer(true)`）。

**`ScriptHelper`**：正则 `^[A-Z][a-z]*(\d+)_[A-Z][a-z]*(\d+)_([A-Z][a-z]*)(\d+)$`（`Compiled`）；
`TryParseScenarioName` 四个分组**无短路地全部解析**后再无分支地 AND，成功才构造 `ScenarioInfo`。

**`GosubResetWitchBook`（`@gosubResetBook`）整类**：
- `Execute`：从 `PlaybackSpot.ScriptPath` 末段解析场景名，失败则 `Warn`（"魔女図鑑リセットが
  想定されていないシナリオで実行されました。"）并结束；调试选项 `CheckWitchBookState` 开启且证据
  状态有变化时导出脚本，若 `Part == Trial` 则到此结束；`IsDebugDirectJump` **显式为 false 时结束**；
  其余交给内联的 `base.Execute`（`Gosub.Execute`，`0x4EFC80` 对照）。
- `TryGetScriptPathAndLabel`：标签为 `Reset{Act}-{Chapter}-{Part}` 或加上 `Version` 参数对象本身
  （`{3}` 走参数对象的 ToString），其余与基类一致。
- `CheckEvidenceState`：按类别取魔女图鉴当前生效证据（按 Id 排序）做快照，与上次快照比较长度与
  逐项 `IdVersionPair.Equals`（lambda 与 `0x3A2E80` 逐字节一致）；首次调用恒为 true；无论结果都会
  覆盖快照。
- `ExportScriptFile`：调试用，向桌面 `ResetWitchBook.nani` 追加一段能重建当前图鉴状态的脚本
  （`# 场景名`、`@clearBook`、每项 `@update {id} category:{cat} version:{ver}`、`@return`），
  `await using StreamWriter`。
- `<>n__0`（`0x329360`）是 async 方法里 `base.Execute` 的编译器访问器。

**`ScenarioInfo` 改回位置记录（更正）**：8 个合成成员（`EqualityContract`/`ToString`/
`PrintMembers`/`GetHashCode`/两个 `Equals`/`<Clone>$`/`Deconstruct`）逐一与 Roslyn 记录降级形态
核对一致，原声明是 `public record ScenarioInfo(int Act, int Chapter, ScenarioPartKind Part, int Section);`。
此前注释称"存在真实 `set` 访问器，所以 backing field 不是 readonly"是错的：那是 `init` 访问器，
dump.cs 的 `readonly` 标记是对的。三个 `ModifyCamera*Tween` 记录保留非位置写法，只更正了指向
`ScenarioInfo.cs` 的注释。

**顺带复原**：`DebugOptions.DebugSkip`/`StartGameScenario`/`CheckWitchBookState`（均为
`IsDebugMode && s_x` 形状）。

**编译基线变化**：警告数 144 → **143**，差值是旧 `ScenarioInfo` 桩里 `Equals(ScenarioInfo? other)`
的 CS8632，改成位置记录后由编译器合成、不再有该注解；0 error 不变。


## 7.8 `Save/Load` 子系统：已完成，31/31 个函数体 / 10,941/10,941 B（用户优先级第 4 位）

`progress.py SaveLoad` 测得 31/31、10,941/10,941 B（100%）；全部 `WitchTrials.*` 合计 278/929、104,318/316,778 B。

**命令**
- `@quickSave`（`QuickSave`）：先写自定义变量 `lastQuickSaveSlotNumber`，再在
  `NaninovelLoadingScreen.Show(LoadingMode.Subtle, haltInput: false)` 期间调用
  `IClickThroughPanelExtended.DontBlockInteractionOnNext()` 并等待
  `IStateManagerExtended.QuickSaveAsync(SlotNumber)`；`Wait` 参数经 `WaitOrForget` 决定是否阻塞脚本。
- `@quickLoad`（`QuickLoad`）：槽位优先取参数，否则读变量 `lastQuickSaveSlotNumber`；
  **两者都没有时只 `Warn("No quick save slot number found.")` 然后照常用 0 号槽继续**；
  `Show(LoadingMode.Long, haltInput: true)` 期间等待 `QuickLoadAsync`。
- `@openLoadUi`（`OpenLoadUi`）：取 `ISaveLoadUI`，`PresentationMode = Load`，再等待 Cysharp
  `ManagedUiExtensions.PresentAsync(ui, null, token.CancellationToken)`。
- 字节里的 `Array.Empty<ICancelEventBlockable>()` 是 `Show(..., params ICancelEventBlockable[])`
  省略 params 实参的编译器降级，源码不写第三个实参。

**`SaveLoadUi` 结构**
- `Awake`：`_panels = _panelReferences.ToDictionary(r => r.Mode, r => r.Panel)`，
  `SetPresentationMode(Load)`，最后 `base.Awake()`。`PresentationMode` setter 与
  `SetPresentationMode` 是同一个原生函数体（缺面板时 `Debug.LogWarning("The panel for {0} does not exists.")`，但仍然赋值）。
- `Initialize`：分别以 `SaveSlotLimit` 初始化 Save/Load 两个面板的网格
  （Save 面板点击→`HandleSaveSlotClicked`，Load 面板点击→`HandleLoadSlotClicked`，删除和取档回调共用），
  `Dismiss` 所有面板，`await UniTask.WhenAll(...)`；不调用 `base.Initialize()`。
- `OnShow` 订阅的 lambda（`<OnShow>b__52_0`）：UI 可见且计数 >0 时显示 `LoadingMode.Short` 遮罩
  （以 `_naninovelScreen` 作为 blockable），计数回到 ≤0 时释放。**此前这里是一个 Confidence D 的占位方法
  `OnShowSlotCountChanged`（`throw NotImplementedException`），本轮已按字节复原并内联回 lambda。**
- `LoadSaveSlotAsync`：槽位不存在返回 null；计数器存在时 `Value++`，`try { await Load } finally { Value-- }`
  （finally 无判空）。
- `FindFocusObject`：当前模式面板网格里 `activeInHierarchy` 的槽位中取 `LastSelectTime` 最大者的
  `gameObject`（严格大于才替换）。

**三条 async 流程**
- 读档 `HandleLoadSlotClickedAsync`：播放读档音效 → `NaninovelInteractionBlocker` 期间：淡出 →
  `_naninovelScreen.DismissAsync(0f)`（不等待）→ 标题 UI 若可见则 `DismissAsync(0f)`（不等待）→
  `SetTitleScreenShown(false)` → `UniTask.Delay(_loadDelay)` → `IStateManager.LoadGame(slotId)` →
  淡入 → `IInputManager.ProcessInput = true`。
- 存档 `HandleSaveSlotClickedAsync`：先播放确认音效；槽位已存在时弹覆盖确认（取消即返回）；
  播放存档音效；`CreateLinkedTokenSource(ct)` + `CancelAfterSlim(_saveGameTimeout)`；
  `LoadingMode.Short` 遮罩期间启动 `SaveGame(slotId)`，并以
  `UniTask.Yield(Update, linkedCts.Token)` 轮询完成——**Yield 的 GetResult 会抛取消异常，所以存档超时表现为整个流程被取消**；
  完成后把新状态 `BindSlot` 到所有面板。
- 删档 `HandleDeleteSlotClickedAsync`：确认音效 → 弹删除确认（取消即返回）→ `DeleteSaveSlot` →
  所有面板 `BindSlot(slotNumber, null)`。
- 三个 `HandleXxxClicked` 同步入口都是 `HandleXxxClickedAsync(slotNumber, destroyCancellationToken).Forget()`。

---

## 7.9 `WitchBook/Data` 子系统：已完成（用户优先级第 5 位）

按顺序推进：模型层 → 五个 Page → ItemButton/Thumbnail → WitchBookScreen → WitchBookUi → 三个命令。

**第 1 组：模型层（22 个函数体，另加 bucket 外的 `IdVersionPair` 6 个）**
- `VersionedState`：
  - `_list` 由构造函数初始化为 `new List<IdVersionPair>(64)`。依据是内联的 List 构造直接以长度 64 调用数组分配器；无参构造只会引用静态空数组，不会分配。
  - `ActiveItems` = `_list.Where(p => p.Version > 0).ToList()`。
  - `SetVersion`：倒序查找 `EqualId`，命中则整项替换为新 `IdVersionPair`，否则 `Add`。
  - `Copy`：`Clear` 后设 `Capacity`，再逐项 `IdVersionPair.Clone`。
  - `Clone` = `new` + `Copy(this)`；`Validate` = `List.Contains`（走 `IEquatable`）；`IsNull` = `_list == null`。
- `IdVersionPair`：
  - `Equals(other)` / `Equals(x, y)` 是 IDE 生成的相等模板，后者多一次 `GetType()` 比较。
  - `GetHashCode(obj)` = `HashCode.Combine(Id, Version)`。
  - `EqualId` 是 `string.Equals(Id, id, Ordinal)`。它带 CompareInfo/CultureInfo 元数据初始化，`==` 版本没有，行为同为序数比较；写法定 B 级。
- `WitchBookDataHelper`：
  - `BuildClueTextureAddress("1-2")` → `"General/WitchBook/Clue_001_002"`：共享 StringBuilder，按 `'-'` 切分，各段 `PadLeft(3,'0')`。
  - `BuildProfileTextureAddress` → `"General/WitchBook/Profile_" + id`。
  - `ParseCategories`：`FastEnum.TryParse`（区分大小写），失败时 `Debug.LogError("Invalid WitchBook category name: X.")` 并跳过。
- 数据类构造：
  - `AuthorData`/`CharacterData`/`SpecialThanksData` 只有 `_items = new List<>()` 字段初始化器；`SpecialThanksData._textSizeByCourses` 没有初始化器。
  - 五个 `XxxData : WitchBookDataBase<T>` 构造为空，`_items` 由 Unity 反序列化填充。
  - `CharacterDataItem`/`RuleDataItem`/`SpecialThanksDataItem` 构造按参数顺序存字段。

**第 2 组：五个 Page + `WitchBookPageBase`（`Reset`/`Show`）**

方法：先完整解码 `CluePage`，再用 `pagediff.py`（归一化地址/类型名后逐行 diff）对其余四页做字节级比对，只精读有差异的部分。

- `LoadDataAsync`：全部是 `var result = await base.LoadDataAsync(ct);` 后建本地化查找表，最后 `return result;`。
  - Clue/Note 的入口、`<>n__0`、`b__N_0`、`b__7` 与 Clue 逐字节相同，Note 的 MoveNext 只差写入偏移（+0xC8/+0xD0）。
  - 查找表形如 `ToDictionary(v => v.IdVersionPair, v => names.Keys.Union(descriptions.Keys).ToDictionary(l => l, l => new LocalizedTexts(names.GetValueOrDefault(l) ?? "", ...)))`。
  - Rule 多一张 `_numberings` 表（`Item.Numbering`）。由于 C# 不合并相同 lambda，`b__10_0` 与 `b__10_2` 字节相同。
  - Map/Profile 只有一层 `ButtonText`/`Description` 的 `ToDictionary(Locale, Text)`，两者的 lambda 逐字节相同。
  - Profile 先 `new AuthorTextBuilder(_authorTextSettings)` 并启动 `InitializeAsync(ct)`（不 await），等基类加载完成后再 `await` 该任务，两者并发。
- `SetupItemButton`：
  - Clue/Profile 走贴图（`BuildClue/ProfileTextureAddress`）。
  - Note 用 `SetupWithText(title, 0)`。
  - Rule 用 `SetupWithText(GetTitleText() + "<br>" + numbering, 2)`。
  - Map 用 `SetupWithText(text, text.Contains("<br>") ? 2 : 1)`（原生编译器折叠为 `movzx; inc`）。
- `RefreshPageContent`：
  - 两级 `IReadOnlyDictionary` 索引器（接口槽 2 = `get_Item`，几个 thunk 均已核对）。
  - Note 标题先经 `s_tagPattern`（`new Regex("<.+?>", Compiled)`）去标签。
  - Map 遍历 `_floors`：匹配的层 `Show(_restrictionAreas)`，其余层 `Hide()`。
  - Profile 优先 `TryBuildAuthorText(id, UseCharacterColor ? NameColor : null)`，失败时回退 `BuildTaggedSimpleName(GetAuthorName(id), null)`。
- `RulePage.GetTitleText`：逐指令内联了 Naninovel `TextManagerExtensions.GetRecordValue`（0x596010，同一元数据标志），值为空时回退 `_managedTextProvider.DefaultValue`。
- 三个 `LocalizedTexts` 嵌套类改写为位置记录。编译器生成的 `LocalizedTexts?` 不再产生 CS8632，**警告基线 143 → 140**。

**第 3 组：列表项控件（ItemButton / Thumbnail / Description / List / SubjectLabel）**

- `SetupWithAddressableTexture` / `Thumbnail.Setup` 流程：
  - 内联 `StateLoading()`：默认贴图、`alpha = 0`、不可交互。
  - `_cts?.Cancel()` 后 `new CancellationTokenSource()`。
  - `GetOrLoadAddressableAsset<Texture2D>(address, _cts.Token).ContinueWith(SetTexture).Forget()`。
  - `SetTexture`：`alpha == 0f` 时设 1f 并可交互；`!activeSelf` 时 `SetActive(true)`。
  - icall 名称已从 PE 字符串核对：get_alpha / set_alpha / set_interactable / get_activeSelf / set_enabled。
- `SetupWithText`：用 `for` 循环（每次迭代重读数组）设 `enabled = i == backgroundIndex`。
- `ClearContent` 不取消进行中的加载。
- `WitchBookItemDescription.SetText` 用的是 `_autoLayoutSupporter` 自己的 `destroyCancellationToken`，不是 `this` 的。
- `WitchBookItemSubjectLabel.SetText` 首字放大：
  - 长度 > 1：`"<size=+{0}>{1}<space={2}><size=+0>{3}"`。
  - 长度 = 1：`"<size=+{0}>{1}"`。
  - `_fontSizeDiff` 字段初始值为 30f。

**第 4 组：`WitchBookScreen`（22 个函数体 + 4 个 MoveNext）**

- 三个惰性 getter：`InputManager` / `DialogProvider`（服务按 `IDialogManager` 查找，存为 `IDialogProvider`，与 SaveLoadUi 同形）/ `NaniAudioManager`；三个 `[CompilerGenerated]` 自动属性（+0xF8/+0x100/+0x108）。
- 生命周期覆写：`BeginToPresent`/`EndToPresent`/`EndToDismiss` 内联了 NaninovelScreen 基类体（与 0xD3CF30/0xD3CF70/0xD3D330 逐字节一致），写作 `base.X()`；只有 `BeginToDismiss` 真正调用基类（0xD3D180）。
- `BeginToPresent`：先取 `IsPresentEvidenceMode`（槽 1）再取 `IsInDebate`（槽 0），`or` 只控制 `ChangeTrialPauseMode(true, true)`；alpha、`blocksRaycasts` 与 `new CancelEventBlocker(new ICancelEventBlockable[] { this })`（数组 ctor 0xCDB6B0）只看 `IsPresentEvidenceMode`。
- `InitializePages`：每次迭代新建 DisplayClass42_0 捕获类别；受限类别隐藏标签与页面；`MapPage` 走 `InitializeAreas(RestrictionAreas)`；`Toggle.Set(true, true)` 即内联的 `isOn = true`；订阅 `OnValueChangedAsObservable()`，回调播放 `ResponseKind.Tab` 音效并 `ChangePage`，`AddTo(_disposables, this)`。
- `PlayCancelSfx` 直接丢弃 `PlaySfx` 返回的 UniTask（无 `ForgetCore`，与 SaveLoadUi 不同）。
- `ChoiceEvidenceAsync`（MoveNext 0x37C580）是重试循环：确认对话框非 Submit 时清空 blocker/dialog 后回到取消检查并再次 `OnPresentEvidenceAsync`；`<>7__wrap5` 是 finally 中 await 的返回标志。`ProvidableDialog` 只实现 `IUniTaskAsyncDisposable`，故写成显式 `try/finally { await dialog.DisposeAsync(); }`（`NaninovelInteractionBlocker` 用无参 ctor 0xCFBE30）。
- `OnPresentEvidenceAsync`：链接 CTS + `UniTask.WhenAny(UniTask<string>[])`，取胜者后 `Cancel()`。`ChoiceAreaAsync`：当前页不是 `MapPage` 时输出日文警告并返回 0。
- 两条日文字面量取自 stringliteral.json 解码，而非反汇编注释。

**第 5 组：`WitchBookUi`（12 个函数体 + 4 个 MoveNext）**

- `Initialize`：先把 `base.Initialize()` 存入 `<result>5__2`（dump.cs 中本类有 `<>n__0` 访问器，指向 CustomUI.Initialize 0x3483B0，其 `return default` 被内联成空 awaiter），再 `await _witchBookScreen.InitializeAsync(destroyCancellationToken)`（Cysharp，已内联），最后 `await result`。
- 三个 `Set*` 与 `GetActiveEvidences`/`UpdateVersion`/`ClearState` 都是对 WitchBookScreen 的转发，被转发目标（自动属性 setter、`PageData[category].Page.X()`）整体内联。
- `ChoiceEvidenceAsync`/`ChoiceAreaAsync`：`using` 一个 `token.CreateLinkedTokenSource()`，把其 Token 交给 Screen 的同名方法并 await。
- `SetFont` 与 DebateTextPrinterPanel.SetFont 同形：内联的 `base.SetFont` + 对 `SelectMany(config => config.Components)` 逐个 `ChangeFontMaterialAsync(...).Forget()`（lambda 折叠在 0x368A00）。
- `SerializeState`：基类调用后 `new GameState { ... = RestrictionX?.ToArray() }`，`SetState` 内联为 `Dictionary.set_Item`。`DeserializeState`：`await base.DeserializeState`，`GetState<GameState>()` 为空时两项置 null，否则 `?.ToList()`（`new List<T>(array)` 内联）。

**第 6 组：`WitchBookButtonUi` 与三个命令（ShowWitchBookUi / ClearWitchBook / UpdateWitchBook）**

- 惰性 getter 均为 `Engine.GetServiceOrErr<T>()`，`WitchBookUi` 为 `NaniUiManager.GetUI<IWitchBookUi>()`（内层 getter 内联）。IWitchBookUi 接口槽：0 SetPresentationMode、1 SetRestrictionCategories、2 SetRestrictionMapAreas、3 GetActiveEvidences、4 ChoiceEvidenceAsync、5 ChoiceAreaAsync、6 UpdateVersion、7 ClearState。
- `WitchBookButtonUi.Start`：`_button.onClick.AsObservable().Subscribe(_ => ...).AddTo(this)`。lambda（0x3776F0）播放 `_openSfxPath`（结果丢弃），重置为 Local / 无限制，再 `PresentAsync(null, destroyCancellationToken).Forget()`（CancellationToken 重载）。CustomUI 与 ScriptableUIBehaviour 都不声明 Start，基类为 Unity 空的 UIBehaviour.Start，故不写 base 调用。
- `ShowWitchBookUi`：
  - `Execute` 传入 `Global ? Global : Local`、`Assigned(CategoryNames) ? ParseCategories : null`、`areas = null`；`Back` 在此不读。
  - `ShowWitchBook` 依次调用槽 0/1/2，再 `await PresentAsync(null, token)`（AsyncToken 重载，已内联）。
  - `IsEnabledDebugSkip` 为 `TryGetVariableValue<bool>("enableDebugSkip", out v) && v`，原生编译器折叠为 `and al`，写法 B。
  - 构造函数只有 `Back = false` / `Global = false` 两个字段初始化。
- `ClearWitchBook`：
  - `IsAct2Chapter1Adv` 与 GosubResetWitchBook 同一前缀（`ScriptPath.Split('/')` 取最后一段 → `TryParseScenarioName`），判定 Act 2、Chapter 1、Part Adv。
  - `Execute` 在 `DebugOptions.CheckWitchBookState` 为真时，Act2-1 ADV 清全部类别，否则清一个零初始化元素的数组（`{ Clue }`，写法 B）；非调试时按 CategoryNames 或全部类别逐个 `ClearState`。
- `UpdateWitchBook`：`FastEnum.TryParse` 失败时 `Err("Invalid category: {0}. Valid values are: {1}")`，但仍继续执行 `UpdateVersion(category, Id, Version)`，此时 category 为 0。
- `ChoiceEvidence.<Execute>b__2_0`（0x326010）已在 Execute 中内联书写，只在 RECONSTRUCTED 行补记 RVA。

进度：`progress.py WitchBook/Data` 测得 **121/121、39,146/39,146 B（100%）**；全部 `WitchTrials.*` 合计 406/929、144,435/316,778 B。

## 7.10 `UI` 子系统：已完成，165/165 个函数体 / 50,436/50,436 B（用户优先级第 6 位）

**第 1 组：小型控件与 Page 构造函数**

- 基类私有成员一律经受保护访问器内联出现，逐个反汇编确认后改写为访问器，而非直接访问字段：
  - `SlideToggle`：`IsActiveSelectableUtil`（0xCF00B0）、`OnHighlightingChangedAsObservable()`（0xCF0A80，SelectableUtility +0x10）、`OnValueChangedAsObservable()`（0xCF0A30）。
  - `RebuildableButton.Label`（0x44CE10 = +0x40）、`RebuildableLabel.UiLabel`（+0x20）。
- `WitchTrialsTabToggle`：
  - `OnDisable` 逐字节内联 SlideToggle.OnDisable（清空基类 +0x138），再清空本类 +0x180 的 `_disposables`，因此是两个不同的 CompositeDisposable。
  - 构造函数中 +0x130/+0x138 的写入是基类字段初始化器；只有 +0x180 属于本类，写作字段初始化器。
  - `OnHighlightingChanged` 为 `value | isOn`。
- `CanvasGroupButton.OnStateChanged`：
  - 高亮 = Highlighted(1) 或 Pressed(2)；枚举值由 dump.cs 常量与 SlideToggle.DoStateTransition 的分派顺序共同确认。
  - 禁用 = `_ignoreParentDisabling ? !interactable : state == Disabled(4)`。读的是 +0xD8 的 `m_Interactable`，不是 `IsInteractable()`。
- `ToggleImageSwitcher.OnValueChanged` 与 `Change` 共用 RVA 0x30FA50（折叠），补丁改为按方法签名匹配，避免同一 RVA 被重复命中。
- `WitchTrialsButton`：
  - `_defaultSize = new Vector2(505f, 120f)`，由 `struct` 解码 0x43FC8000/0x42F00000 得出。
  - `RebuildElements` 在基类调用之前就读取了 `Label as WitchTrialsButtonLabel` 与 `buttonData.ResponseKind`，故写为局部变量（B）；响应为 Submit 时调用 `ModifyEmphasizedText`。
- `WitchTrialsButtonLabel.ModifyEmphasizedText`：
  - 单字与多字两个 `string.Format` 在原生层合并为同一个 FormatHelper 调用点（尾合并），源码写作 if/else。
  - 首字以装箱 `text[0]` 传入；`Substring(1)` 的写法为 B。
  - 构造函数与 RebuildableLabel..ctor 折叠，+0x48 = 1 是基类的初始化器。
- 四个 Page 构造函数（各 62 B）只调用 `WitchBookPageBase<TItem, TState>..ctor`。

**第 2 组：AutoToggleUi / AdvChoiceHandlerButton / MPImageAppearanceChanger 族 / TrialChoiceHandlerPanel**

- `AutoToggleUi`：
  - `OnEnable`/`OnDisable` 为 `base` 调用加 IScriptPlayer 槽 10/11 的 `OnAutoPlay` 订阅与退订。
  - `Initialize` 订阅 `_toggle.OnValueChangedAsObservable()`。lambda（0x336F80）是本类的实例方法，每次经 getter 取播放器，所以是 lambda 而非绑定到播放器的方法组。随后 `SetIsOnWithoutNotify(AutoPlayActive)`（`Toggle.Set(v, false)` 内联）。
  - `HandleAutoModeChanged` 设置 `isOn`，再 `SetSkipEnabled(false)`（槽 34）；`Visible`（ScriptableUIBehaviour 槽 28）为真时播放 Submit 音效。
- `AdvChoiceHandlerButton`：
  - `OnEnable`/`OnDisable` 的 `base` 为内联的 `ScriptableUIControl<Button>.OnEnable/OnDisable`（对 this 调用槽 60/61 `BindUIEvents`/`UnbindUIEvents`）。之后在 hint CanvasGroup 存活时增删 `OnCriticalChoiceHintChanged`（IWitchTrialsChoiceHandlerManager 槽 2/3）。
  - `Initialize` 逐字节内联 `ChoiceHandlerButton.Initialize`（0x615400），再按 `CriticalChoiceHintEnabled` 设置 hint 透明度。
  - `OnButtonClick` 只播放 SFX 并 `Forget`；基类 `ScriptableButton.OnButtonClick` 是共享空函数体 0x30C000，无从判断源码是否调用了 base（B）。
- `MPImageAppearanceChanger`（基类，progress 计入 Other）：
  - 下标越界或长度不足时 `Debug.LogError(string.Format(...), this)`。
  - bool 版本报错时输出两数组长度的较小值（`cmp` + `cmovge`），写作 `Mathf.Min`（B）。
- `LabeledMPImageAppearanceChanger`：`Reset` 与 `ChangeAppearance(int)` 内联基类体，bool 版本则真实调用基类，随后对 `_labelColors` 做同样的范围检查。
- `TrialChoiceHandlerPanel` 构造函数与 Naninovel 的 ChoiceHandlerPanel 构造函数折叠，本类没有自己的初始化器。

**第 3 组：回想面板（WitchTrialsLogUi / WitchTrialsLogMessageUi）与 WitchTrialsTextPrinterPanel**

- **`AuthorPanel` / `AuthorNamePanel` 的覆写是共享的 `return null` 函数体（0x33E0C0）**，本类自己读基类字段。基类私有字段在字节里是直接加载的，但只有非虚的 `base.AuthorPanel`（BacklogMessageUI 0x471780 = +0xA8）与 `base.AuthorNamePanel`（RevealableTextPrinterPanel 0x4933A0 = +0x158）的 getter 内联后才会出现这种加载，两处 getter 均已反汇编确认，故写作 `base.X`。
- **作者文本流程与 ProfilePage 同形**：`GetActorMetaOrDefault` → `UseCharacterColor ? NameColor : null` → `TryBuildAuthorText`，失败时回退 `GetAuthorName` + `BuildTaggedSimpleName`；名字为空白时写入显式 null。
  - 打印面板的回退名优先取 `Author.Label`；它为 `IsEmpty` 时才用 `CharacterManager`（槽 104）按 id 查名。
  - 文本不为空白时，设置铭牌 CanvasGroup 的 alpha，并写入 `AuthorNamePanel.Text`（槽 5）。
  - 回想消息改为设置标签文本，并以 `!string.IsNullOrEmpty(AuthorId)` 调用 `base.AuthorPanel.SetActive`。
- **两个 `Initialize` 的写法**：
  - 共同步骤：先保存 `base.Initialize()`（经 `<>n__0` 内联，回想面板为 `BindInput("ShowBacklog", Show)` 与 `BindInput("Cancel", Hide)`，槽 93/47/48），直接新建 `AuthorTextBuilder`，`await InitializeAsync(destroyCancellationToken)`，最后 `await task`。
  - 打印面板另有：`_changeScaleByAppearances.ToDictionary(Appearance, Targets)`。
- **`SetActiveAppearance`**：按当前外观名 `TryGetValue`，把每个目标 Transform 的 `localScale` 设为 `(Scale.x, Scale.y, 0)`。icall 名 `Transform::set_localScale_Injected` 从解析字符串读出。
- **`WitchTrialsLogUi.SpawnMessage`**：基类调用后，若 `LastMessage` 为 `WitchTrialsLogMessageUi`，则调用 `ModifyAuthorPanel`。
- **`WitchTrialsLogMessageUi` 构造函数**：+0x90/+0xA0 的写入是 BacklogMessageUI 的初始化器，本类没有自己的初始化器。

**第 4 组：HintDisplay（提示应用，19 个函数体 + 3 个 MoveNext）**

- **`Present`**：
  - 清空文本，切换两个 CanvasGroup，`RequestNextMessage` 抽取一条消息（icall `Random::RandomRangeInt`）。
  - `_cts?.Cancel()` 后新建 CTS，并 `_cts.AddTo(destroyCancellationToken)`：Cysharp `CancellationTokenExtensions.AddTo` 已内联，即 `RegisterWithoutCaptureExecutionContext` 配静态 `disposeCallback`。
  - 最后 `MainLoopAsync(_cts.Token).Forget()`。
- **`MainLoopAsync`**：循环执行 `Yield`；消息为空则 `WaitWhile`，再 `ShowTextAsync`；随后 `WaitContinueOrBreakAsync` 为假时退出。循环结束后内联 `Dismiss`（`Close` + `OnClose?.Invoke()`）。
- **`ShowTextAsync`**：逐字追加并调用 `SetText(StringBuilder)` 与 `ForceMeshUpdate`，按 `_waitOnChara` 加字符表额外时间 `Delay(RoundToInt(x*1000))`。
  - `[数字]` 标记：`TryParse` 成功时 `WaitForSeconds`，随后跳过标记。
  - 任一按钮被按下（`TextSkip = 7`）时，用正则去掉剩余标记后一次性写出，再 `WaitForSeconds(_waitSkip)`。
- **`WaitContinueOrBreakAsync`**：每帧清空所选按钮；Witch → true，WordPrinter → false。
- **按钮接线**：关闭按钮经 UniRx 订阅 `Dismiss`（lambda `b__30_0` 与 `Dismiss` 折叠为同一函数体）；其余三个按钮用 `AddListener` 写入 `_selectedButtonKind`。
- **构造函数**：只有字段初始化器 `0.1f` / `0.5f`，以及 `{ '。', '】' }` 各 0.3f 的等待表。

**第 5 组：ControlPanel（游戏内菜单，20 个函数体 + 9 个点击 lambda + 1 个 MoveNext）**

- **`Start`**：先 `Close()`，再为各按钮订阅 `onClick.AsObservable()` 并 `AddTo(this)`：
  - 回想、存档、读档、设置：先播放 Submit 音效、`Close()`，再 `PresentAsync(null, destroyCancellationToken).Forget()`。存档/读档在此之前设置 `SaveLoadUi.PresentationMode = Save/Load`（ISaveLoadUI 槽 1）。
  - 返回标题：调用 `ReturnToTitleAsync(...).Forget()`。
  - 开/关按钮与遮挡按钮：播放对应 SFX（结果丢弃）后 `Open`/`Close`。
  - 提示入口：`_hintDisplay.Present()`、隐藏 home 组，并设 `_hintDisplay.OnClose = Open`。
- **`OnEnable`/`OnDisable`**：`NaniInputManager?.GetSampler("Pause")`（Naninovel IInputManager 槽 6），非空时增删 `OnStart += ToggleVisibility`。
- **`Open`/`Close`**：切换 `_isOpened` 与四个 CanvasGroup，`_naviAnimator.SetBool(s_blink, …)`。
  - `Open` 另外关闭自动与快进（IScriptPlayer 槽 35/34）。
  - `Close` 另外关闭提示显示。
  - `s_blink` 为静态初始化器 `Animator.StringToHash("Blink")`。
- **`ReturnToTitleAsync`**：外层 `using` 阻塞器，内层 `using` 对话框。确认结果为 Cancel 时直接返回；否则对话框先释放，再在阻塞器作用域内 `await _returnToTitleScript.PlayAsync(ct)`。
- **构建修正**：`PresentAsync` 是 `GigaCreation.NaninovelExtender.Ui` 中的扩展方法，补上该 using 后编译通过。

**第 6 组：TitleUi（标题画面，22 个函数体 + 6 个点击 lambda + 3 个 MoveNext）**

- **访问器**：
  - `InputManager` / `DialogProvider` 用 `ServiceLocator.Get<T>(true)` 懒加载（DialogProvider 实际取 `IDialogManager`）。
  - 四个 Naninovel 服务用 `Engine.GetServiceOrErr<T>()`。
  - `GalleryUi` / `OptionsUi` 为 `NaniUiManager.GetUI<ICGGalleryUI / ISettingsUI>()`。
  - `CanLoadGame` = `TryGetVariableValue<int>("g_gameProgress")` 成功且值 > 0。
  - `Visible` 读 `_visibleSelf`，写入时经 vtable 槽 45 转 `SetVisibility`。
- **`Awake`**：`base.Awake()` 之后无条件 `_googleForm = null`。原始字节 `48 C7 83 08 01 00 00 00…` 可证实。因此本构建里 Activate 的 GoogleForm 分支永远不会执行。
- **`ChangeVisibility`**（Naninovel builder）：
  - 停止淡入淡出 tween，写 `_visibleSelf`，调 `HandleVisibilityChanged`（槽 55）。
  - CanvasGroup（槽 42）为空则返回。`DisableInteraction`（槽 33）为 false 时设 `blocksRaycasts`。
  - `ControlOpacity`（槽 43）为 false 则返回。
  - `time = duration ?? FadeTime`（槽 19）。time 为 0 时直接写 alpha。
  - 否则构造 `FloatTween(alpha, target, new Tween(time, _visibleSelf ? 显示缓动 : 隐藏缓动, IgnoreTimeScale, true), SetOpacity)`，并 `await RunAwaitable(tween, asyncToken, this)`。
  - `Tween.Scale` 直接接收 `IgnoreTimeScale`，未取反。
- **`SetVisibility`**：上述逻辑的同步版本，alpha 直接取 1/0。
- **`Activate`**：每个按钮都是 `onClick.AsObservable().Subscribe(lambda).AddTo(_disposables, this)`，`Deactivate` 调 `_disposables.Clear()`。
  - 读档按钮仅在 `CanLoadGame` 时订阅：播放 Submit 音效，然后 `LoadGameAsync`。
  - 新游戏：`PlaySfx(_startGameSfxPath)`，然后 `StartGame`（内联）。
  - 图鉴、设置：`PresentAsync(null, dct)`。
  - 退出：`QuitGameAsync`。
  - GoogleForm：`OpenGoogleFormAsync`。
- **`StartGame`**：`SetTitleScreenShown(false)`（IWitchTrialsNaninovelManager 槽 3），`nextScenario` ← `StartGameScenarioName`（INaninovelManager 槽 0），`ResumeAtLabel("StartGame")`。
- **`LoadGameAsync`**：`PresentationMode = Load` 后 `await SaveLoadUi.PresentAsync(null, ct)`。
- **`QuitGameAsync`**：与 ControlPanel.ReturnToTitleAsync 同构。确认结果为 Cancel 时返回；否则先释放对话框，再在阻塞器作用域内 `ResumeAtLabel("QuitGame")`。

**第 7 组：OptionsUi（设置画面，15 个函数体 + 19 个订阅 lambda + 3 个 MoveNext + 5 个闭包/静态 lambda）**

- **访问器**：
  - GigaCreation `ILocalizationManager` 与 `INaninovelManager` 走 `ServiceLocator.Get<T>(true)`。
  - `ICameraManagerExtended`、`IAudioManager`、`IScriptPlayer`、`ITextPrinterManager`、`ILocalizationManagerExtended` 走 `Engine.GetServiceOrErr<T>()`。
- **`Initialize`**（Naninovel builder）：
  - 先取 `base.Initialize()`，最后再 await。CustomUI.Initialize 被内联成 default，因此状态机只存了 0。
  - 可用语言：调试模式下取 `AvailableLocales`，按 `Languages.GetAll()` 中 tag 的顺序排序；否则取 `ValidLocales`。
  - 语言下拉框用 `Languages.GetNameByTag` 生成名称。字体由 `EnumerableAsyncExtensions.Select(locales, locale => GetOrLoadFontAssetAsync(locale.ToLocaleKind(), dct))` 并发加载，经 `LocalizationDropdown.SetOptions(names, fonts)` 写入。
  - 分辨率与帧率下拉框分别用 `"{0} x {1}"`、`"{0} fps"` 格式化。
  - `_unavailableResolutionOptionCount` = 配置的分辨率数 − 实际可用分辨率数。
- **`ActivateUi`**：`_disposables` 已存在时直接返回；否则先 `ApplyOptionValuesToUi()`，再建立 19 个订阅。
  - 滑块：值 ÷ 10 写入文本速度、自动延迟和四路音量。
  - 开关组与下拉框：先播放 Submit 音效，再写入对应设置。
  - 三个重置按钮：从 `IEngineService<TConfig>.Configuration` 的 Default* 字段恢复默认值，然后重新 `ApplyOptionValuesToUi()`。
  - `_toggleSkipModeToggleGroup` 订阅的是空 lambda，函数体折叠到 0x30C000。
  - 分页：`for` 循环中每次迭代生成一个闭包；Toggle 打开时播放音效并调用 `page.SetActive(enable)`；初始状态为 `SetActive(i == 0)`。
- **`ApplyOptionValuesToUi`**：
  - 滑块值为 `Mathf.RoundToInt(x * 10f)`。0x355C90 即 `(int)Math.Round(double)`。
  - 开关组用 `bool ? 1 : 0`。
  - 语言下拉框的索引为 `_availableLocales.IndexOf(SelectedLocale)`。
  - 分辨率下拉框的值为 `ResolutionLevel - _unavailableResolutionOptionCount`。
- **`ChangeLocaleAsync`**：在 `NaninovelLoadingScreen.Show(Short, true, 空数组)` 的 using 范围内依次隐藏标题、`await SelectLocale(locale)`、恢复标题。
- **`SaveAsync`**：若上一次保存的 `_saveUcs` 仍为 Pending，先在加载画面下等待它；然后新建 UCS，`await SaveSettingsAsync(ct)`，最后 `TrySetResult()`。`Save()` 即 `SaveAsync(dct).Forget()`。
- **构建修正**：`Naninovel.Common` 也定义了 `Naninovel.LinqUtils`（但没有 IndexOf），直接写 `LinqUtils.IndexOf` 会报 CS0433。改用扩展方法写法后，解析到的是唯一的 Elringus 版本；按 TypeDefIndex 4038 判断，二进制里用的正是该版本。
- **补登**：ControlPanel.Start 的 lambda RVA 移到 RECONSTRUCTED 标记行上，`b__47_7/8` 因此计入进度。

进度：`progress.py UI` 测得 165/165、50,436/50,436 B（UI 子系统完成）；全部 `WitchTrials.*` 合计 576/929、195,947/316,778 B；警告基线 140。

## 7.11 `TrialState` 子系统收尾：已完成，18/18 个函数体 / 6,293/6,293 B

试点（§5）之后，这个桶还剩 6 个函数体。选它是因为它能在一次提交内收尾，而且依赖的接口此前都已恢复。

- **`WitchTrialsNaninovelManager.ChangeTrialMode`**（0x399940）：
  - `enable` 为 true 时尾跳到 `StartTrial`。
  - 否则执行 `_isTrialMode.Value = false`。
- **`WitchTrialsNaninovelManager.SetTrialTimer`**（0x3999B0）：
  - 先取 `_trialTimer`，再调 `TimeSpan.Interval((double)time, 1000)`，即内联的 `TimeSpan.FromSeconds`。
  - 然后 `Timer.SetTime(...)`，最后 `_pauseRequestCount = 0`。
- **三个命令的 `Execute`**：
  - 共同结构：先 `ServiceLocator.Get<IWitchTrialsNaninovelManager>(false)`（`cl = 0`，与各处懒缓存用的 `true` 不同），再对参数调用 `Naninovel.Nullable<T>` 隐式转换，最后返回 default（已完成的 UniTask）。
  - `trialMode`：先把槽 2 `IsPresentEvidenceMode` 设为 false，再调槽 4 `ChangeTrialMode(Enable)`。
  - `pauseTrial`：调槽 7 `ChangeTrialPauseMode(Enable, ProcessTimer)`。
  - `timer`：调槽 5 `SetTrialTimer(Time)`。
- **`ChangeTrialPauseMode..ctor`**（0x325690）：
  - 这个函数体与 ArrangeCharacters、ClearChoiceHandler、ChangeActorMaskability 的构造函数折叠在一起。
  - 内容是 `BooleanParameter.op_Implicit(true)` 写入 +0x38，再调 `Command..ctor`。
  - 在本类中 +0x38 即 `ProcessTimer`，因此写作 `ProcessTimer = true`。它与 `[ParameterDefaultValue("true")]` 一致。

进度：TrialState 18/18；全部 `WitchTrials.*` 合计 582/929、197,140/316,778 B；0 错误，警告基线 140。

## 7.12 `Input` 子系统收尾：已完成，25/25 个函数体 / 5,482/5,482 B

- **`WitchTrialsActions`（Unity Input System 生成代码）**：
  - `bindingMask` / `devices` 的 get/set 与 `FindBinding`，都和 `UnityEngine.InputSystem.DefaultInputActions` 共用同一函数体（折叠），内容是对 `asset` 的直接转发。
  - getter 内联了 InputActionAsset 的字段读取（bindingMask 在 +0x38，DeviceArray 在 +0xA8）；setter 在 `DeviceArray.Set` 成功后调 `LazyResolveBindings`。
  - 5 个 `*Scheme` getter 采用生成器的固定写法：索引字段（+0xE8..+0xF8）为 -1 时 `FindControlSchemeIndex("Keyboard&Mouse"/"Gamepad"/"Touch"/"Joystick"/"XR")`，再返回 `asset.controlSchemes[index]`。
- **`WitchTrialsInputManager`**：
  - ctor：先调基类 `InputManager<WitchTrialsActions>` 的 ctor，再保存 `_naninovelActionMapName`。
  - `PlayResponseSfx`：`_naniAudioManager` 为空时，内联 `Engine.TryGetService<IAudioManagerExtended>`（METHODINFO 可证）；取不到时 `Debug.LogWarning("Naninovel AudioManager not found.")` 并返回，取到时缓存。随后调槽 0 `PlayResponseSfx(kind)`。
  - `BuildActionNameOrId`：`actionMapName` 为 null 时换成 `_naninovelActionMapName`，其余部分是基类实现（0x832780，已单独反汇编比对）的内联：映射名为 null 或全空白时返回 actionName，否则返回 `map + "/" + name`。
- **`DebugTextPreferenceNaninovelInputState`**：
  - `NaniInputManager` 为惰性缓存（+0x60）。
  - `LabelText` = `Engine.Initialized ? BuildText() : "Naninovel has not been initialized"`。
  - `BuildText`：清空 StringBuilder，输出 `ProcessInput: O|X |`，再对 `_bindingNames` 做 foreach（数组只加载一次），逐项输出 `| name: O|X `（`IsSampling`，槽 9），最后 `ToString().Trim()`。
  - ctor：`_builder = new StringBuilder()`。

进度：Input 25/25；全部 `WitchTrials.*` 合计 599/929、200,954/316,778 B；0 错误，警告基线 140。

## 7.13 `Audio` 子系统收尾：已完成，18/18 个函数体 / 5,268/5,268 B

- **`PlaySfxModified`（`sfx` 命令，继承 Naninovel `PlaySfx`）**：
  - `Play` 的结构与原版一致：
    - `Duration` 已赋值时取其值，否则取 `AudioManager.Configuration.DefaultFadeDuration`（+0x50）。
    - 有 `SfxPath` 时 await 单条轨道。
    - 没有 `SfxPath` 时用 `CollectionPool` 租出 HashSet 与 List，经槽 28 `GetPlayedSfx` 取当前所有音效，逐条修改后 `UniTask.WhenAll`。
    - `AudioCommand.AudioManager`（0x4DE6D0）就是 `Engine.GetServiceOrErr<IAudioManager>()`，因此循环内的内联形态仍写作 `AudioManager`。
  - 唯一的改动是 `PlayOrModifyTrack` 从静态方法变成实例方法。原因是资源未加载时要调用 `Warn`（Command 槽 7），输出「効果音がロードされていません：」加路径。
    - 已在播放（槽 25）时调 `ModifySfx`（槽 14）。
    - 已加载（`AudioLoader` 槽 11 → `IResourceLoader.IsLoaded` 槽 5）时调 `PlaySfx`（槽 19）。
  - ctor 与 `PlaySfx..ctor` 折叠在一起：基类构造函数被内联，本类自身没有字段，所以函数体为空。
- **`SoundTestUi`**：
  - `ModifyPlayer`：经槽 27 `GetPlayedBgm` 取当前 BGM。多于 1 首时重置播放器并 `Debug.Log`；为 0 首时只重置；否则设置标题，并对 `GetAudioTrack`（槽 30）的结果做 `is AudioTrack` 判断后调 `UpdatePlayerAsync(...).Forget()`。
  - `ResetPlayer`：三个标签写入 U+2212 `−`；关闭 raycast；滑块 `SetValueWithoutNotify(0)`（Slider 槽 48）；两张图的颜色写成全零（`Color.clear`，按 B 级处理拼写）；`_cts` 依次 Cancel、Dispose、置 null。
  - `UpdatePlayerAsync`：
    - 替换 `_cts` 后创建 linked CTS。
    - 有 intro 片段时：用 `AudioSettings.dspTime`（icall 名已读出）驱动进度，按 `{0:mm\:ss} / {1:mm\:ss}` 格式显示，每帧 `UniTask.Yield(Update, token)`。
    - 进入 loop 段：用 `AudioSource.timeSamples` 与 `AudioClip.frequency`（icall 名已读出）计算进度。
    - 滑块的 `OnValueChangedAsObservable().Skip(1)` 订阅挂在 linked token 上：这是内联的 Cysharp `AddTo(IDisposable, CancellationToken)`，由 RegisterWithoutCaptureExecutionContext 加缓存的 dispose 回调组成。订阅的 lambda 反向写回 `timeSamples`。
    - intro 块结束时 +0x48 被清零，这是编译器对提升局部变量的清理，不对应源码语句。
  - `GetTimeSpan` = `new TimeSpan(samples * TicksPerSecond / frequency)`。
  - `SetValueWithoutNotify` 在 0.001 / 0.999 处吸附到 0 / 1，中间值走内联的 `Mathf.Clamp`。
- **`SpawnableWithAudio`**：
  - `Awake`：先解析服务，再读 `_audioSource`，所以混音组先存入局部变量。计算为 `AudioMixer.FindMatchingGroups(path)?.FirstOrDefault()`。
  - `Reset`：执行 `GetComponent<AudioSource>()`。
- **`UpdateAudioPlayerUi`（`audioPlayer` 命令）**：
  - UI 用懒加载缓存。
  - `Execute`：`SoundTestUi.ModifyPlayer(TrackName)`，其中字符串转换是内联的 `StringParameter` 隐式运算符（0x5ABEF0，形态逐字节一致）。
- **CreditsDirectorAct1.NaniAudioManager**：懒加载缓存（+0xA0）。

进度：Audio 18/18；全部 `WitchTrials.*` 合计 613/929、205,414/316,778 B；0 错误，警告基线 140。

## 7.14 `Map` 子系统

### 第 A 组：叶子组件（MapArea / MapAreaFocusButton / MapAreaLabel / MapAreaModifier / MapFloor）

- **`MapArea`**：
  - `Reset`：两次 `GetComponentInChildren<T>()`（METHODINFO 给出 MapAreaFocusButton、MapAreaLabel）。
  - `Show` / `Hide`：内联了 `MapAreaLabel.Show`/`Hide`（0x36E750 / 0x3491C0，已分别反汇编确认是 `_canvasGroup.alpha = 1f / 0f`），因此写成方法调用。
  - `ChangeFocusButtonActivity`：按 `enable` 分支，进入内联的 `MapAreaFocusButton.Enable`/`Disable`（CanvasGroup 的 interactable + blocksRaycasts）。
  - `OnClickAsync`（MoveNext 0x36E320）：`new AsyncUnityEventHandler(_focusButton.onClick, ct, callOnce: true).OnInvokeAsync()`，这正是 Cysharp `UnityAsyncExtensions.OnClickAsync(Button, CancellationToken)` 的内联体。await 之后返回 `_areaKind`。
  - `ModifyInEditor`：两次 `DoActionAsUndoable<T>`，撤销名分别是 'Modify Map Area Focus Button' 和 'Modify Map Area Label'。闭包 DisplayClass10_0 捕获 `showFocusButton`（+0x10）与 `labelMaxWidths`（+0x18）。
- **`MapAreaFocusButton`**：
  - `Awake` 直接调 `Selectable.Awake`，然后把 alpha 置 0。
  - `OnHighlightingChanged` 与 `ModifyInEditor(bool)` 被链接器折叠为同一函数体（`alpha = flag ? 1f : 0f`）。前者写成对后者的调用。
- **`MapAreaLabel.ModifyInEditor`**：
  - 用 `preferredWidth`（TMP_Text 槽 84）做 `Array.BinarySearch<float>`；结果为负时取反，再用 `cmovge` 夹到 `Length - 1`。
  - 这是内联的 int Min，写作 `Math.Min`；与 `Mathf.Min` 无法区分，拼写按 B 级。
  - 然后逐个设置底板图片的 `enabled = i == index`（icall 0x2604C50）。
- **`MapAreaModifier`**：
  - `Reset` 与 `ResetReferences` 折叠为一体，是 `GetComponentsInChildren<MapArea>(true)`（经 `gameObject`，dl=1）。
  - `ModifyInEditor` 对每个 MapArea 内联了 `MapArea.ModifyInEditor`。
- **`MapFloor`**：
  - `Reset` 调用的是无参 `GetComponentsInChildren<MapArea>()`，与 MapAreaModifier 不同，不含非激活对象。
  - `Show(restriction)`：先启用 Canvas 与 GraphicRaycaster，再用 `KeyValuePair.Deconstruct` 遍历 `Areas`。`restriction == null` 或 `Enumerable.Contains` 为真时显示，否则隐藏。
  - `Hide` 与 `WitchBookPageBase.Hide` 折叠。
  - `IsMatch` 是 `pair.Equals(_idAndVersion)`：`pair` 为 null 时直接 NRE，随后是内联的 `IdVersionPair.Equals(IdVersionPair)`。
- **工程配置**：
  - `DoActionAsUndoable` 定义在 `GigaCreation.Tools.UndoExtensions`。该工程已存在于解决方案中，但 Assembly-CSharp 此前没有引用它（CS0234），现已补上 ProjectReference。
  - 注意：`work/make_ide_project3.py` 按 DummyDll 的引用表生成 csproj，而该表里没有这个程序集（泛型方法在 dump 中无 RVA）。重新生成工程会丢掉这条引用。

进度：Map 17/27、5,094/9,894 B；全部 `WitchTrials.*` 合计 629/929、209,717/316,778 B；0 错误，警告基线 140。


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
