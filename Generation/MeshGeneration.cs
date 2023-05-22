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

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            vao.BindVertexAttribPointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            vao.BindVertexAttribPointer(1, 2, VertexAttribPointerType.Float, 5, 3);

            GL.BindVertexArray(0);
            vbo.Dispose();

            return new Mesh(vao, ibo);
        }

        public static Mesh GenerateCube()
        {
            float[] vertices = new float[4 * 6 * 5];
            int[] triangles = new int[2 * 3 * 6];

            for (int i = 0; i < 6; i++)
            {
                triangles[i * 6 + 0] = 0 + i * 4;
                triangles[i * 6 + 1] = 2 + i * 4;
                triangles[i * 6 + 2] = 1 + i * 4;
                triangles[i * 6 + 3] = 3 + i * 4;
                triangles[i * 6 + 4] = 1 + i * 4;
                triangles[i * 6 + 5] = 2 + i * 4;
            }
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    vertices[(x + 2 * y) * 5 + 0] = x;
                    vertices[(x + 2 * y) * 5 + 1] = 0;
                    vertices[(x + 2 * y) * 5 + 2] = y;
                    vertices[(x + 2 * y) * 5 + 3] = 0;
                    vertices[(x + 2 * y) * 5 + 4] = 0;
                }
            }
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    vertices[(x + 2 * y) * 5 + 0 + 4 * 5] = x;
                    vertices[(x + 2 * y) * 5 + 1 + 4 * 5] = 1;
                    vertices[(x + 2 * y) * 5 + 2 + 4 * 5] = y;
                    vertices[(x + 2 * y) * 5 + 3 + 4 * 5] = 0;
                    vertices[(x + 2 * y) * 5 + 4 + 4 * 5] = 0;
                }
            }
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    vertices[(x + 2 * y) * 5 + 0 + 4 * 5 * 2] = x;
                    vertices[(x + 2 * y) * 5 + 1 + 4 * 5 * 2] = y;
                    vertices[(x + 2 * y) * 5 + 2 + 4 * 5 * 2] = 0;
                    vertices[(x + 2 * y) * 5 + 3 + 4 * 5 * 2] = 0;
                    vertices[(x + 2 * y) * 5 + 4 + 4 * 5 * 2] = 0;
                }
            }
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    vertices[(x + 2 * y) * 5 + 0 + 4 * 5 * 3] = x;
                    vertices[(x + 2 * y) * 5 + 1 + 4 * 5 * 3] = y;
                    vertices[(x + 2 * y) * 5 + 2 + 4 * 5 * 3] = 1;
                    vertices[(x + 2 * y) * 5 + 3 + 4 * 5 * 3] = 0;
                    vertices[(x + 2 * y) * 5 + 4 + 4 * 5 * 3] = 0;
                }
            }
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    vertices[(x + 2 * y) * 5 + 0 + 4 * 5 * 4] = 0;
                    vertices[(x + 2 * y) * 5 + 1 + 4 * 5 * 4] = y;
                    vertices[(x + 2 * y) * 5 + 2 + 4 * 5 * 4] = x;
                    vertices[(x + 2 * y) * 5 + 3 + 4 * 5 * 4] = 0;
                    vertices[(x + 2 * y) * 5 + 4 + 4 * 5 * 4] = 0;
                }
            }
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 2; x++)
                {
                    vertices[(x + 2 * y) * 5 + 0 + 4 * 5 * 5] = 1;
                    vertices[(x + 2 * y) * 5 + 1 + 4 * 5 * 5] = y;
                    vertices[(x + 2 * y) * 5 + 2 + 4 * 5 * 5] = x;
                    vertices[(x + 2 * y) * 5 + 3 + 4 * 5 * 5] = 0;
                    vertices[(x + 2 * y) * 5 + 4 + 4 * 5 * 5] = 0;
                }
            }

            VAO vao = new VAO();
            VBO vbo = new VBO(vertices, BufferUsageHint.StaticCopy);
            IBO ibo = new IBO(triangles, BufferUsageHint.StaticCopy);

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            vao.BindVertexAttribPointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            vao.BindVertexAttribPointer(1, 2, VertexAttribPointerType.Float, 5, 3);

            GL.BindVertexArray(0);
            vbo.Dispose();

            return new Mesh(vao, ibo);
        }

        public static Mesh GenerateUVSphere(int slices, int stacks)
        {
            float[] vertices = new float[stacks * slices * 5 + 2 * 5];
            int[] triangles = new int[slices * 6];

            for (int i = 0; i < 5; i++)
            {
                //Top vertex
                vertices[i] = 0;
                //Bottom vertex
                vertices[stacks * slices * 5 + 5 + i] = 100;
            }

            //Modifying top vertex height
            vertices[vertices.Length - 5 + 2] = 100;
            for (int y = 0; y < stacks; y++)
            {
                for (int x = 0; x < slices; x++)
                {
                    vertices[(x + 1) * 5 + 0] = MathF.Cos(x / (float)slices * 2 * MathF.PI) / 2;
                    vertices[(x + 1) * 5 + 1] = MathF.Sin((y + 1) / (float)(stacks + 2) * MathF.PI) / 2;
                    vertices[(x + 1) * 5 + 2] = MathF.Sin(x / (float)slices * 2 * MathF.PI) / 2;
                    vertices[(x + 1) * 5 + 3] = MathF.Cos(x / (float)slices * 2 * MathF.PI) / 2;
                    vertices[(x + 1) * 5 + 4] = MathF.Sin(x / (float)slices * 2 * MathF.PI) / 2;
                }
            }

            //Top and bottom triangles
            for (int i = 0; i < slices; i++)
            {
                triangles[i * 6 + 0] = i + 1;
                triangles[i * 6 + 1] = 0;
                triangles[i * 6 + 2] = (i + 1) % slices + 1;

                triangles[i * 6 + 3] = i + slices * stacks + 1;
                triangles[i * 6 + 4] = 0;
                triangles[i * 6 + 5] = (i + 1) % slices + slices * stacks + 1;
            }

            VAO vao = new VAO();
            VBO vbo = new VBO(vertices, BufferUsageHint.StaticCopy);
            IBO ibo = new IBO(triangles, BufferUsageHint.StaticCopy);

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            vao.BindVertexAttribPointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            vao.BindVertexAttribPointer(1, 2, VertexAttribPointerType.Float, 5, 3);

            GL.BindVertexArray(0);
            vbo.Dispose();

            return new Mesh(vao, ibo);
        }

        public static Mesh GenerateDisk(int verts)
        {
            float[] vertices = new float[(verts + 1) * 5];
            int[] triangles = new int[verts * 3];

            for (int i = 0; i < 5; i++)
            {
                vertices[i] = 0;
            }

            for (int i = 0; i < verts; i++)
            {
                vertices[i * 5 + 0] = MathF.Cos(i / (float)verts * 2 * MathF.PI) / 2;
                vertices[i * 5 + 1] = 0;
                vertices[i * 5 + 2] = MathF.Sin(i / (float)verts * 2 * MathF.PI) / 2;
                vertices[i * 5 + 3] = MathF.Cos(i / (float)verts * 2 * MathF.PI) / 2;
                vertices[i * 5 + 4] = MathF.Sin(i / (float)verts * 2 * MathF.PI) / 2;
            }
            for (int i = 0; i < verts; i++)
            {
                triangles[i * 3 + 0] = i;
                triangles[i * 3 + 1] = 0;
                triangles[i * 3 + 2] = (i + 1) % verts;
            }

            VAO vao = new VAO();
            VBO vbo = new VBO(vertices, BufferUsageHint.StaticCopy);
            IBO ibo = new IBO(triangles, BufferUsageHint.StaticCopy);

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

            vao.BindVertexAttribPointer(0, 3, VertexAttribPointerType.Float, 5, 0);
            vao.BindVertexAttribPointer(1, 2, VertexAttribPointerType.Float, 5, 3);

            GL.BindVertexArray(0);
            vbo.Dispose();

            return new Mesh(vao, ibo);
        }

        public static Mesh GeneratePlane(int xDivisions, int yDivisions, bool riemannCut = false)
        {
            float[] vertices = new float[xDivisions * yDivisions * 5];
            int[] triangles = new int[(xDivisions - 1) * (yDivisions - 1) * 6];
            for (int y = 0; y < yDivisions - 1; y++)
            {
                for (int x = 0; x < xDivisions - 1; x++)
                {
                    //RiemannCut: If the mesh is used for riemann surfaces, it can sometimes help
                    //having a slice from 0 < x < 1 and y = 1/2 without indices.
                    if (y <= (yDivisions) / 2 && y >= (yDivisions - 2) / 2
                        && x <= xDivisions / 2 && riemannCut)
                        continue;
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
