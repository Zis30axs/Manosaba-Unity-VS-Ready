using UnityEngine.EventSystems;
using UniRx.Triggers;
using GigaCreation.Tools.Service;
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

    	// RECONSTRUCTED | Confidence: A | field initializer from the constructor at 0xCE0D20 (shared with LocalizationDropdown, which adds none): new CompositeDisposable() -> +0x30
    	[Token(Token = "0x400007F")]
    	[FieldOffset(Offset = "0x30")]
    	private readonly CompositeDisposable _disposables = new CompositeDisposable();

    	[Token(Token = "0x4000080")]
    	[FieldOffset(Offset = "0x38")]
    	private IInputManager _inputManager;

    	[Token(Token = "0x17000016")]
    	protected TMP_Dropdown Dropdown
    	{
    		[Token(Token = "0x6000090")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x377910, 5 B -- `mov rax, qword ptr [rcx + 0x20]; ret`: the backing field _dropdown (+0x20 in dump.cs)
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    		get
    		{
    			return _dropdown;
    		}
    	}

    	[Token(Token = "0x17000017")]
    	private IInputManager InputManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCE0E00 -- lazy: _inputManager (+0x38), else ServiceLocator.Get<IInputManager>(true)
    		// Annotated disassembly: work/title-path/disas/oid_0xCE0E00.txt
    		[Token(Token = "0x6000091")]
    		[Address(RVA = "0xCE0E00", Offset = "0xCDF800", VA = "0x180CE0E00")]
    		get
    		{
    			if (_inputManager == null)
    			{
    				_inputManager = ServiceLocator.Get<IInputManager>(true);
    			}
    			return _inputManager;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE0EC0, 490 B / b__8_0 0xCE1400, b__8_1 0xCE14F0, b__8_2 0xCE1710 -- each
    	//   AddTo(_disposables (+0x30), this): _autoLayoutSupporter (+0x28).OnEnableAsObservable() -> its
    	//   EnableLayoutComponents() (inlined); its OnDisableAsObservable() -> DisableLayoutComponents() (inlined) and, when
    	//   EventSystem.current has a live currentSelectedGameObject (+0x40), SetSelectedGameObject(null);
    	//   _dropdown (+0x20).OnPointerClickAsObservable() -> InputManager.PlayResponseSfx(Submit) (getter inlined)
    	// Annotated disassembly: work/title-path/disas/oid_0xCE0EC0.txt, oid_0xCE1400.txt, oid_0xCE14F0.txt, oid_0xCE1710.txt
    	[Token(Token = "0x6000092")]
    	[Address(RVA = "0xCE0EC0", Offset = "0xCDF8C0", VA = "0x180CE0EC0")]
    	private void OnEnable()
    	{
    		_autoLayoutSupporter.OnEnableAsObservable().Subscribe(delegate
    		{
    			_autoLayoutSupporter.EnableLayoutComponents();
    		}).AddTo(_disposables, this);
    		_autoLayoutSupporter.OnDisableAsObservable().Subscribe(delegate
    		{
    			_autoLayoutSupporter.DisableLayoutComponents();
    			if (EventSystem.current.currentSelectedGameObject != null)
    			{
    				EventSystem.current.SetSelectedGameObject(null);
    			}
    		}).AddTo(_disposables, this);
    		_dropdown.OnPointerClickAsObservable().Subscribe(delegate
    		{
    			InputManager.PlayResponseSfx(ResponseKind.Submit);
    		}).AddTo(_disposables, this);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE10B0, 52 B -- _disposables.Clear(); tail jump _dropdown.Hide()
    	// Annotated disassembly: work/title-path/disas/oid_0xCE10B0.txt
    	[Token(Token = "0x6000093")]
    	[Address(RVA = "0xCE10B0", Offset = "0xCDFAB0", VA = "0x180CE10B0")]
    	private void OnDisable()
    	{
    		_disposables.Clear();
    		_dropdown.Hide();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE10F0 -- GetComponentInChildren<TMP_Dropdown>() -> _dropdown (+0x20)
    	// Annotated disassembly: work/title-path/disas/oid_0xCE10F0.txt
    	[Token(Token = "0x6000094")]
    	[Address(RVA = "0xCE10F0", Offset = "0xCDFAF0", VA = "0x180CE10F0")]
    	private void Reset()
    	{
    		_dropdown = GetComponentInChildren<TMP_Dropdown>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE1190 -- _dropdown.SetValueWithoutNotify(value) (inlined: tail jump
    	//   TMP_Dropdown.SetValue(value, false))
    	// Annotated disassembly: work/title-path/disas/oid_0xCE1190.txt
    	[Token(Token = "0x6000095")]
    	[Address(RVA = "0xCE1190", Offset = "0xCDFB90", VA = "0x180CE1190")]
    	public void SetValue(int value)
    	{
    		_dropdown.SetValueWithoutNotify(value);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE11C0 -- _dropdown.ClearOptions(); tail jump _dropdown.AddOptions(options)
    	// Annotated disassembly: work/title-path/disas/oid_0xCE11C0.txt
    	[Token(Token = "0x6000096")]
    	[Address(RVA = "0xCE11C0", Offset = "0xCDFBC0", VA = "0x180CE11C0")]
    	public void SetOptions(List<string> options)
    	{
    		_dropdown.ClearOptions();
    		_dropdown.AddOptions(options);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCE1210 -- _dropdown.OnValueChangedAsObservable() (TextMeshProDropdownExtensions,
    	//   inlined) .Skip(1) (edx = 1): the current value it emits on subscription is dropped
    	// Annotated disassembly: work/title-path/disas/oid_0xCE1210.txt
    	[Token(Token = "0x6000097")]
    	[Address(RVA = "0xCE1210", Offset = "0xCDFC10", VA = "0x180CE1210")]
    	public IObservable<int> OnValueChangedAsObservable()
    	{
    		return _dropdown.OnValueChangedAsObservable().Skip(1);
    	}

    	[Token(Token = "0x6000098")]
    	[Address(RVA = "0xCE0D20", Offset = "0xCDF720", VA = "0x180CE0D20")]
    	public OptionItemDropdown()
    	{
    	}
    }
}
