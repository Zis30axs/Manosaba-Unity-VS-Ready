using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x200068A")]
[CommandAlias("slide")]
public class SlideActor : Command
{
	[StructLayout(3)]
	[Token(Token = "0x200068C")]
	[CompilerGenerated]
	private struct _003CSlide_003Ed__9
{
		[Token(Token = "0x40018FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40018FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001900")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SlideActor _003C_003E4__this;

		[Token(Token = "0x4001901")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001902")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private PooledObject<List<UniTask>> _003C__003E5__2;

		[Token(Token = "0x4001903")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001FFF")]
		[Address(RVA = "0x4DD040", Offset = "0x4DBA40", VA = "0x1804DD040", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6002000")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40018F4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[RequiredParameter]
	[ActorContext("*", 0, null)]
	[AppearanceContext(1, null, null)]
	public NamedStringParameter IdAndAppearance;

	[Token(Token = "0x40018F5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterAlias("from")]
	[VectorContext("X,Y,Z", -1, null)]
	public DecimalListParameter FromPosition;

	[Token(Token = "0x40018F6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[ParameterAlias("to")]
	[VectorContext("X,Y,Z", -1, null)]
	[RequiredParameter]
	public DecimalListParameter ToPosition;

	[Token(Token = "0x40018F7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	public BooleanParameter Visible;

	[Token(Token = "0x40018F8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[ParameterAlias("easing")]
	[ConstantContext(typeof(EasingType), -1, null)]
	public StringParameter EasingTypeName;

	[Token(Token = "0x40018F9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[ParameterAlias("time")]
	[ParameterDefaultValue("0.35")]
	public DecimalParameter Duration;

	[Token(Token = "0x40018FA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[ParameterDefaultValue("false")]
	public BooleanParameter Lazy;

	[Token(Token = "0x40018FB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	public BooleanParameter Wait;

	[Token(Token = "0x6001FF9")]
	[Address(RVA = "0x4DCC90", Offset = "0x4DB690", VA = "0x1804DCC90", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001FFA")]
	[Address(RVA = "0x4DCD40", Offset = "0x4DB740", VA = "0x1804DCD40", Slot = "16")]
	[AsyncStateMachine(typeof(_003CSlide_003Ed__9))]
	protected virtual UniTask Slide(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001FFB")]
	[Address(RVA = "0x4DCED0", Offset = "0x4DB8D0", VA = "0x1804DCED0")]
	public SlideActor()
	{
	}
}
