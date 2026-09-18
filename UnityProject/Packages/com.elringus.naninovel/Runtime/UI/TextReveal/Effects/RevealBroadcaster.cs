using UnityEngine;

namespace Naninovel.UI
{
    /// <summary>
    /// A text reveal effect that broadcast when event tag (<@...>) is revealed.
    /// </summary>
    public class RevealBroadcaster : TextRevealEffect
    {
        [SerializeField] private StringUnityEvent onEvent;
        [Tooltip("When enabled, will attempt to parse and play event body as command.")]
        [SerializeField] private bool playCommand = true;

        private IScriptPlayer player;

        private void Awake ()
        {
            player = Engine.GetServiceOrErr<IScriptPlayer>();
        }

        private void OnEnable ()
        {
            Info.OnChange += HandleChange;
        }

        private void OnDisable ()
        {
            if (Text) Info.OnChange -= HandleChange;
        }

        private void HandleChange ()
        {
            var bodies = Text.PopEventsAtChar(Info.LastRevealedCharIndex);
            if (bodies == null) return;

            foreach (var body in bodies)
            {
                onEvent?.Invoke(body);
                if (playCommand) player.PlayTransient("Reveal Event", Compiler.Syntax.CommandLine + body).Forget();
            }
        }
    }
}
