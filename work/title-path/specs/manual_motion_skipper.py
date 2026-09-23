FILE = "UnityProject/Assets/Scripts/Game/GigaCreation.Essentials.Dialogs/GigaCreation.Essentials.Dialogs.Models/ManualMotionSkipper.cs"
USINGS = ["using UnityEngine;"]
EV = "work/title-path/disas/mms_"
BODIES = [
    dict(rva="0xCCF220", signature="public void Dispose()",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xCCF220, 125 B -- _cts (+0x10).Cancel() (inlined: disposed check,",
                  "  NotifyCancellation(false)), then tail jump _cts.Dispose()", "Annotated disassembly: " + EV + "0xCCF220.txt"],
         body=["_cts.Cancel();", "_cts.Dispose();"]),
    dict(rva="0xCCF2A0", signature="public void StartUpdate(float startTime, bool ignoreTimeScale = false)",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xCCF2A0, 476 B -- only the first call does anything (_started +0x18,",
                  "  0x180CCF2E1): _started = true; ManualMotionDispatcher.Default.Update(startTime) (float widened to double);",
                  "  UpdateAsync(ignoreTimeScale).Forget()", "Annotated disassembly: " + EV + "0xCCF2A0.txt"],
         body=["// 0x180CCF2E1: the shipped code starts the update loop once",
               "if (_started)", "{", "\treturn;", "}",
               "_started = true;", "ManualMotionDispatcher.Default.Update(startTime);", "UpdateAsync(ignoreTimeScale).Forget();"]),
    dict(rva="0xCCF480", signature="private UniTask UpdateAsync(bool ignoreTimeScale = false)",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xCCF480 (kickoff) / 0xCCF5D0 (<UpdateAsync>d__5.MoveNext, 953 B): until _cts",
                  "  is cancelled (IsCancellationRequested inlined: state >= 2, 0x180CCF692): await UniTask.Yield(_cts.Token), then",
                  "  ManualMotionDispatcher.Default.Update(ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime) (icalls",
                  "  named at data 0x2608F48 / 0x2608950). Body in UpdateAsyncCore.", "Annotated disassembly: " + EV + "0xCCF5D0.txt"],
         body=["return UpdateAsyncCore(ignoreTimeScale);"],
         after=["private async UniTask UpdateAsyncCore(bool ignoreTimeScale)", "{",
                "\twhile (!_cts.IsCancellationRequested)", "\t{",
                "\t\tawait UniTask.Yield(_cts.Token);",
                "\t\tManualMotionDispatcher.Default.Update(ignoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime);",
                "\t}", "}"]),
]
