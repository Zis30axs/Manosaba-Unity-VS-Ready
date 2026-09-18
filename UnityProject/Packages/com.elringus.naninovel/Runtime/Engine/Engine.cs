using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Scripting;

// Make sure none of the assembly types are stripped when building with IL2CPP.
[assembly: AlwaysLinkAssembly, Preserve]

namespace Naninovel
{
    /// <summary>
    /// Class responsible for management of systems critical to the engine.
    /// </summary>
    public static class Engine
    {
        /// <summary>
        /// Invoked when the engine initialization is started.
        /// </summary>
        public static event Action OnInitializationStarted;
        /// <summary>
        /// Invoked when the engine initialization is finished.
        /// </summary>
        public static event Action OnInitializationFinished;
        /// <summary>
        /// Invoked when the engine initialization progress is changed (in 0.0 to 1.0 range).
        /// </summary>
        public static event Action<float> OnInitializationProgress;
        /// <summary>
        /// Invoked when the engine is destroyed.
        /// </summary>
        public static event Action OnDestroyed;

        /// <inheritdoc cref="EngineTypes"/>
        /// <remarks>It's safe to access this property when engine is not initialized.</remarks>
        public static EngineTypes Types { get; } = TypeCache.Load();
        /// <summary>
        /// Configuration object used to initialize the engine.
        /// </summary>
        public static EngineConfiguration Configuration { get; private set; }
        /// <summary>
        /// Proxy <see cref="MonoBehaviour"/> used by the engine.
        /// </summary>
        public static IEngineBehaviour Behaviour { get; private set; }
        /// <summary>
        /// Proxy time service used by the engine.
        /// </summary>
        public static ITime Time { get; private set; } = new UnityTime();
        /// <summary>
        /// Initialized engine services.
        /// </summary>
        public static IReadOnlyCollection<IEngineService> Services => services;
        /// <summary>
        /// Composition root, containing all the engine-related game objects.
        /// </summary>
        public static GameObject RootObject => Behaviour.GetRootObject();
        /// <summary>
        /// Whether the engine is initialized and ready.
        /// </summary>
        public static bool Initialized => initializeTCS != null && initializeTCS.Task.IsCompleted;
        /// <summary>
        /// Whether the engine is currently being initialized.
        /// </summary>
        public static bool Initializing => initializeTCS != null && !initializeTCS.Task.IsCompleted;
        /// <summary>
        /// Token which is canceled when the engine is destroyed.
        /// </summary>
        public static CancellationToken DestroyToken => destroyCTS?.Token ?? new CancellationToken(true);

        /// <summary>
        /// Path (relative to the Unity project root) to the transient data directory; throws in build.
        /// </summary>
        public static string EditorTransientRoot
        {
            get => Application.isEditor ? editorTransientRoot : throw new Error("Transient directory is editor-only.");
            set => editorTransientRoot = value;
        }

        private static readonly List<UnityEngine.Object> objects = new();
        private static readonly List<IEngineService> services = new();
        private static readonly Dictionary<Type, IEngineService> cachedGetServiceResults = new();
        private static readonly List<Func<UniTask>> preInitializationTasks = new();
        private static readonly List<Func<UniTask>> postInitializationTasks = new();
        private static IConfigurationProvider configurationProvider;
        private static UniTaskCompletionSource initializeTCS;
        private static CancellationTokenSource destroyCTS;
        private static ILogger logger = new UnityLogger();
        private static string editorTransientRoot;

        /// <summary>
        /// Adds an async function delegate to invoke before the engine initialization.
        /// Added delegates will be invoked and awaited in order before starting the initialization.
        /// </summary>
        public static void AddPreInitializationTask (Func<UniTask> task) => preInitializationTasks.Add(task);

        /// <summary>
        /// Removes a delegate added via <see cref="AddPreInitializationTask(Func{UniTask})"/>.
        /// </summary>
        public static void RemovePreInitializationTask (Func<UniTask> task) => preInitializationTasks.Remove(task);

        /// <summary>
        /// Adds an async function delegate to invoke after the engine initialization.
        /// Added delegates will be invoked and awaited in order before finishing the initialization.
        /// </summary>
        public static void AddPostInitializationTask (Func<UniTask> task) => postInitializationTasks.Add(task);

