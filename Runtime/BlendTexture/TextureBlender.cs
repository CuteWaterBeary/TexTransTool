#if UNITY_EDITOR
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
namespace net.rs64.TexTransTool
{
    [AddComponentMenu("TexTransTool/TextureBlender")]
    public class TextureBlender : TextureTransformer
    {
        public Renderer TargetRenderer;
        public int MaterialSelect = 0;
        public Texture2D BlendTexture;
        public BlendType BlendType = BlendType.Normal;
        public string TargetPropertyName = "_MainTex";

        public TextureBlenderDataContainer Container = new TextureBlenderDataContainer();

        [SerializeField] protected bool _IsApply = false;
        public override bool IsApply => _IsApply;

        public override bool IsPossibleApply => TargetRenderer != null && BlendTexture != null;


        public override void Apply(AvatarDomain avatarMaterialDomain = null)
        {
            if (!IsPossibleApply) return;
            if (_IsApply) return;
            _IsApply = true;
            if (avatarMaterialDomain == null) avatarMaterialDomain = new AvatarDomain(TargetRenderer.gameObject);

            var DistMaterials = TargetRenderer.sharedMaterials;

            if (DistMaterials.Length <= MaterialSelect) return;

            var DistMat = DistMaterials[MaterialSelect];

            var DistTex = DistMat.GetTexture(TargetPropertyName) as Texture2D;
            var AddTex = BlendTexture;
            if (DistTex == null) return;


            var DistSize = DistTex.NativeSize();
            if (DistSize != AddTex.NativeSize())
            {
                Compiler.NotFIlterAndReadWritTexture2D(ref AddTex);
                AddTex = TextureLayerUtil.ResizeTexture(AddTex, DistSize);
            }

            var newTex = TextureLayerUtil.BlendTextureUseComputeShader(null, DistTex, AddTex, BlendType);
            var SavedTex = AssetSaveHelper.SavePng(newTex);


            Container.BlendTextures = SavedTex;

            var ChangeDict = avatarMaterialDomain.SetTexture(DistTex, SavedTex);
            Container.GenerateMaterials = ChangeDict;
        }


        public override void Revert(AvatarDomain avatarMaterialDomain = null)
        {
            if (!_IsApply) return;
            _IsApply = false;
            if (avatarMaterialDomain == null) avatarMaterialDomain = new AvatarDomain(TargetRenderer.gameObject);
            IsSelfCallApply = false;

            avatarMaterialDomain.SetMaterials(MatPair.SwitchingList(Container.GenerateMaterials), true);
        }
    }

    public class TextureBlenderDataContainer : TTDataContainer
    {
        [SerializeField] Texture2D _BlendTextures;

        public Texture2D BlendTextures
        {
            set => _BlendTextures = value;
            get => _BlendTextures;
        }

    }
}
#endif
