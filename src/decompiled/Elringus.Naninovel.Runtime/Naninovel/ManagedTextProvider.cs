using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Naninovel;

[Token(Token = "0x20003AC")]
public class ManagedTextProvider : MonoBehaviour
{
	[Serializable]
	[Token(Token = "0x20003AD")]
	private class ValueChangedEvent : UnityEvent<string>
	{
		[Token(Token = "0x60011C5")]
		[Address(RVA = "0x593DA0", Offset = "0x5927A0", VA = "0x180593DA0")]
		public ValueChangedEvent()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20003AE")]
	[CompilerGenerated]
	private struct _003CInvokeValueChanged_003Ed__18
{
		[Token(Token = "0x4000DB2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000DB3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000DB4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public ManagedTextProvider _003C_003E4__this;

		[Token(Token = "0x4000DB5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<Resource>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60011C6")]
		[Address(RVA = "0x593DE0", Offset = "0x5927E0", VA = "0x180593DE0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60011C7")]
		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000DAC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[FormerlySerializedAs("category")]
	[Tooltip("Local resource path of the managed text document, which contains tracked managed text record.")]
	[SerializeField]
	private string document;

	[Token(Token = "0x4000DAD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[Tooltip("ID of the tracked managed text record; when not specified (empty), will use name of the game object to which the component is attached.")]
	[SerializeField]
	private string key;

	[Token(Token = "0x4000DAE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[Tooltip("Default value to use when the tracked record is missing.")]
	[SerializeField]
	private string defaultValue;

	[Token(Token = "0x4000DAF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[Tooltip("Invoked when value of the tracked managed text record is changed (eg, when switching localization); also invoked when the engine is initialized.")]
	[SerializeField]
	private ValueChangedEvent onValueChanged;

	[Token(Token = "0x4000DB0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private ILocalizationManager l10n;

	[Token(Token = "0x4000DB1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private ITextManager docs;

	[Token(Token = "0x17000267")]
	public string Document
	{
		[Token(Token = "0x60011BB")]
		[Address(RVA = "0x593650", Offset = "0x592050", VA = "0x180593650")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000268")]
	public string Key
	{
		[Token(Token = "0x60011BC")]
		[Address(RVA = "0x593710", Offset = "0x592110", VA = "0x180593710")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000269")]
	public string DefaultValue
	{
		[Token(Token = "0x60011BD")]
		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60011BE")]
	[Address(RVA = "0x5937D0", Offset = "0x5921D0", VA = "0x1805937D0")]
	private void OnEnable()
	{
	}

	[Token(Token = "0x60011BF")]
	[Address(RVA = "0x5938A0", Offset = "0x5922A0", VA = "0x1805938A0")]
	private void OnDisable()
	{
	}

	[Token(Token = "0x60011C0")]
	[Address(RVA = "0x5939C0", Offset = "0x5923C0", VA = "0x1805939C0")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x60011C1")]
	[Address(RVA = "0x593A40", Offset = "0x592440", VA = "0x180593A40")]
	private void HandleEngineInitialized()
	{
	}

	[Token(Token = "0x60011C2")]
	[Address(RVA = "0x593C60", Offset = "0x592660", VA = "0x180593C60")]
	private void HandleLocalizationChanged(LocaleChangedArgs _)
	{
	}

	[Token(Token = "0x60011C3")]
	[Address(RVA = "0x593C70", Offset = "0x592670", VA = "0x180593C70")]
	[AsyncStateMachine(typeof(_003CInvokeValueChanged_003Ed__18))]
	private void InvokeValueChanged()
	{
	}

	[Token(Token = "0x60011C4")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public ManagedTextProvider()
	{
	}
}
