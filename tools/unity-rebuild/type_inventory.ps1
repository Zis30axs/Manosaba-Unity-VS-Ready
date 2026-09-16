# Compare the real Naninovel type inventory (Il2CppInterop reference assemblies)
# against the types the reconstructed source tree actually declares.
# Read-only. Usage: . dump_cecil.ps1; . type_inventory.ps1
param()

function Get-CecilAllTypeNames([string]$path) {
    $asm = Get-CecilAssembly $path
    $out = New-Object System.Collections.Generic.List[string]
    function Walk($t) {
        $out.Add($t.FullName)
        foreach ($n in $t.NestedTypes) { Walk $n }
    }
    foreach ($t in $asm.MainModule.Types) { Walk $t }
    return $out
}

function Get-DeclaredTypeNames([string]$root) {
    # Naive but effective: top-level type declarations in decompiled C# (one type per file, Allman).
    $rx = [regex]'^\s*(?:(?:public|private|protected|internal|static|sealed|abstract|partial|readonly|unsafe|new)\s+)*(class|struct|interface|enum)\s+([A-Za-z_]\w*)'
    $out = New-Object System.Collections.Generic.List[string]
    foreach ($f in Get-ChildItem -Path $root -Recurse -Filter *.cs) {
        $ns = ''
        foreach ($line in Get-Content -LiteralPath $f.FullName) {
            $m = [regex]::Match($line, '^namespace\s+([\w\.]+)')
            if ($m.Success) { $ns = $m.Groups[1].Value }
            $t = $rx.Match($line)
            if ($t.Success) {
                $name = $t.Groups[2].Value
                if ($ns) { $out.Add("$ns.$name") } else { $out.Add($name) }
            }
        }
    }
    return $out
}
