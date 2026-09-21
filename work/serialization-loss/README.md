# 序列化丢失审计与修复（2026-09-19）

范围：Boot → Naninovel → EntrySequence → TitleScript 跑起来之后暴露的「序列化恢复不完整」——`LocalizeImage._image` 为 null、
Naninovel 命令参数全空（`@processInput` 无参数、`AnimateActor.ActorIds == null`、`SpawnBlur.ActorId` 丢失导致 Blur 找默认
`MainBackground`）、`PlaybackSpot.scriptPath` 空串。

**结论：没有单一根因，而是重建流水线的三个独立缺陷**，分别由 `ScriptableObjectConverter`（A）和 `PrefabBuilder`（B、C）产生。
下面每个数字都由独立脚本（不信任 Unity 侧计数器）从原始 dump 和磁盘上的 `.asset`/`.prefab` 重新算出。

## A. 命令参数丢失：440 个剧本资产从未用修好的 applier 重新生成

* 旧 `JsonToSerializedApplier` 在 managed-reference token 没有 `rid` 时直接 `return`，把 `references.RefIds[].data` 整个丢弃；
  只建出了类型正确的**空**实例。`so_convert_result.json` 的「304,280 applied / 0 failed」是假阳性（只数实例，不数值）。
* commit `015c0cf` 修了 applier，但此后从未重跑 `ScriptableObjectConverter`。
* 修复（`ScriptableObjectConverter.cs` / `JsonToSerializedApplier.cs`）：
  * **原地更新**：目标已存在且类型相同 → 在其 `SerializedObject` 上 `ApplyAll`，不再 `DeleteAsset + CreateAsset`（后者换 .meta GUID，
    并对每个剧本触发 Naninovel `ScriptAssetProcessor.OnWillDeleteAsset` 的 `SaveAssets`）。转换前后比对 GUID，变了记 `GUID_CHANGED`。
  * **对账看内容**：`hasValue: true` 叶子的「dump 期望值」按 applier 的消费方式展开 `{rid}`（同一个 rid 在 `playlist` 和
    `lines[].command` 各用一次，所以期望值是 raw token 数的两倍）与「实际写入数」比对，不等即 `CONVERTED_PARTIAL`。
* 验证（`work/serialization-loss/verify_scenarios.py`，Python 独立重算）：

| | 修复前 | 修复后 |
|---|---|---|
| YAML 中 `hasValue: 1` 总数 | 0 | **849,954**（= dump 展开期望值，440/440 资产逐个相等） |
| `playbackSpot.scriptPath` 全空的剧本 | 440 | 0 |
| scenario `.meta` GUID 变化 | — | **0 / 440** |
| 第二遍（在已填充资产上原地重跑） | — | 同样 849,954 / 849,954，0 变化（幂等） |

## B. `_image` 丢失：`PrefabBuilder` 按计划顺序 `AddComponent`

* 计划 `prefab_plan_all.json` 的 `objects` 按 `(type, dump 文件名)` 排序，与 GameObject 的 `m_Component` 真实顺序无关。带
  `[RequireComponent(typeof(Image))]` 的 `LocalizeImage` 排在同 GO 的 `Image` 之前时，Unity 自动加了 `Image`；计划里的 `Image` 节点被
  `DisallowMultipleComponent` 拒绝，节点没有 component，`ResolvePointer` 返回 null → `_image` 留空。
* 修复（`PrefabBuilder.cs`）：组件按**该 GO 的 `m_Component` 顺序**创建，再按 `[RequireComponent]` 依赖把提供者提前（覆盖
  `Selectable → Button`、`Button → TweeningButton` 这类基类依赖）；仍被拒绝时认领同 GO 上**确切同类型、未被认领**的自动添加组件。
* 实测（`verify_prefabs.py`）：`_image` 非空 **63/127 → 127/127**；全量重建里 `components_adopted = 35`（认领路径确实被走到）。

## C. 跨资产 PPtr 0/1937 绑定，且 `m_*` 引擎字段被整体跳过

* `ResolvePointer` 用计划里的**出货路径** `Assets/#WitchTrials/…` 调 `LoadAssetAtPath`，而资产实际在 `Assets/GameContent/#WitchTrials/…`
  （多 sprite 贴图在 `<Name>.sprites/<Name>.png`，本地化变体的文件名是 `Name_Ja.png` 而文件夹保留 `Name@Ja.sprites`，字体在
  `GameContent/RuntimeFonts/<Family>/`）。1937 个外部引用一个都没绑上。
