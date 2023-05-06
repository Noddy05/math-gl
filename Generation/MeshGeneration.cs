using MathGL.RenderObjects;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Generation
{
    class MeshGeneration
    {
        public static Mesh GenerateQuad()
        {
            float[] vertices = new float[4 * 5];
            int[] triangles =
            {
                0, 2, 1,
                3, 1, 2
            };
            for(int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    vertices[(x + 2 * y) * 5 + 0] = x;
                    vertices[(x + 2 * y) * 5 + 1] = 0;
                    vertices[(x + 2 * y) * 5 + 2] = y;
                    vertices[(x + 2 * y) * 5 + 3] = x;
                    vertices[(x + 2 * y) * 5 + 4] = y;
                }
            }

            VAO vao = new VAO();
            VBO vbo = new VBO(vertices, BufferUsageHint.StaticCopy);
            IBO ibo = new IBO(triangles, BufferUsageHint.StaticCopy);
            /*
            shader = new Shader(@"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Custom\Simple2DGraph\graph_vert.glsl",
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Custom\Simple2DGraph\graph_frag.glsl");*/

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            vao.BindVertexAttribPointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            vao.BindVertexAttribPointer(1, 2, VertexAttribPointerType.Float, 5, 3);

            GL.BindVertexArray(0);
            vbo.Dispose();

            return new Mesh(vao, ibo);
        }
        public static Mesh GeneratePlane(int xDivisions, int yDivisions)
        {
            float[] vertices = new float[xDivisions * yDivisions * 5];
            int[] triangles = new int[(xDivisions - 1) * (yDivisions - 1) * 6];
            for (int y = 0; y < yDivisions - 1; y++)
            {
                for (int x = 0; x < xDivisions - 1; x++)
                {
                    triangles[(x + (xDivisions - 1) * y) * 6 + 0] = 0 + x + xDivisions * y;
                    triangles[(x + (xDivisions - 1) * y) * 6 + 1] = 0 + x + xDivisions * (y + 1);
                    triangles[(x + (xDivisions - 1) * y) * 6 + 2] = 1 + x + xDivisions * y;
                    triangles[(x + (xDivisions - 1) * y) * 6 + 3] = 1 + x + xDivisions * y;
                    triangles[(x + (xDivisions - 1) * y) * 6 + 4] = 0 + x + xDivisions * (y + 1);
                    triangles[(x + (xDivisions - 1) * y) * 6 + 5] = 1 + x + xDivisions * (y + 1);
                }
            }
            for (int y = 0; y < yDivisions; y++)
            {
                for (int x = 0; x < xDivisions; x++)
                {
                    vertices[(x + xDivisions * y) * 5 + 0] = x / (float)(xDivisions - 1);
                    vertices[(x + xDivisions * y) * 5 + 1] = 0;
                    vertices[(x + xDivisions * y) * 5 + 2] = y / (float)(yDivisions - 1);
                    vertices[(x + xDivisions * y) * 5 + 3] = x / (float)(xDivisions - 1);
                    vertices[(x + xDivisions * y) * 5 + 4] = y / (float)(yDivisions - 1);
                }
            }

            VAO vao = new VAO();
            VBO vbo = new VBO(vertices, BufferUsageHint.StaticCopy);
            IBO ibo = new IBO(triangles, BufferUsageHint.StaticCopy);
            /*
            shader = new Shader(@"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Custom\Simple2DGraph\graph_vert.glsl",
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Custom\Simple2DGraph\graph_frag.glsl");*/

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            vao.BindVertexAttribPointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            vao.BindVertexAttribPointer(1, 2, VertexAttribPointerType.Float, 5, 3);

            GL.BindVertexArray(0);
            vbo.Dispose();

            return new Mesh(vao, ibo);
        }
    }
}
