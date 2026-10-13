# Switch 剧情 CG 与 cut-in 移植

这是现有 `res → sync → Addressables build` 流程中的可选剧情演出配方，使用协作者自己持有并导出的 Switch **2.0.4b** RomFS 数据。共享的是重建代码与配方，不随 Git 分发游戏资源、导出 bundle 或用户存档。普通 PC 资源须先按[开发指南](../DEVELOPMENT.md)导入同一工程所对应的版本。

标题与烟花演出属于独立的[标题配方](switch-title-port.md)。本说明中的“新增”指已比较的 PC 与 Switch 导出数据差异，不表示这些内容永久为 Switch 独占。

2026-10-12 增补了[横条论破语音](switch-objection-voice.md)：189 次艾玛／希罗单人调用接回既有喊话，最终双人横条改为同时播放两人的既有语音。下表保留原视觉移植范围；加上语音后，配方管理的脚本并集为 154 份。运行证据与未验证范围见语音说明。

## 移植范围

| 内容 | 本次配方范围 |
| --- | --- |
| 剧情 CG | 7 组、15 个图像变体：`710_1..710_8`、`720_1`、`730_1`、`740_1`、`750_1..750_2`、`760_1`、`770_1` |
| Cut-in prefab | 18 个：13 位角色的 `CutinSmall_*`、`CutinW1..4`、`ObjectionCutIn_w` |
| 剧情脚本 | 合并 50 份既有 PC 剧情 Script；其中 8 份接入新 CG，42 份接入新 cut-in |
| 正式剧情调用 | 新 CG 显示 22 次；62 次 `Spawn` cut-in 加 1 次 `GosubToObjectionCutIn` 动态双人异议；调试脚本中的 7 次 `Spawn` 与 1 次动态异议调用不移植 |

18 个 prefab 现均有正式剧情引用：17 个通过 62 次 `Spawn` 调用，`ObjectionCutIn_w` 通过参数派发调用 1 次。早期只检查直接生成命令和完整地址字面量，遗漏了 `CutInId="w"` 动态构造 `ObjectionCutIn_w` 的路径；资源虽然已导入并验过外观，原合并脚本当时仍调用 PC 的 Ema 单人版本。该遗漏已补入三方合并器，本轮运行验证状态见文末。7 组 CG、15 个变体和 50 份脚本范围不变；地址和别名仍保留原大小写。

CG 的剧情位置如下；其中 `Adv` 为日常剧情，`Bad` 为对应分支剧情。

| 剧情脚本 | 接入的 CG |
| --- | --- |
| `Act01_Chapter03/Act01_Chapter03_Bad02` | `730_1` |
| `Act01_Chapter05/Act01_Chapter05_Adv07` | `710_1..710_8`、`770_1` |
| `Act01_Chapter05/Act01_Chapter05_Adv10` | `710_4`、`770_1` |
| `Act01_Chapter05/Act01_Chapter05_Bad02` | `740_1` |
| `Act02_Chapter03/Act02_Chapter03_Adv12` | `750_1` |
| `Act02_Chapter04/Act02_Chapter04_Adv13` | `750_2` |
| `Act02_Chapter04/Act02_Chapter04_Adv16` | `760_1` |
| `Act02_Chapter04/Act02_Chapter04_Bad01` | `720_1` |

## 输入与标准操作

先准备自己的对应版本 PC 导入结果和完整 Switch RomFS `Data`。在仓库根目录显式启用：

```powershell
python tools/content/content.py sync --switch-story-source "<RomFS/Data>"
```

参数也接受包含 `Data` 的 RomFS 根目录，或直接包含所需 bundle 的 `StreamingAssets/aa/Switch` 目录。首次准备会校验并缓存选中的依赖，生成本地合并图和计划，再交给正常 Unity 内容同步。它不是另一套独立资源导入流程。

原 PC 的 **24 个剧情 bundle 基线不可缺少**，预期位置为：

```text
res/.imported-source/manosaba_Data/StreamingAssets/aa/StandaloneWindows64/
    naninovel-scripts_assets_naninovelscripts-*.bundle
```

这些基线来自正常 PC 导入缓存，用于区分原始 PC 行为与协作者自己的修改。缺少基线时配方明确失败，不会把当前已编辑的 `res` 当成原始版本。应恢复对应版本的导入缓存；不要用其他版本或当前编辑文件冒充基线。

准备成功后的日常操作仍为：

```powershell
python tools/content/content.py sync
python tools/content/content.py doctor
python tools/content/content.py build
```