* `Options.SkipEngineFields = true` 跳过所有 `m_*`，所以 `Image.m_Sprite`、`m_Color`、TMP `m_fontAsset`、`Selectable.m_TargetGraphic` 从不写入。
* 修复：路径映射（4 个候选）；Sprite 指针取 `.png` 的 Sprite 子资产、组件类型指针（`ChoiceHandlerPanel.defaultButtonPrefab` 等）取 prefab
  根上（否则整个 prefab 里唯一的）该类型组件；`SkipEngineFields=false` + 只忽略 builder 自己拥有的头字段；`PreserveUnresolvedPointers`
  （不把默认非空的引擎字段，如 `SpriteRenderer.m_Materials[0]`，覆盖成 null）。
* 顺带修好的 applier 缺陷：**枚举按「索引」写入，而 dump 里是原始值**（`m_fontWeight: 400`、`m_textAlignment: 65535`、
  `Canvas.m_AdditionalShaderChannelsFlag`）——每个 UI prefab 集合 128 次「enum index 400 out of range」，非顺序枚举被丢弃或写成错误成员；
  现在直接写原始 `intValue`。另补 `Vector2Int/Vector3Int/RectInt/Bounds/Gradient`（标题界面的渐变特效）。
* 实测（全量 156 个 prefab）：0 failed / 0 incomplete / **0 GUID 变化**；`m_Sprite` 0/656 → 499/656（其余 78 见下）；
  TMP 非默认字体 0/542 → 526/542；探针 17 个 prefab 上 `_image` 109/109、sprite 407/407（dump 里非空的全部还原）。

## D. 同 bundle 内指针（`m_FileID == 0`）指向计划外对象：2,191 个经导入台账绑定（2026-09-21）

* `m_FileID == 0` 的指针目标不一定是 prefab 自己的 GameObject/组件：`GalleryStillGridSlot.lockedTexture/loadingTexture`、
  `RawImage.m_Texture`、`LayeredCharacters/*` 与 `Reenact_*` 的 `SpriteRenderer.m_Sprite` 都指向**同一 bundle** 里被导入器单独写成
  `.png` 的 Texture2D/Sprite。规划器 `a13e_plan_prefabs.py` 对 `fileId == 0` 一律 `continue`，builder 只能记 `not in plan`。
  G4b 第 2 轮的直接后果：`CGGalleryGridSlot.Awake` 的 `AssertRequiredObjects` 抛 27 次 `UnityException`。
* 修复：规划器读 `a13_import_ledger.csv`（`status == IMPORTED`，按 `(bundle, path_id)`，0 个键有多个目标）把这类指针写成
  `pid:0:pathId` 引用；`PrefabBuilder.ResolvePointer` 在 `target == null` 时查这张表并加载，之后 applier 的 `AdaptToFieldType`
  照常把 Texture2D 换成 Sprite 子资产。只有真正加载到资产才记 `pointers_sidecar`。
* 副作用与修复：每个这类指针都把一张贴图载入 Editor，batch 会话不会自动卸载；第一次全量在第 137/156 个 job
  （`Reenact_Ch01_E01_S01`，前面 55 个同形 job 都成功）于 `LoadAssetAtPath` 内原生崩溃，同一 job 单独构建成功。现在每个 job 结束
  `EditorUtility.UnloadUnusedAssetsImmediate()` + `GC.Collect()`，全量期间 Unity 常驻约 1.9 GB，无崩溃。
* 实测（全量 156，读回 YAML 核对）：

| | 修复前 | 修复后 |
|---|---|---|
| pointers bound | 8,595 | **10,786**（+2,191，全部经台账） |
| pointers unbound | 5,274 | 3,083 |
| internal，目标不在计划里 | 4,829 | 2,638 |
| GUID 变化 / failed / incomplete | 0 / 0 / 0 | 0 / 0 / 0 |
| `GalleryUI_GalleryStillSlot` 的 `lockedTexture`/`loadingTexture`/`m_Texture` | null | 分别指向 `GalleryUI.png`/`~1`/`~2` 的 GUID |
| `Ema.prefab` / `Reenact_Ch01_E01_S01.prefab` 的 `m_Sprite` 空指针 | — | 0/126、0/23 |

`align_asset_guids.py` dry run：`need remapping: 0`。

## E. 出货数据里的程序集限定类型名仍写 `Assembly-CSharp`（2026-09-21）

