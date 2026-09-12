"""Rank WitchTrials.* methods as reconstruction candidates.

Corrects for IL2CPP body-folding: identical trivial bodies (one-line getters
/setters) are emitted once and shared by many managed methods, which inflates
naive in-degree. Aliased bodies are split out into their own batch.
"""
import sys, os, pickle, json, re
HERE = os.path.dirname(os.path.abspath(__file__))
sys.path.insert(0, HERE)
from il2cpp_pe import PE
from il2cpp_db import build as build_db, EMPTY_THUNK

pe = PE(); db = build_db()
facts = pickle.load(open(os.path.join(HERE, 'facts.pkl'), 'rb'))
size, callers, calls = facts['size'], facts['callers'], facts['calls']
nbranch, strs, minfo = facts['nbranch'], facts['strs'], facts['minfo']

GAME_NS = 'WitchTrials.'
game_methods = []
for full, t in db.types.items():
    if not full.startswith(GAME_NS):
        continue
    for rva, sig in t['methods']:
        game_methods.append((rva, full, sig))
game_rvas = set(r for r, _, _ in game_methods)

# How many *managed* methods (project-wide, all 18k types) share each body?
alias_count = {rva: len(v) for rva, v in db.rva_owner.items()}

SYSTEMS = [
 ('Boot/Init',      r'GameInitializer|SplashScreen|EngineInitialization|EntrySequence|SteamManager|DefaultLocale'),
 ('TrialState',     r'TrialStateManager|ChangeTrialMode|ChangeTrialPauseMode|SetTrialTimer'),
 ('PerspectiveStage', r'PerspectiveStage|StageCamera|StageScreen|SplitPerspective|ModifyPerspective|LoadPerspective|UnloadPerspective|HidePerspective|ShowPerspective|ShakePerspective'),
 ('Naninovel-glue', r'WitchTrialsNaninovel|WitchTrialsScriptPlayer|WitchTrialsChoiceHandler|ScriptHelper|ScenarioInfo|Gosub'),
 ('SaveLoad',       r'SaveLoadUi|QuickSave|QuickLoad|OpenLoadUi'),
 ('WitchBook/Data', r'WitchBook|ClueData|RuleData|NoteData|ProfileData|MapData|CharacterData|AuthorData|MasterData|Versioned|SpecialThanksData'),
 ('Debate/Court',   r'Debate|Court|Objection|Execution|Damage|Evidence'),
 ('Map',            r'MapArea|MapPage|MapFloor|ChoiceMapArea|SpawnableMap'),
 ('Input',          r'Input'),
 ('Audio',          r'Audio|Sfx|SoundTest'),
 ('Credits/Gallery',r'Credit|Gallery|Still|SpecialThanks'),
 ('Debug',          r'Debug|SRDebugger|Slack|Console'),
 ('UI',             r'Ui$|UiModified|Button|Panel|Page|Toggle|Switcher|Grid|Slot|Display|Label|Printer'),
]
def system_of(typefull, sig):
    short = typefull.rsplit('.', 1)[-1]
    for name, pat in SYSTEMS:
        if re.search(pat, short) or re.search(pat, sig):
            return name
    return 'Other'

LIFECYCLE = re.compile(r'\b(Awake|Start|Update|LateUpdate|FixedUpdate|OnEnable|OnDisable|OnDestroy)\s*\(')

rows, aliased = [], []
seen = set()
for rva, full, sig in game_methods:
    if rva == EMPTY_THUNK:
        continue
    al = alias_count.get(rva, 1)
    sz = size.get(rva, 0)
    nb = nbranch.get(rva, 0)
    # in-degree: call sites targeting this body, divided across its aliases
    raw_in = len(callers.get(rva, ()))
    outs = calls.get(rva, set())
    out_game = len([t for t in outs if t in game_rvas and t != EMPTY_THUNK])
    r = dict(rva=rva, type=full, sig=sig, size=sz, nbranch=nb, alias=al,
             indeg=raw_in, outgame=out_game, nstr=len(strs.get(rva, ())),
             nvirt=len(minfo.get(rva, ())), system=system_of(full, sig),
             lifecycle=bool(LIFECYCLE.search(sig)))
    if al > 1 and sz <= 0x40:
        aliased.append(r); continue
    if rva in seen:
        continue
    seen.add(rva)
    rows.append(r)

def difficulty(r):
    s, b = r['size'], r['nbranch']
    if s <= 0x30 and b <= 1:   return 'trivial'
    if s <= 0x120 and b <= 6:  return 'easy'
    if s <= 0x400 and b <= 20: return 'medium'
    if s <= 0xA00:             return 'hard'
    return 'very hard'

for r in rows:
    r['diff'] = difficulty(r)
    r['value'] = (min(r['indeg'], 40) * 3 + r['outgame'] * 5 + r['nstr'] * 3
                  + r['nvirt'] * 2 + (20 if r['lifecycle'] else 0)
                  + min(r['nbranch'], 30))

rows.sort(key=lambda r: -r['value'])
json.dump(dict(rows=rows, aliased=aliased), open(os.path.join(HERE, 'ranked.json'), 'w'), indent=1)

print("unique-body game methods: %d   folded trivial accessors: %d" % (len(rows), len(aliased)))
from collections import Counter
print("\n== unique bodies by subsystem ==")
c = Counter(r['system'] for r in rows); b = Counter()
for r in rows: b[r['system']] += r['size']
for k, v in c.most_common():
    print("  %-18s %4d methods  %7d bytes" % (k, v, b[k]))

print("\n== TOP 50 by reconstruction value (unique bodies only) ==")
print("%-4s %-9s %-17s %-6s %-4s %-4s %-4s %-4s %-4s %-9s %s" % (
    "#", "RVA", "system", "size", "br", "in", "out", "str", "vir", "difficulty", "method"))
for i, r in enumerate(rows[:50], 1):
    short = r['type'].rsplit('.', 1)[-1]
    nm = re.sub(r'^(public|private|protected|internal|static|override|virtual|sealed|abstract|\s)+', '', r['sig'])
    print("%-4d 0x%-7X %-17s 0x%-4X %-4d %-4d %-4d %-4d %-4d %-9s %s::%s" % (
        i, r['rva'], r['system'], r['size'], r['nbranch'], r['indeg'],
        r['outgame'], r['nstr'], r['nvirt'], r['diff'], short, nm[:62]))
