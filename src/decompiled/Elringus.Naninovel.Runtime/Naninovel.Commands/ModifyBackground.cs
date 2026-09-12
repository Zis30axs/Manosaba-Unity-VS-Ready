using Il2CppDummyDll;

namespace Naninovel.Commands;

[Token(Token = "0x200067B")]
[CommandAlias("back")]
[ActorContext("Backgrounds", -1, "Id")]
[ConstantContext("Poses/Backgrounds/{:Id??MainBackground}+Poses/Backgrounds/*", -1, "Pose")]
public class ModifyBackground : ModifyOrthoActor<IBackgroundActor, BackgroundState, BackgroundMetadata, BackgroundsConfiguration, IBackgroundManager>
{
	[Token(Token = "0x40018C4")]
	[FieldOffset(Offset = "0xC8")]
	[ParameterAlias("")]
	[AppearanceContext(0, null, "MainBackground")]
	[ConstantContext(typeof(TransitionType), 1, null)]
	public NamedStringParameter AppearanceAndTransition;

	[Token(Token = "0x170004B6")]
	protected override bool AllowPreload
	{
		[Token(Token = "0x6001FC1")]
		[Address(RVA = "0x4D84A0", Offset = "0x4D6EA0", VA = "0x1804D84A0", Slot = "30")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x170004B7")]
	protected override string AssignedId
	{
		[Token(Token = "0x6001FC2")]
		[Address(RVA = "0x4D8650", Offset = "0x4D7050", VA = "0x1804D8650", Slot = "18")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004B8")]
	protected override string AlternativeAppearance
	{
		[Token(Token = "0x6001FC3")]
		[Address(RVA = "0x4D86C0", Offset = "0x4D70C0", VA = "0x1804D86C0", Slot = "29")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170004B9")]
	protected override string AssignedTransition
	{
		[Token(Token = "0x6001FC4")]
		[Address(RVA = "0x4D8710", Offset = "0x4D7110", VA = "0x1804D8710", Slot = "19")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001FC5")]
	[Address(RVA = "0x4D8860", Offset = "0x4D7260", VA = "0x1804D8860")]
	public ModifyBackground()
	{
	}
}
