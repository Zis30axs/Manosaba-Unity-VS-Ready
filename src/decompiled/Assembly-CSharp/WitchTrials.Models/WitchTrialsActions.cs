using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace WitchTrials.Models;

[Token(Token = "0x2000166")]
public class WitchTrialsActions : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	// RECONSTRUCTED | Confidence: B | offset 0x10
	// This class is Unity Input System's generated wrapper for a .inputactions
	// asset (confirm via the nested-struct `m_Wrapper` back-reference pattern
	// below, identical to Unity's own C# code generator output) -- not
	// hand-written game logic. `m_Asset` is Unity's own conventional name for
	// this field in that generated pattern; dump.cs only shows the anonymous
	// `<asset>k__BackingField`, so the specific name is inferred from the known
	// generator template rather than confirmed byte-for-byte (hence B, not A).
	private InputActionAsset m_Asset;

	[Token(Token = "0x2000167")]
	public struct NaninovelActions
	{
		[Token(Token = "0x40006E3")]
		[FieldOffset(Offset = "0x0")]
		private WitchTrialsActions m_Wrapper;

		[Token(Token = "0x170000E5")]
		public InputAction Submit
		{
			[Token(Token = "0x60005E6")]
			[Address(RVA = "0x391310", Offset = "0x38FD10", VA = "0x180391310")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000E6")]
		public InputAction Cancel
		{
			[Token(Token = "0x60005E7")]
			[Address(RVA = "0x391330", Offset = "0x38FD30", VA = "0x180391330")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000E7")]
		public InputAction NavigateX
		{
			[Token(Token = "0x60005E8")]
			[Address(RVA = "0x391350", Offset = "0x38FD50", VA = "0x180391350")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000E8")]
		public InputAction NavigateY
		{
			[Token(Token = "0x60005E9")]
			[Address(RVA = "0x391370", Offset = "0x38FD70", VA = "0x180391370")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000E9")]
		public InputAction ScrollY
		{
			[Token(Token = "0x60005EA")]
			[Address(RVA = "0x391390", Offset = "0x38FD90", VA = "0x180391390")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000EA")]
		public InputAction Continue
		{
			[Token(Token = "0x60005EB")]
			[Address(RVA = "0x3913B0", Offset = "0x38FDB0", VA = "0x1803913B0")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000EB")]
		public InputAction Pause
		{
			[Token(Token = "0x60005EC")]
			[Address(RVA = "0x3913D0", Offset = "0x38FDD0", VA = "0x1803913D0")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000EC")]
		public InputAction Skip
		{
			[Token(Token = "0x60005ED")]
			[Address(RVA = "0x3913F0", Offset = "0x38FDF0", VA = "0x1803913F0")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000ED")]
		public InputAction ToggleSkip
		{
			[Token(Token = "0x60005EE")]
			[Address(RVA = "0x391410", Offset = "0x38FE10", VA = "0x180391410")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000EE")]
		public InputAction SkipMovie
		{
			[Token(Token = "0x60005EF")]
			[Address(RVA = "0x391430", Offset = "0x38FE30", VA = "0x180391430")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000EF")]
		public InputAction AutoPlay
		{
			[Token(Token = "0x60005F0")]
			[Address(RVA = "0x391450", Offset = "0x38FE50", VA = "0x180391450")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000F0")]
		public InputAction ToggleUI
		{
			[Token(Token = "0x60005F1")]
			[Address(RVA = "0x391470", Offset = "0x38FE70", VA = "0x180391470")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000F1")]
		public InputAction ShowBacklog
		{
			[Token(Token = "0x60005F2")]
			[Address(RVA = "0x391490", Offset = "0x38FE90", VA = "0x180391490")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000F2")]
		public InputAction Rollback
		{
			[Token(Token = "0x60005F3")]
			[Address(RVA = "0x3914B0", Offset = "0x38FEB0", VA = "0x1803914B0")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000F3")]
		public bool enabled
		{
			[Token(Token = "0x60005F7")]
			[Address(RVA = "0x3915B0", Offset = "0x38FFB0", VA = "0x1803915B0")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x60005E5")]
		[Address(RVA = "0x3912B0", Offset = "0x38FCB0", VA = "0x1803912B0")]
		public NaninovelActions(WitchTrialsActions wrapper)
		{
		}

		[Token(Token = "0x60005F4")]
		[Address(RVA = "0x3914D0", Offset = "0x38FED0", VA = "0x1803914D0")]
		public InputActionMap Get()
		{
			return null;
		}

		[Token(Token = "0x60005F5")]
		[Address(RVA = "0x3914F0", Offset = "0x38FEF0", VA = "0x1803914F0")]
		public void Enable()
		{
		}

		[Token(Token = "0x60005F6")]
		[Address(RVA = "0x391570", Offset = "0x38FF70", VA = "0x180391570")]
		public void Disable()
		{
		}

		[Token(Token = "0x60005F8")]
		[Address(RVA = "0x3915E0", Offset = "0x38FFE0", VA = "0x1803915E0")]
		public static implicit operator InputActionMap(NaninovelActions set)
		{
			return null;
		}

		[Token(Token = "0x60005F9")]
		[Address(RVA = "0x391600", Offset = "0x390000", VA = "0x180391600")]
		public void AddCallbacks(INaninovelActions instance)
		{
		}

		[Token(Token = "0x60005FA")]
		[Address(RVA = "0x392CA0", Offset = "0x3916A0", VA = "0x180392CA0")]
		private void UnregisterCallbacks(INaninovelActions instance)
		{
		}

		[Token(Token = "0x60005FB")]
		[Address(RVA = "0x395220", Offset = "0x393C20", VA = "0x180395220")]
		public void RemoveCallbacks(INaninovelActions instance)
		{
		}

		[Token(Token = "0x60005FC")]
		[Address(RVA = "0x3952A0", Offset = "0x393CA0", VA = "0x1803952A0")]
		public void SetCallbacks(INaninovelActions instance)
		{
		}
	}

	[Token(Token = "0x2000168")]
	public struct UIActions
	{
		[Token(Token = "0x40006E4")]
		[FieldOffset(Offset = "0x0")]
		private WitchTrialsActions m_Wrapper;

		[Token(Token = "0x170000F4")]
		public InputAction Navigate
		{
			[Token(Token = "0x60005FE")]
			[Address(RVA = "0x395470", Offset = "0x393E70", VA = "0x180395470")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000F5")]
		public InputAction Submit
		{
			[Token(Token = "0x60005FF")]
			[Address(RVA = "0x395490", Offset = "0x393E90", VA = "0x180395490")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000F6")]
		public InputAction Cancel
		{
			[Token(Token = "0x6000600")]
			[Address(RVA = "0x3954B0", Offset = "0x393EB0", VA = "0x1803954B0")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000F7")]
		public InputAction Point
		{
			[Token(Token = "0x6000601")]
			[Address(RVA = "0x3954D0", Offset = "0x393ED0", VA = "0x1803954D0")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000F8")]
		public InputAction Click
		{
			[Token(Token = "0x6000602")]
			[Address(RVA = "0x3954F0", Offset = "0x393EF0", VA = "0x1803954F0")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000F9")]
		public InputAction RightClick
		{
			[Token(Token = "0x6000603")]
			[Address(RVA = "0x395510", Offset = "0x393F10", VA = "0x180395510")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000FA")]
		public InputAction MiddleClick
		{
			[Token(Token = "0x6000604")]
			[Address(RVA = "0x395530", Offset = "0x393F30", VA = "0x180395530")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000FB")]
		public InputAction ScrollWheel
		{
			[Token(Token = "0x6000605")]
			[Address(RVA = "0x395550", Offset = "0x393F50", VA = "0x180395550")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170000FC")]
		public bool enabled
		{
			[Token(Token = "0x6000609")]
			[Address(RVA = "0x395650", Offset = "0x394050", VA = "0x180395650")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x60005FD")]
		[Address(RVA = "0x3912B0", Offset = "0x38FCB0", VA = "0x1803912B0")]
		public UIActions(WitchTrialsActions wrapper)
		{
		}

		[Token(Token = "0x6000606")]
		[Address(RVA = "0x395570", Offset = "0x393F70", VA = "0x180395570")]
		public InputActionMap Get()
		{
			return null;
		}

		[Token(Token = "0x6000607")]
		[Address(RVA = "0x395590", Offset = "0x393F90", VA = "0x180395590")]
		public void Enable()
		{
		}

		[Token(Token = "0x6000608")]
		[Address(RVA = "0x395610", Offset = "0x394010", VA = "0x180395610")]
		public void Disable()
		{
		}

		[Token(Token = "0x600060A")]
		[Address(RVA = "0x395680", Offset = "0x394080", VA = "0x180395680")]
		public static implicit operator InputActionMap(UIActions set)
		{
			return null;
		}

		[Token(Token = "0x600060B")]
		[Address(RVA = "0x3956A0", Offset = "0x3940A0", VA = "0x1803956A0")]
		public void AddCallbacks(IUIActions instance)
		{
		}

		[Token(Token = "0x600060C")]
		[Address(RVA = "0x396430", Offset = "0x394E30", VA = "0x180396430")]
		private void UnregisterCallbacks(IUIActions instance)
		{
		}

		[Token(Token = "0x600060D")]
		[Address(RVA = "0x3979D0", Offset = "0x3963D0", VA = "0x1803979D0")]
		public void RemoveCallbacks(IUIActions instance)
		{
		}

		[Token(Token = "0x600060E")]
		[Address(RVA = "0x397A50", Offset = "0x396450", VA = "0x180397A50")]
		public void SetCallbacks(IUIActions instance)
		{
		}
	}

	[Token(Token = "0x2000169")]
	public interface INaninovelActions
	{
		[Token(Token = "0x600060F")]
		void OnSubmit(InputAction.CallbackContext context);

		[Token(Token = "0x6000610")]
		void OnCancel(InputAction.CallbackContext context);

		[Token(Token = "0x6000611")]
		void OnNavigateX(InputAction.CallbackContext context);

		[Token(Token = "0x6000612")]
		void OnNavigateY(InputAction.CallbackContext context);

		[Token(Token = "0x6000613")]
		void OnScrollY(InputAction.CallbackContext context);

		[Token(Token = "0x6000614")]
		void OnContinue(InputAction.CallbackContext context);

		[Token(Token = "0x6000615")]
		void OnPause(InputAction.CallbackContext context);

		[Token(Token = "0x6000616")]
		void OnSkip(InputAction.CallbackContext context);

		[Token(Token = "0x6000617")]
		void OnToggleSkip(InputAction.CallbackContext context);

		[Token(Token = "0x6000618")]
		void OnSkipMovie(InputAction.CallbackContext context);

		[Token(Token = "0x6000619")]
		void OnAutoPlay(InputAction.CallbackContext context);

		[Token(Token = "0x600061A")]
		void OnToggleUI(InputAction.CallbackContext context);

		[Token(Token = "0x600061B")]
		void OnShowBacklog(InputAction.CallbackContext context);

		[Token(Token = "0x600061C")]
		void OnRollback(InputAction.CallbackContext context);
	}

	[Token(Token = "0x200016A")]
	public interface IUIActions
	{
		[Token(Token = "0x600061D")]
		void OnNavigate(InputAction.CallbackContext context);

		[Token(Token = "0x600061E")]
		void OnSubmit(InputAction.CallbackContext context);

		[Token(Token = "0x600061F")]
		void OnCancel(InputAction.CallbackContext context);

		[Token(Token = "0x6000620")]
		void OnPoint(InputAction.CallbackContext context);

		[Token(Token = "0x6000621")]
		void OnClick(InputAction.CallbackContext context);

		[Token(Token = "0x6000622")]
		void OnRightClick(InputAction.CallbackContext context);

		[Token(Token = "0x6000623")]
		void OnMiddleClick(InputAction.CallbackContext context);

		[Token(Token = "0x6000624")]
		void OnScrollWheel(InputAction.CallbackContext context);
	}

	[Token(Token = "0x40006C4")]
	[FieldOffset(Offset = "0x18")]
	private readonly InputActionMap m_Naninovel;

	[Token(Token = "0x40006C5")]
	[FieldOffset(Offset = "0x20")]
	// RECONSTRUCTED | Confidence: A | RVA: 0x38FC50 (field initializer, allocated first in the ctor)
	private List<INaninovelActions> m_NaninovelActionsCallbackInterfaces = new List<INaninovelActions>();

	[Token(Token = "0x40006C6")]
	[FieldOffset(Offset = "0x28")]
	private readonly InputAction m_Naninovel_Submit;

	[Token(Token = "0x40006C7")]
	[FieldOffset(Offset = "0x30")]
	private readonly InputAction m_Naninovel_Cancel;

	[Token(Token = "0x40006C8")]
	[FieldOffset(Offset = "0x38")]
	private readonly InputAction m_Naninovel_NavigateX;

	[Token(Token = "0x40006C9")]
	[FieldOffset(Offset = "0x40")]
	private readonly InputAction m_Naninovel_NavigateY;

	[Token(Token = "0x40006CA")]
	[FieldOffset(Offset = "0x48")]
	private readonly InputAction m_Naninovel_ScrollY;

	[Token(Token = "0x40006CB")]
	[FieldOffset(Offset = "0x50")]
	private readonly InputAction m_Naninovel_Continue;

	[Token(Token = "0x40006CC")]
	[FieldOffset(Offset = "0x58")]
	private readonly InputAction m_Naninovel_Pause;

	[Token(Token = "0x40006CD")]
	[FieldOffset(Offset = "0x60")]
	private readonly InputAction m_Naninovel_Skip;

	[Token(Token = "0x40006CE")]
	[FieldOffset(Offset = "0x68")]
	private readonly InputAction m_Naninovel_ToggleSkip;

	[Token(Token = "0x40006CF")]
	[FieldOffset(Offset = "0x70")]
	private readonly InputAction m_Naninovel_SkipMovie;

	[Token(Token = "0x40006D0")]
	[FieldOffset(Offset = "0x78")]
	private readonly InputAction m_Naninovel_AutoPlay;

	[Token(Token = "0x40006D1")]
	[FieldOffset(Offset = "0x80")]
	private readonly InputAction m_Naninovel_ToggleUI;

	[Token(Token = "0x40006D2")]
	[FieldOffset(Offset = "0x88")]
	private readonly InputAction m_Naninovel_ShowBacklog;

	[Token(Token = "0x40006D3")]
	[FieldOffset(Offset = "0x90")]
	private readonly InputAction m_Naninovel_Rollback;

	[Token(Token = "0x40006D4")]
	[FieldOffset(Offset = "0x98")]
	private readonly InputActionMap m_UI;

	[Token(Token = "0x40006D5")]
	[FieldOffset(Offset = "0xA0")]
	// RECONSTRUCTED | Confidence: A | RVA: 0x38FC50 (field initializer)
	private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();

	[Token(Token = "0x40006D6")]
	[FieldOffset(Offset = "0xA8")]
	private readonly InputAction m_UI_Navigate;

	[Token(Token = "0x40006D7")]
	[FieldOffset(Offset = "0xB0")]
	private readonly InputAction m_UI_Submit;

	[Token(Token = "0x40006D8")]
	[FieldOffset(Offset = "0xB8")]
	private readonly InputAction m_UI_Cancel;

	[Token(Token = "0x40006D9")]
	[FieldOffset(Offset = "0xC0")]
	private readonly InputAction m_UI_Point;

	[Token(Token = "0x40006DA")]
	[FieldOffset(Offset = "0xC8")]
	private readonly InputAction m_UI_Click;

	[Token(Token = "0x40006DB")]
	[FieldOffset(Offset = "0xD0")]
	private readonly InputAction m_UI_RightClick;

	[Token(Token = "0x40006DC")]
	[FieldOffset(Offset = "0xD8")]
	private readonly InputAction m_UI_MiddleClick;

	[Token(Token = "0x40006DD")]
	[FieldOffset(Offset = "0xE0")]
	private readonly InputAction m_UI_ScrollWheel;

	[Token(Token = "0x40006DE")]
	[FieldOffset(Offset = "0xE8")]
	// RECONSTRUCTED | Confidence: A | RVA: 0x38FC50 (field initializer; the five indices are stored as
	// two qword -1 writes at +0xE8/+0xF0 plus one dword -1 at +0xF8)
	private int m_KeyboardMouseSchemeIndex = -1;

	[Token(Token = "0x40006DF")]
	[FieldOffset(Offset = "0xEC")]
	// RECONSTRUCTED | Confidence: A | RVA: 0x38FC50 (field initializer)
	private int m_GamepadSchemeIndex = -1;

	[Token(Token = "0x40006E0")]
	[FieldOffset(Offset = "0xF0")]
	// RECONSTRUCTED | Confidence: A | RVA: 0x38FC50 (field initializer)
	private int m_TouchSchemeIndex = -1;

	[Token(Token = "0x40006E1")]
	[FieldOffset(Offset = "0xF4")]
	// RECONSTRUCTED | Confidence: A | RVA: 0x38FC50 (field initializer)
	private int m_JoystickSchemeIndex = -1;

	[Token(Token = "0x40006E2")]
	[FieldOffset(Offset = "0xF8")]
	// RECONSTRUCTED | Confidence: A | RVA: 0x38FC50 (field initializer)
	private int m_XRSchemeIndex = -1;

	[Token(Token = "0x170000D9")]
	public InputActionAsset asset
	{
		// RECONSTRUCTED | Confidence: A (body) / B (field name -- see above)
		// RVA: 0x33FBC0 (shared generic reference-field getter, +0x10)
		[Token(Token = "0x60005CD")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return m_Asset;
		}
	}

	[Token(Token = "0x170000DA")]
	public InputBinding? bindingMask
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x390B60, 76 B
		// Copies InputActionAsset +0x38 (bindingMask, inlined getter) from `asset` (+0x10); folded with UnityEngine.InputSystem.DefaultInputActions (same generated wrapper code).
		[Token(Token = "0x60005D1")]
		[Address(RVA = "0x390B60", Offset = "0x38F560", VA = "0x180390B60", Slot = "7")]
		get
		{
			return asset.bindingMask;
		}
		// RECONSTRUCTED | Confidence: A | RVA: 0x390BB0, 91 B
		// InputActionAsset.set_bindingMask(value); folded with UnityEngine.InputSystem.DefaultInputActions (same generated wrapper code).
		[Token(Token = "0x60005D2")]
		[Address(RVA = "0x390BB0", Offset = "0x38F5B0", VA = "0x180390BB0", Slot = "8")]
		set
		{
			asset.bindingMask = value;
		}
	}

	[Token(Token = "0x170000DB")]
	public ReadOnlyArray<InputDevice>? devices
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x390C10, 68 B
		// InputActionMap.DeviceArray.Get on asset +0xA8 (inlined devices getter); folded with UnityEngine.InputSystem.DefaultInputActions (same generated wrapper code).
		[Token(Token = "0x60005D3")]
		[Address(RVA = "0x390C10", Offset = "0x38F610", VA = "0x180390C10", Slot = "9")]
		get
		{
			return asset.devices;
		}
		// RECONSTRUCTED | Confidence: A | RVA: 0x390C60, 121 B
		// DeviceArray.Set then LazyResolveBindings on the first map (inlined devices setter); folded with UnityEngine.InputSystem.DefaultInputActions (same generated wrapper code).
		[Token(Token = "0x60005D4")]
		[Address(RVA = "0x390C60", Offset = "0x38F660", VA = "0x180390C60", Slot = "10")]
		set
		{
			asset.devices = value;
		}
	}

	[Token(Token = "0x170000DC")]
	public ReadOnlyArray<InputControlScheme> controlSchemes
	{
		[Token(Token = "0x60005D5")]
		[Address(RVA = "0x390CE0", Offset = "0x38F6E0", VA = "0x180390CE0", Slot = "11")]
		// RECONSTRUCTED | Confidence: A | generated wrapper: asset.controlSchemes; folded with UnityEngine.InputSystem.DefaultInputActions (same generated wrapper code)
		get
		{
			return asset.controlSchemes;
		}
	}

	[Token(Token = "0x170000DD")]
	public IEnumerable<InputBinding> bindings
	{
		[Token(Token = "0x60005DB")]
		[Address(RVA = "0x390DA0", Offset = "0x38F7A0", VA = "0x180390DA0", Slot = "4")]
		// RECONSTRUCTED | Confidence: A | generated wrapper: asset.bindings; folded with UnityEngine.InputSystem.DefaultInputActions (same generated wrapper code)
		get
		{
			return asset.bindings;
		}
	}

	[Token(Token = "0x170000DE")]
	public NaninovelActions Naninovel
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x390E40, 91 B
		// "Wrap `this` in the accessor struct" -- one body shared by get_Naninovel and get_UI below, and ICF-folded
		// with 30+ unrelated single-field wrappers (Task.GetAwaiter, DefaultInputActions.get_Player, ParticleSystem
		// module getters); the struct constructor is the reconstructed one at 0x3912B0.
		[Token(Token = "0x60005DE")]
		[Address(RVA = "0x390E40", Offset = "0x38F840", VA = "0x180390E40")]
		get
		{
			return new NaninovelActions(this);
		}
	}

	[Token(Token = "0x170000DF")]
	public UIActions UI
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x390E40, 91 B (same shared body as get_Naninovel above)
		[Token(Token = "0x60005DF")]
		[Address(RVA = "0x390E40", Offset = "0x38F840", VA = "0x180390E40")]
		get
		{
			return new UIActions(this);
		}
	}

	[Token(Token = "0x170000E0")]
	public InputControlScheme KeyboardMouseScheme
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x390EA0, 201 B
		// +0xE8 == -1 -> FindControlSchemeIndex("Keyboard&Mouse"); then controlSchemes[index].
		[Token(Token = "0x60005E0")]
		[Address(RVA = "0x390EA0", Offset = "0x38F8A0", VA = "0x180390EA0")]
		get
		{
			if (m_KeyboardMouseSchemeIndex == -1)
			{
				m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
			}
			return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
		}
	}

	[Token(Token = "0x170000E1")]
	public InputControlScheme GamepadScheme
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x390F70, 201 B
		// +0xEC == -1 -> FindControlSchemeIndex("Gamepad"); then controlSchemes[index].
		[Token(Token = "0x60005E1")]
		[Address(RVA = "0x390F70", Offset = "0x38F970", VA = "0x180390F70")]
		get
		{
			if (m_GamepadSchemeIndex == -1)
			{
				m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
			}
			return asset.controlSchemes[m_GamepadSchemeIndex];
		}
	}

	[Token(Token = "0x170000E2")]
	public InputControlScheme TouchScheme
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x391040, 201 B
		// +0xF0 == -1 -> FindControlSchemeIndex("Touch"); then controlSchemes[index].
		[Token(Token = "0x60005E2")]
		[Address(RVA = "0x391040", Offset = "0x38FA40", VA = "0x180391040")]
		get
		{
			if (m_TouchSchemeIndex == -1)
			{
				m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
			}
			return asset.controlSchemes[m_TouchSchemeIndex];
		}
	}

	[Token(Token = "0x170000E3")]
	public InputControlScheme JoystickScheme
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x391110, 201 B
		// +0xF4 == -1 -> FindControlSchemeIndex("Joystick"); then controlSchemes[index].
		[Token(Token = "0x60005E3")]
		[Address(RVA = "0x391110", Offset = "0x38FB10", VA = "0x180391110")]
		get
		{
			if (m_JoystickSchemeIndex == -1)
			{
				m_JoystickSchemeIndex = asset.FindControlSchemeIndex("Joystick");
			}
			return asset.controlSchemes[m_JoystickSchemeIndex];
		}
	}

	[Token(Token = "0x170000E4")]
	public InputControlScheme XRScheme
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x3911E0, 201 B
		// +0xF8 == -1 -> FindControlSchemeIndex("XR"); then controlSchemes[index].
		[Token(Token = "0x60005E4")]
		[Address(RVA = "0x3911E0", Offset = "0x38FBE0", VA = "0x1803911E0")]
		get
		{
			if (m_XRSchemeIndex == -1)
			{
				m_XRSchemeIndex = asset.FindControlSchemeIndex("XR");
			}
			return asset.controlSchemes[m_XRSchemeIndex];
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x38FC50, 3,678 B
	// 
	// Unity Input System's generated wrapper constructor. The asset JSON below is the metadata string literal at
	// 0x38FF4B -- 46,682 characters over 1,194 LF-separated lines (the disassembler prints it escaped, 47,875
	// characters) -- passed to InputActionAsset.FromJson. It parses as JSON and describes two maps (Naninovel:
	// 14 actions / 61 bindings, UI: 8 / 22) and five control schemes: Keyboard&Mouse, Gamepad, Touch, Joystick, XR.
	// Every lookup passes `throwIfNotFound: true` (r8b = 1). The two callback lists and the five scheme indices are
	// field initializers, which is why their stores precede FromJson in the byte order.
	[Token(Token = "0x60005CE")]
	[Address(RVA = "0x38FC50", Offset = "0x38E650", VA = "0x18038FC50")]
	public WitchTrialsActions()
	{
		m_Asset = InputActionAsset.FromJson(@"{
    ""name"": ""WitchTrialsActions"",
    ""maps"": [
        {
            ""name"": ""Naninovel"",
            ""id"": ""df70fa95-8a34-4494-b137-73ab6b9c7d37"",
            ""actions"": [
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""641cd816-40e6-41b4-8c3d-04687c349290"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""b7230bb6-fc9b-4f52-8b25-f5e19cb2c2ba"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NavigateX"",
                    ""type"": ""Value"",
                    ""id"": ""351f2ccd-1f9f-44bf-9bec-d62ac5c5f408"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""NavigateY"",
                    ""type"": ""Value"",
                    ""id"": ""6b444451-8a00-4d00-a97e-f47457f736a8"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ScrollY"",
                    ""type"": ""Value"",
                    ""id"": ""a48d65e9-58b1-4440-9687-364ff33632b0"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Continue"",
                    ""type"": ""Value"",
                    ""id"": ""852140f2-7766-474d-8707-702459ba45f3"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""6c2ab1b8-8984-453a-af3d-a3c78ae1679a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skip"",
                    ""type"": ""Button"",
                    ""id"": ""27c5f898-bc57-4ee1-8800-db469aca5fe3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleSkip"",
                    ""type"": ""Button"",
                    ""id"": ""f1ba0d36-48eb-4cd5-b651-1c94a6531f70"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SkipMovie"",
                    ""type"": ""Button"",
                    ""id"": ""4a4e79a2-0e3f-435d-8075-770846e8f3d5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AutoPlay"",
                    ""type"": ""Button"",
                    ""id"": ""2776c80d-3c14-4091-8c56-d04ced07a2b0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleUI"",
                    ""type"": ""Button"",
                    ""id"": ""19d5562a-c022-484f-85a9-642047967b68"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ShowBacklog"",
                    ""type"": ""Value"",
                    ""id"": ""fc60fed8-c183-4713-bae2-dfe8693f721f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Rollback"",
                    ""type"": ""Button"",
                    ""id"": ""b5d14201-9478-4870-afc4-313a94aaa18e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""129682f6-ed33-4fb7-af61-e4b9829a9c6b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NavigateX"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0664409b-fd61-4d45-9cec-f9c592862132"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""NavigateX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d3c22900-8273-4270-b05a-4d085e79f192"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""NavigateX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""978bfe49-cc26-4a3d-ab7b-7d7a29327403"",
                    ""path"": ""<Gamepad>/dpad/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""NavigateX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7b07b447-1892-4852-9437-156c08e0cb87"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""NavigateX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""143bb1cd-cc10-4eca-a2f0-a3664166fe91"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05f6913d-c316-48b2-a6bb-e225f14c7960"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""886e731e-7071-4ae4-95c0-e61739dad6fd"",
                    ""path"": ""<SwitchProControllerHID>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee3d0cd2-254e-47a7-a8cb-bc94d9658c54"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37b034fb-5942-40e0-b4cb-bd25e88fefb9"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbac6039-9c09-46a1-b5f2-4e5124ccb5ed"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e15ca19d-e649-4852-97d5-7fe8ccc44e94"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b58ed29-68d8-4e9c-a1ad-f2f4db6b7800"",
                    ""path"": ""<SwitchProControllerHID>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ea81e01-b96d-4db4-a0e5-1e29ce9248e7"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f2e9ba44-c423-42a7-ad56-f20975884794"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91a89f76-5188-4be6-bb33-96da50e134da"",
                    ""path"": ""<Keyboard>/numpadEnter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cbb2f4b-a784-49cc-8d5e-c010b8c7f4e6"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8bf24bf-3f2f-4160-a97c-38ec1eb520ba"",
                    ""path"": ""<SwitchProControllerHID>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d77451f6-7585-4906-8014-27636f9f9363"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""daba33a1-ad0c-4742-a909-43ad1cdfbeb6"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ToggleSkip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""25060bbd-a3a6-476e-8fba-45ae484aad05"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""AutoPlay"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1dd3301a-c1b3-42e6-bd95-02ac3f5b11ce"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""ToggleUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3582c9a2-ebb8-4e68-b69b-9f4b2a30d639"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""ToggleUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0ab5810-0ab5-446f-8280-3bf9f428caa8"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ToggleUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2239461-6e6b-4f74-8bb0-68c1fc5725a3"",
                    ""path"": ""<SwitchProControllerHID>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ToggleUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17ddd106-f704-4c2b-b353-49c57841c28c"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ToggleUI"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2784776c-1c99-4ad1-bfaf-b8cd0049da39"",
                    ""path"": ""<Mouse>/scroll/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""ShowBacklog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""499ea9ab-a859-44d4-8421-829357920e6e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""ShowBacklog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9cbd1150-4a64-42f6-8c78-d236adb1eb3f"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""ShowBacklog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""097ede95-d1ca-45a5-ac52-5502504b0d59"",
                    ""path"": ""<SwitchProControllerHID>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""ShowBacklog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f074ddac-d70d-4927-a6ad-1dcafa071da0"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""ShowBacklog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""934f3124-18bd-4e14-aeb9-3c5debf29583"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""ShowBacklog"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d180ec1a-79ee-443b-86bc-9bcbecf0cacc"",
                    ""path"": ""<Keyboard>/leftBracket"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Rollback"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""27cafef1-16e2-4462-a33f-404089627d00"",
                    ""path"": ""<Mouse>/scroll/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbf23dae-afcb-4a15-92f8-5095cde31f70"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5038a787-0042-4231-b51e-012950a92918"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c04ea5f-b012-41d1-a6f7-02e963b52893"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5221657-82dd-4c5d-91e3-0ce755d88262"",
                    ""path"": ""<Keyboard>/numpadEnter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e83dc8a1-f21a-48d8-9f56-dac0daf2aa3d"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd63e2ae-3478-4d6c-9ca2-970691db36a9"",
                    ""path"": ""<SwitchProControllerHID>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67eb456c-5e95-4909-9b6b-0a081ba251ea"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""391b4369-8553-4545-b015-16f657afc030"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4f4649ac-64a8-4a73-af11-b3faef356a4d"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36e52cba-0905-478e-a818-f4bfcb9f3b9a"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1fe7371b-f66b-458b-9260-93285193ab34"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ae7c91d-13f3-4322-9303-db7a1ff45798"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""SkipMovie"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""afdeb484-126b-4556-bd89-eaca3c58076a"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""SkipMovie"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b694a9a-27c2-4ee4-9201-c089236d80dc"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SkipMovie"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d251e767-4e04-4b19-940d-ab5d4a87b0a4"",
                    ""path"": ""<XInputController>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""SkipMovie"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""268156ce-1da5-4a88-bd0c-55bf68b218d8"",
                    ""path"": ""<SwitchProControllerHID>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SkipMovie"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c76860f-0854-48dd-8f6e-23b6d4e04c6a"",
                    ""path"": ""<SwitchProControllerHID>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""SkipMovie"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""500a755d-525b-46c8-b7b8-ae03683eacc8"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SkipMovie"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""efb56e2b-22f8-4911-9dd5-74c5c1409e69"",
                    ""path"": ""<DualShockGamepad>/touchpadButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""SkipMovie"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7149361-5cd3-41c5-8d41-b524a11a6929"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""SkipMovie"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ead31c64-2ab0-483f-977d-49af205880d1"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""SkipMovie"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc5136b9-9148-4667-b7b4-a06dff4d8709"",
                    ""path"": ""<Gamepad>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""ScrollY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""e6ab775b-d40d-49e0-b355-fdc7eda0a856"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NavigateY"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f71ac180-5e3e-42f8-8fe5-bcd3db075710"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""NavigateY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a324ab6a-f2f2-4680-af63-aedd5b6e76a6"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""NavigateY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a3103061-1793-46da-b87c-41a2eff68fda"",
                    ""path"": ""<Gamepad>/dpad/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""NavigateY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da70d416-4926-4fdb-89ff-28190692f441"",
                    ""path"": ""<Gamepad>/leftStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""NavigateY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""272f6d14-89ba-496f-b7ff-215263d3219f"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c95b2375-e6d9-4b88-9c4c-c5e76515df4b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""7607c7b6-cd76-4816-beef-bd0341cfe950"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""15cef263-9014-4fd5-94d9-4e4a6234a6ef"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""32b35790-4ed0-4e9a-aa41-69ac6d629449"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3c7022bf-7922-4f7c-a998-c437916075ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""44b200b1-1557-4083-816c-b22cbdf77ddf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MiddleClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""dad70c86-b58c-4b17-88ad-f5e53adf419e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0489e84a-4833-4c40-bfae-cea84b696689"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""ff527021-f211-4c02-933e-5976594c46ed"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""eb480147-c587-4a33-85ed-eb0ab9942c43"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""85d264ad-e0a0-4565-b7ff-1a37edde51ac"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cea9b045-a000-445b-95b8-0c171af70a3b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4cda81dc-9edd-4e03-9d7c-a71a14345d0b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fb8277d4-c5cd-4663-9dc7-ee3f0b506d90"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""809f371f-c5e2-4e7a-83a1-d867598f40dd"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""14a5d6e8-4aaf-4119-a9ef-34b8c2c548bf"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2db08d65-c5fb-421b-983f-c71163608d67"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8ba04515-75aa-45de-966d-393d9bbd1c14"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fcd248ae-a788-4676-a12e-f4d81205600b"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9e92bb26-7e3b-4ec4-b06b-3c8f8e498ddc"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;Gamepad;Touch;Joystick;XR"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82627dcc-3b13-4ba9-841d-e4b746d6553e"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;Gamepad;Touch;Joystick;XR"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c52c8e0b-8179-41d3-b8a1-d149033bbe86"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1394cbc-336e-44ce-9ea8-6007ed6193f7"",
                    ""path"": ""<Pen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5693e57a-238a-46ed-b5ae-e64e6e574302"",
                    ""path"": ""<Touchscreen>/touch*/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4faf7dc9-b979-4210-aa8c-e808e1ef89f5"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d66d5ba-88d7-48e6-b1cd-198bbfef7ace"",
                    ""path"": ""<Pen>/tip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47c2a644-3ebc-4dae-a106-589b7ca75b59"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38c99815-14ea-4617-8627-164d27641299"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c191405-5738-4d4b-a523-c6a301dbf754"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24066f69-da47-44f3-a07e-0015fb02eb2e"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MiddleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Joystick"",
            ""bindingGroup"": ""Joystick"",
            ""devices"": [
                {
                    ""devicePath"": ""<Joystick>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""XR"",
            ""bindingGroup"": ""XR"",
            ""devices"": [
                {
                    ""devicePath"": ""<XRController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");

		// Naninovel
		m_Naninovel = m_Asset.FindActionMap("Naninovel", throwIfNotFound: true);
		m_Naninovel_Submit = m_Naninovel.FindAction("Submit", throwIfNotFound: true);
		m_Naninovel_Cancel = m_Naninovel.FindAction("Cancel", throwIfNotFound: true);
		m_Naninovel_NavigateX = m_Naninovel.FindAction("NavigateX", throwIfNotFound: true);
		m_Naninovel_NavigateY = m_Naninovel.FindAction("NavigateY", throwIfNotFound: true);
		m_Naninovel_ScrollY = m_Naninovel.FindAction("ScrollY", throwIfNotFound: true);
		m_Naninovel_Continue = m_Naninovel.FindAction("Continue", throwIfNotFound: true);
		m_Naninovel_Pause = m_Naninovel.FindAction("Pause", throwIfNotFound: true);
		m_Naninovel_Skip = m_Naninovel.FindAction("Skip", throwIfNotFound: true);
		m_Naninovel_ToggleSkip = m_Naninovel.FindAction("ToggleSkip", throwIfNotFound: true);
		m_Naninovel_SkipMovie = m_Naninovel.FindAction("SkipMovie", throwIfNotFound: true);
		m_Naninovel_AutoPlay = m_Naninovel.FindAction("AutoPlay", throwIfNotFound: true);
		m_Naninovel_ToggleUI = m_Naninovel.FindAction("ToggleUI", throwIfNotFound: true);
		m_Naninovel_ShowBacklog = m_Naninovel.FindAction("ShowBacklog", throwIfNotFound: true);
		m_Naninovel_Rollback = m_Naninovel.FindAction("Rollback", throwIfNotFound: true);

		// UI
		m_UI = m_Asset.FindActionMap("UI", throwIfNotFound: true);
		m_UI_Navigate = m_UI.FindAction("Navigate", throwIfNotFound: true);
		m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
		m_UI_Cancel = m_UI.FindAction("Cancel", throwIfNotFound: true);
		m_UI_Point = m_UI.FindAction("Point", throwIfNotFound: true);
		m_UI_Click = m_UI.FindAction("Click", throwIfNotFound: true);
		m_UI_RightClick = m_UI.FindAction("RightClick", throwIfNotFound: true);
		m_UI_MiddleClick = m_UI.FindAction("MiddleClick", throwIfNotFound: true);
		m_UI_ScrollWheel = m_UI.FindAction("ScrollWheel", throwIfNotFound: true);
	}

	[Token(Token = "0x60005CF")]
	[Address(RVA = "0x390AB0", Offset = "0x38F4B0", VA = "0x180390AB0", Slot = "1")]
	~WitchTrialsActions()
	{
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x390AD0, 140 B (the generated wrapper destroys the asset it built)
	[Token(Token = "0x60005D0")]
	[Address(RVA = "0x390AD0", Offset = "0x38F4D0", VA = "0x180390AD0", Slot = "17")]
	public void Dispose()
	{
		UnityEngine.Object.Destroy(m_Asset);
	}

	[Token(Token = "0x60005D6")]
	[Address(RVA = "0x390D20", Offset = "0x38F720", VA = "0x180390D20", Slot = "12")]
	public bool Contains(InputAction action)
	{
		return asset.Contains(action);
	}

	[Token(Token = "0x60005D7")]
	[Address(RVA = "0x390D40", Offset = "0x38F740", VA = "0x180390D40", Slot = "15")]
	public IEnumerator<InputAction> GetEnumerator()
	{
		return asset.GetEnumerator();
	}

	[Token(Token = "0x60005D8")]
	[Address(RVA = "0x390D40", Offset = "0x38F740", VA = "0x180390D40", Slot = "16")]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	[Token(Token = "0x60005D9")]
	[Address(RVA = "0x390D60", Offset = "0x38F760", VA = "0x180390D60", Slot = "13")]
	public void Enable()
	{
		asset.Enable();
	}

	[Token(Token = "0x60005DA")]
	[Address(RVA = "0x390D80", Offset = "0x38F780", VA = "0x180390D80", Slot = "14")]
	public void Disable()
	{
		asset.Disable();
	}

	[Token(Token = "0x60005DC")]
	[Address(RVA = "0x390DC0", Offset = "0x38F7C0", VA = "0x180390DC0", Slot = "5")]
	public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
	{
		return asset.FindAction(actionNameOrId, throwIfNotFound);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x390DE0, 93 B
	// InputActionAsset.FindBinding(bindingMask, out action); folded with UnityEngine.InputSystem.DefaultInputActions (same generated wrapper code).
	[Token(Token = "0x60005DD")]
	[Address(RVA = "0x390DE0", Offset = "0x38F7E0", VA = "0x180390DE0", Slot = "6")]
	public int FindBinding(InputBinding bindingMask, out InputAction action)
	{
		return asset.FindBinding(bindingMask, out action);
	}
}
