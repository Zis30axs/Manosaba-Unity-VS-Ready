using UnityEngine;

namespace LeTai.TrueShadow.PluginInterfaces
{

public interface ITrueShadowCasterSubMeshMaterialProvider
{
	Material GetTrueShadowCasterMaterialForSubMesh(int subMeshIndex);
}
}
