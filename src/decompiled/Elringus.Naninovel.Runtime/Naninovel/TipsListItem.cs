using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Naninovel;

[Token(Token = "0x20004CA")]
public class TipsListItem : MonoBehaviour
{
	[Serializable]
	[Token(Token = "0x20004CB")]
	private class OnLabelChangedEvent : UnityEvent<string>
	{
		[Token(Token = "0x60017FA")]
		[Address(RVA = "0x5FC7B0", Offset = "0x5FB1B0", VA = "0x1805FC7B0")]
		public OnLabelChangedEvent()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20004CC")]
	[CompilerGenerated]
	private struct _003CInstantiate_003Ed__24
{
		[Token(Token = "0x4001238")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001239")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<TipsListItem> _003C_003Et__builder;

		[Token(Token = "0x400123A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public TipsListItem prototype;

		[Token(Token = "0x400123B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public Action<TipsListItem> onClick;

		[Token(Token = "0x400123C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string unlockableId;

		[Token(Token = "0x400123D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public string title;

		[Token(Token = "0x400123E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public bool selectedOnce;

		[Token(Token = "0x400123F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask<TipsListItem>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60017FB")]
		[Address(RVA = "0x5FC7F0", Offset = "0x5FB1F0", VA = "0x1805FC7F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60017FC")]
		[Address(RVA = "0x5FCBE0", Offset = "0x5FB5E0", VA = "0x1805FCBE0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400122B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[Tooltip("Tip label template. `{N}` will be replaced with the record number, `{T}` — with the title.")]
	[SerializeField]
	private string template;

	[Token(Token = "0x400122C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[Tooltip("Non-selected tip label template. `{I}` will be replaced with the tip label content.")]
	[SerializeField]
	private string normalTemplate;

	[Token(Token = "0x400122D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[Tooltip("Selected tip label template. `{I}` will be replaced with the tip label content.")]
	[SerializeField]
	private string selectedTemplate;

	[Token(Token = "0x400122E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[Tooltip("Label template for unlocked tip items that where never selected (seen). `{I}` will be replaced with the tip label content.")]
	[SerializeField]
	private string unseenTemplate;

	[Token(Token = "0x400122F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[Tooltip("Record title to set when the tip item is locked.")]
	[SerializeField]
	private string lockedTitle;

	[Token(Token = "0x4001230")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[Tooltip("The tip button.")]
	[SerializeField]
	private Button button;

	[Token(Token = "0x4001231")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[Tooltip("When assigned, the game object will be activated when the tip is selected.")]
	[SerializeField]
	private GameObject selectedIndicator;

	[Token(Token = "0x4001232")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[SerializeField]
	private OnLabelChangedEvent onLabelChanged;

	[Token(Token = "0x4001233")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private Action<TipsListItem> onClick;

	[Token(Token = "0x4001234")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	private string title;

	[Token(Token = "0x4001235")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	private bool seen;

	[Token(Token = "0x4001236")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x79")]
	private bool unlocked;

	[Token(Token = "0x4001237")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x7A")]
	private bool selected;

	[Token(Token = "0x1700036E")]
	public virtual string UnlockableId
	{
		[Token(Token = "0x60017EB")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "4")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x60017EC")]
		[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x1700036F")]
	public virtual int Number
	{
		[Token(Token = "0x60017ED")]
		[Address(RVA = "0x5FBAA0", Offset = "0x5FA4A0", VA = "0x1805FBAA0", Slot = "5")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x17000370")]
	protected virtual Button Button
	{
		[Token(Token = "0x60017EE")]
		[Address(RVA = "0x453480", Offset = "0x451E80", VA = "0x180453480", Slot = "6")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000371")]
	protected virtual GameObject SelectedIndicator
	{
		[Token(Token = "0x60017EF")]
		[Address(RVA = "0x453950", Offset = "0x452350", VA = "0x180453950", Slot = "7")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60017F0")]
	[Address(RVA = "0x5FBB50", Offset = "0x5FA550", VA = "0x1805FBB50")]
	[AsyncStateMachine(typeof(_003CInstantiate_003Ed__24))]
	public static UniTask<TipsListItem> Instantiate(TipsListItem prototype, string unlockableId, string title, bool selectedOnce, Action<TipsListItem> onClick)
	{
		return default(UniTask<TipsListItem>);
	}

	[Token(Token = "0x60017F1")]
	[Address(RVA = "0x5FBE00", Offset = "0x5FA800", VA = "0x1805FBE00", Slot = "8")]
	public virtual void SetSelected(bool selected)
	{
	}

	[Token(Token = "0x60017F2")]
	[Address(RVA = "0x5FBF60", Offset = "0x5FA960", VA = "0x1805FBF60", Slot = "9")]
	public virtual void SetUnlocked(bool unlocked)
	{
	}

	[Token(Token = "0x60017F3")]
	[Address(RVA = "0x5FBFE0", Offset = "0x5FA9E0", VA = "0x1805FBFE0", Slot = "10")]
	protected virtual void Awake()
	{
	}

	[Token(Token = "0x60017F4")]
	[Address(RVA = "0x5FC190", Offset = "0x5FAB90", VA = "0x1805FC190", Slot = "11")]
	protected virtual void OnEnable()
	{
	}

	[Token(Token = "0x60017F5")]
	[Address(RVA = "0x5FC240", Offset = "0x5FAC40", VA = "0x1805FC240", Slot = "12")]
	protected virtual void OnDisable()
	{
	}

	[Token(Token = "0x60017F6")]
	[Address(RVA = "0x5FC310", Offset = "0x5FAD10", VA = "0x1805FC310", Slot = "13")]
	protected virtual void SetLabel(string value)
	{
	}

	[Token(Token = "0x60017F7")]
	[Address(RVA = "0x5FC370", Offset = "0x5FAD70", VA = "0x1805FC370", Slot = "14")]
	protected virtual void HandleButtonClicked()
	{
	}

	[Token(Token = "0x60017F8")]
	[Address(RVA = "0x5FC390", Offset = "0x5FAD90", VA = "0x1805FC390", Slot = "15")]
	protected virtual string BuildLabel()
	{
		return null;
	}

	[Token(Token = "0x60017F9")]
	[Address(RVA = "0x5FC520", Offset = "0x5FAF20", VA = "0x1805FC520")]
	public TipsListItem()
	{
	}
}