* 重建把 `Assembly-CSharp` 改名为 `WitchTrials.Game`（`apply_game_sources.py` 的 `REMAP`，避免与 Unity 预定义程序集冲突和程序集环）。
  applier 的 managed-reference 路径早已按 `Alias()` 改名，但**字符串字段**原样照抄出货值：
  * `BackgroundsConfiguration.ActorMetadata.Implementation`（4 处，`WitchTrialsBackground`）与 `TextPrintersConfiguration`（1 处，
    `DebateTextPrinter`）：`ActorManager.GetImplementationType` 按名字查类型失败，G4b 第 2 轮引擎停在 `ScriptPlaylist.LoadResources`
    （`'WitchTrials.Views.WitchTrialsBackground, Assembly-CSharp, ...' actor implementation type for 'IBackgroundActor' is not found`，
    `initialize_completion: STILL RUNNING after 60s`）。
  * UnityEvent `PersistentCall.m_TargetAssemblyTypeName`：8 个 UI prefab（TitleUI 3、OptionsUI 5、SaveLoadUI 5 …）共 21 处。
* 修复：`JsonToSerializedApplier.RemapAssemblyQualifiedName`——字符串**整体**是程序集限定类型名（含泛型参数）且程序集记号正好是
  `Assembly-CSharp` 时改成 `WitchTrials.Game`（`-firstpass`/`-Editor` 不动，其他字符串原样返回）。applier 的字符串分支与
  `NaninovelConfigBuilder` 的字符串分支都走它，并分别计数（`assembly_names_remapped`）/记入行 `notes`。
* 实测：`NaninovelConfigBuilder` 重跑后 25 个配置资产里只有这 5 个字符串与提交版不同（其余 23 个逐字节相同）；8 个 UI prefab 重建
  `assembly_names_remapped = 21`、GUID 变化 0，全工程 prefab 里 `Assembly-CSharp` 出现 0 次。
* 出货 `ProjectResources`（`unitycommon/projectresources`）里还有 3 个 `Type` 字符串，但它不是配置资产、不被导入（Naninovel 构建时自己生成）。

## 全量 156 个 prefab 之后**仍然**未绑定的（已分类，非静默）

| 类别 | 数量 | 说明 |
|---|---|---|
| internal，目标不在计划里 | 2,638（builder 实际遇到的；规划器全数 2,881） | 按目标类型（规划器 `ptr_intra_not_in_plan:<type>`）：Material 2,161、MonoBehaviour 418（TimelineAsset 等）、AnimationClip 206、Mesh 31、Camera 28、AnimatorController 18、其他 19。Material/AnimationClip/MonoBehaviour 在导入台账里都是 `DEFERRED_COMPOSITE`（shader 不可恢复、需要动画对象图、需要类型映射），**不是**本修复能绑定的；`PreserveUnresolvedPointers` 让 `m_Materials[0]` 保持 Sprites-Default。**不影响标题路径** |
| internal，目标建不出 | 340 | 30 个 `UiPolygon.PolygonImage`（类型缺失，连带 30 个 TrueShadow 加不上）+ Game.prefab 里 3 个 stripped 组件 |
| 外部资产不在磁盘 | 40 | `Fonts/Common/Arial`、`DebatePrinterStyleSheet`、`LogMessageStyleSheet`、`EndingCredits`、`TrialsClockFont` 这 5 个 TMP 字体资产从未重建 |
| 外部但计划里没有引用条目 | 65 | 内置 Mesh 等（fileId 2） |
| `AnimationCurve` 不支持 | 432 | applier 只记警告，字段保持默认（时间曲线/包络的 wrap mode 映射未经验证，故未猜） |

## 副作用（必须知道）

* `PrefabBuilder` 对每个 prefab `DeleteAsset + SaveAsPrefabAsset`；Addressables 在资产被删时丢掉条目，`Manosaba.Prefabs` 组因此少了约 150 条。
  **prefab 重建之后必须跑 `g3_content_build.ps1 -Group all`**（`AddressablesSetup` 按 GUID 重建条目，本轮：planned=30819 added=30819 missing=0）。
* `applier` 不去重 rid，`playlist.commands[i]` 与 `lines[j].command` 各建一份实例（资产体积翻倍）；运行时只用 `Playlist`，本轮记录为已知偏差。

## 复现

```
python work/serialization-loss/snapshot_guids.py                 # 重生成前的 GUID 基线
powershell work/serialization-loss/regen_all.ps1                 # 440 剧本原地更新 → 156 prefab
python work/serialization-loss/verify_scenarios.py               # 独立对账：期望 hasValue 数 / YAML 实际数 / GUID
python work/serialization-loss/verify_prefabs.py [--plan prefab_plan_probe.json]
python work/serialization-loss/typecheck_editor.py [override_dir]  # 不启动 Unity 的 Editor 程序集离线类型检查
```
