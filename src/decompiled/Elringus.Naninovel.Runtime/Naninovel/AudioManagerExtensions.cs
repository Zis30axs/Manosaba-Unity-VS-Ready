using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel;

[Token(Token = "0x2000105")]
public static class AudioManagerExtensions
{
	[Token(Token = "0x2000106")]
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass0_0
	{
		[Token(Token = "0x400040D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public IAudioManager manager;

		[Token(Token = "0x400040E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public AsyncToken token;

		[Token(Token = "0x6000498")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public _003C_003Ec__DisplayClass0_0()
		{
		}
	}

	[Token(Token = "0x2000107")]
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass0_1
	{
		[Token(Token = "0x400040F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public string path;

		[Token(Token = "0x4000410")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public _003C_003Ec__DisplayClass0_0 CS_0024_003C_003E8__locals1;

		[Token(Token = "0x6000499")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public _003C_003Ec__DisplayClass0_1()
		{
		}

		[Token(Token = "0x600049A")]
		[Address(RVA = "0x483750", Offset = "0x482150", VA = "0x180483750")]
		internal bool _003CPlayVoiceSequence_003Eb__0()
		{
			return default(bool);
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000108")]
	[CompilerGenerated]
	private struct _003CPlayVoiceSequence_003Ed__0
{
		[Token(Token = "0x4000411")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000412")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000413")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public IAudioManager manager;

		[Token(Token = "0x4000414")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4000415")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public IReadOnlyCollection<string> pathList;

		[Token(Token = "0x4000416")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private _003C_003Ec__DisplayClass0_0 _003C_003E8__1;

		[Token(Token = "0x4000417")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		public float volume;

		[Token(Token = "0x4000418")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		public string group;

		[Token(Token = "0x4000419")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		public string authorId;

		[Token(Token = "0x400041A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private _003C_003Ec__DisplayClass0_1 _003C_003E8__2;

		[Token(Token = "0x400041B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
		private IEnumerator<string> _003C_003E7__wrap1;

		[Token(Token = "0x400041C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600049B")]
		[Address(RVA = "0x4837D0", Offset = "0x4821D0", VA = "0x1804837D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600049C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x6000497")]
	[Address(RVA = "0x4834A0", Offset = "0x481EA0", VA = "0x1804834A0")]
	[AsyncStateMachine(typeof(_003CPlayVoiceSequence_003Ed__0))]
	public static UniTask PlayVoiceSequence(this IAudioManager manager, IReadOnlyCollection<string> pathList, float volume = 1f, string group = default, string authorId = default, AsyncToken token = default)
	{
		return default(UniTask);
	}
}
