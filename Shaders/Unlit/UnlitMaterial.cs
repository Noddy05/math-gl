using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace MathGL.Shaders
{
    class UnlitMaterial : Material
    {
        private Color4 color;
        private int colorUniform;

        public UnlitMaterial(Color4 color) : base(
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Unlit\unlit_vert.glsl",
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Unlit\unlit_frag.glsl")
        {
            this.color = color;
            colorUniform = GL.GetUniformLocation(shader, "color");
        }

        public override void Bind(Matrix4 cameraMatrix)
        {
            base.Bind(cameraMatrix);
            GL.Uniform4(colorUniform, color);
        }
    }
}
