# manosaba_editor

## Scripts

The character and scene data is extracted directly from the game's Unity AssetBundles. There are four extractors and one probe; pick the extractor based on what `inspect_bundle.py` reports.

### `scripts/inspect_bundle.py`

Probes a Unity AssetBundle and prints what's inside (object inventory, Texture2D / Sprite summaries, GameObject hierarchy). Use this first to decide which extractor applies.

```bash
python3 scripts/inspect_bundle.py <bundle>
```

Decision rule:
- **Has GameObject + Transform + SpriteRenderer + Sprite** → layered character rig, use `extract_bundle.py`.
- **Has a single MonoBehaviour with a `sprites` list (Naninovel DicedSpriteAtlas) and no Transform tree** → NPC/creature, use `extract_diced_atlas.py`.
- **Has only one Texture2D + one Sprite (no Transform tree, no MonoBehaviour)** → full-frame scene asset (background / still), use `extract_background.py`.
- **Has SpriteAtlas objects with no Transform tree, OR has GameObject + RectTransform + Image MonoBehaviours** → UI bundles. There is no general UI extractor; the only in-tree script targeting UI bundles is `extract_scene_adv.py`, which is scoped to the four Adv-scene editor prefabs. The broader `ui/` tree in this repo is a frozen artifact (see "Static `ui/` tree" below).

### `scripts/extract_bundle.py`

Extracts a layered character (Body, Arms, Eyes, Mouth, Effect_*, ClippingMask_*, …) from an AssetBundle into the renderer's `pos`-based schema.

```bash
python3 scripts/extract_bundle.py <bundle> <out_dir>
```

Outputs:
- `out_dir/{name}.png` — bbox-cropped sprite per layer (UnityPy resolves Sprite.image to the cropped, oriented PNG).
- `out_dir/layers.json` — `{ canvas_size: [W, H], intrinsic_scale: <float>, layers: [{ name, group, order, empty, pos: [x, y], render: {...}, ... }] }`.

How it works:
1. Walks the GameObject/Transform tree, accumulating local positions to a world position per leaf.
2. For each leaf with a SpriteRenderer + Sprite, computes the sprite's pixel-space footprint from `m_Rect`, `m_Pivot`, and `m_PixelsToUnits`.
3. **Auto-grow canvas (option C)**: `canvas_size` is the bbox of every leaf footprint; nothing is hand-tuned.
4. Y is flipped from Unity (Y-up) to PIL (Y-down). `pos` is the top-left of the sprite on the canvas.
5. **`intrinsic_scale`** is the prefab's pre-baked uniform Transform scale — a per-character height equalizer (e.g. Ema/Sherry=0.6, Hanna=0.54, Leia/Hiro/Meruru/Nanoka=0.75, Margo=0.642, Miria=0.66, Noah=0.588). Recorded as metadata, **not** folded into leaf `pos`: leaves stay in raw pixel space, and any consumer that wants on-stage size multiplies by `intrinsic_scale`. Defaults to `1.0` when no non-identity Transform is present. The runtime treats `script_scale = 1.0` as already including this factor.
6. **`render` is derived from the SpriteRenderer's material**:
   - `blend` from the shader name (`Naninovel Extender/<Default|Multiply|Overlay|Softlight>` → `source-over` / `multiply` / `overlay` / `softlight`).
   - `stencil` from material props: `_StencilOp=2 (Replace) ∧ _StencilRef>0` → `{ role: "write", ref, cutoff? }`; `_StencilComp=4 (Equal) ∧ _StencilRef>0` → `{ role: "read", ref }`; otherwise null.
   - `tint` from `m_Color` when not `(1,1,1,1)`.
7. **Auto-derives `requires`** for layers whose name embeds an arm pose (e.g. `Effect_Back_ArmR07` → `requires: "ArmR07"`, `ArmL01_Softlight` → `requires: "ArmL01"`).

Re-running on an existing `out_dir` **preserves curation fields** in `layers.json` by merging on layer name: `group`, `requires`, `excludes_groups`, `requires_groups`, `auto_enable`. The `empty` flag and everything else (positions, render, order) is bundle-driven and overwritten.

### `scripts/extract_diced_atlas.py`

Extracts a Naninovel DicedSpriteAtlas — a flat list of full-frame poses with no rig — into the diced schema used for NPCs/creatures.

```bash
python3 scripts/extract_diced_atlas.py <bundle> <out_dir>
```

Outputs:
- `out_dir/{pose_name}.png` — one PNG per pose, padded to the shared `m_Rect` so every pose lands on a common canvas and aligns pixel-for-pixel.
- `out_dir/meta.json` — `{ type: "diced", name, canvas_size: [W, H], poses: [<sprite_name>, …] }`.

UnityPy reconstructs each diced sprite to its mesh-vertex bbox (smaller than `m_Rect`); `_pad_to_rect` re-anchors that reconstruction inside the full rect using the vertex bbox + rect origin. Poses are sorted with a natural-numeric key so `"10"` follows `"9"` instead of `"1"`.

### `scripts/extract_background.py`

Extracts a single full-frame image (background or still) from a Texture2D+Sprite-only bundle. The Sprite covers the entire texture, so we save the Texture2D directly — no rig walk, no diced reconstruction.

```bash
python3 scripts/extract_background.py <bundle> <out_dir>
```

