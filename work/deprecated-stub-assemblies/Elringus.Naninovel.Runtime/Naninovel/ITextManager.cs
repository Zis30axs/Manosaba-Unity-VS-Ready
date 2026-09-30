using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel.ManagedText;

namespace Naninovel
{
    [Token(Token = "0x20003A3")]
    public interface ITextManager : IEngineService<ManagedTextConfiguration>, IEngineService
    {
    	[Token(Token = "0x17000265")]
    	IResourceLoader DocumentLoader
    	{
    		[Token(Token = "0x60011A9")]
    		get;
    	}

    	[Token(Token = "0x60011AA")]
    	[CanBeNull]
    	ManagedTextDocument GetDocument(string documentPath);
    }
}
