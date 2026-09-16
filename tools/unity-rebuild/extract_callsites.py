#!/usr/bin/env python3
"""Extract call sites of Naninovel.Engine and AddressablesManager members from src/decompiled.

Read-only analysis. Emits tools/unity-rebuild/out/engine_callsites.csv.
"""
import csv
import os
import re
import sys

REPO = r"E:\Manosaba-VSCodeReady"
SRC = os.path.join(REPO, "src", "decompiled")
OUT = os.path.join(REPO, "tools", "unity-rebuild", "out", "engine_callsites.csv")

NANI = "Elringus.Naninovel.Runtime"
ADDR = "GigaCreation.Essentials.AddressablesUtils"

# (member, regex) for Naninovel.Engine. The receiver must be `Engine` (optionally namespace-qualified).
ENGINE_MEMBERS = [
    ("Initialize",                          r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*Initialize\s*\("),
    ("GetService<TService>()",              r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*GetService\s*<[^>]*>\s*\(\s*\)"),
    ("GetService(Type)",                    r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*GetService\s*\(\s*[^)\s]"),
    ("GetService<TService>(string) [NOT-PRESENT-IN-API]",
                                            r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*GetService\s*<[^>]*>\s*\(\s*[A-Za-z_\x22]"),
    ("GetServiceOrErr<TService>()",         r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*GetServiceOrErr\s*<"),
    ("TryGetService<TService>(out)",        r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*TryGetService\s*[<(]"),
    ("GetConfiguration<T>()",               r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*GetConfiguration\s*<"),
    ("GetConfiguration(Type)",              r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*GetConfiguration\s*\(\s*[^)\s]"),
    ("FindService<TService>(Predicate<TService>)",
                                            r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*FindService\s*<"),
    ("FindAllServices<TService>(ICollection<TService>)",
                                            r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*FindAllServices\s*<"),
    ("AddPreInitializationTask",            r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*AddPreInitializationTask\s*\("),
    ("RemovePreInitializationTask",         r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*RemovePreInitializationTask\s*\("),
    ("AddPostInitializationTask",           r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*AddPostInitializationTask\s*\("),
    ("RemovePostInitializationTask",        r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*RemovePostInitializationTask\s*\("),
    ("Reset()/Reset(exclude)",              r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*Reset\s*\("),
    ("Destroy()",                           r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*Destroy\s*\(\s*\)"),
    ("Instantiate<T>",                      r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*Instantiate\s*[<(]"),
    ("CreateObject",                        r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*CreateObject\s*[<(]"),
    ("FindObject",                          r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*FindObject\s*\("),
    ("LoadInternalResource<T>",             r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*LoadInternalResource\s*<"),
    ("UseLogger",                           r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*UseLogger\s*\("),
    ("Log(string, PlaybackSpot?)",          r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*Log\s*\("),
    ("Warn(string, PlaybackSpot?)",         r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*Warn\s*\("),
    ("Err(string, PlaybackSpot?)",          r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*Err\s*\("),
    ("Types",                               r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*Types\b"),
    ("Configuration",                       r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*Configuration\b"),
    ("Behaviour",                           r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*Behaviour\b"),
    ("Time",                                r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*Time\b"),
    ("Services",                            r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*Services\b"),
    ("RootObject",                          r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*RootObject\b"),
    ("Initialized",                         r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*Initialized\b"),
    ("Initializing",                        r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*Initializing\b"),
    ("DestroyToken",                        r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*DestroyToken\b"),
    ("EditorTransientRoot",                 r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*EditorTransientRoot\b"),
    ("event OnInitializationStarted",       r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*OnInitializationStarted\b"),
    ("event OnInitializationFinished",      r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*OnInitializationFinished\b"),
    ("event OnInitializationProgress",      r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*OnInitializationProgress\b"),
    ("event OnDestroyed",                   r"(?<![\w.])(?:Naninovel\s*\.\s*)?Engine\s*\.\s*OnDestroyed\b"),
]

# AddressablesManager members. Receiver is recorded so interface dispatch is visible.
ADDR_MEMBERS = [
    "GetOrLoadAddressableAsset",
    "GetResourceLocationsAsync",
    "CheckIfMainCatalogExistsAsync",
    "CheckIfAdditionalCatalogExistsAsync",
    "LoadAdditionalCatalogAsync",
    "IsAdditionalCatalogLoaded",
    "ResourceLocations",
    "ReleaseHandles",
    "BuildAdditionalCatalogLocationsWithoutExt",
    "WebRequestManager",
    "LoadAssetCts",
]

ADDR_RECEIVER = re.compile(
    r"(?<![\w.])([A-Za-z_][\w.]*)\s*\.\s*(" + "|".join(ADDR_MEMBERS) + r")\b"
)

# Bare (unqualified) uses of these members, which only make sense inside AddressablesManager itself.
ADDR_BARE = re.compile(r"(?<![\w.])(?<!\.)(" + "|".join(ADDR_MEMBERS) + r")\b")
ADDR_SELF_FILES = ("AddressablesManager.cs", "AddressablesManagerRemoteSupported.cs")

# Receiver texts that are actually NAMESPACES (e.g. UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation).
NAMESPACE_RECEIVERS = {
    "UnityEngine.ResourceManagement",
    "UnityEngine.ResourceManagement.ResourceLocations",
    "UnityEngine",
    "UnityEngine.AddressableAssets",
    "System.Collections.Generic",
    "GigaCreation.Essentials.AddressablesUtils",
    "Naninovel",
}

# Interface that a receiver expression most likely resolves to, keyed by receiver text.
RECEIVER_TYPE_HINTS = {
    "AddressablesManager": "GigaCreation.Essentials.AddressablesUtils.AddressablesManager",
    "IAddressablesManager": "GigaCreation.Essentials.AddressablesUtils.IAddressablesManager",
    "IAddressableAssetLoader": "GigaCreation.Essentials.AddressablesUtils.IAddressableAssetLoader",
    "IAddressableResourceLocationProvider": "GigaCreation.Essentials.AddressablesUtils.IAddressableResourceLocationProvider",
    "IContentCatalogManager": "GigaCreation.Essentials.AddressablesUtils.IContentCatalogManager",
    "_addressablesManager": "GigaCreation.Essentials.AddressablesUtils.IAddressablesManager",
    "AddressablesManagerRemoteSupported": "GigaCreation.Essentials.AddressablesUtils.AddressablesManagerRemoteSupported",
    "AddressableAssetLoader": "GigaCreation.Essentials.AddressablesUtils.IAddressableAssetLoader",
    "_addressableAssetLoader": "GigaCreation.Essentials.AddressablesUtils.IAddressableAssetLoader",
    "AddressableResourceLocationProvider": "GigaCreation.Essentials.AddressablesUtils.IAddressableResourceLocationProvider",
    "_addressableResourceLocationProvider": "GigaCreation.Essentials.AddressablesUtils.IAddressableResourceLocationProvider",
    "ContentCatalogManager": "GigaCreation.Essentials.AddressablesUtils.IContentCatalogManager",
    "_contentCatalogManager": "GigaCreation.Essentials.AddressablesUtils.IContentCatalogManager",
    "WebRequestManager": "GigaCreation.Essentials.WebRequestUtils.IWebRequestManager",
    "_webRequestManager": "GigaCreation.Essentials.WebRequestUtils.IWebRequestManager",
    "_loadAssetCts": "System.Threading.CancellationTokenSource",
    "_handles": "System.Collections.Generic.IList<AsyncOperationHandle>",
    "_loadedAssets": "System.Collections.Generic.IDictionary<string, object>",
    "_pendingLoadAssetTasks": "System.Collections.Generic.IDictionary<string, UniTask>",
    "_additionalCatalogLocationsWithoutExt": "System.Collections.Generic.IReadOnlyList<string>",
    "ResourceLocations": "GigaCreation.Essentials.AddressablesUtils.AddressablesManager",
}

TYPE_DECL = re.compile(
    r"^(?P<mods>(?:\[[^\]]*\]\s*)*"
    r"(?:(?:public|private|protected|internal|static|sealed|abstract|partial|readonly|unsafe|new)\s+)*)"
    r"(?P<kind>class|struct|interface|enum)\s+(?P<name>[A-Za-z_][\w]*(?:<[^>]*>)?)"
)
CONTROL = {
    "if", "else", "for", "foreach", "while", "do", "switch", "try", "catch",
    "finally", "using", "lock", "fixed", "unsafe", "return", "get", "set",
    "add", "remove", "where", "sizeof", "typeof", "nameof", "checked",
    "unchecked", "new", "stackalloc", "await", "yield", "case", "default",
}
METHOD_DECL = re.compile(r"^(?P<pre>.*?)(?P<name>[A-Za-z_]\w*)\s*(?:<[^<>]*(?:<[^<>]*>)?[^<>]*>)?\s*\([^()]*\)\s*$")


def declaration_name(stripped: str):
    """Return (kind, name) for a scope-opening declaration line, else None."""
    if not stripped or stripped.startswith("//"):
        return None
    m = TYPE_DECL.match(stripped)
    if m:
        return ("type", m.group("name"))
    if stripped.endswith(";") or stripped.endswith("}"):
        return None
    if "(" not in stripped or ")" not in stripped:
        return None
    head = stripped.split("(", 1)[0].strip()
    if head.startswith("[") or "=" in head:
        return None
    m = METHOD_DECL.match(stripped)
    if not m:
        return None
    name = m.group("name")
    if name in CONTROL:
        return ("block", "?")
    # property accessors and constructors are still useful symbols
    return ("member", name)


DECL_MOD = re.compile(r"^\s*(?:\[[^\]]*\]\s*)*(?:public|protected|private|internal|static|virtual|override|abstract|sealed|new|async|extern|unsafe|readonly)\b")

# Only these files are allowed to contribute DECLARATION rows, so the CSV does not fill up
# with unrelated `Initialize`/`Reset` methods from the rest of the code base.
DECL_FILES = {
    "Engine.cs",
    "AddressablesManager.cs",
    "AddressablesManagerRemoteSupported.cs",
    "IAddressableAssetLoader.cs",
    "IAddressableResourceLocationProvider.cs",
    "IContentCatalogManager.cs",
    "IAddressablesManager.cs",
    "IAddressablesManagerRemoteSupported.cs",
}


def declaration_member(line: str, member: str) -> bool:
    """True when this line DECLARES `member` (rather than calling it)."""
    if not DECL_MOD.match(line):
        return False
    if not re.search(r"(?<![\w.])" + re.escape(member) + r"\s*[<(]", line):
        # property / field declarations: `public Foo Member {` or `public Foo Member;`
        if not re.search(r"(?<![\w.])" + re.escape(member) + r"\s*(?:\{|;|=)", line):
            return False
    return True


def scan_file(path: str):
    with open(path, "r", encoding="utf-8", errors="replace") as fh:
        raw_lines = fh.read().splitlines()

    scopes = []          # list of (depth, kind, name)
    pending = []         # (index, kind, name) awaiting the '{' on the next non-blank line
    depth = 0
    namespace = ""

    rel = os.path.relpath(path, REPO).replace("\\", "/")

    rows_by_line = {}
    decl_lines = set()

    def symbol():
        parts = [n for (_, k, n) in scopes if k in ("type", "member") and n != "?"]
        return ".".join(parts) if parts else "<global>"

    def emit(line_no, line, member, callee_type, callee_asm, notes):
        key = (line_no, member, callee_type)
        if key in rows_by_line:
            return
        rows_by_line[key] = {
            "callee_assembly": callee_asm,
            "callee_type": callee_type,
            "callee_member": member,
            "caller_file": rel,
            "caller_line": line_no,
            "caller_symbol": (namespace + "." if namespace else "") + symbol(),
            "notes": notes,
        }

    for i, line in enumerate(raw_lines):
        stripped = line.strip()
        line_no = i + 1

        if stripped.startswith("namespace "):
            namespace = stripped[len("namespace "):].rstrip(";{ ").strip()

        is_comment = stripped.startswith("//") or stripped.startswith("*") or stripped.startswith("/*")
        cnote = "DOC-COMMENT (documentation reference, no call executed here); " if is_comment else ""

        # 0. declaration sites (the reconstructed stub headers themselves)
        if not is_comment and os.path.basename(path) in DECL_FILES:
            for member, _rx in ENGINE_MEMBERS:
                base = member.split("(")[0].split("<")[0].replace("event ", "")
                if declaration_member(line, base) or (
                        DECL_MOD.match(line) and re.search(r"(?<![\w.])" + re.escape(base) + r"\s*$", line)):
                    decl_lines.add((line_no, member))
                    emit(line_no, line, member, "Naninovel.Engine", NANI,
                         "DECLARATION site (reconstructed stub)")
            for member in ADDR_MEMBERS:
                if declaration_member(line, member) or (
                        DECL_MOD.match(line) and re.search(r"(?<![\w.])" + re.escape(member) + r"\s*$", line)):
                    decl_type = ("GigaCreation.Essentials.AddressesablesUtils.AddressablesManager"
                                 if os.path.basename(path) in ADDR_SELF_FILES
                                 else "GigaCreation.Essentials.AddressesablesUtils.<interface-or-manager>")
                    decl_type = decl_type.replace("Addressesables", "Addressables")
                    decl_lines.add((line_no, member))
                    emit(line_no, line, member, decl_type, ADDR,
                         "DECLARATION site (reconstructed stub)")

        # 1. record call sites on the ORIGINAL line before scope bookkeeping
        for member, rx in ENGINE_MEMBERS:
            if (line_no, member) in decl_lines:
                continue
            if re.search(rx, line):
                emit(line_no, line, member, "Naninovel.Engine", NANI,
                     cnote + "Naninovel.Engine static member")
        for m in ADDR_RECEIVER.finditer(line):
            recv, member = m.group(1), m.group(2)
            if recv in NAMESPACE_RECEIVERS:
                continue
            callee_type = RECEIVER_TYPE_HINTS.get(recv)
            if callee_type is None:
                callee_type = "UNKNOWN(receiver=%s)" % recv
            emit(line_no, line, member, callee_type, ADDR,
                 cnote + "receiver expression: %s" % recv)
        if os.path.basename(path) in ADDR_SELF_FILES:
            for m in ADDR_BARE.finditer(line):
                member = m.group(1)
                if (line_no, member) in decl_lines:
                    continue
                if re.search(r"\.\s*" + member + r"\b", line):
                    continue  # already handled as a qualified use above
                emit(line_no, line, member,
                     "GigaCreation.Essentials.AddressablesUtils.AddressablesManager", ADDR,
                     cnote + "unqualified (this.) use inside AddressablesManager")

        # 2. scope bookkeeping
        if stripped.startswith("}"):
            for _ in stripped:
                pass
            for ch in stripped:
                if ch == "}":
                    depth -= 1
                    while scopes and scopes[-1][0] >= depth:
                        scopes.pop()
            continue

        if stripped == "{":
            if pending and pending[-1][0] == i - 1:
                _, kind, name = pending.pop()
                scopes.append((depth, kind, name))
            else:
                scopes.append((depth, "block", "?"))
            pending = []
            depth += 1
            continue

        # look ahead for a brace line
        nxt = ""
        for j in range(i + 1, min(i + 4, len(raw_lines))):
            if raw_lines[j].strip():
                nxt = raw_lines[j].strip()
                break

        decl = declaration_name(stripped)
        if decl:
            kind, name = decl
            # signatures that end with ';' or are single-line bodies do not open a scope
            if "{" in stripped and stripped.endswith("{"):
                scopes.append((depth, kind, name))
                depth += 1
                pending = []
            elif nxt == "{":
                pending = [(i, kind, name)]
            else:
                pending = []

    return list(rows_by_line.values())


def main():
    rows = []
    for root, _dirs, files in os.walk(SRC):
        for fn in files:
            if not fn.endswith(".cs"):
                continue
            rows.extend(scan_file(os.path.join(root, fn)))

    rows.sort(key=lambda r: (r["callee_type"], r["callee_member"], r["caller_file"], r["caller_line"]))

    os.makedirs(os.path.dirname(OUT), exist_ok=True)
    cols = ["callee_assembly", "callee_type", "callee_member",
            "caller_file", "caller_line", "caller_symbol", "notes"]
    with open(OUT, "w", encoding="utf-8", newline="") as fh:
        w = csv.DictWriter(fh, fieldnames=cols)
        w.writeheader()
        for r in rows:
            w.writerow(r)

    print("rows:", len(rows))
    from collections import Counter
    c = Counter((r["callee_type"], r["callee_member"]) for r in rows)
    for k, v in sorted(c.items()):
        print("%4d  %s :: %s" % (v, k[0], k[1]))


if __name__ == "__main__":
    sys.exit(main())
