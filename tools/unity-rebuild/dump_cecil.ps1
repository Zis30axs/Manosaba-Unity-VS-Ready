# Reflection dump helper for Il2CppInterop reference assemblies (Mono.Cecil based).
# Read-only analysis tool. Usage:
#   . E:\Manosaba-VSCodeReady\tools\unity-rebuild\dump_cecil.ps1
#   Get-CecilAssembly <dll>            -> AssemblyDefinition
#   Get-CecilTypeReport <dll> <type>   -> text report
param()

# The game install path contains CJK characters; resolve it by wildcard so this
# script file stays pure ASCII (Windows PowerShell 5.1 reads .ps1 as ANSI).
$script:CecilPath = (Get-Item "E:\*\manosaba.game-STEAM.CHS\manosaba_game\BepInEx\core\Mono.Cecil.dll").FullName
$script:InteropDir = (Get-Item "E:\*\manosaba.game-STEAM.CHS\manosaba_game\BepInEx\interop").FullName

if (-not ("Mono.Cecil.AssemblyDefinition" -as [type])) {
    Add-Type -Path $script:CecilPath
}

$script:CecilAsm = [System.Reflection.Assembly]::LoadFrom($script:CecilPath)
$script:ReadAssembly = $script:CecilAsm.GetType("Mono.Cecil.AssemblyDefinition").GetMethod("ReadAssembly", [Type[]]@([string]))

function Get-CecilAssembly([string]$path) {
    if (-not [System.IO.Path]::IsPathRooted($path)) { $path = Join-Path $script:InteropDir $path }
    $script:ReadAssembly.Invoke($null, @($path))
}

function Get-CecilType([string]$path, [string]$typeName) {
    $asm = Get-CecilAssembly $path
    $asm.MainModule.GetType($typeName)
}

function Format-CecilTypeRef($t) {
    if ($null -eq $t) { return "?" }
    $name = $t.FullName
    # strip the Il2Cpp namespace prefixes for readability, keep raw in brackets
    return $name
}

function Format-CecilMethod($m) {
    $sb = New-Object System.Text.StringBuilder
    [void]$sb.Append("  ")
    [void]$sb.Append($m.Attributes.ToString().PadRight(28))
    if ($m.IsStatic) { }
    [void]$sb.Append((Format-CecilTypeRef $m.ReturnType).PadRight(46))
    [void]$sb.Append($m.Name)
    if ($m.HasGenericParameters) {
        [void]$sb.Append("<")
        [void]$sb.Append(($m.GenericParameters | ForEach-Object {
            $g = $_.Name
            $cs = @()
            foreach ($c in $_.Constraints) { $cs += $c.ConstraintType.FullName }
            if ($cs.Count -gt 0) { $g += " : " + ($cs -join ", ") }
            $g
        }) -join ", ")
        [void]$sb.Append(">")
    }
    [void]$sb.Append("(")
    $ps = @()
    foreach ($p in $m.Parameters) {
        $s = ""
        $s += $p.ParameterType.FullName
        $s += " " + $p.Name
        $attrs = @()
        foreach ($ca in $p.CustomAttributes) { $attrs += $ca.AttributeType.Name }
        if ($attrs.Count -gt 0) { $s += " [" + ($attrs -join ",") + "]" }
        $ps += $s
    }
    [void]$sb.Append(($ps -join ", "))
    [void]$sb.Append(")")
    return $sb.ToString()
}

function Get-CecilTypeReport([string]$path, [string]$typeName) {
    $asm = Get-CecilAssembly $path
    $t = $asm.MainModule.GetType($typeName)
    if ($null -eq $t) {
        # search nested
        $t = $asm.MainModule.Types | Where-Object { $_.FullName -eq $typeName }
    }
    if ($null -eq $t) { return "TYPE NOT FOUND: $typeName in $($asm.Name.FullName)" }
    $out = New-Object System.Collections.Generic.List[string]
    $out.Add("ASSEMBLY: $($asm.Name.FullName)")
    $out.Add("MODULE MVID: $($asm.MainModule.Mvid)")
    $out.Add("TYPE: $($t.FullName)")
    $out.Add("  ATTRIBUTES: $($t.Attributes)")
    $out.Add("  BASE: $(Format-CecilTypeRef $t.BaseType)")
    if ($t.HasInterfaces) {
        $out.Add("  INTERFACES: " + (($t.Interfaces | ForEach-Object { $_.InterfaceType.FullName }) -join ", "))
    }
    $out.Add("  CUSTOM ATTRS: " + (($t.CustomAttributes | ForEach-Object { $_.AttributeType.FullName }) -join ", "))
    if ($t.HasGenericParameters) {
        $out.Add("  GENERIC PARAMS: " + (($t.GenericParameters | ForEach-Object { $_.Name }) -join ", "))
    }
    $out.Add("--- FIELDS ---")
    foreach ($f in $t.Fields) {
        $out.Add("  $($f.Attributes) :: $(Format-CecilTypeRef $f.FieldType) $($f.Name)")
    }
    $out.Add("--- PROPERTIES ---")
    foreach ($p in $t.Properties) {
        $acc = @()
        if ($p.GetMethod) { $acc += "get:" + $p.GetMethod.Attributes }
        if ($p.SetMethod) { $acc += "set:" + $p.SetMethod.Attributes }
        $out.Add("  $(Format-CecilTypeRef $p.PropertyType) $($p.Name) { $($acc -join ' | ') }")
    }
    $out.Add("--- EVENTS ---")
    foreach ($e in $t.Events) {
        $out.Add("  $(Format-CecilTypeRef $e.EventType) $($e.Name) add=$($e.AddMethod.Attributes) remove=$($e.RemoveMethod.Attributes)")
    }
    $out.Add("--- METHODS ---")
    foreach ($m in $t.Methods) {
        $out.Add((Format-CecilMethod $m))
        foreach ($g in $m.GenericParameters) {
            foreach ($c in $g.Constraints) { $out.Add("        CONSTRAINT $($g.Name) : $($c.ConstraintType.FullName)") }
        }
    }
    if ($t.HasNestedTypes) {
        $out.Add("--- NESTED TYPES ---")
        foreach ($n in $t.NestedTypes) { $out.Add("  $($n.FullName)") }
    }
    return ($out -join "`n")
}

function Get-CecilTypeReportClean([string]$path, [string]$typeName) {
    # Same as Get-CecilTypeReport but drops interop bookkeeping fields.
    (Get-CecilTypeReport $path $typeName) -split "`n" | Where-Object { $_ -notmatch 'NativeFieldInfoPtr_|NativeMethodInfoPtr_' }
}

function Get-CecilMethodIL([string]$path, [string]$typeName, [string]$methodName) {
    $asm = Get-CecilAssembly $path
    $t = $asm.MainModule.GetType($typeName)
    if ($null -eq $t) { return "TYPE NOT FOUND" }
    $out = New-Object System.Collections.Generic.List[string]
    foreach ($m in $t.Methods) {
        if ($m.Name -like "*$methodName*") {
            $out.Add("=== $($m.FullName)  HasBody=$($m.HasBody)")
            if ($m.HasBody) {
                foreach ($i in $m.Body.Instructions) { $out.Add("  $i") }
            }
        }
    }
    return ($out -join "`n")
}

function Find-CecilTypes([string]$path, [string]$pattern) {
    $asm = Get-CecilAssembly $path
    $asm.MainModule.Types | Where-Object { $_.FullName -like $pattern } | ForEach-Object { $_.FullName }
}
