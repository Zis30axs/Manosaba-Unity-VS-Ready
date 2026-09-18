using System.Collections.Generic;

namespace Naninovel
{
    /// <summary>
    /// An actor which is able to present, format and gradually reveal text messages.
    /// </summary>
    /// <remarks>
    /// While most printers use a single text block and concatenate multiple <see cref="Messages"/>,
    /// some may choose to distinguish the messages, for example chat printer.
    /// Additionally, <see cref="Templates"/> are expected to be applied to each message individually,
    /// which allows visually distinguishing even the concatenated messages, for example via line breaks.
    /// The way <see cref="RevealProgress"/> applies to the messages is up to the printer implementation.
    /// </remarks>
    public interface ITextPrinterActor : IActor
    {
        /// <summary>
        /// Printed text messages.
        /// </summary>
        IReadOnlyList<PrintedMessage> Messages { get; set; }
        /// <summary>
        /// Formatting templates applied to the printed messages.
        /// </summary>
        IReadOnlyList<MessageTemplate> Templates { get; set; }
        /// <summary>
        /// The reveal ratio of the printed messages, in 0.0 to 1.0 range.
        /// </summary>
        float RevealProgress { get; set; }

        /// <summary>
        /// Adds specified printed message.
        /// </summary>
        void AddMessage (PrintedMessage message);
        /// <summary>
        /// Appends specified text to the last printed message, or adds a new message
        /// with the specified text when no <see cref="Messages"/> is empty.
        /// </summary>
        void AppendText (LocalizableText text);
        /// <summary>
        /// Reveals the assigned messages over time.
        /// </summary>
        /// <param name="delay">Delay (in seconds) to wait after revealing each text character.</param>
        UniTask Reveal (float delay, AsyncToken token = default);
    }
}
