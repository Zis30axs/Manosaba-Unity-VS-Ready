# 横条论破语音接入

本修复补齐艾玛、希罗及 CreatureHiro 形态的横条论破喊话，并让最终双人横条的两句既有喊话同时播放。复用现有配音文件，不合成新语音，不改全局 `VoiceOverlapPolicy=PreventOverlap`。

## 输入、范围与行为

输入是本机已缓存、经哈希验证的 NS 剧情、原 PC 剧情基线及当前可编辑 PC 剧情。189 次单人调用分布在 140 个脚本中，使用 15 个既有语音路径：Ema 90 次、Hiro 70 次、CreatureHiro 29 次。与此前 50 个视觉移植脚本合并后，正常 SwitchStory 配方共管理 154 个脚本；此前的 CG 和插入动画范围不变。

合并器通过两侧唯一对白 ID 找到同一个横条命令，再核对角色。仅移植 NS 明确赋值的 `VoicePath`、`Volume`、`GroupPath`、`AuthorId`，保留 PC 的立绘索引、流程、文本、条件和无关本地字段；本地语音参数与基线及目标冲突时明确失败。不能直接复制 NS 的 Index，因为部分 NS Index 0 对应 PC Index 3。

`GosubToObjectionCutIn` 现在预载并播放可选语音，然后进入原有 `EndDebate-CutIn` 子流程；等待的是资源加载与起播，不等待整句播放结束。未指定语音的旧命令仍使用原来的即时导航路径。语音起播时点根据 NS 命令边界与现有 Naninovel 接口兼容重建，没有声称恢复了新版 native 方法的逐帧时序。

`Act02_Chapter06/Act02_Chapter06_Trial09` 中，紧邻双人 `w` 横条之前的 `CommonTrial_Ema06197` 和 `CommonTrial_Hiro06086` 原本是两个连续的 `PlayVoice`；在本工程禁止重叠的规则下，后一条会停止前一条。配方只在这个经过核对的位置把两条命令合并为 `PlayObjectionVoicePair`，保留两路各自的音量、作者和混音通道，其他 `PlayVoice` 不改。

双人命令通过通用语音组接口先准备两份资源和声源，再在同一帧开始播放。两路仍走 Voice 混音组与角色音量，`GetPlayedVoice` 会报告尚未结束的一路。普通下一句语音、`StopVoice`、重置、读档及退出引擎会清理整组；独立资源持有者和请求版本防止较早的异步加载复活、误停重放或释放新播放所需的同路径资源。

## 正式代码与复现

- `tools/content/switch_story_scenario.py`：单人参数三方合并及唯一双人命令合并。
- `tools/content/switch_story.py`：正常流程的输入签名、场景范围与缓存失效。
- `tools/content/tests/test_switch_story_scenario.py`、`test_switch_story_recipe.py`：冲突、保留字段、重复运行与无资源变化时的更新检查。
- `UnityProject/Assets/Editor/SwitchStoryContentBuilder.cs`：实际反射读回语音参数，验证 17 个原有音频地址唯一、实际 AudioClip 有效且所属分组参加构建。
- `GosubToObjectionCutIn.cs`、新增 `PlayObjectionVoicePair.cs` 及其 `.meta`：运行时单人和双人命令。
- Naninovel 包的 `AudioManager.cs`、`IAudioManager.cs`、新增 `VoicePlayback.cs` 及其 `.meta`：可停止、可释放的并发语音组。
- 新增 `UnityProject/Assets/Editor/ObjectionVoiceProbe.cs` 及其 `.meta`：隔离存档的实际打包资源验收。

以上需与已有 SwitchStory/Title 移植依赖一起交付。不要只提交本机生成的 GameContent、Addressables 组、bundle、TypeCache 或 `res/.manosaba`。已有副本在获取源码后运行正常流程；即使 `res` 没有编辑，源码签名变化也会重新生成相应命令。

```powershell
python tools/content/content.py sync --file "Assets/#WitchTrials/Scenarios/Act02_Chapter06/Act02_Chapter06_Trial09.asset.json"
python tools/content/content.py doctor
python tools/content/content.py build --file "Assets/#WitchTrials/Scenarios/Act02_Chapter06/Act02_Chapter06_Trial09.asset.json"
```

`--file` 限制普通同步的选择；已启用的 SwitchStory 增量仍完整验证并应用。新副本需先按开发指南从自己可读取的同版本资源完成导入。此修复没有新增配音叶子，17 个语音均继续由原有音频分组提供。

## 验收

本轮证据目录为 `tools/unity-rebuild/out/objection-voice-20261012/`。`source-voice-census.json`、`existing-voice-resources.json`、`scenario-merge-dry-validation.json`、`recipe-prepare.json` 记录全部调用、已有资源和源文件保护；25 项相关 Python 测试已通过，重复 prepare 为 `UNCHANGED`。静态检查不代替实际起播和声源清理验证。

