# 小切入立绘的原版／修正选项

图形设置中的“论破立绘样式”提供“原版／修正”，默认原版。范围是 13 人的 `CutinSmall_*` 竖条切入：Alisa、AnAn、Coco、Ema、Hanna、Hiro、Leia、Margo、Meruru、Miria、Nanoka、Noah、Sherry。它不替换普通对话立绘、剧情 CG、艾玛／希罗横条或双人横条，也不改变切入触发条件和语音。

`WitchTrials.Models.EnhancementSettings.UseCorrectedCutInPortraits` 使用现有设置存储，缺少旧记录时取 `false`；不写主线进度或解锁状态。这个开关与既有艾玛横条三张立绘轮换选项互相独立。普通回标题、读档和开始新游戏保留设置；设置页的重置恢复原版。

## 图像与定位

用户选定的 16:9 修图预览包含人物、玻璃和背景，不能直接盖在切入画面上。正式输入是在这些预览的基础上，用图像工具生成的透明人物 PNG；补足预览截掉的下缘，覆盖到原人物 Sprite 相同的躯干／下摆范围，无需补成完整脚部。保持角色服装、神态、两臂与朝向，希罗以 `Hiro-v2.png` 为参考，艾玛使用用户指出帽子缺失、发带过粗后重新修正的版本。正式 PNG 是图像工具直接输出的透明图，没有脚本抠图或缩放处理。背景、玻璃、独立投影和特效不烘焙进人物图。

每人保留独立 PNG 及完整透明留白。源像素不做横纵独立缩放，也不强行拉到旧 PNG 尺寸。导入器以 alpha 高度计算单一 PPU，再用头顶锚点计算 normalized pivot；坐标使用左上原点。显式锚点来自 `manifest.json` 的 `alignment.original_anchor_px`／`corrected_anchor_px`。未提供时，使用两张图 alpha 包围盒的顶部中心，并在报告中明确记录这个默认定位方法；它不能代替人工构图检查。alpha 包围盒阈值为 16/255。

运行时只替换实例里的 `CharaRig/Chara` 和 `CharaRig/Chara_Shadow` 两个 Sprite 引用。原 Transform、主层／阴影材质、遮罩、排序、玻璃、Timeline、音效均保留。原 prefab 与原人物 PNG 不重建或覆盖。切回原版、实例禁用或销毁时恢复原 Sprite，并释放正在加载和已经持有的修图资源；修图缺失或加载失败时保留原版。

同一实例的 `OnEnable`、生产 `ObjectionCutIn.AwaitSpawn` 与重复调用共享正在进行的修图请求；生产播放等待当前请求完成。后日谈开关变化不重启人物加载，也不释放已持有的修图。实际人物样式变化使旧请求失效时，仍选择修正模式的等待者继续等待正在运行的替换请求；调用方取消继续向生产流程传播。

## 正式输入与正常流程

正式图像位于 `content-overrides/corrected-cutin-portraits/{Character}.png`，其中 `AnAn.png` 区分大小写。`manifest.json` 的第 2 版必须包含全部 13 人。新修图使用源路径及严格 PNG 文件 SHA-256；原 Sprite 使用固定 Unity 资产路径、固定审核画布和必填的 `original_rgba_sha256`，旧 `original_sha256` 保留审核时 PNG 编码的来源记录。格式见同目录的 `manifest.schema.json`。原资源需先由自有、同版本的 Switch 资源完成 [剧情演出移植](switch-story-port.md)。该功能不从用户个人目录或临时生成目录读取正式输入。

角色键、正式 PNG 和原 Sprite 使用 `AnAn`，而原游戏实际 prefab 的文件名／根名为 `CutinSmall_Anan`，地址为 `Naninovel/Spawn/CutinSmall_Anan`。13 人库存核对确认只有这一处命名差异。Adapter 仅将精确的 `Anan` 映射到 `AnAn`；Builder 集中生成真实 prefab 名及地址，Probe 复用该地址，不做宽泛的大小写忽略，也不重命名原 prefab 或图像。

原图的像素身份是无颜色转换、无 alpha 预乘的 RGBA8 原始字节 SHA-256，按左上原点逐行、每像素 R/G/B/A 顺序计算。Unity 验证使用 `linear:true` 的临时 Texture2D，并将 `GetPixels32` 的底左行序翻转为顶左行序。路径归属、原 Sprite 大小／PPU／pivot、两槽引用及朝向门禁继续执行；同尺寸图像修改任一像素仍会改变身份，不能仅凭尺寸接受原图变化。

