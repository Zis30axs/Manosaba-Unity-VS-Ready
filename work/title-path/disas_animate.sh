#!/bin/sh
# Annotated disassembly of the @animate extension (AnimateActorExtended / AnimateExtended)
cd /e/Manosaba-VSCodeReady/work/reversing || exit 1
OUT=/e/Manosaba-VSCodeReady/work/title-path/disas
P=/e/Manosaba-VSCodeReady/tools/venv/Scripts/python.exe
dump () { "$P" disas.py "$2" --type "$3" > "$OUT/$1.txt" 2>&1; echo "$1 $(wc -c < "$OUT/$1.txt") bytes"; }
NS=GigaCreation.NaninovelExtender.ExtendedActors
T=$NS.AnimateActorExtended
dump aae_01_GetParametersForActor 0xD1E940 $T
dump aae_02_ModifyKeysAsRelative 0xD1F3C0 $T
dump aae_03_ctor 0xD1F760 $T
T=$NS.AnimateExtended
dump ae_01_SetSpawnParameters 0xD27530 $T
dump ae_02_ParseAdditionalParameters 0xD27760 $T
dump ae_03_TryParseParameter 0xD27EF0 $T
dump ae_04_AnimateKey 0xD28160 $T
dump ae_05_AnimateKey_MoveNext 0xD28B70 $T
dump ae_06_ParseEasing 0xD28360 $T
dump ae_07_ctor 0xD28540 $T
