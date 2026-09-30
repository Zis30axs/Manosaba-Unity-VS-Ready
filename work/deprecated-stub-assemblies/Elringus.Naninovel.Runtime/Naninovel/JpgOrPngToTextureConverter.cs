using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000237")]
    public class JpgOrPngToTextureConverter : IRawConverter<Texture2D>, IConverter<byte[], Texture2D>, IConverter
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000238")]
    	[CompilerGenerated]
    	private struct _003CConvert_003Ed__6
    {
    		[Token(Token = "0x40008C2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40008C3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<object> _003C_003Et__builder;

    		[Token(Token = "0x40008C4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public JpgOrPngToTextureConverter _003C_003E4__this;

    		[Token(Token = "0x40008C5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public object obj;

    		[Token(Token = "0x40008C6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public string path;

    		[Token(Token = "0x40008C7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<Texture2D>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60009E6")]
    		[Address(RVA = "0x52A080", Offset = "0x528A80", VA = "0x18052A080", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60009E7")]
    		[Address(RVA = "0x52A380", Offset = "0x528D80", VA = "0x18052A380", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x1700016E")]
    	public RawDataRepresentation[] Representations
    	{
    		[Token(Token = "0x60009E0")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60009E1")]
    	[Address(RVA = "0x529870", Offset = "0x528270", VA = "0x180529870", Slot = "5")]
    	public Texture2D ConvertBlocking(byte[] obj, string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x60009E2")]
    	[Address(RVA = "0x5299D0", Offset = "0x5283D0", VA = "0x1805299D0", Slot = "6")]
    	public UniTask<Texture2D> Convert(byte[] obj, string path)
    	{
    		return default(UniTask<Texture2D>);
    	}

    	[Token(Token = "0x60009E3")]
    	[Address(RVA = "0x529A80", Offset = "0x528480", VA = "0x180529A80", Slot = "7")]
    	public object ConvertBlocking(object obj, string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x60009E4")]
    	[Address(RVA = "0x529AF0", Offset = "0x5284F0", VA = "0x180529AF0", Slot = "8")]
    	[AsyncStateMachine(typeof(_003CConvert_003Ed__6))]
    	public UniTask<object> Convert(object obj, string path)
    	{
    		return default(UniTask<object>);
    	}

    	[Token(Token = "0x60009E5")]
    	[Address(RVA = "0x529D50", Offset = "0x528750", VA = "0x180529D50")]
    	public JpgOrPngToTextureConverter()
    	{
    	}
    }
}
