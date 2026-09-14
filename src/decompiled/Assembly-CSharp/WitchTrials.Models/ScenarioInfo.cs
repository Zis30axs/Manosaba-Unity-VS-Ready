using Il2CppDummyDll;

namespace WitchTrials.Models;

// RECONSTRUCTED | Confidence: A | positional record; every member below is compiler-synthesized
// The declaration is recovered from the synthesized members, each checked against
// the Roslyn record lowering. dump.cs's `readonly <X>k__BackingField` fields are
// the init-only property storage (+0x10 Act, +0x14 Chapter, +0x18 Part,
// +0x1C Section).
// RECONSTRUCTED | Confidence: A | RVA: 0x39B6D0 primary ctor: stores the four params, nothing else
// RECONSTRUCTED | Confidence: A | RVA: 0x331650 get_Act (shared 4-byte getter +0x10); init 0x397FF0
// RECONSTRUCTED | Confidence: A | RVA: 0x38F580 get_Chapter (+0x14); init 0x39B770
// RECONSTRUCTED | Confidence: A | RVA: 0x38F590 get_Part (+0x18); init 0x39B780
// RECONSTRUCTED | Confidence: A | RVA: 0x38F5A0 get_Section (+0x1C); init 0x39B790
// RECONSTRUCTED | Confidence: A | RVA: 0x397FF0 init Act
// RECONSTRUCTED | Confidence: A | RVA: 0x39B770 init Chapter
// RECONSTRUCTED | Confidence: A | RVA: 0x39B780 init Part
// RECONSTRUCTED | Confidence: A | RVA: 0x39B790 init Section
// RECONSTRUCTED | Confidence: A | RVA: 0x39B6F0 EqualityContract: `typeof(ScenarioInfo)` (GetTypeFromHandle), 125 B
// RECONSTRUCTED | Confidence: A | RVA: 0x39B7A0 ToString: "ScenarioInfo" + " { ", vtable PrintMembers, ' ' if true, '}', 311 B
// RECONSTRUCTED | Confidence: A | RVA: 0x39B8E0 PrintMembers: "Act = "/", Chapter = "/", Part = " (boxed Enum.ToString)/", Section = ", true, 704 B
// RECONSTRUCTED | Confidence: A | RVA: 0x39BBB0 GetHashCode: EqualityComparer<Type/int/int/ScenarioPartKind/int> chain, the -1521134295 multipliers folded into the imul constants, 425 B
// RECONSTRUCTED | Confidence: A | RVA: 0x39BD60 Equals(object): `Equals(obj as ScenarioInfo)` via vtable slot 7, 146 B
// RECONSTRUCTED | Confidence: A | RVA: 0x39BE00 Equals(ScenarioInfo): reference, null, EqualityContract ==, then four EqualityComparer<T>.Default.Equals, 434 B
// RECONSTRUCTED | Confidence: A | RVA: 0x39BFC0 <Clone>$: `new ScenarioInfo(this)` with the copy ctor inlined, 94 B
// RECONSTRUCTED | Confidence: A | RVA: 0x39C020 copy ctor: four field copies
// RECONSTRUCTED | Confidence: A | RVA: 0x39C050 Deconstruct: four out stores, 28 B
[Token(Token = "0x200017E")]
public record ScenarioInfo(int Act, int Chapter, ScenarioPartKind Part, int Section);
