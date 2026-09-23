# Generates naninovel_presenter.py / input_checker.py / auto_delay_adjuster.py (one FILE per spec for patch_bodies.py)
import pprint

G = "UnityProject/Assets/Scripts/Game/"
EV = "work/title-path/disas/"


def w(name, FILE, USINGS, BODIES):
    with open('work/title-path/specs/%s.py' % name, 'w', encoding='utf-8', newline='\n') as f:
        f.write('FILE = %r\nUSINGS = %r\nBODIES = %s\n' % (FILE, USINGS, pprint.pformat(BODIES, width=140)))


w("naninovel_presenter", G + "GigaCreation.NaninovelExtender.Core/GigaCreation.NaninovelExtender.Core/NaninovelPresenter.cs",
  ["using GigaCreation.Tools.Service;"], [
    dict(rva="0xD03A20", signature="protected virtual void Awake()",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xD03A20, 206 B -- _naninovelManager (+0x20) = new NaninovelManager(); tail jump",
                  "  ServiceLocator.Register<INaninovelManager>(_naninovelManager).  (WitchTrialsNaninovelPresenter overrides this",
                  "  without calling base, so the shipped game registers its own manager instead.)",
                  "Annotated disassembly: " + EV + "np_0xD03A20.txt"],
         body=["_naninovelManager = new NaninovelManager();", "ServiceLocator.Register(_naninovelManager);"]),
    dict(rva="0xD03AF0", signature="protected virtual void OnDestroy()",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xD03AF0, 104 B -- tail jump ServiceLocator.Unregister<INaninovelManager>(_naninovelManager)",
                  "Annotated disassembly: " + EV + "np_0xD03AF0.txt"],
         body=["ServiceLocator.Unregister(_naninovelManager);"]),
])


def log(what):
    return ["Debug.Log(\"[UnityEngine.InputSystem.InputAction] '\" + _actionMapName + \"/\" + context.action.name + \"' %s.\");" % what]


w("input_checker", G + "GigaCreation.Essentials.Input/GigaCreation.Essentials.Input/InputChecker.cs", [], [
    dict(rva="0xCCFB00", signature="get",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xCCFB00 -- lazy: _actionMap (+0x38), else _actionAsset (+0x20)",
                  "  .FindActionMap(_actionMapName (+0x30), false)", "Annotated disassembly: " + EV + "ic_0xCCFB00.txt"],
         body=["if (_actionMap == null)", "{", "\t_actionMap = _actionAsset.FindActionMap(_actionMapName);", "}", "return _actionMap;"]),
    dict(rva="0xCCFB80", signature="private void Start()",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xCCFB80, 698 B -- only with _enable (+0x28): for every action of ActionMap,",
                  "  performed (m_OnPerformed +0x170) += LogInputNamePerformed and canceled (m_OnCanceled +0x120) += LogInputNameCanceled",
                  "Annotated disassembly: " + EV + "ic_0xCCFB80.txt"],
         body=["if (!_enable)", "{", "\treturn;", "}",
               "foreach (InputAction item in ActionMap)", "{", "\titem.performed += LogInputNamePerformed;",
               "\titem.canceled += LogInputNameCanceled;", "}"]),
    dict(rva="0xCCFE40", signature="private void OnDestroy()",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xCCFE40, 698 B -- Start in reverse (-= on both events)",
                  "Annotated disassembly: " + EV + "ic_0xCCFE40.txt"],
         body=["if (!_enable)", "{", "\treturn;", "}",
               "foreach (InputAction item in ActionMap)", "{", "\titem.performed -= LogInputNamePerformed;",
               "\titem.canceled -= LogInputNameCanceled;", "}"]),
    dict(rva="0xCD0100", signature="private void LogInputNamePerformed(InputAction.CallbackContext context)",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xCD0100, 340 B -- Debug.Log(string.Concat(string[5]) of",
                  "  \"[UnityEngine.InputSystem.InputAction] '\", _actionMapName, \"/\", context.action.name, \"' performed.\")",
                  "Annotated disassembly: " + EV + "ic_0xCD0100.txt"],
         body=log("performed")),
    dict(rva="0xCD0260", signature="private void LogInputNameCanceled(InputAction.CallbackContext context)",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xCD0260, 340 B -- as LogInputNamePerformed with \"' canceled.\"",
                  "Annotated disassembly: " + EV + "ic_0xCD0260.txt"],
         body=log("canceled")),
])

