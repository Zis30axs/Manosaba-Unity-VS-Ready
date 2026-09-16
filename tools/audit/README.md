# Unity Project Reconstruction Audit — 一次性分析工具

针对 `E:\Manosaba-VSCodeReady\res`（AssetRipper 式 typetree dump）
+ 游戏安装目录的原始 AssetBundle / Player 数据文件，
回答“现有资源能否机械还原成 Unity Editor 工程”。

> 依赖：`E:\Manosaba-VSCodeReady\tools\venv\Scripts\python.exe`（Python 3.12 + UnityPy 1.25.3）
> `res/`、`tools/` 都在主 checkout 且被 .gitignore 忽略，本目录的脚本用**绝对路径**读取，
> 不要指望在 worktree 里能直接跑通（缺 res/）。

## 运行顺序

```bat
set PY=E:\Manosaba-VSCodeReady\tools\venv\Scripts\python.exe
%PY% a01_inventory.py        :: 全仓 + 游戏目录 文件清点（按扩展名 + 文件头内容嗅探）
%PY% a02_index_stats.py      :: res/_index.csv 统计（类型/容器/pathID）
%PY% a03_sources.py          :: 用 UnityPy 扫全部 457 bundle + 5 个 Player 数据文件（每文件独立子进程 + 缓存）
%PY% a04_refs.py             :: 全 dump 的 PPtr(m_FileID/m_PathID) 引用图与可解析率
%PY% a05_monobehaviour.py    :: MonoBehaviour 实例/脚本类/字段名普查
%PY% a06_rebuild.py          :: Boot.unity / Game.prefab / resources.assets 层级重建 + 校验
%PY% a07_addressables.py     :: Addressables key→bundle→依赖 重建（catalog.bin + AssetBundle manifest）
%PY% a08_prefab_sweep.py     :: 全部 156 个 prefab 容器逐一层级校验
```

结果写到 `out/`；`out/a03_cache/`、`out/containers_by_kind.csv` 是可再生中间产物，未入库。

## 主要结论（详见根目录 UNITY_PROJECT_RECONSTRUCTION_AUDIT.md）

* dump 覆盖 60,144 个源对象中的 58,718 个（97.6%）；缺失的 1,426 个里 457 个是 AssetBundle 元对象。
* 92,585 条序列化引用中，非空引用 70,524 条，可解析 69,666 条（98.8%）。
* 156 个 prefab：153 个层级零缺陷；Boot.unity 场景 33 GO / 33 Transform 完整。
* 全仓 0 个 `.unity/.prefab/.meta/.asset/.mat/.controller/.anim/.spriteatlas/.shader`，0 个 Unity YAML。
