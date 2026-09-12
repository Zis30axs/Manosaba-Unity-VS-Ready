using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using GigaCreation.Essentials.AddressablesUtils;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace GigaCreation.NaninovelExtender.ResourceProvider;

[Token(Token = "0x2000004")]
[UsedImplicitly]
public class AddressableResourceProviderExtended : AddressableResourceProvider
{
	[StructLayout(3)]
	[Token(Token = "0x2000005")]
	[CompilerGenerated]
	private struct _003CLoadAllLocations_003Ed__4
{
		[Token(Token = "0x4000007")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000008")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<List<IResourceLocation>> _003C_003Et__builder;

		[Token(Token = "0x4000009")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AddressableResourceProviderExtended _003C_003E4__this;

		[Token(Token = "0x400000A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private Cysharp.Threading.Tasks.UniTask<List<IResourceLocation>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000006")]
		[Address(RVA = "0xD33A70", Offset = "0xD32470", VA = "0x180D33A70", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000007")]
		[Address(RVA = "0xD33F20", Offset = "0xD32920", VA = "0x180D33F20", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000006")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private IAddressableResourceLocationProvider _addressableResourceLocationProvider;

	[Token(Token = "0x17000001")]
	private IAddressableResourceLocationProvider AddressableResourceLocationProvider
	{
		[Token(Token = "0x6000003")]
		[Address(RVA = "0xD337D0", Offset = "0xD321D0", VA = "0x180D337D0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000004")]
	[Address(RVA = "0xD33890", Offset = "0xD32290", VA = "0x180D33890")]
	public AddressableResourceProviderExtended()
	{
	}

	[Token(Token = "0x6000005")]
	[Address(RVA = "0xD338E0", Offset = "0xD322E0", VA = "0x180D338E0", Slot = "56")]
	[AsyncStateMachine(typeof(_003CLoadAllLocations_003Ed__4))]
	protected override Naninovel.UniTask<List<IResourceLocation>> LoadAllLocations()
	{
		return default(Naninovel.UniTask<List<IResourceLocation>>);
	}
}
