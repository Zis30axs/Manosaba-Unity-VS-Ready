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

PowerShell 5.1 下的逗号类别列表需要加引号。`-DryRun`、`-Category`、`-File` 等别名也可使用。无变更同步不会重写资源或调用转换器；工具或计划变化会使相关转换失效并重跑。`--force` 会强制处理选中资源，首次同步和 force 使用有上限的 Unity 批次。

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
