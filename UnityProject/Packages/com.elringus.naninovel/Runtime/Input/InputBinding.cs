using System.Collections.Generic;
using UnityEngine;

namespace Naninovel
{
    [System.Serializable]
    public class InputBinding
    {
        [Tooltip("Name (ID) of the binding used to access it via the input manager.")]
        public string Name = string.Empty;
        [Tooltip("Whether to always process the binding, even when out of the game and in menus.")]
        public bool AlwaysProcess;
        [Tooltip("Keys that should trigger this binding.")]
        public List<KeyCode> Keys = new();
        [Tooltip("Axes that should trigger this binding.")]
        public List<InputAxisTrigger> Axes = new();
        [Tooltip("Swipes (touch screen) that should trigger this binding.")]
        public List<InputSwipeTrigger> Swipes = new();
    }
}