也可继续使用原有 `tools/content/sync.ps1`、`doctor.ps1`、`build.ps1` 包装入口。后续正常同步和构建使用本地缓存，不要求原 Switch 导出目录一直在线。没有本地剧情缓存或计划且未指定可选参数时，该扩展保持禁用。

协作者拉取代码后，即使 `res` 没有变化，普通同步仍会检查配方和生成结果。Switch 缓存内容、24 个 PC 基线、PC 剧情源、转换器与相关运行时/shader 均参与准备签名；生成输入还单独记录内容哈希。配方变化、输入变化或生成文件缺失会触发重新准备或重建。相同输入可重复执行，已有目标 `.meta` 和 GUID 应保留。

`doctor` 若报告其他资源的待转换项或配方变化，须如实区分范围并处理；未逐项核实原因前不能归为历史遗留，也不得用剧情局部检查代替全项目通过。代码拉取不会提供被忽略的 `res`、GameContent 或 bundle；独立副本须有自己的输入与 Unity 环境。

## 三方合并与保留边界

`switch_story_scenario.py` 比较原 PC bundle、Switch bundle和当前可编辑 PC JSON。它仅选择已审查的新 CG 场景视觉差分，以及由台词 ID 定位的新 cut-in 演出段。Switch 的平台自动存档、试炼计时、系统初始化等其他差异不随整份脚本覆盖到 PC。

当前 PC 文本映射、台词 ID、分支与跳转、存档和输入流程保持原有内容；与目标演出无冲突的本地修改继续保留。直接修改过待替换演出时会报告三方合并冲突，不会静默覆盖。新 cut-in 所需的语音、音效和局部 BGM 降音量/恢复一并接入，不据此替换整场背景曲目。依赖音频通过资源地址核对；缺失的源资源随配方准备。

`CutinW1..4` 的旧 PC 段含 `System_Subroutine.EndDebate-Last`，该调用负责恢复输入、跳过和 UI。因此即使 Switch 源段删去了该调用，合并仍保留 PC 调用，避免把输入恢复误当作旧镜头演出移除。

双人异议位于 `Act02_Chapter06/Act02_Chapter06_Trial09`，由唯一台词锚点 `0206Trial09_Yuki010` 和 `0206Trial09_Ema006` 定位。原 PC 为 `CutInKind="Ema"`、`Index=2`；Switch 为 `CutInId="w"`、`Index=0`。补丁保留 PC 命令类型，只将对应两项映射为 PC 运行时支持的 `CutInKind="w"`、`Index=0`，保留其他本地字段、前置语音、后续恢复调用及同脚本其他异议演出。目标参数与基线冲突、锚点缺失或有歧义时拒绝合并；本地已做相同修复可重复接受。动态调用单独计入 `dynamic_objection_calls`，不混入 62 次 `Spawn`。

图合并会重新分配导入的 managed-reference ID，从 `CommandScriptLine.command` 与 `GenericTextScriptLine.inlinedCommands` 重建播放列表，并更新行位置和参数位置。原有未改行的 `lineHash` 保留；新增或修改的演出行使用稳定语义哈希。当前审查范围内的导入命令均为无条件、缩进 0；源段或本地插入上下文出现未审查的条件嵌套时明确拒绝。

合并器核对保留命令的顺序与语义、完整文本映射、播放列表引用，以及 22 次 CG、62 次 `Spawn` 和 1 次动态双人异议。Unity 侧再校验 PC 源哈希，将相应普通剧情任务延后到合并配方执行，并按原 PC 源文件确认同步状态。完整剧情重建复用现有 `ScriptableObjectConverter.RebuildScenario`；普通 `ApplyEditable` 的安全编辑限制不因此放宽。

新 CG 通过原有 `ModifyBackgroundExtended` 的 `Stills` 显示命令解锁。画廊从背景资源地址获取条目，配方仅按源顺序稳定插入缺失的新 CG 组，保留原 PC 条目顺序与 GalleryUI 的 GUID。导入器不改用户存档、不提前解锁图像；实际游玩显示时仍走既有解锁逻辑。

## 转换、地址与着色限制

`SwitchStoryContentBuilder.EnsureApplied()` 接入普通内容同步和 Addressables 构建，在通用地址设置之后确保 `Manosaba.SwitchStory` 条目完整。叶子资源、prefab、Timeline、动画和完整剧情图复用现有转换器；需要临时场景时保留原场景上下文，不丢弃未保存修改。转换失败或依赖缺失会使调用失败，不能仅写一份成功缓存继续构建。

