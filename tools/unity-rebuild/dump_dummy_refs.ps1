# Dump the assembly-level reference graph of the 43 reconstructed assemblies from the
# Il2CppDumper stub assemblies (metadata is the ground truth; the reconstruction csprojs
# contain at least one spurious edge, see out/unity/assembly_graph_recon.json).
#
#   pwsh -File tools/unity-rebuild/dump_dummy_refs.ps1
#
# Output: tools/unity-rebuild/out/unity/dummy_ref_graph.json
#   { "assemblies": { "<name>": { "assembly_refs": [...], "type_ref_counts": {..} } } }
$ErrorActionPreference = 'Stop'

$repo = (Resolve-Path (Join-Path $PSScriptRoot '..\..')).Path
$dummy = Join-Path $repo 'src\il2cpp\DummyDll'
$projectsJson = Join-Path $repo 'project\projects.json'
$outDir = Join-Path $repo 'tools\unity-rebuild\out\unity'
New-Item -ItemType Directory -Force -Path $outDir | Out-Null

$cecil = Join-Path $dummy 'Mono.Cecil.dll'
if (-not (Test-Path $cecil)) {
    $cecil = (Get-ChildItem (Join-Path $repo 'UnityProject\Library\PackageCache') -Recurse -Filter 'Mono.Cecil.dll' |
              Select-Object -First 1).FullName
}
Add-Type -Path $cecil
Write-Host "[refs] cecil  = $cecil"

$projects = Get-Content $projectsJson -Raw -Encoding UTF8 | ConvertFrom-Json
$assemblies = @()
foreach ($p in $projects) { $assemblies += [System.IO.Path]::GetFileNameWithoutExtension($p.project) }
$assemblies = $assemblies | Sort-Object -Unique
Write-Host "[refs] assemblies = $($assemblies.Count)"

$graph = @{}
foreach ($name in $assemblies) {
    $path = Join-Path $dummy "$name.dll"
    if (-not (Test-Path $path)) { Write-Host "[refs] MISSING stub: $name.dll"; continue }
    $asm = [Mono.Cecil.AssemblyDefinition]::ReadAssembly($path)

    $assemblyRefs = @()
    foreach ($ar in $asm.MainModule.AssemblyReferences) { $assemblyRefs += $ar.Name }

    $typeRefCounts = @{}
    foreach ($tr in $asm.MainModule.GetTypeReferences()) {
        $scope = $tr.Scope
        $scopeName = $null
        if ($scope -is [Mono.Cecil.AssemblyNameReference]) { $scopeName = $scope.Name }
        elseif ($scope -is [Mono.Cecil.ModuleDefinition]) { $scopeName = $scope.Assembly.Name.Name }
        if (-not $scopeName) { continue }
        if ($typeRefCounts.ContainsKey($scopeName)) { $typeRefCounts[$scopeName] = $typeRefCounts[$scopeName] + 1 }
        else { $typeRefCounts[$scopeName] = 1 }
    }

    $graph[$name] = [ordered]@{
        stub                  = $path.Substring($repo.Length + 1)
        assembly_refs         = ($assemblyRefs | Sort-Object -Unique)
        type_ref_counts       = $typeRefCounts
        assembly_ref_count    = ($assemblyRefs | Sort-Object -Unique).Count
        type_ref_scope_count  = $typeRefCounts.Count
    }
    $asm.Dispose()
}

$result = [ordered]@{
    generated        = (Get-Date).ToUniversalTime().ToString('o')
    root             = $dummy
    assembly_count   = $graph.Count
    assemblies       = $graph
}

# ---- namespaces declared by every stub, so a `using` can be resolved to an assembly ------------
$stubNamespaces = @{}
foreach ($dll in (Get-ChildItem $dummy -Filter *.dll | Sort-Object Name)) {
    try {
        $ad = [Mono.Cecil.AssemblyDefinition]::ReadAssembly($dll.FullName)
        $ns = @{}
        foreach ($t in $ad.MainModule.Types) {
            if ($t.Namespace) { $ns[$t.Namespace] = $true }
        }
        $stubNamespaces[$dll.Name] = ($ns.Keys | Sort-Object)
        $ad.Dispose()
    } catch {
        $stubNamespaces[$dll.Name] = @("(unreadable: $($_.Exception.Message))")
    }
}
$result['stub_namespaces'] = $stubNamespaces
$result['stub_dll_count'] = $stubNamespaces.Count

$out = Join-Path $outDir 'dummy_ref_graph.json'
$result | ConvertTo-Json -Depth 8 | Set-Content -Path $out -Encoding UTF8
Write-Host "[refs] wrote $out"
Write-Host "[refs] stub dlls with namespaces: $($stubNamespaces.Count)"

# quick summary: edges that the reconstruction csprojs got wrong
$csprojEdges = @{}
foreach ($p in $projects) {
    $a = [System.IO.Path]::GetFileNameWithoutExtension($p.project)
    foreach ($r in $p.dllRefs) { $csprojEdges["$a|$r"] = $true }
}
$spurious = @()
foreach ($name in $graph.Keys) {
    foreach ($r in $graph[$name].assembly_refs) {
        if ($csprojEdges.ContainsKey("$name|$r")) { continue }
        $spurious += "$name -> $r"
    }
}
Write-Host "[refs] metadata edges missing from projects.json: $($spurious.Count)"
$spurious | Select-Object -First 20 | ForEach-Object { "    $_" }
