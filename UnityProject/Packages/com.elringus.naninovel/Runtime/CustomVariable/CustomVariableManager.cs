using System;
using System.Collections.Generic;
using System.Linq;
using Naninovel.Commands;

namespace Naninovel
{
    /// <inheritdoc cref="ICustomVariableManager"/>
    /// <remarks>Initialization order lowered, as other services implicitly use custom variables (eg, via <see cref="ExpressionEvaluator"/>).</remarks>
    [InitializeAtRuntime(int.MinValue + 1), Goto.DontReset]
    public class CustomVariableManager : IStatefulService<GameStateMap>, IStatefulService<GlobalStateMap>, ICustomVariableManager
    {
        [Serializable]
        public class GlobalState
        {
            public CustomVariable[] GlobalVariables;
        }

        [Serializable]
        public class GameState
        {
            public CustomVariable[] LocalVariables;
        }

        public event Action<CustomVariableUpdatedArgs> OnVariableUpdated;

        public virtual CustomVariablesConfiguration Configuration { get; }
        public virtual IReadOnlyCollection<CustomVariable> Variables => variablesByName.Values;

        private readonly Dictionary<string, CustomVariable> variablesByName = new();
        private readonly Dictionary<string, string> predefineByName;
        private readonly ITextManager docs;

        public CustomVariableManager (CustomVariablesConfiguration config, ITextManager docs)
        {
            Configuration = config;
            predefineByName = config.PredefinedVariables.ToDictionary(p => p.Name, p => p.Expression);
            this.docs = docs;
            // Initialize local variables after engine init, so that managed text script vars are available.
            Engine.AddPostInitializationTask(InitializeLocalVariables);
        }

        public virtual async UniTask InitializeService ()
        {
            await docs.DocumentLoader.Load(ManagedTextPaths.ScriptConstants, this);
        }

        public virtual void ResetService ()
        {
            ResetAllVariables(CustomVariableScope.Local);
        }

        public virtual void DestroyService ()
        {
            docs.DocumentLoader.ReleaseAll(this);
            Engine.RemovePostInitializationTask(InitializeLocalVariables);
        }

        public virtual void SaveServiceState (GlobalStateMap stateMap)
        {
            var state = new GlobalState {
                GlobalVariables = variablesByName.Values.Where(v => v.Scope == CustomVariableScope.Global).ToArray()
            };
            stateMap.SetState(state);
        }

        public virtual UniTask LoadServiceState (GlobalStateMap stateMap)
        {
            ResetAllVariables(CustomVariableScope.Global);

            var state = stateMap.GetState<GlobalState>();
            if (state is null) return UniTask.CompletedTask;

            foreach (var var in state.GlobalVariables)
                variablesByName[var.Name] = var;
            return UniTask.CompletedTask;
        }

        public virtual void SaveServiceState (GameStateMap stateMap)
        {
            var state = new GameState {
                LocalVariables = variablesByName.Values.Where(v => v.Scope == CustomVariableScope.Local).ToArray()
            };
            stateMap.SetState(state);
        }

        public virtual UniTask LoadServiceState (GameStateMap stateMap)
        {
            ResetAllVariables(CustomVariableScope.Local);

            var state = stateMap.GetState<GameState>();
            if (state is null) return UniTask.CompletedTask;

            foreach (var var in state.LocalVariables)
                variablesByName[var.Name] = var;
            return UniTask.CompletedTask;
        }

        public virtual bool VariableExists (string name)
        {
            return variablesByName.ContainsKey(name);
        }

        public virtual CustomVariableValue GetVariableValue (string name)
        {
            return GetVariable(name).Value;
        }

        public virtual void SetVariableValue (string name, CustomVariableValue value)
        {
            var scope = CustomVariablesConfiguration.HasGlobalPrefix(name) ? CustomVariableScope.Global : CustomVariableScope.Local;

            if (!VariableExists(name))
            {
                variablesByName[name] = new(name, scope, value);
                OnVariableUpdated?.Invoke(new(name, value, null));
                return;
            }

            var initialValue = GetVariable(name).Value;
            variablesByName[name] = new(name, scope, value);
            if (initialValue != value)
                OnVariableUpdated?.Invoke(new(name, value, initialValue));
        }

        public virtual void ResetVariable (string name)
        {
            var var = GetVariable(name);
            variablesByName.Remove(name);
            if (predefineByName.TryGetValue(name, out var predefine))
                ResetPredefinedVariable(name, predefine);
            else OnVariableUpdated?.Invoke(new(name, null, var.Value));
        }

        public virtual void ResetAllVariables (CustomVariableScope? scope = null)
        {
            foreach (var var in Variables.ToArray())
                if (scope == null || var.Scope == scope)
                    ResetVariable(var.Name);
            foreach (var kv in predefineByName)
                if (ShouldCreatePredefined(kv.Key))
                    ResetPredefinedVariable(kv.Key, kv.Value);

            bool ShouldCreatePredefined (string name)
            {
                if (VariableExists(name)) return false;
                return scope == null ||
                       scope == CustomVariableScope.Global && CustomVariablesConfiguration.HasGlobalPrefix(name) ||
                       scope == CustomVariableScope.Local && !CustomVariablesConfiguration.HasGlobalPrefix(name);
            }
        }

        protected virtual UniTask InitializeLocalVariables ()
        {
            ResetAllVariables(CustomVariableScope.Local);
            return UniTask.CompletedTask;
        }

        protected virtual CustomVariable GetVariable (string name)
        {
            if (!variablesByName.TryGetValue(name, out var var))
                throw new Error($"Custom variable '{name}' doesn't exist.");
            return var;
        }

        protected virtual void ResetPredefinedVariable (string name, string predefine)
        {
            var operand = ExpressionEvaluator.Evaluate(predefine,
                e => Engine.Warn($"Failed to pre-define '{name}' variable with `{predefine}` expression: {e}"));
            if (operand is Expression.String str) SetVariableValue(name, new(str.Value));
            else if (operand is Expression.Boolean boo) SetVariableValue(name, new(boo.Value));
            else SetVariableValue(name, new((float)(double)operand.GetValue()));
        }
    }
}
