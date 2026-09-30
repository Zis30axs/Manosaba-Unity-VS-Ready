// Bump on every deploy to invalidate stale browser caches of JSON/PNG assets.
// Also bump the matching ?v= on styles.css and app.js in index.html.
const BUILD_VERSION = '20260717a';
const assetUrl = (path) => `${path}?v=${BUILD_VERSION}`;

// IndexedDB-backed snapshot store shared with the scene editor. Dynamic
// import so the BUILD_VERSION query string busts module-script caches the
// same way the static asset URLs do.
const { snapshotPut, snapshotChannel } =
  await import(`./snapshot_store.js?v=${BUILD_VERSION}`);

const CHARACTERS = [
  // Layered main cast
  "Alisa","AnAn","Coco","Ema","Hanna","Hiro",
  "Leia","Margo","Meruru","Miria","Nanoka","Noah","Sherry",
  // Diced NPCs
  "Warden","Yuki","JailerA","JailerB","JailerC",
  // Diced creature variants
  "CreatureAlisa","CreatureAnAn","CreatureCoco","CreatureEma","CreatureHanna",
  "CreatureHiro","CreatureLeia","CreatureMargo","CreatureMeruru","CreatureMiria",
  "CreatureNanoka","CreatureNoah","CreatureSherry",
];
// Naninovel diced atlases — full-frame poses, no layered rig. Loader takes a
// separate path: meta.json instead of layers.json/compositions.json/default.json,
// and a single Pose group instead of head/expression/layer panels.
const DICED_CHARACTERS = new Set([
  "Warden","Yuki","JailerA","JailerB","JailerC",
  "CreatureAlisa","CreatureAnAn","CreatureCoco","CreatureEma","CreatureHanna",
  "CreatureHiro","CreatureLeia","CreatureMargo","CreatureMeruru","CreatureMiria",
  "CreatureNanoka","CreatureNoah","CreatureSherry",
]);
// Characters hidden behind the spoiler toggle (story-spoiler NPCs/creatures).
// Kept separate from DICED_CHARACTERS even though they overlap today: a future
// layered character could be a spoiler, or a future diced character could be
// safe to show by default. Conflating the two would break that day.
const SPOILER_CHARACTERS = new Set([
  "Warden","Yuki","JailerA","JailerB","JailerC",
  "CreatureAlisa","CreatureAnAn","CreatureCoco","CreatureEma","CreatureHanna",
  "CreatureHiro","CreatureLeia","CreatureMargo","CreatureMeruru","CreatureMiria",
  "CreatureNanoka","CreatureNoah","CreatureSherry",
]);
// Initial selection on load; also the fallback when the active character
// disappears from the dropdown (e.g. spoilers toggled off while viewing one).
const DEFAULT_CHARACTER = "Sherry";

let currentChar = null;
let charType = 'layered';  // 'layered' | 'diced'
let layersInfo = [];       // layered: from layers.json
let compositions = {};     // layered: from compositions.json
let defaultEnabled = [];   // layered: resolved default list for current active head
let defaultJsonRaw = {};   // layered: raw default.json (may be flat or per-head)
let headBases = [];        // layered: e.g. ['Head01', 'Head02'] if multi-head
let activeHead = null;     // layered: currently active head base name
let activeState = {};      // layered: { layerName: true/false }
let advancedMode = false;
let groupOffsets = {};     // layered: { displayGroupPath: { dx, dy } } — facial groups only
let dicedMeta = null;      // diced: { type, name, canvas_size, poses }
let activePose = null;     // diced: currently selected pose name
let spoilerMode = false;   // false hides SPOILER_CHARACTERS from the dropdown

// --- State persistence ---
//
// Everything the editor remembers across reloads lives under one localStorage
// key: the global toggles plus one record per character. Per-character (rather
// than last-character-only) means switching away to check another portrait and
// coming back leaves your composition intact.
//
// One key, unlike scene.js's placements/config split — that split exists to
// stop a drag's ~200ms write cadence from echoing into another tab and
// clobbering in-progress text entry. Here the only high-frequency mutation is
// the advanced-offset sliders, and they belong to the same per-character
// record as everything else, so there's nothing to separate.

const EDITOR_STATE_KEY     = 'manosaba.editor.state';
const EDITOR_STATE_VERSION = 1;

// charName → { head, enabled, offsets } | { pose }. Read from localStorage once
// at init, then kept in sync as the live state changes.
let savedChars = {};

// Snapshot the live state of the current character.
function captureCharState() {
  if (charType === 'diced') return { pose: activePose };
  return {
    head: activeHead,
    // Enabled names, not the full {name: bool} map: compact, and stale entries
    // cost nothing on load because activeState is rebuilt from the live
    // layersInfo anyway (see applySavedEnabled).
    enabled: Object.keys(activeState).filter(n => activeState[n]),
    offsets: groupOffsets,
  };
}

function saveEditorState() {
  try {
    let onDisk = {};
    try {
      const raw = localStorage.getItem(EDITOR_STATE_KEY);
      const prev = raw ? JSON.parse(raw) : null;
      if (prev && prev.version === EDITOR_STATE_VERSION && prev.chars) onDisk = prev.chars;
    } catch { /* corrupt payload — start the map fresh rather than refuse to save */ }

    // Read-modify-write the chars map: disk wins for every character except the
    // one this tab is editing. Two tabs each hold a stale view of the other's
    // records, so a blind whole-blob write would erase the other tab's work.
    savedChars = { ...savedChars, ...onDisk };
    if (currentChar) savedChars[currentChar] = captureCharState();

    localStorage.setItem(EDITOR_STATE_KEY, JSON.stringify({
      version:  EDITOR_STATE_VERSION,
      lastChar: currentChar,
      spoilerMode,
      advancedMode,
      chars:    savedChars,
    }));
  } catch (e) {
    console.error('Failed to save editor state:', e);
  }
}

// Coalesces high-frequency mutations (slider drags fire on every `input`) into
// one write per ~200ms idle, matching scene.js's debounce. Tab close between
// writes loses at most the last 200ms of slider motion — acceptable.
let _stateSaveTimer = null;
function scheduleEditorStateSave() {
  if (_stateSaveTimer) clearTimeout(_stateSaveTimer);
  _stateSaveTimer = setTimeout(() => { _stateSaveTimer = null; saveEditorState(); }, 200);
}

// Restores the global toggles + the saved-character map. Returns the character
// to open, or null to fall back to DEFAULT_CHARACTER. Doesn't touch the DOM —
// init applies the toggles itself so the spoiler confirm modal stays silent.
function loadEditorState() {
  let data;
  try {
    const raw = localStorage.getItem(EDITOR_STATE_KEY);
    if (!raw) return null;
    data = JSON.parse(raw);
  } catch (e) {
    console.error('Failed to parse saved editor state:', e);
    return null;
  }
  if (!data || data.version !== EDITOR_STATE_VERSION) return null;

  if (typeof data.spoilerMode  === 'boolean') spoilerMode  = data.spoilerMode;
  if (typeof data.advancedMode === 'boolean') advancedMode = data.advancedMode;
  if (data.chars && typeof data.chars === 'object') savedChars = data.chars;

  // A spoiler character is only restorable with spoilers on — otherwise it
  // isn't in the dropdown and the <select> would show a value it can't offer.
  const c = data.lastChar;
  if (!CHARACTERS.includes(c)) return null;
  if (!spoilerMode && SPOILER_CHARACTERS.has(c)) return null;
  return c;
}

// Rebuild activeState from a saved `enabled` list. layers.json is regenerated
// by extract_bundle.py, so a saved list can name layers a re-extraction renamed
// away; iterating the live layersInfo rather than the saved array drops those
// unknown names for free. enforceDeps() runs after this and self-heals the
// `requires` chains, so this only has to decide the starting truth.
//
// Always-on bases are forced back on when the saved list lost one — they're
// hidden from the panel and nothing declares `requires: "Body"`, so
// enforceDeps() couldn't rescue a composite missing its body. But the force-on
// is intersected with defaultEnabled rather than applied to every
// isAlwaysOnBase() match: that predicate matches any `HeadBase*`, and a bundle
// can ship a HeadBase the character never uses (Hanna has HeadBase02 with no
// Head02-side facials). Forcing every match on renders two heads at once.
// Every default.json enables exactly the bases its character needs, so
// defaultEnabled is the right authority — and for multi-head characters it's
// already resolved to the restored head's block.
function applySavedEnabled(saved) {
  const wanted = new Set(saved);
  for (const l of layersInfo) {
    activeState[l.name] =
      wanted.has(l.name) || (isAlwaysOnBase(l) && defaultEnabled.includes(l.name));
  }
}

// Clamp a saved offsets map to the live axis config. Unknown group paths are
// harmless (getLayerOffset simply never looks them up), but a non-numeric or
// out-of-range value would reach the renderer's transform math, so every axis
// is validated against AXIS_CONFIG rather than trusted.
function sanitizeOffsets(saved) {
  const out = {};
  if (!saved || typeof saved !== 'object') return out;
  for (const [groupPath, off] of Object.entries(saved)) {
    if (!off || typeof off !== 'object') continue;
    const clean = {};
    for (const [axis, cfg] of Object.entries(AXIS_CONFIG)) {
      const v = off[axis];
      if (typeof v !== 'number' || Number.isNaN(v)) continue;
      clean[axis] = Math.max(cfg.min, Math.min(cfg.max, v));
    }
    if (Object.keys(clean).length) out[groupPath] = clean;
  }
  return out;
}

