using Il2CppDummyDll;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace PPT.Noise
{
    [Token(Token = "0x2000004")]
    [RequireComponent(typeof(RawImage))]
    [ExecuteInEditMode]
    public class Noise : MonoBehaviour
    {
    	[Token(Token = "0x2000005")]
    	[BurstCompile(CompileSynchronously = true)]
    	private struct NoiseJob : IJobParallelFor
    	{
    		[Token(Token = "0x4000013")]
    		[FieldOffset(Offset = "0x0")]
    		public int pixWidth;

    		[Token(Token = "0x4000014")]
    		[FieldOffset(Offset = "0x4")]
    		public float invWidth;

    		[Token(Token = "0x4000015")]
    		[FieldOffset(Offset = "0x8")]
    		public float invHeight;

    		[Token(Token = "0x4000016")]
    		[FieldOffset(Offset = "0xC")]
    		public float invDepth;

    		[Token(Token = "0x4000017")]
    		[FieldOffset(Offset = "0x10")]
    		public Vector3 scale;

    		[Token(Token = "0x4000018")]
    		[FieldOffset(Offset = "0x1C")]
    		public float velocityZ;

    		[Token(Token = "0x4000019")]
    		[FieldOffset(Offset = "0x20")]
    		public float2 originRC;

    		[Token(Token = "0x400001A")]
    		[FieldOffset(Offset = "0x28")]
    		public float originZO;

    		[Token(Token = "0x400001B")]
    		[FieldOffset(Offset = "0x30")]
    		[ReadOnly]
    		public NativeArray<Color32> gradientColors;

    		[Token(Token = "0x400001C")]
    		[FieldOffset(Offset = "0x40")]
    		[WriteOnly]
    		public NativeArray<Color32> result;

    		[Token(Token = "0x600000A")]
    		[Address(RVA = "0x1238EE0", Offset = "0x12378E0", VA = "0x181238EE0", Slot = "4")]
    		public void Execute(int idx)
    		{
    		}
    	}

    	[Token(Token = "0x4000006")]
    	[FieldOffset(Offset = "0x20")]
    	[Header("Size for the noise generation")]
    	public int pixWidth;

    	[Token(Token = "0x4000007")]
    	[FieldOffset(Offset = "0x24")]
    	public int pixHeight;

    	[Token(Token = "0x4000008")]
    	[FieldOffset(Offset = "0x28")]
    	public int depth;

    	[Token(Token = "0x4000009")]
    	[FieldOffset(Offset = "0x2C")]
    	[Header("Noise values")]
    	public Vector3 scale;

    	[Token(Token = "0x400000A")]
    	[FieldOffset(Offset = "0x38")]
    	public Vector3 velocity;

    	[Token(Token = "0x400000B")]
    	[FieldOffset(Offset = "0x44")]
    	[Header("For Radial movement")]
    	public int radi;

    	[Token(Token = "0x400000C")]
    	[FieldOffset(Offset = "0x48")]
    	[Header("For the Noise color")]
    	public bool useColorOne;

    	[Token(Token = "0x400000D")]
    	[FieldOffset(Offset = "0x4C")]
    	public Color uniqueColor;

    	[Token(Token = "0x400000E")]
    	[FieldOffset(Offset = "0x60")]
    	public Gradient colorGradient;

    	[Token(Token = "0x400000F")]
    	[FieldOffset(Offset = "0x68")]
    	private Texture2D noiseTex;

    	[Token(Token = "0x4000010")]
    	[FieldOffset(Offset = "0x70")]
    	private float cachedTime;

    	[Token(Token = "0x4000011")]
    	[FieldOffset(Offset = "0x78")]
    	private NativeArray<Color32> jobResult;

    	[Token(Token = "0x4000012")]
    	[FieldOffset(Offset = "0x88")]
    	private NativeArray<Color32> gradientColors;

    	[Token(Token = "0x6000003")]
    	[Address(RVA = "0x12380F0", Offset = "0x1236AF0", VA = "0x1812380F0")]
    	private void Awake()
    	{
    	}

    	[Token(Token = "0x6000004")]
    	[Address(RVA = "0x1238100", Offset = "0x1236B00", VA = "0x181238100")]
    	private void Start()
    	{
    	}

    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0x1238350", Offset = "0x1236D50", VA = "0x181238350")]
    	private void Update()
    	{
    	}

    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0x1238440", Offset = "0x1236E40", VA = "0x181238440")]
    	private void RunNoiseJob()
    	{
    	}

    	[Token(Token = "0x6000007")]
    	[Address(RVA = "0x1238950", Offset = "0x1237350", VA = "0x181238950")]
    	private void OnDestroy()
    	{
    	}

    	[Token(Token = "0x6000008")]
    	[Address(RVA = "0x12389D0", Offset = "0x12373D0", VA = "0x1812389D0")]
    	private void PrecomputeGradient()
    	{
    	}

    	[Token(Token = "0x6000009")]
    	[Address(RVA = "0x1238CF0", Offset = "0x12376F0", VA = "0x181238CF0")]
    	public Noise()
    	{
    	}
    }
}
