using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel.FX;

[Token(Token = "0x20005F6")]
public class ShakeCharacter : ShakeTransform
{
	[StructLayout(3)]
	[Token(Token = "0x20005F8")]
	[CompilerGenerated]
	private struct _003CShakeSequence_003Ed__2
{
		[Token(Token = "0x4001735")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001736")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001737")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ShakeCharacter _003C_003E4__this;

		[Token(Token = "0x4001738")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4001739")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private float _003Cduration_003E5__2;

		[Token(Token = "0x400173A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001DE9")]
		[Address(RVA = "0x4C00F0", Offset = "0x4BEAF0", VA = "0x1804C00F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001DEA")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001732")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	[SerializeField]
	private bool preventPositiveYOffset;

	[Token(Token = "0x6001DE2")]
	[Address(RVA = "0x4BF950", Offset = "0x4BE350", VA = "0x1804BF950", Slot = "31")]
	protected override Transform GetShakenTransform()
	{
		return null;
	}

	[Token(Token = "0x6001DE3")]
	[Address(RVA = "0x4BFCE0", Offset = "0x4BE6E0", VA = "0x1804BFCE0", Slot = "32")]
	[AsyncStateMachine(typeof(_003CShakeSequence_003Ed__2))]
	protected override UniTask ShakeSequence(AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6001DE4")]
	[Address(RVA = "0x4BFE60", Offset = "0x4BE860", VA = "0x1804BFE60")]
	public ShakeCharacter()
	{
	}
}
