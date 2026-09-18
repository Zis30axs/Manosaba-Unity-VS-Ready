using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Naninovel
{
    public class FountainSplitter
    {
        public class Section
        {
            public string Path { get; set; }
            public string Text { get; set; }
        }

        private static readonly char[] invalidChars = Path.GetInvalidFileNameChars();
        private readonly StringBuilder builder = new();
        private readonly List<Section> sections = new();
        private readonly Stack<string> level = new();

        public Section[] Split (string text)
        {
            Reset();
            foreach (var line in text.IterateLines())
                ProcessLine(line);
            if (builder.Length > 0) AddSection();
            return sections.ToArray();
        }

        private void Reset ()
        {
            builder.Clear();
            sections.Clear();
            level.Clear();
        }

        private void ProcessLine (string lineText)
        {
            if (IsSectionLine(lineText)) ProcessSectionLine(lineText);
            else builder.Append(lineText).Append('\n');
        }

        private bool IsSectionLine (string lineText)
        {
            return lineText.TrimFull().StartsWithFast("#");
        }

        private void ProcessSectionLine (string sectionLine)
        {
            if (builder.Length > 0) AddSection();
            var name = ExtractSectionName(sectionLine);
            var curLevel = level.Count;
            var newLevel = CountSectionLevel(sectionLine);
            if (newLevel > curLevel + 1)
                for (int i = 0; i < newLevel - curLevel - 1; i++)
                    level.Push("_");
            else
                for (int i = 0; i <= curLevel - newLevel; i++)
                    level.Pop();
            level.Push(name);
        }

        private void AddSection ()
        {
            var path = level.Count == 0 ? "_" : string.Join("/", level.Reverse());
            var section = new Section { Path = path, Text = builder.ToString() };
            sections.Add(section);
            builder.Clear();
        }

        private static int CountSectionLevel (string sectionLine)
        {
            var level = 0;
            for (int i = sectionLine.IndexOf('#'); i < sectionLine.Length; i++)
                if (sectionLine[i] == '#') level++;
                else break;
            return level;
        }

        private static string ExtractSectionName (string sectionLine)
        {
            var builder = new StringBuilder();
            for (int i = sectionLine.LastIndexOf('#') + 1; i < sectionLine.Length; i++)
                if (invalidChars.Any(c => c == sectionLine[i])) builder.Append('_');
                else builder.Append(sectionLine[i]);
            return builder.ToString().TrimFull();
        }
    }
}
