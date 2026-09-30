using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000235")]
    public class JpgOrPngToSpriteConverter : IRawConverter<Sprite>, IConverter<byte[], Sprite>, IConverter
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000236")]
    	[CompilerGenerated]
    	private struct _003CConvert_003Ed__6
    {
    		[Token(Token = "0x40008BB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40008BC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<object> _003C_003Et__builder;

    		[Token(Token = "0x40008BD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public JpgOrPngToSpriteConverter _003C_003E4__this;

    		[Token(Token = "0x40008BE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public object obj;

    		[Token(Token = "0x40008BF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public string path;

    		[Token(Token = "0x40008C0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<Sprite>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60009DE")]
    		[Address(RVA = "0x529540", Offset = "0x527F40", VA = "0x180529540", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60009DF")]
    		[Address(RVA = "0x529840", Offset = "0x528240", VA = "0x180529840", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x1700016D")]
    	public RawDataRepresentation[] Representations
    	{
    		[Token(Token = "0x60009D8")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60009D9")]
    	[Address(RVA = "0x528C40", Offset = "0x527640", VA = "0x180528C40", Slot = "5")]
    	public Sprite ConvertBlocking(byte[] obj, string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x60009DA")]
    	[Address(RVA = "0x528E90", Offset = "0x527890", VA = "0x180528E90", Slot = "6")]
    	public UniTask<Sprite> Convert(byte[] obj, string path)
    	{
    		return default(UniTask<Sprite>);
    	}

    	[Token(Token = "0x60009DB")]
    	[Address(RVA = "0x528F40", Offset = "0x527940", VA = "0x180528F40", Slot = "7")]
    	public object ConvertBlocking(object obj, string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x60009DC")]
    	[Address(RVA = "0x528FB0", Offset = "0x5279B0", VA = "0x180528FB0", Slot = "8")]
    	[AsyncStateMachine(typeof(_003CConvert_003Ed__6))]
    	public UniTask<object> Convert(object obj, string path)
    	{
    		return default(UniTask<object>);
    	}

    	[Token(Token = "0x60009DD")]
    	[Address(RVA = "0x529210", Offset = "0x527C10", VA = "0x180529210")]
    	public JpgOrPngToSpriteConverter()
    	{
    	}
    }
}
