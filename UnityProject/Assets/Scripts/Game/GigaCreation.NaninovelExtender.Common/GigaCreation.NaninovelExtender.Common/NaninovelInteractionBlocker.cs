using System;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.UI;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Common
{
    [Token(Token = "0x2000019")]
    public class NaninovelInteractionBlocker : CancelEventBlocker, INaninovelInteractionBlocker, IDisposable
    {
    	[Token(Token = "0x4000045")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly IClickThroughPanelExtended _clickThroughPanel;

    	[Token(Token = "0x4000046")]
    	[FieldOffset(Offset = "0x20")]
    	private int? _originalSortingOrder;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCFBE30 -- Array.Empty<ICancelEventBlockable>(), Array.Empty<string>(), tail jump
    	//   to the full constructor (0xCFC030)
    	[Token(Token = "0x6000034")]
    	[Address(RVA = "0xCFBE30", Offset = "0xCFA830", VA = "0x180CFBE30")]
    	public NaninovelInteractionBlocker()
    		: this(Array.Empty<ICancelEventBlockable>(), Array.Empty<string>())
    	{
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCFBEB0 -- new ICancelEventBlockable[1] { blockable }, Array.Empty<string>()
    	[Token(Token = "0x6000035")]
    	[Address(RVA = "0xCFBEB0", Offset = "0xCFA8B0", VA = "0x180CFBEB0")]
    	public NaninovelInteractionBlocker(ICancelEventBlockable blockable)
    		: this(new ICancelEventBlockable[] { blockable }, Array.Empty<string>())
    	{
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCFBFD0 -- Array.Empty<ICancelEventBlockable>(), allowedSamplers
    	[Token(Token = "0x6000036")]
    	[Address(RVA = "0xCFBFD0", Offset = "0xCFA9D0", VA = "0x180CFBFD0")]
    	public NaninovelInteractionBlocker(params string[] allowedSamplers)
    		: this(Array.Empty<ICancelEventBlockable>(), allowedSamplers)
    	{
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCFC030
    	// - CancelEventBlocker(blockables) (0xCDB6B0).
    	// - Engine.TryGetService<IUIManager> and UIManagerExtensions.TryGetUI<IClickThroughPanelExtended> inlined (the
    	//   panel goes straight to +0x18); each miss logs its warning and stops.
    	// - IManagedUI slot 2 Visible; when hidden, IClickThroughPanel.Show(hideOnClick: false, onClick: null, allowedSamplers).
    	// Annotated disassembly: work/title-path/disas/nib_04_ctor_full.txt
    	[Token(Token = "0x6000037")]
    	[Address(RVA = "0xCFC030", Offset = "0xCFAA30", VA = "0x180CFC030")]
    	public NaninovelInteractionBlocker(ICancelEventBlockable[] blockables, params string[] allowedSamplers)
    		: base(blockables)
    	{
    		if (!Engine.TryGetService<IUIManager>(out IUIManager uiManager))
    		{
    			Debug.LogWarning("Naninovel UIManager not found.");
    			return;
    		}
    		if (!uiManager.TryGetUI<IClickThroughPanelExtended>(out _clickThroughPanel))
    		{
    			Debug.LogWarning("Naninovel ClickThroughPanelExtended not found.");
    			return;
    		}
    		if (!_clickThroughPanel.Visible)
    		{
    			_clickThroughPanel.Show(false, null, allowedSamplers);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCFC310 -- IClickThroughPanelExtended slot 0 Exists; the current IManagedUI slot 7
    	//   SortingOrder saved to +0x20 (int?), then slot 2 SetSortingOrder(order)
    	[Token(Token = "0x6000038")]
    	[Address(RVA = "0xCFC310", Offset = "0xCFAD10", VA = "0x180CFC310", Slot = "6")]
    	public void SetSortingOrder(int order)
    	{
    		if (_clickThroughPanel.Exists)
    		{
    			_originalSortingOrder = _clickThroughPanel.SortingOrder;
    			_clickThroughPanel.SetSortingOrder(order);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCFC400 -- when the panel still exists: IClickThroughPanel slot 1 Hide, and the
    	//   saved sorting order restored; then tail jump CancelEventBlocker.Dispose (0xCDB820)
    	// Annotated disassembly: work/title-path/disas/nib_06_Dispose.txt
    	[Token(Token = "0x6000039")]
    	[Address(RVA = "0xCFC400", Offset = "0xCFAE00", VA = "0x180CFC400", Slot = "5")]
    	public override void Dispose()
    	{
    		if (_clickThroughPanel.Exists)
    		{
    			_clickThroughPanel.Hide();
    			if (_originalSortingOrder.HasValue)
    			{
    				_clickThroughPanel.SetSortingOrder(_originalSortingOrder.Value);
    			}
    		}
    		base.Dispose();
    	}
    }
}
