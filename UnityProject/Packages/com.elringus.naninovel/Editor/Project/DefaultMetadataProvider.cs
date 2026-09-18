using System;
using Naninovel.Metadata;
using UnityEditor;

namespace Naninovel
{
    public class DefaultMetadataProvider : IMetadataProvider
    {
        public Project GetMetadata ()
        {
            var meta = new Project();
            var cfg = ProjectConfigurationProvider.LoadOrDefault<ScriptsConfiguration>();
            meta.EntryScript = cfg.StartGameScript;
            meta.TitleScript = cfg.TitleScript;
            Notify("Processing commands...", 0);
            meta.Commands = MetadataGenerator.GenerateCommandsMetadata();
            Notify("Processing resources...", .25f);
            meta.Resources = MetadataGenerator.GenerateResourcesMetadata();
            Notify("Processing actors...", .50f);
            meta.Actors = MetadataGenerator.GenerateActorsMetadata();
            Notify("Processing variables...", .75f);
            meta.Variables = MetadataGenerator.GenerateVariablesMetadata();
            Notify("Processing functions...", .95f);
            meta.Functions = MetadataGenerator.GenerateFunctionsMetadata();
            Notify("Processing constants...", .99f);
            meta.Constants = MetadataGenerator.GenerateConstantsMetadata();
            meta.Syntax = Compiler.Syntax;
            return meta;
        }

        private static void Notify (string info, float progress)
        {
            if (EditorUtility.DisplayCancelableProgressBar("Generating Metadata", info, progress))
                throw new OperationCanceledException("Metadata generation cancelled by the user.");
        }
    }
}
