#if ADDRESSABLES_AVAILABLE

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Naninovel.Metadata;
using Naninovel.Parsing;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;

namespace Naninovel
{
    /// <summary>
    /// Assigns labels to the Naninovel addressable assets by the scenario script path
    /// they're used in for a more efficient bundle packing.
    /// </summary>
    public static class AddressableLabeler
    {
        private readonly struct ParaCtx
        {
            public readonly ICommandParameter Para;
            public readonly Metadata.Parameter Meta;

            public ParaCtx (ICommandParameter para, Metadata.Parameter meta)
            {
                Para = para;
                Meta = meta;
            }

            public void Deconstruct (out ICommandParameter param, out Metadata.Parameter meta)
            {
                param = Para;
                meta = Meta;
            }
        }

        private readonly struct ValueCtx
        {
            public readonly string Value;
            public readonly ValueContext Ctx;

            public ValueCtx (string value, ValueContext ctx)
            {
                Value = value;
                Ctx = ctx;
            }

            public void Deconstruct (out string value, out ValueContext ctx)
            {
                value = Value;
                ctx = Ctx;
            }
        }

        private const string scriptLabelPrefix = "Scripts/";

        public static void Label (AddressableAssetSettings settings)
        {
            try { AssignLabels(LoadScripts(), MapEntries(settings), GenerateMetadata()); }
            catch (Exception e) { Engine.Err($"Failed to label addressables: {e}"); }
            finally { EditorUtility.ClearProgressBar(); }
        }

        // [MenuItem("Naninovel/This/Label Addressable")]
        // private static void LabelFromMenu () => Label(UnityEditor.AddressableAssets.AddressableAssetSettingsDefaultObject.GetSettings(false));

        private static IReadOnlyCollection<Script> LoadScripts ()
        {
            Progress("Loading scenario scripts...");
            return AssetDatabase.FindAssets("t:Naninovel.Script", new[] { PackagePath.ScriptsRoot })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Script>)
                .ToArray();
        }

        private static IReadOnlyDictionary<string, AddressableAssetEntry> MapEntries (AddressableAssetSettings settings)
        {
            Progress("Mapping existing entries...");
            var entryByAddress = new Dictionary<string, AddressableAssetEntry>();
            foreach (var group in settings.groups)
            foreach (var entry in group.entries)
            {
                if (!entry.labels.Contains(ResourceProviderConfiguration.AddressableId)) continue;
                entryByAddress[entry.address] = entry;
                foreach (var label in entry.labels.ToArray())
                    if (label.StartsWithFast(scriptLabelPrefix))
                        entry.SetLabel(label, false);
            }
            return entryByAddress;
        }

        private static IReadOnlyDictionary<string, Metadata.Command> GenerateMetadata ()
        {
            Progress("Generating commands metadata...");
            var metaByTypeName = new Dictionary<string, Metadata.Command>();
            foreach (var meta in MetadataGenerator.GenerateCommandsMetadata())
                metaByTypeName[meta.Id] = meta;
            return metaByTypeName;
        }

