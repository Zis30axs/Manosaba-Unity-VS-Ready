using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;
using WitchTrials.Models;

namespace WitchTrials.Views
{
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
    	// RECONSTRUCTED | Confidence: A | RVA: 0x36EC40
    	//
    	// Lazily-cached: `_areas ??= _mapAreas.ToDictionary(m => m.AreaKind, m => m);`.
    	// As with WitchBookScreen.PageData, most of the 791-byte body is Roslyn's
    	// standard "cache a non-capturing lambda in a static field of the
    	// compiler-generated <>c class" boilerplate, not meaningful logic.
    	// The element selector `Func<MapArea, MapArea>` (same in/out type, no other
    	// MapArea property it could plausibly be) is the identity lambda `m => m`,
    	// which is why the source needed the two-selector ToDictionary overload
    	// (METHODINFO confirms `ToDictionary<MapArea, MapAreaKind, MapArea>`)
    	// rather than the more common single-selector one.
    	public IReadOnlyDictionary<MapAreaKind, MapArea> Areas
    	{
    		[Token(Token = "0x60003F3")]
    		[Address(RVA = "0x36EC40", Offset = "0x36D640", VA = "0x18036EC40")]
    		get
    		{
    			return _areas ??= _mapAreas.ToDictionary(m => m.AreaKind, m => m);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36EF60
    	// 
    	// GetComponent<Canvas> / GetComponent<GraphicRaycaster> / the parameterless
    	// GetComponentsInChildren<MapArea>() (unlike MapAreaModifier, no includeInactive).
    	[Token(Token = "0x60003F4")]
    	[Address(RVA = "0x36EF60", Offset = "0x36D960", VA = "0x18036EF60")]
    	private void Reset()
    	{
    		_canvas = GetComponent<Canvas>();
    		_graphicRaycaster = GetComponent<GraphicRaycaster>();
    		_mapAreas = GetComponentsInChildren<MapArea>();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36F100
    	// 
    	// Enables canvas and raycaster (set_enabled dl=1), then iterates `Areas` through
    	// `IEnumerable<KeyValuePair<MapAreaKind, MapArea>>` with `KeyValuePair.Deconstruct`.
    	// A null `restriction` or `Enumerable.Contains<MapAreaKind>` true jumps to the
    	// alpha-1 path (MapArea.Show inlined); otherwise alpha 0 (MapArea.Hide inlined).
    	[Token(Token = "0x60003F5")]
    	[Address(RVA = "0x36F100", Offset = "0x36DB00", VA = "0x18036F100")]
    	public void Show([Optional] IReadOnlyCollection<MapAreaKind> restriction)
    	{
    		_canvas.enabled = true;
    		_graphicRaycaster.enabled = true;
    		foreach (var (kind, mapArea) in Areas)
    		{
    			if (restriction == null || restriction.Contains(kind))
    			{
    				mapArea.Show();
    			}
    			else
    			{
    				mapArea.Hide();
    			}
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36F420 (folded with WitchBookPageBase.Hide)
    	[Token(Token = "0x60003F6")]
    	[Address(RVA = "0x36F420", Offset = "0x36DE20", VA = "0x18036F420")]
    	public void Hide()
    	{
    		_canvas.enabled = false;
    		_graphicRaycaster.enabled = false;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36F460
    	// 
    	// `pair` null -> NRE (instance call on `pair`); then IdVersionPair.Equals inlined:
    	// reference-equal -> true, `other` (`_idAndVersion`, +0x38) null -> false, string
    	// Id compared via SequenceEqual, then int Version (+0x18).
    	[Token(Token = "0x60003F7")]
    	[Address(RVA = "0x36F460", Offset = "0x36DE60", VA = "0x18036F460")]
    	public bool IsMatch(IdVersionPair pair)
    	{
    		return pair.Equals(_idAndVersion);
    	}

    	[Token(Token = "0x60003F8")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public MapFloor()
    	{
    	}
    }
}
