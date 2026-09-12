import io

P = r"E:\Manosaba-VSCodeReady\README.md"
text = io.open(P, encoding="utf-8").read().replace("\r\n", "\n")
lines = text.split("\n")

# 找到重复章节的起点（第二次出现 "## 1. 目录结构"）
idx = [i for i, l in enumerate(lines) if l.strip() == "## 1. 目录结构"]
if len(idx) >= 2:
    lines = lines[: idx[1] - 1]

# 去掉尾部多余空行
while lines and lines[-1].strip() == "":
    lines.pop()

section = """
## 独立编译 / 源码包

只需要 **.NET SDK 8+**：不用 IDE、不用联网、不写 C 盘。

```powershell
.\\build.ps1              # 一键构建 -> dist\\ 44 个 dll（可双击 build.cmd）
.\\build.ps1 -Clean       # 清 bin/obj 后全量重建
.\\build.ps1 -Pack        # 顺便导出源码包
dotnet build project\\Manosaba.sln      # 等价的手工命令
```

### 源码包 `dist\\Manosaba.Sources.zip`（约 18 MB）

自带全部依赖、**不含绝对路径**，解压到任意机器/任意目录即可编译
（已实测：解压到另一个盘符后 `dotnet build project\\Manosaba.sln` = **0 error**）：

| 包内内容 | 规模 |
|---|---|
| `src\\decompiled\\` 43 个游戏程序集源码 | **1651 个 .cs** |
| `src\\il2cpp\\DummyDll\\` 参考程序集（编译必需） | 129 个 |
| `src\\il2cpp\\` `dump.cs` / `script.json` / `il2cpp.h` | — |
| `project\\` 44 个 `.csproj` + `.sln`（含 BepInEx 插件工程） | — |
| `build.ps1` / `build.cmd` / `build.sh` | — |

构建产物就是 43 个游戏程序集：`Assembly-CSharp.dll`、`Elringus.Naninovel.Runtime.dll`、
`GigaCreation.*.dll` …… 与原始托管程序集一一对应。

> **“完整”的边界要说清楚**：这里完整的是 **类型系统** —— 43 个程序集、18076 个类型、
> 字段偏移、方法签名、原生地址（RVA/VA）全都在，而且能编译通过（0 error）。
> 但 IL2CPP 已经把**方法体**编译成原生代码，托管侧只剩 `return null;` 占位，
> 所以不存在“编译出来就能玩”的源码；要真正的逻辑只能走 Ghidra/IDA
> （`script.json` + `il2cpp.h` 已备好，见第 3 节）。
"""

# 插到 "## 4. 已知边界" 之前
pos = next((i for i, l in enumerate(lines) if l.startswith("## 4. 已知边界")), len(lines))
out = lines[:pos] + section.strip("\n").split("\n") + [""] + lines[pos:]

io.open(P, "w", encoding="utf-8", newline="\n").write("\n".join(out) + "\n")
print("lines:", len(out))
for i, l in enumerate(out, 1):
    if l.startswith("##") or l.startswith("### "):
        print(i, l)
