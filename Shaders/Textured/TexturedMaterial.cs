using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Shaders
{
    class TexturedMaterial : Material
    {
        public int texture;

        public TexturedMaterial(int texture) : base(
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Textured\textured_vert.glsl",
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Textured\textured_frag.glsl")
        {
            this.texture = texture;
        }

        public override void Bind(Matrix4 cameraMatrix)
        {
            base.Bind(cameraMatrix);
            GL.BindTexture(TextureTarget.Texture2D, texture);
        }
    }
}
