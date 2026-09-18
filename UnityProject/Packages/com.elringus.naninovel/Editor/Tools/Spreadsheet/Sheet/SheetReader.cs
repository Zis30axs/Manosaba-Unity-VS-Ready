using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Naninovel.Spreadsheet
{
    public class SheetReader
    {
        protected virtual List<SheetColumn> Columns { get; } = new();
        protected virtual List<string[]> Rows { get; } = new();
        protected virtual List<string> Cells { get; } = new();
        protected virtual int ColumnCount { get; private set; }

        public virtual Sheet Read (string path)
        {
            Reset();
            using var file = new FileStream(path, FileMode.Open);
            using var stream = new StreamReader(file);
            // Disable trimming, because Microsoft Excel doesn't follow RFC 4180:
            // it doesn't wrap fields with leading/trailing space in quotes.
            return Read(new Csv.Reader(stream, new() { TrimFields = false }));
        }

        protected virtual void Reset ()
        {
            Columns.Clear();
            Rows.Clear();
            ColumnCount = -1;
        }

        protected virtual Sheet Read (Csv.Reader csv)
        {
            while (csv.ReadRow()) ReadRow(csv);
            return new(CreateColumns());
        }

        protected virtual void ReadRow (Csv.Reader csv)
        {
            Cells.Clear();
            if (ColumnCount < 0) ColumnCount = csv.FieldsCount;
            for (int i = 0; i < csv.FieldsCount; i++)
                Cells.Add(csv[i]);
            Rows.Add(Cells.ToArray());
        }

        protected virtual SheetColumn[] CreateColumns ()
        {
            for (int rowIdx = 0; rowIdx < ColumnCount; rowIdx++)
            {
                Cells.Clear();
                foreach (var cells in Rows)
                    Cells.Add(cells.ElementAtOrDefault(rowIdx) ?? "");
                Columns.Add(new(Cells.ToArray()));
            }
            return Columns.ToArray();
        }
    }
}