        /// <summary>
        /// Removes a delegate added via <see cref="AddPostInitializationTask(Func{UniTask})"/>.
        /// </summary>
        public static void RemovePostInitializationTask (Func<UniTask> task) => postInitializationTasks.Remove(task);

        /// <summary>
        /// Initializes the engine behaviour and services.
        /// </summary>
        /// <param name="params">Parameters required for engine initialization.</param>
        public static async UniTask Initialize (EngineParams @params)
        {
            if (Initialized) return;
            if (Initializing)
            {
                await initializeTCS.Task;
                return;
            }

            destroyCTS = new();
            initializeTCS = new();
            OnInitializationStarted?.Invoke();

            for (int i = preInitializationTasks.Count - 1; i >= 0; i--)
            {
                OnInitializationProgress?.Invoke(.25f * (1 - i / (float)preInitializationTasks.Count));
                await preInitializationTasks[i]();
                if (!Initializing) return; // In case initialization process was terminated (eg, exited play mode).
            }

            configurationProvider = @params.ConfigurationProvider;
            Configuration = GetConfiguration<EngineConfiguration>();
            Behaviour = @params.Behaviour;
            Behaviour.OnBehaviourDestroy += Destroy;
            Time = @params.Time;

            objects.Clear();
            services.ReplaceWith(@params.Services);

            for (var i = 0; i < services.Count; i++)
            {
                OnInitializationProgress?.Invoke(.25f + .5f * (i / (float)services.Count));
                await services[i].InitializeService();
                if (!Initializing) return;
            }

            for (int i = 0; i < postInitializationTasks.Count; i++)
            {
                OnInitializationProgress?.Invoke(.75f + .25f * (i / (float)postInitializationTasks.Count));
                await postInitializationTasks[i]();
                if (!Initializing) return;
            }

            initializeTCS?.TrySetResult();
            OnInitializationFinished?.Invoke();
        }

        /// <summary>
        /// Resets state of all the engine services.
        /// </summary>
        public static void Reset () => services.ForEach(s => s.ResetService());

        /// <summary>
        /// Resets state of engine services.
        /// </summary>
        /// <param name="exclude">Type of the engine services (interfaces) to exclude from reset.</param>
        public static void Reset (IReadOnlyCollection<Type> exclude)
        {
            if (services.Count == 0) return;

            foreach (var service in services)
                if (exclude is null || exclude.Count == 0 || !exclude.Any(t => t.IsInstanceOfType(service)))
                    service.ResetService();
        }

        /// <inheritdoc cref="Reset()"/>
        public static void Reset (params Type[] exclude) => Reset((IReadOnlyCollection<Type>)exclude);

        /// <summary>
        /// Deconstructs all the engine services and stops the behaviour.
        /// </summary>
        public static void Destroy ()
        {
            initializeTCS = null;

            services.ForEach(s => s.DestroyService());
            services.Clear();
            cachedGetServiceResults.Clear();

            if (Behaviour != null)
            {
                Behaviour.OnBehaviourDestroy -= Destroy;
                Behaviour.Destroy();
                Behaviour = null;
            }

            foreach (var obj in objects)
            {
                if (!obj) continue;
                var go = obj as GameObject ?? (obj as Component)?.gameObject;
                ObjectUtils.DestroyOrImmediate(go);
            }
            objects.Clear();

            Configuration = null;
            configurationProvider = null;

            OnDestroyed?.Invoke();

            destroyCTS?.Cancel();
            destroyCTS?.Dispose();
            destroyCTS = null;
        }

        /// <summary>
        /// Attempts to provide a <see cref="Naninovel.Configuration"/> object of the specified type 
        /// via <see cref="IConfigurationProvider"/> used to initialize the engine.
        /// </summary>
        /// <typeparam name="T">Type of the requested configuration object.</typeparam>
        public static T GetConfiguration<T> () where T : Configuration => GetConfiguration(typeof(T)) as T;

        /// <summary>
        /// Attempts to provide a <see cref="Naninovel.Configuration"/> object of the specified type 
        /// via <see cref="IConfigurationProvider"/> used to initialize the engine.
        /// </summary>
        /// <param name="type">Type of the requested configuration object.</param>
        public static Configuration GetConfiguration (Type type)
        {
            if (configurationProvider is null)
                throw new Error($"Failed to provide '{type.Name}' configuration object: Configuration provider is not available or the engine is not initialized.");

            return configurationProvider.GetConfiguration(type);
        }

