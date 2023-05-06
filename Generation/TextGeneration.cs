using MathGL.Import;
using MathGL.RenderObjects;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Generation
{
    class TextGeneration
    {
        public static Mesh GenerateMesh(string text, string fontface)
        {
            float xAdvanced = 0;
            float yAdvanced = 0;

            float[] vertices = new float[text.Length * 4 * 5];
            int[] triangles = new int[text.Length * 2 * 3];
            for(int i = 0; i < text.Length; i++)
            {
                if(text[i] == '\n')
                {
                    xAdvanced = 0;
                    yAdvanced -= 0.2f;
                    continue;
                }
                CharLocation charLocation = FontLibrary.fonts[fontface].charOffsets[text[i]];
                float charWidth = (charLocation.xEnd - charLocation.xStart);
                float charHeight = (charLocation.yEnd - charLocation.yStart);
                float[] charVerts =
                {
                    charLocation.xOffset + xAdvanced, -charHeight - charLocation.yOffset + yAdvanced, 0,
                       charLocation.xStart, charLocation.yEnd,
                    charWidth + charLocation.xOffset + xAdvanced, -charHeight - charLocation.yOffset + yAdvanced, 0,
                       charLocation.xEnd, charLocation.yEnd,
                    charLocation.xOffset + xAdvanced, - charLocation.yOffset + yAdvanced, 0,
                       charLocation.xStart, charLocation.yStart,
                    charWidth + charLocation.xOffset + xAdvanced, - charLocation.yOffset + yAdvanced, 0,
                                charLocation.xEnd, charLocation.yStart,
                };
                int[] charTriangles =
                {
                    0 + i * 4, 1 + i * 4, 2 + i * 4,
                    3 + i * 4, 2 + i * 4, 1 + i * 4
                };
                for (int j = 0; j < charVerts.Length; j++)
                {
                    vertices[i * 4 * 5 + j] = charVerts[j];
                }
                for (int j = 0; j < charTriangles.Length; j++)
                {
                    triangles[i * 2 * 3 + j] = charTriangles[j];
                }

                xAdvanced += charLocation.xAdvance;
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
