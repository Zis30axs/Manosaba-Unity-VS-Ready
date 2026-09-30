#!/bin/sh
cd /e/Manosaba-VSCodeReady/work/reversing || exit 1
OUT=/e/Manosaba-VSCodeReady/work/title-path/disas
P=/e/Manosaba-VSCodeReady/tools/venv/Scripts/python.exe
dump () { "$P" disas.py "$2" --type "$3" > "$OUT/$1.txt" 2>&1; echo "$1 $(wc -c < "$OUT/$1.txt") bytes"; }
T=GigaCreation.NaninovelExtender.UI.NaninovelScreen
dump ns_13_DismissAsync 0xD3CFB0 $T
dump ns_14_DismissAsync_MoveNext 0xD3DC00 $T
dump ns_15_PresentAsync_MoveNext 0xD3E7C0 $T
dump ns_16_BeginToDismiss 0xD3D180 $T
dump ns_17_EndToDismiss 0xD3D330 $T
dump ns_18_SetCanvasesEnabled 0xD3D360 $T
dump ns_19_SetGraphicRaycastersEnabled 0xD3D490 $T
dump ns_20_EnableCancelEvent 0xD3D5C0 $T
dump ns_21_DisableCancelEvent 0xD3D880 $T
dump ns_22_Cancel 0xD3DA10 $T
dump ns_23_PlayCancelSfx 0xD3DA80 $T
dump ns_24_get_IsCancelEventBlocked 0x492EF0 $T
dump ns_25_set_IsCancelEventBlocked 0x492F00 $T
dump ns_26_ctor 0x30D8F0 $T
