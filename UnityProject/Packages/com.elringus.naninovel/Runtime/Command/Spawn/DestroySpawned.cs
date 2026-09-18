using System.Collections.Generic;

namespace Naninovel.Commands
{
    [Doc(
        @"
Destroys an object spawned with [@spawn] command.",
        @"
If prefab has a `MonoBehaviour` component attached the root object, and the component implements
a `IParameterized` interface, will pass the specified `params` values before destroying the object;
if the component implements `IAwaitable` interface, command execution will wait for
the async completion task returned by the implementation before destroying the object.",
        @"
; Given '@spawn Rainbow' command was executed before, de-spawn (destroy) it.
@despawn Rainbow"
    )]
    [CommandAlias("despawn")]
    public class DestroySpawned : Command
    {
        public interface IParameterized
        {
            void SetDestroyParameters (IReadOnlyList<string> parameters);
        }

        public interface IAwaitable
        {
            UniTask AwaitDestroy (AsyncToken token = default);
        }

        [Doc("Name (path) of the prefab resource to destroy. A [@spawn] command with the same parameter is expected to be executed before.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter]
        public StringParameter Path;
        [Doc("Parameters to set before destroying the prefab. Requires the prefab to have a `IParameterized` component attached the root object.")]
        public StringListParameter Params;
        [Doc("Whether to wait while the spawn is destroying over time in case it implements `IAwaitable` interface.")]
        public BooleanParameter Wait;

        protected virtual ISpawnManager SpawnManager => Engine.GetServiceOrErr<ISpawnManager>();

        public override UniTask Execute (AsyncToken token = default)
        {
            return WaitOrForget(Destroy, Wait, token);
        }

        protected virtual async UniTask Destroy (AsyncToken token)
        {
            if (!SpawnManager.IsSpawned(Path))
            {
                Warn($"Failed to destroy spawned object '{Path}': the object is not found.");
                return;
            }
            var spawned = SpawnManager.GetSpawned(Path);
            // remove from the spawn state instantly, otherwise the object leaks
            // to the consequent rollback snapshots while awaiting the async destroy
            SpawnManager.DestroySpawned(Path, false);
            spawned.SetDestroyParameters(Params?.ToReadOnlyList());
            try { await spawned.AwaitDestroy(token); }
            finally { ObjectUtils.DestroyOrImmediate(spawned.GameObject); }
        }
    }
}
