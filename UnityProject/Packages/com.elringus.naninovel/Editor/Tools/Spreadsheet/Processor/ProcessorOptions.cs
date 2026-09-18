using System;

namespace Naninovel.Spreadsheet
{
    public class ProcessorOptions
    {
        public string ScriptFolder { get; set; }
        public string TextFolder { get; set; }
        public string L10nFolder { get; set; }
        public string OutputFolder { get; set; }
        public string SourceLocale { get; set; }
        public bool Annotate { get; set; }

        public Action<ProcessorProgress> OnProgress { get; set; }
    }
}