function isFacialGroup(groupPath) {
  if (groupPath.startsWith('__')) return false; // synthetic mask buckets
  const leaf = groupPath.split('/').pop();
  return /^(Eyes|Mouth)\d*$/.test(leaf);
}

// Groups visible in non-advanced mode. Effects, shadows, clipping masks, and
// other detail groups are hidden until Advanced is enabled.
function isBasicGroup(groupPath) {
  const leaf = groupPath.split('/').pop();
  if (/^(Eyes|Mouth|Cheeks|Pale|Sweat)/.test(leaf)) return true;
  if (leaf.startsWith('Option_Arm')) return true;
  // Hiro's OptionB is a per-head silhouette layer (not a meaningful user choice);
  // Nanoka's OptionB carries actual outfit variants and stays in basic mode.
  if (leaf === 'OptionB') return currentChar !== 'Hiro';
  return ['Mask', 'ArmL', 'ArmR', 'Arms'].includes(leaf);
}

function getLayerOffset(layer) {
  const o = groupOffsets[getDisplayGroup(layer)];
  if (!o) return { dx: 0, dy: 0, scale: 1, rotate: 0 };
  return { dx: o.dx || 0, dy: o.dy || 0, scale: o.scale ?? 1, rotate: o.rotate || 0 };
}

// Sprite top-left in canvas space. Layers without `pos` are canvas-sized PNGs
// drawn at canvas origin; `pos` PNGs are tightly cropped sprites placed at the
// recorded position. Both schemas coexist during the rollout — the renderer
// just adds `pos` to every drawImage / mask op.
function getPos(layer) {
  return layer.pos || [0, 0];
}

// --- Data Loading ---

async function loadCharacter(name) {
  currentChar = name;
  invalidateImageCache();
  groupOffsets = {};

  if (DICED_CHARACTERS.has(name)) {
    await loadDicedCharacter(name);
  } else {
    await loadLayeredCharacter(name);
  }

  // Reset zoom/pan on character switch.
  zoomLevel = 1; panX = 0; panY = 0;
  applyTransform();

  buildHeadSelector();
  buildUI();
  renderPreview();
  // Records lastChar. On the init restore this rewrites the state we just
  // read back, which is harmless — and self-healing, since it persists the
  // cleaned-up version after stale layers were dropped.
  scheduleEditorStateSave();
}

async function loadLayeredCharacter(name) {
  charType = 'layered';
  dicedMeta = null;
  activePose = null;

  const [layersJson, compsJson, defaultJson] = await Promise.all([
    fetch(assetUrl(`characters/${name}/layers.json`)).then(r => r.json()),
    fetch(assetUrl(`characters/${name}/compositions.json`)).then(r => r.json()),
    fetch(assetUrl(`characters/${name}/default.json`)).then(r => r.json()),
  ]);

  layersInfo = layersJson.layers.filter(l => !l.empty);
  const canvasSize = layersJson.canvas_size || [2500, 5000];
  layersInfo.forEach(l => { l._canvasW = canvasSize[0]; l._canvasH = canvasSize[1]; });
  compositions = compsJson;
  defaultJsonRaw = defaultJson || {};

  // Detect head bases (Head01, Head02, etc.). A head is only kept if it has
  // at least one facial feature scoped to it — Hanna's bundle ships HeadBase02
  // + HairB02 with no Head02-side eyes/mouths, so Head02 is unused and would
  // otherwise show as a broken option in the head selector.
  const headSet = new Set();
  for (const l of layersInfo) {
    const match = l.group.match(/Head(\d+)/);
    if (match) headSet.add('Head' + match[1]);
  }
  const facialKeywords = ['Eyes', 'Mouth', 'Cheeks', 'Pale', 'Sweat', 'Facial'];
  const hasFacialFeatures = head => layersInfo.some(l =>
    l.group.includes(head) &&
    facialKeywords.some(k => l.group.includes(k) || l.name.includes(k)));
  headBases = [...headSet].filter(hasFacialFeatures).sort();
  const saved = savedChars[name];
  if (headBases.length > 1) {
    // Per-head shape: pick first head that has a block; else legacy fallback via HeadBase entry
    const perHead = headBases.find(h => defaultJsonRaw[h]);
    if (perHead) {
      activeHead = perHead;
    } else {
      const legacy = (defaultJsonRaw.enabled || []).find(n => n.startsWith('HeadBase'));
      activeHead = legacy ? 'Head' + legacy.replace('HeadBase', '') : headBases[0];
    }
    // A saved head overrides the default, but only if the bundle still has it.
    // This must land before computeDefaultEnabled() — that reads
    // defaultJsonRaw[activeHead], so restoring later would resolve defaults
    // against the wrong head.
    if (saved && headBases.includes(saved.head)) activeHead = saved.head;
  } else {
    activeHead = null;
  }
  defaultEnabled = computeDefaultEnabled();

  resetToDefault();
  if (saved && Array.isArray(saved.enabled)) applySavedEnabled(saved.enabled);
  groupOffsets = sanitizeOffsets(saved?.offsets);
  enforceDeps();
}

async function loadDicedCharacter(name) {
  charType = 'diced';
  // Wipe layered state so a stale character can't leak into UI builders.
  layersInfo = [];
  compositions = {};
  defaultEnabled = [];
  defaultJsonRaw = {};
  headBases = [];
  activeHead = null;
  activeState = {};

  dicedMeta = await fetch(assetUrl(`characters/${name}/meta.json`)).then(r => r.json());
  // A saved pose only survives if the atlas still lists it — poses are named
  // from the bundle's sprite names, which a re-extraction can change.
  const saved = savedChars[name];
  activePose = (saved && dicedMeta.poses.includes(saved.pose))
    ? saved.pose
    : dicedMeta.poses[0];
}

// --- Render descriptor helpers ---
//
// Every layer carries a `render` object derived from its Unity material:
//   { blend: 'source-over' | 'multiply' | 'overlay' | 'softlight',
//     stencil: null | { role: 'write' | 'read', ref: int, cutoff?: float } }
// Writers contribute their alpha to the per-ref stencil buffer (binary,
// thresholded by `cutoff`). Readers are gated by that buffer when composited.

function getRender(layer) {
  return layer.render || { blend: 'source-over', stencil: null };
}

function isStencilReader(l) {
  return getRender(l).stencil?.role === 'read';
}

// Body and HeadBase* are render-foundation layers — disabling them produces a
// broken composite. Treat as always-on: skip in panel UI and the "None" sweep.
function isAlwaysOnBase(l) {
  return l.name === 'Body' || /^Body\d+$/.test(l.name) || l.name.startsWith('HeadBase');
}

// UI bucketing for Effect_* layer families: cluster sibling effect variants
// (e.g. Effect_Back_ArmR01..09) under a single section regardless of how the
// bundle nested them. Everything else falls through to its bundle-derived
// group.
function getDisplayGroup(l) {
  const segments = l.group.split('/');
  const headMatch = l.group.match(/Head\d+/);
  const headPrefix = headMatch ? `${headMatch[0]}/` : '';
  const effectSeg = [...segments].reverse().find(s => /^Effect/.test(s));
  if (effectSeg) return `__EffectMask__/${headPrefix}${effectSeg}`;
  return l.group;
}

// --- State Management ---

function computeDefaultEnabled() {
  // New per-head shape: { Head01: { enabled: [...] }, Head02: { enabled: [...] } }
  if (activeHead && defaultJsonRaw[activeHead]) {
    return defaultJsonRaw[activeHead].enabled || [];
  }
  // Legacy flat shape: { enabled: [...] }
  return defaultJsonRaw.enabled || [];
}

function resetToDefault() {
  activeState = {};
  for (const l of layersInfo) {
    activeState[l.name] = defaultEnabled.includes(l.name);
  }
}

function enforceDeps() {
  // If a required layer is off, turn off all layers that depend on it.
  // Iterate until stable so chained requirements (A→B→C) all collapse.
  let changed = true;
  while (changed) {
    changed = false;
    for (const l of layersInfo) {
      const reqs = getRequires(l);
      if (reqs.length && activeState[l.name] && !reqs.every(r => activeState[r])) {
        activeState[l.name] = false;
        changed = true;
      }
    }
  }
}

// Layers with `auto_enable: true` follow their `requires` parent on the
// off→on transition: when the user activates the parent, dependents flip on
// once. The user can disable them manually afterward and they stay off until
// the parent toggles off→on again.
function autoEnableDependents(layerName) {
  for (const dep of layersInfo) {
    const reqs = getRequires(dep);
    if (dep.auto_enable && reqs.includes(layerName) && reqs.every(r => activeState[r])) {
      activeState[dep.name] = true;
    }
  }
}

// Normalize the `requires` field to a list. A layer may declare `requires`
// as a single layer name (string) or a list of names (array); both forms
// mean "all listed layers must be active for this one to remain enabled".
function getRequires(layer) {
  if (!layer.requires) return [];
  return Array.isArray(layer.requires) ? layer.requires : [layer.requires];
}

