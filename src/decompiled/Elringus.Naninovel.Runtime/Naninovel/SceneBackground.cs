using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.FX;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Naninovel;

[Token(Token = "0x20000A9")]
public class SceneBackground : MonoBehaviourActor<BackgroundMetadata>, IBackgroundActor, IActor, Blur.IBlurable
{
	[Token(Token = "0x20000AA")]
	protected class SceneData
	{
		[Token(Token = "0x4000266")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public Scene Scene;

		[Token(Token = "0x4000267")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public RenderTexture RenderTexture;

		[Token(Token = "0x4000268")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Camera Camera;

		[Token(Token = "0x4000269")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public readonly HashSet<object> Holders;

		[Token(Token = "0x6000344")]
		[Address(RVA = "0x46A8F0", Offset = "0x4692F0", VA = "0x18046A8F0")]
		public SceneData()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000AB")]
	[CompilerGenerated]
	private struct _003CChangeAppearance_003Ed__19
{
		[Token(Token = "0x400026A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400026B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400026C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SceneBackground _003C_003E4__this;

		[Token(Token = "0x400026D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string appearance;

		[Token(Token = "0x400026E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public AsyncToken token;

		[Token(Token = "0x400026F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public Tween tween;

		[Token(Token = "0x4000270")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		public Transition? transition;

		[Token(Token = "0x4000271")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
		private string _003CpreviousAppearance_003E5__2;

		[Token(Token = "0x4000272")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
		private UniTask<SceneData>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000273")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000345")]
		[Address(RVA = "0x46A9B0", Offset = "0x4693B0", VA = "0x18046A9B0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000346")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000AC")]
	[CompilerGenerated]
	private struct _003CChangeVisibility_003Ed__20
{
		[Token(Token = "0x4000274")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000275")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000276")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SceneBackground _003C_003E4__this;

		[Token(Token = "0x4000277")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public bool visible;

		[Token(Token = "0x4000278")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
		public Tween tween;

		[Token(Token = "0x4000279")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken token;

		[Token(Token = "0x400027A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000347")]
		[Address(RVA = "0x46B230", Offset = "0x469C30", VA = "0x18046B230", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000348")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000AD")]
	[CompilerGenerated]
	private struct _003CGetOrLoadScene_003Ed__26
{
		[Token(Token = "0x400027B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400027C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<SceneData> _003C_003Et__builder;

		[Token(Token = "0x400027D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x400027E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string appearance;

		[Token(Token = "0x400027F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public SceneBackground _003C_003E4__this;

		[Token(Token = "0x4000280")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private RenderTexture _003CrenderTexture_003E5__2;

		[Token(Token = "0x4000281")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000282")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private UniTask<Scene>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000349")]
		[Address(RVA = "0x46B580", Offset = "0x469F80", VA = "0x18046B580", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600034A")]
		[Address(RVA = "0x46BEA0", Offset = "0x46A8A0", VA = "0x18046BEA0", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000AE")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__17
{
		[Token(Token = "0x4000283")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000284")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000285")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SceneBackground _003C_003E4__this;

		[Token(Token = "0x4000286")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600034B")]
		[Address(RVA = "0x46BED0", Offset = "0x46A8D0", VA = "0x18046BED0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600034C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000AF")]
	[CompilerGenerated]
	private struct _003CLoadScene_003Ed__28
{
		[Token(Token = "0x4000287")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000288")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<Scene> _003C_003Et__builder;

		[Token(Token = "0x4000289")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public SceneBackground _003C_003E4__this;

		[Token(Token = "0x400028A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string appearance;

		[Token(Token = "0x400028B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken token;

		[Token(Token = "0x400028C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private string _003CscenePath_003E5__2;

		[Token(Token = "0x400028D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UnityAsyncExtensions.AsyncOperationAwaiter _003C_003Eu__1;

		[Token(Token = "0x600034D")]
		[Address(RVA = "0x46C310", Offset = "0x46AD10", VA = "0x18046C310", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600034E")]
		[Address(RVA = "0x46C890", Offset = "0x46B290", VA = "0x18046C890", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000261")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly Dictionary<string, SceneData> loadedScenes;

	[Token(Token = "0x4000262")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
	private static readonly Semaphore loadSemaphore;

	[Token(Token = "0x4000263")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private BackgroundMatcher matcher;

	[Token(Token = "0x4000264")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
	private string appearance;

	[Token(Token = "0x4000265")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
	private bool visible;

	[Token(Token = "0x1700008C")]
	public override string Appearance
	{
		[Token(Token = "0x600032B")]
		[Address(RVA = "0x468E20", Offset = "0x467820", VA = "0x180468E20", Slot = "27")]
		get
		{
			return null;
		}
		[Token(Token = "0x600032C")]
		[Address(RVA = "0x468E30", Offset = "0x467830", VA = "0x180468E30", Slot = "28")]
		set
		{
		}
	}

	[Token(Token = "0x1700008D")]
	public override bool Visible
	{
		[Token(Token = "0x600032D")]
		[Address(RVA = "0x468E50", Offset = "0x467850", VA = "0x180468E50", Slot = "29")]
		get
		{
			return default(bool);
		}
		[Token(Token = "0x600032E")]
		[Address(RVA = "0x468E60", Offset = "0x467860", VA = "0x180468E60", Slot = "30")]
		set
		{
		}
	}

	[Token(Token = "0x1700008E")]
	protected virtual TransitionalRenderer TransitionalRenderer
	{
		[Token(Token = "0x600032F")]
		[Address(RVA = "0x468E80", Offset = "0x467880", VA = "0x180468E80", Slot = "63")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x6000330")]
		[Address(RVA = "0x468E90", Offset = "0x467890", VA = "0x180468E90")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x6000331")]
	[Address(RVA = "0x468EF0", Offset = "0x4678F0", VA = "0x180468EF0")]
	public SceneBackground(string id, BackgroundMetadata meta)
	{
	}

	[Token(Token = "0x6000332")]
	[Address(RVA = "0x468F50", Offset = "0x467950", VA = "0x180468F50", Slot = "41")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__17))]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000333")]
	[Address(RVA = "0x469070", Offset = "0x467A70", VA = "0x180469070", Slot = "64")]
	public virtual UniTask Blur(float intensity, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000334")]
	[Address(RVA = "0x469100", Offset = "0x467B00", VA = "0x180469100", Slot = "42")]
	[AsyncStateMachine(typeof(_003CChangeAppearance_003Ed__19))]
	public override UniTask ChangeAppearance(string appearance, Tween tween, [Optional] Transition? transition, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000335")]
	[Address(RVA = "0x469390", Offset = "0x467D90", VA = "0x180469390", Slot = "43")]
	[AsyncStateMachine(typeof(_003CChangeVisibility_003Ed__20))]
	public override UniTask ChangeVisibility(bool visible, Tween tween, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000336")]
	[Address(RVA = "0x469550", Offset = "0x467F50", VA = "0x180469550", Slot = "48")]
	public override void Dispose()
	{
	}

	[Token(Token = "0x6000337")]
	[Address(RVA = "0x469820", Offset = "0x468220", VA = "0x180469820", Slot = "65")]
	protected virtual void SetAppearance(string appearance)
	{
	}

	[Token(Token = "0x6000338")]
	[Address(RVA = "0x4698B0", Offset = "0x4682B0", VA = "0x1804698B0", Slot = "66")]
	protected virtual void SetVisibility(bool visible)
	{
	}

	[Token(Token = "0x6000339")]
	[Address(RVA = "0x469920", Offset = "0x468320", VA = "0x180469920", Slot = "57")]
	protected override Color GetBehaviourTintColor()
	{
		return default(Color);
	}

	[Token(Token = "0x600033A")]
	[Address(RVA = "0x469980", Offset = "0x468380", VA = "0x180469980", Slot = "58")]
	protected override void SetBehaviourTintColor(Color tintColor)
	{
	}

	[Token(Token = "0x600033B")]
	[Address(RVA = "0x469A40", Offset = "0x468440", VA = "0x180469A40", Slot = "67")]
	[AsyncStateMachine(typeof(_003CGetOrLoadScene_003Ed__26))]
	protected virtual UniTask<SceneData> GetOrLoadScene(string appearance, AsyncToken token)
	{
		return default(UniTask<SceneData>);
	}

	[Token(Token = "0x600033C")]
	[Address(RVA = "0x469C90", Offset = "0x468690", VA = "0x180469C90", Slot = "68")]
	protected virtual RenderTexture CreateRenderTexture()
	{
		return null;
	}

	[Token(Token = "0x600033D")]
	[Address(RVA = "0x469EC0", Offset = "0x4688C0", VA = "0x180469EC0", Slot = "69")]
	[AsyncStateMachine(typeof(_003CLoadScene_003Ed__28))]
	protected virtual UniTask<Scene> LoadScene(string appearance, AsyncToken token)
	{
		return default(UniTask<Scene>);
	}

	[Token(Token = "0x600033E")]
	[Address(RVA = "0x46A110", Offset = "0x468B10", VA = "0x18046A110", Slot = "70")]
	protected virtual void UnloadScene(string appearance)
	{
	}

	[Token(Token = "0x600033F")]
	[Address(RVA = "0x46A310", Offset = "0x468D10", VA = "0x18046A310", Slot = "71")]
	protected virtual Camera FindCameraInScene(Scene scene)
	{
		return null;
	}

	[Token(Token = "0x6000340")]
	[Address(RVA = "0x46A590", Offset = "0x468F90", VA = "0x18046A590")]
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void ResetStaticData()
	{
	}
}
