"""Replace stub member bodies of a reconstructed C# file, located by RVA + signature, with recovered code.

    python work/title-path/patch_bodies.py <spec.py>

The spec is a Python file defining FILE (repo-relative path), BODIES (list of dicts) and optionally USINGS (list of
`using X;` lines to add).  Each BODIES item:
    rva        "0xD366E0"                       the member's [Address(RVA = ...)] value
    signature  "protected override void Awake()" the start of the line that follows the attribute lines
    body       ["base.Awake();", ...]           new body lines (relative indentation with \\t)
    comment    ["RECONSTRUCTED | ...", ...]     optional; inserted above the member's first attribute line
    occurrence 2                                optional, 1-based: which matching member to patch when identical-code
                                                folding gives several members the same RVA and signature
    auto       True                             optional: turn the accessor into an auto-property accessor (`get;`)
                                                instead of giving it a body
    new_signature "private async UniTask X()"    optional: replaces `signature` in the member line (e.g. to add `async`)
    after      ["private async UniTask XCore()", "{", ...]  optional: member lines inserted after this member (at its
                                                indentation), e.g. the XxxCore body behind an [AsyncStateMachine] kickoff

Keeps the file's BOM and line endings; refuses to touch a member whose signature line does not match.
"""
import runpy
import sys


def main():
    spec = runpy.run_path(sys.argv[1])
    path = spec["FILE"]
    raw = open(path, "rb").read()
    bom = raw[:3] == b"\xef\xbb\xbf"
    text = raw.decode("utf-8-sig")
    crlf = "\r\n" in text
    if crlf:
        text = text.replace("\r\n", "\n")
    lines = text.split("\n")

    for item in spec["BODIES"]:
        rva, signature = item["rva"], item["signature"]
        skip = item.get("occurrence", 1) - 1
        done = False
        for i, line in enumerate(lines):
            if '[Address(RVA = "%s"' % rva not in line:
                continue
            j = i + 1
            while lines[j].strip().startswith("["):
                j += 1
            if not lines[j].strip().startswith(signature):
                continue
            if skip:
                skip -= 1
                continue
            k = j + 1
            if lines[k].strip() != "{":
                raise SystemExit("no body brace after %s %s: %r" % (rva, signature, lines[k]))
            depth, end = 0, None
            for m in range(k, len(lines)):
                depth += lines[m].count("{") - lines[m].count("}")
                if depth == 0:
                    end = m
                    break
            indent = lines[k][:len(lines[k]) - len(lines[k].lstrip())]
            if item.get("new_signature"):
                lines[j] = lines[j].replace(signature, item["new_signature"], 1)
            if item.get("auto"):
                lines[j] = lines[j].rstrip() + ";"
                del lines[k:end + 1]
            else:
                after = [""] + [(indent + a) if a else "" for a in item["after"]] if item.get("after") else []
                lines[k:end + 1] = ([indent + "{"] + [(indent + "\t" + b) if b else "" for b in item["body"]] + [indent + "}"]
                                    + after)
            if item.get("comment"):
                a = i
                while a > 0 and lines[a - 1].strip().startswith("["):
                    a -= 1
                lines[a:a] = [indent + "// " + c for c in item["comment"]]
            done = True
            break
        if not done:
            raise SystemExit("member not found: %s %s" % (rva, signature))

    text = "\n".join(lines)
    for using in spec.get("USINGS", []):
        if using not in text:
            first = text.find("using ")
            text = text[:first] + using + "\n" + text[first:]
    if crlf:
        text = text.replace("\n", "\r\n")
    open(path, "wb").write((b"\xef\xbb\xbf" if bom else b"") + text.encode("utf-8"))
    print("patched %d member(s) in %s" % (len(spec["BODIES"]), path))


if __name__ == "__main__":
    main()
