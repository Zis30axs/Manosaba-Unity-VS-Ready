using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel;

[Token(Token = "0x20003C1")]
public class NaniToScriptAssetConverter : IRawConverter<Script>, IConverter<byte[], Script>, IConverter
{
	[StructLayout(3)]
	[Token(Token = "0x20003C2")]
	[CompilerGenerated]
	private struct _003CConvert_003Ed__6
{
		[Token(Token = "0x4000E13")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000E14")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<object> _003C_003Et__builder;

		[Token(Token = "0x4000E15")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public NaniToScriptAssetConverter _003C_003E4__this;

		[Token(Token = "0x4000E16")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public object obj;

		[Token(Token = "0x4000E17")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string path;

		[Token(Token = "0x4000E18")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<Script>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6001236")]
		[Address(RVA = "0x59AA00", Offset = "0x599400", VA = "0x18059AA00", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6001237")]
		[Address(RVA = "0x59AD00", Offset = "0x599700", VA = "0x18059AD00", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x17000275")]
	public RawDataRepresentation[] Representations
	{
		[Token(Token = "0x6001230")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6001231")]
	[Address(RVA = "0x59A150", Offset = "0x598B50", VA = "0x18059A150", Slot = "5")]
	public Script ConvertBlocking(byte[] obj, string path)
	{
		return null;
	}

	[Token(Token = "0x6001232")]
	[Address(RVA = "0x59A480", Offset = "0x598E80", VA = "0x18059A480", Slot = "6")]
	public UniTask<Script> Convert(byte[] obj, string path)
	{
		return default(UniTask<Script>);
	}

	[Token(Token = "0x6001233")]
	[Address(RVA = "0x59A530", Offset = "0x598F30", VA = "0x18059A530", Slot = "7")]
	public object ConvertBlocking(object obj, string path)
	{
		return null;
	}

	[Token(Token = "0x6001234")]
	[Address(RVA = "0x59A5A0", Offset = "0x598FA0", VA = "0x18059A5A0", Slot = "8")]
	[AsyncStateMachine(typeof(_003CConvert_003Ed__6))]
	public UniTask<object> Convert(object obj, string path)
	{
		return default(UniTask<object>);
	}

	[Token(Token = "0x6001235")]
	[Address(RVA = "0x59A800", Offset = "0x599200", VA = "0x18059A800")]
	public NaniToScriptAssetConverter()
	{
	}
}
