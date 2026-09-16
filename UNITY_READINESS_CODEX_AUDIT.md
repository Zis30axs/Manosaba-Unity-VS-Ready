# Unity 全资源可用性与独立运行复核 — Codex

## 0. 基线、边界与结论

- Reviewer / note author: **Codex (OpenAI)**，按仓库约定标记审计来源。
- 日期：2026-09-16。
- 审查基线：`39f51c13dc2af037a2b74598bdf99905fa521198`，`main`。
- 本轮实际执行：读取仓库树、资源审计报告、已提交的 JSON 统计、审计脚本、构建配置及启动/资源加载/Naninovel 相关 C#；对照 Unity 官方文档进行静态复核。
- 本轮没有执行：访问用户 E: 上的原始游戏/`res/`、重新扫描其二进制、安装 Unity、重新编译、进入 Play Mode、导出并运行 Player。**历史扫描结果不能标记为本轮实测。**
- 结论：**当前完整 Unity 工程可用性 = BLOCKED / RED。** 素材与大量对象关系有重建价值，不代表已经能直接打开、导入、编译并运行所有内容。
- 修正此前审查的范围：Boot/Init 是 `WitchTrials.*` 静态重建统计中的主要剩余项，**不是整个独立可运行工程的唯一剩余工作**。运行时依赖仍有关键存根。

资源本体在 Git 忽略的本地目录；本仓库提供的是源代码、工具及历史审计证据。本报告不声称已经逐个验证 58,750 个本地文件，更不声称资源全部损坏。

## 1. 独立发现

### UR-001 / 阻断：没有现成 Unity 项目与 Editor 资产

[原资源审计](UNITY_PROJECT_RECONSTRUCTION_AUDIT.md) 记录 `.unity/.prefab/.meta/.asset/.mat/.controller/.anim/.shader` 等 Editor 产物为零，转换器没有实现。根目录与 `project/` 是重建/分析工程，不能把其 `.sln` 等同 Unity 工程。

需要新增真正的 `Assets/`、`Packages/manifest.json`、`ProjectSettings/ProjectVersion.txt`、场景、资产、导入设置和引用映射；目标版本先以已有证据的 **Unity 6000.0.48f1 / Windows x64 / Addressables 2.4.6** 为基线，再在本机复核。

### UR-002 / 阻断：Naninovel 不只是缺 Editor，核心运行时仍是存根

实际源码 [Engine.cs](src/decompiled/Elringus.Naninovel.Runtime/Naninovel/Engine.cs)：

- `Initialize(EngineParams)` 返回 `default(UniTask)`；
- `GetService<TService>()`、`GetServiceOrErr<TService>()`、`GetConfiguration<T>()` 返回 `null`；
- 多个初始化、事件与服务操作方法为空。

因此，原报告中“runtime 已反编译可编译，只缺编辑器侧”的措辞不足以表达运行风险。获得匹配的真实实现或经过验证的功能实现，是运行门槛，不是可选优化。

### UR-003 / 阻断：GigaCreation 资源加载器也没有真实行为

实际源码 [AddressablesManager.cs](src/decompiled/GigaCreation.Essentials.AddressablesUtils/GigaCreation.Essentials.AddressablesUtils/AddressablesManager.cs)：

- `GetResourceLocationsAsync` 返回默认任务；
- `GetOrLoadAddressableAsset<T>(string, ...)` 和 `GetOrLoadAddressableAsset<T>(AssetReference, ...)` 返回默认任务；
- catalog 检查/加载、属性与释放流程亦有存根。

不能认为补上官方 Addressables 包就自动补好了自研适配层。必须扩展审计到全部可达 `GigaCreation.*` 与其他运行时依赖；本轮的明确结论针对上述已读方法，不声称逐方法检查了全部程序集。

### UR-004 / 阻断：启动链本身没有闭合

实际源码 [GameInitializer.cs](src/decompiled/Assembly-CSharp/WitchTrials.Views/GameInitializer.cs)：`Awake()` 为空，`InitializeAsync(...)` 返回默认任务；已恢复的 `Start()` 只是调用该初始化入口。恢复场景引用不会自动恢复初始化行为。

