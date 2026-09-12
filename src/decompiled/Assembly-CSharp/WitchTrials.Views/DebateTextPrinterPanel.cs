using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.Localization;
using GigaCreation.Essentials.Ui;
using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x20000E2")]
public class DebateTextPrinterPanel : UITextPrinterPanel, ILocalizableUI
{
	[Serializable]
	[Token(Token = "0x20000E3")]
	public class LabelSpaceData
	{
		[Token(Token = "0x4000458")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		[SerializeField]
		private LocaleKind _localeKind;

		[Token(Token = "0x4000459")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
		[SerializeField]
		private float _space;

		[Token(Token = "0x1700007D")]
		public LocaleKind LocaleKind
		{
			[Token(Token = "0x6000386")]
			[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
			get
			{
				return default(LocaleKind);
			}
		}

		[Token(Token = "0x1700007E")]
		public float Space
		{
			[Token(Token = "0x6000387")]
			[Address(RVA = "0x3686E0", Offset = "0x3670E0", VA = "0x1803686E0")]
			get
			{
				return default(float);
			}
		}

		[Token(Token = "0x6000388")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public LabelSpaceData()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000E5")]
	[CompilerGenerated]
	private struct _003CGosub_003Ed__36
{
		[Token(Token = "0x4000462")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000463")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000464")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public string id;

		[Token(Token = "0x4000465")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000392")]
		[Address(RVA = "0x368AC0", Offset = "0x3674C0", VA = "0x180368AC0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000393")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000E6")]
	[CompilerGenerated]
	private struct _003CRevealMessages_003Ed__30
{
		[Token(Token = "0x4000466")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000467")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000468")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public DebateTextPrinterPanel _003C_003E4__this;

		[Token(Token = "0x4000469")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x400046A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000394")]
		[Address(RVA = "0x368E80", Offset = "0x367880", VA = "0x180368E80", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000395")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000E7")]
	[CompilerGenerated]
	private struct _003CWatAndRevealAsync_003Ed__35
{
		[Token(Token = "0x400046B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400046C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400046D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public float delay;

		[Token(Token = "0x400046E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public CancellationToken ct;

		[Token(Token = "0x400046F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public DebateText label;

		[Token(Token = "0x4000470")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000471")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000396")]
		[Address(RVA = "0x3697F0", Offset = "0x3681F0", VA = "0x1803697F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000397")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400044C")]
	private const string EnableDebugSkipVarName = "enableDebugSkip";

	[Token(Token = "0x400044D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x138")]
	[Header("References for Debate Printer")]
	[SerializeField]
	private FontMaterialChanger _fontMaterialChanger;

	[Token(Token = "0x400044E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x140")]
	[SerializeField]
	private AutoLayoutSupporter _autoLayoutSupporter;

	[Token(Token = "0x400044F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x148")]
	[SerializeField]
	private HorizontalOrVerticalLayoutGroup _layoutGroup;

	[Token(Token = "0x4000450")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x150")]
	[SerializeField]
	private DebateText[] _labels;

	[Token(Token = "0x4000451")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x158")]
	[Header("parameters for Debate Printer")]
	[SerializeField]
	private LabelSpaceData[] _labelSpaceData;

	[Token(Token = "0x4000452")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x160")]
	private readonly StringBuilder _builder;

	[Token(Token = "0x4000453")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x168")]
	private ILocalizationProvider _localizationProvider;

	[Token(Token = "0x4000454")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x170")]
	private ICustomVariableManager _naniVarManager;

	[Token(Token = "0x4000455")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x178")]
	private IDictionary<LocaleKind, float> _labelSpaces;

	[Token(Token = "0x4000456")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x180")]
	private CancellationTokenSource _revealCts;

	[Token(Token = "0x17000078")]
	private ILocalizationProvider LocalizationProvider
	{
		[Token(Token = "0x6000371")]
		[Address(RVA = "0x366090", Offset = "0x364A90", VA = "0x180366090")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000079")]
	private ICustomVariableManager NaniVarManager
	{
		[Token(Token = "0x6000372")]
		[Address(RVA = "0x366160", Offset = "0x364B60", VA = "0x180366160")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700007A")]
	private bool IsEnabledDebugSkip
	{
		[Token(Token = "0x6000373")]
		[Address(RVA = "0x366230", Offset = "0x364C30", VA = "0x180366230")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x1700007B")]
	public override float RevealProgress
	{
		[Token(Token = "0x6000374")]
		[Address(RVA = "0x366350", Offset = "0x364D50", VA = "0x180366350", Slot = "96")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6000375")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "97")]
		set
		{
		}
	}

	[Token(Token = "0x1700007C")]
	public override string Appearance
	{
		[Token(Token = "0x6000376")]
		[Address(RVA = "0x366360", Offset = "0x364D60", VA = "0x180366360", Slot = "98")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000377")]
		[Address(RVA = "0x366370", Offset = "0x364D70", VA = "0x180366370", Slot = "99")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x6000378")]
	[Address(RVA = "0x3663D0", Offset = "0x364DD0", VA = "0x1803663D0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6000379")]
	[Address(RVA = "0x3666F0", Offset = "0x3650F0", VA = "0x1803666F0", Slot = "8")]
	protected override void OnDestroy()
	{
	}

	[Token(Token = "0x600037A")]
	[Address(RVA = "0x3667E0", Offset = "0x3651E0", VA = "0x1803667E0", Slot = "83")]
	public override Naninovel.UniTask Initialize()
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x600037B")]
	[Address(RVA = "0x366B20", Offset = "0x365520", VA = "0x180366B20", Slot = "107")]
	public override void SetMessages(IReadOnlyList<PrintedMessage> messages)
	{
	}

	[Token(Token = "0x600037C")]
	[Address(RVA = "0x3670B0", Offset = "0x365AB0", VA = "0x1803670B0", Slot = "108")]
	public override void AddMessage(PrintedMessage message)
	{
	}

	[Token(Token = "0x600037D")]
	[Address(RVA = "0x367170", Offset = "0x365B70", VA = "0x180367170", Slot = "109")]
	public override void AppendText(Naninovel.LocalizableText text)
	{
	}

	[Token(Token = "0x600037E")]
	[Address(RVA = "0x3675A0", Offset = "0x365FA0", VA = "0x1803675A0", Slot = "111")]
	[AsyncStateMachine(typeof(_003CRevealMessages_003Ed__30))]
	public override Naninovel.UniTask RevealMessages(float delay, AsyncToken token)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x600037F")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "112")]
	public override void SetWaitForInputIndicatorVisible(bool visible)
	{
	}

	[Token(Token = "0x6000380")]
	[Address(RVA = "0x367720", Offset = "0x366120", VA = "0x180367720", Slot = "84")]
	public override void SetFont(TMP_FontAsset font)
	{
	}

	[Token(Token = "0x6000381")]
	[Address(RVA = "0x367AB0", Offset = "0x3664B0", VA = "0x180367AB0")]
	private void SetText(string text)
	{
	}

	[Token(Token = "0x6000382")]
	[Address(RVA = "0x367E20", Offset = "0x366820", VA = "0x180367E20")]
	private void AddText(string text)
	{
	}

	[Token(Token = "0x6000383")]
	[Address(RVA = "0x368350", Offset = "0x366D50", VA = "0x180368350")]
	[AsyncStateMachine(typeof(_003CWatAndRevealAsync_003Ed__35))]
	private static Naninovel.UniTask WatAndRevealAsync(DebateText label, float delay, [Optional] CancellationToken ct)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000384")]
	[Address(RVA = "0x368500", Offset = "0x366F00", VA = "0x180368500")]
	[AsyncStateMachine(typeof(_003CGosub_003Ed__36))]
	private static Naninovel.UniTask Gosub(string id)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000385")]
	[Address(RVA = "0x368620", Offset = "0x367020", VA = "0x180368620")]
	public DebateTextPrinterPanel()
	{
	}
}