新增程序化 shader 是本工程内置渲染管线的**兼容近似实现**，不是 Switch NVN shader 源码，也不保证像素一致。纹理、对象图、动画曲线、Timeline 与材质参数尽量依据导出数据重建；这些数据一致仍不能证明最终透明混合、局部坐标、发光、噪声、玻璃碎裂和粒子效果完全一致。动画和音轨还需实际播放，确认等待时间、结束清理以及语音/SFX没有意外重叠。

AssetDatabase 转换不会自动刷新旧 catalog、bundle 或 Player。应构建新的 Addressables，再进行新的 Play/已构建内容检查；旧 Player、编译成功或静态对象数量相等都不能代替此次运行验收。

## 可追踪文件与本地产物

| 路径 | 用途 |
| --- | --- |
| `tools/content/switch_story.py` | 可选入口、缓存清单、签名与生成计划 |
| `tools/content/switch_story_assets.py` | 已核实的 CG、cut-in 及其依赖抽取配方 |
| `tools/content/switch_story_scenario.py` | 原 PC / Switch / 当前 PC 三方演出合并 |
| `UnityProject/Assets/Editor/SwitchStoryContentBuilder.cs`、`SwitchStoryAssets.cs` | 现有转换器集成、素材与剧情验证、地址及画廊补齐 |
| `UnityProject/Assets/Editor/SwitchStoryVisualProbe.cs`、`SwitchStoryBuiltProbe.cs` | 图形取样与已构建内容的隔离检查入口 |
| `UnityProject/Assets/Shaders/ManosabaSwitchStory/` | 兼容 shader 与 include；相关运行时包括 `SpriteLocalCoords`、`ObjectionCutIn`、`SetShaderTimerAsUnscale` |
| `res/.imported-source/switch-story/` | 本地源 bundle 缓存与 SHA-256 清单，不提交 |
| `res/.manosaba/switch-story/plan.json` | 本地生成计划；同目录包含生成叶子数据和合并剧情，不提交 |
| `UnityProject/Library/ManosabaContent/switch-story-state.json` | 本次 Unity 转换状态、输出哈希与检查结果，不提交 |
| `tools/unity-rebuild/out/switch-story-research/` | 本机比较、图缓存与 Python 验证报告，不作正式配方输入、不提交 |

以上源码、配方、测试以及新增 Unity 文件/目录的 `.meta` 应纳入修复交付。不要强制提交整份游戏资源或本机生成树来替代可复现配方。已有工作区的其他改动须独立审查。

## 上一轮构建与素材验证（2026-10-11，动态调用修复前）

以下为签名 `cba69278…ba5e65` 的上一轮检查，证明资源导入、打包加载与所述画面取样结果；当时遗漏的剧情动态调用不能由这些结果证明已接通。转换状态中的 `render_verified: false` 表示转换器没有替代图形验收；画面结论来自另外记录的探针取样与人工检查。

