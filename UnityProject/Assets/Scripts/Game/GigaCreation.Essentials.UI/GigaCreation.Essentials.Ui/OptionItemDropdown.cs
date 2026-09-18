using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Input;
using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using TMPro;
using UniRx;
using UnityEngine;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000024")]
    public class OptionItemDropdown : MonoBehaviour
    {
    	[Token(Token = "0x400007D")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private TMP_Dropdown _dropdown;

    	[Token(Token = "0x400007E")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private AutoLayoutSupporter _autoLayoutSupporter;

    	[Token(Token = "0x400007F")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly CompositeDisposable _disposables;

    	[Token(Token = "0x4000080")]
    	[FieldOffset(Offset = "0x38")]
    	private IInputManager _inputManager;

    	[Token(Token = "0x17000016")]
    	protected TMP_Dropdown Dropdown
    	{
    		[Token(Token = "0x6000090")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000017")]
    	private IInputManager InputManager
    	{
    		[Token(Token = "0x6000091")]
    		[Address(RVA = "0xCE0E00", Offset = "0xCDF800", VA = "0x180CE0E00")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000092")]
    	[Address(RVA = "0xCE0EC0", Offset = "0xCDF8C0", VA = "0x180CE0EC0")]
    	private void OnEnable()
    	{
    	}

    	[Token(Token = "0x6000093")]
    	[Address(RVA = "0xCE10B0", Offset = "0xCDFAB0", VA = "0x180CE10B0")]
    	private void OnDisable()
    	{
    	}

    	[Token(Token = "0x6000094")]
    	[Address(RVA = "0xCE10F0", Offset = "0xCDFAF0", VA = "0x180CE10F0")]
    	private void Reset()
    	{
    	}

    	[Token(Token = "0x6000095")]
    	[Address(RVA = "0xCE1190", Offset = "0xCDFB90", VA = "0x180CE1190")]
    	public void SetValue(int value)
    	{
    	}

    	[Token(Token = "0x6000096")]
    	[Address(RVA = "0xCE11C0", Offset = "0xCDFBC0", VA = "0x180CE11C0")]
    	public void SetOptions(List<string> options)
    	{
    	}

    	[Token(Token = "0x6000097")]
    	[Address(RVA = "0xCE1210", Offset = "0xCDFC10", VA = "0x180CE1210")]
    	public IObservable<int> OnValueChangedAsObservable()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000098")]
    	[Address(RVA = "0xCE0D20", Offset = "0xCDF720", VA = "0x180CE0D20")]
    	public OptionItemDropdown()
    	{
    	}
    }
}