// Group exclusion / fallback engine. Layers may declare:
//   excludes_groups: [groupLeaf, ...]     — clear these groups when I activate
//   requires_groups: { groupLeaf: name }  — when I'm cleared, ensure these
//                                           groups have a layer; if empty,
//                                           fall back to `name`.
// Single-side declaration: e.g. only Arms layers carry the rule, and
// activating an ArmL/ArmR layer clears Arms via the inverse scan in step (2).
function applyClearedFallbacks(clearedLayers, excludesContext) {
  for (const ol of clearedLayers) {
    if (!ol.requires_groups) continue;
    for (const [gleaf, defaultName] of Object.entries(ol.requires_groups)) {
      // Don't repopulate a group the activator just forbade.
      if (excludesContext && excludesContext.includes(gleaf)) continue;
      const groupHasActive = layersInfo.some(x =>
        x.group.split('/').pop() === gleaf && activeState[x.name]);
      if (!groupHasActive && activeState.hasOwnProperty(defaultName)) {
        activeState[defaultName] = true;
        autoEnableDependents(defaultName);
      }
    }
  }
}

function enforceExclusions(activatedName) {
  const me = layersInfo.find(l => l.name === activatedName);
  if (!me) return;
  const myLeaf = me.group.split('/').pop();
  const cleared = [];

  // (1) Clear groups I exclude.
  if (me.excludes_groups) {
    for (const gleaf of me.excludes_groups) {
      for (const ol of layersInfo) {
        if (ol.group.split('/').pop() === gleaf && activeState[ol.name]) {
          activeState[ol.name] = false;
          cleared.push(ol);
        }
      }
    }
  }

  // (2) Inverse: any active layer whose excludes_groups names my group
  //     gets cleared because I'm now in that group.
  for (const ol of layersInfo) {
    if (ol === me) continue;
    if (ol.excludes_groups && ol.excludes_groups.includes(myLeaf) && activeState[ol.name]) {
      activeState[ol.name] = false;
      cleared.push(ol);
    }
  }

  // (3) Fallbacks from each cleared layer's requires_groups, suppressed
  //     for any group I forbid.
  applyClearedFallbacks(cleared, me.excludes_groups);
}


function applyPreset(presetName) {
  const enabled = compositions[presetName];
  if (!enabled) return;

  // Turn off all facial layers, then enable the preset's list
  const facialKeywords = ['Eyes', 'Mouth', 'Cheeks', 'Pale', 'Sweat', 'Mask'];
  // Match by name OR group path: some characters (e.g. Leia) name their cheek
  // sprites bare ('Flushed', 'Normal') under an 'Angle01/Facial/Cheeks' group,
  // so a name-only check misses them.
  const isFacial = l => facialKeywords.some(k =>
    l.name.includes(k) || l.group.includes(k));

  // Only clear facial layers belonging to the active head (or all if single head).
  // Stencil readers (clipping masks) are preset-independent and stay on/off
  // per their own state; we only cycle the regular facial sprites here.
  for (const l of layersInfo) {
    if (isStencilReader(l)) continue;
    if (isFacial(l)) {
      if (!activeHead || l.group.includes(activeHead)) {
        activeState[l.name] = false;
      }
    }
  }

  // Enable the preset layers
  for (const name of enabled) {
    if (activeState.hasOwnProperty(name)) {
      activeState[name] = true;
    }
  }
}

// --- UI Building ---

function buildHeadSelector() {
  const section = document.getElementById('headSelectorSection');
  const container = document.getElementById('headSelector');
  container.innerHTML = '';

  if (headBases.length <= 1) {
    section.style.display = 'none';
    return;
  }

  section.style.display = '';
  for (const head of headBases) {
    const btn = document.createElement('button');
    btn.className = 'preset-btn' + (head === activeHead ? ' active' : '');
    btn.textContent = head;
    btn.onclick = () => {
      const oldHead = activeHead;
      const belongsToHead = (l, h) => {
        if (l.group.includes(h)) return true;
        const m = l.name.match(/Head(\d+)/);
        return !!(m && 'Head' + m[1] === h);
      };
      // Turn off all layers of the old head (matched by group OR by Head## suffix in name).
      for (const l of layersInfo) {
        if (belongsToHead(l, oldHead)) activeState[l.name] = false;
      }
      activeHead = head;
      defaultEnabled = computeDefaultEnabled();
      // Enable head-scoped layers per the new head's default.json entries.
      for (const l of layersInfo) {
        if (belongsToHead(l, head) && defaultEnabled.includes(l.name)) {
          activeState[l.name] = true;
        }
      }
      // Fallback: ensure HeadBase is on even if missing from default
      const base = layersInfo.find(l => l.name === 'HeadBase' + head.replace('Head', ''));
      if (base) activeState[base.name] = true;
      // Apply Normal expression for the new head
      const headNum = head.replace('Head', '').replace(/^0+/, '');
      const normalPreset = 'Normal' + headNum;
      if (compositions[normalPreset]) {
        applyPreset(normalPreset);
      }
      // Update head buttons
      for (const b of container.querySelectorAll('.preset-btn')) {
        b.classList.toggle('active', b.textContent === head);
      }
      buildUI();
      renderPreview();
      scheduleEditorStateSave();
    };
    container.appendChild(btn);
  }
}

function isGroupForActiveHead(groupPath) {
  if (!activeHead) return true; // single head, show all
  // If group contains a Head## marker, only show if it matches activeHead
  const match = groupPath.match(/Head(\d+)/);
  if (!match) return true; // not head-specific, always show
  return 'Head' + match[1] === activeHead;
}

function isLayerForActiveHead(name) {
  if (!activeHead) return true;
  // For shared groups (e.g. OptionB) where head-specific layers live side by side,
  // filter by Head## marker in the layer name itself.
  const match = name.match(/Head(\d+)/);
  if (!match) return true;
  return 'Head' + match[1] === activeHead;
}

function buildUI() {
  if (charType === 'diced') {
    buildUIDiced();
    return;
  }
  // Restore layered-only sections in case we're switching back from a diced
  // character (head selector visibility is managed by buildHeadSelector).
  document.getElementById('expressionSection').style.display = '';
  document.getElementById('advancedToggle').style.display = '';
  document.getElementById('resetBtn').style.display = '';
  buildPresets();
  buildGroups();
}

function buildUIDiced() {
  // Diced characters have no expressions, head variants, advanced offsets,
  // or default-state to reset to. Hide those controls; keep the dropdown
  // and Export PNG. The head selector hides itself via buildHeadSelector
  // when headBases is empty.
  document.getElementById('expressionSection').style.display = 'none';
  document.getElementById('advancedToggle').style.display = 'none';
  document.getElementById('resetBtn').style.display = 'none';

  // Single Pose group with one button per pose, reusing layer-btn styling.
  const container = document.getElementById('groups');
  container.innerHTML = '';

  const div = document.createElement('div');
  div.className = 'group';

  const header = document.createElement('div');
  header.className = 'group-header';
  const titleSpan = document.createElement('span');
  titleSpan.textContent = 'Pose';
  const statusSpan = document.createElement('span');
  statusSpan.className = 'group-status';
  titleSpan.appendChild(statusSpan);
  const arrowSpan = document.createElement('span');
  arrowSpan.className = 'arrow';
  arrowSpan.textContent = '▼';
  header.appendChild(titleSpan);
  header.appendChild(arrowSpan);
  header.onclick = () => div.classList.toggle('collapsed');
  div.appendChild(header);

  const opts = document.createElement('div');
  opts.className = 'group-options';

  for (const pose of dicedMeta.poses) {
    const btn = document.createElement('button');
    btn.className = 'layer-btn' + (pose === activePose ? ' active' : '');
    btn.textContent = pose;
    btn.dataset.pose = pose;
    btn.onclick = () => {
      activePose = pose;
      for (const b of opts.querySelectorAll('.layer-btn')) {
        b.classList.toggle('active', b.dataset.pose === pose);
      }
      statusSpan.textContent = ` — ${pose}`;
      renderPreview();
      scheduleEditorStateSave();
    };
    opts.appendChild(btn);
  }
  if (activePose) statusSpan.textContent = ` — ${activePose}`;

  div.appendChild(opts);
  container.appendChild(div);
}

function buildPresets() {
  const container = document.getElementById('presets');
  container.innerHTML = '';

  // Find expression presets (compositions that set Eyes + Mouth)
  const expressionNames = ['Normal','Smile','Angry','Pensive','Cry','Flushed','Surprised','Fearful','Wink','Sparkle','Determined'];
  // For multi-head: filter to presets matching the active head number
  const headNum = activeHead ? activeHead.replace('Head', '').replace(/^0+/, '') : null;
  const presets = Object.keys(compositions).filter(k => {
    if (!expressionNames.some(e => k.startsWith(e))) return false;
    if (!headNum) return true; // single head, show all
    // Match presets ending with the head number (Normal1 for Head01, Normal2 for Head02)
    // Also include presets without a number suffix or aliased (Smile, Angry, etc.)
    const trailingNum = k.match(/(\d+)$/);
    if (!trailingNum) return true; // no number, show always (alias like "Smile")
    return trailingNum[1] === headNum;
  });

  // Deduplicate: prefer short names
  const seen = new Set();
  const uniquePresets = [];
  for (const p of presets) {
    const base = p.replace(/\d+$/, '');
    if (!seen.has(base)) {
      seen.add(base);
      uniquePresets.push(p);
    }
  }

  for (const name of uniquePresets) {
    const btn = document.createElement('button');
    btn.className = 'preset-btn';
    btn.dataset.preset = name;
    btn.textContent = name.replace(/\d+$/, '');
    btn.onclick = () => {
      applyPreset(name);
      enforceDeps();
      updateUI();
      renderPreview();
      scheduleEditorStateSave();
    };
    container.appendChild(btn);
  }
}

