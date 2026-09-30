using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x2000026")]
    [InitializeAtRuntime(0, null)]
    public class BackgroundManager : OrthoActorManager<IBackgroundActor, BackgroundState, BackgroundMetadata, BackgroundsConfiguration>
    {
    	[Token(Token = "0x60000AA")]
    	[Address(RVA = "0x44C530", Offset = "0x44AF30", VA = "0x18044C530")]
    	public BackgroundManager(BackgroundsConfiguration config = default, CameraConfiguration cameraConfig = default)
    	{
    	}
    }
}
