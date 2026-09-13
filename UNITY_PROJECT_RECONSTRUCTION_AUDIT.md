# Unity Project Reconstruction Audit

**对象**：Manosaba（WitchTrials.* / Unity 6000.0.48f1 / IL2CPP / Addressables 2.4.6）
**分支**：`audit/unity-assets`（worktree `E:\Manosaba-DS`）
**被审计资源**：`E:\Manosaba-VSCodeReady\res`（58,750 文件 / 11 GiB，主 checkout，git 忽略）
**原始 ground truth**：`E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\manosaba_Data\`（457 个 `.bundle` + 5 个 Player SerializedFile，共 ~6.44 GiB）
**分析工具**：`tools/audit/a01..a08`（Python 3.12 + UnityPy 1.25.3），输出 `tools/audit/out/`

> 本审计**不恢复任何 native 方法体**，不改 `RECONSTRUCTION.md`，不改已确认的 reconstructed C#。

---

## 0. 结论（TL;DR）

| 问题 | 结论 |
|---|---|
| 现有 `res/` 是“按类型导出的 AssetRipper dump”？ | **不是 AssetRipper**。是 UnityPy 1.25.3 逐 SerializedFile 导出的 **typetree JSON dump**，目录按 Addressables container path 镜像 |
| 是“散装 asset”还是“保留对象关系”？ | **保留了完整对象关系**：每个对象的 typetree JSON 里 `m_Component / m_Children / m_Father / m_Script / PPtr(m_FileID,m_PathID)` 原样存在 |
| 是否有 Editor-ready 的 `.unity / .prefab / .meta`？ | **完全没有**。全仓 `.unity=0 .prefab=0 .meta=0 .asset=0 .mat=0 .controller=0 .anim=0 .spriteatlas=0 .shader=0`，Unity YAML 文件 **0 个**（A 类 = 0） |
| Scene 数据是否存在？ | **存在**：唯一内建场景 `Assets/#WitchTrials/Scenes/Boot.unity`（= `level0`，18,804 B，95 个对象 dump） |
| GameObject hierarchy 是否可恢复？ | **可以，且已用脚本重建验证**：Boot.unity 33 GO/33 Transform/10 根/深度 6；156 个 prefab 中 **153 个层级零缺陷** |
| serialized references 是否可恢复？ | **98.8% 可解析**（非空引用 70,524 条中 69,666 条可解析到具体对象/类） |
| Addressables 是否可恢复？ | **可以**：28,332 个 key 全部能映射到 bundle+对象；`catalog.bin` 原文件仍在 |
| AssetBundle 依赖关系是否存在？ | **存在**：95 条依赖边、9 个目标 bundle，100% 可解析 |
| `.meta/GUID` 缺失影响 | 无法还原“原 GUID 同一性”，但可用自生成 GUID + pathID 图重映射；另有 20 个原始 GUID 残留在 `AssetReference` 字段里 |
| **结论等级** | **YELLOW（偏上，接近 GREEN）**：“对象关系基本完整、可高度自动转换”，但必须自研转换器 + 补 4 个不可得的第三方包，不能一键出可运行工程 |

一句话：**资源不是“只剩散装贴图/音频”，Scene/Prefab/组件挂接/引用图基本都在；缺的是“Editor 表示层”（YAML/.meta/GUID）和几个第三方运行时包。**

---

## 1. 调查范围、方法与可复现证据

### 1.1 扫描范围
| 根 | 内容 |
|---|---|
| `E:\Manosaba-VSCodeReady`（主 checkout） | `res/` 11 GiB / `src/` 196 MiB / `project/` 45 MiB / `work/` 97 MiB / `dist/` |
| 游戏安装目录 `manosaba_Data` | 457 `.bundle`（6.5 GiB）、`level0`、`globalgamemanagers(.assets)`、`resources.assets`、`sharedassets0.assets`、`.resS` 流、`Resources/unity default resources`、`Resources/unity_builtin_extra` |
| `E:\Manosaba-DS`（worktree） | 仅脚本/文档 |

### 1.2 方法（全部可复现，见 `tools/audit/README.md`）
1. **不只看扩展名**：`a01_inventory.py` 对每个文件读前 64 B 做 magic 嗅探（`UnityFS` / `%YAML 1.1` / `AddressablesCatalog 0x0DE38942` / `OggS` / `\x89PNG` / `OTTO` / `MZ` …）。
2. `a03_sources.py` 用 UnityPy 对**全部 457 bundle + 5 个 Player 数据文件**逐个独立子进程扫描（防 >300 MiB bundle OOM），记录每个 SerializedFile 的 `m_Externals` 表、内嵌 TypeTree 数量、AssetBundle `m_Container/m_Dependencies`、对象类型直方图。
3. `a04_refs.py` 正则提取全部 28,445 个 dump JSON 里的 `{m_FileID,m_PathID}`，按 fileID 0 / ≥1 分类并逐条解析。
4. `a06/a08` 从 dump JSON 直接**重建** GameObject/Transform 树并做父子回链、组件挂接校验。
5. `a07_addressables.py` 合并 `catalog.bin` 头 + 每个 bundle 的 manifest，重建 key→bundle→依赖。

### 1.3 关键机制发现（决定本次判定）
* **Addressables bundle 内嵌完整 TypeTree**：`SerializedFile._enable_type_tree = True`，9/9 类型都有 `node`，MonoBehaviour 的派生字段名（`_items/_taggedText/_locale/...`）都在 → 字段名/字段值可直接读出。
* **Player 数据文件（`level0`/`resources.assets`/`globalgamemanagers.assets`/`sharedassets0.assets`）没有 TypeTree**：`_enable_type_tree = False`，这些文件里的 MonoBehaviour 只有 32 B 基类头 + 剩余裸字节。
* **两套 MonoScript 存储**：bundle 内的 `m_Script` 指向 `bc78c2a5..._monoscripts.bundle`（211 个 MonoScript，pathID 与 `globalgamemanagers.assets` 的 3,045 个**完全不重叠**，但 211/211 的 `assembly|namespace|class` 身份在后者中存在）；`level0/resources.assets` 的 `m_Script` 则直接指向 `globalgamemanagers.assets`（pathID 可直接查 `res/_monoscripts.csv`）。

---

## 2. 当前资源结构概览

