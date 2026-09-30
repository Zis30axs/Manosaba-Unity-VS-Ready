using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000234")]
    public interface IRawConverter<TResult> : IConverter<byte[], TResult>, IConverter
    {
    	[Token(Token = "0x1700016C")]
    	RawDataRepresentation[] Representations
    	{
    		[Token(Token = "0x60009D7")]
    		get;
    	}
    }
}
