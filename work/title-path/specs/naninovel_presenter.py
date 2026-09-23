FILE = 'UnityProject/Assets/Scripts/Game/GigaCreation.NaninovelExtender.Core/GigaCreation.NaninovelExtender.Core/NaninovelPresenter.cs'
USINGS = ['using GigaCreation.Tools.Service;']
BODIES = [{'body': ['_naninovelManager = new NaninovelManager();', 'ServiceLocator.Register(_naninovelManager);'],
  'comment': ['RECONSTRUCTED | Confidence: A | RVA: 0xD03A20, 206 B -- _naninovelManager (+0x20) = new NaninovelManager(); tail jump',
              '  ServiceLocator.Register<INaninovelManager>(_naninovelManager).  (WitchTrialsNaninovelPresenter overrides this',
              '  without calling base, so the shipped game registers its own manager instead.)',
              'Annotated disassembly: work/title-path/disas/np_0xD03A20.txt'],
  'rva': '0xD03A20',
  'signature': 'protected virtual void Awake()'},
 {'body': ['ServiceLocator.Unregister(_naninovelManager);'],
  'comment': ['RECONSTRUCTED | Confidence: A | RVA: 0xD03AF0, 104 B -- tail jump '
              'ServiceLocator.Unregister<INaninovelManager>(_naninovelManager)',
              'Annotated disassembly: work/title-path/disas/np_0xD03AF0.txt'],
  'rva': '0xD03AF0',
  'signature': 'protected virtual void OnDestroy()'}]
