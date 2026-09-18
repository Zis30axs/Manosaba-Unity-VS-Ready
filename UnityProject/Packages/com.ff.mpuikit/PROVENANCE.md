# Provenance: MPUIKit (UI mirror of the Asset Store package)

* Repository: https://github.com/Yokesh-4040/MPUIKit  (branch `main`)
* Archive: https://codeload.github.com/Yokesh-4040/MPUIKit/tar.gz/refs/heads/main
* Archive SHA-256: 7950806AF3046F9F22C982E449D2279D910D983FDF1A0D07147307A60EACD7B7
* Upstream self-description: package.json `com.ff.mpuikit` 1.0.0, displayName "MPUIKit (Dependency)",
  description "Dependency package used by FF Experiment Manager", author "FF Installer", unity 2021.3
* Vendored path: UnityProject/Packages/com.ff.mpuikit (Runtime + Editor + package.json as published)

!!! LICENSE STATUS: the repository declares NO license and carries no LICENSE file.  It is a third-party
!!! mirror of the Unity Asset Store package "MPUIKit" (namespace `MPUIKIT`), supplied by the project owner
!!! for this reconstruction.  Treat it as a placeholder: replace this folder with the properly licensed
!!! package (Asset Store purchase) when available -- the swap is a folder replacement, no game source
!!! touches it.

Why it is used: the shipped game's prefabs carry `MPUIKIT.MPImage` components, and the Il2CppDumper stub
of MPUIKit is metadata-only (Unity refuses to load it), which forced every prefab containing an MPImage
into the content-build exclusion list.  API compatibility was verified field by field against the stub
(the authority, being generated from the shipped game): `namespace MPUIKIT`, `MPImage : Image`, and the
serialized fields m_DrawShape / m_ImageType / m_MaterialMode / m_StrokeWidth / m_OutlineWidth /
m_OutlineColor / m_FalloffDistance / m_ConstrainRotation / m_ShapeRotation / m_FlipHorizontal /
m_FlipVertical / m_Triangle / m_Rectangle / m_Circle / m_Pentagon / m_Hexagon / m_NStarPolygon /
m_GradientEffect are all present, as are the shape types (Triangle, Rectangle, Circle, Pentagon, Hexagon,
NStarPolygon), Enums (DrawShape, MaterialMode, GradientType, CornerStyleType) and GradientEffect.
