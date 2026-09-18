using System;
using System.Collections.Generic;
using System.Linq;

namespace Naninovel
{
    /// <summary>
    /// Allows automatically generating and assigning
    /// persistent identifiers to localizable text parameters.
    /// </summary>
    public class ScriptTextIdentifier
    {
        /// <summary>
        /// Symbol prepended to text identifier indicating ID is a reference to another existing ID.
        /// The symbol is ignored when resolving text value from hash map.
        /// </summary>
        public const string RefPrefix = "&";
        /// <summary>
        /// Symbol prepended to text identifier indicating the unstable nature,
        /// such as being generated automatically from content hashes.
        /// </summary>
        public const string VolatilePrefix = "~";

        /// <summary>
        /// Identification options.
        /// </summary>
        public readonly struct Options
        {
            /// <summary>
            /// Will generate text IDs above the revision; use to prevent collisions.
            /// </summary>
            public readonly int Revision;
            /// <summary>
            /// Path of the identified script asset; used when logging errors.
            /// </summary>
            public readonly string AssetPath;

            public Options (int rev = 0, string path = null)
            {
                Revision = rev;
                AssetPath = path;
            }
        }

        /// <summary>
        /// Text identification result.
        /// </summary>
        public readonly struct Result
        {
            /// <summary>
            /// Line indexes that was modified (had identifiers added or modified).
            /// </summary>
            public readonly IReadOnlyList<int> ModifiedLines;
            /// <summary>
            /// Largest generated text ID (revision).
            /// </summary>
            public readonly int Revision;

            public Result (IReadOnlyList<int> lines, int rev)
            {
                ModifiedLines = lines;
                Revision = rev;
            }
        }

        private readonly List<Action<string>> modifications = new();
        private readonly HashSet<string> existingIds = new();
        private readonly HashSet<int> modifiedLineIndexes = new();
        private string scriptPath;
        private Script script;
        private int lineIndex;

        /// <summary>
        /// Mutates specified script converting all plain text raw parts of localizable parameters to identified.
        /// </summary>
        public Result Identify (Script script, Options options = default)
        {
            Reset(script, options.AssetPath);
            for (lineIndex = 0; lineIndex < script.Lines.Count; lineIndex++)
                ProcessLine(script.Lines[lineIndex]);
            return new(modifiedLineIndexes.ToArray(), ApplyModifications(options.Revision));
        }

        private void Reset (Script script, string scriptPath)
        {
            this.script = script;
            this.scriptPath = scriptPath;
            modifications.Clear();
            existingIds.Clear();
            modifiedLineIndexes.Clear();
        }
        private void ProcessLine (ScriptLine line)
        {
            if (line is CommandScriptLine commandLine)
                ProcessCommand(commandLine.Command);
            else if (line is GenericTextScriptLine genericLine)
                foreach (var command in genericLine.InlinedCommands)
                    ProcessCommand(command);
        }

        private void ProcessCommand (Command command)
        {
            foreach (var info in CommandParameter.Extract(command))
                ProcessParameter(info.Instance);
        }

        private void ProcessParameter (ICommandParameter param)
        {
            var textParam = param as LocalizableTextParameter;
            if (textParam?.RawValue == null) return;
            for (var i = 0; i < textParam.RawValue.Value.Parts.Count; i++)
                ProcessValuePart(textParam.RawValue.Value.Parts, i);
        }

        private void ProcessValuePart (IReadOnlyList<RawValuePart> parts, int index)
        {
            if (IsMissingStableId(parts[index]))
            {
                modifications.Add(id => {
                    ((ScriptTextMap.SerializableTextMap)script.TextMap.Map)[id] = ResolveText(parts[index]);
                    ((IList<RawValuePart>)parts)[index] = RawValuePart.FromIdentifiedText(id);
                });
                modifiedLineIndexes.Add(lineIndex);
            }
            else if (parts[index].Kind == ParameterValuePartKind.IdentifiedText)
                if (!existingIds.Add(parts[index].Id) && !parts[index].Id.StartsWithFast(RefPrefix))
                    NotifyCollision(parts[index].Id);
        }

        private bool IsMissingStableId (RawValuePart part)
        {
            return part.Kind == ParameterValuePartKind.PlainText ||
                   (part.Kind == ParameterValuePartKind.IdentifiedText && part.Id.StartsWithFast(VolatilePrefix));
        }

        private string ResolveText (RawValuePart part)
        {
            if (part.Kind == ParameterValuePartKind.PlainText) return part.Text;
            return script.TextMap.GetTextOrNull(part.Id);
        }

        private int ApplyModifications (int revision)
        {
            foreach (var mod in modifications)
            {
                while (existingIds.Contains((++revision).ToString("x")))
                    continue;
                mod(revision.ToString("x"));
            }
            return revision;
        }

        private void NotifyCollision (string id)
        {
            var path = scriptPath != null ? StringUtils.BuildAssetLink(scriptPath, lineIndex + 1) : $"{script.Path}:{lineIndex + 1}";
            Engine.Warn($"Text ID '{id}' used multiple times at '{path}'. All IDs should be unique inside script document." +
                        " Either remove the ID and let it auto-regenerate or manually assign unique ID. In case you're intentionally creating" +
                        " a reference, prepend & to the ID.");
        }
    }
}
