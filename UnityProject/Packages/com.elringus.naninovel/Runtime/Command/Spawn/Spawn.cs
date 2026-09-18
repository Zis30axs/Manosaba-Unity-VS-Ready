using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel.Commands
{
    [Doc(
        @"
Instantiates a prefab or a [special effect](/guide/special-effects);
when performed over an already spawned object, will update the spawn parameters instead.",
        @"
If prefab has a `MonoBehaviour` component attached the root object, and the component implements
a `IParameterized` interface, will pass the specified `params` values after the spawn;
if the component implements `IAwaitable` interface, command execution will be able to wait for
the async completion task returned by the implementation.",
        @"
; Given a 'Rainbow' prefab is assigned in spawn resources, instantiate it.
@spawn Rainbow"
    )]
    public class Spawn : Command, Command.IPreloadable
    {
        /// <summary>
        /// When implemented by a component on the spawned object root,
        /// enables the object to receive spawn parameters from scenario scripts.
        /// </summary>
        public interface IParameterized
        {
            /// <summary>
            /// Applies parameters to the spawned object.
            /// </summary>
            /// <param name="parameters">The parameters to apply.</param>
            /// <param name="asap">Whether to apply all the parameters instantly, even when a duration is specified.</param>
            void SetSpawnParameters (IReadOnlyList<string> parameters, bool asap);
        }

        /// <summary>
        /// When implemented by a component on the spawned object root, enables the object to be awaited on spawn.
        /// </summary>
        public interface IAwaitable
        {
            /// <summary>
            /// Returns task to be awaited on spawn.
            /// </summary>
            UniTask AwaitSpawn (AsyncToken token = default);
        }

        [Doc("Name (path) of the prefab resource to spawn.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter, ResourceContext(SpawnConfiguration.DefaultPathPrefix)]
        public StringParameter Path;
        [Doc("Parameters to set when spawning the prefab. Requires the prefab to have a `IParameterized` component attached the root object.")]
        public StringListParameter Params;
        [Doc("Position (relative to the scene borders, in percents) to set for the spawned object. " +
             "Position is described as follows: `0,0` is the bottom left, `50,50` is the center and `100,100` is the top right corner of the scene. " +
             "Use Z-component (third member, eg `,,10`) to move (sort) by depth while in ortho mode.")]
        [ParameterAlias("pos"), VectorContext("X,Y,Z")]
        public DecimalListParameter ScenePosition;
        [Doc("Position (in world space) to set for the spawned object.")]
        [VectorContext("X,Y,Z")]
        public DecimalListParameter Position;
        [Doc("Rotation to set for the spawned object.")]
        [VectorContext("X,Y,Z")]
        public DecimalListParameter Rotation;
        [Doc("Scale to set for the spawned object.")]
        [VectorContext("X,Y,Z")]
        public DecimalListParameter Scale;
        [Doc("Whether to wait for the spawn to warm-up in case it implements `IAwaitable` interface.")]
        public BooleanParameter Wait;

        protected virtual ISpawnManager SpawnManager => Engine.GetServiceOrErr<ISpawnManager>();

        public virtual async UniTask PreloadResources ()
        {
            if (!Assigned(Path) || Path.DynamicValue || string.IsNullOrWhiteSpace(Path)) return;
            await SpawnManager.HoldResources(Path, this);
        }

        public virtual void ReleaseResources ()
        {
            if (!Assigned(Path) || Path.DynamicValue || string.IsNullOrWhiteSpace(Path)) return;
            SpawnManager.ReleaseResources(Path, this);
        }

        public override async UniTask Execute (AsyncToken token = default)
        {
            var spawn = await SpawnManager.GetOrSpawn(Path, token);
            ApplyParameters(spawn);
            ApplyScenePosition(spawn);
            ApplyPosition(spawn);
            ApplyRotation(spawn);
            ApplyScale(spawn);
            await WaitOrForget(spawn.AwaitSpawn, Wait, token);
        }

        protected virtual void ApplyParameters (SpawnedObject spawn)
        {
            var parameters = Params?.ToReadOnlyList();
            spawn.SetSpawnParameters(parameters, false);
        }

        protected virtual void ApplyScenePosition (SpawnedObject spawn)
        {
            if (!Assigned(ScenePosition)) return;
            var config = Engine.GetServiceOrErr<ICameraManager>().Configuration;
            spawn.Transform.position = new(
                ScenePosition.ElementAtOrDefault(0) != null
                    ? config.SceneToWorldSpace(new Vector2(ScenePosition[0] / 100f, 0)).x
                    : spawn.Transform.position.x,
                ScenePosition.ElementAtOrDefault(1) != null
                    ? config.SceneToWorldSpace(new Vector2(0, ScenePosition[1] / 100f)).y
                    : spawn.Transform.position.y,
                ScenePosition.ElementAtOrDefault(2) ?? spawn.Transform.position.z);
        }

        protected virtual void ApplyPosition (SpawnedObject spawn)
        {
            if (!Assigned(Position)) return;
            spawn.Transform.position = new(
                Position.ElementAtOrDefault(0) ?? spawn.Transform.position.x,
                Position.ElementAtOrDefault(1) ?? spawn.Transform.position.y,
                Position.ElementAtOrDefault(2) ?? spawn.Transform.position.z);
        }

        protected virtual void ApplyRotation (SpawnedObject spawn)
        {
            if (!Assigned(Rotation)) return;
            spawn.Transform.rotation = Quaternion.Euler(
                Rotation.ElementAtOrDefault(0) ?? spawn.Transform.eulerAngles.x,
                Rotation.ElementAtOrDefault(1) ?? spawn.Transform.eulerAngles.y,
                Rotation.ElementAtOrDefault(2) ?? spawn.Transform.eulerAngles.z);
        }

        protected virtual void ApplyScale (SpawnedObject spawn)
        {
            if (!Assigned(Scale)) return;

            if (Scale.Length == 1 && Scale[0].HasValue)
                spawn.Transform.localScale = new(Scale[0], Scale[0], Scale[0]);
            else
                spawn.Transform.localScale = new(
                    Scale.ElementAtOrDefault(0) ?? spawn.Transform.localScale.x,
                    Scale.ElementAtOrDefault(1) ?? spawn.Transform.localScale.y,
                    Scale.ElementAtOrDefault(2) ?? spawn.Transform.localScale.z);
        }
    }
}
