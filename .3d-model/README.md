# 牢房开场 3D 测试

这是樱羽艾玛从牢房醒来到出门前的独立 Unity 3D 测试场景。使用已有艾玛模型，其他角色暂用缩放后的 BMPT-72 占位；典狱长广播也用显示器附近的 BMPT 桌面模型暂代。

**启动：**双击本目录的 [Start-3D.cmd](Start-3D.cmd)。它会启动 `dist/3d-prototype/Manosaba-Cell3D.exe`；程序不存在时先转换模型并构建。使用本原型不需要安装 Blender 或 MMD。当前本机已经安装所需 Unity，首次构建或重新构建还需要 Python 3.10 或更新版本；启动脚本会优先使用项目的 `tools/venv`。

**新克隆的准备条件：**仓库共享源码、着色器和构建脚本。模型 ZIP/GLB、`res/` 游戏资源、生成的 Unity 模型与场景、独立程序和 `preview/` 检查记录均为本地输入或输出，不随 Git 克隆提供。

1. 在本目录准备 `SakurabaEma_ByPOWER_v1_0.zip` 和 `bmpt-72_war_thunder_line_of_contact.glb`。艾玛 ZIP 内需要保留 `SakurabaEma_ByPOWER_v1_0/SakurabaEma_ByPOWER.pmx` 及其相对纹理路径。
2. 准备仓库根目录的 `res/` 提取资源；原型需要 Adv02 Scenario JSON、简体中文本地化文本、33 条原配音、`SourceHanSerifSC.otf` 字体和下文的牢房参考图。提取与 Unity 工程准备流程参见仓库现有重建文档。仅克隆源码不能恢复这些媒体。
3. 安装 `UnityProject/ProjectSettings/ProjectVersion.txt` 指定的 Unity `6000.0.48f1`，包括 Windows Player 构建支持，并安装 Python 3.10+。模型转换与剧情提取只使用 Python 标准库。Unity 不在脚本常见安装位置时，设置环境变量 `MANOSABA_UNITY_EDITOR` 为 Editor 的可执行文件路径；可用 `MANOSABA_PYTHON` 指定 Python。
4. 关闭正在使用此工程的 Unity 编辑器，再运行下述重建命令。生成完场景后，才能在编辑器中使用“Open prison scene”菜单。

下文带日期的验收信息说明当前本机已经完成的检查；链接到 `preview/`、`inspection/` 和 `generated/` 的文件需在本地生成或另行保留。

**操作：**

| 按键或按钮 | 功能 |
| --- | --- |
| `Space` / `Enter` | 继续下一句 |
| `←` | 返回上一句 |
| `R` | 从头重播 |
| `V` / 全景按钮 | 切换全景与剧情镜头 |
| `F` / 自由视角按钮 | 切换自由视角与固定镜头 |
| 自由视角下按住右键拖动 | 环绕查看场景 |
| 自由视角下滚轮 | 调整查看距离 |
| 原图对照按钮 | 显示或收起游戏背景参考图 |
| 静音 / 开启配音按钮 | 切换原配音 |

片段结束后可以重播，也可以继续检查场景。

**在 Unity 编辑器中打开：**打开仓库的 `UnityProject`，选择菜单 `Manosaba/3D Prototype/Open prison scene`，然后进入 Play。场景路径为 `Assets/ThreeDPrototype/Scenes/CellWakeUp.unity`。

**重新构建与运行检查：**先关闭正在打开此工程的 Unity 编辑器，在仓库根目录运行：

```powershell
.\.3d-model\Build-3D.ps1 -Rebuild -NoLaunch -Verify
```

这条命令重新转换模型、提取原剧情、生成场景、构建独立程序，再运行检查；`-NoLaunch` 表示检查结束后不再打开交互窗口。构建日志写入本目录 `build.log`，运行日志写入 `runtime.log`，截图与检查记录位于 `preview/`。具体结果以 `preview/runtime-verification.json` 和日志为准。

运行检查的 PNG 是独立 Player 的离屏场景渲染，不包含交互窗口上的对白和操作面板。可在正常启动的窗口中查看中文对白与推进操作。

2026-10-03 已完成独立程序构建和运行检查，结果为 `PASS`：遍历 79 句剧情、10 个场景阶段，载入 33 条配音和 82 个 BlendShape；艾玛进入镜头，场景截图非空，使用的材质受到当前显卡支持。检查记录见 [preview/runtime-verification.json](preview/runtime-verification.json)，场景预览见 [preview/overview.png](preview/overview.png)。这项自动检查覆盖剧情调度和实际场景渲染，未模拟鼠标或键盘操作。

