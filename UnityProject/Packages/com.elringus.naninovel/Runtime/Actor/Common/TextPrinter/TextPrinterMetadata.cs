using System;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents data required to construct and initialize a <see cref="ITextPrinterActor"/>.
    /// </summary>
    [Serializable]
    public class TextPrinterMetadata : OrthoActorMetadata
    {
        [Serializable]
        public class Map : ActorMetadataMap<TextPrinterMetadata> { }

        [Tooltip("Whether to automatically reset the printer on each `@print` command (unless `reset` parameter is explicitly disabled).")]
        public bool AutoReset = true;
        [Tooltip("Whether to automatically make the printer default and hide other printers on each `@print` command (unless `default` parameter is explicitly disabled).")]
        public bool AutoDefault = true;
        [Tooltip("Whether to automatically wait for user input on each `@print` command (unless `waitInput` parameter is explicitly disabled).")]
        public bool AutoWait = true;
        [Tooltip("Controls whether to wait for user input after text reveal was skipped. When disabled, will not wait, no matter the `Auto Wait` option or `waitInput` parameter.")]
        public bool WaitAfterRevealSkip = true;
        [Tooltip("Whether to add printed messages to the printer backlog.")]
        public bool AddToBacklog = true;
        [Tooltip("Whether to stop any playing voices on each `@print` command.")]
        public bool StopVoice;
        [Tooltip("Default visibility change animation duration; used when corresponding parameter is not specified in script command.")]
        public float ChangeVisibilityDuration = .3f;
        [Tooltip("Whether to wait until the printer is fully visible before starting to print the text.")]
        public bool WaitVisibilityBeforePrint;
        [Tooltip("Number of frames to wait before completing @print command. A value greater than zero is required to make the printed text visible while in skip mode.")]
        public int PrintFrameDelay = 1;
        [Tooltip("Whether to always show printed text instantly, no matter the reveal speed setting.")]
        public bool RevealInstantly;

        public TextPrinterMetadata ()
        {
            Implementation = typeof(UITextPrinter).AssemblyQualifiedName;
            Loader = new() { PathPrefix = TextPrintersConfiguration.DefaultPathPrefix };
            Pivot = new(.5f, .5f);
        }

        public override ActorPose<TState> GetPose<TState> (string poseName) => null;
    }
}
