#if UNITY_EDITOR
using System;
using UnityEngine;
using System.Collections.Generic;

namespace net.rs64.TexTransTool.TextureAtlas
{
    public class AtlasTextureDataContainer
    {
        List<List<PropAndTexture2D>> _atlasTextures;
        public List<List<PropAndTexture2D>> AtlasTextures
        {
            get => _atlasTextures;
            set
            {
                var AtlasTextures = value;
                if (AtlasTextures == null) { return; }
                _atlasTextures = AtlasTextures;
            }
        }

        List<MeshAndMatRef> _meshes;
        public List<MeshAndMatRef> GenerateMeshes
        {
            get => _meshes;
            set
            {
                var Meshs = value;
                if (Meshs == null) { return; }
                _meshes = Meshs;
            }
        }

        List<List<int>> _matReference;
        public List<List<int>> ChannelsMatRef { get => _matReference; set => _matReference = value; }

        public AtlasTextureDataContainer()
        {

        }
        [Serializable]
        public class MeshAndMatRef
        {
            public int RefMesh;
            public Mesh Mesh;
            public int[] MatRefs;

            public MeshAndMatRef(int refMesh, Mesh mesh, int[] matrefs)
            {
                RefMesh = refMesh;
                Mesh = mesh;
                MatRefs = matrefs;
            }
            public MeshAndMatRef()
            {

            }
        }
    }
}
#endif
