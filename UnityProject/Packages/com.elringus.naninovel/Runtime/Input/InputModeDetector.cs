using System;

namespace Naninovel
{
    /// <summary>
    /// Handles <see cref="InputMode"/> detection based on last active input device.
    /// </summary>
    public class InputModeDetector : IDisposable
    {
        // ReSharper disable once NotAccessedField.Local (used with new input)
        private readonly IInputManager input;

        public InputModeDetector (IInputManager input)
        {
            this.input = input;
        }

        public void Start ()
        {
            #if ENABLE_INPUT_SYSTEM && INPUT_SYSTEM_AVAILABLE
            UnityEngine.InputSystem.InputSystem.onEvent += HandleInputEvent;
            #endif
        }

        public void Stop ()
        {
            #if ENABLE_INPUT_SYSTEM && INPUT_SYSTEM_AVAILABLE
            UnityEngine.InputSystem.InputSystem.onEvent -= HandleInputEvent;
            #endif
        }

        public void Dispose () => Stop();

        #if ENABLE_INPUT_SYSTEM && INPUT_SYSTEM_AVAILABLE
        private void HandleInputEvent (UnityEngine.InputSystem.LowLevel.InputEventPtr ptr, UnityEngine.InputSystem.InputDevice device)
        {
            if (ShouldChangeToMouse(device)) input.InputMode = InputMode.MouseAndKeyboard;
            else if (ShouldChangeToKeyboard(device)) input.InputMode = InputMode.MouseAndKeyboard;
            else if (ShouldChangeToTouch(device)) input.InputMode = InputMode.Touch;
            else if (ShouldChangeToGamepad(device)) input.InputMode = InputMode.Gamepad;
        }

        private bool ShouldChangeToMouse (UnityEngine.InputSystem.InputDevice device)
        {
            if (input.InputMode == InputMode.MouseAndKeyboard) return false;
            return device is UnityEngine.InputSystem.Mouse mouse && (
                mouse.leftButton.isPressed ||
                mouse.middleButton.isPressed ||
                mouse.rightButton.isPressed
            );
        }

        private bool ShouldChangeToKeyboard (UnityEngine.InputSystem.InputDevice device)
        {
            if (input.InputMode == InputMode.MouseAndKeyboard) return false;
            return device is UnityEngine.InputSystem.Keyboard board && board.anyKey.isPressed;
        }

        private bool ShouldChangeToTouch (UnityEngine.InputSystem.InputDevice device)
        {
            if (input.InputMode == InputMode.Touch) return false;
            return device is UnityEngine.InputSystem.Touchscreen touch && touch.primaryTouch.isInProgress;
        }

        private bool ShouldChangeToGamepad (UnityEngine.InputSystem.InputDevice device)
        {
            if (input.InputMode == InputMode.Gamepad) return false;
            return device is UnityEngine.InputSystem.Gamepad pad && (
                pad.buttonNorth.isPressed ||
                pad.buttonEast.isPressed ||
                pad.buttonSouth.isPressed ||
                pad.buttonWest.isPressed ||
                pad.dpad.down.isPressed ||
                pad.dpad.up.isPressed ||
                pad.dpad.left.isPressed ||
                pad.dpad.right.isPressed ||
                pad.leftStick.down.isPressed ||
                pad.leftStick.up.isPressed ||
                pad.leftStick.left.isPressed ||
                pad.leftStick.right.isPressed ||
                pad.rightStick.down.isPressed ||
                pad.rightStick.up.isPressed ||
                pad.rightStick.left.isPressed ||
                pad.rightStick.right.isPressed ||
                pad.leftTrigger.isPressed ||
                pad.rightTrigger.isPressed ||
                pad.leftShoulder.isPressed ||
                pad.rightShoulder.isPressed ||
                pad.selectButton.isPressed ||
                pad.startButton.isPressed
            );
        }
        #endif
    }
}
