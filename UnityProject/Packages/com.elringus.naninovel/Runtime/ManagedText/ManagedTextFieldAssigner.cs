using System;
using System.Collections.Generic;
using System.Reflection;
using Naninovel.ManagedText;

namespace Naninovel
{
    /// <summary>
    /// Assigns static fields marked with <see cref="ManagedTextAttribute"/>.
    /// </summary>
    public class ManagedTextFieldAssigner
    {
        private struct ManagedField
        {
            public FieldInfo Info;
            public ManagedTextAttribute Attribute;
        }

        private readonly List<UniTask> tasks = new();
        private readonly List<ManagedField> fields = new();
        private readonly ITextManager docs;

        public ManagedTextFieldAssigner (ITextManager docs)
        {
            this.docs = docs;
        }

        public async UniTask Assign ()
        {
            Reset();
            foreach (var hostType in Engine.Types.ManagedTextFieldHosts)
            foreach (var field in GetManagedFields(hostType))
                tasks.Add(AssignField(field));
            await UniTask.WhenAll(tasks);
            docs.DocumentLoader.ReleaseAll(this);
        }

        private void Reset ()
        {
            tasks.Clear();
            fields.Clear();
        }

        private IReadOnlyCollection<ManagedField> GetManagedFields (Type hostType)
        {
            foreach (var field in hostType.GetFields(ManagedTextUtils.ManagedFieldBindings))
                if (field.GetCustomAttribute<ManagedTextAttribute>() is { } attribute)
                    fields.Add(new() { Info = field, Attribute = attribute });
            return fields;
        }

        private async UniTask AssignField (ManagedField field)
        {
            var documentPath = field.Attribute.DocumentPath;
            if (!docs.DocumentLoader.IsLoaded(documentPath))
                await docs.DocumentLoader.Load(documentPath, this);
            var key = $"{field.Info.DeclaringType!.Name}.{field.Info.Name}";
            if (docs.TryGetRecord(key, documentPath, out var record))
                field.Info.SetValue(null, GetValueWithFallback(record));
        }

        private string GetValueWithFallback (ManagedTextRecord record)
        {
            if (!string.IsNullOrEmpty(record.Value)) return record.Value;
            if (!string.IsNullOrEmpty(record.Comment)) return record.Comment;
            return record.Key;
        }
    }
}
