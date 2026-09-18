using System;

namespace Naninovel
{
    /// <summary>
    /// When applied to a <see cref="ConfigurationSettings"/> implementation,
    /// the implementation will be used instead of the built-in one when drawing Naninovel configuration editor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class OverrideSettingsAttribute : Attribute { }
}