        /// <summary>
        /// Attempts to resolve an <see cref="IEngineService"/> of the specified type.
        /// </summary>
        /// <remarks>
        /// Results per requested types are cached, so it's fine to use this method frequently.
        /// </remarks>
        /// <typeparam name="TService">Type of the requested service.</typeparam>
        /// <returns>First matching service or null, when no matches found.</returns>
        [CanBeNull]
        public static TService GetService<TService> ()
            where TService : class, IEngineService
        {
            return GetService(typeof(TService)) as TService;
        }

        /// <inheritdoc cref="GetService{TService}()"/>
        /// <returns>Whether the service was found.</returns>
        public static bool TryGetService<TService> (out TService result)
            where TService : class, IEngineService
        {
            result = GetService<TService>();
            return result != null;
        }

        /// <summary>
        /// Resolves an <see cref="IEngineService"/> of the specified type; throws when fails.
        /// </summary>
        /// <remarks>
        /// Results per requested types are cached, so it's fine to use this method frequently.
        /// </remarks>
        /// <typeparam name="TService">Type of the requested service.</typeparam>
        /// <returns>First matching service.</returns>
        /// <exception cref="Error">Thrown when service of requested type is not found or engine is not initialized.</exception>
        public static TService GetServiceOrErr<TService> ()
            where TService : class, IEngineService
        {
            return GetService<TService>()
                   ?? throw new Error($"Failed to get '{typeof(TService).FullName}' engine service: not found or engine is not initialized.");
        }

        /// <inheritdoc cref="GetService{TService}()"/>
        /// <param name="serviceType">Type of the service to resolve.</param>
        [CanBeNull]
        public static IEngineService GetService (Type serviceType)
        {
            if (cachedGetServiceResults.TryGetValue(serviceType, out var cachedResult))
                return cachedResult;
            var result = services.FirstOrDefault(serviceType.IsInstanceOfType);
            if (result is null) return null;
            cachedGetServiceResults[serviceType] = result;
            return result;
        }

        /// <summary>
        /// Attempts to resolve first matching <see cref="IEngineService"/> object from
        /// the services list using specified <paramref name="predicate"/>.
        /// </summary>
        /// <typeparam name="TService">Type of the requested service.</typeparam>
        /// <param name="predicate">Additional filter to apply when looking for a match.</param>
        /// <returns>First matching service or null, when no matches found.</returns>
        [CanBeNull]
        public static TService FindService<TService> (Predicate<TService> predicate)
            where TService : class, IEngineService
        {
            foreach (var service in services)
                if (service is TService engineService && predicate(engineService))
                    return engineService;
            return null;
        }

        /// <inheritdoc cref="FindService{TService}"/>
        /// <param name="state">Use instead of capturing outside context to prevent allocations.</param>
        [CanBeNull]
        public static TService FindService<TService, TState> (TState state, Func<TService, TState, bool> predicate)
            where TService : class, IEngineService
        {
            foreach (var service in services)
                if (service is TService engineService && predicate(engineService, state))
                    return engineService;
            return null;
        }

        /// <summary>
        /// Collects all the matching <see cref="IEngineService"/> to the specified collection.
        /// </summary>
        /// <typeparam name="TService">Type of the requested services.</typeparam>
        public static void FindAllServices<TService> (ICollection<TService> services)
            where TService : class, IEngineService
        {
            foreach (var service in Engine.services)
                if (service is TService s)
                    services.Add(s);
        }

        /// <inheritdoc cref="FindAllServices{TService}(System.Collections.Generic.ICollection{TService})"/>
        /// <param name="predicate">Additional filter to apply when looking for a match.</param>
        public static void FindAllServices<TService> (ICollection<TService> services,
            Predicate<TService> predicate) where TService : class, IEngineService
        {
            foreach (var service in Engine.services)
                if (service is TService s && predicate(s))
                    services.Add(s);
        }

        /// <inheritdoc cref="FindAllServices{TService}(System.Collections.Generic.ICollection{TService})"/>
        /// <param name="state">Use instead of capturing outside context to prevent allocations.</param>
        /// <param name="predicate">Additional filter to apply when looking for a match.</param>
        public static void FindAllServices<TService, TState> (ICollection<TService> services,
            TState state, Func<TService, TState, bool> predicate) where TService : class, IEngineService
        {
            foreach (var service in Engine.services)
                if (service is TService s && predicate(s, state))
                    services.Add(s);
        }

