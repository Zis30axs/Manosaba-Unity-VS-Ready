using System.IO;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    public class FountainWindow : EditorWindow
    {
        protected string FilePath { get => PlayerPrefs.GetString(filePathKey); set => PlayerPrefs.SetString(filePathKey, value); }
        protected string OutputPath { get => PlayerPrefs.GetString(outputPathKey); set => PlayerPrefs.SetString(outputPathKey, value); }

        private const string filePathKey = "Naninovel." + nameof(FountainWindow) + "." + nameof(FilePath);
        private const string outputPathKey = "Naninovel." + nameof(FountainWindow) + "." + nameof(OutputPath);

        private readonly FountainConverter converter = new(Progress);

        [MenuItem("Naninovel/Tools/Fountain Screenplay")]
        public static void OpenWindow ()
        {
            var position = new Rect(100, 100, 500, 160);
            GetWindowWithRect<FountainWindow>(position, true, "Fountain Screenplay", true);
        }

        private void OnGUI ()
        {
            EditorGUILayout.LabelField("Fountain Screenplay Converter", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("The tool to convert .fountain to .nani; see `Fountain` guide for usage instructions.", EditorStyles.miniLabel);
            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                FilePath = EditorGUILayout.TextField("Fountain File", FilePath);
                if (GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.Width(65)))
                    FilePath = EditorUtility.OpenFilePanel("Fountain File", "", "fountain");
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                OutputPath = EditorGUILayout.TextField("Output Directory", OutputPath);
                if (GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.Width(65)))
                    OutputPath = EditorUtility.OpenFolderPanel("Output Directory", "", "");
            }

            GUILayout.FlexibleSpace();

            if (!File.Exists(FilePath) || !Directory.Exists(OutputPath))
                EditorGUILayout.HelpBox("Select .fountain file to convert and output directory to store the generated .nani files.", MessageType.Warning, true);
            else if (GUILayout.Button("Convert Screenplay", GUIStyles.NavigationButton))
                try { ConvertScreenplay(); }
                finally { EditorUtility.ClearProgressBar(); }

            EditorGUILayout.Space();
        }

        private void ConvertScreenplay ()
        {
            Progress("Cleaning output directory...", 0);
            if (CleanDirectoryPrompt.PromptIfNotEmpty(OutputPath))
                Directory.Delete(OutputPath, true);
            else throw new Error("Operation canceled by user.");
            Progress("Reading fountain document...", 0.1f);
            var fountainText = File.ReadAllText(FilePath);
            var scripts = converter.Convert(fountainText);
            Progress("Writing generated scripts...", 0.9f);
            foreach (var script in scripts)
                WriteScript(Path.Combine(OutputPath, script.LocalPath), script.ScriptText);
        }

        private static void WriteScript (string path, string text)
        {
            var dirPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            File.WriteAllText(path, text);
        }

        private static void Progress (string info, float value)
        {
            EditorUtility.DisplayProgressBar("Converting Screenplay", info, value);
        }
    }
}