### UR-005 / 默认源码导入阻断：.NET 构建口径与 Unity 编译口径不同

[Directory.Build.props](project/Directory.Build.props) 将引用根指向 `src/il2cpp/DummyDll`。
[Naninovel csproj](project/src/Elringus.Naninovel.Runtime/Elringus.Naninovel.Runtime.csproj) 使用 `LangVersion=latest`，并引用 DummyDll 下的 Unity/第三方程序集。

实际 `Engine.cs`、`GameInitializer.cs` 等使用 `namespace X;` 文件范围命名空间。Unity 6.0 官方默认语言版本是 C# 9，该语法属于 C# 10。不能把现有 csproj 的构建成功视为默认 Unity 源码导入成功；需要兼容转换或另一条明确验证的编译路径。不要直接升级 Unity 来掩盖兼容问题。

### UR-006 / 高：缺失对象、未解析引用和已解析引用是三个不同指标

历史资源审计记录 **730 个未导出的 MonoBehaviour 对象**：`resources.assets` 648、`level0` 47、`sharedassets0.assets` 8、`globalgamemanagers.assets` 27。

[ref_census.json](tools/audit/out/ref_census.json) 另记录：

- 总引用 92,585；合法空引用 22,061；非空引用 70,524；
- 已解析 69,666；未解析 **858** = 文件内缺失 730 + external fileID 越界 68 + 内置资源未解析 60。

这里“730 条引用”和“730 个对象”不是同一统计维度，不能因数字相同合并为一个证据。并且已解析目标不等于目标已导入、字段正确或行为可运行。

### UR-007 / 高：Prefab clean 统计存在漏检

[a08_prefab_sweep.py](tools/audit/a08_prefab_sweep.py) 的明确实现问题：

1. JSON 读取失败被 `except Exception` 替换成 `{}`，没有独立报告读取失败。
2. `orphan_go` 算出后没有加入 `issues` 或输出。
3. 对象字典仅按 `pid`，指针校验忽略 `m_FileID`；遇到多来源/外部引用时不能可靠表达对象身份。
4. 没有完整核验每个组件的 `m_GameObject` 回指；只核验目标 pid 是否存在不足以证明组件正确挂接。
5. `multi_root` 单独计数，不阻止 `clean`；输出只保留有 issues 的样本，没有全量条目或多根名单。

因此，[prefab_sweep.json](tools/audit/out/prefab_sweep.json) 中的 **153/156 clean** 只是旧校验器结果，不能当作 153 个完整、可运行 Prefab 的证明。

原结果列出的三个缺组件容器必须回源检查：
`ProvidableDialog.prefab`、`DebugLabel.prefab`、`RebuildableToggleLabel.prefab`。

### UR-008 / 高：Addressables 的 100% 是容器路径命中率，不是运行时加载通过率

[a03_worker.py](tools/audit/a03_worker.py) 用 `cont = [k for k, _ in d.m_Container]` 只保留路径，丢弃 AssetInfo/目标指针。

[a07_addressables.py](tools/audit/a07_addressables.py)：

- `key2bundle.setdefault(c, src)` 对重复容器 key 只保留第一个来源；
- `idx_keys` 只比较路径存在，没有验证正确目标对象、类型与子资源；
- `catalog.bin` 仅解析头部并用正则搜索 ASCII 字符串，没有完整解析实际 resource locations、GUID key、label 和 provider 数据；
- `disk_bundles.add(f)` 用 basename 去重，导致 [addressables.json](tools/audit/out/addressables.json) 同时显示 `bundles.total=457` 与 `bundles_on_disk=450`。后者实际是不同文件名数量，不是可靠的物理文件总数。

因此不能把 **28,332 个 AssetBundle 容器路径命中**写成“所有 Addressables 运行时 key 已正确恢复且能加载”。

五个多根 Prefab 应先回源检查被丢弃的 container 目标/PPtr/preload 信息，不应一开始就假定只能凭语义人工选根。可否全部自动确定仍待逐项验证。

### UR-009 / 高：引用解析器的身份模型需要修正

[a04_refs.py](tools/audit/a04_refs.py) 将同一来源的多个 SerializedFile externals 直接拼接，且主要按 `(source_bundle, pathID)` 建对象表。此模型无法普遍正确表达一个 bundle 内多个 SerializedFile 的局部 fileID。