```
res/                                      11 GiB / 58,750 files
├─ Assets/                                 ← 镜像 Addressables container path（6,500 json + 26,537 ogg + 3,221 png + 448 txt + 4 m4v + 字体）
│  ├─ #WitchTrials/
│  │  ├─ Audio/{BGM,SFX,Voice}/...         26,537 个 .ogg（AudioClip 转码）
│  │  ├─ Data/ScriptableObjects/*.asset.json   37 个 SO 容器（AuthorData/CharacterData/ClueData/MapData/...）
│  │  ├─ Fonts/  Localization/  ManagedText/  Movies/
│  │  ├─ Prefabs/General/{Core,Debug,PerspectiveStage,UI}      ← 156 个 .prefab 容器（JSON 展开）
│  │  ├─ Prefabs/Naninovel/{Characters,Spawn,UI}
│  │  ├─ Scenarios/{Act01_*,Act02_*,Common,Debug,System}      440 个 .nani + 448 .txt
│  │  ├─ SpriteAtlases/  Textures/
│  ├─ GigaCreation/NaninovelExtender/...
│  ├─ TextMesh Pro/Shaders/TMP_SDF-Mobile.json
│  └─ Packages/com.elringus.naninovel/Prefabs/{DefaultUI,FX}/...
├─ Uncategorized/<Type>/                   ← 无 container 路径的对象（GameObject 559 / Transform / CanvasRenderer / MonoScript / Camera / ...）
├─ _serialized/<mirrored path>/*.json      ← 结构化对象（GameObject/Transform/RectTransform/CanvasRenderer/Canvas/...）17,290 个
├─ _index.csv        58,748 行  type,name,asset_path,output_file,source_bundle,path_id
├─ _index_audio.csv  26,538 行
└─ _monoscripts.csv   2,869 条  class,namespace,assembly,path_id,out
```

要点：
* `assets_path` 列 = **Addressables container path**（即 addressable key），`source_bundle` 列 = 该对象来自哪个 `.bundle`（或 `level0`/`resources.assets` 等），`path_id` 列 = **Unity PathID（fileID）**。这三列把 dump 重新变成了可查询的对象表。
* `res/Assets/**` 里的 `X.prefab` 目录下是 `X.json / X~1.json / X~2.json ...`，**一个 prefab 的所有子对象被平铺成多个 JSON**（Game.prefab = 251 个 JSON），不是单个 prefab 文件。
* `_serialized/**` 放的是不进入镜像路径的结构对象（GameObject/Transform/…），仍然可以通过 `_index.csv` 反查 `source_bundle + path_id`。

---

## 3. 文件类型统计（扩展名 + 内容嗅探）

### 3.1 `res/`（被审计资源本体）
| 扩展名 | 数量 | 说明 |
|---|---:|---|
| `.json` | 28,445 | typetree dump（对象本体/材质/动画/Shader/…） |
| `.ogg` | 26,537 | AudioClip → OGG Vorbis |
| `.png` | 3,302 | Texture2D / Sprite |
| `.txt` | 454 | TextAsset（Naninovel 剧本/ManagedText/本地化） |
| `.m4v` | 4 | VideoClip |
| `.otf`/`.ttf` | 3/1 | Font |
| `.csv` | 3 | `_index.csv` / `_index_audio.csv` / `_monoscripts.csv` |

### 3.2 全仓（repo + 游戏目录，76,488 文件）按内容嗅探
| 内容类型 | 数量 | | 内容类型 | 数量 |
|---|---:|---|---|---:|
| json/text | 28,666 | | AssetBundle(`UnityFS`) | **457** |
| ogg-audio | 26,537 | | binary | 359 |
| text（含 `.cs`） | 15,923 | | zip | 49 |
| png | 3,303 | | mp4 | 4 |
| PE-binary（dll/exe） | 1,186 | | font | 3 |
| **AddressablesCatalog(0x0DE38942)** | **1** | | **UnityYAML** | **0** |

### 3.3 Unity 工程扩展名普查（决定性证据）
| 扩展名 | 数量 | 扩展名 | 数量 |
|---|---:|---|---:|
| `.unity` | **0** | `.controller` | **0** |
| `.prefab` | **0** | `.anim` / `.overrideController` | **0** |
| `.meta` | **0** | `.playable` / `.timeline` | **0** |
| `.asset` | **0** | `.spriteatlas` | **0** |
| `.mat` | **0** | `.shader` / `.shadergraph` | **0** |
| `.asmdef` | **0** | `.bundle`（仅游戏目录） | 457 |
| `.cs`（src/ 反编译） | 12,185 | `.dll` | 1,122 |

> 即：**没有任何文件以 `%YAML 1.1`+`tag:unity3d.com,2011` 开头**。所有“资产”都是 JSON dump 或裸二进制。

### 3.4 `_index.csv` 类型直方图（Top）
| 类型 | 数量 | 类型 | 数量 |
|---|---:|---|---:|
| AudioClip | 26,538 | CanvasRenderer | 1,628 |
| GameObject | 6,978 | Texture2D | 906 |
| MonoBehaviour | 5,903 | TextAsset | 454 |
| Transform | 4,393 | Animator | 345 |
| MonoScript | 3,045 | AnimationClip | 193 |
| RectTransform | 2,584 | PlayableDirector | 188 |
| Sprite | 2,396 | Material | 155 |
| SpriteRenderer | 2,069 | Shader / SpriteAtlas / Mesh… | 133 / 17 / … |

共 43 种类型、461 个来源文件、28,339 个不同 container path。

### 3.5 dump 完整性：60,144 个源对象 vs 58,718 个已 dump

用 UnityPy 直接枚举 462 个来源文件得到 **60,144** 个对象，`_index.csv` 覆盖 **58,718** 个 = **97.63%**。差值 1,426 个全部可解释：

| 缺失 | 数量 | 原因 | 可否找回 |
|---|---:|---|---|
| AssetBundle 元对象 | 457 | 抽取脚本显式 `SKIP` | 不必要（可重新扫描） |
| MonoBehaviour（`resources.assets` 648 / `level0` 47 / `sharedassets0.assets` 8 / `globalgamemanagers.assets` 27） | 730 | 这些文件 `_enable_type_tree=False`，UnityPy 读 typetree 抛异常被 `continue` | **可以**：裸数据仍在 Player 文件里，需 IL2CPP 解码器 |
| MonoScript（`monoscripts.bundle`） | 211 | 该 bundle 被抽取阶段跳过 | **可以/已冗余**：`globalgamemanagers.assets` 有同一批类的 3,045 条 MonoScript |
| Player 管理器对象（PlayerSettings/InputManager/…） | 19 | 非资产类型 | 不必要（工程设置需另行重建） |
| 其它（RenderTexture 2、Texture2D 5、Sprite 1、AudioClip 1 等） | ~9 | 读取失败/被跳过 | 可重导 |

