using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI;

[Token(Token = "0x2000508")]
public class CGViewerPanel : ScriptableButton
{
	[Token(Token = "0x4001397")]
	[FieldOffset(Offset = "0xA8")]
	[Tooltip("The image where the assigned CGs will be shown.")]
	[SerializeField]
	private RawImage contentImage;

	[Token(Token = "0x4001398")]
	[FieldOffset(Offset = "0xB0")]
	[Tooltip("When multiple CGs assigned, controls crossfade duration, in seconds.")]
	[SerializeField]
	private float crossfadeDuration;

	[Token(Token = "0x17000394")]
	protected virtual string ShaderName
	{
		[Token(Token = "0x6001915")]
		[Address(RVA = "0x468E20", Offset = "0x467820", VA = "0x180468E20", Slot = "63")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000395")]
	protected virtual RawImage ContentImage
	{
		[Token(Token = "0x6001916")]
		[Address(RVA = "0x471780", Offset = "0x470180", VA = "0x180471780", Slot = "64")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000396")]
	protected virtual float CrossfadeDuration
	{
		[Token(Token = "0x6001917")]
		[Address(RVA = "0x496540", Offset = "0x494F40", VA = "0x180496540", Slot = "65")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x17000397")]
	protected virtual Queue<Texture2D> TextureQueue
	{
		[Token(Token = "0x6001918")]
		[Address(RVA = "0x492EE0", Offset = "0x4918E0", VA = "0x180492EE0", Slot = "66")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000398")]
	protected virtual ImageCrossfader Crossfader
	{
		[Token(Token = "0x6001919")]
		[Address(RVA = "0x33B470", Offset = "0x339E70", VA = "0x18033B470", Slot = "67")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x600191A")]
		[Address(RVA = "0x33B480", Offset = "0x339E80", VA = "0x18033B480")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x600191B")]
	[Address(RVA = "0x614710", Offset = "0x613110", VA = "0x180614710", Slot = "68")]
	public virtual void Show(IEnumerable<Texture2D> textures)
	{
	}

	[Token(Token = "0x600191C")]
	[Address(RVA = "0x6147C0", Offset = "0x6131C0", VA = "0x1806147C0", Slot = "69")]
	public virtual void ShowNextOrHide()
	{
	}

	[Token(Token = "0x600191D")]
	[Address(RVA = "0x614860", Offset = "0x613260", VA = "0x180614860", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x600191E")]
	[Address(RVA = "0x6149D0", Offset = "0x6133D0", VA = "0x1806149D0", Slot = "8")]
	protected override void OnDestroy()
	{
	}

	[Token(Token = "0x600191F")]
	[Address(RVA = "0x614A00", Offset = "0x613400", VA = "0x180614A00", Slot = "62")]
	protected override void OnButtonClick()
	{
	}

	[Token(Token = "0x6001920")]
	[Address(RVA = "0x614A20", Offset = "0x613420", VA = "0x180614A20", Slot = "55")]
	protected override void HandleVisibilityChanged(bool visible)
	{
	}

	[Token(Token = "0x6001921")]
	[Address(RVA = "0x614AF0", Offset = "0x6134F0", VA = "0x180614AF0", Slot = "70")]
	protected virtual void EnqueueTextures(IEnumerable<Texture2D> textures)
	{
	}

	[Token(Token = "0x6001922")]
	[Address(RVA = "0x614DF0", Offset = "0x6137F0", VA = "0x180614DF0", Slot = "71")]
	protected virtual void ShowNextTexture(float duration)
	{
	}

	[Token(Token = "0x6001923")]
	[Address(RVA = "0x34FDF0", Offset = "0x34E7F0", VA = "0x18034FDF0")]
	public CGViewerPanel()
	{
	}
}
