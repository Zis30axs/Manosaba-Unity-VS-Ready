# 开发指南

本指南面向修改本地游戏资源、维护 Unity 重建代码和扩展同步工具的开发者。以本仓库 `Manosaba-Unity-VS-Ready` 的实际代码为准。

日常开发的数据流是：

```text
自己可正常读取的游戏资源 → 首次 import → res/
                                            ↓ 编辑、增量 sync
                              UnityProject/Assets/GameContent/
                                            ↓ Addressables build
                                  自建 catalog 和 bundles
                                            ↓
                                   Unity Play / Windows Player
```

## Portable resource identity map

`res/` and `UnityProject/Assets/GameContent/` stay local and are regenerated from each collaborator's readable dump. The tracked `tools/content/resource-map.csv` is an identity comparison table, not a copy of the dump: it contains object type, source bundle, path ID, logical asset path, and the generated source filename. It contains no absolute paths, Unity `.meta` files, GUIDs, bundles, or generated prefabs.

Every collaborator must import the same game build and verify the table before syncing. A `~N` filename may be reassigned during extraction; the verifier follows the stable object identity and fails closed for recovered objects whose identity cannot be proven.

```powershell
# Import the collaborator's own dump, verify the table, rebuild local GameContent and Addressables.
.\tools\content\setup_from_dump.ps1 "D:\games\manosaba_game"

# Or import first, inspect the comparison result, and build later.
.\tools\content\setup_from_dump.ps1 "D:\games\manosaba_game" -SkipBuild
python tools/content/resource_map.py verify --check-files
```

The table does not carry edited bytes. Script/scenario text continues through the TEXT/SCENARIO sync path; character sprites and CGs continue through the TEXTURE/SPRITE path; `content.py build` rebinds Addressables to the current checkout's local metadata. Shared text changes use `content-overrides/`. A changed PNG or an unsupported prefab hierarchy must be added as a reviewed payload/override or implemented in the converter; a table alone cannot recreate those bytes.

`res/` 是资源编辑源；GameContent 是生成树。首次导入后，普通同步、内容构建和 Player 构建使用本地缓存。原安装目录只用于首次导入或显式研究、对照操作。

## 1. 准备环境

从仓库根目录运行本文的命令。新建开发副本时：

```powershell
git clone https://github.com/Zis30axs/Manosaba-Unity-VS-Ready.git
Set-Location Manosaba-Unity-VS-Ready
```

| 工具 | 项目要求 |
| --- | --- |
| Unity | `6000.0.48f1`，已激活许可证；构建 Windows Player 时需要对应 Windows Build Support |
| Python | 3.11+；本次验证使用 Python 3.12 |
| Python 库 | UnityPy、Pillow、soundfile |
| PowerShell | Windows PowerShell 5.1 或 PowerShell 7；完整双引擎 CLI 回归需要两者都可用 |
| IDE | Unity Editor；可搭配 Rider、IDEA 或 VS Code |

Unity 包版本以 [manifest.json](UnityProject/Packages/manifest.json) 和项目锁文件为准；当前 Addressables 为 2.4.6。打开 `UnityProject/`，让 Unity 完成包解析和资产导入。首次导入和全量内容构建涉及数 GB 数据，应预留磁盘空间与可用内存。

若没有可用 Python 环境，可以创建本地 venv；已有环境则直接设置 `MANOSABA_PYTHON`，无需重复创建：

```powershell
python -m venv tools/venv
& .\tools\venv\Scripts\python.exe -m pip install UnityPy==1.25.3 Pillow soundfile

$env:MANOSABA_PYTHON = (Resolve-Path .\tools\venv\Scripts\python.exe).Path
$env:MANOSABA_UNITY_EDITOR = 'D:\Unity\6000.0.48f1\Editor\Unity.exe'
$env:PYTHONUTF8 = '1'

& $env:MANOSABA_PYTHON -c "import UnityPy, PIL, soundfile; print('Python dependencies OK')"
```

Unity 路径是示例，请替换为实际安装路径。验证机安装的版本是 UnityPy 1.25.3、Pillow 12.3.0、soundfile 0.14.0；仓库当前没有统一 Python requirements/lock 文件，升级依赖后应重新运行相应验证。`tools/venv/` 不进入 Git。

环境变量仅在当前终端有效。机器路径也可保存在 gitignored 的 `tools/audit/audit.config.local.json`；公共 `audit.config.json` 保持机器无关。内容 CLI 的 Python 解释器优先使用 `MANOSABA_PYTHON`，其次本地 venv，再次 PATH 中的 Python。

## 2. 首次导入与保护已有修改

```powershell
.\tools\content\import.ps1 "D:\games\manosaba_game"
.\tools\content\doctor.ps1 --deep
```

导入只读取自己持有且可正常访问的资源，不要求运行原游戏或安装 BepInEx。它复用现有提取器与 Unity 转换器，保存资源缓存、映射账本、转换计划和原始 hash。已有完整 `res` 和 Unity 重建树时，导入会接纳它们并保留编辑。

| 本地目录/文件 | 用途 |
| --- | --- |
| `res/Assets/`、`res/_serialized/` | 可编辑文本、图片、JSON 等资源 |
| `res/_index.csv` | 提取对象身份和输出路径索引 |
| `res/.manosaba/base-hashes.json` | 识别原始资源与用户修改 |
| `res/.manosaba-content-state.json` | 增量同步状态、目标映射、GUID、依赖与字段可编辑性 |
| `res/.imported-source/` | 首次导入的资源和元数据缓存 |
| `res/.manosaba/reconstruction/` | 后续转换、构建所需的本地重建证据 |

保留自己的 `res` 编辑备份；这些文件被 gitignore 排除，代码提交不能代替资源备份。普通重新 import 不重新导出；确实需要重新提取时使用 `--reextract`。只有打算覆盖对应资源修改时才增加 `--overwrite-user-changes`：

```powershell
.\tools\content\import.ps1 "D:\games\manosaba_game" --reextract
```

本次已实测接纳完整资源树、全量同步和断开原目录后的构建；完全清空 `res`、GameContent、Library 后的首次导入尚未端到端验证，不能把本机验收当作该冷导入流程已完成实测。

## 3. 修改资源的日常循环

1. 在 `res/` 修改目标文本、PNG 或安全 JSON 字段。
2. 运行 dry-run 查看受影响项，再执行 sync。
3. 使用 doctor 检查结果。
4. 构建 Addressables，再进入新的 Play 会话；需要独立程序时构建 Player。

