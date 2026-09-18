using System;
using System.Collections.Generic;
using UnityEditor;

namespace Naninovel
{
    public class BackgroundsSettings : OrthoActorManagerSettings<BackgroundsConfiguration, IBackgroundActor, BackgroundMetadata>
    {
        protected override string HelpUri => "guide/backgrounds";
        protected override string ResourcesSelectionTooltip => GetTooltip();
        protected override MetadataEditor<IBackgroundActor, BackgroundMetadata> MetadataEditor { get; } = new BackgroundMetadataEditor();
        protected override HashSet<string> LockedActorIds => new() { BackgroundsConfiguration.MainActorId };

        private static bool editMainRequested;

        public override void OnGUI (string searchContext)
        {
            if (editMainRequested)
            {
                editMainRequested = false;
                MetadataMapEditor.SelectEditedMetadata(BackgroundsConfiguration.MainActorId);
            }

            base.OnGUI(searchContext);
        }

        protected override Dictionary<string, Action<SerializedProperty>> OverrideConfigurationDrawers ()
        {
            var drawers = base.OverrideConfigurationDrawers();
            drawers[nameof(CharactersConfiguration.SharedPoses)] = ActorPosesEditor.Draw;
            return drawers;
        }

        private string GetTooltip ()
        {
            if (EditedActorId == BackgroundsConfiguration.MainActorId && AllowMultipleResources)
                return "Use `@back %name%` in naninovel scripts to show main background with the selected appearance.";
            if (AllowMultipleResources)
                return $"Use `@back %name% id:{EditedActorId}` in naninovel scripts to show this background with the selected appearance.";
            return $"Use `@back id:{EditedActorId}` in naninovel scripts to show this background.";
        }

        [MenuItem("Naninovel/Resources/Backgrounds")]
        private static void OpenResourcesWindow ()
        {
            // Automatically open main background editor when opened via resources context menu.
            editMainRequested = true;
            OpenResourcesWindowImpl();
        }
    }
}