其余对象（GameObject 6,970、Transform 4,386、RectTransform 2,584、CanvasRenderer 1,628、MonoBehaviour 5,888…）**一个不少**。
`source_bundle` 维度：磁盘上 457 个 bundle 有 456 个进入 index（唯一缺席的是 12.5 KB 的 `monoscripts.bundle`，且已冗余）。


---

## 4. Scene 数据是否存在

**结论：存在，且是“结构完整 + MonoBehaviour 裸数据完整”的 B 类。**

| 证据 | 内容 |
|---|---|
| 场景名 | `globalgamemanagers` 的 `BuildSettings` 字符串直接给出唯一场景：`Assets/#WitchTrials/Scenes/Boot.unity`（`find` 全 Data 目录无 `level1+`） |
| 场景文件 | `manosaba_Data/level0`（18,804 B，SerializedFile v22，Unity 6000.0.48f1） |
| dump 覆盖 | `res/_index.csv` 中 `source_bundle = level0` 共 **95 行**：GameObject 33/33、Transform 13/13、RectTransform 20/20、CanvasRenderer 12/12、Canvas 4/4、CanvasGroup 4/4、Camera 3/3、**MonoBehaviour 3/50**、RenderSettings/LightmapSettings/NavMeshSettings 各 1 |
| externals | `level0.m_Externals = [globalgamemanagers.assets, sharedassets0.assets, Library/unity default resources]` → 全部可在本机解析（`globalgamemanagers.assets`、`sharedassets0.assets` 是磁盘文件；`Library/unity default resources` 对应 `manosaba_Data/Resources/unity default resources`，5.86 MB，**也在**） |
| 层级重建 | `tools/audit/a06_rebuild.py` 从 95 个 JSON 重建出 **10 个根、深度 6、33 个 GameObject/Transform 全部连通**，唯一缺陷 = `component_missing_from_dump: 47`（即 47 个未 dump 的 MonoBehaviour） |
| 根对象 | `SplashScreen / LetterBox / ForefrontOverlay / PerspectiveStage / GameInitializer / SteamManager / Presenters / Debug / EventSystem / MainCamera` |

重建片段（工具原样输出，未人工修饰）：

```
- SplashScreen  [Canvas CanvasGroup MISSING RectTransform]
  - Background  [CanvasRenderer MISSING RectTransform]
  - Wrapper  [RectTransform]
    - BrandLogo  [CanvasRenderer MISSING RectTransform]
    - CompanyLogo  [CanvasRenderer MISSING RectTransform]
- PerspectiveStage  [MISSING Transform]
  - ScreenCanvas  [Canvas MISSING RectTransform]
    - Wrapper  [RectTransform]
      - StageScreenContainer_1 [CanvasGroup MISSING RectTransform]
        - Mask [CanvasRenderer MISSING RectTransform]
          - RenderTexture [CanvasRenderer MISSING RectTransform]
- MainCamera [Camera MISSING Transform]
```

**为什么 47/50 个 MonoBehaviour 没进 dump（重要）**：
`level0._enable_type_tree = False`。UnityPy 对无 TypeTree 的 MonoBehaviour 无法解析，抛
`ValueError: Expected to read 136 bytes, but only read 32 bytes` → 抽取脚本 `continue`，只记日志不落盘。
实测这 47 个对象的**裸数据本身在 `level0` 里是完整的**（52/136/312/64/168 B…），
其中 32 B 是可手工解析的基类头：`m_GameObject(12) + m_Enabled(4) + m_Script(12) + m_Name(4)`，
`m_Script.m_FileID = 1` 指向 `globalgamemanagers.assets`，其 `m_PathID` 能直接在 `res/_monoscripts.csv` 里查到类名：

| level0 pathID | m_Script.pathID | 解析出的类 | 对象大小 | 自定义字段字节 |
|---|---:|---|---:|---:|
| 93 | 1470 | `UnityEngine.UI|UnityEngine.EventSystems|EventSystem` | 52 | 20 |
| 96 | 974 | `UnityEngine.UI|Button` | 312 | 280 |
| 98 | 762 | `Assembly-CSharp|WitchTrials.Views|StageCameraController` | 60 | 28 |
| 101 | 2948 | `Assembly-CSharp|WitchTrials.Views|SplashScreen` | 64 | 32 |
| 116 | 1504 | `Assembly-CSharp|WitchTrials.Views|GameInitializer` | 120 | 88 |

**字段值可解性抽样验证**（用 `src/il2cpp/dump.cs` 的字段声明与偏移做核对）：

| 类 | 序列化字段（dump.cs） | 裸数据尾长 | 是否吻合 |
|---|---|---:|---|
| `SplashScreen` | `PPtr _canvasGroup; PPtr _wholeScreenButton; float _fadeDuration; float _displayDuration` | 32 | ✅ 12+12+4+4 = 32 |
| `StageCameraController` | `StageCameraContainer[] _cameraContainers` | 28 | ✅ 4(size)+2×12(PPtr) = 28 |
| `GameInitializer` | `AssetReferenceGameObject _gamePrefab; SteamManager _steamManager; SplashScreen _splashScreen; string _gamePrefabInstanceName` | 88 | ✅ 结构可容纳（12+12+12+4+len 对齐） |

→ **Scene 是“B 类：可机械转换”**：层级/组件挂接 100% 在位；47 个 MonoBehaviour 的**字段值需要写一个 IL2CPP-offset 解码器**（原始字节在 `level0`，字段布局在 `dump.cs`），不是数据丢失。逐字段的 100% 精确性本次**未全部验证（UNKNOWN，见 §12）**。

---

## 5. Prefab 数据是否存在

**结论：存在（B 类）。156 个 `.prefab` 容器，全部可枚举、可重建层级；但没有原生 `.prefab` 文件，也没有 nested-prefab 元数据。**