建议规范身份：
`(source-relative-path/source-hash, serialized-file-id, pathID)` → `(assetPath, assetGUID, localFileID)`。

目标应包括 GUID **和** localFileID，不能只把每个对象转换成 GUID。多个组件/子资源可以共享同一资产 GUID，但必须有不同的本地标识。64 位 PathID 要无损保存，不能经浮点数或 JavaScript 普通 number 中转。

### UR-010 / 高：无 TypeTree 解码不能直接使用 IL2CPP 内存偏移

原报告建议用 `dump.cs` 字段声明与偏移解码 Player 中的缺失对象。字段声明有参考价值，但 IL2CPP 运行时内存布局不能直接当成 Unity 磁盘序列化布局。

例如 `GameInitializer._gamePrefab` 是 `AssetReferenceGameObject`。必须按实际序列化类型/继承/字段规则恢复，不能因为字段在内存中是引用就按一个固定 PPtr 读。字符串、数组、内联类、SerializeReference、对齐与版本差异均须验证。

尺寸“装得下”不是逐字段解码成功。使用已知 TypeTree、合法可用的匹配程序集元数据、经过测试的转换工具和原始字节交叉验证；保留未解析范围与证据，禁止猜值补齐。

### UR-011 / 高：Shader、动画、Sprite、字体和 Mixer 不能统一 JSON 改 YAML

Editor 表示层缺失不意味着所有类型能用同一个字段转写器解决。Shader/ComputeShader 的构建后数据不等于可编译源文件；动画控制器、时间线、Sprite 子资源、骨骼/网格、字体图集、Mixer 的恢复需要对应类型转换与真实导入验证。

从正确版本包重建、从合法原始材料恢复，或实施明确标记并验证的功能替代，均需逐项记录。未知不能写成通过；纯色材质、默认字体、空组件和假加载成功不得冒充等价恢复。

### UR-012 / 高：复现、完整媒体验证与 Player 验收尚未建立

[common.py](tools/audit/common.py) 硬编码 E: 路径；`res/`、原始 Player 数据、DummyDll 等不是完整随库交付的输入。新环境必须有预检、输入路径配置、hash 清单和工具版本锁定。

音频、PNG、视频、字体的标准格式只能证明值得尝试导入，不能替代逐文件解码、Unity 导入、依赖恢复和目标平台运行测试。索引对象数与物理文件数的差异也不能直接判定为丢失。

## 2. 全资源类别台账

下列规模来自历史 [index_summary.json](tools/audit/out/index_summary.json) 和原资源审计，未在本轮重扫原始文件。

| 类别 | 已有证据/规模 | 当前完整使用结论 |
|---|---|---|
| 音频 | 26,538 AudioClip 索引行 / 26,537 OGG 文件 | 需逐对象对账、完整解码、导入设置与循环/路由验证 |
| 图片/Sprite/图集 | Texture2D 906、Sprite 2,396、SpriteAtlas 17；PNG 文件 3,302 | 像素材料存在；Sprite rect/pivot/border、packing、子资源、骨骼与引用未完成 Editor 验收 |
| 字体/TMP | Font 5 索引对象；导出字体 3 OTF + 1 TTF；TMP 相关实例 | 需区别字体文件、TMP 字体资产、字形表、图集、材质与 fallback；不能直接以数量差认定丢失 |
| 视频 | VideoClip 4 / M4V 4 | 尚无逐文件解码与 Windows Player 的 VideoPlayer 验证 |
| 文本/剧本/本地化 | TextAsset 454；历史报告 Naninovel Script 440 | 剧本主要是预编译对象数据，不应当作普通 `.nani` 源码直接改后缀；引擎运行时有存根 |
| Scene/Prefab | Boot 场景；156 prefab 容器 | 没有 Editor 场景/Prefab 产物；缺组件、引用与校验器缺陷均未闭合 |
| 自定义组件/SO | MonoBehaviour 索引行 5,903；历史 SO 容器 37 | 类归属可识别不代表包存在、字段已导入或运行时实现完整 |
| 动画/Timeline | AnimationClip 193、AnimatorController 8、Animator 345、Animation 83、PlayableDirector 188；Timeline 类在 MB 中 | 需曲线、绑定路径、状态/转换、参数、轨道/Playable 子资源、事件恢复 |
| 材质/渲染 | Material 155、Shader 133、ComputeShader 1、Camera 17、Light 2 | 缺源码/类型转换及 URP 设置验证；不能宣称无粉材质或效果等价 |
| 网格/粒子/约束 | Mesh 2、MeshFilter/Renderer 各 35、MeshCollider 27、ParticleSystem/Renderer 各 35、RotationConstraint 4 | 二进制载荷、流文件、顶点/索引/绑定与目标组件尚需验证 |
| AudioMixer | Controller 2、GroupController 8、SnapshotController 2、AudioSource 3 | 需要真实 `.mixer`、分组路由、快照/参数行为，不能只导入音频 |
| UI/基础组件/场景设置 | GO/Transform/RectTransform/Canvas/Renderer/SortingGroup/SpriteMask/RenderSettings/LightmapSettings/NavMeshSettings/PreloadData 等 | 需完整对象图、挂接、排序、蒙版、配置与依赖处理 |
| Addressables/工程与程序集 | 457 bundle 记录、28,332 容器路径；无真实工程与依赖闭环 | 完整 catalog、GUID/label/provider、包与引擎实现、内容构建/Player 测试均未通过 |