| 检查 | 当前证据状态 | 证据或剩余工作 |
| --- | --- | --- |
| Python 回归 | **89 项通过**：discovery 70 项、`test_content.py` 19 项 | `python-tests-complete.log`、`python-content-tests-complete.log` |
| 三方合并数据 | 50/50 文本映射与当前 PC 一致；23,518 条保留命令语义及顺序一致；22 次 CG、62 次 cut-in | `merge-validation.json`；309 条演出命令替换，488 条插入 |
| Unity 最终重建与读回 | **OK**：92 个叶子资源、18 个 prefab、50 个 Script、84 个地址；176,108 个有效参数全部写入，`failed` / `needs_binding` 均为 0 | `story-state-delivery-rebuild.json`；画廊插入 7 组并保留原序，用户存档未修改 |
| 动画数据 | 实际使用的 146 个 clip、649 条曲线绑定的路径/属性/类型集合与源数据精确一致；审计源及生成文件哈希匹配最终产物 | `animation-binding-audit.json`；114 条普通警告来自 21 个未使用 clip，Alisa 另有 2 条源无效绑定被跳过 |
| 最终多时点图形取样（Visual） | **PASS**：18 个 prefab、163 帧、15 个 CG，匹配当前构建签名；人工检查范围见下文 | `switch-story-visual/20261011-123036-354/report.json`；使用 AssetDatabase 和隔离 Timeline 取样，未运行正式剧情或播放音频 |
| 重复同步与画廊记录 | **exit 0**：过滤同步无转换任务、0 changed、51 unchanged；story `cache.hit=true`、`rebuilt=false`，输出指纹与构建一致 | `idempotence-and-ledger.json`：119 条画廊关联记录目标哈希均正确，源哈希、状态、转换签名及其他字段均无变化；未另做缺失目标恢复实验 |
| 全量 Addressables 构建 | **exit 0，605.2 秒**：336 个 bundle、28,642 个 entry、29,870 个 location；3,704,895,492 字节 | `build-delivery.log`、`addressables-delivery.json`；构建签名见下文 |
| 已构建内容加载（Built） | **PASS 84/84**：全部经 `BundledAssetProvider` 加载，匹配当前构建签名；`play_mode=true`、`builder_restored=true` | `switch-story-built/20261011-122802-021/report.json`；未执行 boot、剧情或存档访问，也未实例化 prefab 作渲染 |
| 探针后哈希复核 | 440 个输出、1,890 个生成输入和 50 个 PC 剧情源哈希全部匹配 | `final-verification.json` |
| 全项目 doctor | **FAIL**：23,477 条 `CONVERSION_CHANGED`、489 条 `PENDING_IMPORT`；全局 `TARGET_CHANGED` 为 0 | `doctor-final.json`、`final-verification.json`；50 份合并剧情源的问题数为 0，但画廊关联的 119 条通用转换签名变化仍保留；不能称全剧情/画廊范围或全项目全部通过 |
| 完整剧情、画廊实际解锁、音频听验、Player | **未验证** | 后续需用隔离测试存档检查剧情、结束清理及音频衔接；本次未生成新的 Player 可执行文件 |
| 独立副本或冷导入 | **未验证** | 同版本自有输入的完整重建结果 |

表中的合并、状态与测试文件位于 `tools/unity-rebuild/out/switch-story-research/`；Built / Visual 目录位于 `tools/unity-rebuild/out/unity/`。首次用错误的 `-m` 形式调用内容测试时发生导入失败，保留在 `python-content-tests-delivery.log`；改为直接运行脚本后 19 项通过。

上一轮成功构建的配方签名为 `cba69278cab74ef36e74374e295e31540e16ebd084fba32bcdc1a3dea7ba5e65`。

第一轮图形取样 `20261011-114222-990` 虽获得机器 PASS，人工检查发现黑底，因此没有采纳为画面通过。修复 shader 混合并补充独立 Animator 模拟后，才执行第二轮检查。另发现 5 个 WAV 的强制 PCM 设置与全局 `AudioImportPolicy` 冲突、导致重复重建；改为尊重全局策略后，上述重复同步已确认命中缓存、不再重建。shader 始终是兼容近似，以上数据和截图检查不能替代完整剧情与音频运行验收。

最终人工画面检查单独记录于此：已查看 `20261011-123036-354` 的 `cutin-contact-1/2/3.jpg`，覆盖全部 18 个 prefab，以及 CG 接触表和原尺寸 `CutinW2-2.000.png`、`ObjectionCutIn_w-2.500.png`。所查看样本中未见不透明裂纹黑底、缺失或粉色材质；这不代表与 Switch 原图逐像素一致。机器报告的 `visual_quality_reviewed: false` 按原样保留，不用修改机器标记替代人工检查记录。

画廊新增排序修改了一个由 119 条编辑源记录共同引用的 `GalleryUI.prefab`；先前这些记录仍保存旧目标哈希，因而产生 119 条 `TARGET_CHANGED`。现通过成功扩展报告确认该固定目标：核对计划签名、计划哈希、目标与 `.meta` 哈希及所有旧 GUID 后，仅更新目标快照，不运行无关转换器、不提升待转换状态。真实重复同步已验证目标匹配成功扩展输出，PC 源和其他字段不变；119 条记录原有的 `CONVERSION_CHANGED` 仍在，未被当作已完成通用转换。此确认不能反推此前从未有人手改过生成树，其余全项目问题也未逐项确定原因。

最终 Unity Play 探针重新生成了两个开始前已存在未提交修改的临时桥接文件：`UnityProject/Assets/NaninovelData/.nani/Transient/Bridging/Server` 和同一 `Transient` 目录下的 `Metadata.json`。检查已记录这两项变化；由于此前只保存了哈希、没有原文件副本，未尝试覆盖还原。存档、设置和字体文件保持不变。

## 动态双人异议修复的后续验证

