using System.Linq;
using System.Collections.Generic;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Ui
{
    [Token(Token = "0x2000007")]
    public class ChoiceHandlerPanelModified : ChoiceHandlerPanel
    {
    	[Token(Token = "0x400000E")]
    	[FieldOffset(Offset = "0x118")]
    	[SerializeField]
    	private CustomChoiceButtonData[] _customChoiceButtonData;

    	[Token(Token = "0x400000F")]
    	[FieldOffset(Offset = "0x120")]
    	private IDictionary<string, ChoiceHandlerButton> _customButtons;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD36180, 842 B
    	// Synchronous: the serialized `_customChoiceButtonData` (+0x118, path + prefab pairs) becomes the `_customButtons` dictionary
    	// (+0x120) through Enumerable.ToDictionary with the cached <>c lambdas b__2_0 (Path) / b__2_1 (CustomButtonPrefab); the
    	// result is an already completed task (`mov rax, rbx` with rbx = 0, 0x180D364B9) and the base is not called.
    	// Annotated disassembly: work/title-path/disas/chpm_01_Initialize.txt
    	[Token(Token = "0x6000014")]
    	[Address(RVA = "0xD36180", Offset = "0xD34B80", VA = "0x180D36180", Slot = "83")]
    	public override UniTask Initialize()
    	{
    		_customButtons = _customChoiceButtonData.ToDictionary((CustomChoiceButtonData data) => data.Path, (CustomChoiceButtonData data) => data.CustomButtonPrefab);
    		return UniTask.CompletedTask;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD364D0, 356 B
    	// The stub returned null, so every `@choice ... button:` reached Instantiate(null) ("The Object you want to instantiate is
    	// null.", G4b round 24, Adv05 line 82 `ChoiceButtons/Adv/Bad`).  `_customButtons` (+0x120) TryGetValue (IDictionary slot 7,
    	// 0x180D3659B) -> the registered prefab; otherwise Debug.LogWarning(STR + path, context = this) (0x180D365D5) and the package's
    	// own ChoiceHandlerPanel.LoadCustomButtonPrefab (0x1806167A0).  The literal (global-metadata, UTF-8) is
    	// "選択肢パネルにカスタムボタンが登録されていません：".
    	// Annotated disassembly: work/title-path/disas/chpm_02_LoadCustomButtonPrefab.txt
    	[Token(Token = "0x6000015")]
    	[Address(RVA = "0xD364D0", Offset = "0xD34ED0", VA = "0x180D364D0", Slot = "104")]
    	protected override ChoiceHandlerButton LoadCustomButtonPrefab(string path)
    	{
    		if (_customButtons.TryGetValue(path, out ChoiceHandlerButton customButton))
    		{
    			return customButton;
    		}
    		Debug.LogWarning("選択肢パネルにカスタムボタンが登録されていません：" + path, this);
    		return base.LoadCustomButtonPrefab(path);
    	}

    	[Token(Token = "0x6000016")]
    	[Address(RVA = "0x36BA60", Offset = "0x36A460", VA = "0x18036BA60")]
    	public ChoiceHandlerPanelModified()
    	{
    	}
    }
}
