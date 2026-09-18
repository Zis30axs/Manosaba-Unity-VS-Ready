using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Views
{
    [Token(Token = "0x20000F9")]
    public class MapAreaModifier : MonoBehaviour
    {
    	[Token(Token = "0x40004A6")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private MapArea[] _mapAreas;

    	[Token(Token = "0x40004A7")]
    	[FieldOffset(Offset = "0x28")]
    	[SerializeField]
    	private float[] _labelMaxWidths;

    	[Token(Token = "0x40004A8")]
    	[FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private bool _showFocusButton;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36E920 (identical-code-folded with ResetReferences)
    	[Token(Token = "0x60003EF")]
    	[Address(RVA = "0x36E920", Offset = "0x36D320", VA = "0x18036E920")]
    	private void Reset()
    	{
    		ResetReferences();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36E920
    	// 
    	// `gameObject.GetComponentsInChildren<MapArea>(includeInactive: dl=1)` - the
    	// inlined `Component.GetComponentsInChildren<T>(bool)` wrapper.
    	[Token(Token = "0x60003F0")]
    	[Address(RVA = "0x36E920", Offset = "0x36D320", VA = "0x18036E920")]
    	public void ResetReferences()
    	{
    		_mapAreas = GetComponentsInChildren<MapArea>(true);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x36E9F0
    	// 
    	// Array loop over `_mapAreas` reading `_showFocusButton` (+0x30) and
    	// `_labelMaxWidths` (+0x28) per iteration; the body is MapArea.ModifyInEditor
    	// (0x36DF50) inlined (same DisplayClass10_0, undo names and closures).
    	[Token(Token = "0x60003F1")]
    	[Address(RVA = "0x36E9F0", Offset = "0x36D3F0", VA = "0x18036E9F0")]
    	public void ModifyInEditor()
    	{
    		MapArea[] mapAreas = _mapAreas;
    		foreach (MapArea mapArea in mapAreas)
    		{
    			mapArea.ModifyInEditor(_showFocusButton, _labelMaxWidths);
    		}
    	}

    	[Token(Token = "0x60003F2")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public MapAreaModifier()
    	{
    	}
    }
}
