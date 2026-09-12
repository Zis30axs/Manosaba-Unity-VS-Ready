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

## 三个必须记住的坑

1. **`RVA 0x30C000` 是共享空函数体**（`ret 0`，已反汇编确认）。
   所有指向它的托管方法都是**真的空方法**，可以直接判定 Confidence A，不需要再分析。
2. **函数体合并（body folding）**：IL2CPP 把内容相同的平凡访问器合并成一份。
   例如 `0x33A4E0` 被 68 个不同的 getter 共用。
   所以 RVA → 托管方法是**一对多**，用 RVA 做字典键会静默丢方法；
   而且朴素入度会被严重放大（`rank2.py` 已修正）。
3. **`MethodInfo*` 是最后一个参数，不是隐藏的第一个参数**。
   实例方法 `__this` 在 RCX；静态方法没有 `__this`，第一个实参就在 RCX。
   参数多时 `MethodInfo*` 落在栈上，不要把它当成真实参数。