| 指标 | 值 | 证据 |
|---|---:|---|
| `.prefab` container | 156 | `out/addressables.json`、`_index.csv` 的 `asset_path` 后缀统计 |
| 每 prefab dump 对象数 | 中位数 1（多为单对象 prefab），最大 3,189（`CreditsUI.prefab`） | `out/index_summary.json` |
| 全量层级校验 | **153/156 零缺陷**；3 个各缺 1 个组件引用；5 个多根 | `tools/audit/a08_prefab_sweep.py` → `out/prefab_sweep.json` |
| 代表：`Game.prefab` | 46 GameObject / 46 Transform(44 RT+2 T) / 102 MonoBehaviour / 36 CanvasRenderer / 16 CanvasGroup / 2 Canvas，1 根、深度 7、**issues = {}** | `out/hierarchy_Game_prefab.json` |

最大的 5 个 prefab（按 dump 对象数）：

| prefab | 对象 |
|---|---:|
| `Assets/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/Credits/CreditsUI.prefab` | 3,189 |
| `.../CustomUI/WitchBook/WitchBookUI.prefab` | 2,095 |
| `.../CustomUI/OptionsUI.prefab` | 1,219 |
| `.../Characters/LayeredCharacters/Ema.prefab` | 685 |
| `.../Characters/LayeredCharacters/Meruru.prefab` | 666 |

**明确缺失（不是 UNKNOWN，是确定丢失）**：
* 全 dump 中 `m_PrefabInstance` / `m_PrefabAsset` / `m_CorrespondingSourceObject` 字段出现次数 = **0**。
  原因：Player/AssetBundle 构建会把 nested prefab **烘焙（flatten）**成单一对象图，
  因此“这个子物体来自哪个子 prefab”的**嵌套来源关系不可恢复**；但“合并后的层级与组件”完整。
* 5 个 prefab 有多个根 Transform：dump 里没有 Prefab 根对象可供判断“真正的 prefab 根”，需要人工决定（见 §12）。

---

## 6. GameObject hierarchy 是否可恢复

**结论：可以，且已用脚本做端到端重建验证。**

数据来源（完全机械）：
* `GameObject.m_Component[].component.{m_FileID,m_PathID}` → 组件挂接
* `Transform/RectTransform.m_GameObject / m_Children / m_Father` → 父子关系
* `MonoBehaviour.m_GameObject` → 反向挂接
* `_index.csv.(source_bundle, path_id) → output_file` → 把 PathID 解析成 JSON 文件

| 范围 | GameObject | Transform | 根 | 深度 | 缺陷 |
|---|---:|---:|---:|---:|---|
| `Boot.unity`（level0） | 33 | 33 | 10 | 6 | 47 个 MonoBehaviour 引用来自未 dump 对象 |
| `Game.prefab` | 46 | 46 | 1 | 7 | **0** |
| `resources.assets` | 526 | 526 | 32 | 6 | 615（全部是未 dump 的 MonoBehaviour） |
| 全部 156 prefab | — | — | — | — | 153 clean / 3 各缺 1 组件 / 5 多根 |

校验项（`a06/a08`）：孩子表↔`m_Father` 回链一致、组件列表可达、组件回指 GameObject、环检测、可达性。
**结果：156 个 prefab 中 0 个出现 orphan transform / father mismatch / cycle。**

---

## 7. serialized references 是否可恢复

对 28,445 个 dump JSON 提取 **92,585 条 PPtr**（`{m_FileID,m_PathID}`），逐条解析：

| 分类 | 条数 | 说明 |
|---|---:|---|
| `m_PathID = 0`（空引用） | 22,061 | 合法 null（如 ScriptableObject 的 `m_GameObject`），**不是缺失** |
| intra-file（`m_FileID=0`）解析成功 | 61,148 | 52,897 普通对象 + 8,251 指向 MonoBehaviour |
| 外部引用 → MonoScript 表命中 | 6,113 | `m_FileID=1` → `monoscripts.bundle` 的 211 条 pathID 表（99.7% 的组件因此能确定类名） |
| 外部引用 → 其它 bundle 已 dump 对象 | 2,405 | 见下 |
| 外部引用 → `Library/unity default resources` | 60 | 内置资源，本机有对应 Player 资源文件，可重定向 |
| **intra-file 解析失败** | **730** | 全部是未 dump 的 MonoBehaviour（616 `resources.assets` + 47 `level0` + 59 recovered 行 + 8 零星） |
| `m_FileID` 越界（指向不存在的 external） | 68 | 待查（UNKNOWN），占比 0.07% |

> **非空引用可解析率 = 69,666 / 70,524 = 98.78%。**

外部引用的目标解析（fileID 2/3/4/5… → `m_Externals[fileID-1]`）：

| 目标（CAB） | 引用数 | 目标 bundle |
|---|---:|---|
| `CAB-453800d5…` | 6,113 | `bc78c2a5…_monoscripts.bundle` |
| `CAB-750d14ba…` | 1,396 | `general-fonts-tsukushimincho_assets_all.bundle` |
| `CAB-bb2ab16d…` | 905 | `general-sprites_assets_all.bundle` |
| `CAB-e4b5aa33…` / `CAB-3ba62fb0…` / … | 38 / 28 / … | 其它 general-* bundle |
| `Library/unity default resources` | 60 | 内置（Player 侧有 `Resources/unity default resources`） |

**结论：引用图基本完整**——`m_Script → 类名`、`组件 → GameObject`、`Transform → 父子`、`SO → 子对象/数组元素` 都可重建；
唯一系统缺口是 §4 的 730 个无 TypeTree MonoBehaviour（对象本身在原始文件里，字段值需解码器）。

---

## 8. MonoBehaviour 序列化字段（结合 reconstructed C# 类型）

`tools/audit/a05_monobehaviour.py` 对 **5,903 个 MonoBehaviour dump** 逐个解析：

| 指标 | 值 |
|---|---:|
| dump 的 MonoBehaviour 实例 | 5,903 |
| 能确定 `assembly|namespace|class`（经 MonoScript 表） | **5,885（99.7%）** |
| 含基类之外的**已解码命名字段** | **5,616（95.1%）** |
| 不同实例化的类 | **211**（= `monoscripts.bundle` 的 211 个 MonoScript，身份 100% 对得上） |
| 未定类 | 18（30 个 `(recovered)` 行 + 8 个 `adopt_orphans` 误判文件） |

