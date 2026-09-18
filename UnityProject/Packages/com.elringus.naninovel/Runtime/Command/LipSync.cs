namespace Naninovel.Commands
{
    [Doc(
        @"
Allows to force-stop the lip sync mouth animation for a character with the specified ID; when stopped, the animation
won't start again, until this command is used again to allow it.
The character should be able to receive the lip sync events (currently generic, layered and Live2D implementations only).
See [characters guide](/guide/characters#lip-sync) for more information on lip sync feature.",
        null,
        @"
; Given auto voicing is disabled and lip sync is driven by text messages,
; exclude punctuation from the mouth animation.
Kohaku: Lorem ipsum dolor sit amet[lipSync Kohaku.false]... [lipSync Kohaku.true]Consectetur adipiscing elit."
    )]
    public class LipSync : Command
    {
        /// <summary>
        /// Implementation is is able to receive <see cref="LipSync"/> commands.
        /// </summary>
        public interface IReceiver
        {
            /// <summary>
            /// The character should refrain from the lip sync mouth animation until allowed.
            /// </summary>
            void AllowLipSync (bool active);
        }

        [Doc("Character ID followed by a boolean (true or false) on whether to halt or allow the lip sync animation.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter, ActorContext(CharactersConfiguration.DefaultPathPrefix, 0)]
        public NamedBooleanParameter CharIdAndAllow;

        public override UniTask Execute (AsyncToken token = default)
        {
            var characterManager = Engine.GetServiceOrErr<ICharacterManager>();
            if (!characterManager.ActorExists(CharIdAndAllow.Name))
            {
                Warn($"Failed to control lip sync for '{CharIdAndAllow.Name}': character with the ID doesn't exist.");
                return UniTask.CompletedTask;
            }

            if (characterManager.GetActor(CharIdAndAllow.Name) is IReceiver receiver)
                receiver.AllowLipSync(CharIdAndAllow.NamedValue ?? false);
            else
            {
                Warn($"Failed to control lip sync for '{CharIdAndAllow.Name}': character is not able to receive lip sync events.");
                return UniTask.CompletedTask;
            }

            return UniTask.CompletedTask;
        }
    }
}
