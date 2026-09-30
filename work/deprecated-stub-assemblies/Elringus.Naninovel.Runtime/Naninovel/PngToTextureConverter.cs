using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200023D")]
    public class PngToTextureConverter : IRawConverter<Texture2D>, IConverter<byte[], Texture2D>, IConverter
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200023E")]
    	[CompilerGenerated]
    	private struct _003CConvert_003Ed__6
    {
    		[Token(Token = "0x40008D7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40008D8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<object> _003C_003Et__builder;

    		[Token(Token = "0x40008D9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public PngToTextureConverter _003C_003E4__this;

    		[Token(Token = "0x40008DA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public object obj;

    		[Token(Token = "0x40008DB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public string path;

    		[Token(Token = "0x40008DC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<Texture2D>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60009FE")]
    		[Address(RVA = "0x52C090", Offset = "0x52AA90", VA = "0x18052C090", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60009FF")]
    		[Address(RVA = "0x52C390", Offset = "0x52AD90", VA = "0x18052C390", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x17000171")]
    	public RawDataRepresentation[] Representations
    	{
    		[Token(Token = "0x60009F8")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60009F9")]
    	[Address(RVA = "0x52B9B0", Offset = "0x52A3B0", VA = "0x18052B9B0", Slot = "5")]
    	public Texture2D ConvertBlocking(byte[] obj, string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x60009FA")]
    	[Address(RVA = "0x52BB10", Offset = "0x52A510", VA = "0x18052BB10", Slot = "6")]
    	public UniTask<Texture2D> Convert(byte[] obj, string path)
    	{
    		return default(UniTask<Texture2D>);
    	}

    	[Token(Token = "0x60009FB")]
    	[Address(RVA = "0x52BBC0", Offset = "0x52A5C0", VA = "0x18052BBC0", Slot = "7")]
    	public object ConvertBlocking(object obj, string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x60009FC")]
    	[Address(RVA = "0x52BC30", Offset = "0x52A630", VA = "0x18052BC30", Slot = "8")]
    	[AsyncStateMachine(typeof(_003CConvert_003Ed__6))]
    	public UniTask<object> Convert(object obj, string path)
    	{
    		return default(UniTask<object>);
    	}

    	[Token(Token = "0x60009FD")]
    	[Address(RVA = "0x52BE90", Offset = "0x52A890", VA = "0x18052BE90")]
    	public PngToTextureConverter()
    	{
    	}
    }
}
