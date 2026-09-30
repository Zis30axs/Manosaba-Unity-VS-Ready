using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel
{
    [Token(Token = "0x20002E8")]
    public static class IOUtils
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20002E9")]
    	[CompilerGenerated]
    	private struct _003CReadFile_003Ed__0
    {
    		[Token(Token = "0x4000B35")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000B36")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<byte[]> _003C_003Et__builder;

    		[Token(Token = "0x4000B37")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string filePath;

    		[Token(Token = "0x4000B38")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private FileStream _003CfileStream_003E5__2;

    		[Token(Token = "0x4000B39")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private byte[] _003CfileData_003E5__3;

    		[Token(Token = "0x4000B3A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private object _003C_003E7__wrap3;

    		[Token(Token = "0x4000B3B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private int _003C_003E7__wrap4;

    		[Token(Token = "0x4000B3C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private byte[] _003C_003E7__wrap5;

    		[Token(Token = "0x4000B3D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    		private TaskAwaiter<int> _003C_003Eu__1;

    		[Token(Token = "0x4000B3E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    		private ValueTaskAwaiter _003C_003Eu__2;

    		[Token(Token = "0x6000DE3")]
    		[Address(RVA = "0x549B50", Offset = "0x548550", VA = "0x180549B50", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000DE4")]
    		[Address(RVA = "0x54A390", Offset = "0x548D90", VA = "0x18054A390", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20002EA")]
    	[CompilerGenerated]
    	private struct _003CReadTextFile_003Ed__2
    {
    		[Token(Token = "0x4000B3F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000B40")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder<string> _003C_003Et__builder;

    		[Token(Token = "0x4000B41")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string filePath;

    		[Token(Token = "0x4000B42")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private StreamReader _003Cstream_003E5__2;

    		[Token(Token = "0x4000B43")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private TaskAwaiter<string> _003C_003Eu__1;

    		[Token(Token = "0x6000DE5")]
    		[Address(RVA = "0x54A3C0", Offset = "0x548DC0", VA = "0x18054A3C0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000DE6")]
    		[Address(RVA = "0x54A990", Offset = "0x549390", VA = "0x18054A990", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20002EB")]
    	[CompilerGenerated]
    	private struct _003CWriteFile_003Ed__1
    {
    		[Token(Token = "0x4000B44")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000B45")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000B46")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public string filePath;

    		[Token(Token = "0x4000B47")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public byte[] fileData;

    		[Token(Token = "0x4000B48")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private FileStream _003CfileStream_003E5__2;

    		[Token(Token = "0x4000B49")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private object _003C_003E7__wrap2;

    		[Token(Token = "0x4000B4A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private int _003C_003E7__wrap3;

    		[Token(Token = "0x4000B4B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private TaskAwaiter _003C_003Eu__1;

    		[Token(Token = "0x4000B4C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private ValueTaskAwaiter _003C_003Eu__2;

    		[Token(Token = "0x6000DE7")]
    		[Address(RVA = "0x54A9C0", Offset = "0x5493C0", VA = "0x18054A9C0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000DE8")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20002EC")]
    	[CompilerGenerated]
    	private struct _003CWriteTextFile_003Ed__3
    {
    		[Token(Token = "0x4000B4D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000B4E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000B4F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public string filePath;

    		[Token(Token = "0x4000B50")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string fileText;

    		[Token(Token = "0x4000B51")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private StreamWriter _003Cstream_003E5__2;

    		[Token(Token = "0x4000B52")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private object _003C_003E7__wrap2;

    		[Token(Token = "0x4000B53")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private int _003C_003E7__wrap3;

    		[Token(Token = "0x4000B54")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private TaskAwaiter _003C_003Eu__1;

    		[Token(Token = "0x4000B55")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private ValueTaskAwaiter _003C_003Eu__2;

    		[Token(Token = "0x6000DE9")]
    		[Address(RVA = "0x54B0F0", Offset = "0x549AF0", VA = "0x18054B0F0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000DEA")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x6000DD9")]
    	[Address(RVA = "0x5492B0", Offset = "0x547CB0", VA = "0x1805492B0")]
    	[AsyncStateMachine(typeof(_003CReadFile_003Ed__0))]
    	public static UniTask<byte[]> ReadFile(string filePath)
    	{
    		return default(UniTask<byte[]>);
    	}

    	[Token(Token = "0x6000DDA")]
    	[Address(RVA = "0x549460", Offset = "0x547E60", VA = "0x180549460")]
    	[AsyncStateMachine(typeof(_003CWriteFile_003Ed__1))]
    	public static UniTask WriteFile(string filePath, byte[] fileData)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000DDB")]
    	[Address(RVA = "0x549600", Offset = "0x548000", VA = "0x180549600")]
    	[AsyncStateMachine(typeof(_003CReadTextFile_003Ed__2))]
    	public static UniTask<string> ReadTextFile(string filePath)
    	{
    		return default(UniTask<string>);
    	}

    	[Token(Token = "0x6000DDC")]
    	[Address(RVA = "0x549790", Offset = "0x548190", VA = "0x180549790")]
    	[AsyncStateMachine(typeof(_003CWriteTextFile_003Ed__3))]
    	public static UniTask WriteTextFile(string filePath, string fileText)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000DDD")]
    	[Address(RVA = "0x549930", Offset = "0x548330", VA = "0x180549930")]
    	public static void DeleteFile(string filePath)
    	{
    	}

    	[Token(Token = "0x6000DDE")]
    	[Address(RVA = "0x549940", Offset = "0x548340", VA = "0x180549940")]
    	public static void MoveFile(string sourceFilePath, string destFilePath)
    	{
    	}

    	[Token(Token = "0x6000DDF")]
    	[Address(RVA = "0x549980", Offset = "0x548380", VA = "0x180549980")]
    	public static void CreateDirectory(string path)
    	{
    	}

    	[Token(Token = "0x6000DE0")]
    	[Address(RVA = "0x549990", Offset = "0x548390", VA = "0x180549990")]
    	public static void DeleteDirectory(string path, bool recursive)
    	{
    	}

    	[Token(Token = "0x6000DE1")]
    	[Address(RVA = "0x549AE0", Offset = "0x5484E0", VA = "0x180549AE0")]
    	public static void EnsureDirectories(string path)
    	{
    	}

    	[Token(Token = "0x6000DE2")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public static void WebGLSyncFs()
    	{
    	}
    }
}
