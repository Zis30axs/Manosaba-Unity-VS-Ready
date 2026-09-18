using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents '.nani' scenario script file serialized as Unity asset.
    /// </summary>
    [Serializable]
    public class Script : ScriptableObject
    {
        /// <summary>
        /// Prepended to the paths of the transient scripts.
        /// </summary>
        public const string TransientPathPrefix = "~";

        /// <summary>
        /// Unique (project-wide) local resource path of the script.
        /// </summary>
        public string Path => path;
        /// <summary>
        /// Map of the identified (localizable) text contained in the script.
        /// </summary>
        public ScriptTextMap TextMap => textMap;
        /// <summary>
        /// The list of lines this script contains, in order.
        /// </summary>
        public IReadOnlyList<ScriptLine> Lines => lines;
        /// <summary>
        /// Playlist associated with the script.
        /// </summary>
        public ScriptPlaylist Playlist => playlist;

        [SerializeField] private string path;
        [SerializeField] private ScriptTextMap textMap;
        [SerializeField] private ScriptPlaylist playlist;
        [SerializeReference] private ScriptLine[] lines;

        /// <summary>
        /// Creates new script asset from specified compiled (pre-parsed) lines.
        /// </summary>
        /// <param name="path">Unique (project-wide) local resource path of the script.</param>
        /// <param name="lines">Parsed lines of the script, in order.</param>
        /// <param name="textMap">Map of identified (localizable) text contained in the script.</param>
        public static Script Create (string path, ScriptLine[] lines, ScriptTextMap textMap)
        {
            var asset = CreateInstance<Script>();
            asset.name = System.IO.Path.GetFileName(path);
            asset.path = path;
            asset.lines = lines;
            asset.textMap = textMap;
            asset.playlist = new(path, asset.ExtractCommands());
            return asset;
        }

        /// <summary>
        /// Creates new script asset by parsing specified source script text.
        /// </summary>
        /// <param name="path">Unique (project-wide) local resource path of the script.</param>
        /// <param name="text">The source script text to parse.</param>
        /// <param name="file">Optional location of the script file to be used in error logs.</param>
        public static Script FromText (string path, string text, string file = null)
        {
            var logger = ScriptParseErrorLogger.GetFor(file ?? path);
            var script = Compiler.ScriptAssetParser.ParseText(path, text, new(logger, false));
            ScriptParseErrorLogger.Return(logger);
            return script;
        }

        /// <summary>
        /// Creates new transient script instance by parsing specified source script text.
        /// </summary>
        /// <remarks>
        /// Use this method when creating "one-off" scripts at runtime to prevent localizable text resolve errors
        /// and other issues related to the fact, that the instance won't be properly registered as a script resource.
        /// </remarks>
        /// <param name="name">Arbitrary name of the script to identify it in the error logs.</param>
        /// <param name="text">The source script text to parse.</param>
        public static Script FromTransient (string name, string text)
        {
            var path = $"{TransientPathPrefix}/{name}";
            var logger = ScriptParseErrorLogger.GetFor(path);
            var script = Compiler.ScriptAssetParser.ParseText(path, text, new(logger, true));
            ScriptParseErrorLogger.Return(logger);
            return script;
        }

        /// <summary>
        /// Collects all the contained commands (preserving the order).
        /// </summary>
        public List<Command> ExtractCommands ()
        {
            var commands = new List<Command>();
            foreach (var line in lines)
                if (line is CommandScriptLine commandLine)
                    commands.Add(commandLine.Command);
                else if (line is GenericTextScriptLine genericLine)
                    commands.AddRange(genericLine.InlinedCommands);
            return commands;
        }

        /// <summary>
        /// Returns first script line of <typeparamref name="TLine"/> filtered by <paramref name="predicate"/> or null.
        /// </summary>
        public TLine FindLine<TLine> (Predicate<TLine> predicate) where TLine : ScriptLine
        {
            return lines.FirstOrDefault(l => l is TLine tline && predicate(tline)) as TLine;
        }

        /// <summary>
        /// Returns all the script lines of <typeparamref name="TLine"/> filtered by <paramref name="predicate"/>.
        /// </summary>
        public List<TLine> FindLines<TLine> (Predicate<TLine> predicate) where TLine : ScriptLine
        {
            return lines.Where(l => l is TLine tline && predicate(tline)).Cast<TLine>().ToList();
        }

        /// <summary>
        /// Checks whether a <see cref="LabelScriptLine"/> with the specified value exists in this script.
        /// </summary>
        public bool LabelExists (string label)
        {
            foreach (var line in lines)
                if (line is LabelScriptLine labelLine && labelLine.LabelText.EqualsFast(label))
                    return true;
            return false;
        }

        /// <summary>
        /// Attempts to retrieve index of a <see cref="LabelScriptLine"/> with the specified <see cref="LabelScriptLine.LabelText"/>.
        /// Returns -1 in case the label is not found.
        /// </summary>
        public int GetLineIndexForLabel (string label)
        {
            foreach (var line in lines)
                if (line is LabelScriptLine labelLine && labelLine.LabelText.EqualsFast(label))
                    return labelLine.LineIndex;
            return -1;
        }

        /// <summary>
        /// Returns first <see cref="LabelScriptLine.LabelText"/> located above line with the specified index.
        /// Returns null when not found.
        /// </summary>
        public string GetLabelForLine (int lineIndex)
        {
            if (!lines.IsIndexValid(lineIndex)) return null;
            for (var i = lineIndex; i >= 0; i--)
                if (lines[i] is LabelScriptLine labelLine)
                    return labelLine.LabelText;
            return null;
        }

        /// <summary>
        /// Returns first <see cref="CommentScriptLine.CommentText"/> located above line with the specified index.
        /// Returns null when not found.
        /// </summary>
        public string GetCommentForLine (int lineIndex)
        {
            if (!lines.IsIndexValid(lineIndex)) return null;
            for (var i = lineIndex; i >= 0; i--)
                if (lines[i] is CommentScriptLine commentLine)
                    return commentLine.CommentText;
            return null;
        }
    }
}
