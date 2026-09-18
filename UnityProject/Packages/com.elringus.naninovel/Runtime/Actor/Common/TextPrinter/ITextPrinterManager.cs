using System;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to manage <see cref="ITextPrinterActor"/> actors.
    /// </summary>
    public interface ITextPrinterManager : IActorManager<ITextPrinterActor, TextPrinterState, TextPrinterMetadata, TextPrintersConfiguration>
    {
        /// <summary>
        /// Invoked when a print text operation is started.
        /// </summary>
        event Action<PrintMessageArgs> OnPrintStarted;
        /// <summary>
        /// Invoked when a print text operation is finished.
        /// </summary>
        event Action<PrintMessageArgs> OnPrintFinished;

        /// <summary>
        /// ID of the printer actor to use by default when a specific one is not specified.
        /// </summary>
        string DefaultPrinterId { get; set; }
        /// <summary>
        /// Base speed for revealing text messages as per the game settings, in 0.0 to 1.0 range.
        /// </summary>
        float BaseRevealSpeed { get; set; }
        /// <summary>
        /// Base delay while waiting to continue in auto play mode (scaled by printed characters count) as per the game settings, in 0.0 to 1.0 range.
        /// </summary>
        float BaseAutoDelay { get; set; }

        /// <summary>
        /// Prints (reveals) specified message over time using printer with the specified ID.
        /// </summary>
        /// <param name="printerId">ID of the printer actor which should print the message.</param>
        /// <param name="message">Text message to print.</param>
        /// <param name="append">Whether to append printed text to the last message of the text printer.</param>
        /// <param name="speed">Text reveal speed (<see cref="BaseRevealSpeed"/> modifier).</param>
        UniTask Print (string printerId, PrintedMessage message, bool append = false, float speed = 1, AsyncToken token = default);
    }
}