在完成正常构建后，用 Unity 入口 `Manosaba.EditorTools.ObjectionVoiceProbe.Run` 验收（需要图形，不传 `-nographics` 或 `-quit`）。探针使用真实 Boot、Game 和新 bundle，并注入内存存档；不打开真实玩家存档。实际结果与限制以新鲜报告为准。

本轮正常同步完成 7 批、155 个剧情源记录（含后日谈依赖），失败为 0。Story 首次重建后，后续检查 `cache.hit=true`、`rebuilt=false`，154 份合并脚本和 17 个既有语音依赖继续读回通过。配方签名为 `1c153554646a4dec5d4340971131341cc4fc547d3e46bf40b2c8d757f705fd5b`。

正常 `doctor` 仍为 FAIL：23,861 条既有问题；相对同日后日谈验收的 23,965 条记录，新增 0 条，消除 104 条。本轮未扩改无关资源；详见 `doctor-comparison.json`，不能将本次范围通过称为全项目检查通过。

正常 Addressables 构建退出 0，结果 OK：337 个 bundle、3,710,624,066 字节，构建阶段 702.5 秒，签名与本轮配方一致；证据为 `addressables-build.json`、`addressables-build.log`。日志保留 Unity 原生 `pred(*previous, *i)` 断言，同一 `GatherMainAndReferencedSerializedTypes` 调用栈在 10 月 1 日、4 日的既有构建日志中已出现。本次没有缺类型或 managed-reference 序列化失败，入口正常退出；这不代替后续实际加载检查。

首轮运行 `objection-voice/20261012-072044-310/report.json` 为 FAIL，保留为诊断记录：测试每次重置状态后没有恢复真实初始化流程的 `enableDebugSkip` 布尔变量，共享子程序因此在条件判断处停止。艾玛的实际声源已起播、采样位置推进且输出 RMS 非零，但该轮未完成横条演出验收。修正限定于探针恢复正式初始化条件。

第二轮 `objection-voice/20261012-072611-999/report.json` 中，单人、双人、主动停止、返回标题、快进、同路径替换与角色音量共 11 项通过；整体仍为 FAIL。兼容性测试在 3 秒以上剧情预载之前播放了约 2 秒的哨兵语音，随后将自然结束误判为被无语音命令截断。此外，人工截图发现标题 UI 未隐藏、双人取样过早，故该轮不作为画面验收通过的证据。后续探针仅修正测试预载顺序、标题 UI 状态与取样时机，保持实际声音和演出断言。

最终运行 `tools/unity-rebuild/out/unity/objection-voice/20261012-073412-981/report.json` **PASS / exit 0，12 项全部通过**，报告同时保存为本轮证据目录的 `final-runtime-report.json`。它使用新 bundle 的正式 Trial09 命令与原共享子程序，覆盖：

- 艾玛、希罗单人喊话，以及双人喊话的自然完成；实际声源对应正确 AudioClip，采样位置推进，四段语音均检测到非零 RMS 输出。
- 双人两路在同一个采样帧首次出现，各自持续播放；短的一路结束后，较长的希罗一路仍被语音管理器正确报告。它们沿用 Voice 混音组。
- 主动 StopVoice、双人／单人返回标题、快进后的状态清理、双人同路径重播、双人切为单人同路径，以及独立的单人同路径重播检查。
- 角色与命令音量、旧命令未指定 VoicePath 时保留先前声音；临时改动的已加载命令字段已在 finally 恢复，没有保存 Script 资产。

人工查看了本次艾玛和双人横条的入场截图，标题 UI 覆盖已消失，人物与演出同时存在；截图是入场过程，双人尚未站定，不能用作最终构图、完整法庭背景或与 NS 逐像素一致的验收。记录的是实际 Unity 声源与音频输出数据，没有声称完成实体扬声器的人工试听，也没有逐一人工播放全部 189 处调用。未模拟冷加载取消与后续语音组的竞态，未跑完整庭审、独立 Player 或新 clone 冷导入。

最终 `final-verification.json` 为 `SCOPED_CHECKS_PASS`：plan / state / build / runtime 签名一致；12 项运行检查无错误，测试用内存存档前后相同，场景与构建模式已恢复。3 个真实存档／设置文件及 3 个相关配置与本轮基线哈希一致；154 份 PC 剧情源哈希和生成 GUID 保留，见 `source-guid-verification.json`。Metadata、Bridging/Server、TypeCache 已恢复为本轮开始时的精确字节，保留原有本地修改。

新增脚本及 `.meta`、配方、测试和文档均未被忽略，本轮改动范围的格式检查通过。当前仅完成 **E 盘工程源码和 Addressables 内容包**，**没有重建独立 EXE，也尚未 commit / push**。拉取源码后的协作重建方式见上文正常同步流程；这不是已在独立副本完成的验证。
