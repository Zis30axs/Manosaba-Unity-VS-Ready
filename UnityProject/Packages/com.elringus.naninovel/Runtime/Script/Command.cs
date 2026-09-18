using System;
using System.Linq;
using System.Threading;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents a <see cref="Script"/> command.
    /// </summary>
    [Serializable]
    public abstract class Command
    {
        /// <summary>
        /// Implementing <see cref="Command"/> will be included in localization scripts.
        /// </summary>
        public interface ILocalizable { }

        /// <summary>
        /// Implementing <see cref="Command"/> is able to preload resources it uses.
        /// </summary>
        public interface IPreloadable
        {
            /// <summary>
            /// Preloads the resources used by the command; invoked by <see cref="IScriptLoader"/>
            /// when preloading the script resources, in accordance with <see cref="ResourcePolicy"/>.
            /// </summary>
            /// <remarks>
            /// Make sure to only preload resources associated with static parameter values, as
            /// dynamic values may change at any point while the preloaded script is playing.
            /// Resources associated with the dynamic values have to be loaded just before executing
            /// the command, using the resolved value, which is propagated to the underlying consumers.
            /// </remarks>
            UniTask PreloadResources ();
            /// <summary>
            /// Releases the preloaded resources used by the command.
            /// </summary>
            void ReleaseResources ();
        }

        /// <summary>
        /// Implementing <see cref="Command"/> hosts an underlying block of commands associated
        /// with it (nested under) via indentation. The host command is able to control which
        /// or whether nested commands are executed and in which order.
        /// </summary>
        public interface INestedHost
        {
            /// <summary>
            /// Given specified playlist contains the command and is being played at specified index,
            /// returns next index inside the list to play.
            /// </summary>
            int GetNextPlaybackIndex (ScriptPlaylist playlist, int playedIndex);
        }

        /// <summary>
        /// Assigns an alias name for <see cref="Command"/>.
        /// Aliases can be used instead of the command IDs (type names) to reference commands in naninovel script.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class, Inherited = false)]
        public sealed class CommandAliasAttribute : Attribute
        {
            public string Alias { get; }

            public CommandAliasAttribute (string alias)
            {
                Alias = alias;
            }
        }

        /// <summary>
        /// Registers the field as a required <see cref="ICommandParameter"/> logging error when it's not supplied in naninovel scripts.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field)]
        public sealed class RequiredParameterAttribute : Attribute { }

        /// <summary>
        /// Assigns an alias name to a <see cref="ICommandParameter"/> field allowing it to be used instead of the field name in naninovel scripts.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field)]
        public sealed class ParameterAliasAttribute : Attribute
        {
            public string Alias { get; }

            /// <param name="alias">Alias name of the parameter.</param>
            public ParameterAliasAttribute (string alias)
            {
                Alias = alias;
            }
        }

        /// <summary>
        /// Associates a default value with the <see cref="ICommandParameter"/> field.
        /// Intended for external tools to access metadata; ignored at runtime.
        /// </summary>
        [AttributeUsage(AttributeTargets.Field)]
        public sealed class ParameterDefaultValueAttribute : Attribute
        {
            // ReSharper disable once UnusedAutoPropertyAccessor.Global
            public string Value { get; }

            public ParameterDefaultValueAttribute (string value)
            {
                Value = value;
            }
        }

        /// <summary>
        /// Namespace for all the built-in commands implementations.
        /// </summary>
        public const string DefaultNamespace = "Naninovel.Commands";
        /// <summary>
        /// Use this alias to specify a nameless command parameter.
        /// </summary>
        public const string NamelessParameterAlias = "";
        /// <summary>
        /// Contains all the available <see cref="Command"/> types in the application domain, 
        /// indexed by command alias (if available) or implementing type name. Keys are case-insensitive.
        /// </summary>
        public static LiteralMap<Type> CommandTypes => commandTypesCache ??= GetCommandTypes();

        /// <summary>
        /// In case the command belongs to a <see cref="Script"/> asset, represents position inside the script.
        /// </summary>
        public PlaybackSpot PlaybackSpot { get => playbackSpot; set => playbackSpot = value; }
        /// <summary>
        /// Indentation level of the line to which this command belong.
        /// </summary>
        public int Indent { get => indent; set => indent = value; }
        /// <summary>
        /// Whether this command should be executed, as per <see cref="ConditionalExpression"/>.
        /// </summary>
        public virtual bool ShouldExecute => string.IsNullOrEmpty(ConditionalExpression) || ExpressionEvaluator.Evaluate<bool>(ConditionalExpression, Err);

        [Doc("A boolean [script expression](/guide/script-expressions), controlling whether this command should execute.")]
        [ParameterAlias("if"), ConditionContext]
        public StringParameter ConditionalExpression;

        [SerializeField] private PlaybackSpot playbackSpot = PlaybackSpot.Invalid;
        [SerializeField] private int indent;

        private static LiteralMap<Type> commandTypesCache;

        /// <summary>
        /// Attempts to find a <see cref="Command"/> type based on the specified command alias or type name.
        /// </summary>
        public static Type ResolveCommandType (string commandId)
        {
            if (string.IsNullOrEmpty(commandId))
                return null;

            // First, try to resolve by key.
            CommandTypes.TryGetValue(commandId, out var result);
            // If not found, look by type name (in case type name was requested for a command with a defined alias).
            return result ?? CommandTypes.Values.FirstOrDefault(commandType => commandType.Name.EqualsFastIgnoreCase(commandId));
        }

        /// <summary>
        /// Executes the command asynchronously.
        /// </summary>
        /// <param name="token">Controls the asynchronous execution of the command; see <see href="https://naninovel.com/guide/custom-commands#asynctoken"/> for more info.</param>
        public abstract UniTask Execute (AsyncToken token = default);

        /// <summary>
        /// Logs an informational message to the console; will include script path and line number of the command.
        /// </summary>
        public virtual void Log (string message) => Engine.Log(message, PlaybackSpot);

        /// <summary>
        /// Logs a warning to the console; will include script path and line number of the command.
        /// </summary>
        public virtual void Warn (string message) => Engine.Warn(message, PlaybackSpot);

        /// <summary>
        /// Logs an error to the console; will include script path and line number of the command.
        /// </summary>
        public virtual void Err (string message) => Engine.Err(message, PlaybackSpot);

        /// <summary>
        /// Whether specified parameter has a value assigned.
        /// </summary>
        public static bool Assigned (ICommandParameter parameter) => parameter is not null && parameter.HasValue;

        /// <summary>
        /// Whether specified parameter has a value assigned and is dynamic (value may change at runtime).
        /// </summary>
        /// <remarks>
        /// Resources associated with dynamic parameters can't be resolved when preloading script, as their value may change
        /// while the script is playing; resources of such parameters have to be loaded just before the command is executed.
        /// </remarks>
        public static bool AssignedDynamic (ICommandParameter parameter) => Assigned(parameter) && parameter.DynamicValue;

        /// <summary>
        /// Whether specified parameter has a value assigned and is static (value is immutable at runtime).
        /// </summary>
        /// <remarks>
        /// Resources associated with static parameters can be resolved when preloading script, as their value won't change
        /// while the script is playing.
        /// </remarks>
        public static bool AssignedStatic (ICommandParameter parameter) => Assigned(parameter) && !parameter.DynamicValue;

        /// <summary>
        /// Preloads the resources associated with the specified static localizable text parameters.
        /// </summary>
        protected virtual UniTask PreloadStaticTextResources (params LocalizableTextParameter[] text)
        {
            return UniTask.WhenAll(text.Select(t => AssignedStatic(t)
                ? t.Value.Load(this) : UniTask.CompletedTask));
        }

        /// <summary>
        /// Releases the preloaded resources associated with the specified static localizable text parameters.
        /// </summary>
        protected virtual void ReleaseStaticTextResources (params LocalizableTextParameter[] text)
        {
            foreach (var t in text)
                if (AssignedStatic(t))
                    t.Value.Release(this);
        }

        /// <summary>
        /// Loads the resources associated with the specified dynamic localizable text parameters;
        /// dispose returned instance to release the resources.
        /// </summary>
        /// <remarks>
        /// It's safe to dispose the returned text holder on <see cref="Execute"/> context exit,
        /// as at that point the text is expected to be held by the underlying consumers, when necessary.
        /// </remarks>
        protected virtual async UniTask<IDisposable> LoadDynamicTextResources (params LocalizableTextParameter[] text)
        {
            await UniTask.WhenAll(text.Select(t => AssignedDynamic(t)
                ? t.Value.Load(this) : UniTask.CompletedTask));
            return new DeferredAction(() => {
                foreach (var t in text)
                    if (AssignedDynamic(t))
                        t.Value.Release(this);
            });
        }

        /// <summary>
        /// Loads the resources associated with the specified dynamic localizable text parameters;
        /// dispose returned instance to release the resources.
        /// </summary>
        protected virtual async UniTask<IDisposable> LoadDynamicTextResources (params (LocalizableTextParameter Parameter, LocalizableText ResolvedValue)[] text)
        {
            await UniTask.WhenAll(text.Select(t => AssignedDynamic(t.Parameter)
                ? t.ResolvedValue.Load(this) : UniTask.CompletedTask));
            return new DeferredAction(() => {
                foreach (var t in text)
                    if (AssignedDynamic(t.Parameter))
                        // ReSharper disable once PossiblyImpureMethodCallOnReadonlyVariable (false-positive)
                        t.ResolvedValue.Release(this);
            });
        }

        /// <summary>
        /// Executes specified function and either awaits or forgets the async result depending on the specified
        /// wait parameter or (when not assigned) <see cref="ScriptPlayerConfiguration.WaitByDefault"/> configuration.
        /// When the result is awaited, will as well merge continue input events to the completion token in case
        /// <see cref="ScriptPlayerConfiguration.CompleteOnContinue"/> is enabled. 
        /// </summary>
        /// <param name="wait">Command parameter controlling whether the function result has to be awaited.</param>
        /// <param name="token">Command execution token as specified in <see cref="Execute"/>.</param>
        /// <param name="fn">The async function which result is the subject of this method.</param>
        protected virtual async UniTask WaitOrForget (Func<AsyncToken, UniTask> fn, BooleanParameter wait, AsyncToken token)
        {
            if (Assigned(wait) ? wait.Value : Engine.GetConfiguration<ScriptPlayerConfiguration>().WaitByDefault)
            {
                using var _ = CompleteOnContinueWhenEnabled(ref token);
                await fn(token);
            }
            else fn(token).Forget();
        }

        /// <summary>
        /// Invokes <see cref="CompleteOnContinue"/> when <see cref="ScriptPlayerConfiguration.CompleteOnContinue"/> is enabled.
        /// Make sure to dispose returned object to prevent memory leaks.
        /// </summary>
        protected virtual IDisposable CompleteOnContinueWhenEnabled (ref AsyncToken token)
        {
            if (Engine.GetConfiguration<ScriptPlayerConfiguration>().CompleteOnContinue)
                return CompleteOnContinue(ref token);
            return new EmptyDisposable();
        }

        /// <summary>
        /// Merges continue input events to the completion token of the specified async token (in-place),
        /// so that <see cref="AsyncToken.Completed"/> triggers when player activates continue or skip inputs.
        /// Make sure to dispose returned object to prevent memory leaks.
        /// </summary>
        protected virtual IDisposable CompleteOnContinue (ref AsyncToken token)
        {
            var input = Engine.GetServiceOrErr<IInputManager>();
            if (input.GetContinue() is not { } continueInput) return new EmptyDisposable();
            var skipInput = input.GetSkip();
            var toggleSkipInput = input.GetToggleSkip();
            var continueInputCT = continueInput.GetNext();
            var skipInputCT = skipInput?.GetNext() ?? default;
            var toggleSkipInputCT = toggleSkipInput?.GetNext() ?? default;
            var completionAndContinueCTS = CancellationTokenSource.CreateLinkedTokenSource(token.CompletionToken, continueInputCT, skipInputCT, toggleSkipInputCT);
            token = new(token.CancellationToken, completionAndContinueCTS.Token);
            return completionAndContinueCTS;
        }

        private static LiteralMap<Type> GetCommandTypes ()
        {
            var result = new LiteralMap<Type>();
            var commandTypes = Engine.Types.Commands
                // Put built-in commands first, so they're overridden by custom commands with same aliases.
                .OrderByDescending(type => type.Namespace == DefaultNamespace);
            foreach (var commandType in commandTypes)
            {
                if (Compiler.Commands.TryGetValue(commandType.Name, out var locale) && !string.IsNullOrWhiteSpace(locale.Alias))
                {
                    result[locale.Alias] = commandType;
                    continue;
                }

                var commandKey = commandType.GetCustomAttributes(typeof(CommandAliasAttribute), false)
                    .FirstOrDefault() is CommandAliasAttribute tagAttribute && !string.IsNullOrEmpty(tagAttribute.Alias)
                    ? tagAttribute.Alias
                    : commandType.Name;
                result[commandKey] = commandType;
            }
            return result;
        }
    }
}
