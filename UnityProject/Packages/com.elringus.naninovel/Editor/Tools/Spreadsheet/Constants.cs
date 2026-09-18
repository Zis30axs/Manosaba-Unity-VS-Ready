namespace Naninovel.Spreadsheet
{
    public static class Constants
    {
        public const string KeysColumnHeader = "key";
        public const string AnnotationsColumnHeader = "annotation";
        public const string ScriptExtension = ".nani";
        public const string TextExtension = ".txt";
        public const string CsvExtension = ".csv";
        public const string ScriptPattern = "*" + ScriptExtension;
        public const string TextPattern = "*" + TextExtension;
        public const string CsvPattern = "*" + CsvExtension;
        public const string TextFolderName = ManagedTextConfiguration.DefaultPathPrefix;
        public const string ScriptFolderName = ManagedTextPaths.ScriptLocalizationPrefix;
    }
}
