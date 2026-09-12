using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel;

[Token(Token = "0x20003BA")]
public class GDocToScriptAssetConverter : IGoogleDriveConverter<Script>, IRawConverter<Script>, IConverter<byte[], Script>, IConverter
{
	[StructLayout(3)]
	[Token(Token = "0x20003BB")]
	[CompilerGenerated]
	private struct _003CConvert_003Ed__8
{
		[Token(Token = "0x4000DF5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000DF6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<object> _003C_003Et__builder;

		[Token(Token = "0x4000DF7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public GDocToScriptAssetConverter _003C_003E4__this;

		[Token(Token = "0x4000DF8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public object obj;

		[Token(Token = "0x4000DF9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string path;

		[Token(Token = "0x4000DFA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<Script>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001217")]
		[Address(RVA = "0x599D70", Offset = "0x598770", VA = "0x180599D70", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001218")]
		[Address(RVA = "0x59A070", Offset = "0x598A70", VA = "0x18059A070", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x17000272")]
	public RawDataRepresentation[] Representations
	{
		[Token(Token = "0x6001210")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "5")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000273")]
	public string ExportMimeType
	{
		[Token(Token = "0x6001211")]
		[Address(RVA = "0x5994A0", Offset = "0x597EA0", VA = "0x1805994A0", Slot = "4")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001212")]
	[Address(RVA = "0x5994E0", Offset = "0x597EE0", VA = "0x1805994E0", Slot = "6")]
	public Script ConvertBlocking(byte[] obj, string path)
	{
		return null;
	}

	[Token(Token = "0x6001213")]
	[Address(RVA = "0x599810", Offset = "0x598210", VA = "0x180599810", Slot = "7")]
	public UniTask<Script> Convert(byte[] obj, string path)
	{
		return default(UniTask<Script>);
	}

	[Token(Token = "0x6001214")]
	[Address(RVA = "0x5998C0", Offset = "0x5982C0", VA = "0x1805998C0", Slot = "8")]
	public object ConvertBlocking(object obj, string path)
	{
		return null;
	}

	[Token(Token = "0x6001215")]
	[Address(RVA = "0x599930", Offset = "0x598330", VA = "0x180599930", Slot = "9")]
	[AsyncStateMachine(typeof(_003CConvert_003Ed__8))]
	public UniTask<object> Convert(object obj, string path)
	{
		return default(UniTask<object>);
	}

	[Token(Token = "0x6001216")]
	[Address(RVA = "0x599B90", Offset = "0x598590", VA = "0x180599B90")]
	public GDocToScriptAssetConverter()
	{
	}
}
