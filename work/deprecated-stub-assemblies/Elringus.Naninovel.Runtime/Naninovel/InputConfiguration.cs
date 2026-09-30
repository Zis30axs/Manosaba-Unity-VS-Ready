using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Naninovel
{
    [Token(Token = "0x2000369")]
    [EditInProjectSettings]
    public class InputConfiguration : Configuration
    {
    	[Token(Token = "0x4000C9A")]
    	[FieldOffset(Offset = "0x18")]
    	[Tooltip("Whether to spawn an event system when initializing.")]
    	public bool SpawnEventSystem;

    	[Token(Token = "0x4000C9B")]
    	[FieldOffset(Offset = "0x20")]
    	[Tooltip("A prefab with an `EventSystem` component to spawn for input processing. Will spawn a default one when not specified.")]
    	public EventSystem CustomEventSystem;

    	[Token(Token = "0x4000C9C")]
    	[FieldOffset(Offset = "0x28")]
    	[Tooltip("Whether to spawn an input module when initializing.")]
    	public bool SpawnInputModule;

    	[Token(Token = "0x4000C9D")]
    	[FieldOffset(Offset = "0x30")]
    	[Tooltip("A prefab with an `InputModule` component to spawn for input processing. Will spawn a default one when not specified.")]
    	public BaseInputModule CustomInputModule;

    	[Token(Token = "0x4000C9E")]
    	[FieldOffset(Offset = "0x38")]
    	[Tooltip("When Unity's new input system is installed, assign input actions asset here.\n\nTo map input actions to Naninovel's input bindings, create `Naninovel` action map and add actions with names equal to the binding names (found below under `Control Scheme` -> Bindings list).\n\nBe aware, that 2-dimensional (Vector2) axes are not supported.")]
    	public InputActionAsset InputActions;

    	[Token(Token = "0x4000C9F")]
    	[FieldOffset(Offset = "0x40")]
    	[Tooltip("Whether to process legacy input bindings. Disable in case you're using Unity's new input system and don't want the legacy bindings to work in addition to input actions.")]
    	public bool ProcessLegacyBindings;

    	[Token(Token = "0x4000CA0")]
    	[FieldOffset(Offset = "0x44")]
    	[Tooltip("Limits frequency of the registered touch inputs, in seconds. For legacy input only.")]
    	public float TouchFrequencyLimit;

    	[Token(Token = "0x4000CA1")]
    	[FieldOffset(Offset = "0x48")]
    	[Tooltip("Limits distance of the registered touch inputs, in pixels. For legacy input only.")]
    	public float TouchDistanceLimit;

    	[Token(Token = "0x4000CA2")]
    	[FieldOffset(Offset = "0x4C")]
    	[Tooltip("Whether to change input mode when associated device is activated. Eg, switch to gamepad when any gamepad button is pressed and switch back to mouse when mouse button clicked. Requires new input system.")]
    	public bool DetectInputMode;

    	[Token(Token = "0x4000CA3")]
    	[FieldOffset(Offset = "0x50")]
    	[Header("Control Scheme")]
    	[Tooltip("Bindings to process input for; find descriptions for each default input in the 'Input Processing' guide.")]
    	public List<InputBinding> Bindings;

    	[Token(Token = "0x6001064")]
    	[Address(RVA = "0x5732A0", Offset = "0x571CA0", VA = "0x1805732A0")]
    	public InputConfiguration()
    	{
    	}
    }
}
