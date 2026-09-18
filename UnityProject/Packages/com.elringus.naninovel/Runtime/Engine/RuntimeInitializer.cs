using System;
using System.Collections.Generic;
using System.Linq;
using Naninovel.Async;
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Default engine initializer for runtime environment.
    /// </summary>
    public class RuntimeInitializer : MonoBehaviour
    {
        [SerializeField] private bool initializeOnAwake = true;

        private const string initPrefabName = "EngineInitializationUI";

        private static UniTaskCompletionSource initializeTCS;

        /// <summary>
        /// Invokes default engine initialization routine.
        /// </summary>
        /// <param name="configurationProvider">Configuration provider to use for engine initialization.</param>
        /// <param name="customInitializationData">Use to inject services without <see cref="InitializeAtRuntimeAttribute"/>.</param>
        /// <param name="time">Time service to use by the engine.</param>
        public static async UniTask Initialize (IConfigurationProvider configurationProvider = null,
            IEnumerable<ServiceInitializationData> customInitializationData = null, ITime time = null)
        {
            if (Engine.Initialized) return;
            if (initializeTCS != null)
            {
                await initializeTCS.Task;
                return;
            }

            initializeTCS = new();

            configurationProvider ??= new ProjectConfigurationProvider();
            var engineConfig = configurationProvider.GetConfiguration<EngineConfiguration>();

            UniTaskScheduler.UnobservedExceptionWriteLogType = engineConfig.AsyncExceptionLogType;

            var initializationUI = default(ScriptableUIBehaviour);
            if (engineConfig.ShowInitializationUI)
            {
                var initPrefab = engineConfig.CustomInitializationUI
                    ? engineConfig.CustomInitializationUI
                    : Engine.LoadInternalResource<ScriptableUIBehaviour>(initPrefabName);
                initializationUI = Instantiate(initPrefab);
                initializationUI.Show();
            }

            var initData = customInitializationData?.ToList() ?? new List<ServiceInitializationData>();
            var overridenTypes = initData.Where(d => d.Override != null).Select(d => d.Override).ToList();
            foreach (var type in Engine.Types.InitializeAtRuntime)
            {
                var initAttribute = Attribute.GetCustomAttribute(type, typeof(InitializeAtRuntimeAttribute), false) as InitializeAtRuntimeAttribute;
                if (initAttribute is null) throw new Error($"Failed to initialize '{type.FullName}' engine service: missing attribute.");
                initData.Add(new(type, initAttribute));
                if (initAttribute.Override != null)
                    overridenTypes.Add(initAttribute.Override);
            }
            initData = initData.Where(d => !overridenTypes.Contains(d.Type)).ToList(); // Exclude services overriden by user.

            bool IsService (Type t) => typeof(IEngineService).IsAssignableFrom(t);
            bool IsBehaviour (Type t) => typeof(IEngineBehaviour).IsAssignableFrom(t);
            bool IsConfig (Type t) => typeof(Configuration).IsAssignableFrom(t);

            // Order by initialization priority and then perform topological order to make sure ctor references initialized before they're used.
            // ReSharper disable once AccessToModifiedClosure (false positive: we're assigning result of the closure to the variable in question)
            IEnumerable<ServiceInitializationData> GetDependencies (ServiceInitializationData d) =>
                d.CtorArgs.Where(IsService).SelectMany(argType => initData.Where(dd => d != dd && argType.IsAssignableFrom(dd.Type)));
            initData = initData.OrderBy(d => d.Priority).TopologicalOrder(GetDependencies).ToList();

            var behaviour = RuntimeBehaviour.Create(engineConfig.SceneIndependent);
            var services = new List<IEngineService>();
            var ctorParams = new List<object>();
            foreach (var data in initData)
            {
                foreach (var argType in data.CtorArgs)
                    if (IsService(argType)) ctorParams.Add(services.First(s => argType.IsInstanceOfType(s)));
                    else if (IsBehaviour(argType)) ctorParams.Add(behaviour);
                    else if (IsConfig(argType)) ctorParams.Add(configurationProvider.GetConfiguration(argType));
                    else
                        throw new Error($"Only '{nameof(Configuration)}', '{nameof(IEngineBehaviour)}' and '{nameof(IEngineService)}' " +
                                        $"with an '{nameof(InitializeAtRuntimeAttribute)}' can be requested in an engine service constructor.");
                var service = Activator.CreateInstance(data.Type, ctorParams.ToArray()) as IEngineService;
                services.Add(service);
                ctorParams.Clear();
            }

            await Engine.Initialize(new() {
                Services = services,
                ConfigurationProvider = configurationProvider,
                Behaviour = behaviour,
                Time = time ?? new UnityTime()
            });

            if (!Engine.Initialized) // In case terminated in the midst of initialization.
            {
                if (initializationUI)
                    ObjectUtils.DestroyOrImmediate(initializationUI.gameObject);
                DisposeTCS();
                return;
            }

            ExpressionEvaluator.Initialize();

            if (initializationUI)
            {
                await initializationUI.ChangeVisibility(false, token: Engine.DestroyToken);
                ObjectUtils.DestroyOrImmediate(initializationUI.gameObject);
            }

            Engine.GetServiceOrErr<IInputManager>().ProcessInput = true;

            await PlayIntroMovie();
            await PlayInitScript();

            DisposeTCS();
        }

        private static async UniTask PlayIntroMovie ()
        {
            var cfg = Engine.GetConfiguration<MoviesConfiguration>();
            if (!cfg.PlayIntroMovie) return;
            // Keep duration = 0 to prevent user from activating input (eg, showing pause UI) while movie UI is fading-out.
            await new PlayMovie { MovieName = cfg.IntroMovieName, Duration = 0 }.Execute(Engine.DestroyToken);
        }

        private static async UniTask PlayInitScript ()
        {
            var scriptPath = Engine.GetConfiguration<ScriptsConfiguration>().InitializationScript;
            if (string.IsNullOrEmpty(scriptPath)) return;

            var player = Engine.GetServiceOrErr<IScriptPlayer>();
            if (player.PlayedScript) // Usually happen when init script is playing in parallel or after title.
            {
                Engine.Warn($"Initialization script won't be played because '{player.PlayedScript.Path}' is currently playing. " +
                            "In case conflicting with title script, disable 'Visible On Awake' in title UI " +
                            "and show it at the end of initialization script with @showUI command.");
                return;
            }

            using (new InteractionBlocker())
                await player.LoadAndPlay(scriptPath);
            while (player.Playing)
                await AsyncUtils.WaitEndOfFrame(Engine.DestroyToken);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void DisposeTCS ()
        {
            initializeTCS?.TrySetResult();
            initializeTCS = null;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void OnApplicationLoaded ()
        {
            var engineConfig = Configuration.GetOrDefault<EngineConfiguration>();
            if (engineConfig.InitializeOnApplicationLoad)
                Initialize().Forget();
        }

        private async void Awake ()
        {
            if (initializeOnAwake)
                await Initialize();
        }
    }
}