实例数 Top（完整表 `out/mb_classes.csv`）：

| 实例 | 类 | 字段数 |
|---:|---|---:|
| 1,085 | `Elringus.Naninovel.Runtime|Naninovel|LayeredActorLayer` | 2 |
| 634 | `UnityEngine.UI|Image` | 16 |
| 533 | `TMPro.TextMeshProUGUI` | 69 |
| 440 | `Naninovel|Script` | 5 |
| 388 | `LeTai.TrueShadow|TrueShadow` | 20 |
| 343 | `UnityEngine.U2D.Animation|SpriteSkin` | 5 |
| 257 | `WitchTrials.Views|PauseOrUnpause` | 0 |
| 196 | `UnityEngine.Timeline|AnimationTrack` | 28 |
| 77 | `UnityEngine.Timeline|TimelineAsset` | 6 |
| 55 | `WitchTrials.Views|LocalizeImage` | 2 |

**字段值样例**（来自 `res/Assets/#WitchTrials/Data/ScriptableObjects/MapData.asset.json`，完整命名结构，非猜测）：

```json
{ "m_GameObject": {"m_FileID":0,"m_PathID":0}, "m_Enabled":1,
  "m_Script": {"m_FileID":1,"m_PathID":-6177371553458970899},
  "m_Name": "MapData",
  "_items": [ { "_id": "...", "_version": 0,
                "_item": { "_buttonText": [ {"_locale":0,"_text":"..."} ] } } ] }
```

**引用图重建可行性**：`TrialManager → UI → Camera → ScriptableObject → 其它组件` 所需的三类边
（`m_Script`→类、`m_GameObject`→GO、任意 PPtr→对象）都具备；唯一要补的是
**“PathID → 新 GUID / 新文件路径”映射表**（见 §11）。

按程序集统计的实例分布（决定“要装哪些包”）：

| 程序集 | 实例 | 类数 | 工程内是否已有源码 |
|---|---:|---:|---|
| Elringus.Naninovel.Runtime | 1,636 | 27 | ✅ `project/src/Elringus.Naninovel.Runtime`（付费插件，已反编译） |
| UnityEngine.UI | 1,418 | 18 | ❌ UPM `com.unity.ugui`（免费） |
| Assembly-CSharp | 755 | 90 | ✅ |
| Unity.TextMeshPro | 544 | 4 | ❌ UPM（Unity 6 内置） |
| LeTai.TrueShadow | 388 | 1 | ❌ **Asset Store 付费** |
| Unity.Timeline | 376 | 10 | ❌ UPM `com.unity.timeline` |
| Unity.2D.Animation.Runtime | 343 | 1 | ❌ UPM `com.unity.2d.animation` |
| GigaCreation.*（自研） | ~390 | ~40 | ✅ `project/src/GigaCreation.*` |
| UIPolygon / MPUIKit / URP / ppt-noise | 68 | 6 | ❌ 第三方/Asset Store |

---

## 9. Addressables 是否可恢复

**结论：可以。三层视图（catalog.bin / AssetBundle manifest / dump index）互相印证。**

| 证据 | 值 |
|---|---|
| `StreamingAssets/aa/settings.json` | `m_AddressablesVersion=2.4.6`，`m_buildTarget=StandaloneWindows64`，`m_SettingsHash=99250bc4457babf51d44b54188172317`，provider=`ContentCatalogProvider` |
| `aa/catalog.bin` | **5,551,003 B**，magic `0x0DE38942`（BinaryStorageBuffer **v2**），types offset/size = 36 / 457,736，90,588 个 ASCII 字符串 |
| catalog 内含 | provider 类名（`AssetBundleProvider`/`BundledAssetProvider`/`AtlasSpriteProvider`/`SceneProvider`/`InstanceProvider`）、173 个 `StandaloneWindows64` 运行时路径、**156 个 `.prefab`**、442 个 `.nani`、661 个 bundle 名 |
| bundle manifest | `AssetBundle.m_Container` 共 **29,053 条 entry → 28,332 个不同 key** |
| key 覆盖 | **28,332 / 28,332** 全部能在 `_index.csv` 找到已 dump 对象（`out/addressables.json → cross_check`） |
| 其它 | 457/457 bundle 都有 AssetBundle manifest；`aa/AddressablesLink/link.xml`（45 KB）为 Addressables 构建产物 |

映射链（已导出 `out/addressable_keys.csv.gz`，28,332 行）：

```
Addressable key (Assets/#WitchTrials/...)
  → source bundle (StreamingAssets\aa\StandaloneWindows64\*.bundle)
  → CAB SerializedFile
  → 对象 PathID (_index.csv)
  → 依赖 bundle 列表 (AssetBundle.m_Dependencies)
```

**未做 / UNKNOWN**：`catalog.bin` 的**完整反序列化**（BinaryStorageBuffer 类型表 + 前缀压缩字符串表 + entries/keyData）
本次只做到“文件头 + 字符串证据 + 与 manifest 交叉验证”。若需要 catalog-only 的 **label / group / CRC / 远程依赖**，
需补一个 parser（信息在文件里，但未验证可完整还原）。对“跑起来”不影响：运行时用的 key+bundle 都在。

---

## 10. AssetBundle 依赖关系

* 457 个 bundle：169 个在 `StandaloneWindows64/` 顶层，288 个在子目录
  （`naninovel-backgrounds_assets_naninovel/backgrounds/{mainbackground 62, stills 172, tricks 19}`、
  `naninovel-characters_assets_naninovel/characters 31`、`…/movies` 等）。
* **68 个 bundle 声明了依赖**，共 **95 条边、9 个不同目标**；`CAB-xxxx` 哈希 100% 能对回实际 bundle 文件。

| 依赖目标（CAB） | 被引用 | 目标 bundle |
|---|---:|---|
| `cab-453800d550384885247ea13fa1524373` | 68 | `bc78c2a5…_monoscripts.bundle` |
| `cab-750d14ba14462fdcb2b6452c4f614bf6` | 8 | `general-fonts-tsukushimincho_assets_all.bundle` |
| `cab-bb2ab16d2d20f975407bb805d65cfd25` | 6 | `general-sprites_assets_all.bundle` |
| `cab-e4b5aa33913270dee53bf9edeee84127` | 6 | `general-data_assets_all.bundle` |
| 其余 5 个 | 1–3 各 | 其它 general-* bundle |

