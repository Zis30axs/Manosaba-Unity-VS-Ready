# 选项分类点击音修复

2026-10-13：`OptionsUi.ActivateUi` 在分类选中时调用 `WitchTrialsInputManager.PlayResponseSfx(ResponseKind.Submit)`；输入管理器正确转发，但 `AudioManagerExtended.PlayResponseSfx` 仍为空方法，导致反馈音被丢弃。音效文件、配置映射及 SFX 混音器均可用。

## 可追踪输入与行为

- 正式修复：`UnityProject/Assets/Scripts/Game/GigaCreation.NaninovelExtender.Audio/GigaCreation.NaninovelExtender.Audio/AudioManagerExtended.cs`，保留已有 `.meta`。
- 回归入口：`UnityProject/Assets/Editor/EnhancementSettingsProbe.cs` 的 `RunOptionsAudio`，以及已有 `.meta`。该探针复用同仓库 `FireworksEpilogueProbe` 的内存存档隔离；交付时需保留原设置功能修复的配套文件。
- 本文：`docs/options-response-audio.md`。

原生证据为 RVA `0xCF4980`：配置字典 `TryGetValue` 成功后调用 `AudioManager.PlaySfx`（虚表槽 81），参数为音量 1、淡入 0、不循环、无分组、默认取消令牌，然后 `Forget`；缺少映射时记录警告。本次恢复这一行为，不增加重叠音效规则。警告改用等义英文说明；原文为“指定された ResponseKind に対応する効果音が設定されていません：{0}”。

原版选项订阅回调 RVA `0x357280` 在选中分支传入枚举值 1（Submit）。因此分类仍使用原有确认音，不把它改成枚举 Tab 的音效。配置分别映射 Cancel、Submit、Move、Tab、Alert，修复对共用这条调用路径的其他界面也生效。播放器仍遵循原有音效音量和混音器设置。

本次不需要新增音频、资源覆盖配方或修改生成的 prefab。已有副本中即使 `res` 未变化，Unity 编译修复后的 C# 即可生效；旧 Player 需要重新构建。

## 运行验证

通过真实 Boot/EntrySequence 进入游戏，使用生产界面的打开／关闭入口、实际分类 Toggle 的指针射线点击以及 UI Submit 事件。构建资产的加载提供者必须是 `BundledAssetProvider`。每次测试前停止所有待测反馈音，检查正确 AudioClip、播放采样位置推进、非零输出 RMS 和 SFX 混音器路由。

- 修复前：`tools/unity-rebuild/out/unity/enhancement-settings/20261013-010250-569/report.json`，Unity 退出 1，探针 FAIL。直接播放确认音成功；六次分类切换、重开后提交、五种输入反馈全部无声。
- 修复后：`tools/unity-rebuild/out/unity/enhancement-settings/20261013-010631-612/report.json`，Unity 退出 0，探针 PASS。以上 13 项均观察到正确音效、采样推进和非零 RMS；分类切换各触发一次选中事件。
- 音频测试使用内存存档处理器，退出后恢复场景及构建读取模式。配置／存档字节保护证据位于 `tools/unity-rebuild/out/options-ui-audio-20261013/`。
- 全库 doctor 仍为 FAIL：23,372 条 `CONVERSION_CHANGED`、489 条 `PENDING_IMPORT`，与前一任务基线相同，新增问题为 0。本次不扩展处理这些历史资源状态，不能表述为全库 doctor 通过。

正常 `content.py build`（预同步选定 `Act02_Chapter05_Adv09.asset.json`，全部配置的 Addressables 打包）退出 0；同步为 156 项 unchanged、0 failed。冻结报告为 `tools/unity-rebuild/out/options-ui-audio-20261013/addressables-build.json`，构建状态 OK，338 个 bundle、3,740,245,647 bytes。该命令未加 `--player`。

新构建后的最终回归报告为 `tools/unity-rebuild/out/unity/enhancement-settings/20261013-011617-247/report.json`：Unity 退出 0，13 项均 PASS、失败列表为空，场景和构建读取模式恢复成功，内存存档前后相同。最终磁盘保护核对见 `tools/unity-rebuild/out/options-ui-audio-20261013/final-audit.json`；正式输入清单及 SHA256 见同目录 `delivery-inputs.json`。这些日志／报告保留为本机证据，不作为需要提交的资源源文件。

## 协作者执行

已导入同版本资源的副本按 `DEVELOPMENT.md` 执行 `tools/content/sync.ps1`、`doctor.ps1`、`build.ps1`，再启动新的 Play 会话。全局检查失败时先评估问题，不能将既有失败默认为本机同一基线。需要独立程序时执行 `build.ps1 --player` 并使用新产物。

在 Editor 关闭时，用本机匹配版本的 Unity 执行（路径由协作者提供）：

```powershell
& $env:MANOSABA_UNITY_EDITOR -batchmode -projectPath "$PWD/UnityProject" `
  -executeMethod Manosaba.EditorTools.EnhancementSettingsProbe.RunOptionsAudio `
  -logFile "$PWD/options-audio.log"
```

不要加 `-nographics` 或 `-quit`；探针自行进入 Play、完成验证并按结果退出。报告输出在 `tools/unity-rebuild/out/unity/enhancement-settings/<时间>/report.json`。

验证范围为本机 Unity 实际运行与 AudioSource 输出，不代表物理扬声器、硬件手柄、独立 Player 或全新 clone 冷导入已验收。探针预加载测试音频，不声称覆盖冷加载。构建／Editor 退出阶段仍有既有原生断言／Persistent allocation 警告，本次没有将整份日志标为无警告。尚未 commit 或 push；GitHub 未收到本次修复。
