using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI;

[Token(Token = "0x20005DB")]
public class VariableInputPanel : CustomUI, IVariableInputUI, IManagedUI
{
	[Serializable]
	[Token(Token = "0x20005DC")]
	public new class GameState
	{
		[Token(Token = "0x4001675")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public string VariableName;

		[Token(Token = "0x4001676")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CustomVariableValueType ValueType;

		[Token(Token = "0x4001677")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public LocalizableText SummaryText;

		[Token(Token = "0x4001678")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string InputFieldText;

		[Token(Token = "0x4001679")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public bool PlayOnSubmit;

		[Token(Token = "0x6001D42")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public GameState()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20005DD")]
	[CompilerGenerated]
	private struct _003CDeserializeState_003Ed__34
{
		[Token(Token = "0x400167A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400167B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400167C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public VariableInputPanel _003C_003E4__this;

		[Token(Token = "0x400167D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public GameStateMap stateMap;

		[Token(Token = "0x400167E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private GameState _003Cstate_003E5__2;

		[Token(Token = "0x400167F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001D43")]
		[Address(RVA = "0x4B10D0", Offset = "0x4AFAD0", VA = "0x1804B10D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001D44")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001667")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	[SerializeField]
	private TMP_InputField inputField;

	[Token(Token = "0x4001668")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	[SerializeField]
	private Button submitButton;

	[Token(Token = "0x4001669")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	[Tooltip("Whether to automatically select and activate input field when the UI is shown.")]
	[SerializeField]
	private bool activateOnShow;

	[Token(Token = "0x400166A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF9")]
	[Tooltip("Whether to attempt submit input field value when a `Submit` input is activated.")]
	[SerializeField]
	private bool submitOnInput;

	[Token(Token = "0x400166B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
	[Tooltip("When assigned, the game object will be de-/activated based on whether summary is assigned.")]
	[SerializeField]
	private GameObject summaryContainer;

	[Token(Token = "0x400166C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
	[SerializeField]
	private StringUnityEvent onSummaryChanged;

	[Token(Token = "0x400166D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
	[SerializeField]
	private StringUnityEvent onPredefinedValueChanged;

	[Token(Token = "0x400166E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x118")]
	private IScriptPlayer scriptPlayer;

	[Token(Token = "0x400166F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x120")]
	private ICustomVariableManager variableManager;

	[Token(Token = "0x4001670")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x128")]
	private IStateManager stateManager;

	[Token(Token = "0x4001671")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x130")]
	private IInputSampler submitInput;

	[Token(Token = "0x4001672")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x138")]
	private string variableName;

	[Token(Token = "0x4001673")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x140")]
	private CustomVariableValueType valueType;

	[Token(Token = "0x4001674")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x144")]
	private bool playOnSubmit;

	[Token(Token = "0x1700043E")]
	protected virtual LocalizableText Summary
	{
		[Token(Token = "0x6001D2D")]
		[Address(RVA = "0x33E0B0", Offset = "0x33CAB0", VA = "0x18033E0B0", Slot = "96")]
		[CompilerGenerated]
		get
		{
			return default(LocalizableText);
		}
		[Token(Token = "0x6001D2E")]
		[Address(RVA = "0x33E0D0", Offset = "0x33CAD0", VA = "0x18033E0D0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x1700043F")]
	protected virtual TMP_InputField InputField
	{
		[Token(Token = "0x6001D2F")]
		[Address(RVA = "0x49D2A0", Offset = "0x49BCA0", VA = "0x18049D2A0", Slot = "97")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000440")]
	protected virtual Button SubmitButton
	{
		[Token(Token = "0x6001D30")]
		[Address(RVA = "0x49D2B0", Offset = "0x49BCB0", VA = "0x18049D2B0", Slot = "98")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000441")]
	protected virtual bool ActivateOnShow
	{
		[Token(Token = "0x6001D31")]
		[Address(RVA = "0x4AFA70", Offset = "0x4AE470", VA = "0x1804AFA70", Slot = "99")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000442")]
	protected virtual bool SubmitOnInput
	{
		[Token(Token = "0x6001D32")]
		[Address(RVA = "0x4AFA80", Offset = "0x4AE480", VA = "0x1804AFA80", Slot = "100")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000443")]
	protected virtual GameObject SummaryContainer
	{
		[Token(Token = "0x6001D33")]
		[Address(RVA = "0x37A040", Offset = "0x378A40", VA = "0x18037A040", Slot = "101")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001D34")]
	[Address(RVA = "0x4AFA90", Offset = "0x4AE490", VA = "0x1804AFA90", Slot = "102")]
	public virtual void Show(string variableName, CustomVariableValueType valueType, LocalizableText summary, LocalizableText predefinedValue, bool playOnSubmit)
	{
	}

	[Token(Token = "0x6001D35")]
	[Address(RVA = "0x4AFBF0", Offset = "0x4AE5F0", VA = "0x1804AFBF0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001D36")]
	[Address(RVA = "0x4AFFA0", Offset = "0x4AE9A0", VA = "0x1804AFFA0", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x6001D37")]
	[Address(RVA = "0x4B0170", Offset = "0x4AEB70", VA = "0x1804B0170", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x6001D38")]
	[Address(RVA = "0x4B03B0", Offset = "0x4AEDB0", VA = "0x1804B03B0", Slot = "87")]
	protected override void SerializeState(GameStateMap stateMap)
	{
	}

	[Token(Token = "0x6001D39")]
	[Address(RVA = "0x4B0620", Offset = "0x4AF020", VA = "0x1804B0620", Slot = "88")]
	[AsyncStateMachine(typeof(_003CDeserializeState_003Ed__34))]
	protected override UniTask DeserializeState(GameStateMap stateMap)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001D3A")]
	[Address(RVA = "0x4B07A0", Offset = "0x4AF1A0", VA = "0x1804B07A0", Slot = "103")]
	protected virtual void SetSummary(LocalizableText value)
	{
	}

	[Token(Token = "0x6001D3B")]
	[Address(RVA = "0x4B0A50", Offset = "0x4AF450", VA = "0x1804B0A50", Slot = "104")]
	protected virtual void SetPredefinedValue(LocalizableText value)
	{
	}

	[Token(Token = "0x6001D3C")]
	[Address(RVA = "0x4B0AE0", Offset = "0x4AF4E0", VA = "0x1804B0AE0", Slot = "105")]
	protected virtual void SetInputValidation(CustomVariableValueType type)
	{
	}

	[Token(Token = "0x6001D3D")]
	[Address(RVA = "0x4B0B40", Offset = "0x4AF540", VA = "0x1804B0B40", Slot = "106")]
	protected virtual void HandleInputChanged(string text)
	{
	}

	[Token(Token = "0x6001D3E")]
	[Address(RVA = "0x4B0C00", Offset = "0x4AF600", VA = "0x1804B0C00", Slot = "107")]
	protected virtual void HandleSubmit()
	{
	}

	[Token(Token = "0x6001D3F")]
	[Address(RVA = "0x4B0E90", Offset = "0x4AF890", VA = "0x1804B0E90", Slot = "108")]
	protected virtual CustomVariableValue ParseValue(string inputText)
	{
		return default(CustomVariableValue);
	}

	[Token(Token = "0x6001D40")]
	[Address(RVA = "0x4B10C0", Offset = "0x4AFAC0", VA = "0x1804B10C0")]
	public VariableInputPanel()
	{
	}
}
