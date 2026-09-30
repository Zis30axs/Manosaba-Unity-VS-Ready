using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000232")]
    public interface IGoogleDriveConverter<TResult> : IRawConverter<TResult>, IConverter<byte[], TResult>, IConverter
    {
    	[Token(Token = "0x1700016B")]
    	string ExportMimeType
    	{
    		[Token(Token = "0x60009D2")]
    		get;
    	}
    }
}
