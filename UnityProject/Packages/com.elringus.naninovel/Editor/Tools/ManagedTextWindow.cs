using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Naninovel.ManagedText;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    public class ManagedTextWindow : EditorWindow
    {
        protected string OutputPath
        {
            get => PlayerPrefs.GetString(outputPathKey, $"{Application.dataPath}/Resources/Naninovel/{Configuration.GetOrDefault<ManagedTextConfiguration>().Loader.PathPrefix}");
            set
            {
                PlayerPrefs.SetString(outputPathKey, value);
                ValidateOutputPath();
            }
        }

        private static readonly GUIContent outputPathContent = new("Output Path", "Path to the folder under which to sore generated managed text documents; should be `Resources/Naninovel/Text` by default.");
        private static readonly GUIContent deleteUnusedContent = new("Delete Unused", "Whether to delete documents that doesn't correspond to any static fields with `ManagedTextAttribute`.");

        private const string outputPathKey = "Naninovel." + nameof(ManagedTextWindow) + "." + nameof(OutputPath);
        private bool isWorking;
        private bool deleteUnused;
        private bool outputPathValid;
        private string pathPrefix;

        [MenuItem("Naninovel/Tools/Managed Text")]
        public static void OpenWindow ()
        {
            var position = new Rect(100, 100, 500, 135);
            GetWindowWithRect<ManagedTextWindow>(position, true, "Managed Text", true);
        }

        private void OnEnable ()
        {
            ValidateOutputPath();
        }

        private void ValidateOutputPath ()
        {
            pathPrefix = Configuration.GetOrDefault<ManagedTextConfiguration>().Loader.PathPrefix;
            outputPathValid = OutputPath?.EndsWith(pathPrefix) ?? false;
        }

        private void OnGUI ()
        {
            EditorGUILayout.LabelField("Naninovel Managed Text", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("The tool to generate managed text documents; see `Managed Text` guide for usage instructions.", EditorStyles.miniLabel);

            EditorGUILayout.Space();

            if (isWorking)
            {
                EditorGUILayout.HelpBox("Working, please wait...", MessageType.Info);
                return;
            }

            using (new EditorGUILayout.HorizontalScope())
            {
                OutputPath = EditorGUILayout.TextField(outputPathContent, OutputPath);
                if (GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.Width(65)))
                    OutputPath = EditorUtility.OpenFolderPanel("Output Path", "", "");
            }
            deleteUnused = EditorGUILayout.Toggle(deleteUnusedContent, deleteUnused);

            GUILayout.FlexibleSpace();

            if (!outputPathValid)
                EditorGUILayout.HelpBox($"Output path is not valid. Make sure it points to a `{pathPrefix}` folder stored under a `Resources` folder.", MessageType.Error);
            else if (GUILayout.Button("Generate Managed Text Documents", GUIStyles.NavigationButton))
                GenerateDocuments();
            EditorGUILayout.Space();
        }

        private void GenerateDocuments ()
        {
            isWorking = true;

            if (!Directory.Exists(OutputPath))
                Directory.CreateDirectory(OutputPath);

            var recordsByPath = GenerateRecordsGroupedByPath();

            foreach (var kv in recordsByPath)
                ProcessDocument(kv.Key, kv.Value);

            if (deleteUnused)
                DeleteUnusedDocuments(recordsByPath.Keys.ToList());

            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();

            isWorking = false;
            Repaint();
        }

        private void ProcessDocument (string documentPath, HashSet<ManagedTextRecord> records)
        {
            var filePath = $"{OutputPath}/{documentPath}.txt";

            // Try to update existing resource.
            if (File.Exists(filePath))
            {
                var documentText = File.ReadAllText(filePath);
                var existingRecords = new HashSet<ManagedTextRecord>(ManagedTextUtils.Parse(documentText, documentPath, filePath).Records);
                // Remove existing fields no longer associated with the document path (possibly moved to another or deleted).
                existingRecords.RemoveWhere(t => !records.Contains(t));
                // Remove new fields that already exist in the updated document, to prevent overriding.
                records.ExceptWith(existingRecords);
                // Add existing fields to the new set.
                records.UnionWith(existingRecords);
                File.Delete(filePath);
            }

            var document = new ManagedTextDocument(records.OrderBy(r => r.Key));
            File.WriteAllText(filePath, ManagedTextUtils.Serialize(document, documentPath, 0));
        }

        private void DeleteUnusedDocuments (List<string> usedDocumentPaths)
        {
            usedDocumentPaths.Add(ManagedTextPaths.Tips);
            usedDocumentPaths.Add(ManagedTextPaths.ScriptConstants);
            foreach (var filePath in Directory.EnumerateFiles(OutputPath, "*.txt"))
                if (!usedDocumentPaths.Contains(Path.GetFileName(filePath).GetBeforeLast(".txt")))
                    File.Delete(filePath);
        }

        private static Dictionary<string, HashSet<ManagedTextRecord>> GenerateRecordsGroupedByPath ()
        {
            var map = new Dictionary<string, HashSet<ManagedTextRecord>>();
            AddStaticFields(map);
            AddDisplayNames(map);
            AddPrefabs(map);
            AddLocales(map);
            return map;
        }

        private static void AddStaticFields (Dictionary<string, HashSet<ManagedTextRecord>> map)
        {
            foreach (var fieldInfo in Engine.Types.ManagedTextFieldHosts
                         .SelectMany(type => type.GetFields(ManagedTextUtils.ManagedFieldBindings))
                         .Where(field => field.IsDefined(typeof(ManagedTextAttribute))))
                AddFromFieldInfo(fieldInfo);

            void AddFromFieldInfo (FieldInfo fieldInfo)
            {
                var attribute = fieldInfo.GetCustomAttribute<ManagedTextAttribute>();
                var key = $"{fieldInfo.DeclaringType!.Name}.{fieldInfo.Name}";
                var value = fieldInfo.GetValue(null) as string;
                GetOrAddDocument(map, attribute.DocumentPath).Add(new(key, value));
            }
        }

        private static void AddDisplayNames (Dictionary<string, HashSet<ManagedTextRecord>> map)
        {
            var records = new HashSet<ManagedTextRecord>();
            var charConfig = Configuration.GetOrDefault<CharactersConfiguration>();
            foreach (var kv in charConfig.Metadata.ToDictionary())
                if (kv.Value.HasName)
                    records.Add(new(kv.Key, kv.Value.DisplayName));
            map[ManagedTextPaths.DisplayNames] = records;
        }

        private static void AddPrefabs (Dictionary<string, HashSet<ManagedTextRecord>> map)
        {
            var providers = new List<ManagedTextProvider>();
            var editorResources = EditorResources.LoadOrDefault();
            var uiConfig = Configuration.GetOrDefault<UIConfiguration>();
            foreach (var kv in editorResources.GetAllRecords(uiConfig.UILoader.PathPrefix))
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(kv.Value);
                if (assetPath is null) continue; // UI with a non-valid resource.
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                ProcessPrefab(prefab);
            }

            foreach (var kv in Configuration.GetOrDefault<TextPrintersConfiguration>().Metadata.ToDictionary())
                ProcessActor<ITextPrinterActor>(kv.Key, kv.Value);
            foreach (var kv in Configuration.GetOrDefault<ChoiceHandlersConfiguration>().Metadata.ToDictionary())
                ProcessActor<IChoiceHandlerActor>(kv.Key, kv.Value);

            void ProcessPrefab (GameObject prefab)
            {
                if (!ObjectUtils.IsValid(prefab)) return;
                prefab.GetComponentsInChildren(true, providers);
                providers.ForEach(p => GetOrAddDocument(map, p.Document).Add(new(p.Key, p.DefaultValue)));
                providers.Clear();
            }

            void ProcessActor<TActor> (string id, ActorMetadata meta) where TActor : IActor
            {
                if (!typeof(TActor).IsAssignableFrom(Type.GetType(meta.Implementation))) return;
                var resourcePath = $"{meta.Loader.PathPrefix}/{id}";
                var guid = editorResources.GetGuidByPath(resourcePath);
                if (guid is null) return; // Actor without an assigned resource.
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                if (assetPath is null) return; // Actor with a non-valid resource.
                var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);
                ProcessPrefab(prefab);
            }
        }

        private static void AddLocales (Dictionary<string, HashSet<ManagedTextRecord>> map)
        {
            var records = new HashSet<ManagedTextRecord>();
            foreach (var lang in Languages.GetAll())
                records.Add(new(lang.Tag, lang.Name));
            map[ManagedTextPaths.Locales] = records;
        }

        private static HashSet<ManagedTextRecord> GetOrAddDocument (Dictionary<string, HashSet<ManagedTextRecord>> map, string documentPath)
        {
            if (string.IsNullOrEmpty(documentPath)) documentPath = ManagedTextPaths.Default;
            if (map.TryGetValue(documentPath, out var result)) return result;
            return map[documentPath] = new();
        }
    }
}
