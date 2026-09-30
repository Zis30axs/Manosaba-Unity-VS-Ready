using Il2CppDummyDll;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI
{
    [Token(Token = "0x2000561")]
    public class LoadLoggerScrollRect : ScriptableUIComponent<ScrollRect>
    {
    	[Token(Token = "0x400148D")]
    	[FieldOffset(Offset = "0x88")]
    	[SerializeField]
    	private TMP_Text loggerText;

    	[Token(Token = "0x400148E")]
    	[FieldOffset(Offset = "0x90")]
    	[SerializeField]
    	private TMP_Text memoryUsageText;

    	[Token(Token = "0x400148F")]
    	[FieldOffset(Offset = "0x98")]
    	private IResourceProviderManager providerManager;

    	[Token(Token = "0x170003C2")]
    	protected virtual TMP_Text LoggerText
    	{
    		[Token(Token = "0x6001AAC")]
    		[Address(RVA = "0x46D4E0", Offset = "0x46BEE0", VA = "0x18046D4E0", Slot = "60")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003C3")]
    	protected virtual TMP_Text MemoryUsageText
    	{
    		[Token(Token = "0x6001AAD")]
    		[Address(RVA = "0x468E20", Offset = "0x467820", VA = "0x180468E20", Slot = "61")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170003C4")]
    	protected virtual bool LogEnabled
    	{
    		[Token(Token = "0x6001AAE")]
    		[Address(RVA = "0x62DFA0", Offset = "0x62C9A0", VA = "0x18062DFA0", Slot = "62")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x6001AAF")]
    	[Address(RVA = "0x62E070", Offset = "0x62CA70", VA = "0x18062E070", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x6001AB0")]
    	[Address(RVA = "0x62E200", Offset = "0x62CC00", VA = "0x18062E200", Slot = "6")]
    	protected override void Start()
    	{
    	}

    	[Token(Token = "0x6001AB1")]
    	[Address(RVA = "0x62E350", Offset = "0x62CD50", VA = "0x18062E350", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x6001AB2")]
    	[Address(RVA = "0x62E410", Offset = "0x62CE10", VA = "0x18062E410", Slot = "8")]
    	protected override void OnDestroy()
    	{
    	}

    	[Token(Token = "0x6001AB3")]
    	[Address(RVA = "0x62E4C0", Offset = "0x62CEC0", VA = "0x18062E4C0", Slot = "63")]
    	protected virtual void LogResourceProviderMessage(string message)
    	{
    	}

    	[Token(Token = "0x6001AB4")]
    	[Address(RVA = "0x62E5B0", Offset = "0x62CFB0", VA = "0x18062E5B0", Slot = "64")]
    	protected virtual void Append(string message)
    	{
    	}

    	[Token(Token = "0x6001AB5")]
    	[Address(RVA = "0x62E6D0", Offset = "0x62D0D0", VA = "0x18062E6D0", Slot = "65")]
    	protected virtual void UpdateMemoryUsage()
    	{
    	}

    	[Token(Token = "0x6001AB6")]
    	[Address(RVA = "0x62E890", Offset = "0x62D290", VA = "0x18062E890", Slot = "66")]
    	protected virtual void EnsureCapacity(string message)
    	{
    	}

    	[Token(Token = "0x6001AB7")]
    	[Address(RVA = "0x62EC70", Offset = "0x62D670", VA = "0x18062EC70")]
    	public LoadLoggerScrollRect()
    	{
    	}
    }
}
