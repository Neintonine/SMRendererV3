using System;

namespace SM.OGL.Mesh
{
    public struct TypeDefinition
    {
        internal int PointerSize;

        public TypeDefinition(int pointerSize)
        {
            PointerSize = pointerSize;
        }
    }
}