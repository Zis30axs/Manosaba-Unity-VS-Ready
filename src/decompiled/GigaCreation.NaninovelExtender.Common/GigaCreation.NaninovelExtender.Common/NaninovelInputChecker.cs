using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Common;

[Token(Token = "0x2000015")]
public class NaninovelInputChecker : MonoBehaviour
{
	[Token(Token = "0x2000016")]
	private class Logger
	{
		[Token(Token = "0x17000004")]
		public string InputName
		{
			[Token(Token = "0x600002A")]
			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
			[CompilerGenerated]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x600002B")]
		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
		public Logger(string inputName)
		{
		}

		[Token(Token = "0x600002C")]
		[Address(RVA = "0xCFAFF0", Offset = "0xCF99F0", VA = "0x180CFAFF0")]
		public void LogInputNameAsStarted()
		{
		}

		[Token(Token = "0x600002D")]
		[Address(RVA = "0xCFB080", Offset = "0xCF9A80", VA = "0x180CFB080")]
		public void LogInputNameAsEnded()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000018")]
	[CompilerGenerated]
	private struct _003CInitializeAsync_003Ed__4
{
		[Token(Token = "0x4000040")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000041")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000042")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CancellationToken ct;

		[Token(Token = "0x4000043")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public NaninovelInputChecker _003C_003E4__this;

		[Token(Token = "0x4000044")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000032")]
		[Address(RVA = "0xCFB2A0", Offset = "0xCF9CA0", VA = "0x180CFB2A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000033")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400003A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[SerializeField]
	private bool _enable;

	[Token(Token = "0x400003B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private Logger[] _loggers;

	[Token(Token = "0x6000026")]
	[Address(RVA = "0xCFA6F0", Offset = "0xCF90F0", VA = "0x180CFA6F0")]
	private void Start()
	{
	}

	[Token(Token = "0x6000027")]
	[Address(RVA = "0xCFA8A0", Offset = "0xCF92A0", VA = "0x180CFA8A0")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000028")]
	[Address(RVA = "0xCFAE50", Offset = "0xCF9850", VA = "0x180CFAE50")]
	[AsyncStateMachine(typeof(_003CInitializeAsync_003Ed__4))]
	private UniTask InitializeAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000029")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public NaninovelInputChecker()
	{
	}
}
