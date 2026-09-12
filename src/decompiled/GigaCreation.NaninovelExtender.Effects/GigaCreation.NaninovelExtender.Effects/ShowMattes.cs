using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace GigaCreation.NaninovelExtender.Effects;

[Token(Token = "0x2000007")]
[UsedImplicitly]
public class ShowMattes : Command, Command.IPreloadable
{
	[StructLayout(3)]
	[Token(Token = "0x2000008")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__12
{
		[Token(Token = "0x400001E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400001F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000020")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ShowMattes _003C_003E4__this;

		[Token(Token = "0x4000021")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x4000022")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<SpawnedObject>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000023")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000011")]
		[Address(RVA = "0xD05B00", Offset = "0xD04500", VA = "0x180D05B00", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000012")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000009")]
	[CompilerGenerated]
	private struct _003CPreloadResources_003Ed__10
{
		[Token(Token = "0x4000024")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000025")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000026")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ShowMattes _003C_003E4__this;

		[Token(Token = "0x4000027")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000013")]
		[Address(RVA = "0xD06190", Offset = "0xD04B90", VA = "0x180D06190", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000014")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000016")]
	private const string MattesName = "Mattes";

	[Token(Token = "0x4000017")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly]
	public StringListParameter Width;

	[Token(Token = "0x4000018")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly]
	[ParameterAlias("posZ")]
	public StringParameter PositionZ;

	[Token(Token = "0x4000019")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly]
	[ParameterAlias("tint")]
	public StringParameter TintColor;

	[Token(Token = "0x400001A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[UsedImplicitly]
	[ParameterAlias("time")]
	public StringListParameter Duration;

	[Token(Token = "0x400001B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[UsedImplicitly]
	[ParameterAlias("easing")]
	public StringParameter EasingTypeName;

	[Token(Token = "0x400001C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[UsedImplicitly]
	public StringParameter SubId;

	[Token(Token = "0x400001D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private ISpawnManager _spawnManager;

	[Token(Token = "0x17000001")]
	private ISpawnManager SpawnManager
	{
		[Token(Token = "0x600000B")]
		[Address(RVA = "0xD04D80", Offset = "0xD03780", VA = "0x180D04D80")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600000C")]
	[Address(RVA = "0xD04E40", Offset = "0xD03840", VA = "0x180D04E40", Slot = "16")]
	[AsyncStateMachine(typeof(_003CPreloadResources_003Ed__10))]
	public UniTask PreloadResources()
	{
		return default(UniTask);
	}

	[Token(Token = "0x600000D")]
	[Address(RVA = "0xD04F60", Offset = "0xD03960", VA = "0x180D04F60", Slot = "17")]
	public void ReleaseResources()
	{
	}

	[Token(Token = "0x600000E")]
	[Address(RVA = "0xD04FE0", Offset = "0xD039E0", VA = "0x180D04FE0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__12))]
	public override UniTask Execute([Optional] AsyncToken asyncToken)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600000F")]
	[Address(RVA = "0xD05170", Offset = "0xD03B70", VA = "0x180D05170")]
	private IReadOnlyList<string> GetParameters()
	{
		return null;
	}

	[Token(Token = "0x6000010")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public ShowMattes()
	{
	}
}
