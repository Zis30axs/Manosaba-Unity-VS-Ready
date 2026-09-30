using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI
{
    [Token(Token = "0x200053E")]
    public class CustomVariableGUI : MonoBehaviour
    {
    	[Token(Token = "0x200053F")]
    	private class Record
    	{
    		[Token(Token = "0x4001437")]
    		[FieldOffset(Offset = "0x10")]
    		public string Name;

    		[Token(Token = "0x4001438")]
    		[FieldOffset(Offset = "0x18")]
    		public string Value;

    		[Token(Token = "0x4001439")]
    		[FieldOffset(Offset = "0x20")]
    		public string EditedValue;

    		[Token(Token = "0x170003B4")]
    		public bool Changed
    		{
    			[Token(Token = "0x6001A08")]
    			[Address(RVA = "0x624540", Offset = "0x622F40", VA = "0x180624540")]
    			get
    			{
    				return default(bool);
    			}
    		}

    		[Token(Token = "0x6001A09")]
    		[Address(RVA = "0x6245F0", Offset = "0x622FF0", VA = "0x1806245F0")]
    		public Record(string name, string value)
    		{
    		}
    	}

    	[Token(Token = "0x400142D")]
    	private const float width = 400f;

    	[Token(Token = "0x400142E")]
    	private const int windowId = 1;

    	[Token(Token = "0x400142F")]
    	[FieldOffset(Offset = "0x0")]
    	private static Rect windowRect;

    	[Token(Token = "0x4001430")]
    	[FieldOffset(Offset = "0x10")]
    	private static Vector2 scrollPos;

    	[Token(Token = "0x4001431")]
    	[FieldOffset(Offset = "0x18")]
    	private static bool show;

    	[Token(Token = "0x4001432")]
    	[FieldOffset(Offset = "0x20")]
    	private static string search;

    	[Token(Token = "0x4001433")]
    	[FieldOffset(Offset = "0x28")]
    	private static CustomVariableGUI instance;

    	[Token(Token = "0x4001434")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly SortedList<string, Record> records;

    	[Token(Token = "0x4001435")]
    	[FieldOffset(Offset = "0x28")]
    	private ICustomVariableManager variableManager;

    	[Token(Token = "0x4001436")]
    	[FieldOffset(Offset = "0x30")]
    	private IStateManager stateManager;

    	[Token(Token = "0x60019FA")]
    	[Address(RVA = "0x622460", Offset = "0x620E60", VA = "0x180622460")]
    	public static void Toggle()
    	{
    	}

    	[Token(Token = "0x60019FB")]
    	[Address(RVA = "0x6226B0", Offset = "0x6210B0", VA = "0x1806226B0")]
    	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    	private static void ResetOnPlayMode()
    	{
    	}

    	[Token(Token = "0x60019FC")]
    	[Address(RVA = "0x622710", Offset = "0x621110", VA = "0x180622710")]
    	private void Awake()
    	{
    	}

    	[Token(Token = "0x60019FD")]
    	[Address(RVA = "0x622850", Offset = "0x621250", VA = "0x180622850")]
    	private void OnEnable()
    	{
    	}

    	[Token(Token = "0x60019FE")]
    	[Address(RVA = "0x622A20", Offset = "0x621420", VA = "0x180622A20")]
    	private void OnDisable()
    	{
    	}

    	[Token(Token = "0x60019FF")]
    	[Address(RVA = "0x622C00", Offset = "0x621600", VA = "0x180622C00")]
    	private void OnGUI()
    	{
    	}

    	[Token(Token = "0x6001A00")]
    	[Address(RVA = "0x622D60", Offset = "0x621760", VA = "0x180622D60")]
    	private void DrawWindow(int windowId)
    	{
    	}

    	[Token(Token = "0x6001A01")]
    	[Address(RVA = "0x623690", Offset = "0x622090", VA = "0x180623690")]
    	private void SetEditedValue(string name, string editedValue)
    	{
    	}

    	[Token(Token = "0x6001A02")]
    	[Address(RVA = "0x623B90", Offset = "0x622590", VA = "0x180623B90")]
    	private string ToRecordValue(CustomVariableValue value)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001A03")]
    	[Address(RVA = "0x623D00", Offset = "0x622700", VA = "0x180623D00")]
    	private void HandleVariableUpdated(CustomVariableUpdatedArgs args)
    	{
    	}

    	[Token(Token = "0x6001A04")]
    	[Address(RVA = "0x623F90", Offset = "0x622990", VA = "0x180623F90")]
    	private void HandleGameLoadFinished(GameSaveLoadArgs obj)
    	{
    	}

    	[Token(Token = "0x6001A05")]
    	[Address(RVA = "0x623FA0", Offset = "0x6229A0", VA = "0x180623FA0")]
    	private void UpdateRecords()
    	{
    	}

    	[Token(Token = "0x6001A06")]
    	[Address(RVA = "0x624320", Offset = "0x622D20", VA = "0x180624320")]
    	public CustomVariableGUI()
    	{
    	}
    }
}
