using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20002AE")]
    [RequireComponent(typeof(RectTransform))]
    public class DragDrop : MonoBehaviour
    {
    	[Token(Token = "0x4000A78")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private DragDropHandle handle;

    	[Token(Token = "0x4000A79")]
    	[FieldOffset(Offset = "0x28")]
    	[Tooltip("Whether to prevent dragging over the canvas bounds.")]
    	[SerializeField]
    	private bool clipOverCanvas;

    	[Token(Token = "0x4000A7A")]
    	[FieldOffset(Offset = "0x30")]
    	private RectTransform trs;

    	[Token(Token = "0x4000A7B")]
    	[FieldOffset(Offset = "0x38")]
    	private RectTransform handleTrs;

    	[Token(Token = "0x4000A7C")]
    	[FieldOffset(Offset = "0x40")]
    	private Canvas canvas;

    	[Token(Token = "0x6000C8A")]
    	[Address(RVA = "0x53C3A0", Offset = "0x53ADA0", VA = "0x18053C3A0", Slot = "4")]
    	protected virtual void Awake()
    	{
    	}

    	[Token(Token = "0x6000C8B")]
    	[Address(RVA = "0x53C5B0", Offset = "0x53AFB0", VA = "0x18053C5B0", Slot = "5")]
    	protected virtual void OnEnable()
    	{
    	}

    	[Token(Token = "0x6000C8C")]
    	[Address(RVA = "0x53C720", Offset = "0x53B120", VA = "0x18053C720", Slot = "6")]
    	protected virtual void OnDisable()
    	{
    	}

    	[Token(Token = "0x6000C8D")]
    	[Address(RVA = "0x53C890", Offset = "0x53B290", VA = "0x18053C890", Slot = "7")]
    	protected virtual void HandleDrag(Vector2 position)
    	{
    	}

    	[Token(Token = "0x6000C8E")]
    	[Address(RVA = "0x53CE30", Offset = "0x53B830", VA = "0x18053CE30")]
    	public DragDrop()
    	{
    	}
    }
}
