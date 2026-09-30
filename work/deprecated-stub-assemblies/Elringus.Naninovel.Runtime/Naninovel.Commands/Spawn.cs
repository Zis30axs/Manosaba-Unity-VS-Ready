using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x200070E")]
    public class Spawn : Command, Command.IPreloadable
    {
    	[Token(Token = "0x200070F")]
    	public interface IParameterized
    	{
    		[Token(Token = "0x6002197")]
    		void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap);
    	}

    	[Token(Token = "0x2000710")]
    	public interface IAwaitable
    	{
    		[Token(Token = "0x6002198")]
    		UniTask AwaitSpawn([Optional] AsyncToken token);
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000711")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__13
    {
    		[Token(Token = "0x4001AF8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001AF9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001AFA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Spawn _003C_003E4__this;

    		[Token(Token = "0x4001AFB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001AFC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<SpawnedObject>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4001AFD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6002199")]
    		[Address(RVA = "0x50A8C0", Offset = "0x5092C0", VA = "0x18050A8C0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600219A")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000712")]
    	[CompilerGenerated]
    	private struct _003CPreloadResources_003Ed__11
    {
    		[Token(Token = "0x4001AFE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001AFF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001B00")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Spawn _003C_003E4__this;

    		[Token(Token = "0x4001B01")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600219B")]
    		[Address(RVA = "0x50AED0", Offset = "0x5098D0", VA = "0x18050AED0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600219C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001AF1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	[ResourceContext("Spawn", -1, null)]
    	public StringParameter Path;

    	[Token(Token = "0x4001AF2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	public StringListParameter Params;

    	[Token(Token = "0x4001AF3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[ParameterAlias("pos")]
    	[VectorContext("X,Y,Z", -1, null)]
    	public DecimalListParameter ScenePosition;

    	[Token(Token = "0x4001AF4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[VectorContext("X,Y,Z", -1, null)]
    	public DecimalListParameter Position;

    	[Token(Token = "0x4001AF5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[VectorContext("X,Y,Z", -1, null)]
    	public DecimalListParameter Rotation;

    	[Token(Token = "0x4001AF6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	[VectorContext("X,Y,Z", -1, null)]
    	public DecimalListParameter Scale;

    	[Token(Token = "0x4001AF7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	public BooleanParameter Wait;

    	[Token(Token = "0x170004E8")]
    	protected virtual ISpawnManager SpawnManager
    	{
    		[Token(Token = "0x600218D")]
    		[Address(RVA = "0x509030", Offset = "0x507A30", VA = "0x180509030", Slot = "18")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x600218E")]
    	[Address(RVA = "0x509090", Offset = "0x507A90", VA = "0x180509090", Slot = "19")]
    	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__11))]
    	public virtual UniTask PreloadResources()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x600218F")]
    	[Address(RVA = "0x5091B0", Offset = "0x507BB0", VA = "0x1805091B0", Slot = "20")]
    	public virtual void ReleaseResources()
    	{
    	}

    	[Token(Token = "0x6002190")]
    	[Address(RVA = "0x509330", Offset = "0x507D30", VA = "0x180509330", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__13))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6002191")]
    	[Address(RVA = "0x5094C0", Offset = "0x507EC0", VA = "0x1805094C0", Slot = "21")]
    	protected virtual void ApplyParameters(SpawnedObject spawn)
    	{
    	}

    	[Token(Token = "0x6002192")]
    	[Address(RVA = "0x509500", Offset = "0x507F00", VA = "0x180509500", Slot = "22")]
    	protected virtual void ApplyScenePosition(SpawnedObject spawn)
    	{
    	}

    	[Token(Token = "0x6002193")]
    	[Address(RVA = "0x509AD0", Offset = "0x5084D0", VA = "0x180509AD0", Slot = "23")]
    	protected virtual void ApplyPosition(SpawnedObject spawn)
    	{
    	}

    	[Token(Token = "0x6002194")]
    	[Address(RVA = "0x509F20", Offset = "0x508920", VA = "0x180509F20", Slot = "24")]
    	protected virtual void ApplyRotation(SpawnedObject spawn)
    	{
    	}

    	[Token(Token = "0x6002195")]
    	[Address(RVA = "0x50A250", Offset = "0x508C50", VA = "0x18050A250", Slot = "25")]
    	protected virtual void ApplyScale(SpawnedObject spawn)
    	{
    	}

    	[Token(Token = "0x6002196")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public Spawn()
    	{
    	}
    }
}
