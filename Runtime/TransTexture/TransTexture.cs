#if UNITY_EDITOR
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Rs64.TexTransTool
{
    public static class TransTexture
    {
        //sRGBのRenderTexture
        public static void TransTextureToRenderTexture(
            RenderTexture TargetTexture,
            Texture2D SouseTexture,
            IReadOnlyList<TraiangleIndex> TrianglesToIndex,
            IReadOnlyList<Vector2> TargetUV,
            IReadOnlyList<Vector2> SourceUV,
            float? Pading = null,
            Vector2? WarpRange = null
            )
        {
            var Mesh = new Mesh();
            var Vertices = TargetUV.Select(I => new Vector3(I.x, I.y, 0)).ToArray();
            var UV = SourceUV.ToArray();
            var Triangles = TrianglesToIndex.SelectMany(I => I).ToArray();
            Mesh.vertices = Vertices;
            Mesh.uv = UV;
            Mesh.triangles = Triangles;

            var Material = new Material(Shader.Find("Hidden/TransTexture"));
            Material.SetTexture("_MainTex", SouseTexture);
            if (Pading != null) Material.SetFloat("_Pading", Pading.Value);

            if (WarpRange != null)
            {
                Material.EnableKeyword("WarpRange");
                Material.SetFloat("_WarpRangeX", WarpRange.Value.x);
                Material.SetFloat("_WarpRangeY", WarpRange.Value.y);
            }




            var Pre = RenderTexture.active;

            try
            {
                RenderTexture.active = TargetTexture;
                Material.SetPass(0);
                Graphics.DrawMeshNow(Mesh, Matrix4x4.identity);
                if (Pading != null)
                {
                    Material.SetPass(1);
                    Graphics.DrawMeshNow(Mesh, Matrix4x4.identity);
                }

            }
            finally
            {
                RenderTexture.active = Pre;
            }
        }
        public static Texture2D CopyTexture2D(this RenderTexture Rt)
        {
            var Pre = RenderTexture.active;
            try
            {
                RenderTexture.active = Rt;
                var Texture = new Texture2D(Rt.width, Rt.height, Rt.graphicsFormat, UnityEngine.Experimental.Rendering.TextureCreationFlags.MipChain);
                Texture.ReadPixels(new Rect(0, 0, Rt.width, Rt.height), 0, 0);
                Texture.Apply();
                return Texture;
            }
            finally
            {
                RenderTexture.active = Pre;
            }
        }
    }

}
#endif
