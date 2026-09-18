using System.IO;
using System.Linq;
using System.Text;

namespace Naninovel.Spreadsheet
{
    public class SheetWriter
    {
        protected virtual Encoding Encoding { get; } = Encoding.UTF8;

        public virtual void Write (Sheet sheet, string path)
        {
            using var file = new FileStream(path, FileMode.Create);
            using var stream = new StreamWriter(file, Encoding);
            Write(sheet, new Csv.Writer(stream));
        }

        protected virtual void Write (Sheet sheet, Csv.Writer csv)
        {
            for (int rowIndex = 0; rowIndex < GetRowCount(sheet); rowIndex++)
            {
                foreach (var column in sheet.Columns)
                    csv.WriteField(column.Cells.ElementAtOrDefault(rowIndex));
                csv.NextRecord();
            }
        }

        protected virtual int GetRowCount (Sheet sheet)
        {
            return sheet.Columns[0].Cells.Count;
        }
    }
}
