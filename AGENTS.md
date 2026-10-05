# AI 协作约定

修改本仓库前，阅读 [开发指南](DEVELOPMENT.md)，特别是“GitHub 协作与生成资源修复”和“维护代码与扩展转换器”。统一流程以开发指南和实际入口代码为准。

## 可跨副本复现的修复

- `res/` 是本地资源编辑源，`UnityProject/Assets/GameContent/` 是生成树，`Library/` 是本地缓存。它们的忽略文件不会随 Git 拉取同步。不得仅靠修改这些文件交付共享修复。
- 文本增量使用现有 `content-overrides/`；导入/转换修复保存为可追踪的代码或配方，并复用现有 import/sync/build 入口。不要新增一套平行导入流程，也不要强制提交整份 GameContent、bundle 或本机 `.meta` 来替代重建。
- 新修复必须覆盖已有副本“拉取源码后 res 没有变化”的情况。保留协作者本地 GUID、引用和无关字段；重复运行应不产生额外资源改动；失败必须传递至调用入口。
- 幕布恢复参考 `UnityProject/Assets/Editor/RestoreOriginalCurtainSkin.cs` 和 `OriginalCurtainSpriteData.json`。它们是六张幕布的专用修复，不能据此声称全部忽略资源都已自动同步。正式输入不能依赖 `work/tmp`、个人目录或未提交文件。
- `PrefabBuilder.ReadStreamedFrames()` 解码 packed AnimationClip 时，`inSlope` 必须使用前一个 key 的 cubic 系数在当前时间点求导：`3*prior.c1*dt*dt + 2*prior.c2*dt + prior.outSlope`，其中 `dt = 当前帧时间 - prior.time`，`PackedKey.time` 必须在读帧时赋值（曾漏赋值为 0，除首段外切线全错，幕布达 16,483°/s）；若 `prior.c1/c2/outSlope` 全为 0（阶跃段，如 ObjectionCutIn 的 0/1 开关），`inSlope = +Infinity`。路径哈希在本 prefab 中找不到的绑定必须跳过，不能写到 Animator 根。使用当前 key 的系数会造成约 ±1e4°/s 切线、Hermite 过冲和 SpriteSkin 极端扭曲。修改后必须重新生成 prefab，并在关键帧之间取样验证；不要只手改被忽略的 GameContent。
- 改动 `Assets/Editor` 转换器后，打开的 Editor 必须先完成编译再转换。`ContentSyncBridge` 会在加载的编辑器程序集早于转换器源码时先刷新编译、再执行请求；不要绕过它直接在同一调用中 Refresh 后运行 PrefabBuilder，否则旧代码会写出 prefab 而 sync 记录新配方已完成。
- sync 对 prefab 默认只做字段补丁（`ApplyEditable`），补丁无法重建 Timeline/AnimationClip 子资源。转换配方变化时，计划中含 AnimationClip 的 prefab（当前 68 个，均只被 GUID/地址引用）改走完整 `BuildOne` 重建，随后自动重跑 Addressables setup 恢复被删除重存丢掉的分组条目。其余 prefab 中有的被别的 prefab 以内部 fileID 引用，仍只打补丁；需要让它们吃到转换器结构性修改时另行评估。

## 检查与交付

- 检查 `git status --short --untracked-files=all` 和相关路径的 `git check-ignore -v`，列出新增的脚本、配方、Unity 源资产 `.meta`；确认它们进入修复提交清单。保留工作区已有改动，不混入存档、临时状态、日志或机器路径。
- 以本次退出结果、实际 Unity 读回及新鲜报告验证修复。检查幂等性；涉及协作复现时说明是否在独立副本或冷导入环境验证过。旧 PASS、资源数量相等、doctor 通过或 dotnet 编译成功不能替代 Unity/运行验收。
- 区分 AssetDatabase 与打包内容。修好 importer 不会刷新旧 catalog、bundle 或 Player；验收 `Use Existing Build`/Player 前重建 Addressables。当前 Player 构建入口的幕布校验不代表所有旧 bundle 都已更新。
- GitHub hosted/static CI 不能替代 Unity runtime 验收；Unity gates 需要 self-hosted Windows、匹配的 Unity 许可证和本地生成资源。compile-only 通过时如实标为编译验证。
- 已导入副本的标准顺序为：拉取 → `tools/content/sync.ps1` → `tools/content/doctor.ps1` → `tools/content/build.ps1` → 新 Play 会话；任一步失败先处理。新 clone 先完成指南中的同版本资源导入。执行这些动作仍按当前任务范围与用户授权。
- 交付说明必须包含可追踪输入、协作者执行方法、验证证据、未验证范围和 commit/push 状态。仅本地修改不得表述为“GitHub 已同步”；未运行或失败的测试如实说明。
