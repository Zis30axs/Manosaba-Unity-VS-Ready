using System;
using System.Text;

namespace Naninovel
{
    /// <summary>
    /// Allows resolving text of <see cref="LocalizableTextParameter"/>
    /// outside of runtime (while engine is not initialized).
    /// </summary>
    public static class LocalizableTextResolver
    {
        private static readonly StringBuilder builder = new();

        /// <summary>
        /// Attempts to resolve specified localizable text parameter.
        /// Will handle dynamic values, skip expressions and resolve localizable IDs with the specified resolver.
        /// Will return empty when parameter is not assigned or is dynamic and is missing raw value.
        /// </summary>
        /// <param name="param">Parameter to resolve.</param>
        /// <param name="resolveId">Function that takes localizable text ID and returns associated localized text.</param>
        public static string Resolve (LocalizableTextParameter param, Func<string, string> resolveId)
        {
            if (!Command.Assigned(param)) return "";
            builder.Clear();
            if (param.DynamicValue) return ResolveDynamic(param, resolveId);
            return ResolveStatic(param, resolveId);
        }

        private static string ResolveDynamic (LocalizableTextParameter param, Func<string, string> resolveId)
        {
            if (!param.RawValue.HasValue) return "";
            foreach (var part in param.RawValue.Value.Parts)
            {
                if (part.Kind == ParameterValuePartKind.PlainText) builder.Append(part.Text);
                else if (part.Kind == ParameterValuePartKind.IdentifiedText) builder.Append(resolveId(part.Id));
            }
            return builder.ToString();
        }

        private static string ResolveStatic (LocalizableTextParameter param, Func<string, string> resolveId)
        {
            foreach (var part in param.Value.Parts)
            {
                if (part.PlainText) builder.Append(part.PlainText);
                else builder.Append(resolveId(part.Id));
            }
            return builder.ToString();
        }
    }
}
