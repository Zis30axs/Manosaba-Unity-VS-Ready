using System.Collections.Generic;
using System.Linq;

namespace Naninovel.Spreadsheet
{
    public readonly struct SheetColumn
    {
        public IReadOnlyList<string> Cells { get; }
        public string Header => Cells?.ElementAtOrDefault(0);

        public SheetColumn (string[] cells)
        {
            Cells = cells;
        }
    }
}
