FILE = "UnityProject/Assets/Scripts/Game/GigaCreation.NaninovelExtender.UI/GigaCreation.NaninovelExtender.UI/ManagedUiExtensions.cs"
USINGS = ["using Naninovel.UI;"]
EV = "work/title-path/disas/mue_"


def body(verb, visible, token_kind):
    if token_kind == "async":
        head = ["private static async Cysharp.Threading.Tasks.UniTask %sAsyncCore(IManagedUI self, float? duration, AsyncToken token)" % verb, "{",
                "\tif (self is CustomUI customUI && customUI.TryGetComponent<NaninovelScreen>(out NaninovelScreen component))", "\t{",
                "\t\tusing CancellationTokenSource linkedCts = token.CreateLinkedTokenSource();",
                "\t\tawait component.%sAsync(duration, linkedCts.Token);" % verb, "\t}",
                "\telse", "\t{", "\t\tawait self.ChangeVisibility(%s, duration, token);" % visible, "\t}", "}"]
    else:
        head = ["private static async Cysharp.Threading.Tasks.UniTask %sAsyncCore(IManagedUI self, float? duration, CancellationToken ct)" % verb, "{",
                "\tif (self is CustomUI customUI && customUI.TryGetComponent<NaninovelScreen>(out NaninovelScreen component))", "\t{",
                "\t\tawait component.%sAsync(duration, ct);" % verb, "\t}",
                "\telse", "\t{", "\t\tawait self.ChangeVisibility(%s, duration, new AsyncToken(ct));" % visible, "\t}", "}"]
    return head


BODIES = [
    dict(rva="0xD39A40", signature="public static Cysharp.Threading.Tasks.UniTask PresentAsync(this IManagedUI self, [Optional] float? duration, [Optional] AsyncToken token)",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xD39A40 (kickoff) / 0xD3AE60 (<PresentAsync>d__0.MoveNext, 1857 B): a CustomUI",
                  "  carrying a NaninovelScreen (isinst + TryGetComponent via its GameObject) is presented through it with a token",
                  "  source linked from the AsyncToken (<linkedCts>5__2, disposed in a finally); any other UI gets the Naninovel",
                  "  ChangeVisibility(true, duration, token) (<>u__2). Body in PresentAsyncCore.",
                  "  (The skeleton returned default: Options / Gallery / Load on the title screen did nothing.)",
                  "Annotated disassembly: " + EV + "0xD3AE60.txt"],
         body=["return PresentAsyncCore(self, duration, token);"], after=body("Present", "true", "async")),
    dict(rva="0xD39C10", signature="public static Cysharp.Threading.Tasks.UniTask DismissAsync(this IManagedUI self, [Optional] float? duration, [Optional] AsyncToken token)",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xD39C10 (kickoff) / 0xD3A180 (<DismissAsync>d__1.MoveNext, 1857 B): PresentAsync",
                  "  with NaninovelScreen.DismissAsync and ChangeVisibility(false, ...). Body in DismissAsyncCore.",
                  "Annotated disassembly: " + EV + "0xD3A180.txt"],
         body=["return DismissAsyncCore(self, duration, token);"], after=body("Dismiss", "false", "async")),
    dict(rva="0xD39DE0", signature="public static Cysharp.Threading.Tasks.UniTask PresentAsync(this IManagedUI self, [Optional] float? duration, [Optional] CancellationToken ct)",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xD39DE0 (kickoff) / 0xD3B5B0 (<PresentAsync>d__2.MoveNext, 1416 B): a CustomUI",
                  "  carrying a NaninovelScreen is presented through it with ct; any other UI gets ChangeVisibility(true, duration,",
                  "  new AsyncToken(ct)). Body in PresentAsyncCore.", "Annotated disassembly: " + EV + "0xD3B5B0.txt"],
         body=["return PresentAsyncCore(self, duration, ct);"], after=body("Present", "true", "ct")),
    dict(rva="0xD39FB0", signature="public static Cysharp.Threading.Tasks.UniTask DismissAsync(this IManagedUI self, [Optional] float? duration, [Optional] CancellationToken ct)",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xD39FB0 (kickoff) / 0xD3A8D0 (<DismissAsync>d__3.MoveNext, 1416 B): the",
                  "  CancellationToken PresentAsync with NaninovelScreen.DismissAsync and ChangeVisibility(false, ...). Body in",
                  "  DismissAsyncCore.", "Annotated disassembly: " + EV + "0xD3A8D0.txt"],
         body=["return DismissAsyncCore(self, duration, ct);"], after=body("Dismiss", "false", "ct")),
]
