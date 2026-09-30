using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000239")]
    public class JpgToSpriteConverter : IRawConverter<Sprite>, IConverter<byte[], Sprite>, IConverter
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200023A")]
    	[CompilerGenerated]
    	private struct _003CConvert_003Ed__6
    {
    		[Token(Token = "0x40008C9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40008CA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<object> _003C_003Et__builder;

    		[Token(Token = "0x40008CB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public JpgToSpriteConverter _003C_003E4__this;

    		[Token(Token = "0x40008CC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public object obj;

    		[Token(Token = "0x40008CD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public string path;

    		[Token(Token = "0x40008CE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<Sprite>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60009EE")]
    		[Address(RVA = "0x52AB80", Offset = "0x529580", VA = "0x18052AB80", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60009EF")]
    		[Address(RVA = "0x52AE80", Offset = "0x529880", VA = "0x18052AE80", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x1700016F")]
    	public RawDataRepresentation[] Representations
    	{
    		[Token(Token = "0x60009E8")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60009E9")]
    	[Address(RVA = "0x52A3B0", Offset = "0x528DB0", VA = "0x18052A3B0", Slot = "5")]
    	public Sprite ConvertBlocking(byte[] obj, string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x60009EA")]
    	[Address(RVA = "0x52A600", Offset = "0x529000", VA = "0x18052A600", Slot = "6")]
    	public UniTask<Sprite> Convert(byte[] obj, string path)
    	{
    		return default(UniTask<Sprite>);
    	}

    	[Token(Token = "0x60009EB")]
    	[Address(RVA = "0x52A6B0", Offset = "0x5290B0", VA = "0x18052A6B0", Slot = "7")]
    	public object ConvertBlocking(object obj, string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x60009EC")]
    	[Address(RVA = "0x52A720", Offset = "0x529120", VA = "0x18052A720", Slot = "8")]
    	[AsyncStateMachine(typeof(_003CConvert_003Ed__6))]
    	public UniTask<object> Convert(object obj, string path)
    	{
    		return default(UniTask<object>);
    	}

    	[Token(Token = "0x60009ED")]
    	[Address(RVA = "0x52A980", Offset = "0x529380", VA = "0x18052A980")]
    	public JpgToSpriteConverter()
    	{
    	}
    }
}
