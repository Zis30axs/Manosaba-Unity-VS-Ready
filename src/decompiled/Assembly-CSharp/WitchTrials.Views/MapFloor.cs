using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x20000FA")]
public class MapFloor : MonoBehaviour
{
	[Token(Token = "0x40004A9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private Canvas _canvas;

	[Token(Token = "0x40004AA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private GraphicRaycaster _graphicRaycaster;

	[Token(Token = "0x40004AB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private MapArea[] _mapAreas;

	[Token(Token = "0x40004AC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[Header("Parameters")]
	[SerializeField]
	private IdVersionPair _idAndVersion;

	[Token(Token = "0x40004AD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private IReadOnlyDictionary<MapAreaKind, MapArea> _areas;

	[Token(Token = "0x17000086")]
	public IReadOnlyDictionary<MapAreaKind, MapArea> Areas
	{
		[Token(Token = "0x60003F3")]
		[Address(RVA = "0x36EC40", Offset = "0x36D640", VA = "0x18036EC40")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60003F4")]
	[Address(RVA = "0x36EF60", Offset = "0x36D960", VA = "0x18036EF60")]
	private void Reset()
	{
	}

	[Token(Token = "0x60003F5")]
	[Address(RVA = "0x36F100", Offset = "0x36DB00", VA = "0x18036F100")]
	public void Show([Optional] IReadOnlyCollection<MapAreaKind> restriction)
	{
	}

	[Token(Token = "0x60003F6")]
	[Address(RVA = "0x36F420", Offset = "0x36DE20", VA = "0x18036F420")]
	public void Hide()
	{
	}

	[Token(Token = "0x60003F7")]
	[Address(RVA = "0x36F460", Offset = "0x36DE60", VA = "0x18036F460")]
	public bool IsMatch(IdVersionPair pair)
	{
		return default(bool);
	}

	[Token(Token = "0x60003F8")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public MapFloor()
	{
	}
}
