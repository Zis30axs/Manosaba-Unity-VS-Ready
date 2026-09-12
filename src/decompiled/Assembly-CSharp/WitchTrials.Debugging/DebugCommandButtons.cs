using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Input;
using GigaCreation.Tools.Debugging.Ui;
using Il2CppDummyDll;
using Naninovel;
using UnityEngine;
using UnityEngine.UI;
using WitchTrials.Models;

namespace WitchTrials.Debugging;

[Token(Token = "0x20001AA")]
public class DebugCommandButtons : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x20001AC")]
	[CompilerGenerated]
	private struct _003CGoToDebugHomeAsync_003Ed__24
{
		[Token(Token = "0x40007CE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40007CF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40007D0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public DebugCommandButtons _003C_003E4__this;

		[Token(Token = "0x40007D1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x40007D2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40007D3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000768")]
		[Address(RVA = "0x3A8E60", Offset = "0x3A7860", VA = "0x1803A8E60", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000769")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40007BD")]
	private const string EnableDebugSkipVarName = "enableDebugSkip";

	[Token(Token = "0x40007BE")]
	private const string AllowContinueDebateVarName = "allowContinueDebate";

	[Token(Token = "0x40007BF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private DebugPanel _debugPanel;

	[Token(Token = "0x40007C0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[Space]
	[SerializeField]
	private Button _togglePauseButton;

	[Token(Token = "0x40007C1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Button _speedUpButton;

	[Token(Token = "0x40007C2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private Button _slowDownButton;

	[Token(Token = "0x40007C3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[Space]
	[SerializeField]
	private Button _debugHomeButton;

	[Token(Token = "0x40007C4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[SerializeField]
	private Button _toggleDebugSkipButton;

	[Token(Token = "0x40007C5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[SerializeField]
	private Button _toggleAllowContinueDebateButton;

	[Token(Token = "0x40007C6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[SerializeField]
	private Button _unlockAllStillsButton;

	[Token(Token = "0x40007C7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[Header("Parameters")]
	[ResourcePopup("Scripts", "Scripts", null)]
	[SerializeField]
	private string _debugHomeScenario;

	[Token(Token = "0x40007C8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private GigaCreation.Essentials.Input.IInputManager _inputManager;

	[Token(Token = "0x40007C9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	private IWitchTrialsNaninovelManager _witchTrialsNaninovelManager;

	[Token(Token = "0x40007CA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	private ICustomVariableManager _naniVarManager;

	[Token(Token = "0x40007CB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	private IUIManager _naniUiManager;

	[Token(Token = "0x17000137")]
	private GigaCreation.Essentials.Input.IInputManager InputManager
	{
		[Token(Token = "0x6000759")]
		[Address(RVA = "0x3A7BD0", Offset = "0x3A65D0", VA = "0x1803A7BD0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000138")]
	private IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
	{
		[Token(Token = "0x600075A")]
		[Address(RVA = "0x3A7C90", Offset = "0x3A6690", VA = "0x1803A7C90")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000139")]
	private ICustomVariableManager NaniVarManager
	{
		[Token(Token = "0x600075B")]
		[Address(RVA = "0x3A7D50", Offset = "0x3A6750", VA = "0x1803A7D50")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700013A")]
	private IUIManager NaniUiManager
	{
		[Token(Token = "0x600075C")]
		[Address(RVA = "0x3A7E10", Offset = "0x3A6810", VA = "0x1803A7E10")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600075D")]
	[Address(RVA = "0x3A7EE0", Offset = "0x3A68E0", VA = "0x1803A7EE0")]
	private void Start()
	{
	}

	[Token(Token = "0x600075E")]
	[Address(RVA = "0x3A83D0", Offset = "0x3A6DD0", VA = "0x1803A83D0")]
	[AsyncStateMachine(typeof(_003CGoToDebugHomeAsync_003Ed__24))]
	private Cysharp.Threading.Tasks.UniTask GoToDebugHomeAsync([Optional] CancellationToken ct)
	{
		return default(Cysharp.Threading.Tasks.UniTask);
	}

	[Token(Token = "0x600075F")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugCommandButtons()
	{
	}
}