原索引的 43 种类型必须全部进入下一轮台账，不得只测贴图/音频：
`AudioClip, GameObject, MonoBehaviour, Transform, MonoScript, RectTransform, Sprite, SpriteRenderer, CanvasRenderer, Texture2D, TextAsset, Animator, CanvasGroup, AnimationClip, PlayableDirector, Material, Shader, Canvas, Animation, SortingGroup, MeshFilter, MeshRenderer, ParticleSystem, ParticleSystemRenderer, MeshCollider, Camera, SpriteAtlas, AnimatorController, AudioMixerGroupController, Font, RotationConstraint, SpriteMask, VideoClip, AudioSource, AudioMixerController, AudioMixerSnapshotController, Light, Mesh, PreloadData, ComputeShader, LightmapSettings, NavMeshSettings, RenderSettings`。

这 43 种是旧导出索引分母，不是原始源数据类型全集；重扫源文件发现的其他类型与此前漏导对象同样必须纳入。

## 3. 可直接交给本地施工代理的修改 prompt

```text
你接手 Zis30axs/Manosaba 的“全资源可利用、Unity Editor 可编译、独立 Player 可运行”集成重建工作。

这不是仅补 WitchTrials.* 的 Boot/Init，也不是再写一份可行性报告。目标是在用户持有输入与依赖的环境中实际实现转换、构建与测试；缺少输入/授权包/执行环境时，对该项标 BLOCKED，继续完成不依赖它的工作。禁止用占位对象和假成功刷完成率。

一、基线与安全边界
1. 先读 UNITY_READINESS_CODEX_AUDIT.md、UNITY_PROJECT_RECONSTRUCTION_AUDIT.md、RECONSTRUCTION.md、RECONSTRUCTION_CLAUDE_NOTES.md，以及 tools/audit 全部代码/输出。
2. 记录当前 commit、分支、dirty 状态；不要覆盖其他代理未提交改动。用独立 worktree/分支 recon/unity-runtime-integration 工作，不自行强推。
3. 原始游戏目录、原始 bundle/Player 数据、res/ 保持只读；转换输出写入新 UnityProject/，中间证据写 tools/unity-rebuild/out/。
4. 为 SourceRoot/ResRoot/GameDataRoot/UnityEditorPath/OutputRoot 提供命令行或本地配置，移除 common.py 中必须依赖 E: 的假设；本地绝对路径与敏感数据不入库。
5. 预检原始 bundle、Player SerializedFile、resS/resource 流、catalog/settings、metadata/native binary、工具和 Unity。记录存在性、大小、SHA-256、版本、来源；缺失必须给精确清单，不能只返回空结果。
6. 保留证据纪律：原代码 A/B 恢复、功能兼容替代、纯基础设施代码分开标识。没有证据的 C/D 不伪装成原版实现。不改原版二进制，不伪造鉴权，不提交商业原始资产、密钥或无权分发的包。

二、先修审计器，建立可信分母
1. 建 SourceObjectKey=(source identity, SerializedFile identity, signed 64-bit pathID)，不要用 basename 或单个 pathID 做全局主键。跨语言传递 pathID 时使用无损整数或十进制字符串。
2. a03 保留每个 m_Container 条目的 AssetInfo/目标 PPtr、preloadIndex/preloadSize、所属 SerializedFile 与依赖；不能只保留 key 字符串。
3. a04 按每个 SerializedFile 自身的 externals 解析 fileID，禁止拼接不同文件的 externals；结构化遍历 JSON，记录字段路径，处理 null/builtin/未知目标/类型错配/重复身份。
4. a07 用完整相对路径与 hash 对账 bundle；解释原报告 457 vs 450。使用 key->locations[] 多值模型保留别名/重复 key/子资源，不准 setdefault 静默丢来源。
5. a08 将 JSON 读取错误、orphan GO、orphan Transform、组件回指错误、空组件表、重复身份、跨文件引用和不合理多根纳入结果。保留允许合法多根/合法空对象的证据，不以一刀切修复替代判断。
6. 为上述缺陷写小型回归测试：同 basename 不同目录、同 pathID 不同文件、缺/坏 JSON、无根环、孤立 GO、错误组件回指、重复地址等。
7. 输出全量对象/资产/引用/Prefab 台账，而非只截前 25/100 条。对象数、文件数、容器路径数、catalog key 数、引用条数、可达运行时类型数分别统计。
8. 重扫全部源文件与旧索引的 43 种类型，并纳入此前漏导类型、730 个缺失对象、约 9 个其他漏导对象、所有索引外文件。没有索引不能直接跳过。

三、建立真实 Unity 工程、包与源码编译链
1. 先核对原始版本，再固定 Unity 6000.0.48f1 / Windows x64 与 Addressables 2.4.6；其他包版本由原始证据或实际兼容测试决定，不猜版本、不盲装最新版。
2. 新建 Assets、Packages、ProjectSettings；提交正确的 manifest、packages-lock、ProjectVersion、程序集定义与必要设置。不要把 Unity 自动生成的 csproj 当作工程配置的唯一真相。
3. 根据 ScriptingAssemblies、csproj、实际类型使用和序列化实例建立 dependency_matrix.csv：程序集/来源包/版本证据/安装方式/真实实现或存根/许可证输入/使用点/测试状态。
4. Unity 自带模块、UPM 包、第三方包、自研代码分别处理；165 个程序集不是 165 个必须独立安装的包。
5. 核查 UGUI/TMP、URP、Timeline、2D Animation、Addressables、Input System、Burst/Collections/Mathematics，以及 UniTask、UniRx、LitMotion、DOTween、Steamworks.NET、TrueShadow、MPUIKit、UIPolygon、ppt-noise、Naninovel、SpriteDicing、全部可达 GigaCreation.*。
6. DummyDll、IL2CPP interop 代理、Unity/System 反编译参考代码、BepInEx 分析插件不得作为新 Player 的运行时替代实现，也不得引入重复同名类型/程序集。
7. AST 扫描实际源码。将默认 Unity C# 9 不接受的文件范围 namespace 等语法转换为兼容写法；检查 record/init 的支持边界、生成类型、unsafe、平台条件与程序集循环。不要靠改 IDE csproj 的 LangVersion=latest 假装 Unity 已兼容。
8. 在保留原始证据副本的前提下，为运行时源码去除不必要的 Il2CppDummyDll 标记依赖；不能误删 SerializeField/Serializable 等真实序列化特性。
9. 扫描全部可达程序集中的疑似存根。空方法/return null/default 只是候选，结合证据区分真实原生 no-op、抽象/接口、已重建、未重建；禁止正则一刀切补值。
10. 明确修复或替换 Naninovel Engine.Initialize/GetService、GigaCreation AddressablesManager 的 catalog/locations/load/release 方法，以及各自被调用的依赖。真实包与兼容层只能选择一条明确路线，不能让同名类型混用。
11. 缺商业包时列出需用户提供的准确包/API/类型/资产，不擅自购买。未经验证的兼容实现不能标为完整等价，也不能通过删掉组件绕过。

四、补导无 TypeTree 对象和缺失载荷
1. 对 730 个历史缺失 MonoBehaviour 逐个回源，保存来源、pathID、script identity、原始字节/hash、解码 schema、每字段取值与置信度。
2. 优先使用能匹配版本的既有 TypeTree/有效程序集元数据或经过验证的恢复工具；对 AssetRipper 等先在 Boot、Game.prefab、复杂 UI/Timeline 小集合试验，记录实际支持情况，再决定全量路线。
3. IL2CPP FieldOffset 不是磁盘序列化偏移。遵循真实序列化字段/继承/内联类/字符串数组/对齐/SerializeReference/版本规则，不把 AssetReferenceGameObject 盲读成 12 字节 PPtr。
4. 按实际格式校验消费长度、允许 padding、数组边界、引用目标类型与往返结果；尺寸恰好相同不能当解码成功。未知字节和无法解释的字段保留为 BLOCKED。
5. 对已知有 TypeTree 的匹配类型建立解码交叉测试；防止用解析器自己的输出自证正确。
6. 重导失败的 Texture2D/Sprite/AudioClip/RenderTexture 等对象及所有外部流依赖，逐对象给结论，不能按扩展名数量差猜缺失。

五、将对象图转换为真正 Editor 资产
1. 两阶段导入：先创建资产/子资源/组件并分配身份，再绑定全部引用；必要时多阶段导入避免循环依赖。转换可重复、可增量、可重试且不覆盖手工修正。
2. 映射必须到 (assetPath, GUID, localFileID)，按资产文件生成稳定 GUID，按组件/子资源生成或读取正确 localFileID。不能给一个 Prefab 的每个组件随便生成一个独立 GUID。
3. 保存后通过 AssetDatabase.TryGetGUIDAndLocalFileIdentifier 等实际 Editor 结果核对。重导与重启不能改变已承诺稳定的映射。
4. 对 MonoScript 按程序集+namespace+class 绑定真实类型，核对继承和序列化字段，而不是只看类名。处理迁移后的 assembly-qualified/SerializeReference 身份。
5. 先导出并验证 Boot.unity 和 Game.prefab，随后覆盖全部 156 prefab、SO、默认 Resources 对象与其他场景资产。
6. 回源检查 ProvidableDialog、DebugLabel、RebuildableToggleLabel 的缺组件；五个多根容器优先用 m_Container 目标、preload 和原始对象图确定真实根，不随手添加空父对象改变局部坐标或动画路径。
7. 保留 Transform/RectTransform、组件顺序、激活状态、Layer/Tag/Sorting、事件绑定、Prefab 本地/跨资产引用。缺失不可改成 null 后报成功；原本合法 null 也不可误报为缺失。
8. 嵌套 Prefab 来源关系若已在构建中 flatten，可交付经验证的等价平铺 Prefab，并明确原编辑结构未恢复；不能为追求原层级编造信息。
9. JSON->YAML 不是所有类型的通用答案。优先使用可靠类型转换、Unity Editor API 或匹配包资产；手写 YAML 必须验证 Unity 能读、重序列化并保留等价图。

六、逐类别恢复“内容 + 导入设置 + 行为”
1. Texture/Sprite/Atlas：像素与外部流、尺寸/alpha/sRGB、filter/wrap/mipmap/压缩、Sprite rect/pivot/border/PPU、packing/rotation、subasset、skin/bone/mesh 与 atlas 依赖。
2. Audio：完整解码所有输入，校对采样数/声道/时长、导入类型与流式策略，验证循环及 BGM/SFX/Voice 路由；恢复 AudioMixer/Group/Snapshot/参数，不只导入 OGG。
3. Video：验证全部视频的容器/编码、时长、音轨与 VideoPlayer 准备/播放；需要转码时保留原件、转码设置与质量/时长验证。
4. Font/TMP：字体文件与 FontAsset 分开恢复；字形/字符表、atlas/material/fallback、中文日文覆盖、rich text、换行和缺字日志逐项测试。
5. Mesh/Particle/Camera/Light/Constraint：恢复完整载荷与引用、几何/骨骼/材质、粒子模块、RenderTexture、后处理/排序/遮罩及场景设置；只挂组件但不填值不算通过。
6. Animation/Animator/Timeline：恢复曲线、绑定路径、object reference curves、事件、状态/转换/参数、Avatar/skin、track/clip/Playable 子资源及 Director bindings；保留可评估与不可恢复项的明细。
7. Shader/ComputeShader/Material：建立每个 shader 的来源与恢复矩阵；优先匹配官方/第三方包，不能把编译后 JSON 改扩展名。核对纹理槽、keywords、render queue、blend/stencil、URP 资产与 feature。功能替代需画面对照并明确不等价部分；粉色/纯白材质不算成功。
8. SO/剧情/本地化：按原 Script/主数据结构恢复序列化对象。预编译剧情 JSON 不直接冒充 .nani 源文本；核对脚本名/label/gosub/命令参数/作者ID/语音ID/ManagedText/中文日文映射。
9. 扫描全部剧情文件的命令与表达式，映射到真实处理器和资源；重建表中未恢复/不支持的调用点必须显式列出，不能静默跳过。只测试开场不代表全部章节可运行。

七、真正闭合 Addressables
1. 用匹配 Addressables 运行时/API或经验证的解析器加载完整 catalog，枚举真实 key、GUID、label、IResourceLocation、ResourceType、ProviderId、InternalId 与 dependencies。ASCII 字符串搜索不算完整解析。
2. 交叉核验 catalog locations、bundle container 目标、源对象与 Editor 资产，而不是只比较文件路径集合。
3. 原始 GUID 若能由 catalog/key 对应恢复，保留并记录证据；不能一开始就断言全部不可恢复。无法保留的 GUID 建立完整 old->new 映射，同时重写 AssetReference 字符串字段及子资源引用。
4. 查询 AssetReference、代码字符串、剧情资源key、本地化key和动态组合路径。给不静态可枚举的动态路径补运行时日志/测试，不能以正则没找到当作不存在。
5. 新建功能等价 Addressables settings/groups/labels，正确配置本地/远端/平台路径，重新构建内容；原 Editor 分组结构若未保留不必编造。
6. 在实际 Unity 中逐个可加载 location 执行合适的 Load/Instantiate/Scene/Video 等验证并释放 handle；不要一次全部常驻内存。去重资源对象，但保留每个 key/别名/label 的定位测试。
7. 可建立隔离的“读取原 bundle”诊断路线，但不能把依赖原包运行冒充可编辑、可重新构建的完成版。Windows 构建资源不能未经验证视为跨平台可用。

八、闭合 Boot 与运行时生命周期
1. 按证据恢复 GameInitializer.Awake/InitializeAsync、SteamManager、DefaultLocale、SplashScreen、EntrySequence、场景创建和服务注册链。
2. 把 RECONSTRUCTION_CLAUDE_NOTES.md 中的必填字段全部变成导入后断言：如 DebateUi label/button、Court 灯光/特效、PerspectiveStage 的 parent/controllers/data、StageCameraController 的容器、StageScreenController 的屏幕容器等。
3. 保存初始化内部顺序、异步 await/取消/异常语义、事件注册/注销、persistent 对象和销毁行为；不同对象的 Awake 顺序不得仅凭方法名称假定，检查原设置/调用依赖。
4. Steam/外部服务的开发模式必须明确隔离并标注，不得通过假授权或永久跳过原逻辑冒充原版验证。原生平台依赖按架构设置导入条件。
5. 对用户存档使用隔离测试目录和备份，测试不能覆盖真实存档。

九、分级验收，任何跳过都不能计作通过
G0 输入与审计：输入/hash/版本可复现；每个源对象都有台账归属；审计回归测试通过。
G1 工程与编译：Unity 实际包解析和脚本编译 0 error，无错误 DummyDll 运行时依赖、重复类型、未说明的存根依赖；新警告逐条归因。不能用 dotnet build 代替。
G2 全资源导入：全部目标资源/子资源都有成功导入或明确失败记录；必需引用无悬空、无 Missing Script、无未解释的 GUID/fileID 错配。Editor 保存、关闭、重新打开后再验证，不能只看第一次导入。
G3 内容构建：新 Addressables 内容构建成功；全部 key/location 的定位与类型正确；可加载内容全部通过对应加载/释放测试。合法 null、非运行时管理对象等排除项必须有证据。
G4 Play Mode：Boot->标题->新游戏/读档->对话/地图/辩论/审判->选项/图鉴/画廊/片尾->返回标题；中日切换、文字/语音/BGM/视频、skip/auto、存读档与场景重入；观察重复持久对象、异常、悬挂任务和未释放资源。
G5 独立 Player：真实构建 Windows x64（目标 IL2CPP 时必须验证 IL2CPP 的裁剪/AOT/泛型/反射/link 配置），从独立输出目录运行相同烟测；不能依赖原游戏 exe、BepInEx 或 Unity Editor 的 AssetDatabase 快捷加载。
G6 全内容覆盖：所有脚本/命令/资源验证与已跑剧情分支分别统计；未执行的用 NOT_RUN，缺输入的用 BLOCKED，不能将抽样测试结果扩成“所有内容已验证”。

批处理构建与测试要保存完整命令、Unity/包版本、log、机器可读结果、退出码、耗时与输入hash；出现编译/导入/测试失败必须非零退出。图形/音视频测试使用有实际图形和播放能力的环境，不能由无图形批处理结果替代视觉/听觉验收。

十、交付与持续记录
提交实际转换器、修正后的审计器、回归测试、UnityProject 工程配置、bootstrap/build/test 脚本及说明。批量原资源继续遵守 Git 忽略策略；新 clone 的用户应能用自有输入复现，不要求把商业资源上传仓库。

至少交付：
- UNITY_RUNTIME_INTEGRATION.md：版本、输入、配置、恢复/运行/重建步骤；
- dependency_matrix.csv 与 stub_inventory.csv：真实实现、已确认 no-op、缺包/未恢复/兼容替代分开；
- source_inventory.jsonl、asset_inventory.csv、object_map.jsonl：逐对象与逐文件映射；
- unresolved_objects.csv、unresolved_references.csv、unsupported_assets.csv：全量缺口，不截样本；
- addressables_validation.json、shader_recovery.csv、script_command_coverage.csv；
- import/build/playmode/player/content_coverage 的真实 log、XML/JSON 结果及必要截图。

每批只提交一个明确主题，记录实际作者；不要把 Codex 的审计署名冒充成施工代理的实现署名。保留已有 Codex 文档来源，施工工作写自己的记录。

最终分别回答：全部输入是否齐全、全部类别是否可导入、源码能否由 Unity 编译、全部资源是否通过加载测试、关键流程能否运行、独立 Player 是否实测、全剧情覆盖到哪里。附失败项目/文件/对象ID/证据/修复动作。

不得用“97.5% 源码完成”“所有 key 在索引里”“153 prefab clean”“0 dotnet errors”代替以上验收。未经执行的验收不得写 PASS。
```

## 4. 官方语义参考

- Unity 6.0 C# 编译器（C# 9 与支持限制）：https://docs.unity3d.com/cn/6000.0/Manual/csharp-compiler.html
- Microsoft C# 10 文件范围命名空间说明：https://learn.microsoft.com/en-us/visualstudio/releases/2022/release-notes-v17.0
- Unity 6.0 序列化规则（字段、UnityEngine.Object 引用、内联类/SerializeReference）：https://docs.unity3d.com/cn/6000.0/Manual/script-serialization-rules.html
- Unity 6.0 GUID + local file identifier：https://docs.unity3d.com/6000.0/Documentation/ScriptReference/AssetDatabase.TryGetGUIDAndLocalFileIdentifier.html

以上用于核对语义，不代替本项目特定版本的实际编译与运行。Addressables 2.4.6 的 API/二进制 catalog 行为须以本机匹配包验证，本轮没有完成其运行时解析。

## 5. Provenance

本提交仅增加审计与施工提示，没有修复运行时代码，没有转换资产，没有更改历史审计数字，也没有将未执行的测试标为通过。

`Co-authored-by: Codex <codex@openai.com>`
