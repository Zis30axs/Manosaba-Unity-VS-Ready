# 资源增量清单

这里保存可以在不同资源副本之间复现的、小范围资源修改。清单不包含原始游戏资源、Unity `GameContent`、`.meta`、Addressables bundle 或本机路径。

应用前先完成资源 import 和 Unity bootstrap，然后在仓库根目录运行：

```powershell
.\tools\content\overrides.ps1 apply content-overrides/current-debug-zh-Hans.json --source-root res
.\tools\content\overrides.ps1 apply content-overrides/current-debug-zh-Hans.json --source-root res --apply
.\tools\content\sync.ps1 --category "PREFAB,TEXT"
.\tools\content\build.ps1
```

第一次命令只预览。应用工具会按 `res/_index.csv` 的对象身份定位重新提取后可能变成 `~12`、`~25` 等不同文件名的对象；它只写 `m_text` 或 UTF-8 文本，并在发现冲突时整批停止。它不会触碰 GUID、引用、Unity metadata，也不会自动导入或构建。

当前清单恢复 Debug 左侧菜单的 16 个中文文本和 `Debug_Home.txt`。本地开场测试句属于独立的可选测试，不随这份正式 Debug 清单应用。
