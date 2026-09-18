using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    public class CharacterExtractorWindow : EditorWindow
    {
        protected string InputFolder { get => PlayerPrefs.GetString(inputFolderKey); set => PlayerPrefs.SetString(inputFolderKey, value); }

        private const string inputFolderKey = "Naninovel." + nameof(CharacterExtractorWindow) + "." + nameof(InputFolder);
        private readonly HashSet<char> hash = new();
        private string result = "";
        private Vector2 scroll;

        [MenuItem("Naninovel/Tools/Character Extractor")]
        public static void OpenWindow ()
        {
            var position = new Rect(100, 100, 500, 235);
            GetWindowWithRect<CharacterExtractorWindow>(position, true, "Character Extractor", true);
        }

        private void OnGUI ()
        {
            EditorGUILayout.LabelField("Character Extractor Utility", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Extracts unique characters in managed text and script assets.", EditorStyles.miniLabel);
            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                InputFolder = EditorGUILayout.TextField("Input Folder", InputFolder);
                if (GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.Width(65)))
                    InputFolder = EditorUtility.OpenFolderPanel("Input Folder", "", "");
            }

            EditorGUILayout.Space();

            scroll = EditorGUILayout.BeginScrollView(scroll);
            var wasWrap = EditorStyles.textField.wordWrap;
            EditorStyles.textField.wordWrap = true;
            EditorGUILayout.TextArea(result, GUILayout.Height(100), GUILayout.Width(480));
            EditorStyles.textField.wordWrap = wasWrap;
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();

            if (!Directory.Exists(InputFolder))
                EditorGUILayout.HelpBox("Select input folder with script and/or managed text assets.", MessageType.Warning, true);
            else if (GUILayout.Button("Extract Characters", GUIStyles.NavigationButton))
                try { ExtractCharacters(); }
                finally { EditorUtility.ClearProgressBar(); }

            EditorGUILayout.Space();
        }

        private void ExtractCharacters ()
        {
            hash.Clear();
            var folders = new[] { PathUtils.AbsoluteToAssetPath(InputFolder) };
            ExtractScripts(AssetDatabase.FindAssets("t:Naninovel.Script", folders));
            ExtractText(AssetDatabase.FindAssets("t:TextAsset", folders));
            result = new(hash.ToArray());
        }

        private void ExtractScripts (string[] guids)
        {
            for (int i = 0; i < guids.Length; i++)
            {
                var file = AssetDatabase.GUIDToAssetPath(guids[i]);
                Progress(guids.Length, i, file);
                foreach (var text in AssetDatabase.LoadAssetAtPath<Script>(file).TextMap.Map.Values)
                foreach (var character in text)
                    hash.Add(character);
            }
        }

        private void ExtractText (string[] guids)
        {
            for (int i = 0; i < guids.Length; i++)
            {
                var file = AssetDatabase.GUIDToAssetPath(guids[i]);
                Progress(guids.Length, i, file);
                var documentText = AssetDatabase.LoadAssetAtPath<TextAsset>(file).text;
                var document = ManagedTextUtils.Parse(documentText, name: file);
                foreach (var record in document.Records)
                foreach (var character in record.Value)
                    hash.Add(character);
            }
        }

        private void Progress (int length, int index, string file)
        {
            var info = $"Processing {Path.GetFileName(file)}...";
            var progress = index / (float)length;
            EditorUtility.DisplayProgressBar("Extracting Characters", info, progress);
        }
    }
}
