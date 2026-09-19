#!/bin/sh
# Annotated disassembly of the shared generic instantiations ModifyActorUtility<object,object> / MultipliableActorManagerUtility<>
cd /e/Manosaba-VSCodeReady/work/reversing || exit 1
OUT=/e/Manosaba-VSCodeReady/work/title-path/disas
mkdir -p "$OUT"
P=/e/Manosaba-VSCodeReady/tools/venv/Scripts/python.exe
dump () { "$P" disas.py "$2" --type "$3" > "$OUT/$1.txt" 2>&1; echo "$1 $(wc -c < "$OUT/$1.txt") bytes"; }
NS=GigaCreation.NaninovelExtender.ExtendedActors
T=$NS.ModifyActorUtility
dump mau_01_get_AddressableAssetLoader 0x86BA20 $T
dump mau_02_ctor 0x86BAE0 $T
dump mau_03_GetDissolveTexture 0x86BBC0 $T
dump mau_04_GetDissolveTexture_MoveNext 0x9E7300 $T
dump mau_05_PreloadResources 0x86BE20 $T
dump mau_06_PreloadResources_MoveNext 0xA06560 $T
dump mau_07_ReleaseResources 0x86C0A0 $T
dump mau_08_GetOrAddActor 0x86C150 $T
dump mau_09_GetOrAddActor_MoveNext 0x9EC2F0 $T
dump mau_10_BuildAppearanceTransition 0x86C380 $T
dump mau_11_EvaluateWorldPosition 0x86C630 $T
T=$NS.MultipliableActorManagerUtility
dump mamu_01_PendingAddActorTaskExists 0x87C670 $T
dump mamu_02_WaitForCompletionOfPendingAddActorAsync 0x87C730 $T
dump mamu_03_WaitForCompletion_MoveNext 0xBA6740 $T
dump mamu_04_CreatePendingAddActorTask 0x87CB00 $T
dump mamu_05_SetResultAndRemovePendingAddActorTask 0x87CC70 $T
dump mamu_06_ctor 0x87CE20 $T
