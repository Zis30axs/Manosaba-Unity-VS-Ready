# GitHub Actions

The repository has two workflows.

## Manosaba CI

`.github/workflows/ci.yml` runs on GitHub-hosted Ubuntu for every push and pull request to
`main`. It does not require Unity or any proprietary game payload. It verifies the Unity version
pin, parses the PowerShell tooling, compiles the Python tooling, checks the converted C# tree
structure, and publishes the current IL2CPP-stub scan as an artifact.

## Unity Gates

`.github/workflows/unity-gates.yml` is designed for a Windows x64 **self-hosted** runner with the
custom label `manosaba-unity`. This is intentional: the runtime reconstruction depends on an
activated Unity 6000.0.48f1 installation and on ignored local payload such as
`UnityProject/Assets/GameContent`, which must not be committed to Git.

The runner should have:

- Unity 6000.0.48f1 with Windows/IL2CPP support and a valid license.
- Python available as `python`.
- PowerShell 7.
- The checkout's tracked content recipes; Unity gates reapply and verify generated SpriteSkin data before building or entering Play Mode.
- The ignored reconstruction payload seeded in the runner workspace. Checkout uses `clean: false`
  so GitHub Actions will not delete it between runs.

Optional repository variables:

- `MANOSABA_UNITY_EDITOR`: absolute path to `Unity.exe`; leave empty to use `common.ps1`
  auto-discovery.
- `MANOSABA_GAME_ROOT`: absolute path to the original installed game used by the audit/preflight.
- `MANOSABA_RUN_UNITY_CI=1`: run G1 automatically on pushes to `main`.
- `MANOSABA_RUN_RUNTIME_CI=1`: after G1, also rebuild committed Addressables content and run
  G4/G5/G6 automatically on pushes.

Without those opt-in variables, pushes still run the hosted static workflow. The Unity workflow can
always be launched manually from **Actions -> Unity Gates -> Run workflow**, choosing either
`compile` or `runtime`.
