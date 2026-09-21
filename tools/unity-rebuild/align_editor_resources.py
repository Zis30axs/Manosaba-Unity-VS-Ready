"""Point Naninovel's EditorResources records at the game's own prefabs where the shipped build overrode a built-in.

    python tools/unity-rebuild/align_editor_resources.py            # dry run: report only
    python tools/unity-rebuild/align_editor_resources.py --apply    # rewrite the guid lines

Why: Naninovel keeps its built-in resources (default UIs, spawn FX, printers, choice handlers) in
Assets/NaninovelData/EditorResources.asset as `name / pathPrefix / guid` records.  In the Editor these records are served
before any other provider, and at build time Naninovel publishes each of them to Addressables as
`Naninovel/<pathPrefix>/<name>`.  A game that overrides a built-in (the game's TitleUI replaces Naninovel's TitleMenu)
does so by pointing that record at its own prefab -- which is why the shipped catalog maps `Naninovel/UI/TitleUI` to
Assets/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/TitleUI.prefab.  The EditorResources asset of this reconstruction was
created fresh by the package, so every record still pointed at the package default: in Play Mode `@showUI TitleUI` showed
Naninovel.UI.TitleMenu, whose ChangeVisibility replays the title script, and System_Title restarted every ~3.2 s instead of
reaching @stop (G4b round 8, 2026-09-21).

Rule: a record is repointed only when the shipped address `Naninovel/<pathPrefix>/<name>` resolves (addressables plan,
i.e. the shipped catalog's own address) to a project asset OUTSIDE the Naninovel package; records whose shipped asset is
the package's own prefab are left alone.  Everything is reported; nothing else in the file changes.
"""
import json
import os
import re
import sys

REPO = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", ".."))
PROJECT = os.path.join(REPO, "UnityProject")
ASSET = os.path.join(PROJECT, "Assets", "NaninovelData", "EditorResources.asset")
PLAN = os.path.join(REPO, "tools", "unity-rebuild", "out", "unity", "addressables_plan_all.json")
RECORD = re.compile(r"^(\s*- name: )(.*)\r?\n(\s*pathPrefix: )(.*)\r?\n(\s*guid: )([0-9a-f]*)", re.M)


def meta_guid(asset_path):
    path = os.path.join(PROJECT, asset_path.replace("/", os.sep) + ".meta")
    if not os.path.exists(path):
        return None
    with open(path, encoding="utf-8", errors="replace") as fh:
        m = re.search(r"^guid: ([0-9a-f]+)", fh.read(), re.M)
    return m.group(1) if m else None


def main():
    apply = "--apply" in sys.argv
    with open(PLAN, encoding="utf-8") as fh:
        plan = json.load(fh)
    by_address = {}
    for entry in plan["entries"]:
        if entry.get("status") == "excluded":
            continue
        by_address.setdefault(entry["address"], []).append(entry)

    raw = open(ASSET, "rb").read()
    text = raw.decode("utf-8-sig")
    changes, kept, unmapped = [], [], []

    def repoint(m):
        name, prefix, guid = m.group(2).strip(), m.group(4).strip(), m.group(6)
        address = "Naninovel/%s/%s" % (prefix, name)
        candidates = [e for e in by_address.get(address, [])
                      if e["asset_path"].endswith((".prefab", ".asset"))]
        if not candidates:
            unmapped.append(address)
            return m.group(0)
        game = [e for e in candidates if not e["container_path"].startswith("Packages/com.elringus.naninovel/")]
        if not game:
            kept.append(address)
            return m.group(0)
        if len(game) > 1:
            raise SystemExit("ambiguous shipped asset for %s: %s" % (address, [e["asset_path"] for e in game]))
        target = meta_guid(game[0]["asset_path"])
        if not target:
            raise SystemExit("no .meta guid for %s" % game[0]["asset_path"])
        if target == guid:
            kept.append(address)
            return m.group(0)
        changes.append((address, guid, target, game[0]["asset_path"]))
        return m.group(1) + m.group(2) + m.group(0)[len(m.group(1)) + len(m.group(2)):m.start(6) - m.start(0)] + target

    new_text = RECORD.sub(repoint, text)
    print("records: %d repointed, %d already right or package-owned, %d without a shipped address"
          % (len(changes), len(kept), len(unmapped)))
    for address, old, new, path in changes:
        print("  %-36s %s -> %s  (%s)" % (address, old, new, path))
    if apply and changes:
        out = new_text.encode("utf-8")
        if raw[:3] == b"\xef\xbb\xbf":
            out = b"\xef\xbb\xbf" + out
        open(ASSET, "wb").write(out)
        print("written:", ASSET)
    elif changes:
        print("dry run; re-run with --apply to rewrite")


if __name__ == "__main__":
    main()
