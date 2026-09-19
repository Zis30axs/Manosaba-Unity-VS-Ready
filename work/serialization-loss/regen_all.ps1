# Full regeneration: 440 scenarios (in place), then the 156 prefabs.  Stops at the first failing stage.
$here = $PSScriptRoot
& "$here\run_so_convert.ps1" -Plan so_convert_plan_scenarios.json `
    -Out tools/unity-rebuild/out/unity/so_convert_result_scenarios.json -Log work/serialization-loss/so_convert_full2.log
if ($LASTEXITCODE -ne 0) { Write-Host "scenario stage failed: $LASTEXITCODE"; exit $LASTEXITCODE }
& "$here\run_prefab_build.ps1" -Plan prefab_plan_all.json `
    -Out tools/unity-rebuild/out/unity/prefab_build_result.json -Log work/serialization-loss/prefab_build_full.log
Write-Host "prefab stage exit: $LASTEXITCODE"
exit $LASTEXITCODE