```powershell
.\tools\content\sync.ps1 --dry-run
.\tools\content\sync.ps1
.\tools\content\doctor.ps1
.\tools\content\build.ps1

# 先关闭手动打开的工程 Editor，再构建/运行 Windows Player 冒烟测试
.\tools\content\build.ps1 --player
```

`build` 会先同步选中的源资源并构建全部已配置 Addressables；`--player` 额外执行 G5，不等同于运行全部 G1–G5。Sprite、剧情 Script 和普通数据需要重建内容才能进入已有构建模式；MasterData 则保持现有 `Resources.Load("MasterData")` 入口。生成的开发 Player 位于 `dist/player/manosaba-rebuild.exe`。

### GitHub 协作与生成资源修复

统一约定：**共享修复的源码、配方和验证入口进入 Git；每位协作者在自己的资源树上生成结果。** 只在本机修改被忽略的 `res/`、`GameContent/` 或 `Library/`，不能算作已交付给协作者。资源文字改动使用下一节的 `content-overrides/`；导入元数据、骨骼和转换行为修复应进入可追踪的转换器或配方，并接入现有同步、构建流程。

已有可运行副本在拉取后，先结束当前 Play 会话，再从仓库根目录运行以下命令；任一步失败应停止并修正原因：

```powershell
git pull --ff-only
.\tools\content\sync.ps1
.\tools\content\doctor.ps1
.\tools\content\build.ps1
```

`build.ps1` 自带同步和 Addressables 本地 GUID 重绑；单独列出 sync/doctor 便于定位故障。构建成功后启动新的 Play 会话。需要独立程序时关闭占用该工程的 Editor，再运行 `.\tools\content\build.ps1 --player` 并使用新生成的 Player。全新 clone 仍需先按首次导入流程准备同版本资源；Git 拉取不会提供被忽略的 PNG、bundle 或本地缓存。

#### 幕布 SpriteSkin 的共享修复

此前幕布 PNG 虽然存在，但生成的 `.meta` 缺少骨骼、网格和权重，导致原始 Timeline 无法使幕布变形。正式输入为 [OriginalCurtainSpriteData.json](UnityProject/Assets/Editor/OriginalCurtainSpriteData.json)，恢复入口为 [RestoreOriginalCurtainSkin.cs](UnityProject/Assets/Editor/RestoreOriginalCurtainSkin.cs) 的 `Manosaba.EditorTools.RestoreOriginalCurtainSkin.EnsureApplied()`。这两个文件及各自的 Unity `.meta` 必须随修复一同提交；恢复器不从 `work/tmp` 读取输入。

| 入口 | 当前行为 |
| --- | --- |
| `tools/content/import.ps1`、普通 `sync.ps1` | 通过 `ContentSyncBridge` 在导入/转换后应用并读回幕布数据；普通 sync 即使没有源资源变化也执行此检查 |
| `AddressablesBuild.Execute` | 构建 bundle 前应用并验证，失败则构建失败 |
| `BuildPlayer.Execute`、`PlayModeGate.Run` | 在各自入口校验，失败则停止该入口 |
| Unity 直接按 Play | `ExitingEditMode` 回调检查，失败取消进入 Play |
| `sync --dry-run`、`sync --copy-only`、`doctor` | 不执行这项 Unity SpriteSkin 修复；doctor 的 GUID/状态检查不能代替实际骨骼和权重读回 |

恢复器逐项比较骨骼名称/身份、父子关系、变换、顶点坐标、索引及实际 BoneWeight 的权重和 boneIndex。已有数据一致时不重写网格；不一致时更新该 Sprite 的生成数据，再次读回，不删除或替换协作者的资产 `.meta`。六张生成幕布的 `.png.meta` 继续保持忽略，不用 `git add -f` 单独发布它们。

本地报告为 `work/tmp/curtain_skin_import.report.json`，包含 `data_version`、JSON 的 `data_sha256`、各 Sprite 数量和 `repaired`。验收须结合本次调用结果、日志及新鲜报告；`repaired: false` 表示本次比较已一致。报告中的版本/hash用于标识输入，入口仍检查实际数据，不只相信历史成功回执。

**修复 AssetDatabase 不会更新旧 bundle、catalog 或 Player。** `Use Asset Database` 可读取修复后的本地资产；`Use Existing Build` 和 Player 必须先重建 Addressables。不要把启动时的自动检查、`doctor`、dotnet 编译成功或旧报告当作最新打包内容已经验证。当前这项门禁针对六张幕布；它不是覆盖全部资源的自动迁移系统。

2026-10-04 已在现有完整资源树上验证 Unity 实际编译、六张幕布逐项读回，以及重复执行时六项均为 `repaired: false`。这不等于已完成全新 clone 的冷导入、所有运行入口的端到端测试或画面验收；后续交付需明确实际覆盖范围。

#### 流式 AnimationClip 曲线切线

幕布开合的 `AnimationClip` 使用 Unity 的 packed streamed cubic 数据。解码时，当前 key 的 `inSlope` 必须是**前一个 key 的多项式在区间末端的导数**：

```text
dt = current.time - previous.time
inSlope = 3 * previous.c1 * dt^2 + 2 * previous.c2 * dt + previous.outSlope
# previous.c1 == previous.c2 == previous.outSlope == 0 时为阶跃段：inSlope = +Infinity
```

阶跃段在下一个 key 之前保持 `previous.value`，即使数值跳变（ObjectionCutIn 等的 0/1 开关）；写成 0 切线会把跳变变成缓动。按上式解码，`prefab_plan_all.json` 中 193 个 packed clip 以 120 Hz 取样与原始 cubic 的最大误差约 `1e-4`。

不能拿当前 key 的 `c1/c2/outSlope` 反推入切线。后者会把普通的几十度变化写成约 `±12,000–16,000°/s`，Hermite 插值在两个采样点之间就会产生多圈旋转和 SpriteSkin 爆炸形变。实现位于 [PrefabBuilder.cs](UnityProject/Assets/Editor/PrefabBuilder.cs) 的 `ReadStreamedFrames()`；修改后必须重新运行 PrefabBuilder/内容构建，不能只改被忽略的 `GameContent` prefab 或旧 bundle。

