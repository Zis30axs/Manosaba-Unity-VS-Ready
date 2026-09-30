"""Split the single game-start commit into categorized commits with Chinese messages.

Safety properties this script enforces:

  * it only runs when HEAD is the commit it is meant to split, and that commit's parent is the
    expected base, so it can never rewrite unrelated history;
  * the reset is `--mixed`, which never touches the working tree;
  * the final tree is compared against the original commit's tree hash: if the nine commits do not
    reproduce the exact same content, the script reports it loudly instead of claiming success;
  * commit messages are handed to git through `-F <utf-8 file>` rather than `-m`, because passing
    non-ASCII through a PowerShell/console argument is exactly how this repository has mangled text
    before.

Usage: python work/split_commit_into_categories.py [--dry-run]
"""
import argparse
import glob
import io
import os
import subprocess
import sys

REPO = r"E:\Manosaba-VSCodeReady"
SPLIT_HEAD = "a9359338f0d7840e2964676e72edbabf0d765f4e"   # the commit to split
EXPECTED_PARENT = "81387ca"                                # its parent, abbreviated
MSG_DIR = os.path.join(REPO, "work", "commit_msgs")

# (title, body, [pathspecs]) -- order is the commit order.
GROUPS = [
    (
        "补上忽略规则，并记入集成过程的完整台账",
        """跟踪原则：只跟踪作者产出的东西，可再生的批量产物一律排除。
新增忽略项：
  Assets/GameContent/       由 res/ + bundle 经 a13_import_assets.py 再生
  Assets/StreamingAssets/   apply_shipped_aa.ps1 拷入的出货运行时数据
  UnityProject/out/         误落在工程根目录的 58.9 MB catalog dump
  UnityProject/UpgradeLog.htm、.vs/、*.user、*.suo

UNITY_RUNTIME_INTEGRATION.md 记录 G0-G6 各门、每一轮的真因与实测读数；第 41 节是本次
"引擎终于完成初始化"的完整证据链（三处独立根因 + 各自的原生代码依据 + 门状态）。""",
        [".gitignore", "UNITY_RUNTIME_INTEGRATION.md"],
    ),
    (
        "提交审核工具链（tools/audit）",
        """对出货构建做静态清点：容器与资源清单、按资源类型分类、参考关系求解、地址化键表。
本次更新的 addressables.json、bundles.json、addressable_keys.csv.gz 与 config.py 是
"重建内容 vs 出货内容"比对的数据来源，a07_addressables.py 负责抽取地址化键。""",
        ["tools/audit"],
    ),
    (
        "提交重建流水线工具（tools/unity-rebuild）",
        """从出货资源重建 Unity 工程的完整流水线，按阶段编号：
  a13*                 导入资产、MonoScript 索引、脚本绑定、ScriptableObject/prefab/地址化计划
  decode_*、read_*     解析 level0 启动场景、MasterData、Naninovel 配置等出货数据
  g1 ~ g6              六个验收门（编译、导入、内容构建、Play Mode、独立 Player、内容覆盖）

本次新增两个工具：
  align_asset_guids.py     把工程 asset 的 .meta GUID 对齐到出货 GUID（932 个 / 938 文件 / 1996 处）
  compare_catalog_keys.py  用运行时解析器 dump 并对比出货与重建 catalog 的 key 表

另附 work/check_data_assets.py 与 work/check_scenario_addressing.py —— 第 41 节引用的两处测量脚本。""",
        ["tools/unity-rebuild", "work/check_data_assets.py", "work/check_scenario_addressing.py"],
    ),
    (
        "提交 Unity 工程设置（ProjectSettings）",
        """Unity 6000.0.48f1 的工程设置，构建目标 Windows x64、Player 走 IL2CPP。
EditorBuildSettings 把重建出来的 Assets/Scenes/Boot.unity 排在构建列表首位。""",
        ["UnityProject/ProjectSettings"],
    ),
    (
        "内嵌 Naninovel 等七个包（Packages）",
        """packages-lock.json 把这七个包标记为 source=embedded，也就是这些目录本身就是包内容，
无法从 registry 还原，必须入库：
  com.elringus.naninovel（Naninovel 1.20；本次修好的启动链有相当一部分落在这里，
    其中 Runtime/Engine/Engine.cs 曾在排查时临时插桩，已按快照还原）
  com.cysharp.unitask、com.neuecc.unirx、com.le-tai.true-shadow、
  com.annulusgames.litmotion、com.ff.mpuikit、com.rlabrecque.steamworks.net
其余依赖是 registry/builtin，由 manifest.json + packages-lock.json 决定，不入库。""",
        ["UnityProject/Packages"],
    ),
    (
        "提交重建的游戏脚本源码（Assets/Scripts）",
        """由 IL2CPP 元数据加原生代码逐成员重建的游戏 C# 源码（1503 个文件），按原程序集分目录
（GigaCreation.*、WitchTrials.*、Naninovel 扩展等）。每个重建成员都带
`// RECONSTRUCTED | Confidence: ... | RVA: ...` 标注与证据说明，未重建的仍保留 dumper 桩体。

让引擎真正完成初始化的三处修复就在这批文件里：
  GigaCreation.Essentials.AddressablesUtils/AddressablesManager.cs
    补上两个泛型 GetOrLoadAddressableAsset<T> 重载。原先体为 `return default(UniTask<T>)`，
    其 awaiter 为空，await 得到 default(T)=null，于是每一处 AssetReference 加载都静默返回 null。
  GigaCreation.Essentials.Localization/LocalizedText.cs
    补上 Locale / Text / 构造器。Locale 原为桩、恒返回 default(LocaleKind)=Ja，
    使 AuthorTextBuilder.LoadDataAsync 的 item.Name.ToDictionary(t => t.Locale, ...) 抛
    "An item with the same key has already been added. Key: Ja"。
  （第三处是 .meta GUID 对齐，属工程资产，见地址化与场景那一笔提交。）""",
        ["UnityProject/Assets/Scripts"],
    ),
    (
        "提交编辑器端重建工具（Assets/Editor）",
        """在 Unity 内执行重建与验收的 Editor 工具：
  PrefabBuilder、ScriptableObjectConverter、JsonToSerializedApplier   按出货 typetree 还原资产
  Level0SceneBuilder、Level0SceneProbe                                 重建 level0 为 Assets/Scenes/Boot.unity
  AddressablesSetup、AddressablesBuild、CatalogDump                    地址化分组、内容构建、catalog dump
  CompileGate、ImportVerifier、PlayModeGate、BuildPlayer               对应 G1 ~ G5 各门
  ProjectBootstrap、TmpEssentialsSetup                                 启动场景与 TMP essential 资源
  MasterDataImporter、MaterialImporter、NaninovelConfigBuilder、ImportVerifier 等 导入与校验""",
        ["UnityProject/Assets/Editor"],
    ),
    (
        "提交地址化配置与重建的 level0 场景",
        """Assets/AddressableAssetsData：10 个分组、30819 个条目（Audio 26537、Prefabs 1952、
Textures 1389、Text 450、Scenarios 440、Fonts 24、Sprites 17、Data 8、Video 2）。
组条目的 m_GUID 已是出货 GUID、m_Address 是出货主键，AssetReference 因此能解析到目标资产。

Assets/Scenes/Boot.unity：由出货 level0 的序列化引用重建（77.5 KB），并被 Level0SceneBuilder
注册为构建列表首场景。G4b 实测 component_source=scene，即启动入口 GameInitializer 确实来自该场景。""",
        ["UnityProject/Assets/AddressableAssetsData", "UnityProject/Assets/Scenes"],
    ),
    (
        "提交导入资源与工程资产",
        """Assets/Plugins：FastEnum 依赖的 System.Runtime.CompilerServices.Unsafe.dll。
  Player 构建的链接器会因缺它报 IL1010，该副本取自 Burst 包，来源见同目录 PROVENANCE.md。
Assets/NaninovelData：Naninovel 配置资产与脚本元数据。
Assets/TextMesh Pro：TMP essential 资源；缺失时每个 TMP_Text 都会在 LoadDefaultSettings 处 NRE。
Assets/*.meta、DefaultVolumeProfile.asset、UniversalRenderPipelineGlobalSettings.asset：URP 设置与目录 GUID。
steam_appid.txt：Steam 应用号。""",
        ["UnityProject/Assets/Plugins", "UnityProject/Assets/NaninovelData",
         "UnityProject/Assets/TextMesh Pro", "UnityProject/Assets/DefaultVolumeProfile.asset",
         "UnityProject/Assets/DefaultVolumeProfile.asset.meta",
         "UnityProject/Assets/UniversalRenderPipelineGlobalSettings.asset",
         "UnityProject/Assets/UniversalRenderPipelineGlobalSettings.asset.meta",
         "UnityProject/steam_appid.txt",
         # The folder metas (AddressableAssetsData.meta, Scenes.meta, ...).  They were missing from
         # the first run of this split: 13 files that no other group matched, which the tree-hash
         # comparison at the end of main() caught.
         "UnityProject/Assets/*.meta"],
    ),
]


