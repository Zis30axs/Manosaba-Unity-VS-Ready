import re, os, json

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
log = []


def patch(path, old, new, note):
    p = os.path.join(DEC, path)
    t = open(p, encoding="utf-8-sig").read()
    if old not in t:
        print("SKIP (pattern not found):", path, note)
        return
    t = t.replace(old, new, 1)
    open(p, "w", encoding="utf-8-sig").write(t)
    log.append({"file": path, "fix": note})
    print("patched", path, "-", note)


# 1) UnityParsing: move the out-parameter assignment into the right method
patch(r"CustomInspector.Helpers\CustomInspector.Extensions\UnityParsing.cs",
      """    public static bool IsColor(string s, out Color parsedColor)
    {
            return default(bool);
    }""",
      """    public static bool IsColor(string s, out Color parsedColor)
    {
            parsedColor = default(Color);
            return default(bool);
    }""", "assign-out-param (correct method)")
t = open(os.path.join(DEC, r"CustomInspector.Helpers\CustomInspector.Extensions\UnityParsing.cs"), encoding="utf-8-sig").read()
t = t.replace("                parsedColor = default;\n                parsedColor = default;\n", "")
open(os.path.join(DEC, r"CustomInspector.Helpers\CustomInspector.Extensions\UnityParsing.cs"), "w", encoding="utf-8-sig").write(t)
log.append({"file": r"CustomInspector.Helpers\CustomInspector.Extensions\UnityParsing.cs",
            "fix": "remove misplaced assignments"})

# 2) PooledObject: restore the generic constraint that was cut with the interface list
patch(r"Naninovel.Common\Naninovel\PooledObject.cs",
      "public readonly struct PooledObject<T> {",
      "public readonly struct PooledObject<T> where T : class\n{",
      "restore generic constraint")

# 3) ServiceLocator.TryGet: assign the out parameter
patch(r"GigaCreation.Tools.Service\GigaCreation.Tools.Service\ServiceLocator.cs",
      """    public static bool TryGet<TService>(out TService service, bool includeDerivedType = false) where TService : class, IService
    {
            return default(bool);
    }""",
      """    public static bool TryGet<TService>(out TService service, bool includeDerivedType = false) where TService : class, IService
    {
            service = null;
            return default(bool);
    }""", "assign-out-param")

# 4) UniTaskHelper: [Optional] + default value is illegal -> drop the attribute
for f in [r"GigaCreation.Tools.UniTasks\GigaCreation.Tools.UniTasks\UniTaskHelper.cs"]:
    p = os.path.join(DEC, f)
    t = open(p, encoding="utf-8-sig").read()
    n = t.count("[Optional] CancellationToken ct = default)")
    t = t.replace("[Optional] CancellationToken ct = default)", "CancellationToken ct = default)")
    open(p, "w", encoding="utf-8-sig").write(t)
    log.append({"file": f, "fix": "drop [Optional] attribute", "count": n})
    print("patched", f, "- drop [Optional] x", n)

# 5) find any stray 'sservice = default;' inserted by the automatic patcher
for root, dirs, files in os.walk(DEC):
    for fn in files:
        if not fn.endswith(".cs"):
            continue
        p = os.path.join(root, fn)
        t = open(p, encoding="utf-8-sig").read()
        if "sservice" in t:
            t2 = "\n".join(l for l in t.split("\n") if "sservice" not in l)
            open(p, "w", encoding="utf-8-sig").write(t2)
            print("cleaned stray line in", p)
            log.append({"file": p, "fix": "remove stray 'sservice' line"})

rep = os.path.join(ROOT, "work", "patches.json")
old = json.load(open(rep, encoding="utf-8")) if os.path.exists(rep) else []
old.extend(log)
json.dump(old, open(rep, "w", encoding="utf-8"), ensure_ascii=False, indent=1)
print("total recorded patches:", len(old))
