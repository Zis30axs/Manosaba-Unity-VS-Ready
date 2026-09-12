using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x2000540")]
public class DebugInfoGUI : MonoBehaviour
{
	[Token(Token = "0x400143A")]
	private const int windowId = 0;

	[Token(Token = "0x400143B")]
	[FieldOffset(Offset = "0x0")]
	private static DebugInfoGUI instance;

	[Token(Token = "0x400143C")]
	[FieldOffset(Offset = "0x20")]
	private Rect windowRect;

	[Token(Token = "0x400143D")]
	[FieldOffset(Offset = "0x30")]
	private Vector2 voiceScroll;

	[Token(Token = "0x400143E")]
	[FieldOffset(Offset = "0x38")]
	private bool show;

	[Token(Token = "0x400143F")]
	[FieldOffset(Offset = "0x40")]
	private EngineVersion version;

	[Token(Token = "0x4001440")]
	[FieldOffset(Offset = "0x48")]
	private IScriptPlayer player;

	[Token(Token = "0x4001441")]
	[FieldOffset(Offset = "0x50")]
	private IAudioManager audioManager;

	[Token(Token = "0x4001442")]
	[FieldOffset(Offset = "0x58")]
	private IStateManager stateManager;

	[Token(Token = "0x4001443")]
	[FieldOffset(Offset = "0x60")]
	private string lastCommandInfo;

	[Token(Token = "0x4001444")]
	[FieldOffset(Offset = "0x68")]
	private string lastAutoVoiceName;

	[Token(Token = "0x6001A0A")]
	[Address(RVA = "0x624700", Offset = "0x623100", VA = "0x180624700")]
	public static void Toggle()
	{
	}

	[Token(Token = "0x6001A0B")]
	[Address(RVA = "0x624930", Offset = "0x623330", VA = "0x180624930")]
	private void Awake()
	{
	}

	[Token(Token = "0x6001A0C")]
	[Address(RVA = "0x624B30", Offset = "0x623530", VA = "0x180624B30")]
	private void OnEnable()
	{
	}

	[Token(Token = "0x6001A0D")]
	[Address(RVA = "0x624C50", Offset = "0x623650", VA = "0x180624C50")]
	private void OnDisable()
	{
	}

	[Token(Token = "0x6001A0E")]
	[Address(RVA = "0x624D70", Offset = "0x623770", VA = "0x180624D70")]
	private void OnGUI()
	{
	}

	[Token(Token = "0x6001A0F")]
	[Address(RVA = "0x624EA0", Offset = "0x6238A0", VA = "0x180624EA0")]
	private void DrawWindow(int windowID)
	{
	}

	[Token(Token = "0x6001A10")]
	[Address(RVA = "0x6253C0", Offset = "0x623DC0", VA = "0x1806253C0")]
	private void HandleActionExecuted(Command command)
	{
	}

	[Token(Token = "0x6001A11")]
	[Address(RVA = "0x625540", Offset = "0x623F40", VA = "0x180625540")]
	private void HandleRollbackFinished()
	{
	}

	[Token(Token = "0x6001A12")]
	[Address(RVA = "0x6255A0", Offset = "0x623FA0", VA = "0x1806255A0")]
	public DebugInfoGUI()
	{
	}
}
