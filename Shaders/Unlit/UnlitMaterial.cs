using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Shaders
{
    class UnlitMaterial : Material
    {
        public UnlitMaterial() : base(
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Unlit\unlit_vert.glsl",
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Unlit\unlit_frag.glsl")
        {

        }

        public override void Bind(Matrix4 cameraMatrix)
        {
            base.Bind(cameraMatrix);
        }
    }
}
