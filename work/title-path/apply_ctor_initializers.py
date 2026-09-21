"""Restore field initializers the Il2CppDumper skeleton dropped, from the native constructors found by ctor_sweep.py.

    python work/title-path/apply_ctor_initializers.py

Each entry: file, the exact field declaration, the initializer, and the constructor RVA / instruction that proves it.
The constructor keeps its empty body; a `// RECONSTRUCTED` line above the field names the evidence.  Idempotent: a
declaration that already carries an initializer is left alone.  Keeps BOM and line endings.
"""
import os

ROOT = os.path.join(os.path.dirname(os.path.abspath(__file__)), "..", "..", "UnityProject", "Assets", "Scripts", "Game")
UI = "GigaCreation.Essentials.UI/GigaCreation.Essentials.Ui/"

ENTRIES = [
    (UI + "ButtonExtendedBase.cs", "private readonly CompositeDisposable _disposables", "new CompositeDisposable()",
     "0xCD6AA0: new CompositeDisposable() -> +0x108"),
    (UI + "LabeledSlider.cs", "private readonly CompositeDisposable _disposables", "new CompositeDisposable()",
     "0xCEB270: new CompositeDisposable() -> +0x30"),
    (UI + "OptionItemDropdown.cs", "private readonly CompositeDisposable _disposables", "new CompositeDisposable()",
     "0xCE0D20 (shared with LocalizationDropdown, which adds none): new CompositeDisposable() -> +0x30"),
    (UI + "OptionItemToggle.cs", "private readonly ISubject<int> _onValueChangedSubject", "new Subject<int>()",
     "0xCEC1C0: new Subject<int>() -> +0x28"),
    (UI + "OptionItemToggle.cs", "private readonly CompositeDisposable _disposables", "new CompositeDisposable()",
     "0xCEC1C0: new CompositeDisposable() -> +0x30"),
    (UI + "OptionItemToggleGroup.cs", "private readonly ISubject<int> _valueChangedSubject", "new Subject<int>()",
     "0xCEC940: new Subject<int>() -> +0x28"),
    (UI + "OptionItemToggleGroup.cs", "private readonly CompositeDisposable _disposable", "new CompositeDisposable()",
     "0xCEC940: new CompositeDisposable() -> +0x30"),
    (UI + "SlideToggle.cs", "private SlideUtility _slideUtil", "new SlideUtility()",
     "0xCF0AA0: new SlideUtility() (its ctor inlined: _tweenDuration 0.1) -> +0x130"),
    (UI + "SlideToggle.cs", "private readonly CompositeDisposable _disposables", "new CompositeDisposable()",
     "0xCF0AA0: new CompositeDisposable() -> +0x138"),
    (UI + "TweeningToggle.cs", "private readonly CompositeDisposable _disposables", "new CompositeDisposable()",
     "0xCF1290: new CompositeDisposable() -> +0x160"),
    (UI + "TweeningDropdown.cs", "private readonly CompositeDisposable _selectableDisposables", "new CompositeDisposable()",
     "0xCE2CE0 (shared with TweeningDropdownLocalized): new CompositeDisposable() -> +0x1B0"),
    (UI + "TweeningDropdown.cs", "private readonly CompositeDisposable _playSfxDisposables", "new CompositeDisposable()",
     "0xCE2CE0: new CompositeDisposable() -> +0x1B8"),
    (UI + "FadeUtility.cs", "private float _alphaOnDisabled", "0.5f", "0xCDBF50: mov [rcx+0x10], 0x3F000000"),
    (UI + "FadeUtility.cs", "private float _tweenDuration", "0.1f", "0xCDBF50: mov [rcx+0x14], 0x3DCCCCCD"),
    (UI + "ScaleUtility.cs", "private float _scaleOnHighlighted", "1.1f", "0xCDD130: mov [rcx+0x10], 0x3F8CCCCD"),
    (UI + "ScaleUtility.cs", "private float _tweenDuration", "0.1f", "0xCDD130: mov [rcx+0x14], 0x3DCCCCCD"),
    (UI + "SlideUtility.cs", "private float _tweenDuration", "0.1f", "0xCDD4A0: mov [rcx+0x14], 0x3DCCCCCD"),
    (UI + "LabelGlowUtility.cs", "private float _glowDuration", "0.5f", "0xCDC750: mov [rcx+0x20], 0x3F000000"),
    (UI + "LabelData.cs", "private ToggleableStruct<float> _scale", "1f",
     "0xCE41F0: ToggleableStruct<float>.op_Implicit(1.0) -> +0x18"),
    (UI + "LabelData.cs", "private ToggleableStruct<Color> _color", "UnityEngine.Color.black",
     "0xCE41F0: ToggleableStruct<Color>.op_Implicit(data 0x2AE3470 = (0, 0, 0, 1)) -> +0x20"),
    (UI + "LabelData.cs", "private ToggleableStruct<HorizontalAlignmentOptions> _alignment", "HorizontalAlignmentOptions.Left",
     "0xCE41F0: ToggleableStruct<HorizontalAlignmentOptions>.op_Implicit(1 = Left) -> +0x3C"),
    (UI + "RebuildableLabel.cs", "private HorizontalAlignmentOptions _defaultHorizontalAlignment", "HorizontalAlignmentOptions.Left",
     "0x311810 (shared with WitchTrialsButtonLabel): mov [rcx+0x48], 1"),
    ("GigaCreation.Essentials.Localization/GigaCreation.Essentials.Localization/LocalizableText.cs",
     "private bool _changeFont", "true", "0xCD1C10: mov byte [rcx+0x20], 1"),
]


def main():
    by_file = {}
    for entry in ENTRIES:
        by_file.setdefault(entry[0], []).append(entry[1:])
    for rel, items in by_file.items():
        path = os.path.normpath(os.path.join(ROOT, rel))
        raw = open(path, "rb").read()
        bom = raw[:3] == b"\xef\xbb\xbf"
        text = raw.decode("utf-8-sig")
        crlf = "\r\n" in text
        lines = text.replace("\r\n", "\n").split("\n")
        changed = 0
        for decl, init, evidence in items:
            hits = [i for i, l in enumerate(lines) if l.strip() == decl + ";"]
            if not hits:
                if any(l.strip().startswith(decl + " =") for l in lines):
                    continue  # already restored
                raise SystemExit("%s: declaration not found: %s" % (rel, decl))
            if len(hits) > 1:
                raise SystemExit("%s: ambiguous declaration: %s" % (rel, decl))
            i = hits[0]
            indent = lines[i][:len(lines[i]) - len(lines[i].lstrip())]
            lines[i] = indent + decl + " = " + init + ";"
            a = i
            while a > 0 and lines[a - 1].strip().startswith("["):
                a -= 1
            lines.insert(a, indent + "// RECONSTRUCTED | Confidence: A | field initializer from the constructor at " + evidence)
            changed += 1
        if changed:
            text = "\n".join(lines)
            if crlf:
                text = text.replace("\n", "\r\n")
            open(path, "wb").write((b"\xef\xbb\xbf" if bom else b"") + text.encode("utf-8"))
        print("%-80s %d initializer(s)" % (rel, changed))


if __name__ == "__main__":
    main()
