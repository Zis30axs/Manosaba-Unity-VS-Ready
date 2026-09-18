using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Naninovel.UI;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="ITextPrinterActor"/> implementation using <see cref="UITextPrinterPanel"/> to represent the actor.
    /// </summary>
    [ActorResources(typeof(UITextPrinterPanel), false)]
    public class UITextPrinter : MonoBehaviourActor<TextPrinterMetadata>, ITextPrinterActor
    {
        public override GameObject GameObject => PrinterPanel.gameObject;
        public override string Appearance { get => PrinterPanel.Appearance; set => PrinterPanel.Appearance = value; }
        public override bool Visible { get => PrinterPanel.Visible; set => PrinterPanel.Visible = value; }
        public virtual IReadOnlyList<PrintedMessage> Messages { get => messages; set => SetMessages(value); }
        public virtual IReadOnlyList<MessageTemplate> Templates { get => templates; set => SetTemplates(value); }
        public virtual float RevealProgress { get => PrinterPanel.RevealProgress; set => SetRevealProgress(value); }
        public virtual UITextPrinterPanel PrinterPanel { get; private set; }

        private readonly IUIManager uis;
        private readonly ILocalizationManager l10n;
        private readonly IResourceProviderManager resources;
        private readonly AspectMonitor aspectMonitor;
        private readonly List<PrintedMessage> messages = new();
        private readonly List<MessageTemplate> templates = new();
        private CancellationTokenSource revealTextCTS;

        public UITextPrinter (string id, TextPrinterMetadata meta)
            : base(id, meta)
        {
            uis = Engine.GetServiceOrErr<IUIManager>();
            l10n = Engine.GetServiceOrErr<ILocalizationManager>();
            resources = Engine.GetServiceOrErr<IResourceProviderManager>();
            aspectMonitor = new();
        }

        public override async UniTask Initialize ()
        {
            await base.Initialize();
            var prefab = await LoadUIPrefab();
            PrinterPanel = await uis.AddUI(prefab, group: BuildActorCategory()) as UITextPrinterPanel;
            if (!PrinterPanel) throw new Error($"Failed to initialize '{Id}' printer actor: printer panel UI instantiation failed.");
            aspectMonitor.OnChanged += HandleAspectChanged;
            aspectMonitor.Start(target: PrinterPanel);
            l10n.OnLocaleChanged += HandleLocaleChanged;
            SetTemplates(Array.Empty<MessageTemplate>());
            SetMessages(Array.Empty<PrintedMessage>());
            RevealProgress = 0;
            Visible = false;
        }

        public override UniTask ChangeAppearance (string appearance, Tween tween,
            Transition? transition = default, AsyncToken token = default)
        {
            Appearance = appearance;
            return UniTask.CompletedTask;
        }

        public override async UniTask ChangeVisibility (bool visible, Tween tween, AsyncToken token = default)
        {
            await PrinterPanel.ChangeVisibility(visible, tween.Duration, token);
        }

        public void AddMessage (PrintedMessage message)
        {
            message.Text.Hold(this);
            message.Author?.Label.Hold(this);
            messages.Add(message);
            PrinterPanel.AddMessage(message);
        }

        public void AppendText (LocalizableText text)
        {
            if (messages.Count == 0)
            {
                AddMessage(new(text));
                return;
            }
            text.Hold(this);
            messages[^1] = new(messages[^1].Text + text, messages[^1].Author ?? default);
            PrinterPanel.AppendText(text);
        }

        public virtual async UniTask Reveal (float delay, AsyncToken token = default)
        {
            CancelRevealTextRoutine();
            revealTextCTS = CancellationTokenSource.CreateLinkedTokenSource(token.CancellationToken);
            var revealTextToken = new AsyncToken(revealTextCTS.Token, token.CompletionToken);
            await PrinterPanel.RevealMessages(delay, revealTextToken);
        }

        public override void Dispose ()
        {
            base.Dispose();

            aspectMonitor?.Stop();
            CancelRevealTextRoutine();

            if (PrinterPanel)
            {
                uis.RemoveUI(PrinterPanel);
                ObjectUtils.DestroyOrImmediate(PrinterPanel.gameObject);
                PrinterPanel = null;
            }

            if (l10n != null)
                l10n.OnLocaleChanged -= HandleLocaleChanged;
        }

        protected virtual async UniTask<GameObject> LoadUIPrefab ()
        {
            return await ActorMeta.Loader.CreateLocalizableFor<GameObject>(resources, l10n).LoadOrErr(Id);
        }

        protected override GameObject CreateHostObject () => null;

        protected virtual void SetRevealProgress (float value)
        {
            CancelRevealTextRoutine();
            PrinterPanel.RevealProgress = value;
        }

        protected override Vector3 GetBehaviourPosition ()
        {
            if (!PrinterPanel || !PrinterPanel.Content) return Vector3.zero;
            return PrinterPanel.Content.position;
        }

        protected override void SetBehaviourPosition (Vector3 position)
        {
            if (!PrinterPanel || !PrinterPanel.Content) return;
            PrinterPanel.Content.position = (Vector2)position; // don't change z-pos, as it'll break UI ordering
        }

        protected override Quaternion GetBehaviourRotation ()
        {
            if (!PrinterPanel || !PrinterPanel.Content) return Quaternion.identity;
            return PrinterPanel.Content.rotation;
        }

        protected override void SetBehaviourRotation (Quaternion rotation)
        {
            if (!PrinterPanel || !PrinterPanel.Content) return;
            PrinterPanel.Content.rotation = rotation;
        }

        protected override Vector3 GetBehaviourScale ()
        {
            if (!PrinterPanel || !PrinterPanel.Content) return Vector3.one;
            return PrinterPanel.Content.localScale;
        }

        protected override void SetBehaviourScale (Vector3 scale)
        {
            if (!PrinterPanel || !PrinterPanel.Content) return;
            PrinterPanel.Content.localScale = scale;
        }

        protected override Color GetBehaviourTintColor () => PrinterPanel.TintColor;

        protected override void SetBehaviourTintColor (Color value) => PrinterPanel.TintColor = value;

        protected virtual void SetMessages (IReadOnlyList<PrintedMessage> messages)
        {
            var count = Mathf.Max(this.messages.Count, messages.Count);
            for (int i = 0; i < count; i++)
            {
                var from = this.messages.ElementAtOrDefault(i);
                var to = messages.ElementAtOrDefault(i);
                from.Text.Juggle(to.Text, this);
                if (from.Author is { Label: { IsEmpty: false } label })
                    label.Juggle(to.Author?.Label ?? default, this);
                else to.Author?.Label.Hold(this);
            }
            this.messages.ReplaceWith(messages);
            PrinterPanel.SetMessages(messages);
        }

        protected virtual void SetTemplates (IReadOnlyList<MessageTemplate> templates)
        {
            this.templates.ReplaceWith(templates);
            PrinterPanel.SetTemplates(templates);
        }

        protected virtual void HandleAspectChanged (AspectMonitor monitor)
        {
            // UI printers anchored to canvas borders are moved on aspect change;
            // re-set position here to return them to correct relative positions.
            SetBehaviourPosition(GetBehaviourPosition());
        }

        protected virtual void HandleLocaleChanged (LocaleChangedArgs _)
        {
            PrinterPanel.SetMessages(messages);
        }

        protected virtual void CancelRevealTextRoutine ()
        {
            revealTextCTS?.Cancel();
            revealTextCTS?.Dispose();
            revealTextCTS = null;
        }
    }
}
