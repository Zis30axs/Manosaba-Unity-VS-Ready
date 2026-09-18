
namespace Naninovel
{
    /// <summary>
    /// Implementation is able to manage <see cref="IBackgroundActor"/> actors.
    /// </summary>
    public interface IBackgroundManager : IActorManager<IBackgroundActor, BackgroundState, BackgroundMetadata, BackgroundsConfiguration>
    {

    }
}
