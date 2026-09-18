using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Naninovel.UI;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// A <see cref="IChoiceHandlerActor"/> implementation using <see cref="UI.ChoiceHandlerPanel"/> to represent the actor.
    /// </summary>
    [ActorResources(typeof(ChoiceHandlerPanel), false)]
    public class UIChoiceHandler : MonoBehaviourActor<ChoiceHandlerMetadata>, IChoiceHandlerActor
    {
        public override GameObject GameObject => HandlerPanel.gameObject;
        public override string Appearance { get; set; }
        public override bool Visible { get => HandlerPanel.Visible; set => HandlerPanel.Visible = value; }
        IReadOnlyList<ChoiceState> IChoiceHandlerActor.Choices => Choices;

        protected virtual ChoiceHandlerPanel HandlerPanel { get; private set; }
        protected virtual List<ChoiceState> Choices { get; } = new();

        private readonly IChoiceHandlerManager handlers;
        private readonly IScriptPlayer player;
        private readonly IStateManager state;
        private readonly IUIManager uis;

        public UIChoiceHandler (string id, ChoiceHandlerMetadata meta)
            : base(id, meta)
        {
            handlers = Engine.GetServiceOrErr<IChoiceHandlerManager>();
            player = Engine.GetServiceOrErr<IScriptPlayer>();
            state = Engine.GetServiceOrErr<IStateManager>();
            uis = Engine.GetServiceOrErr<IUIManager>();
        }

        public override async UniTask Initialize ()
        {
            await base.Initialize();
            var prefab = await LoadUIPrefab();
            HandlerPanel = await uis.AddUI(prefab, group: BuildActorCategory()) as ChoiceHandlerPanel;
            if (!HandlerPanel) throw new Error($"Failed to initialize '{Id}' choice handler actor: choice panel UI instantiation failed.");
            HandlerPanel.OnChoice += HandleChoice;
            Visible = false;
        }

        public override UniTask ChangeAppearance (string appearance, Tween tween,
            Transition? transition = default, AsyncToken token = default)
        {
            return UniTask.CompletedTask;
        }

        public override async UniTask ChangeVisibility (bool visible, Tween tween, AsyncToken token = default)
        {
            if (HandlerPanel)
                await HandlerPanel.ChangeVisibility(visible, tween.Duration);
        }

        public virtual void AddChoice (ChoiceState choice)
        {
            choice.Summary.Hold(this);
            Choices.Add(choice);
            HandlerPanel.AddChoiceButton(choice);
        }

        public virtual void RemoveChoice (string id)
        {
            for (var i = Choices.Count - 1; i >= 0; i--)
            {
                var choice = Choices[i];
                if (choice.Id != id) continue;
                Choices.RemoveAt(i);
                choice.Summary.Release(this);
            }
            HandlerPanel.RemoveChoiceButton(id);
        }

        public virtual void HandleChoice (string id)
        {
            foreach (var choice in Choices)
                if (choice.Id == id)
                {
                    HandleChoice(choice);
                    return;
                }
            throw new Error($"Failed to handle choice with ID '{id}': choice not found.");
        }

        public override void Dispose ()
        {
            base.Dispose();

            if (HandlerPanel)
            {
                uis.RemoveUI(HandlerPanel);
                ObjectUtils.DestroyOrImmediate(HandlerPanel.gameObject);
                HandlerPanel = null;
            }
        }

        protected virtual async UniTask<GameObject> LoadUIPrefab ()
        {
            var resources = Engine.GetServiceOrErr<IResourceProviderManager>();
            var l10n = Engine.GetServiceOrErr<ILocalizationManager>();
            return await ActorMeta.Loader.CreateLocalizableFor<GameObject>(resources, l10n).LoadOrErr(Id);
        }

        protected override GameObject CreateHostObject () => null;

        protected override Color GetBehaviourTintColor () => Color.white;

        protected override void SetBehaviourTintColor (Color tintColor) { }

        protected virtual async void HandleChoice (ChoiceState choice)
        {
            if (!Choices.Exists(c => c.Id.EqualsFast(choice.Id))) return;

            state.PeekRollbackStack()?.AllowPlayerRollback();
            AddChoiceToBacklog(choice);
            ClearChoices();

            if (choice.Nested)
            {
                var continueAt = PlaybackSpot.Invalid;
                if (player.Playing) continueAt = player.PlaybackSpot;
                else
                {
                    var nextIdx = player.Playlist.MoveAt(player.PlayedIndex);
                    if (player.Playlist.IsIndexValid(nextIdx))
                        continueAt = player.Playlist[nextIdx].PlaybackSpot;
                    // Don't throw when next index is invalid, as we may have @goto inside nested callback.
                    // Otherwise a descriptive error is thrown in @choice on exiting the nested callback block.
                }
                handlers.PushPickedChoice(choice.HostedAt, continueAt);
            }

            var scriptText = choice.OnSelectScript;

            if (HandlerPanel)
            {
                HandlerPanel.RemoveAllChoiceButtonsDelayed(); // Delayed to allow custom onClick logic.
                HandlerPanel.Hide();
                if (ActorMeta.WaitHideOnChoice)
                    scriptText = $"@wait {HandlerPanel.FadeTime}\n" + scriptText;
            }

            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(GetDestroyCancellationToken()))
            {
                state.OnRollbackStarted += cts.Cancel;
                try { await player.PlayTransient($"'{Id}' on choice script", scriptText, cts.Token); }
                catch (OperationCanceledException) { return; }
                finally
                {
                    if (state != null)
                        state.OnRollbackStarted -= cts.Cancel;
                }
            }

            if (choice.Nested)
                NavigateToNested(choice.HostedAt);
            else if (choice.AutoPlay && !player.Playing)
            {
                var nextIndex = player.PlayedIndex + 1;
                player.Resume(nextIndex);
            }
        }

        protected virtual void AddChoiceToBacklog (ChoiceState state)
        {
            var backlog = uis.GetUI<IBacklogUI>();
            if (backlog == null) return;
            var choices = Choices.Select(c => new BacklogChoice(c.Summary, c.Id == state.Id)).ToArray();
            backlog.AddChoice(choices);
        }

        protected virtual void NavigateToNested (PlaybackSpot hostedAt)
        {
            if (hostedAt.ScriptPath != player.PlayedScript.Path)
                throw new Error(Engine.FormatMessage("Choice callback from another script is not supported.", player.PlaybackSpot));
            var index = player.Playlist.IndexOf(hostedAt) + 1;
            if (!player.Playlist.IsIndexValid(index))
                throw new Error(Engine.FormatMessage("Failed navigating to choice callback: playlist index is invalid.", player.PlaybackSpot));
            player.Resume(index);
        }

        protected virtual void ClearChoices ()
        {
            foreach (var choice in Choices)
                choice.Summary.Release(this);
            Choices.Clear();
        }
    }
}