* dump 侧同样自洽：bundle A 的 `m_Externals[n].path = archive:/CAB-X/CAB-X`，
  与 `CAB-X → bundle B` 的映射一致（`a03_worker.py` 扫描每个 bundle 内层 SerializedFile 名建立 CAB→bundle 表）。
* 5 个 Player 数据文件没有 AssetBundle 对象，但互相依赖在各自 `m_Externals` 中完整记录：
  `level0 → globalgamemanagers.assets / sharedassets0.assets / Library/unity default resources`。

---

## 11. `.meta` / GUID 缺失的实际影响

**事实（文件级证据）**
* `res/` + `project/` + `src/` + `work/` + 游戏目录：`.meta` 文件 **0 个**（`out/inventory_summary.json`）。
* Unity YAML 文件 **0 个**。
* bundle 的 `m_Externals[].guid` **全为 0**（462 个来源文件里只有 5 条非零 guid）；
  即 AssetBundle 构建剥掉了依赖资产的原 GUID。
* 唯一的 GUID 残留：`AssetReference*` 字段里序列化的字符串——dump 中找到 **20 个不同 GUID、90 处出现**（180 个 JSON 文件），
  例：`res/Assets/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/LogUI~11.asset.json` 的
  `m_AssetGUID = 519e9e2fa7c404c4689c3f073450d8e5`。
* `boot.config` 里有 `build-guid = 95348ca61c0145b5be2271fe01725089`（Player 构建 GUID，不是资产 GUID）。

**影响分级**

| 影响 | 是否致命 | 说明 |
|---|---|---|
| 不能直接 `git clone` 出 Unity 工程打开 | 是（流程性） | 没有 `.meta`，Unity 会把所有资产当成新导入 |
| 原有 GUID↔资产的同一性 | **不可还原** | 任何依赖“原 GUID”的外部工具/存档都会失配；编辑器内部引用可用自生成 GUID 替代 |
| 资产之间的引用（Scene/Prefab/SO 内部） | **不受影响** | 引用是 `m_FileID + m_PathID`，都在 dump 的引用图里（§7） |
| `AssetReference` 的 Addressables key | **部分可还原** | 20 个 GUID 直接以字符串存在于字段值里 |
| 脚本引用（MonoScript） | 不受影响 | `m_Script` → 211/3,045 张 pathID→类名表 |
| Shader/Material 的 built-in 引用 | 可重定向 | `Library/unity default resources` 在本机 Player 侧存在 |

**结论**：`.meta/GUID` 缺失是**“表示层”缺口，不是“数据层”缺口**。
做法是：导入时给每个重建资产分配**全新 GUID**，并按 `(source_bundle, pathID)` → 新 GUID 的映射重写引用。
只要保证映射是单射且全量覆盖，工程内部引用就完全自洽；无法恢复的只是“与外部世界的原始 GUID 等价性”。

---

## 12. 自动重建 / 人工重建 / 无法判断

### 12.1 可以（高置信度）自动重建 —— `tools/audit` 已给出可运行证明

| # | 内容 | 依据 |
|---|---|---|
| 1 | **156 个 prefab 的 GameObject/Transform 树 + 组件挂接** | `a08_prefab_sweep.py`：153/156 零缺陷，无 orphan/回链错误 |
| 2 | **Boot.unity 场景对象图**（33 GO / 33 Transform / 10 根 / 深度 6） | `a06_rebuild.py` → `out/hierarchy_Boot_unity.json` |
| 3 | **5,885/5,903 个 MonoBehaviour 的类归属** | MonoScript 双表（211 + 3,045）→ `out/mb_classes.csv` |
| 4 | **5,616 个 MonoBehaviour 的命名字段与字段值** | bundle 内嵌 TypeTree，JSON 已是具名字段 |
| 5 | **Addressables key → asset → bundle → 依赖** | 28,332 key 全覆盖 + 95 条依赖边 |
| 6 | **媒体资产本体**：26,537 ogg / 3,302 png / 4 m4v / 3+1 字体 / 454 txt | 已是标准可导入格式 |
| 7 | **ScriptableObject 内容**（37 个 `.asset` 容器 + 场景资源） | typetree JSON 具名+具值 |
| 8 | **对象表**：`(source_bundle, pathID) → 文件 → container` | `_index.csv` 58,748 行 |

### 12.2 需要“机械但须自研工具”的重建（工作量中等）

| # | 内容 | 缺口 | 建议做法 |
|---|---|---|---|
| 1 | **typetree JSON → Unity YAML / `.prefab` / `.asset` / `.mat` / `.controller` / `.anim` / `.spriteatlas` / `.shader`** | dump 是 JSON，Unity 只认 YAML | 写 writer（用 `_index.csv.type` 找 classID、用 JSON 字段序写 YAML）；或补充 `AssetRipper` 重导 |
| 2 | **GUID 生成 + 引用重写** | 无 `.meta` | 建 `(bundle,pathID) → 新GUID` 映射表，全量重写 PPtr |
| 3 | **730 个无 TypeTree 的 MonoBehaviour 字段值**（648 `resources.assets` + 47 `level0` + 8 `sharedassets0.assets` + 27 `globalgamemanagers.assets`） | Player 文件无 TypeTree | 用 `src/il2cpp/dump.cs` 字段声明 + 偏移写 IL2CPP 解码器（抽样已验证尺寸吻合） |
| 4 | **Unity 内建类重建**（SpriteRenderer 2,069 / CanvasRenderer 1,628 / Canvas 119 / Camera 17 / Animator 345 / ParticleSystem 35 / MeshFilter 35 …） | 同样是 JSON | 同上 writer，字段直接映射到 Unity 组件 |
| 5 | **音频格式** | 已转 OGG | Unity 可直接导入；若要还原原始 `.wav`/压缩设置需查原始 bundle 的 AudioClip typetree |

### 12.3 需要人工/外部资源（自动工具无法补齐）

