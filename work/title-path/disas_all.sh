#!/bin/sh
# Annotated disassembly of the @back (ModifyBackgroundExtended) cluster, April CHS GameAssembly.dll
cd /e/Manosaba-VSCodeReady/work/reversing || exit 1
OUT=/e/Manosaba-VSCodeReady/work/title-path/disas
mkdir -p "$OUT"
P=/e/Manosaba-VSCodeReady/tools/venv/Scripts/python.exe
dump () { # name rva type
  "$P" disas.py "$2" --type "$3" > "$OUT/$1.txt" 2>&1
  echo "$1 $(wc -c < "$OUT/$1.txt") bytes"
}
NS=GigaCreation.NaninovelExtender.ExtendedActors
T=$NS.ModifyBackgroundExtended
dump mbe_01_ctor 0x4D8860 $T
dump mbe_02_get_AssignedPosition 0xD204C0 $T
dump mbe_03_get_AssignedSubId 0xD204D0 $T
dump mbe_04_get_AssignedMatchScreenWidth 0xD20560 $T
dump mbe_05_get_ActorUtility 0xD20610 $T
dump mbe_06_PreloadResources 0xD20700 $T
dump mbe_07_PreloadResources_MoveNext 0xD23130 $T
dump mbe_08_ReleaseResources 0xD20820 $T
dump mbe_09_Execute 0xD20960 $T
dump mbe_10_Execute_MoveNext 0xD21B00 $T
dump mbe_11_Modify 0xD20AE0 $T
dump mbe_12_Modify_MoveNext 0xD22230 $T
dump mbe_13_ApplyAppearanceModification 0xD20C70 $T
dump mbe_14_ApplyAppearanceModification_MoveNext 0xD21330 $T
dump mbe_15_AttemptScenePosition 0xD20E80 $T
T=$NS.BackgroundManagerExtended
dump bme_01_ActorExists 0xD10F00 $T
dump bme_02_GetActor 0xD10F90 $T
dump bme_03_AddActor 0xD11020 $T
dump bme_04_AddActor_MoveNext 0xD11F80 $T
dump bme_05_GetOrAddActor_subId 0xD119D0 $T
dump bme_06_GetOrAddActor_subId_MoveNext 0xD13830 $T
dump bme_07_GetOrAddActor_id_MoveNext 0xD133A0 $T
dump bme_08_ConstructActorAsync 0xD11C30 $T
dump bme_09_ConstructActorAsync_MoveNext 0xD12600 $T
T=$NS.MultipliableActorManagerExtensions
dump mame_01_GetOrAddActor 0xD18B70 $T
dump mame_02_GetOrAddActor_MoveNext 0xD18DD0 $T
dump mame_03_GetOrAddActor_MoveNext_generic 0xD19100 $T
dump imab_01_BuildId 0xD18B00 $NS.IMultipliableActor
T=$NS.BackgroundStateExtended
dump bse_01_OverwriteFromActor 0xD146E0 $T
dump bse_02_ApplyToActor 0xD147E0 $T
T=$NS.SpriteBackgroundExtended
dump sbe_01_ChangePivot 0xD1E620 $T
dump sbe_02_ChangeMaskability 0xD1E510 $T
dump sbe_03_IsMasked 0xD1E410 $T
dump sbe_04_get_0xD1CE90 0xD1CE90 $T
dump sbe_05_set_0xD1CED0 0xD1CED0 $T
dump sbe_06_get_0x492CE0 0x492CE0 $T
