using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Il2CppDummyDll;
using Naninovel.ArabicSupport;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Naninovel.UI;

[Token(Token = "0x200058B")]
public class NaninovelTMProText : TextMeshProUGUI, IPointerClickHandler, IEventSystemHandler, IInputTrigger
{
	[Serializable]
	[Token(Token = "0x200058C")]
	public class LinkClickedEvent : UnityEvent<TMP_LinkInfo>
	{
		[Token(Token = "0x6001B8B")]
		[Address(RVA = "0x492370", Offset = "0x490D70", VA = "0x180492370")]
		public LinkClickedEvent()
		{
		}
	}

	[Token(Token = "0x400152E")]
	[FieldOffset(Offset = "0x7C8")]
	[Tooltip("Vertical line offset to use for the ruby (furigana) text; supported units: em, px, %.")]
	[SerializeField]
	private string rubyVerticalOffset;

	[Token(Token = "0x400152F")]
	[FieldOffset(Offset = "0x7D0")]
	[Tooltip("Font size scale (relative to the main text font size) to apply for the ruby (furigana) text.")]
	[SerializeField]
	private float rubySizeScale;

	[Token(Token = "0x4001530")]
	[FieldOffset(Offset = "0x7D4")]
	[Tooltip("Whether to compensate (add) line height for the lines that contain ruby tags.")]
	[SerializeField]
	private bool addRubyLineHeight;

	[Token(Token = "0x4001531")]
	[FieldOffset(Offset = "0x7D5")]
	[Tooltip("Whether to automatically unlock associated tip records when text wrapped in <tip> tags is printed.")]
	[SerializeField]
	private bool unlockTipsOnPrint;

	[Token(Token = "0x4001532")]
	[FieldOffset(Offset = "0x7D8")]
	[Tooltip("Template to use when processing text wrapped in <tip> tags. %TIP% will be replaced with the actual tip content.")]
	[SerializeField]
	private string tipTemplate;

	[Token(Token = "0x4001533")]
	[FieldOffset(Offset = "0x7E0")]
	[Tooltip("Invoked when a text wrapped in <tip> tags is clicked; returned string argument is the ID of the clicked tip. Be aware, that the default behaviour (showing `ITipsUI` when a tip is clicked) won't be invoked when a custom handler is assigned.")]
	[SerializeField]
	private StringUnityEvent onTipClicked;

	[Token(Token = "0x4001534")]
	[FieldOffset(Offset = "0x7E8")]
	[Tooltip("Whether to modify the text to support arabic languages (fix letters connectivity issues).")]
	[SerializeField]
	private bool fixArabicText;

	[Token(Token = "0x4001535")]
	[FieldOffset(Offset = "0x7E9")]
	[Tooltip("When `Fix Arabic Text` is enabled, controls to whether also fix Farsi characters.")]
	[SerializeField]
	private bool fixArabicFarsi;

	[Token(Token = "0x4001536")]
	[FieldOffset(Offset = "0x7EA")]
	[Tooltip("When `Fix Arabic Text` is enabled, controls to whether also fix rich text tags.")]
	[SerializeField]
	private bool fixArabicTextTags;

	[Token(Token = "0x4001537")]
	[FieldOffset(Offset = "0x7EB")]
	[Tooltip("When `Fix Arabic Text` is enabled, controls to whether preserve numbers.")]
	[SerializeField]
	private bool fixArabicPreserveNumbers;

	[Token(Token = "0x4001538")]
	[FieldOffset(Offset = "0x7F0")]
	[Tooltip("Template to use when processing text wrapped in <link> tags. %LINK% will be replaced with the actual tip content. When nothing is specified, the link tags won't be modified.")]
	[SerializeField]
	private string linkTemplate;

	[Token(Token = "0x4001539")]
	[FieldOffset(Offset = "0x7F8")]
	[Tooltip("Invoked when a text wrapped in <link> tags is clicked.")]
	[SerializeField]
	private LinkClickedEvent onLinkClicked;

