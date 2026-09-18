using UnityEngine;

namespace Naninovel.FX
{
    /// <summary>
    /// Shakes a <see cref="ITextPrinterActor"/> with specified ID or an active one.
    /// </summary>
    public class ShakePrinter : ShakeTransform
    {
        protected override Transform GetShakenTransform ()
        {
            var manager = Engine.GetServiceOrErr<ITextPrinterManager>();
            var id = string.IsNullOrEmpty(ObjectName) ? manager.DefaultPrinterId : ObjectName;
            if (id is null || !manager.ActorExists(id))
                throw new Error($"Failed to shake printer with '{id}' ID: actor not found.");
            return (manager.GetActor(id) as UITextPrinter)?.PrinterPanel.Content;
        }
    }
}
