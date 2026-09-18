using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using static Naninovel.Spreadsheet.Constants;

namespace Naninovel.Spreadsheet
{
    public class SpreadsheetWindow : EditorWindow
    {
        protected string InputScriptFolder { get => GetPref(); set => SetPrefAndValidate(value); }
        protected string InputTextFolder { get => GetPref(); set => SetPrefAndValidate(value); }
        protected string InputL10nFolder { get => GetPref(); set => SetPrefAndValidate(value); }
        protected string OutputFolder { get => GetPref(); set => SetPrefAndValidate(value); }
        protected bool Annotate { get => GetPref("true") == "true"; set => SetPrefAndValidate(value ? "true" : "false"); }
        protected string SourceLocale => Configuration.GetOrDefault<LocalizationConfiguration>().SourceLocale;
        protected bool PathsValid => string.IsNullOrEmpty(pathsError);

        private static readonly GUIContent inputScriptFolderContent = new("Input Scripts Folder", "Folder containing source naninovel script files (.nani).");
        private static readonly GUIContent inputTextFolderContent = new("Input Text Folder", "Folder containing source managed text files (.txt).");
        private static readonly GUIContent inputL10nFolderContent = new("Input Localization Folder", "Localization resources root folder.");
        private static readonly GUIContent spreadsheetPathContent = new("Output Folder", "Folder to store generated spreadsheets (.csv).");
        private static readonly GUIContent annotateContent = new("Include Annotations", "Whether to insert a column with script comments placed above localized lines.");

        private string pathsError;

        [MenuItem("Naninovel/Tools/Spreadsheet")]
        private static void OpenWindow ()
        {
            var position = new Rect(100, 100, 500, 210);
            GetWindowWithRect<SpreadsheetWindow>(position, true, "Spreadsheet", true);
        }

        private void OnEnable ()
        {
            ValidatePaths();
        }

        private void OnGUI ()
        {
            EditorGUILayout.LabelField("Naninovel Spreadsheet", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("The tool to export/import scenario script amd managed text localizations to/from spreadsheets.", EditorStyles.wordWrappedMiniLabel);

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                InputScriptFolder = EditorGUILayout.TextField(inputScriptFolderContent, InputScriptFolder);
                if (GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.Width(65)))
                    InputScriptFolder = EditorUtility.OpenFolderPanel(inputScriptFolderContent.text, "", "");
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                InputTextFolder = EditorGUILayout.TextField(inputTextFolderContent, InputTextFolder);
                if (GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.Width(65)))
                    InputTextFolder = EditorUtility.OpenFolderPanel(inputTextFolderContent.text, "", "");
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                InputL10nFolder = EditorGUILayout.TextField(inputL10nFolderContent, InputL10nFolder);
                if (GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.Width(65)))
                    InputL10nFolder = EditorUtility.OpenFolderPanel(inputL10nFolderContent.text, "", "");
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                OutputFolder = EditorGUILayout.TextField(spreadsheetPathContent, OutputFolder);
                if (GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.Width(65)))
                    OutputFolder = EditorUtility.OpenFolderPanel(spreadsheetPathContent.text, "", "");
            }

            Annotate = EditorGUILayout.Toggle(annotateContent, Annotate);

            GUILayout.FlexibleSpace();

            if (PathsValid)
            {
                if (GUILayout.Button("Export", GUIStyles.NavigationButton))
                    Export();

                if (GUILayout.Button("Import", GUIStyles.NavigationButton))
                    Import();
            }
            else EditorGUILayout.HelpBox(pathsError, MessageType.Error);

            EditorGUILayout.Space();
        }

        private void Export ()
        {
            if (!EditorUtility.DisplayDialog("Export data to spreadsheet?",
                    "Are you sure you want to export the scenario scripts, managed text and localization data to spreadsheets?\n\n" +
                    "Spreadsheets in the output folder will be overwritten, existing data could be lost. The effect of this action is permanent and can't be undone, so make sure to backup the spreadsheet file before confirming.\n\n" +
                    "In case a spreadsheet is currently open in another program, close the program before proceeding.", "Export", "Cancel")) return;
            try { CreateProcessor(p => EditorUtility.DisplayProgressBar("Exporting Naninovel Scripts", p.Info, p.Progress)).Export(); }
            finally { EditorUtility.ClearProgressBar(); }
        }

        private void Import ()
        {
            if (!EditorUtility.DisplayDialog("Import data from spreadsheet?",
                    "Are you sure you want to import the spreadsheets data to this project?\n\n" +
                    "Affected localization documents will be overwritten, existing data could be lost. The effect of this action is permanent and can't be undone, so make sure to backup the project before confirming.\n\n" +
                    "In case a spreadsheet is currently open in another program, close the program before proceeding.", "Import", "Cancel")) return;
            try { CreateProcessor(p => EditorUtility.DisplayProgressBar("Importing Naninovel Scripts", p.Info, p.Progress)).Import(); }
            finally { EditorUtility.ClearProgressBar(); }
        }

        private Processor CreateProcessor (Action<ProcessorProgress> onProgress)
        {
            var options = CreateOptions(onProgress);
            var customType = UnityEditor.TypeCache.GetTypesDerivedFrom<Processor>().FirstOrDefault(t => t != typeof(Processor));
            if (customType is null) return new(options);
            try { return (Processor)Activator.CreateInstance(customType, options); }
            catch (Exception e) { throw new Error($"Custom processor '{customType.Name}' is invalid: {e}"); }
        }

        private ProcessorOptions CreateOptions (Action<ProcessorProgress> onProgress) => new() {
            ScriptFolder = InputScriptFolder,
            TextFolder = InputTextFolder,
            L10nFolder = InputL10nFolder,
            OutputFolder = OutputFolder,
            SourceLocale = SourceLocale,
            OnProgress = onProgress,
            Annotate = Annotate
        };

        private string GetPref (string defaultValue = null, [CallerMemberName] string name = "")
        {
            return PlayerPrefs.GetString(BuildPrefName(name), defaultValue);
        }

        private void SetPrefAndValidate (string value, [CallerMemberName] string name = "")
        {
            PlayerPrefs.SetString(BuildPrefName(name), PathUtils.FormatPath(value));
            ValidatePaths();
        }

        private string BuildPrefName (string name) => $"Naninovel.Spreadsheet.{name}";

        private void ValidatePaths ()
        {
            if (!Directory.Exists(OutputFolder))
                pathsError = "Output Folder is not valid; make sure it points to an existing folder.";
            else if (!Directory.Exists(InputL10nFolder) || !ContainsAnyDirectory(InputL10nFolder))
                pathsError = "Input Localization Folder is not valid; make sure it points to localization root containing sub-folders for supported locales.";
            else if (!Directory.Exists(InputTextFolder) || !ContainsFileWithExtension(InputTextFolder, TextExtension))
                pathsError = "Input Text Folder is not valid; make sure it points to managed text documents root containing .txt files.";
            else if (!Directory.Exists(InputScriptFolder) || !ContainsFileWithExtension(InputScriptFolder, ScriptExtension))
                pathsError = "Input Script Folder is not valid; make sure it points to scenario root containing .nani files.";
            else pathsError = null;
        }

        private bool ContainsAnyDirectory (string path)
        {
            return Directory.EnumerateDirectories(path).Any();
        }

        private bool ContainsFileWithExtension (string path, string extension)
        {
            return Directory.EnumerateFiles(path, $"*{extension}", SearchOption.AllDirectories).FirstOrDefault() != null;
        }
    }
}
