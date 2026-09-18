using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Serializable state of a <see cref="IChoiceHandlerActor"/>.
    /// </summary>
    [Serializable]
    public class ChoiceHandlerState : ActorState<IChoiceHandlerActor>
    {
        /// <inheritdoc cref="IChoiceHandlerActor.Choices"/>
        public List<ChoiceState> Choices => new(choices);

        [SerializeField] private List<ChoiceState> choices = new();

        public override void OverwriteFromActor (IChoiceHandlerActor actor)
        {
            base.OverwriteFromActor(actor);

            choices.ReplaceWith(actor.Choices);
        }

        public override async UniTask ApplyToActor (IChoiceHandlerActor actor)
        {
            await base.ApplyToActor(actor);

            using var _ = ListPool<ChoiceState>.Rent(out var existingChoices);
            existingChoices.ReplaceWith(actor.Choices);
            foreach (var existingChoice in existingChoices)
                if (!choices.Contains(existingChoice))
                {
                    actor.RemoveChoice(existingChoice.Id);
                    existingChoice.Summary.Release(actor);
                }

            foreach (var choice in choices)
                if (!actor.Choices.Contains(choice))
                {
                    await choice.Summary.Load(actor);
                    actor.AddChoice(choice);
                }
        }
    }
}
