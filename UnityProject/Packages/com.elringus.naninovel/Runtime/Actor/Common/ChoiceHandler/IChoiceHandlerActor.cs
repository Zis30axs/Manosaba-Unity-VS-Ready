using System.Collections.Generic;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to represent a choice handler actor on scene.
    /// </summary>
    public interface IChoiceHandlerActor : IActor
    {
        /// <summary>
        /// Currently available options to choose from, in the added order.
        /// </summary>
        IReadOnlyList<ChoiceState> Choices { get; }

        /// <summary>
        /// Adds an option to choose from.
        /// </summary>
        void AddChoice (ChoiceState choice);
        /// <summary>
        /// Removes a choice option with the specified ID.
        /// </summary>
        void RemoveChoice (string id);
        /// <summary>
        /// Selects a choice option with the specified ID.
        /// </summary>
        void HandleChoice (string id);
    }
}
