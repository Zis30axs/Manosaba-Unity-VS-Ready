using System;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;

namespace Naninovel;

[Token(Token = "0x200032B")]
public class CustomVariableTrigger : MonoBehaviour
{
	[Serializable]
	[Token(Token = "0x200032C")]
	private class StringChangedEvent : UnityEvent<string>
	{
		[Token(Token = "0x6000F38")]
		[Address(RVA = "0x563520", Offset = "0x561F20", VA = "0x180563520")]
		public StringChangedEvent()
		{
		}
	}

	[Serializable]
	[Token(Token = "0x200032D")]
	private class NumericChangedEvent : UnityEvent<float>
	{
		[Token(Token = "0x6000F39")]
		[Address(RVA = "0x563560", Offset = "0x561F60", VA = "0x180563560")]
		public NumericChangedEvent()
		{
		}
	}

	[Serializable]
	[Token(Token = "0x200032E")]
	private class IntegerChangedEvent : UnityEvent<int>
	{
		[Token(Token = "0x6000F3A")]
		[Address(RVA = "0x5635A0", Offset = "0x561FA0", VA = "0x1805635A0")]
		public IntegerChangedEvent()
		{
		}
	}

	[Serializable]
	[Token(Token = "0x200032F")]
	private class BooleanChangedEvent : UnityEvent<bool>
	{
		[Token(Token = "0x6000F3B")]
		[Address(RVA = "0x5635E0", Offset = "0x561FE0", VA = "0x1805635E0")]
		public BooleanChangedEvent()
		{
		}
	}

	[Serializable]
	[Token(Token = "0x2000330")]
	private class VariableRemovedEvent : UnityEvent
	{
		[Token(Token = "0x6000F3C")]
		[Address(RVA = "0x563620", Offset = "0x562020", VA = "0x180563620")]
		public VariableRemovedEvent()
		{
		}
	}

	[Token(Token = "0x4000BE0")]
	[FieldOffset(Offset = "0x20")]
	[Tooltip("Name of a custom state variable to listen for.")]
	[SerializeField]
	private string customVariableName;

	[Token(Token = "0x4000BE1")]
	[FieldOffset(Offset = "0x28")]
	[Tooltip("Whether to broadcast the change events on component start (given variable exists at that point).")]
	[SerializeField]
	private bool triggerOnStart;

	[Token(Token = "0x4000BE2")]
	[FieldOffset(Offset = "0x30")]
	[Tooltip("Invoked when value of a custom variable with specified name is changed. Invoked even when the value type is not string, in which case the value is converted to string.")]
	[SerializeField]
	private StringChangedEvent onStringChanged;

	[Token(Token = "0x4000BE3")]
	[FieldOffset(Offset = "0x38")]
	[Tooltip("Invoked when value of a custom variable with specified name is changed and the value is a number (both float and integer).")]
	[SerializeField]
	private NumericChangedEvent onNumericChanged;

	[Token(Token = "0x4000BE4")]
	[FieldOffset(Offset = "0x40")]
	[Tooltip("Invoked when value of a custom variable with specified name is changed and the value is an integer number.")]
	[SerializeField]
	private IntegerChangedEvent onIntegerChanged;

	[Token(Token = "0x4000BE5")]
	[FieldOffset(Offset = "0x48")]
	[Tooltip("Invoked when value of a custom variable with specified name is changed and the value is a boolean.")]
	[SerializeField]
	private BooleanChangedEvent onBooleanChanged;

	[Token(Token = "0x4000BE6")]
	[FieldOffset(Offset = "0x50")]
	[Tooltip("Invoked when variable with specified name is removed.")]
	[SerializeField]
	private VariableRemovedEvent onRemoved;

	[Token(Token = "0x4000BE7")]
	[FieldOffset(Offset = "0x58")]
	private ICustomVariableManager variableManager;

	[Token(Token = "0x4000BE8")]
	[FieldOffset(Offset = "0x60")]
	private IStateManager stateManager;

	[Token(Token = "0x17000207")]
	public virtual string CustomVariableName
	{
		[Token(Token = "0x6000F2B")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "4")]
		get
		{
			return null;
		}
		[Token(Token = "0x6000F2C")]
		[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0", Slot = "5")]
		set
		{
		}
	}

	[Token(Token = "0x17000208")]
	public virtual CustomVariableValue? CustomVariableValue
	{
		[Token(Token = "0x6000F2D")]
		[Address(RVA = "0x562A40", Offset = "0x561440", VA = "0x180562A40", Slot = "6")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000F2E")]
	[Address(RVA = "0x562A80", Offset = "0x561480", VA = "0x180562A80", Slot = "7")]
	protected virtual void Awake()
	{
	}

	[Token(Token = "0x6000F2F")]
	[Address(RVA = "0x562BC0", Offset = "0x5615C0", VA = "0x180562BC0", Slot = "8")]
	protected virtual void OnEnable()
	{
	}

	[Token(Token = "0x6000F30")]
	[Address(RVA = "0x562CC0", Offset = "0x5616C0", VA = "0x180562CC0", Slot = "9")]
	protected virtual void OnDisable()
	{
	}

	[Token(Token = "0x6000F31")]
	[Address(RVA = "0x562DC0", Offset = "0x5617C0", VA = "0x180562DC0", Slot = "10")]
	protected virtual void Start()
	{
	}

	[Token(Token = "0x6000F32")]
	[Address(RVA = "0x562EB0", Offset = "0x5618B0", VA = "0x180562EB0", Slot = "11")]
	protected virtual void HandleVariableUpdated(CustomVariableUpdatedArgs args)
	{
	}

	[Token(Token = "0x6000F33")]
	[Address(RVA = "0x562FA0", Offset = "0x5619A0", VA = "0x180562FA0", Slot = "12")]
	protected virtual UniTask HandleGameDeserialized(GameStateMap state)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000F34")]
	[Address(RVA = "0x563030", Offset = "0x561A30", VA = "0x180563030", Slot = "13")]
	protected virtual void NotifyValueChanged(CustomVariableValue? value)
	{
	}

	[Token(Token = "0x6000F35")]
	[Address(RVA = "0x563390", Offset = "0x561D90", VA = "0x180563390", Slot = "14")]
	protected virtual CustomVariableValue? GetCurrentValueOrNull()
	{
		return null;
	}

	[Token(Token = "0x6000F36")]
	[Address(RVA = "0x5634C0", Offset = "0x561EC0", VA = "0x1805634C0", Slot = "15")]
	protected virtual bool IsInteger(float value)
	{
		return default(bool);
	}

	[Token(Token = "0x6000F37")]
	[Address(RVA = "0x53CE30", Offset = "0x53B830", VA = "0x18053CE30")]
	public CustomVariableTrigger()
	{
	}
}
