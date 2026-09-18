using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Naninovel.Parsing;

namespace Naninovel
{
    public class CommandParser
    {
        protected virtual IErrorHandler ErrorHandler { get; }
        protected virtual Parsing.Command CommandModel => commandModel;
        protected virtual IReadOnlyList<Parameter> ParameterModels => CommandModel.Parameters;
        protected virtual Command Command => command;
        protected virtual Type CommandType => commandType;
        protected virtual string CommandId => commandModel.Identifier;

        private readonly HashSet<string> supportedParamIds = new(StringComparer.OrdinalIgnoreCase);
        private readonly MixedValueParser mixedParser;
        private readonly StringBuilder builder = new();
        private Parsing.Command commandModel;
        private Command command;
        private Type commandType;
        private PlaybackSpot spot;
        private bool transient;
        private CommandLocalization locale;

        public CommandParser (ITextIdentifier identifier, IErrorHandler errorHandler = null)
        {
            mixedParser = new(identifier);
            ErrorHandler = errorHandler;
        }

        public virtual Command Parse (CommandParseArgs args)
        {
            ResetState(args);
            if (!TryGetCommandType(out commandType)) return null;
            if (!TryCreateCommand(out command)) return null;
            Compiler.Commands.TryGetValue(commandType.Name, out locale);
            Command.PlaybackSpot = spot;
            Command.Indent = args.Indent;
            AssignParameters();
            return command;
        }

        protected virtual void ResetState (CommandParseArgs args)
        {
            supportedParamIds.Clear();
            commandModel = args.Model;
            command = null;
            commandType = null;
            spot = args.PlaybackSpot;
            transient = args.Transient;
            locale = default;
        }

        protected virtual void AddError (string error)
        {
            ErrorHandler?.HandleError(new(error, 0, 0));
        }

        protected virtual bool TryGetCommandType (out Type commandType)
        {
            commandType = Command.ResolveCommandType(CommandId);
            if (commandType is null) AddError($"Command '{CommandId}' is not found.");
            return commandType != null;
        }

        protected virtual bool TryCreateCommand (out Command command)
        {
            command = Activator.CreateInstance(CommandType) as Command;
            if (command is null) AddError($"Failed to create instance of '{CommandType}' command.");
            return command != null;
        }

        protected virtual void AssignParameters ()
        {
            var fields = CommandType.GetFields(BindingFlags.Public | BindingFlags.Instance)
                .Where(f => typeof(ICommandParameter).IsAssignableFrom(f.FieldType)).ToArray();
            supportedParamIds.UnionWith(fields.Select(f => f.Name));
            foreach (var field in fields)
                AssignField(field);
            CheckUnsupportedParameters();
        }

        protected virtual void AssignField (FieldInfo field)
        {
            if (!TryFindModelFor(field, out var model)) return;
            if (!TryCreateParameter(field, out var parameter)) return;
            var raw = mixedParser.Parse(model.Value, ShouldHashPlainText(field));
            parameter.AssignRaw(raw, spot, out var errors);
            if (!string.IsNullOrEmpty(errors)) AddError(errors);
            field.SetValue(Command, parameter);
        }

        protected virtual bool TryFindModelFor (FieldInfo field, out Parameter model)
        {
            var l10nAlias = locale.Parameters?.FirstOrDefault(p => p.Id == field.Name).Alias;
            var required = field.GetCustomAttribute<Command.RequiredParameterAttribute>() != null;
            var alias = string.IsNullOrWhiteSpace(l10nAlias) ? field.GetCustomAttribute<Command.ParameterAliasAttribute>()?.Alias : l10nAlias;
            if (alias != null) supportedParamIds.Add(alias);

            var id = alias != null && ParameterModels.Any(m => m.Nameless && alias == "" || (m.Identifier?.Text.EqualsFastIgnoreCase(alias) ?? false)) ? alias : field.Name;
            model = ParameterModels.FirstOrDefault(m => m.Nameless && id == "" || (m.Identifier?.Text.EqualsFastIgnoreCase(id) ?? false));
            if (model is null && required) AddError($"Command '{CommandId}' is missing '{id}' parameter.");
            return model != null;
        }

        protected virtual bool TryCreateParameter (FieldInfo field, out ICommandParameter parameter)
        {
            parameter = Activator.CreateInstance(field.FieldType) as ICommandParameter;
            if (parameter is null) AddError($"Failed to create instance of '{field.FieldType}' parameter for '{CommandId}' command.");
            return parameter != null;
        }

        protected virtual void CheckUnsupportedParameters ()
        {
            foreach (var model in ParameterModels)
                if (!supportedParamIds.Contains(model.Identifier ?? ""))
                    AddError($"Command '{CommandId}' has an unsupported '{GetId(model)}' parameter.");
            string GetId (Parameter p) => p.Nameless ? "nameless" : p.Identifier?.Text;
        }

        protected virtual bool ShouldHashPlainText (FieldInfo field)
        {
            return !transient && field.FieldType == typeof(LocalizableTextParameter);
        }

        protected virtual string Interpolate (IEnumerable<IValueComponent> mixed)
        {
            builder.Clear();
            foreach (var value in mixed)
                if (value is PlainText text) builder.Append(text);
                else if (value is Parsing.Expression expression)
                {
                    builder.Append(Compiler.Syntax.ExpressionOpen[0]);
                    builder.Append(expression.Body);
                    builder.Append(Compiler.Syntax.ExpressionClose[0]);
                }
            return builder.ToString();
        }
    }
}
