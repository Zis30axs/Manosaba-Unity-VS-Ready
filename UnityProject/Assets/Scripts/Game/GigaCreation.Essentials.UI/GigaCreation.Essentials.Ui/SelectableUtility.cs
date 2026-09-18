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
    		[Token(Token = "0x600012C")]
    		[Address(RVA = "0xCEA6C0", Offset = "0xCE90C0", VA = "0x180CEA6C0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x600012D")]
    	[Address(RVA = "0xCEA820", Offset = "0xCE9220", VA = "0x180CEA820")]
    	public SelectableUtility(GameObject go)
    	{
    	}

    	[Token(Token = "0x600012E")]
    	[Address(RVA = "0xCEA9F0", Offset = "0xCE93F0", VA = "0x180CEA9F0")]
    	public void OnStateTransitionToNormal()
    	{
    	}

    	[Token(Token = "0x600012F")]
    	[Address(RVA = "0xCEAA70", Offset = "0xCE9470", VA = "0x180CEAA70")]
    	public void OnStateTransitionToHighlighted()
    	{
    	}

    	[Token(Token = "0x6000130")]
    	[Address(RVA = "0xCEAAF0", Offset = "0xCE94F0", VA = "0x180CEAAF0")]
    	public void OnStateTransitionToPressed()
    	{
    	}

    	[Token(Token = "0x6000131")]
    	[Address(RVA = "0xCEAB70", Offset = "0xCE9570", VA = "0x180CEAB70")]
    	public void OnStateTransitionToSelected()
    	{
    	}

    	[Token(Token = "0x6000132")]
    	[Address(RVA = "0xCEABF0", Offset = "0xCE95F0", VA = "0x180CEABF0")]
    	public void OnStateTransitionToDisabled()
    	{
    	}

    	[Token(Token = "0x6000133")]
    	[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    	public IObservable<bool> OnHighlightingChangedAsObservable()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000134")]
    	[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    	public IObservable<bool> OnEnablingChangedAsObservable()
    	{
    		return null;
    	}
    }
}
