using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Import
{
    struct CharLocation
    {
        public float xStart;
        public float xEnd;
        public float yStart;
        public float yEnd;
        public float xOffset;
        public float yOffset;
        public float xAdvance;

        public CharLocation(float xStart, float xEnd, float yStart, float yEnd, float xOffset, float yOffset, float xAdvance)
        {
            this.xStart = xStart;
            this.xEnd = xEnd;
            this.yStart = yStart;
            this.yEnd = yEnd;
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            this.xAdvance = xAdvance;
        }
    }

    struct Font
    {
        public readonly int textureHandle;
        public readonly CharLocation[] charOffsets;

        public Font(int textureHandle, CharLocation[] offsets)
        {
            this.textureHandle = textureHandle;
            charOffsets = offsets;
        }
    }
}
