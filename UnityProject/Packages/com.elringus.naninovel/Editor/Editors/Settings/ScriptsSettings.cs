using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    public class ScriptsSettings : ResourcefulSettings<ScriptsConfiguration>
    {
        protected override string HelpUri => "guide/naninovel-scripts";
        protected override Type ResourcesTypeConstraint => typeof(Script);
        protected override string ResourcesCategoryId => Configuration.Loader.PathPrefix;
        protected override string ResourcesSelectionTooltip => "Use `@goto %name%` in naninovel scripts to load and start playing selected naninovel script.";

        private static readonly string[] implementations, labels;
        private static readonly GUIContent rootContent = new("Scenario Root", "The common ancestor directory of all the scenario scripts ('.nani' files). The location is resolved automatically based on the existing scenario files in the project.");

        static ScriptsSettings ()
        {
            InitializeImplementationOptions<IScriptParser>(ref implementations, ref labels);
        }

        protected override void DrawConfigurationEditor ()
        {
            EditorGUILayout.LabelField(rootContent, new GUIContent(PackagePath.ScriptsRoot));
            base.DrawConfigurationEditor();
        }

        protected override Dictionary<string, Action<SerializedProperty>> OverrideConfigurationDrawers ()
        {
            var drawers = base.OverrideConfigurationDrawers();
            drawers[nameof(ScriptsConfiguration.ScriptParser)] = p => DrawImplementations(implementations, labels, p);
            drawers[nameof(ScriptsConfiguration.WatchScripts)] = p => OnChanged(ScriptFileWatcher.Initialize, p);
            drawers[nameof(ScriptsConfiguration.ExternalLoader)] = p => DrawWhen(Configuration.EnableCommunityModding, p);
            drawers[nameof(ScriptsConfiguration.HideUnusedParameters)] = p => DrawWhen(Configuration.EnableVisualEditor, p);
            drawers[nameof(ScriptsConfiguration.InsertLineKey)] = p => DrawWhen(Configuration.EnableVisualEditor, p);
            drawers[nameof(ScriptsConfiguration.InsertLineModifier)] = p => DrawWhen(Configuration.EnableVisualEditor, p);
            drawers[nameof(ScriptsConfiguration.SaveScriptKey)] = p => DrawWhen(Configuration.EnableVisualEditor, p);
            drawers[nameof(ScriptsConfiguration.SaveScriptModifier)] = p => DrawWhen(Configuration.EnableVisualEditor, p);
            drawers[nameof(ScriptsConfiguration.EditorPageLength)] = p => DrawWhen(Configuration.EnableVisualEditor, p);
            drawers[nameof(ScriptsConfiguration.EditorCustomStyleSheet)] = p => DrawWhen(Configuration.EnableVisualEditor, p);
            drawers[nameof(ScriptsConfiguration.SelectPlayedScript)] = p => DrawWhen(Configuration.EnableVisualEditor, p);
            drawers[nameof(ScriptsConfiguration.RewindMouseButton)] = p => DrawWhen(Configuration.EnableVisualEditor, p);
            drawers[nameof(ScriptsConfiguration.RewindModifier)] = p => DrawWhen(Configuration.EnableVisualEditor, p);
            drawers[nameof(ScriptsConfiguration.CompilerLocalization)] = DrawCompilerLocalizationProperty;
            return drawers;
        }

        [MenuItem("Naninovel/Resources/Scripts")]
        private static void OpenResourcesWindow () => OpenResourcesWindowImpl();

        private void DrawCompilerLocalizationProperty (SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property);
            if (property.objectReferenceValue) return;

            var path = PathUtils.Combine(PackagePath.PrefabsPath, "DefaultCompiler.asset");
            var asset = AssetDatabase.LoadAssetAtPath<CompilerLocalization>(path);
            property.objectReferenceValue = asset;
        }
    }
}
