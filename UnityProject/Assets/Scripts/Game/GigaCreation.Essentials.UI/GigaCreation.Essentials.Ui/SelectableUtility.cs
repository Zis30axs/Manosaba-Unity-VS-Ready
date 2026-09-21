using System;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000041")]
    public class SelectableUtility
    {
    	[Token(Token = "0x400010E")]
    	public const Selectable.Transition DefaultTransition = Selectable.Transition.None;

    	[Token(Token = "0x400010F")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly ReactiveProperty<bool> _isHighlighted;

    	[Token(Token = "0x4000110")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly ReactiveProperty<bool> _isEnabled;

    	[Token(Token = "0x4000111")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly GameObject _gameObject;

    	[Token(Token = "0x17000037")]
    	public static Navigation DefaultNavigation
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCEA6A0 -- zeroes the 0x28-byte Navigation (mode None, no targets)
    		[Token(Token = "0x600012B")]
    		[Address(RVA = "0xCEA6A0", Offset = "0xCE90A0", VA = "0x180CEA6A0")]
    		get
    		{
    			return default(Navigation);
    		}
    	}

    	[Token(Token = "0x17000038")]
    	public bool IsActive
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xCEA6C0 -- Application.IsPlaying(Object) icall on _gameObject (+0x20; the
    		//   ArgumentNullException "obj" path names it), then GameObject.activeInHierarchy
    		// Annotated disassembly: work/title-path/disas/su_02_IsActive.txt
    		[Token(Token = "0x600012C")]
    		[Address(RVA = "0xCEA6C0", Offset = "0xCE90C0", VA = "0x180CEA6C0")]
    		get
    		{
    			return Application.IsPlaying(_gameObject) && _gameObject.activeInHierarchy;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEA820 -- new ReactiveProperty<bool>(false) -> +0x10,
    	//   new ReactiveProperty<bool>(true) -> +0x18, go -> +0x20
    	[Token(Token = "0x600012D")]
    	[Address(RVA = "0xCEA820", Offset = "0xCE9220", VA = "0x180CEA820")]
    	public SelectableUtility(GameObject go)
    	{
    		_isHighlighted = new ReactiveProperty<bool>(false);
    		_isEnabled = new ReactiveProperty<bool>(true);
    		_gameObject = go;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEA9F0 -- ReactiveProperty<bool> +0x10 = false, +0x18 = true
    	[Token(Token = "0x600012E")]
    	[Address(RVA = "0xCEA9F0", Offset = "0xCE93F0", VA = "0x180CEA9F0")]
    	public void OnStateTransitionToNormal()
    	{
    		_isHighlighted.Value = false;
    		_isEnabled.Value = true;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEAA70 -- +0x10 = true, +0x18 = true
    	[Token(Token = "0x600012F")]
    	[Address(RVA = "0xCEAA70", Offset = "0xCE9470", VA = "0x180CEAA70")]
    	public void OnStateTransitionToHighlighted()
    	{
    		_isHighlighted.Value = true;
    		_isEnabled.Value = true;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEAAF0 -- +0x10 = true, +0x18 = true
    	[Token(Token = "0x6000130")]
    	[Address(RVA = "0xCEAAF0", Offset = "0xCE94F0", VA = "0x180CEAAF0")]
    	public void OnStateTransitionToPressed()
    	{
    		_isHighlighted.Value = true;
    		_isEnabled.Value = true;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEAB70 -- +0x10 = true, +0x18 = true
    	[Token(Token = "0x6000131")]
    	[Address(RVA = "0xCEAB70", Offset = "0xCE9570", VA = "0x180CEAB70")]
    	public void OnStateTransitionToSelected()
    	{
    		_isHighlighted.Value = true;
    		_isEnabled.Value = true;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCEABF0 -- +0x10 = false, +0x18 = false
    	[Token(Token = "0x6000132")]
    	[Address(RVA = "0xCEABF0", Offset = "0xCE95F0", VA = "0x180CEABF0")]
    	public void OnStateTransitionToDisabled()
    	{
    		_isHighlighted.Value = false;
    		_isEnabled.Value = false;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x33FBC0 (shared) -- mov rax, [rcx+0x10]
    	[Token(Token = "0x6000133")]
    	[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    	public IObservable<bool> OnHighlightingChangedAsObservable()
    	{
    		return _isHighlighted;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (shared) -- mov rax, [rcx+0x18]
    	[Token(Token = "0x6000134")]
    	[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    	public IObservable<bool> OnEnablingChangedAsObservable()
    	{
    		return _isEnabled;
    	}
    }
}
