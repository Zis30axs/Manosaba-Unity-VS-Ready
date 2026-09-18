#!/bin/sh
# Dump annotated disassembly of the ExtendedSpawn native methods (April CHS GameAssembly.dll)
cd /e/Manosaba-VSCodeReady/work/reversing || exit 1
OUT=/e/Manosaba-VSCodeReady/work/spawn-nre/disas
mkdir -p "$OUT"
P=/e/Manosaba-VSCodeReady/tools/venv/Scripts/python.exe
dump () { # name rva type
  "$P" disas.py "$2" --type "$3" > "$OUT/$1.txt" 2>&1
  echo "$1 $(wc -c < "$OUT/$1.txt") bytes"
}
T=GigaCreation.NaninovelExtender.ExtendedSpawn.MultipliableSpawn
dump 01_ctor 0xD2C2C0 $T
dump 02_get_Spawned 0xD2C270 $T
dump 03_InitializeService 0xD2C540 $T
dump 04_DestroyService 0xD2C710 $T
dump 05_HoldResources 0xD2C760 $T
dump 06_HoldResources_MoveNext 0xD2E000 $T
dump 07_ReleaseResources 0xD2C950 $T
dump 08_Spawn_path 0xD2CA90 $T
dump 09_Spawn_path_MoveNext 0xD2F0F0 $T
dump 10_Spawn_path_subId 0xD2CCE0 $T
dump 11_Spawn_path_subId_MoveNext 0xD2F7E0 $T
dump 12_DestroySpawned_path 0xD2CF90 $T
dump 13_DestroySpawned_path_subId 0xD2D040 $T
dump 14_IsSpawned_path 0xD2D150 $T
dump 15_IsSpawned_path_subId 0xD2D1D0 $T
dump 16_GetSpawned_path 0xD2D2B0 $T
dump 17_GetSpawned_path_subId 0xD2D320 $T
dump 18_SaveServiceState 0xD2D3F0 $T
dump 19_LoadServiceState 0xD2D6E0 $T
dump 20_LoadServiceState_MoveNext 0xD2EB70 $T
dump 21_LoadObjectsAsync 0xD2D860 $T
dump 22_LoadObjectsAsync_MoveNext 0xD2E270 $T
dump 23_UpdateObject 0xD2D9E0 $T
dump 24_SpawnObjectAsync 0xD2DC30 $T
dump 25_SpawnObjectAsync_MoveNext 0xD300F0 $T
dump 26_DestroyAllSpawned 0xD2DDF0 $T
T2=GigaCreation.NaninovelExtender.ExtendedSpawn.SpawnExtended
dump 30_get_AssignedSubId 0xD2B440 $T2
dump 31_get_MultipliableManager 0xD2B4D0 $T2
dump 32_Execute 0xD2B530 $T2
dump 33_Execute_MoveNext 0xD2BBD0 $T2
dump 34_ApplyScenePosition 0xD2B6C0 $T2
T3=GigaCreation.NaninovelExtender.ExtendedSpawn.MultipliableSpawnManagerExtensions
dump 40_GetOrSpawnAsync 0xD30580 $T3
dump 41_GetOrSpawnAsync_MoveNext 0xD30830 $T3
T4=GigaCreation.NaninovelExtender.ExtendedSpawn.DestroySpawnedExtended
dump 50_get_AssignedSubId 0x86C880 $T4
dump 51_Destroy 0xD2A950 $T4
dump 52_Destroy_MoveNext 0xD2ABD0 $T4
dump 53_Warn 0xD2AAD0 $T4