| # | 内容 | 原因 |
|---|---|---|
| 1 | **第三方运行时包**：`LeTai.TrueShadow`(388 实例)、`MPUIKit`(22)、`UIPolygon`(30)、`ppt-noise`(1)、`LitMotion`、`UniRx`、`UniTask`、`DOTween`、`Steamworks.NET` | 未随游戏分发原始包；必须获取同版本或重写 |
| 2 | **Unity 官方包按 6000.0.48f1 版本对齐**：ugui(1,418)、TMP(544)、Timeline(376)、2D Animation(343)、URP(16)、Addressables(2.4.6)、Burst/Mathematics/InputSystem… | `ScriptingAssemblies.json` 列出 165 个程序集，必须逐一配齐（UPM 免费但版本要对） |
| 3 | **Naninovel（付费插件）编辑器侧** | 只有 runtime 反编译源码；Naninovel 的 Editor/生成器/默认 UI 资源需另行获取（`res/Assets/Packages/com.elringus.naninovel/**` 里只有 prefab dump） |
| 4 | **nested prefab 的嵌套来源关系** | Player 构建已 flatten（`m_PrefabInstance/m_PrefabAsset` 全 dump = 0），无损合并图在，来源不可考 |
| 5 | **5 个多根 prefab 的“正确根”** | 数据里没有 Prefab 资产对象，需要人按语义决定 |
| 6 | **ProjectSettings / Packages/manifest.json / Quality / Graphics / Input 设置** | 只有 `globalgamemanagers` 的 `PlayerSettings/BuildSettings/QualitySettings/GraphicsSettings/InputManager/TagManager` 二进制，可读但要重建为 YAML 工程设置（且 URP 资产缺失） |
| 7 | **场景/prefab 中缺失的 3 个组件引用**（`ProvidableDialog.prefab`、`DebugLabel.prefab`、`RebuildableToggleLabel.prefab` 各 1） | dump 时对象读取失败；原始 bundle 里还在，可重导 |

### 12.4 目前无法判断（UNKNOWN，禁止猜）

| # | 项 | 为什么 UNKNOWN |
|---|---|---|
| 1 | `catalog.bin` 的 label/group/CRC/远程依赖能否 100% 还原 | 未写 BinaryStorageBuffer 完整 parser（信息在文件里） |
| 2 | 730 个无 TypeTree MonoBehaviour 的**逐字段**值是否 100% 与 `dump.cs` 布局吻合 | 只抽样核对了 `SplashScreen`(32B)/`StageCameraController`(28B) 等尺寸；未全量验证（尤其含多态/string/array/AssetReference 的类） |
| 3 | 现成 AssetRipper 版本能否吃下 Unity 6000.0.48f1 + IL2CPP DummyDll 直接出工程 | 本机未安装/未联网验证 |
| 4 | 5 个多根 prefab 的原始功能结构 | 数据无答案 |
| 5 | `resources.assets` 内 526 个 GameObject 的 MonoBehaviours 的**语义可用性**（Naninovel 默认 UI 等） | 未逐类核对 |
| 6 | Shader 的可用性：133 个 Shader 是 typetree JSON（`m_Script` 里是编译后字节码） | 未验证能否反编译回 `.shader` 源码；URP/TMP 的 shader 需要对应包提供 |
| 7 | `m_FileID` 越界的 68 条引用 | 未逐条溯源 |

### 12.5 判定分类（用户要求 A/B/C）

| 类别 | 判定 | 证据 |
|---|---|---|
| A：真正 Editor-ready 的 `.unity/.prefab` YAML | **0 个** | 全仓 UnityYAML 文件 = 0 |
| B：可机械转换回 Unity Editor 的结构化 dump | **全部对象层**（58,718 对象 / 92,585 引用 / 156 prefab / 1 场景） | §4–§8 |
| C：只有独立 asset 内容、关系已丢失 | **仅媒体本体**（ogg/png/m4v/字体/txt）；**但它们的宿主对象/挂接仍在 JSON 里** | §3、§6 |

---

## 13. 对 “Unity Editor → Play Mode” 的真实阻塞项排序

按“不解决就绝对无法进入 Play Mode / 解决成本”排序：

| # | 阻塞项 | 严重度 | 现状 | 证据 |
|---|---|---|---|---|
| 1 | **没有 Unity 工程骨架**：无 `ProjectSettings/`、无 `Packages/manifest.json`、无 `.meta`、无 `.unity/.prefab` | 🔴 阻断 | 需要以 6000.0.48f1 新建工程，导入重写后的资产 | 全仓 0 个 `.meta/.unity/.prefab`；`boot.config`/`app.info` 仅有版本与 build-guid |
| 2 | **资产是 typetree JSON，不是 Unity YAML** | 🔴 阻断 | 需要 JSON→YAML writer（或用 AssetRipper 重导） | `res/Assets/**` 全是 `.json`，156 个 prefab = 251/3189 个 JSON 分片 |
| 3 | **第三方包缺失**（TrueShadow 388 实例 / MPUIKit / UIPolygon / ppt-noise / LitMotion / UniRx / UniTask / DOTween / Steamworks.NET） | 🔴 阻断 | 付费或第三方，必须获得或替换 | `manosaba_Data/ScriptingAssemblies.json` 165 个程序集；`mb_classes.csv` |
| 4 | **730 个 MonoBehaviour 无 TypeTree**（`level0` 47 + `resources.assets` 648 + `sharedassets0` 8 + `ggm.assets` 27） | 🟠 高 | 裸字节在原始文件；需 IL2CPP 解码器；否则场景/默认 UI 上的脚本组件为空 | §4 实测 `_enable_type_tree=False`；`Expected to read 136, read 32` |
| 5 | **脚本重新绑定**：`m_Script` → 类名 → 工程内新脚本 GUID | 🟠 高 | 211 个类有实例（双表可查），但需在导入后把 MonoScript 指回去 | `_monoscripts.csv` + `monoscripts.bundle` 211 条 |
| 6 | **Addressables 运行环境**：`catalog.bin` + bundle 需按 Addressables 2.4.6 组进工程 | 🟠 高 | key/bundle 图完整，但需要配置 `BuildRemoteCatalog`/`LoadPath` 等设置重建（settings.json 有原始值） | `aa/settings.json`、`out/addressables.json` |
| 7 | **URP / Graphics 设置与 Shader**：133 Shader 只有编译字节码 dump，URP 资产缺失 | 🟠 高 | 需按 URP 版本获取 Shader；`GraphicsSettings` 需重设 | `_index.csv` Shader=133；`ScriptingAssemblies.json` 含 URP 全套 |
| 8 | **Naninovel 包（付费）的编辑器侧** | 🟡 中 | runtime 已反编译可编译；但 Naninovel 的 Editor 工具/默认资源不可再生 | `project/src/Elringus.Naninovel.Runtime.csproj`；`res/Assets/Packages/com.elringus.naninovel/Prefabs/**` |
| 9 | **nested prefab 来源关系已 flatten** | 🟡 中 | 合并图可用，嵌套“来源”不可考；若玩法不依赖嵌套编辑可忽略 | 全 dump `m_PrefabInstance=0` |
| 10 | **5 个多根 prefab / 3 个缺失组件** | 🟢 低 | 局部人工修复 | `out/prefab_sweep.json` |
| 11 | `catalog.bin` 完整解析（label/CRC） | 🟢 低 | 只影响增量更新/远端校验，不影响本地 Play | §9 |

