using UnityEngine;

namespace Naninovel.FX
{
    /// <summary>
    /// Shakes a <see cref="IBackgroundActor"/> or the main one.
    /// </summary>
    public class ShakeBackground : ShakeTransform
    {
        protected override Transform GetShakenTransform ()
        {
            var manager = Engine.GetServiceOrErr<IBackgroundManager>();
            var id = string.IsNullOrEmpty(ObjectName) ? BackgroundsConfiguration.MainActorId : ObjectName;
            if (!manager.ActorExists(id))
                throw new Error($"Failed to shake background with '{id}' ID: actor not found.");
            return (manager.GetActor(id) as MonoBehaviourActor<BackgroundMetadata>)?.Transform;
        }
    }
}
