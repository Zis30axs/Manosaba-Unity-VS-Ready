using System.Collections.Generic;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Serializable state of a <see cref="ITextPrinterActor"/>.
    /// </summary>
    [System.Serializable]
    public class TextPrinterState : ActorState<ITextPrinterActor>
    {
        /// <inheritdoc cref="ITextPrinterActor.Messages"/>
        public List<PrintedMessage> Messages => messages;
        /// <inheritdoc cref="ITextPrinterActor.Templates"/>
        public List<MessageTemplate> Templates => templates;
        /// <inheritdoc cref="ITextPrinterActor.RevealProgress"/>
        public float RevealProgress => revealProgress;

        [SerializeField] private List<PrintedMessage> messages = new();
        [SerializeField] private List<MessageTemplate> templates = new();
        [SerializeField] private float revealProgress;

        public override void OverwriteFromActor (ITextPrinterActor actor)
        {
            base.OverwriteFromActor(actor);

            templates.ReplaceWith(actor.Templates);
            messages.ReplaceWith(actor.Messages);
            revealProgress = actor.RevealProgress;
        }

        public override async UniTask ApplyToActor (ITextPrinterActor actor)
        {
            await base.ApplyToActor(actor);

            using var _ = ListPool<UniTask>.Rent(out var tasks);
            foreach (var message in messages)
            {
                tasks.Add(message.Text.Load(actor));
                if (message.Author is { Label: { IsEmpty: false } label })
                    tasks.Add(label.Load(actor));
            }
            await UniTask.WhenAll(tasks);

            actor.Templates = templates;
            actor.Messages = messages;
            actor.RevealProgress = revealProgress;
        }
    }
}