        /// <summary>
        /// Invokes <see cref="UnityEngine.Object.InstantiateAsync{T}(T)"/> and adds the object as child of the engine object.
        /// </summary>
        /// <param name="prototype">Prototype of the object to instantiate.</param>
        /// <param name="name">Name to assign for the instantiated object. Will use name of the prototype when not specified.</param>
        /// <param name="layer">Layer to assign for the instantiated object. When not specified and override layer is enabled in the engine configuration, will assign the layer specified in the configuration.</param>
        /// <param name="parent">When specified, will make the instantiated object child of the transform.</param>
        public static async UniTask<T> Instantiate<T> (T prototype, string name = default, int? layer = default,
            Transform parent = default, AsyncToken token = default) where T : UnityEngine.Object
        {
            if (Behaviour is null)
                throw new Error($"Failed to instantiate '{name ?? prototype.name}': engine is not ready. " +
                                $"Make sure you're not attempting to instantiate and object inside an engine service constructor (use '{nameof(IEngineService.InitializeService)}' method instead).");

            var newObj = default(T);
            if (Configuration.AsyncInstantiation)
            {
                var handle = parent ? UnityEngine.Object.InstantiateAsync(prototype, parent) : UnityEngine.Object.InstantiateAsync(prototype);
                while (!handle.isDone) await AsyncUtils.WaitEndOfFrame(token);
                if (handle.Result is null || !handle.Result[0]) throw new OperationCanceledException();
                newObj = handle.Result[0];
            }
            else newObj = parent ? UnityEngine.Object.Instantiate(prototype, parent) : UnityEngine.Object.Instantiate(prototype);

            var gameObj = newObj as GameObject ?? (newObj as Component)?.gameObject;
            if (!parent) Behaviour.AddChildObject(gameObj);

            if (!string.IsNullOrEmpty(name)) newObj.name = name;

            if (layer.HasValue) gameObj.ForEachDescendant(obj => obj.layer = layer.Value);
            else if (Configuration.OverrideObjectsLayer) gameObj.ForEachDescendant(obj => obj.layer = Configuration.ObjectsLayer);

            objects.Add(newObj);

            return newObj;
        }

        /// <summary>
        /// Creates a new <see cref="GameObject"/>, making it a child of the engine object and (optionally) adding specified components.
        /// </summary>
        /// <param name="name">Name to assign for the instantiated object. Will use a default name when not specified.</param>
        /// <param name="layer">Layer to assign for the instantiated object. When not specified and override layer is enabled in the engine configuration, will assign the layer specified in the configuration.</param>
        /// <param name="parent">When specified, will make the created object child of the transform.</param>
        /// <param name="components">Components to add on the created object.</param>
        public static GameObject CreateObject (string name = default, int? layer = default,
            Transform parent = default, params Type[] components)
        {
            if (Behaviour is null)
                throw new Error($"Failed to create '{name ?? string.Empty}' object: engine is not ready. " +
                                $"Make sure you're not attempting to create and object inside an engine service constructor (use '{nameof(IEngineService.InitializeService)}' method instead).");

            var objName = name ?? "NaninovelObject";
            GameObject newObj;
            if (components != null) newObj = new(objName, components);
            else newObj = new(objName);

            if (parent) newObj.transform.SetParent(parent);
            else Behaviour.AddChildObject(newObj);

            if (layer.HasValue) newObj.ForEachDescendant(obj => obj.layer = layer.Value);
            else if (Configuration.OverrideObjectsLayer) newObj.ForEachDescendant(obj => obj.layer = Configuration.ObjectsLayer);

            objects.Add(newObj);

            return newObj;
        }

        /// <summary>
        /// Creates a new <see cref="GameObject"/>, making it a child of the engine object and adding specified component type.
        /// </summary>
        /// <param name="name">Name to assign for the instantiated object. Will use a default name when not specified.</param>
        /// <param name="layer">Layer to assign for the instantiated object. When not specified and override layer is enabled in the engine configuration, will assign the layer specified in the configuration.</param>
        /// <param name="parent">When specified, will make the instantiated object child of the transform.</param>
        public static T CreateObject<T> (string name = default, int? layer = default, Transform parent = default) where T : Component
        {
            if (Behaviour is null)
                throw new Error($"Failed to create '{name ?? string.Empty}' object of type '{typeof(T).Name}': engine is not ready. " +
                                $"Make sure you're not attempting to create and object inside an engine service constructor (use '{nameof(IEngineService.InitializeService)}' method instead).");

            var newObj = new GameObject(name ?? typeof(T).Name);

            if (parent) newObj.transform.SetParent(parent);
            else Behaviour.AddChildObject(newObj);

            if (layer.HasValue) newObj.ForEachDescendant(obj => obj.layer = layer.Value);
            else if (Configuration.OverrideObjectsLayer) newObj.ForEachDescendant(obj => obj.layer = Configuration.ObjectsLayer);

            objects.Add(newObj);

            return newObj.AddComponent<T>();
        }