`dt` 是区间长度：读帧时必须把帧时间写入 `PackedKey.time`。此前它从未赋值（恒为 0），上式在每条曲线第二段起都用绝对时间代替区间长度，幕布在 `t=0.75` 得到 `16,483°/s` 而不是 `-27.9°/s`，关键帧之间偏离原曲线最多数百度——公式正确但切线仍然爆炸的原因就在这里。另外，路径哈希在当前 prefab 层级中解析不到的绑定（例如 S01 共享的 Leia clip 出现在没有 Leia 的 prefab 中）会被跳过，与 Unity 忽略未绑定曲线的行为一致，不再写到 Animator 根节点。

`PrefabBuilder.cs`、`ContentSyncBridge.cs` 的哈希属于每个 PREFAB 转换签名。sync 对 prefab 默认只做字段补丁（`ApplyEditable`），补丁不会重建 Timeline 或重新解码 AnimationClip，所以单靠签名变化无法把解码修复送到已有副本。现在 `content.py` 在配方变化时给任务加 `rebuild` 标记（中断后保留到确认为止）；`ContentSyncBridge` 对计划中含 AnimationClip 的 prefab 改用完整 `BuildOne` 重建（`PrefabBuilder.RebuildEditable`），在空场景中构建并恢复原场景（当前场景有未保存修改时拒绝执行），之后自动运行 Addressables setup——`BuildOne` 先删除再保存资产，会使 Addressables 删掉该条目，GUID 保持不变。这 68 个 prefab（56 个 Reenact 及 ObjectionCutIn、Map、DebateText、TimeIsUp）只通过 GUID/地址被引用；其余 88 个 prefab 中的 ChoiceButton、DialogMessageLabel 等被其他 prefab 以内部 fileID 引用（2026-10-05 扫描到 29 处），删除重存会使这些引用失效，所以仍只打补丁。

Editor 打开时，`ContentSyncBridge` 发现 `Assets/Editor` 源码新于已加载程序集，会先返回 `WAITING_FOR_COMPILE` 并刷新编译，域重载后再转换；编译失败时请求以 ERROR 结束。此前 batch 重建因包编译错误和许可证失败从未生效，而在同一调用里 Refresh 后转换会运行旧代码。

验收至少要在关键帧之间取样（例如 `t=0.30`、`t=0.50`、`t=0.5833333`），并检查生成的 `m_Curve` 和运行时 `AnimationClip` 均不再出现五位数切线；只取 `t=0.75` 这类回到原角度的关键帧会漏掉过冲。应同时检查位置曲线（骨骼 y 值不应从约 `-13` 插到 `-77` 或从约 `13` 插到 `71`）和幕布 SpriteSkin 的有效顶点包围盒。

#### 分层立绘重叠（渲染管线差异）

原版游戏使用 URP（`globalgamemanagers` 的 `GraphicsSettings.m_CustomRenderPipeline` 指向管线资产），本工程 `GraphicsSettings` 未设置管线，按内置管线渲染。分层角色（Ema、Hiro 等）以相机模式渲染到 `RenderTexture.GetTemporary` 的临时纹理；`LayeredActor` 每次换表情都取新的临时纹理并释放旧的，同尺寸角色之间会互相复用。原版 prefab 的渲染相机是 `CameraClearFlags.Nothing`，内置管线按字面不清屏，结果后画的角色叠在前一个角色残留的图像上（双立绘错位重叠）。修复在内嵌包 `Packages/com.elringus.naninovel/Runtime/Actor/Layered/LayeredDrawer.cs` 的 `DrawWithCamera()`：渲染前把目标纹理清为透明，与同包 `DicedSpriteActor` 的做法一致；不要改 prefab 的 clear flags（sync 会按原版数据写回）。整体切换到 URP 属于另一项工作，见审计文档的 URP/Shader 缺口。

#### Naninovel 输入（Ctrl 快进）与泛型类桩

原版 `InputConfiguration.InputActions` 指向 `globalgamemanagers.assets` 的 path_id 3144，即游戏自己的 `WitchTrialsActions` 输入资产。它的第一个 map 是 `Naninovel`，其中 Skip 绑定 `<Keyboard>/ctrl`。本工程原来该字段为空，而原版 `ProcessLegacyBindings = 0`，因此所有 Naninovel InputSampler（Ctrl 快进、键盘继续、右键跳过影片等）都不会触发；`InputSampler.InitializeInputAction` 遇到空资产会直接返回。

修复内容：

- 新增可追踪的 `Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Models/WitchTrialsActions.inputactions`，内容来自重建的 `WitchTrialsActions.cs` 内嵌 JSON。与原版对象相比，107 个 action/binding id 完全一致。
- 它的 `.meta` 固定 GUID，并设置 `generateWrapperCode: 0`，避免 Unity 覆盖重建的包装类。
- 已跟踪的 `InputConfiguration.asset` 引用该资产。
- `NaninovelConfigBuilder.BindInputActions` 在重新 bootstrap 时写回同一绑定。

快进仍受 `GetSkipAllowed` 约束，默认只跳过已读文本。验收时应在已读内容上按住 Ctrl，或把跳过模式设为全部。

`stub_census` 看不到泛型类，因此不能据此认为没有空桩。已发现的泛型桩如下：

- `WitchBookPageBase<TItem,TState>`、`VersionedItem<T>`：已重建。魔女图鉴的 `ArgumentNullException (source)` 来自前者返回 null 的 `GetDisplayableIdVersionPairs`。
- `GigaCreation.Essentials.Input.InputManager<TControls>`：已重建（`GetAction`/`TryGetAction` 经 `BuildActionNameOrId` 取 `map/action` 键，缓存未命中时走控制器的 `FindAction`）。Cancel、Point、Submit 的查找依赖它。同时补全了生成包装类 `WitchTrialsActions` 的接口声明和 `Enable`/`Disable`/`FindAction`/`Contains`/`GetEnumerator`/`controlSchemes`/`bindings`。其中 `GetAction` 未命中时是否抛异常、`Dispose` 的具体调用是推断（Confidence B）。

#### 新增共享修复的交付要求