function buildGroups() {
  const container = document.getElementById('groups');
  container.innerHTML = '';

  // Group layers by their display path (mostly bundle-derived; Pale/Effect/
  // Shadow get synthetic buckets via getDisplayGroup so siblings cluster).
  const groups = new Map();
  for (const l of layersInfo) {
    const key = getDisplayGroup(l);
    if (!groups.has(key)) groups.set(key, []);
    groups.get(key).push(l);
  }

  // Simplify group names for display
  function displayName(group) {
    const parts = group.split('/');
    return parts[parts.length - 1];
  }

  // Sort groups: facial first, arms second, everything else last
  // Strict total order so the panel layout is identical across every
  // character. Earlier versions used coarse buckets that left ties; sort
  // stability then exposed file-iteration order, which differs per
  // character. Each leaf gets a distinct rank.
  function groupOrder(path) {
    const leaf = path.split('/').pop();

    // ClippingMask group sits between OptionB (29) and Effect_Front_ArmL (40).
    // Checked first so the looser `Mask` substring match below doesn't catch it.
    if (leaf === 'ClippingMask') return 35;

    // Facial features
    if (leaf.includes('Eyes')) return 0;
    if (leaf.includes('Mouth')) return 1;
    if (leaf.includes('Cheeks')) return 2;
    if (leaf.includes('Pale')) return 3;
    if (leaf.includes('Sweat')) return 4;
    if (leaf.includes('Mask')) return 5;

    // OptionB top-level group (Hiro/Nanoka) sits after the Option_Arm clusters.
    if (leaf === 'OptionB') return 29;

    // Arms: category first, then limb (L → R → combined within each category)
    if (leaf.includes('Arm')) {
      const limb = leaf.includes('ArmL') ? 0 : leaf.includes('ArmR') ? 1 : 2;
      if (leaf === 'ArmL' || leaf === 'ArmR' || leaf === 'Arms') return 10 + limb;
      if (leaf.startsWith('Option_Arm')) return 20 + limb;
      if (leaf.startsWith('Effect_Front_Arm')) return 40 + limb;
      if (leaf.startsWith('Effect_Middle_Arm')) return 50 + limb;
      if (leaf.startsWith('Effect_Back_Arm')) return 60 + limb;
      if (leaf.startsWith('Shadow_Arm')) return 70 + limb;
      return 80 + limb;
    }
    return 100;
  }
  const sortedGroups = [...groups.entries()].sort((a, b) => groupOrder(a[0]) - groupOrder(b[0]));

  for (const [groupPath, layers] of sortedGroups) {
    // Skip groups whose layers are all always-on (Body, HeadBase, HairB, non-source-over masks)
    if (layers.every(l => l.name === 'Body' || /^Body\d+$/.test(l.name) || l.name.startsWith('HeadBase') || l.name.startsWith('HairB'))) continue;
    // Skip groups belonging to inactive head
    if (!isGroupForActiveHead(groupPath)) continue;
    // Hide detail groups (Effects, Shadows, ClippingMask, etc.) in non-advanced mode
    if (!advancedMode && !isBasicGroup(groupPath)) continue;
    // Skip groups with no selectable buttons for the active head — e.g. Meruru's
    // OptionF on Head02, where every entry is name-tagged Head01 and would
    // otherwise render an empty section with just the None button.
    if (!layers.some(l => isLayerForActiveHead(l.name))) continue;

    const div = document.createElement('div');
    div.className = 'group';

    const header = document.createElement('div');
    header.className = 'group-header';
    const titleSpan = document.createElement('span');
    titleSpan.textContent = displayName(groupPath);
    const statusSpan = document.createElement('span');
    statusSpan.className = 'group-status';
    statusSpan.dataset.statusGroup = groupPath;
    titleSpan.appendChild(statusSpan);
    const arrowSpan = document.createElement('span');
    arrowSpan.className = 'arrow';
    arrowSpan.textContent = '▼';
    header.appendChild(titleSpan);
    header.appendChild(arrowSpan);
    header.onclick = () => div.classList.toggle('collapsed');
    div.appendChild(header);

    const opts = document.createElement('div');
    opts.className = 'group-options';

    // "None" button
    const noneBtn = document.createElement('button');
    noneBtn.className = 'none-btn';
    noneBtn.textContent = 'None';
    noneBtn.dataset.group = groupPath;
    noneBtn.onclick = () => {
      // Collect what we actually clear so cleared layers can drive their
      // requires_groups fallbacks (e.g. clearing Arms repopulates ArmL/ArmR).
      const cleared = [];
      for (const l of layers) {
        if (!isLayerForActiveHead(l.name)) continue; // don't touch layers belonging to inactive head
        if (isAlwaysOnBase(l)) continue; // Body/HeadBase stay on regardless
        if (activeState[l.name]) cleared.push(l);
        activeState[l.name] = false;
      }
      applyClearedFallbacks(cleared);
      enforceDeps();
      updateUI();
      renderPreview();
      scheduleEditorStateSave();
    };
    opts.appendChild(noneBtn);

    for (const l of layers) {
      if (!isLayerForActiveHead(l.name)) continue; // hide layers that belong to a different head
      if (isAlwaysOnBase(l)) continue; // Body/HeadBase aren't user-toggleable
      const btn = document.createElement('button');
      btn.className = 'layer-btn';
      // Strip the group's leaf segment from the start of the layer name —
      // e.g. for group leaf "Eyes", "Eyes_Normal_Open01" → "Normal_Open01".
      // If the name doesn't start with the leaf, leave it unchanged.
      const leaf = groupPath.split('/').pop();
      let label = l.name;
      if (label.startsWith(leaf + '_')) label = label.slice(leaf.length + 1);
      else if (label.startsWith(leaf)) label = label.slice(leaf.length);
      btn.textContent = label || l.name;
      btn.dataset.layer = l.name;
      btn.dataset.group = groupPath;
      btn.onclick = () => {
        const leaf = groupPath.split('/').pop();
        // Stencil readers are decorative overlays — multi-select makes sense
        // (you may want both Hair_Normal AND Hair_Multiply, or several root masks).
        const multiSelect = isStencilReader(l)
          || leaf === 'Facial' || leaf === 'ClippingMask' || leaf === 'Angle01'
          || /^Head\d+$/.test(leaf);

        const activated = [];
        const radioCleared = [];
        if (multiSelect) {
          // Toggle behavior: flip this layer on/off
          activeState[l.name] = !activeState[l.name];
          if (activeState[l.name]) activated.push(l.name);
        } else {
          // Radio behavior: turn off all in group, turn on this one —
          // but additively preserve the clicked layer's `requires` chain.
          const keepOn = new Set([l.name]);
          const queue = [l];
          while (queue.length) {
            const cur = queue.shift();
            for (const r of getRequires(cur)) {
              if (keepOn.has(r)) continue;
              keepOn.add(r);
              const parent = layersInfo.find(x => x.name === r);
              if (parent) queue.push(parent);
            }
          }
          for (const other of layers) {
            if (!keepOn.has(other.name)) {
              if (activeState[other.name]) radioCleared.push(other);
              activeState[other.name] = false;
            }
          }
          for (const n of keepOn) {
            activeState[n] = true;
            activated.push(n);
          }
        }

        // Fallbacks for radio-cleared layers (e.g. ArmL05's requires_groups
        // repopulating ArmR when replaced by a regular ArmL). Suppress per
        // the combined excludes_groups of the activators so e.g. Arms01
        // replacing ArmL05 still keeps ArmR/ArmL bare.
        const suppression = new Set();
        for (const n of activated) {
          const me = layersInfo.find(x => x.name === n);
          if (me?.excludes_groups) for (const g of me.excludes_groups) suppression.add(g);
        }
        applyClearedFallbacks(radioCleared, [...suppression]);

        for (const n of activated) {
          enforceExclusions(n);
          autoEnableDependents(n);
        }

        enforceDeps();
        updateUI();
        renderPreview();
        scheduleEditorStateSave();
      };
      opts.appendChild(btn);
    }

    div.appendChild(opts);
    if (advancedMode && isFacialGroup(groupPath)) {
      div.appendChild(buildAdvancedSliders(groupPath));
    }
    container.appendChild(div);
  }
}

