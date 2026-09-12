using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000241")]
public class WavToAudioClipConverter : IRawConverter<AudioClip>, IConverter<byte[], AudioClip>, IConverter
{
	[StructLayout(3)]
	[Token(Token = "0x2000243")]
	[CompilerGenerated]
	private struct _003CConvert_003Ed__4
{
		[Token(Token = "0x40008E6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40008E7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<AudioClip> _003C_003Et__builder;

		[Token(Token = "0x40008E8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public byte[] obj;

		[Token(Token = "0x40008E9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string path;

		[Token(Token = "0x40008EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<float[]>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000A11")]
		[Address(RVA = "0x52D800", Offset = "0x52C200", VA = "0x18052D800", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000A12")]
		[Address(RVA = "0x52DC70", Offset = "0x52C670", VA = "0x18052DC70", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000244")]
	[CompilerGenerated]
	private struct _003CConvert_003Ed__6
{
		[Token(Token = "0x40008EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40008EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<object> _003C_003Et__builder;

		[Token(Token = "0x40008ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public WavToAudioClipConverter _003C_003E4__this;

		[Token(Token = "0x40008EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public object obj;

		[Token(Token = "0x40008EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string path;

		[Token(Token = "0x40008F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<AudioClip>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000A13")]
		[Address(RVA = "0x52DCA0", Offset = "0x52C6A0", VA = "0x18052DCA0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000A14")]
		[Address(RVA = "0x52E0F0", Offset = "0x52CAF0", VA = "0x18052E0F0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x17000173")]
	public RawDataRepresentation[] Representations
	{
		[Token(Token = "0x6000A08")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000A09")]
	[Address(RVA = "0x52CDD0", Offset = "0x52B7D0", VA = "0x18052CDD0", Slot = "5")]
	public AudioClip ConvertBlocking(byte[] obj, string path)
	{
		return null;
	}

	[Token(Token = "0x6000A0A")]
	[Address(RVA = "0x52CF10", Offset = "0x52B910", VA = "0x18052CF10", Slot = "6")]
	[AsyncStateMachine(typeof(_003CConvert_003Ed__4))]
	public UniTask<AudioClip> Convert(byte[] obj, string path)
	{
		return default(UniTask<AudioClip>);
	}

	[Token(Token = "0x6000A0B")]
	[Address(RVA = "0x52D100", Offset = "0x52BB00", VA = "0x18052D100", Slot = "7")]
	public object ConvertBlocking(object obj, string path)
	{
		return null;
	}

	[Token(Token = "0x6000A0C")]
	[Address(RVA = "0x52D270", Offset = "0x52BC70", VA = "0x18052D270", Slot = "8")]
	[AsyncStateMachine(typeof(_003CConvert_003Ed__6))]
	public UniTask<object> Convert(object obj, string path)
	{
		return default(UniTask<object>);
	}

	[Token(Token = "0x6000A0D")]
	[Address(RVA = "0x52D4D0", Offset = "0x52BED0", VA = "0x18052D4D0")]
	private static float[] Pcm16ToFloatArray(byte[] input)
	{
		return null;
	}

	[Token(Token = "0x6000A0E")]
	[Address(RVA = "0x52D5F0", Offset = "0x52BFF0", VA = "0x18052D5F0")]
	public WavToAudioClipConverter()
	{
	}
}
