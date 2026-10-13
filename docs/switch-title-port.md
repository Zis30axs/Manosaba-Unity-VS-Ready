# Switch 标题演出移植

这是现有 `res → sync → build` 流程中的可选标题资源配方。它使用协作者自己导出的 Switch RomFS `Data`，不随 Git 分发游戏资源，也不要求运行原游戏。普通 PC 资源仍按 [开发指南](../DEVELOPMENT.md) 完成首次导入。

## 启用与日常同步

在仓库根目录运行，参数指向自己的 RomFS `Data` 目录：

```powershell
python tools/content/content.py sync --switch-title-source "<RomFS/Data>"
```

该入口准备本地 `res/.manosaba/switch-title/plan.json` 及所需源数据，再由正常 Unity 内容同步应用。之后继续使用常规入口；不要手动复制生成资源或执行另一套导入器：

```powershell
python tools/content/content.py sync
python tools/content/content.py doctor
python tools/content/content.py build
```

共享的是准备配方、转换器、运行时代码、shader 和各自所需的 `.meta`；`res/`、`Assets/GameContent/`、`Library/` 中的本地数据不会通过 Git 提供。另一台机器须提供自己的对应资源并重新准备。没有本地标题计划时，此扩展返回 `disabled`，普通同步和构建照常进行。

## 构建行为

`SwitchTitleContentBuilder.EnsureApplied()` 由 `ContentSyncBridge` 在普通转换和 Addressables setup 之后执行，也在 Windows/Android 内容构建前执行。它复用 `PrefabBuilder.RebuildEditable` 和 `ScriptableObjectConverter.RebuildScenario`；后者使用现有共享转换器，在临时 `Naninovel.Script` 上重建已合并的完整剧情图，通过参数及播放列表检查后才原位更新资产。普通 `ApplyEditable` 仍保留安全字段编辑限制。

输入和配方签名一致、目标仍完整且生成文件哈希一致时，跳过 prefab/剧情重建；缺失或被普通导入覆盖的目标会重新生成。PNG 按内容哈希复制，保留现有 `.meta` 与 GUID；导入器检查完整尺寸、单 Sprite 的 PPU/pivot/border 及纹理 wrap。每次调用仍补齐 `Manosaba.SwitchTitle` 地址组，防止通用 Addressables setup 移走条目。新组复制默认组 schema，再使用本地构建/加载路径，不改默认组。

需要重建时先检查未保存场景，标题转换在临时空场景完成并恢复原活动场景。发现转换失败、缺失脚本、空剧情播放列表、被缩小的纹理或地址冲突会使同步/构建失败，而不是把缓存记为成功。

实际结果保存在 `UnityProject/Library/ManosabaContent/switch-title-state.json`，包括输入签名、输出哈希、转换器警告、GUID、导入尺寸、剧情命令数和地址条目。该报告不是画面验收：`render_verified` 保持 `false`。构建结果另外记录本次标题配方状态。

## 证据与范围

