namespace Naninovel.Commands
{
    [Doc(
        @"
Destroys all the objects spawned with [@spawn] command.
Equal to invoking [@despawn] for all the currently spawned objects.",
        null,
        @"
@spawn Rainbow
@spawn SunShafts
; Will de-spawn (destroy) both Rainbow and SunShafts.
@despawnAll"
    )]
    [CommandAlias("despawnAll")]
    public class DestroyAllSpawned : Command
    {
        [Doc("Whether to wait while the spawns are destroying over time in case they implements `IAwaitable` interface.")]
        public BooleanParameter Wait;

        protected virtual ISpawnManager SpawnManager => Engine.GetServiceOrErr<ISpawnManager>();

        public override UniTask Execute (AsyncToken token = default)
        {
            return WaitOrForget(DestroyAll, Wait, token);
        }

        protected virtual async UniTask DestroyAll (AsyncToken token)
        {
            using var _ = ListPool<UniTask>.Rent(out var tasks);
            using var __ = ListPool<SpawnedObject>.Rent(out var spawned);
            spawned.ReplaceWith(SpawnManager.Spawned);
            foreach (var s in spawned)
                tasks.Add(new DestroySpawned { Path = s.Path, Wait = Wait }.Execute(token));
            await UniTask.WhenAll(tasks);
        }
    }
}
