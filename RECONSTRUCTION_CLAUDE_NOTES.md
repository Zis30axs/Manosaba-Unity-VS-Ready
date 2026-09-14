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
- `[SerializeField] private StageScreenContainer[] _screenContainers;` -- dereferenced
  unconditionally (explicit inlined NRE throw) in `Awake()` to size the `_tokenSources`
  array via `.Length` -- same "empty-but-non-null array silently produces a zero-length
  table instead of throwing" failure mode already logged below for
  `StageCameraController::_cameraContainers`, not the usual "unassigned throws NRE"
  pattern.

All six fields above have no fallback/null-check path in their respective
disassemblies -- same "unconditional NRE-throwing dereference" pattern already logged
for `DebateUi`/`Court`. Worth confirming prefab wiring for the `PerspectiveStagePresenter`
and `StageScreenController` components if anyone hits a runtime NRE in the
PerspectiveStage flow.

## `WitchTrials.Views.StageCameraController`

File: `src/decompiled/Assembly-CSharp/WitchTrials.Views/StageCameraController.cs`

- `[SerializeField] private StageCameraContainer[] _cameraContainers;` -- dereferenced
  unconditionally (explicit inlined NRE throw, THREE separate times) in `Awake()` to
  size the `_modifyCameraTasks`/`_shakeCameraTasks`/`_tokenSources` arrays via
  `.Length`. An empty-but-non-null array on the prefab would NOT throw here -- it would
  silently produce zero-length task/token-source tables, which is a distinct failure
  mode from the usual "unassigned field throws NRE" pattern logged elsewhere in this
  file: worth flagging separately if the camera subsystem ever silently no-ops instead
  of throwing.

## `WitchTrials.Views.DebateTextPrinterPanel`

File: `src/decompiled/Assembly-CSharp/WitchTrials.Views/DebateTextPrinterPanel.cs`

- `[SerializeField] private DebateText[] _labels;` -- dereferenced unconditionally
  (explicit inlined NRE throws) in `SetText`/`AddText`, and passed to
  `Enumerable.Where` in `RevealMessages` (a null array there throws LINQ's
  `ArgumentNullException` rather than an NRE). Every element is also dereferenced per
  iteration, so a null entry inside the array throws too.
- `[SerializeField] private HorizontalOrVerticalLayoutGroup _layoutGroup;` --
  unconditional NRE-throwing dereference in `RevealMessages` (its `spacing` is set).
- `[SerializeField] private AutoLayoutSupporter _autoLayoutSupporter;` --
  unconditional NRE-throwing dereference in `RevealMessages`
  (`ExecuteRebuildingAsync`).
- `[SerializeField] private LabelSpaceData[] _labelSpaceData;` -- read only in
  `Awake`, which builds the runtime `_labelSpaces` dictionary from it with
  `ToDictionary(LocaleKind, Space)` (now reconstructed; this replaces the earlier
  "presumably built in Awake, unverified" wording). A null array throws LINQ's
  `ArgumentNullException`; two rows with the same `LocaleKind` throw
  `ArgumentException` from `ToDictionary`. `RevealMessages` then gives a locale with
  no entry spacing `0f`, not an error, so a missing row on the prefab shows up as
  zero line spacing rather than a crash.

## `WitchTrials.Views.SkipDebateButton`

File: `src/decompiled/Assembly-CSharp/WitchTrials.Views/SkipDebateButton.cs`

- `_canvasGroup`, `_selectable`, `_iconImage`, `_iconShadow` (all `[SerializeField]`)
  -- each has an explicit inlined NRE throw on dereference: `_iconImage`/`_iconShadow`
  in `Awake` and `SetIconShadowEnabled`, `_canvasGroup` in `Present`/`Dismiss`.
  `_selectable` is passed to UniRx `OnPointerDown/UpAsObservable` without a null check
  in this class; what UniRx does with a null there was not examined.
- The editor-only `Reset()` fills all four from the button's own hierarchy, so a
  prefab set up via Reset has them wired automatically; `_iconImage` is the first
  child `Image` with a non-null `sprite`.

## `WitchTrials.Views.ExecutionButtonScreen`

File: `src/decompiled/Assembly-CSharp/WitchTrials.Views/ExecutionButtonScreen.cs`

- `_blood` (Image) and `_checkIcon` (CanvasGroup): unconditional NRE-throwing
  dereferences in `Awake`, `Ready`, `Dispose`, the per-frame update lambda and
  `OnFullFillAsync`.
- `_button`: dereferenced by `Ready` for the pointer triggers.
- `_buttonContainer`: passed to LitMotion `BindToLocalScaleXY` in `PressAsync` and
  `ReleaseAsync`.
- `_chargingSfx` / `_completeSfx`: passed straight to Naninovel `PlaySfx`/`StopSfx`
  with no empty check.
- `_increaseSpeed` / `_decreaseSpeed` are applied **per frame** (no `Time.deltaTime`
  in the update lambda), so the fill time depends on the frame rate. While pressed,
  every frame also floors the fill at 0.1 (`max(fill + _increaseSpeed, 0.1)`), so it
  jumps to at least 10% as soon as pressing starts.
- `_delayAfterFullFill`: seconds, waited with `DelayType.DeltaTime`, i.e. scaled by
  `Time.timeScale`.

## `WitchTrials.Views.ObjectionCutIn`

File: `src/decompiled/Assembly-CSharp/WitchTrials.Views/ObjectionCutIn.cs`

- `[SerializeField] private PlayableDirector _playableDirector;` -- unconditional
  NRE-throwing dereference in `AwaitSpawn` (`playableGraph`, `time`, `Evaluate`).
- `[SerializeField] private ActivationGameObjects[] _objectLibrary;` -- `AwaitSpawn`
  NRE-throws on a null array, a null entry, or an entry whose `Objects` array is null.
  A null `GameObject` inside `Objects` also throws. The first element of the list
  passed to `SetSpawnParameters` (parsed as an invariant int, 0 when missing or
  unparsable) picks which entry is shown; every other entry is hidden. An index
  outside the array silently hides all of them rather than erroring. How Naninovel's
  `@spawn` command fills that list is third-party behavior and was not examined.
- When the director's `playableGraph` is already valid (a re-spawn), `AwaitSpawn`
  rewinds `time` to 0 and resumes; otherwise it plays from the start.