Outputs:
- `out_dir/{m_Name}.png` — full-resolution PNG named after the asset's `m_Name` (e.g. `Background_001_001`, `Still_001_001`). Zero-padded so lexical sort = numeric sort.

Re-running on an existing PNG is a no-op (the file is preserved as-is). The script does not write `meta.json`; that is built separately by `build_backgrounds_meta.py` once all bundles are extracted.

### Static `ui/` tree

The `ui/` tree (per-prefab JSON layouts + per-atlas sprite folders) is a frozen artifact — no script in the repo regenerates it. It was originally produced by a now-removed `extract_ui_layers.py` UI extractor; if you need to re-extract from updated bundles, recover that script from git history. `compose_ui_panel.py` and the schema below remain the contract for any consumer that reads these files.

Layout:

```
out_dir/
  <AtlasName>/
    <sprite_name>.png          # one PNG per unique sprite, deduplicated
    ...
    layers.json                # sprite-centric: { sprites: [{name, file, size, pivot, usages: [...]}, ...] }
  <PrefabName>.json            # prefab-centric: { canvas_size, name, source, layers: [...] }
  meta.json                    # { type: "ui_layout_index", canvas_size, atlases: [...], prefabs: [...] }
```

Per-prefab `<PrefabName>.json` schema (drop-in compositing input — mirrors `characters/{Character}/layers.json` shape, with `file` extended to a relative path):

```json
{
  "canvas_size": [2560, 1440],
  "name":   "NormalPrinter",
  "source": "naninovel-textprinters_assets_all.bundle",
  "layers": [
    {
      "name":  "NormalPrinter_Frame_Top",
      "file":  "UI_Adv/NormalPrinter_Frame_Top.png",  // relative to out_dir
      "go":    "Frame_Top",
      "atlas": "UI_Adv",
      "group": "Wrapper/Default-Stream",
      "order": 0,
      "pos":   [2273, 0],                              // top-left on canvas (PIL Y-down)
      "size":  [287, 470]                              // RectTransform-resolved size
    },
    ...
  ],
  "containers": [                                      // optional, non-rendered layout rules
    {
      "go":    "Content",
      "group": "Wrapper",
      "pos":   [1106, 787],
      "size":  [899, 0],                               // 0 dims = ContentSizeFitter will grow at runtime
      "pivot": [0.5, 0.5],                             // Unity Y-up pivot, [x, y] in [0..1]
      "layout_group": {
        "kind": "VerticalLayoutGroup",                 // or HorizontalLayoutGroup / GridLayoutGroup /
                                                       // HorizontalOrVerticalLayoutGroup (ambiguous)
        "padding": [0, 0, 0, 0],
        "spacing": 80.0,                               // float for HV; [x, y] for Grid
        "child_alignment": "MiddleRight",              // Unity TextAnchor enum
        "child_control_width": false,
        "child_control_height": false,
        "child_force_expand_width": false,
        "child_force_expand_height": false
        // Grid-specific extras: cell_size, start_corner, start_axis, constraint, constraint_count
      },
      "size_fitter": {
        "horizontal_fit": "Unconstrained",             // Unconstrained / MinSize / PreferredSize
        "vertical_fit":   "PreferredSize"
      },
      "placement": {                                   // pre-resolved geometry for compositors
        "kind": "vertical",                            // or "horizontal" or "grid"
        "x_anchor_canvas_pil": 2005,                   // children's cross-axis anchor on canvas
        "x_anchor_basis": "right",                     // "left" | "center" | "right"
        "y_pivot_canvas_pil": 787,                     // pivot point for column extension
        "y_pivot_position": 0.5,                       // 0=column-top in PIL, 1=column-bottom
        "spacing": 80.0,
        "padding_top": 0,
        "padding_bottom": 0
      }
    }
  ],
  "root_intrinsic_size": [1099, 318],                  // optional; set when root has non-zero size_delta.
                                                       // Indicates a *widget* prefab (e.g. ChoiceButton_Trial)
                                                       // whose natural size is what gets laid out under a
                                                       // parent layout group. Screen prefabs (NormalPrinter)
                                                       // omit this — their static layout fills the canvas.
  "texts": [                                           // optional; TextMeshProUGUI leaves
    {
      "go":          "MessageLabel",
      "group":       "Wrapper",
      "order":       7,                                // shared counter with `layers` for global z-order
      "pos":         [573, 1163],                      // top-left on canvas (PIL Y-down)
      "size":        [1414, 200],                      // RectTransform-resolved rect
      "pivot":       [0.5, 0.5],                       // Unity Y-up

      "text":        "Message",                        // placeholder; Naninovel overrides at runtime
      "font_asset_pptr": [2, -4585659588899376959],    // [file_id, path_id] — font lives outside our bundles
      "font_size":   48.0,
      "font_weight": 400,                              // TMP FontWeight: 400=Regular, 700=Bold
      "font_style":  "Normal",                         // FontStyles bitmask, '|'-joined ("Bold|Italic")
      "h_align":     "Left",                           // 1/2/4/8/16/32 -> Left/Center/Right/Justified/Flush/Geometry
      "v_align":     "Top",                            // 256/512/1024/2048/4096/8192 ->
                                                       //   Top/Middle/Bottom/Baseline/Geometry/Capline
      "margin":      [0, 0, 0, 0],                     // [L, T, R, B]
      "wrapping":    "Normal",                         // 0..3 -> NoWrap/Normal/PreserveWhitespace/PreserveWhitespaceNoWrap
      "overflow":    "Overflow",                       // 0..6 -> Overflow/Ellipsis/Masking/Truncate/ScrollRect/Page/Linked
      "color":       [1, 1, 1, 1],                     // optional; omitted when default white
      "spacing":     {"character": 0, "word": 0,
                      "line": 0, "paragraph": 0},      // optional; omitted when all zero
      "auto_size":   {"min": 16.0, "max": 64.0},       // optional; only when m_enableAutoSizing is set
      "ruby":        {"vertical_offset": "0.875em",    // optional; only on Naninovel RubyTextPrinter
                      "size_scale": 0.4,
                      "add_line_height": false}
    }
  ]
}
```

