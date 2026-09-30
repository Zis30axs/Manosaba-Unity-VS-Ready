using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using TMPro;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000378")]
    [InitializeAtRuntime(0, null)]
    public class CommunityLocalization : ICommunityLocalization, IEngineService
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000379")]
    	[CompilerGenerated]
    	private struct _003CEject_003Ed__32
    {
    		[Token(Token = "0x4000CFD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000CFE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000CFF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CommunityLocalization _003C_003E4__this;

    		[Token(Token = "0x4000D00")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private LocalizationEjector _003Cejector_003E5__2;

    		[Token(Token = "0x4000D01")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60010D9")]
    		[Address(RVA = "0x57C530", Offset = "0x57AF30", VA = "0x18057C530", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60010DA")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200037A")]
    	[CompilerGenerated]
    	private struct _003CInitializeService_003Ed__25
    {
    		[Token(Token = "0x4000D02")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000D03")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000D04")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CommunityLocalization _003C_003E4__this;

    		[Token(Token = "0x4000D05")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private UniTask<(string Author, string Font)>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60010DB")]
    		[Address(RVA = "0x57D040", Offset = "0x57BA40", VA = "0x18057D040", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60010DC")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200037B")]
    	[CompilerGenerated]
    	private struct _003CResolveInfo_003Ed__33
    {
    		[Token(Token = "0x4000D06")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000D07")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<(string Author, string Font)> _003C_003Et__builder;

    		[Token(Token = "0x4000D08")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CommunityLocalization _003C_003E4__this;

    		[Token(Token = "0x4000D09")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<string>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60010DD")]
    		[Address(RVA = "0x57D590", Offset = "0x57BF90", VA = "0x18057D590", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60010DE")]
    		[Address(RVA = "0x57D9D0", Offset = "0x57C3D0", VA = "0x18057D9D0", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000CFB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private readonly IResourceProviderManager resources;

    	[Token(Token = "0x4000CFC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	private ResourceLoader<TextAsset> textLoader;

    	[Token(Token = "0x1700023D")]
    	public virtual bool Active
    	{
    		[Token(Token = "0x60010C4")]
    		[Address(RVA = "0x44D320", Offset = "0x44BD20", VA = "0x18044D320", Slot = "11")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x60010C5")]
    		[Address(RVA = "0x30C130", Offset = "0x30AB30", VA = "0x18030C130")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700023E")]
    	public virtual string Author
    	{
    		[Token(Token = "0x60010C6")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "12")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60010C7")]
    		[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700023F")]
    	public IResourceLoader<TextAsset> TextLoader
    	{
    		[Token(Token = "0x60010C8")]
    		[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0", Slot = "6")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000240")]
    	protected virtual string Root
    	{
    		[Token(Token = "0x60010C9")]
    		[Address(RVA = "0x57B420", Offset = "0x579E20", VA = "0x18057B420", Slot = "13")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000241")]
    	protected virtual string InfoFilePath
    	{
    		[Token(Token = "0x60010CA")]
    		[Address(RVA = "0x57B4C0", Offset = "0x579EC0", VA = "0x18057B4C0", Slot = "14")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000242")]
    	protected virtual string TextFolder
    	{
    		[Token(Token = "0x60010CB")]
    		[Address(RVA = "0x57B540", Offset = "0x579F40", VA = "0x18057B540", Slot = "15")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000243")]
    	protected virtual string ScriptFolder
    	{
    		[Token(Token = "0x60010CC")]
    		[Address(RVA = "0x57B5C0", Offset = "0x579FC0", VA = "0x18057B5C0", Slot = "16")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000244")]
    	protected virtual string FontFileName
    	{
    		[Token(Token = "0x60010CD")]
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "17")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60010CE")]
    		[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x60010CF")]
    	[Address(RVA = "0x57B640", Offset = "0x57A040", VA = "0x18057B640")]
    	public CommunityLocalization(IResourceProviderManager resources)
    	{
    	}

    	[Token(Token = "0x60010D0")]
    	[Address(RVA = "0x57B790", Offset = "0x57A190", VA = "0x18057B790", Slot = "18")]
    	[AsyncStateMachine(typeof(_003CInitializeService_003Ed__25))]
    	public virtual UniTask InitializeService()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60010D1")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "19")]
    	public virtual void ResetService()
    	{
    	}

    	[Token(Token = "0x60010D2")]
    	[Address(RVA = "0x57B8B0", Offset = "0x57A2B0", VA = "0x18057B8B0", Slot = "20")]
    	public virtual void DestroyService()
    	{
    	}

    	[Token(Token = "0x60010D3")]
    	[Address(RVA = "0x57B950", Offset = "0x57A350", VA = "0x18057B950", Slot = "21")]
    	protected virtual ResourceLoader<TextAsset> CreateTextLoader()
    	{
    		return null;
    	}

    	[Token(Token = "0x60010D4")]
    	[Address(RVA = "0x57BB50", Offset = "0x57A550", VA = "0x18057BB50", Slot = "22")]
    	public virtual UniTask<TMP_FontAsset> LoadFont()
    	{
    		return default(UniTask<TMP_FontAsset>);
    	}

    	[Token(Token = "0x60010D5")]
    	[Address(RVA = "0x57BF90", Offset = "0x57A990", VA = "0x18057BF90", Slot = "23")]
    	protected virtual bool IsPlatformSupported()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60010D6")]
    	[Address(RVA = "0x57C100", Offset = "0x57AB00", VA = "0x18057C100", Slot = "24")]
    	protected virtual bool IsEjectionRequested()
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60010D7")]
    	[Address(RVA = "0x57C270", Offset = "0x57AC70", VA = "0x18057C270", Slot = "25")]
    	[AsyncStateMachine(typeof(_003CEject_003Ed__32))]
    	protected virtual UniTask Eject()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60010D8")]
    	[Address(RVA = "0x57C390", Offset = "0x57AD90", VA = "0x18057C390", Slot = "26")]
    	[AsyncStateMachine(typeof(_003CResolveInfo_003Ed__33))]
    	protected virtual UniTask<(string, string)> ResolveInfo()
    	{
    		return default(UniTask<(string, string)>);
    	}
    }
}
