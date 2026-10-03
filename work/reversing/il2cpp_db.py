"""Symbol database for the Manosaba IL2CPP dump.

Loads Il2CppDumper's script.json + dump.cs into fast lookup tables and caches
them as a pickle, so per-function analysis is instant.

Coordinate system: everything is an RVA (imagebase 0x180000000 is added only
for display). script.json "Address" fields are RVAs -- verified against
dump.cs RVA/Offset pairs via the PE section table.
"""
import json, os, pickle, re, sys, bisect

HERE = os.path.dirname(os.path.abspath(__file__))
IL2 = os.environ.get("MANOSABA_IL2CPP_DUMP") or r"E:\Manosaba-VSCodeReady\src\il2cpp"
CACHE = os.path.join(HERE, "symdb.pkl")

# RVA of the shared empty-body thunk (verified: `ret 0` at 0x30C000).
EMPTY_THUNK = 0x30C000


class SymDB:
    def __init__(self, d):
        self.__dict__.update(d)
        self._fn_starts = sorted(self.methods_by_rva.keys())

    # ---- lookups -------------------------------------------------------
    def method_names(self, rva):
        return self.methods_by_rva.get(rva, [])

    def method_name(self, rva):
        n = self.methods_by_rva.get(rva)
        if not n:
            return None
        return n[0] if len(n) == 1 else "%s (+%d aliases)" % (n[0], len(n) - 1)

    def func_end(self, rva):
        """End of the function starting at rva, from the next known start."""
        i = bisect.bisect_right(self._fn_starts, rva)
        if i < len(self._fn_starts):
            return self._fn_starts[i]
        return rva + 0x200

    def describe_data(self, rva):
        """What does this data RVA refer to? -> (kind, text) or None"""
        if rva in self.strings:
            return ("str", self.strings[rva])
        if rva in self.metadata:
            return ("meta", self.metadata[rva])
        if rva in self.metamethod:
            name, maddr = self.metamethod[rva]
            return ("metamethod", name)
        return None

    def metamethod_target(self, rva):
        e = self.metamethod.get(rva)
        return e[1] if e else None


def _parse_dump_cs(path):
    """Extract per-type field offsets and method RVAs from dump.cs.

    dump.cs layout:
        // Namespace: WitchTrials.Models
        public class TrialStateManager : ... {
            // Fields
            private IPerspectiveStageManager _stageManager; // 0x10
            // Methods
            // RVA: 0x39DA30 Offset: 0x39C430 VA: 0x18039DA30 Slot: 4
            public void RegisterCurrentStage(PerspectiveStageKind kind) { }
        }
    """
    types = {}          # fullname -> {'ns','name','fields':{off:[(name,type)]},'methods':[...]}
    fields_by_type = {}
    rva_owner = {}      # rva -> [(typefull, methodsig)]

    re_ns = re.compile(r'^// Namespace: (.*)$')
    re_type = re.compile(
        r'^(?:\[.*\]\s*)*(?:public |internal |private |protected )?'
        r'(?:static |sealed |abstract |readonly |unsafe )*'
        r'(class|struct|interface|enum)\s+([^\s:{]+)')
    re_field = re.compile(r'^\s*(.+?)\s+(\S+)\s*;\s*//\s*(0x[0-9A-Fa-f]+)\s*$')
    re_rva = re.compile(r'^\s*// RVA: (0x[0-9A-Fa-f]+) Offset: (0x[0-9A-Fa-f]+)(?: VA: (0x[0-9A-Fa-f]+))?')

    cur_ns = ''
    cur_type = None
    pending_rva = None
    section = None

    with open(path, encoding='utf-8', errors='replace') as f:
        for line in f:
            m = re_ns.match(line)
            if m:
                cur_ns = m.group(1).strip()
                continue
            s = line.strip()
            if s.startswith('// Fields'):
                section = 'f'; continue
            if s.startswith('// Methods') or s.startswith('// Properties') or s.startswith('// Events'):
                section = 'm'; continue

            m = re_type.match(line)
            if m and not line.startswith((' ', '\t')):
                kind, nm = m.group(1), m.group(2)
                full = (cur_ns + '.' + nm) if cur_ns else nm
                cur_type = full
                types[full] = {'ns': cur_ns, 'name': nm, 'kind': kind,
                               'fields': {}, 'methods': []}
                section = None
                continue

            m = re_rva.match(line)
            if m:
                pending_rva = int(m.group(1), 16)
                continue

            if section == 'f' and cur_type:
                m = re_field.match(line)
                if m:
                    ftype, fname, foff = m.group(1), m.group(2), int(m.group(3), 16)
                    ftype = re.sub(r'^\s*(?:\[.*\]\s*)*', '', ftype)
                    ftype = re.sub(r'^(public|private|protected|internal|static|readonly|const)\s+', '',
                                   ftype).strip()
                    types[cur_type]['fields'].setdefault(foff, []).append((fname, ftype))
                    continue

            if pending_rva is not None and s and not s.startswith('//'):
                sig = s.rstrip('{ }').strip()
                if cur_type:
                    types[cur_type]['methods'].append((pending_rva, sig))
                    rva_owner.setdefault(pending_rva, []).append((cur_type, sig))
                pending_rva = None

    return types, rva_owner


def build(force=False):
    if not force and os.path.exists(CACHE):
        with open(CACHE, 'rb') as f:
            return SymDB(pickle.load(f))

    sys.stderr.write("building symbol db (one-off, ~1 min) ...\n")
    sj = json.load(open(os.path.join(IL2, 'script.json'), encoding='utf-8'))

    methods_by_rva = {}
    sig_by_rva = {}
    for e in sj['ScriptMethod']:
        a = e['Address']
        methods_by_rva.setdefault(a, []).append(e['Name'])
        sig_by_rva.setdefault(a, e.get('Signature', ''))

    strings = {e['Address']: e['Value'] for e in sj['ScriptString']}
    metadata = {e['Address']: e['Name'] for e in sj['ScriptMetadata']}
    metamethod = {e['Address']: (e['Name'], e['MethodAddress'])
                  for e in sj['ScriptMetadataMethod']}
    addresses = sorted(set(sj['Addresses']))

    sys.stderr.write("  parsing dump.cs ...\n")
    types, rva_owner = _parse_dump_cs(os.path.join(IL2, 'dump.cs'))

    d = dict(methods_by_rva=methods_by_rva, sig_by_rva=sig_by_rva,
             strings=strings, metadata=metadata, metamethod=metamethod,
             addresses=addresses, types=types, rva_owner=rva_owner)
    with open(CACHE, 'wb') as f:
        pickle.dump(d, f, protocol=4)
    sys.stderr.write("  done: %d methods, %d types\n" % (len(methods_by_rva), len(types)))
    return SymDB(d)


if __name__ == '__main__':
    db = build(force='--force' in sys.argv)
    print("methods(unique rva): %d" % len(db.methods_by_rva))
    print("strings: %d  metadata: %d  metamethod: %d" % (
        len(db.strings), len(db.metadata), len(db.metamethod)))
    print("types parsed from dump.cs: %d" % len(db.types))
    t = db.types.get('WitchTrials.Models.TrialStateManager')
    print("\nTrialStateManager fields:", t['fields'] if t else "NOT FOUND")
    print("TrialStateManager methods:")
    for rva, sig in (t['methods'] if t else []):
        print("   0x%-8X %s" % (rva, sig))