const AXIS_CONFIG = {
  dx:     { label: 'X', title: 'X offset',         default: 0, min: -1000, max: 1000, sliderMin: -200, sliderMax: 200, step: 1,    decimals: 0 },
  dy:     { label: 'Y', title: 'Y offset',         default: 0, min: -1000, max: 1000, sliderMin: -200, sliderMax: 200, step: 1,    decimals: 0 },
  scale:  { label: 'S', title: 'Scale',            default: 1, min: 0.3,   max: 3.0,  sliderMin: 0.5,  sliderMax: 1.5, step: 0.01, decimals: 2 },
  rotate: { label: 'R', title: 'Rotation (°)',     default: 0, min: -180,  max: 180,  sliderMin: -30,  sliderMax: 30,  step: 1,    decimals: 0 },
};

function buildAdvancedSliders(groupPath) {
  const wrap = document.createElement('div');
  wrap.className = 'group-advanced';
  if (!groupOffsets[groupPath]) groupOffsets[groupPath] = {};
  const off = groupOffsets[groupPath];
  for (const a of Object.keys(AXIS_CONFIG)) {
    if (off[a] === undefined) off[a] = AXIS_CONFIG[a].default;
  }
  const inputs = {};
  const values = {};

  function set(axis, v) {
    const cfg = AXIS_CONFIG[axis];
    if (Number.isNaN(v)) return;
    v = Math.max(cfg.min, Math.min(cfg.max, v));
    v = parseFloat((Math.round(v / cfg.step) * cfg.step).toFixed(cfg.decimals));
    if (off[axis] === v) return;
    off[axis] = v;
    inputs[axis].value = v;
    values[axis].value = v.toFixed(cfg.decimals);
    renderPreview();
    // The no-op early-return above means a drag only saves on real change;
    // the debounce collapses the rest of the drag into one write.
    scheduleEditorStateSave();
  }

  for (const axis of Object.keys(AXIS_CONFIG)) {
    const cfg = AXIS_CONFIG[axis];
    const row = document.createElement('div');
    row.className = 'slider-row';

    const label = document.createElement('label');
    label.textContent = cfg.label;
    label.title = cfg.title;

    const dec = document.createElement('button');
    dec.className = 'slider-step';
    dec.textContent = '−';
    dec.onclick = () => set(axis, off[axis] - cfg.step);

    const input = document.createElement('input');
    input.type = 'range';
    input.min = String(cfg.sliderMin);
    input.max = String(cfg.sliderMax);
    input.step = String(cfg.step);
    input.value = off[axis];
    input.addEventListener('input', () => set(axis, parseFloat(input.value)));
    inputs[axis] = input;

    const inc = document.createElement('button');
    inc.className = 'slider-step';
    inc.textContent = '+';
    inc.onclick = () => set(axis, off[axis] + cfg.step);

    const value = document.createElement('input');
    value.type = 'number';
    value.min = String(cfg.min);
    value.max = String(cfg.max);
    value.step = String(cfg.step);
    value.className = 'slider-value';
    value.value = off[axis].toFixed(cfg.decimals);
    value.addEventListener('input', () => {
      const v = parseFloat(value.value);
      if (!Number.isNaN(v)) set(axis, v);
    });
    value.addEventListener('blur', () => { value.value = off[axis].toFixed(cfg.decimals); });
    values[axis] = value;

    row.appendChild(label);
    row.appendChild(dec);
    row.appendChild(input);
    row.appendChild(inc);
    row.appendChild(value);
    wrap.appendChild(row);
  }

  const resetBtn = document.createElement('button');
  resetBtn.className = 'slider-reset';
  resetBtn.textContent = 'Reset';
  resetBtn.onclick = () => {
    for (const axis of Object.keys(AXIS_CONFIG)) set(axis, AXIS_CONFIG[axis].default);
  };
  wrap.appendChild(resetBtn);

  return wrap;
}

function updateUI() {
  // Update preset buttons — preset is active iff currently-on facial layers
  // (within applyPreset's scope: same head, controllable) equal the preset
  // exactly. Looser "every" matching falsely highlights when extras are on.
  const facialKeywords = ['Eyes', 'Mouth', 'Cheeks', 'Pale', 'Sweat', 'Mask'];
  const isFacial = l => facialKeywords.some(k =>
    l.name.includes(k) || l.group.includes(k));
  const isControllable = l => !isStencilReader(l);
  const facialOnNames = new Set(
    layersInfo
      .filter(l => isFacial(l)
        && (!activeHead || l.group.includes(activeHead))
        && isControllable(l)
        && activeState[l.name])
      .map(l => l.name)
  );
  for (const btn of document.querySelectorAll('#presets .preset-btn')) {
    const enabled = compositions[btn.dataset.preset];
    if (!enabled || enabled.length === 0) { btn.classList.remove('active'); continue; }
    const enabledSet = new Set(enabled);
    const matches = enabled.every(n => activeState[n])
      && [...facialOnNames].every(n => enabledSet.has(n));
    btn.classList.toggle('active', matches);
  }
  // Update layer buttons
  for (const btn of document.querySelectorAll('.layer-btn')) {
    const layer = layersInfo.find(l => l.name === btn.dataset.layer);
    const reqMet = !layer || getRequires(layer).every(r => !!activeState[r]);
    btn.disabled = !reqMet;
    btn.classList.toggle('active', !!activeState[btn.dataset.layer]);
  }
  // Update none buttons
  for (const btn of document.querySelectorAll('.none-btn')) {
    const group = btn.dataset.group;
    const anyActive = layersInfo.some(l =>
      getDisplayGroup(l) === group && activeState[l.name]);
    btn.classList.toggle('active', !anyActive);
  }
  // Update group status labels
  for (const span of document.querySelectorAll('.group-status')) {
    const group = span.dataset.statusGroup;
    const active = layersInfo.find(l =>
      getDisplayGroup(l) === group && activeState[l.name]);
    span.textContent = active ? ` — ${active.name}` : '';
  }
}

// --- Rendering ---

// Image cache to avoid reloading on every render
const imageCache = {};

function loadLayerImage(charName, layerName) {
  const key = `${charName}/${layerName}`;
  if (imageCache[key]) return imageCache[key];
  const promise = new Promise((resolve) => {
    const img = new Image();
    img.onload = () => resolve(img);
    img.onerror = () => resolve(null);
    img.src = assetUrl(`characters/${charName}/${layerName}.png`);
  });
  imageCache[key] = promise;
  return promise;
}

function invalidateImageCache() {
  for (const key in imageCache) delete imageCache[key];
}

