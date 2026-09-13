# 反编译工具链（native → 可读 C#）

从 IL2CPP 原生二进制恢复方法体的分析工具。纯 Python + capstone，不依赖 IDA / Ghidra。

## 目标二进制（已校验）

```
E:\迅雷云盘\manosaba.game-STEAM.CHS\manosaba_game\GameAssembly.dll
PE timestamp 0x68B45C56   imagebase 0x180000000
```

**注意：`E:\SteamLibrary\steamapps\common\manosaba_demo\GameAssembly.dll` 是另一个构建
（timestamp 0x684C4689，节区布局不同），RVA 与本工程的 dump 不对应，绝对不能混用。**

校验方法：dump.cs 里 `RVA 0x33A4E0 / Offset 0x338EE0` 这一对，用 PE 节表换算必须吻合。
CHS 正式版吻合，demo 版算出 `0x3390E0`，不吻合。

## 环境

```powershell
E:\Manosaba-VSCodeReady\tools\venv\Scripts\python.exe -m pip install capstone
```

## 用法

```powershell
$py = "E:\Manosaba-VSCodeReady\tools\venv\Scripts\python.exe"
cd E:\Manosaba-VSCodeReady\work\reversing

# 1) 建符号库（一次性，约 1 分钟，产出 symdb.pkl）
& $py il2cpp_db.py

# 2) 全局扫描：调用图 / 分支数 / 字符串引用 / 字段偏移（约 2 分钟，产出 facts.pkl）
& $py sweep.py

# 3) 按恢复价值给游戏程序集的方法排序（产出 ranked.json）
& $py rank2.py

# 4) 反汇编单个方法（核心工具）
& $py disas.py "WitchTrials.Models.TrialStateManager::RegisterCameraState"
& $py disas.py 0x39DA50 --type WitchTrials.Models.TrialStateManager
```

`disas.py` 会自动标注：

| 标注 | 含义 |
|---|---|
| `-> Ns.Type$$Method` | 直接调用，从 `script.json` 的 `ScriptMethod` 反查 |
| `STR '...'` | 字符串常量（`ScriptString`） |
| `TYPE X_TypeInfo` | `Il2CppClass*` 槽位（`ScriptMetadata`） |
| `METHODINFO Method$X => Y` | `MethodInfo*` 槽位，含泛型/接口实参（`ScriptMetadataMethod`） |
| `rcx->Type.field` | `[reg+imm]` 按类型的字段偏移表反查，并用访问宽度过滤 |
| `[boilerplate] ...` | IL2CPP 运行时脚手架，不属于游戏逻辑 |

## 文件

| 文件 | 作用 |
|---|---|
| `il2cpp_pe.py` | PE 解析、RVA↔文件偏移换算、`.pdata` 函数表、capstone 初始化 |
| `il2cpp_db.py` | 载入 `script.json` + 解析 `dump.cs`（类型/字段偏移/方法 RVA），缓存为 `symdb.pkl` |
| `disas.py` | 带语义标注的反汇编器 |
| `sweep.py` | 全量扫描 101,275 个函数，建调用图与事实表 |
| `rank2.py` | 恢复优先级排序（已修正 IL2CPP 函数体合并造成的入度虚高） |
| `ident_helpers.py` | 识别高频运行时 helper |
| `probe_exports.py` | 解析导出表，得到 264 个 `il2cpp_*` / `mono_*` 真名 |

## 已识别的运行时 helper

| RVA | 含义 | 调用点数 |
|---|---|---|
| `0x250FB0` | `il2cpp_codegen_initialize_runtime_metadata`（类型初始化守卫，**样板**） | 51,281 |
| `0x246350` | 同上的 thunk（**样板**） | 11,711 |
| `0x246730` | 抛 `NullReferenceException`（空引用检查落地点） | 36,613 |
| `0x2980E0` | `il2cpp_codegen_object_new` | 12,718 |
| `0x2466E0` | 同上的 thunk | 10,391 |
| `0x24CE60` | 接口/虚方法分派解析 | 24,888 |
| `0x243A20` | 解析接口方法槽 | 1,260 |
| `0x22E0` | 接口调用 thunk | 3,926 |
| `0x7EC0` / `0x7F50` | 泛型接口调用 thunk（`IList<T>.get_Item` 等） | — |

另外两种在正文中反复出现、**应当忽略**的样板：

* `lea r8,[obj+off]` → `shr/and` → `bts` + `lock cmpxchg [rdx+0x462e0]`：GC 写屏障（card table）。
* `cmp byte ptr [rip+X],0` / `jne` 包住一串 `initialize_runtime_metadata`：类型初始化一次性守卫。

## 运行时验证通道（BepInEx 6 + Il2CppInterop）

2026-09-13 验证：这游戏（Unity 6000.0.48f1，metadata v31）**可以**用 BepInEx 6 挂 Harmony，
但工程里原来打包的 `tools/bepinex.zip`（约 2022 年的 BepInEx 6 bleeding-edge，用老一代
**Unhollower** 后端）完全不兼容——启动即崩：

```
System.TypeInitializationException: The type initializer for
'UnhollowerBaseLib.Runtime.UnityVersionHandler' threw an exception.
---> Could not load file or assembly 'Il2Cppmscorlib, Version=3.7.1.6...'
```

Unhollower 已经停止维护，新版本的 BepInEx 6 全面转向 **Il2CppInterop** 后端
（CoreCLR 宿主，不再是 Mono）。修复：从 <https://builds.bepinex.dev/projects/bepinex_be>
下载最新 `BepInEx-Unity.IL2CPP-win-x64-*.zip`（当前用的是 `6.0.0-be.788+5b766a3`），
替换 `tools/bepinex/` 整个目录。

