using System;
using System.IO;
using System.Text;
using Naninovel.Metadata;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Naninovel
{
    [ScriptedImporter(version: 54, ext: "nani")]
    public class ScriptImporter : ScriptedImporter
    {
        private readonly ScriptTextIdentifier identifier = new();

        public override void OnImportAsset (AssetImportContext ctx)
        {
            try
            {
                var cfg = Configuration.GetOrDefault<ScriptsConfiguration>();
                var assetPath = ctx.assetPath;
                var assetBytes = File.ReadAllBytes(assetPath);
                var scriptText = Encoding.UTF8.GetString(assetBytes, 0, assetBytes.Length);
                PurgeBom(assetPath, scriptText);

                var root = BuildProcessor.Building
                    ? Path.Combine(ResourcesBuilder.TempResourcesPath, "Naninovel", cfg.Loader.PathPrefix)
                    : PackagePath.ScriptsRoot;
                var pathResolver = new ScriptPathResolver { RootUri = root };
                var scriptPath = pathResolver.Resolve(assetPath);
                var script = Script.FromText(scriptPath, scriptText, assetPath);
                script.name = Path.GetFileNameWithoutExtension(assetPath);
                script.hideFlags = HideFlags.NotEditable;
                ctx.AddObjectToAsset("naniscript", script);
                ctx.SetMainObject(script);

                if (cfg.StableIdentification && !BuildProcessor.Building)
                    EditorApplication.delayCall += () => IdentifyText(assetPath, scriptText);
            }
            catch (Exception e)
            {
                ctx.LogImportError($"Failed to import Naninovel scenario script: {e}");
            }
        }

        // Unity auto adding BOM when creating script assets: https://git.io/fjVgY
        private static void PurgeBom (string assetPath, string contents)
        {
            if (contents.Length > 0 && contents[0] == '\uFEFF')
                File.WriteAllText(assetPath, contents[1..]);
        }

        private void IdentifyText (string assetPath, string scriptText)
        {
            var revs = ScriptRevisions.LoadOrDefault();
            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            var script = AssetDatabase.LoadAssetAtPath<Script>(assetPath);
            if (!script || guid is null)
                throw new Error($"Failed to identify '{EditorUtils.BuildAssetLink(script)}' script text: failed to load asset.");

            var options = new ScriptTextIdentifier.Options(revs.GetRevision(guid), assetPath);
            var result = identifier.Identify(script, options);
            if (result.ModifiedLines.Count == 0) return;
            revs.SetRevision(guid, result.Revision);
            revs.SaveAsset();

            var lines = Parsing.ScriptParser.SplitText(scriptText);
            foreach (var modifiedLineIndex in result.ModifiedLines)
                if (lines.IsIndexValid(modifiedLineIndex) && script.Lines.IsIndexValid(modifiedLineIndex))
                    lines[modifiedLineIndex] = Compiler.ScriptAssetSerializer.Serialize(script.Lines[modifiedLineIndex], script.TextMap);
                else Engine.Warn($"Failed to identify '{EditorUtils.BuildAssetLink(script, modifiedLineIndex + 1)}' script text: incorrect line index.");
            File.WriteAllText(assetPath, string.Join("\n", lines));
        }
    }
}
