using MathGL.RenderObjects;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Shaders
{
    class Material
    {
        public Shader shader;
        private int cameraUniform;

        public Material(string vertShaderLocation, string fragShaderLocation)
        {
            shader = new Shader(vertShaderLocation, fragShaderLocation);
            cameraUniform = GL.GetUniformLocation(shader, "camera");
        }

        /// <summary>
        /// base.Bind() should be the first thing called in any class that inherits this function
        /// </summary>
        public virtual void Bind(Matrix4 cameraMatrix) { 
            GL.UseProgram(shader);
            GL.UniformMatrix4(cameraUniform, false, ref cameraMatrix);
        }
    }
}
