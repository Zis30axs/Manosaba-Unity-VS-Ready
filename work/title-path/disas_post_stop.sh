#!/bin/sh
# Annotated disassembly of the commands after @stop on the title path (GotoModified, Quit) and the UI screen base class
cd /e/Manosaba-VSCodeReady/work/reversing || exit 1
OUT=/e/Manosaba-VSCodeReady/work/title-path/disas
P=/e/Manosaba-VSCodeReady/tools/venv/Scripts/python.exe
dump () { "$P" disas.py "$2" --type "$3" > "$OUT/$1.txt" 2>&1; echo "$1 $(wc -c < "$OUT/$1.txt") bytes"; }
NS=GigaCreation.NaninovelExtender.Common
T=$NS.GotoModified
dump gm_01_Execute 0xCF7AD0 $T
dump gm_02_Execute_MoveNext 0xCF7F00 $T
dump gm_03_NavigateOtherScript 0xCF7BF0 $T
dump gm_04_NavigateOtherScript_MoveNext 0xCF8600 $T
dump gm_05_LoadAndPlay 0xCF7DD0 $T
dump quit_01_Execute 0xCF8EF0 $NS.Quit
T=GigaCreation.NaninovelExtender.UI.NaninovelScreen
dump ns_01_Awake 0xD3C860 $T
dump ns_02_OnEnable 0xD3C8D0 $T
dump ns_03_OnDisable 0xD3CA40 $T
dump ns_04_Reset 0xD3CBB0 $T
dump ns_05_OnVisibilityChangedByCustomUi 0xD3CC50 $T
dump ns_06_Present 0xD3CCC0 $T
dump ns_07_Dismiss 0xD3CD10 $T
dump ns_08_PresentAsync 0xD3CD60 $T
dump ns_09_BeginToPresent 0xD3CF30 $T
dump ns_10_EndToPresent 0xD3CF70 $T
dump ns_11_get_0xD3C6E0 0xD3C6E0 $T
dump ns_12_get_0xD3C7A0 0xD3C7A0 $T
