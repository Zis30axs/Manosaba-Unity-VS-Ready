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
| Debate/Court 子系统 | 本轮攻坚（独立分支 `recon/core-systems`）：33/82 个函数体、16,262/32,044 B 完成，全部 Confidence A；`DebateUi.cs` 整个文件完成；`WaitForDebate`/`Print()`/`Execute()` 均已复原成真正的 `async`/`await`（`Print()` 3 个 await 点、`Execute()` 2 个 await 点），不是 MoveNext 逐状态翻译；`ModifyDebatePrinter`（此前判定"very hard"暂缓的场景位置/旋转/动画种类配置）已复原，`Execute → GetOrAddPrinter → ModifyDebatePrinter → Print → WaitForDebate` 入口链现已完全闭合；外部复核发现并已修复 4 处提交后才暴露的错误（2 处虚表槽位号、1 处遗漏的 disposal 守卫、1 处多余语句），另有 1 处提交前发现并订正的注释错误（混淆了 `set_Position`/`set_Rotation` 两种不同的接口分派机制），详见 §7.5"本轮修正"；顺带修复了两处阻塞性第三方/工程配置缺口（`Naninovel.UniTask.Awaiter` 缺失基接口列表；`Assembly-CSharp.csproj` 完全没有引用 `FastEnum.dll`） |

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
| 25 | PerspectiveStage | `GosubToModifyPerspectiveStageCamera::Execute` | `0x31D870` | 2520 | 48 | 0 | 0 | 11 | 8 | hard |
| 26 | PerspectiveStage | `GosubToSplitPerspectiveStageScreens::Execute` | `0x31E250` | 2338 | 44 | 0 | 0 | 10 | 7 | hard |
| 27 | PerspectiveStage | `PerspectiveStagePresenter::Awake` | `0x38D8D0` | 1558 | 33 | 0 | 1 | 0 | 5 | hard |
| 28 | PerspectiveStage | `StageCameraController::Awake` | `0x3846C0` | 840 | 24 | 0 | 0 | 0 | 0 | hard |
| 29 | WitchBook/Data | `ClueDataItem::.ctor` | `0x36C0D0` ✅ | 170 | 4 | 72 | 0 | 0 | 0 | easy |
| 30 | WitchBook/Data | `MapDataItem::.ctor` | `0x30C0D0` ✅ | 82 | 2 | 56 | 0 | 0 | 0 | easy |
| 31 | WitchBook/Data | `WitchBookScreen::InitializePages` | `0x37A880` | 1408 | 31 | 0 | 5 | 0 | 5 | hard |
| 32 | WitchBook/Data | `WitchBookScreen::get_PageData` | `0x3799B0` ✅ | 807 | 15 | 14 | 0 | 0 | 3 | medium |
| 33 | Debate/Court | `DebateTextPrinterPanel::AddText` | `0x367E20` | 1322 | 36 | 1 | 0 | 3 | 5 | hard |
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

## 7.5 `Debate/Court` 子系统：本轮攻坚，33 个原生方法体 / 16,262 B（独立 worktree
`recon/core-systems`，无资源审计工作，见并行的 `audit/unity-assets`）

**进度**：`Debate/Court` 桶原本 82 个独立函数体 / 32,044 B，本轮完成 **33 个 /
16,262 B（约 40% 方法数、约 51% 字节数——首次过半）**，全部 Confidence A。（初次
提交时有 2 处被误判为需要 Confidence B 的调用目标，实际是虚表基址算错导致的——
已在外部复核后重新独立锚定虚表基址并修正为 Confidence A；另有 1 处 disposal 守卫
遗漏和 1 处多余语句，详见下方"本轮修正"。）

| 文件 | 完成 / 总数 | 说明 |
|---|---|---|
| `DebateUi.cs` | **14 / 14（整个文件）** | 辩论 UI：倒计时文本、跳过按钮、HP 变化钩子 |
| `Court.cs` | 2 / 3（ctor 是已知的共享空构造体） | 法庭火焰特效 |
| `ChangeCourtFireFxActivity.cs` | 2 / 3（同上） | 火焰开关 Command |
| `ChangeDebateMode.cs` | 1 / 2（同上） | 辩论模式开关 Command |
| `PrintDebateText.cs` | 14 / ~15 | 辩论文字打印 Command（脚本内 2,003 次调用）——`Execute()`/`Print()` 均已复原成结构化 `async`/`await`（见下方详述）；`ModifyDebatePrinter`（场景位置/旋转/动画种类配置）已复原，**`Execute → GetOrAddPrinter → ModifyDebatePrinter → Print → WaitForDebate` 入口链现已完全闭合**；`CompleteOnContinue`/`ShouldPlayAutoVoice`/`WaitAutoVoice`/构造函数还没做 |

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

### 尚未解决的问题（诚实记录，不是回避）

1. **`DebateTextPrinterPanel::AddText`（RVA `0x367E20`，1322 B）没有动**——第 4 节
   优先级表 #33，跟 `PrintDebateText` 同一个"打字机效果"流程但目前还没排查。
2. **`PrintDebateText.cs` 剩余尾巴**——`CompleteOnContinue`（Slot 15 空桩）、
   `ShouldPlayAutoVoice`（Slot 33 空桩）、`WaitAutoVoice`（Slot 40 空桩）、实例构造函数
   （`0x323690`）都还没排查，是这个文件收尾的最后一批。
3. **`ModifyDebatePrinter` 已复原，`Execute()` 入口链已闭合**——`Execute → GetOrAddPrinter
   → ModifyDebatePrinter → Print → WaitForDebate` 现在完整连通，不再是阻塞项（记录在这里
   是为了明确此前"尚未解决"列表里的第 1 条已经解决，不是悄悄消失）。

### 下一批最高价值目标（按价值排序，供下一轮直接开始）

1. `DebateTextPrinterPanel::AddText`（0x367E20，优先级表 #33，`PrintDebateText` 同流程）
2. PerspectiveStage 子系统（本轮完全未触碰，用户优先级第 2 位，65 个函数体 / 27,784 B，
   `GosubToModifyPerspectiveStageCamera::Execute`/`GosubToSplitPerspectiveStageScreens::Execute`
   两个 2000+ B 的 `Execute` 是下一个自然的攻坚对象）
3. `PrintDebateText.cs` 收尾（`CompleteOnContinue`/`ShouldPlayAutoVoice`/`WaitAutoVoice`/构造函数）

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
