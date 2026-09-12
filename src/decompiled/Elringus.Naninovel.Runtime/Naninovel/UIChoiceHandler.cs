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

[Token(Token = "0x20000BB")]
[ActorResources(typeof(ChoiceHandlerPanel), false)]
public class UIChoiceHandler : MonoBehaviourActor<ChoiceHandlerMetadata>
{
	[Token(Token = "0x20000BC")]
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass34_0
	{
		[Token(Token = "0x40002C9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public ChoiceState choice;

		[Token(Token = "0x600039C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public _003C_003Ec__DisplayClass34_0()
		{
		}

		[Token(Token = "0x600039D")]
		[Address(RVA = "0x46EF30", Offset = "0x46D930", VA = "0x18046EF30")]
		internal bool _003CHandleChoice_003Eb__0(ChoiceState c)
		{
			return default(bool);
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000BE")]
	[CompilerGenerated]
	private struct _003CChangeVisibility_003Ed__25
{
		[Token(Token = "0x40002CB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002CC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40002CD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public UIChoiceHandler _003C_003E4__this;

		[Token(Token = "0x40002CE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public bool visible;

		[Token(Token = "0x40002CF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public Tween tween;

		[Token(Token = "0x40002D0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60003A0")]
		[Address(RVA = "0x46F0D0", Offset = "0x46DAD0", VA = "0x18046F0D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60003A1")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000BF")]
	[CompilerGenerated]
	private struct _003CHandleChoice_003Ed__34
{
		[Token(Token = "0x40002D1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002D2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40002D3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public ChoiceState choice;

		[Token(Token = "0x40002D4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
		public UIChoiceHandler _003C_003E4__this;

		[Token(Token = "0x40002D5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
		private _003C_003Ec__DisplayClass34_0 _003C_003E8__1;

		[Token(Token = "0x40002D6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
		private CancellationTokenSource _003Ccts_003E5__2;

		[Token(Token = "0x40002D7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60003A2")]
		[Address(RVA = "0x46F4A0", Offset = "0x46DEA0", VA = "0x18046F4A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60003A3")]
		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000C0")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__23
{
		[Token(Token = "0x40002D8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002D9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40002DA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public UIChoiceHandler _003C_003E4__this;

		[Token(Token = "0x40002DB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40002DC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<GameObject>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x40002DD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask<IManagedUI>.Awaiter _003C_003Eu__3;

		[Token(Token = "0x60003A4")]
		[Address(RVA = "0x4707A0", Offset = "0x46F1A0", VA = "0x1804707A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60003A5")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000C1")]
	[CompilerGenerated]
	private struct _003CLoadUIPrefab_003Ed__30
{
		[Token(Token = "0x40002DE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002DF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<GameObject> _003C_003Et__builder;

		[Token(Token = "0x40002E0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public UIChoiceHandler _003C_003E4__this;

		[Token(Token = "0x40002E1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<Resource<GameObject>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60003A6")]
		[Address(RVA = "0x471210", Offset = "0x46FC10", VA = "0x180471210", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60003A7")]
		[Address(RVA = "0x471600", Offset = "0x470000", VA = "0x180471600", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40002C5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	private readonly IChoiceHandlerManager handlers;

	[Token(Token = "0x40002C6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
	private readonly IScriptPlayer player;

	[Token(Token = "0x40002C7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
	private readonly IStateManager state;

	[Token(Token = "0x40002C8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
	private readonly IUIManager uis;

	[Token(Token = "0x17000094")]
	public override GameObject GameObject
	{
		[Token(Token = "0x6000382")]
		[Address(RVA = "0x46D400", Offset = "0x46BE00", VA = "0x18046D400", Slot = "39")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000095")]
	public override string Appearance
	{
		[Token(Token = "0x6000383")]
		[Address(RVA = "0x468E80", Offset = "0x467880", VA = "0x180468E80", Slot = "27")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000384")]
		[Address(RVA = "0x468E90", Offset = "0x467890", VA = "0x180468E90", Slot = "28")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x17000096")]
	public override bool Visible
	{
		[Token(Token = "0x6000385")]
		[Address(RVA = "0x46D430", Offset = "0x46BE30", VA = "0x18046D430", Slot = "29")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x6000386")]
		[Address(RVA = "0x46D470", Offset = "0x46BE70", VA = "0x18046D470", Slot = "30")]
		set
		{
		}
	}

	[Token(Token = "0x17000097")]
	private IReadOnlyList<ChoiceState> Naninovel_002EIChoiceHandlerActor_002EChoices
	{
		[Token(Token = "0x6000387")]
		[Address(RVA = "0x46D4C0", Offset = "0x46BEC0", VA = "0x18046D4C0", Slot = "62")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000098")]
	protected virtual ChoiceHandlerPanel HandlerPanel
	{
		[Token(Token = "0x6000388")]
		[Address(RVA = "0x46D4E0", Offset = "0x46BEE0", VA = "0x18046D4E0", Slot = "66")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000389")]
		[Address(RVA = "0x46D4F0", Offset = "0x46BEF0", VA = "0x18046D4F0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000099")]
	protected virtual List<ChoiceState> Choices
	{
		[Token(Token = "0x600038A")]
		[Address(RVA = "0x468E20", Offset = "0x467820", VA = "0x180468E20", Slot = "67")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600038B")]
	[Address(RVA = "0x46D550", Offset = "0x46BF50", VA = "0x18046D550")]
	public UIChoiceHandler(string id, ChoiceHandlerMetadata meta)
	{
	}

	[Token(Token = "0x600038C")]
	[Address(RVA = "0x46D850", Offset = "0x46C250", VA = "0x18046D850", Slot = "41")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__23))]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x600038D")]
	[Address(RVA = "0x46D980", Offset = "0x46C380", VA = "0x18046D980", Slot = "42")]
	public override UniTask ChangeAppearance(string appearance, Tween tween, [Optional] Transition? transition, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600038E")]
	[Address(RVA = "0x46D9D0", Offset = "0x46C3D0", VA = "0x18046D9D0", Slot = "43")]
	[AsyncStateMachine(typeof(_003CChangeVisibility_003Ed__25))]
	public override UniTask ChangeVisibility(bool visible, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600038F")]
	[Address(RVA = "0x46DB20", Offset = "0x46C520", VA = "0x18046DB20", Slot = "68")]
	public virtual void AddChoice(ChoiceState choice)
	{
	}

	[Token(Token = "0x6000390")]
	[Address(RVA = "0x46DCA0", Offset = "0x46C6A0", VA = "0x18046DCA0", Slot = "69")]
	public virtual void RemoveChoice(string id)
	{
	}

	[Token(Token = "0x6000391")]
	[Address(RVA = "0x46DE80", Offset = "0x46C880", VA = "0x18046DE80", Slot = "70")]
	public virtual void HandleChoice(string id)
	{
	}

	[Token(Token = "0x6000392")]
	[Address(RVA = "0x46E130", Offset = "0x46CB30", VA = "0x18046E130", Slot = "48")]
	public override void Dispose()
	{
	}

	[Token(Token = "0x6000393")]
	[Address(RVA = "0x46E330", Offset = "0x46CD30", VA = "0x18046E330", Slot = "71")]
	[AsyncStateMachine(typeof(_003CLoadUIPrefab_003Ed__30))]
	protected virtual UniTask<GameObject> LoadUIPrefab()
	{
		return default(UniTask<GameObject>);
	}

	[Token(Token = "0x6000394")]
	[Address(RVA = "0x33E0C0", Offset = "0x33CAC0", VA = "0x18033E0C0", Slot = "59")]
	protected override GameObject CreateHostObject()
	{
		return null;
	}

	[Token(Token = "0x6000395")]
	[Address(RVA = "0x37E310", Offset = "0x37CD10", VA = "0x18037E310", Slot = "57")]
	protected override Color GetBehaviourTintColor()
	{
		return default(Color);
	}

	[Token(Token = "0x6000396")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "58")]
	protected override void SetBehaviourTintColor(Color tintColor)
	{
	}

	[Token(Token = "0x6000397")]
	[Address(RVA = "0x46E4C0", Offset = "0x46CEC0", VA = "0x18046E4C0", Slot = "72")]
	[AsyncStateMachine(typeof(_003CHandleChoice_003Ed__34))]
	protected virtual void HandleChoice(ChoiceState choice)
	{
	}

	[Token(Token = "0x6000398")]
	[Address(RVA = "0x46E6A0", Offset = "0x46D0A0", VA = "0x18046E6A0", Slot = "73")]
	protected virtual void AddChoiceToBacklog(ChoiceState state)
	{
	}

	[Token(Token = "0x6000399")]
	[Address(RVA = "0x46E900", Offset = "0x46D300", VA = "0x18046E900", Slot = "74")]
	protected virtual void NavigateToNested(PlaybackSpot hostedAt)
	{
	}

	[Token(Token = "0x600039A")]
	[Address(RVA = "0x46EC80", Offset = "0x46D680", VA = "0x18046EC80", Slot = "75")]
	protected virtual void ClearChoices()
	{
	}
}