1. 明确每项改动的可追踪输入、生成目标和执行入口。临时修改生成树可用于定位问题，最终修复必须能由仓库源码/配方重现。
2. 复用现有 import/sync/build 入口；覆盖已有副本拉取后没有 `res` 变化的情况。验证失败应传递给调用者，不能仅写日志后返回成功。
3. 保留本地 GUID 和引用身份，只更新修复拥有的字段；重复执行应无额外资源改动。新增修复按需接入入口并验证，不假设它会被幕布恢复器自动处理。
4. 对新增输入运行 `git status --short --untracked-files=all`、`git check-ignore -v -- <文件>`，确认配方、脚本及 Unity 源资产的 `.meta` 已包含在提交清单中。未被忽略不等于已经提交；未 commit/push 不能称为 GitHub 协作者已收到。
5. 检查差异、运行相关验证，并报告本次退出结果、读回结果及尚未覆盖的场景。仅“本机运行正常”不足以证明另一份 clone 可以复现。

#### 可复制给 AI 的任务提示

仓库 AI 约束见根目录 [AGENTS.md](AGENTS.md)。给新会话或其他 AI 的提示可直接使用：

```text
请先阅读 AGENTS.md 和 DEVELOPMENT.md 的“GitHub 协作与生成资源修复”。
本次修复必须能让 GitHub 协作者在自己的已导入资源树上复现。不要只修改被忽略的 res、GameContent 或 Library；把共享输入保存为可追踪的转换器、配方或 content-overrides，并接入现有同步/构建入口。保留本地 GUID 和无关修改，覆盖“拉取后 res 未变化”的情况，失败须阻止对应流程继续。
如果涉及 Unity packed streamed AnimationClip，先核对 ReadStreamedFrames 的 cubic 区间方向：inSlope 必须由 previous.c1/previous.c2/previous.outSlope 在 current.time 处求导，不能使用当前 key 的系数；在关键帧之间采样并检查切线上限，避免把 Hermite 过冲误判为 SpriteSkin 或骨骼绑定错误。修复后重新生成 prefab、Addressables 和 Player（若使用），不要把手工改写被忽略的 prefab 当作共享交付。
检查新增文件及 Unity .meta 是否被忽略或漏入提交清单。验证实际生成数据和重复执行；涉及打包内容时重建 Addressables，再验证新 Play/Player。不要用旧回执、doctor 或 dotnet 编译替代 Unity/运行验证。
交付时列出可追踪输入、协作者命令、实际验证结果、未覆盖范围，以及是否已提交/推送。提交与推送按用户授权执行。
```

### 在不同副本之间共享资源修改

`res/`、`Assets/GameContent/` 和 Unity 的 `Library/` 都是本地数据，默认不会进入 Git。仅提交生成的 prefab、catalog 或完整资源树，协作者重新导入时仍会得到原始内容，也会丢失对象身份映射。要共享已经审阅过的资源字段，使用 `content-overrides/` 中的增量清单：它只记录资源相对路径、对象身份、字段的原值和新值；当前工具只允许 `MonoBehaviour.m_text` 和 UTF-8 文本，保留 GUID、`.meta`、PPtr 引用及协作者的其他字段。

本仓库当前提供 Debug 中文清单，包含左侧 Debug 菜单 16 个文本组件和 `Debug_Home.txt`：

```powershell
# 先准备本机可读取的资源树并完成一次 import/bootstrap
.\tools\content\import.ps1 "D:\games\manosaba_game"
& $env:MANOSABA_PYTHON tools/unity-rebuild/bootstrap_import.py --unity-editor $env:MANOSABA_UNITY_EDITOR --timeout 7200

# 预览；确认没有冲突后才写入 res
.\tools\content\overrides.ps1 apply content-overrides/current-debug-zh-Hans.json --source-root res
.\tools\content\overrides.ps1 apply content-overrides/current-debug-zh-Hans.json --source-root res --apply

# 将 res 的增量同步到生成树，再重建 Addressables
.\tools\content\sync.ps1 --category "PREFAB,TEXT"
.\tools\content\build.ps1
```

清单应用会先检查所有条目；任一条目已经被协作者改成第三种内容时，整批停止且不写入任何文件。重复应用是幂等的。`overrides.py export` 需要显式提供未修改的 `--base-root`，不会从 `base-hashes.json` 的空基线猜测差异，也不会导出 `.meta`、二进制、完整 prefab 或原始游戏文件。工具不自动启动 Unity、导入资源或构建内容。

本地复现还原工程时，第一次导入后需要运行 `bootstrap_import.py`。它会重新生成 Naninovel 配置、TMP 字体资源、prefab 和 Addressables 映射；`RuntimeResourceRepair.cs`、`NaninovelConfigBuilder.cs` 和 `AddressablesSetup.cs` 是可追踪的重建入口。不要把生成的 `GameContent` 或 `Library` 复制到 Git 中。

选项菜单检查记录：当前 `OptionsUI.prefab` 的 1,219 个源对象与本地原始资源逐项一致，没有可发布的选项界面字段差异。若两个副本的选项菜单不同，先完成上述 import/bootstrap、字体资源修复和 Addressables 重建，再比较运行时配置与自建 catalog；不要用整份原始 prefab 覆盖协作者的对象树。新的选项菜单改动应按同一增量清单方式提交。

#### Debug 菜单回退日语的原因与 IDEA 同步入口

运行时的左侧 Debug 菜单由两部分组成：Addressables 中的 Debug prefab 文本，以及 Naninovel 的 `zh-Hans/Text/Scripts/Debug/Debug_Home.txt`。即使这两部分已经是中文，`DefaultLocaleSelector` 仍会先读取 `UnityProject/Assets/StreamingAssets/config.json`；文件缺失时会保留代码中的 `ja` 默认值，文件把 `ja` 列为可用语言时还会按日文 Windows 的系统语言选择日语。表现就是 Debug 菜单从中文回退成日文，而静态 prefab 检查仍然通过。

恢复版把 `config.json` 固定为只使用 `zh-Hans`，并为 `ja`、`zh-Hant`、系统未支持的语言提供 `zh-Hans` fallback。这个小文件必须进入 Git；`StreamingAssets` 其余内容仍保持忽略，因为 Addressables bundle 可以本地重建。修改后不要只重启 Play，必须让 Unity 重新导入并重建 Addressables。

IDEA/Rider 可直接运行 `.idea/runConfigurations/Sync_Localization_Build.xml`：脚本 `tools/unity-rebuild/sync_localization_idea.ps1` 会把 `res/Assets/#WitchTrials/Localization/zh-Hans/Text` 增量同步到 `UnityProject/Assets/GameContent/.../zh-Hans/Text`，校正 `config.json`，然后在 Unity 已打开时通过 `Library/ManosabaLocalization/request.json` 请求 Editor 构建；Unity 未打开时则使用 batchmode 构建。只想复制文件时运行 `Sync_Localization_Copy.xml`（等价于 `-SyncOnly`）。

