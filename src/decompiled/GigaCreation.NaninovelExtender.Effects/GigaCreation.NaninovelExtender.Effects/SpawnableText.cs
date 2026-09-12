using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using GigaCreation.Tools.Ui;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using TMPro;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Effects;

[Token(Token = "0x2000019")]
public class SpawnableText : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IParameterized, DestroySpawned.IAwaitable
{
	[StructLayout(3)]
	[Token(Token = "0x200001B")]
	[CompilerGenerated]
	private struct _003CAwaitDestroy_003Ed__24
{
		[Token(Token = "0x400009F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000A0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000A1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnableText _003C_003E4__this;

		[Token(Token = "0x40000A2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x40000A3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000054")]
		[Address(RVA = "0xD0FB00", Offset = "0xD0E500", VA = "0x180D0FB00", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000055")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200001C")]
	[CompilerGenerated]
	private struct _003CAwaitSpawn_003Ed__22
{
		[Token(Token = "0x40000A4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000A5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000A6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpawnableText _003C_003E4__this;

		[Token(Token = "0x40000A7")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken asyncToken;

		[Token(Token = "0x40000A8")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private float _003CtimeRequestedToPresent_003E5__2;

		[Token(Token = "0x40000A9")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x40000AA")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000056")]
		[Address(RVA = "0xD0FE10", Offset = "0xD0E810", VA = "0x180D0FE10", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000057")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x200001D")]
	[CompilerGenerated]
	private struct _003CFadeAsync_003Ed__26
{
		[Token(Token = "0x40000AB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000AC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000AD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public float from;

		[Token(Token = "0x40000AE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x1C")]
		public float to;

		[Token(Token = "0x40000AF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public float time;

		[Token(Token = "0x40000B0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public SpawnableText _003C_003E4__this;

		[Token(Token = "0x40000B1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public AsyncToken asyncToken;

		[Token(Token = "0x40000B2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000058")]
		[Address(RVA = "0xD104F0", Offset = "0xD0EEF0", VA = "0x180D104F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000059")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400008D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("References")]
	[SerializeField]
	private RectTransform _rectTransform;

	[Token(Token = "0x400008E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[SerializeField]
	private Canvas _canvas;

	[Token(Token = "0x400008F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private RectTransform _contentRectTransform;

	[Token(Token = "0x4000090")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[SerializeField]
	private CanvasGroup _contentCanvasGroup;

	[Token(Token = "0x4000091")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[SerializeField]
	private AutoLayoutSupporter _autoLayoutSupporter;

	[Token(Token = "0x4000092")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[SerializeField]
	private TextMeshProUGUI _label;

	[Token(Token = "0x4000093")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[Header("Parameters")]
	[SerializeField]
	private float _defaultFontSize;

	[Token(Token = "0x4000094")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x54")]
	[SerializeField]
	private float _defaultPositionZ;

	[Token(Token = "0x4000095")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[SerializeField]
	private float _defaultDuration;

	[Token(Token = "0x4000096")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x5C")]
	[SerializeField]
	private EasingType _easingType;

	[Token(Token = "0x4000097")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	private readonly Tweener<FloatTween> _tweener;

	[Token(Token = "0x4000098")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private Camera _camera;

	[Token(Token = "0x4000099")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	private string _text;

	[Token(Token = "0x400009A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	private float _fontSize;

	[Token(Token = "0x400009B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x7C")]
	private Vector3 _position;

	[Token(Token = "0x400009C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x88")]
	private float _duration;

	[Token(Token = "0x400009D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8C")]
	private float _stopDuration;

	[Token(Token = "0x17000006")]
	private Camera Camera
	{
		[Token(Token = "0x6000049")]
		[Address(RVA = "0xD0E4D0", Offset = "0xD0CED0", VA = "0x180D0E4D0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x600004A")]
	[Address(RVA = "0xD0E5C0", Offset = "0xD0CFC0", VA = "0x180D0E5C0")]
	private void Awake()
	{
	}

	[Token(Token = "0x600004B")]
	[Address(RVA = "0xD0E5F0", Offset = "0xD0CFF0", VA = "0x180D0E5F0")]
	private void Reset()
	{
	}

	[Token(Token = "0x600004C")]
	[Address(RVA = "0xD0E7A0", Offset = "0xD0D1A0", VA = "0x180D0E7A0", Slot = "4")]
	public void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
	{
	}

	[Token(Token = "0x600004D")]
	[Address(RVA = "0xD0F2A0", Offset = "0xD0DCA0", VA = "0x180D0F2A0", Slot = "8")]
	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__22))]
	public virtual Naninovel.UniTask AwaitSpawn([Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x600004E")]
	[Address(RVA = "0xD0F430", Offset = "0xD0DE30", VA = "0x180D0F430", Slot = "6")]
	public void SetDestroyParameters(IReadOnlyList<string> parameters)
	{
	}

	[Token(Token = "0x600004F")]
	[Address(RVA = "0xD0F600", Offset = "0xD0E000", VA = "0x180D0F600", Slot = "7")]
	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__24))]
	public Naninovel.UniTask AwaitDestroy([Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000050")]
	[Address(RVA = "0xD0F780", Offset = "0xD0E180", VA = "0x180D0F780")]
	protected void CompleteTweenerIfRunning()
	{
	}

	[Token(Token = "0x6000051")]
	[Address(RVA = "0xD0F820", Offset = "0xD0E220", VA = "0x180D0F820")]
	[AsyncStateMachine(typeof(_003CFadeAsync_003Ed__26))]
	protected Naninovel.UniTask FadeAsync(float from, float to, float time, [Optional] AsyncToken asyncToken)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000052")]
	[Address(RVA = "0xD0FA00", Offset = "0xD0E400", VA = "0x180D0FA00")]
	protected void SetAlpha(float a)
	{
	}

	[Token(Token = "0x6000053")]
	[Address(RVA = "0xD0FA20", Offset = "0xD0E420", VA = "0x180D0FA20")]
	public SpawnableText()
	{
	}
}
