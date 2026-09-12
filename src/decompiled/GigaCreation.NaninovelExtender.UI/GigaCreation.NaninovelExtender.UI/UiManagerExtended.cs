using System.Collections.Generic;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.UI;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Ui;

[Token(Token = "0x2000005")]
[UsedImplicitly]
[InitializeAtRuntime(0, typeof(UIManager))]
public class UiManagerExtended : UIManager
{
	[Token(Token = "0x4000008")]
	[FieldOffset(Offset = "0xB0")]
	private readonly IScriptPlayer _naniScriptPlayer;

	[Token(Token = "0x4000009")]
	[FieldOffset(Offset = "0xB8")]
	private readonly IList<IManagedUI> _currentModalUis;

	[Token(Token = "0x400000A")]
	[FieldOffset(Offset = "0xC0")]
	private GameObject _modalContainer;

	[Token(Token = "0x400000B")]
	[FieldOffset(Offset = "0xC8")]
	private CanvasGroup _containerGroup;

	[Token(Token = "0x17000001")]
	private GameObject ModalContainer
	{
		[Token(Token = "0x6000006")]
		[Address(RVA = "0xD34230", Offset = "0xD32C30", VA = "0x180D34230")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000002")]
	private CanvasGroup ContainerGroup
	{
		[Token(Token = "0x6000007")]
		[Address(RVA = "0xD34390", Offset = "0xD32D90", VA = "0x180D34390")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000008")]
	[Address(RVA = "0xD34510", Offset = "0xD32F10", VA = "0x180D34510")]
	public UiManagerExtended(UIConfiguration config, IResourceProviderManager resources, ILocalizationManager l10n, ICommunityLocalization communityL10n, ICameraManager cameraManager, IInputManager inputManager, IScriptPlayer scriptPlayer)
	{
	}

	[Token(Token = "0x6000009")]
	[Address(RVA = "0xD346B0", Offset = "0xD330B0", VA = "0x180D346B0", Slot = "50")]
	public override void AddModalUI(IManagedUI managedUI)
	{
	}

	[Token(Token = "0x600000A")]
	[Address(RVA = "0xD34C70", Offset = "0xD33670", VA = "0x180D34C70", Slot = "51")]
	public override void RemoveModalUI(IManagedUI managedUI)
	{
	}

	[Token(Token = "0x600000B")]
	[Address(RVA = "0xD350E0", Offset = "0xD33AE0", VA = "0x180D350E0")]
	private static void SetCancelEventBlocked(bool isBlocked, params ICancelEventBlockable[] blockables)
	{
	}

	[Token(Token = "0x600000C")]
	[Address(RVA = "0xD35210", Offset = "0xD33C10", VA = "0x180D35210", Slot = "55")]
	protected override bool IsModal(IManagedUI managedUI)
	{
		return default(bool);
	}

	[Token(Token = "0x600000D")]
	[Address(RVA = "0xD35280", Offset = "0xD33C80", VA = "0x180D35280", Slot = "61")]
	protected override void ToggleUI()
	{
	}
}