        private static void AssignLabels (
            IReadOnlyCollection<Script> scripts,
            IReadOnlyDictionary<string, AddressableAssetEntry> entryByAddress,
            IReadOnlyDictionary<string, Metadata.Command> metaByTypeName)
        {
            var syntax = Configuration.GetOrDefault<ScriptsConfiguration>().CompilerLocalization.GetSyntax();
            var namedParser = new NamedValueParser(syntax);
            var listParser = new ListValueParser(syntax);

            var done = 0f;
            var total = scripts.Count;
            foreach (var script in scripts)
                ProcessScript(script);

            void AssignLabel (string resourcePathPrefix, string resourcePath, string scriptPath)
            {
                var address = $"{ResourceProviderConfiguration.AddressableId}/{resourcePathPrefix}/{resourcePath}";
                if (!entryByAddress.TryGetValue(address, out var entry)) return;
                entry.SetLabel($"{scriptLabelPrefix}{scriptPath}", true, true);
            }

            void ProcessScript (Script script)
            {
                Progress($"Processing '{script}'...", done / total);
                foreach (var command in script.ExtractCommands())
                    ProcessCommand(command, script.Path);
            }

            void ProcessCommand (Command command, string scriptPath)
            {
                var cmdMeta = metaByTypeName.GetValueOrDefault(command.GetType().Name)
                              ?? throw new Error($"Missing '{command.GetType().Name}' command metadata.");
                var cmdParas = new List<ParaCtx>();
                foreach (var field in command.GetType().GetFields())
                foreach (var paraMeta in cmdMeta.Parameters)
                    if (paraMeta.Id == field.Name)
                        cmdParas.Add(new((ICommandParameter)field.GetValue(command), paraMeta));
                foreach (var para in cmdParas)
                    ProcessParameter(para, cmdParas, scriptPath);
            }

            void ProcessParameter (ParaCtx para, IReadOnlyCollection<ParaCtx> cmdParas, string scriptPath)
            {
                if (!Command.Assigned(para.Para)) return;
                if (para.Meta.ValueContext is null || !para.Meta.ValueContext.Any(c => c != null && IsResourcefulContext(c.Type))) return;
                if (para.Para.DynamicValue)
                {
                    Engine.Warn($"Expression in '{para.Meta.Label}' parameter value prevents resolving associated resources ahead of time, " +
                                $"which may result in degraded runtime performance and inefficient asset bundle packaging. " +
                                $"Consider using custom command instead.", para.Para.PlaybackSpot);
                    return;
                }
                foreach (var (value, valueCtx) in FindValues(para, IsResourcefulContext))
                    if (!string.IsNullOrWhiteSpace(value) && FindPathPrefix(valueCtx, cmdParas) is { } pre)
                        AssignLabel(pre, value, scriptPath);
            }

            IReadOnlyCollection<ValueCtx> FindValues (ParaCtx para, Predicate<ValueContextType> predicate)
            {
                // Resources can't be stored in localizable parameters.
                if (para.Para is LocalizableTextParameter) return Array.Empty<ValueCtx>();
                var raw = para.Para.RawValue.HasValue && Command.AssignedStatic(para.Para) ? para.Para.RawValue!.Value.Parts[0].Text : null;
                var result = new List<ValueCtx>();
                if (para.Meta.ValueContainerType == ValueContainerType.Single)
                {
                    if (CheckContextAt(0, out var ctx)) result.Add(new(raw, ctx));
                }
                if (para.Meta.ValueContainerType == ValueContainerType.Named)
                {
                    var named = raw != null ? namedParser.Parse(raw) : default;
                    if (CheckContextAt(0, out var ctx0)) result.Add(new(named.Name, ctx0));
                    if (CheckContextAt(1, out var ctx1)) result.Add(new(named.Value, ctx1));
                }
                if (para.Meta.ValueContainerType == ValueContainerType.List)
                {
                    var list = raw != null ? listParser.Parse(raw) : new[] { default(string) };
                    if (CheckContextAt(0, out var ctx))
                        foreach (var item in list)
                            result.Add(new(item, ctx));
                }
                if (para.Meta.ValueContainerType == ValueContainerType.NamedList)
                {
                    var list = raw != null ? listParser.Parse(raw) : new[] { default(string) };
                    if (CheckContextAt(0, out var ctx))
                        foreach (var item in list)
                            result.Add(new((item != null ? namedParser.Parse(item) : default).Name, ctx));
                }
                return result;

                bool CheckContextAt (int idx, out ValueContext ctx)
                {
                    ctx = para.Meta.ValueContext?.ElementAtOrDefault(idx);
                    return ctx != null && predicate(ctx.Type);
                }
            }

            [CanBeNull]
            string FindPathPrefix (ValueContext ctx, IReadOnlyCollection<ParaCtx> cmdParas)
            {
                // Prefix for appearance resource is the prefix of the associated actor resource + actor ID.
                // Associated actor is specified in a parameter in the same command; when it's not assigned,
                // appearance may have default actor ID specified in sub-type.
                if (ctx.Type == ValueContextType.Appearance)
                {
                    var (actorId, actorCtx) = cmdParas.SelectMany(p =>
                        FindValues(p, ct => ct == ValueContextType.Actor)).FirstOrDefault();
                    if (string.IsNullOrEmpty(actorCtx.SubType) || actorCtx.SubType == "*") return null;
                    if (string.IsNullOrEmpty(actorId)) actorId = ctx.SubType;
                    if (string.IsNullOrEmpty(actorId)) return null;
                    return $"{actorCtx.SubType}/{actorId}";
                }
                // Prefix for resource and actor contexts is specified in sub-type.
                if (string.IsNullOrEmpty(ctx.SubType) || ctx.SubType == "*") return null;
                return ctx.SubType;
            }

            static bool IsResourcefulContext (ValueContextType ct) => ct switch {
                ValueContextType.Resource => true,
                ValueContextType.Actor => true,
                ValueContextType.Appearance => true,
                _ => false
            };
        }

        private static void Progress (string activity, float progress = 1)
        {
            EditorUtility.DisplayProgressBar("Labelling Naninovel Addressables", activity, progress);
        }
    }
}

#endif
