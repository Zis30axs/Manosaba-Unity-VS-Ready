#!/bin/sh
cd /e/Manosaba-VSCodeReady/work/reversing || exit 1
OUT=/e/Manosaba-VSCodeReady/work/title-path/disas
P=/e/Manosaba-VSCodeReady/tools/venv/Scripts/python.exe
dump () { "$P" disas.py "$2" --type "$3" > "$OUT/$1.txt" 2>&1; echo "$1 $(wc -c < "$OUT/$1.txt") bytes"; }
T=GigaCreation.NaninovelExtender.Common.NaninovelLoadingScreen
dump nls_01_ctor 0xCFC550 $T
dump nls_02_Dispose 0xCFC960 $T
dump nls_03_Show 0xCFCED0 $T
dump gm_06_closure_b0 0xCF7ED0 GigaCreation.NaninovelExtender.Common.GotoModified
T=GigaCreation.NaninovelExtender.Common.NaninovelInteractionBlocker
dump nib_01_ctor0 0xCFBE30 $T
dump nib_02_ctor_blockable 0xCFBEB0 $T
dump nib_03_ctor_samplers 0xCFBFD0 $T
dump nib_04_ctor_full 0xCFC030 $T
dump nib_05_SetSortingOrder 0xCFC310 $T
dump nib_06_Dispose 0xCFC400 $T