本轮确认系统 Python 的 Pillow 11.3.0 与项目 venv 的 12.3.0 会把相同像素编码成不同 PNG 文件字节。13 张图在两环境的解码 RGBA 身份全部一致；用 12.3.0 对当前未改动像素仅在内存重编码，13／13 精确还原旧审核文件 hash。因此原图门禁核对明确的像素身份，修图仍严格核对文件 hash，未增加任意替代 hash 允许列表。证明报告为 `tools/unity-rebuild/out/options-art-epilogue-20261012/portrait-png-encoding-proof.json`，已补入真实 Unity Builder 的 13 项解码像素与修图 Sprite 几何读回，仍与完整 Addressables 构建、游戏运行验收分开记录。

在仓库根目录使用现有入口：

```powershell
python tools/content/content.py sync
python tools/content/content.py doctor
python tools/content/content.py build
```

`CorrectedCutInPortraitContentBuilder.EnsureApplied()` 已接入普通 `ContentSyncBridge`，包括没有 `res` 源变化的请求，并在 Windows／Android Addressables 构建入口执行。它在通用 Addressables setup 之后补齐 `Manosaba.CorrectedCutInPortraits` 组，使用独立地址 `Naninovel/CorrectedCutInPortraits/{Character}`。没有 manifest 时返回 `disabled`；manifest 已存在但缺图、hash 不符、原 Sprite／节点不同、定位越界或地址冲突时使同步／构建失败，不接受部分 13 人输入。

导入保留已有修图 `.meta`／GUID，检查实际 Texture2D 尺寸、Sprite rect、PPU、pivot、锚点世界坐标及人物 alpha 高度。输出没有变化时不重写图像或导入参数，但仍校验资源并恢复地址条目。发现生成目标存在未经前次成功报告确认的修改时拒绝覆盖。

`UnityProject/Library/ManosabaContent/corrected-cutin-portraits-state.json` 只由完整成功的导入更新，记录签名、原／修图几何、透明范围、定位误差、输出 hash、GUID、地址和缓存情况。失败详情另写同目录的 `corrected-cutin-portraits-error.json`，不覆盖上一份成功确认；这样修正错误输入后，正常同步仍能识别此前已确认的生成目标。普通构建报告另记录 `corrected_cutin_portraits_status`、`rebuilt`、`signature`、`count` 对应字段。导入报告的 `render_verified=false` 是有意保留的：导入参数正确并不等于构图验收通过。

全局转换签名策略保持原样。Bridge 变动会使普通 job 的账本签名失效；本次 `ObjectionCutIn` 入口改动也属于既有 Story 配方依赖，因此首次正常同步可能重新准备、重建 Story 资源。不能通过伪造旧签名或手填缓存略过。完成一次成功转换后，后续批次应命中缓存；需要限定处理范围时使用已有 `--file` 选项，活动扩展控制的剧情仍会正常处理和确认。

### 真实设置 UI 的唯一注册

第三次实际运行探针明确发现生产入口冲突：标题和控制面板均通过 `GetUI<ISettingsUI>()` 选择设置页，实际选中 `Naninovel.UI.GameSettingsMenu`（`Naninovel<Runtime>/UI/SettingsUI`），同时存在真实 `WitchTrials.Views.OptionsUi`（`Naninovel<Runtime>/UI/OptionsUI`）。失败报告为 `tools/unity-rebuild/out/unity/enhancement-settings/20261012-224609-032/report.json`。默认页来自本地 `Assets/NaninovelData/EditorResources.asset` 的 `UI/SettingsUI` 包资源记录；初始化时它作为 MasterProvider 资源先注册。默认页没有新增选项，不能通过让探针强制选 typed UI 或调用私有 `ActivateUi` 把这个生产问题写成通过。

正式修复位于现有 `NaninovelConfigBuilder.EnsureOptionsUiResource()`，由配置导入、普通 `ContentSyncBridge`（包括无源变化）、Windows／Android Addressables 构建共用。既有 `OptionsUI.prefab` 尚未导入时返回 `disabled`；存在时必须确认根节点唯一设置组件为 `WitchTrials.Views.OptionsUi`。门禁从本机包 `Prefabs/DefaultUI/SettingsUI.prefab.meta` 读取实际 GUID 并与 AssetDatabase 核对，通过 `SerializedObject` 枚举所有原始 `resourceCategories/Resources` 条目，逐一确认每条 `UI/SettingsUI` 都指向包默认 GUID，才移除这一别名。任一同名条目属于用户覆盖时，在修改前拒绝；没有改动 Gallery 或其他 UI。