w("auto_delay_adjuster", G + "GigaCreation.NaninovelExtender.UI/GigaCreation.NaninovelExtender.UI/AutoDelayAdjuster.cs", [], [
    dict(rva="0xD3F160", signature="get",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xD3F160 -- lazy: _scriptPlayerConfiguration (+0x20), else",
                  "  Engine.GetConfiguration<ScriptPlayerConfiguration>()", "Annotated disassembly: " + EV + "ada_0xD3F160.txt"],
         body=["if (_scriptPlayerConfiguration == null)", "{",
               "\t_scriptPlayerConfiguration = Engine.GetConfiguration<ScriptPlayerConfiguration>();", "}",
               "return _scriptPlayerConfiguration;"]),
    dict(rva="0xD3F220", signature="get",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xD3F220 -- lazy: _naniAudioManager (+0x28), else Engine.GetServiceOrErr<IAudioManager>()",
                  "Annotated disassembly: " + EV + "ada_0xD3F220.txt"],
         body=["if (_naniAudioManager == null)", "{", "\t_naniAudioManager = Engine.GetServiceOrErr<IAudioManager>();", "}",
               "return _naniAudioManager;"]),
    dict(rva="0xD3F2E0", signature="private void OnDestroy()",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xD3F2E0 (the body shared with RestoreIfShortened) -- RestoreIfShortened()",
                  "Annotated disassembly: " + EV + "ada_0xD3F2E0.txt"],
         body=["RestoreIfShortened();"]),
    dict(rva="0xD3F320", signature="public void Adjust()",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xD3F320, 529 B -- voice = NaniAudioManager.GetPlayedVoice() (IAudioManager slot 29);",
                  "  a non-null voice other than _previousPlayedVoicePath (+0x38) shortens the auto-play delay, anything else restores",
                  "  it (both inlined); the voice is remembered", "Annotated disassembly: " + EV + "ada_0xD3F320.txt"],
         body=["string playedVoice = NaniAudioManager.GetPlayedVoice();",
               "if (playedVoice != null && playedVoice != _previousPlayedVoicePath)", "{", "\tShortenIfNotShortened();", "}",
               "else", "{", "\tRestoreIfShortened();", "}",
               "_previousPlayedVoicePath = playedVoice;"]),
    dict(rva="0xD3F540", signature="private void ShortenIfNotShortened()",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xD3F540, 82 B -- unless _isShortened (+0x30): store MinAutoPlayDelay (+0x20) in",
                  "  _storedMinAutoPlayDelay (+0x34), multiply it by DelayMultiplier 0.2 (data 0x2AE2670), set _isShortened",
                  "Annotated disassembly: " + EV + "ada_0xD3F540.txt"],
         body=["if (!_isShortened)", "{", "\t_storedMinAutoPlayDelay = ScriptPlayerConfiguration.MinAutoPlayDelay;",
               "\tScriptPlayerConfiguration.MinAutoPlayDelay *= 0.2f;", "\t_isShortened = true;", "}"]),
    dict(rva="0xD3F2E0", signature="private void RestoreIfShortened()",
         comment=["RECONSTRUCTED | Confidence: A | RVA: 0xD3F2E0, 44 B -- with _isShortened: MinAutoPlayDelay = _storedMinAutoPlayDelay,",
                  "  _isShortened = false", "Annotated disassembly: " + EV + "ada_0xD3F2E0.txt"],
         body=["if (_isShortened)", "{", "\tScriptPlayerConfiguration.MinAutoPlayDelay = _storedMinAutoPlayDelay;",
               "\t_isShortened = false;", "}"]),
])
