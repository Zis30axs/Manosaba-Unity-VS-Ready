using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands;

[Token(Token = "0x2000706")]
public class ShowUI : Command
{
	[StructLayout(3)]
	[Token(Token = "0x2000707")]
	[CompilerGenerated]
	private struct _003CShow_003Ed__4
{
		[Token(Token = "0x4001ADC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001ADD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001ADE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ShowUI _003C_003E4__this;

		[Token(Token = "0x4001ADF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private PooledObject<List<UniTask>> _003C__003E5__2;

		[Token(Token = "0x4001AE0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600217D")]
		[Address(RVA = "0x507210", Offset = "0x505C10", VA = "0x180507210", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600217E")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001AD9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[ResourceContext("UI", -1, null)]
	public StringListParameter UINames;

	[Token(Token = "0x4001ADA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[ParameterAlias("time")]
	public DecimalParameter Duration;

	[Token(Token = "0x4001ADB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	public BooleanParameter Wait;

	[Token(Token = "0x600217A")]
	[Address(RVA = "0x507040", Offset = "0x505A40", VA = "0x180507040", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600217B")]
	[Address(RVA = "0x5070F0", Offset = "0x505AF0", VA = "0x1805070F0", Slot = "16")]
	[AsyncStateMachine(typeof(_003CShow_003Ed__4))]
	protected virtual UniTask Show(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600217C")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public ShowUI()
	{
	}
}
