# COMPATIBILITY RECONSTRUCTION — not ORIGINAL ASSET

These nine shaders and the accompanying include files are independently authored
for the interfaces in this repository's existing TrueShadow C# source. No shader
body was copied from a community repository or from the commercial package.

Interface inventory:

| Caller | Shader / resource | Contract |
|---|---|---|
| `BlendModeExtensions` | `UI/TrueShadow-{Normal,Additive,Screen,Multiply}` | UI texture/color, stencil, clip rectangle, four blend modes |
| `ScalableBlur` | `Hidden/TrueShadow/Generate` | passes 0 blur, 1 crop+blur, 2 dither+blur; `_Radius`, `_CropRegion`, `_BlueNoise`, `_TargetSize` |
| `BlurHQ` | `Hidden/TrueShadow/GenerateHQ` | passes 0 horizontal, 1 vertical; 64 `_Weights`/`_Offsets`, `_Extent` |
| `ShadowFactory` | `Hidden/TrueShadow/ImprintPostProcess` | `BLEACH`, `INSET` keywords |
| `ShadowFactory` | `Hidden/TrueShadow/Cutout` | `_Offset`, `_OverflowAlpha`, destination-alpha cutout |
| `ShadowFactory` | `Hidden/TrueShadow/PostProcess` | `_ShadowTex`, `_AlphaMultiplier` spread |
| `LeTai.Effects.Resources` | `True Shadow Blue Noise` | Texture2D, generated deterministic 32x32 noise |
| `ProjectSettings` | `True Shadow Project Settings` | native ScriptableObject referencing the existing script GUID |

There are no `Resources.Load<Shader>` calls in the audited package; shaders are
located by `Shader.Find`. Keeping them under `Resources` also retains them in
player builds. The noise is **white noise, not recovered blue noise**. Settings
use the C# defaults with an empty preset list, not recovered author presets.

Limitations: this is a minimal rendering compatibility implementation, not
pixel-identical recovery. Fast blur, spread/inset edge behavior, color-space and
mask softness may differ. HQ consumes the C# generated weights and skips zero
weights whose offsets can be NaN. Generated rendering has to be tested separately
from simply resolving shader names. Do not describe this directory as the
licensed/original TrueShadow package, and do not change the parent package's
UNLICENSED provenance declaration.

Public community structure checked (no implementation adopted):
`xukonxe/LibreAirspace` at `317660ea244f923e1f3fb0defa4a9eed0e2bc908`,
`Assets/Le Tai's Asset/TrueShadow/Resources/Shaders/` and
`Scripts/Structure/BlendMode.cs`. Four `Shader.Find` names match exactly. A
repository-level MIT declaration does not establish redistribution rights for
its vendored commercial plugin.
