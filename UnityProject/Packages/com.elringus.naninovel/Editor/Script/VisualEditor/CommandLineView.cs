using System.Collections.Generic;
using System.Linq;
using Naninovel.Parsing;
using UnityEngine.UIElements;

namespace Naninovel
{
    public class CommandLineView : ScriptLineView
    {
        private struct ParameterFieldData
        {
            public LineTextField Field;
            public string Id, Value;
            public bool Nameless;
        }

        public string CommandId { get; private set; }

        private static readonly Metadata.Command[] metadata = MetadataGenerator.GenerateCommandsMetadata();
        private readonly List<ParameterFieldData> parameterFields = new();
        private readonly List<ParameterFieldData> delayedAddFields = new();
        private bool hideParameters;
        private CommandLineView (int lineIndex, int lineIndent, VisualElement container)
            : base(lineIndex, lineIndent, container) { }

        public static ScriptLineView CreateDefault (int lineIndex, string commandId,
            VisualElement container, bool hideParameters)
        {
            var lineText = $"{Compiler.Syntax.CommandLine}{commandId}";
            var line = (CommandLine)Compiler.ParseLine(lineText);
            return CreateOrError(lineIndex, lineText, line, null, container, hideParameters);
        }

        public static ScriptLineView CreateOrError (int lineIndex, string lineText, CommandLine line,
            ErrorCollector errors, VisualElement container, bool hideParameters)
        {
            var cmdModel = line.Command;
            if (errors?.Count > 0) return Error(errors.FirstOrDefault()?.Message);

            var cmdId = cmdModel.Identifier.Text;
            var cmdMeta = metadata.FirstOrDefault(c =>
                c.Id.EqualsFastIgnoreCase(cmdId) ||
                (c.Alias?.EqualsFastIgnoreCase(cmdId) ?? false));
            if (cmdMeta is null) return Error($"Unknown command: `{cmdId}`");

            var nameLabel = new Label(cmdId.FirstToLower());
            nameLabel.name = "InputLabel";
            nameLabel.AddToClassList("Inlined");

            var lineView = new CommandLineView(lineIndex, line.Indent, container);
            lineView.Content.Add(nameLabel);
            lineView.CommandId = cmdId;
            lineView.hideParameters = hideParameters;

            foreach (var paramMeta in cmdMeta.Parameters)
                AddParamField(CreateParamFieldData(paramMeta));

            return lineView;

            ErrorLineView Error (string e) => new(lineIndex, line.Indent, lineText, container, cmdModel.Identifier, e);

            ParameterFieldData CreateParamFieldData (Metadata.Parameter paramMeta) => new() {
                Id = string.IsNullOrEmpty(paramMeta.Alias) ? paramMeta.Id.FirstToLower() : paramMeta.Alias,
                Value = GetValueFor(paramMeta),
                Nameless = paramMeta.Nameless
            };

            void AddParamField (ParameterFieldData data)
            {
                if (lineView.ShouldShowParameter(data))
                    lineView.AddParameterField(data);
                else lineView.delayedAddFields.Add(data);
            }

            string GetValueFor (Metadata.Parameter m)
            {
                var param = cmdModel.Parameters.FirstOrDefault(p => p.Nameless && m.Nameless || p.Identifier != null &&
                    (p.Identifier.Text.EqualsFastIgnoreCase(m.Id) || p.Identifier.Text.EqualsFastIgnoreCase(m.Alias)));
                if (param is null) return null;
                return Compiler.ScriptSerializer.Serialize(param.Value, new() { ParameterValue = true, NamelessParameterValue = param.Nameless });
            }
        }

        public override string GenerateLineText ()
        {
            var result = $"{GenerateLineIndent()}{Compiler.Syntax.CommandLine}{CommandId}";
            var parameters = new List<string>();
            foreach (var data in parameterFields)
                if (!string.IsNullOrWhiteSpace(data.Field.value))
                    if (data.Nameless) parameters.Insert(0, data.Field.value);
                    else if (data.Field.value?.EqualsFastIgnoreCase("true") ?? false) parameters.Add($"{data.Id}!");
                    else if (data.Field.value?.EqualsFastIgnoreCase("false") ?? false) parameters.Add($"!{data.Id}");
                    else parameters.Add($"{data.Id}:{data.Field.value}");
            if (parameters.Count > 0) result += $" {string.Join(" ", parameters)}";
            return result;
        }

        protected override void ApplyFocusedStyle ()
        {
            base.ApplyFocusedStyle();

            if (DragManipulator.Active) return;
            ShowUnAssignedNamedFields();
        }

        protected override void ApplyNotFocusedStyle ()
        {
            base.ApplyNotFocusedStyle();

            HideUnAssignedNamedFields();
        }

        protected override void ApplyHoveredStyle ()
        {
            base.ApplyHoveredStyle();

            if (DragManipulator.Active) return;
            ShowUnAssignedNamedFields();
        }

        protected override void ApplyNotHoveredStyle ()
        {
            base.ApplyNotHoveredStyle();

            if (FocusedLine == this) return;
            HideUnAssignedNamedFields();
        }

        private void AddParameterField (ParameterFieldData data)
        {
            data.Field = new(data.Nameless ? "" : data.Id, data.Value ?? "");
            if (!data.Nameless) data.Field.AddToClassList("NamedParameterLabel");
            parameterFields.Add(data);
            if (ShouldShowParameter(data)) Content.Add(data.Field);
        }

        private bool ShouldShowParameter (ParameterFieldData data)
        {
            return !hideParameters || data.Nameless || !string.IsNullOrEmpty(data.Value);
        }

        private void ShowUnAssignedNamedFields ()
        {
            if (!hideParameters) return;

            // Add un-assigned fields in case they weren't added on init.
            if (delayedAddFields.Count > 0)
            {
                foreach (var data in delayedAddFields)
                    AddParameterField(data);
                delayedAddFields.Clear();
            }

            foreach (var data in parameterFields)
                if (!Content.Contains(data.Field))
                    Content.Add(data.Field);
        }

        private void HideUnAssignedNamedFields ()
        {
            if (!hideParameters) return;

            foreach (var data in parameterFields)
                if (!string.IsNullOrEmpty(data.Field.label)
                    && string.IsNullOrWhiteSpace(data.Field.value)
                    && Content.Contains(data.Field))
                    Content.Remove(data.Field);
        }
    }
}