IDEA 的 `Run_Unity_Game.xml` 用 `tools/unity-rebuild/run_unity_game_idea.ps1` 直接启动项目要求版本的 `Unity.exe`，不经过 Unity Hub。Unity 已打开时脚本只写入 `Library/ManosabaIdeaLauncher/request.json`，由 `IdeaPlayModeBridge` 让当前 Editor 进入 `Assets/Scenes/Boot.unity` 的 Play Mode；这样不会为同一个项目启动第二个 Editor。IDEA 默认选择 Addressables 的 `Use Asset Database` 播放模式，直接读取当前项目资源，不要求先有 Player Content；需要验证 catalog/bundle 时才传 `-UsePackedContent`，脚本会检查实际 catalog、bundle 和远程缓存，而不是只相信旧回执。`-SkipContentCheck` 只用于有意绕过打包内容检查的实验。

#### IDEA 黑屏、日文 Debug 和白块的排查顺序

`20ba94a2` 同时把 `StateConfiguration.asset` 的 `GameStateHandler` 改成了 `GigaCreation.NaninovelExtender.Common.UniversalGameStateSerializerExtended`，并把 Addressables active profile 切到 `ManosabaAndroidPatch`。前者在没有对应类型时会让 `StateManager` 的 `Type.GetType` 返回空，随后 `Activator.CreateInstance` 抛出 `ArgumentNullException(type)`；`EntrySequence.ResetDebugVariables` 继续读取 `ICustomVariableManager` 时才出现二次错误。现在由 `UniversalGameStateSerializerExtended` 继承 `IOGameStateSlotManagerExtended` 提供该类型。

后者的 `Remote.LoadPath` 是 `file://[Application.persistentDataPath]/extracted/[BuildTarget]`，编辑器会展开到 `C:\Users\Administrator\AppData\LocalLow\acacia\manosaba\extracted\StandaloneWindows64`。该目录不存在或 catalog 与 bundle 不匹配时，最早的真实错误是 bundle 404，接着 `Text/CharacterNames`、`System/System_Title` 加载失败；Game prefab 已经生成，所以画面会留下 Debug UI 而呈黑屏。远程 profile 还会读到旧的日文/资源版本，表现为 Debug 菜单变回日文或标题素材变成白块。仓库当前 active profile 固定为本地 `Default`，IDEA 本地调试也使用 `Use Asset Database`；Android/原版缓存测试仍可选用远程 profile，但必须先导入同一版本的完整 extracted 目录和 catalog。

`Default` profile（`40d2c9efc9e57424c8b9e7879ac2fd48`）用于 Editor/Windows 本地资源；`ManosabaAndroidPatch` 只用于已有同版本 extracted 缓存的移动/原版测试。切换 profile 只改变后续构建的路径变量，不会改写已经生成的 catalog；因此第一次导入或内容同步后，必须用 `addressables_plan_all.json` 重新绑定本地 `.meta` GUID，再构建 Addressables。`tools/content/build.ps1` 和 Content Sync bridge 已内置这一步，避免协作者提交的旧 GUID 在另一份生成树中静默丢资源。

IDEA 的 `Use Asset Database` 模式还会启用 `LocalOpeningTest` 的 EditorResourceProvider。Naninovel 的 `EditorResources.asset` 将 `UI/CGGalleryUI` 和 `UI/SettingsUI` 指向包内 DefaultUI；如果直接复制这些记录，它们会优先遮住本地 `GameContent/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/GalleryUI.prefab` 和 `OptionsUI.prefab`，画廊与选项就会看起来像还原版本。`LocalOpeningTest` 现在只跳过这两个默认记录，让 Addressables 的 `Naninovel/UI/GalleryUI` 和 `Naninovel/UI/OptionsUI` 成为唯一加载源；不要把自定义 prefab 再注册到 `CGGalleryUI/SettingsUI` 别名，否则同一 prefab 可能被实例化两次。这只影响 Editor Database 测试，不改变 Naninovel 包或 Addressables profile。

IDEA bridge 的 `PLAYING` 只表示 Unity 已进入 Play Mode，不表示 Naninovel 已完成初始化。若仍有白块，先看 `tools/unity-rebuild/out/logs/idea_unity_editor.log` 中第一条 `RemoteProviderException` 或 `Resource ... failed`，再看组件字段空引用。`PaginationIndicator` 的序列化字段是 `_canvasGroup`，不能改成 `_canvas`；后者会让保存/读档分页 prefab 的 CanvasGroup 引用丢失，并在 `Pagination.ChangeIndicatorStyle` 抛出 `NullReferenceException`。

启动最前面的两个品牌 Logo 另有一个容易混淆的情况：`UnityProject/Assets/Scenes/Boot.unity` 中 `BrandLogo`、`CompanyLogo` 的 `Image.m_Sprite` 必须分别指向 `GameContent/Uncategorized/Sprite/BrandLogo_Acacia.sprite.sprites/BrandLogo_Acacia.png.meta` 和 `CompanyLogo_ReAER.sprite.sprites/CompanyLogo_ReAER.png.meta` 的 GUID。若场景仍保存旧 GUID，Unity 会把 Sprite 解析为 `None`，但 Image 的白色背景仍会显示，于是看起来像两个白块。遇到这个现象先按 GUID 检查并让 Unity 重新导入场景，不要替换 Logo 图片或修改 SplashScreen 逻辑。

Unity 6000 的 Editor-only Addressables locator 在第一次扫描 `Naninovel` 全量标签时，可能对少数复杂 CutIn prefab 输出 `Assertion failed on expression: 'pred(*previous, *i)'`；探针确认这些条目仍全部返回，且 packed build 不依赖该 Editor 扫描。只要没有紧随其后的资源加载失败，就把它视为编辑器扫描噪声，不要为消除它删除 Naninovel 标签或本地资源映射。

2026-10-04 的 IDEA 启动复核进一步确认：`IdeaPlayModeBridge` 已选择 `Use Asset Database`，本地生成的 440 个剧本资源、444 个本地化资源和 Trial10 的 S01-S05 prefab 都已在当前项目中被导入；这次不能归因于“修复没有同步”。旧的运行时证据还显示 Naninovel 初始化完成、TitleUI 可见并能推进剧本，因此上述 locator 断言本身不是人偶或黑屏的根因。

