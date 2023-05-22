using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Shaders
{
    class RiemannSurface : Material
    {
        private int branchUniform;
        private Window window;
        public float branch = 0;

        public RiemannSurface(float branch) : base(
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Custom\Riemann Surface\graph_vert.glsl",
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Custom\Riemann Surface\graph_frag.glsl")
        {
            branchUniform = GL.GetUniformLocation(shader, "branch");
            this.branch = branch;
            window = Program.GetWindow();
        }

        public override void Bind(Matrix4 cameraMatrix)
        {
            base.Bind(cameraMatrix);

            GL.Uniform1(branchUniform, branch);
        }
    }
}
