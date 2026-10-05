using Il2CppDummyDll;
using System.Linq;
using UnityEngine;

namespace GigaCreation.Tools.Debugging.Ui;

[Token(Token = "0x2000007")]
public class DebugPanelExclusive : DebugPanel
{
	[Token(Token = "0x400000F")]
	[FieldOffset(Offset = "0x48")]
	private Transform[] _otherDebugPanels;

	[Token(Token = "0x4000010")]
	[FieldOffset(Offset = "0x50")]
	private bool _isQuitting;

	// RECONSTRUCTED | Confidence: A | RVA: 0xD47FF0, 816 B; lambdas <get_OtherDebugPanels>b__3_0 0xD48750
	//   (Unity `panel != this`) and <>c.b__3_1 0xD48930 (`panel.transform`).
	// - lazily +0x48 = FindObjectsByType<DebugPanelExclusive>(FindObjectsSortMode.None (ecx 0))
	//   .Where(b__3_0).Select(b__3_1).ToArray().
	// Annotated disassembly: work/title-path/disas/dbgp_*.txt (regenerate with work/reversing/disas.py).
	[Token(Token = "0x17000001")]
	private Transform[] OtherDebugPanels
	{
		[Token(Token = "0x6000011")]
		[Address(RVA = "0xD47FF0", Offset = "0xD469F0", VA = "0x180D47FF0")]
		get
		{
			if (_otherDebugPanels == null)
			{
				_otherDebugPanels = FindObjectsByType<DebugPanelExclusive>(FindObjectsSortMode.None)
					.Where(panel => panel != this)
					.Select(panel => panel.transform)
					.ToArray();
			}
			return _otherDebugPanels;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0xD48320, 341 B -- unless `_isQuitting` (+0x50), every other panel's
	//   localScale = Vector3.one (static +0xC), so destroying an expanded panel does not leave the others hidden.
	[Token(Token = "0x6000012")]
	[Address(RVA = "0xD48320", Offset = "0xD46D20", VA = "0x180D48320")]
	private void OnDestroy()
	{
		if (_isQuitting)
		{
			return;
		}
		foreach (Transform otherDebugPanel in OtherDebugPanels)
		{
			otherDebugPanel.localScale = Vector3.one;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0xD48480, 5 B -- `_isQuitting` (+0x50) = 1.
	[Token(Token = "0x6000013")]
	[Address(RVA = "0xD48480", Offset = "0xD46E80", VA = "0x180D48480")]
	private void OnApplicationQuit()
	{
		_isQuitting = true;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0xD48490, 333 B -- direct base call (0xD47830), then every other
	//   panel's localScale = Vector3.zero (static +0x0): only one exclusive panel is expanded at a time.
	[Token(Token = "0x6000014")]
	[Address(RVA = "0xD48490", Offset = "0xD46E90", VA = "0x180D48490", Slot = "4")]
	public override void ShowContent()
	{
		base.ShowContent();
		foreach (Transform otherDebugPanel in OtherDebugPanels)
		{
			otherDebugPanel.localScale = Vector3.zero;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0xD485F0, 333 B -- direct base call (0xD47BB0), then every other
	//   panel's localScale = Vector3.one (static +0xC).
	[Token(Token = "0x6000015")]
	[Address(RVA = "0xD485F0", Offset = "0xD46FF0", VA = "0x180D485F0", Slot = "5")]
	public override void HideContent()
	{
		base.HideContent();
		foreach (Transform otherDebugPanel in OtherDebugPanels)
		{
			otherDebugPanel.localScale = Vector3.one;
		}
	}

	[Token(Token = "0x6000016")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugPanelExclusive()
	{
	}
}
