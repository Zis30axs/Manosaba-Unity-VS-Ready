#nullable enable annotations
using Il2CppDummyDll;
using Naninovel;
using TMPro;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views
{
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
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33E0B0 (shared field getter, +0xE0)
    		[Token(Token = "0x600020C")]
    		[Address(RVA = "0x33E0B0", Offset = "0x33CAB0", VA = "0x18033E0B0", Slot = "60")]
    		get
    		{
    			return _authorId;
    		}
    	}

    	[Token(Token = "0x1700003B")]
    	public override GameObject AuthorPanel
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33E0C0 (shared `return null` body)
    		// Overridden to hide the author panel from Naninovel's base logic; ModifyAuthorPanel uses
    		// `base.AuthorPanel` (BacklogMessageUI 0x471780 = +0xA8, inlined) instead.
    		[Token(Token = "0x600020D")]
    		[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "63")]
    		get
    		{
    			return null;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x33E0D0 (shared field setter body)
    	// Stores `authorId` into +0xE0 with the write barrier; no base call.
    	[Token(Token = "0x600020E")]
    	[Address(RVA = "0x33E0D0", Offset = "0x33CAD0", VA = "0x18033E0D0", Slot = "70")]
    	protected override void SetAuthor(string authorId)
    	{
    		_authorId = authorId;
    	}

    	// RECONSTRUCTED | Confidence: A (local and ternary spelling: B) | RVA: 0x33E130, 898 B
    	// Same author-text shape as ProfilePage.RefreshPageContent:
    	// - `Engine.GetServiceOrErr<ICharacterManager>()` is held in a local and used twice; `AuthorId` (slot 60) is
    	//   re-read through the vtable at every use.
    	// - Inlined `GetActorMetaOrDefault` -> `UseCharacterColor` (+0x80) ? `new Color?(NameColor +0x84)` : null.
    	// - TryBuildAuthorText failing -> ICharacterManager slot 7 GetAuthorName; an explicit null store when the name is
    	//   null or whitespace (inlined IsNullOrWhiteSpace loop), else BuildTaggedSimpleName(name, null).
    	// - `_authorPanelCanvasGroup` (+0xD0) alpha 0f/1f by IsNullOrWhiteSpace(text); `_authorLabel` (+0xD8) SetText(text).
    	// - `base.AuthorPanel` (+0xA8, loaded first) SetActive(!string.IsNullOrEmpty(AuthorId)).
    	[Token(Token = "0x600020F")]
    	[Address(RVA = "0x33E130", Offset = "0x33CB30", VA = "0x18033E130")]
    	public void ModifyAuthorPanel(AuthorTextBuilder authorTextBuilder)
    	{
    		ICharacterManager characterManager = Engine.GetServiceOrErr<ICharacterManager>();
    		CharacterMetadata actorMetaOrDefault = characterManager.GetActorMetaOrDefault(AuthorId);
    		Color? color = actorMetaOrDefault.UseCharacterColor ? new Color?(actorMetaOrDefault.NameColor) : null;
    		if (!authorTextBuilder.TryBuildAuthorText(AuthorId, color, out string text))
    		{
    			string authorName = characterManager.GetAuthorName(AuthorId);
    			text = string.IsNullOrWhiteSpace(authorName) ? null : authorTextBuilder.BuildTaggedSimpleName(authorName, null);
    		}
    		_authorPanelCanvasGroup.alpha = string.IsNullOrWhiteSpace(text) ? 0f : 1f;
    		_authorLabel.SetText(text);
    		base.AuthorPanel.SetActive(!string.IsNullOrEmpty(AuthorId));
    	}

    	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x33E4C0, 354 B
    	// No initializers of its own: +0x90 = PlaybackSpot static (Invalid) and +0xA0 = new List<string>() are
    	// BacklogMessageUI's field initializers, inlined, followed by the ScriptableUIBehaviour..ctor tail jump.
    	[Token(Token = "0x6000210")]
    	[Address(RVA = "0x33E4C0", Offset = "0x33CEC0", VA = "0x18033E4C0")]
    	public WitchTrialsLogMessageUi()
    	{
    	}
    }
}
