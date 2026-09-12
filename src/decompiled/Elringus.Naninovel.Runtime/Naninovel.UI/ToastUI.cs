using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x20005D9")]
public class ToastUI : CustomUI, IToastUI, IManagedUI, ILocalizableUI
{
	[Token(Token = "0x4001662")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	[Tooltip("The appearance used by default, when 'appearance' parameter is not specified.")]
	[SerializeField]
	private ToastAppearance defaultAppearance;

	[Token(Token = "0x4001663")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	[Tooltip("Seconds to wait before hiding the toast; used by default, when 'duration' parameter is not specified.")]
	[SerializeField]
	private float defaultDuration;

	[Token(Token = "0x4001664")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	private readonly Dictionary<string, ToastAppearance> appearances;

	[Token(Token = "0x4001665")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	private Timer hideTimer;

	[Token(Token = "0x6001D28")]
	[Address(RVA = "0x4AF000", Offset = "0x4ADA00", VA = "0x1804AF000", Slot = "97")]
	public virtual void Show(LocalizableText text, [Optional] string appearance, [Optional] float? duration)
	{
	}

	[Token(Token = "0x6001D29")]
	[Address(RVA = "0x4AF2D0", Offset = "0x4ADCD0", VA = "0x1804AF2D0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001D2A")]
	[Address(RVA = "0x4AF530", Offset = "0x4ADF30", VA = "0x1804AF530", Slot = "98")]
	protected virtual bool TrySelectAppearance(string appearanceName, out ToastAppearance selectedAppearance)
	{
		selectedAppearance = default;
		selectedAppearance = default;
		return default(bool);
	}

	[Token(Token = "0x6001D2B")]
	[Address(RVA = "0x4AF920", Offset = "0x4AE320", VA = "0x1804AF920")]
	public ToastUI()
	{
	}
}
