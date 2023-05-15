using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Shaders
{
    class Simple2DGraph2 : Material
    {
        public Simple2DGraph2() : base(
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Custom\Simple2DGraph\graph_vert2.0.glsl",
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Custom\Simple2DGraph\graph_frag.glsl")
        {

        }

        public override void Bind(Matrix4 cameraMatrix)
        {
            base.Bind(cameraMatrix);
        }
    }
}
