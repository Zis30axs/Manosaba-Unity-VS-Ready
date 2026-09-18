namespace Naninovel
{
    /// <summary>
    /// Implementation is able to represent a character actor on scene.
    /// </summary>
    public interface ICharacterActor : IActor
    {
        /// <summary>
        /// Look direction of the character.
        /// </summary>
        CharacterLookDirection LookDirection { get; set; }

        /// <summary>
        /// Changes character look direction over specified time using specified tween animation.
        /// </summary>
        UniTask ChangeLookDirection (CharacterLookDirection lookDirection, Tween tween, AsyncToken token = default);
    }
}
