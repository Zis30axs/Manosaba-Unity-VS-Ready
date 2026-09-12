using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x200023F")]
public class TxtToTextAssetConverter : IRawConverter<TextAsset>, IConverter<byte[], TextAsset>, IConverter
{
	[StructLayout(3)]
	[Token(Token = "0x2000240")]
	[CompilerGenerated]
	private struct _003CConvert_003Ed__6
{
		[Token(Token = "0x40008DE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40008DF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<object> _003C_003Et__builder;

		[Token(Token = "0x40008E0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public TxtToTextAssetConverter _003C_003E4__this;

		[Token(Token = "0x40008E1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public object obj;

		[Token(Token = "0x40008E2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string path;

		[Token(Token = "0x40008E3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<TextAsset>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000A06")]
		[Address(RVA = "0x52CAA0", Offset = "0x52B4A0", VA = "0x18052CAA0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000A07")]
		[Address(RVA = "0x52CDA0", Offset = "0x52B7A0", VA = "0x18052CDA0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x17000172")]
	public RawDataRepresentation[] Representations
	{
		[Token(Token = "0x6000A00")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000A01")]
	[Address(RVA = "0x52C3C0", Offset = "0x52ADC0", VA = "0x18052C3C0", Slot = "5")]
	public TextAsset ConvertBlocking(byte[] obj, string path)
	{
		return null;
	}

	[Token(Token = "0x6000A02")]
	[Address(RVA = "0x52C520", Offset = "0x52AF20", VA = "0x18052C520", Slot = "6")]
	public UniTask<TextAsset> Convert(byte[] obj, string path)
	{
		return default(UniTask<TextAsset>);
	}

	[Token(Token = "0x6000A03")]
	[Address(RVA = "0x52C5D0", Offset = "0x52AFD0", VA = "0x18052C5D0", Slot = "7")]
	public object ConvertBlocking(object obj, string path)
	{
		return null;
	}

	[Token(Token = "0x6000A04")]
	[Address(RVA = "0x52C640", Offset = "0x52B040", VA = "0x18052C640", Slot = "8")]
	[AsyncStateMachine(typeof(_003CConvert_003Ed__6))]
	public UniTask<object> Convert(object obj, string path)
	{
		return default(UniTask<object>);
	}

	[Token(Token = "0x6000A05")]
	[Address(RVA = "0x52C8A0", Offset = "0x52B2A0", VA = "0x18052C8A0")]
	public TxtToTextAssetConverter()
	{
	}
}
