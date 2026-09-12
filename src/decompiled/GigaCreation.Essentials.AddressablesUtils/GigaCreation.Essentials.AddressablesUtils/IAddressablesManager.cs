using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Essentials.AddressablesUtils;

[Token(Token = "0x2000020")]
public interface IAddressablesManager : IContentCatalogManager, IAddressableResourceLocationProvider, IAddressableAssetLoader, IService, IDisposable
{
	[StructLayout(3)]
	[Token(Token = "0x2000021")]
	[CompilerGenerated]
	private struct _003CInitializeAsync_003Ed__4
{
		[Token(Token = "0x400007D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400007E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400007F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CancellationToken ct;

		[Token(Token = "0x4000080")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask<UnityEngine.AddressableAssets.ResourceLocators.IResourceLocator>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600005E")]
		[Address(RVA = "0xCC7C70", Offset = "0xCC6670", VA = "0x180CC7C70", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600005F")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x1700000C")]
	static UniTaskCompletionSource InitializeUcs
	{
		[Token(Token = "0x600005A")]
		[Address(RVA = "0xCC7980", Offset = "0xCC6380", VA = "0x180CC7980")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x600005B")]
		[Address(RVA = "0xCC79C0", Offset = "0xCC63C0", VA = "0x180CC79C0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x600005C")]
	[Address(RVA = "0xCC7A60", Offset = "0xCC6460", VA = "0x180CC7A60")]
	[AsyncStateMachine(typeof(_003CInitializeAsync_003Ed__4))]
	static UniTask InitializeAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600005D")]
	[Address(RVA = "0xCC7BA0", Offset = "0xCC65A0", VA = "0x180CC7BA0")]
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	static void ResetStaticProperties()
	{
	}
}