**剧情来源与范围：**中文正文按真实 Naninovel Scenario 的 `playlist` 命令顺序关联本地化键，保存在 [generated/story.json](generated/story.json)。包含 `Act01_Chapter01/Act01_Chapter01_Adv02` 的 79 句原文和 33 条原配音，止于 Adv02 末尾。Adv03 才开始推门、进入过道，本测试停在此前。调度顺序为惊醒、观察牢房、呼救、希罗起身与下床、屏幕广播、看守接近、抓住希罗手臂、被甩开与拒绝。

目前独立场景拥有自己的镜头、调度和操作界面，尚未自动替换主游戏 `New Game`、Naninovel 剧情播放或存档流程。启动这个测试不会把主游戏开场变成 3D。

**切回原游戏：**停止 Unity 的 Play，在 Project 面板双击 `Assets/Scenes/Boot.unity`，再进入 Play。独立程序与编辑器的场景选择是两回事；双击 `Start-3D.cmd` 只启动独立测试。

最初的测试构建曾触发全游戏 Addressables 重建并清空旧缓存；2026-10-03 已从已有自建主游戏 Player 恢复完整的 333 个资源包及 catalog/settings。当前 3D 构建流程会暂时关闭自动 Addressables 重建并在结束后恢复原设置。日后若编辑了主游戏资源，应使用 `Manosaba/Build Addressables Content` 更新打包内容，再进入原游戏的 Play。

恢复后的 337 个文件逐个通过 SHA-256 一致性检查；已在当前 Unity 编辑器保留 `Use Existing Build` 模式，实际进入 Boot 的 Play，确认引擎初始化、标题剧情执行及 `BundledAssetProvider` 资源定位，运行未出现 Error/Exception。验证为 `PASS`，随后停止测试会话并保留 Boot 场景；临时检查脚本已删除。记录见 [preview/main-game-cache-verification.json](preview/main-game-cache-verification.json) 和 [preview/main-game-play-verification.json](preview/main-game-play-verification.json)。

随后发现旧 Player 缓存的左侧 Debug 按钮仍为日文。2026-10-03 已按原汉化补丁恢复 16 处界面文本，并写入对应 `res` 组件 JSON，避免正常资源同步再次覆盖译文；只修改 `m_text`，保留字体、GUID 与其余字段。当前 Addressables 已重新构建为 333 个包，逐个界面对象核对源 JSON、prefab 和实际 bundle 的 16 处译文全部通过；开场及 `Debug_Home` 中文 TextAsset 也通过一致性检查。已另完成 Boot 的打包内容 Play 检查，4 个资源和 3 个预制体加载通过，使用 `BundledAssetProvider`。记录见 [preview/debug-translation-restoration.json](preview/debug-translation-restoration.json)、[preview/debug-translation-verification.json](preview/debug-translation-verification.json) 和 [preview/debug-translation-play-verification.json](preview/debug-translation-play-verification.json)。前述缓存复制检查是恢复旧包时的历史记录，当前资源以本次重建结果为准。

**场景依据：**原图可通过运行中的“原图对照”按钮查看；源文件保留在 `res/Assets/#WitchTrials/Textures/Naninovel/Backgrounds/`：

- `MainBackground/RGB/Background_001_001.png`：空牢房，铁门、双层床与显示器布局的主要依据。
- `MainBackground/RGB/Background_001_002.png`：同机位的门外看守。
- `Stills/RGB/Still_490_001.png`：艾玛在下铺醒来、坐起的 CG。
- `MainBackground/RGB/Background_025_001.png`：原典狱长广播的控制室背景。

场景依据原图保留左侧拱形铁门、右侧双层床、门右上显示器、床上方吊灯，以及石墙、地砖、链条和门外冷紫光的相对位置。米制尺寸、原图未显示的墙面和过道布局是为原型补出的推定，单张背景图无法确定它们的真实尺寸。

**艾玛的表情与当前限制：**模型确实有表情。原 PMX 含 85 个形变，本次保留 82 个顶点或组合 BlendShape，支持眨眼、口型以及惊讶、困惑、悲伤、微笑等顶点表情。当前姿态由程序驱动 FK 骨骼设置，用于演示剧情中的站位与姿势；尚无正式动作动画、MMD 头发或裙摆物理、IK 和完整 MMD 材质效果。脸红、青脸等 3 个材质形变尚未实现，组合表情中的对应材质部分也不能完整复现。BMPT-72 原模型没有角色骨骼、表情或动作，仅作位置、旋转和缩放占位。

模型规格、转换细节与表情名称见 [inspection/model-report.md](inspection/model-report.md)。原始 `SakurabaEma_ByPOWER_v1_0.zip` 和 `bmpt-72_war_thunder_line_of_contact.glb` 保留在本目录；生成数据在 `generated/`。

当前美术质量属于场景几何和站位验证阶段。石墙、床铺和灯具使用基础材质；坐姿裙摆采用程序展开，会呈现较硬的扇面，肩袖也可能在大角度姿态下出现不自然的形变。后续需要补正式动作、衣料约束和材质细节，才能接近原图的表现。