门禁保留类别与记录的原始次序，比较移除已核准条目后的预期列表和实际列表。`options_ui_resource` 报告包含移除数量、是否修改、前后记录指纹及 `other_records_preserved`；指纹以未折叠的类别、name／pathPrefix／guid 原始数组计算，字典覆盖重复 path 不能代替这项核对。无目标条目时不写资源资产；移除后只保存这一 EditorResources 资产，保留其 GUID。改动前的配置生成器源文件与本地记录原文件／hash 已留存于本轮审计目录 `options-ui-resource-before/`。

该修复由跟踪的配置生成器与普通同步／构建入口复现，不依靠提交本机生成的资源记录或修改 Naninovel 通用 UIManager。构建后重新运行原探针：保持生产 `GetUI<ISettingsUI>()` 与 Present／Dismiss 生命周期，要求注册的 `ISettingsUI` 唯一且 generic 选择正是真实 OptionsUi；报告记录选中对象的具体类型、路径、可见性及两行控件状态。缺行或选择不符明确失败。

门禁新代码已通过真实 Unity 编译，第一批正常同步已成功移除恰好 1 条包默认 SettingsUI 记录，日志已冻结为 `tools/unity-rebuild/out/options-art-epilogue-20261012/options-ui-first-removal.log`。原始记录指纹从 `538cccacdcad68eed6bffba8f32beb003a5405260c72dcd24dc98e5f1ee89d14` 变为 `51a8f46d50f69b3507efc58d46cf3c77e5d7428ae90cd7665b9b1dab6126a9c1`。独立字节核对确认本地 EditorResources 只删除了这条记录的 3 行，其他文件字节完全保留。后续实际运行报告已确认 generic 唯一选中真实 OptionsUi，范围见下文；首次移除成功日志本身不代替运行验收。

探针还修正了一处调用时序：不能对已打开设置页重复调用生产 `PresentAsync`。原探针先切到图形页，随后又 Present 已可见的 OptionsUi；`NaninovelScreen` 再次启用了全部子 Canvas／raycaster，但没有再次触发设置页激活逻辑，导致声音页 `VoiceVolume/Slider/.../Handle` 抢占点击。路径证据记录于失败报告 `20261012-231115-433/report.json`。现在只在未显示时 Present、仍显示时 Dismiss，保留已打开页的状态；没有修改生产 `OptionsUi.PageCanvas` 或直接调用其私有激活方法。修正后真实 raycast、设置事件和后续验证通过。

### 剧情准备阶段的内存修复

本轮首次正常构建在 Python 剧情准备阶段发生 `MemoryError`，尚未进入 Unity。`tools/content/switch_story_scenario.py` 现在按 bundle 顺序读取并释放 UnityPy 环境，把选中的源图、PC 基线和合并结果暂存为 JSON；每次只合并一份剧情，并使用分块 JSON 写入和文件比较。所有合并及汇总调用数通过校验后才发布生成输出，源输入、身份核对、冲突检查和调用数量门禁保持原有规则。

本机独立导出核对报告为 `tools/unity-rebuild/out/options-art-epilogue-20261012/scenario-memory-verification.json`：154 份记录的原合并报告与输出 hash 一致，重复执行时记录及输出 mtime 一致，测得峰值工作集约 223 MiB。这是该准备阶段的字节、记录与重复运行证据，不代表 Unity 导入、Addressables 或游戏运行验收。相关回归测试位于 `tools/content/tests/test_switch_story_scenario.py`，包括晚期合并／汇总失败不发布输出，以及释放 bundle 环境与重复身份拒绝。

### 编译进程失败后的本机串行重试

本轮正常构建随后在 Bee 的 Player 脚本编译阶段出现多个 C# 编译器进程异常退出（包括 `-1073741571`、CLR `0x80131506` 和 `-532462766`）。当时 C 盘仅剩约 344 MB，可用物理内存约 832 MB、可用虚拟内存约 1.2 GB。已保存本次失败报告 `tools/unity-rebuild/out/options-art-epilogue-20261012/bee-compiler-failure.json` 及编译日志；这些是环境压力下的编译进程失败，不能写成代码或游戏运行验收通过。

