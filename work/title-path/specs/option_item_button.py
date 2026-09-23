FILE = 'UnityProject/Assets/Scripts/Game/GigaCreation.Essentials.UI/GigaCreation.Essentials.Ui/OptionItemButton.cs'
USINGS = ['using UniRx;']
BODIES = [{'body': ['_button = GetComponentInChildren<Button>();'],
  'comment': ['RECONSTRUCTED | Confidence: A | RVA: 0xCD80B0 -- GetComponentInChildren<Button>() -> _button (+0x20)',
              'Annotated disassembly: work/title-path/disas/oib_0xCD80B0.txt'],
  'rva': '0xCD80B0',
  'signature': 'private void Reset()'},
 {'body': ['return _button.onClick.AsObservable();'],
  'comment': ['RECONSTRUCTED | Confidence: A | RVA: 0x33A4B0 -- tail jump UnityEventExtensions.AsObservable(_button.onClick)',
              'Annotated disassembly: work/title-path/disas/oib_0x33A4B0.txt'],
  'rva': '0x33A4B0',
  'signature': 'public IObservable<Unit> OnClickAsObservable()'}]
