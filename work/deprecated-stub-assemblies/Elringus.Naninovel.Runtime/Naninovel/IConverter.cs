using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000230")]
    public interface IConverter
    {
    	[Token(Token = "0x60009CE")]
    	object ConvertBlocking(object obj, string path);

    	[Token(Token = "0x60009CF")]
    	UniTask<object> Convert(object obj, string path);
    }
    [Token(Token = "0x2000231")]
    public interface IConverter<TSource, TResult> : IConverter
    {
    	[Token(Token = "0x60009D0")]
    	TResult ConvertBlocking(TSource obj, string path);

    	[Token(Token = "0x60009D1")]
    	UniTask<TResult> Convert(TSource obj, string path);
    }
}
