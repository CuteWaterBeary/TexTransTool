using UnityEngine;
using System.Collections.Generic;
using net.rs64.TexTransCore.TransTextureCore;

namespace net.rs64.TexTransCore.Decal
{
    public class ParallelProjectionFilter : DecalUtility.ITrianglesFilter<ParallelProjectionSpace>
    {
        public List<TriangleFilterUtility.ITriangleFiltering<List<Vector3>>> Filters;

        public ParallelProjectionFilter(List<TriangleFilterUtility.ITriangleFiltering<List<Vector3>>> Filters)
        {
            this.Filters = Filters;
        }

        public virtual List<TriangleIndex> Filtering(ParallelProjectionSpace Space, List<TriangleIndex> Triangles)
        {
            return TriangleFilterUtility.FilteringTriangle(Triangles, Space.PPSVert, Filters);
        }
    }
}

