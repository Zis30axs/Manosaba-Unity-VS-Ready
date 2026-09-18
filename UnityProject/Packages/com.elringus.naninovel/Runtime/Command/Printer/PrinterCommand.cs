namespace Naninovel.Commands
{
    public abstract class PrinterCommand : Command
    {
        protected abstract string AssignedPrinterId { get; }
        protected virtual string AssignedAuthorId => null;

        protected ITextPrinterManager PrinterManager => Engine.GetServiceOrErr<ITextPrinterManager>();
        protected ICharacterManager CharacterManager => Engine.GetServiceOrErr<ICharacterManager>();
        protected TextPrintersConfiguration Configuration => PrinterManager.Configuration;

        public virtual UniTask PreloadResources ()
        {
            return GetOrAddPrinter();
        }

        public virtual void ReleaseResources () { }

        protected virtual async UniTask<ITextPrinterActor> GetOrAddPrinter (AsyncToken token = default)
        {
            var printerId = default(string);

            if (string.IsNullOrEmpty(AssignedPrinterId) && !string.IsNullOrEmpty(AssignedAuthorId))
                printerId = CharacterManager.Configuration.GetMetadataOrDefault(AssignedAuthorId).LinkedPrinter;

            if (string.IsNullOrEmpty(printerId))
                printerId = AssignedPrinterId;

            var printer = await PrinterManager.GetOrAddActor(printerId ?? PrinterManager.DefaultPrinterId);
            token.ThrowIfCanceled();
            return printer;
        }
    }
}
