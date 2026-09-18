using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Audio
{
    [Token(Token = "0x2000007")]
    public interface IAudioManagerExtended : IEngineService<AudioConfigurationExtended>, IEngineService
    {
    	[Token(Token = "0x600000E")]
    	void PlayResponseSfx(ResponseKind responseKind);
    }
}
