using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel.Commands;

[Token(Token = "0x20006B3")]
[CommandAlias("camera")]
public class ModifyCamera : Command
{
	[StructLayout(3)]
	[Token(Token = "0x20006B8")]
	[CompilerGenerated]
	private struct _003CModify_003Ed__14
{
		[Token(Token = "0x40019B3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40019B4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40019B5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ModifyCamera _003C_003E4__this;

		[Token(Token = "0x40019B6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40019B7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private PooledObject<List<UniTask>> _003C__003E5__2;

		[Token(Token = "0x40019B8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6002073")]
		[Address(RVA = "0x4EB680", Offset = "0x4EA080", VA = "0x1804EB680", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6002074")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40019A0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[VectorContext("X,Y,Z", -1, null)]
	public DecimalListParameter Offset;

	[Token(Token = "0x40019A1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	public DecimalParameter Roll;

	[Token(Token = "0x40019A2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[VectorContext("X,Y,Z", -1, null)]
	public DecimalListParameter Rotation;

	[Token(Token = "0x40019A3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	public DecimalParameter Zoom;

	[Token(Token = "0x40019A4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[ParameterAlias("ortho")]
	public BooleanParameter Orthographic;

	[Token(Token = "0x40019A5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[ParameterAlias("toggle")]
	public StringListParameter ToggleTypeNames;

	[Token(Token = "0x40019A6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[ParameterAlias("set")]
	public NamedBooleanListParameter SetTypeNames;

	[Token(Token = "0x40019A7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	[ParameterAlias("easing")]
	[ConstantContext(typeof(EasingType), -1, null)]
	public StringParameter EasingTypeName;

	[Token(Token = "0x40019A8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	[ParameterAlias("time")]
	[ParameterDefaultValue("0.35")]
	public DecimalParameter Duration;

	[Token(Token = "0x40019A9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	[ParameterDefaultValue("false")]
	public BooleanParameter Lazy;

	[Token(Token = "0x40019AA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	public BooleanParameter Wait;

	[Token(Token = "0x40019AB")]
	private const string selectAllSymbol = "*";

	[Token(Token = "0x40019AC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private readonly List<MonoBehaviour> componentsCache;

	[Token(Token = "0x6002068")]
	[Address(RVA = "0x4EAD00", Offset = "0x4E9700", VA = "0x1804EAD00", Slot = "5")]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6002069")]
	[Address(RVA = "0x4EADB0", Offset = "0x4E97B0", VA = "0x1804EADB0", Slot = "16")]
	[AsyncStateMachine(typeof(_003CModify_003Ed__14))]
	protected virtual UniTask Modify(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600206A")]
	[Address(RVA = "0x4EAF40", Offset = "0x4E9940", VA = "0x1804EAF40")]
	public ModifyCamera()
	{
	}
}