发行商 Re,AER 的[官方公告](https://prtimes.jp/main/html/rd/p/000000012.000066682.html)说明 Switch 版先行加入新 CG、少女们的新 cut-in，并针对掌机游玩优化；“先行”不能表述为永久平台独占。这份公告没有公开说明烟花标题的精确解锁条件。

本次配方中的标题图层、对象引用和演出参数以用户自己的 Switch 导出数据为输入。烟花标题按最终章完成后的标题状态接入；这项解锁行为须结合本地剧情/状态数据和实际存档进度验证，不应仅凭宣传公告推断，也不应把所有进度的标题替换成烟花版本。

本地 Switch `System/System_Title` 给出了明确分支：`g_gameProgress <= 5` 使用 `Stills/1_1` 和效果 1，`6..11` 使用 `Stills/2_1` 和效果 2，`> 11` 使用烟花图 `Stills/3_1` 和效果 3。PC 的 `Act02_Chapter05_Adv09` 已有 `g_gameProgress = max(12, g_gameProgress)`，因此不需要修改存档或增加新的解锁变量。

合并结果保留 PC 输入、菜单、音乐顺序和原有模糊/缩放命令，仅接入三阶段画面选择、效果生成与退出清理。两个背景命令仅替换画面选择参数，保留本地时长、颜色、位置和缩放编辑；已合并输入可重复准备。普通同步对该精确标题任务延后到合并配方执行，成功后仍按原 PC 源文件确认状态。计划中的 PC 输入哈希不一致时拒绝转换；本地计划也参与标题的增量签名，配方变化不会遗漏同步确认。

Switch 原 shader 与本项目渲染管线不同，移植 shader 是对原效果的近似实现。恢复相同贴图、材质参数或对象数量不能证明像素一致。实际验收须使用新生成内容，在多个时间点检查标题图层、透明混合、烟花/粒子变化，并检查未解锁与已解锁状态；旧 Player、旧 bundle、静态资源加载及编译通过均不能代替这项验证。不要为截图改写用户存档。

其中光照动画曲线、图层、粒子参数来自导出数据；Fog/Noise 的程序化着色和 `TitleScreenEffect.AwaitSpawn` 方法体为兼容重写。源 Fog/Noise 不采样 `_MainTex`，挂载的背景 Sprite 只提供几何与 UV；粒子则使用导出的柔边纹理并绑定独立材质，避免变为纯色方块。

尚未运行的导入、幂等性、Addressables、Play/Player 或图形检查，应在交付说明中明确保留为未验证；不要把本地修改描述为已经提交或推送。

## 可追踪交付文件

- 配方：`tools/content/switch_title.py`、`switch_title_scenario.py`，以及现有 `content.py` 的入口和标题依赖登记。
- 运行时：`WitchTrials.Views/TitleScreenEffect.cs`；着色器：`Assets/Shaders/ManosabaSwitchTitle/`。
- Unity 转换：`SwitchTitleContentBuilder.cs`、`SwitchTitleAssets.cs`，以及现有 `PrefabBuilder`、`JsonToSerializedApplier`、`ScriptableObjectConverter`、`ContentSyncBridge`、`AddressablesBuild` 的扩展。
- 验证入口：`SwitchTitleVisualProbe.cs`、`SwitchTitleBuiltProbe.cs`。前者取样画面，后者使用真实已构建内容运行隔离 Play 检查。
- 新 Unity 脚本、shader、include 文件和目录的 `.meta` 一并保留。`BuildAndroidPlayer.cs` 仅为原有 Android 专用引用补充相同的平台编译保护，解决 Windows Editor 编译阻断。

以上源码/配方应与本说明一起共享；不提交本机导出游戏内容、报告、缓存或存档。当前工作区中已有的字体、cut-in、Addressables 设置等其他改动需独立审查，不能整体混入标题移植提交。

## 2026-10-11 本机验证结果

以下均针对本次生成内容；报告和截图为被忽略的本机产物，不作为协作者的正式输入。

| 检查 | 结果与范围 | 本机证据 |
| --- | --- | --- |
| 导入、转换与重复同步 | 三套 prefab、21 张纹理、合并标题的 36 条命令已由 Unity 读回；转换无警告，重复应用不重建，原 PC 标题 GUID 保留。合并检查覆盖保留本地背景字段、重复执行及冲突拒绝。 | `UnityProject/Library/ManosabaContent/switch-title-state.json` |
| 图形取样 | Direct3D 11 下三套标题各取 0/1/3/6 秒，共 12/12 PASS；检查默认光照动画、柔边粒子材质绑定、背景模糊与图层，人工查看了第一套和烟花画面。 | `tools/unity-rebuild/out/unity/switch-title-visual/20261011T100510410Z/report.json` 及同目录 PNG |
| 全量 Windows Addressables | 本次构建成功，335 个 bundle、3,631,334,013 字节，标题组含 5 个条目。 | `tools/unity-rebuild/out/unity/addressables_build_result.json`，生成时间 `2026-10-11T10:41:08.7595859Z` |
| 已构建内容的隔离 Play | 5/5 PASS，所有条目均使用 `BundledAssetProvider`。读回三套特效、烟花纹理及标题脚本；检查进度 0/5/6/11/12 的选择。共享 `AwaitSpawn` 在第一套上实测约 2.700 秒、重入及禁用恢复通过。 | `tools/unity-rebuild/out/unity/switch-title-built/20261011-105136-970/report.json` |

三套源 prefab 的粒子系统数量分别为 **1、0、2**；第二套的呼吸效果来自 SpriteRenderer 光照图层。第一次已构建内容检查错误地要求每套都含粒子，已修正检查后复测同一 catalog；没有为满足测试修改游戏资源。最终报告确认退出 Play 后恢复原构建模式，未启动游戏引擎或执行剧情、未访问存档。

配方签名为 `15a931d7c8d6a94d643ad8cec18308ed66032dc3392b8ec4e4e130c3cf4f5d36`。Python 准备和场景合并、Unity 转换读回、图形取样与真实 bundle 加载分别核查，不能把其中一项通过扩展为其他范围已经通过。

全局 `doctor` 尚未通过：标题同步前的报告有 23,528 条 `CONVERSION_CHANGED` 和 490 条 `PENDING_IMPORT`，其中标题的一条 pending 已在最终同步中处理，其他内容不属于这次标题移植；通用转换器配方变化也会使更广的资源签名失效。本次没有对这些资源强制全量转换。全量构建日志仍含 Unity 原生对象标识断言，因此构建成功不代表整个工程没有警告或问题。

图形取样没有菜单和黑色入场遮罩；隔离 Play 只读标题命令以验证分支，没有执行完整标题/结局流程。实际存档的解锁流程、独立 Windows/Android Player、独立副本/冷导入及与 NS 的逐像素对照仍未验证。

构建时项目盘空间不足，已保留全部生成缓存并将 `Library/BuildCache` 迁到另一块本机磁盘，原位置使用目录联接；迁移前后 29,943 个文件、12,341,735,775 字节一致。记录见 `tools/unity-rebuild/out/unity/switch-title-cache-migration.json`。游戏资源、源码和存档未随此次缓存迁移移动。

协作者按上面的普通 `sync → doctor → build` 入口重建，再进行新的 Play 验收；本机通过不替代该流程。本次改动仅在工作区，**尚未 commit 或 push**。
