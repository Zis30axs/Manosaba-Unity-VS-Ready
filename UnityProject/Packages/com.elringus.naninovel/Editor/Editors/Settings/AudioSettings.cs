using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    public class AudioSettings : ResourcefulSettings<AudioConfiguration>
    {
        protected override string HelpUri => "guide/audio";
        protected override Type ResourcesTypeConstraint => typeof(AudioClip);
        protected override string ResourcesCategoryId => Configuration.AudioLoader.PathPrefix;
        protected override string ResourcesSelectionTooltip => "Use `@bgm %name%` or `@sfx %name%` in naninovel scripts to play a background music or sound effect of the selected audio clip.";

        private static readonly string[] implementations, labels;

        static AudioSettings ()
        {
            InitializeImplementationOptions<IAudioPlayer>(ref implementations, ref labels);
        }

        protected override void DrawConfigurationEditor ()
        {
            base.DrawConfigurationEditor();

            if (Configuration.EnableAutoVoicing && GUILayout.Button("Open Voice Map Utility", GUIStyles.NavigationButton))
                VoiceMapWindow.OpenWindow();
        }

        protected override Dictionary<string, Action<SerializedProperty>> OverrideConfigurationDrawers ()
        {
            var drawers = base.OverrideConfigurationDrawers();
            drawers[nameof(AudioConfiguration.AudioPlayer)] = p => DrawImplementations(implementations, labels, p);
            return drawers;
        }

        [MenuItem("Naninovel/Resources/Audio")]
        private static void OpenResourcesWindow () => OpenResourcesWindowImpl();
    }
}
