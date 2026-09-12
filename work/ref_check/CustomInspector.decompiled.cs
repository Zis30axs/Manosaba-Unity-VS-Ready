using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Il2CppDummyDll;
using UnityEngine;

[assembly: AssemblyVersion("0.0.0.0")]
[Token(Token = "0x2000002")]
[CompilerGenerated]
[EditorBrowsable(EditorBrowsableState.Never)]
[GeneratedCode("Unity.MonoScriptGenerator.MonoScriptInfoGenerator", null)]
internal class UnitySourceGeneratedAssemblyMonoScriptTypes_v1
{
	[Token(Token = "0x2000003")]
	private struct MonoScriptData
	{
		[Token(Token = "0x4000001")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public byte[] FilePathsData;

		[Token(Token = "0x4000002")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public byte[] TypesData;

		[Token(Token = "0x4000003")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public int TotalTypes;

		[Token(Token = "0x4000004")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
		public int TotalFiles;

		[Token(Token = "0x4000005")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public bool IsEditorOnly;
	}

	[MethodImpl(256)]
	[Token(Token = "0x6000001")]
	[Address(RVA = "0x43C3C0", Offset = "0x43ADC0", VA = "0x18043C3C0")]
	private static MonoScriptData Get()
	{
		return default(MonoScriptData);
	}

	[Token(Token = "0x6000002")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public UnitySourceGeneratedAssemblyMonoScriptTypes_v1()
	{
	}
}
namespace CustomInspector
{
	[Token(Token = "0x2000004")]
	public enum Size
	{
		[Token(Token = "0x4000007")]
		small,
		[Token(Token = "0x4000008")]
		medium,
		[Token(Token = "0x4000009")]
		big,
		[Token(Token = "0x400000A")]
		max
	}
	[Token(Token = "0x2000005")]
	public enum FixedColor
	{
		[Token(Token = "0x400000C")]
		BabyBlue,
		[Token(Token = "0x400000D")]
		Black,
		[Token(Token = "0x400000E")]
		Blue,
		[Token(Token = "0x400000F")]
		CherryRed,
		[Token(Token = "0x4000010")]
		CloudWhite,
		[Token(Token = "0x4000011")]
		Cyan,
		[Token(Token = "0x4000012")]
		DarkGray,
		[Token(Token = "0x4000013")]
		DustyBlue,
		[Token(Token = "0x4000014")]
		Gray,
		[Token(Token = "0x4000015")]
		Green,
		[Token(Token = "0x4000016")]
		IceWhite,
		[Token(Token = "0x4000017")]
		Magenta,
		[Token(Token = "0x4000018")]
		Orange,
		[Token(Token = "0x4000019")]
		PressedBlue,
		[Token(Token = "0x400001A")]
		Purple,
		[Token(Token = "0x400001B")]
		Red,
		[Token(Token = "0x400001C")]
		Yellow,
		[Token(Token = "0x400001D")]
		Transparent
	}
	[Token(Token = "0x2000006")]
	public enum LabelStyle
	{
		[Token(Token = "0x400001F")]
		NoLabel,
		[Token(Token = "0x4000020")]
		EmptyLabel,
		[Token(Token = "0x4000021")]
		NoSpacing,
		[Token(Token = "0x4000022")]
		FullSpacing
	}
	[Token(Token = "0x2000007")]
	public enum DisabledStyle
	{
		[Token(Token = "0x4000024")]
		GreyedOut,
		[Token(Token = "0x4000025")]
		Invisible
	}
	[Token(Token = "0x2000008")]
	public enum InspectorIcon
	{
		[Token(Token = "0x4000027")]
		AddFolder,
		[Token(Token = "0x4000028")]
		AreaLight,
		[Token(Token = "0x4000029")]
		Arrow_left,
		[Token(Token = "0x400002A")]
		Arrow_right,
		[Token(Token = "0x400002B")]
		AssetLabel,
		[Token(Token = "0x400002C")]
		Camera,
		[Token(Token = "0x400002D")]
		Error,
		[Token(Token = "0x400002E")]
		Warning,
		[Token(Token = "0x400002F")]
		Wrench,
		[Token(Token = "0x4000030")]
		Help,
		[Token(Token = "0x4000031")]
		Menu,
		[Token(Token = "0x4000032")]
		Settings,
		[Token(Token = "0x4000033")]
		Account,
		[Token(Token = "0x4000034")]
		Lock,
		[Token(Token = "0x4000035")]
		Lightbaking,
		[Token(Token = "0x4000036")]
		Lightbaking_Off,
		[Token(Token = "0x4000037")]
		Server_connected,
		[Token(Token = "0x4000038")]
		Server_disabled,
		[Token(Token = "0x4000039")]
		Server_disconnected,
		[Token(Token = "0x400003A")]
		Cross,
		[Token(Token = "0x400003B")]
		Cloud,
		[Token(Token = "0x400003C")]
		Picker,
		[Token(Token = "0x400003D")]
		Info_console,
		[Token(Token = "0x400003E")]
		Info_Inactive,
		[Token(Token = "0x400003F")]
		Script,
		[Token(Token = "0x4000040")]
		CustomSorting,
		[Token(Token = "0x4000041")]
		Debug,
		[Token(Token = "0x4000042")]
		DebuggerAttached,
		[Token(Token = "0x4000043")]
		DebuggerDisabled,
		[Token(Token = "0x4000044")]
		DebuggerEnabled,
		[Token(Token = "0x4000045")]
		DefaultAsset,
		[Token(Token = "0x4000046")]
		Edit_collision,
		[Token(Token = "0x4000047")]
		Edit_constraints,
		[Token(Token = "0x4000048")]
		Exposure,
		[Token(Token = "0x4000049")]
		Favorite,
		[Token(Token = "0x400004A")]
		Filter_by_label,
		[Token(Token = "0x400004B")]
		Filter_by_type,
		[Token(Token = "0x400004C")]
		Folder,
		[Token(Token = "0x400004D")]
		Folder_favorite,
		[Token(Token = "0x400004E")]
		Folder_opened,
		[Token(Token = "0x400004F")]
		Frame_capture,
		[Token(Token = "0x4000050")]
		GameObject,
		[Token(Token = "0x4000051")]
		Gizmos,
		[Token(Token = "0x4000052")]
		GridAndSnap,
		[Token(Token = "0x4000053")]
		GridAxisY,
		[Token(Token = "0x4000054")]
		GridAxisY_On,
		[Token(Token = "0x4000055")]
		Lighting,
		[Token(Token = "0x4000056")]
		Linked,
		[Token(Token = "0x4000057")]
		Unlinked,
		[Token(Token = "0x4000058")]
		Material,
		[Token(Token = "0x4000059")]
		Mesh,
		[Token(Token = "0x400005A")]
		More,
		[Token(Token = "0x400005B")]
		MoreOptions,
		[Token(Token = "0x400005C")]
		Move,
		[Token(Token = "0x400005D")]
		Minus,
		[Token(Token = "0x400005E")]
		Minus_filled,
		[Token(Token = "0x400005F")]
		Minus_act,
		[Token(Token = "0x4000060")]
		Plus,
		[Token(Token = "0x4000061")]
		Plus_filled,
		[Token(Token = "0x4000062")]
		Plus_act,
		[Token(Token = "0x4000063")]
		Orientation,
		[Token(Token = "0x4000064")]
		PackageManager,
		[Token(Token = "0x4000065")]
		Pause,
		[Token(Token = "0x4000066")]
		Pick,
		[Token(Token = "0x4000067")]
		Play,
		[Token(Token = "0x4000068")]
		Loop,
		[Token(Token = "0x4000069")]
		Prefab,
		[Token(Token = "0x400006A")]
		Context,
		[Token(Token = "0x400006B")]
		Ticked,
		[Token(Token = "0x400006C")]
		Rect,
		[Token(Token = "0x400006D")]
		Rotate,
		[Token(Token = "0x400006E")]
		Scale,
		[Token(Token = "0x400006F")]
		Scene,
		[Token(Token = "0x4000070")]
		pickable,
		[Token(Token = "0x4000071")]
		not_pickable,
		[Token(Token = "0x4000072")]
		pickable_mixed,
		[Token(Token = "0x4000073")]
		not_pickable_mixed,
		[Token(Token = "0x4000074")]
		_2D,
		[Token(Token = "0x4000075")]
		Sound,
		[Token(Token = "0x4000076")]
		Sound_off,
		[Token(Token = "0x4000077")]
		Sound_muted,
		[Token(Token = "0x4000078")]
		SceneCamera,
		[Token(Token = "0x4000079")]
		SceneView,
		[Token(Token = "0x400007A")]
		Lighting_Off,
		[Token(Token = "0x400007B")]
		Snap,
		[Token(Token = "0x400007C")]
		ScriptableObject,
		[Token(Token = "0x400007D")]
		Search,
		[Token(Token = "0x400007E")]
		Search_menu,
		[Token(Token = "0x400007F")]
		Search_jump,
		[Token(Token = "0x4000080")]
		Shaded,
		[Token(Token = "0x4000081")]
		ShadedWireframe,
		[Token(Token = "0x4000082")]
		Shortcut,
		[Token(Token = "0x4000083")]
		SnapIncrement,
		[Token(Token = "0x4000084")]
		Step,
		[Token(Token = "0x4000085")]
		TextAsset,
		[Token(Token = "0x4000086")]
		Center,
		[Token(Token = "0x4000087")]
		Global,
		[Token(Token = "0x4000088")]
		Local,
		[Token(Token = "0x4000089")]
		Pivot,
		[Token(Token = "0x400008A")]
		ToolSettings,
		[Token(Token = "0x400008B")]
		Transform,
		[Token(Token = "0x400008C")]
		Undo,
		[Token(Token = "0x400008D")]
		Console,
		[Token(Token = "0x400008E")]
		Game,
		[Token(Token = "0x400008F")]
		Info,
		[Token(Token = "0x4000090")]
		Hierarchy,
		[Token(Token = "0x4000091")]
		ViewOptions,
		[Token(Token = "0x4000092")]
		View_Move,
		[Token(Token = "0x4000093")]
		Eye,
		[Token(Token = "0x4000094")]
		Zoom,
		[Token(Token = "0x4000095")]
		Loading,
		[Token(Token = "0x4000096")]
		Wireframe,
		[Token(Token = "0x4000097")]
		Light,
		[Token(Token = "0x4000098")]
		Download,
		[Token(Token = "0x4000099")]
		Lock_opened,
		[Token(Token = "0x400009A")]
		Ticked_green,
		[Token(Token = "0x400009B")]
		Lens_flare,
		[Token(Token = "0x400009C")]
		Light_probe,
		[Token(Token = "0x400009D")]
		Package,
		[Token(Token = "0x400009E")]
		Particle_system,
		[Token(Token = "0x400009F")]
		Force_field,
		[Token(Token = "0x40000A0")]
		PointLight,
		[Token(Token = "0x40000A1")]
		Reflection_Probe,
		[Token(Token = "0x40000A2")]
		Refresh,
		[Token(Token = "0x40000A3")]
		SpotLight,
		[Token(Token = "0x40000A4")]
		Unity,
		[Token(Token = "0x40000A5")]
		Update,
		[Token(Token = "0x40000A6")]
		Visual_effect,
		[Token(Token = "0x40000A7")]
		WindZone
	}
	[Token(Token = "0x2000009")]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class AnimatorParameterAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000A8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string animatorPath;

		[Token(Token = "0x6000003")]
		[Address(RVA = "0x43C530", Offset = "0x43AF30", VA = "0x18043C530")]
		public AnimatorParameterAttribute(string animatorPath)
		{
		}
	}
	[Token(Token = "0x200000A")]
	public enum InspectorButtonState
	{
		[Token(Token = "0x40000AA")]
		isPressed,
		[Token(Token = "0x40000AB")]
		isSelected,
		[Token(Token = "0x40000AC")]
		notSelected
	}
	[Token(Token = "0x200000B")]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class AsButtonAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000AD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly bool staysPressed;

		[Token(Token = "0x40000AE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		public Size size;

		[Token(Token = "0x40000AF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public FixedColor selectedColor;

		[Token(Token = "0x40000B0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string label;

		[Token(Token = "0x40000B1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string tooltip;

		[Token(Token = "0x6000004")]
		[Address(RVA = "0x43C590", Offset = "0x43AF90", VA = "0x18043C590")]
		public AsButtonAttribute(bool staysPressed = true)
		{
		}
	}
	[Token(Token = "0x200000C")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class AsRangeAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000B2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly float minLimit;

		[Token(Token = "0x40000B3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		public readonly float maxLimit;

		[Token(Token = "0x6000005")]
		[Address(RVA = "0x43C640", Offset = "0x43B040", VA = "0x18043C640")]
		public AsRangeAttribute(float minLimit, float maxLimit)
		{
		}
	}
	[Token(Token = "0x200000D")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class AssetsOnlyAttribute : PropertyAttribute
	{
		[Token(Token = "0x6000006")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public AssetsOnlyAttribute()
		{
		}
	}
	[Token(Token = "0x200000E")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class BackgroundColorAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000B4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly FixedColor color;

		[Token(Token = "0x40000B5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		public readonly float borderSize;

		[Token(Token = "0x6000007")]
		[Address(RVA = "0x43C660", Offset = "0x43B060", VA = "0x18043C660")]
		public BackgroundColorAttribute(FixedColor color = FixedColor.DarkGray, float borderSize = 5f)
		{
		}
	}
	[Token(Token = "0x200000F")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class ButtonAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000B6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string methodPath;

		[Token(Token = "0x40000B7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly bool usePropertyAsParameter;

		[Token(Token = "0x40000B8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string label;

		[Token(Token = "0x40000B9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string tooltip;

		[Token(Token = "0x40000BA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public Size size;

		[Token(Token = "0x6000008")]
		[Address(RVA = "0x43C680", Offset = "0x43B080", VA = "0x18043C680")]
		public ButtonAttribute(string methodPath, bool usePropertyAsParameter = false)
		{
		}
	}
	[Token(Token = "0x2000010")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class ColorPaletteAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000BB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public bool hideFoldout;

		[Token(Token = "0x40000BC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly string paletteName;

		[Token(Token = "0x6000009")]
		[Address(RVA = "0x43C780", Offset = "0x43B180", VA = "0x18043C780")]
		public ColorPaletteAttribute(string paletteName = "default")
		{
		}
	}
	[Token(Token = "0x2000011")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class CopyPasteAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000BD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly bool previewClipboard;

		[Token(Token = "0x600000A")]
		[Address(RVA = "0x43C820", Offset = "0x43B220", VA = "0x18043C820")]
		public CopyPasteAttribute(bool previewClipboard = true)
		{
		}
	}
	[Token(Token = "0x2000012")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class DecimalsAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000BE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly int amount;

		[Token(Token = "0x600000B")]
		[Address(RVA = "0x43C830", Offset = "0x43B230", VA = "0x18043C830")]
		public DecimalsAttribute(int amount)
		{
		}
	}
	[Token(Token = "0x2000013")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class Delayed2Attribute : PropertyAttribute
	{
		[Token(Token = "0x600000C")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public Delayed2Attribute()
		{
		}
	}
	[Token(Token = "0x2000014")]
	[Obsolete("Use [field: <attribute>] instead to directly work with the field")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class DisplayAutoPropertyAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000BF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string propertyPath;

		[Token(Token = "0x40000C0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly bool allowChange;

		[Token(Token = "0x40000C1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string label;

		[Token(Token = "0x40000C2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string tooltip;

		[Token(Token = "0x600000D")]
		[Address(RVA = "0x43C840", Offset = "0x43B240", VA = "0x18043C840")]
		public DisplayAutoPropertyAttribute(string propertyPath, bool allowChange = true)
		{
		}
	}
	[Token(Token = "0x2000015")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class FixedValuesAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000C3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public object[] values;

		[Token(Token = "0x600000E")]
		[Address(RVA = "0x43C530", Offset = "0x43AF30", VA = "0x18043C530")]
		public FixedValuesAttribute(params object[] values)
		{
		}
	}
	[Token(Token = "0x2000016")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class FoldoutAttribute : PropertyAttribute
	{
		[Token(Token = "0x600000F")]
		[Address(RVA = "0x43C8B0", Offset = "0x43B2B0", VA = "0x18043C8B0")]
		public FoldoutAttribute()
		{
		}
	}
	[Token(Token = "0x2000017")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class ForceFillAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000C4")]
		public const float errorSize = 35f;

		[Token(Token = "0x40000C5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string[] notAllowed;

		[Token(Token = "0x40000C6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string errorMessage;

		[Token(Token = "0x40000C7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public bool onlyTestInPlayMode;

		[Token(Token = "0x6000010")]
		[Address(RVA = "0x43C8C0", Offset = "0x43B2C0", VA = "0x18043C8C0")]
		public ForceFillAttribute()
		{
		}

		[Token(Token = "0x6000011")]
		[Address(RVA = "0x43C8D0", Offset = "0x43B2D0", VA = "0x18043C8D0")]
		public ForceFillAttribute(params string[] notAllowed)
		{
		}
	}
	[Token(Token = "0x2000018")]
	public static class ForceFill
	{
		[Token(Token = "0x6000012")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		[Conditional("UNITY_EDITOR")]
		public static void CheckForceFilled(this MonoBehaviour @object)
		{
		}

		[Token(Token = "0x6000013")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		[Conditional("UNITY_EDITOR")]
		public static void CheckForceFilled(this object @object, Transform owner)
		{
		}
	}
	[Token(Token = "0x2000019")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class FromChildrenAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000C8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public bool allowNull;

		[Token(Token = "0x6000014")]
		[Address(RVA = "0x43C8C0", Offset = "0x43B2C0", VA = "0x18043C8C0")]
		public FromChildrenAttribute()
		{
		}
	}
	[Token(Token = "0x200001A")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class GetSetAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000C9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string getmethodPath;

		[Token(Token = "0x40000CA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly string setmethodPath;

		[Token(Token = "0x40000CB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string label;

		[Token(Token = "0x40000CC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string tooltip;

		[Token(Token = "0x6000015")]
		[Address(RVA = "0x43C930", Offset = "0x43B330", VA = "0x18043C930")]
		public GetSetAttribute(string getmethodPath, string setmethodPath)
		{
		}
	}
	[Token(Token = "0x200001B")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class GUIColorAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000CD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string colorString;

		[Token(Token = "0x40000CE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly FixedColor? fixedColor;

		[Token(Token = "0x40000CF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public readonly bool colorWholeUI;

		[Token(Token = "0x6000016")]
		[Address(RVA = "0x43C9F0", Offset = "0x43B3F0", VA = "0x18043C9F0")]
		public GUIColorAttribute(string color = "(0.9, 0.0, 0, 1)", bool colorWholeUI = false)
		{
		}

		[Token(Token = "0x6000017")]
		[Address(RVA = "0x43CA60", Offset = "0x43B460", VA = "0x18043CA60")]
		public GUIColorAttribute(FixedColor fixedColor, bool colorWholeUI = false)
		{
		}
	}
	[Token(Token = "0x200001C")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class HideFieldAttribute : PropertyAttribute
	{
		[Token(Token = "0x6000018")]
		[Address(RVA = "0x43CAF0", Offset = "0x43B4F0", VA = "0x18043CAF0")]
		public HideFieldAttribute()
		{
		}
	}
	[Token(Token = "0x200001D")]
	public enum ExecutionTarget
	{
		[Token(Token = "0x40000D1")]
		Always,
		[Token(Token = "0x40000D2")]
		IsPlaying,
		[Token(Token = "0x40000D3")]
		IsNotPlaying
	}
	[Token(Token = "0x200001E")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class HookAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000D4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string methodPath;

		[Token(Token = "0x40000D5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly ExecutionTarget target;

		[Token(Token = "0x40000D6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public bool useHookOnly;

		[Token(Token = "0x6000019")]
		[Address(RVA = "0x43CB00", Offset = "0x43B500", VA = "0x18043CB00")]
		public HookAttribute(string methodPath, ExecutionTarget target = ExecutionTarget.Always)
		{
		}
	}
	[Token(Token = "0x200001F")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class HorizontalGroupAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000D7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly bool beginNewGroup;

		[Token(Token = "0x40000D8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		public float size;

		[Token(Token = "0x40000D9")]
		public const int defaultOrder = -10;

		[Token(Token = "0x600001A")]
		[Address(RVA = "0x43CB70", Offset = "0x43B570", VA = "0x18043CB70")]
		public HorizontalGroupAttribute(bool beginNewGroup = false)
		{
		}
	}
	[Token(Token = "0x2000020")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class HorizontalLineAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000DA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public string message;

		[Token(Token = "0x40000DB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float thickness;

		[Token(Token = "0x40000DC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public float spacing;

		[Token(Token = "0x40000DD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float gapSize;

		[Token(Token = "0x40000DE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public readonly FixedColor color;

		[Token(Token = "0x600001B")]
		[Address(RVA = "0x43C8C0", Offset = "0x43B2C0", VA = "0x18043C8C0")]
		private HorizontalLineAttribute()
		{
		}

		[Token(Token = "0x600001C")]
		[Address(RVA = "0x43CB90", Offset = "0x43B590", VA = "0x18043CB90")]
		public HorizontalLineAttribute(float thickness = 1f, FixedColor color = FixedColor.Black, float spacing = 7f, float gapSize = 0f)
		{
		}

		[Token(Token = "0x600001D")]
		[Address(RVA = "0x43CBC0", Offset = "0x43B5C0", VA = "0x18043CBC0")]
		public HorizontalLineAttribute(string message, float thickness = 1f, FixedColor color = FixedColor.Cyan, float spacing = 15f, float gapSize = 0f)
		{
		}

		[Token(Token = "0x600001E")]
		[Address(RVA = "0x43CC40", Offset = "0x43B640", VA = "0x18043CC40")]
		public HorizontalLineAttribute(string message, float thickness, float spacing, float gapSize = 0f)
		{
		}
	}
	[Token(Token = "0x2000021")]
	public interface IMinMaxAttribute
	{
		[Token(Token = "0x17000001")]
		float CapValue
		{
			[Token(Token = "0x600001F")]
			get;
		}

		[Token(Token = "0x17000002")]
		string CapPath
		{
			[Token(Token = "0x6000020")]
			get;
		}

		[Token(Token = "0x6000021")]
		bool DependsOnOtherProperty();
	}
	[Token(Token = "0x2000022")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class IndentAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000DF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly int additionalIndentLevel;

		[Token(Token = "0x6000022")]
		[Address(RVA = "0x43C830", Offset = "0x43B230", VA = "0x18043C830")]
		public IndentAttribute(int additionalIndentLevel = 1)
		{
		}
	}
	[Token(Token = "0x2000023")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class InspectorIconAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000E0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly InspectorIcon icon;

		[Token(Token = "0x40000E1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		public readonly bool appendAtEnd;

		[Token(Token = "0x6000023")]
		[Address(RVA = "0x43CCC0", Offset = "0x43B6C0", VA = "0x18043CCC0")]
		public InspectorIconAttribute(InspectorIcon icon, bool appendAtEnd = false)
		{
		}
	}
	[Token(Token = "0x2000024")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class LabelSettingsAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000E2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly LabelStyle style;

		[Token(Token = "0x40000E3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly string newName;

		[Token(Token = "0x6000024")]
		[Address(RVA = "0x43CCE0", Offset = "0x43B6E0", VA = "0x18043CCE0")]
		public LabelSettingsAttribute(LabelStyle style)
		{
		}

		[Token(Token = "0x6000025")]
		[Address(RVA = "0x43CCF0", Offset = "0x43B6F0", VA = "0x18043CCF0")]
		public LabelSettingsAttribute(string newName, LabelStyle style = LabelStyle.FullSpacing)
		{
		}
	}
	[Token(Token = "0x2000025")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class LayerAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000E4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string requiredName;

		[Token(Token = "0x6000026")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public LayerAttribute()
		{
		}

		[Token(Token = "0x6000027")]
		[Address(RVA = "0x43C530", Offset = "0x43AF30", VA = "0x18043C530")]
		public LayerAttribute(string requiredName)
		{
		}
	}
	[Token(Token = "0x2000026")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class MaskAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000E5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly int bitsAmount;

		[Token(Token = "0x40000E6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly string[] bitNames;

		[Token(Token = "0x6000028")]
		[Address(RVA = "0x43CD60", Offset = "0x43B760", VA = "0x18043CD60")]
		public MaskAttribute()
		{
		}

		[Token(Token = "0x6000029")]
		[Address(RVA = "0x43CD70", Offset = "0x43B770", VA = "0x18043CD70")]
		public MaskAttribute(int bitsAmount)
		{
		}

		[Token(Token = "0x600002A")]
		[Address(RVA = "0x43CE00", Offset = "0x43B800", VA = "0x18043CE00")]
		public MaskAttribute(params string[] bitNames)
		{
		}
	}
	[Token(Token = "0x2000027")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class MaxAttribute : PropertyAttribute, IMinMaxAttribute
	{
		[Token(Token = "0x40000E7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		private readonly float max;

		[Token(Token = "0x40000E8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private readonly string maxPath;

		[Token(Token = "0x17000003")]
		public float CapValue
		{
			[Token(Token = "0x600002B")]
			[Address(RVA = "0x39FB70", Offset = "0x39E570", VA = "0x18039FB70", Slot = "7")]
			get
			{
				return default(float);
			}
		}

		[Token(Token = "0x17000004")]
		public string CapPath
		{
			[Token(Token = "0x600002C")]
			[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "8")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x600002D")]
		[Address(RVA = "0x43C8C0", Offset = "0x43B2C0", VA = "0x18043C8C0")]
		private MaxAttribute()
		{
		}

		[Token(Token = "0x600002E")]
		[Address(RVA = "0x43CF00", Offset = "0x43B900", VA = "0x18043CF00")]
		public MaxAttribute(float max)
		{
		}

		[Token(Token = "0x600002F")]
		[Address(RVA = "0x43CF20", Offset = "0x43B920", VA = "0x18043CF20")]
		public MaxAttribute(string maxPath)
		{
		}

		[Token(Token = "0x6000030")]
		[Address(RVA = "0x43CFF0", Offset = "0x43B9F0", VA = "0x18043CFF0", Slot = "9")]
		public bool DependsOnOtherProperty()
		{
			return default(bool);
		}
	}
	[Token(Token = "0x2000028")]
	public enum MessageBoxType
	{
		[Token(Token = "0x40000EA")]
		None,
		[Token(Token = "0x40000EB")]
		Info,
		[Token(Token = "0x40000EC")]
		Warning,
		[Token(Token = "0x40000ED")]
		Error
	}
	[Token(Token = "0x2000029")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class MessageBoxAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string content;

		[Token(Token = "0x40000EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly MessageBoxType type;

		[Token(Token = "0x6000031")]
		[Address(RVA = "0x43D000", Offset = "0x43BA00", VA = "0x18043D000")]
		public MessageBoxAttribute(string content, MessageBoxType type = MessageBoxType.Error)
		{
		}
	}
	[Token(Token = "0x200002A")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class Min2Attribute : PropertyAttribute, IMinMaxAttribute
	{
		[Token(Token = "0x40000F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		private readonly float min;

		[Token(Token = "0x40000F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private readonly string minPath;

		[Token(Token = "0x17000005")]
		public float CapValue
		{
			[Token(Token = "0x6000032")]
			[Address(RVA = "0x39FB70", Offset = "0x39E570", VA = "0x18039FB70", Slot = "7")]
			get
			{
				return default(float);
			}
		}

		[Token(Token = "0x17000006")]
		public string CapPath
		{
			[Token(Token = "0x6000033")]
			[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "8")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x6000034")]
		[Address(RVA = "0x43C8C0", Offset = "0x43B2C0", VA = "0x18043C8C0")]
		public Min2Attribute()
		{
		}

		[Token(Token = "0x6000035")]
		[Address(RVA = "0x43CF00", Offset = "0x43B900", VA = "0x18043CF00")]
		public Min2Attribute(float min)
		{
		}

		[Token(Token = "0x6000036")]
		[Address(RVA = "0x43D070", Offset = "0x43BA70", VA = "0x18043D070")]
		public Min2Attribute(string minPath)
		{
		}

		[Token(Token = "0x6000037")]
		[Address(RVA = "0x43CFF0", Offset = "0x43B9F0", VA = "0x18043CFF0", Slot = "9")]
		public bool DependsOnOtherProperty()
		{
			return default(bool);
		}
	}
	[Token(Token = "0x200002B")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class MultipleOfAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string stepPath;

		[Token(Token = "0x40000F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly double step;

		[Token(Token = "0x6000038")]
		[Address(RVA = "0x43D140", Offset = "0x43BB40", VA = "0x18043D140")]
		private MultipleOfAttribute()
		{
		}

		[Token(Token = "0x6000039")]
		[Address(RVA = "0x43D160", Offset = "0x43BB60", VA = "0x18043D160")]
		public MultipleOfAttribute(string stepPath)
		{
		}

		[Token(Token = "0x600003A")]
		[Address(RVA = "0x43D1D0", Offset = "0x43BBD0", VA = "0x18043D1D0")]
		public MultipleOfAttribute(double value)
		{
		}

		[Token(Token = "0x600003B")]
		[Address(RVA = "0x43D1F0", Offset = "0x43BBF0", VA = "0x18043D1F0")]
		public MultipleOfAttribute(float value)
		{
		}

		[Token(Token = "0x600003C")]
		[Address(RVA = "0x43D210", Offset = "0x43BC10", VA = "0x18043D210")]
		public MultipleOfAttribute(int value)
		{
		}
	}
	[Token(Token = "0x200002C")]
	public static class MultipleOf
	{
		[Token(Token = "0x600003D")]
		[Address(RVA = "0x43D230", Offset = "0x43BC30", VA = "0x18043D230")]
		public static bool IsMultiple(double value, double step)
		{
			return default(bool);
		}

		[Token(Token = "0x600003E")]
		[Address(RVA = "0x43D2C0", Offset = "0x43BCC0", VA = "0x18043D2C0")]
		public static bool IsMultiple(float value, float step)
		{
			return default(bool);
		}

		[Token(Token = "0x600003F")]
		[Address(RVA = "0x43D350", Offset = "0x43BD50", VA = "0x18043D350")]
		public static double NearestMultiple(double value, double step)
		{
			return default(double);
		}

		[Token(Token = "0x6000040")]
		[Address(RVA = "0x43D390", Offset = "0x43BD90", VA = "0x18043D390")]
		public static float NearestMultiple(float value, float step)
		{
			return default(float);
		}

		[Token(Token = "0x6000041")]
		[Address(RVA = "0x43D430", Offset = "0x43BE30", VA = "0x18043D430")]
		public static int NearestMultiple(int value, int step)
		{
			return default(int);
		}
	}
	[Token(Token = "0x200002D")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class PreviewAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly float thumbnailSize;

		[Token(Token = "0x6000042")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public PreviewAttribute(Size size = Size.medium)
		{
		}
	}
	[Token(Token = "0x200002E")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class ProgressBarAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000F5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public Size size;

		[Token(Token = "0x40000F6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly string minGetter;

		[Token(Token = "0x40000F7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public readonly float min;

		[Token(Token = "0x40000F8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public readonly string maxGetter;

		[Token(Token = "0x40000F9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public readonly float max;

		[Token(Token = "0x40000FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
		public bool isReadOnly;

		[Token(Token = "0x6000043")]
		[Address(RVA = "0x43D480", Offset = "0x43BE80", VA = "0x18043D480")]
		public ProgressBarAttribute(float max)
		{
		}

		[Token(Token = "0x6000044")]
		[Address(RVA = "0x43D490", Offset = "0x43BE90", VA = "0x18043D490")]
		[Obsolete("Use syntax ProgressBar(float max, size = Size.medium) instead of ProgressBar(float max, Size.medium)")]
		public ProgressBarAttribute(float max, Size size = Size.medium)
		{
		}

		[Token(Token = "0x6000045")]
		[Address(RVA = "0x43D4A0", Offset = "0x43BEA0", VA = "0x18043D4A0")]
		public ProgressBarAttribute(float min, float max)
		{
		}

		[Token(Token = "0x6000046")]
		[Address(RVA = "0x43D4B0", Offset = "0x43BEB0", VA = "0x18043D4B0")]
		public ProgressBarAttribute(string minGetter, float max)
		{
		}

		[Token(Token = "0x6000047")]
		[Address(RVA = "0x43D510", Offset = "0x43BF10", VA = "0x18043D510")]
		public ProgressBarAttribute(float min, string maxGetter)
		{
		}

		[Token(Token = "0x6000048")]
		[Address(RVA = "0x43D570", Offset = "0x43BF70", VA = "0x18043D570")]
		public ProgressBarAttribute(string minGetter, string maxGetter)
		{
		}
	}
	[Token(Token = "0x200002F")]
	public enum DisableStyle
	{
		[Token(Token = "0x40000FC")]
		GreyedOut,
		[Token(Token = "0x40000FD")]
		OnlyText
	}
	[Token(Token = "0x2000030")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class ReadOnlyAttribute : PropertyAttribute
	{
		[Token(Token = "0x40000FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public LabelStyle labelStyle;

		[Token(Token = "0x40000FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		public DisableStyle disableStyle;

		[Token(Token = "0x6000049")]
		[Address(RVA = "0x43D630", Offset = "0x43C030", VA = "0x18043D630")]
		public ReadOnlyAttribute(DisableStyle disableStyle = DisableStyle.GreyedOut, LabelStyle labelStyle = LabelStyle.FullSpacing)
		{
		}
	}
	[Token(Token = "0x2000031")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class RequireTypeAttribute : PropertyAttribute
	{
		[Token(Token = "0x17000007")]
		public Type requiredType
		{
			[Token(Token = "0x600004A")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
			[CompilerGenerated]
			get
			{
				return null;
			}
			[Token(Token = "0x600004B")]
			[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
			[CompilerGenerated]
			private set
			{
			}
		}

		[Token(Token = "0x600004C")]
		[Address(RVA = "0x43C530", Offset = "0x43AF30", VA = "0x18043C530")]
		public RequireTypeAttribute(Type type)
		{
		}
	}
	[Token(Token = "0x2000032")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class RichTextAttribute : PropertyAttribute
	{
		[Token(Token = "0x600004D")]
		[Address(RVA = "0x43D650", Offset = "0x43C050", VA = "0x18043D650")]
		public RichTextAttribute()
		{
		}

		[Token(Token = "0x600004E")]
		[Address(RVA = "0x43D650", Offset = "0x43C050", VA = "0x18043D650")]
		[Obsolete("Please use the [Multiline]-attribute or [TextArea]-attribute to edit the input-box height and it's wrapping.")]
		public RichTextAttribute(bool allowMultipleLines, int maxLines = 15)
		{
		}
	}
	[Token(Token = "0x2000033")]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class SceneAttribute : PropertyAttribute
	{
		[Token(Token = "0x4000101")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly bool useFullPath;

		[Token(Token = "0x600004F")]
		[Address(RVA = "0x43D660", Offset = "0x43C060", VA = "0x18043D660")]
		public SceneAttribute(bool useFullPath = false)
		{
		}
	}
	[Token(Token = "0x2000034")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class SelfFillAttribute : PropertyAttribute
	{
		[Token(Token = "0x4000102")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public OwnerMode mode;

		[Token(Token = "0x4000103")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		public readonly bool hideIfFilled;

		[Token(Token = "0x6000050")]
		[Address(RVA = "0x43D670", Offset = "0x43C070", VA = "0x18043D670")]
		public SelfFillAttribute(bool hideIfFilled = false)
		{
		}
	}
	[Token(Token = "0x2000035")]
	public enum OwnerMode
	{
		[Token(Token = "0x4000105")]
		Self,
		[Token(Token = "0x4000106")]
		Children,
		[Token(Token = "0x4000107")]
		Parent,
		[Token(Token = "0x4000108")]
		Root,
		[Token(Token = "0x4000109")]
		Parents,
		[Token(Token = "0x400010A")]
		DirectChildren
	}
	[Token(Token = "0x2000036")]
	public static class SelfFill
	{
		[Token(Token = "0x6000051")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		[Conditional("UNITY_EDITOR")]
		public static void CheckSelfFilled(this MonoBehaviour @object)
		{
		}

		[Token(Token = "0x6000052")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		[Conditional("UNITY_EDITOR")]
		public static void CheckSelfFilled(this object @object, Transform owner)
		{
		}
	}
	[Token(Token = "0x2000037")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class ShowAssetReferenceAttribute : PropertyAttribute
	{
		[Token(Token = "0x400010B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string fileName;

		[Token(Token = "0x6000053")]
		[Address(RVA = "0x43C530", Offset = "0x43AF30", VA = "0x18043C530")]
		public ShowAssetReferenceAttribute([Optional] string fileName)
		{
		}
	}
	[Token(Token = "0x2000038")]
	public enum BoolOperator
	{
		[Token(Token = "0x400010D")]
		And,
		[Token(Token = "0x400010E")]
		Or
	}
	[Token(Token = "0x2000039")]
	public enum ComparisonOp
	{
		[Token(Token = "0x4000110")]
		Equals,
		[Token(Token = "0x4000111")]
		NotNull,
		[Token(Token = "0x4000112")]
		Null
	}
	[Token(Token = "0x200003A")]
	public enum StaticConditions
	{
		[Token(Token = "0x4000114")]
		IsPlaying,
		[Token(Token = "0x4000115")]
		IsNotPlaying,
		[Token(Token = "0x4000116")]
		True,
		[Token(Token = "0x4000117")]
		False,
		[Token(Token = "0x4000118")]
		IsEnabled,
		[Token(Token = "0x4000119")]
		IsActiveAndEnabled
	}
	[Token(Token = "0x200003B")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class ShowIfAttribute : PropertyAttribute
	{
		[Token(Token = "0x400011A")]
		public const float indentation = 15f;

		[Token(Token = "0x400011B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly BoolOperator? op;

		[Token(Token = "0x400011C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly ComparisonOp? comOp;

		[Token(Token = "0x400011D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public readonly string[] conditionPaths;

		[Token(Token = "0x400011E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public DisabledStyle style;

		[Token(Token = "0x400011F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
		public int indent;

		[Token(Token = "0x17000008")]
		public bool Invert
		{
			[Token(Token = "0x6000054")]
			[Address(RVA = "0x43D680", Offset = "0x43C080", VA = "0x18043D680")]
			[CompilerGenerated]
			get
			{
				return default(bool);
			}
			[Token(Token = "0x6000055")]
			[Address(RVA = "0x43D690", Offset = "0x43C090", VA = "0x18043D690")]
			[CompilerGenerated]
			protected set
			{
			}
		}

		[Token(Token = "0x6000056")]
		[Address(RVA = "0x43D6A0", Offset = "0x43C0A0", VA = "0x18043D6A0")]
		private ShowIfAttribute()
		{
		}

		[Token(Token = "0x6000057")]
		[Address(RVA = "0x43D6C0", Offset = "0x43C0C0", VA = "0x18043D6C0")]
		protected ShowIfAttribute(params string[] conditionPaths)
		{
		}

		[Token(Token = "0x6000058")]
		[Address(RVA = "0x43D730", Offset = "0x43C130", VA = "0x18043D730")]
		public ShowIfAttribute(string conditionPath)
		{
		}

		[Token(Token = "0x6000059")]
		[Address(RVA = "0x43D810", Offset = "0x43C210", VA = "0x18043D810")]
		public ShowIfAttribute(StaticConditions condition)
		{
		}

		[Token(Token = "0x600005A")]
		[Address(RVA = "0x43D880", Offset = "0x43C280", VA = "0x18043D880")]
		public ShowIfAttribute(BoolOperator op, params string[] conditionPaths)
		{
		}

		[Token(Token = "0x600005B")]
		[Address(RVA = "0x43D960", Offset = "0x43C360", VA = "0x18043D960")]
		public ShowIfAttribute(BoolOperator op, StaticConditions cond1, [Optional] string cond2, [Optional] string cond3, [Optional] string cond4)
		{
		}

		[Token(Token = "0x600005C")]
		[Address(RVA = "0x43DA70", Offset = "0x43C470", VA = "0x18043DA70")]
		public ShowIfAttribute(BoolOperator op, string cond1, StaticConditions cond2, [Optional] string cond3, [Optional] string cond4)
		{
		}

		[Token(Token = "0x600005D")]
		[Address(RVA = "0x43DB80", Offset = "0x43C580", VA = "0x18043DB80")]
		public ShowIfAttribute(BoolOperator op, string cond1, string cond2, StaticConditions cond3, [Optional] string cond4)
		{
		}

		[Token(Token = "0x600005E")]
		[Address(RVA = "0x43DC90", Offset = "0x43C690", VA = "0x18043DC90")]
		public ShowIfAttribute(ComparisonOp com, params string[] conditionPaths)
		{
		}
	}
	[Token(Token = "0x200003C")]
	[Conditional("UNITY_EDITOR")]
	public class ShowIfNotAttribute : ShowIfAttribute
	{
		[Token(Token = "0x600005F")]
		[Address(RVA = "0x43DD70", Offset = "0x43C770", VA = "0x18043DD70")]
		public ShowIfNotAttribute(string conditionPath)
		{
		}

		[Token(Token = "0x6000060")]
		[Address(RVA = "0x43DD90", Offset = "0x43C790", VA = "0x18043DD90")]
		public ShowIfNotAttribute(StaticConditions condition)
		{
		}

		[Token(Token = "0x6000061")]
		[Address(RVA = "0x43DE10", Offset = "0x43C810", VA = "0x18043DE10")]
		public ShowIfNotAttribute(BoolOperator op, params string[] conditionPaths)
		{
		}

		[Token(Token = "0x6000062")]
		[Address(RVA = "0x43DE30", Offset = "0x43C830", VA = "0x18043DE30")]
		public ShowIfNotAttribute(BoolOperator op, StaticConditions cond1, [Optional] string cond2, [Optional] string cond3, [Optional] string cond4)
		{
		}

		[Token(Token = "0x6000063")]
		[Address(RVA = "0x43DF40", Offset = "0x43C940", VA = "0x18043DF40")]
		public ShowIfNotAttribute(BoolOperator op, string cond1, StaticConditions cond2, [Optional] string cond3, [Optional] string cond4)
		{
		}

		[Token(Token = "0x6000064")]
		[Address(RVA = "0x43E060", Offset = "0x43CA60", VA = "0x18043E060")]
		public ShowIfNotAttribute(BoolOperator op, string cond1, string cond2, StaticConditions cond3, [Optional] string cond4)
		{
		}

		[Token(Token = "0x6000065")]
		[Address(RVA = "0x43E180", Offset = "0x43CB80", VA = "0x18043E180")]
		public ShowIfNotAttribute(ComparisonOp com, params string[] conditionPaths)
		{
		}
	}
	[Token(Token = "0x200003D")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class ShowIfIsAttribute : PropertyAttribute
	{
		[Token(Token = "0x4000121")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string fieldPath;

		[Token(Token = "0x4000122")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly object value;

		[Token(Token = "0x4000123")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public DisabledStyle style;

		[Token(Token = "0x4000124")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x2C")]
		public int indent;

		[Token(Token = "0x17000009")]
		public bool Inverted
		{
			[Token(Token = "0x6000066")]
			[Address(RVA = "0x43E270", Offset = "0x43CC70", VA = "0x18043E270")]
			[CompilerGenerated]
			get
			{
				return default(bool);
			}
			[Token(Token = "0x6000067")]
			[Address(RVA = "0x43E280", Offset = "0x43CC80", VA = "0x18043E280")]
			[CompilerGenerated]
			protected set
			{
			}
		}

		[Token(Token = "0x6000068")]
		[Address(RVA = "0x43E290", Offset = "0x43CC90", VA = "0x18043E290")]
		public ShowIfIsAttribute(string fieldPath, object value)
		{
		}
	}
	[Token(Token = "0x200003E")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class ShowIfIsNotAttribute : ShowIfIsAttribute
	{
		[Token(Token = "0x6000069")]
		[Address(RVA = "0x43E360", Offset = "0x43CD60", VA = "0x18043E360")]
		public ShowIfIsNotAttribute(string fieldPath, object value)
		{
		}
	}
	[Token(Token = "0x200003F")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class ShowMethodAttribute : PropertyAttribute
	{
		[Token(Token = "0x4000126")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string getmethodPath;

		[Token(Token = "0x4000127")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string label;

		[Token(Token = "0x4000128")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string tooltip;

		[Token(Token = "0x600006A")]
		[Address(RVA = "0x43C8D0", Offset = "0x43B2D0", VA = "0x18043C8D0")]
		public ShowMethodAttribute(string getmethodPath)
		{
		}
	}
	[Token(Token = "0x2000040")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class ShowPropertyAttribute : PropertyAttribute
	{
		[Token(Token = "0x4000129")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string getPropertyPath;

		[Token(Token = "0x400012A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string label;

		[Token(Token = "0x400012B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string tooltip;

		[Token(Token = "0x600006B")]
		[Address(RVA = "0x43C8D0", Offset = "0x43B2D0", VA = "0x18043C8D0")]
		public ShowPropertyAttribute(string getPropertyPath)
		{
		}
	}
	[Token(Token = "0x2000041")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class Space2Attribute : PropertyAttribute
	{
		[Token(Token = "0x400012C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly float pixels;

		[Token(Token = "0x600006C")]
		[Address(RVA = "0x43C8C0", Offset = "0x43B2C0", VA = "0x18043C8C0")]
		private Space2Attribute()
		{
		}

		[Token(Token = "0x600006D")]
		[Address(RVA = "0x43E430", Offset = "0x43CE30", VA = "0x18043E430")]
		public Space2Attribute(float pixel)
		{
		}
	}
	[Token(Token = "0x2000042")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class TabAttribute : PropertyAttribute
	{
		[Token(Token = "0x400012D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string groupName;

		[Token(Token = "0x400012E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		[Obsolete("Please use the [BackgroundColor]-attribute instead")]
		public FixedColor backgroundColor;

		[Token(Token = "0x600006E")]
		[Address(RVA = "0x43E450", Offset = "0x43CE50", VA = "0x18043E450")]
		public TabAttribute(string groupName)
		{
		}

		[Token(Token = "0x600006F")]
		[Address(RVA = "0x43E4B0", Offset = "0x43CEB0", VA = "0x18043E4B0")]
		public TabAttribute(InspectorIcon icon)
		{
		}
	}
	[Token(Token = "0x2000043")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class TagAttribute : PropertyAttribute
	{
		[Token(Token = "0x6000070")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public TagAttribute()
		{
		}
	}
	[Token(Token = "0x2000044")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class TitleAttribute : PropertyAttribute
	{
		[Token(Token = "0x400012F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string content;

		[Token(Token = "0x4000130")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly bool underlined;

		[Token(Token = "0x4000131")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public int upperSpacing;

		[Token(Token = "0x4000132")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string tooltip;

		[Token(Token = "0x4000133")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public byte fontSize;

		[Token(Token = "0x4000134")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
		public TextAlignment alignment;

		[Token(Token = "0x6000071")]
		[Address(RVA = "0x43E570", Offset = "0x43CF70", VA = "0x18043E570")]
		public TitleAttribute(string content, bool underlined = false)
		{
		}
	}
	[Token(Token = "0x2000045")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class ToolbarAttribute : PropertyAttribute
	{
		[Token(Token = "0x4000135")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly float height;

		[Token(Token = "0x4000136")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		public readonly float space;

		[Token(Token = "0x4000137")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly float maxWidth;

		[Token(Token = "0x6000072")]
		[Address(RVA = "0x43E5E0", Offset = "0x43CFE0", VA = "0x18043E5E0")]
		public ToolbarAttribute(float height = 30f, float space = 10f, float maxWidth = 150f)
		{
		}
	}
	[Token(Token = "0x2000046")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class TooltipBoxAttribute : PropertyAttribute
	{
		[Token(Token = "0x4000138")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string content;

		[Token(Token = "0x6000073")]
		[Address(RVA = "0x43E730", Offset = "0x43D130", VA = "0x18043E730")]
		public TooltipBoxAttribute(string content)
		{
		}
	}
	[Token(Token = "0x2000047")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class UnfoldAttribute : PropertyAttribute
	{
		[Token(Token = "0x6000074")]
		[Address(RVA = "0x43C8C0", Offset = "0x43B2C0", VA = "0x18043C8C0")]
		public UnfoldAttribute()
		{
		}
	}
	[Token(Token = "0x2000048")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class UnitAttribute : PropertyAttribute
	{
		[Token(Token = "0x4000139")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string unitName;

		[Token(Token = "0x6000075")]
		[Address(RVA = "0x43E730", Offset = "0x43D130", VA = "0x18043E730")]
		public UnitAttribute(string unitName)
		{
		}
	}
	[Token(Token = "0x2000049")]
	[AttributeUsage(AttributeTargets.Field)]
	[Conditional("UNITY_EDITOR")]
	public class UnwrapAttribute : PropertyAttribute
	{
		[Token(Token = "0x400013A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public bool applyName;

		[Token(Token = "0x6000076")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public UnwrapAttribute()
		{
		}
	}
	[Token(Token = "0x200004A")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class URLAttribute : PropertyAttribute
	{
		[Token(Token = "0x400013B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string link;

		[Token(Token = "0x400013C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string label;

		[Token(Token = "0x400013D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string tooltip;

		[Token(Token = "0x6000077")]
		[Address(RVA = "0x43E790", Offset = "0x43D190", VA = "0x18043E790")]
		public URLAttribute(string link)
		{
		}
	}
	[Token(Token = "0x200004B")]
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
	[Conditional("UNITY_EDITOR")]
	public class ValidateAttribute : PropertyAttribute
	{
		[Token(Token = "0x400013E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly string methodPath;

		[Token(Token = "0x400013F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public readonly string errorMessage;

		[Token(Token = "0x4000140")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public MessageBoxType errorType;

		[Token(Token = "0x6000078")]
		[Address(RVA = "0x43E890", Offset = "0x43D290", VA = "0x18043E890")]
		public ValidateAttribute(string methodPath, string errorMessage = "Value is not valid", MessageBoxType errorType = MessageBoxType.Error)
		{
		}
	}
	[Token(Token = "0x200004C")]
	[Conditional("UNITY_EDITOR")]
	public class Array2DAttribute : PropertyAttribute
	{
		[Token(Token = "0x6000079")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public Array2DAttribute()
		{
		}
	}
	[Serializable]
	[Token(Token = "0x200004D")]
	public class Array2D<T>
	{
		[Serializable]
		[Token(Token = "0x200004E")]
		private class Row
		{
			[Token(Token = "0x4000144")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public T[] elements;

			[Token(Token = "0x6000083")]
			public Row(int elementAmount)
			{
			}
		}

		[Token(Token = "0x4000141")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		[Min(1f)]
		private int rowAmount;

		[Token(Token = "0x4000142")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		[Min(1f)]
		private int columnAmount;

		[Token(Token = "0x4000143")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		private Row[] rows;

		[Token(Token = "0x1700000A")]
		public int RowAmount
		{
			[Token(Token = "0x600007A")]
			get
			{
				return default(int);
			}
		}

		[Token(Token = "0x1700000B")]
		public int ColumnAmount
		{
			[Token(Token = "0x600007B")]
			get
			{
				return default(int);
			}
		}

		[Token(Token = "0x1700000C")]
		public T this[int rowIndex, int columnIndex]
		{
			[Token(Token = "0x600007C")]
			get
			{
				return (T)null;
			}
			[Token(Token = "0x600007D")]
			set
			{
			}
		}

		[Token(Token = "0x600007E")]
		public T GetElement(int rowIndex, int columnIndex)
		{
			return (T)null;
		}

		[Token(Token = "0x600007F")]
		public void SetElement(T value, int rowIndex, int columnIndex)
		{
		}

		[Token(Token = "0x6000080")]
		private void ThrowExceptionsIfInvalid(int rowIndex, int columnIndex)
		{
		}

		[Token(Token = "0x6000081")]
		public Array2D()
		{
		}

		[Token(Token = "0x6000082")]
		public Array2D(int rowAmount, int columnAmount)
		{
		}
	}
	[Token(Token = "0x200004F")]
	[Conditional("UNITY_EDITOR")]
	public class AssetPathAttribute : PropertyAttribute
	{
		[Token(Token = "0x6000084")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public AssetPathAttribute()
		{
		}
	}
	[Serializable]
	[Token(Token = "0x2000050")]
	public abstract class AssetPath
	{
		[Token(Token = "0x4000145")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		[SerializeField]
		private string path;

		[Token(Token = "0x4000146")]
		protected const string invalidString = "<invalid>";

		[Token(Token = "0x4000147")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly Type RequiredType;

		[Token(Token = "0x4000148")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		[SerializeField]
		[HideInInspector]
		private UnityEngine.Object assetReference;

		[Token(Token = "0x1700000D")]
		public string Name
		{
			[Token(Token = "0x6000085")]
			[Address(RVA = "0x43E960", Offset = "0x43D360", VA = "0x18043E960")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x1700000E")]
		public Type AssetType
		{
			[Token(Token = "0x600008B")]
			[Address(RVA = "0x43EE40", Offset = "0x43D840", VA = "0x18043EE40")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x6000086")]
		[Address(RVA = "0x43E970", Offset = "0x43D370", VA = "0x18043E970")]
		protected AssetPath(string defaultPath = "<invalid>")
		{
		}

		[Token(Token = "0x6000087")]
		[Address(RVA = "0x43EAB0", Offset = "0x43D4B0", VA = "0x18043EAB0")]
		protected AssetPath(Type fileType)
		{
		}

		[Token(Token = "0x6000088")]
		[Address(RVA = "0x43EC60", Offset = "0x43D660", VA = "0x18043EC60")]
		protected AssetPath(string defaultPath, Type fileType)
		{
		}

		[Token(Token = "0x6000089")]
		[Address(RVA = "0x43EE00", Offset = "0x43D800", VA = "0x18043EE00")]
		public string GetPath()
		{
			return null;
		}

		[Token(Token = "0x600008A")]
		[Address(RVA = "0x43EE10", Offset = "0x43D810", VA = "0x18043EE10")]
		public string GetFileName()
		{
			return null;
		}

		[Token(Token = "0x600008C")]
		[Address(RVA = "0x43EE90", Offset = "0x43D890", VA = "0x18043EE90")]
		public string GetAbsolutePath()
		{
			return null;
		}

		[Token(Token = "0x600008D")]
		[Address(RVA = "0x43EEC0", Offset = "0x43D8C0", VA = "0x18043EEC0")]
		public bool SetPath(string path)
		{
			return default(bool);
		}

		[Token(Token = "0x600008E")]
		[Address(RVA = "0x43EEE0", Offset = "0x43D8E0", VA = "0x18043EEE0")]
		public bool HasPath()
		{
			return default(bool);
		}

		[Token(Token = "0x600008F")]
		[Address(RVA = "0x43EF00", Offset = "0x43D900", VA = "0x18043EF00")]
		public bool HasEnding(string ending)
		{
			return default(bool);
		}

		[Token(Token = "0x6000090")]
		[Address(RVA = "0x43EF40", Offset = "0x43D940", VA = "0x18043EF40")]
		public static bool IsValidPath(string path, Type fileType)
		{
			return default(bool);
		}

		[Token(Token = "0x6000091")]
		[Address(RVA = "0x43EF90", Offset = "0x43D990", VA = "0x18043EF90", Slot = "3")]
		public override string ToString()
		{
			return null;
		}

		[Token(Token = "0x6000092")]
		[Address(RVA = "0x43EFE0", Offset = "0x43D9E0", VA = "0x18043EFE0")]
		public static string AbsoluteToRelativePath(string absolute)
		{
			return null;
		}
	}
	[Serializable]
	[Token(Token = "0x2000051")]
	public class FilePath : AssetPath
	{
		[Token(Token = "0x6000093")]
		[Address(RVA = "0x43F070", Offset = "0x43DA70", VA = "0x18043F070")]
		public FilePath(string defaultPath = "<invalid>")
		{
		}

		[Token(Token = "0x6000094")]
		[Address(RVA = "0x43F120", Offset = "0x43DB20", VA = "0x18043F120")]
		public FilePath(string defaultPath, Type fileType)
		{
		}

		[Token(Token = "0x6000095")]
		[Address(RVA = "0x43F130", Offset = "0x43DB30", VA = "0x18043F130")]
		public FilePath(Type fileType, string defaultPath = "<invalid>")
		{
		}

		[Token(Token = "0x6000096")]
		public T LoadAsset<T>() where T : UnityEngine.Object
		{
			return null;
		}

		[Token(Token = "0x6000097")]
		[Address(RVA = "0x43F150", Offset = "0x43DB50", VA = "0x18043F150")]
		public void OverrideAsset(UnityEngine.Object asset)
		{
		}

		[Token(Token = "0x6000098")]
		[Address(RVA = "0x43F1A0", Offset = "0x43DBA0", VA = "0x18043F1A0")]
		public string ReadAllText()
		{
			return null;
		}

		[Token(Token = "0x6000099")]
		[Address(RVA = "0x43F1F0", Offset = "0x43DBF0", VA = "0x18043F1F0")]
		public string[] ReadAllLines()
		{
			return null;
		}

		[Token(Token = "0x600009A")]
		[Address(RVA = "0x43F240", Offset = "0x43DC40", VA = "0x18043F240")]
		public void OverrideAllText(string content)
		{
		}

		[Token(Token = "0x600009B")]
		[Address(RVA = "0x43F290", Offset = "0x43DC90", VA = "0x18043F290")]
		public void AppendAllText(string content)
		{
		}
	}
	[Token(Token = "0x2000052")]
	public class Folder
	{
		[Token(Token = "0x600009C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public Folder()
		{
		}
	}
	[Serializable]
	[Token(Token = "0x2000053")]
	public class FolderPath : AssetPath
	{
		[Token(Token = "0x600009D")]
		[Address(RVA = "0x43F2E0", Offset = "0x43DCE0", VA = "0x18043F2E0")]
		public FolderPath(string defaultPath = "<invalid>")
		{
		}

		[Token(Token = "0x600009E")]
		[Address(RVA = "0x43F390", Offset = "0x43DD90", VA = "0x18043F390")]
		public void CreateAsset(UnityEngine.Object asset, string assetName)
		{
		}

		[Token(Token = "0x600009F")]
		[Address(RVA = "0x43F3E0", Offset = "0x43DDE0", VA = "0x18043F3E0")]
		public void DeleteAsset(string assetName)
		{
		}

		[Token(Token = "0x60000A0")]
		[Address(RVA = "0x43F440", Offset = "0x43DE40", VA = "0x18043F440")]
		public void DeleteAsset(string assetName, Type assetType)
		{
		}

		[Token(Token = "0x60000A1")]
		public T LoadAsset<T>(string assetName) where T : UnityEngine.Object
		{
			return null;
		}

		[Token(Token = "0x60000A2")]
		[Address(RVA = "0x43F490", Offset = "0x43DE90", VA = "0x18043F490")]
		public UnityEngine.Object LoadAsset(string assetName, Type type)
		{
			return null;
		}

		[Token(Token = "0x60000A3")]
		[Address(RVA = "0x43F4E0", Offset = "0x43DEE0", VA = "0x18043F4E0")]
		public List<FolderPath> GetSubFolders()
		{
			return null;
		}

		[Token(Token = "0x60000A4")]
		[Address(RVA = "0x43F530", Offset = "0x43DF30", VA = "0x18043F530")]
		public List<FilePath> GetFiles(string fileEnding = "*")
		{
			return null;
		}

		[Token(Token = "0x60000A5")]
		[Address(RVA = "0x43F580", Offset = "0x43DF80", VA = "0x18043F580")]
		public List<FilePath> GetAllFiles(string fileEnding = "*")
		{
			return null;
		}

		[Token(Token = "0x60000A6")]
		[Address(RVA = "0x43F5D0", Offset = "0x43DFD0", VA = "0x18043F5D0")]
		public void CreateOrReplaceFileAllText(string fileName, string content)
		{
		}

		[Token(Token = "0x60000A7")]
		[Address(RVA = "0x43F620", Offset = "0x43E020", VA = "0x18043F620")]
		public void CreateFileAllText(string fileName, string content)
		{
		}

		[Token(Token = "0x60000A8")]
		[Address(RVA = "0x43F670", Offset = "0x43E070", VA = "0x18043F670")]
		public void OverrideFileAllText(string fileName, string content)
		{
		}

		[Token(Token = "0x60000A9")]
		[Address(RVA = "0x43F6C0", Offset = "0x43E0C0", VA = "0x18043F6C0")]
		public void AppendFileAllText(string fileName, string content)
		{
		}
	}
	[Token(Token = "0x2000054")]
	[Conditional("UNITY_EDITOR")]
	public class DynamicSliderAttribute : PropertyAttribute
	{
		[Token(Token = "0x60000AA")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public DynamicSliderAttribute()
		{
		}
	}
	[Token(Token = "0x2000055")]
	public enum FixedSide
	{
		[Token(Token = "0x400014A")]
		None,
		[Token(Token = "0x400014B")]
		FixedMin,
		[Token(Token = "0x400014C")]
		FixedMax
	}
	[Serializable]
	[Token(Token = "0x2000056")]
	public class DynamicSlider
	{
		[Token(Token = "0x400014D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		[SerializeField]
		public float value;

		[Token(Token = "0x400014E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
		[SerializeField]
		private float min;

		[Token(Token = "0x400014F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		[SerializeField]
		private float max;

		[Token(Token = "0x4000150")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		private readonly float defaultMin;

		[Token(Token = "0x4000151")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private readonly float defaultMax;

		[Token(Token = "0x4000152")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		private readonly FixedSide fixedSide;

		[Token(Token = "0x60000AB")]
		[Address(RVA = "0x39F310", Offset = "0x39DD10", VA = "0x18039F310")]
		public DynamicSlider(float value, float min, float max, FixedSide fixedSide = FixedSide.None)
		{
		}

		[Token(Token = "0x60000AC")]
		[Address(RVA = "0x43F710", Offset = "0x43E110", VA = "0x18043F710", Slot = "3")]
		public override string ToString()
		{
			return null;
		}

		[Token(Token = "0x60000AD")]
		[Address(RVA = "0x43F720", Offset = "0x43E120", VA = "0x18043F720")]
		public static implicit operator float(DynamicSlider dynamicSlider)
		{
			return default(float);
		}

		[Token(Token = "0x60000AE")]
		[Address(RVA = "0x43F720", Offset = "0x43E120", VA = "0x18043F720")]
		public static float operator +(DynamicSlider d)
		{
			return default(float);
		}

		[Token(Token = "0x60000AF")]
		[Address(RVA = "0x43F740", Offset = "0x43E140", VA = "0x18043F740")]
		public static float operator -(DynamicSlider d)
		{
			return default(float);
		}

		[Token(Token = "0x60000B0")]
		[Address(RVA = "0x43F760", Offset = "0x43E160", VA = "0x18043F760")]
		public static float operator +(DynamicSlider a, DynamicSlider b)
		{
			return default(float);
		}

		[Token(Token = "0x60000B1")]
		[Address(RVA = "0x43F790", Offset = "0x43E190", VA = "0x18043F790")]
		public static float operator -(DynamicSlider a, DynamicSlider b)
		{
			return default(float);
		}

		[Token(Token = "0x60000B2")]
		[Address(RVA = "0x43F7C0", Offset = "0x43E1C0", VA = "0x18043F7C0")]
		public static float operator *(DynamicSlider a, DynamicSlider b)
		{
			return default(float);
		}

		[Token(Token = "0x60000B3")]
		[Address(RVA = "0x43F7F0", Offset = "0x43E1F0", VA = "0x18043F7F0")]
		public static float operator /(DynamicSlider a, DynamicSlider b)
		{
			return default(float);
		}

		[Token(Token = "0x60000B4")]
		[Address(RVA = "0x43F820", Offset = "0x43E220", VA = "0x18043F820")]
		public static float operator +(float f, DynamicSlider d)
		{
			return default(float);
		}

		[Token(Token = "0x60000B5")]
		[Address(RVA = "0x43F840", Offset = "0x43E240", VA = "0x18043F840")]
		public static float operator +(DynamicSlider d, float f)
		{
			return default(float);
		}

		[Token(Token = "0x60000B6")]
		[Address(RVA = "0x43F860", Offset = "0x43E260", VA = "0x18043F860")]
		public static float operator -(float f, DynamicSlider d)
		{
			return default(float);
		}

		[Token(Token = "0x60000B7")]
		[Address(RVA = "0x43F880", Offset = "0x43E280", VA = "0x18043F880")]
		public static float operator -(DynamicSlider d, float f)
		{
			return default(float);
		}

		[Token(Token = "0x60000B8")]
		[Address(RVA = "0x43F8A0", Offset = "0x43E2A0", VA = "0x18043F8A0")]
		public static float operator *(float f, DynamicSlider d)
		{
			return default(float);
		}

		[Token(Token = "0x60000B9")]
		[Address(RVA = "0x43F8C0", Offset = "0x43E2C0", VA = "0x18043F8C0")]
		public static float operator *(DynamicSlider d, float f)
		{
			return default(float);
		}

		[Token(Token = "0x60000BA")]
		[Address(RVA = "0x43F8E0", Offset = "0x43E2E0", VA = "0x18043F8E0")]
		public static float operator /(float f, DynamicSlider d)
		{
			return default(float);
		}

		[Token(Token = "0x60000BB")]
		[Address(RVA = "0x43F900", Offset = "0x43E300", VA = "0x18043F900")]
		public static float operator /(DynamicSlider d, float f)
		{
			return default(float);
		}
	}
	[Serializable]
	[Token(Token = "0x2000057")]
	public class LineGraph
	{
		[Token(Token = "0x4000153")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		[SerializeField]
		private bool _;

		[Token(Token = "0x4000154")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		[SerializeField]
		[HideInInspector]
		private Vector2[] points;

		[Token(Token = "0x1700000F")]
		public Vector2[] Points
		{
			[Token(Token = "0x60000BE")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
			get
			{
				return null;
			}
			[Token(Token = "0x60000BF")]
			[Address(RVA = "0x43FC30", Offset = "0x43E630", VA = "0x18043FC30")]
			set
			{
			}
		}

		[Token(Token = "0x60000BC")]
		[Address(RVA = "0x43F920", Offset = "0x43E320", VA = "0x18043F920")]
		public LineGraph()
		{
		}

		[Token(Token = "0x60000BD")]
		[Address(RVA = "0x43FA20", Offset = "0x43E420", VA = "0x18043FA20")]
		public LineGraph(Vector2[] points)
		{
		}

		[Token(Token = "0x60000C0")]
		[Address(RVA = "0x43FD40", Offset = "0x43E740", VA = "0x18043FD40")]
		private void SortPoints()
		{
		}

		[Token(Token = "0x60000C1")]
		[Address(RVA = "0x43FF10", Offset = "0x43E910", VA = "0x18043FF10")]
		public float GetYValue(float xValue)
		{
			return default(float);
		}
	}
	[Token(Token = "0x200005A")]
	[Conditional("UNITY_EDITOR")]
	public class LineGraphAttribute : PropertyAttribute
	{
		[Token(Token = "0x4000158")]
		public const float defaultGraphHeight = 150f;

		[Token(Token = "0x4000159")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public float graphHeight;

		[Token(Token = "0x60000C6")]
		[Address(RVA = "0x440150", Offset = "0x43EB50", VA = "0x180440150")]
		public LineGraphAttribute()
		{
		}
	}
	[Serializable]
	[Token(Token = "0x200005B")]
	public class ListContainer<T> : ICollection<T>, IEnumerable<T>, IEnumerable, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection, IList
	{
		[Token(Token = "0x400015A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		private bool _;

		[Token(Token = "0x400015B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		private List<T> values;

		[Token(Token = "0x17000010")]
		public T this[int index]
		{
			[Token(Token = "0x60000CD")]
			get
			{
				return (T)null;
			}
			[Token(Token = "0x60000CE")]
			set
			{
			}
		}

		[Token(Token = "0x17000011")]
		public int Count
		{
			[Token(Token = "0x60000CF")]
			get
			{
				return default(int);
			}
		}

		[Token(Token = "0x17000012")]
		public int Capacity
		{
			[Token(Token = "0x60000D0")]
			get
			{
				return default(int);
			}
		}

		[Token(Token = "0x17000013")]
		private bool System.Collections.Generic.ICollection<T>.IsReadOnly
		{
			[Token(Token = "0x60000FF")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x17000014")]
		public bool IsSynchronized
		{
			[Token(Token = "0x6000100")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x17000015")]
		public object SyncRoot
		{
			[Token(Token = "0x6000101")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x17000016")]
		public bool IsFixedSize
		{
			[Token(Token = "0x6000102")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x17000017")]
		public bool IsReadOnly
		{
			[Token(Token = "0x6000103")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x17000018")]
		private object System.Collections.IList.Item
		{
			[Token(Token = "0x6000104")]
			get
			{
				return null;
			}
			[Token(Token = "0x6000105")]
			set
			{
			}
		}

		[Token(Token = "0x60000C7")]
		public ListContainer()
		{
		}

		[Token(Token = "0x60000C8")]
		public ListContainer(int capacity)
		{
		}

		[Token(Token = "0x60000C9")]
		public ListContainer(List<T> collection)
		{
		}

		[Token(Token = "0x60000CA")]
		public ListContainer(IEnumerable<T> collection)
		{
		}

		[Token(Token = "0x60000CB")]
		public static implicit operator ListContainer<T>(List<T> list)
		{
			return null;
		}

		[Token(Token = "0x60000CC")]
		public static implicit operator List<T>(ListContainer<T> container)
		{
			return null;
		}

		[Token(Token = "0x60000D1")]
		public void Add(T item)
		{
		}

		[Token(Token = "0x60000D2")]
		public void AddRange(IEnumerable<T> collection)
		{
		}

		[Token(Token = "0x60000D3")]
		public ReadOnlyCollection<T> AsReadOnly()
		{
			return null;
		}

		[Token(Token = "0x60000D4")]
		public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
		{
			return default(int);
		}

		[Token(Token = "0x60000D5")]
		public int BinarySearch(T item)
		{
			return default(int);
		}

		[Token(Token = "0x60000D6")]
		public int BinarySearch(T item, IComparer<T> comparer)
		{
			return default(int);
		}

		[Token(Token = "0x60000D7")]
		public void Clear()
		{
		}

		[Token(Token = "0x60000D8")]
		public bool Contains(T item)
		{
			return default(bool);
		}

		[Token(Token = "0x60000D9")]
		public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
		{
			return null;
		}

		[Token(Token = "0x60000DA")]
		public void CopyTo(T[] array, int arrayIndex)
		{
		}

		[Token(Token = "0x60000DB")]
		public void CopyTo(T[] array)
		{
		}

		[Token(Token = "0x60000DC")]
		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
		}

		[Token(Token = "0x60000DD")]
		public bool Exists(Predicate<T> match)
		{
			return default(bool);
		}

		[Token(Token = "0x60000DE")]
		public T Find(Predicate<T> match)
		{
			return (T)null;
		}

		[Token(Token = "0x60000DF")]
		public List<T> FindAll(Predicate<T> match)
		{
			return null;
		}

		[Token(Token = "0x60000E0")]
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			return default(int);
		}

		[Token(Token = "0x60000E1")]
		public int FindIndex(int startIndex, Predicate<T> match)
		{
			return default(int);
		}

		[Token(Token = "0x60000E2")]
		public int FindIndex(Predicate<T> match)
		{
			return default(int);
		}

		[Token(Token = "0x60000E3")]
		public T FindLast(Predicate<T> match)
		{
			return (T)null;
		}

		[Token(Token = "0x60000E4")]
		public int FindLastIndex(int startIndex, int count, Predicate<T> match)
		{
			return default(int);
		}

		[Token(Token = "0x60000E5")]
		public int FindLastIndex(int startIndex, Predicate<T> match)
		{
			return default(int);
		}

		[Token(Token = "0x60000E6")]
		public int FindLastIndex(Predicate<T> match)
		{
			return default(int);
		}

		[Token(Token = "0x60000E7")]
		public void ForEach(Action<T> action)
		{
		}

		[Token(Token = "0x60000E8")]
		public List<T>.Enumerator GetEnumerator()
		{
			return default(List<T>.Enumerator);
		}

		[Token(Token = "0x60000E9")]
		public List<T> GetRange(int index, int count)
		{
			return null;
		}

		[Token(Token = "0x60000EA")]
		public int IndexOf(T item, int index, int count)
		{
			return default(int);
		}

		[Token(Token = "0x60000EB")]
		public int IndexOf(T item, int index)
		{
			return default(int);
		}

		[Token(Token = "0x60000EC")]
		public int IndexOf(T item)
		{
			return default(int);
		}

		[Token(Token = "0x60000ED")]
		public void Insert(int index, T item)
		{
		}

		[Token(Token = "0x60000EE")]
		public void InsertRange(int index, IEnumerable<T> collection)
		{
		}

		[Token(Token = "0x60000EF")]
		public int LastIndexOf(T item)
		{
			return default(int);
		}

		[Token(Token = "0x60000F0")]
		public int LastIndexOf(T item, int index)
		{
			return default(int);
		}

		[Token(Token = "0x60000F1")]
		public int LastIndexOf(T item, int index, int count)
		{
			return default(int);
		}

		[Token(Token = "0x60000F2")]
		public bool Remove(T item)
		{
			return default(bool);
		}

		[Token(Token = "0x60000F3")]
		public int RemoveAll(Predicate<T> match)
		{
			return default(int);
		}

		[Token(Token = "0x60000F4")]
		public void RemoveAt(int index)
		{
		}

		[Token(Token = "0x60000F5")]
		public void RemoveRange(int index, int count)
		{
		}

		[Token(Token = "0x60000F6")]
		public void Reverse(int index, int count)
		{
		}

		[Token(Token = "0x60000F7")]
		public void Reverse()
		{
		}

		[Token(Token = "0x60000F8")]
		public void Sort(Comparison<T> comparison)
		{
		}

		[Token(Token = "0x60000F9")]
		public void Sort(int index, int count, IComparer<T> comparer)
		{
		}

		[Token(Token = "0x60000FA")]
		public void Sort()
		{
		}

		[Token(Token = "0x60000FB")]
		public void Sort(IComparer<T> comparer)
		{
		}

		[Token(Token = "0x60000FC")]
		public T[] ToArray()
		{
			return null;
		}

		[Token(Token = "0x60000FD")]
		public void TrimExcess()
		{
		}

		[Token(Token = "0x60000FE")]
		public bool TrueForAll(Predicate<T> match)
		{
			return default(bool);
		}

		[Token(Token = "0x6000106")]
		private IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			return null;
		}

		[Token(Token = "0x6000107")]
		private IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return null;
		}

		[Token(Token = "0x6000108")]
		public void CopyTo(Array array, int index)
		{
		}

		[Token(Token = "0x6000109")]
		public int Add(object value)
		{
			return default(int);
		}

		[Token(Token = "0x600010A")]
		public bool Contains(object value)
		{
			return default(bool);
		}

		[Token(Token = "0x600010B")]
		public int IndexOf(object value)
		{
			return default(int);
		}

		[Token(Token = "0x600010C")]
		public void Insert(int index, object value)
		{
		}

		[Token(Token = "0x600010D")]
		public void Remove(object value)
		{
		}
	}
	[Token(Token = "0x200005C")]
	[Conditional("UNITY_EDITOR")]
	public class ListContainerAttribute : PropertyAttribute
	{
		[Token(Token = "0x600010E")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public ListContainerAttribute()
		{
		}
	}
	[Token(Token = "0x200005D")]
	[Conditional("UNITY_EDITOR")]
	public class MessageDrawerAttribute : PropertyAttribute
	{
		[Token(Token = "0x600010F")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public MessageDrawerAttribute()
		{
		}
	}
	[Serializable]
	[Token(Token = "0x200005E")]
	public class MessageDrawer
	{
		[Token(Token = "0x400015C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		[SerializeField]
		private string info;

		[Token(Token = "0x6000110")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		[Conditional("UNITY_EDITOR")]
		public void HideMessages(bool value)
		{
		}

		[Token(Token = "0x6000111")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		[Conditional("UNITY_EDITOR")]
		public void DrawMessage(string message, MessageBoxType type = MessageBoxType.None, bool replaceCurrents = false)
		{
		}

		[Token(Token = "0x6000112")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		[Conditional("UNITY_EDITOR")]
		public void DrawInfo(string message, bool replaceCurrents = false)
		{
		}

		[Token(Token = "0x6000113")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		[Conditional("UNITY_EDITOR")]
		public void DrawWarning(string message, bool replaceCurrents = false)
		{
		}

		[Token(Token = "0x6000114")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		[Conditional("UNITY_EDITOR")]
		public void DrawError(string message, bool replaceCurrents = false)
		{
		}

		[Token(Token = "0x6000115")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public MessageDrawer()
		{
		}
	}
	[Serializable]
	[Token(Token = "0x200005F")]
	public class SerializableDateTime
	{
		[Token(Token = "0x2000060")]
		public enum InspectorFormat
		{
			[Token(Token = "0x4000165")]
			Default,
			[Token(Token = "0x4000166")]
			AddTextInput,
			[Token(Token = "0x4000167")]
			TextInput,
			[Token(Token = "0x4000168")]
			DateEnums
		}

		[Token(Token = "0x400015D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		[Min(1f)]
		[SerializeField]
		public int year;

		[Token(Token = "0x400015E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
		[Min(1f)]
		[SerializeField]
		public int month;

		[Token(Token = "0x400015F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		[Min(1f)]
		[SerializeField]
		public int day;

		[Token(Token = "0x4000160")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		[Min(0f)]
		[SerializeField]
		public int hour;

		[Token(Token = "0x4000161")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		[Min(0f)]
		[SerializeField]
		public int minute;

		[Token(Token = "0x4000162")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		[Min(0f)]
		[SerializeField]
		public int second;

		[Token(Token = "0x4000163")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		[SerializeField]
		public DateTimeKind kind;

		[Token(Token = "0x17000019")]
		public int Year
		{
			[Token(Token = "0x6000116")]
			[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
			get
			{
				return default(int);
			}
			[Token(Token = "0x6000117")]
			[Address(RVA = "0x4401B0", Offset = "0x43EBB0", VA = "0x1804401B0")]
			set
			{
			}
		}

		[Token(Token = "0x1700001A")]
		public int Month
		{
			[Token(Token = "0x6000118")]
			[Address(RVA = "0x38F580", Offset = "0x38DF80", VA = "0x18038F580")]
			get
			{
				return default(int);
			}
			[Token(Token = "0x6000119")]
			[Address(RVA = "0x4401E0", Offset = "0x43EBE0", VA = "0x1804401E0")]
			set
			{
			}
		}

		[Token(Token = "0x1700001B")]
		public int Day
		{
			[Token(Token = "0x600011A")]
			[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590")]
			get
			{
				return default(int);
			}
			[Token(Token = "0x600011B")]
			[Address(RVA = "0x440200", Offset = "0x43EC00", VA = "0x180440200")]
			set
			{
			}
		}

		[Token(Token = "0x1700001C")]
		public int Hour
		{
			[Token(Token = "0x600011D")]
			[Address(RVA = "0x38F5A0", Offset = "0x38DFA0", VA = "0x18038F5A0")]
			get
			{
				return default(int);
			}
			[Token(Token = "0x600011E")]
			[Address(RVA = "0x440340", Offset = "0x43ED40", VA = "0x180440340")]
			set
			{
			}
		}

		[Token(Token = "0x1700001D")]
		public int Minute
		{
			[Token(Token = "0x600011F")]
			[Address(RVA = "0x440360", Offset = "0x43ED60", VA = "0x180440360")]
			get
			{
				return default(int);
			}
			[Token(Token = "0x6000120")]
			[Address(RVA = "0x440370", Offset = "0x43ED70", VA = "0x180440370")]
			set
			{
			}
		}

		[Token(Token = "0x1700001E")]
		public int Second
		{
			[Token(Token = "0x6000121")]
			[Address(RVA = "0x440390", Offset = "0x43ED90", VA = "0x180440390")]
			get
			{
				return default(int);
			}
			[Token(Token = "0x6000122")]
			[Address(RVA = "0x4403A0", Offset = "0x43EDA0", VA = "0x1804403A0")]
			set
			{
			}
		}

		[Token(Token = "0x1700001F")]
		public DateTimeKind Kind
		{
			[Token(Token = "0x6000123")]
			[Address(RVA = "0x4403C0", Offset = "0x43EDC0", VA = "0x1804403C0")]
			get
			{
				return default(DateTimeKind);
			}
			[Token(Token = "0x6000124")]
			[Address(RVA = "0x4403D0", Offset = "0x43EDD0", VA = "0x1804403D0")]
			set
			{
			}
		}

		[Token(Token = "0x600011C")]
		[Address(RVA = "0x4402A0", Offset = "0x43ECA0", VA = "0x1804402A0")]
		private void ClampDayValue()
		{
		}

		[Token(Token = "0x6000125")]
		[Address(RVA = "0x4403E0", Offset = "0x43EDE0", VA = "0x1804403E0")]
		public SerializableDateTime()
		{
		}

		[Token(Token = "0x6000126")]
		[Address(RVA = "0x440450", Offset = "0x43EE50", VA = "0x180440450")]
		public SerializableDateTime(DateTime dateTime)
		{
		}

		[Token(Token = "0x6000127")]
		[Address(RVA = "0x440620", Offset = "0x43F020", VA = "0x180440620")]
		public static implicit operator SerializableDateTime(DateTime dateTime)
		{
			return null;
		}

		[Token(Token = "0x6000128")]
		[Address(RVA = "0x440680", Offset = "0x43F080", VA = "0x180440680")]
		public static explicit operator DateTime(SerializableDateTime dateTime)
		{
			return default(DateTime);
		}
	}
	[Token(Token = "0x2000061")]
	[Conditional("UNITY_EDITOR")]
	public class SerializableDateTimeAttribute : PropertyAttribute
	{
		[Token(Token = "0x4000169")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public readonly SerializableDateTime.InspectorFormat format;

		[Token(Token = "0x6000129")]
		[Address(RVA = "0x4406E0", Offset = "0x43F0E0", VA = "0x1804406E0")]
		public SerializableDateTimeAttribute(SerializableDateTime.InspectorFormat format)
		{
		}
	}
	[Serializable]
	[Token(Token = "0x2000062")]
	public class SerializableInterface<T> : ISerializationCallbackReceiver where T : class
	{
		[Token(Token = "0x400016A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		private MonoBehaviour serializedReference;

		[Token(Token = "0x400016B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private T castedReference;

		[Token(Token = "0x17000020")]
		public T Value
		{
			[Token(Token = "0x600012A")]
			get
			{
				return null;
			}
			[Token(Token = "0x600012B")]
			set
			{
			}
		}

		[Token(Token = "0x600012C")]
		public SerializableInterface()
		{
		}

		[Token(Token = "0x600012D")]
		public SerializableInterface(MonoBehaviour value)
		{
		}

		[Token(Token = "0x600012E")]
		private void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		[Token(Token = "0x600012F")]
		private void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
		}
	}
	[Token(Token = "0x2000063")]
	[Conditional("UNITY_EDITOR")]
	public class InterfaceAttribute : PropertyAttribute
	{
		[Token(Token = "0x6000130")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public InterfaceAttribute()
		{
		}
	}
	[Token(Token = "0x2000064")]
	[Conditional("UNITY_EDITOR")]
	public class NullableAttribute : PropertyAttribute
	{
		[Token(Token = "0x6000131")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public NullableAttribute()
		{
		}
	}
	[Serializable]
	[Token(Token = "0x2000065")]
	public struct SerializableNullable<T> where T : struct
	{
		[Token(Token = "0x2000066")]
		public class NullType
		{
			[Token(Token = "0x6000147")]
			public NullType()
			{
			}
		}

		[Token(Token = "0x400016C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		private bool hasValue;

		[Token(Token = "0x400016D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		private T value;

		[Token(Token = "0x17000021")]
		public bool HasValue
		{
			[Token(Token = "0x6000132")]
			get
			{
				return default(bool);
			}
			[Token(Token = "0x6000133")]
			set
			{
			}
		}

		[Token(Token = "0x17000022")]
		public T Value
		{
			[Token(Token = "0x6000134")]
			get
			{
				return (T)null;
			}
			[Token(Token = "0x6000135")]
			set
			{
			}
		}

		[Token(Token = "0x6000136")]
		public SerializableNullable(T value)
		{
		}

		[Token(Token = "0x6000137")]
		public SerializableNullable(NullType _)
		{
		}

		[Token(Token = "0x6000138")]
		public static explicit operator T(SerializableNullable<T> value)
		{
			return (T)null;
		}

		[Token(Token = "0x6000139")]
		public static implicit operator SerializableNullable<T>(T value)
		{
			return default(SerializableNullable<T>);
		}

		[Token(Token = "0x600013A")]
		public static implicit operator SerializableNullable<T>(NullType value)
		{
			return default(SerializableNullable<T>);
		}

		[Token(Token = "0x600013B")]
		public static explicit operator SerializableNullable<T>(T? value)
		{
			return default(SerializableNullable<T>);
		}

		[Token(Token = "0x600013C")]
		public static explicit operator T?(SerializableNullable<T> value)
		{
			return null;
		}

		[Token(Token = "0x600013D")]
		public override readonly bool Equals(object obj)
		{
			return default(bool);
		}

		[Token(Token = "0x600013E")]
		public readonly bool Equals(SerializableNullable<T> other)
		{
			return default(bool);
		}

		[Token(Token = "0x600013F")]
		public override readonly int GetHashCode()
		{
			return default(int);
		}

		[Token(Token = "0x6000140")]
		public override string ToString()
		{
			return null;
		}

		[Token(Token = "0x6000141")]
		public static bool operator ==(SerializableNullable<T> a, SerializableNullable<T> b)
		{
			return default(bool);
		}

		[Token(Token = "0x6000142")]
		public static bool operator !=(SerializableNullable<T> a, SerializableNullable<T> b)
		{
			return default(bool);
		}

		[Token(Token = "0x6000143")]
		public static bool operator ==(SerializableNullable<T> a, NullType b)
		{
			return default(bool);
		}

		[Token(Token = "0x6000144")]
		public static bool operator !=(SerializableNullable<T> a, NullType b)
		{
			return default(bool);
		}

		[Token(Token = "0x6000145")]
		public static bool operator ==(NullType b, SerializableNullable<T> a)
		{
			return default(bool);
		}

		[Token(Token = "0x6000146")]
		public static bool operator !=(NullType b, SerializableNullable<T> a)
		{
			return default(bool);
		}
	}
	[Token(Token = "0x2000067")]
	public class TupleAttribute : PropertyAttribute
	{
		[Token(Token = "0x6000148")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public TupleAttribute()
		{
		}
	}
	[Serializable]
	[Token(Token = "0x2000068")]
	public class SerializableTuple<T1, T2>
	{
		[Token(Token = "0x400016E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T1 item1;

		[Token(Token = "0x400016F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T2 item2;

		[Token(Token = "0x6000149")]
		public SerializableTuple()
		{
		}

		[Token(Token = "0x600014A")]
		public SerializableTuple(T1 item1, T2 item2)
		{
		}
	}
	[Serializable]
	[Token(Token = "0x2000069")]
	public class SerializableTuple<T1, T2, T3>
	{
		[Token(Token = "0x4000170")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T1 item1;

		[Token(Token = "0x4000171")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T2 item2;

		[Token(Token = "0x4000172")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T3 item3;

		[Token(Token = "0x600014B")]
		public SerializableTuple()
		{
		}

		[Token(Token = "0x600014C")]
		public SerializableTuple(T1 item1, T2 item2, T3 item3)
		{
		}
	}
	[Serializable]
	[Token(Token = "0x200006A")]
	public class SerializableTuple<T1, T2, T3, T4>
	{
		[Token(Token = "0x4000173")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T1 item1;

		[Token(Token = "0x4000174")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T2 item2;

		[Token(Token = "0x4000175")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T3 item3;

		[Token(Token = "0x4000176")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T4 item4;

		[Token(Token = "0x600014D")]
		public SerializableTuple()
		{
		}

		[Token(Token = "0x600014E")]
		public SerializableTuple(T1 item1, T2 item2, T3 item3, T4 item4)
		{
		}
	}
	[Serializable]
	[Token(Token = "0x200006B")]
	public class SerializableTuple<T1, T2, T3, T4, T5>
	{
		[Token(Token = "0x4000177")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T1 item1;

		[Token(Token = "0x4000178")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T2 item2;

		[Token(Token = "0x4000179")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T3 item3;

		[Token(Token = "0x400017A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T4 item4;

		[Token(Token = "0x400017B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T5 item5;

		[Token(Token = "0x600014F")]
		public SerializableTuple()
		{
		}

		[Token(Token = "0x6000150")]
		public SerializableTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
		}
	}
	[Serializable]
	[Token(Token = "0x200006C")]
	public class SerializableTuple<T1, T2, T3, T4, T5, T6>
	{
		[Token(Token = "0x400017C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T1 item1;

		[Token(Token = "0x400017D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T2 item2;

		[Token(Token = "0x400017E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T3 item3;

		[Token(Token = "0x400017F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T4 item4;

		[Token(Token = "0x4000180")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T5 item5;

		[Token(Token = "0x4000181")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public T6 item6;

		[Token(Token = "0x6000151")]
		public SerializableTuple()
		{
		}

		[Token(Token = "0x6000152")]
		public SerializableTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
		}
	}
	[Serializable]
	[Token(Token = "0x200006D")]
	public class ReorderableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[Serializable]
		[Token(Token = "0x200006E")]
		public class SerializableKeyValuePair
		{
			[Token(Token = "0x4000184")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public TKey key;

			[Token(Token = "0x4000185")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public TValue value;

			[Token(Token = "0x4000186")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public bool isValid;

			[Token(Token = "0x6000167")]
			public SerializableKeyValuePair(TKey key, TValue value, bool isValid)
			{
			}
		}

		[Token(Token = "0x4000182")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		private bool _;

		[Token(Token = "0x4000183")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		private List<SerializableKeyValuePair> keyValuePairs;

		[Token(Token = "0x17000023")]
		public new TValue this[TKey key]
		{
			[Token(Token = "0x6000165")]
			get
			{
				return (TValue)null;
			}
			[Token(Token = "0x6000166")]
			set
			{
			}
		}

		[Token(Token = "0x6000153")]
		public ReorderableDictionary()
		{
		}

		[Token(Token = "0x6000154")]
		public ReorderableDictionary(ReorderableDictionary<TKey, TValue> dict)
		{
		}

		[Token(Token = "0x6000155")]
		public ReorderableDictionary(IDictionary<TKey, TValue> dictionary)
		{
		}

		[Token(Token = "0x6000156")]
		public ReorderableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
		}

		[Token(Token = "0x6000157")]
		public ReorderableDictionary(IEqualityComparer<TKey> comparer)
		{
		}

		[Token(Token = "0x6000158")]
		public ReorderableDictionary(int capacity)
		{
		}

		[Token(Token = "0x6000159")]
		public ReorderableDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
		{
		}

		[Token(Token = "0x600015A")]
		public ReorderableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
		{
		}

		[Token(Token = "0x600015B")]
		public ReorderableDictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
		}

		[Token(Token = "0x600015C")]
		protected ReorderableDictionary(SerializationInfo info, StreamingContext context)
		{
		}

		[Token(Token = "0x600015D")]
		[Conditional("UNITY_EDITOR")]
		private void ApplyValuesToList()
		{
		}

		[Token(Token = "0x600015E")]
		private void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		[Token(Token = "0x600015F")]
		private void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
		{
		}

		[Token(Token = "0x6000160")]
		public new void Clear()
		{
		}

		[Token(Token = "0x6000161")]
		public new bool TryAdd(TKey key, TValue value)
		{
			return default(bool);
		}

		[Token(Token = "0x6000162")]
		public new void Add(TKey key, TValue value)
		{
		}

		[Token(Token = "0x6000163")]
		public new bool Remove(TKey key)
		{
			return default(bool);
		}

		[Token(Token = "0x6000164")]
		public new bool Remove(TKey key, out TValue value)
		{
			return default(bool);
		}
	}
	[Token(Token = "0x2000072")]
	[Conditional("UNITY_EDITOR")]
	public class DictionaryAttribute : PropertyAttribute
	{
		[Token(Token = "0x400018B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public string keyLabel;

		[Token(Token = "0x400018C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string valueLabel;

		[Token(Token = "0x400018D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public readonly float keySize;

		[Token(Token = "0x400018E")]
		public const float defaultKeySize = 0.4f;

		[Token(Token = "0x600016F")]
		[Address(RVA = "0x4406F0", Offset = "0x43F0F0", VA = "0x1804406F0")]
		public DictionaryAttribute(float keySize = 0.4f)
		{
		}
	}
	[Serializable]
	[Token(Token = "0x2000073")]
	public class SerializableDictionary<TKey, TValue> : IEnumerable, IDictionary, ICollection, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IDeserializationCallback
	{
		[Token(Token = "0x2000074")]
		protected class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			[Token(Token = "0x4000194")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private readonly DictionaryEntry[] items;

			[Token(Token = "0x4000195")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private int index;

			[Token(Token = "0x17000033")]
			public object Current
			{
				[Token(Token = "0x60001A1")]
				get
				{
					return null;
				}
			}

			[Token(Token = "0x17000034")]
			public DictionaryEntry Entry
			{
				[Token(Token = "0x60001A2")]
				get
				{
					return default(DictionaryEntry);
				}
			}

			[Token(Token = "0x17000035")]
			public object Key
			{
				[Token(Token = "0x60001A3")]
				get
				{
					return null;
				}
			}

			[Token(Token = "0x17000036")]
			public object Value
			{
				[Token(Token = "0x60001A4")]
				get
				{
					return null;
				}
			}

			[Token(Token = "0x60001A0")]
			public DictionaryEnumerator(SerializableDictionary<TKey, TValue> sd)
			{
			}

			[Token(Token = "0x60001A5")]
			public bool MoveNext()
			{
				return default(bool);
			}

			[Token(Token = "0x60001A6")]
			private void ValidateIndex()
			{
			}

			[Token(Token = "0x60001A7")]
			public void Reset()
			{
			}
		}

		[Token(Token = "0x2000077")]
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__26 : IEnumerator<(TKey, TValue)>, IEnumerator, IDisposable
		{
			[Token(Token = "0x400019D")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private int <>1__state;

			[Token(Token = "0x400019E")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private (TKey key, TValue value) <>2__current;

			[Token(Token = "0x400019F")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public SerializableDictionary<TKey, TValue> <>4__this;

			[Token(Token = "0x40001A0")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private int <i>5__2;

			[Token(Token = "0x17000037")]
			private (TKey, TValue) System.Collections.Generic.IEnumerator<(TKeykey,TValuevalue)>.Current
			{
				[Token(Token = "0x60001B4")]
				[DebuggerHidden]
				get
				{
					return default((TKey, TValue));
				}
			}

			[Token(Token = "0x17000038")]
			private object System.Collections.IEnumerator.Current
			{
				[Token(Token = "0x60001B6")]
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[Token(Token = "0x60001B1")]
			[DebuggerHidden]
			public <GetEnumerator>d__26(int <>1__state)
			{
			}

			[Token(Token = "0x60001B2")]
			[DebuggerHidden]
			private void System.IDisposable.Dispose()
			{
			}

			[Token(Token = "0x60001B3")]
			private bool MoveNext()
			{
				return default(bool);
			}

			[Token(Token = "0x60001B5")]
			[DebuggerHidden]
			private void System.Collections.IEnumerator.Reset()
			{
			}
		}

		[Token(Token = "0x400018F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		private bool info;

		[Token(Token = "0x4000190")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		protected SerializableSet<TKey> keys;

		[Token(Token = "0x4000191")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		protected ListContainer<TValue> values;

		[Token(Token = "0x4000192")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		private TKey editor_keyInput;

		[Token(Token = "0x4000193")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		private TValue editor_valueInput;

		[Token(Token = "0x17000024")]
		public ReadOnlyCollection<TKey> Keys
		{
			[Token(Token = "0x6000170")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x17000025")]
		public ReadOnlyCollection<TValue> Values
		{
			[Token(Token = "0x6000171")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x17000026")]
		public int Count
		{
			[Token(Token = "0x6000172")]
			get
			{
				return default(int);
			}
		}

		[Token(Token = "0x17000027")]
		public TValue this[TKey key]
		{
			[Token(Token = "0x600017E")]
			get
			{
				return (TValue)null;
			}
			[Token(Token = "0x600017F")]
			set
			{
			}
		}

		[Token(Token = "0x17000028")]
		public bool IsReadOnly
		{
			[Token(Token = "0x6000189")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x17000029")]
		public bool IsFixedSize
		{
			[Token(Token = "0x600018A")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x1700002A")]
		public bool IsSynchronized
		{
			[Token(Token = "0x600018B")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x1700002B")]
		private object System.Collections.ICollection.SyncRoot
		{
			[Token(Token = "0x600018C")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x1700002C")]
		private ICollection System.Collections.IDictionary.Keys
		{
			[Token(Token = "0x600018D")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x1700002D")]
		private ICollection System.Collections.IDictionary.Values
		{
			[Token(Token = "0x600018E")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x1700002E")]
		private ICollection<TKey> System.Collections.Generic.IDictionary<TKey,TValue>.Keys
		{
			[Token(Token = "0x600018F")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x1700002F")]
		private ICollection<TValue> System.Collections.Generic.IDictionary<TKey,TValue>.Values
		{
			[Token(Token = "0x6000190")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x17000030")]
		private IEnumerable<TKey> System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>.Keys
		{
			[Token(Token = "0x6000191")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x17000031")]
		private IEnumerable<TValue> System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>.Values
		{
			[Token(Token = "0x6000192")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x17000032")]
		private object System.Collections.IDictionary.Item
		{
			[Token(Token = "0x6000193")]
			get
			{
				return null;
			}
			[Token(Token = "0x6000194")]
			set
			{
			}
		}

		[Token(Token = "0x6000173")]
		public SerializableDictionary()
		{
		}

		[Token(Token = "0x6000174")]
		public SerializableDictionary(IDictionary<TKey, TValue> dictionary)
		{
		}

		[Token(Token = "0x6000175")]
		public SerializableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
		}

		[Token(Token = "0x6000176")]
		protected SerializableDictionary(SerializableSet<TKey> keys, List<TValue> values)
		{
		}

		[Token(Token = "0x6000177")]
		public void Add(TKey key, TValue value)
		{
		}

		[Token(Token = "0x6000178")]
		public virtual bool TryAdd(TKey key, TValue value)
		{
			return default(bool);
		}

		[Token(Token = "0x6000179")]
		public bool Remove(TKey key)
		{
			return default(bool);
		}

		[Token(Token = "0x600017A")]
		public bool TryRemove(TKey key, out TValue value)
		{
			return default(bool);
		}

		[Token(Token = "0x600017B")]
		public void RemoveAt(int index)
		{
		}

		[Token(Token = "0x600017C")]
		public bool TryGetValue(TKey key, out TValue value)
		{
			return default(bool);
		}

		[Token(Token = "0x600017D")]
		public TValue GetValue(TKey key)
		{
			return (TValue)null;
		}

		[Token(Token = "0x6000180")]
		public bool ContainsKey(TKey key)
		{
			return default(bool);
		}

		[Token(Token = "0x6000181")]
		public bool Contains(TKey key, TValue value)
		{
			return default(bool);
		}

		[Token(Token = "0x6000182")]
		public void Clear()
		{
		}

		[Token(Token = "0x6000183")]
		[IteratorStateMachine(typeof(SerializableDictionary<, >.<GetEnumerator>d__26))]
		public IEnumerator<(TKey, TValue)> GetEnumerator()
		{
			return null;
		}

		[Token(Token = "0x6000184")]
		public static explicit operator (List<TKey>, List<TValue>)(SerializableDictionary<TKey, TValue> d)
		{
			return default((List<TKey>, List<TValue>));
		}

		[Token(Token = "0x6000185")]
		public static explicit operator List<(TKey, TValue)>(SerializableDictionary<TKey, TValue> d)
		{
			return null;
		}

		[Token(Token = "0x6000186")]
		public static explicit operator Dictionary<TKey, TValue>(SerializableDictionary<TKey, TValue> d)
		{
			return null;
		}

		[Token(Token = "0x6000187")]
		[IteratorStateMachine(typeof(<System-Collections-Generic-IEnumerable<System-Collections-Generic-KeyValuePair<TKey\u005c))]
		private IEnumerator<KeyValuePair<TKey, TValue>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey,TValue>>.GetEnumerator()
		{
			return null;
		}

		[Token(Token = "0x6000188")]
		private IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return null;
		}

		[Token(Token = "0x6000195")]
		private void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
		}

		[Token(Token = "0x6000196")]
		private bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			return default(bool);
		}

		[Token(Token = "0x6000197")]
		private bool System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return default(bool);
		}

		[Token(Token = "0x6000198")]
		private void System.Collections.IDictionary.Add(object key, object value)
		{
		}

		[Token(Token = "0x6000199")]
		private void System.Collections.IDictionary.Remove(object key)
		{
		}

		[Token(Token = "0x600019A")]
		private bool System.Collections.IDictionary.Contains(object key)
		{
			return default(bool);
		}

		[Token(Token = "0x600019B")]
		private IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator()
		{
			return null;
		}

		[Token(Token = "0x600019C")]
		private void System.Collections.ICollection.CopyTo(Array array, int index)
		{
		}

		[Token(Token = "0x600019D")]
		private void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
		}

		[Token(Token = "0x600019E")]
		private void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		[Token(Token = "0x600019F")]
		protected virtual void Internal_OnDeserialization(object sender)
		{
		}
	}
	[Token(Token = "0x2000079")]
	[Conditional("UNITY_EDITOR")]
	public class SetAttribute : PropertyAttribute
	{
		[Token(Token = "0x60001BD")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public SetAttribute()
		{
		}
	}
	[Serializable]
	[Token(Token = "0x200007A")]
	public class SerializableSet<T> : ICollection, IEnumerable, ICollection<T>, IEnumerable<T>, IDeserializationCallback
	{
		[Token(Token = "0x200007B")]
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__21 : IEnumerator<T>, IEnumerator, IDisposable
		{
			[Token(Token = "0x40001A8")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private int <>1__state;

			[Token(Token = "0x40001A9")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private T <>2__current;

			[Token(Token = "0x40001AA")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			public SerializableSet<T> <>4__this;

			[Token(Token = "0x40001AB")]
			[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
			private List<T>.Enumerator <>7__wrap1;

			[Token(Token = "0x17000040")]
			private T System.Collections.Generic.IEnumerator<T>.Current
			{
				[Token(Token = "0x60001DA")]
				[DebuggerHidden]
				get
				{
					return (T)null;
				}
			}

			[Token(Token = "0x17000041")]
			private object System.Collections.IEnumerator.Current
			{
				[Token(Token = "0x60001DC")]
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[Token(Token = "0x60001D6")]
			[DebuggerHidden]
			public <GetEnumerator>d__21(int <>1__state)
			{
			}

			[Token(Token = "0x60001D7")]
			[DebuggerHidden]
			private void System.IDisposable.Dispose()
			{
			}

			[Token(Token = "0x60001D8")]
			private bool MoveNext()
			{
				return default(bool);
			}

			[Token(Token = "0x60001D9")]
			private void <>m__Finally1()
			{
			}

			[Token(Token = "0x60001DB")]
			[DebuggerHidden]
			private void System.Collections.IEnumerator.Reset()
			{
			}
		}

		[Token(Token = "0x40001A5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		private bool info;

		[Token(Token = "0x40001A6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		protected ListContainer<T> values;

		[Token(Token = "0x40001A7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		[SerializeField]
		private T editor_input;

		[Token(Token = "0x1700003B")]
		public int Count
		{
			[Token(Token = "0x60001BE")]
			get
			{
				return default(int);
			}
		}

		[Token(Token = "0x1700003C")]
		public virtual T this[int index]
		{
			[Token(Token = "0x60001C7")]
			get
			{
				return (T)null;
			}
			[Token(Token = "0x60001C8")]
			set
			{
			}
		}

		[Token(Token = "0x1700003D")]
		private bool System.Collections.ICollection.IsSynchronized
		{
			[Token(Token = "0x60001D3")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x1700003E")]
		private object System.Collections.ICollection.SyncRoot
		{
			[Token(Token = "0x60001D4")]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x1700003F")]
		private bool System.Collections.Generic.ICollection<T>.IsReadOnly
		{
			[Token(Token = "0x60001D5")]
			get
			{
				return default(bool);
			}
		}

		[Token(Token = "0x60001BF")]
		public SerializableSet()
		{
		}

		[Token(Token = "0x60001C0")]
		public SerializableSet(IEnumerable<T> collection)
		{
		}

		[Token(Token = "0x60001C1")]
		public void Add(T item)
		{
		}

		[Token(Token = "0x60001C2")]
		public virtual bool TryAdd(T item)
		{
			return default(bool);
		}

		[Token(Token = "0x60001C3")]
		public virtual bool Remove(T item)
		{
			return default(bool);
		}

		[Token(Token = "0x60001C4")]
		public void RemoveAt(int index)
		{
		}

		[Token(Token = "0x60001C5")]
		public virtual int GetIndexOf(T item)
		{
			return default(int);
		}

		[Token(Token = "0x60001C6")]
		public T GetByIndex(int index)
		{
			return (T)null;
		}

		[Token(Token = "0x60001C9")]
		private void System.Runtime.Serialization.IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		[Token(Token = "0x60001CA")]
		protected virtual void Internal_OnDeserialization(object sender)
		{
		}

		[Token(Token = "0x60001CB")]
		public void Clear()
		{
		}

		[Token(Token = "0x60001CC")]
		public virtual bool Contains(T item)
		{
			return default(bool);
		}

		[Token(Token = "0x60001CD")]
		public ReadOnlyCollection<T> AsReadOnly()
		{
			return null;
		}

		[Token(Token = "0x60001CE")]
		private IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return null;
		}

		[Token(Token = "0x60001CF")]
		[IteratorStateMachine(typeof(SerializableSet<>.<GetEnumerator>d__21))]
		public IEnumerator<T> GetEnumerator()
		{
			return null;
		}

		[Token(Token = "0x60001D0")]
		public static explicit operator List<T>(SerializableSet<T> s)
		{
			return null;
		}

		[Token(Token = "0x60001D1")]
		private void System.Collections.ICollection.CopyTo(Array array, int index)
		{
		}

		[Token(Token = "0x60001D2")]
		private void System.Collections.Generic.ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
		}
	}
	[Serializable]
	[Token(Token = "0x200007C")]
	public class SerializableSortedDictionary<TKey, TValue> : SerializableDictionary<TKey, TValue> where TKey : IComparable
	{
		[Token(Token = "0x40001AC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly SerializableSortedSet<TKey> keys_casted;

		[Token(Token = "0x60001DD")]
		public SerializableSortedDictionary()
		{
		}

		[Token(Token = "0x60001DE")]
		public SerializableSortedDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
		{
		}

		[Token(Token = "0x60001DF")]
		public override bool TryAdd(TKey key, TValue value)
		{
			return default(bool);
		}

		[Token(Token = "0x60001E0")]
		protected override void Internal_OnDeserialization(object sender)
		{
		}
	}
	[Serializable]
	[Token(Token = "0x200007D")]
	public class SerializableSortedSet<T> : SerializableSet<T>, ICollection, IEnumerable, ICollection<T>, IEnumerable<T> where T : IComparable
	{
		[Token(Token = "0x17000042")]
		public override T this[int index]
		{
			[Token(Token = "0x60001EC")]
			get
			{
				return (T)null;
			}
			[Token(Token = "0x60001ED")]
			set
			{
			}
		}

		[Token(Token = "0x60001E1")]
		public override bool TryAdd(T item)
		{
			return default(bool);
		}

		[Token(Token = "0x60001E2")]
		public int AddAndGetIndex(T item)
		{
			return default(int);
		}

		[Token(Token = "0x60001E3")]
		public override bool Remove(T item)
		{
			return default(bool);
		}

		[Token(Token = "0x60001E4")]
		public int RemoveAndGetIndex(T item)
		{
			return default(int);
		}

		[Token(Token = "0x60001E5")]
		public override int GetIndexOf(T item)
		{
			return default(int);
		}

		[Token(Token = "0x60001E6")]
		public T[] GetItemsBetween(T t1, T t2)
		{
			return null;
		}

		[Token(Token = "0x60001E7")]
		public override bool Contains(T item)
		{
			return default(bool);
		}

		[Token(Token = "0x60001E8")]
		public IEnumerable<T> GetIEnumerable()
		{
			return null;
		}

		[Token(Token = "0x60001E9")]
		private IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			return null;
		}

		[Token(Token = "0x60001EA")]
		private IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return null;
		}

		[Token(Token = "0x60001EB")]
		private (int, bool) GetTheoreticalIndex(T item)
		{
			return default((int, bool));
		}

		[Token(Token = "0x60001EE")]
		protected override void Internal_OnDeserialization(object sender)
		{
		}

		[Token(Token = "0x60001EF")]
		private void System.Collections.ICollection.CopyTo(Array array, int index)
		{
		}

		[Token(Token = "0x60001F0")]
		private void System.Collections.Generic.ICollection<T>.CopyTo(T[] array, int arrayIndex)
		{
		}

		[Token(Token = "0x60001F1")]
		public SerializableSortedSet()
		{
		}
	}
	[Token(Token = "0x200007F")]
	[Conditional("UNITY_EDITOR")]
	public class StaticsDrawerAttribute : PropertyAttribute
	{
		[Token(Token = "0x60001F3")]
		[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
		public StaticsDrawerAttribute()
		{
		}
	}
	[Serializable]
	[Token(Token = "0x2000080")]
	public class StaticsDrawer
	{
		[Token(Token = "0x40001AF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		[SerializeField]
		private bool b;

		[Token(Token = "0x60001F4")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public StaticsDrawer()
		{
		}
	}
}
namespace CustomInspector.Extensions
{
	[Token(Token = "0x2000081")]
	public static class StylesConvert
	{
		[Token(Token = "0x60001F5")]
		[Address(RVA = "0x4407D0", Offset = "0x43F1D0", VA = "0x1804407D0")]
		public static Color ToColor(this FixedColor c)
		{
			return default(Color);
		}

		[Token(Token = "0x60001F6")]
		[Address(RVA = "0x440A20", Offset = "0x43F420", VA = "0x180440A20")]
		public static TextAnchor AlignmentToAnchor(TextAlignment textAlignment)
		{
			return default(TextAnchor);
		}

		[Token(Token = "0x60001F7")]
		[Address(RVA = "0x440AD0", Offset = "0x43F4D0", VA = "0x180440AD0")]
		public static InternalLabelStyle ToInteralStyle(this LabelStyle labelStyle)
		{
			return default(InternalLabelStyle);
		}

		[Token(Token = "0x60001F8")]
		[Address(RVA = "0x440BA0", Offset = "0x43F5A0", VA = "0x180440BA0")]
		public static string ToInternalIconName(this InspectorIcon icon)
		{
			return null;
		}

		[Token(Token = "0x60001F9")]
		[Address(RVA = "0x440C00", Offset = "0x43F600", VA = "0x180440C00")]
		public static string ToInternalIconName(string iconName)
		{
			return null;
		}

		[Token(Token = "0x60001FA")]
		[Address(RVA = "0x444670", Offset = "0x443070", VA = "0x180444670")]
		public static float ToButtonHeight(Size size)
		{
			return default(float);
		}

		[Token(Token = "0x60001FB")]
		[Address(RVA = "0x444750", Offset = "0x443150", VA = "0x180444750")]
		public static float ToButtonWidth(float availableWidth, GUIContent buttonLabel, Size size)
		{
			return default(float);
		}
	}
}
