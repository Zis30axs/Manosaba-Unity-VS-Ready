# `@spawn` NullReferenceException 审计（2026-09-18）

## 症状

`System/System_Title` 播到第 7 条命令（index 6，`@blur` = `SpawnBlur`）时：

```
System.NullReferenceException
  at Naninovel.Commands.Spawn.ApplyParameters (SpawnedObject spawn)   Spawn.cs:97
  at Naninovel.Commands.Spawn.Execute (AsyncToken token)              Spawn.cs:86
```

## 临时诊断（已撤）实测输出（`game_start_stubdiag.log`，Editor Play Mode，`g4b -Wire`，60 s 链窗口）

```
[SpawnDiag] Execute cmd=Spawn playedScript='System/System_Title' playedIndex=6 playedCmd=SpawnBlur
            spot='Invalid #0.-1' Path='Blur#null' Params=[null,null,null]
            manager=GigaCreation.NaninovelExtender.ExtendedSpawn.MultipliableSpawn, GigaCreation.NaninovelExtender.ExtendedSpawn, ...
            isStdNaninovelSpawnManager=False IsSpawned=False
[SpawnDiag] GetOrSpawn manager=GigaCreation.NaninovelExtender.ExtendedSpawn.MultipliableSpawn path='Blur#null' branch=Spawn
[SpawnDiag] GetOrSpawn Spawn returned NULL
[SpawnDiag] GetOrSpawn returned NULL
```

* 执行的命令：`SpawnBlur`（`@blur`，`System_Title.asset` rid 4985271874984609886，playlist index 6）。它内部 `new Spawn { Path = "Blur#{ActorId}" ... }.Execute()`，
  因此 `PlaybackSpot` 是 Invalid、`Path = "Blur#null"`（ActorId 未赋值，`StringParameter` 隐式转 string 为 null；资源路径经 `ProcessInputPath` 仍是 `Blur`，
  与四月原版行为一致，`Naninovel.FX.Blur` 自己把 null 参数回落成 `MainBackground`）。
* 实际 `ISpawnManager`：`GigaCreation.NaninovelExtender.ExtendedSpawn.MultipliableSpawn`（`[InitializeAtRuntime(0, typeof(SpawnManager))]` 覆盖了标准实现，
  与四月 dump.cs:735222 一致，不是重复注册）。
* null 产生在 `MultipliableSpawn.Spawn(string, AsyncToken)`：它是 Il2CppDumper 骨架 `return default(UniTask<SpawnedObject>);`，
  即一个已完成、结果为 null 的 UniTask。`GetOrSpawn` 走 `Spawn` 分支，把 null 原样返回。

## 根因

`GigaCreation.NaninovelExtender.ExtendedSpawn` 程序集的方法体从未复原：`MultipliableSpawn` 的 `Spawn/IsSpawned/GetSpawned/InitializeService/...`、
`SpawnExtended.Execute/ApplyScenePosition`、`DestroySpawnedExtended.Destroy/Warn`、`MultipliableSpawnManagerExtensions.GetOrSpawnAsync`、
`CameraConfigurationUtility.EvaluateSceneRect` 全是 `return default/null` 存根。`InitializeService` 存根还意味着 `_loader/_container` 从未创建，
即便 `Spawn` 有实现也会在下一行 NRE。

## 修复

按 `work/spawn-nre/disas/*.txt`（`work/reversing/disas.py` 对四月 CHS `GameAssembly.dll` 的带注释反汇编）逐函数复原上述方法体，
遵循仓库既有的 async 复原惯例（`XxxCore` + 保留编译器状态机元数据）。每个方法头部注释都标出 RVA、关键指令地址与 vtable 槽位推导。

有意的偏差（只改报错文案，不改语义）：四月 `Spawn` 在 `prefabResource.Valid == false` 时复用了 "already spawned" 的错误字符串；复原版改为指出
"spawn resource not registered"，否则资源注册问题会被误导性文案掩盖。

## 验证

* 离线：`work/spawn-nre/typecheck_extendedspawn.py` 用 Unity 自带 Roslyn + Bee 记录的引用集编译 Cameras / ExtendedSpawn 两个程序集：0 error。
* 运行时：`g4b_game_start.ps1 -Wire`（`MANOSABA_GAMESTART_CHAIN_SECONDS=60`），见 `g4b_fixed_console.txt` 与本文件末尾的结论。

## 独立待修项（不属于本问题）

* `FileNotFoundException: Could not load the file 'Assembly-CSharp'`（`ManosabaBoot.ProbeCatalog`）— catalog 里的 `Assembly-CSharp` MonoScript 引用，保持独立跟踪。
* `LocalizeImage.get_Map` NRE、`IDefaultLocaleSelector/IWitchTrialsNaninovelManager` 重复注册告警 — 与 spawn 无关。

## 修复后实测（`g4b_fixed2_console.txt` / `game_start_fixed2.log`，2026-09-18 23:5x）

* `Spawn.ApplyParameters` NRE：0 次（修复前同窗口 7 次）。
* `MultipliableSpawn.SpawnCore` 实际实例化了 `Naninovel/Spawn/Blur`，`Blur.AwaitSpawn` 随后报
  `Failed to apply blur effect: Can't find 'MainBackground' actor`（Engine.Err，非异常，不阻塞）——标题脚本此时尚无主背景，
  与命令语义一致。
* 脚本推进：`System/System_Title` 的 auto-advance 快照从 `index=6` 变为 `index=8`。
* 新的第一个阻塞异常（独立待修项）：

  ```
  System.NullReferenceException
    at Naninovel.Commands.AnimateActor.Execute (AsyncToken) AnimateActor.cs:105
  ```

  即 playlist index 8 的 `AnimateActorExtended`（`GigaCreation.NaninovelExtender.ExtendedActors`），
  其 `GetParametersForActor` 等仍是 `return null` 存根。
* 门禁本身报 BLOCKED（无最终 evidence）：Unity 6 的 "Define symbols changed" 重编译 + 域重载在 auto-advance 窗口内落地
  （日志第 5935 行），把探针协程杀掉了；这是门禁时序问题，与本修复无关（11:42 那次成功运行只是重载来得更晚）。
