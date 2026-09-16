# RECONSTRUCTION_CODEX_NOTES.md

Independent reconstruction review notes produced by **Codex (OpenAI)**.

This file is intentionally separate from `RECONSTRUCTION_CLAUDE_NOTES.md`: Claude's file records findings produced during the reconstruction work itself; this file records an independent review of the merged reconstruction state and documentation. It should not be read as a claim that Codex authored or re-ran Claude's reconstruction work.

## Audit baseline

- Repository: `Zis30axs/Manosaba`
- Branch reviewed: `main`
- Baseline commit: `33cd707b61adc99120c403685ee6ec87296d6505`
- Baseline merge message: `Merge recon/core-systems: core-system reconstruction + Unity project audit`
- Review type: repository/documentation/code-structure audit
- Fresh Unity runtime/build verification by Codex: **not performed in this pass**

The merged reconstruction notes report the following current aggregate baseline:

- Reconstructed independent function bodies: **903 / 929** (**97.20%** by function count)
- Reconstructed native bytes: **308,952 / 316,778** (**97.53%** by byte coverage)
- `Other`: **161 / 162**
- `Boot/Init`: **7 / 32**
- Reported compile baseline: **0 errors / 140 warnings**
- Remaining non-Boot item: `GoogleForm.OpenGoogleFormAsync`

These figures are documentation/reconstruction baselines from the merged work. They are not a substitute for a fresh runtime verification pass.

---

## CODEX-001 — TL;DR contains a historical progress line that now reads as current

`RECONSTRUCTION.md` still contains an early TL;DR row describing the "first batch 44 methods" as `27/44` complete and saying `Boot/Init` and `TrialState` were both complete in that batch.

That statement is useful historical information, but after the 107-commit reconstruction merge it is easy to misread as the current global subsystem state. The later/current aggregate status still has `Boot/Init` at **7/32**.

### Recommendation

Keep the historical row only if it is explicitly labelled as a historical first-batch snapshot, or replace the TL;DR progress wording with the current aggregate reconstruction status.

**Severity:** documentation / medium

---

## CODEX-002 — 97.2% and 97.5% are both correct, but measure different things

The reconstruction is approximately 97.5% complete **by native-byte coverage**, while it is approximately 97.2% complete **by independent function-body count**.

These metrics should remain separate wherever progress is summarized:

- Function bodies: `903 / 929 = 97.20%`
- Native bytes: `308,952 / 316,778 = 97.53%`

Using only "~97.5% complete" without naming the metric can overstate the method-count completion slightly and makes later audits harder to reproduce.

**Severity:** documentation / low

---

## CODEX-003 — Boot/Init is the remaining high-risk reconstruction area

The remaining `Boot/Init` methods are disproportionately risky compared with ordinary leaf methods because Unity startup behavior is sensitive to lifecycle ordering and side effects even when the reconstructed C# compiles cleanly.

The final Boot/Init pass should treat the following as behavior that must be preserved, not merely syntax that must compile:

- `Awake` / `Start` ordering and initialization dependencies
- singleton/service registration timing
- event subscription and unsubscription order
- coroutine / `async` scheduling and continuation timing
- scene-transition side effects
- `DontDestroyOnLoad` and object lifetime semantics
- first-frame state and any initialization that is intentionally deferred by one frame/await

A compile-success baseline is necessary but not sufficient evidence for this area.

**Severity:** runtime / high

---

## CODEX-004 — Runtime verification coverage is still narrower than reconstruction coverage

The repository documents a strong static reconstruction and compile baseline, but the amount of explicitly recorded runtime verification is much smaller than the amount of reconstructed code.

The clearest recorded startup runtime verification is `GameInitializer::Start`; therefore, the merged reconstruction should not be described as globally runtime-verified merely because it builds with zero errors.

### Recommendation

For the final Boot/Init tranche, record a small runtime smoke-test matrix next to the reconstruction evidence. At minimum:

1. process reaches the expected first scene/UI state;
2. core service/singleton initialization completes once;
3. no duplicate persistent objects are created across an initial scene transition;
4. save/input/Naninovel-facing services become available in the expected order;
5. startup produces no new exceptions relative to the known baseline.

**Severity:** verification / medium-high

---

## CODEX-005 — Deferring `GoogleForm.OpenGoogleFormAsync` is preferable to fabricating a 100% result

`GoogleForm.OpenGoogleFormAsync` remains the lone non-Boot reconstruction gap. If the available evidence remains below the project's accepted confidence threshold, leaving it intentionally unresolved is consistent with the repository's stated rule that evidence has priority over nominal completeness.

Do not fill this method solely to turn `161/162` into `162/162` unless native behavior can be justified from stronger evidence.

**Severity:** evidence quality / informational

---

## Review conclusion

The merged reconstruction work is internally much closer to completion than the old first-batch TL;DR suggests. The main remaining engineering risk is concentrated in `Boot/Init`, where lifecycle/timing errors can survive compilation and static review.

The current documentation should therefore distinguish three separate claims:

1. **static reconstruction coverage** (function/byte metrics),
2. **buildability** (reported compiler baseline), and
3. **runtime verification** (explicitly tested behavior).

Keeping those claims separate will make the final reconstruction result easier to audit and harder to accidentally overstate.

---

## Codex provenance

Reviewer / note author: **Codex (OpenAI)**

Commit trailer used for Codex-authored review commits:

`Co-authored-by: Codex <codex@openai.com>`

This provenance marker is intentionally distinct from Claude-authored reconstruction notes and from the repository owner's Git identity.