The `containers` array describes **runtime layout rules**, not rendered geometry. A node appears here when its GameObject carries a `LayoutGroup` (Horizontal / Vertical / Grid) or `ContentSizeFitter` — these tell a compositor how children would be laid out when the prefab is populated at runtime by Naninovel's choice / list / menu drivers. Examples: `TrialChoicePanel/Wrapper/Content` is the `VerticalLayoutGroup` that stacks `ChoiceButton_Trial@*` instances; `GalleryUI/.../StillGrid` is the `GridLayoutGroup` (3-col 672×392) where unlocked stills get instantiated.

The `placement` field on each `containers[i]` (when the layout kind is unambiguous) provides pre-resolved canvas coordinates so a consumer doesn't need to redo the pivot/anchor math. Composing under a layout group reduces to plugging in N (child count) + a child size:

- **Grid**: enumerate cells via `first_cell_canvas_pil + (col * step.x, row * step.y)`. Wrap rows/columns by `constraint_count` based on `constraint` (FixedColumnCount / FixedRowCount). The `start_axis` controls fill order.
- **Vertical**: `column_height = N * child_h + (N-1) * spacing + padding_top + padding_bottom`; `column_top_pil = y_pivot_canvas_pil - column_height * y_pivot_position`. Each child's top: `column_top_pil + padding_top + k * (child_h + spacing)`. Cross-axis (x): `x_anchor_canvas_pil` adjusted by `x_anchor_basis` (`right` → subtract child_w; `center` → subtract child_w/2; `left` → use as-is).
- **Horizontal**: symmetric (swap x/y, column → row).

