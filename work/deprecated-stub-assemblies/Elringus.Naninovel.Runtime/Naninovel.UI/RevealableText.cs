using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x20005B5")]
    public class RevealableText : NaninovelTMProText, IRevealableText
    {
    	[Token(Token = "0x40015E2")]
    	[FieldOffset(Offset = "0x830")]
    	[Tooltip("Whether to reveal characters under ruby (furigana) tag instantly.")]
    	[SerializeField]
    	private bool revealRubyInstantly;

    	[Token(Token = "0x40015E3")]
    	[FieldOffset(Offset = "0x838")]
    	private new string text;

    	[Token(Token = "0x17000429")]
    	public virtual IRevealInfo RevealInfo
    	{
    		[Token(Token = "0x6001C78")]
    		[Address(RVA = "0x4A11E0", Offset = "0x49FBE0", VA = "0x1804A11E0", Slot = "176")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700042A")]
    	public virtual string Text
    	{
    		[Token(Token = "0x6001C79")]
    		[Address(RVA = "0x4A1200", Offset = "0x49FC00", VA = "0x1804A1200", Slot = "177")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001C7A")]
    		[Address(RVA = "0x4A1210", Offset = "0x49FC10", VA = "0x1804A1210", Slot = "178")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700042B")]
    	public virtual Color TextColor
    	{
    		[Token(Token = "0x6001C7B")]
    		[Address(RVA = "0x4A1230", Offset = "0x49FC30", VA = "0x1804A1230", Slot = "179")]
    		get
    		{
    			return default(Color);
    		}
    		[Token(Token = "0x6001C7C")]
    		[Address(RVA = "0x4A1260", Offset = "0x49FC60", VA = "0x1804A1260", Slot = "180")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700042C")]
    	public virtual float RevealProgress
    	{
    		[Token(Token = "0x6001C7D")]
    		[Address(RVA = "0x4A1290", Offset = "0x49FC90", VA = "0x1804A1290", Slot = "181")]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001C7E")]
    		[Address(RVA = "0x4A12B0", Offset = "0x49FCB0", VA = "0x1804A12B0", Slot = "182")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700042D")]
    	protected virtual RevealState RevealState
    	{
    		[Token(Token = "0x6001C7F")]
    		[Address(RVA = "0x4A12D0", Offset = "0x49FCD0", VA = "0x1804A12D0", Slot = "183")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700042E")]
    	protected virtual HashSet<TextRevealEffect> ConfirmationRequests
    	{
    		[Token(Token = "0x6001C80")]
    		[Address(RVA = "0x4A12E0", Offset = "0x49FCE0", VA = "0x1804A12E0", Slot = "184")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700042F")]
    	protected virtual string TMPText
    	{
    		[Token(Token = "0x6001C81")]
    		[Address(RVA = "0x48E6E0", Offset = "0x48D0E0", VA = "0x18048E6E0", Slot = "185")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001C82")]
    		[Address(RVA = "0x48E6F0", Offset = "0x48D0F0", VA = "0x18048E6F0", Slot = "186")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000430")]
    	protected virtual int LastCharIndex
    	{
    		[Token(Token = "0x6001C83")]
    		[Address(RVA = "0x4A12F0", Offset = "0x49FCF0", VA = "0x1804A12F0", Slot = "187")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x17000431")]
    	protected virtual int LastRevealedCharIndex
    	{
    		[Token(Token = "0x6001C84")]
    		[Address(RVA = "0x4A1320", Offset = "0x49FD20", VA = "0x1804A1320", Slot = "188")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x17000432")]
    	protected virtual bool RevealRubyInstantly
    	{
    		[Token(Token = "0x6001C85")]
    		[Address(RVA = "0x4A1350", Offset = "0x49FD50", VA = "0x1804A1350", Slot = "189")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x6001C86")]
    	[Address(RVA = "0x4A1360", Offset = "0x49FD60", VA = "0x1804A1360", Slot = "190")]
    	public virtual bool RevealNextChars(int count, float duration, AsyncToken token)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001C87")]
    	[Address(RVA = "0x4A1460", Offset = "0x49FE60", VA = "0x1804A1460", Slot = "191")]
    	public virtual Vector2 GetLastRevealedCharPosition()
    	{
    		return default(Vector2);
    	}

    	[Token(Token = "0x6001C88")]
    	[Address(RVA = "0x4A1640", Offset = "0x4A0040", VA = "0x1804A1640", Slot = "192")]
    	public virtual char GetLastRevealedChar()
    	{
    		return default(char);
    	}

    	[Token(Token = "0x6001C89")]
    	[Address(RVA = "0x4A16C0", Offset = "0x4A00C0", VA = "0x1804A16C0", Slot = "193")]
    	public virtual bool RequestConfirmation(TextRevealEffect owner)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001C8A")]
    	[Address(RVA = "0x4A1730", Offset = "0x4A0130", VA = "0x1804A1730", Slot = "194")]
    	public virtual bool ResolveConfirmation(TextRevealEffect owner)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6001C8B")]
    	[Address(RVA = "0x4A17A0", Offset = "0x4A01A0", VA = "0x1804A17A0", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001C8C")]
    	[Address(RVA = "0x4A1970", Offset = "0x4A0370", VA = "0x1804A1970", Slot = "195")]
    	protected virtual void SetTextValue(string value)
    	{
    	}

    	[Token(Token = "0x6001C8D")]
    	[Address(RVA = "0x4A1A70", Offset = "0x4A0470", VA = "0x1804A1A70", Slot = "196")]
    	protected virtual float GetRevealProgress()
    	{
    		return default(float);
    	}

    	[Token(Token = "0x6001C8E")]
    	[Address(RVA = "0x4A1B20", Offset = "0x4A0520", VA = "0x1804A1B20", Slot = "197")]
    	protected virtual void SetRevealProgress(float progress)
    	{
    	}

    	[Token(Token = "0x6001C8F")]
    	[Address(RVA = "0x4A1CD0", Offset = "0x4A06D0", VA = "0x1804A1CD0", Slot = "198")]
    	protected virtual int GetNextRevealable(int charIndex)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6001C90")]
    	[Address(RVA = "0x4A1D40", Offset = "0x4A0740", VA = "0x1804A1D40", Slot = "199")]
    	protected virtual bool TryGetCharInfo(int charIndex, out TMP_CharacterInfo info)
    	{
    		info = default;
    		info = default;
    		return default(bool);
    	}

    	[Token(Token = "0x6001C91")]
    	[Address(RVA = "0x4A1FF0", Offset = "0x4A09F0", VA = "0x1804A1FF0", Slot = "200")]
    	protected virtual int GetLastOnDisplayedPage()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6001C92")]
    	[Address(RVA = "0x4A20E0", Offset = "0x4A0AE0", VA = "0x1804A20E0", Slot = "201")]
    	protected virtual int GetPreviousNonRuby(int charIndex)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6001C93")]
    	[Address(RVA = "0x4A2150", Offset = "0x4A0B50", VA = "0x1804A2150", Slot = "202")]
    	protected virtual int GetNextNonRuby(int charIndex)
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6001C94")]
    	[Address(RVA = "0x4A21F0", Offset = "0x4A0BF0", VA = "0x1804A21F0")]
    	public RevealableText()
    	{
    	}
    }
}