def git(*args, check=True):
    proc = subprocess.run(["git"] + list(args), cwd=REPO, capture_output=True, text=True,
                          encoding="utf-8", errors="replace")
    if check and proc.returncode != 0:
        raise SystemExit("git %s failed (%d):\n%s\n%s"
                         % (" ".join(args), proc.returncode, proc.stdout, proc.stderr))
    return proc.stdout


def main():
    ap = argparse.ArgumentParser()
    ap.add_argument("--dry-run", action="store_true")
    args = ap.parse_args()

    head = git("rev-parse", "HEAD").strip()
    if head != SPLIT_HEAD:
        raise SystemExit("HEAD is %s, expected the commit to split (%s)" % (head[:10], SPLIT_HEAD[:10]))
    parent = git("rev-parse", "--short", "HEAD~1").strip()
    if not parent.startswith(EXPECTED_PARENT[:7]):
        raise SystemExit("parent is %s, expected %s" % (parent, EXPECTED_PARENT))

    committed = [p for p in git("show", "--name-only", "--format=", head).splitlines() if p.strip()]
    original_tree = git("rev-parse", "HEAD^{tree}").strip()
    print("splitting %s (%d files) on top of %s" % (head[:10], len(committed), parent))
    print("original tree %s" % original_tree[:12])
    if args.dry_run:
        print("dry run; stopping before the reset")
        return 0

    # --mixed reset: unstage everything, leave the working tree untouched.
    git("reset", "--mixed", "HEAD~1")
    os.makedirs(MSG_DIR, exist_ok=True)

    staged_union = []
    for index, (title, body, specs) in enumerate(GROUPS, start=1):
        paths = []
        for spec in specs:
            if any(ch in spec for ch in "*?["):
                paths.extend(sorted(glob.glob(os.path.join(REPO, spec))))
                paths = [os.path.relpath(p, REPO).replace("\\", "/") for p in paths]
            else:
                paths.append(spec)
        existing = [p for p in paths if os.path.exists(os.path.join(REPO, p))]
        git("add", "--", *existing)
        added = [p for p in git("diff", "--cached", "--name-only").splitlines() if p.strip()]
        if not added:
            raise SystemExit("group %d (%s) staged nothing" % (index, title))
        msg_path = os.path.join(MSG_DIR, "%02d.txt" % index)
        with io.open(msg_path, "w", encoding="utf-8", newline="\n") as fh:
            fh.write(title + "\n\n" + body.rstrip() + "\n")
        git("commit", "-F", msg_path, "--quiet")
        staged_union.extend(added)
        print("  %d/%d  %-46s %5d files" % (index, len(GROUPS), title, len(added)))

    # The split must reproduce the original content exactly.
    new_tree = git("rev-parse", "HEAD^{tree}").strip()
    print()
    print("new tree      %s" % new_tree[:12])
    if new_tree != original_tree:
        raise SystemExit("TREE MISMATCH: the split does not reproduce the original content")
    print("tree matches the original commit exactly")

    duplicates = len(staged_union) - len(set(staged_union))
    print("files committed across the groups: %d (duplicates: %d, original: %d)"
          % (len(staged_union), duplicates, len(committed)))
    leftover = git("status", "--porcelain").splitlines()
    print("working tree leftovers: %d" % len([l for l in leftover if l.strip()]))
    print()
    return 0


if __name__ == "__main__":
    sys.exit(main())
