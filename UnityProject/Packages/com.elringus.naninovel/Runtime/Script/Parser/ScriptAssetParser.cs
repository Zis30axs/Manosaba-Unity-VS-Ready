using System.Collections.Generic;
using System.Linq;
using Naninovel.Parsing;

namespace Naninovel
{
    /// <inheritdoc cref="IScriptParser"/>
    public class ScriptAssetParser : IScriptParser
    {
        protected virtual CommentLineParser CommentLineParser { get; }
        protected virtual LabelLineParser LabelLineParser { get; }
        protected virtual CommandLineParser CommandLineParser { get; }
        protected virtual GenericTextLineParser GenericTextLineParser { get; }

        private readonly List<ScriptLine> lines = new();
        private readonly ParseErrorHandler errorHandler = new();
        private readonly TextMapper textMapper = new();
        private readonly ScriptParser modelParser;

        public ScriptAssetParser ()
        {
            CommentLineParser = new();
            LabelLineParser = new();
            CommandLineParser = new(textMapper, errorHandler);
            GenericTextLineParser = new(textMapper, errorHandler);
            modelParser = new(new() {
                Syntax = Compiler.Syntax,
                Handlers = new() { ErrorHandler = errorHandler, TextIdentifier = textMapper }
            });
        }

        public virtual Script ParseText (string scriptPath, string scriptText, ParseOptions options = default)
        {
            Reset(options);
            var textLines = ScriptParser.SplitText(scriptText);
            for (int i = 0; i < textLines.Length; i++)
                lines.Add(ParseLine(i, textLines[i]));
            return Script.Create(scriptPath, lines.ToArray(), CreateTextMap());

            ScriptLine ParseLine (int lineIndex, string lineText)
            {
                errorHandler.LineIndex = lineIndex;
                switch (modelParser.ParseLine(lineText))
                {
                    case CommentLine comment: return ParseCommentLine(new(scriptPath, lineText, lineIndex, options.Transient, comment));
                    case LabelLine label: return ParseLabelLine(new(scriptPath, lineText, lineIndex, options.Transient, label));
                    case CommandLine command: return ParseCommandLine(new(scriptPath, lineText, lineIndex, options.Transient, command));
                    case GenericLine generic:
                        if (string.IsNullOrWhiteSpace(lineText)) return new EmptyScriptLine(lineIndex, generic.Indent);
                        return ParseGenericTextLine(new(scriptPath, lineText, lineIndex, options.Transient, generic));
                    default: throw new Error($"Unknown line type: {lineText}");
                }
            }
        }

        protected virtual CommentScriptLine ParseCommentLine (LineParseArgs<CommentLine> args)
        {
            return CommentLineParser.Parse(args);
        }

        protected virtual LabelScriptLine ParseLabelLine (LineParseArgs<LabelLine> args)
        {
            return LabelLineParser.Parse(args);
        }

        protected virtual CommandScriptLine ParseCommandLine (LineParseArgs<CommandLine> args)
        {
            return CommandLineParser.Parse(args);
        }

        protected virtual GenericTextScriptLine ParseGenericTextLine (LineParseArgs<GenericLine> args)
        {
            return GenericTextLineParser.Parse(args);
        }

        protected virtual ScriptTextMap CreateTextMap ()
        {
            return new(textMapper.Map.ToDictionary(kv => kv.Key, kv => kv.Value));
        }

        private void Reset (ParseOptions options)
        {
            errorHandler.Errors = options.Errors;
            lines.Clear();
            textMapper.Clear();
        }
    }
}
