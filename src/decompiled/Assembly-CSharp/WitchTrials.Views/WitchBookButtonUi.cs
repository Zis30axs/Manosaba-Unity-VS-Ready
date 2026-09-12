using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x200010F")]
public class WitchBookButtonUi : CustomUI, IWitchBookButtonUi, IManagedUI
{
	[Token(Token = "0x400050C")]
	[FieldOffset(Offset = "0xE0")]
	[Header("References for WitchBook Button")]
	[SerializeField]
	private Button _button;

	[Token(Token = "0x400050D")]
	[FieldOffset(Offset = "0xE8")]
	[Header("Parameters for WitchBook Button")]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _openSfxPath;

	[Token(Token = "0x400050E")]
	[FieldOffset(Offset = "0xF0")]
	private IAudioManager _naniAudioManager;

	[Token(Token = "0x400050F")]
	[FieldOffset(Offset = "0xF8")]
	private IUIManager _naniUiManager;

	[Token(Token = "0x4000510")]
	[FieldOffset(Offset = "0x100")]
	private IWitchBookUi _witchBookUi;

	[Token(Token = "0x1700008F")]
	private IAudioManager NaniAudioManager
	{
		[Token(Token = "0x600046D")]
		[Address(RVA = "0x3772E0", Offset = "0x375CE0", VA = "0x1803772E0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000090")]
	private IUIManager NaniUiManager
	{
		[Token(Token = "0x600046E")]
		[Address(RVA = "0x3773B0", Offset = "0x375DB0", VA = "0x1803773B0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000091")]
	private IWitchBookUi WitchBookUi
	{
		[Token(Token = "0x600046F")]
		[Address(RVA = "0x377480", Offset = "0x375E80", VA = "0x180377480")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000470")]
	[Address(RVA = "0x377600", Offset = "0x376000", VA = "0x180377600", Slot = "6")]
	protected override void Start()
	{
	}

	[Token(Token = "0x6000471")]
	[Address(RVA = "0x336F70", Offset = "0x335970", VA = "0x180336F70")]
	public WitchBookButtonUi()
	{
	}
}
