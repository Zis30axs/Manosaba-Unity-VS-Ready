# RECONSTRUCTION_CLAUDE_NOTES.md

Scene/Prefab/serialized-reference evidence noticed incidentally while reconstructing
`Assembly-CSharp`/`WitchTrials.*` native logic in this worktree (`recon/core-systems`).
This file only logs findings for the parallel resource-audit agent
(worktree `E:/Manosaba-DS`, branch `audit/unity-assets`) -- it is not a resource audit
itself, and no resource-analysis tooling from that effort was touched to produce it.

## `WitchTrials.Views.DebateUi`

File: `src/decompiled/Assembly-CSharp/WitchTrials.Views/DebateUi.cs`

- `[SerializeField] private TextMeshProUGUI _label;` -- dereferenced unconditionally
  (NRE-throwing, not a `?.`) in `OnVisibilityChangedByCustomUi`'s `b__18_0` lambda
  (`_label.SetText(TimeSpanToText(timeSpan))`). Must be wired to a real
  `TextMeshProUGUI` on the prefab/scene object this component lives on, or the debate
  countdown display throws at runtime the first time the remaining-time observable
  fires.
- `[SerializeField] private SkipDebateButton _skipDebateButton;` -- also dereferenced
  unconditionally throughout `Initialize()`, `OnVisibilityChangedByCustomUi`, and
  `OnDebateSkippableChangedAsObservable`'s `b__18_1` lambda (`.Present()`/`.Dismiss()`).
  Must be wired to a real `SkipDebateButton`-bearing object.

## `WitchTrials.Views.Court`

File: `src/decompiled/Assembly-CSharp/WitchTrials.Views/Court.cs`

- `[SerializeField] private GameObject _fireVfx;` -- toggled via `SetActive(bool)` in
  `Awake()` (forced off) and `ChangeFireFxActivity(bool)`.
- `[SerializeField] private Light _light;` -- `Awake()` reads `_light.color` once to
  cache `_initialFireLightColor`; `ChangeFireFxActivity(bool)` then switches
  `_light.color` between that cached original and `_fireLightColor` depending on the
  `active` flag.
- `[SerializeField] private Color _fireLightColor;` -- the "fire is active" light
  color; whatever value is authored on the prefab is the actual in-game fire-lit
  color, not derivable from code alone.

None of these fields have a fallback/null-check path in the disassembly -- if any of
them are unassigned on the prefab, the corresponding method throws a
`NullReferenceException` the first time it runs. Worth confirming prefab wiring for
all five if anyone hits a runtime NRE in the Debate/Court flow.

## `WitchTrials.Presenters.PerspectiveStagePresenter`

File: `src/decompiled/Assembly-CSharp/WitchTrials.Presenters/PerspectiveStagePresenter.cs`

- `[SerializeField] private Transform _stageParent;` -- dereferenced unconditionally
  (explicit inlined NRE throw) in `Awake()` to read `.position` for the
  `PerspectiveStageManager`'s `stageOffset` constructor argument.
- `[SerializeField] private StageScreenController _screenController;` -- dereferenced
  unconditionally (explicit inlined NRE throw) in `Awake()`, both directly (`.Dismiss()`)
  and as a constructor argument to `PerspectiveStageManager`.
- `[SerializeField] private StageCameraController _cameraController;` -- same pattern as
  `_screenController`: unconditional NRE-throwing dereference in `Awake()`, both
  directly (`.Dismiss()`) and as a `PerspectiveStageManager` constructor argument.
- `[SerializeField] private PerspectiveStageData[] _stageData;` -- passed directly into
  `Enumerable.ToDictionary(...)` in `Awake()` with no inlined guard of its own (the
  guard, if any, is `ToDictionary`'s own internal `ArgumentNullException` check, not
  visible in this method's disassembly). Each element's `.Kind`/`.Stage` sub-fields
  (a `PerspectiveStageKind` and an `AssetReferenceGameObject`, both `[SerializeField]`
  on `PerspectiveStageData`) are read via property getters with no defensive checks.

## `WitchTrials.Views.StageScreenController`

File: `src/decompiled/Assembly-CSharp/WitchTrials.Views/StageScreenController.cs`

- `[SerializeField] private GeneralScreen _generalScreen;` -- dereferenced unconditionally
  (explicit inlined NRE throw) inside `Dismiss()`, which is itself inlined at multiple
  call sites (including `PerspectiveStagePresenter::Awake`) -- so this field being unset
  on the prefab would surface as an NRE at whatever call site happened to trigger first,
  not necessarily inside `StageScreenController` itself.

All five fields above have no fallback/null-check path in their respective
disassemblies -- same "unconditional NRE-throwing dereference" pattern already logged
for `DebateUi`/`Court`. Worth confirming prefab wiring for the `PerspectiveStagePresenter`
and `StageScreenController` components if anyone hits a runtime NRE in the
PerspectiveStage flow.