	[Token(Token = "0x400153A")]
	private const int rubyLinkHash = -153532380;

	[Token(Token = "0x400153B")]
	private const string rubyLinkId = "NANINOVEL.RUBY";

	[Token(Token = "0x400153C")]
	private const string tipIdPrefix = "NANINOVEL.TIP.";

	[Token(Token = "0x400153D")]
	private const string tipTemplateLiteral = "%TIP%";

	[Token(Token = "0x400153E")]
	private const string linkTemplateLiteral = "%LINK%";

	[Token(Token = "0x400153F")]
	[FieldOffset(Offset = "0x0")]
	private static readonly Regex captureRubyRegex;

	[Token(Token = "0x4001540")]
	[FieldOffset(Offset = "0x8")]
	private static readonly Regex captureTipRegex;

	[Token(Token = "0x4001541")]
	[FieldOffset(Offset = "0x10")]
	private static readonly Regex captureLinkRegex;

	[Token(Token = "0x4001542")]
	[FieldOffset(Offset = "0x18")]
	private static readonly Regex captureEventRegex;

	[Token(Token = "0x4001543")]
	[FieldOffset(Offset = "0x20")]
	private static readonly Regex captureExpressionRegex;

	[Token(Token = "0x4001544")]
	[FieldOffset(Offset = "0x800")]
	private readonly FastStringBuilder arabicBuilder;

	[Token(Token = "0x4001545")]
	[FieldOffset(Offset = "0x808")]
	private readonly Dictionary<int, string> charToEvent;

	[Token(Token = "0x4001546")]
	[FieldOffset(Offset = "0x810")]
	private string unprocessedText;

	[Token(Token = "0x4001547")]
	[FieldOffset(Offset = "0x818")]
	private Canvas topmostCanvasCache;

	[Token(Token = "0x170003E1")]
	public override string text
	{
		[Token(Token = "0x6001B6F")]
		[Address(RVA = "0x48E6E0", Offset = "0x48D0E0", VA = "0x18048E6E0", Slot = "65")]
		get
		{
			return null;
		}
		[Token(Token = "0x6001B70")]
		[Address(RVA = "0x48E6F0", Offset = "0x48D0F0", VA = "0x18048E6F0", Slot = "66")]
		set
		{
		}
	}

