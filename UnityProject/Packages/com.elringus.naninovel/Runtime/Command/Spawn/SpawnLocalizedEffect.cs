namespace Naninovel.Commands
{
    /// <summary>
    /// Base class for FX spawn commands, which spawn game objects
    /// that can be localized on scene (@rain, @snow, @sun, etc).
    /// </summary>
    public abstract class SpawnLocalizedEffect : SpawnEffect
    {
        [Doc("Position (relative to the scene borders, in percents) to set for the spawned effect game object. " +
             "Position is described as follows: `0,0` is the bottom left, `50,50` is the center and `100,100` is the top right corner of the scene. " +
             "Use Z-component (third member, eg `,,10`) to move (sort) by depth while in ortho mode.")]
        [ParameterAlias("pos"), VectorContext("X,Y,Z")]
        public DecimalListParameter ScenePosition;
        [Doc("Position (in world space) to set for the spawned effect game object.")]
        [VectorContext("X,Y,Z")]
        public DecimalListParameter Position;
        [Doc("Rotation to set for the spawned effect game object.")]
        [VectorContext("X,Y,Z")]
        public DecimalListParameter Rotation;
        [Doc("Scale to set for the spawned effect game object.")]
        [VectorContext("X,Y,Z")]
        public DecimalListParameter Scale;

        protected override DecimalListParameter GetScenePosition () => ScenePosition;
        protected override DecimalListParameter GetPosition () => Position;
        protected override DecimalListParameter GetRotation () => Rotation;
        protected override DecimalListParameter GetScale () => Scale;
    }
}
