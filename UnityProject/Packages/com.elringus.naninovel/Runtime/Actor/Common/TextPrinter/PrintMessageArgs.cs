using System;

namespace Naninovel
{
    /// <summary>
    /// Arguments associated with the print text events invoked by <see cref="ITextPrinterManager"/>. 
    /// </summary>
    public readonly struct PrintMessageArgs : IEquatable<PrintMessageArgs>
    {
        /// <summary>
        /// Printer actor which is printing the text.
        /// </summary>
        public readonly ITextPrinterActor Printer;
        /// <summary>
        /// Text of the printed message.
        /// </summary>
        public readonly PrintedMessage Message;
        /// <summary>
        /// Whether to append the message to the last printed one of the printer.
        /// </summary>
        public readonly bool Append;
        /// <summary>
        /// Text print speed (base reveal speed modifier).
        /// </summary>
        public readonly float Speed;

        public PrintMessageArgs (ITextPrinterActor printer, PrintedMessage message, bool append, float speed)
        {
            Printer = printer;
            Message = message;
            Append = append;
            Speed = speed;
        }

        public bool Equals (PrintMessageArgs other)
        {
            return Printer.Equals(other.Printer) &&
                   Message.Equals(other.Message) &&
                   Append == other.Append &&
                   Speed.Equals(other.Speed);
        }

        public override bool Equals (object obj)
        {
            return obj is PrintMessageArgs other && Equals(other);
        }

        public override int GetHashCode ()
        {
            return HashCode.Combine(Printer, Message, Append, Speed);
        }
    }
}
