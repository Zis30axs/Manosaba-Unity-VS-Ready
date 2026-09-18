# Provenance: LitMotion

* Upstream: https://github.com/annulusgames/LitMotion
* Version tag: v2.0.2
* Archive: https://codeload.github.com/annulusgames/LitMotion/tar.gz/refs/tags/v2.0.2
* Archive SHA-256: 6D20DED8B03126BAA49AE7B3CEFFFFA48A05D209ECDB03B898AD3878B54834AA
* Vendored path: UnityProject/Packages/com.annulusgames.litmotion (`Runtime/**`, i.e. LitMotion +
  LitMotion.Extensions; the Editor assembly and LitMotion.Animation are not vendored)
* License: MIT (see LICENSE.txt)

Referenced as a local UPM package (not as a precompiled stub) so that LitMotion's `versionDefines`
fire: its UniTask/UniRx integration is what `MotionHandle.ToUniTask(ct)` / `ToObservable()` need,
and those defines are keyed on the package names `com.cysharp.unitask` / `com.neuecc.unirx`.