	[Token(Token = "0x170003E2")]
	protected virtual string RubyVerticalOffset
	{
		[Token(Token = "0x6001B71")]
		[Address(RVA = "0x48E890", Offset = "0x48D290", VA = "0x18048E890", Slot = "145")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003E3")]
	protected virtual float RubySizeScale
	{
		[Token(Token = "0x6001B72")]
		[Address(RVA = "0x48E8A0", Offset = "0x48D2A0", VA = "0x18048E8A0", Slot = "146")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x170003E4")]
	protected virtual bool AddRubyLineHeight
	{
		[Token(Token = "0x6001B73")]
		[Address(RVA = "0x48E8B0", Offset = "0x48D2B0", VA = "0x18048E8B0", Slot = "147")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x170003E5")]
	protected virtual bool UnlockTipsOnPrint
	{
		[Token(Token = "0x6001B74")]
		[Address(RVA = "0x48E8C0", Offset = "0x48D2C0", VA = "0x18048E8C0", Slot = "148")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x170003E6")]
	protected virtual bool FixArabicText
	{
		[Token(Token = "0x6001B75")]
		[Address(RVA = "0x48E8D0", Offset = "0x48D2D0", VA = "0x18048E8D0", Slot = "149")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x170003E7")]
	protected virtual string TipTemplate
	{
		[Token(Token = "0x6001B76")]
		[Address(RVA = "0x48E8E0", Offset = "0x48D2E0", VA = "0x18048E8E0", Slot = "150")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003E8")]
	protected virtual Canvas TopmostCanvas
	{
		[Token(Token = "0x6001B77")]
		[Address(RVA = "0x48E8F0", Offset = "0x48D2F0", VA = "0x18048E8F0", Slot = "151")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170003E9")]
	protected virtual bool Edited
	{
		[Token(Token = "0x6001B78")]
		[Address(RVA = "0x48EA50", Offset = "0x48D450", VA = "0x18048EA50", Slot = "152")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6001B79")]
	[Address(RVA = "0x48EB00", Offset = "0x48D500", VA = "0x18048EB00", Slot = "153")]
	public virtual void OnPointerClick(PointerEventData eventData)
	{
	}

	[Token(Token = "0x6001B7A")]
	[Address(RVA = "0x48ED20", Offset = "0x48D720", VA = "0x18048ED20", Slot = "154")]
	public virtual bool CanTriggerInput()
	{
		return default(bool);
	}

	[Token(Token = "0x6001B7B")]
	[Address(RVA = "0x48EE60", Offset = "0x48D860", VA = "0x18048EE60", Slot = "155")]
	public virtual IReadOnlyCollection<string> PopEventsAtChar(int index)
	{
		return null;
	}

	[Token(Token = "0x6001B7C")]
	[Address(RVA = "0x48F2B0", Offset = "0x48DCB0", VA = "0x18048F2B0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001B7D")]
	[Address(RVA = "0x48F410", Offset = "0x48DE10", VA = "0x18048F410", Slot = "156")]
	protected virtual void OnLinkClicked(TMP_LinkInfo linkInfo)
	{
	}

	[Token(Token = "0x6001B7E")]
	[Address(RVA = "0x48F890", Offset = "0x48E290", VA = "0x18048F890", Slot = "157")]
	protected virtual void OnTipClicked(string tipId)
	{
	}

	[Token(Token = "0x6001B7F")]
	[Address(RVA = "0x48FA50", Offset = "0x48E450", VA = "0x18048FA50", Slot = "158")]
	protected virtual string ProcessText(string content)
	{
		return null;
	}

	[Token(Token = "0x6001B80")]
	[Address(RVA = "0x48FB10", Offset = "0x48E510", VA = "0x18048FB10", Slot = "159")]
	protected virtual string ProcessLinkTags(string content)
	{
		return null;
	}

	[Token(Token = "0x6001B81")]
	[Address(RVA = "0x4900A0", Offset = "0x48EAA0", VA = "0x1804900A0", Slot = "160")]
	protected virtual string ProcessRubyTags(string content)
	{
		return null;
	}

	[Token(Token = "0x6001B82")]
	[Address(RVA = "0x4908F0", Offset = "0x48F2F0", VA = "0x1804908F0", Slot = "161")]
	protected virtual string ProcessEventTags(string content)
	{
		return null;
	}

	[Token(Token = "0x6001B83")]
	[Address(RVA = "0x490D40", Offset = "0x48F740", VA = "0x180490D40", Slot = "162")]
	protected virtual string ProcessExpressionTags(string content)
	{
		return null;
	}

	[Token(Token = "0x6001B84")]
	[Address(RVA = "0x491210", Offset = "0x48FC10", VA = "0x180491210", Slot = "163")]
	protected virtual string ProcessTipTags(string content)
	{
		return null;
	}

	[Token(Token = "0x6001B85")]
	[Address(RVA = "0x4917F0", Offset = "0x4901F0", VA = "0x1804917F0", Slot = "164")]
	protected virtual bool IsRuby(int charIndex)
	{
		return default(bool);
	}

	[Token(Token = "0x6001B86")]
	[Address(RVA = "0x491940", Offset = "0x490340", VA = "0x180491940", Slot = "165")]
	protected virtual string FixArabic(string value)
	{
		return null;
	}

	[Token(Token = "0x6001B87")]
	[Address(RVA = "0x491AE0", Offset = "0x4904E0", VA = "0x180491AE0", Slot = "166")]
	protected virtual bool TryGetMousePosition(out Vector2 position)
	{
		position = default;
		position = default;
		return default(bool);
	}

	[Token(Token = "0x6001B88")]
	[Address(RVA = "0x491BC0", Offset = "0x4905C0", VA = "0x180491BC0")]
	public NaninovelTMProText()
	{
	}
}
