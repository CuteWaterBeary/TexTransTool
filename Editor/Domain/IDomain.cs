#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static net.rs64.TexTransTool.TextureLayerUtil;

namespace net.rs64.TexTransTool
{
    public interface IAssetSaver
    {
        void TransferAsset(UnityEngine.Object Asset);
    }

    public interface IDomain : IAssetSaver
    {
        /// <summary>
        /// Sets the value to specified property with recording for revert
        /// </summary>
        void SetSerializedProperty(SerializedProperty property, Object value);

        void ReplaceMaterials(Dictionary<Material, Material> mapping, bool rendererOnly = false);
        void SetMesh(Renderer renderer, Mesh mesh);
        void AddTextureStack(Texture2D dist, BlendTextures setTex);
    }

    public static class DomainUtility
    {
        public static void transferAssets(this IDomain domain, IEnumerable<UnityEngine.Object> UnityObjects)
        {
            foreach (var unityObject in UnityObjects)
            {
                domain.TransferAsset(unityObject);
            }
        }
    }
}
#endif