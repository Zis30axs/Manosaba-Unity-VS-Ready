using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x200022E")]
public class GDocToTextAssetConverter : IGoogleDriveConverter<TextAsset>, IRawConverter<TextAsset>, IConverter<byte[], TextAsset>, IConverter
{
	[StructLayout(3)]
	[Token(Token = "0x200022F")]
	[CompilerGenerated]
	private struct _003CConvert_003Ed__8
{
		[Token(Token = "0x40008B2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40008B3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<object> _003C_003Et__builder;

		[Token(Token = "0x40008B4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public GDocToTextAssetConverter _003C_003E4__this;

		[Token(Token = "0x40008B5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public object obj;

		[Token(Token = "0x40008B6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string path;

		[Token(Token = "0x40008B7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<TextAsset>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60009CC")]
		[Address(RVA = "0x5287F0", Offset = "0x5271F0", VA = "0x1805287F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60009CD")]
		[Address(RVA = "0x528AF0", Offset = "0x5274F0", VA = "0x180528AF0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x17000169")]
	public RawDataRepresentation[] Representations
	{
		[Token(Token = "0x60009C5")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "5")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700016A")]
	public string ExportMimeType
	{
		[Token(Token = "0x60009C6")]
		[Address(RVA = "0x5280F0", Offset = "0x526AF0", VA = "0x1805280F0", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60009C7")]
	[Address(RVA = "0x528130", Offset = "0x526B30", VA = "0x180528130", Slot = "6")]
	public TextAsset ConvertBlocking(byte[] obj, string path)
	{
		return null;
	}

	[Token(Token = "0x60009C8")]
	[Address(RVA = "0x528290", Offset = "0x526C90", VA = "0x180528290", Slot = "7")]
	public UniTask<TextAsset> Convert(byte[] obj, string path)
	{
		return default(UniTask<TextAsset>);
	}

	[Token(Token = "0x60009C9")]
	[Address(RVA = "0x528340", Offset = "0x526D40", VA = "0x180528340", Slot = "8")]
	public object ConvertBlocking(object obj, string path)
	{
		return null;
	}

	[Token(Token = "0x60009CA")]
	[Address(RVA = "0x5283B0", Offset = "0x526DB0", VA = "0x1805283B0", Slot = "9")]
	[AsyncStateMachine(typeof(_003CConvert_003Ed__8))]
	public UniTask<object> Convert(object obj, string path)
	{
		return default(UniTask<object>);
	}

	[Token(Token = "0x60009CB")]
	[Address(RVA = "0x528610", Offset = "0x527010", VA = "0x180528610")]
	public GDocToTextAssetConverter()
	{
	}
}