如果 IDEA 配置只显示 `PLAYING` 后 Unity 窗口消失，先看 `tools/unity-rebuild/out/logs/idea_unity_editor.log` 的末尾。`No valid Unity Editor license found`、`No ULF license found` 或 `Unable to update licenses` 不一定表示许可证过期：2026-10-04 本机仍有有效的 Unity Personal entitlement，但直接启动的旧 Licensing Client 读不到 Hub 的登录会话。现在 `run_unity_game_idea.ps1` 默认先唤醒 Unity Hub 授权 broker，动态发现 `Unity-LicenseClient-*` 管道，再把临时的 `-useHub -hubIPC -licensingIpc LicenseClient-*` 参数交给 IDEA 直启的 Editor；管道名、Hub access token 和 refresh token 都不写入仓库。首次使用或协作者换电脑时运行 `.idea/runConfigurations/Setup_Unity_License.xml`，点击“获取授权 / 登录 Unity”在 Hub 完成自己的 Unity 账号登录，再点“启动 Unity 游戏”。窗口也支持可选的 Editor access token 输入，使用当前 Windows 用户 DPAPI 加密保存到 `%LOCALAPPDATA%\ManosabaUnity`，不会显示或提交；Personal 用户通常不需要填写。需要强制测试完全独立的 Editor 授权时才传 `-NoHubLicense`。脚本会清理旧的 `result.json`，并在认证失败时立即给出明确错误，不再把旧的 `PLAYING` 回执当成新会话。认证问题未解决前不要为了排除黑屏去改 Addressables 标签或删除人偶资源。

```powershell
# IDEA 外部工具或终端
powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools/unity-rebuild/sync_localization_idea.ps1
powershell.exe -NoProfile -ExecutionPolicy Bypass -File tools/unity-rebuild/sync_localization_idea.ps1 -SyncOnly

# Python 入口也会校正同一个 locale 配置
python tools/unity-rebuild/sync_localization.py --dry-run
python tools/unity-rebuild/sync_localization.py --rebuild
```

验证时至少检查三层：`verify_debug_ui_translation.py --static-only` 检查 prefab/TextAsset，`addressables_build_result.json` 的时间戳确认内容是新构建，最后用新 Play 会话或新 Player 检查左侧菜单。旧 Player 或旧 `Library/com.unity.addressables/aa` 不代表当前 `res` 已经生效。

#### 返回标题页面后立即收起的原因

暂停菜单的标题按钮会先调用 `ControlPanel.Close()`，所以菜单先收起是预期行为。确认后，`ControlPanel` 依赖临时 `NaninovelScriptPlayer` 播放 `@returnToTitle`；还原代码中该组件的 `Awake`、`PlayAsync` 重载和内部播放方法曾全部是空实现，只返回已完成的 `UniTask`，导致脚本没有执行，标题状态也不会切换。修复时必须让它按 `ScriptPlayer` 语义调用 `LoadAndPlay` 或 `PlayTransient`，再重新编译 Unity Player/Addressables。这个故障与 locale 无关。

如果确认框本身一闪就消失，先检查输入扩展是否仍是 stub：`InputSystemExtensions.OnPerformAsync` 和 `ResponseExtensions.ToCancel` 若返回默认完成的 `UniTask`，`_cancelableByAction=1` 的任何 `ProvidableDialog` 都会立即得到 `Cancel`。现在前者等待真实的 `InputAction.performed` 并在取消后解除订阅，后者等待输入任务完成后才返回 `ResponseKind.Cancel`。手机标题按钮还可能把同一个 EventSystem pointer 事件穿透到确认框的全屏 underlay；`ControlPanel.ReturnToTitleAsync` 在开始 `ConfirmAsync` 前等待一帧，确保打开按钮的 pointer-up 已经结束，再注册取消监听。不要通过恢复旧 catalog 或切回远程 profile 规避这个输入时序问题。

| 修改目标 | 编辑位置 | 注意事项 |
| --- | --- | --- |
| 中文及其他本地化文本 | `res/Assets/#WitchTrials/Localization/**` | UTF-8；保留现有键和文件身份 |
| ManagedText | `res/Assets/#WitchTrials/ManagedText/**` | 复用现有 TextAsset 链 |
| 立绘、Texture、Sprite crop | 索引映射的 `Textures/**`、`Prefabs/**` PNG | 保留尺寸和 Sprite 身份；crop 复用透明补边规则 |
| Naninovel 剧情 | `res/Assets/#WitchTrials/Scenarios/**/*.asset.json` | 修改支持的 textMap/命令参数；保留 registry 类型和 rid 身份 |
| CharacterData、MapData 等 | `res/Assets/#WitchTrials/Data/**/*.asset.json` | 优先 string/bool/number/enum、primitive list 和简单结构 |
| MasterData | `res/Assets/#WitchTrials/Data/MasterData.asset.json` | 转换到现有 Resources 资产 |
| Prefab 简单字段 | `res/_serialized/**` 和计划指向的组件 JSON | 定位现有组件；不新增层级或组件 |

原始 JSON 的形状随资产类型而异，应先查看已有字段和同步报告，避免自行替换整个对象。`.meta`、GUID、Addressables address、原对象 path_id、已有引用身份不是普通内容字段。图片同步更新文件内容并保留已有 importer 参数；不要通过删除 `.meta` 解决导入问题。

可限定同步范围，`--file` 路径相对 `res`，可重复指定：

```powershell
.\tools\content\sync.ps1 --category "text,texture,sprite"
.\tools\content\sync.ps1 --file "Assets/#WitchTrials/Localization/zh-Hans/Text/Scripts/Act01_Chapter01/Act01_Chapter01_Adv01.txt"
.\tools\content\sync.ps1 --file "Assets/#WitchTrials/Data/ScriptableObjects/CharacterData.asset.json"
.\tools\content\sync.ps1 --category "data,scenario" --timeout 3600
.\tools\content\doctor.ps1 --deep
```

PowerShell 5.1 下的逗号类别列表需要加引号。`-DryRun`、`-Category`、`-File` 等别名也可使用。无变更时跳过普通资源复制/转换，但普通 sync 仍调用 Unity 幕布 SpriteSkin 校验；工具或计划变化会使相关转换失效并重跑。`--force` 会强制处理选中资源，首次同步和 force 使用有上限的 Unity 批次。