### 关键认识：这不是"免费的反编译"

第一次看到日志里的

```
[Info   :Il2CppInteropGen] Unstripping method bodies...
[Info   :Il2CppInteropGen] IL unstrip statistics: 10712 successful, 1478 failed
```

以为 Cpp2IL/Il2CppInterop 批量恢复了上万个方法体，**这个判断是错的**，已经反编译验证过：

`BepInEx/interop/Assembly-CSharp.dll` 里的 `TrialStateManager` 每个方法都是这种形状——

```csharp
public unsafe virtual void RegisterCurrentStage(PerspectiveStageKind kind)
{
    System.IntPtr* ptr = stackalloc System.IntPtr[1];
    *ptr = (nint)(&kind);
    IL2CPP.il2cpp_runtime_invoke(NativeMethodInfoPtr_RegisterCurrentStage_..., ..., (void**)ptr, ref intPtr);
}
```

纯粹是参数封送 + `il2cpp_runtime_invoke` 转发，**没有一行游戏逻辑**。
"IL unstrip" 针对的是 Unity/BCL 类型（靠下载的官方参考库对比恢复），
跟 `Assembly-CSharp`/`Naninovel` 这些自研代码完全无关。

方法体依然只能靠 capstone/Ghidra 静态分析（本目录其余工具）+ Harmony 做经验验证。

### 这条通道真正的价值：验证已恢复的方法

装好之后 `project/src/Manosaba.Plugin` 能正常编译加载，可以写 Harmony postfix
读游戏运行时的真实字段值，跟静态复原的结论对照。已经用它验证过一条：

```csharp
[HarmonyPatch(typeof(TrialStateManager), nameof(TrialStateManager.InitializeService))]
private static void Postfix(TrialStateManager __instance) { ... }
```

结果（游戏启动后自动触发，不需要玩家操作）：

```
[verify] InitializeService() -> _stageManager=IPerspectiveStageManager _cameraStates=non-null _screenStates=non-null
```

跟 `RECONSTRUCTION.md` 里对 RVA `0x39D3A0` 的静态复原结论完全一致——
`_stageManager = ServiceLocator.Get<IPerspectiveStageManager>(true)`、
两个 List 各自 `new` 出来。这是目前工程里**唯一**一条从 Confidence A（静态）
升级到"静态 + 运行时双重确认"的方法。

**坑：`Traverse.Field("_x")` 对 Il2CppInterop 类型不起作用，会静默返回 null。**
第一次测试就中招——interop 代理把每个字段（哪怕原来是 private）都实现成
手写 get/set 的**属性**，直接做指针运算读原生内存，压根没有真正的 CLR 字段。
`Traverse.Field` 找不到字段会静默失败而不是抛异常，看起来像"复原错了"，
其实是验证代码本身的 bug。**要读字段直接用属性访问（`__instance._x`），
不要用 `Traverse.Field`。**

### 复现步骤

```powershell
# 换成新版 BepInEx（tools/bepinex 已经是修好的版本，直接装）
E:\Manosaba-VSCodeReady\install_bepinex.ps1

# 首次启动会跑 Cpp2IL + 生成 interop 程序集，大约 1~2 分钟；之后的启动是秒开
E:\Manosaba-VSCodeReady\tools\launch_game.ps1

# 卸载，游戏恢复原样
E:\Manosaba-VSCodeReady\install_bepinex.ps1 -Uninstall
```

日志：`<游戏目录>\BepInEx\LogOutput.log`；崩在 doorstop 阶段的话看
`<游戏目录>\preloader_*.log`（这个不在 `BepInEx\` 目录下，容易漏看）。

## 四个必须记住的坑

1. **`RVA 0x30C000` 是共享空函数体**（`ret 0`，已反汇编确认）。
   所有指向它的托管方法都是**真的空方法**，可以直接判定 Confidence A，不需要再分析。
2. **函数体合并（body folding）**：IL2CPP 把内容相同的平凡访问器合并成一份。
   例如 `0x33A4E0` 被 68 个不同的 getter 共用。
   所以 RVA → 托管方法是**一对多**，用 RVA 做字典键会静默丢方法；
   而且朴素入度会被严重放大（`rank2.py` 已修正）。
3. **`MethodInfo*` 是最后一个参数，不是隐藏的第一个参数**。
   实例方法 `__this` 在 RCX；静态方法没有 `__this`，第一个实参就在 RCX。
   参数多时 `MethodInfo*` 落在栈上，不要把它当成真实参数。
4. **`dotnet build` 的增量缓存会掩盖警告**。只改了少数几个文件后跑
   `dotnet build`，MSBuild 只重新编译并报告"这次实际重新编译的项目"的警告，
   已经是最新状态的项目（哪怕它们本来就有一堆 `CS8632` 之类的历史警告）不会
   重新打印——很容易被"0 个警告"骗过去，以为自己的改动是干净的。
   踩过一次：加一行 `using System;` 时没注意到文件开头已经有一行（BOM 后面），
   造成 `CS0105` 重复 using 警告，但当时的增量构建报的是"0 个警告"，
   直到后来跑了一次 `dotnet build ... --no-incremental` 做全量检查才暴露出来。
   **要确认自己改动没有引入新警告，必须 `--no-incremental` 全量重建一次**，
   而且要接受全量重建下 100+ 个警告是这个工程的正常基线（其他 42 个程序集里
   还有大量未触碰的 `CS8632` 历史警告），只需要抓自己碰过的文件有没有新增的。
