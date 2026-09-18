using System;
using System.Collections.Generic;
using Naninovel.ManagedText;

namespace Naninovel
{
    /// <summary>
    /// Generates localization documents for managed text.
    /// </summary>
    public class ManagedTextLocalizer
    {
        /// <summary>
        /// Generation options.
        /// </summary>
        public class Options
        {
            /// <summary>
            /// Whether to prepend comments of the source records into the localized records.
            /// </summary>
            public bool Annotate { get; set; } = true;
            /// <summary>
            /// String to insert before annotation lines to distinguish them for the localized text.
            /// </summary>
            public string AnnotationPrefix { get; set; } = "> ";
            /// <summary>
            /// Delegate to invoke when an untranslated text is discovered; invoked with managed text record key.
            /// Has effect only when existing (previously generated) localization document is specified.
            /// </summary>
            public Action<string> OnUntranslated { get; set; }
        }

        private readonly List<ManagedTextRecord> records = new();
        private readonly Options options;
        private ManagedTextDocument existing;

        public ManagedTextLocalizer (Options options = null)
        {
            this.options = options ?? new Options();
        }

        /// <summary>
        /// Generates localization document for specified source managed text document by moving
        /// source record values into comments of the localized records, assuming values of the
        /// localized records would contain the associated translation.
        /// When existing (previously generated) document is specified, preserves localized values.
        /// </summary>
        /// <param name="script">Source managed text document to generate localization document for.</param>
        /// <param name="existing">Document previously generated for the source with localized values to preserve.</param>
        /// <returns>Generated localization document.</returns>
        public ManagedTextDocument Localize (ManagedTextDocument source, ManagedTextDocument existing = null)
        {
            Reset(existing);
            foreach (var record in source.Records)
                AppendRecord(record);
            return new(records.ToArray());
        }

        private void Reset (ManagedTextDocument existing)
        {
            records.Clear();
            this.existing = existing;
        }

        private void AppendRecord (ManagedTextRecord record)
        {
            var key = record.Key;
            var comment = record.Value;
            if (options.Annotate && !string.IsNullOrWhiteSpace(record.Comment))
                comment = $"{options.AnnotationPrefix}{record.Comment}\n{comment}";
            var value = ResolveExisting(key);
            records.Add(new(key, value, comment));
        }

        private string ResolveExisting (string key)
        {
            if (existing is null) return null;
            if (existing.TryGet(key, out var val) &&
                !string.IsNullOrEmpty(val.Value)) return val.Value;
            options.OnUntranslated?.Invoke(key);
            return null;
        }
    }
}
