using Il2CppDummyDll;
using Naninovel;
using TMPro;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000096")]
public sealed class WitchTrialsLogMessageUi : BacklogMessageUI
{
	[Token(Token = "0x4000284")]
	[FieldOffset(Offset = "0xD0")]
	[SerializeField]
	private CanvasGroup _authorPanelCanvasGroup;

	[Token(Token = "0x4000285")]
	[FieldOffset(Offset = "0xD8")]
	[SerializeField]
	private TMP_Text _authorLabel;

	[Token(Token = "0x4000286")]
	[FieldOffset(Offset = "0xE0")]
	private string _authorId;

	[Token(Token = "0x1700003A")]
	public override string AuthorId
	{
		[Token(Token = "0x600020C")]
		[Address(RVA = "0x33E0B0", Offset = "0x33CAB0", VA = "0x18033E0B0", Slot = "60")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700003B")]
	public override GameObject AuthorPanel
	{
		[Token(Token = "0x600020D")]
		[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "63")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600020E")]
	[Address(RVA = "0x33E0D0", Offset = "0x33CAD0", VA = "0x18033E0D0", Slot = "70")]
	protected override void SetAuthor(string authorId)
	{
	}

	[Token(Token = "0x600020F")]
	[Address(RVA = "0x33E130", Offset = "0x33CB30", VA = "0x18033E130")]
	public void ModifyAuthorPanel(AuthorTextBuilder authorTextBuilder)
	{
	}

	[Token(Token = "0x6000210")]
	[Address(RVA = "0x33E4C0", Offset = "0x33CEC0", VA = "0x18033E4C0")]
	public WitchTrialsLogMessageUi()
	{
	}
}