本轮只补上述一处参数派发，未重做 CG、材质或动画配方。新生成数据只有 Trial09 改变，全部 PC 剧情源哈希不变；命令统计为 23,517 条语义不变的 PC 命令，加 1 条只更新两项参数的命令，原有 309 条替换、488 条插入保持不变。`double-objection-fix/prepared-data-check.json` 确认仍为 22 次 CG、62 次 `Spawn` 加 1 次动态双人异议。

| 检查 | 本轮状态 |
| --- | --- |
| Python 回归 | **94 项通过**：75 项 discovery 加 19 项直接运行的 `test_content.py`；其中新增 5 项覆盖字段保护、冲突、锚点及重复应用。日志为 `double-objection-fix/python-tests.log` 与 `python-content-tests.log` |
| 实际 Trial09 合并检查 | 台词锚点唯一；只更改目标 `CutInKind` 与 `Index`，其余两处异议保持 `Hiro / 2`、`Ema / 2`；专项与配方共 14 项通过，见 `dynamic-objection-merge-tests.log` |
| 本轮正常同步与 Addressables 重建 | **exit 0 / OK，312.2 秒**：336 个 bundle、3,704,893,924 字节；`double-objection-fix/addressables-build.json`，新签名见下文 |
| 构建流程内缓存检查 | 正常构建的后续同步批次及最终检查中，扩展 `cache.hit=true`、`rebuilt=false`；440 个输出文件哈希全部匹配。本轮未额外运行独立重复同步，见 `state-after-build.json`、`final-verification.json` |
| 新 Built 探针 | **exit 0 / PASS 84/84**，匹配新签名；`switch-story-built/20261011-144535-959/report.json`，Play Mode 检查后已恢复原 builder |
| 动态命令与地址读回 | 56 条 `GosubToObjectionCutIn` 参数已核对，4 个不同动态地址均解析到唯一的 `BundledAssetProvider` 位置；其中 `w` 实际加载并测试参数。实际 Trial09 的 playlist 321 / line 461 为 `w / 0` |
| `Index=0` 隔离参数检查 | 对未激活的 bundle prefab 副本实际调用 `SetSpawnParameters(0)`：`is_ema_cut_in=false`，Ema / Hiro 两个层级的本地启用标志均为 true；未执行 `Awake`、`AwaitSpawn`、boot 或存档访问，不能据此声称完整演出已播放 |
| 本轮 Visual 图形取样 | **exit 0 / PASS**：18 个 prefab、163 帧、15 个 CG，匹配本轮签名；`switch-story-visual/20261011-144842-696/report.json`，人工检查范围见下文 |
| 探针后数据与工作区复核 | 构建、Built、Visual 签名一致，输出及生成输入哈希匹配；50 个 PC 剧情源、220 个既有 `.meta`、存档、设置和字体均保持本轮开始时状态，见 `final-verification.json`、`final-audit.json` |
| 本轮全项目 doctor | **仍为 FAIL**：问题集合与上一轮完全相同，23,477 条配方变化、489 条待导入；50 份合并剧情源问题为 0 |
| 完整剧情、`AwaitSpawn` 实际播放、画廊解锁、音频听验、新 Player、冷导入 | **仍未验证**；全项目 doctor 也未因本补丁变为通过 |

本轮配方签名为 `e201df9c022baee80bf802f9aaa68d8152f5a9c41b361da42a2b54f0c2f5c2c3`。本节未另标目录的状态与数据检查文件位于 `tools/unity-rebuild/out/switch-story-research/double-objection-fix/`；Built / Visual 仍位于 `tools/unity-rebuild/out/unity/`。

本轮人工画面检查已查看 `20261011-144842-696` 的 `cutin-contact-1/2/3.jpg`（全部 18 个 prefab）、`cg-contact-sheet.png`（全部 15 个 CG 缩图），以及原尺寸 `ObjectionCutIn_w-2.500.png`、`CutinW2-2.000.png`。所查看样本中未见意外的不透明裂纹黑底或缺失材质；不声称与 Switch 逐像素一致或完整剧情播放通过。机器报告的 `visual_quality_reviewed: false` 保持原样，人工结论单独记录于此。

两个探针退出后，已确认没有此项目的 Unity 进程。此次开始前保存了上述两个临时桥接文件的原始副本，现已恢复到**本轮开始时**的精确字节并核对哈希，见 `transient-restoration.json`；这不追溯还原上一轮只有哈希、未保存副本时的状态。相对于本轮基线，正式文件变化仅为 5 项预期修复文件及构建生成的 Addressables 状态文件，见 `final-audit.json`。

