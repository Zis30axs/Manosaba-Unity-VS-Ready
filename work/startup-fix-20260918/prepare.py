from pathlib import Path
import hashlib, json, difflib

ROOT = Path(__file__).resolve().parents[2]
OUT = Path(__file__).resolve().parent
FILES = ['UnityProject/Assets/Scripts/Game/WitchTrials.Game/WitchTrials.Views/LocalizeImage.cs',
         'UnityProject/Assets/Scripts/Runtime/ManosabaBoot.cs']
manifest = {}
for rel in FILES:
    original = (ROOT / rel).read_bytes()
    for role in ('baseline', 'modified'):
        dest = OUT / role / rel
        dest.parent.mkdir(parents=True, exist_ok=True)
        dest.write_bytes(original)
    manifest[rel] = hashlib.sha256(original).hexdigest()

p = OUT / 'modified' / FILES[0]
s = p.read_bytes().decode('utf-8')
old = '\t\t_disposable = LocalizationProvider.OnLocaleChangedAsObservable()'
assert s.count(old) == 1
s = s.replace(old, '''\t\t// Reset is editor-only initialization; reconstructed/runtime-created prefabs may
    \t\t// have no serialized Image reference. Resolve it before subscribing (a replaying
    \t\t// observable can call ModifyImageByLocale immediately).
    \t\tif (_image == null) _image = GetComponent<Image>();
    \t\tif (_image == null)
    \t\t{
    \t\t\tDebug.LogError("LocalizeImage requires an Image component.", this);
    \t\t\treturn;
    \t\t}
    \t\t_disposable = LocalizationProvider.OnLocaleChangedAsObservable()'''.replace('\n', '\r\n'))
s = s.replace('_image.overrideSprite = null;', 'if (_image != null) _image.overrideSprite = null;')
p.write_bytes(s.encode('utf-8'))

p = OUT / 'modified' / FILES[1]
s = p.read_bytes().decode('utf-8')
newline = '\r\n' if '\r\n' in s else '\n'
s = s.replace('\r\n', '\n').replace('\n', '\r\n')
needle = '            var probe = new CatalogProbe { path = catalogPath };\r\n'
assert s.count(needle) == 1
s = s.replace(needle, needle + '''            // Only the shipped reference catalog retains the pre-reconstruction name.
            // Keep this alias scoped to its synchronous probe, not all runtime loads.
            ResolveEventHandler resolveLegacyAssembly = (sender, args) =>
                new AssemblyName(args.Name).Name == "Assembly-CSharp"
                    ? typeof(WitchTrials.Views.GameInitializer).Assembly : null;
            AppDomain.CurrentDomain.AssemblyResolve += resolveLegacyAssembly;
'''.replace('\n', '\r\n'))
needle = '            return probe;\r\n        }\r\n\r\n        private static bool HasFlag'
assert s.count(needle) == 1
s = s.replace(needle, '''            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= resolveLegacyAssembly;
            }
            return probe;
        }

        private static bool HasFlag'''.replace('\n', '\r\n'))
p.write_bytes(s.replace('\r\n', newline).encode('utf-8'))

patch = ''
for rel in FILES:
    patch += ''.join(difflib.unified_diff((OUT/'baseline'/rel).read_text().splitlines(True),
                                        (OUT/'modified'/rel).read_text().splitlines(True),
                                        fromfile='a/'+rel, tofile='b/'+rel))
(OUT/'DIFF_FILE.patch').write_text(patch, encoding='utf-8', newline='\n')
(OUT/'manifest.json').write_text(json.dumps(manifest, indent=2), encoding='utf-8')
# Self-contained rollback embeds pristine bytes; accepts a directory containing the two paths.
import base64
lines = ['#!/bin/sh', 'set -eu', 'test "$#" -eq 1 || { echo "usage: ROLLBACK.sh TARGET_COPY_DIRECTORY" >&2; exit 2; }',
         'root=$1', 'test -d "$root" || { echo "target directory missing" >&2; exit 2; }']
for i, rel in enumerate(FILES):
    encoded = base64.b64encode((OUT/'baseline'/rel).read_bytes()).decode()
    lines += [f'mkdir -p "$root/{Path(rel).parent.as_posix()}"',
              f'base64 -d > "$root/{rel}" <<\'BASELINE_{i}\'', encoded, f'BASELINE_{i}']
(OUT/'ROLLBACK.sh').write_text('\n'.join(lines)+'\n', encoding='utf-8', newline='\n')
(OUT/'ROLLBACK.sh').chmod(0o755)
print(json.dumps(manifest, indent=2))
