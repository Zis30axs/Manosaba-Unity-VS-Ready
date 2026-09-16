using Naninovel;
using System;
using System.Collections.Generic;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using Naninovel.UI;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x20000BF")]
public class GalleryStillViewerPanel : CGViewerPanel
{
	[Token(Token = "0x400035F")]
	[FieldOffset(Offset = "0xB8")]
	private RectTransform _viewerRectTransform;

	[Token(Token = "0x4000360")]
	[FieldOffset(Offset = "0xC0")]
	private ICancelEventBlockable[] _parentBlockables;

	[Token(Token = "0x4000361")]
	[FieldOffset(Offset = "0xC8")]
	private IDisposable _cancelEventBlockerDisposable;

	[Token(Token = "0x1700004E")]
	private RectTransform ViewerRectTransform
	{
		// RECONSTRUCTED | Confidence: A (behaviour), B (if/return spelling) | RVA: 0x34F670, 388 B
		// Same shape as GalleryStillGridSlot.ThumbnailRectTransform over CGViewerPanel slot 64 `ContentImage`.
		[Token(Token = "0x60002B3")]
		[Address(RVA = "0x34F670", Offset = "0x34E070", VA = "0x18034F670")]
		get
		{
			if (_viewerRectTransform != null)
			{
				return _viewerRectTransform;
			}
			return _viewerRectTransform = ContentImage.transform as RectTransform;
		}
	}

	[Token(Token = "0x1700004F")]
	private ICancelEventBlockable[] ParentBlockables
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x34F800, 170 B (plain reference null test, +0xC0)
		[Token(Token = "0x60002B4")]
		[Address(RVA = "0x34F800", Offset = "0x34E200", VA = "0x18034F800")]
		get
		{
			return _parentBlockables ??= GetComponentsInParent<ICancelEventBlockable>();
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x34F8B0, 220 B
	// base.OnDisable() is inlined (ScriptableUIControl<Button>: the virtual UnbindUIEvents, slot 61); the blocker (+0xC8) is
	// disposed and cleared only when present.
	[Token(Token = "0x60002B5")]
	[Address(RVA = "0x34F8B0", Offset = "0x34E2B0", VA = "0x18034F8B0", Slot = "7")]
	protected override void OnDisable()
	{
		base.OnDisable();
		if (_cancelEventBlockerDisposable != null)
		{
			_cancelEventBlockerDisposable.Dispose();
			_cancelEventBlockerDisposable = null;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x34F990, 495 B
	// 
	// A CancelEventBlocker over the parent blockables (getter inlined) is kept until Hide/OnDisable. The rest is
	// CGViewerPanel.Show (0x614710) inlined instruction for instruction: contentImage.texture = null, EnqueueTextures
	// (slot 70), ShowNextTexture(0) (slot 71), ChangeVisibility(true) (slot 44) -> Forget().
	[Token(Token = "0x60002B6")]
	[Address(RVA = "0x34F990", Offset = "0x34E390", VA = "0x18034F990", Slot = "68")]
	public override void Show(IEnumerable<Texture2D> textures)
	{
		_cancelEventBlockerDisposable = new CancelEventBlocker(ParentBlockables);
		base.Show(textures);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x34FB80, 217 B
	// base.Hide() (ChangeVisibility(false) -> Forget()) inlined first; the blocker is then disposed (`?.`) and cleared.
	[Token(Token = "0x60002B7")]
	[Address(RVA = "0x34FB80", Offset = "0x34E580", VA = "0x18034FB80", Slot = "48")]
	public override void Hide()
	{
		base.Hide();
		_cancelEventBlockerDisposable?.Dispose();
		_cancelEventBlockerDisposable = null;
	}

	// RECONSTRUCTED | Confidence: A (behaviour), B (local split) | RVA: 0x34FC60, 395 B
	// 
	// Like CGViewerPanel.ShowNextTexture (0x614DF0: TextureQueue slot 66 Dequeue -> Crossfader slot 67 Crossfade ->
	// Forget), with the viewer resized to the texture's aspect ratio at the current width first. The call order
	// (second ViewerRectTransform read before `height`, its sizeDelta after) fixes the local split.
	[Token(Token = "0x60002B8")]
	[Address(RVA = "0x34FC60", Offset = "0x34E660", VA = "0x18034FC60", Slot = "71")]
	protected override void ShowNextTexture(float duration)
	{
		Texture2D texture = TextureQueue.Dequeue();
		float scale = ViewerRectTransform.sizeDelta.x / (float)texture.width;
		RectTransform viewerRectTransform = ViewerRectTransform;
		float height = (float)texture.height * scale;
		viewerRectTransform.sizeDelta = new Vector2(viewerRectTransform.sizeDelta.x, height);
		Crossfader.Crossfade(texture, duration).Forget();
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x34FDF0 (identical-code-folded with CGViewerPanel..ctor; no initializers of its own)
	[Token(Token = "0x60002B9")]
	[Address(RVA = "0x34FDF0", Offset = "0x34E7F0", VA = "0x18034FDF0")]
	public GalleryStillViewerPanel()
	{
	}
}