`HorizontalOrVerticalLayoutGroup` (kind that couldn't be specialized to H or V) gets no `placement` field — orientation must be inferred elsewhere or the layout simulated manually.

`HorizontalOrVerticalLayoutGroup` is the parent class of `HorizontalLayoutGroup` and `VerticalLayoutGroup` — the two are byte-identical in serialized typetree form (only the `m_Script` PPtr differs, which references an external Unity assembly we don't load). The script tries to specialize the label using the sibling `ContentSizeFitter` direction (e.g. `vertical_fit=PreferredSize` → `VerticalLayoutGroup`); when that's ambiguous (both axes constrained, or no fitter present), it stays as the parent-class label.

The `texts` array captures every `TextMeshProUGUI` leaf — same `pos` / `size` / `pivot` semantics as `layers` (top-left on canvas in PIL Y-down, RectTransform-resolved rect, Unity Y-up pivot). The `order` field shares its counter with `layers`, so a text-aware compositor can merge `layers + texts` and sort by `order` for true z-order. The placeholder string in `text` is overwritten at runtime by Naninovel — most labels render dynamic content (dialog text, author name, choice labels, credits roll). The `font_asset_pptr` points to a TMP font asset that lives outside the prefab bundle (a `(file_id, path_id)` reference into one of the bundle's externals); to actually rasterize, the renderer needs to either resolve that asset from the game's main data or substitute a TTF/OTF (the game ships Noto Serif KR for the Korean build and Tsukushi Mincho for the Japanese build; see `compose_ui_panel.py` for the open Noto Serif CJK stand-ins). Optional fields (`color`, `spacing`, `auto_size`, `ruby`) are omitted when at default values.

Per-atlas `<AtlasName>/layers.json` schema (sprite-centric — best for browsing or "where is this sprite used"):

```json
{
  "atlas": "UI_Adv",
  "canvas_size": [2560, 1440],
  "sprites": [
    {
      "name":  "Balloon_Default",
      "file":  "Balloon_Default.png",   // basename within this atlas dir
      "size":  [1099, 318],              // intrinsic sprite size
      "pivot": [0.5, 0.5],
      "usages": [
        { "prefab": "ChoiceButton_Trial", "go": "Balloon", "group": "...",
          "order": 0, "pos": [0, 0], "rect_size": [2560, 1440] },
        ...
      ]
    },
    ...
  ]
}
```

Optional fields, omitted when default: `color: [r,g,b,a]` if `Image.m_Color != (1,1,1,1)`, `material: "<name>"` if `Image.m_Material != null`. These appear on per-prefab layer entries and per-atlas usage entries.

Extraction-time invariants baked into the static tree:
- **Empty prefabs** (templates with no Image leaves, no text leaves, *and* no layout-bearing containers — e.g. `ClickThroughPanel`) were skipped, so no JSON exists for them. Prefabs with text-only content (`DebatePrinter`, `SoundTestUI`) or runtime layout containers (`AdvChoicePanel`) were kept — their `layers` array may be empty but `texts` and/or `containers` carry actionable data.
- **CanvasGroup gating**: any subtree under a GameObject with `CanvasGroup.m_Alpha == 0` is absent (handles NormalPrinter's hidden `Stream` template, etc.).
- **Root rect convention**: prefab roots' RectTransform fields were treated as canvas-sized regardless of serialized stubs (because at runtime the prefab is parented under a Canvas); children measure against the full canvas.
- **Sprite deduplication**: each unique sprite appears once in its atlas folder; per-prefab JSONs and per-atlas usage lists both reference the single PNG.
- **Atlas filtering**: only atlases that at least one extracted prefab references have a folder.

### `scripts/extract_character_meta.py`

Extracts per-character metadata into `characters/configuration.json`. Combines two sources:

1. **`AuthorData`** (typetree-readable) from `general-data_assets_all.bundle` — TMP rich-text per character per locale (with `<color=%COLOR%>` placeholder), e.g. `<color=%COLOR%><size=136>桜</size></color><space=4><voffset=-2><size=73>羽</size></voffset>...`.
2. **`CharactersConfiguration`** (typetree stripped, byte-signature parsed) from `resources.assets` — per-character `nameColor` (RGBA), Japanese `displayName`, asset GUID. The parser anchors on the literal byte sequence `\x0a\x00\x00\x00Characters\x00\x00\x02\x00\x00\x00` (the `Loader.ResourcesPath="Characters"` followed by `providerTypes` count = 2), which appears exactly once per `CharacterMetadata` record.

```bash
python3 scripts/extract_character_meta.py <resources.assets> <general-data.bundle> [<out.json>]
```

Output schema:
```json
{
  "source": "...",
  "locales": {"0": "ja", "1": "en-US", "2": "ko", "3": "zh-Hans", "4": "zh-Hant"},
  "characters": [
    {
      "id":             "Ema",
      "displayName_ja": "桜羽エマ",
      "nameColor":      [1.0, 0.572549, 0.705882, 1.0],
      "nameColor_hex":  "#FF92B4",
      "asset_guid":     "f5c651f0-c6e7-42b4-bdf0-61d6a24aa585",
      "tagged_name": {
        "ja":      "<color=%COLOR%><size=136>桜</size></color><space=4>...",
        "ko":      "<color=%COLOR%><size=136>사</size></color><space=4>...",
        "zh-Hans": "<color=%COLOR%><size=136>櫻</size></color>...",
        "en-US":   ""
      }
    }, ...
  ]
}
```

41 characters total: 14 main cast + 13 `Creature*` doppelgangers (each reuses its human form's `nameColor`) + 14 mob/narrator entries (default white).

### `scripts/build_backgrounds_meta.py`

Scans `scene/backgrounds/main/` and `scene/backgrounds/stills/` and emits `scene/backgrounds/meta.json` — the index the scene editor reads to populate its picker.

```bash
python3 scripts/build_backgrounds_meta.py [<root>]   # default root: ./scene/backgrounds
```

Output schema:
```json
{
  "main":    [{ "id": "NNN_MMM", "name": "Background_NNN_MMM", "file": "...png", "size": [W, H] }, ...],
  "stills":  [{ "id": "NNN_MMM", "name": "Still_NNN_MMM",      "file": "...png", "size": [W, H] }, ...],
  "utility": [{                  "name": "Grid_001",           "file": "...png", "size": [W, H], "from": "main" }, ...]
}
```

`utility` collects the non-numbered helpers shipped alongside numbered backgrounds (`Grid_001`, `Grid_002`, `SolidColor`, `Transparent`) so the scene editor can offer them as primitives without polluting the numbered list. Each utility entry carries a `from` field naming the directory it came from (currently always `main`). Numeric ids are zero-padded, so lexical sort yields the correct order.

### `scripts/build_bg_thumbs.py`

Bakes the thumbnails the scene editor's background picker previews on hover. The source art is
4096×2048 and 5–10 MB per file — far too heavy to preview directly — so the picker shows these
instead.

```bash
python3 scripts/build_bg_thumbs.py [<root>]   # default root: ./scene/backgrounds
```

Reads `scene/backgrounds/meta.json` and processes its `main` and `stills` lists. The `utility`
list is skipped, matching the picker, which omits those author/debug helpers.

Outputs:
- `scene/backgrounds/thumbs/{main,stills}/{name}.webp` — 480×270 WebP, quality 80. About 10–30 KB
  each, ~3.9 MB for all 230. Gitignored, like every other generated image in the repo.

The crop mirrors `scene.js`'s `renderBackground`: cover-fit with `max(CANVAS_W/iw, CANVAS_H/ih)`,
then center-crop to 2560×1440. Source art is 2:1 but the stage is 16:9, so ~10% of image height
never reaches the screen; cropping any other way would make thumbnails promise framing the stage
won't deliver. **That correspondence is the script's one invariant — if `renderBackground` ever
changes its fit, change this script with it.**

Re-runnable and incremental: a thumbnail at least as new as its source is skipped, and thumbs whose
source is gone are pruned. A cold run over all 230 takes about a minute (decoding the 4096×2048
PNGs dominates, not the resize).

The picker derives the thumbnail URL as `thumbs/{dir}/{name}.webp` rather than reading it from
`meta.json`, so this script and `build_backgrounds_meta.py` stay independent. A missing thumbnail
404s and the picker shows a "run scripts/build_bg_thumbs.py" placeholder.

### `scripts/extract_scene_adv.py`

Self-contained extractor for the scene editor's Adv-mode dialog frame: walks four prefabs (`NormalPrinter`, `AutoToggle`, `ControlPanel`, `WitchBookButtonUI`) straight out of the source AssetBundles, applies the resting-frame filters (`dropLayers` / `keepGroupPrefix`) offline, saves referenced sprite PNGs with flat basenames, and emits one consolidated `scene/adv/meta.json` so `scene.js` can fetch one layout file and render. Carries its own bundle walker + sprite reconstruction (no dependency on the static `ui/` tree).

```bash
python3 scripts/extract_scene_adv.py <out_root> <bundle> [<bundle> ...]
# bundles auto-classify (SpriteAtlas-bearing → sprite source; rest → prefab source).
# Three bundles cover the four target prefabs:
#   general-sprites_assets_all.bundle           (sprite source)
#   naninovel-textprinters_assets_all.bundle    (NormalPrinter)
#   naninovel-ui_assets_all.bundle              (AutoToggle, ControlPanel, WitchBookButtonUI)
```

`scene/adv/meta.json` schema is documented in the script's docstring. Re-runnable; deletes stale PNGs that aren't in the new sprite set.

### `scripts/extract_trial_digits.py`

Extractor for the `TrialsClockFont` TMP sprite atlas — the 11 digit/colon glyphs the debate timer renders via `<sprite name="0">…<sprite name=":">…` tags. Reads the `TrialsClockFont` MonoBehaviour's `m_SpriteCharacterTable` (TMP tag → glyph index) + `m_GlyphTable` (glyph index → sprite path_id + per-glyph metrics) from `general-fonts-common_assets_all.bundle`, then saves each sprite padded to its full `m_Rect` (64×64) using the same pivot-anchored pad as the non-diced sprites in `extract_bundle.py`.

```bash
python3 scripts/extract_trial_digits.py <out_dir> <bundle>
# Typical:
python3 scripts/extract_trial_digits.py scene/trial/digits \
  <…>/general-fonts-common_assets_all.bundle
```

Outputs:
- `out_dir/<tag>.png` — 11 PNGs (`0.png`–`9.png`, `colon.png`). The `:` tag is renamed to `colon` for filesystem/URL safety; the tag-to-file mapping is preserved in `meta.json`.
- `out_dir/meta.json` — `{ atlas, atlas_native_em, sprites: [{ tag, file, rect:[W,H], metrics:{width,height,horizontalBearingX,horizontalBearingY,horizontalAdvance}, scale }, …] }`.

**Sizing note for the renderer**: TMP's sprite-glyph scale formula is `font_size × m_Scale / primary_font.faceInfo.pointSize`, but when the sprite asset has empty `m_FaceInfo` (as `TrialsClockFont` does), TMP's fallback uses the *primary text font*'s `m_FaceInfo.ascentLine` as the visual reference instead. For the trial label that's `TsukushiMincho.ascentLine = 79.2`, giving an empirically-correct in-game match at `52 / 79.2 ≈ 0.657` (atlas px → screen px). The `atlas_native_em` field in `meta.json` records the atlas's own em (64) for documentation; the actual divisor used by `renderTimerSprites` is the primary font's ascentLine, hardcoded in `scene.js` (`TIMER_PRIMARY_FONT_ASCENT_PX`). The line baseline uses a separate scale — `font_size / pointSize` — pulled from `TIMER_PRIMARY_FONT_POINT_SIZE` (90). The two scales diverge because TMP measures the visible glyph and the surrounding line geometry against different reference dimensions of the same font asset; see the constant block in `scene.js` for the full rationale.

Re-runnable; overwrites existing PNGs and rewrites `meta.json`.

### `scripts/build_scene_authors.py`

Bakes the slim author metadata the scene editor needs into `scene/authors.json`. Extracts only the three fields `scene.js` reads (`id`, `nameColor_hex`, `tagged_name`) from `characters/configuration.json`. Decouples the editor's deploy data from the offline compositor's full configuration.

```bash
python3 scripts/build_scene_authors.py [<src>] [<dst>]
                                       # defaults: ./characters/configuration.json  ./scene/authors.json
```

Re-run after `extract_character_meta.py` regenerates `characters/configuration.json`.

## Bundle extraction workflow

### 1. Probe the bundle

```bash
python3 scripts/inspect_bundle.py path/to/CharacterX_bundle
```

Look at the object inventory and hierarchy depth to pick layered vs diced (see decision rule above).

### 2. Run the extractor

Layered:
```bash
python3 scripts/extract_bundle.py path/to/Alisa_bundle characters/Alisa
```

Diced:
```bash
python3 scripts/extract_diced_atlas.py path/to/Warden_bundle characters/Warden
```

Background / still (bulk; the extractor is single-bundle, so loop over a directory):
```bash
for f in path/to/mainbackground/*.bundle; do python3 scripts/extract_background.py "$f" scene/backgrounds/main;   done
for f in path/to/stills/*.bundle;         do python3 scripts/extract_background.py "$f" scene/backgrounds/stills; done
python3 scripts/build_backgrounds_meta.py     # rebuild scene/backgrounds/meta.json
python3 scripts/build_bg_thumbs.py            # rebuild the picker's hover thumbnails
```

Adv-scene editor UI (4 prefabs only — there is no general UI extractor; the broader `ui/` tree is frozen):
```bash
python3 scripts/extract_scene_adv.py scene/adv \
  path/to/general-sprites_assets_all.bundle \
  path/to/naninovel-textprinters_assets_all.bundle \
  path/to/naninovel-ui_assets_all.bundle
```

### 3. Curate `layers.json` (layered only)

The bundle gives geometry + render flags but not behavioural rules. Hand-add curation fields for the renderer's UI logic:

- `requires`: dependency on another layer (auto-derived for arm-pose names; add the rest).
- `excludes_groups`: when this layer activates, clear all layers in these group leaves (e.g. selecting `Arms01` clears `ArmL` + `ArmR`).
- `requires_groups`: `{ groupLeaf: defaultName }` — when this layer is cleared, ensure the named group has at least the listed default active.
- `auto_enable: true`: dependent layer flips on automatically when its `requires` parent toggles off→on.

Re-running `extract_bundle.py` preserves these fields, so curation survives bundle re-extraction.

### 4. Author `compositions.json` and `default.json` (layered only)

- `compositions.json` — expression presets: `{ "Normal1": ["Eyes_Normal_Open01", "Mouth_Normal_Closed01", ...] }`.
- `default.json` — initial state: `{ "enabled": ["Body", "ArmL01", ...] }` (single-head) or `{ "Head01": { "enabled": [...] }, "Head02": { "enabled": [...] } }` (multi-head).

These files are not generated by the extractors — they encode editorial choices about which combinations make sense for the UI.

### 5. Extract character meta (one-shot, before any author-aware UI render)

```bash
python3 scripts/extract_character_meta.py \
  /path/to/<game>_Data/resources.assets \
  /path/to/general-data_assets_all.bundle \
  characters/configuration.json
```

Produces a single JSON keyed by character `id` with `nameColor`, Japanese display name, asset GUID, and per-locale TMP rich-text (used by the AuthorLabel). The compositor reads it when `MANOSABA_AUTHOR=<id>` is set; without that env var the compositor falls back to the bundled placeholder string ("Author") so the file is only required for author-aware renders. See "Compositing" below for env-var details.

## Compositing UI panels

`scripts/compose_ui_panel.py` renders a UI prefab + optional widget instances + optional background as a 2560×1440 PNG. Linear-space alpha throughout; converts back to sRGB on save.

```bash
python3 scripts/compose_ui_panel.py <panel> "<widget1>[,widget2,...]|-" <out.png> [bg.png]
```

Env-var configuration (all optional):

| variable | default | effect |
|---|---|---|
| `MANOSABA_LOCALE` | `ko` | Selects font face: `ko` → Noto Serif CJK KR (matches game's Noto Serif KR), `ja` → Noto Serif CJK JP (Mincho stand-in for Tsukushi Mincho). Also picks which `tagged_name[locale]` to use for author overrides. |
| `MANOSABA_AUTHOR` | unset | When set to a character `id` from `characters/configuration.json` (e.g. `Ema`), the AuthorLabel text leaf is replaced with that character's per-locale TMP rich-text, with `<color=%COLOR%>` substituted from `nameColor_hex`. Honours stack-semantic `<color>` / `<size>` / `<voffset>` / `<space>` / `<cspace>` tags. |

The text rasterizer uses PIL/freetype + a small TMP rich-text walker. For `Capline` vertical alignment (used by NormalPrinter's AuthorLabel) the cap-top reference blends `OS/2.sTypoAscender` with `hhea.ascender` at a 0.55/0.45 mix — empirically tuned to match the in-game NormalPrinter render; the actual TMP_FontAsset `m_FaceInfo.capLine` isn't shipped in any AssetBundle. See `_render_tagged_text` in `compose_ui_panel.py` for the math.

See `docs/adv_ui_compositing.md` (NormalPrinter walkthrough), `docs/trial_ui_compositing.md` (TrialChoicePanel walkthrough), and `docs/text_shadows.md` (TMP underlay/outline catalog + per-leaf material assignment) for end-to-end recipes.

## Python dependencies

| package | used for |
|---|---|
| `UnityPy` | every extractor — reads AssetBundles + `resources.assets` |
| `Pillow` (PIL) | sprite I/O, text rasterization, font loading via freetype |
| `numpy` | linear-space pixel buffers, sprite resize, alpha compositing |
| `fontTools` | `compose_ui_panel.py` only — reads `OS/2.sTypoAscender` from font files for Capline alignment; gracefully degrades to a 0.85 default if missing or the font can't be parsed |

## Tests

```bash
python3 -m pytest tests/ -q                  # extractor/script tests
node --test tests/bg_picker.test.mjs         # background picker model tests
```

`package.json` exists only to mark `.js` files as ES modules so Node's built-in test runner can
import them. **It declares no dependencies and there is no build step** — the site is still served
straight from disk with `python3 -m http.server 8080`. Never add a dependency to it without a
deliberate decision to give that up.

`bg_picker.js` keeps everything above `createBgPicker` pure and DOM-free precisely so `node --test`
can import it without a DOM; the widget's own DOM behaviour is exercised by hand through
`bg_picker_test.html`.

## Project structure

- `characters/{Character}/` — Layered character output. Contains `layers.json`, `compositions.json`, `default.json`, and one PNG per layer.
- `characters/{Character}/layers.json` — Canvas size, prefab intrinsic scale, and per-layer geometry + render descriptor + curation fields. Generated by `extract_bundle.py`.
- `characters/{Character}/compositions.json` — Expression presets (facial layer sets). Hand-authored.
- `characters/{Character}/default.json` — Default enabled layers on load. Hand-authored.
- `characters/{NPC}/` — Diced atlas output. Contains `meta.json` and one PNG per pose. Generated by `extract_diced_atlas.py`.
- `scene/backgrounds/main/{Background_NNN_MMM}.png` — Numbered scene backgrounds. Generated by `extract_background.py`.
- `scene/backgrounds/stills/{Still_NNN_MMM}.png` — Numbered story stills / event CGs. Generated by `extract_background.py`.
- `scene/backgrounds/meta.json` — Index of all backgrounds + stills + utility primitives, read by the scene editor's bg picker. Generated by `build_backgrounds_meta.py`.
- `scene/backgrounds/thumbs/{main,stills}/{name}.webp` — 480×270 WebP thumbnails previewed on hover by the scene editor's background picker. Stage-framed (same cover-fit + center-crop as `renderBackground`). Generated by `build_bg_thumbs.py`; gitignored.
- `scene/adv/meta.json`, `scene/adv/<sprite>.png` — Self-contained Adv-mode dialog frame data for the scene editor: one consolidated layout JSON (4 prefabs filtered to resting-frame leaves) plus the deduplicated sprite PNGs. Generated by `extract_scene_adv.py` directly from the source UI bundles (`general-sprites` + `naninovel-textprinters` + `naninovel-ui`).
- `scene/trial/digits/<tag>.png`, `scene/trial/digits/meta.json` — `TrialsClockFont` TMP sprite atlas: 11 glyph PNGs (`0.png`–`9.png`, `colon.png`) plus per-glyph TMP metrics (bearing, advance, scale). Read by `renderTimerSprites` in `scene.js` to render the debate-scene timer's `<sprite name="X">` tags. Generated by `extract_trial_digits.py` from `general-fonts-common_assets_all.bundle`.
- `scene/authors.json` — Slim per-character metadata used by the scene editor's AuthorLabel: only `id`, `nameColor_hex`, and `tagged_name`. Generated by `build_scene_authors.py` from `characters/configuration.json`.
- `bg_picker.js` — The scene editor's background dropdown: a custom listbox that floats a thumbnail popover beside the hovered row (a native `<select>` can't — browsers don't fire hover on `<option>`). Its pure logic (`filterGroups`, `nextActiveValue`, `tapAction`) is exported DOM-free and tested by `tests/bg_picker.test.mjs`; `bg_picker_test.html` exercises the widget standalone.
- `characters/configuration.json` — Per-character metadata: `nameColor`, Japanese `displayName`, asset GUID, and per-locale TMP rich-text for the AuthorLabel. Generated by `extract_character_meta.py` from `resources.assets` + `general-data_assets_all.bundle`.
- `characters/font_materials.json` — TMP material variants for `SourceHanSerifSC` (9 entries: `Atlas Material`, `Font Material`, `Shadow1..6`, `Outline_Shadow`) with the `_Underlay*` / `_Outline*` shader properties. Used by `compose_ui_panel.py` to apply per-leaf text shadow. See [`docs/text_shadows.md`](./docs/text_shadows.md). Extracted from `general-fonts-sourcehanserifsc_assets_all.bundle`.
- `ui/<AtlasName>/<sprite>.png`, `ui/<AtlasName>/layers.json` — Per-atlas folders: deduplicated sprite PNGs plus sprite-centric `layers.json` (each sprite's `usages` list shows every prefab that places it). **Frozen artifact** — no script in this repo regenerates these files; recover the original `extract_ui_layers.py` from git history if you need to re-extract.
- `ui/<PrefabName>.json`, `ui/meta.json` — Per-prefab compositing inputs at the top level (drop-in `layers.json`-style format with `file` paths pointing into the atlas folders) plus a top-level index of all atlases and prefabs. **Frozen artifact** (same caveat as above). Read by `compose_ui_panel.py`.
- `_ref/`, `characters/{Character}/crop/`, `_backup/` — Artifacts of the previous sprite-sheet pipeline. Not used by the current extractors or renderer; kept for reference.

## Frontend (`index.html` + `styles.css` + `app.js`)

Standalone HTML/CSS/JS character portrait editor. Serve with `python3 -m http.server 8080`.

### Data files per character

**Layered characters:**
- **`layers.json`** — `{ canvas_size: [W, H], layers: [...] }`. Each layer has:
  - `name`, `group`, `order` (sort key, descending = drawn later = on top).
  - `pos: [x, y]` — top-left of the sprite on the canvas.
  - `empty` — placeholder slot in the prefab; skip in the renderer.
  - `render: { blend, stencil, tint? }` — see "Render pipeline" below.
  - Curation: `requires` (string or string[]), `excludes_groups`, `requires_groups`, `auto_enable`.
- **`compositions.json`** — `{ "Normal1": ["LayerA", ...], ... }`. Multi-head characters number variants per head (`Normal1` for Head01, `Normal2` for Head02). The active preset is computed by comparing the current facial layers against each preset's enabled list.
- **`default.json`** — `{ "enabled": [...] }` or `{ "Head01": { "enabled": [...] }, ... }`.

**Diced NPCs:**
- **`meta.json`** — `{ type: "diced", name, canvas_size, poses: [...] }`. The renderer takes a separate code path: no layer panel, just a pose selector.

There is no separate `clipping.json` file — clipping behavior is encoded in each layer's `render.stencil`.

### UI layout

Left panel (460px desktop; mobile drawer below 900px):
1. **Character selector** — dropdown.
2. **Head selector** — buttons for Head01/Head02 (only shown for multi-head characters: Ema, Hiro, Meruru).
3. **Pose selector** (diced only) — replaces presets/groups for NPCs.
4. **Expression presets** — pill buttons (Normal, Smile, Angry, …). Filtered by active head; active state is determined by comparing current facial layers against the preset's enabled list.
5. **Layer groups** — collapsible sections. Order: Eyes/Mouth/Cheeks → Pale/Sweat/Mask → ArmL → ArmR → Arms → Option_Arm* → Effect_*_Arm* → Shadow_Arm* → rest. Effect family siblings (e.g. `Effect_Back_ArmR01..09`) are bucketed under a synthetic `__EffectMask__/...` group regardless of how the bundle nested them.
6. **Action buttons** — Reset, Export PNG.

Right panel: live preview with zoom (scroll wheel, +/- buttons, 1:1 reset) and pan (click+drag on desktop, single-finger drag + pinch-to-zoom on touch).

### Key behaviours

- **Mutual exclusion** is driven by `excludes_groups` / `requires_groups` on the activating layer (e.g. `Arms01` declares `excludes_groups: ["ArmL", "ArmR"]` and `requires_groups: { ArmL: "ArmL01", ArmR: "ArmR01" }`).
- **Layer dependencies**: layers with `requires` are disabled when any required layer is inactive. Deselecting a required layer auto-disables its dependents (iterated until stable for chained `A→B→C`).
- **Auto-enable on activation**: a dependent with `auto_enable: true` flips on when its `requires` parent toggles off→on; the user can disable it manually afterwards.
- **Head switching**: changes active head base, clears the old head's layers, applies the new head's Normal preset, rebuilds presets and groups.
- **Always-on bases** (hidden from panel): `Body`, `Body\d+`, anything starting with `HeadBase`. Disabling them produces a broken composite.
- **Stencil readers as decorative overlays** (`render.stencil.role === "read"`): preset-independent — they're not cleared/restored when applying expression presets, so the user keeps direct on/off control.

### Render pipeline

Every layer carries `render = { blend, stencil, tint? }` derived from its Unity material. The compositor is a single z-ordered pass:

**Pass 1: build per-ref stencil buffers.**
For each `ref` value, walk all *writers* (layers with `render.stencil.role === "write"`, ref matching) and union their `α > cutoff·255` footprints into a 1-bit-per-pixel-equivalent buffer covering the full canvas. Multiple writers contribute by union.

**Pass 2: composite all layers in z-order.**
For each enabled, non-empty layer:
1. **Pre-tint** by `render.tint` if present (per-channel multiply against the sprite's RGB; α scaled by `tint[3]`). This mirrors Unity's `fragment_color = texture · m_Color`.
2. **Stencil-gate** (readers only): mask the sprite by the buffer for `render.stencil.ref` — output α = sprite.α × (1 if buffer set, else 0) at the corresponding canvas position.
3. **Blit** with `render.blend`:
   - `source-over` → Canvas2D `drawImage` (built-in, fast path).
   - `multiply` / `overlay` / `softlight` → per-pixel kernel via `getImageData` + `putImageData`.

#### Per-pixel blend kernels

All three non-`source-over` blends use the same kernel structure on a clipped rect:

```
for each pixel where αb > 0 and αs > 0:
    as = αs / 255
    Cout = blend(Cb, Cs)              // depends on mode (below)
    out  = Cout · as + Cb · (1 - as)  // mix unblended dest at AA edges
    αout = αb                         // preserved
```

Mode formulas (operating on 0–255):
- **multiply**: `Cmul = Cb · Cs / 255`.
- **overlay**: `Cb < 128 ? 2·Cb·Cs / 255 : 255 − 2·(255−Cb)·(255−Cs) / 255`.
- **softlight** (Pegtop, normalized to [0,1]): `Cout = (1 − 2·Cs')·Cb'² + 2·Cb'·Cs'`.
- Unknown mode → pass through as `source-over` color.

Guarantees:
- **Anti-aliasing preserved** — `αout = αb`, so a layer never adds visible pixels outside the existing canvas content.
- **Short-circuits** — `αb = 0` or `αs = 0` skip the kernel; transparent masks/regions cost almost nothing.
- **No mask-color leak at AA edges** — the `(1-as)` factor mixes the unblended dest back in, matching Canvas2D's `source-over` AA behaviour.

Cost: one `getImageData` + `putImageData` per non-`source-over` layer per render. Real arithmetic only runs inside the layer footprint.

Why not `globalCompositeOperation` for these blends: Canvas2D's W3C blend formula `Cs' = (1-αb)·Cs + αb·B(Cb,Cs)` injects mask color at anti-aliased dest edges and forces `αout` toward 1 inside the source's footprint. Both are wrong for "darken/blend color but preserve silhouette" — exactly what these character mask/overlay layers want.

### Color palette (CSS variables)

```
--bg-deep: #121214     (page background)
--bg-panel: #1c1c21    (left panel)
--bg-card: #26262d     (raised surfaces — groups, buttons)
--accent: #ff6b4a      (coral — presets, export button)
--active: #a78bfa      (soft violet — selected layers, group status)
--text: #e8e6e3        (primary text)
--text-muted: #8a8a8e  (secondary text)
--border: #333338      (borders)
```

