using System;
using System.Collections.Generic;
using System.Linq;
using Naninovel.Commands;
using Naninovel.Parsing;
using static Naninovel.Command;
using static Naninovel.CommandParameter;

namespace Naninovel
{
    public class GenericTextLineParser : ScriptLineParser<GenericTextScriptLine, GenericLine>
    {
        protected virtual CommandParser CommandParser { get; }
        protected virtual GenericLine Model { get; private set; }
        protected virtual IList<Command> InlinedCommands { get; } = new List<Command>();
        protected virtual string AuthorId => Model.Prefix?.Author ?? "";
        protected virtual string AuthorAppearance => Model.Prefix?.Appearance ?? "";
        protected virtual PlaybackSpot Spot => new(ScriptPath, LineIndex, InlinedCommands.Count);

        private readonly MixedValueParser mixedParser;
        private readonly IErrorHandler errorHandler;
        private readonly Type printCommandType;

        public GenericTextLineParser (ITextIdentifier identifier, IErrorHandler errorHandler = null)
        {
            this.errorHandler = errorHandler;
            mixedParser = new(identifier);
            CommandParser = new(identifier, errorHandler);
            printCommandType = CommandTypes.Values.First(typeof(PrintText).IsAssignableFrom);
        }

        protected override GenericTextScriptLine Parse (GenericLine lineModel)
        {
            ResetState(lineModel);
            AddAppearanceChange();
            AddContent();
            AddLastWaitInput();
            return new(InlinedCommands, LineIndex, lineModel.Indent, LineHash);
        }

        protected virtual void ResetState (GenericLine model)
        {
            Model = model;
            InlinedCommands.Clear();
        }

        protected virtual void AddAppearanceChange ()
        {
            if (string.IsNullOrEmpty(AuthorId)) return;
            if (string.IsNullOrEmpty(AuthorAppearance)) return;
            AddCommand(new ModifyCharacter {
                IsGenericPrefix = true,
                IdAndAppearance = new NamedString(AuthorId, AuthorAppearance),
                Wait = false,
                PlaybackSpot = Spot,
                Indent = Model.Indent
            });
        }

        protected virtual void AddContent ()
        {
            foreach (var content in Model.Content)
                if (content is InlinedCommand inlined)
                    if (string.IsNullOrEmpty(inlined.Command.Identifier)) continue;
                    else AddCommand(inlined.Command);
                else AddGenericText(content as MixedValue);
        }

        protected virtual void AddCommand (Parsing.Command commandModel)
        {
            var spot = new PlaybackSpot(ScriptPath, LineIndex, InlinedCommands.Count);
            var args = new CommandParseArgs(commandModel, spot, Model.Indent, Transient);
            var command = CommandParser.Parse(args);
            AddCommand(command);
        }

        protected virtual void AddCommand (Command command)
        {
            if (command is ParametrizeGeneric param)
                ParameterizeLastPrint(param);

            // Route [i] after printed text to wait input param of the print command.
            if (command is WaitForInput && InlinedCommands.LastOrDefault() is PrintText print)
                print.WaitForInput = true;
            else InlinedCommands.Add(command);
        }

        protected virtual void ParameterizeLastPrint (ParametrizeGeneric p)
        {
            var print = InlinedCommands.LastOrDefault(c => c is PrintText) as PrintText;
            if (print is null)
                throw new Error(Engine.FormatMessage(
                    "Failed to parametrize generic text: make sure [< ...] is inlined after text.", Spot));

            if (Assigned(p.PrinterId)) print.PrinterId = Ref(p.PrinterId);
            if (Assigned(p.AuthorId)) print.AuthorId = Ref(p.AuthorId);
            if (Assigned(p.AuthorLabel)) print.AuthorLabel = Ref(p.AuthorLabel);
            if (Assigned(p.RevealSpeed)) print.RevealSpeed = Ref(p.RevealSpeed);
            if (Assigned(p.SkipWaitingInput))
                if (!p.SkipWaitingInput.DynamicValue) print.WaitForInput = !p.SkipWaitingInput;
                else throw new Error(Engine.FormatMessage("Dynamic 'skip' in [< ...] is not supported.", Spot));
            if (Assigned(p.Join))
                if (!p.Join.DynamicValue) print.ResetPrinter = !p.Join;
                else throw new Error(Engine.FormatMessage("Dynamic 'join' in [< ...] is not supported.", Spot));
        }

        protected virtual void AddGenericText (MixedValue genericText)
        {
            var printedBefore = InlinedCommands.Any(c => c is PrintText);
            var print = (PrintText)Activator.CreateInstance(printCommandType);
            var raw = mixedParser.Parse(genericText, !Transient);
            print.Text = FromRaw<LocalizableTextParameter>(raw, Spot, out var errors);
            if (errors != null) errorHandler?.HandleError(new(errors, 0, 0));
            if (!string.IsNullOrEmpty(AuthorId)) print.AuthorId = AuthorId;
            if (printedBefore)
            {
                print.Append = true;
                print.ResetPrinter = false;
            }
            print.Wait = true;
            print.WaitForInput = false;
            print.PlaybackSpot = Spot;
            print.Indent = Model.Indent;
            AddCommand(print);
        }

        protected virtual void AddLastWaitInput ()
        {
            if (!InlinedCommands.Any(c => c is PrintText)) return;
            if (InlinedCommands.Any(c => c is ParametrizeGeneric p && Assigned(p.SkipWaitingInput) && p.SkipWaitingInput)) return;
            var last = InlinedCommands.LastOrDefault();
            if (last is ParametrizeGeneric p && Assigned(p.SkipWaitingInput) && !p.SkipWaitingInput) return;
            if (last is WaitForInput) return;
            if (last is PrintText print) print.WaitForInput = true;
            else AddCommand(new WaitForInput { PlaybackSpot = Spot, Indent = Model.Indent });
        }
    }
}
