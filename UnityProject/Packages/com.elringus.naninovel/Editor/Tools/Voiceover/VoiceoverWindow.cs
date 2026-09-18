using Naninovel.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    public class VoiceoverWindow : EditorWindow
    {
        public enum Format
        {
            Plaintext,
            Markdown,
            CSV
        }

        protected string OutputPath { get => PlayerPrefs.GetString(outputPathKey); set => PlayerPrefs.SetString(outputPathKey, value); }
        protected Format OutputFormat { get => (Format)PlayerPrefs.GetInt(outputFormatKey); set => PlayerPrefs.SetInt(outputFormatKey, (int)value); }

        private static readonly GUIContent localeLabel = new("Locale");
        private static readonly GUIContent formatLabel = new("Format",
            "Type of file and formatting of the voiceover documents to produce:" +
            "\n • Plaintext — .txt file without any formatting." +
            "\n • Markdown — .md file with additional markdown for better readability." +
            "\n • CSV — .csv file with comma-separated values to be used with table processors, such as Google Sheets or Microsoft Excel.");

        private const string outputPathKey = "Naninovel." + nameof(VoiceoverWindow) + "." + nameof(OutputPath);
        private const string outputFormatKey = "Naninovel." + nameof(VoiceoverWindow) + "." + nameof(OutputFormat);

        private static readonly IVoiceoverDocumentGenerator customGenerator = GetCustomGenerator();
        private bool isWorking;
        private IScriptManager scripts;
        private ILocalizationManager l10n;
        private ITextManager text;
        private string locale;

        [MenuItem("Naninovel/Tools/Voiceover Documents")]
        public static void OpenWindow ()
        {
            var position = new Rect(100, 100, 500, 160);
            GetWindowWithRect<VoiceoverWindow>(position, true, "Voiceover Documents", true);
        }

        private void OnEnable ()
        {
            if (!Engine.Initialized)
            {
                isWorking = true;
                Engine.OnInitializationFinished += InitializeEditor;
                EditorInitializer.Initialize().Forget();
            }
            else InitializeEditor();
        }

        private void OnDisable ()
        {
            Engine.Destroy();
        }

        private void InitializeEditor ()
        {
            Engine.OnInitializationFinished -= InitializeEditor;

            scripts = Engine.GetServiceOrErr<IScriptManager>();
            l10n = Engine.GetServiceOrErr<ILocalizationManager>();
            text = Engine.GetServiceOrErr<ITextManager>();
            locale = Configuration.GetOrDefault<LocalizationConfiguration>().SourceLocale;
            isWorking = false;
        }

        private void OnGUI ()
        {
            EditorGUILayout.LabelField("Naninovel Voiceover Documents", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("The tool to generate voiceover documents; see 'Voicing' guide for usage instructions.", EditorStyles.miniLabel);
            EditorGUILayout.Space();

            if (isWorking)
            {
                EditorGUILayout.HelpBox("Working, please wait...", MessageType.Info);
                return;
            }

            locale = LocalesPopupDrawer.Draw(locale, localeLabel);
            if (customGenerator != null) EditorGUILayout.LabelField("Custom Generator", customGenerator.GetType().Name);
            else OutputFormat = (Format)EditorGUILayout.EnumPopup(formatLabel, OutputFormat);
            using (new EditorGUILayout.HorizontalScope())
            {
                OutputPath = EditorGUILayout.TextField("Output Path", OutputPath);
                if (GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.Width(65)))
                    OutputPath = EditorUtility.OpenFolderPanel("Output Path", "", "");
            }

            GUILayout.FlexibleSpace();

            if (!l10n.LocaleAvailable(locale))
                EditorGUILayout.HelpBox($"Selected locale is not available. Make sure a '{locale}' directory exists in the localization resources.", MessageType.Warning, true);
            else
            {
                EditorGUI.BeginDisabledGroup(string.IsNullOrEmpty(OutputPath));
                if (GUILayout.Button("Generate Voiceover Documents", GUIStyles.NavigationButton))
                    GenerateVoiceoverDocuments().Forget();
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.Space();
        }

        private async UniTask GenerateVoiceoverDocuments ()
        {
            try
            {
                isWorking = true;

                EditorUtility.DisplayProgressBar("Generating Voiceover Documents", "Initializing...", 0f);

                await l10n.SelectLocale(locale);

                var resources = await scripts.ScriptLoader.LoadAll();
                await text.DocumentLoader.LoadAll();
                WriteVoiceoverDocuments(resources.Select(r => r.Object).ToArray());

                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();

                isWorking = false;
                Repaint();

                EditorUtility.ClearProgressBar();
            }
            catch (Exception e)
            {
                Engine.Err($"Failed to generate voiceover documents: {e}");
            }
            finally
            {
                isWorking = false;
                EditorUtility.ClearProgressBar();
            }
        }

        private void WriteVoiceoverDocuments (IReadOnlyList<Script> scripts)
        {
            if (!Directory.Exists(OutputPath))
                Directory.CreateDirectory(OutputPath);

            var outputDir = new DirectoryInfo(OutputPath);
            if (CleanDirectoryPrompt.PromptIfNotEmpty(outputDir))
                outputDir.GetFiles().ToList().ForEach(f => f.Delete());
            else throw new Error("Operation canceled by user.");

            for (int i = 0; i < scripts.Count; i++)
            {
                var scriptPath = scripts[i].Path;
                var list = scripts[i].Playlist;
                var progress = i / (float)scripts.Count;
                EditorUtility.DisplayProgressBar("Generating Voiceover Documents", $"Processing '{scriptPath}' script...", progress);

                if (customGenerator != null)
                    customGenerator.GenerateVoiceoverDocument(list, locale ?? "default", OutputPath);
                else if (OutputFormat == Format.Plaintext) GeneratePlainText(list, scriptPath);
                else if (OutputFormat == Format.Markdown) GenerateMarkdown(list, scriptPath);
                else if (OutputFormat == Format.CSV) GenerateCSV(list, scriptPath);
            }
        }

        private static IVoiceoverDocumentGenerator GetCustomGenerator ()
        {
            var type = UnityEditor.TypeCache.GetTypesDerivedFrom<IVoiceoverDocumentGenerator>().FirstOrDefault();
            if (type is null) return null;
            return Activator.CreateInstance(type) as IVoiceoverDocumentGenerator;
        }

        private void GeneratePlainText (ScriptPlaylist list, string scriptPath)
        {
            var builder = new StringBuilder($"Voiceover document for script '{scriptPath}' ({locale ?? "default"} locale)\n\n");
            foreach (var cmd in list.OfType<PrintText>())
            {
                if (AutoVoiceResolver.Resolve(cmd.Text) is not { } id) continue;
                builder.Append($"# {id}\n");
                if (Command.Assigned(cmd.AuthorId))
                    builder.Append($"{cmd.AuthorId}: ");
                builder.Append($"{ResolveText(list.ScriptPath, cmd.Text)}\n\n");
            }
            var outFilePath = $"{OutputPath}/{Path.ChangeExtension(scriptPath, "txt")}";
            var outDirPath = Path.GetDirectoryName(outFilePath);
            if (!Directory.Exists(outDirPath)) Directory.CreateDirectory(outDirPath);
            File.WriteAllText(outFilePath, builder.ToString());
        }

        private void GenerateMarkdown (ScriptPlaylist list, string scriptPath)
        {
            var builder = new StringBuilder($"# Voiceover document for script `{scriptPath}` ({locale ?? "default"} locale)\n\n");
            foreach (var cmd in list.OfType<PrintText>())
            {
                if (AutoVoiceResolver.Resolve(cmd.Text) is not { } id) continue;
                builder.Append($"## `{id}`\n");
                if (Command.Assigned(cmd.AuthorId))
                    builder.Append($"**{cmd.AuthorId}**: ");
                builder.Append($"{ResolveText(list.ScriptPath, cmd.Text)}\n\n");
            }
            var outFilePath = $"{OutputPath}/{Path.ChangeExtension(scriptPath, "md")}";
            var outDirPath = Path.GetDirectoryName(outFilePath);
            if (!Directory.Exists(outDirPath)) Directory.CreateDirectory(outDirPath);
            File.WriteAllText(outFilePath, builder.ToString());
        }

        private void GenerateCSV (ScriptPlaylist script, string scriptPath)
        {
            var builder = new StringBuilder("Path,Author,Text\n");
            foreach (var cmd in script.OfType<PrintText>())
            {
                if (AutoVoiceResolver.Resolve(cmd.Text) is not { } id) continue;
                var author = EscapeCSV(cmd.AuthorId?.ToString());
                var text = EscapeCSV(ResolveText(script.ScriptPath, cmd.Text));
                builder.Append($"{id},{author},{text}\n");
            }
            var outFilePath = $"{OutputPath}/{Path.ChangeExtension(scriptPath, "csv")}";
            var outDirPath = Path.GetDirectoryName(outFilePath);
            if (!Directory.Exists(outDirPath)) Directory.CreateDirectory(outDirPath);
            File.WriteAllText(outFilePath, builder.ToString());

            string EscapeCSV (string text)
            {
                if (string.IsNullOrEmpty(text)) return "";
                return '"' + text.Replace("\"", "\"\"") + '"';
            }
        }

        private string ResolveText (string scriptPath, LocalizableTextParameter param)
        {
            return LocalizableTextResolver.Resolve(param, ResolveId);

            string ResolveId (string id)
            {
                if (l10n.IsSourceLocaleSelected())
                    return scripts.ScriptLoader.GetLoadedOrErr(scriptPath).TextMap.GetTextOrNull(id);
                var documentPath = ManagedTextUtils.ResolveScriptL10nPath(scriptPath);
                return documentPath is null ? null : text.GetRecordValue(id, documentPath);
            }
        }
    }
}
