FILE = "UnityProject/Assets/Scripts/Game/GigaCreation.Essentials.UI/GigaCreation.Essentials.Ui/LabelData.cs"
USINGS = ["using System.Text.RegularExpressions;"]
EV = "work/title-path/disas/ld_"
BODIES = [
    dict(rva="0xCE3F40", signature="get",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xCE3F40, 17 B -- copies the 20-byte _color (+0x20) to the return buffer",
                  "Annotated disassembly: " + EV + "0xCE3F40.txt"],
         body=["return _color;"]),
    dict(rva="0xCE3F80", signature="public bool IsEmpty()",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xCE3F80, 387 B -- LocalizableText.IsEmpty inlined on _localizableText",
                  "  (+0x10; null throws NullReferenceException at 0x180CE4102)",
                  "Annotated disassembly: " + EV + "0xCE3F80.txt"],
         body=["return _localizableText.IsEmpty();"]),
    dict(rva="0xCE4110", signature="public bool ValidateLabelId(string value)",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xCE4110, 222 B -- empty -> true; !Regex.IsMatch(value, \"^[0-9].*$\") -> true;",
                  "  else Debug.LogWarning(literal 0x2E686E8 + value) and false",
                  "Annotated disassembly: " + EV + "0xCE4110.txt"],
         body=["if (string.IsNullOrEmpty(value))", "{", "\treturn true;", "}",
               "if (!Regex.IsMatch(value, \"^[0-9].*$\"))", "{", "\treturn true;", "}",
               "Debug.LogWarning(\"ラベル ID の先頭は数字以外である必要があります：\" + value);", "return false;"]),
]