当前为本地工作区改动，**尚未 commit 或 push**。以上保留失败与未验证范围，不把单机局部通过扩大为全项目或全平台通过。

## 用户参考图纠错（背景）

用户随后提供的参考图显示了后方窗格、彩色玻璃斜带及其文字边框和裂纹，与上述隔离取样明显不同。此前的机器 PASS 只证明检查范围内资源可加载、可渲染；人工检查未报错也不能证明外观已经还原。本轮据此撤回任何超出这一范围的外观结论，保留旧报告作为诊断历史，重新做分层对照。参考图的具体获取方式未独立核验。

已确认的具体问题及本轮修复范围如下：

- 人物发光层错误地使用 Sprite 图片 alpha；Ema / Hiro 的发光图含不透明黑区，因而遮住背景。按已恢复的 PC 同族程序改用红通道，并结合 NS 活跃参数 `_transparent` 控制透明度；移除 NS Forward 未使用的 `_Tick` 溶解。RGB 保持原颜色计算，不改源贴图。
- 玻璃阶段的 `m_IsActive` 曲线被生成成 Discrete 绑定，实际取样停在 001。原始 packed 绑定不是 int curve；保留相同关键帧的临时 FloatCurve 副本在同一时刻正确切换到 003。正式重建后的分层报告已确认 2.5 秒时 003 启用，但该修复单独不足以消除粉色斜带。
- 001 / 002 / 003 的源 Sprite 带 Tight 网格，分别有 211 / 207 / 230 个顶点及 627 / 615 / 708 个索引；原导入只保留矩形 PNG，丢失用于裁掉图集扩展像素的网格。现已恢复源几何并完成实际 bundle 读回：3 个不同 Sprite、4 处组件引用，共 648 个顶点、1,950 个索引，顶点最大误差为 0，索引精确一致；未用 AssetDatabase Sprite 替代被测 bundle 对象。
- Story Glasses 额外叠加了 `_luminescence`，而原 NS Forward 的活跃纹理只有 `_MainTex`、`_kirakira`、`_kirakira2`。现移除该额外叠加，保留材质兼容属性。源玻璃与斜带发光 shader 都是 Transparent / 3000，未发现原版通过不同队列让玻璃压住发光层的证据。
- 两份 Story shader 各修复一条 DXBC 向量指令的别名错误：`mul r0.xzw, r0.xxxx, cb1[6].xxyz` 和 `add r1.xyzw, r1.xyxy, (1,0,0,1)` 必须先读取全部旧源分量，再写目的分量。原顺序赋值分别改变了颜色与噪声格点。局部修复各经 1,024 组单精度输入对照；除此之外的程序行不变，未扩改 PC 旧 shader 或通用翻译器。原指令、源 Forward 参数、哈希、数值反例和验证范围保存于 `double-objection-background-fix/shader-corrections-evidence.json`；这些数值检查不替代 Unity 编译与渲染。

以下三份均为 `ObjectionCutIn_w` 在 2.5 秒的 **AssetDatabase 分层诊断**，`complete_visual_gate=false`；没有运行正式剧情、音频或存档。原始报告位于 `tools/unity-rebuild/out/unity/switch-story-visual/`，副本和汇总 `diagnostic-evidence.json` 位于 `tools/unity-rebuild/out/switch-story-research/double-objection-background-fix/`。

| 阶段 | 报告 | 已观察结果 |
| --- | --- | --- |
| 修前 | `20261011-150349-530/report.json`（`before-report.json`） | `only_glow` 有 221,556 个不透明黑像素；完整合成有 96,458 个 |
| 仅修人物发光透明度 | `20261011-150704-068/report.json`（`after-glow-report.json`） | `only_glow` 不透明黑像素降到 0，完整合成降到 10,413；尚不能据此认定其他层或整体外观通过 |
| 激活曲线 A/B | `20261011-151653-932/report.json`（`curve-ab-report.json`） | 实际 Timeline 和原 clip 的直接取样均为 001 开 / 003 关；只改绑定种类的临时副本变为 001 关 / 003 开，与保存关键帧预期一致，未修改持久化 clip |

这些报告仍带旧配方签名，只用于修复过程中的局部诊断；其中只有 `_UnscaledTime` 受采样时刻控制，Unity 内置 `_Time` 未锁定，不能把不同次截图视为全部 shader 相位完全相同。

