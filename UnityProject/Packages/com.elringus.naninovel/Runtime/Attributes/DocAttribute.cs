using System;
using System.Diagnostics;

namespace Naninovel
{
    /// <summary>
    /// Used by Naninovel authoring tools to resolve documentation for metadata artifacts,
    /// such as commands and expression functions.
    /// </summary>
    /// <remarks>Web docs overflow limit for examples is 80 chars.</remarks>
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public sealed class DocAttribute : Attribute
    {
        public readonly string Summary;
        public readonly string Remarks;
        public readonly string[] Examples;

        /// <param name="summary">Description of the artifact.</param>
        /// <param name="remarks">Additional info about the artifact.</param>
        /// <param name="examples">Examples on using the artifact.</param>
        public DocAttribute (string summary, string remarks = null, params string[] examples)
        {
            Summary = summary;
            Remarks = remarks;
            Examples = examples;
        }
    }

    /// <summary>
    /// Common documentation artifacts.
    /// </summary>
    public static class SharedDocs
    {
        public const string DurationParameter = @"
Duration of the animation initiated by the command, in seconds.";
        public const string WaitParameter = @"
Whether to wait for the command to finish before starting executing next command in the scenario script.
Default behaviour is controlled by `Wait By Default` option in the script player configuration.";
        public const string TintParameter = @"
The tint color to apply.

Strings that begin with `#` will be parsed as hexadecimal in the following way:
`#RGB` (becomes `RRGGBB`), `#RRGGBB`, `#RGBA` (becomes `RRGGBBAA`), `#RRGGBBAA`; when alpha is not specified will default to `FF`.

Strings that do not begin with `#` will be parsed as literal colors, with the following supported:
red, cyan, blue, darkblue, lightblue, purple, yellow, lime, fuchsia, white, silver, grey, black, orange, brown, maroon, green, olive, navy, teal, aqua, magenta.";
        public const string EasingParameter = @"
Name of the [easing function](/guide/transition-effects#animation-easing) to apply.
When not specified, will use a default function set in the configuration.";
        public const string LazyParameter = @"
When the animation initiated by the command is already running, enabling `lazy` will continue the animation to the new target from the current state.
When `lazy` is not enabled (default behaviour), currently running animation will instantly complete before starting animating to the new target.";
    }
}
