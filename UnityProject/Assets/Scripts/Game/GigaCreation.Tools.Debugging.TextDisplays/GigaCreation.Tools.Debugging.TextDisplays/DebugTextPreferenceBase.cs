using System;
using GigaCreation.Tools.Debugging.Core;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;

namespace GigaCreation.Tools.Debugging.TextDisplays
{
    [Token(Token = "0x2000007")]
    public abstract class DebugTextPreferenceBase : MonoBehaviour
    {
    	[Token(Token = "0x400000D")]
    	[FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private int _priority;

    	[Token(Token = "0x400000E")]
    	[FieldOffset(Offset = "0x24")]
    	[SerializeField]
    	private bool _onlyOnceOnStart;

    	[Token(Token = "0x400000F")]
    	[FieldOffset(Offset = "0x28")]
    	private IDebugManager _debugManager;

    	[Token(Token = "0x4000010")]
    	[FieldOffset(Offset = "0x30")]
    	private DebugLabelManager _debugLabelManager;

    	[Token(Token = "0x4000011")]
    	[FieldOffset(Offset = "0x38")]
    	private TextMeshProUGUI _label;

    	[Token(Token = "0x4000012")]
    	[FieldOffset(Offset = "0x40")]
    	private bool _isQuitting;

    	[Token(Token = "0x4000013")]
    	[FieldOffset(Offset = "0x48")]
    	private IDisposable _labelUpdateDisposable;

    	[Token(Token = "0x17000002")]
    	protected abstract string LabelText
    	{
    		[Token(Token = "0x600000D")]
    		get;
    	}

    	[Token(Token = "0x600000E")]
    	[Address(RVA = "0xD45860", Offset = "0xD44260", VA = "0x180D45860")]
    	private void Start()
    	{
    	}

    	[Token(Token = "0x600000F")]
    	[Address(RVA = "0xD45A50", Offset = "0xD44450", VA = "0x180D45A50")]
    	private void OnDestroy()
    	{
    	}

    	[Token(Token = "0x6000010")]
    	[Address(RVA = "0x5DA780", Offset = "0x5D9180", VA = "0x1805DA780")]
    	private void OnApplicationQuit()
    	{
    	}

    	[Token(Token = "0x6000011")]
    	[Address(RVA = "0xD45BC0", Offset = "0xD445C0", VA = "0x180D45BC0")]
    	private void Initialize()
    	{
    	}

    	[Token(Token = "0x6000012")]
    	[Address(RVA = "0xD45D50", Offset = "0xD44750", VA = "0x180D45D50")]
    	private TextMeshProUGUI AddLabel()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000013")]
    	[Address(RVA = "0xD45F30", Offset = "0xD44930", VA = "0x180D45F30", Slot = "5")]
    	protected virtual IDisposable ActivateLabelUpdate()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000014")]
    	[Address(RVA = "0xD46010", Offset = "0xD44A10", VA = "0x180D46010")]
    	protected void UpdateLabel()
    	{
    	}

    	[Token(Token = "0x6000015")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	protected DebugTextPreferenceBase()
    	{
    	}
    }
}
