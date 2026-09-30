using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel.Commands
{
    [Token(Token = "0x2000659")]
    [CommandAlias("animate")]
    public class AnimateActor : Command
    {
    	[Token(Token = "0x200065A")]
    	[CompilerGenerated]
    	private sealed class _003C_003Ec__DisplayClass18_0
    	{
    		[Token(Token = "0x4001822")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public SpawnedObject[] spawned;

    		[Token(Token = "0x6001F53")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public _003C_003Ec__DisplayClass18_0()
    		{
    		}

    		[Token(Token = "0x6001F54")]
    		[Address(RVA = "0x4D26E0", Offset = "0x4D10E0", VA = "0x1804D26E0")]
    		internal UniTask _003CExecute_003Eb__0(AsyncToken token)
    		{
    			return default(UniTask);
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200065C")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__18
    {
    		[Token(Token = "0x4001824")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001825")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001826")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AnimateActor _003C_003E4__this;

    		[Token(Token = "0x4001827")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4001828")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private _003C_003Ec__DisplayClass18_0 _003C_003E8__1;

    		[Token(Token = "0x4001829")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private PooledObject<List<UniTask<SpawnedObject>>> _003C__003E5__2;

    		[Token(Token = "0x400182A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask<SpawnedObject[]>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400182B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6001F57")]
    		[Address(RVA = "0x4D2870", Offset = "0x4D1270", VA = "0x1804D2870", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001F58")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200065D")]
    	[CompilerGenerated]
    	private struct _003CSpawn_003Ed__19
    {
    		[Token(Token = "0x400182C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400182D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<SpawnedObject> _003C_003Et__builder;

    		[Token(Token = "0x400182E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string actorId;

    		[Token(Token = "0x400182F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public ISpawnManager spawner;

    		[Token(Token = "0x4001830")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		public AsyncToken token;

    		[Token(Token = "0x4001831")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		public AnimateActor _003C_003E4__this;

    		[Token(Token = "0x4001832")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask<SpawnedObject>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001F59")]
    		[Address(RVA = "0x4D3530", Offset = "0x4D1F30", VA = "0x1804D3530", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001F5A")]
    		[Address(RVA = "0x4D3850", Offset = "0x4D2250", VA = "0x1804D3850", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4001811")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	public static readonly char[] KeyDelimiters;

    	[Token(Token = "0x4001812")]
    	public const string prefabPath = "Animate";

    	[Token(Token = "0x4001813")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	public StringListParameter ActorIds;

    	[Token(Token = "0x4001814")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[ParameterDefaultValue("false")]
    	public BooleanParameter Loop;

    	[Token(Token = "0x4001815")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	public StringParameter Appearance;

    	[Token(Token = "0x4001816")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	public StringParameter Transition;

    	[Token(Token = "0x4001817")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	public StringParameter Visibility;

    	[Token(Token = "0x4001818")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	[ParameterAlias("posX")]
    	public StringParameter ScenePositionX;

    	[Token(Token = "0x4001819")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	[ParameterAlias("posY")]
    	public StringParameter ScenePositionY;

    	[Token(Token = "0x400181A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
    	[ParameterAlias("posZ")]
    	public StringParameter PositionZ;

    	[Token(Token = "0x400181B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
    	public StringParameter Rotation;

    	[Token(Token = "0x400181C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
    	public StringParameter Scale;

    	[Token(Token = "0x400181D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
    	[ParameterAlias("tint")]
    	public StringParameter TintColor;

    	[Token(Token = "0x400181E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
    	[ParameterAlias("easing")]
    	public StringParameter EasingTypeName;

    	[Token(Token = "0x400181F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
    	[ParameterAlias("time")]
    	public StringParameter Duration;

    	[Token(Token = "0x4001820")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
    	public BooleanParameter Wait;

    	[Token(Token = "0x4001821")]
    	private const string defaultDuration = "0.35";

    	[Token(Token = "0x6001F4D")]
    	[Address(RVA = "0x4D18E0", Offset = "0x4D02E0", VA = "0x1804D18E0")]
    	public static string BuildSpawnPath(string actorId)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001F4E")]
    	[Address(RVA = "0x314C70", Offset = "0x313670", VA = "0x180314C70", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__18))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001F4F")]
    	[Address(RVA = "0x4D1930", Offset = "0x4D0330", VA = "0x1804D1930", Slot = "16")]
    	[AsyncStateMachine(typeof(_003CSpawn_003Ed__19))]
    	protected virtual UniTask<SpawnedObject> Spawn(ISpawnManager spawner, string actorId, AsyncToken token)
    	{
    		return default(UniTask<SpawnedObject>);
    	}

    	[Token(Token = "0x6001F50")]
    	[Address(RVA = "0x4D1BE0", Offset = "0x4D05E0", VA = "0x1804D1BE0", Slot = "17")]
    	protected virtual IReadOnlyList<string> GetParametersForActor(string actorId)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001F51")]
    	[Address(RVA = "0x4D2580", Offset = "0x4D0F80", VA = "0x1804D2580")]
    	public AnimateActor()
    	{
    	}
    }
}