async function compositeToCanvas(layers, canvasW, canvasH) {
  // Load all images in parallel
  const entries = (await Promise.all(layers.map(async l => ({
    layer: l,
    render: getRender(l),
    img: await loadLayerImage(currentChar, l.name),
  })))).filter(e => e.img);

  const canvas = document.createElement('canvas');
  canvas.width = canvasW;
  canvas.height = canvasH;
  const ctx = canvas.getContext('2d');

  // Apply the SpriteRenderer's per-instance color tint to a sprite. Mirrors
  // Unity's `fragment = texture · m_Color` step. Returns a tile canvas when
  // tint is non-identity; returns the raw image otherwise so callers don't
  // pay imageData cost on the common (untinted) path.
  function applyTint(srcImg, tint) {
    if (!tint) return srcImg;
    const [tr, tg, tb, ta] = tint;
    if (tr === 1 && tg === 1 && tb === 1 && ta === 1) return srcImg;
    const sw = srcImg.width, sh = srcImg.height;
    const out = document.createElement('canvas');
    out.width = sw;
    out.height = sh;
    const oCtx = out.getContext('2d');
    oCtx.drawImage(srcImg, 0, 0);
    const td = oCtx.getImageData(0, 0, sw, sh);
    const t = td.data;
    for (let i = 0; i < t.length; i += 4) {
      if (t[i + 3] === 0) continue;
      t[i]     = t[i]     * tr;
      t[i + 1] = t[i + 1] * tg;
      t[i + 2] = t[i + 2] * tb;
      t[i + 3] = t[i + 3] * ta;
    }
    oCtx.putImageData(td, 0, 0);
    return out;
  }

  // Pre-tint every entry once; downstream stencil + blend passes consume the
  // tinted source. For untinted layers (most), e.tinted === e.img (no-op).
  for (const e of entries) e.tinted = applyTint(e.img, e.render.tint);

  // Compute the overlap rectangle between a sprite of size (mw, mh) placed at
  // (offsetX, offsetY) and a destination of size (tileW, tileH). dx,dy are
  // destination top-left; sx,sy,w,h are the source slice to read. null = miss.
  function clipRegion(offsetX, offsetY, mw, mh, tileW, tileH) {
    const dx = Math.max(0, offsetX);
    const dy = Math.max(0, offsetY);
    const sx = Math.max(0, -offsetX);
    const sy = Math.max(0, -offsetY);
    const w = Math.min(mw - sx, tileW - dx);
    const h = Math.min(mh - sy, tileH - dy);
    if (w <= 0 || h <= 0) return null;
    return { dx, dy, sx, sy, w, h };
  }

  // Anchor used by Advanced-mode scale/rotate: sprite center in canvas space.
  function getLayerAnchor(entry) {
    const [px, py] = getPos(entry.layer);
    return { ax: px + entry.img.width / 2, ay: py + entry.img.height / 2 };
  }

  // Pass 1: build per-ref stencil buffers from active writers.
  // Each buffer is a canvas-sized ImageData where alpha[px] = 255 if any
  // writer's source alpha at that pixel exceeds its cutoff, else 0. This is
  // Unity stencil semantics: hard binary mask, multiple writers union.
  const stencilBuffers = new Map();
  function getOrCreateBuffer(ref) {
    let buf = stencilBuffers.get(ref);
    if (buf) return buf;
    buf = ctx.createImageData(canvasW, canvasH);
    stencilBuffers.set(ref, buf);
    return buf;
  }
  for (const e of entries) {
    const stc = e.render.stencil;
    if (stc?.role !== 'write') continue;
    const cutoffByte = (stc.cutoff ?? 0) * 255;
    const [px, py] = getPos(e.layer);
    const src = e.tinted;
    const sw = src.width, sh = src.height;
    const r = clipRegion(px, py, sw, sh, canvasW, canvasH);
    if (!r) continue;
    const tile = document.createElement('canvas');
    tile.width = r.w;
    tile.height = r.h;
    const tctx = tile.getContext('2d');
    tctx.drawImage(src, r.sx, r.sy, r.w, r.h, 0, 0, r.w, r.h);
    const sd = tctx.getImageData(0, 0, r.w, r.h).data;
    const buf = getOrCreateBuffer(stc.ref);
    const bd = buf.data;
    for (let yy = 0; yy < r.h; yy++) {
      const dstRow = (r.dy + yy) * canvasW * 4;
      const srcRow = yy * r.w * 4;
      for (let xx = 0; xx < r.w; xx++) {
        if (sd[srcRow + xx * 4 + 3] > cutoffByte) {
          bd[dstRow + (r.dx + xx) * 4 + 3] = 255;
        }
      }
    }
  }

  // Per-pixel blend kernels. All preserve dest alpha (αout = αb) so a layer
  // never adds visible pixels outside the existing canvas content. Source α=0
  // and dest α=0 are short-circuits. Formulas operate on 0–255 values; the
  // (1-as) factor mixes the unblended dest back in at semi-transparent edges,
  // matching Canvas2D's source-over behaviour at AA fringes.
  const INV_255 = 1 / 255;
  function applyBlendBlit(targetCtx, srcCanvas, mode, offsetX, offsetY) {
    if (mode === 'source-over') {
      targetCtx.drawImage(srcCanvas, offsetX, offsetY);
      return;
    }
    const sw = srcCanvas.width, sh = srcCanvas.height;
    const tw = targetCtx.canvas.width, th = targetCtx.canvas.height;
    const r = clipRegion(offsetX, offsetY, sw, sh, tw, th);
    if (!r) return;
    const srcCtx = (srcCanvas.getContext ? srcCanvas : null);
    const sCtx = srcCtx ? srcCtx.getContext('2d') : (() => {
      const t = document.createElement('canvas');
      t.width = sw; t.height = sh;
      t.getContext('2d').drawImage(srcCanvas, 0, 0);
      return t.getContext('2d');
    })();
    const sd = sCtx.getImageData(r.sx, r.sy, r.w, r.h).data;
    const pd = targetCtx.getImageData(r.dx, r.dy, r.w, r.h);
    const p = pd.data;
    for (let i = 0; i < p.length; i += 4) {
      const ab = p[i + 3];
      if (ab === 0) continue;
      const sa = sd[i + 3];
      if (sa === 0) continue;
      const as = sa * INV_255;
      const k = 1 - as;
      const r0 = p[i], g0 = p[i + 1], b0 = p[i + 2];
      const sr = sd[i], sg = sd[i + 1], sb = sd[i + 2];
      let or, og, ob;
      if (mode === 'multiply') {
        // Cmul = Cb · Cs / 255
        or = (r0 * sr) * INV_255;
        og = (g0 * sg) * INV_255;
        ob = (b0 * sb) * INV_255;
      } else if (mode === 'overlay') {
        // Cb < 128: 2·Cb·Cs / 255 ; else: 255 − 2·(255−Cb)·(255−Cs) / 255
        or = r0 < 128 ? (2 * r0 * sr) * INV_255 : 255 - (2 * (255 - r0) * (255 - sr)) * INV_255;
        og = g0 < 128 ? (2 * g0 * sg) * INV_255 : 255 - (2 * (255 - g0) * (255 - sg)) * INV_255;
        ob = b0 < 128 ? (2 * b0 * sb) * INV_255 : 255 - (2 * (255 - b0) * (255 - sb)) * INV_255;
      } else if (mode === 'softlight') {
        // Pegtop: Cout = (1 − 2·Cs')·Cb'² + 2·Cb'·Cs'  (normalized [0,1])
        const cbR = r0 * INV_255, csR = sr * INV_255;
        const cbG = g0 * INV_255, csG = sg * INV_255;
        const cbB = b0 * INV_255, csB = sb * INV_255;
        or = ((1 - 2 * csR) * cbR * cbR + 2 * cbR * csR) * 255;
        og = ((1 - 2 * csG) * cbG * cbG + 2 * cbG * csG) * 255;
        ob = ((1 - 2 * csB) * cbB * cbB + 2 * cbB * csB) * 255;
      } else {
        // Unknown blend → pass through as source-over color
        or = sr; og = sg; ob = sb;
      }
      p[i]     = or * as + r0 * k;
      p[i + 1] = og * as + g0 * k;
      p[i + 2] = ob * as + b0 * k;
      // αout = αb (preserved)
    }
    targetCtx.putImageData(pd, r.dx, r.dy);
  }

  // Pre-clip a sprite by a stencil buffer. Returns a sprite-sized canvas
  // whose alpha = sprite.alpha · (stencilBuf.alpha at corresponding canvas
  // coords) / 255. Pixels outside the canvas are zeroed.
  function gateByStencil(srcImg, stencilBuf, posX, posY) {
    const sw = srcImg.width, sh = srcImg.height;
    const tile = document.createElement('canvas');
    tile.width = sw;
    tile.height = sh;
    const tctx = tile.getContext('2d');
    tctx.drawImage(srcImg, 0, 0);
    const td = tctx.getImageData(0, 0, sw, sh);
    const t = td.data;
    const bd = stencilBuf.data;
    for (let yy = 0; yy < sh; yy++) {
      const cy = posY + yy;
      const rowOff = yy * sw * 4;
      if (cy < 0 || cy >= canvasH) {
        for (let xx = 0; xx < sw; xx++) t[rowOff + xx * 4 + 3] = 0;
        continue;
      }
      const bRow = cy * canvasW * 4;
      for (let xx = 0; xx < sw; xx++) {
        const cx = posX + xx;
        const ti = rowOff + xx * 4 + 3;
        if (cx < 0 || cx >= canvasW) { t[ti] = 0; continue; }
        t[ti] = (t[ti] * bd[bRow + cx * 4 + 3]) >>> 8; // ≈ /256, safe for [0,255]·[0,255]
      }
    }
    tctx.putImageData(td, 0, 0);
    return tile;
  }

  // Pass 2: composite layers in z-order. Stencil readers are gated by their
  // ref's buffer; non-readers compose directly. Blend mode is applied at the
  // final blit (see applyBlendBlit). Advanced-mode dx/dy/scale/rotate offsets
  // are honoured for source-over only — per-pixel blend kernels operate on
  // axis-aligned tiles, so transformations are baked at sprite origin.
  for (const e of entries) {
    const r = e.render;
    const [px, py] = getPos(e.layer);
    let src = e.tinted;

    if (r.stencil?.role === 'read') {
      const buf = stencilBuffers.get(r.stencil.ref);
      if (!buf) continue; // no writer this frame → reader contributes nothing
      src = gateByStencil(src, buf, px, py);
    }

    if (r.blend === 'source-over') {
      const off = getLayerOffset(e.layer);
      ctx.globalCompositeOperation = 'source-over';
      if (off.scale === 1 && off.rotate === 0) {
        ctx.drawImage(src, px + off.dx, py + off.dy);
      } else {
        const a = getLayerAnchor(e);
        ctx.save();
        ctx.translate(off.dx + a.ax, off.dy + a.ay);
        if (off.rotate) ctx.rotate(off.rotate * Math.PI / 180);
        if (off.scale !== 1) ctx.scale(off.scale, off.scale);
        ctx.translate(-a.ax, -a.ay);
        ctx.drawImage(src, px, py);
        ctx.restore();
      }
    } else {
      applyBlendBlit(ctx, src, r.blend, px, py);
    }
  }

  ctx.globalCompositeOperation = 'source-over';
  return canvas;
}

let renderSeq = 0;
async function renderPreview() {
  if (charType === 'diced') return renderDiced();

  const seq = ++renderSeq;
  const container = document.getElementById('previewContainer');

  const activeLayers = layersInfo
    .filter(l => activeState[l.name])
    .sort((a, b) => a.order - b.order);

  const canvasW = layersInfo.length > 0 ? (layersInfo[0]._canvasW || 2500) : 2500;
  const canvasH = layersInfo.length > 0 ? (layersInfo[0]._canvasH || 5000) : 5000;

  const canvas = await compositeToCanvas(activeLayers, canvasW, canvasH);
  // Discard if a newer render started while we were compositing — keeps the
  // visible canvas consistent with the latest activeState and avoids stale
  // overwrites when the user clicks rapidly.
  if (seq !== renderSeq) return;

  canvas.style.height = '950px'
  canvas.style.width = 'auto';
  container.replaceChildren(canvas);

  updateUI();
}

