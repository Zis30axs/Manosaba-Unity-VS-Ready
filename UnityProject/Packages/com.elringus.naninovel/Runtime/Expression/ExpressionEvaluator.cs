using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Naninovel.Expression;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Allows parsing and evaluating script expressions.
    /// </summary>
    public static class ExpressionEvaluator
    {
        private static readonly List<ParseDiagnostic> errors = new();
        private static ILookup<string, ExpressionFunction> fnByName;
        private static Parser parser;
        private static Evaluator evaluator;

        public static void Initialize ()
        {
            parser = new(new() {
                Syntax = Compiler.Syntax,
                HandleDiagnostic = errors.Add
            });
            evaluator = new(new() {
                ResolveVariable = ResolveVariable,
                ResolveFunction = ResolveFunction
            });
            fnByName = ExpressionFunctions.Resolve()
                .ToLookup(fn => fn.Id, comparer: StringComparer.OrdinalIgnoreCase);
        }

        public static TResult Evaluate<TResult> (string text, Action<string> onError = null)
        {
            return Evaluate(text, onError).GetValue<TResult>();
        }

        public static bool TryEvaluate<TResult> (string text, out TResult result, Action<string> onError = null)
        {
            result = default;
            try { return (result = Evaluate<TResult>(text, onError)) != null; }
            catch { return false; }
        }

        public static IOperand Evaluate (string text, Action<string> onError = null)
        {
            EnsureInitialized();

            if (string.IsNullOrWhiteSpace(text))
            {
                onError?.Invoke("Expression is missing.");
                return default;
            }

            errors.Clear();

            if (!parser.TryParse(text, out var exp))
            {
                onError?.Invoke($"Failed to parse '{text}' expression: {errors.FirstOrDefault()}");
                return default;
            }

            try { return evaluator.Evaluate(exp); }
            catch (Expression.Error err)
            {
                onError?.Invoke($"Failed to evaluate '{text}' expression: {err.Message}");
                return default;
            }
        }

        public static IOperand Evaluate (IExpression exp, Action<string> onError = null)
        {
            EnsureInitialized();

            errors.Clear();

            try { return evaluator.Evaluate(exp); }
            catch (Expression.Error err)
            {
                onError?.Invoke($"Failed to evaluate expression: {err.Message}");
                return default;
            }
        }

        public static void ParseAssignments (string text, IList<Assignment> assignments, Action<string> onError = null)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                onError?.Invoke("Expression is missing.");
                return;
            }

            errors.Clear();

            if (!parser.TryParseAssignments(text, assignments))
            {
                onError?.Invoke($"Failed to parse '{text}' assignment expression: {errors.FirstOrDefault()}");
                return;
            }
        }

        private static void EnsureInitialized ()
        {
            if (parser is null) Initialize();
            Debug.Assert(parser != null && evaluator != null && fnByName != null);
        }

        private static IOperand ResolveVariable (string name)
        {
            if (name.StartsWith(Compiler.ScriptConstantPrefix, StringComparison.OrdinalIgnoreCase))
            {
                var docs = Engine.GetServiceOrErr<ITextManager>();
                var managedTextValue = docs.GetRecordValue(name, ManagedTextPaths.ScriptConstants);
                if (string.IsNullOrEmpty(managedTextValue))
                {
                    Engine.Warn($"Missing '{name}' script constant. Make sure associated record exists in '{ManagedTextPaths.ScriptConstants}' managed text document.");
                    managedTextValue = $"{{{name}}}";
                }
                return new Expression.String(managedTextValue);
            }

            var vars = Engine.GetServiceOrErr<ICustomVariableManager>();
            if (!vars.VariableExists(name))
            {
                Engine.Warn($"Custom variable '{name}' is not initialized, but its value is requested in a script expression. " +
                            "Make sure to initialize variables with '@set' command or via 'Custom Variables' configuration menu before using them.");
                return new Expression.String("");
            }

            var value = vars.GetVariableValue(name);
            if (value.Type == CustomVariableValueType.String) return new Expression.String(value.String);
            if (value.Type == CustomVariableValueType.Boolean) return new Expression.Boolean(value.Boolean);
            return new Numeric(value.Number);
        }

        private static IOperand ResolveFunction (string name, IReadOnlyList<IOperand> args)
        {
            var methodArgs = args.Select(p => p.GetValue()).ToArray();
            var value = InvokeMethod(name, methodArgs, true) ??
                        InvokeMethod(name, methodArgs, false) ??
                        throw new Error($"Requested '{name}' expression function is not found.");
            return ValueToOperand(value);
        }

        private static object InvokeMethod (string name, object[] args, bool strictType)
        {
            if (!fnByName.Contains(name)) return null;

            foreach (var fn in fnByName[name])
            {
                var argsInfo = fn.Method.GetParameters();

                // Handle functions with single 'params' argument.
                if (argsInfo.Length == 1 && argsInfo[0].IsDefined(typeof(ParamArrayAttribute)) &&
                    args.All(p => IsCompatible(p, argsInfo[0].ParameterType.GetElementType())))
                {
                    var elementType = argsInfo[0].ParameterType.GetElementType();
                    for (int i = 0; i < args.Length; i++)
                        args[i] = ConvertValue(args[i], elementType);
                    var elements = Array.CreateInstance(elementType, args.Length);
                    Array.Copy(args, elements, args.Length);
                    return fn.Method.Invoke(null, new object[] { elements });
                }

                // Check argument count equality.
                if (argsInfo.Length != args.Length) continue;

                // Check argument type and order equality.
                var paramTypeCheckPassed = true;
                for (int i = 0; i < argsInfo.Length; i++)
                    if (!IsCompatible(args[i], argsInfo[i].ParameterType))
                    {
                        paramTypeCheckPassed = false;
                        break;
                    }
                    else args[i] = ConvertValue(args[i], argsInfo[i].ParameterType);
                if (!paramTypeCheckPassed) continue;

                return fn.Method.Invoke(null, args);
            }

            return null;

            bool IsCompatible (object actual, Type expected)
            {
                if (strictType)
                {
                    var actualType = actual.GetType();
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    if (actualType == typeof(double) && (double)actual == Convert.ToInt32(actual))
                        actualType = typeof(int);
                    return actualType == expected;
                }
                try { return Convert.ChangeType(actual, expected).GetType() == expected; }
                catch { return false; }
            }

            object ConvertValue (object actual, Type expected)
            {
                if (actual.GetType() == expected) return actual;
                return Convert.ChangeType(actual, expected);
            }
        }

        private static IOperand ValueToOperand (object value)
        {
            if (value is string str) return new Expression.String(str);
            if (value is bool boolean) return new Expression.Boolean(boolean);
            return new Numeric(Convert.ToDouble(value));
        }
    }
}