打开 Editor 时先停止 Play，sync 通过 `ContentSyncBridge` 处理；Editor 未打开时使用 batchmode。完整 G gates、`build.ps1 --player` 和 round-trip 会启动独立 batch 验证，应先关闭占用该工程的 Editor，并按顺序运行。

## 4. 编辑性与恢复

`FULL` 表示支持的内容字段可写，身份字段仍保留；`PARTIAL` 表示存在明确的只读字段；`READ_ONLY` 表示保留现有资产，不强行转换。具体原因写在 manifest 中。

含未知复杂结构、AnimationCurve 或未解析引用的数组不能安全增删；同长度数组的安全标量仍可修改。已确认的 primitive/简单值类型列表支持增删。未映射 PPtr、Timeline/PlayableDirector、重复 Sprite 的次要输出和无法唯一定位的 Prefab 组件可能只读。CreditsUI 当前保留 `READ_ONLY_HIERARCHY`。这些边界不能靠删除资产、修改 GUID 或关闭验证绕过。

成功批次会保存检查点，失败项留待重试。修正错误后重新运行同一 sync 即可。`--copy-only` 仅暂存叶子内容，会留下 `PENDING_IMPORT`；随后普通 sync 才完成导入，doctor 不会把待导入状态判成成功。

## 5. 维护代码与扩展转换器

| 代码位置 | 维护职责 |
| --- | --- |
| `UnityProject/Assets/Scripts/Game/` | 当前 Unity 游戏程序集和恢复实现 |
| `UnityProject/Assets/Scripts/Runtime/` | 重建启动、内容探针与运行时集成 |
| `UnityProject/Assets/Editor/` | 导入策略、字段转换、Addressables、构建和 Editor 验证 |
| `tools/content/content.py` | 统一 CLI、资源分类、增量调度、doctor 和构建入口 |
| `tools/content/import_workspace.py`、`state_store.py` | 保护编辑的提取合并、流式状态文件与字段报告 |
| `tools/unity-rebuild/a13_import_assets.py` | 现有叶子导入和目标映射 |
| `tools/unity-rebuild/` 的其他工具 | 既有转换计划、构建/运行 gates 和重建证据恢复 |
| `src/decompiled/`、`src/il2cpp/`、`project/` | 结构参考和研究工程；不能用 stub 数量判断 Unity 工程是否能运行 |

扩展支持时先复用现有 importer/applier，再新增资源类别或字段规则。每个改动应明确：源对象身份、唯一目标映射、GUID 保留方式、允许写入的字段、依赖失效条件、失败检查点与恢复方法。未知引用或字段应记录只读原因，避免猜测或静默覆盖。

修改转换器要同时考虑 Python 调度和 C# 实际序列化行为。转换签名负责让依赖变更触发重新同步；批次必须保留整个 Prefab 的原子性。不要增加第二套资源 importer，也不要把原版 EXE、GameAssembly、IL2CPP 元数据或安装目录重新接入普通 sync/build 预检。

Unity 编译和运行是重建工程的验收依据。`project/Manosaba.sln` 服务于参考代码浏览/研究，其 dotnet 构建不能替代 Unity 编译门禁。

## 6. 验证改动

### 不依赖游戏资产的 Python 回归

```powershell
& $env:MANOSABA_PYTHON -m unittest discover -s tools/content -p test_content.py -v
& $env:MANOSABA_PYTHON -m unittest discover -s tools/content/tests -p "test_*.py" -v
& $env:MANOSABA_PYTHON -m unittest discover -s tools/unity-rebuild -p "test_*.py" -v
```

截至 2026-10-02，三个集合分别为 19、38、32 项，共 89 项通过；字段安全另有实际 Unity/C# 检查。PowerShell 不可用时相关测试会跳过，不能把跳过当作对应平台已验证。针对改动运行相关回归；资源转换行为还需要实际 Unity 验证。

### 实际修改、构建、加载、恢复

```powershell
# TEXT / IMAGE / DATA / SCENARIO，包含 built-bundle 与 Play Mode 验证
& $env:MANOSABA_PYTHON tools/content/roundtrip.py run --directory tools/unity-rebuild/out/content-roundtrip-dev01

# MasterData 的 Resources getter 验证
& $env:MANOSABA_PYTHON tools/content/roundtrip.py run --master-data-only --directory tools/unity-rebuild/out/content-roundtrip-master-dev01
```

每次选新输出目录。工具先记录备份 journal，结束后恢复；若被强制中断，按照 [round-trip 恢复说明](tools/content/README-roundtrip.md) 完成恢复、重新导入和必要的内容重建。单纯复制成功、JSON 改变或 AssetDatabase 加载不能替代 built-bundle/runtime 验证。

### 现有 Unity gates 与原目录独立性

| Gate | 验证对象 | 脚本 |
| --- | --- | --- |
| G1 | Unity 实际编译、程序集图和加载错误 | `g1_compile.ps1` |
| G2 | 两次 Editor 导入、资产完整性、GUID 稳定 | `g2_import_gate.ps1` |
| G3 | 自建 Addressables catalog/bundles | `g3_content_build.ps1` |
| G4 | 现有构建模式下的 Editor Play 启动和 bundle 加载 | `g4_play_mode.ps1` |
| G5 | Windows Player 构建、启动、内容加载 | `g5_player_smoke.ps1` |

这些脚本均在 `tools/unity-rebuild/`；执行 G3 完整内容构建时使用 `-Group all`。完整独立性 runner 会顺序运行 sync 和全部 gates，临时改名原目录，并在 `finally` 中恢复：

```powershell
& $env:MANOSABA_PYTHON tools/unity-rebuild/check_content_independence.py --disconnect-original --game-root "D:/games/manosaba_game" --timeout 7200
```

运行前退出原游戏和占用目录的工具；若进程被强制终止，检查输出目录的 `disconnect.json`，按记录的实际路径恢复目录。`--static-only` 仅做静态检查，阶段子集或非 all 组只能取得部分验证状态。

2026-10-02 已完成 TEXT/IMAGE/DATA/SCENARIO、MasterData 实际 round-trip、深度 doctor，以及原目录实际断开后的 sync + G1–G5。最终 G4/G5 为无图形冒烟，验证启动和内容加载；完整游戏分支、显示画面/音视频和 IL2CPP release 没有据此宣称验证完成。残留诊断和精确数据见 [VERIFICATION.md](tools/content/VERIFICATION.md)。

