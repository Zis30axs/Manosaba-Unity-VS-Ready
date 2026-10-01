BMPT-72 本地测试序言

当前已启用。只在 Unity 编辑器的 Play 模式生效。

试玩
1. Unity 等待脚本编译、素材导入完成。
2. 退出已有 Play，然后在 Assets/Scenes/Boot.unity 进入新 Play。
3. 选择 New Game，或调试主界面 → 第1章 第1幕 → 剧情部分 场景1。
4. 黑幕内切换到 AI 生成的黄昏校园庭院，再揭开黑幕。黑色 BMPT-72 剪影登场，播放低速倒车音和 2A42 机炮。
5. 艾玛说“……谁！”后点击继续；立绘恢复白蓝红，名称中的大写 B 呈白/蓝/红三条水平色带。
6. BMPT-72 说“我理解了你的请求，消灭了霸凌你的人。”后继续。
7. 黑场切换到原 Adv02，艾玛说“——不对！”，接回牢房醒来。

一键注释/恢复
IDEA 运行配置：
- 一键注释 BMPT-72 测试序言
- 启用 BMPT-72 本地测试序言
运行后重新进入 Play。无需重建 Addressables。
关闭配置会在 BEGIN_LOCAL_BMPT_TEST / END_LOCAL_BMPT_TEST 之间的活动行前加
“; LOCAL_BMPT_DISABLED ”；恢复只移除这个前缀，手工注释保持原样。
已验证关闭/恢复后剧本文件逐字节一致。
多色姓名接口 MulticolorAuthorNames 独立于这个开关；关闭序言仍保留接口。

手工修改
funny/opening-bmpt72-test.nani 是可编辑测试剧本。
只需保存，再进入新 Play；它独立于先前 zh-Hans 中文文本同步配置。
可以把任意命令行用 ; 注释。若整段命令都已注释，恢复原 Adv01 序言。
从中途存档继续会绕过开头；请从新的第一幕场景1开始。

素材
仓库素材位于 UnityProject/Assets/Editor/LocalOpeningTest/Media/：
- BMPT72.png：AI 编辑的透明立绘。
- SchoolCourtyard.png：AI 生成的黄昏校园庭院背景。
- Reverse.wav：本地合成的低速引擎/倒车提示音（不是实车录音）。
- Autocannon.ogg：用户提供的 2A42_loop.wav，实际编码是 Ogg Vorbis，改用正确扩展名供 Unity 导入。
funny/ 中的原始素材、重复图片和未用音频仅留本地；试玩直接使用上述仓库素材。
艾玛的“……谁！”为无配音；牢房“不对！”沿用原剧情文本和配音。

本地生效方式
LocalOpeningTest.cs 在引擎预初始化阶段把独立解析的 Adv01 副本交给临时资源提供者；不写原 .asset、不写 localization 文件。
用原始脚本路径开始，因此 New Game 和调试跳转都能播放测试段。
停止 Play 或程序集重载后释放测试副本。原剧本始终保持原样。
立绘、背景和音效仅暂时注册到 Naninovel EditorResourceProvider；不修改 EditorResources.asset 或 Addressables 分组。
角色和背景配置是引擎的运行时副本；三色 B 由独立的 MulticolorAuthorNames 接口生成临时 TMP sprite，不保存字体资产。名称使用一个完整的私用字形，交给游戏原有的首字放大模板处理，避免拆坏 rich-text 标签。
接口说明：tools/unity-rebuild/multicolor-author-names.txt。
全部测试代码和素材在 Editor 目录，剧本在 UnityProject 外，不参与玩家构建。
既有 Addressables bundle 保持正式剧情。此次不生成或发布任何 Player。

立绘制作记录
使用内置 image_gen 编辑功能。提示：保留 BMPT-72 的轮廓、比例、姿势、透视、炮塔/机炮/履带和设备；
仅把橄榄绿装甲改为上部白、中部蓝、下部红，机械部件保持暗灰；移除背景，输出透明PNG，无文字或标志。
用户随后澄清白蓝红用于大字名称中的 B，并允许保留该配色立绘。
背景制作记录
使用内置 image_gen（默认工具模式），创建 illustration-story 游戏背景。
最终仓库图片：UnityProject/Assets/Editor/LocalOpeningTest/Media/SchoolCourtyard.png
最终提示词：
Use case: illustration-story
Asset type: 16:9 background illustration for a local visual novel test scene.
Primary request: An empty Japanese school courtyard at blue hour, viewed at human eye level from a broad paved foreground. A modest school building and a covered walkway stand in the distance, with a chain-link fence and a few trees. The setting should feel like a quiet, unsettling dream and support a military vehicle character sprite in front.
Style/medium: Polished hand-painted anime visual novel background, clean perspective, detailed environment, restrained cinematic lighting.
Composition/framing: Wide 16:9 composition, full-bleed opaque background. Keep the central lower-middle foreground open and uncluttered for a large character sprite; keep the bottom quarter darker and visually calm so game dialogue can be read over it.
Lighting/mood: Cool blue dusk, muted violet shadows, a few warm window lights, faint ground haze. Eerie but non-graphic.
Constraints: Environment only. No people, no characters, no vehicles, no weapons, no text, no signs with lettering, no logos, no UI, no borders, no watermark. Fill the entire image; do not include transparent areas.
验证记录（2026-10-01）
- Unity 编辑器实际 Play：独立的黄昏校园背景显示正常，BMPT剪影/彩色切换正常，立绘不遮挡对话。
- 名称只显示三色B与MPT-72；B按原有首字模板放大，未露出<size>或<sprite>标签。
- 黑幕切场后接回Adv02原有牢房醒来段落；艾玛姓名排版正常。
- 使用Unity自带Roslyn及工程实际引用联合编译测试、通用多色姓名接口和同步桥接文件，编译通过，无警告或错误。
- 接口独立后再次校验一键注释/恢复：剧情逐字节恢复，多色接口文件逐字节不变。该次接口整理只做编译和开关校验，未重复完整 Play 测试。
- 原Act01_Chapter01_Adv01.asset的SHA-256在测试前后相同。
- Console的native pred(*previous,*i)断言在本功能添加前已出现；本次暂无C#异常或缺失资源错误。
- 一键注释后，新 Play / New Game 实际播放原学校序言；重新启用后剧本文件的 SHA-256 逐字节一致。最终状态为已启用，Unity 已停止 Play。
