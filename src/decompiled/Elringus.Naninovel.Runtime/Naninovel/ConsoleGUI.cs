using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000317")]
public class ConsoleGUI : MonoBehaviour
{
	[Token(Token = "0x2000318")]
	private class OnGUIProxy : MonoBehaviour
	{
		[Token(Token = "0x4000BB6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Action OnGUIDelegate;

		[Token(Token = "0x6000EDB")]
		[Address(RVA = "0x55D410", Offset = "0x55BE10", VA = "0x18055D410")]
		private void OnGUI()
		{
		}

		[Token(Token = "0x6000EDC")]
		[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
		public OnGUIProxy()
		{
		}
	}

	[Token(Token = "0x4000BAB")]
	private const string inputControlName = "input";

	[Token(Token = "0x4000BAC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	private static ConsoleGUI instance;

	[Token(Token = "0x4000BAD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private static readonly Regex regex;

	[Token(Token = "0x4000BAE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private static readonly List<string> inputBuffer;

	[Token(Token = "0x4000BAF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	private static OnGUIProxy guiProxy;

	[Token(Token = "0x4000BB0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	private static Rect consoleRect;

	[Token(Token = "0x4000BB1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private static Rect execButtonRect;

	[Token(Token = "0x4000BB2")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	private static Rect closeButtonRect;

	[Token(Token = "0x4000BB3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
	private static bool setFocusPending;

	[Token(Token = "0x4000BB4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
	private static string input;

	[Token(Token = "0x4000BB5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
	private static int inputBufferIndex;

	[Token(Token = "0x170001F3")]
	public static KeyCode ToggleKey
	{
		[Token(Token = "0x6000EB7")]
		[Address(RVA = "0x559520", Offset = "0x557F20", VA = "0x180559520")]
		[CompilerGenerated]
		get
		{
			return default(KeyCode);
		}
		[Token(Token = "0x6000EB8")]
		[Address(RVA = "0x559580", Offset = "0x557F80", VA = "0x180559580")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170001F4")]
	public static bool ToggleByMultitouch
	{
		[Token(Token = "0x6000EB9")]
		[Address(RVA = "0x5595E0", Offset = "0x557FE0", VA = "0x1805595E0")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000EBA")]
		[Address(RVA = "0x559640", Offset = "0x558040", VA = "0x180559640")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170001F5")]
	public static bool ScaleByResolution
	{
		[Token(Token = "0x6000EBB")]
		[Address(RVA = "0x5596A0", Offset = "0x5580A0", VA = "0x1805596A0")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000EBC")]
		[Address(RVA = "0x559700", Offset = "0x558100", VA = "0x180559700")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170001F6")]
	public static Font Font
	{
		[Token(Token = "0x6000EBD")]
		[Address(RVA = "0x559760", Offset = "0x558160", VA = "0x180559760")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000EBE")]
		[Address(RVA = "0x5597C0", Offset = "0x5581C0", VA = "0x1805597C0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170001F7")]
	public static Color BackgroundColor
	{
		[Token(Token = "0x6000EBF")]
		[Address(RVA = "0x559880", Offset = "0x558280", VA = "0x180559880")]
		[CompilerGenerated]
		get
		{
			return default(Color);
		}
		[Token(Token = "0x6000EC0")]
		[Address(RVA = "0x5598E0", Offset = "0x5582E0", VA = "0x1805598E0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170001F8")]
	public static bool ShowCompleteList
	{
		[Token(Token = "0x6000EC1")]
		[Address(RVA = "0x559940", Offset = "0x558340", VA = "0x180559940")]
		[CompilerGenerated]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000EC2")]
		[Address(RVA = "0x5599A0", Offset = "0x5583A0", VA = "0x1805599A0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170001F9")]
	public static Predicate<char> IgnoreCharacter
	{
		[Token(Token = "0x6000EC3")]
		[Address(RVA = "0x559A00", Offset = "0x558400", VA = "0x180559A00")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000EC4")]
		[Address(RVA = "0x559A60", Offset = "0x558460", VA = "0x180559A60")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170001FA")]
	public static Action OnShow
	{
		[Token(Token = "0x6000EC5")]
		[Address(RVA = "0x559B20", Offset = "0x558520", VA = "0x180559B20")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000EC6")]
		[Address(RVA = "0x559B80", Offset = "0x558580", VA = "0x180559B80")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170001FB")]
	public static Action OnHide
	{
		[Token(Token = "0x6000EC7")]
		[Address(RVA = "0x559C40", Offset = "0x558640", VA = "0x180559C40")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000EC8")]
		[Address(RVA = "0x559CA0", Offset = "0x5586A0", VA = "0x180559CA0")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x170001FC")]
	public static bool Visible
	{
		[Token(Token = "0x6000EC9")]
		[Address(RVA = "0x559D60", Offset = "0x558760", VA = "0x180559D60")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x170001FD")]
	internal static Vector2 Size
	{
		[Token(Token = "0x6000ECA")]
		[Address(RVA = "0x559EA0", Offset = "0x5588A0", VA = "0x180559EA0")]
		[CompilerGenerated]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x6000ECB")]
		[Address(RVA = "0x559F10", Offset = "0x558910", VA = "0x180559F10")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x170001FE")]
	internal static Vector2 Scale
	{
		[Token(Token = "0x6000ECC")]
		[Address(RVA = "0x559F80", Offset = "0x558980", VA = "0x180559F80")]
		[CompilerGenerated]
		get
		{
			return default(Vector2);
		}
		[Token(Token = "0x6000ECD")]
		[Address(RVA = "0x559FF0", Offset = "0x5589F0", VA = "0x180559FF0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x170001FF")]
	internal static GUIStyle Style
	{
		[Token(Token = "0x6000ECE")]
		[Address(RVA = "0x55A060", Offset = "0x558A60", VA = "0x18055A060")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000ECF")]
		[Address(RVA = "0x55A0C0", Offset = "0x558AC0", VA = "0x18055A0C0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x6000ED0")]
	[Address(RVA = "0x55A180", Offset = "0x558B80", VA = "0x18055A180")]
	public static void Initialize([Optional] Dictionary<string, MethodInfo> commands)
	{
	}

	[Token(Token = "0x6000ED1")]
	[Address(RVA = "0x55B070", Offset = "0x559A70", VA = "0x18055B070")]
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	public static void Destroy()
	{
	}

	[Token(Token = "0x6000ED2")]
	[Address(RVA = "0x55B310", Offset = "0x559D10", VA = "0x18055B310")]
	public static void Show()
	{
	}

	[Token(Token = "0x6000ED3")]
	[Address(RVA = "0x55B3F0", Offset = "0x559DF0", VA = "0x18055B3F0")]
	public static void Hide()
	{
	}

	[Token(Token = "0x6000ED4")]
	[Address(RVA = "0x55B4C0", Offset = "0x559EC0", VA = "0x18055B4C0")]
	public static void Toggle()
	{
	}

	[Token(Token = "0x6000ED5")]
	[Address(RVA = "0x55B730", Offset = "0x55A130", VA = "0x18055B730")]
	private void OnApplicationQuit()
	{
	}

	[Token(Token = "0x6000ED6")]
	[Address(RVA = "0x55B780", Offset = "0x55A180", VA = "0x18055B780")]
	private void DrawGUI()
	{
	}

	[Token(Token = "0x6000ED7")]
	[Address(RVA = "0x55C0F0", Offset = "0x55AAF0", VA = "0x18055C0F0")]
	private void HandleGUIInput()
	{
	}

	[Token(Token = "0x6000ED8")]
	[Address(RVA = "0x55C7E0", Offset = "0x55B1E0", VA = "0x18055C7E0")]
	private static void ExecuteInput()
	{
	}

	[Token(Token = "0x6000ED9")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public ConsoleGUI()
	{
	}
}
