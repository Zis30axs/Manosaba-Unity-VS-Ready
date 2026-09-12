import os
RES = r"E:\manosaba_re\res"


def stats(p):
    n = s = 0
    for r, d, f in os.walk(p):
        for x in f:
            try:
                s += os.path.getsize(os.path.join(r, x))
                n += 1
            except OSError:
                pass
    return n, s


base = os.path.join(RES, "Assets", "#WitchTrials")
for d in sorted(os.listdir(base)):
    p = os.path.join(base, d)
    if os.path.isdir(p):
        n, s = stats(p)
        print("%-16s %6d files %10.1f MiB" % (d, n, s / 2 ** 20))
print()
for d in ["_serialized", "Uncategorized", "Packages"]:
    p = os.path.join(RES, d)
    n, s = stats(p)
    print("%-16s %6d files %10.1f MiB" % (d, n, s / 2 ** 20))
n, s = stats(RES)
print("%-16s %6d files %10.2f GiB" % ("TOTAL", n, s / 2 ** 30))

SRC = r"E:\manosaba_re\src"
for d in ["il2cpp", "decompiled", "decompiled_external"]:
    n, s = stats(os.path.join(SRC, d))
    print("%-16s %6d files %10.1f MiB" % (d, n, s / 2 ** 20))
