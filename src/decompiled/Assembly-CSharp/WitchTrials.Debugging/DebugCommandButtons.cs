using GigaCreation.NaninovelExtender.Ui;
using GigaCreation.Tools.Debugging.General;
using Naninovel.Commands;
using Naninovel.UI;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Input;
using GigaCreation.Tools.Debugging.Ui;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using Naninovel;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using WitchTrials.Models;
using WitchTrials.Views;

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
		// RECONSTRUCTED | Confidence: A | RVA: 0x3A7BD0 (same lazily-cached
		// ServiceLocator pattern as OptionsUi.InputManager)
		[Token(Token = "0x6000759")]
		[Address(RVA = "0x3A7BD0", Offset = "0x3A65D0", VA = "0x1803A7BD0")]
		get
		{
			return _inputManager ??= ServiceLocator.Get<GigaCreation.Essentials.Input.IInputManager>(includeDerivedType: true);
		}
	}

	[Token(Token = "0x17000138")]
	private IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x3A7C90, 186 B
		// Lazy cache in +0x70 via `ServiceLocator.Get<IWitchTrialsNaninovelManager>(true)`.
		[Token(Token = "0x600075A")]
		[Address(RVA = "0x3A7C90", Offset = "0x3A6690", VA = "0x1803A7C90")]
		get
		{
			return _witchTrialsNaninovelManager ??= GigaCreation.Tools.Service.ServiceLocator.Get<IWitchTrialsNaninovelManager>(true);
		}
	}

	[Token(Token = "0x17000139")]
	private ICustomVariableManager NaniVarManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x3A7D50, 186 B
		// Lazy cache in +0x78 via `Engine.GetServiceOrErr<ICustomVariableManager>()`.
		[Token(Token = "0x600075B")]
		[Address(RVA = "0x3A7D50", Offset = "0x3A6750", VA = "0x1803A7D50")]
		get
		{
			return _naniVarManager ??= Engine.GetServiceOrErr<ICustomVariableManager>();
		}
	}

	[Token(Token = "0x1700013A")]
	private IUIManager NaniUiManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x3A7E10, 202 B
		// Lazy cache in +0x80 via `Engine.GetServiceOrErr<IUIManager>()`. This is the same shape as the 186 B
		// getters; the extra 16 bytes come only from the +0x80 displacement encoding.
		[Token(Token = "0x600075C")]
		[Address(RVA = "0x3A7E10", Offset = "0x3A6810", VA = "0x1803A7E10")]
		get
		{
			return _naniUiManager ??= Engine.GetServiceOrErr<IUIManager>();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3A7EE0, 1260 B; <>c__DisplayClass23_0 lambdas b__0 0x3A8580,
	// b__1 0x3A8600, b__2 0x3A8680, b__3 0x3A8700, b__4 0x3A8940, b__5 0x3A8A50, b__6 0x3A8B60
	// 
	// - The DisplayClass holds `this` (+0x10) and `timeDebugCommands` (+0x18) =
	//   `Object.FindAnyObjectByType<TimeDebugCommands>()`.
	// - Each of the seven buttons (+0x28..+0x58, in field order) does
	//   `onClick (+0x100).AsObservable().Subscribe(Action<Unit>).AddTo<IDisposable>(this)` (UniRx).
	// - Every lambda first calls IInputManager slot 0 `PlayResponseSfx(ResponseKind.Submit)` (r9d = 1) through
	//   the InputManager getter.
	// - Time buttons: `TogglePause()`, `SpeedUp(0.2f)`, `SlowDown(0.2f)` (rdata 0x2AE2670 = 0.2).
	// - Debug home: DebugPanel vtable +0x188 = slot 5 `HideContent()`, then
	//   `GoToDebugHomeAsync(destroyCancellationToken)` (builder stub inlined) and Cysharp `Forget()`.
	// - Two toggles: `TryGetVariableValue<bool>` and, only on success, `TrySetVariableValue<bool>(name, !value)`
	//   (`sete`).
	// - Unlock stills: `TryGetUI<IGalleryUi>`, an exact class compare (GalleryUi is sealed), and
	//   GalleryUi.UnlockAllStills (0x350380) inlined.
	[Token(Token = "0x600075D")]
	[Address(RVA = "0x3A7EE0", Offset = "0x3A68E0", VA = "0x1803A7EE0")]
	private void Start()
	{
		TimeDebugCommands timeDebugCommands = Object.FindAnyObjectByType<TimeDebugCommands>();
		_togglePauseButton.onClick.AsObservable().Subscribe(delegate
		{
			InputManager.PlayResponseSfx(ResponseKind.Submit);
			timeDebugCommands.TogglePause();
		}).AddTo(this);
		_speedUpButton.onClick.AsObservable().Subscribe(delegate
		{
			InputManager.PlayResponseSfx(ResponseKind.Submit);
			timeDebugCommands.SpeedUp(0.2f);
		}).AddTo(this);
		_slowDownButton.onClick.AsObservable().Subscribe(delegate
		{
			InputManager.PlayResponseSfx(ResponseKind.Submit);
			timeDebugCommands.SlowDown(0.2f);
		}).AddTo(this);
		_debugHomeButton.onClick.AsObservable().Subscribe(delegate
		{
			InputManager.PlayResponseSfx(ResponseKind.Submit);
			_debugPanel.HideContent();
			GoToDebugHomeAsync(destroyCancellationToken).Forget();
		}).AddTo(this);
		_toggleDebugSkipButton.onClick.AsObservable().Subscribe(delegate
		{
			InputManager.PlayResponseSfx(ResponseKind.Submit);
			if (NaniVarManager.TryGetVariableValue(EnableDebugSkipVarName, out bool enabled))
			{
				NaniVarManager.TrySetVariableValue(EnableDebugSkipVarName, !enabled);
			}
		}).AddTo(this);
		_toggleAllowContinueDebateButton.onClick.AsObservable().Subscribe(delegate
		{
			InputManager.PlayResponseSfx(ResponseKind.Submit);
			if (NaniVarManager.TryGetVariableValue(AllowContinueDebateVarName, out bool allowed))
			{
				NaniVarManager.TrySetVariableValue(AllowContinueDebateVarName, !allowed);
			}
		}).AddTo(this);
		_unlockAllStillsButton.onClick.AsObservable().Subscribe(delegate
		{
			InputManager.PlayResponseSfx(ResponseKind.Submit);
			if (NaniUiManager.TryGetUI(out IGalleryUi galleryUi) && galleryUi is GalleryUi gallery)
			{
				gallery.UnlockAllStills();
			}
		}).AddTo(this);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3A83D0 (builder stub); MoveNext 0x3A8E60, 1904 B
	// 
	// - `TryGetUI<ITitleUI>` (inlined, null-safe on the manager), then IManagedUI slot 2 `get_Visible`.
	// - If visible: `ManagedUiExtensions.DismissAsync(ui, null, ct)` (CancellationToken overload 0xD39FB0),
	//   awaited as Cysharp.
	// - The inlined WitchTrialsNaninovelManager getter (+0x70), then slot 3 `SetTitleScreenShown(false)`.
	// - `new Goto()` (Command ctor only), whose +0x30 `Path` =
	//   `new NamedString(_debugHomeScenario (+0x60), null)` via the NamedStringParameter implicit.
	// - Command vtable +0x188 = slot 5 `Execute(default)`, awaited as Naninovel.UniTask.
	[Token(Token = "0x600075E")]
	[Address(RVA = "0x3A83D0", Offset = "0x3A6DD0", VA = "0x1803A83D0")]
	[AsyncStateMachine(typeof(_003CGoToDebugHomeAsync_003Ed__24))]
	private async Cysharp.Threading.Tasks.UniTask GoToDebugHomeAsync([Optional] CancellationToken ct)
	{
		if (NaniUiManager.TryGetUI(out ITitleUI titleUi) && titleUi.Visible)
		{
			await titleUi.DismissAsync(null, ct);
		}
		WitchTrialsNaninovelManager.SetTitleScreenShown(false);
		Goto gotoCommand = new Goto
		{
			Path = new NamedString(_debugHomeScenario, null)
		};
		await gotoCommand.Execute();
	}

	[Token(Token = "0x600075F")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugCommandButtons()
	{
	}
}
