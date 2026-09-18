namespace Naninovel
{
    /// <summary>
    /// Allows resolving local resource paths of voice clips associated with auto-voicing feature.
    /// </summary>
    /// <remarks>
    /// Local auto voice path has the following format:
    /// <code>ScriptPath/TextID</code>
    /// — where 'TextID' is the localizable text ID of the voiced line and
    /// 'ScriptPath' is the local resource path of the script containing the voiced line.
    /// </remarks>
    public static class AutoVoiceResolver
    {
        /// <summary>
        /// Returns auto voice clip (local) resource path based on specified localizable text;
        /// returns empty when the text doesn't contain localizable parts.
        /// </summary>
        public static string Resolve (LocalizableText text)
        {
            for (int i = 0; i < text.Parts.Count; i++)
                if (!text.Parts[i].PlainText)
                    return $"{text.Parts[i].Spot.ScriptPath}/{text.Parts[i].Id}";
            return string.Empty;
        }

        /// <summary>
        /// Returns auto voice clip (local) resource path based on specified localizable text parameter;
        /// returns null when the text doesn't contain localizable parts.
        /// </summary>
        public static string Resolve (LocalizableTextParameter param)
        {
            if (!Command.Assigned(param)) return null;
            if (param.DynamicValue)
            {
                if (param.RawValue.HasValue)
                    foreach (var part in param.RawValue.Value.Parts)
                        if (part.Kind == ParameterValuePartKind.IdentifiedText)
                            return $"{param.PlaybackSpot?.ScriptPath}/{part.Id}";
                return null;
            }
            return Resolve(param.Value);
        }
    }
}
