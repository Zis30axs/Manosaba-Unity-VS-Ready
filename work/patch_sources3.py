import os, json

ROOT = r"E:\manosaba_re"
DEC = os.path.join(ROOT, "src", "decompiled")
log = []


def patch(path, old, new, note):
    p = os.path.join(DEC, path)
    t = open(p, encoding="utf-8-sig").read()
    if old not in t:
        print("SKIP:", path, note)
        return
    open(p, "w", encoding="utf-8-sig", newline="").write(t.replace(old, new, 1))
    log.append({"file": path, "fix": note})
    print("patched", path, "-", note)


patch(r"CustomInspector.Helpers\CustomInspector.Extensions\UnityParsing.cs",
      "public static bool IsColor(string s, out Color parsedColor)\n\t{\n",
      "public static bool IsColor(string s, out Color parsedColor)\n\t{\n\t\tparsedColor = default(Color);\n",
      "assign out param")

patch(r"GigaCreation.Tools.Service\GigaCreation.Tools.Service\ServiceLocator.cs",
      "public static bool TryGet<TService>(out TService service, bool includeDerivedType = false) where TService : class, IService\n\t{\n",
      "public static bool TryGet<TService>(out TService service, bool includeDerivedType = false) where TService : class, IService\n\t{\n\t\t\tservice = null;\n",
      "assign out param")

rep = os.path.join(ROOT, "work", "patches.json")
old = json.load(open(rep, encoding="utf-8")) if os.path.exists(rep) else []
old.extend(log)
json.dump(old, open(rep, "w", encoding="utf-8"), ensure_ascii=False, indent=1)
print("recorded", len(log))