背景透底调整采用有源依据的同族语义：PC `Background_0Fix` 正常情况下以 noise 作 alpha、以 `_Float` 决定裁剪；NS 保留 Ema / Hiro 的原材质身份、阈值和颜色，换为使用 Noise 纹理的 Performant 版本。本轮保留现有噪声采样和 RGB 近似，仅将透明度改为 `saturate(value)`、阈值改为 `clip(value - _Float)`。**NS 噪声算法仍未精确恢复，也不声称实现了真实折射。** W 背景 Sprite 本身是纯白方块，验证透底需提供后方图像，黑底隔离场景不会凭空出现法庭窗格。

第一轮正式重建后的结果如下。以下签名 `20ac28da4dc66a1f43a3a286e2f4cc5fb8d53f28d89bb3b826c781c96347d3f3` 尚不包含后续斜带排序适配，不能作为该最终修复的验收结果。

| 检查 | 结果与边界 |
| --- | --- |
| 本轮 Python 回归 | **98 项通过**：79 项 discovery 加 19 项直接运行的 `test_content.py`；`double-objection-background-fix/python-tests.log`、`python-content-tests.log` |
| 第一轮 Addressables 构建 | **exit 0 / OK，302.5 秒**：336 个 bundle、3,704,962,049 字节；`double-objection-background-fix/addressables-build.json` |
| 第一轮 Built | **PASS 84/84**；`switch-story-built/20261011-154116-716/report.json`。上述 3 个 Sprite 的几何已从实际 bundle 读回通过；这不证明合成外观正确 |
| 第一轮全量 Visual | `switch-story-visual/20261011-154014-186/report.json` **机测 PASS，人工外观不通过**：`ObjectionCutIn_w-2.500.png` 仍是平粉色斜带，不能把退出成功或阶段检查通过视为问题已解决 |
| 再次分层与排序 A/B | `switch-story-visual/20261011-154518-862/report.json`：`only_glass_texture` 已能显示彩窗图案；去除斜带发光层的交叉取样也显出图案。仅在临时副本中把斜带 glow 的排序从 200 改为 199 后，彩窗与发光层正常合成；没有改材质、重采 Timeline 或修改持久化资产 |
| 最终排序适配后的标准重建 | **exit 0 / OK，279.1 秒**：336 个 bundle、3,704,971,410 字节；`double-objection-background-fix/addressables-final-build.json`。构建内后续检查为 `cache.hit=true`、`rebuilt=false`，见 `state-final-build.json`；不是另一次独立重复同步 |
| 最终全量 Visual | `switch-story-visual/20261011-160706-049/report.json` **PASS**：18 个 prefab、163 帧、15 个 CG，匹配最终签名；人工确认彩窗与裂纹可见、平粉条覆盖消失，剩余差异见下文 |
| 最终 Story Built | **exit 0 / PASS 84/84**，匹配最终签名，`builder_restored=true`；`switch-story-built/20261011-160746-989/report.json`。实际 bundle 的 3 个 Sprite / 4 处引用、648 个顶点和 1,950 个索引再次读回通过；未运行 boot、正式剧情或访问存档 |
| 标题 Visual 回归 | **exit 0 / PASS**，12 个取样；`switch-title-visual/20261011T161021185Z/report.json`。已人工查看三个标题的 6 秒截图，未见缺失材质的粉色错误；标题为隔离取样，`liveRuntimeValidated=false`。该报告不含配方签名，不称与本次 Story 签名匹配 |
| 标题 Built 回归 | **exit 0 / PASS 5/5**，`builder_restored=true`；`switch-title-built/20261011-161042-296/report.json`。未执行 boot 或正式剧情；该报告不含配方签名 |
| 工作区审计及临时文件恢复 | **已完成限定范围复核**：6 个受保护文件、50 份 PC 剧情源保持本轮基线；220 个生成 `.meta` 身份保留，其中仅 3 份经过审阅的几何元数据字节改变，没有非预期元数据变化或 GUID 冲突。两个临时文件已恢复，项目 Unity 进程为 0；见 `audit-post-run.json`、`transient-restoration.json`，详情如下 |
| 最终汇总检查 | `double-objection-background-fix/final-verification.json` 为 **SCOPED_CHECKS_PASS**：Story 的 plan / state / build / Built / Visual 签名一致；1,890 个 Story、173 个 Title 生成输入以及 440 个 Story、36 个 Title 输出哈希全部匹配。Title 仅核对 plan / state 签名，未给不含签名的标题探针补作此断言 |
| 本轮全项目 doctor | **仍为 FAIL**：23,477 条 `CONVERSION_CHANGED`、489 条 `PENDING_IMPORT`，问题集合与第一轮相同；50 份合并剧情源问题为 0。见 `double-objection-background-fix/doctor-final.json`、`doctor-comparison.json` |

