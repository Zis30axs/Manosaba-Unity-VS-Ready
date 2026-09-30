using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;

namespace Naninovel.FX
{
    [Token(Token = "0x20005E9")]
    public class DepthOfField : MonoBehaviour, Spawn.IParameterized, Spawn.IAwaitable, DestroySpawned.IParameterized, DestroySpawned.IAwaitable
    {
    	[Token(Token = "0x20005EA")]
    	private class CameraComponent : MonoBehaviour
    	{
    		[Token(Token = "0x20005EB")]
    		private enum KernelSizeType
    		{
    			[Token(Token = "0x40016DF")]
    			Small,
    			[Token(Token = "0x40016E0")]
    			Medium,
    			[Token(Token = "0x40016E1")]
    			Large,
    			[Token(Token = "0x40016E2")]
    			VeryLarge
    		}

    		[Token(Token = "0x40016D9")]
    		private const KernelSizeType kernelSize = KernelSizeType.Medium;

    		[Token(Token = "0x40016DA")]
    		private const float fNumber = 1.4f;

    		[Token(Token = "0x40016DB")]
    		private const float filmHeight = 0.024f;

    		[Token(Token = "0x40016DC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private Camera targetCamera;

    		[Token(Token = "0x40016DD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private Material material;

    		[Token(Token = "0x17000460")]
    		public Transform PointOfFocus
    		{
    			[Token(Token = "0x6001DA5")]
    			[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    			[CompilerGenerated]
    			get
    			{
    				return null;
    			}
    			[Token(Token = "0x6001DA6")]
    			[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x17000461")]
    		public float FocusDistance
    		{
    			[Token(Token = "0x6001DA7")]
    			[Address(RVA = "0x398CF0", Offset = "0x3976F0", VA = "0x180398CF0")]
    			[CompilerGenerated]
    			get
    			{
    				return default(float);
    			}
    			[Token(Token = "0x6001DA8")]
    			[Address(RVA = "0x3A6BA0", Offset = "0x3A55A0", VA = "0x1803A6BA0")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x17000462")]
    		public bool UseCameraFov
    		{
    			[Token(Token = "0x6001DA9")]
    			[Address(RVA = "0x4B99C0", Offset = "0x4B83C0", VA = "0x1804B99C0")]
    			[CompilerGenerated]
    			get
    			{
    				return default(bool);
    			}
    			[Token(Token = "0x6001DAA")]
    			[Address(RVA = "0x4B99D0", Offset = "0x4B83D0", VA = "0x1804B99D0")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x17000463")]
    		public float FocalLength
    		{
    			[Token(Token = "0x6001DAB")]
    			[Address(RVA = "0x398D10", Offset = "0x397710", VA = "0x180398D10")]
    			[CompilerGenerated]
    			get
    			{
    				return default(float);
    			}
    			[Token(Token = "0x6001DAC")]
    			[Address(RVA = "0x4B7100", Offset = "0x4B5B00", VA = "0x1804B7100")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x6001DAD")]
    		[Address(RVA = "0x4B99E0", Offset = "0x4B83E0", VA = "0x1804B99E0")]
    		private void OnEnable()
    		{
    		}

    		[Token(Token = "0x6001DAE")]
    		[Address(RVA = "0x4B9C50", Offset = "0x4B8650", VA = "0x1804B9C50")]
    		private void OnDestroy()
    		{
    		}

    		[Token(Token = "0x6001DAF")]
    		[Address(RVA = "0x4B9C60", Offset = "0x4B8660", VA = "0x1804B9C60")]
    		private void OnRenderImage(RenderTexture source, RenderTexture destination)
    		{
    		}

    		[Token(Token = "0x6001DB0")]
    		[Address(RVA = "0x4BA1D0", Offset = "0x4B8BD0", VA = "0x1804BA1D0")]
    		private float CalculateFocalLength()
    		{
    			return default(float);
    		}

    		[Token(Token = "0x6001DB1")]
    		[Address(RVA = "0x4BA230", Offset = "0x4B8C30", VA = "0x1804BA230")]
    		private float CalculateMaxCoCRadius(int screenHeight)
    		{
    			return default(float);
    		}

    		[Token(Token = "0x6001DB2")]
    		[Address(RVA = "0x4BA260", Offset = "0x4B8C60", VA = "0x1804BA260")]
    		private void SetUpShaderParameters(RenderTexture source)
    		{
    		}

    		[Token(Token = "0x6001DB3")]
    		[Address(RVA = "0x4BA740", Offset = "0x4B9140", VA = "0x1804BA740")]
    		public CameraComponent()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005EC")]
    	[CompilerGenerated]
    	private struct _003CAwaitDestroy_003Ed__31
    {
    		[Token(Token = "0x40016E3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40016E4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40016E5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public DepthOfField _003C_003E4__this;

    		[Token(Token = "0x40016E6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x40016E7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001DB4")]
    		[Address(RVA = "0x4BA790", Offset = "0x4B9190", VA = "0x1804BA790", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001DB5")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20005ED")]
    	[CompilerGenerated]
    	private struct _003CAwaitSpawn_003Ed__29
    {
    		[Token(Token = "0x40016E8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40016E9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40016EA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public DepthOfField _003C_003E4__this;

    		[Token(Token = "0x40016EB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x40016EC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6001DB6")]
    		[Address(RVA = "0x4BACD0", Offset = "0x4B96D0", VA = "0x1804BACD0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6001DB7")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40016C9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[SerializeField]
    	private float defaultFocusDistance;

    	[Token(Token = "0x40016CA")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
    	[SerializeField]
    	private float defaultFocalLength;

    	[Token(Token = "0x40016CB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[SerializeField]
    	private float defaultDuration;

    	[Token(Token = "0x40016CC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private static readonly int blurTexId;

    	[Token(Token = "0x40016CD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x4")]
    	private static readonly int distanceId;

    	[Token(Token = "0x40016CE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    	private static readonly int lensCoeffId;

    	[Token(Token = "0x40016CF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xC")]
    	private static readonly int maxCoCId;

    	[Token(Token = "0x40016D0")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private static readonly int rcpMaxCoCId;

    	[Token(Token = "0x40016D1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x14")]
    	private static readonly int rcpAspectId;

    	[Token(Token = "0x40016D2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	private readonly Tweener<FloatTween> focusDistanceTweener;

    	[Token(Token = "0x40016D3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private readonly Tweener<FloatTween> focalLengthTweener;

    	[Token(Token = "0x40016D4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private CameraComponent cameraComponent;

    	[Token(Token = "0x1700045C")]
    	protected float FocusDistance
    	{
    		[Token(Token = "0x6001D94")]
    		[Address(RVA = "0x4B8670", Offset = "0x4B7070", VA = "0x1804B8670")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001D95")]
    		[Address(RVA = "0x4B8680", Offset = "0x4B7080", VA = "0x1804B8680")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700045D")]
    	protected float FocalLength
    	{
    		[Token(Token = "0x6001D96")]
    		[Address(RVA = "0x4B8690", Offset = "0x4B7090", VA = "0x1804B8690")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001D97")]
    		[Address(RVA = "0x4B86A0", Offset = "0x4B70A0", VA = "0x1804B86A0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700045E")]
    	protected float Duration
    	{
    		[Token(Token = "0x6001D98")]
    		[Address(RVA = "0x398CF0", Offset = "0x3976F0", VA = "0x180398CF0")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001D99")]
    		[Address(RVA = "0x3A6BA0", Offset = "0x3A55A0", VA = "0x1803A6BA0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x1700045F")]
    	protected float StopDuration
    	{
    		[Token(Token = "0x6001D9A")]
    		[Address(RVA = "0x398D00", Offset = "0x397700", VA = "0x180398D00")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6001D9B")]
    		[Address(RVA = "0x4B70F0", Offset = "0x4B5AF0", VA = "0x1804B70F0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x6001D9C")]
    	[Address(RVA = "0x4B86B0", Offset = "0x4B70B0", VA = "0x1804B86B0", Slot = "8")]
    	public virtual void SetSpawnParameters(IReadOnlyList<string> parameters, bool asap)
    	{
    	}

    	[Token(Token = "0x6001D9D")]
    	[Address(RVA = "0x4B90D0", Offset = "0x4B7AD0", VA = "0x1804B90D0", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CAwaitSpawn_003Ed__29))]
    	public UniTask AwaitSpawn([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001D9E")]
    	[Address(RVA = "0x4B9250", Offset = "0x4B7C50", VA = "0x1804B9250", Slot = "6")]
    	public void SetDestroyParameters(IReadOnlyList<string> parameters)
    	{
    	}

    	[Token(Token = "0x6001D9F")]
    	[Address(RVA = "0x4B9420", Offset = "0x4B7E20", VA = "0x1804B9420", Slot = "7")]
    	[AsyncStateMachine(typeof(_003CAwaitDestroy_003Ed__31))]
    	public UniTask AwaitDestroy([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6001DA0")]
    	[Address(RVA = "0x4B95A0", Offset = "0x4B7FA0", VA = "0x1804B95A0")]
    	private void ApplyFocusDistance(float value)
    	{
    	}

    	[Token(Token = "0x6001DA1")]
    	[Address(RVA = "0x4B95C0", Offset = "0x4B7FC0", VA = "0x1804B95C0")]
    	private void ApplyFocalLength(float value)
    	{
    	}

    	[Token(Token = "0x6001DA2")]
    	[Address(RVA = "0x4B95E0", Offset = "0x4B7FE0", VA = "0x1804B95E0")]
    	private void OnDestroy()
    	{
    	}

    	[Token(Token = "0x6001DA3")]
    	[Address(RVA = "0x4B9710", Offset = "0x4B8110", VA = "0x1804B9710")]
    	public DepthOfField()
    	{
    	}
    }
}
