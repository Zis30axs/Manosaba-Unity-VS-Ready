using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20000C2")]
[ActorResources(typeof(UITextPrinterPanel), false)]
public class UITextPrinter : MonoBehaviourActor<TextPrinterMetadata>, ITextPrinterActor, IActor
{
	[StructLayout(3)]
	[Token(Token = "0x20000C3")]
	[CompilerGenerated]
	private struct _003CChangeVisibility_003Ed__31
{
		[Token(Token = "0x40002EA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002EB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40002EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public UITextPrinter _003C_003E4__this;

		[Token(Token = "0x40002ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public bool visible;

		[Token(Token = "0x40002EE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public Tween tween;

		[Token(Token = "0x40002EF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken token;

		[Token(Token = "0x40002F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60003CE")]
		[Address(RVA = "0x474680", Offset = "0x473080", VA = "0x180474680", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60003CF")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000C4")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__29
{
		[Token(Token = "0x40002F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40002F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public UITextPrinter _003C_003E4__this;

		[Token(Token = "0x40002F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40002F5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<GameObject>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x40002F6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<IManagedUI>.Awaiter _003C_003Eu__3;

		[Token(Token = "0x60003D0")]
		[Address(RVA = "0x4749F0", Offset = "0x4733F0", VA = "0x1804749F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60003D1")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000C5")]
	[CompilerGenerated]
	private struct _003CLoadUIPrefab_003Ed__36
{
		[Token(Token = "0x40002F7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002F8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<GameObject> _003C_003Et__builder;

		[Token(Token = "0x40002F9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public UITextPrinter _003C_003E4__this;

		[Token(Token = "0x40002FA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<Resource<GameObject>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60003D2")]
		[Address(RVA = "0x4754C0", Offset = "0x473EC0", VA = "0x1804754C0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60003D3")]
		[Address(RVA = "0x475850", Offset = "0x474250", VA = "0x180475850", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000C6")]
	[CompilerGenerated]
	private struct _003CReveal_003Ed__34
{
		[Token(Token = "0x40002FB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002FC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40002FD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public UITextPrinter _003C_003E4__this;

		[Token(Token = "0x40002FE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40002FF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public float delay;

		[Token(Token = "0x4000300")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60003D4")]
		[Address(RVA = "0x475880", Offset = "0x474280", VA = "0x180475880", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60003D5")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40002E3")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private readonly IUIManager uis;

	[Token(Token = "0x40002E4")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	private readonly ILocalizationManager l10n;

	[Token(Token = "0x40002E5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	private readonly IResourceProviderManager resources;

	[Token(Token = "0x40002E6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
	private readonly AspectMonitor aspectMonitor;

	[Token(Token = "0x40002E7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
	private readonly List<PrintedMessage> messages;

	[Token(Token = "0x40002E8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
	private readonly List<MessageTemplate> templates;

	[Token(Token = "0x40002E9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB8")]
	private CancellationTokenSource revealTextCTS;

	[Token(Token = "0x1700009A")]
	public override GameObject GameObject
	{
		[Token(Token = "0x60003A8")]
		[Address(RVA = "0x471630", Offset = "0x470030", VA = "0x180471630", Slot = "39")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700009B")]
	public override string Appearance
	{
		[Token(Token = "0x60003A9")]
		[Address(RVA = "0x471660", Offset = "0x470060", VA = "0x180471660", Slot = "27")]
		get
		{
			return null;
		}
		[Token(Token = "0x60003AA")]
		[Address(RVA = "0x4716A0", Offset = "0x4700A0", VA = "0x1804716A0", Slot = "28")]
		set
		{
		}
	}

	[Token(Token = "0x1700009C")]
	public override bool Visible
	{
		[Token(Token = "0x60003AB")]
		[Address(RVA = "0x4716F0", Offset = "0x4700F0", VA = "0x1804716F0", Slot = "29")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x60003AC")]
		[Address(RVA = "0x471730", Offset = "0x470130", VA = "0x180471730", Slot = "30")]
		set
		{
		}
	}

	[Token(Token = "0x1700009D")]
	public virtual IReadOnlyList<PrintedMessage> Messages
	{
		[Token(Token = "0x60003AD")]
		[Address(RVA = "0x471780", Offset = "0x470180", VA = "0x180471780", Slot = "71")]
		get
		{
			return null;
		}
		[Token(Token = "0x60003AE")]
		[Address(RVA = "0x471790", Offset = "0x470190", VA = "0x180471790", Slot = "72")]
		set
		{
		}
	}

	[Token(Token = "0x1700009E")]
	public virtual IReadOnlyList<MessageTemplate> Templates
	{
		[Token(Token = "0x60003AF")]
		[Address(RVA = "0x4717B0", Offset = "0x4701B0", VA = "0x1804717B0", Slot = "73")]
		get
		{
			return null;
		}
		[Token(Token = "0x60003B0")]
		[Address(RVA = "0x4717C0", Offset = "0x4701C0", VA = "0x1804717C0", Slot = "74")]
		set
		{
		}
	}

	[Token(Token = "0x1700009F")]
	public virtual float RevealProgress
	{
		[Token(Token = "0x60003B1")]
		[Address(RVA = "0x4717E0", Offset = "0x4701E0", VA = "0x1804717E0", Slot = "75")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x60003B2")]
		[Address(RVA = "0x471820", Offset = "0x470220", VA = "0x180471820", Slot = "76")]
		set
		{
		}
	}

	[Token(Token = "0x170000A0")]
	public virtual UITextPrinterPanel PrinterPanel
	{
		[Token(Token = "0x60003B3")]
		[Address(RVA = "0x468E80", Offset = "0x467880", VA = "0x180468E80", Slot = "77")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x60003B4")]
		[Address(RVA = "0x468E90", Offset = "0x467890", VA = "0x180468E90")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x60003B5")]
	[Address(RVA = "0x471840", Offset = "0x470240", VA = "0x180471840")]
	public UITextPrinter(string id = default, TextPrinterMetadata meta = default)
	{
	}

	[Token(Token = "0x60003B6")]
	[Address(RVA = "0x471BF0", Offset = "0x4705F0", VA = "0x180471BF0", Slot = "41")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__29))]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60003B7")]
	[Address(RVA = "0x471D20", Offset = "0x470720", VA = "0x180471D20", Slot = "42")]
	public override UniTask ChangeAppearance(string appearance, Tween tween, [Optional] Transition? transition, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60003B8")]
	[Address(RVA = "0x471D90", Offset = "0x470790", VA = "0x180471D90", Slot = "43")]
	[AsyncStateMachine(typeof(_003CChangeVisibility_003Ed__31))]
	public override UniTask ChangeVisibility(bool visible, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60003B9")]
	[Address(RVA = "0x471F50", Offset = "0x470950", VA = "0x180471F50", Slot = "68")]
	public void AddMessage(PrintedMessage message)
	{
	}

	[Token(Token = "0x60003BA")]
	[Address(RVA = "0x4720F0", Offset = "0x470AF0", VA = "0x1804720F0", Slot = "69")]
	public void AppendText(LocalizableText text)
	{
	}

	[Token(Token = "0x60003BB")]
	[Address(RVA = "0x472770", Offset = "0x471170", VA = "0x180472770", Slot = "78")]
	[AsyncStateMachine(typeof(_003CReveal_003Ed__34))]
	public virtual UniTask Reveal(float delay, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x60003BC")]
	[Address(RVA = "0x472910", Offset = "0x471310", VA = "0x180472910", Slot = "48")]
	public override void Dispose()
	{
	}

	[Token(Token = "0x60003BD")]
	[Address(RVA = "0x472BF0", Offset = "0x4715F0", VA = "0x180472BF0", Slot = "79")]
	[AsyncStateMachine(typeof(_003CLoadUIPrefab_003Ed__36))]
	protected virtual UniTask<GameObject> LoadUIPrefab()
	{
		return default(UniTask<GameObject>);
	}

	[Token(Token = "0x60003BE")]
	[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "59")]
	protected override GameObject CreateHostObject()
	{
		return null;
	}

	[Token(Token = "0x60003BF")]
	[Address(RVA = "0x472D80", Offset = "0x471780", VA = "0x180472D80", Slot = "80")]
	protected virtual void SetRevealProgress(float value)
	{
	}

	[Token(Token = "0x60003C0")]
	[Address(RVA = "0x472DF0", Offset = "0x4717F0", VA = "0x180472DF0", Slot = "50")]
	protected override Vector3 GetBehaviourPosition()
	{
		return default(Vector3);
	}

	[Token(Token = "0x60003C1")]
	[Address(RVA = "0x4730D0", Offset = "0x471AD0", VA = "0x1804730D0", Slot = "51")]
	protected override void SetBehaviourPosition(Vector3 position)
	{
	}

	[Token(Token = "0x60003C2")]
	[Address(RVA = "0x473390", Offset = "0x471D90", VA = "0x180473390", Slot = "52")]
	protected override Quaternion GetBehaviourRotation()
	{
		return default(Quaternion);
	}

	[Token(Token = "0x60003C3")]
	[Address(RVA = "0x473660", Offset = "0x472060", VA = "0x180473660", Slot = "53")]
	protected override void SetBehaviourRotation(Quaternion rotation)
	{
	}

	[Token(Token = "0x60003C4")]
	[Address(RVA = "0x473900", Offset = "0x472300", VA = "0x180473900", Slot = "55")]
	protected override Vector3 GetBehaviourScale()
	{
		return default(Vector3);
	}

	[Token(Token = "0x60003C5")]
	[Address(RVA = "0x473BE0", Offset = "0x4725E0", VA = "0x180473BE0", Slot = "56")]
	protected override void SetBehaviourScale(Vector3 scale)
	{
	}

	[Token(Token = "0x60003C6")]
	[Address(RVA = "0x473E80", Offset = "0x472880", VA = "0x180473E80", Slot = "57")]
	protected override Color GetBehaviourTintColor()
	{
		return default(Color);
	}

	[Token(Token = "0x60003C7")]
	[Address(RVA = "0x473EE0", Offset = "0x4728E0", VA = "0x180473EE0", Slot = "58")]
	protected override void SetBehaviourTintColor(Color value)
	{
	}

	[Token(Token = "0x60003C8")]
	[Address(RVA = "0x473F40", Offset = "0x472940", VA = "0x180473F40", Slot = "81")]
	protected virtual void SetMessages(IReadOnlyList<PrintedMessage> messages)
	{
	}

	[Token(Token = "0x60003C9")]
	[Address(RVA = "0x474430", Offset = "0x472E30", VA = "0x180474430", Slot = "82")]
	protected virtual void SetTemplates(IReadOnlyList<MessageTemplate> templates)
	{
	}

	[Token(Token = "0x60003CA")]
	[Address(RVA = "0x4744C0", Offset = "0x472EC0", VA = "0x1804744C0", Slot = "83")]
	protected virtual void HandleAspectChanged(AspectMonitor monitor)
	{
	}

	[Token(Token = "0x60003CB")]
	[Address(RVA = "0x474520", Offset = "0x472F20", VA = "0x180474520", Slot = "84")]
	protected virtual void HandleLocaleChanged(LocaleChangedArgs _)
	{
	}

	[Token(Token = "0x60003CC")]
	[Address(RVA = "0x474570", Offset = "0x472F70", VA = "0x180474570", Slot = "85")]
	protected virtual void CancelRevealTextRoutine()
	{
	}
}
