# 烟花后日谈

本增量把用户提供的中文后日谈接在原最终结尾之后，并为已通关玩家增加标题重看入口。原 ED、典狱长收尾及通关进度更新都保留；只将 `Act02_Chapter05_Adv09` 的最后一次返回标题改为设置分支 `RouteFireworksEpilogue`。开启时进入 `System/System_FireworksEpilogue`，关闭时仍执行原有返回标题。原始 `res` 剧情 JSON 和烟花 CG 不修改。

设置的图形页新增“论破立绘样式：原版／修正”和“烟花后日谈：关／开”，默认分别为原版、开启。`EnhancementSettings` 将两个偏好保存在现有 `SettingsStateMap`，不会写入主线进度或复用艾玛三张立绘轮换的 `CutInPortraitSetting.UseAll`。旧设置没有新记录时使用默认值；普通回标题、读档和开始游戏不会重置偏好。图形设置的重置按钮会恢复这两个默认值。

关闭后日谈同时隐藏标题重看入口；重新开启后，通关进度与资源检查通过即可恢复入口。设置只影响下次进入，正在阅读的后日谈继续到结束或由玩家正常退出。禁用分支完整保留原末条命令的 `FadeDuration=1.8`、`MinDelayDuration=0.9` 和 `Wait=true`，不使用 `ReturnToTitle` 类自身较短的默认值。完成后的标题仍按原进度显示烟花 CG。

## 输入与演出

- `content-overrides/fireworks-epilogue/dialogue.zh-Hans.json`：42 页中文原文，独立的对白 ID、已确定说话人及演出节点。群像对白保持场外呈现，不根据字色猜测身份。
- 同目录 `source-poster.png`：用户提供的完整原文与制作署名，保留 Bilibili@简易长矛汉化组及所有参与者的原始署名图。完整图随资源包分发；最后一页显示原图底部署名条，结束时一同淡出。署名直接取原图像素，不猜测或重新拼写参与者名字，也不增加正文页。
- 同目录 `empty-rooftop.png`：使用内置 image_gen 编辑原烟花 CG 得到的无人天台底图；完整提示词、输入和生成方式见 `image-generation.json`。只用于本段开场，不作为画廊条目。
- 完整画面继续使用本机已准备的 Switch 标题资源 `Stills/3_1`。无人图源 PNG 保持 1774×887；只对其 Unity Texture2D 导入启用 ToNearest/max2048，实际资源归一为与完整 CG 相同的 2048×1024，并在构建时严格读回尺寸。这样渐变前后使用相同画布，避免转换结束时缩放跳动。原图、辅助图源文件和已有 GUID 均保留。

第 4 页开始两秒渐显，同时淡入音乐；第 9 页进入回忆段；第 18 页进入十三人团聚段。第 42 页读完才结束。烟花光照使用后日谈专用 stationary 参数，不执行标题的 1.05→1 缩放入场，原有标题行为保留。新增中文本地化文本和默认回退文档；其他语言环境回退显示同一份中文原文，不冒充未经确认的译文。

音乐使用既有 `Bgm_40_1_Intro` 和 `Bgm_40_1_Loop`。开场静音，显现时两秒淡入至 0.65，回忆时一秒降至 0.40，团聚时两秒回到 0.65，结束三秒淡出。数值是每曲音量，继续乘以玩家 BGM/master 设置；不改设置，不在段落间重播前奏。未新增配音或未经确认的烟花音效。

## 同步与构建

此增量复用正常 import/sync/build；所需 Switch 标题源需先按 `switch-title-port.md` 准备。增量输入已安装但依赖或图像缺失时明确失败，不产生可进入却没有内容的按钮。

```powershell
python tools/content/content.py sync --file "Assets/#WitchTrials/Scenarios/Act02_Chapter05/Act02_Chapter05_Adv09.asset.json"
python tools/content/content.py doctor
python tools/content/content.py build --file "Assets/#WitchTrials/Scenarios/Act02_Chapter05/Act02_Chapter05_Adv09.asset.json"
```

