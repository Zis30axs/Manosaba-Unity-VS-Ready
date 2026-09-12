import os, json, sys
sys.path.insert(0, r"E:\manosaba_re\work")
import extract_res as E

tm = E.load_types()
targets = E.all_targets("audio", tm, None)
targets.sort(key=lambda x: -x[1])
print("total", len(targets))
for rel, size in targets[:20]:
    print(f"  {size/2**20:8.1f} MiB  {os.path.basename(rel)}")
print("--- smallest ---")
for rel, size in targets[-5:]:
    print(f"  {size/2**20:8.1f} MiB  {os.path.basename(rel)}")
