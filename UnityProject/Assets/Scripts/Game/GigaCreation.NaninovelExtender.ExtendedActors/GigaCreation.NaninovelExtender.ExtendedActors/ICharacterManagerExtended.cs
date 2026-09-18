using System.Collections.Generic;
using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x2000019")]
    public interface ICharacterManagerExtended : IMultipliableCharacterManager, IMultipliableActorManager<ICharacterActor>, IActorManager, IEngineService, IEngineService<CharactersConfigurationExtended>
    {
    	[Token(Token = "0x17000003")]
    	IReadOnlyDictionary<string, float> ScaleCoefficients
    	{
    		[Token(Token = "0x6000042")]
    		get;
    	}
    }
}
