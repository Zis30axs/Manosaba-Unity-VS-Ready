using System.Collections.Generic;
using UnityEngine;

namespace Naninovel.UI
{
    /// <summary>
    /// Used by <see cref="UITextPrinter"/> to control the printed text.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class UITextPrinterPanel : CustomUI, IManagedUI
    {
        /// <summary>
        /// Contents of the printer to be used for transformations.
        /// </summary>
        public virtual RectTransform Content => content;
        /// <summary>
        /// The reveal ratio of the assigned messages, in 0.0 to 1.0 range.
        /// </summary>
        public abstract float RevealProgress { get; set; }
        /// <summary>
        /// Appearance of the printer.
        /// </summary>
        public abstract string Appearance { get; set; }
        /// <summary>
        /// Tint color of the printer.
        /// </summary>
        public virtual Color TintColor { get => tintColor; set => SetTintColor(value); }
        /// <summary>
        /// Objects that should trigger continue input when interacted with.
        /// </summary>
        public virtual IReadOnlyCollection<GameObject> ContinueInputTriggers => continueInputTriggers;
        /// <summary>
        /// Formatting templates to apply by default (before any templates assigned via `@format` command) for the messages printed by the printer actor.
        /// </summary>
        public virtual IReadOnlyList<MessageTemplate> DefaultTemplates => defaultTemplates;

        protected virtual ICharacterManager CharacterManager { get; private set; }
        protected virtual List<PrintedMessage> Messages { get; } = new();
        protected virtual List<MessageTemplate> Templates { get; } = new();

        [Tooltip("Transform used for printer position, scale and rotation external manipulations.")]
        [SerializeField] private RectTransform content;
        [Tooltip("Objects that should trigger continue input when interacted with. Make sure the objects are a raycast target and not blocked by other raycast targets.")]
        [SerializeField] private List<GameObject> continueInputTriggers = new();
        [Tooltip("Formatting templates to apply by default (before any templates assigned via `@format` command) for the messages printed by the printer actor." +
                 "\n\n%TEXT% is replaced with the message text, %AUTHOR% — with the author name." +
                 "\n\nThe templates are applied in order and filtered by the author: `+` applies for any authored message, `-` — for un-authored messages and `*` for all messages, authored or not.")]
        [SerializeField] private List<MessageTemplate> defaultTemplates = new();
        [Tooltip("Event invoked when tint color of the printer actor is changed.")]
        [SerializeField] private ColorUnityEvent onTintChanged;

        private IInputSampler continueInput;
        private IScriptPlayer scriptPlayer;
        private Color tintColor = Color.white;

        public override async UniTask Initialize ()
        {
            await base.Initialize();

            if (continueInput != null)
                foreach (var go in ContinueInputTriggers)
                    continueInput.AddObjectTrigger(go);
            scriptPlayer.OnWaitingForInput += SetWaitForInputIndicatorVisible;
        }

        UniTask IManagedUI.ChangeVisibility (bool visible, float? duration, AsyncToken token)
        {
            Engine.Err("@showUI and @hideUI commands can't be used with text printers; use @show/hide or @show/hidePrinter commands instead");
            return UniTask.CompletedTask;
        }

        /// <summary>
        /// Assigns text messages to print.
        /// </summary>
        public abstract void SetMessages (IReadOnlyList<PrintedMessage> messages);
        /// <summary>
        /// Adds specified text message to print.
        /// </summary>
        public abstract void AddMessage (PrintedMessage message);
        /// <summary>
        /// Appends specified text to the last printed message, or adds new message with the text when no messages printed.
        /// </summary>
        public abstract void AppendText (LocalizableText text);
        /// <summary>
        /// Assigns templates to format consequent printed messages (doesn't affect current messages).
        /// </summary>
        public virtual void SetTemplates (IReadOnlyList<MessageTemplate> templates) => Templates.ReplaceWith(templates);
        /// <summary>
        /// Reveals the <see cref="Messages"/>'s text char by char over time.
        /// </summary>
        /// <param name="delay">Delay (in seconds) between revealing consequent characters.</param>
        /// <param name="token">The reveal should be canceled when requested by the specified token.</param>
        public abstract UniTask RevealMessages (float delay, AsyncToken token);
        /// <summary>
        /// Controls visibility of the wait for input indicator.
        /// </summary>
        public abstract void SetWaitForInputIndicatorVisible (bool visible);

        protected override void Awake ()
        {
            base.Awake();
            this.AssertRequiredObjects(content);

            continueInput = Engine.GetServiceOrErr<IInputManager>().GetContinue();
            scriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>();
            CharacterManager = Engine.GetServiceOrErr<ICharacterManager>();
        }

        protected override void OnDestroy ()
        {
            base.OnDestroy();

            if (continueInput != null)
                foreach (var go in ContinueInputTriggers)
                    continueInput.RemoveObjectTrigger(go);
            if (scriptPlayer != null)
                scriptPlayer.OnWaitingForInput -= SetWaitForInputIndicatorVisible;
        }

        protected virtual void SetTintColor (Color color)
        {
            tintColor = color;
            onTintChanged?.Invoke(color);
        }

        protected virtual string FormatMessage (PrintedMessage message)
        {
            var text = (string)message.Text;
            if (DefaultTemplates.Count + Templates.Count == 0) return text;

            var authorLabel = message.Author is { Id: { Length: > 0 } authorId } author
                ? (author.Label.IsEmpty ? CharacterManager.GetAuthorName(authorId) : author.Label)
                : "";
            foreach (var template in DefaultTemplates)
                if (template.Applicable(message.Author?.Id))
                    ApplyTemplate(template.Template);
            foreach (var template in Templates)
                if (template.Applicable(message.Author?.Id))
                    ApplyTemplate(template.Template);

            return text;

            void ApplyTemplate (string template)
            {
                text = template.Replace("%AUTHOR%", authorLabel).Replace("%TEXT%", text);
            }
        }
    }
}
