#nullable enable annotations
using Il2CppDummyDll;

namespace GigaCreation.Essentials.Dialogs.Views
{
    // RECONSTRUCTED | Confidence: A | positional record; every member below is compiler-synthesized
    // The skeleton declared each synthesized member by hand with a stub body, which overrode the record lowering:
    // LabelId / Texts read null, Equals was always false and ToString returned null, so every text insertion
    // (ProvidableDialog.InsertTexts) looked up a null label id.  dump.cs's `readonly <X>k__BackingField` fields are the
    // init-only property storage (+0x10 LabelId, +0x18 Texts); the primary constructor keeps its `params` array.
    // RECONSTRUCTED | Confidence: A | RVA: 0x36C0D0 primary ctor: stores the two params (shared 170 B two-reference body)
    // RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 get_LabelId (shared +0x10 getter); init 0x30C0D0
    // RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 get_Texts (shared +0x18 getter); init 0x36C200
    // RECONSTRUCTED | Confidence: A | RVA: 0xCCDA90 EqualityContract: `typeof(TextInsertData)` (GetTypeFromHandle), 125 B
    // RECONSTRUCTED | Confidence: A | RVA: 0xCCDB10 ToString: "TextInsertData" + " { ", vtable PrintMembers, ' ' if true, '}', 311 B
    // RECONSTRUCTED | Confidence: A | RVA: 0xCCDC50 PrintMembers: "LabelId = " / ", Texts = ", true, 213 B
    // RECONSTRUCTED | Confidence: A | RVA: 0xCCDD30 GetHashCode: EqualityComparer<Type/string/string[]> chain, 287 B
    // RECONSTRUCTED | Confidence: A | RVA: 0xCCDE50 Equals(object): `Equals(obj as TextInsertData)` via vtable slot 7, 146 B
    // RECONSTRUCTED | Confidence: A | RVA: 0xCCDEF0 Equals(TextInsertData): reference, null, EqualityContract ==, then two
    //   EqualityComparer<T>.Default.Equals, 318 B
    // RECONSTRUCTED | Confidence: A | RVA: 0xCCE030 <Clone>$: `new TextInsertData(this)` with the copy ctor inlined, 247 B
    // RECONSTRUCTED | Confidence: A | RVA: 0x36C8D0 copy ctor: two reference field copies (shared body)
    // RECONSTRUCTED | Confidence: A | RVA: 0x36C9B0 Deconstruct: two out stores (shared body)
    // Annotated disassembly: work/title-path/disas/tid_*.txt
    [Token(Token = "0x2000013")]
    public record TextInsertData(string LabelId, params string[] Texts);
}
