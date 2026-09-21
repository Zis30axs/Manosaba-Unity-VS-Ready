using Il2CppDummyDll;
using UniRx;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000006")]
    public abstract class ButtonExtendedBase : Button
    {
    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCD6AA0: new CompositeDisposable() -> +0x108
    	[Token(Token = "0x400000A")]
    	[FieldOffset(Offset = "0x108")]
    	private readonly CompositeDisposable _disposables = new CompositeDisposable();

    	[Token(Token = "0x400000B")]
    	[FieldOffset(Offset = "0x110")]
    	private SelectableUtility _selectableUtil;

    	[Token(Token = "0x17000005")]
    	private SelectableUtility SelectableUtil
    	{
    		[Token(Token = "0x6000009")]
    		[Address(RVA = "0xCD6650", Offset = "0xCD5050", VA = "0x180CD6650")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x600000A")]
    	[Address(RVA = "0xCD6720", Offset = "0xCD5120", VA = "0x180CD6720", Slot = "5")]
    	protected override void OnEnable()
    	{
    	}

    	[Token(Token = "0x600000B")]
    	[Address(RVA = "0xCD6780", Offset = "0xCD5180", VA = "0x180CD6780", Slot = "7")]
    	protected override void OnDisable()
    	{
    	}

    	[Token(Token = "0x600000C")]
    	[Address(RVA = "0xCD67B0", Offset = "0xCD51B0", VA = "0x180CD67B0", Slot = "43")]
    	protected virtual void Initialize()
    	{
    	}

    	[Token(Token = "0x600000D")]
    	[Address(RVA = "0xCD6910", Offset = "0xCD5310", VA = "0x180CD6910", Slot = "26")]
    	protected override void DoStateTransition(SelectionState state, bool instant)
    	{
    	}

    	[Token(Token = "0x600000E")]
    	protected abstract void OnHighlightingChanged(bool isHighlighted);

    	[Token(Token = "0x600000F")]
    	protected abstract void OnEnablingChanged(bool isEnabled);

    	[Token(Token = "0x6000010")]
    	[Address(RVA = "0xCD6AA0", Offset = "0xCD54A0", VA = "0x180CD6AA0")]
    	protected ButtonExtendedBase()
    	{
    	}
    }
}
