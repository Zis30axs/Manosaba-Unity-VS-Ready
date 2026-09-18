using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.AddressablesUtils;
using GigaCreation.Essentials.Localization;
using Il2CppDummyDll;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000029")]
    public class FontMaterialChanger : MonoBehaviour
    {
    	[StructLayout(3)]
    	[Token(Token = "0x200002B")]
    	[CompilerGenerated]
    	private struct _003CChangeFontMaterialAsync_003Ed__10
    {
    		[Token(Token = "0x4000099")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400009A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400009B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public FontMaterialChanger _003C_003E4__this;

    		[Token(Token = "0x400009C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public TMP_Text label;

    		[Token(Token = "0x400009D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x400009E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private TMP_Text _003C_003E7__wrap1;

    		[Token(Token = "0x400009F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<Material>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60000C0")]
    		[Address(RVA = "0xCE38D0", Offset = "0xCE22D0", VA = "0x180CE38D0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000C1")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000092")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[SerializeField]
    	private FontMaterialData[] _fontMaterialData;

    	[Token(Token = "0x4000093")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private IAddressableAssetLoader _addressableAssetLoader;

    	[Token(Token = "0x4000094")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	private ILocalizationProvider _localizationProvider;

    	[Token(Token = "0x4000095")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private Dictionary<LocaleKind, AssetReferenceT<Material>> _materialReferences;

    	[Token(Token = "0x1700001C")]
    	private IAddressableAssetLoader AddressableAssetLoader
    	{
    		[Token(Token = "0x60000B7")]
    		[Address(RVA = "0xCE3180", Offset = "0xCE1B80", VA = "0x180CE3180")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700001D")]
    	private ILocalizationProvider LocalizationProvider
    	{
    		[Token(Token = "0x60000B8")]
    		[Address(RVA = "0xCE3240", Offset = "0xCE1C40", VA = "0x180CE3240")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700001E")]
    	private IReadOnlyDictionary<LocaleKind, AssetReferenceT<Material>> MaterialReferences
    	{
    		[Token(Token = "0x60000B9")]
    		[Address(RVA = "0xCE3300", Offset = "0xCE1D00", VA = "0x180CE3300")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60000BA")]
    	[Address(RVA = "0xCE3620", Offset = "0xCE2020", VA = "0x180CE3620")]
    	[AsyncStateMachine(typeof(_003CChangeFontMaterialAsync_003Ed__10))]
    	public UniTask ChangeFontMaterialAsync(TMP_Text label, [Optional] CancellationToken ct)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60000BB")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public FontMaterialChanger()
    	{
    	}
    }
}