**可以完全跳过的项**：native method body（本次不在范围内）、`RECONSTRUCTION.md` 的 native 进度。

---

## 14. 结论等级

> 判定口径：**“现有资源能否机械地还原成可编译、可运行的 Unity Editor 工程”**

| 维度 | 等级 | 理由 |
|---|---|---|
| 对象关系保真度（Scene/Prefab/组件/引用） | **GREEN** | 98.8% 引用可解析；153/156 prefab 层级零缺陷；场景层级完整；组件类归属 99.7% |
| 资产本体保真度（贴图/音频/字体/视频/文本） | **GREEN** | 全部为标准格式，数量与容器一一对应（28,332 key 全覆盖） |
| Editor 表示层（YAML/.meta/GUID） | **ORANGE** | 完全没有，必须自研 writer + GUID 重映射 |
| 可运行性（第三方包/URP/Naninovel） | **ORANGE** | 5 个第三方包不可再生 + URP/Shader 缺失 |

### **总评：YELLOW（偏上，接近 GREEN）**

* 不是 ORANGE/RED：**没有“对象关系明显缺口”**——引用图、层级、字段值都在，且 `tools/audit` 已给出可执行的重建证明；
* 不是 GREEN：**没有任何 Editor 可读文件**，必须写转换器、生成 GUID、补齐第三方包；不是“一键可用”。

### 建议路线（不展开实现，仅排序）

1. 新建 Unity **6000.0.48f1** 空工程 + URP + Addressables 2.4.6 + TMP/Timeline/2D Animation/InputSystem。
2. 写 **dump→YAML writer**（或先试 AssetRipper 全量重导，用它生成 `.meta/GUID` 与新 YAML，把 `res/` 的 JSON 作为交叉校验源）。
3. 建 `(bundle, pathID) → 新 GUID/路径` 映射并重写引用；先做 `Game.prefab` + `Boot.unity` 两个端到端样本。
4. 写 **IL2CPP-offset MonoBehaviour 解码器**处理 730 个无 TypeTree 实例；优先 `SplashScreen / GameInitializer / PerspectiveStagePresenter` 等启动链。
5. 逐个补第三方包（TrueShadow/MPUIKit/UIPolygon/ppt-noise）；不可得的用等价组件替换并重挂。
6. 用 `Boot.unity → SplashScreen → GameInitializer → Game.prefab` 走通 Play Mode，再扩到全部 156 prefab。

---

## 附录 A：证据文件清单（`tools/audit/out/`）

| 文件 | 内容 | 本报告引用的数字 |
|---|---|---|
| `inventory_summary.json` | 全部 76,488 文件的扩展名/内容嗅探汇总 | §3 表格、`.meta=0`、UnityYAML=0 |
| `inventory_files.csv.gz` | 逐文件清单（root/relpath/ext/content_kind/size） | 可复查任意文件 |
| `index_summary.json` | `_index.csv` 类型/来源/容器统计 | 58,748 行、28,339 容器、156 prefab |
| `sources.json` | 457 bundle + 5 Player 文件的逐文件扫描（SerializedFile、externals、TypeTree、对象直方图、dump 覆盖） | §4/§6/§7/§10 |
| `bundles.json` | 457 个 AssetBundle 的 name/container/dependencies | §9/§10 |
| `a03_errors.json` | 扫描错误（本次为空） | 扫描完整性 |
| `ref_census.json` | 92,585 条 PPtr 的分类账 | §7 |
| `monobehaviour.json` / `mb_classes.csv` | MonoBehaviour 实例/类/字段普查（211 类） | §8 |
| `monoscript_table.json` | 3,045 + 211 条 pathID→类表 | §7/§8 |
| `hierarchy_Boot_unity.json` / `hierarchy_Game_prefab.json` / `hierarchy_report.json` | 场景/prefab 重建结果 | §4/§6 |
| `prefab_sweep.json` | 156 个 prefab 的层级校验汇总 | §5/§6 |
| `addressables.json` / `addressable_keys.csv.gz` | Addressables key→bundle→依赖（28,332 行） | §9/§10 |

未入库（可再生中间产物）：`out/a03_cache/`、`out/containers_by_kind.csv`。

## 附录 B：复现命令

```bat
set PY=E:\Manosaba-VSCodeReady\tools\venv\Scripts\python.exe
cd E:\Manosaba-DS\tools\audit
%PY% a01_inventory.py
%PY% a02_index_stats.py
%PY% a03_sources.py          :: ~8 分钟（457 bundle，独立子进程 + out/a03_cache 断点续跑）
%PY% a04_refs.py             :: ~3 分钟（28,445 JSON）
%PY% a05_monobehaviour.py
%PY% a06_rebuild.py
%PY% a07_addressables.py
%PY% a08_prefab_sweep.py
```

> 依赖：`UnityPy 1.25.3`（`E:\Manosaba-VSCodeReady\tools\venv`）。
> 注意：`res/`、`tools/` 在主 checkout 且被 git 忽略；worktree 内没有这些目录，脚本用绝对路径读取。

## 附录 C：本次审计的边界（未做的事）

* ❌ 未恢复任何 native method body；未改动 `RECONSTRUCTION.md`；未改动 `src/decompiled/**` 已确认的重建 C#（`git status` 仅新增本报告与 `tools/audit/**`）。
* ❌ 未猜测/手工编造任何缺失的 Scene 层级——所有层级均由 dump JSON 重建，缺陷以 `MISSING` 显式标出。
* ❌ 未用文件大小推断工作量；所有结论均落到具体文件/行/计数。
* ❌ 未修复 §12.2 的转换器（仅审计 + 提供辅助工具与验证脚本）。
