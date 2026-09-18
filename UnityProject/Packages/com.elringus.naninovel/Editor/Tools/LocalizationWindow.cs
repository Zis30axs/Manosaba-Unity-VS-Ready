using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Naninovel.ManagedText;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    public class LocalizationWindow : EditorWindow
    {
        protected string SourceScriptsPath
        {
            get => PlayerPrefs.GetString(sourceScriptsPathKey, $"{Application.dataPath}/Scenario");
            set
            {
                PlayerPrefs.SetString(sourceScriptsPathKey, value);
                ValidatePaths();
            }
        }
        protected string SourceManagedTextPath
        {
            get => PlayerPrefs.GetString(sourceManagedTextPathKey, $"{Application.dataPath}/Resources/Naninovel/{Configuration.GetOrDefault<ManagedTextConfiguration>().Loader.PathPrefix}");
            set => PlayerPrefs.SetString(sourceManagedTextPathKey, value);
        }
        protected string LocaleFolderPath
        {
            get => PlayerPrefs.GetString(localeFolderPathKey, $"{Application.dataPath}/Resources/Naninovel/Localization");
            set
            {
                PlayerPrefs.SetString(localeFolderPathKey, value);
                ValidatePaths();
            }
        }
        protected bool Annotate
        {
            get => PlayerPrefs.GetInt(annotatePathKey, 1) == 1;
            set => PlayerPrefs.SetInt(annotatePathKey, value ? 1 : 0);
        }

        private const string sourceScriptsPathKey = "Naninovel." + nameof(LocalizationWindow) + "." + nameof(SourceScriptsPath);
        private const string sourceManagedTextPathKey = "Naninovel." + nameof(LocalizationWindow) + "." + nameof(SourceManagedTextPath);
        private const string localeFolderPathKey = "Naninovel." + nameof(LocalizationWindow) + "." + nameof(LocaleFolderPath);
        private const string annotatePathKey = "Naninovel." + nameof(LocalizationWindow) + "." + nameof(Annotate);
        private const string progressBarTitle = "Generating Localization Resources";

        private static readonly GUIContent sourceScriptsPathContent = new("Script Folder (input)", "Folder under which source scripts (.nani) are stored. Alternatively, pick a text folder with the previously generated localization documents to generate based on a non-source locale.");
        private static readonly GUIContent sourceManagedTextPathContent = new("Text Folder (input)", "Folder under which source managed text documents are stored ('Resources/Naninovel/Text' by default). Won't generate localization for managed text when not specified.");
        private static readonly GUIContent localeFolderPathContent = new("Locale Folder (output)", "The folder for the target locale where to store generated localization resources. Should be inside localization root ('Assets/Resources/Naninovel/Localization' by default) and have a name equal to one of the supported localization tags.");
        private static readonly GUIContent annotateContent = new("Include Annotations", "Whether to include source script content and comments placed before it to give translators additional context about the localized content.");
        private static readonly GUIContent warnUntranslatedContent = new("Warn Untranslated", "Whether to log warnings when untranslated lines detected while generating localization documents.");
        private static readonly GUIContent spacingContent = new("Line Spacing", "Number of empty lines to insert between localized records.");

        private bool localizationRootSelected => availableLocalizations.Count > 0;
        private bool baseOnSourceLocale => sourceTag == l10nConfig.SourceLocale;

        private readonly List<string> availableLocalizations = new();
        private LocalizationConfiguration l10nConfig;
        private bool warnUntranslated;
        private int wordCount = -1, spacing = 1;
        private bool outputPathValid, sourcePathValid;
        private string targetTag, targetLanguage, sourceTag, sourceLanguage;

        [MenuItem("Naninovel/Tools/Localization")]
        public static void OpenWindow ()
        {
            var position = new Rect(100, 100, 500, 350);
            GetWindowWithRect<LocalizationWindow>(position, true, "Localization", true);
        }

        private void OnEnable ()
        {
            l10nConfig = Configuration.GetOrDefault<LocalizationConfiguration>();
            ValidatePaths();
        }

        private void ValidatePaths ()
        {
            var localizationRoot = l10nConfig.Loader.PathPrefix;

            availableLocalizations.Clear();
            if (LocaleFolderPath != null && Directory.Exists(LocaleFolderPath) && LocaleFolderPath.EndsWithFast(localizationRoot))
                foreach (var locale in Directory.GetDirectories(LocaleFolderPath).Select(Path.GetFileName))
                    if (Languages.ContainsTag(locale))
                        availableLocalizations.Add(locale);

            targetTag = LocaleFolderPath?.GetAfter("/");
            sourceTag = SourceScriptsPath?.GetAfterFirst($"{localizationRoot}/")?.GetBefore("/") ?? l10nConfig.SourceLocale;
            sourcePathValid = Directory.Exists(SourceScriptsPath);
            outputPathValid = localizationRootSelected || (LocaleFolderPath?.GetBeforeLast("/")?.EndsWith(localizationRoot) ?? false) &&
                Languages.ContainsTag(targetTag) && targetTag != l10nConfig.SourceLocale;
            if (outputPathValid)
            {
                targetLanguage = localizationRootSelected ? string.Join(", ", availableLocalizations) : Languages.GetNameByTag(targetTag);
                sourceLanguage = Languages.GetNameByTag(sourceTag);
            }
        }

        private void OnGUI ()
        {
            EditorGUILayout.LabelField("Naninovel Localization", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("The tool to generate localization documents; see Localization guide for usage instructions.", EditorStyles.miniLabel);
            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                SourceScriptsPath = EditorGUILayout.TextField(sourceScriptsPathContent, SourceScriptsPath);
                if (GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.Width(65)))
                    SourceScriptsPath = EditorUtility.OpenFolderPanel("Source Scripts Folder (input)", "", "");
            }
            if (sourcePathValid)
                EditorGUILayout.HelpBox(sourceLanguage, MessageType.None, false);

            if (baseOnSourceLocale)
            {
                EditorGUILayout.Space();
                using (new EditorGUILayout.HorizontalScope())
                {
                    SourceManagedTextPath = EditorGUILayout.TextField(sourceManagedTextPathContent, SourceManagedTextPath);
                    if (GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.Width(65)))
                        SourceManagedTextPath = EditorUtility.OpenFolderPanel("Source Managed Text Folder (input)", "", "");
                }
            }

            EditorGUILayout.Space();

            using (new EditorGUILayout.HorizontalScope())
            {
                LocaleFolderPath = EditorGUILayout.TextField(localeFolderPathContent, LocaleFolderPath);
                if (GUILayout.Button("Select", EditorStyles.miniButton, GUILayout.Width(65)))
                    LocaleFolderPath = EditorUtility.OpenFolderPanel("Locale Folder Path (output)", "", "");
            }
            if (outputPathValid)
                EditorGUILayout.HelpBox(targetLanguage, MessageType.None, false);

            EditorGUILayout.Space();

            warnUntranslated = EditorGUILayout.Toggle(warnUntranslatedContent, warnUntranslated);
            Annotate = EditorGUILayout.Toggle(annotateContent, Annotate);
            spacing = EditorGUILayout.IntField(spacingContent, spacing);
            GUILayout.FlexibleSpace();

            if (sourcePathValid && outputPathValid)
                EditorGUILayout.HelpBox(wordCount >= 0 ? $"Total localizable words in scenario scripts: {wordCount}." : "Total localizable word count will appear here after the documents are generated.", MessageType.Info);

            if (!sourcePathValid) EditorGUILayout.HelpBox("Script Folder (input) path is not valid. Make sure it points either to folder where naninovel (.nani) scripts are stored or to a folder with the previously generated text localization documents (.txt).", MessageType.Error);
            else if (!outputPathValid)
            {
                if (targetTag == l10nConfig.SourceLocale)
                    EditorGUILayout.HelpBox($"You're trying to create a '{targetTag}' localization, which is equal to the project source locale. That is not allowed; see 'Localization' guide for more info.", MessageType.Error);
                else EditorGUILayout.HelpBox("Locale Folder (output) path is not valid. Make sure it points to the localization root or a subdirectory with name equal to one of the supported language tags.", MessageType.Error);
            }

            EditorGUI.BeginDisabledGroup(!outputPathValid || !sourcePathValid);
            if (GUILayout.Button("Generate Localization Documents", GUIStyles.NavigationButton))
                GenerateLocalizationResources();
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.Space();
        }

        private void GenerateLocalizationResources ()
        {
            EditorUtility.DisplayProgressBar(progressBarTitle, "Reading source documents...", 0f);

            try
            {
                if (localizationRootSelected)
                    foreach (var locale in availableLocalizations)
                        DoGenerate(Path.Combine(LocaleFolderPath, locale));
                else DoGenerate(LocaleFolderPath);
                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();
            }
            finally
            {
                EditorUtility.ClearProgressBar();
                Repaint();
            }

            void DoGenerate (string localeFolderPath)
            {
                LocalizeScripts(localeFolderPath);
                LocalizeManagedText(localeFolderPath);
            }
        }

        private void LocalizeScripts (string localeFolderPath)
        {
            wordCount = 0;
            var missingKeys = new List<string>();
            var outputDirPath = $"{localeFolderPath}/{Configuration.GetOrDefault<ManagedTextConfiguration>().Loader.PathPrefix}/{ManagedTextPaths.ScriptLocalizationPrefix}";
            if (!Directory.Exists(outputDirPath)) Directory.CreateDirectory(outputDirPath);
            if (baseOnSourceLocale) LocalizeBasedOnScripts();
            else LocalizeBasedOnExistingL10nDocuments();

            void LocalizeBasedOnScripts ()
            {
                EditorUtility.DisplayProgressBar(progressBarTitle, "Finding source script assets...", 0);
                var scriptPaths = Directory.GetFiles(SourceScriptsPath, "*.nani", SearchOption.AllDirectories);
                for (int i = 0; i < scriptPaths.Length; i++)
                {
                    var scriptPath = scriptPaths[i];
                    EditorUtility.DisplayProgressBar(progressBarTitle, $"Processing '{Path.GetFileName(scriptPath)}'...", i / (float)scriptPaths.Length);
                    var script = AssetDatabase.LoadAssetAtPath<Script>(PathUtils.AbsoluteToAssetPath(scriptPath));
                    var outputPath = ResolveLocalizationDocumentFilePath(scriptPath, SourceScriptsPath);
                    var existingAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(PathUtils.AbsoluteToAssetPath(outputPath));
                    var existingDoc = existingAsset ? ManagedTextUtils.ParseMultiline(existingAsset.text) : null;
                    var localizer = new ScriptLocalizer(new() {
                        Syntax = Compiler.Syntax,
                        OnUntranslated = missingKeys.Add,
                        Annotate = Annotate,
                        AnnotationPrefix = l10nConfig.AnnotationPrefix,
                        Separator = l10nConfig.RecordSeparator[0]
                    });
                    var document = localizer.Localize(script, existingDoc);
                    var documentText = ManagedTextUtils.SerializeMultiline(document, spacing);
                    var output = PrependHeader(documentText, localeFolderPath, $"'{script.Path}' Naninovel scenario script");
                    var outputDir = Path.GetDirectoryName(outputPath);
                    if (!Directory.Exists(outputDir)) Directory.CreateDirectory(outputDir);
                    File.WriteAllText(outputPath, output);
                    AppendWordCount(document);
                    if (warnUntranslated) WarnUntranslated(existingAsset);
                }
            }

            void LocalizeBasedOnExistingL10nDocuments ()
            {
                EditorUtility.DisplayProgressBar(progressBarTitle, "Finding source localization documents...", 0);
                var sourceL10nDocsDir = Path.Combine(SourceScriptsPath, ManagedTextPaths.ScriptLocalizationPrefix);
                if (!Directory.Exists(sourceL10nDocsDir))
                {
                    Debug.LogError("Failed to generate localization documents based on non-source locale. Make sure to generate script localization documents for the locale and select managed text root folder of the locale ('Assets/Resources/Naninovel/Localization/{locale}/Text' by default).");
                    return;
                }
                var sourceFilePaths = Directory.GetFiles(sourceL10nDocsDir, "*.txt", SearchOption.AllDirectories);
                for (int i = 0; i < sourceFilePaths.Length; i++)
                {
                    var sourceFilePath = sourceFilePaths[i];
                    var sourceFileName = Path.GetFileNameWithoutExtension(sourceFilePath);
                    EditorUtility.DisplayProgressBar(progressBarTitle, $"Processing '{sourceFileName}.txt'...", i / (float)sourceFilePaths.Length);
                    var sourceAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(PathUtils.AbsoluteToAssetPath(sourceFilePath));
                    var sourceDoc = ManagedTextUtils.ParseMultiline(sourceAsset.text);
                    var outputPath = ResolveLocalizationDocumentFilePath(sourceFilePath, sourceL10nDocsDir);
                    var existingAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(PathUtils.AbsoluteToAssetPath(outputPath));
                    var existingDoc = existingAsset ? ManagedTextUtils.ParseMultiline(existingAsset.text) : null;
                    var localizer = new ManagedTextLocalizer(new() {
                        Annotate = Annotate,
                        AnnotationPrefix = l10nConfig.AnnotationPrefix,
                        OnUntranslated = missingKeys.Add
                    });
                    var document = localizer.Localize(sourceDoc, existingDoc);
                    var documentText = ManagedTextUtils.SerializeMultiline(document, spacing);
                    var output = PrependHeader(documentText, localeFolderPath, $"'{sourceFileName}' Naninovel scenario script");
                    var outputDir = Path.GetDirectoryName(outputPath);
                    if (!Directory.Exists(outputDir)) Directory.CreateDirectory(outputDir);
                    File.WriteAllText(outputPath, output);
                    AppendWordCount(document);
                    if (warnUntranslated) WarnUntranslated(existingAsset);
                }
            }

            string ResolveLocalizationDocumentFilePath (string sourceFilePath, string sourceDirPath)
            {
                sourceFilePath = PathUtils.FormatPath(sourceFilePath);
                sourceDirPath = PathUtils.FormatPath(sourceDirPath);
                if (!sourceDirPath.EndsWithFast("/")) sourceDirPath += "/";
                var relative = sourceFilePath.GetAfterFirst(sourceDirPath);
                return Path.Combine(outputDirPath, Path.ChangeExtension(relative, "txt"));
            }

            void WarnUntranslated (TextAsset doc)
            {
                if (!doc || missingKeys.Count == 0) return;
                var lines = doc.text.SplitByNewLine();
                foreach (var key in missingKeys)
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].GetAfterFirst(ManagedTextConstants.RecordMultilineKeyLiteral)?.Trim() != key) continue;
                        Engine.Warn($"{EditorUtils.BuildAssetLink(doc, i + 1)} localization document is missing '{key}' key translation at line #{i + 1}.");
                        break;
                    }
                missingKeys.Clear();
            }

            void AppendWordCount (ManagedTextDocument doc)
            {
                foreach (var record in doc.Records)
                    // string.Split(null) will delimit by whitespace chars; 'default(char[])' is used to prevent ambiguity in case of overloads.
                    wordCount += record.Comment.SplitByNewLine().LastOrDefault()?
                        .Split(default(char[]), StringSplitOptions.RemoveEmptyEntries).Length ?? 0;
            }
        }

        private void LocalizeManagedText (string localeFolderPath)
        {
            if (!Directory.Exists(SourceManagedTextPath)) return;

            var outputPath = $"{localeFolderPath}/{Configuration.GetOrDefault<ManagedTextConfiguration>().Loader.PathPrefix}";
            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);

            var filePaths = Directory.GetFiles(baseOnSourceLocale ? SourceManagedTextPath : SourceScriptsPath, "*.txt", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < filePaths.Length; i++)
            {
                var filePath = filePaths[i];
                var documentPath = Path.GetFileNameWithoutExtension(filePath);
                var targetPath = Path.Combine(outputPath, $"{documentPath}.txt");
                var localizer = new ManagedTextLocalizer(new() {
                    Annotate = Annotate,
                    AnnotationPrefix = l10nConfig.AnnotationPrefix
                });
                var sourceText = File.ReadAllText(filePath);
                var sourceDoc = ManagedTextUtils.Parse(sourceText, documentPath, filePath);
                var existingText = File.Exists(targetPath) ? File.ReadAllText(targetPath) : null;
                var existingDoc = existingText != null ? ManagedTextUtils.Parse(existingText, documentPath, targetPath) : null;
                var document = localizer.Localize(sourceDoc, existingDoc);
                var documentText = ManagedTextUtils.Serialize(document, documentPath, spacing);
                var output = PrependHeader(documentText, localeFolderPath, $"'{documentPath}' managed text document");
                File.WriteAllText(targetPath, output);
            }
        }

        private string PrependHeader (string documentText, string localeFolderPath, string sourceName)
        {
            var targetTag = Path.GetFileName(localeFolderPath);
            var targetLanguage = Languages.GetNameByTag(targetTag);
            var header = $"{ManagedTextConstants.RecordCommentLiteral}" +
                         $"{sourceLanguage.Remove(" (source)")} <{sourceTag}> to " +
                         $"{targetLanguage} <{targetTag}> localization document for {sourceName}";
            if (!string.IsNullOrWhiteSpace(documentText))
            {
                if (!documentText.StartsWithFast("\n")) header += '\n';
                if (!documentText.StartsWithFast("\n\n")) header += '\n';
            }
            return header + documentText;
        }
    }
}
