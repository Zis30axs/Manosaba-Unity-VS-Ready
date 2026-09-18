using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using LeTai.Effects;
using LeTai.TrueShadow.PluginInterfaces;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace LeTai.TrueShadow
{

[AddComponentMenu("UI/True Shadow/True Shadow")]
[RequireComponent(typeof(Graphic))]
[HelpURL("https://leloctai.com/trueshadow/docs/articles/customize.html")]
[ExecuteAlways]
public class TrueShadow : UIBehaviour, IMeshModifier, ICanvasElement
{
	private Action checkHierarchyDirtiedDelegate;

	private IChangeTracker[] transformTrackers;

	private ChangeTracker<int>[] hierarchyTrackers;

	private ITrueShadowCasterMaterialProvider casterMaterialProvider;

	private ITrueShadowCasterSubMeshMaterialProvider casterSubMeshMaterialProvider;

	private ITrueShadowCasterMeshProvider casterMeshProvider;

	private ITrueShadowCasterMaterialPropertiesModifier casterMaterialPropertiesModifier;

	private ITrueShadowCasterMeshModifier casterMeshModifier;

	private ITrueShadowCasterClearColorProvider casterClearColorProvider;

	private ITrueShadowRendererMaterialProvider rendererMaterialProvider;

	private ITrueShadowRendererMaterialModifier rendererMaterialModifier;

	private ITrueShadowRendererMeshModifier rendererMeshModifier;

	private ITrueShadowCustomHashProviderV2 customHashProviderV2;

	private readonly List<Color32> meshColors = new List<Color32>(4);

	private readonly List<Color32> meshColorsOpaque = new List<Color32>(4);

	private static readonly Color DEFAULT_COLOR = new Color(0f, 0f, 0f, 0.6f);

	[Tooltip("Accurate algorithm doesn't miss small features, but can be much slower for large or dynamic shadows. Fast is recommended in most cases")]
	[SerializeField]
	private BlurAlgorithmSelection algorithm;

	[Tooltip("Size of the shadow")]
	[SerializeField]
	private float size = 32f;

	[Tooltip("Make the shadow thicker")]
	[SpreadSlider]
	[SerializeField]
	private float spread;

	[SerializeField]
	private bool useGlobalAngle;

	[Tooltip("Direction to offset the shadow toward")]
	[Knob]
	[SerializeField]
	private float offsetAngle = 90f;

	[Tooltip("How far to offset the shadow")]
	[SerializeField]
	private float offsetDistance = 12f;

	[SerializeField]
	private Vector2 offset = Vector2.zero;

	[Tooltip("Tint the shadow")]
	[SerializeField]
	private Color color = DEFAULT_COLOR;

	[Tooltip("Inset shadow")]
	[InsetToggle]
	[SerializeField]
	private bool inset;

	[Tooltip("Blend mode of the shadow")]
	[SerializeField]
	private BlendMode blendMode;

	[FormerlySerializedAs("multiplyCasterAlpha")]
	[Tooltip("Allow shadow to cross-fade with caster")]
	[SerializeField]
	private bool useCasterAlpha = true;

	[Tooltip("Ignore the shadow caster's color, so you can choose specific color for your shadow")]
	[SerializeField]
	private bool ignoreCasterColor;

	[Tooltip("Skip expensive hashing of long text. Using this is recommended for long animated dialogue. If the text may change without changing length, you must set CustomHash for the change to be detected.")]
	[SerializeField]
	private bool skipTextHashing;

	[Tooltip("Reduce memory use of disabled shadows in exchange for slower enable")]
	[SerializeField]
	private bool deallocateOnDisable;

	[Tooltip("May improve shadow fit on some casters with thin features")]
	[SerializeField]
	private bool disableFitCompensation;

	[Tooltip("Position the shadow GameObject as previous sibling of the UI element")]
	[SerializeField]
	private bool shadowAsSibling;

	[Tooltip("Cut the source image from the shadow to avoid shadow showing behind semi-transparent UI")]
	[SerializeField]
	private bool cutout;

	[Tooltip("Bake the shadow to a sprite to reduce CPU and GPU usage at runtime, at the cost of storage, memory and flexibility")]
	[SerializeField]
	private bool baked;

	[SerializeField]
	private bool modifiedFromInspector;

	[SerializeField]
	private List<Sprite> bakedShadows;

	internal ShadowRenderer shadowRenderer;

	private ShadowContainer shadowContainer;

	private int customHash;

	private bool textureDirty;

	private bool layoutDirty;

	private int shadowIndex = -1;

	public bool UsingRendererMaterialProvider => rendererMaterialProvider != null;

	public BlurAlgorithmSelection Algorithm
	{
		get
		{
			return algorithm;
		}
		set
		{
			if (modifiedFromInspector || algorithm != value)
			{
				modifiedFromInspector = false;
				SetLayoutDirty();
				SetTextureDirty();
			}
			algorithm = value;
			Size = size;
		}
	}

	public float Size
	{
		get
		{
			return size;
		}
		set
		{
			float num = Mathf.Max(0f, value);
			if (Algorithm == BlurAlgorithmSelection.Accurate)
			{
				num = Mathf.Min(num, 126f);
			}
			if (modifiedFromInspector || !Mathf.Approximately(size, num))
			{
				modifiedFromInspector = false;
				SetLayoutDirty();
				SetTextureDirty();
			}
			size = num;
			if (Inset && OffsetDistance > Size)
			{
				OffsetDistance = Size;
			}
		}
	}

	public float Spread
	{
		get
		{
			return spread;
		}
		set
		{
			float b = Mathf.Clamp01(value);
			if (modifiedFromInspector || !Mathf.Approximately(spread, b))
			{
				modifiedFromInspector = false;
				SetLayoutDirty();
				SetTextureDirty();
			}
			spread = b;
		}
	}

	public bool UseGlobalAngle
	{
		get
		{
			return useGlobalAngle;
		}
		set
		{
			useGlobalAngle = value;
			ProjectSettings.Instance.globalAngleChanged -= OnGlobalAngleChanged;
			float globalAngle = ProjectSettings.Instance.GlobalAngle;
			if (useGlobalAngle)
			{
				offset = Math.AngleDistanceVector(globalAngle, offset.magnitude, Vector2.right);
				SetLayoutDirty();
				if (Cutout)
				{
					SetTextureDirty();
				}
				ProjectSettings.Instance.globalAngleChanged += OnGlobalAngleChanged;
			}
			else
			{
				float num = offsetAngle;
				OffsetAngle = globalAngle;
				OffsetAngle = num;
			}
		}
	}

	public float OffsetAngle
	{
		get
		{
			return offsetAngle;
		}
		set
		{
			if (UseGlobalAngle)
			{
				return;
			}
			float b = (value + 360f) % 360f;
			if (modifiedFromInspector || !Mathf.Approximately(offsetAngle, b))
			{
				modifiedFromInspector = false;
				SetLayoutDirty();
				if (Cutout)
				{
					SetTextureDirty();
				}
			}
			offsetAngle = b;
			offset = Math.AngleDistanceVector(offsetAngle, offset.magnitude, Vector2.right);
		}
	}

	public float OffsetDistance
	{
		get
		{
			return offsetDistance;
		}
		set
		{
			float num = value;
			num = ((!Inset) ? Mathf.Max(0f, num) : Mathf.Clamp(num, 0f, Size));
			if (modifiedFromInspector || !Mathf.Approximately(offsetDistance, num))
			{
				modifiedFromInspector = false;
				SetLayoutDirty();
				if (Cutout)
				{
					SetTextureDirty();
				}
			}
			offsetDistance = num;
			offset = ((offset.sqrMagnitude < 1E-06f) ? Math.AngleDistanceVector(offsetAngle, offsetDistance, Vector2.right) : (offset.normalized * offsetDistance));
		}
	}

	public Color Color
	{
		get
		{
			return color;
		}
		set
		{
			if (modifiedFromInspector || value != color)
			{
				modifiedFromInspector = false;
				SetLayoutDirty();
			}
			color = value;
		}
	}

	public bool UseCasterAlpha
	{
		get
		{
			return useCasterAlpha;
		}
		set
		{
			if (modifiedFromInspector || value != useCasterAlpha)
			{
				modifiedFromInspector = false;
				SetLayoutDirty();
			}
			useCasterAlpha = value;
		}
	}

	public bool IgnoreCasterColor
	{
		get
		{
			return ignoreCasterColor;
		}
		set
		{
			if (modifiedFromInspector || value != ignoreCasterColor)
			{
				modifiedFromInspector = false;
				SetTextureDirty();
			}
			ignoreCasterColor = value;
		}
	}

	public bool SkipTextHashing
	{
		get
		{
			return skipTextHashing;
		}
		set
		{
			if (modifiedFromInspector || value != skipTextHashing)
			{
				modifiedFromInspector = false;
				SetLayoutTextureDirty();
				SetTextureDirty();
			}
			skipTextHashing = value;
		}
	}

	public bool Inset
	{
		get
		{
			return inset;
		}
		set
		{
			if (modifiedFromInspector || value != inset)
			{
				modifiedFromInspector = false;
				SetLayoutDirty();
				SetTextureDirty();
			}
			inset = value;
			if (Inset && OffsetDistance > Size)
			{
				OffsetDistance = Size;
			}
		}
	}

	public BlendMode BlendMode
	{
		get
		{
			return blendMode;
		}
		set
		{
			if ((bool)Graphic && (bool)CanvasRenderer)
			{
				blendMode = value;
				shadowRenderer.UpdateMaterial();
			}
		}
	}

	public bool DisableFitCompensation
	{
		get
		{
			return disableFitCompensation;
		}
		set
		{
			if (modifiedFromInspector || disableFitCompensation != value)
			{
				modifiedFromInspector = false;
				disableFitCompensation = value;
				SetLayoutDirty();
			}
		}
	}

	public bool DeallocateOnDisable
	{
		get
		{
			return deallocateOnDisable;
		}
		set
		{
			deallocateOnDisable = value;
		}
	}

	public Color ClearColor => casterClearColorProvider?.GetTrueShadowCasterClearColor() ?? Color.clear;

	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool ShadowAsSibling
	{
		get
		{
			return shadowAsSibling;
		}
		set
		{
			shadowAsSibling = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public bool Cutout
	{
		get
		{
			if (shadowAsSibling)
			{
				return cutout;
			}
			return true;
		}
		set
		{
			cutout = value;
		}
	}

	public int CustomHash
	{
		get
		{
			return customHash;
		}
		set
		{
			if (customHash != value)
			{
				SetTextureDirty();
			}
			customHash = value;
		}
	}

	public Vector2 Offset => offset;

	internal ObjectHandle<Mesh> SpriteMeshHandle { get; private set; }

	internal Graphic Graphic { get; private set; }

	internal CanvasRenderer CanvasRenderer { get; private set; }

	internal RectTransform RectTransform { get; private set; }

	internal Texture Content
	{
		get
		{
			Graphic graphic = Graphic;
			if (!(graphic is Image { overrideSprite: var overrideSprite }))
			{
				if (!(graphic is RawImage rawImage))
				{
					if (!(graphic is TextMeshProUGUI textMeshProUGUI))
					{
						if (graphic is TMP_SubMeshUI tMP_SubMeshUI)
						{
							return tMP_SubMeshUI.materialForRendering.mainTexture;
						}
						return Graphic.mainTexture;
					}
					return textMeshProUGUI.materialForRendering.mainTexture;
				}
				return rawImage.texture;
			}
			if (!overrideSprite)
			{
				return null;
			}
			return overrideSprite.texture;
		}
	}

	internal ShadowContainer ShadowContainer => shadowContainer;

	internal bool HierarchyDirty { get; private set; }

	private void InitInvalidator()
	{
		checkHierarchyDirtiedDelegate = CheckHierarchyDirtied;
		hierarchyTrackers = new ChangeTracker<int>[2]
		{
			new ChangeTracker<int>(() => RectTransform.GetSiblingIndex(), delegate(int newValue)
			{
				SetHierachyDirty();
				return newValue;
			}),
			new ChangeTracker<int>(() => shadowRenderer ? shadowRenderer.transform.GetSiblingIndex() : (-1), delegate(int newValue)
			{
				SetHierachyDirty();
				return newValue;
			})
		};
		transformTrackers = new IChangeTracker[3]
		{
			new ChangeTracker<Vector3>(() => RectTransform.position, delegate(Vector3 newValue)
			{
				SetLayoutDirty();
				return newValue;
			}, (Vector3 prev, Vector3 curr) => prev == curr),
			new ChangeTracker<Quaternion>(() => RectTransform.rotation, delegate(Quaternion newValue)
			{
				SetLayoutDirty();
				if (Cutout)
				{
					SetTextureDirty();
				}
				return newValue;
			}, (Quaternion prev, Quaternion curr) => prev == curr),
			new ChangeTracker<Color>(() => CanvasRenderer.GetColor(), delegate(Color newValue)
			{
				SetLayoutDirty();
				return newValue;
			}, (Color prev, Color curr) => prev == curr)
		};
		if (Graphic is TextMeshProUGUI || Graphic is TMP_SubMeshUI)
		{
			IChangeTracker[] array = transformTrackers;
			transformTrackers = new IChangeTracker[array.Length + 1];
			Array.Copy(array, transformTrackers, array.Length);
			transformTrackers[array.Length] = new ChangeTracker<Vector3>(() => RectTransform.lossyScale, delegate(Vector3 newValue)
			{
				SetLayoutTextureDirty();
				return newValue;
			}, delegate(Vector3 prev, Vector3 curr)
			{
				if (prev == curr)
				{
					return true;
				}
				if (prev.x * prev.y * prev.z < 1E-09f && curr.x * curr.y * curr.z > 1E-09f)
				{
					return false;
				}
				Vector3 vector = curr - prev;
				return Mathf.Abs(vector.x / prev.x) < 0.25f && Mathf.Abs(vector.y / prev.y) < 0.25f && Mathf.Abs(vector.z / prev.z) < 0.25f;
			});
		}
		Graphic.RegisterDirtyLayoutCallback(SetLayoutTextureDirty);
		Graphic.RegisterDirtyVerticesCallback(SetLayoutTextureDirty);
		Graphic.RegisterDirtyMaterialCallback(OnGraphicMaterialDirty);
		CheckHierarchyDirtied();
		CheckTransformDirtied();
	}

	private void TerminateInvalidator()
	{
		if ((bool)Graphic)
		{
			Graphic.UnregisterDirtyLayoutCallback(SetLayoutTextureDirty);
			Graphic.UnregisterDirtyVerticesCallback(SetLayoutTextureDirty);
			Graphic.UnregisterDirtyMaterialCallback(OnGraphicMaterialDirty);
		}
	}

	private void OnGraphicMaterialDirty()
	{
		SetLayoutTextureDirty();
		shadowRenderer.UpdateMaterial();
	}

	internal void CheckTransformDirtied()
	{
		if (transformTrackers != null)
		{
			for (int i = 0; i < transformTrackers.Length; i++)
			{
				transformTrackers[i].Check();
			}
		}
	}

	internal void CheckHierarchyDirtied()
	{
		if (ShadowAsSibling && hierarchyTrackers != null)
		{
			for (int i = 0; i < hierarchyTrackers.Length; i++)
			{
				hierarchyTrackers[i].Check();
			}
		}
	}

	internal void ForgetSiblingIndexChanges()
	{
		for (int i = 0; i < hierarchyTrackers.Length; i++)
		{
			hierarchyTrackers[i].Forget();
		}
	}

	protected override void OnTransformParentChanged()
	{
		base.OnTransformParentChanged();
		if (base.isActiveAndEnabled)
		{
			SetHierachyDirty();
			this.NextFrames(checkHierarchyDirtiedDelegate);
		}
	}

	protected override void OnRectTransformDimensionsChange()
	{
		base.OnRectTransformDimensionsChange();
		if (base.isActiveAndEnabled)
		{
			SetLayoutTextureDirty();
		}
	}

	protected override void OnDidApplyAnimationProperties()
	{
		if (base.isActiveAndEnabled)
		{
			SetLayoutTextureDirty();
		}
	}

	public void ModifyMesh(Mesh mesh)
	{
		if (casterMeshProvider == null && base.isActiveAndEnabled)
		{
			SpriteMeshHandle.Dispose();
			SpriteMeshHandle = ObjectHandle.Borrow(UnityEngine.Object.Instantiate(mesh));
			ModifyShadowCastingMesh(SpriteMeshHandle.obj);
			SetLayoutTextureDirty();
		}
	}

	public void ModifyMesh(VertexHelper verts)
	{
		if (casterMeshProvider != null || !base.isActiveAndEnabled)
		{
			return;
		}
		if (!(Graphic is TextMeshProUGUI))
		{
			if (!SpriteMeshHandle.obj)
			{
				SpriteMeshHandle = ObjectHandle.Take(new Mesh());
			}
			verts.FillMesh(SpriteMeshHandle.obj);
			ModifyShadowCastingMesh(SpriteMeshHandle.obj);
		}
		SetLayoutTextureDirty();
	}

	private void SetLayoutTextureDirty()
	{
		Mesh mesh = null;
		if (Graphic is TextMeshProUGUI textMeshProUGUI)
		{
			mesh = (string.IsNullOrEmpty(textMeshProUGUI.text) ? null : textMeshProUGUI.mesh);
		}
		else if (Graphic is TMP_SubMeshUI tMP_SubMeshUI)
		{
			mesh = ((string.IsNullOrEmpty(tMP_SubMeshUI.textComponent.text) | !tMP_SubMeshUI.canvasRenderer.GetMesh()) ? null : tMP_SubMeshUI.mesh);
		}
		if (mesh != null)
		{
			SpriteMeshHandle.Dispose();
			SpriteMeshHandle = ObjectHandle.Borrow(mesh);
		}
		SetLayoutDirty();
		SetTextureDirty();
	}

	private void InitializePlugins()
	{
		casterMaterialProvider = GetComponent<ITrueShadowCasterMaterialProvider>();
		casterSubMeshMaterialProvider = GetComponent<ITrueShadowCasterSubMeshMaterialProvider>();
		casterMeshProvider = GetComponent<ITrueShadowCasterMeshProvider>();
		casterMaterialPropertiesModifier = GetComponent<ITrueShadowCasterMaterialPropertiesModifier>();
		casterMeshModifier = GetComponent<ITrueShadowCasterMeshModifier>();
		casterClearColorProvider = GetComponent<ITrueShadowCasterClearColorProvider>();
		rendererMaterialProvider = GetComponent<ITrueShadowRendererMaterialProvider>();
		rendererMaterialModifier = GetComponent<ITrueShadowRendererMaterialModifier>();
		rendererMeshModifier = GetComponent<ITrueShadowRendererMeshModifier>();
		customHashProviderV2 = GetComponent<ITrueShadowCustomHashProviderV2>();
		if (casterMeshProvider != null)
		{
			casterMeshProvider.trueShadowCasterMeshChanged += HandleCasterMeshProviderMeshChanged;
		}
		if (casterMaterialProvider != null)
		{
			casterMaterialProvider.materialReplaced += HandleCasterMaterialReplaced;
			casterMaterialProvider.materialModified += HandleCasterMaterialModified;
		}
		if (rendererMaterialProvider != null)
		{
			rendererMaterialProvider.materialReplaced += HandleRendererMaterialReplaced;
			rendererMaterialProvider.materialModified += HandleRendererMaterialModified;
		}
		if (customHashProviderV2 != null)
		{
			customHashProviderV2.trueShadowCustomHashChanged += HandleTrueShadowCustomHashChanged;
		}
	}

	private void TerminatePlugins()
	{
		if (casterMeshProvider != null)
		{
			casterMeshProvider.trueShadowCasterMeshChanged -= HandleCasterMeshProviderMeshChanged;
		}
		if (casterMaterialProvider != null)
		{
			casterMaterialProvider.materialReplaced -= HandleCasterMaterialReplaced;
			casterMaterialProvider.materialModified -= HandleCasterMaterialModified;
		}
		if (rendererMaterialProvider != null)
		{
			rendererMaterialProvider.materialReplaced -= HandleRendererMaterialReplaced;
			rendererMaterialProvider.materialModified -= HandleRendererMaterialModified;
		}
		if (customHashProviderV2 != null)
		{
			customHashProviderV2.trueShadowCustomHashChanged -= HandleTrueShadowCustomHashChanged;
		}
	}

	public void RefreshPlugins()
	{
		TerminatePlugins();
		InitializePlugins();
	}

	private void HandleCasterMeshProviderMeshChanged(Mesh mesh)
	{
		SpriteMeshHandle = ObjectHandle.Borrow(mesh);
		SetLayoutTextureDirty();
	}

	private void HandleCasterMaterialReplaced()
	{
		SetTextureDirty();
	}

	private void HandleRendererMaterialReplaced()
	{
		if ((bool)shadowRenderer)
		{
			shadowRenderer.UpdateMaterial();
		}
	}

	private void HandleCasterMaterialModified()
	{
		SetTextureDirty();
	}

	private void HandleRendererMaterialModified()
	{
		if ((bool)shadowRenderer)
		{
			shadowRenderer.SetMaterialDirty();
		}
	}

	private void HandleTrueShadowCustomHashChanged(int customHash)
	{
		CustomHash = customHash;
		SetLayoutTextureDirty();
	}

	public virtual Material GetShadowCastingMaterial()
	{
		if (casterSubMeshMaterialProvider != null)
		{
			return casterSubMeshMaterialProvider.GetTrueShadowCasterMaterialForSubMesh(0);
		}
		Material material = null;
		if (casterMaterialProvider != null)
		{
			material = casterMaterialProvider.GetTrueShadowCasterMaterial();
		}
		else if (Graphic is TextMeshProUGUI || Graphic is TMP_SubMeshUI)
		{
			material = Graphic.materialForRendering;
		}
		if (!(material != null))
		{
			return Graphic.material;
		}
		return material;
	}

	public virtual Material GetShadowCastingMaterialForSubMesh(int subMeshIndex)
	{
		if (casterSubMeshMaterialProvider == null)
		{
			Debug.LogError("Custom UI that use submeshes must implement ITrueShadowCasterSubMeshMaterialProvider");
			return null;
		}
		return casterSubMeshMaterialProvider.GetTrueShadowCasterMaterialForSubMesh(subMeshIndex);
	}

	public virtual void ModifyShadowCastingMaterialProperties(MaterialPropertyBlock propertyBlock)
	{
		casterMaterialPropertiesModifier?.ModifyTrueShadowCasterMaterialProperties(propertyBlock);
	}

	public virtual void ModifyShadowCastingMesh(Mesh mesh)
	{
		casterMeshModifier?.ModifyTrueShadowCasterMesh(mesh);
		MakeOpaque(mesh);
	}

	private void MakeOpaque(Mesh mesh)
	{
		if (shadowAsSibling)
		{
			return;
		}
		mesh.GetColors(meshColors);
		int count = meshColors.Count;
		if (count < 1)
		{
			return;
		}
		if (meshColorsOpaque.Count == count)
		{
			if (meshColors[0].a == meshColorsOpaque[0].a)
			{
				return;
			}
		}
		else
		{
			meshColorsOpaque.Clear();
			meshColorsOpaque.AddRange(Enumerable.Repeat(new Color32(0, 0, 0, 0), count));
		}
		for (int i = 0; i < count; i++)
		{
			Color32 value = meshColors[i];
			value.a = byte.MaxValue;
			meshColorsOpaque[i] = value;
		}
		mesh.SetColors(meshColorsOpaque);
	}

	public virtual Material GetShadowRenderingMaterial()
	{
		Material material = rendererMaterialProvider?.GetTrueShadowRendererMaterial();
		if (!(material != null))
		{
			return BlendMode.GetMaterial();
		}
		return material;
	}

	public virtual void ModifyShadowRendererMaterial(Material baseMaterial)
	{
		rendererMaterialModifier?.ModifyTrueShadowRendererMaterial(baseMaterial);
	}

	public virtual void ModifyShadowRendererMesh(VertexHelper vertexHelper)
	{
		rendererMeshModifier?.ModifyTrueShadowRendererMesh(vertexHelper);
	}

	private void OnGlobalAngleChanged(float angle)
	{
		offset = Math.AngleDistanceVector(angle, offset.magnitude, Vector2.right);
		SetLayoutDirty();
		if (Cutout)
		{
			SetTextureDirty();
		}
	}

	protected override void Awake()
	{
	}

	protected override void OnEnable()
	{
		TrueShadow[] components = GetComponents<TrueShadow>();
		int num = 0;
		for (int i = 0; i < components.Length; i++)
		{
			if (components[i] == this || (bool)components[i].shadowRenderer)
			{
				components[i].shadowIndex = num++;
			}
		}
		RectTransform = GetComponent<RectTransform>();
		Graphic = GetComponent<Graphic>();
		CanvasRenderer = GetComponent<CanvasRenderer>();
		if (!SpriteMeshHandle.obj)
		{
			SpriteMeshHandle = ObjectHandle.Take(new Mesh());
		}
		InitializePlugins();
		if (bakedShadows == null)
		{
			bakedShadows = new List<Sprite>(4);
		}
		InitInvalidator();
		ShadowRenderer.Initialize(this, ref shadowRenderer);
		Canvas.willRenderCanvases += OnWillRenderCanvas;
		if (UseGlobalAngle)
		{
			ProjectSettings.Instance.globalAngleChanged -= OnGlobalAngleChanged;
			ProjectSettings.Instance.globalAngleChanged += OnGlobalAngleChanged;
			OnGlobalAngleChanged(ProjectSettings.Instance.GlobalAngle);
		}
		if ((bool)Graphic)
		{
			Graphic.SetVerticesDirty();
		}
		this.NextFrames(CopyToTMPSubMeshes, 2);
	}

	public void ApplySerializedData()
	{
		Algorithm = algorithm;
		Size = size;
		Spread = spread;
		OffsetAngle = offsetAngle;
		OffsetDistance = offsetDistance;
		BlendMode = blendMode;
		SetHierachyDirty();
		SetLayoutDirty();
		SetTextureDirty();
		if ((bool)shadowRenderer)
		{
			shadowRenderer.SetMaterialDirty();
		}
		CopyToTMPSubMeshes();
	}

	protected override void OnDisable()
	{
		ProjectSettings.Instance.globalAngleChanged -= OnGlobalAngleChanged;
		Canvas.willRenderCanvases -= OnWillRenderCanvas;
		TerminateInvalidator();
		TerminatePlugins();
		if (DeallocateOnDisable)
		{
			ShadowFactory.Instance.ReleaseContainer(ref shadowContainer);
		}
		if ((bool)shadowRenderer)
		{
			shadowRenderer.gameObject.SetActive(value: false);
		}
	}

	protected override void OnDestroy()
	{
		SpriteMeshHandle.Dispose();
		if ((bool)shadowRenderer)
		{
			shadowRenderer.Dispose();
		}
		ShadowFactory.Instance.ReleaseContainer(ref shadowContainer);
		StopAllCoroutines();
	}

	private bool ShouldPerformWorks()
	{
		bool flag = (bool)CanvasRenderer && CanvasRenderer.cull && (bool)shadowRenderer.CanvasRenderer && shadowRenderer.CanvasRenderer.cull;
		if (base.isActiveAndEnabled)
		{
			return !flag;
		}
		return false;
	}

	private void LateUpdate()
	{
		shadowRenderer.gameObject.SetActive((bool)Graphic && Graphic.isActiveAndEnabled);
		if (ShouldPerformWorks())
		{
			CheckTransformDirtied();
		}
	}

	public void Rebuild(CanvasUpdate executing)
	{
		if (ShouldPerformWorks() && executing == CanvasUpdate.PostLayout && layoutDirty)
		{
			shadowRenderer.ReLayout();
			layoutDirty = false;
		}
	}

	private void OnWillRenderCanvas()
	{
		if (!base.isActiveAndEnabled || !ShouldPerformWorks())
		{
			return;
		}
		if (textureDirty && (bool)Graphic && (bool)Graphic.canvas)
		{
			ShadowFactory.Instance.Get(new ShadowSettingSnapshot(this), ref shadowContainer);
			shadowRenderer.SetTexture(shadowContainer?.Texture);
			textureDirty = false;
		}
		if (!shadowAsSibling)
		{
			if (shadowRenderer.transform.parent != RectTransform)
			{
				shadowRenderer.transform.SetParent(RectTransform, worldPositionStays: true);
			}
			if (shadowRenderer.transform.GetSiblingIndex() != shadowIndex)
			{
				shadowRenderer.transform.SetSiblingIndex(shadowIndex);
			}
			UnSetHierachyDirty();
			if (layoutDirty)
			{
				shadowRenderer.ReLayout();
				layoutDirty = false;
			}
		}
	}

	public void LayoutComplete()
	{
	}

	public void GraphicUpdateComplete()
	{
	}

	public void SetTextureDirty()
	{
		textureDirty = true;
	}

	public void SetLayoutDirty()
	{
		layoutDirty = true;
	}

	public void SetHierachyDirty()
	{
		HierarchyDirty = true;
	}

	internal void UnSetHierachyDirty()
	{
		HierarchyDirty = false;
	}

	public void CopyTo(TrueShadow other)
	{
		other.Inset = Inset;
		other.Algorithm = Algorithm;
		other.Size = Size;
		other.Spread = Spread;
		other.UseGlobalAngle = UseGlobalAngle;
		other.OffsetAngle = OffsetAngle;
		other.OffsetDistance = OffsetDistance;
		other.Color = Color;
		other.BlendMode = BlendMode;
		other.UseCasterAlpha = UseCasterAlpha;
		other.IgnoreCasterColor = IgnoreCasterColor;
		other.DisableFitCompensation = DisableFitCompensation;
		other.SetLayoutTextureDirty();
	}

	public void CopyTo(GameObject other)
	{
		TrueShadow component = other.GetComponent<TrueShadow>();
		if ((bool)component)
		{
			CopyTo(component);
			return;
		}
		TrueShadow other2 = other.AddComponent<TrueShadow>();
		CopyTo(other2);
	}

	public void CopyToTMPSubMeshes()
	{
		if (Graphic is TextMeshProUGUI)
		{
			TMP_SubMeshUI[] componentsInChildren = GetComponentsInChildren<TMP_SubMeshUI>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				CopyTo(componentsInChildren[i].gameObject);
			}
		}
	}
}
}