## 7. IDE 与本地开场测试

IDEA/Rider 的 `.idea/runConfigurations/` 保留：

- `Content_Sync.xml`：同步所有可编辑资源。
- `Content_Build.xml`：同步并构建重建内容。
- `Sync_Localization_Copy.xml`、`Sync_Localization_Build.xml`：原有中文文本入口。
- `Local_Opening_Enable.xml`、`Local_Opening_Disable.xml`：原有本地开场测试开关。

运行配置的工作目录为仓库根目录。IDE 从已配置环境的终端启动，或在 IDE 中设置工具环境变量；不要提交本机绝对路径。

本地开场入口编辑 `funny/opening-bmpt72-test.nani`，切换后开启新的 Play 会话，无需 Addressables 重建。这是现有本地开发测试入口；发布资源仍走 `res → sync → build`。详细行为见 [README-local-opening.txt](funny/README-local-opening.txt)。

## 8. 排错与提交

| 现象 | 优先检查 |
| --- | --- |
| Python 或 Unity 找不到 | 解释器是否可运行、环境变量、Unity 路径和许可证 |
| 提示 workspace/Editor 正忙 | 停止 Play；等待前一任务退出，完整 batch gate 前关闭该工程 Editor |
| `PENDING_IMPORT` | 是否用了 copy-only 或上次失败；修正原因后重新 sync |
| GUID 漂移、meta 缺失 | 从自己的备份恢复原 `.meta`，不要重新创建身份 |
| res 已改但运行还是旧内容 | 查看目标映射和同步结果；重建 Addressables；使用现有构建模式并启动新会话；检查本地化覆盖 |
| 某个 JSON 字段没生效 | 查看 PARTIAL/READ_ONLY 字段说明、managed 类型/rid、数组和引用边界 |
| 大批量超时或内存不足 | 查看当前批次日志与检查点；释放非必要内存，必要时提高 `--timeout` 后续跑 |
| gate 失败但有旧 PASS 文件 | 核对退出码和本次新鲜回执；旧结果不能证明此次成功 |

输出主要位于 `tools/unity-rebuild/out/`：Unity 日志在 `logs/`，原生回执与 gate 结果在 `unity/`，独立性和 round-trip 各有独立子目录。Editor 请求/批次信息在 `UnityProject/Library/ManosabaContent/`。报告结合源路径、目标、GUID、字段原因和退出码阅读，不要只看最后一句日志。

`preflight.py --mode research` 和 BepInEx/原游戏对照工具保留研究用途；普通开发使用 content/build 预检，不需配置原版运行二进制。资源来自自己能正常读取的本地数据；不绕过 DRM、访问权限或下载游戏资源。

直接调用预检时必须指定模式；它只检查输入，不替代编译或运行验证：

```powershell
& $env:MANOSABA_PYTHON tools/unity-rebuild/preflight.py --mode content
& $env:MANOSABA_PYTHON tools/unity-rebuild/preflight.py --mode build
```

代码贡献应按具体文件暂存并检查差异，只提交代码、配置、测试和文档。`res/`、GameContent、资源缓存、日志、备份和 `dist/` 保持本地；Git 不会保存这些编辑。保留工作区中已有的独立修改，不把 Unity 运行产生的存档、临时服务状态或本机路径混入提交。

更多 CLI 参数和支持范围见 [可编辑资源工作流](tools/content/README.md)。


## Trial10 人偶剧 Timeline 重建

第二幕第 6 章辩论 10 通过 SpawnExtended 依次加载 Reenact_Ch02_E06_S01 到 S05。每个人偶 prefab 必须同时包含三个 PlayableDirector 的 m_PlayableAsset、本章 Timeline 的 AnimationTrack/AnimationClip，以及共用的 CurtainOpen/CurtainClose Timeline 图。此前 prefab 只有场景层级，Timeline 子资产没有写入，导致幕布和人偶都无法演出。

a13e_plan_prefabs.py 会把跨 prefab 的 Timeline/Track/Clip 图复制为当前 prefab 的子资产；PrefabBuilder 负责创建这些对象、写入 32 位动画字段并在 AddObjectToAsset 后再次保存。验收时检查 prefab_build_puppet_result.json 的五个 job 没有 Timeline 相关 unresolved，并确认五个 prefab 中 m_PlayableAsset 数量为 3、fileID 0 数量为 0。BUILT_PARTIAL 若只剩 SpriteRenderer 默认材质 PPtr 未映射，不影响人偶时间轴。

Addressables 计划中一个 prefab 只保留一个 prefab 地址；prefab 内嵌的 PNG/Sprite 是依赖，不再作为同地址的重复 location。修改这一规则后需要重新生成 G3 基线，不能把预期删除的内嵌成员当作内容丢失。



## Trial10 当前修复与验收

`PrefabBuilder` 现在会解码原始 `m_MuscleClip.m_Clip` 中的 packed generic 曲线，并按 Animator 绑定根解析 CRC32 路径后写回普通 `AnimationClip` 曲线。路径解析必须同时索引完整层级的每个后缀；例如 prefab 中的 `BackgroundBlack/black_R` 对应 Timeline 的 `black_R`，幕布则从 `CurtainRoot/...` 开始。只创建非空 `AnimationClip` 对象而不恢复曲线，会让 Timeline 看似有资源但实际没有任何动作。

`PlayableDirectorExtensions` 原先的 `PlayAsync`、`ResumeAsync`、`IsFinished` 和 `WaitForFinishAsync` 都是反编译留下的默认实现，导致人偶 Timeline 从未真正播放。现在会调用 `PlayableDirector.Play/Resume`，等待 PlayableGraph 根节点完成，并在取消时停止 director；UnityProject 源码和 `src/decompiled` canonical 源码保持同步。

验收工具为 `Manosaba.EditorTools.ReenactmentProbe`，运行后写入 `tools/unity-rebuild/out/unity/reenactment_probe.json`。当前第二幕第六章辩论 10 的 S01-S05 全部加载成功；每个 prefab 的 Story、CurtainOpen、CurtainClose 曲线均非空，且对 CurtainOpen/CurtainClose 做 director 演算时能观察到 CurtainRoot 骨骼变换和 `black`/`black_L`/`black_R` 的 SpriteRenderer alpha 变化。重新生成内容后要重新运行该探针，不能只看旧的 prefab 或 Addressables 结果文件。
