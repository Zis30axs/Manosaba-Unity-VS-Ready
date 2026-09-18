using System;

namespace Naninovel.Commands
{
    [Doc(
        @"
Assigns [formatting templates](/guide/text-printers#message-templates) to be applied for printed messages.",
        @"
You can also format printed text with [style tags](/guide/text-printers#text-styles).",
        @"
; Print first two sentences in bold red text with 45px size,
; then reset the style and print the last sentence using default style.
@format <color=#ff0000><b><size=45>%TEXT%</size></b></color>
Lorem ipsum dolor sit amet.
Cras ut nisi eget ex viverra egestas in nec magna.
@format default
Consectetur adipiscing elit.",
        @"
; Instead of using the @format command, it's possible to apply the styles
; to the printed text directly.
Lorem ipsum sit amet. <b>Consectetur adipiscing elit.</b>"
    )]
    [CommandAlias("format")]
    public class FormatText : PrinterCommand
    {
        [Doc("The templates to apply, in `Template.AuthorFilter` format; see the [formatting templates](/guide/text-printers#message-templates) guide for more info.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter, ActorContext(CharactersConfiguration.DefaultPathPrefix, 1)]
        public NamedStringListParameter Templates;
        [Doc("ID of the printer actor to assign templates for. Will use a default one when not specified.")]
        [ParameterAlias("printer"), ActorContext(TextPrintersConfiguration.DefaultPathPrefix)]
        public StringParameter PrinterId;

        protected override string AssignedPrinterId => PrinterId;

        public override async UniTask Execute (AsyncToken token = default)
        {
            var printer = await GetOrAddPrinter(token);
            if (ShouldResetTemplates()) ResetTemplates(printer);
            else AssignTemplates(printer);
        }

        protected virtual bool ShouldResetTemplates ()
        {
            return Templates.Length == 1 && Templates[0].Value.Name.EqualsFastIgnoreCase("default");
        }

        protected void ResetTemplates (ITextPrinterActor printer)
        {
            printer.Templates = Array.Empty<MessageTemplate>();
        }

        protected void AssignTemplates (ITextPrinterActor printer)
        {
            using var _ = ListPool<MessageTemplate>.Rent(out var templates);
            foreach (var template in Templates)
                templates.Add(new(template.NamedValue.HasValue ? template.NamedValue : "*", template.Name));
            printer.Templates = templates;
        }
    }
}
