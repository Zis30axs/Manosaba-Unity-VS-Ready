using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Naninovel
{
    /// <inheritdoc cref="IInputManager"/>
    [InitializeAtRuntime]
    public class InputManager : IStatefulService<GameStateMap>, IInputManager
    {
        [Serializable]
        public class GameState
        {
            public bool ProcessInput;
            public List<string> DisabledSamplers;
        }

        public event Action<InputMode> OnInputModeChanged;

        public virtual InputConfiguration Configuration { get; }
        public virtual bool ProcessInput { get; set; }
        public virtual InputMode InputMode { get => inputMode; set => ChangeInputMode(value); }

        private readonly Dictionary<string, InputSampler> samplersMap = new(StringComparer.Ordinal);
        private readonly Dictionary<IManagedUI, string[]> blockingUIs = new();
        private readonly HashSet<string> blockedSamplers = new();
        private readonly CancellationTokenSource sampleCTS = new();
        private readonly InputModeDetector inputModeDetector;
        private InputMode inputMode;
        private GameObject gameObject;

        public InputManager (InputConfiguration config)
        {
            Configuration = config;
            inputModeDetector = new(this);
        }

        public virtual async UniTask InitializeService ()
        {
            foreach (var binding in Configuration.Bindings)
            {
                var sampler = new InputSampler(Configuration, binding, null, this);
                samplersMap[binding.Name] = sampler;
            }

            gameObject = Engine.CreateObject(nameof(InputManager));

            if (Configuration.SpawnEventSystem)
            {
                if (Configuration.CustomEventSystem)
                    await Engine.Instantiate(Configuration.CustomEventSystem, parent: gameObject.transform);
                else gameObject.AddComponent<EventSystem>();
            }

            if (Configuration.SpawnInputModule)
            {
                if (Configuration.CustomInputModule)
                    await Engine.Instantiate(Configuration.CustomInputModule, parent: gameObject.transform);
                else
                {
                    #if ENABLE_INPUT_SYSTEM && INPUT_SYSTEM_AVAILABLE
                    var inputModule = gameObject.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();
                    inputModule.AssignDefaultActions();
                    #else
                    var inputModule = gameObject.AddComponent<StandaloneInputModule>();
                    #endif
                    await AsyncUtils.WaitEndOfFrame();
                    inputModule.enabled = false; // Otherwise it stops processing UI events when using new input system.
                    inputModule.enabled = true;
                }
            }

            ChangeInputMode(GetDefaultInputMode());
            if (Configuration.DetectInputMode)
                inputModeDetector.Start();

            SampleInput(sampleCTS.Token).Forget();
        }

        public virtual void ResetService () { }

        public virtual void DestroyService ()
        {
            sampleCTS?.Cancel();
            sampleCTS?.Dispose();
            inputModeDetector?.Dispose();
            samplersMap.Clear(); // Otherwise dev console leak event subs between play sessions.
            ObjectUtils.DestroyOrImmediate(gameObject);
        }

        public virtual void SaveServiceState (GameStateMap stateMap)
        {
            var state = new GameState {
                ProcessInput = ProcessInput,
                DisabledSamplers = samplersMap.Where(kv => !kv.Value.Enabled).Select(kv => kv.Key).ToList()
            };
            stateMap.SetState(state);
        }

        public virtual UniTask LoadServiceState (GameStateMap stateMap)
        {
            var state = stateMap.GetState<GameState>();
            if (state is null) return UniTask.CompletedTask;

            ProcessInput = state.ProcessInput;

            foreach (var kv in samplersMap)
                kv.Value.Enabled = !state.DisabledSamplers?.Contains(kv.Key) ?? true;

            return UniTask.CompletedTask;
        }

        public virtual IInputSampler GetSampler (string bindingName)
        {
            return samplersMap.GetValueOrDefault(bindingName);
        }

        public virtual void AddBlockingUI (IManagedUI ui, params string[] allowedSamplers)
        {
            if (!blockingUIs.TryAdd(ui, allowedSamplers)) return;
            ui.OnVisibilityChanged += HandleBlockingUIVisibilityChanged;
            HandleBlockingUIVisibilityChanged(ui.Visible);
        }

        public virtual void RemoveBlockingUI (IManagedUI ui)
        {
            if (!blockingUIs.ContainsKey(ui)) return;
            blockingUIs.Remove(ui);
            ui.OnVisibilityChanged -= HandleBlockingUIVisibilityChanged;
            HandleBlockingUIVisibilityChanged(ui.Visible);
        }

        public virtual bool IsSampling (string bindingName)
        {
            if (!ProcessInput) return false;
            if (!samplersMap.TryGetValue(bindingName, out var sampler)) return false;
            return sampler.Enabled && (!blockedSamplers.Contains(bindingName) || sampler.Binding.AlwaysProcess);
        }

        protected virtual void ChangeInputMode (InputMode mode)
        {
            inputMode = mode;
            OnInputModeChanged?.Invoke(mode);
        }

        protected virtual InputMode GetDefaultInputMode ()
        {
            if (Application.isConsolePlatform) return InputMode.Gamepad;
            if (Application.isMobilePlatform) return InputMode.Touch;
            return InputMode.MouseAndKeyboard;
        }

        protected virtual void HandleBlockingUIVisibilityChanged (bool visible)
        {
            // If any of the blocking UIs are visible, all the samplers should be blocked,
            // except ones that are explicitly allowed by ALL the visible blocking UIs.

            // 1. Find the allowed samplers first; start with clearing the set.
            blockedSamplers.Clear();
            // 2. Store all the existing samplers.
            blockedSamplers.UnionWith(samplersMap.Keys);
            // 3. Remove samplers that are not allowed by any of the visible blocking UIs.
            foreach (var kv in blockingUIs)
                if (kv.Key.Visible)
                    blockedSamplers.IntersectWith(kv.Value);
            // 4. This will filter-out the samplers contained in both collections,
            // effectively storing only the non-allowed (blocked) ones in the set.
            blockedSamplers.SymmetricExceptWith(samplersMap.Keys);
        }

        protected virtual async UniTaskVoid SampleInput (CancellationToken cancellationToken)
        {
            while (Application.isPlaying)
            {
                // It's important to sample early; eg, when sampling later and close button
                // of a blocking UI (eg, backlog) is pressed with enter key, the UI will un-block
                // before the sampling is performed, causing an unexpected continue input activation.
                await UniTask.Yield(PlayerLoopTiming.EarlyUpdate);
                if (cancellationToken.IsCancellationRequested) return;

                if (!ProcessInput) continue;

                foreach (var kv in samplersMap)
                    if (IsSampling(kv.Key))
                        kv.Value.SampleInput();
            }
        }
    }
}
