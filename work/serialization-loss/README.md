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

## 全量 156 个 prefab 之后**仍然**未绑定的（已分类，非静默）

| 类别 | 数量 | 说明 |
|---|---|---|
| internal，目标不在计划里 | 4,829 | 主要是 `LayeredCharacters/*.prefab` 的 `SpriteRenderer.m_Sprite`（2,064）/`m_Materials`（2,065）、`PlayableDirector`（681）：指向**同一 bundle** 里的 Sprite/Material/TimelineAsset，计划只含 GameObject/组件层级。需要按 (bundle, path_id) 查 `res/_index.csv` 的规划器增强；**不影响标题路径**（TitleUI/ControlPanel/OptionsUI 0 unbound） |
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