本机 Unity 附带的 `bee_backend --help` 支持 `-j/--threads`，其环境解析使用 `BEE_BUILD_THREADS`（最小值 1）。本次使用单构建线程，并将该调用及子进程的 `TEMP`／`TMP` 指向项目下的新临时目录，保留现有资源、bundle 和用户文件。它不修改机器或用户的持久环境变量，也不调整系统页文件。[Unity 命令行文档](https://docs.unity3d.com/cn/6000.0/Manual/EditorCommandLineArguments.html) 的 `-job-worker-count` 属于 Unity JobQueue 参数，不代替 Bee 编译并发限制。

在确认前一轮构建已正常退出后，可从仓库根目录使用以下局部重试方式；目录名仅是本轮示例：

```powershell
$taskPreviousBee = $env:BEE_BUILD_THREADS
$taskPreviousTemp = $env:TEMP
$taskPreviousTmp = $env:TMP
$taskBuildTemp = Join-Path (Get-Location).Path 'tools/unity-rebuild/out/options-art-epilogue-20261012/build-temp'
[IO.Directory]::CreateDirectory($taskBuildTemp) | Out-Null
try {
    $env:BEE_BUILD_THREADS = '1'
    $env:TEMP = $taskBuildTemp
    $env:TMP = $taskBuildTemp
    python tools/content/content.py build --file 'Assets/#WitchTrials/Scenarios/Act02_Chapter05/Act02_Chapter05_Adv09.asset.json' --timeout 7200
    if ($LASTEXITCODE -ne 0) { throw "Content build failed: $LASTEXITCODE" }
}
finally {
    [Environment]::SetEnvironmentVariable('BEE_BUILD_THREADS', $taskPreviousBee, 'Process')
    [Environment]::SetEnvironmentVariable('TEMP', $taskPreviousTemp, 'Process')
    [Environment]::SetEnvironmentVariable('TMP', $taskPreviousTmp, 'Process')
}
```

串行与临时目录迁移只减少并发及 C 盘临时写入，不能保证在仍然缺少内存／系统盘空间时成功。本轮串行重试日志为 `tools/unity-rebuild/out/options-art-epilogue-20261012/content-build-serial.log`：它已越过 C# 编译，但在随后 `GenerateLocationListsTask` 阶段出现 native access violation（退出 `3221225477`）。

已使用本机与 Unity.dll 的 GUID／Age 匹配的 PDB 离线解析栈，报告为 `tools/unity-rebuild/out/options-art-epilogue-20261012/unity-native-stack-symbols.json`。顶端是 `DynamicHeapAllocator::CreateTLSFBlock`／`Allocate`，下层为 `SharedTextureData`、`Texture2D::CreatePixelDataWhenReading` 和纹理反序列化，经 `SpriteRenderer::AwakeFromLoad` 到 `GetTypeForObjects`。这将故障定位到读取纹理时的 native 内存分配路径；未看到明确 OOM 报错，不能单凭栈声称已经证明内存耗尽，也不能把它写成 typetree 重复字段或 identifier 排序故障。

随后通过正常 `content.py build` 完成一次只改变压缩并发的实验：保留前轮 `BEE_BUILD_THREADS=1` 和临时目录环境，只在构建期间创建原本不存在的 `ProjectSettings/ScriptableBuildPipeline.json`：

```json
{"threadedArchiving":false,"prefabPackedHeaderSize":2}
```

SBP 2.3.8 的 `threadedArchiving=false` 使归档逐 bundle 执行；它没有 `MaximumArchiveThreads` 配置。`prefabPackedHeaderSize:2` 显式保持当前无设置文件时的布局基线，避免其兼容旧项目的加载逻辑改用 4。外层进程在结束后核对临时文件未被修改，再恢复原不存在状态；不改 Source／PackageCache，也不删除构建缓存。恢复审计 `tools/unity-rebuild/out/options-art-epilogue-20261012/temporary-pipeline-settings.json` 已记录 `restored:true`、`build_exit:1`，实际设置文件已不存在。

实验日志为 `tools/unity-rebuild/out/options-art-epilogue-20261012/content-build-single-archive.log`，Unity 崩溃日志另存 `unity-single-archive-crash.log`。该轮仍在 `GenerateLocationListsTask` 的 `GetTypeForObjects`／`GatherMainAndReferencedSerializedTypes` 路径发生 native access violation；顶端连续五个分配器帧的地址与前轮相同，中间加载帧不同。关闭并发归档未解决本次故障。压缩任务本身会等待完成后才进入 location-list 阶段，不能把压缩线程、明确 OOM 或代码逻辑错误写成已确定根因。这是此前两轮构建失败的记录。

用户腾出系统盘空间后，保持上述进程内 `BEE_BUILD_THREADS=1`／项目临时目录环境，再次调用正常 `content.py build` 已成功；没有持久 SBP 配置或新增 Unity 资产卸载补丁。新鲜成功报告 `tools/unity-rebuild/out/options-art-epilogue-20261012/addressables-space-retry-success.json` 与对应 `unity-space-retry-success.log` 已冻结，报告时间为 `2026-10-12T22:37:21.2034296Z`。结果为 338 个 bundle、3,740,245,168 bytes、28,660 entries、29,903 locations；标题／剧情／后日谈／13 人修图门禁均为 `OK`、`rebuilt=false`。独立核对磁盘上的 bundle 数量和总大小与报告完全一致，新 `catalog.bin` 为 5,634,189 bytes，生成时间为 `2026-10-12T22:39:52.216491Z`。这是本轮 Windows Addressables 构建成功证据，不代表游戏运行、Player 或 Android 构建验收。

## 交付与验证状态

应交付的正式输入包括 13 张透明 PNG、manifest／schema、图像生成来源记录、`CorrectedCutInPortraitAdapter.cs`、`CorrectedCutInPortraitContentBuilder.cs` 及两个 `.meta`；同时包含 `ObjectionCutIn` 的入口、`ContentSyncBridge`／`AddressablesBuild` 的调用、`NaninovelConfigBuilder` 的设置 UI 注册规则、设置服务与设置 UI，以及对应探针。生成的 GameContent、EditorResources 资源记录、Addressables 组、bundle、Library 状态、日志和用户存档不作为源配方提交。

当前已完成 Adapter／Builder 与扩展探针的 Game／Editor 离线编译，以及 13 张正式 PNG 的文件 hash、原 Sprite 的解码像素身份、透明度、锚点与计算 pivot 范围核对。首次 Unity 编译发现新 Builder 的 `.meta` GUID 长度错误，已修为 32 位 `491469c85f1449eb97f18630af3d2a4c`，并核对在 Assets／Packages 中唯一；这也说明离线编译不能代替 Unity 对源资产和 metadata 的真实导入。

此前正常构建的同步批次已由真实 Unity 成功导入／验证全部 13 人，原图顶左 RGBA8 身份全部命中审核值，修图导入几何通过；当时 Builder 状态签名为 `2f191bceafbdb3decbf5a3435f83dde936b3c652eff326542f8c7407e878bfb9`。最新 `Anan` 映射修复已通过正常 `content.py build` 的真实同步阶段，结果为 `unchanged:156`、`pending_import:0`、`failed:0`；13 人 Builder 新签名为 `719937286a30b3b981e2edc2b4ec4e8608984e0c2057c810a2983f95f0ba46ae`、`rebuilt=false`，杏杏报告地址为 `Naninovel/Spawn/CutinSmall_Anan`。

最终正常 `content.py build` 已退出 0，日志为 `tools/unity-rebuild/out/options-art-epilogue-20261012/content-build-delivery.log`。冻结的 `addressables-delivery-success.json`／`unity-delivery-success.log`／`content-delivery-success.json`／`portrait-delivery-success.json` 确认构建 `OK`，新报告生成时间为 `2026-10-12T23:21:34.6677285Z`：338 个 bundle、3,740,245,647 bytes、28,660 entries、29,903 locations，实际磁盘数量和总大小与报告一致；新 catalog 为 5,634,189 bytes。修图门禁为 13 项 `OK`、`cache.hit=true`、`rebuilt=false`，签名为上述 `719937…`。设置 UI 资源门禁重复调用为 `removed_count=0`、`other_records_preserved=true`，前后原始记录指纹均为 `51a8f46d50f69b3507efc58d46cf3c77e5d7428ae90cd7665b9b1dab6126a9c1`。这是包含映射修复的正式 Windows 内容构建确认；随后使用该新构建的最后一轮运行回归也已退出 0，结果和验收边界如下。

成功构建后文件幂等报告 `tools/unity-rebuild/out/options-art-epilogue-20261012/after-build-portrait-idempotence.json` 确认：65 项原图／修图 PNG 与 meta、原 prefab 的 hash 和 mtime 均未变化。同期保护文件报告 `after-build-protection.json` 中 9 项全部与本轮运行验证前的快照相同，其中 6 项实际配置与存档也与任务开始时的基线完全相同；另 3 项编辑器瞬态记录仅与运行验证前快照一致，不声称它们自任务开始以来未变。这些报告的边界是构建结束后、运行验收前。

最终新构建的实际运行报告 `tools/unity-rebuild/out/unity/enhancement-settings/20261012-232500-560/report.json` 为 `PASS`，对应日志为本轮审计目录中的 `enhancement-runtime-delivery.log`，进程退出 0。前一轮 `20261012-231450-086` 的 PASS 和 `enhancement-runtime-anan.log` 保留为回归历史；最终结论采用新构建之后的报告。此前默认 SettingsUI 的明确失败、重复 Present 的 raycast 失败和 `AnAn` 地址遗漏均保留失败记录；没有将它们隐藏或记作通过。本次已独立核对以下结果：

- 生产 generic 始终选中唯一的 `WitchTrials.Views.OptionsUi`，通过生产 Present／Dismiss 和真实 raycast 提交选项。旧设置缺少新记录时为原版／后日谈开启；存储及读回修正／关闭成功，未移除无关设置记录，游戏状态重置保留选项。
- 13／13 人从 bundle 加载，在主层与阴影两槽完成原版／修正／还原，材质和取样 Transform 保持一致，disable／reenable／destroy 的资源租约断言通过。杏杏实际从 `CutinSmall_Anan` 加载并使用 `AnAn` 修图。
- Alisa 的真实 `ObjectionCutIn.AwaitSpawn` 覆盖加载中切后日谈和并发等待；Hiro 覆盖真实取消及成功 Timeline 播放；Ema 覆盖 pending 中禁用及迟到结果拒绝。本次三例均实际观察到 pending，但未宣称清空 bundle／磁盘缓存。
- 后日谈开／关覆盖进度值 0、11、12 的入口门禁，以及 built 结尾最后四条命令。关闭时未进入后日谈并返回标题、重看入口隐藏；开启时进入稳定首屏并可通过实际标题按钮重看。原结尾 fade 1.8、delay 0.9、wait true 保留，结尾进度为 12。
- 预置内存游戏存档哈希前后相同，受控 Play Mode 导入失败保留成功确认且错误另存；场景设置／Play Mode start scene／构建模式恢复读回通过。本次初始与结束均为同一干净空场景。保存隔离使用三个内存 handler，不把内存模拟当作加载了真实用户旧存档。

33 项直接探针加载均为 `BundledAssetProvider`，17 张最终运行截图已输出。人工查看了本轮设置两张、Alisa 原／修／还原、Ema／Hiro 修图、Hiro 真实 Timeline，以及后日谈开／关标题；结尾进入和重看首屏与前一轮已查看的两张 PNG 文件 hash 完全一致。未见截图与报告矛盾；Alisa 还原画面与原版一致，关闭标题没有后日谈按钮、开启时可见，艾玛保留帽子和正常发带，两种后日谈进入路径均显示无人烟花天台和相同首段文字。完整 13 人动画与剧情触发、全部后日谈 42 页／ED 电影、独立冷副本、Player、Android 和实体手柄尚未由本次探针验收。`ui_clicks` 的 60 项是选择状态确认记录，包含已选中时跳过点击的情况，不是 60 次实际点击；进度门禁循环重新调用标题呈现事件，不能据此单独宣称同一标题画面设置变化后的即时刷新已验收。

最终退出阶段仍有日志残留：`enhancement-runtime-delivery.log` 的退出 Play／恢复场景期间出现 4 次 `Assertion failed on expression: 'pred(*previous, *i)'`，进程清理时出现 `Persistent allocates 6 individual allocations` 提示，与前一轮相同。报告 `errors:[]` 的监控范围仅为 `LogType.Error`／`Exception`，没有计入 Assert；因此 PASS 表示上述功能断言通过，不表示整份日志没有断言或分配泄漏提示。当前尚未定位这些退出提示的来源，不能据此认定为本功能错误，也没有将其隐藏。

最终运行后的只读保护审计 `tools/unity-rebuild/out/options-art-epilogue-20261012/final-protection.json` 为 `PASS`：6 项实际配置与存档的字节 hash 均与任务开始和运行验证前的快照相同，内存存档未变，场景恢复通过。3 项生成瞬态文件单独观察；它们不全部等于先前快照，不将其写成受保护文件未变化。同期 `final-portrait-idempotence.json` 为 `PASS`，65 项原图／修图 PNG 与 meta、原 prefab 的 hash、字节数和 mtime 均保持第一次成功导入后的值，变化数为 0。

最终包含映射修复的正常 Windows 内容构建和新构建的上述运行回归均已通过，保留未覆盖范围和退出日志提示。本轮尚未 commit／push。
