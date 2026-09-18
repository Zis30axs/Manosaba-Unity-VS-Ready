using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Provides extension methods for <see cref="IActor"/>.
    /// </summary>
    public static class ActorExtensions
    {
        /// <summary>
        /// Changes <see cref="IActor.Position"/> over X-axis over specified time using specified animation tween.
        /// </summary>
        public static async UniTask ChangePositionX (this IActor actor, float posX, Tween tween, AsyncToken token = default)
            => await actor.ChangePosition(new(posX, actor.Position.y, actor.Position.z), tween, token);
        /// <summary>
        /// Changes <see cref="IActor.Position"/> over Y-axis over specified time using specified animation tween.
        /// </summary>
        public static async UniTask ChangePositionY (this IActor actor, float posY, Tween tween, AsyncToken token = default)
            => await actor.ChangePosition(new(actor.Position.x, posY, actor.Position.z), tween, token);
        /// <summary>
        /// Changes <see cref="IActor.Position"/> over Z-axis over specified time using specified animation tween.
        /// </summary>
        public static async UniTask ChangePositionZ (this IActor actor, float posZ, Tween tween, AsyncToken token = default)
            => await actor.ChangePosition(new(actor.Position.x, actor.Position.y, posZ), tween, token);

        /// <summary>
        /// Changes <see cref="IActor.Rotation"/> over X-axis (Euler angle) over specified time using specified animation tween.
        /// </summary>
        public static async UniTask ChangeRotationX (this IActor actor, float rotX, Tween tween, AsyncToken token = default)
            => await actor.ChangeRotation(Quaternion.Euler(rotX, actor.Rotation.eulerAngles.y, actor.Rotation.eulerAngles.z), tween, token);
        /// <summary>
        /// Changes <see cref="IActor.Rotation"/> over Y-axis (Euler angle) over specified time using specified animation tween.
        /// </summary>
        public static async UniTask ChangeRotationY (this IActor actor, float rotY, Tween tween, AsyncToken token = default)
            => await actor.ChangeRotation(Quaternion.Euler(actor.Rotation.eulerAngles.x, rotY, actor.Rotation.eulerAngles.z), tween, token);
        /// <summary>
        /// Changes <see cref="IActor.Rotation"/> over Z-axis (Euler angle) over specified time using specified animation tween.
        /// </summary>
        public static async UniTask ChangeRotationZ (this IActor actor, float rotZ, Tween tween, AsyncToken token = default)
            => await actor.ChangeRotation(Quaternion.Euler(actor.Rotation.eulerAngles.x, actor.Rotation.eulerAngles.y, rotZ), tween, token);

        /// <summary>
        /// Changes <see cref="IActor.Scale"/> over X-axis over specified time using specified animation tween.
        /// </summary>
        public static async UniTask ChangeScaleX (this IActor actor, float scaleX, Tween tween, AsyncToken token = default)
            => await actor.ChangeScale(new(scaleX, actor.Scale.y, actor.Scale.z), tween, token);
        /// <summary>
        /// Changes <see cref="IActor.Scale"/> over Y-axis over specified time using specified animation tween.
        /// </summary>
        public static async UniTask ChangeScaleY (this IActor actor, float scaleY, Tween tween, AsyncToken token = default)
            => await actor.ChangeScale(new(actor.Scale.x, scaleY, actor.Scale.z), tween, token);
        /// <summary>
        /// Changes <see cref="IActor.Scale"/> over Z-axis over specified time using specified animation tween.
        /// </summary>
        public static async UniTask ChangeScaleZ (this IActor actor, float scaleZ, Tween tween, AsyncToken token = default)
            => await actor.ChangeScale(new(actor.Scale.x, actor.Scale.y, scaleZ), tween, token);

        /// <summary>
        /// Changes <see cref="IActor.Position"/> over X-axis.
        /// </summary>
        public static void ChangePositionX (this IActor actor, float posX) => actor.Position = new(posX, actor.Position.y, actor.Position.z);
        /// <summary>
        /// Changes <see cref="IActor.Position"/> over Y-axis.
        /// </summary>
        public static void ChangePositionY (this IActor actor, float posY) => actor.Position = new(actor.Position.x, posY, actor.Position.z);
        /// <summary>
        /// Changes <see cref="IActor.Position"/> over Z-axis.
        /// </summary>
        public static void ChangePositionZ (this IActor actor, float posZ) => actor.Position = new(actor.Position.x, actor.Position.y, posZ);

        /// <summary>
        /// Changes <see cref="IActor.Rotation"/> over X-axis (Euler angle).
        /// </summary>
        public static void ChangeRotationX (this IActor actor, float rotX) => actor.Rotation = Quaternion.Euler(rotX, actor.Rotation.eulerAngles.y, actor.Rotation.eulerAngles.z);
        /// <summary>
        /// Changes <see cref="IActor.Rotation"/> over Y-axis (Euler angle).
        /// </summary>
        public static void ChangeRotationY (this IActor actor, float rotY) => actor.Rotation = Quaternion.Euler(actor.Rotation.eulerAngles.x, rotY, actor.Rotation.eulerAngles.z);
        /// <summary>
        /// Changes <see cref="IActor.Rotation"/> over Z-axis (Euler angle).
        /// </summary>
        public static void ChangeRotationZ (this IActor actor, float rotZ) => actor.Rotation = Quaternion.Euler(actor.Rotation.eulerAngles.x, actor.Rotation.eulerAngles.y, rotZ);

        /// <summary>
        /// Changes <see cref="IActor.Scale"/> over X-axis.
        /// </summary>
        public static void ChangeScaleX (this IActor actor, float scaleX) => actor.Scale = new(scaleX, actor.Scale.y, actor.Scale.z);
        /// <summary>
        /// Changes <see cref="IActor.Scale"/> over Y-axis.
        /// </summary>
        public static void ChangeScaleY (this IActor actor, float scaleY) => actor.Scale = new(actor.Scale.x, scaleY, actor.Scale.z);
        /// <summary>
        /// Changes <see cref="IActor.Scale"/> over Z-axis.
        /// </summary>
        public static void ChangeScaleZ (this IActor actor, float scaleZ) => actor.Scale = new(actor.Scale.x, actor.Scale.y, scaleZ);
    }
}
