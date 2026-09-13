"""Patch one trivial getter/setter body in a decompiled .cs file, in place.

Finds the accessor (get/set) whose preceding [Address(RVA = "0x...")] matches,
replaces its body, and inserts a RECONSTRUCTED confidence comment right before
the [Token]/[Address] attribute pair. Idempotent: if the comment is already
there (re-run), it skips.

Usage as a library: call patch(path, rva_hex, kind, body_lines, comment_lines)
"""
import re, sys

def patch(path, rva_hex, kind, new_body_lines, comment_lines, dry_run=False):
    """
    path: file path
    rva_hex: e.g. "0x33A4E0" (must match exactly as it appears in the file)
    kind: 'get' or 'set'
    new_body_lines: list of C# statement lines (no braces), to replace the body
    comment_lines: list of comment strings (without '// ') to insert above [Token]
    """
    with open(path, encoding='utf-8') as f:
        text = f.read()

    # Locate the Address attribute with this RVA.
    addr_pat = re.compile(r'\[Address\(RVA = "%s"[^\]]*\)\]' % re.escape(rva_hex))
    m = addr_pat.search(text)
    if not m:
        return False, "RVA not found in file"

    # Already patched? (idempotency check)
    marker = "// RECONSTRUCTED"
    # Find the start of the attribute stack for this accessor: walk backwards
    # to the start of the line containing the nearest preceding blank/`get`/`set`/`{`.
    addr_start = m.start()

    # Find the accessor keyword (get/set) after the Address attribute.
    after = text[m.end():]
    kw_pat = re.compile(r'\s*(get|set)\s*\n(\s*)\{')
    km = kw_pat.match(after)
    if not km or km.group(1) != kind:
        # maybe there are other attributes between Address and get/set; widen search
        kw_pat2 = re.compile(r'(?:\[[^\]]*\]\s*)*\s*(get|set)\s*\n(\s*)\{')
        km = kw_pat2.match(after)
        if not km or km.group(1) != kind:
            return False, "accessor keyword '%s' not found after Address attr (found: %r)" % (
                kind, after[:80])

    indent = km.group(2)
    body_start = m.end() + km.end()
    # Find matching closing brace for this accessor body (bodies here are flat, no nested braces
    # except possibly object/array initializers -- handle simple brace counting).
    depth = 1
    i = body_start
    while depth > 0:
        c = text[i]
        if c == '{':
            depth += 1
        elif c == '}':
            depth -= 1
        i += 1
    body_end = i - 1  # position of the closing '}'

    stmt_indent = indent + '\t'
    new_body = '\n' + '\n'.join(stmt_indent + l for l in new_body_lines) + '\n' + indent
    new_text = text[:body_start] + new_body + text[body_end:]

    # Insert comment block just before the start of the attribute stack for this
    # accessor. Find the start of line containing addr_start, then walk back
    # further over any preceding [Token(...)] attribute line(s) that belong to
    # this same accessor (immediately adjacent, no blank line between).
    line_start = new_text.rfind('\n', 0, addr_start) + 1
    # walk back over preceding attribute lines (e.g. [Token(...)]) with no blank line
    probe = line_start
    while True:
        prev_line_start = new_text.rfind('\n', 0, probe - 1) + 1 if probe > 0 else 0
        prev_line = new_text[prev_line_start:probe - 1] if probe > 0 else ''
        if re.match(r'^\s*\[Token\(', prev_line):
            probe = prev_line_start
        else:
            break
    insert_at = probe

    if marker in new_text[max(0, insert_at - 400):insert_at]:
        return False, "already patched (marker found nearby)"

    comment_block = ''.join(indent + '// ' + l + '\n' for l in comment_lines)
    if dry_run:
        return True, "WOULD INSERT:\n" + comment_block + "WOULD SET BODY TO:\n" + new_body
    final_text = new_text[:insert_at] + comment_block + new_text[insert_at:]

    with open(path, 'w', encoding='utf-8', newline='\n') as f:
        f.write(final_text)
    return True, "patched"


if __name__ == '__main__':
    import json
    jobs = json.load(open(sys.argv[1], encoding='utf-8'))
    dry = '--dry' in sys.argv
    for j in jobs:
        ok, msg = patch(j['path'], j['rva'], j['kind'], j['body'], j['comment'], dry_run=dry)
        print(('OK  ' if ok else 'FAIL'), j['path'], j['rva'], j['kind'], '->', msg[:120])
