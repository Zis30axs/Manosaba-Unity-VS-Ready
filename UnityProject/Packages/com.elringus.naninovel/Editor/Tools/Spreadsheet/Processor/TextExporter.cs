using System.Collections.Generic;
using System.IO;
using Naninovel.ManagedText;
using static Naninovel.Spreadsheet.Constants;

namespace Naninovel.Spreadsheet
{
    public class TextExporter
    {
        protected virtual string L10nFolder { get; }
        protected virtual string OutputFolder { get; }
        protected virtual string SourceLocale { get; }
        protected virtual bool Annotate { get; }
        protected virtual List<SheetColumn> Columns { get; } = new();
        protected virtual Dictionary<string, int> KeyToIndex { get; } = new();
        protected virtual List<string> Cells { get; } = new();
        protected virtual SheetWriter SheetWriter { get; } = new();

        public TextExporter (string l10nFolder, string outputFolder, string sourceLocale, bool annotate)
        {
            L10nFolder = l10nFolder;
            OutputFolder = outputFolder;
            SourceLocale = sourceLocale;
            Annotate = annotate;
        }

        public virtual void Export (string filePath)
        {
            Reset();
            var sourceFileName = Path.GetFileNameWithoutExtension(filePath);
            var documentText = File.ReadAllText(filePath);
            var doc = ManagedTextUtils.Parse(documentText, name: filePath);
            var sheet = BuildSheet(doc, sourceFileName);
            WriteSheet(sheet, sourceFileName);
        }

        protected virtual void Reset ()
        {
            Columns.Clear();
            KeyToIndex.Clear();
            Cells.Clear();
        }

        protected virtual Sheet BuildSheet (ManagedTextDocument doc, string sourceFileName)
        {
            AppendKeysColumnAndIndexKeys(doc);
            if (Annotate) AppendAnnotationsColumn(doc);
            AppendSourceColumn(doc);
            foreach (var localeFolder in Directory.EnumerateDirectories(L10nFolder))
                AppendLocaleColumn(PathUtils.FormatPath(localeFolder), sourceFileName);
            return new(Columns.ToArray());
        }

        protected virtual void WriteSheet (Sheet sheet, string sourceFileName)
        {
            var path = PathUtils.Combine(OutputFolder, TextFolderName, sourceFileName + CsvExtension);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            SheetWriter.Write(sheet, path);
        }

        protected virtual void AppendKeysColumnAndIndexKeys (ManagedTextDocument doc)
        {
            Cells.Clear();
            Cells.Add(KeysColumnHeader);
            foreach (var record in doc.Records)
            {
                KeyToIndex[record.Key] = Cells.Count;
                Cells.Add(record.Key);
            }
            Columns.Add(new(Cells.ToArray()));
        }

        protected virtual void AppendAnnotationsColumn (ManagedTextDocument doc)
        {
            Cells.Clear();
            Cells.Add(AnnotationsColumnHeader);
            foreach (var record in doc.Records)
                AssignCell(record.Key, record.Comment ?? "");
            Columns.Add(new(Cells.ToArray()));
        }

        protected virtual void AppendSourceColumn (ManagedTextDocument doc)
        {
            Cells.Clear();
            Cells.Add(SourceLocale);
            foreach (var record in doc.Records)
                AssignCell(record.Key, record.Value);
            Columns.Add(new(Cells.ToArray()));
        }

        protected virtual void AppendLocaleColumn (string localeFolder, string sourceFileName)
        {
            if (!TryLoadL10nDocument(localeFolder, sourceFileName, out var doc)) return;
            Cells.Clear();
            Cells.Add(localeFolder.GetAfter("/"));
            foreach (var record in doc.Records)
                AssignCell(record.Key, record.Value);
            Columns.Add(new(Cells.ToArray()));
        }

        protected virtual void AssignCell (string key, string value)
        {
            if (KeyToIndex.TryGetValue(key, out var index))
                Cells.Insert(index, value);
            else Engine.Warn($"Failed to assign {value} to {key} while exporting script.");
        }

        protected virtual bool TryLoadL10nDocument (string localeFolder, string sourceFileName, out ManagedTextDocument doc)
        {
            var path = PathUtils.Combine(localeFolder, TextFolderName, sourceFileName + TextExtension);
            if (!File.Exists(path))
            {
                Engine.Warn($"Failed to load {path}: make sure localization is generated.");
                doc = null;
                return false;
            }
            doc = ManagedTextUtils.Parse(File.ReadAllText(path), sourceFileName, path);
            return true;
        }
    }
}
