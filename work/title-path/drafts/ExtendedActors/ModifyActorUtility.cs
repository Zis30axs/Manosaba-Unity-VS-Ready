using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.AddressablesUtils;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.ExtendedActors
{
    [Token(Token = "0x2000039")]
    public class ModifyActorUtility<TActor, TManager> where TActor : class, IActor where TManager : class, IMultipliableActorManager<TActor>
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200003A")]
    	[CompilerGenerated]
    	private struct _003CGetDissolveTexture_003Ed__6
    {
    		[Token(Token = "0x40000BD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000BE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<Texture2D> _003C_003Et__builder;

    		[Token(Token = "0x40000BF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public ModifyActorUtility<TActor, TManager> _003C_003E4__this;

    		[Token(Token = "0x40000C0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string name;

    		[Token(Token = "0x40000C1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public CancellationToken ct;

    		[Token(Token = "0x40000C2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private Cysharp.Threading.Tasks.UniTask<Texture2D>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60000C4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000C5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200003B")]
    	[CompilerGenerated]
    	private struct _003CGetOrAddActor_003Ed__9
    {
    		[Token(Token = "0x40000C3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000C4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<TActor> _003C_003Et__builder;

    		[Token(Token = "0x40000C5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public ModifyActorUtility<TActor, TManager> _003C_003E4__this;

    		[Token(Token = "0x40000C6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string actorId;

    		[Token(Token = "0x40000C7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string subId;

    		[Token(Token = "0x40000C8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private Naninovel.UniTask<TActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60000C6")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000C7")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200003C")]
    	[CompilerGenerated]
    	private struct _003CPreloadResources_003Ed__7
    {
    		[Token(Token = "0x40000C9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000CA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000CB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public ModifyActorUtility<TActor, TManager> _003C_003E4__this;

    		[Token(Token = "0x40000CC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string actorId;

    		[Token(Token = "0x40000CD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string subId;

    		[Token(Token = "0x40000CE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public string appearance;

    		[Token(Token = "0x40000CF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public object holder;

    		[Token(Token = "0x40000D0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private Naninovel.UniTask<TActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x40000D1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private Naninovel.UniTask<Resource>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60000C8")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000C9")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40000BA")]
    	private const string AddressToDissolveTextures = "General/Transitions/";

    	[Token(Token = "0x40000BB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly TManager _multipliableActorManager;

    	[Token(Token = "0x40000BC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private IAddressableAssetLoader _addressableAssetLoader;

    	// Generic class: the shared instantiation ModifyActorUtility<object, object> carries the code (RVAs below are its
    	// script.json entries).  Annotated disassembly: work/title-path/disas/mau_*.txt

    	// RECONSTRUCTED | Confidence: A | RVA: 0x86BA20, 186 B
    	// Lazy service lookup: `_addressableAssetLoader` (+0x18) is filled from ServiceLocator.Get<IAddressablesManager>()
    	// (0x180751910 with the IAddressablesManager MethodInfo) on first use and cached (GC write barrier at 0x18086BAA5).
    	[Token(Token = "0x17000013")]
    	private IAddressableAssetLoader AddressableAssetLoader
    	{
    		[Token(Token = "0x60000BC")]
    		get
    		{
    			return _addressableAssetLoader ??= GigaCreation.Tools.Service.ServiceLocator.Get<IAddressablesManager>();
    		}
    	}

    	// RECONSTRUCTED | Confidence: B | RVA: 0x86BAE0, 209 B
    	// `manager as TManager`: the rgctx type is read from the generic context ([r8+0x20] -> [+0xC0] -> [+8]) and passed
    	// to the cast helper 0x180298020 (returns null when the object is not a TManager), then stored at +0x10.  The helper
    	// call is emitted twice with the same type (the second store re-writes the same field with the GC card mark at
    	// 0x18086BB5B); a single `as` assignment is the behaviour-preserving spelling.
    	[Token(Token = "0x60000BD")]
    	public ModifyActorUtility(IActorManager manager)
    	{
    		_multipliableActorManager = manager as TManager;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x86BBC0 (kickoff) / 0x9E7300 (<GetDissolveTexture>d__6.MoveNext, 1098 B)
    	// MoveNext state -1: AddressableAssetLoader (the lazy getter, inlined) ->
    	//   String.Concat("General/Transitions/" (STR), name [+0x28])   0x1809E74D0
    	//   IAddressableAssetLoader.GetOrLoadAddressableAsset<Texture2D>(address, ct [+0x30])   0x1809E74F1 (interface thunk 0x180003310)
    	//   Cysharp UniTask<Texture2D>.GetAwaiter, await, SetResult(texture)
    	[Token(Token = "0x60000BE")]
    	[AsyncStateMachine(typeof(ModifyActorUtility<, >._003CGetDissolveTexture_003Ed__6))]
    	public Naninovel.UniTask<Texture2D> GetDissolveTexture(string name, [Optional] CancellationToken ct)
    	{
    		return GetDissolveTextureCore(name, ct);
    	}

    	private async Naninovel.UniTask<Texture2D> GetDissolveTextureCore(string name, CancellationToken ct)
    	{
    		return await AddressableAssetLoader.GetOrLoadAddressableAsset<Texture2D>(AddressToDissolveTextures + name, ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x86BE20 (kickoff) / 0xA06560 (<PreloadResources>d__7.MoveNext, 1073 B)
    	// State -1: await GetOrAddActor(actorId [+0x20], subId [+0x28])   (0x180A06668, this.GetOrAddActor below)
    	// then, on `_multipliableActorManager` (+0x10; a null one is THROW NRE at 0x180A06982, no guard):
    	//   0x180A0679D  loader = ((IActorManager)manager).GetAppearanceLoader(actorId)   (interface thunk 0x180003140, slot 12)
    	//   loader != null && appearance [+0x30] non-empty ([rcx+0x10] > 0)
    	//   0x180A067DA  await ResourceLoaderExtensions.LoadOrErr(loader, appearance, holder [+0x38])
    	//   otherwise complete.
    	[Token(Token = "0x60000BF")]
    	[AsyncStateMachine(typeof(ModifyActorUtility<, >._003CPreloadResources_003Ed__7))]
    	public Naninovel.UniTask PreloadResources(string actorId, string subId, string appearance, object holder)
    	{
    		return PreloadResourcesCore(actorId, subId, appearance, holder);
    	}

    	private async Naninovel.UniTask PreloadResourcesCore(string actorId, string subId, string appearance, object holder)
    	{
    		await GetOrAddActor(actorId, subId);
    		IResourceLoader loader = _multipliableActorManager.GetAppearanceLoader(actorId);
    		if (loader != null && !string.IsNullOrEmpty(appearance))
    		{
    			await loader.LoadOrErr(appearance, holder);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x86C0A0, 169 B
    	// `_multipliableActorManager` (+0x10) null -> nothing (0x18086C0F9 skips to the epilogue, unlike Preload's NRE).
    	//   0x18086C10A  loader = manager.GetAppearanceLoader(id)     (thunk 0x180003140, IActorManager slot 12)
    	//   loader != null && appearance non-empty ([rbx+0x10] > 0)
    	//   0x18086C12F  loader.Release(appearance, holder)            (thunk 0x180021C30; the trailing byte 1 is the
    	//                default `unload = true` of IResourceLoader.Release)
    	[Token(Token = "0x60000C0")]
    	public void ReleaseResources(string id, string appearance, object holder)
    	{
    		if (_multipliableActorManager == null)
    		{
    			return;
    		}
    		IResourceLoader loader = _multipliableActorManager.GetAppearanceLoader(id);
    		if (loader != null && !string.IsNullOrEmpty(appearance))
    		{
    			loader.Release(appearance, holder);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x86C150 (kickoff) / 0x9EC2F0 (<GetOrAddActor>d__9.MoveNext, 727 B)
    	// State -1: MultipliableActorManagerExtensions.GetOrAddActor<TActor>(this._multipliableActorManager [+0x10],
    	// actorId [+0x28], subId [+0x30])  (0x1809EC380, shared instantiation 0x1807166F0); GetAwaiter, await, SetResult.
    	[Token(Token = "0x60000C1")]
    	[AsyncStateMachine(typeof(ModifyActorUtility<, >._003CGetOrAddActor_003Ed__9))]
    	public Naninovel.UniTask<TActor> GetOrAddActor(string actorId, string subId)
    	{
    		return GetOrAddActorCore(actorId, subId);
    	}

    	private async Naninovel.UniTask<TActor> GetOrAddActorCore(string actorId, string subId)
    	{
    		return await MultipliableActorManagerExtensions.GetOrAddActor<TActor>(_multipliableActorManager, actorId, subId);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x86C380, 675 B
    	// The package's ModifyActor.ApplyAppearanceModification transition block, factored out:
    	//   0x18086C3F5  StringParameter.op_Implicit(string -> StringParameter) (0x1805ABE50), TransitionUtils.ResolveParameterValue
    	//                -> transitionName;  TransitionUtils.GetDefaultParams(transitionName) -> defaults (Vector4, xmm6)
    	//   0x18086C468  ICommandParameter.HasValue(paramList): assigned -> new Vector4(ElementAtOrNull(0..3) ?? defaults.xyzw)
    	//                (NullableFloat.op_Implicit at 0x180526BA0 supplies the default), otherwise defaults
    	//   0x18086C608  new Transition(transitionName, Nullable<Vector4>(parameters), dissolveTexture [stack arg])
    	[Token(Token = "0x60000C2")]
    	public Transition BuildAppearanceTransition(string transition, DecimalListParameter paramList, Texture2D dissolveTexture)
    	{
    		string transitionName = TransitionUtils.ResolveParameterValue(transition);
    		Vector4 defaultParams = TransitionUtils.GetDefaultParams(transitionName);
    		Vector4 transitionParams = paramList != null && paramList.HasValue
    			? new Vector4(
    				paramList.ElementAtOrNull(0) ?? defaultParams.x,
    				paramList.ElementAtOrNull(1) ?? defaultParams.y,
    				paramList.ElementAtOrNull(2) ?? defaultParams.z,
    				paramList.ElementAtOrNull(3) ?? defaultParams.w)
    			: defaultParams;
    		return new Transition(transitionName, transitionParams, dissolveTexture);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x86C630, 588 B
    	//   0x18086C6F0  rect = CameraConfigurationUtility.EvaluateSceneRect(new bool?(matchScreenWidth))   (xmm6 = x,y,w,h)
    	//   0x18086C704  position = new float?[3]
    	//   [0] ElementAtOrNull(0) != null ? scenePosition[0] / 100f * rect.width  + rect.x : null     (shufps 0xAA = width)
    	//   [1] ElementAtOrNull(1) != null ? scenePosition[1] / 100f * rect.height + rect.y : null     (0xFF = height, 0x55 = y)
    	//   [2] scenePosition.ElementAtOrNull(2)   (NullableFloat.op_Implicit -> float?)
    	// 100.0f = data_2AE2BC4.  A null `scenePosition` is the THROW NRE at 0x18086C86B.
    	[Token(Token = "0x60000C3")]
    	public float?[] EvaluateWorldPosition(DecimalListParameter scenePosition, bool matchScreenWidth)
    	{
    		Rect sceneRect = GigaCreation.NaninovelExtender.Cameras.CameraConfigurationUtility.EvaluateSceneRect(matchScreenWidth);
    		float?[] position = new float?[3];
    		position[0] = scenePosition.ElementAtOrNull(0) != null ? scenePosition[0] / 100f * sceneRect.width + sceneRect.x : (float?)null;
    		position[1] = scenePosition.ElementAtOrNull(1) != null ? scenePosition[1] / 100f * sceneRect.height + sceneRect.y : (float?)null;
    		position[2] = scenePosition.ElementAtOrNull(2);
    		return position;
    	}
    }
}
