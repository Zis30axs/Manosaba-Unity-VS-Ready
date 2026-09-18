using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Naninovel.Parsing;

namespace Naninovel
{
    public class FountainConverter
    {
        public class Script
        {
            public string LocalPath { get; set; }
            public string ScriptText { get; set; }
        }

        private readonly FountainSplitter splitter = new();
        private readonly List<Script> scripts = new();
        private readonly List<IScriptLine> lines = new();
        private readonly Action<string, float> progress;

        private string character;

        public FountainConverter (Action<string, float> progress)
        {
            this.progress = progress;
        }

        public Script[] Convert (string text)
        {
            scripts.Clear();
            progress("Splitting sections...", 0.2f);
            var sections = splitter.Split(text);
            for (int i = 0; i < sections.Length; i++)
            {
                progress($"Converting {sections[i].Path}...", (float)i / sections.Length);
                ProcessSection(sections[i]);
            }
            return scripts.ToArray();
        }

        private void ProcessSection (FountainSplitter.Section section)
        {
            foreach (var line in section.Text.IterateLines())
                ProcessLine(line);
            var scriptText = Compiler.ScriptSerializer.Serialize(lines);
            if (!string.IsNullOrWhiteSpace(scriptText))
                scripts.Add(new() { LocalPath = section.Path + ".nani", ScriptText = scriptText });
            lines.Clear();
        }

        private void ProcessLine (string line)
        {
            if (character != null) ProcessLineAfterCharacter(line);
            else if (IsCharacter(line)) character = line;
            else if (IsAction(line)) lines.Add(BuildGenericLine(line));
            else if (ShouldAddComment(line)) lines.Add(new CommentLine(line));
        }

        private void ProcessLineAfterCharacter (string line)
        {
            if (!string.IsNullOrWhiteSpace(line))
                lines.Add(BuildAuthoredLine(character, line));
            else character = null;
        }

        private static GenericLine BuildAuthoredLine (string character, string text)
        {
            var builder = new StringBuilder();
            foreach (var c in character)
                if (char.IsLetterOrDigit(c)) builder.Append(c);
                else if (char.IsWhiteSpace(c)) builder.Append('_');
            var author = builder.ToString();
            return new(new(author), new[] { new MixedValue(new IValueComponent[] { new PlainText(text) }) });
        }

        private static GenericLine BuildGenericLine (string text)
        {
            return new(new[] { new MixedValue(new IValueComponent[] { new PlainText(text) }) });
        }

        private bool IsCharacter (string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return false;
            var trimmed = line.TrimFull();
            if (trimmed.StartsWithFast("@")) return true;
            if (!char.IsUpper(trimmed.FirstOrDefault())) return false;
            foreach (var c in trimmed)
                if (c == '(') return true;
                else if (char.IsWhiteSpace(c)) continue;
                else if (!char.IsUpper(c) && !char.IsDigit(c)) return false;
            return true;
        }

        private bool ShouldAddComment (string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return false;
            return line.Any(char.IsLetter);
        }

        private bool IsAction (string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return false;
            var trimmed = line.TrimFull();
            return char.IsLetterOrDigit(trimmed.FirstOrDefault()) &&
                   !trimmed.StartsWithFast("INT ") &&
                   !trimmed.StartsWithFast("INT.") &&
                   !trimmed.StartsWithFast("EXT ") &&
                   !trimmed.StartsWithFast("EXT.") &&
                   !trimmed.StartsWithFast("EST ") &&
                   !trimmed.StartsWithFast("EST.") &&
                   !trimmed.StartsWithFast("INT./EXT ") &&
                   !trimmed.StartsWithFast("INT./EXT.") &&
                   !trimmed.StartsWithFast("INT/EXT ") &&
                   !trimmed.StartsWithFast("INT/EXT.") &&
                   !trimmed.StartsWithFast("I/E ") &&
                   !trimmed.StartsWithFast("I/E.") &&
                   !trimmed.StartsWithFast("INT ") &&
                   !trimmed.StartsWithFast("INT.") &&
                   !trimmed.EndsWithFast("TO:");
        }
    }
}
