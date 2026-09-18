using System.Collections.Generic;
using System.IO;
using System.Linq;
using Naninovel.ManagedText;
using static Naninovel.Spreadsheet.Constants;

namespace Naninovel.Spreadsheet
{
    public class SheetImporter
    {
        protected virtual string L10nFolder { get; }
        protected virtual string SourceLocale { get; }
        protected virtual SheetReader SheetReader { get; } = new();
        protected virtual string CsvPath { get; private set; }
        protected virtual Sheet Sheet { get; private set; }
        protected virtual string LocalDocumentFilePath { get; private set; }
        protected virtual Dictionary<int, string> IndexToKey { get; } = new();

        public SheetImporter (string l10nFolder, string sourceLocale)
        {
            L10nFolder = l10nFolder;
            SourceLocale = sourceLocale;
        }

        public virtual void Import (string csvPath, string localDocumentFilePath)
        {
            Reset(csvPath, localDocumentFilePath);
            if (!TryGetKeyColumn(out var keyColumn)) return;
            IndexKeys(keyColumn);
            foreach (var column in Sheet.Columns)
                if (!IsKeyColumn(column) && !IsSourceColumn(column) && !IsAnnotationColumn(column))
                    ImportLocaleColumn(column);
        }

        protected virtual void Reset (string csvPath, string localDocumentFilePath)
        {
            CsvPath = csvPath;
            Sheet = SheetReader.Read(csvPath);
            LocalDocumentFilePath = localDocumentFilePath;
            IndexToKey.Clear();
        }

        protected virtual bool TryGetKeyColumn (out SheetColumn column)
        {
            column = Sheet.Columns.FirstOrDefault(IsKeyColumn);
            if (column.Cells is null) Engine.Warn($"Failed to import {CsvPath}: sheet is missing keys column.");
            return column.Cells != null;
        }

        protected virtual void IndexKeys (SheetColumn keysColumn)
        {
            for (int i = 1; i < keysColumn.Cells.Count; i++)
                IndexToKey[i] = keysColumn.Cells[i];
        }

        protected virtual bool IsKeyColumn (SheetColumn column)
        {
            return column.Header == KeysColumnHeader;
        }

        protected virtual bool IsAnnotationColumn (SheetColumn column)
        {
            return column.Header == AnnotationsColumnHeader;
        }

        protected virtual bool IsSourceColumn (SheetColumn column)
        {
            return column.Header == SourceLocale;
        }

        protected virtual void ImportLocaleColumn (SheetColumn column)
        {
            var locale = column.Header;
            if (string.IsNullOrWhiteSpace(locale)) return;
            if (!TryLoadL10nDocument(locale, out var doc, out var text, out var path)) return;
            var records = new List<ManagedTextRecord>(doc.Records);
            for (int i = 1; i < column.Cells.Count; i++)
            {
                IndexToKey.TryGetValue(i, out var key);
                var index = records.FindIndex(r => r.Key == key);
                if (index >= 0) records[index] = new(key, column.Cells[i], records[index].Comment);
                else Engine.Warn($"Failed to import '{key}' cell at '{column}' of {CsvPath}: {path} localization document is missing the key.");
            }
            var document = new ManagedTextDocument(records, doc.Header);
            var documentText = ManagedTextDetector.IsMultiline(text)
                ? ManagedTextUtils.SerializeMultiline(document)
                : ManagedTextUtils.SerializeInline(document);
            File.WriteAllText(path, documentText);
        }

        protected virtual bool TryLoadL10nDocument (string locale, out ManagedTextDocument doc, out string text, out string path)
        {
            doc = null;
            text = null;
            path = PathUtils.Combine(L10nFolder, locale, TextFolderName, LocalDocumentFilePath);
            if (!File.Exists(path))
            {
                Engine.Warn($"Failed to import '{locale}' column of {CsvPath}: missing localization document at {path}.");
                return false;
            }
            text = File.ReadAllText(path);
            doc = ManagedTextUtils.Parse(text, null, path);
            return true;
        }
    }
}