        /// <summary>
        /// Attempts to find an engine object with the specified name.
        /// Returns null if not found.
        /// </summary>
        public static GameObject FindObject (string name)
        {
            foreach (var obj in objects)
                if (obj && obj is GameObject go && go.name == name)
                    return go;
            return null;
        }

        /// <summary>
        /// Attempts to <see cref="Resources.Load{T}"/> using the specified path, prefixed with "Naninovel".
        /// In case the resource is not found, will raise an exception mentioning the package was probably modified or is corrupted.
        /// </summary>
        /// <param name="relativePath">Relative (to "Naninovel") path to the resource.</param>
        /// <typeparam name="T">Type of the resource to load.</typeparam>
        public static T LoadInternalResource<T> (string relativePath) where T : UnityEngine.Object
        {
            var fullPath = $"Naninovel/{relativePath}";
            var asset = Resources.Load<T>(fullPath);
            if (!asset) throw new Error($"Failed to load an internal Naninovel asset stored at 'Naninovel/Resources/{fullPath}'. The Naninovel package was probably modified or is corrupted. Try removing 'Naninovel' folder from the project and re-importing the package from the Asset Store. Remember, that you shouldn't modify contents of the 'Naninovel' folder: be it adding, removing, editing or moving anything inside the folder.");
            return asset;
        }

        /// <summary>
        /// Injects custom logging handler.
        /// </summary>
        public static void UseLogger (ILogger logger)
        {
            Engine.logger = logger;
        }

        /// <summary>
        /// Logs an information message with the current logger.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="spot">When specified, will prepend associated script path and line index to the message.</param>
        public static void Log (string message, PlaybackSpot? spot = null) => logger.Log(FormatMessage(message, spot));

        /// <summary>
        /// Logs a warning with the current logger.
        /// </summary>
        /// <inheritdoc cref="Log"/>
        public static void Warn (string message, PlaybackSpot? spot = null) => logger.Warn(FormatMessage(message, spot));

        /// <summary>
        /// Logs an error message with the current logger.
        /// </summary>
        /// <inheritdoc cref="Log"/>
        public static void Err (string message, PlaybackSpot? spot = null) => logger.Err(FormatMessage(message, spot));

        internal static string FormatMessage (string message, PlaybackSpot? spot)
        {
            if (!spot.HasValue) return message;
            var line = spot.Value.InlineIndex >= 0 ? $"{spot.Value.LineNumber}.{spot.Value.InlineIndex}" : $"{spot.Value.LineNumber}";
            return $"Naninovel script '{spot.Value.ScriptPath}' at line #{line}: {message}";
        }

        [RuntimeInitializeOnLoadMethod]
        private static void CheckUnityVersion ()
        {
            if (!Application.isEditor) return;

            var version = Application.unityVersion;
            if (!ParseUtils.TryInvariantInt(version.GetBefore("."), out var major) ||
                !ParseUtils.TryInvariantInt(version.GetBetween("."), out var minor) ||
                !ParseUtils.TryInvariantInt(new(version.GetAfter(".").TakeWhile(char.IsDigit).ToArray()), out var patch))
                throw new Error($"Failed to parse '{version}' Unity version.");

            if (major < 2022) Err("Minimum supported Unity version is 2022.3.61.");
            if (major == 2022) CheckMinorAndPatch(3, 61); // https://github.com/naninovel/docs/issues/168
            if (major == 6000) CheckMinorAndPatch(0, 45); // https://github.com/naninovel/docs/issues/168

            void CheckMinorAndPatch (int minMinor, int minPatch)
            {
                if (minor < minMinor || minor == minMinor && patch < minPatch)
                    Err($"Minimum supported Unity release in {major} stream is {major}.{minMinor}.{minPatch}.");
            }
        }
    }
}
