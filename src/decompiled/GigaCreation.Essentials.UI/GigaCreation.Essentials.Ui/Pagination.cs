using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using TMPro;
using UniRx;
using UnityEngine;

namespace GigaCreation.Essentials.Ui;

[Token(Token = "0x2000036")]
public class Pagination : MonoBehaviour
{
	[Token(Token = "0x40000C2")]
	[FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private MovingArrow _previousPageButton;

	[Token(Token = "0x40000C3")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private MovingArrow _nextPageButton;

	[Token(Token = "0x40000C4")]
	[FieldOffset(Offset = "0x30")]
	[Space]
	[SerializeField]
	private PaginationIndicator[] _indicators;

	[Token(Token = "0x40000C5")]
	[FieldOffset(Offset = "0x38")]
	[SerializeField]
	private DotIndicator _originalDot;

	[Token(Token = "0x40000C6")]
	[FieldOffset(Offset = "0x40")]
	[SerializeField]
	private TextMeshProUGUI _currentPageLabel;

	[Token(Token = "0x40000C7")]
	[FieldOffset(Offset = "0x48")]
	[SerializeField]
	private TextMeshProUGUI _totalPageLabel;

	[Token(Token = "0x40000C8")]
	[FieldOffset(Offset = "0x50")]
	[Header("Parameters")]
	[SerializeField]
	private PaginationIndicatorStyle _indicatorStyle;

	[Token(Token = "0x40000C9")]
	[FieldOffset(Offset = "0x54")]
	[SerializeField]
	private bool _wrapped;

	[Token(Token = "0x40000CA")]
	[FieldOffset(Offset = "0x58")]
	private readonly ReactiveProperty<int> _currentPageIndex;

	[Token(Token = "0x40000CB")]
	[FieldOffset(Offset = "0x60")]
	private readonly CompositeDisposable _disposables;

	[Token(Token = "0x40000CC")]
	[FieldOffset(Offset = "0x68")]
	private readonly List<DotIndicator> _dots;

	[Token(Token = "0x40000CD")]
	[FieldOffset(Offset = "0x70")]
	private IInputManager _inputManager;

	[Token(Token = "0x1700002F")]
	public int CurrentPageIndex
	{
		[Token(Token = "0x60000EF")]
		[Address(RVA = "0xCE5BF0", Offset = "0xCE45F0", VA = "0x180CE5BF0")]
		get
		{
			return default(int);
		}
	}

	[Token(Token = "0x17000030")]
	private IInputManager InputManager
	{
		[Token(Token = "0x60000F0")]
		[Address(RVA = "0xCE5C40", Offset = "0xCE4640", VA = "0x180CE5C40")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60000F1")]
	[Address(RVA = "0xCE5D00", Offset = "0xCE4700", VA = "0x180CE5D00")]
	private void Awake()
	{
	}

	[Token(Token = "0x60000F2")]
	[Address(RVA = "0xCE5D00", Offset = "0xCE4700", VA = "0x180CE5D00")]
	private void ChangeIndicatorStyle()
	{
	}

	[Token(Token = "0x60000F3")]
	[Address(RVA = "0xCE5DA0", Offset = "0xCE47A0", VA = "0x180CE5DA0")]
	public void Present(int numOfTotalPage, bool resetCurrentPage = true)
	{
	}

	[Token(Token = "0x60000F4")]
	[Address(RVA = "0xCE62F0", Offset = "0xCE4CF0", VA = "0x180CE62F0")]
	private void ModifySpriteInDotsEnabledTo(int enableIndex)
	{
	}

	[Token(Token = "0x60000F5")]
	[Address(RVA = "0xCE63D0", Offset = "0xCE4DD0", VA = "0x180CE63D0")]
	public void Dismiss()
	{
	}

	[Token(Token = "0x60000F6")]
	[Address(RVA = "0xCE6530", Offset = "0xCE4F30", VA = "0x180CE6530")]
	public IObservable<int> OnPageChangedAsObservable()
	{
		return null;
	}

	[Token(Token = "0x60000F7")]
	[Address(RVA = "0xCE65A0", Offset = "0xCE4FA0", VA = "0x180CE65A0")]
	public Pagination()
	{
	}
}