async function renderDiced() {
  const seq = ++renderSeq;
  const [w, h] = dicedMeta.canvas_size;
  const img = await loadLayerImage(currentChar, activePose);
  if (seq !== renderSeq) return;

  const canvas = document.createElement('canvas');
  canvas.width = w;
  canvas.height = h;
  if (img) canvas.getContext('2d').drawImage(img, 0, 0);
  canvas.style.height = '950px';
  canvas.style.width = 'auto';
  document.getElementById('previewContainer').replaceChildren(canvas);
}

// --- Export ---

// Crop a canvas to its alpha bbox plus a transparent margin, so the exported
// PNG hugs the character but keeps a uniform border on all sides. Empty input
// is returned unchanged.
// Tight-crop a canvas to its non-transparent bbox + a uniform margin.
// Returns the cropped canvas plus the bbox offset so callers can translate
// coordinates from the source frame to the cropped frame:
//   cropped_x = source_x - bbox.minX + margin
//   cropped_y = source_y - bbox.minY + margin
// (Used by makeSnapshotCanvas to track the rig pivot through the crop.)
function tightCrop(srcCanvas, margin = 10) {
  const w = srcCanvas.width, h = srcCanvas.height;
  const data = srcCanvas.getContext('2d').getImageData(0, 0, w, h).data;
  let minX = w, minY = h, maxX = -1, maxY = -1;
  for (let y = 0; y < h; y++) {
    const row = y * w * 4;
    for (let x = 0; x < w; x++) {
      if (data[row + x * 4 + 3]) {
        if (x < minX) minX = x;
        if (y < minY) minY = y;
        if (x > maxX) maxX = x;
        if (y > maxY) maxY = y;
      }
    }
  }
  if (maxX < 0) return { canvas: srcCanvas, minX: 0, minY: 0, margin: 0 };
  const out = document.createElement('canvas');
  out.width = (maxX - minX + 1) + margin * 2;
  out.height = (maxY - minY + 1) + margin * 2;
  out.getContext('2d').drawImage(srcCanvas, margin - minX, margin - minY);
  return { canvas: out, minX, minY, margin };
}

// Shared composite path for both Export PNG and Send to Scene. Returns the
// bbox-cropped canvas plus a `variant` label (preset name for layered, pose
// name for diced) — null when there's nothing to render.
async function makeSnapshotCanvas() {
  if (charType === 'diced') {
    const img = await loadLayerImage(currentChar, activePose);
    if (!img) return null;
    const full = document.createElement('canvas');
    full.width = img.width;
    full.height = img.height;
    full.getContext('2d').drawImage(img, 0, 0);
    // Naninovel's Sprites actor (used by diced NPCs) defaults the sprite's
    // m_Pivot to (0.5, 0.5) — geometric centre of the padded m_Rect. Tight-
    // cropping translates that pixel into the cropped frame.
    const { canvas, minX, minY, margin } = tightCrop(full);
    const pivotX = full.width  / 2 - minX + margin;
    const pivotY = full.height / 2 - minY + margin;
    return { canvas, variant: activePose, pivotX, pivotY };
  }
  const activeLayers = layersInfo
    .filter(l => activeState[l.name])
    .sort((a, b) => a.order - b.order);
  if (activeLayers.length === 0) return null;
  const canvasW = layersInfo[0]._canvasW || 2500;
  const canvasH = layersInfo[0]._canvasH || 5000;
  const full = await compositeToCanvas(activeLayers, canvasW, canvasH);
  // Layered characters: the rig root's local origin (Naninovel's @char
  // position anchor) lands at the canvas geometric centre, because the
  // bundle's auto-grown bbox is symmetric around the root in both axes.
  // Tight-cropping shifts that pixel into the cropped frame.
  const { canvas, minX, minY, margin } = tightCrop(full);
  const pivotX = canvasW / 2 - minX + margin;
  const pivotY = canvasH / 2 - minY + margin;
  return { canvas, variant: detectActivePreset(), pivotX, pivotY };
}

// Mirror of updateUI()'s preset-active rule, returning the preset name (or
// null when no preset matches the current facial layer set). Used to label
// snapshots — keeps the dropdown highlight and snapshot label in sync.
function detectActivePreset() {
  if (!compositions || Object.keys(compositions).length === 0) return null;
  const facialKeywords = ['Eyes', 'Mouth', 'Cheeks', 'Pale', 'Sweat', 'Mask'];
  const isFacial = l => facialKeywords.some(k =>
    l.name.includes(k) || l.group.includes(k));
  const isControllable = l => !isStencilReader(l);
  const facialOnNames = new Set(
    layersInfo
      .filter(l => isFacial(l)
        && (!activeHead || l.group.includes(activeHead))
        && isControllable(l)
        && activeState[l.name])
      .map(l => l.name)
  );
  for (const [name, enabled] of Object.entries(compositions)) {
    if (!enabled || enabled.length === 0) continue;
    const enabledSet = new Set(enabled);
    if (enabled.every(n => activeState[n])
        && [...facialOnNames].every(n => enabledSet.has(n))) {
      return name;
    }
  }
  return null;
}

function flashButton(id, text) {
  const btn = document.getElementById(id);
  const orig = btn.textContent;
  btn.textContent = text;
  btn.classList.add('exported');
  setTimeout(() => {
    btn.textContent = orig;
    btn.classList.remove('exported');
  }, 1200);
}

function canvasToBlob(canvas, type = 'image/png') {
  return new Promise((resolve, reject) => {
    canvas.toBlob(b => b ? resolve(b) : reject(new Error('toBlob failed')), type);
  });
}

document.getElementById('exportBtn').onclick = async () => {
  const snap = await makeSnapshotCanvas();
  if (!snap) return;
  const downloadName = snap.variant
    ? `${currentChar}_${snap.variant}_${Date.now()}.png`
    : `${currentChar}_${Date.now()}.png`;
  const a = document.createElement('a');
  a.href = snap.canvas.toDataURL('image/png');
  a.download = downloadName;
  a.click();
  flashButton('exportBtn', 'Exported!');
};

document.getElementById('sendBtn').onclick = async () => {
  const snap = await makeSnapshotCanvas();
  if (!snap) return;
  const variant = snap.variant || 'Custom';
  // Timestamp suffix keeps repeated sends of the same (character, variant)
  // independent — user can curate the library in the scene editor instead of
  // overwriting silently. The scene editor reloads on the BroadcastChannel
  // message we post below.
  const slug = `${currentChar}_${variant}_${Date.now()}`;
  let blob;
  try {
    blob = await canvasToBlob(snap.canvas, 'image/png');
  } catch (e) {
    console.error('Snapshot encode failed:', e);
    await showModal('Failed to encode snapshot. See console for details.');
    return;
  }
  const record = {
    slug,
    name: `${currentChar} (${variant})`,
    character: currentChar,
    variant,
    charType,
    width:  snap.canvas.width,
    height: snap.canvas.height,
    // Rig pivot in cropped pixel coordinates (PIL Y-down). The scene
    // editor's 3D trial render uses this to plant the snapshot's pivot at
    // Naninovel's `characterPositionY = 5 m` instead of treating the
    // snapshot's geometric centre as the pivot.
    pivotX: snap.pivotX,
    pivotY: snap.pivotY,
    blob,
    createdAt: new Date().toISOString(),
  };
  try {
    await snapshotPut(record);
  } catch (e) {
    console.error('Failed to save snapshot:', e);
    if (e && e.name === 'QuotaExceededError') {
      await showModal('Storage full. Delete unused snapshots in the scene editor and try again.');
    } else {
      await showModal('Failed to save snapshot. See console for details.');
    }
    return;
  }
  snapshotChannel.postMessage({ type: 'updated', slug });
  flashButton('sendBtn', 'Sent!');
};

// --- Reset ---

document.getElementById('resetBtn').onclick = async () => {
  if (!await showModal('Reset all customizations to default?')) return;
  resetToDefault();
  groupOffsets = {};
  advancedMode = false;
  document.getElementById('advancedToggle').classList.remove('active');
  enforceDeps();
  buildGroups();
  updateUI();
  renderPreview();
  // Persists the reset itself — next load restores defaults, not the state
  // that was just discarded.
  scheduleEditorStateSave();
};

document.getElementById('advancedToggle').onclick = (e) => {
  advancedMode = !advancedMode;
  e.currentTarget.classList.toggle('active', advancedMode);
  buildGroups();
  updateUI();
  scheduleEditorStateSave();
};

// --- Init ---

const charSelect = document.getElementById('charSelect');
charSelect.onchange = () => loadCharacter(charSelect.value);

// Friendly display label for the dropdown. The internal name stays in
// option.value so all data paths (`characters/{name}/`, image cache keys,
// export filenames) keep working unchanged.
function displayName(name) {
  if (name.startsWith('Creature')) return `${name.slice('Creature'.length)} (Creature)`;
  const m = name.match(/^Jailer([A-Z]+)$/);
  if (m) return `Jailer (${m[1]})`;
  return name;
}

