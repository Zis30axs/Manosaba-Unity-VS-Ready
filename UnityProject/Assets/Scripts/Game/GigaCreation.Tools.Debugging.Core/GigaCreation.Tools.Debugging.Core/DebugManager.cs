using System;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UniRx;

namespace GigaCreation.Tools.Debugging.Core
{
    [Token(Token = "0x2000004")]
    public class DebugManager : IDebugManager, IService, IDisposable
    {
    	[Token(Token = "0x4000006")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly ReactiveProperty<bool> _isDebugMode;

    	[Token(Token = "0x17000001")]
    	public IReactiveProperty<bool> IsDebugMode
    	{
    		[Token(Token = "0x6000003")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0", Slot = "4")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0xD42010", Offset = "0xD40A10", VA = "0x180D42010")]
    	public DebugManager(bool initialMode)
    	{
    	}

    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xD420F0", Offset = "0xD40AF0", VA = "0x180D420F0", Slot = "5")]
    	public void Dispose()
    	{
    	}
    }
}
