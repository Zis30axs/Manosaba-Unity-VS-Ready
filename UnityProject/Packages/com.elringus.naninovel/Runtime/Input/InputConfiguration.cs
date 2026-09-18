using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Naninovel.InputNames;

namespace Naninovel
{
    [EditInProjectSettings]
    public class InputConfiguration : Configuration
    {
        [Tooltip("Whether to spawn an event system when initializing.")]
        public bool SpawnEventSystem = true;
        [Tooltip("A prefab with an `EventSystem` component to spawn for input processing. Will spawn a default one when not specified.")]
        public EventSystem CustomEventSystem;
        [Tooltip("Whether to spawn an input module when initializing.")]
        public bool SpawnInputModule = true;
        [Tooltip("A prefab with an `InputModule` component to spawn for input processing. Will spawn a default one when not specified.")]
        public BaseInputModule CustomInputModule;
        #if ENABLE_INPUT_SYSTEM && INPUT_SYSTEM_AVAILABLE
        [Tooltip("When Unity's new input system is installed, assign input actions asset here.\n\nTo map input actions to Naninovel's input bindings, create `Naninovel` action map and add actions with names equal to the binding names (found below under `Control Scheme` -> Bindings list).\n\nBe aware, that 2-dimensional (Vector2) axes are not supported.")]
        public UnityEngine.InputSystem.InputActionAsset InputActions;
        #endif
        [Tooltip("Whether to process legacy input bindings. Disable in case you're using Unity's new input system and don't want the legacy bindings to work in addition to input actions.")]
        public bool ProcessLegacyBindings = true;
        [Tooltip("Limits frequency of the registered touch inputs, in seconds. For legacy input only.")]
        public float TouchFrequencyLimit = .1f;
        [Tooltip("Limits distance of the registered touch inputs, in pixels. For legacy input only.")]
        public float TouchDistanceLimit = 25f;
        [Tooltip("Whether to change input mode when associated device is activated. Eg, switch to gamepad when any gamepad button is pressed and switch back to mouse when mouse button clicked. Requires new input system.")]
        public bool DetectInputMode = true;

        [Header("Control Scheme"), Tooltip("Bindings to process input for; find descriptions for each default input in the 'Input Processing' guide.")]
        public List<InputBinding> Bindings = new() {
            new() {
                Name = Submit,
                Keys = new() { KeyCode.Return, KeyCode.JoystickButton0 },
                AlwaysProcess = true
            },
            new() {
                Name = Cancel,
                Keys = new() { KeyCode.Escape, KeyCode.JoystickButton1 },
                AlwaysProcess = true
            },
            new() {
                Name = Delete,
                Keys = new() { KeyCode.Delete, KeyCode.JoystickButton7 },
                AlwaysProcess = true
            },
            new() {
                Name = NavigateX,
                AlwaysProcess = true
            },
            new() {
                Name = NavigateY,
                AlwaysProcess = true
            },
            new() {
                Name = ScrollY,
                Axes = new() { new() { AxisName = "Vertical", TriggerMode = InputAxisTriggerMode.Both } },
                AlwaysProcess = true
            },
            new() {
                Name = Page,
                AlwaysProcess = true
            },
            new() {
                Name = Tab,
                AlwaysProcess = true
            },
            new() {
                Name = Continue,
                Keys = new() { KeyCode.Return, KeyCode.KeypadEnter, KeyCode.JoystickButton0 },
                Axes = new() { new() { AxisName = "Mouse ScrollWheel", TriggerMode = InputAxisTriggerMode.Negative } },
                Swipes = new() { new() { Direction = InputSwipeDirection.Left } }
            },
            new() {
                Name = Pause,
                Keys = new() { KeyCode.Backspace, KeyCode.JoystickButton7 }
            },
            new() {
                Name = Skip,
                Keys = new() { KeyCode.LeftControl, KeyCode.RightControl, KeyCode.JoystickButton1 }
            },
            new() {
                Name = ToggleSkip,
                Keys = new() { KeyCode.Tab, KeyCode.JoystickButton9 }
            },
            new() {
                Name = SkipMovie,
                Keys = new() { KeyCode.Escape, KeyCode.JoystickButton1 }
            },
            new() {
                Name = AutoPlay,
                Keys = new() { KeyCode.A, KeyCode.JoystickButton2 }
            },
            new() {
                Name = ToggleUI,
                Keys = new() { KeyCode.Space, KeyCode.JoystickButton3 },
                Swipes = new() { new() { Direction = InputSwipeDirection.Down } }
            },
            new() {
                Name = ShowBacklog,
                Keys = new() { KeyCode.L, KeyCode.JoystickButton5 },
                Swipes = new() { new() { Direction = InputSwipeDirection.Up } }
            },
            new() {
                Name = Rollback,
                Keys = new() { KeyCode.JoystickButton4 },
                Axes = new() { new() { AxisName = "Mouse ScrollWheel", TriggerMode = InputAxisTriggerMode.Positive } },
                Swipes = new() { new() { Direction = InputSwipeDirection.Right } }
            },
            new() {
                Name = CameraLookX,
                Axes = new() {
                    new() { AxisName = "Horizontal", TriggerMode = InputAxisTriggerMode.Both },
                    new() { AxisName = "Mouse X", TriggerMode = InputAxisTriggerMode.Both }
                }
            },
            new() {
                Name = CameraLookY,
                Axes = new() {
                    new() { AxisName = "Vertical", TriggerMode = InputAxisTriggerMode.Both },
                    new() { AxisName = "Mouse Y", TriggerMode = InputAxisTriggerMode.Both }
                }
            },
            new() {
                Name = ToggleConsole,
                Keys = new() { KeyCode.BackQuote },
                AlwaysProcess = true
            }
        };
    }
}
