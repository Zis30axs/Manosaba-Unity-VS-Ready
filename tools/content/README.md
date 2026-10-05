# 可编辑资源工作流

首次配置环境或维护转换器，请先看仓库根目录的 [开发指南](../../DEVELOPMENT.md)。

`res/` 是用户本地、gitignored 的唯一日常编辑源；`UnityProject/Assets/GameContent/` 是由它生成的 Unity 导入和构建树。不要在重新同步之后依赖手动修改的 GameContent 文件。

幕布 SpriteSkin 也遵循这条规则：`UnityProject/Assets/Editor/OriginalCurtainSpriteData.json` 是版本库中的唯一配方，`RestoreOriginalCurtainSkin.cs` 会在内容同步、Addressables/Player 构建和 Play Mode 门禁前自动应用并逐项读回校验。六个生成的 `.png.meta` 不提交；协作者拉取配方和脚本后，运行普通 `sync.ps1`、`build.ps1`、Player 构建或新的 Play 会话即可触发这项检查。`doctor.ps1`、`sync.ps1 --dry-run` 和 `--copy-only` 只做各自的静态/暂存工作，不会执行 Unity SpriteSkin 恢复。缺失或不匹配会使实际入口失败而不会生成一个看似可运行的旧状态。详见 [开发指南的协作章节](../../DEVELOPMENT.md#github-协作与生成资源修复)。

需要 Python 3.11+、Unity 6000.0.48f1（已激活许可证）和项目依赖。首次提取使用既有 `work/extract_res.py`，还需要 UnityPy、Pillow、soundfile；当前本地 `tools/venv` 可以继续使用。用 `MANOSABA_PYTHON` 和 `MANOSABA_UNITY_EDITOR` 指定工具位置，机器路径只放在环境变量或 gitignored 的 `tools/audit/audit.config.local.json`。

```powershell
# 只读取你自己能够正常访问的本地游戏资源
.\tools\content\import.ps1 "D:\games\manosaba_game"

# 从此主要编辑 res/；原游戏目录可以断开
.\tools\content\sync.ps1
.\tools\content\doctor.ps1
.\tools\content\build.ps1

# 同时构建 Windows Player 并运行现有 G5 冒烟测试
.\tools\content\build.ps1 --player
```

首次导入没有 `res/_index.csv` 时，原有提取器在隔离的暂存目录导出对象，按原对象身份合并进 `res`，然后运行现有 a13、ScriptableObject/Prefab/config/boot 转换工具。已有完整提取树和 Unity 资产时则接纳它们，保留现有开发测试入口。它不会运行 BepInEx 或要求 EXE、GameAssembly、IL2CPP 元数据。

本次实际验证使用现有完整资源树，接纳和全量同步已经完成，独立构建检查结果见验证记录；完全清空 `res`、GameContent、Library 后的首次导入尚未端到端验证。

导入保存两种本地数据：

- `res/.imported-source/manosaba_Data/`：可读 bundle、catalog 和 SerializedFile 资源缓存；不复制 EXE、DLL、IL2CPP 或 BepInEx。
- `res/.manosaba/reconstruction/`：既有导入账本、脚本绑定、转换计划、catalog 地址/label 证据。删除 `tools/unity-rebuild/out` 后可以恢复。

日常 Addressables 构建使用本工程的 Addressables settings，产生自己的 bundle、catalog 和 settings。出货 catalog 留在本地导入缓存/`StreamingAssets/shipped-aa`，供重建证据和原有比较测试使用。`apply_shipped_aa.ps1` 只读导入缓存，不再读取安装目录。

## 增量同步

```powershell
.\tools\content\sync.ps1 --dry-run
.\tools\content\sync.ps1 --category "text,texture,sprite"
.\tools\content\sync.ps1 --file "Assets/#WitchTrials/Localization/zh-Hans/Text/Scripts/Act01_Chapter01/Act01_Chapter01_Adv01.txt"
.\tools\content\sync.ps1 --force
.\tools\content\doctor.ps1 --deep
```

`--file` 可重复，路径相对 `res`；`-File`、`-Category`、`-DryRun`、`-Force` 等 PowerShell 写法也支持。`--copy-only` 可以只暂存叶子资源，manifest 保留 `PENDING_IMPORT`，doctor 不会把它当成完成，下一次普通 sync 会重试 Unity 导入/转换。

manifest `res/.manosaba-content-state.json` 记录路径、大小、纳秒 mtime、SHA-256、资源类别、Unity 目标、元数据 GUID、Addressables 地址/组、转换器/计划依赖、同步状态和字段可编辑性。源、目标和转换依赖都未改变时不复制、不调用转换器；转换器或计划变更会重新转换相关资产。复制 PNG/文本/音频等复用 a13 的叶子导入函数，只修改文件内容；`.meta`、GUID、Addressables address 和已有 Sprite importer 参数保持不变。GUID 变化会明确报错，须恢复原 `.meta`；源文件删除会报错并保留 Unity 资产，避免隐式破坏引用。

原版 trimmed Sprite crop 会复用现有透明补边规则，按导入元数据中的矩形和偏移生成 Unity PNG；`res` 中的 crop 保持原样。manifest 同时保存转换后 hash，重复同步不会把补边图覆盖回裁切图。每轴允许最多一像素舍入越界并裁去该像素；缺失偏移、非法尺寸或超过该容差的越界会明确失败。

首次同步和 `--force` 按有上限的批次转换，每个成功批次立即保存检查点；同一个 Prefab 的全部选中源对象保持在同批。进程退出后再次运行 sync 会重试剩余项。首次导入默认每次 Unity 调用最长 7200 秒，日常同步/构建默认 1800 秒，可用 `--timeout` 调整。

状态文件逐资源读取，数组字段用 `.Array.data[*]` 合并相同路径，并以 `occurrences` 保留元素出现次数；同一路径包含不同支持级别时标为 `PARTIAL`。旧版逐元素字段说明会自动合并，避免大型首次同步耗尽内存。

打开的 Unity Editor 通过 `ContentSyncBridge` 接收请求；需先停止 Play Mode。Editor 没打开时使用 batchmode。IDEA/Rider 新增“同步所有可编辑资源”和“同步并构建重建内容”运行配置；原有中文文本/local opening 配置继续可用。

## 可以编辑什么

| 类别 | 编辑源 | 转换/运行入口 | 边界 |
|---|---|---|---|
| TEXT | `Localization/**`、`ManagedText/**` 的 txt | 原有文本复制链 → TextAsset → Addressables | 现有地址和本地化覆盖规则不变 |
| TEXTURE / SPRITE | `Textures/**`、`Prefabs/**` 的 PNG 和 crop | a13 目标映射 → Unity Texture/Sprite → bundle | 保留现有 importer；原账本中重复目标的次要 crop 为 READ_ONLY |
| DATA | `Data/**/*.asset.json` | 现有共享字段 applier → ScriptableObject → Addressables | string/bool/int/float/enum、primitive list、简单嵌套结构可编辑 |
| MasterData | `Data/MasterData.asset.json`（首次导入导出） | `Assets/NaninovelData/Resources/MasterData.asset` → `Resources.Load` | 保留 Resources 身份和现有引用 |
| SCENARIO | `Scenarios/**/*.asset.json` | 现有托管引用转换器 → 真正的 `Naninovel.Script` → bundle | textMap 和已支持的命令参数可编辑；registry 类型/rid 身份保留 |
| PREFAB | `_serialized/**` 以及计划指向的组件 JSON | 原有 PrefabBuilder，定位现有对象后批量更新 | Transform/RectTransform、简单 serialized field；不添加层级/组件 |
| AUDIO / VIDEO / FONT | 索引内的 ogg/m4v/ttf 等 | 原有 a13 叶子导入 → Unity → Addressables | 使用现有导入策略 |

`FULL` 表示支持的内容字段均可写；Unity 身份字段仍不可改。`PARTIAL` 表示有明确的只读字段；`READ_ONLY` 表示保留数据而不强行转换。复杂原始 PPtr、无法唯一定位的 Prefab 组件和未支持资产保留原内容，结果逐字段写出原因。已经稳定映射的引用可以使用 `{ "guid": "...", "file_id": 123 }`。不要猜测原始 `{m_FileID,m_PathID}`。

既有 CreditsUI Prefab 无法通过 Unity `LoadPrefabContents` 作为单一层级加载，保留为 `READ_ONLY_HIERARCHY`，不重建其层级或修改文件。含无法稳定映射引用的 Timeline/PlayableDirector 字段同样保留只读；导入音频文件并不等于恢复这类动画引用。

数组增删会先检查已有元素：若包含 AnimationCurve、未知复杂结构或未解析引用，则保留整个数组并记录只读原因；同长度数组中的安全标量仍可编辑。已确认的 primitive 和简单值类型列表允许增删。

## 重新导入时保护修改

普通 `import` 接纳已有 res，不重新导出。需要重新提取原始资源时：

```powershell
.\tools\content\import.ps1 "D:\games\manosaba_game" --reextract
# 只有明确希望丢弃对应用户修改时才加这个选项
.\tools\content\import.ps1 "D:\games\manosaba_game" --reextract --overwrite-user-changes
```

`res/.manosaba/base-hashes.json` 保存原始资源 hash。当前文件与上次原始 hash 不同则保留为 `USER_MODIFIED`；接纳既有提取树时无法证明原始 hash 的文件采用保守保护。合并按 bundle/path_id/type 身份恢复原输出路径，不依赖容易变动的 `~1` 后缀。不会隐式删除旧资源。

## 验证

研究预检保留：`preflight.py --mode research`；普通流程使用 `--mode content` 和 `--mode build`。后两者不把原游戏二进制当成要求。

```powershell
# 实际 TEXT / IMAGE / DATA / SCENARIO 修改、bundle 验证、Play Mode 加载，并恢复
python tools/content/roundtrip.py run --unity "D:/Unity/6000.0.48f1/Editor/Unity.exe"

# 单独验证 MasterData 的 Resources 运行时 getter，并恢复
python tools/content/roundtrip.py run --master-data-only --unity "D:/Unity/6000.0.48f1/Editor/Unity.exe"

# 自动断开原游戏，运行 sync + G1/G2/G3/G4/G5，finally 恢复目录
python tools/unity-rebuild/check_content_independence.py --disconnect-original --game-root "D:/games/manosaba_game"

# 静态检查和子集测试会明确标记 STATIC_ONLY / PARTIAL，不能冒充完整 PASS
python tools/unity-rebuild/check_content_independence.py --static-only
```

round-trip 恢复流程和输出见 [README-roundtrip.md](README-roundtrip.md)。运行结果、恢复 journal 和版权资源均保持 gitignored。只从用户能正常读取的资源导入，不绕过 DRM、Steam ownership、加密或文件权限，不下载或提交游戏资源。

已完成的实际验证与尚未覆盖的范围见 [VERIFICATION.md](VERIFICATION.md)。
