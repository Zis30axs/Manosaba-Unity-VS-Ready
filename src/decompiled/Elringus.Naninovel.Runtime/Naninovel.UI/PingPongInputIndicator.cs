using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI;

[Token(Token = "0x200059B")]
public class PingPongInputIndicator : ScriptableUIComponent<RawImage>, IInputIndicator
{
	[Token(Token = "0x4001578")]
	[FieldOffset(Offset = "0x88")]
	[Tooltip("Whether to tint the image in ping and pong colors when visible.")]
	[SerializeField]
	private bool tintPingPong;

	[Token(Token = "0x4001579")]
	[FieldOffset(Offset = "0x8C")]
	[SerializeField]
	private Color pingColor;

	[Token(Token = "0x400157A")]
	[FieldOffset(Offset = "0x9C")]
	[SerializeField]
	private Color pongColor;

	[Token(Token = "0x400157B")]
	[FieldOffset(Offset = "0xAC")]
	[SerializeField]
	private float pingPongTime;

	[Token(Token = "0x400157C")]
	[FieldOffset(Offset = "0xB0")]
	[SerializeField]
	private float revealTime;

	[Token(Token = "0x400157D")]
	[FieldOffset(Offset = "0xB4")]
	private float showTime;

	[Token(Token = "0x17000403")]
	protected virtual bool TintPingPong
	{
		[Token(Token = "0x6001BDA")]
		[Address(RVA = "0x496510", Offset = "0x494F10", VA = "0x180496510", Slot = "64")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x17000404")]
	protected virtual Color PingColor
	{
		[Token(Token = "0x6001BDB")]
		[Address(RVA = "0x486B60", Offset = "0x485560", VA = "0x180486B60", Slot = "65")]
		get
		{
			return default(Color);
		}
	}

	[Token(Token = "0x17000405")]
	protected virtual Color PongColor
	{
		[Token(Token = "0x6001BDC")]
		[Address(RVA = "0x496520", Offset = "0x494F20", VA = "0x180496520", Slot = "66")]
		get
		{
			return default(Color);
		}
	}

	[Token(Token = "0x17000406")]
	protected virtual float PingPongTime
	{
		[Token(Token = "0x6001BDD")]
		[Address(RVA = "0x496530", Offset = "0x494F30", VA = "0x180496530", Slot = "67")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x17000407")]
	protected virtual float RevealTime
	{
		[Token(Token = "0x6001BDE")]
		[Address(RVA = "0x496540", Offset = "0x494F40", VA = "0x180496540", Slot = "68")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x6001BDF")]
	[Address(RVA = "0x496550", Offset = "0x494F50", VA = "0x180496550", Slot = "47")]
	public override void Show()
	{
	}

	[Token(Token = "0x6001BE0")]
	[Address(RVA = "0x496690", Offset = "0x495090", VA = "0x180496690", Slot = "48")]
	public override void Hide()
	{
	}

	[Token(Token = "0x6001BE1")]
	[Address(RVA = "0x4966B0", Offset = "0x4950B0", VA = "0x1804966B0", Slot = "53")]
	protected override void Update()
	{
	}

	[Token(Token = "0x6001BE2")]
	[Address(RVA = "0x496910", Offset = "0x495310", VA = "0x180496910")]
	public PingPongInputIndicator()
	{
	}
}
