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
		[Token(Token = "0x60002B3")]
		[Address(RVA = "0x34F670", Offset = "0x34E070", VA = "0x18034F670")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700004F")]
	private ICancelEventBlockable[] ParentBlockables
	{
		[Token(Token = "0x60002B4")]
		[Address(RVA = "0x34F800", Offset = "0x34E200", VA = "0x18034F800")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60002B5")]
	[Address(RVA = "0x34F8B0", Offset = "0x34E2B0", VA = "0x18034F8B0", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x60002B6")]
	[Address(RVA = "0x34F990", Offset = "0x34E390", VA = "0x18034F990", Slot = "68")]
	public override void Show(IEnumerable<Texture2D> textures)
	{
	}

	[Token(Token = "0x60002B7")]
	[Address(RVA = "0x34FB80", Offset = "0x34E580", VA = "0x18034FB80", Slot = "48")]
	public override void Hide()
	{
	}

	[Token(Token = "0x60002B8")]
	[Address(RVA = "0x34FC60", Offset = "0x34E660", VA = "0x18034FC60", Slot = "71")]
	protected override void ShowNextTexture(float duration)
	{
	}

	[Token(Token = "0x60002B9")]
	[Address(RVA = "0x34FDF0", Offset = "0x34E7F0", VA = "0x18034FDF0")]
	public GalleryStillViewerPanel()
	{
	}
}
