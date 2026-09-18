using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Naninovel
{
    /// <summary>
    /// Allows to play a <see cref="Script"/> or execute script commands via Unity API.
    /// </summary>
    public class PlayScript : MonoBehaviour
    {
        protected virtual string ScriptPath => scriptPath;
        protected virtual string ScriptText => scriptText;
        protected virtual bool PlayOnAwake => playOnAwake;
        protected virtual bool DisableWaitInput => disableWaitInput;

        [FormerlySerializedAs("scriptName"), Tooltip("Local resource path of a script asset to play.")]
        [ResourcePopup(ScriptsConfiguration.DefaultPathPrefix, ScriptsConfiguration.DefaultPathPrefix, emptyOption: "None (play script text)")]
        [SerializeField] private string scriptPath;
        [TextArea(3, 10), Tooltip("The naninovel script text (commands) to execute; has no effect when 'Script Path' is specified. Argument of the event (if any) can be referenced in the script text via '{arg}' expression. Conditional block commands (if, else, etc) are not supported.")]
        [SerializeField] private string scriptText;
        [Tooltip("Whether to automatically play the script when the game object is instantiated.")]
        [SerializeField] private bool playOnAwake;
        [Tooltip("Whether to disable waiting for input mode when the script is played.")]
        [SerializeField] private bool disableWaitInput;

        private string argument;

        public virtual void Play ()
        {
            argument = null;
            DoPlayScript();
        }

        public virtual void Play (string argument)
        {
            this.argument = argument;
            DoPlayScript();
        }

        public virtual void Play (float argument)
        {
            this.argument = argument.ToString(CultureInfo.InvariantCulture);
            DoPlayScript();
        }

        public virtual void Play (int argument)
        {
            this.argument = argument.ToString(CultureInfo.InvariantCulture);
            DoPlayScript();
        }

        public virtual void Play (bool argument)
        {
            this.argument = argument.ToString(CultureInfo.InvariantCulture).ToLower();
            DoPlayScript();
        }

        protected virtual void Awake ()
        {
            if (PlayOnAwake) Play();
        }

        protected virtual void DoPlayScript ()
        {
            var player = Engine.GetServiceOrErr<IScriptPlayer>();

            if (!string.IsNullOrEmpty(ScriptPath))
            {
                player.LoadAndPlay(ScriptPath).Forget();
                return;
            }

            if (DisableWaitInput) player.SetWaitingForInputEnabled(false);

            if (!string.IsNullOrWhiteSpace(ScriptText))
            {
                var text = string.IsNullOrEmpty(argument) ? ScriptText : ScriptText.Replace("{arg}", argument);
                player.PlayTransient($"'{name}' generated script", text).Forget();
            }
        }
    }
}