该 A/B 将残留粉条定位到同排序层的覆盖关系。源 NS 的 glass / glow 都是 `sortingOrder=200`，Z 与渲染队列也相同；**199 是为 Built-in 管线明确指定覆盖顺序的兼容适配，不是恢复出的原 NS 数值**。正式 C# 修复已严格限定于 `ObjectionCutIn_w` 的该发光层，保留源 dump 与其他 prefab 的排序；新签名重建和新鲜图形取样已确认该适配持久化，彩窗不再被光条盖住。0.7 / 1.3 / 2.5 秒的严格阶段检查分别通过 001 / 002 / 003。没有据此反转 `_Tick`：原 PC 程序在 1 时应显示光条，而材质纹理和 Sprite 图像本身都只是白色光条，换采样来源也不会产生彩窗图案。

最终配方签名为 `430ef74d36c114f0a9fce940bb131becf8d126d08136d0411a1359f5d446f191`。人工已查看本次 `cutin-contact-1/2/3.jpg`（18 个 prefab）、CG 接触表及原尺寸 `ObjectionCutIn_w-2.500.png`，并独立对照用户参考图。彩色斜带、文字边框和裂纹已恢复可见；**这仍不是与参考图完全一致的画面验收**：隔离取样后方仍是柔化红色噪声，没有参考图的法庭窗格和大尺度波纹；裂纹高光的颜色、亮度以及飘散碎片的位置和数量也不同。参考图与探针时刻没有对齐，内置 `_Time` 未锁定，且探针没有加载正式法庭场景，不能仅据这两帧把每项差异归因于 shader，也不能据此忽略差异。机器报告的 `visual_quality_reviewed: false` 保持原样，人工观察单独记录。

法庭窗格另有实际引用链证据：NS `Act02_Chapter06_TrialInit` 设置 `stageName="CourtFinal"`，再由 `System_TrialInit` 加载；当前 PC 的 `Boot.unity` 将 `CourtFinal` 绑定到 `Court_Final.prefab`。该 prefab 的 `Wall_1` / `Wall_2` 渲染器引用 `Court_Wall@7`，其 `_BaseMap` 与 `_MainTex` 均指向已有 `Textures/General/Packed/PerspectiveStage/Court/Background_014_001.png`。w 前最近显式镜头是 Index 0、Ema、Center、Zoom 2；`EndDebate-CutIn` 只处理输入、UI 及异议 prefab 的生成/销毁，没有切换背景或摄像机。这支持现有法庭舞台作为后方窗格来源，不能把参考图相似直接当作缺少独占 CG；尚未实际运行该剧情，也未逐像素核对镜头与合成结果。

原 NS Glasses 确实使用 `_UVRemap`，本实现尚未直接复刻该参数；但导出的独立 PNG 已按原 rect / pivot 重映射，且彩窗单层已正常显示，不能仅凭少了同名参数就判定它是最终根因。真机时间序列、完整剧情、实际解锁、音频、Player 和冷导入的未验证范围继续保留。

本轮恢复的是 `Transient/Bridging/Server` 和 `Transient/Metadata.json` **开始时已含本地修改的精确字节，不是 HEAD 版本**；恢复后哈希一致，未覆盖用户先前修改。审计相对基线记录 11 个预期代码、文档和 Addressables 状态文件变化，唯一新出现的 dirty 路径为 `tools/content/tests/test_switch_story_geometry.py`。这份审计限定于保存的基线文件及上述 50 份剧情源，不扩称全部 PC 资源或全部用户配置从未改变；`audit-post-run.json` 自身为证据记录，构建与运行结论来自各新鲜探针及汇总报告。

最终变更范围的 `git diff --check` 退出 0，另对本轮修改及未跟踪的正式文本逐项检查，无行尾空白，正式输入未被忽略。全局 `git diff --check` 仍报告旧 `ManosabaCutIn` shader 和 `port_cutin_shaders.py` 的格式问题；这些文件与本轮开始时的哈希一致，没有扩大修复范围。当前仍是本地改动，**尚未 commit 或 push**；全项目 doctor 的 FAIL 与上述未验证范围保持明确。
