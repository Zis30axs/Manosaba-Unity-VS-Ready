FILE = 'UnityProject/Assets/Scripts/Game/GigaCreation.Essentials.UI/GigaCreation.Essentials.Ui/LocalizationDropdown.cs'
USINGS = []
BODIES = [{'body': ['base.Dropdown.ClearOptions();',
           'base.Dropdown.AddOptions(options);',
           'if (base.Dropdown is TweeningDropdownLocalized tweeningDropdownLocalized)',
           '{',
           '\ttweeningDropdownLocalized.ItemFontAssets = fontAssets;',
           '}'],
  'comment': ['RECONSTRUCTED | Confidence: A | RVA: 0xCE0B70, 262 B -- Dropdown (+0x20, getter inlined).ClearOptions(); '
              'AddOptions(options);',
              '  when the dropdown is a TweeningDropdownLocalized (isinst) its ItemFontAssets = fontAssets',
              'Annotated disassembly: work/title-path/disas/ldd_0xCE0B70.txt'],
  'rva': '0xCE0B70',
  'signature': 'public void SetOptions(List<string> options, TMP_FontAsset[] fontAssets)'},
 {'body': ['if (base.Dropdown is TweeningDropdownLocalized tweeningDropdownLocalized)',
           '{',
           '\ttweeningDropdownLocalized.ChangeCaptionVisibility(visible);',
           '}'],
  'comment': ['RECONSTRUCTED | Confidence: A | RVA: 0xCE0C80, 150 B -- when Dropdown is a TweeningDropdownLocalized (isinst):',
              '  its ChangeCaptionVisibility(visible) (inlined: _captionCanvasGroup.alpha)',
              'Annotated disassembly: work/title-path/disas/ldd_0xCE0C80.txt'],
  'rva': '0xCE0C80',
  'signature': 'public void ChangeCaptionVisibility(bool visible)'}]
