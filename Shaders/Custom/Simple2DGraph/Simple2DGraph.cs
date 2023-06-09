﻿using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Shaders
{
    class Simple2DGraph : Material
    {
        private int uniformT;
        private Window window;

        public Simple2DGraph() : base(
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Custom\Simple2DGraph\graph_vert.glsl",
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Custom\Simple2DGraph\graph_frag.glsl")
        {
            uniformT = GL.GetUniformLocation(shader, "t");
            window = Program.GetWindow();
        }

        public override void Bind(Matrix4 cameraMatrix)
        {
            base.Bind(cameraMatrix);

            GL.Uniform1(uniformT, window.timeSinceStart);
        }
    }
}
