using System;
using System.Globalization;
using System.Linq;

namespace Naninovel
{
    public static class ParseUtils
    {
        /// <summary>
        /// Invokes a <see cref="int.TryParse(string, NumberStyles, System.IFormatProvider, out int)"/> on the specified string,
        /// using <see cref="CultureInfo.InvariantCulture"/> and <see cref="NumberStyles.Integer"/>.
        /// </summary>
        /// <returns>Whether parsing succeeded.</returns>
        public static bool TryInvariantInt (string str, out int result)
        {
            return int.TryParse(str, NumberStyles.Integer, CultureInfo.InvariantCulture, out result);
        }

        /// <summary>
        /// Invokes a <see cref="float.TryParse(string, NumberStyles, System.IFormatProvider, out float)"/> on the specified string,
        /// using <see cref="CultureInfo.InvariantCulture"/> and <see cref="NumberStyles.Float"/>.
        /// </summary>
        /// <returns>Whether parsing succeeded.</returns>
        public static bool TryInvariantFloat (string str, out float result)
        {
            return float.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out result);
        }

        /// <summary>
        /// Invokes a <see cref="int.TryParse(string, NumberStyles, System.IFormatProvider, out int)"/> on the specified string,
        /// using <see cref="CultureInfo.InvariantCulture"/> and <see cref="NumberStyles.Integer"/>.
        /// </summary>
        /// <returns>Parsed value when parsing succeeded or null.</returns>
        public static int? AsInvariantInt (this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            var succeeded = TryInvariantInt(str, out var result);
            return succeeded ? result : null;
        }

        /// <summary>
        /// Invokes a <see cref="float.TryParse(string, NumberStyles, System.IFormatProvider, out float)"/> on the specified string,
        /// using <see cref="CultureInfo.InvariantCulture"/> and <see cref="NumberStyles.Float"/>.
        /// </summary>
        /// <returns>Parsed value when parsing succeeded or null.</returns>
        public static float? AsInvariantFloat (this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            var succeeded = TryInvariantFloat(str, out var result);
            return succeeded ? result : null;
        }

        /// <summary>
        /// Attempts to parse an enum value assigned to script command parameter with constant context.
        /// Will consider constants localized via <see cref="Compiler"/>.
        /// </summary>
        /// <returns>Whether parsing succeeded.</returns>
        public static bool TryConstantParameter<TEnum> (string value, out TEnum result) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum) throw new Error("Specified type is not an enum.");
            if (Compiler.Constants.TryGetValue(typeof(TEnum).Name, out var l10n))
                if (l10n.Values.FirstOrDefault(v => v.Alias.EqualsFastIgnoreCase(value)) is var vn)
                    if (!string.IsNullOrWhiteSpace(vn.Alias))
                        value = vn.Value;
            return Enum.TryParse<TEnum>(value, true, out result);
        }
    }
}
