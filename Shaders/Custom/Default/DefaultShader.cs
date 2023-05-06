using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Shaders
{
    class DefaultShader : Material
    {
        public DefaultShader(string vert, string frag) : base(vert, frag)
        {

        }

        public override void Bind(Matrix4 cameraMatrix)
        {
            base.Bind(cameraMatrix);
        }
    }
}
