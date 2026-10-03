using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Common
{
    // The updated StateConfiguration selects this handler so the game-state slot manager can
    // expose the auto-save helpers added by IOGameStateSlotManagerExtended.  The wrapper was
    // missing from the reconstructed source, which made Type.GetType(GameStateHandler) return
    // null and prevented Naninovel's engine from initializing.
    [Token(Token = "0x2000029")]
    public class UniversalGameStateSerializerExtended : IOGameStateSlotManagerExtended
    {
        public UniversalGameStateSerializerExtended(StateConfiguration config, string savesFolderPath)
            : base(config, savesFolderPath)
        {
        }
    }
}
