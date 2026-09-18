using System;
using System.Collections.Generic;
using System.Linq;
using Naninovel.ManagedText;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    public class VoiceMapWindow : EditorWindow
    {
        private class MapItem
        {
            public bool Assigned => !string.IsNullOrEmpty(ClipGuid);
            public bool Loaded => ClipObject;
            public GUIContent Label;
            public string ClipGuid;
            public AudioClip ClipObject;
            public string Author;
            public Commands.PrintText Command;
        }

        private static readonly GUIContent scriptContent = new("Scenario Script", "Naninovel script for which to edit auto voice associations.");
        private static readonly GUIContent managedTextContent = new("Localization Document", "To associate with a non-source locale, assign related localization managed text document.");
        private static readonly GUIContent authorFilterContent = new("Author Filter", "When specified, will show only print commands with the specified author. Specify '*' to show commands with any author.");
        private static readonly Color invalidObjectColor = new(1, .8f, .8f);
        private static readonly Type clipType = typeof(AudioClip);
        private static readonly Type scriptType = typeof(Script);
        private static readonly Type textType = typeof(TextAsset);

        private readonly Dictionary<string, MapItem> map = new();
        private AssetDropHandler dropHandler;
        private AudioConfiguration audioConfig;
        private LocalizationConfiguration l10nConfig;
        private EditorResources editorResources;
        private Script script;
        private TextAsset managedTextAsset;
        private ManagedTextDocument managedText = new(Array.Empty<ManagedTextRecord>());
        private string locale = "";
        private string authorFilter;
        private Vector2 scrollPos;

        [MenuItem("Naninovel/Tools/Voice Map")]
        public static void OpenWindow ()
        {
            GetWindow<VoiceMapWindow>("Voice Map", true);
        }

        private void OnEnable ()
        {
            editorResources = EditorResources.LoadOrDefault();
            audioConfig = Configuration.GetOrDefault<AudioConfiguration>();
            l10nConfig = Configuration.GetOrDefault<LocalizationConfiguration>();
            dropHandler = new(ProcessDroppedClips);
            dropHandler.TypeConstraint = typeof(AudioClip);
            dropHandler.DropMessage = "Drop voice clips or folders here to attempt automatic assignment.\nClip name should equal to the beginning of the printed text in order for this to work.";
            UpdateSelectedScript();
        }

        private void OnDisable ()
        {
            SaveResources();
        }

        private void OnGUI ()
        {
            DrawScriptSelection();

            if (!script)
            {
                EditorGUILayout.HelpBox("Select a script to start mapping the voice clips.", MessageType.None, false);
                return;
            }

            DrawManagedTextSelection();

            authorFilter = EditorGUILayout.TextField(authorFilterContent, authorFilter);

            EditorGUILayout.Space();

            if (map.Count == 0)
            {
                EditorGUILayout.HelpBox("Selected script doesn't contain any commands to associate voice clips with. " +
                                        "Voice clips can be associated with generic text lines (containing a text to print) and with @print commands.", MessageType.Info);
                return;
            }

            if (dropHandler.CanHandleDraggedObjects())
                dropHandler.DrawDropArea(EditorGUILayout.GetControlRect());
            DrawItems();
        }

        private void DrawScriptSelection ()
        {
            EditorGUI.BeginChangeCheck();
            script = EditorGUILayout.ObjectField(scriptContent, script, scriptType, false) as Script;
            if (EditorGUI.EndChangeCheck()) UpdateSelectedScript();
        }

        private void DrawManagedTextSelection ()
        {
            EditorGUI.BeginChangeCheck();
            managedTextAsset = EditorGUILayout.ObjectField(managedTextContent, managedTextAsset, textType, false) as TextAsset;
            if (EditorGUI.EndChangeCheck()) UpdateSelectedManagedText();
            if (!string.IsNullOrEmpty(locale)) EditorGUILayout.HelpBox(locale, MessageType.None, false);
        }

        private void DrawItems ()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            foreach (var kv in map)
            {
                if (!ShouldDrawItem(kv.Value)) continue;
                EditorGUILayout.BeginHorizontal();
                DrawItem(kv.Value);
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }

        private bool ShouldDrawItem (MapItem item)
        {
            if (string.IsNullOrEmpty(authorFilter)) return true;
            return !string.IsNullOrEmpty(item.Author) &&
                   (authorFilter == "*" || item.Author == authorFilter);
        }

        private void DrawItem (MapItem item)
        {
            EditorGUILayout.LabelField(item.Label);

            if (ShouldDrawWithoutLoading(item))
            {
                DrawWithoutLoading(item);
                return;
            }

            if (item.Assigned && !item.Loaded)
                LoadItemClip(item);

            EditorGUI.BeginChangeCheck();
            var initialGuidColor = GUI.color;
            if (!item.ClipObject) GUI.color = invalidObjectColor;
            item.ClipObject = EditorGUILayout.ObjectField(GUIContent.none, item.ClipObject, clipType, false) as AudioClip;
            GUI.color = initialGuidColor;
            if (EditorGUI.EndChangeCheck())
            {
                if (!item.ClipObject) item.ClipGuid = string.Empty;
                else AssetDatabase.TryGetGUIDAndLocalFileIdentifier(item.ClipObject, out item.ClipGuid, out long _);
            }
        }

        private bool ShouldDrawWithoutLoading (MapItem item)
        {
            var rect = GUILayoutUtility.GetLastRect();
            rect.width = EditorGUIUtility.currentViewWidth;
            var hovered = rect.Contains(Event.current.mousePosition);
            return !hovered && !item.Loaded && item.Assigned;
        }

        private void DrawWithoutLoading (MapItem item)
        {
            var path = AssetDatabase.GUIDToAssetPath(item.ClipGuid);
            if (path.Contains("/")) path = path.GetAfter("/");
            if (path.Length > 30) path = path[^30..];
            EditorGUILayout.LabelField(path, EditorStyles.objectField);
        }

        private void LoadItemClip (MapItem item)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(item.ClipGuid);
            item.ClipObject = AssetDatabase.LoadAssetAtPath<AudioClip>(assetPath);
            if (!item.ClipObject) item.ClipGuid = string.Empty;
        }

        private void UpdateSelectedScript ()
        {
            SaveResources();
            map.Clear();

            if (!script) return;

            var extractedCommands = script.ExtractCommands()
                .Where(c => c is Commands.PrintText)
                .Cast<Commands.PrintText>();

            foreach (var command in extractedCommands)
                ProcessCommand(command);
        }

        private void UpdateSelectedManagedText ()
        {
            SaveResources();
            map.Clear();

            var docPath = AssetDatabase.GetAssetPath(managedTextAsset);
            managedText = !managedTextAsset
                ? new(Array.Empty<ManagedTextRecord>())
                : ManagedTextUtils.ParseMultiline(managedTextAsset.text);
            locale = !managedTextAsset ? "" : docPath.GetBetween("Localization/", "/Text");
            if (managedTextAsset && string.IsNullOrEmpty(locale))
                Debug.LogError($"Failed to resolve locale for '{docPath}' script localization document. " +
                               "Make sure it's stored under 'Localization/{locale}/Text' folder");
            else UpdateSelectedScript();
        }

        private void ProcessCommand (Commands.PrintText cmd)
        {
            if (AutoVoiceResolver.Resolve(cmd.Text) is not { } localPath) return;
            var fullPath = GetFullVoiceResourcePath(localPath);
            var label = $"#{cmd.PlaybackSpot.LineNumber}.{cmd.PlaybackSpot.InlineIndex} ";
            if (!string.IsNullOrEmpty(cmd.AuthorId)) label += $"{cmd.AuthorId}: ";
            label += ResolveText(cmd.Text);
            map[fullPath] = new() {
                Label = new(label, label),
                ClipGuid = editorResources.GetGuidByPath(fullPath),
                Author = GetAuthor(cmd),
                Command = cmd
            };
        }

        private string GetFullVoiceResourcePath (string localPath)
        {
            var fullPath = PathUtils.Combine(audioConfig.VoiceLoader.PathPrefix, localPath);
            if (string.IsNullOrEmpty(locale)) return fullPath;
            return PathUtils.Combine(l10nConfig.Loader.PathPrefix, locale, fullPath);
        }

        private string GetAuthor (Commands.PrintText cmd)
        {
            if (!Command.Assigned(cmd.AuthorId) || cmd.AuthorId.DynamicValue) return "";
            return cmd.AuthorId.Value;
        }

        private string ResolveText (LocalizableTextParameter param)
        {
            return LocalizableTextResolver.Resolve(param, ResolveId);

            string ResolveId (string id) => managedText.TryGet(id, out var val) ? val.Value : script.TextMap.GetTextOrNull(id);
        }

        private void SaveResources ()
        {
            if (!audioConfig) return;

            var category = string.IsNullOrEmpty(locale)
                ? audioConfig.VoiceLoader.PathPrefix
                : PathUtils.Combine(l10nConfig.Loader.PathPrefix, locale, audioConfig.VoiceLoader.PathPrefix);

            foreach (var kv in map)
            {
                var fullPath = kv.Key;
                var name = fullPath.GetAfterFirst(category + "/");
                var item = kv.Value;

                editorResources.RemoveAllRecordsWithPath(category, name, category);
                if (item.Assigned) editorResources.AddRecord(category, category, name, item.ClipGuid);
            }

            EditorUtility.SetDirty(editorResources);
            AssetDatabase.SaveAssets();
        }

        private void ProcessDroppedClips (DroppedAsset[] assets)
        {
            foreach (var asset in assets)
                AttemptAutoAssign(asset.Asset as AudioClip, asset.Guid);
        }

        private void AttemptAutoAssign (AudioClip clip, string guid)
        {
            var itemToAssign = map.Values.FirstOrDefault(ShouldAssign);
            if (itemToAssign is null) return;
            itemToAssign.ClipObject = clip;
            itemToAssign.ClipGuid = guid;

            bool ShouldAssign (MapItem item)
            {
                if (item.Assigned) return false;
                var text = ResolveText(item.Command.Text);
                return text?.StartsWith(clip.name, StringComparison.OrdinalIgnoreCase) ?? false;
            }
        }
    }
}
