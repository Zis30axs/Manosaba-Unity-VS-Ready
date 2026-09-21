using System;
using System.Runtime.CompilerServices;
using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000042")]
    public class LabeledSlider : MonoBehaviour
    {
    	[Token(Token = "0x4000112")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private Slider _slider;

    	[Token(Token = "0x4000113")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private TextMeshProUGUI _label;

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCEB270: new CompositeDisposable() -> +0x30
    	[Token(Token = "0x4000114")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly CompositeDisposable _disposables = new CompositeDisposable();

    	[Token(Token = "0x4000115")]
    	[FieldOffset(Offset = "0x38")]
    	private IInputManager _inputManager;

    	[Token(Token = "0x17000039")]
    	private Func<float, string> BuildLabelTextFunc
    	{
    		[Token(Token = "0x6000135")]
    		[Address(RVA = "0xCEAC70", Offset = "0xCE9670", VA = "0x180CEAC70")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700003A")]
    	public Func<float, string> OverriddenBuildLabelTextFunc
    	{
    		[Token(Token = "0x6000136")]
    		[Address(RVA = "0x44CE10", Offset = "0x44B810", VA = "0x18044CE10")]
    		[CompilerGenerated]
    		private get
    		{
    			return null;
    		}
    		[Token(Token = "0x6000137")]
    		[Address(RVA = "0x5405E0", Offset = "0x53EFE0", VA = "0x1805405E0")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x1700003B")]
    	private IInputManager InputManager
    	{
    		[Token(Token = "0x6000138")]
    		[Address(RVA = "0xCEACF0", Offset = "0xCE96F0", VA = "0x180CEACF0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000139")]
    	[Address(RVA = "0xCEADB0", Offset = "0xCE97B0", VA = "0x180CEADB0")]
    	private void OnEnable()
    	{
    	}

    	[Token(Token = "0x600013A")]
    	[Address(RVA = "0xCEAF00", Offset = "0xCE9900", VA = "0x180CEAF00")]
    	public void OnDisable()
    	{
    	}

    	[Token(Token = "0x600013B")]
    	[Address(RVA = "0xCEAF20", Offset = "0xCE9920", VA = "0x180CEAF20")]
    	private string BuildValueText(float value)
    	{
    		return null;
    	}

    	[Token(Token = "0x600013C")]
    	[Address(RVA = "0xCEB040", Offset = "0xCE9A40", VA = "0x180CEB040")]
    	public void SetMaxValue(float value)
    	{
    	}

    	[Token(Token = "0x600013D")]
    	[Address(RVA = "0xCEB0E0", Offset = "0xCE9AE0", VA = "0x180CEB0E0")]
    	public void SetValue(int value)
    	{
    	}

    	[Token(Token = "0x600013E")]
    	[Address(RVA = "0xCEB120", Offset = "0xCE9B20", VA = "0x180CEB120")]
    	public IObservable<float> OnSlideAsObservable()
    	{
    		return null;
    	}

    	[Token(Token = "0x600013F")]
    	[Address(RVA = "0xCEB1A0", Offset = "0xCE9BA0", VA = "0x180CEB1A0")]
    	public IObservable<float> OnConfirmAsObservable()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000140")]
    	[Address(RVA = "0xCEB270", Offset = "0xCE9C70", VA = "0x180CEB270")]
    	public LabeledSlider()
    	{
    	}
    }
}
