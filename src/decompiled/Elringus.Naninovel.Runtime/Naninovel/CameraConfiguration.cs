using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000112")]
[EditInProjectSettings]
public class CameraConfiguration : Configuration
{
	[Token(Token = "0x400042C")]
	[FieldOffset(Offset = "0x18")]
	[Tooltip("The reference resolution is used to evaluate proper rendering dimensions, so that actors are correctly positioned on scene. As a rule of thumb, set this equal to the resolution of the background textures you make for the game.")]
	public Vector2Int ReferenceResolution;

	[Token(Token = "0x400042D")]
	[FieldOffset(Offset = "0x20")]
	[Tooltip("How many pixels correspond to a scene unit. Reducing this will make all the actors appear smaller and vice-versa. Default value of 100 is recommended for most cases.")]
	public float ReferencePPU;

	[Token(Token = "0x400042E")]
	[FieldOffset(Offset = "0x24")]
	[Tooltip("Whether reference scene rectangle width should be matched against screen width. When enabled, relative (scene) position evaluation will use screen border as the origin; otherwise reference resolution is used.")]
	public bool MatchScreenWidth;

	[Token(Token = "0x400042F")]
	[FieldOffset(Offset = "0x28")]
	[Tooltip("Initial world position of the managed cameras.")]
	public Vector3 InitialPosition;

	[Token(Token = "0x4000430")]
	[FieldOffset(Offset = "0x38")]
	[Tooltip("A prefab with a camera component to use for rendering. Will use a default one when not specified. In case you wish to set some camera properties (background color, FOV, HDR, etc) or add post-processing scripts, create a prefab with the desired camera setup and assign the prefab to this field.")]
	public Camera CustomCameraPrefab;

	[Token(Token = "0x4000431")]
	[FieldOffset(Offset = "0x40")]
	[Tooltip("Whether to render the UI with a dedicated camera. This option is for backward-compatibility and should not be disabled in new projects. Expect issues when disabled (eg, constant uGUI layout rebuilds on camera animations).")]
	public bool UseUICamera;

	[Token(Token = "0x4000432")]
	[FieldOffset(Offset = "0x48")]
	[Tooltip("A prefab with a camera component to use for UI rendering. Will use a default one when not specified. Has no effect when `Use UI Camera` is disabled")]
	public Camera CustomUICameraPrefab;

	[Token(Token = "0x4000433")]
	[FieldOffset(Offset = "0x50")]
	[Tooltip("Default duration (in seconds) for all the camera modifications (changing zoom, position, rotation, etc).")]
	public float DefaultDuration;

	[Token(Token = "0x4000434")]
	[FieldOffset(Offset = "0x54")]
	[Tooltip("Easing function to use by default for all the camera modifications (changing zoom, position, rotation, etc).")]
	public EasingType DefaultEasing;

	[Token(Token = "0x4000435")]
	[FieldOffset(Offset = "0x58")]
	[Header("Thumbnails")]
	[Tooltip("The resolution in which thumbnails to preview game save slots will be captured.")]
	public Vector2Int ThumbnailResolution;

	[Token(Token = "0x4000436")]
	[FieldOffset(Offset = "0x60")]
	[Tooltip("Whether to ignore UI layer when capturing thumbnails.")]
	public bool HideUIInThumbnails;

	[Token(Token = "0x170000C9")]
	public Rect SceneRect
	{
		[Token(Token = "0x60004E3")]
		[Address(RVA = "0x485630", Offset = "0x484030", VA = "0x180485630")]
		get
		{
			return default(Rect);
		}
	}

	[Token(Token = "0x60004E4")]
	[Address(RVA = "0x485660", Offset = "0x484060", VA = "0x180485660")]
	public static Rect EvaluateSceneRect(Vector2 center, Vector2 resolution, float ppu)
	{
		return default(Rect);
	}

	[Token(Token = "0x60004E5")]
	[Address(RVA = "0x4856D0", Offset = "0x4840D0", VA = "0x1804856D0")]
	public static Vector3 SceneToWorldSpace(Vector3 scenePosition, Rect sceneRect)
	{
		return default(Vector3);
	}

	[Token(Token = "0x60004E6")]
	[Address(RVA = "0x485710", Offset = "0x484110", VA = "0x180485710")]
	public static Vector3 WorldToSceneSpace(Vector3 worldPosition, Rect sceneRect)
	{
		return default(Vector3);
	}

	[Token(Token = "0x60004E7")]
	[Address(RVA = "0x485750", Offset = "0x484150", VA = "0x180485750", Slot = "4")]
	public virtual Vector3 SceneToWorldSpace(Vector3 scenePosition)
	{
		return default(Vector3);
	}

	[Token(Token = "0x60004E8")]
	[Address(RVA = "0x4857D0", Offset = "0x4841D0", VA = "0x1804857D0", Slot = "5")]
	public virtual Vector3 WorldToSceneSpace(Vector3 worldPosition)
	{
		return default(Vector3);
	}

	[Token(Token = "0x60004E9")]
	[Address(RVA = "0x485850", Offset = "0x484250", VA = "0x180485850", Slot = "6")]
	protected virtual Rect EvaluateSceneRect()
	{
		return default(Rect);
	}

	[Token(Token = "0x60004EA")]
	[Address(RVA = "0x4859C0", Offset = "0x4843C0", VA = "0x1804859C0")]
	public CameraConfiguration()
	{
	}
}