`--file` 只限制普通同步范围，构建仍包含全部已配置 Addressables。没有原资源修改时仍检查本增量，稳定输入会命中缓存；冲突、编译错误、缺失地址或无效剧情会传递为失败。新脚本、辅助图和修改后的结尾由可追踪配方生成，保留已有资产 GUID。不得只分发本机 GameContent 或旧 bundle。

可追踪实现包括 `tools/content/fireworks_epilogue.py`、对应专项测试、`FireworksEpilogueContentBuilder.cs`、`FireworksEpilogueProbe.cs`，以及 `WitchTrials.Views` 中的后日谈命令、会话和署名视图（新增 Unity 脚本均带 `.meta`）。现有标题、保存入口、画廊和同步／构建入口包含必要接入改动；生成资源依照配方在本地重建。

本轮正式交付清单：

- `content-overrides/fireworks-epilogue/` 的对白 JSON、两张 PNG 和 `image-generation.json`，以及本文档。
- `tools/content/fireworks_epilogue.py`、`tests/test_fireworks_epilogue.py` 与 `content.py` 中的入口接入。
- Editor 的 `FireworksEpilogueContentBuilder.cs`、`FireworksEpilogueProbe.cs` 及其 `.meta`；`ContentSyncBridge.cs`、`AddressablesBuild.cs` 中的构建接入。
- `WitchTrials.Views` 的 `BeginFireworksEpilogue`、`FinishFireworksEpilogue`、`FireworksEpilogueSession`、`SetFireworksEpilogueMood`、`ShowFireworksEpilogueCredits`、`FireworksEpilogueCreditsOverlay` 共六份源码及 `.meta`。
- `TitleUi`、`AutoSave`、`QuickSave`、`ReturnToTitle`、`ControlPanel`、`GalleryUi`、`ModifyBackgroundExtended`、包内 `CGGalleryPanel` 的必要接入。
- 开关接入包含 `WitchTrials.Models/EnhancementSettings.cs`、`WitchTrials.Views/RouteFireworksEpilogue.cs` 及其 `.meta`，以及 `OptionsUi.cs`、`TitleUi.cs`。配方仅更换结尾命令类型，保留原参数、命令身份、行位置及其余本地编辑；原末条或通关清理锚点发生冲突时停止。
- 原有移植中的 `TitleScreenEffect.cs` 和标题 `Additive/Fog/Noise` shader 的本轮修改，以及原有 `.meta` 和 `TitleNoise.cginc` 依赖。

本机此前的 SwitchTitle/Story 移植源码仍有未跟踪文件；分享修复时须先包含相应移植代码、配方和 Unity 源资产依赖，不能只复制本轮新文件。生成的 Addressables 分组和 schema、TypeCache、GameContent、`res/.manosaba`、Library、bundle、内容状态、日志及存档不属于源码交付清单。当前只完成本地实现，没有提交或推送到 GitHub。

## 保存与退出

标题按钮只在后日谈开关启用、`g_gameProgress >= 12` 且新剧情可解析时出现，旧通关存档可直接重看。后日谈期间禁止自动存档、快速存档和菜单保存；保留阅读、自动播放、快进、历史和读档能力。退出、读档或引擎销毁会取消当前过渡并清理本段音乐和光效。延迟自动存档通过会话版本阻止跨场景落盘。后日谈本身不更新主线进度。

原始完整 CG 继续使用既有解锁逻辑；无人辅助图不会解锁、进入画廊或出现在调试“全部解锁”列表。

## 验证记录

下列记录对应增加设置开关之前的后日谈版本，不代表本次开关和分支已完成新运行验收。本次开关须重建资源并在隔离存档的实际 Play 会话验证设置保存、关闭后返回标题及入口隐藏、重新开启和进行中不打断。

