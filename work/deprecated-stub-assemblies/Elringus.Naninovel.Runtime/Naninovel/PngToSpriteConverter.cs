using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200023B")]
    public class PngToSpriteConverter : IRawConverter<Sprite>, IConverter<byte[], Sprite>, IConverter
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200023C")]
    	[CompilerGenerated]
    	private struct _003CConvert_003Ed__6
    {
    		[Token(Token = "0x40008D0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40008D1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<object> _003C_003Et__builder;

    		[Token(Token = "0x40008D2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public PngToSpriteConverter _003C_003E4__this;

    		[Token(Token = "0x40008D3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public object obj;

    		[Token(Token = "0x40008D4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public string path;

    		[Token(Token = "0x40008D5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<Sprite>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60009F6")]
    		[Address(RVA = "0x52B680", Offset = "0x52A080", VA = "0x18052B680", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60009F7")]
    		[Address(RVA = "0x52B980", Offset = "0x52A380", VA = "0x18052B980", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x17000170")]
    	public RawDataRepresentation[] Representations
    	{
    		[Token(Token = "0x60009F0")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60009F1")]
    	[Address(RVA = "0x52AEB0", Offset = "0x5298B0", VA = "0x18052AEB0", Slot = "5")]
    	public Sprite ConvertBlocking(byte[] obj, string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x60009F2")]
    	[Address(RVA = "0x52B100", Offset = "0x529B00", VA = "0x18052B100", Slot = "6")]
    	public UniTask<Sprite> Convert(byte[] obj, string path)
    	{
    		return default(UniTask<Sprite>);
    	}

    	[Token(Token = "0x60009F3")]
    	[Address(RVA = "0x52B1B0", Offset = "0x529BB0", VA = "0x18052B1B0", Slot = "7")]
    	public object ConvertBlocking(object obj, string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x60009F4")]
    	[Address(RVA = "0x52B220", Offset = "0x529C20", VA = "0x18052B220", Slot = "8")]
    	[AsyncStateMachine(typeof(_003CConvert_003Ed__6))]
    	public UniTask<object> Convert(object obj, string path)
    	{
    		return default(UniTask<object>);
    	}

    	[Token(Token = "0x60009F5")]
    	[Address(RVA = "0x52B480", Offset = "0x529E80", VA = "0x18052B480")]
    	public PngToSpriteConverter()
    	{
    	}
    }
}
