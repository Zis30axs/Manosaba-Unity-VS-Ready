using System.Collections.Generic;

namespace Naninovel.Spreadsheet
{
    public readonly struct Sheet
    {
        public IReadOnlyList<SheetColumn> Columns { get; }

        public Sheet (SheetColumn[] columns)
        {
            Columns = columns;
        }
    }
}
