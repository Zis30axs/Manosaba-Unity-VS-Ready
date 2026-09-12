using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x2000569")]
public class RollbackUI : CustomUI, IRollbackUI, IManagedUI
{
	[Token(Token = "0x4001499")]
	[FieldOffset(Offset = "0xE0")]
	[SerializeField]
	private float hideTime;

	[Token(Token = "0x400149A")]
	[FieldOffset(Offset = "0xE8")]
	private IStateManager stateManager;

	[Token(Token = "0x400149B")]
	[FieldOffset(Offset = "0xF0")]
	private Timer hideTimer;

	[Token(Token = "0x170003C7")]
	protected float HideTime
	{
		[Token(Token = "0x6001AC8")]
		[Address(RVA = "0x62F690", Offset = "0x62E090", VA = "0x18062F690")]
		get
		{
			return default(float);
		}
	}

	[Token(Token = "0x6001AC9")]
	[Address(RVA = "0x62F6A0", Offset = "0x62E0A0", VA = "0x18062F6A0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6001ACA")]
	[Address(RVA = "0x62F860", Offset = "0x62E260", VA = "0x18062F860", Slot = "5")]
	protected override void OnEnable()
	{
	}

	[Token(Token = "0x6001ACB")]
	[Address(RVA = "0x62F960", Offset = "0x62E360", VA = "0x18062F960", Slot = "7")]
	protected override void OnDisable()
	{
	}

	[Token(Token = "0x6001ACC")]
	[Address(RVA = "0x62FA60", Offset = "0x62E460", VA = "0x18062FA60", Slot = "95")]
	protected virtual void HandleRollbackStarted()
	{
	}

	[Token(Token = "0x6001ACD")]
	[Address(RVA = "0x62FAF0", Offset = "0x62E4F0", VA = "0x18062FAF0", Slot = "96")]
	protected virtual void HandleRollbackFinished()
	{
	}

	[Token(Token = "0x6001ACE")]
	[Address(RVA = "0x62FB90", Offset = "0x62E590", VA = "0x18062FB90")]
	public RollbackUI()
	{
	}
}
