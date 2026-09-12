using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;

namespace Naninovel.Async.Internal;

[Token(Token = "0x2000644")]
public class UniTaskSynchronizationContext : SynchronizationContext
{
	[StructLayout(3)]
	[Token(Token = "0x2000645")]
	private readonly struct Callback
	{
		[Token(Token = "0x40017EC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		private readonly SendOrPostCallback callback;

		[Token(Token = "0x40017ED")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		private readonly object state;

		[Token(Token = "0x6001F05")]
		[Address(RVA = "0x42B240", Offset = "0x429C40", VA = "0x18042B240")]
		public Callback(SendOrPostCallback callback, object state)
		{
		}

		[Token(Token = "0x6001F06")]
		[Address(RVA = "0x4CF8D0", Offset = "0x4CE2D0", VA = "0x1804CF8D0")]
		public void Invoke()
		{
		}
	}

	[Token(Token = "0x40017E3")]
	private const int MaxArrayLength = 2146435071;

	[Token(Token = "0x40017E4")]
	private const int InitialSize = 16;

	[Token(Token = "0x40017E5")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static SpinLock gate;

	[Token(Token = "0x40017E6")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x4")]
	private static bool dequing;

	[Token(Token = "0x40017E7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
	private static int actionListCount;

	[Token(Token = "0x40017E8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private static Callback[] actionList;

	[Token(Token = "0x40017E9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private static int waitingListCount;

	[Token(Token = "0x40017EA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private static Callback[] waitingList;

	[Token(Token = "0x40017EB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private static int opCount;

	[Token(Token = "0x6001EFD")]
	[Address(RVA = "0x4CEAB0", Offset = "0x4CD4B0", VA = "0x1804CEAB0", Slot = "4")]
	public override void Send(SendOrPostCallback d, object state)
	{
	}

	[Token(Token = "0x6001EFE")]
	[Address(RVA = "0x4CEAE0", Offset = "0x4CD4E0", VA = "0x1804CEAE0", Slot = "5")]
	public override void Post(SendOrPostCallback d, object state)
	{
	}

	[Token(Token = "0x6001EFF")]
	[Address(RVA = "0x4CF2B0", Offset = "0x4CDCB0", VA = "0x1804CF2B0", Slot = "6")]
	public override void OperationStarted()
	{
	}

	[Token(Token = "0x6001F00")]
	[Address(RVA = "0x4CF310", Offset = "0x4CDD10", VA = "0x1804CF310", Slot = "7")]
	public override void OperationCompleted()
	{
	}

	[Token(Token = "0x6001F01")]
	[Address(RVA = "0x437020", Offset = "0x435A20", VA = "0x180437020", Slot = "9")]
	public override SynchronizationContext CreateCopy()
	{
		return null;
	}

	[Token(Token = "0x6001F02")]
	[Address(RVA = "0x4CF370", Offset = "0x4CDD70", VA = "0x1804CF370")]
	internal static void Run()
	{
	}

	[Token(Token = "0x6001F03")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public UniTaskSynchronizationContext()
	{
	}
}