2026-10-12 的最终构建和新 Play 会话均通过。使用 Unity 6000.0.48f1、Windows/D3D11 和实际新打包的资源；后日谈配方签名为 `47989bf9a8e29b1567f14a57becf7144f74c4a1531cbf6f85afda8b66178e28d`。构建生成 337 个 bundle；探针逐项确认使用 `BundledAssetProvider`，没有用 AssetDatabase 冒充打包验收。

实际验收结果：

- 正常阅读完整到达 42 页，顺序与文本 ID 完全一致；自动播放实际推进，快进重看也到达全部 42 页。附件文字逐句人工比对，保留用户确认的“我的目标是当个侦探”。末页署名从原图直接显示。
- 空景和原 CG 均读回 2048×1024。渐变前、中、后的构图稳定；13 个光效渲染器一起渐显。PostLateUpdate 采样及完成后连续两帧的最大画布误差小于 `.002`，没有此前的结束缩放跳动。最终显示原始 CG。
- BGM 40 前奏自然进入循环，沿用同一个音轨，段落间重播次数为 0。实际每曲音量依次为 `.65 → .40 → .65`；完成后本段音乐与光效清理，标题播放原有曲目。
- 未通关进度 0、11 隐藏入口；既有通关进度 12 显示且可点击。实际结尾末四条命令保持 `11 → 12` 的原通关语义并进入后日谈；返回时验证 `Stills/3_1` 原纹理、正常标题光效及标题状态，保留烟花标题。
- 早退、重复快进、普通读档、快速读档、退出后延迟自动保存均通过。自动保存、快速保存和菜单保存不能写入后日谈；测试已有内存存档的哈希与写入次数不变。
- 真实玩家存档、全局存档和设置的 SHA-256 均与本轮开始前相同。无人辅助图没有解锁或进入画廊。测试场景、构建器及临时元数据恢复；TypeCache 是启动时自动生成的缓存，未作为源码交付。

证据位于本机被忽略的 `tools/unity-rebuild/out/fireworks-epilogue-20261012/`：`addressables-build-normalized.json`、`unity-content-readback-final.json`、`runtime-pass-final.json`、`protected-files-final.json`、`text-review.json`。最终逐页记录及截图在 `tools/unity-rebuild/out/unity/fireworks-epilogue/20261012-053721-057/`；以该轮 `PASS`、退出码 0 为准，前几轮失败报告保留用于追溯修复。配方最终专项 18 项测试通过；本轮相关 Python 测试集合此前 96 项、内容入口测试 19 项通过。重复 prepare 字节与时间戳相同；Unity 第二次执行命中缓存且不重写资产。

原烟花 CG 的所有 RGBA 像素保持完全相同。标题配方因源码变化重新导出其派生 PNG，无损压缩编码变化使文件增加 5 字节；旧副本与新源图、GameContent 图的像素及解压后的 PNG 扫描行均完全相同，不能把它表述为文件哈希未变。完整证据见同目录 `audit/original-cg-pixel-verification.json`。

验证边界：实际运行的是新 bundle 的 Editor Play 会话，启动时跳过 Steam/splash 以注入内存存档；未导出或验收独立 Player，未在独立副本或冷导入环境重跑。原 ED 和典狱长段落通过结构检查确认保留，只有最后返回命令改为后日谈入口；本轮实际结尾路线测试从末四条命令开始，没有重新播放整段原 ED。正常读档兼容通过内存构造的旧通关状态验证，未打开真实用户存档。仓库全局 doctor 仍有 23,965 条既有问题，本次最终比对无新增，不能称全项目 doctor 通过。当前未 commit/push。

在完成上面的正常构建后，可由干净空场景运行 Editor 菜单 `Manosaba/Verify Fireworks Epilogue (Isolated Saves, Built Content)`，或通过 Unity 的 `-executeMethod Manosaba.EditorTools.FireworksEpilogueProbe.Run` 批处理入口执行。图形验收不可加 `-nographics`，异步入口不可加 `-quit`。验收只使用临时内存存档，不打开玩家存档；报告和截图写入 `tools/unity-rebuild/out/unity/fireworks-epilogue/<本轮时间>/`。
