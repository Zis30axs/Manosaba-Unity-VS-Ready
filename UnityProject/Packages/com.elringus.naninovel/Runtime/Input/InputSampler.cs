using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Naninovel
{
    /// <inheritdoc cref="IInputSampler"/>
    public class InputSampler : IInputSampler
    {
        public event Action OnStart;
        public event Action OnEnd;
        public event Action<float> OnChange;

        public virtual InputBinding Binding { get; }
        public virtual bool Enabled { get; set; } = true;
        public virtual bool Active => Value != 0;
        public virtual float Value { get; private set; }
        public virtual bool StartedDuringFrame => Active && Engine.Time.FrameCount == lastActiveFrame;
        public virtual bool EndedDuringFrame => !Active && Engine.Time.FrameCount == lastActiveFrame;

        // ReSharper disable once NotAccessedField.Local (used with new input)
        private readonly IInputManager inputManager;
        private readonly InputConfiguration config;
        private readonly HashSet<GameObject> objectTriggers;
        private readonly Stack<InputInterceptRequest> intercepts = new();
        private CancellationTokenSource onNextCTS;

        private int lastActiveFrame;

        // Touch detection for old input.
        #pragma warning disable CS0414, CS0169
        private float lastTouchTime;
        private bool readyForNextTap;
        private Vector2 lastTouchBeganPosition;
        #pragma warning restore CS0414

        #if ENABLE_INPUT_SYSTEM && INPUT_SYSTEM_AVAILABLE
        private UnityEngine.InputSystem.InputAction inputAction;
        #endif

        /// <param name="config">Input manager configuration asset.</param>
        /// <param name="binding">Binding to trigger input.</param>
        /// <param name="objectTriggers">Objects to trigger input.</param>
        public InputSampler (InputConfiguration config, InputBinding binding,
            IEnumerable<GameObject> objectTriggers, IInputManager inputManager)
        {
            Binding = binding;
            this.config = config;
            this.objectTriggers = objectTriggers != null ? new(objectTriggers) : new HashSet<GameObject>();
            this.inputManager = inputManager;
            InitializeInputAction();
        }

        public virtual void AddObjectTrigger (GameObject obj) => objectTriggers.Add(obj);

        public virtual void RemoveObjectTrigger (GameObject obj) => objectTriggers.Remove(obj);

        public virtual CancellationToken GetNext ()
        {
            onNextCTS ??= new();
            return onNextCTS.Token;
        }

        public virtual CancellationToken InterceptNext (CancellationToken token)
        {
            var cts = new CancellationTokenSource();
            intercepts.Push(new(cts, token));
            return cts.Token;
        }

        public virtual void Activate (float value) => SetInputValue(value);

        /// <summary>
        /// Performs the sampling, updating the input status; expected to be invoked on each render loop update.
        /// </summary>
        public virtual void SampleInput ()
        {
            if (!Enabled) return;

            #if ENABLE_LEGACY_INPUT_MANAGER
            if (config.ProcessLegacyBindings && Binding.Keys?.Count > 0)
                SampleKeys();

            if (config.ProcessLegacyBindings && Binding.Axes?.Count > 0)
                SampleAxes();

            if (config.ProcessLegacyBindings && IsTouchSupported() && Binding.Swipes?.Count > 0)
                SampleSwipes();

            if (config.ProcessLegacyBindings && objectTriggers.Count > 0 && IsTriggered())
                SampleObjectTriggers();

            void SampleKeys ()
            {
                foreach (var key in Binding.Keys)
                {
                    if (Input.GetKeyDown(key)) SetInputValue(1);
                    if (Input.GetKeyUp(key)) SetInputValue(0);
                }
            }

            void SampleAxes ()
            {
                var maxValue = 0f;
                foreach (var axis in Binding.Axes)
                {
                    var axisValue = axis.Sample();
                    if (Mathf.Abs(axisValue) > Mathf.Abs(maxValue))
                        maxValue = axisValue;
                }
                if (!Mathf.Approximately(maxValue, Value))
                    SetInputValue(maxValue);
            }

            void SampleSwipes ()
            {
                var swipeRegistered = false;
                foreach (var swipe in Binding.Swipes)
                    if (swipe.Sample())
                    {
                        swipeRegistered = true;
                        break;
                    }
                if (swipeRegistered != Active) SetInputValue(swipeRegistered ? 1 : 0);
            }

            bool IsTriggered () => IsTouchedLegacy() || Input.touchCount == 0 && Input.GetMouseButtonDown(0);

            bool IsTouchedLegacy ()
            {
                if (!IsTouchSupported() || Input.touchCount == 0) return false;

                var touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    RegisterTouchBegan(touch.position);
                    return false;
                }
                return touch.phase == TouchPhase.Ended && RegisterTouchEnded(touch.position);
            }

            bool IsTouchSupported () => Input.touchSupported || Application.isEditor;

            void RegisterTouchBegan (Vector2 position)
            {
                lastTouchBeganPosition = position;
            }

            bool RegisterTouchEnded (Vector2 position)
            {
                var cooldown = Engine.Time.UnscaledTime - lastTouchTime <= config.TouchFrequencyLimit;
                if (cooldown) return false;

                var distance = Vector2.Distance(position, lastTouchBeganPosition);
                var withinDistanceLimit = distance < config.TouchDistanceLimit;
                if (!withinDistanceLimit) return false;

                readyForNextTap = false;
                lastTouchTime = Engine.Time.UnscaledTime;
                return true;
            }
            #endif

            #if ENABLE_INPUT_SYSTEM && INPUT_SYSTEM_AVAILABLE
            if (objectTriggers.Count > 0 && (Touched() || Clicked())) SampleObjectTriggers();
            bool Touched () => UnityEngine.InputSystem.Touchscreen.current?.primaryTouch.press.wasPressedThisFrame ?? false;
            bool Clicked () => UnityEngine.InputSystem.Mouse.current?.leftButton.wasPressedThisFrame ?? false;
            #endif
        }

        protected virtual void SampleObjectTriggers ()
        {
            if (!EventSystem.current) throw new Error("Failed to find event system. Make sure `Spawn Event System` is enabled in input configuration or manually spawn an event system before initializing Naninovel.");
            var hoveredObject = EventUtils.GetHoveredGameObject();
            if (hoveredObject && objectTriggers.Contains(hoveredObject))
                if (!hoveredObject.TryGetComponent<IInputTrigger>(out var trigger) || trigger.CanTriggerInput())
                    ActivateInputDelayed().Forget();

            // Delay to prevent button down event from propagating to any UI that may get shown after continue
            // input activation (eg, when next command is @showUI), which may result in a button activation.
            async UniTaskVoid ActivateInputDelayed ()
            {
                await UniTask.DelayFrame(1);
                // Check if it's not the same frame of the initial activation to prevent concurrent activations.
                if (Engine.Time.FrameCount > lastActiveFrame + 1)
                    SetInputValue(1f);
            }
        }

        protected void InitializeInputAction ()
        {
            #if ENABLE_INPUT_SYSTEM && INPUT_SYSTEM_AVAILABLE
            if (!config.InputActions) return;
            inputAction = config.InputActions.FindActionMap("Naninovel")?.FindAction(Binding.Name);
            if (inputAction is null) return;
            inputAction.Enable();
            inputAction.performed += HandlePerformed;
            inputAction.canceled += HandleCanceled;

            void HandlePerformed (UnityEngine.InputSystem.InputAction.CallbackContext _)
            {
                if (inputManager.IsSampling(Binding.Name))
                    SetInputValue(inputAction.ReadValue<float>());
            }

            void HandleCanceled (UnityEngine.InputSystem.InputAction.CallbackContext _)
            {
                if (inputManager.IsSampling(Binding.Name))
                    SetInputValue(0);
            }
            #endif
        }

        protected virtual void SetInputValue (float value)
        {
            if (!Mathf.Approximately(Value, value))
                OnChange?.Invoke(value);

            Value = value;
            lastActiveFrame = Engine.Time.FrameCount;

            while (intercepts.Count > 0)
                if (HandleInterceptRequest(intercepts.Pop()))
                    return;

            if (Active)
            {
                onNextCTS?.Cancel();
                onNextCTS?.Dispose();
                onNextCTS = null;
            }

            if (Active) OnStart?.Invoke();
            else OnEnd?.Invoke();
        }

        protected virtual bool HandleInterceptRequest (InputInterceptRequest request)
        {
            var ownerStillExpectsInterception = !request.HandlerToken.IsCancellationRequested;
            if (ownerStillExpectsInterception) request.CTS.Cancel();
            request.CTS.Dispose();
            return ownerStillExpectsInterception;
        }
    }
}
