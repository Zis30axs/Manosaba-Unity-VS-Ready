using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x20005AE")]
    public class RevealBroadcaster : TextRevealEffect
    {
    	[Token(Token = "0x40015D7")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private StringUnityEvent onEvent;

    	[Token(Token = "0x40015D8")]
    	[FieldOffset(Offset = "0x30")]
    	[Tooltip("When enabled, will attempt to parse and play event body as command.")]
    	[SerializeField]
    	private bool playCommand;

    	[Token(Token = "0x40015D9")]
    	[FieldOffset(Offset = "0x38")]
    	private IScriptPlayer player;

    	[Token(Token = "0x6001C4C")]
    	[Address(RVA = "0x49EE40", Offset = "0x49D840", VA = "0x18049EE40")]
    	private void Awake()
    	{
    	}

    	[Token(Token = "0x6001C4D")]
    	[Address(RVA = "0x49EEF0", Offset = "0x49D8F0", VA = "0x18049EEF0")]
    	private void OnEnable()
    	{
    	}

    	[Token(Token = "0x6001C4E")]
    	[Address(RVA = "0x49EFB0", Offset = "0x49D9B0", VA = "0x18049EFB0")]
    	private void OnDisable()
    	{
    	}

    	[Token(Token = "0x6001C4F")]
    	[Address(RVA = "0x49F130", Offset = "0x49DB30", VA = "0x18049F130")]
    	private void HandleChange()
    	{
    	}

    	[Token(Token = "0x6001C50")]
    	[Address(RVA = "0x49F440", Offset = "0x49DE40", VA = "0x18049F440")]
    	public RevealBroadcaster()
    	{
    	}
    }
}