// Repopulate the dropdown for the current spoilerMode. If the active character
// was just hidden by toggling spoilers off, switch to the first visible one
// rather than leaving the <select> showing a value that's no longer an option.
function rebuildCharSelect() {
  const visible = spoilerMode
    ? CHARACTERS
    : CHARACTERS.filter(c => !SPOILER_CHARACTERS.has(c));
  charSelect.innerHTML = '';
  for (const c of visible) {
    const opt = document.createElement('option');
    opt.value = c;
    opt.textContent = displayName(c);
    charSelect.appendChild(opt);
  }
  if (currentChar && visible.includes(currentChar)) {
    charSelect.value = currentChar;
  } else if (currentChar) {
    charSelect.value = DEFAULT_CHARACTER;
    loadCharacter(DEFAULT_CHARACTER);
  }
}

// Themed in-page replacement for window.confirm. Returns true on Confirm,
// false on Cancel / Esc / click-outside. Resolves on the first user action
// and clears all listeners — safe to call back-to-back.
function showModal(message) {
  return new Promise((resolve) => {
    const overlay = document.getElementById('modalOverlay');
    const messageEl = document.getElementById('modalMessage');
    const confirmBtn = document.getElementById('modalConfirm');
    const cancelBtn = document.getElementById('modalCancel');
    messageEl.textContent = message;
    overlay.classList.add('active');
    // Focus Cancel so an accidental Enter doesn't fire a destructive action.
    cancelBtn.focus();

    function close(result) {
      overlay.classList.remove('active');
      confirmBtn.onclick = cancelBtn.onclick = overlay.onclick = null;
      document.removeEventListener('keydown', onKey);
      resolve(result);
    }
    function onKey(e) {
      if (e.key === 'Escape') close(false);
      else if (e.key === 'Enter') close(true);
    }
    confirmBtn.onclick = () => close(true);
    cancelBtn.onclick = () => close(false);
    overlay.onclick = (e) => { if (e.target === overlay) close(false); };
    document.addEventListener('keydown', onKey);
  });
}

document.getElementById('spoilerToggle').onchange = async (e) => {
  const target = e.currentTarget;
  if (target.checked) {
    // Hold the toggle visually off while asking — prevents the flicker of
    // sliding on then back off if the user cancels. Programmatic `checked`
    // writes don't refire `change`, so this doesn't loop.
    target.checked = false;
    if (!await showModal('Show spoiler characters?')) {
      return;
    }
    target.checked = true;
  }
  spoilerMode = target.checked;
  rebuildCharSelect();
  scheduleEditorStateSave();
};

// Restore before the first rebuildCharSelect() — it reads spoilerMode to
// decide which characters the dropdown offers.
const restoredChar = loadEditorState();
// Applied straight to the DOM rather than by firing the handlers: a
// programmatic `checked` write doesn't refire `change`, so restoring spoilers
// doesn't re-prompt the confirm modal on every load.
document.getElementById('spoilerToggle').checked = spoilerMode;
document.getElementById('advancedToggle').classList.toggle('active', advancedMode);

const startChar = restoredChar || DEFAULT_CHARACTER;
rebuildCharSelect();
charSelect.value = startChar;
loadCharacter(startChar);

// --- Zoom & Pan ---

let zoomLevel = 1;
let panX = 0, panY = 0;
let isDragging = false, dragStartX = 0, dragStartY = 0, panStartX = 0, panStartY = 0;
const container = document.getElementById('previewContainer');
const previewArea = document.getElementById('previewArea');

function applyTransform() {
  container.style.transform = `translate(${panX}px, ${panY}px) scale(${zoomLevel})`;
}

function zoom(delta, centerX, centerY) {
  const oldZoom = zoomLevel;
  zoomLevel = Math.min(8, Math.max(0.25, zoomLevel * (1 + delta)));
  // Adjust pan to zoom toward cursor/center
  const ratio = zoomLevel / oldZoom;
  panX = centerX - ratio * (centerX - panX);
  panY = centerY - ratio * (centerY - panY);
  applyTransform();
}

// Scroll wheel zoom
previewArea.addEventListener('wheel', e => {
  e.preventDefault();
  const rect = previewArea.getBoundingClientRect();
  const cx = e.clientX - rect.left - rect.width / 2;
  const cy = e.clientY - rect.top - rect.height / 2;
  zoom(e.deltaY > 0 ? -0.15 : 0.15, cx, cy);
}, { passive: false });

// Button zoom
document.getElementById('zoomIn').onclick = () => { zoom(0.3, 0, 0); };
document.getElementById('zoomOut').onclick = () => { zoom(-0.3, 0, 0); };
document.getElementById('zoomReset').onclick = () => { zoomLevel = 1; panX = 0; panY = 0; applyTransform(); };
document.getElementById('bgToggle').onclick = () => { previewArea.classList.toggle('bg-white'); };

// Stop pointerdown from bubbling out of the zoom controls. Without this, the
// previewArea pointerdown handler below calls setPointerCapture, which steals
// the pointerup + synthesized click from the button — so onclick never fires.
document.querySelector('.zoom-controls').addEventListener('pointerdown', e => e.stopPropagation());

// Pointer-based pan + pinch-to-zoom. Pointer Events unify mouse, touch, and
// pen — one handler set covers desktop drag, mobile single-finger pan, and
// two-finger pinch. The active-pointer Map lets us:
//   * single pointer  → translate the preview (pan)
//   * two pointers    → distance ratio between them drives zoomLevel
// When a finger lifts during a pinch, the surviving pointer cleanly resumes
// pan — without re-anchoring dragStart, the container would jump to the
// pinch midpoint when the second finger left.
const activePointers = new Map();
let pinchStartDist = 0;
let pinchStartZoom = 1;

function pointerArray() { return [...activePointers.values()]; }
function pointerDistance() {
  const [a, b] = pointerArray();
  return Math.hypot(a.x - b.x, a.y - b.y);
}
function pointerMidpoint() {
  const [a, b] = pointerArray();
  return { x: (a.x + b.x) / 2, y: (a.y + b.y) / 2 };
}

previewArea.addEventListener('pointerdown', e => {
  // Ignore right / middle mouse buttons; touch + pen always report button 0.
  if (e.pointerType === 'mouse' && e.button !== 0) return;
  previewArea.setPointerCapture(e.pointerId);
  activePointers.set(e.pointerId, { x: e.clientX, y: e.clientY });

  if (activePointers.size === 1) {
    isDragging = true;
    dragStartX = e.clientX;
    dragStartY = e.clientY;
    panStartX = panX;
    panStartY = panY;
    container.classList.add('dragging');
  } else if (activePointers.size === 2) {
    isDragging = false;
    container.classList.remove('dragging');
    pinchStartDist = pointerDistance();
    pinchStartZoom = zoomLevel;
  }
});

previewArea.addEventListener('pointermove', e => {
  if (!activePointers.has(e.pointerId)) return;
  activePointers.set(e.pointerId, { x: e.clientX, y: e.clientY });

  if (activePointers.size === 2) {
    if (pinchStartDist === 0) return;
    const ratio = pointerDistance() / pinchStartDist;
    const targetZoom = Math.min(8, Math.max(0.25, pinchStartZoom * ratio));
    const mid = pointerMidpoint();
    const rect = previewArea.getBoundingClientRect();
    const cx = mid.x - rect.left - rect.width / 2;
    const cy = mid.y - rect.top - rect.height / 2;
    // Apply absolute zoom around the pinch midpoint. We can't use zoom() —
    // that takes a delta and would compound across move events; we want a
    // single ratio relative to pinchStartZoom.
    const oldZoom = zoomLevel;
    zoomLevel = targetZoom;
    const scaleRatio = zoomLevel / oldZoom;
    panX = cx - scaleRatio * (cx - panX);
    panY = cy - scaleRatio * (cy - panY);
    applyTransform();
  } else if (isDragging && activePointers.size === 1) {
    panX = panStartX + (e.clientX - dragStartX);
    panY = panStartY + (e.clientY - dragStartY);
    applyTransform();
  }
});

function endPointer(e) {
  if (!activePointers.has(e.pointerId)) return;
  activePointers.delete(e.pointerId);
  pinchStartDist = 0;
  if (activePointers.size === 0) {
    isDragging = false;
    container.classList.remove('dragging');
  } else if (activePointers.size === 1) {
    // One finger lifted mid-pinch — re-anchor pan to the surviving pointer's
    // current position so the next move doesn't snap.
    const remaining = pointerArray()[0];
    dragStartX = remaining.x;
    dragStartY = remaining.y;
    panStartX = panX;
    panStartY = panY;
    isDragging = true;
  }
}
previewArea.addEventListener('pointerup', endPointer);
previewArea.addEventListener('pointercancel', endPointer);

// --- Drawer (mobile bottom sheet) ---
// CSS gates visibility behind @media (max-width: 768px); these handlers run
// on every viewport but the affected elements only render on mobile, so the
// extra clicks on desktop are harmless.
const drawerToggleBtn = document.getElementById('drawerToggle');
const drawerBackdrop = document.getElementById('drawerBackdrop');
const drawerCloseBtn = document.getElementById('drawerClose');
drawerToggleBtn.onclick = () => document.body.classList.add('drawer-open');
drawerBackdrop.onclick = () => document.body.classList.remove('drawer-open');
drawerCloseBtn.onclick = () => document.body.classList.remove('drawer-open');

