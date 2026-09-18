using Naninovel.Parsing;

namespace Naninovel
{
    public class CommandLineParser : ScriptLineParser<CommandScriptLine, CommandLine>
    {
        protected virtual CommandParser CommandParser { get; }

        public CommandLineParser (ITextIdentifier identifier, IErrorHandler errorHandler = null)
        {
            CommandParser = new(identifier, errorHandler);
        }

        protected override CommandScriptLine Parse (CommandLine lineModel)
        {
            var spot = new PlaybackSpot(ScriptPath, LineIndex, 0);
            var args = new CommandParseArgs(lineModel.Command, spot, lineModel.Indent, Transient);
            var command = CommandParser.Parse(args);
            return new(command, LineIndex, lineModel.Indent, LineHash);
        }
    }
}
