using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="ICharacterActor"/> implementation, which doesn't have any presence on scene
    /// and can be used to represent a narrator (author of the printed text messages).
    /// </summary>
    [ActorResources(null, false)]
    public class NarratorCharacter : ICharacterActor
    {
        public string Id { get; }
        public string Appearance { get; set; }
        public bool Visible { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 Scale { get; set; }
        public Color TintColor { get; set; }
        public CharacterLookDirection LookDirection { get; set; }

        public NarratorCharacter (string id, CharacterMetadata metadata)
        {
            Id = id;
        }
        
        public UniTask Initialize () => UniTask.CompletedTask;

        public UniTask ChangeAppearance (string appearance, Tween tween, 
            Transition? transition = default, AsyncToken token = default) => UniTask.CompletedTask;

        public UniTask ChangeVisibility (bool visible, Tween tween, 
            AsyncToken token = default) => UniTask.CompletedTask;

        public UniTask ChangePosition (Vector3 position, Tween tween, 
            AsyncToken token = default) => UniTask.CompletedTask;

        public UniTask ChangeRotation (Quaternion rotation, Tween tween, 
            AsyncToken token = default) => UniTask.CompletedTask;

        public UniTask ChangeScale (Vector3 scale, Tween tween, 
            AsyncToken token = default) => UniTask.CompletedTask;

        public UniTask ChangeTintColor (Color tintColor, Tween tween, 
            AsyncToken token = default) => UniTask.CompletedTask;
        
        public UniTask ChangeLookDirection (CharacterLookDirection lookDirection, Tween tween, 
            AsyncToken token = default) => UniTask.CompletedTask;
    }
}
