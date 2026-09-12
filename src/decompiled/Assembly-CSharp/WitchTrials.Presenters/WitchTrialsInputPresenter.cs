using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Presenters;

[Token(Token = "0x200014F")]
public class WitchTrialsInputPresenter : MonoBehaviour
{
	[Token(Token = "0x400066F")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private string _naninovelActionMapName;

	[Token(Token = "0x4000670")]
	[FieldOffset(Offset = "0x28")]
	private IInputManager _inputManager;

	[Token(Token = "0x6000591")]
	[Address(RVA = "0x38EC50", Offset = "0x38D650", VA = "0x18038EC50")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000592")]
	[Address(RVA = "0x38EDC0", Offset = "0x38D7C0", VA = "0x18038EDC0")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000593")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public WitchTrialsInputPresenter()
	{
	}
}
