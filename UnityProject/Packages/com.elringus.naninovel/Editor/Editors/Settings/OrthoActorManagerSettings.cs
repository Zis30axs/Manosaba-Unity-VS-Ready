namespace Naninovel
{
    public abstract class OrthoActorManagerSettings<TConfig, TActor, TMeta> : ActorManagerSettings<TConfig, TActor, TMeta>
        where TConfig : OrthoActorManagerConfiguration<TMeta>
        where TActor : IActor
        where TMeta : OrthoActorMetadata
    {

    }
}
