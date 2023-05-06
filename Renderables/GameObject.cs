using MathGL.RenderObjects;
using MathGL.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Renderables
{
    class GameObject
    {
        public Mesh mesh;
        private Material material;
        public readonly Transform3D transform;

        private int projectionUniform;

        public void SetMaterial(Material material)
        {
            this.material = material;
            transform.ChangeMaterial(material);
            projectionUniform = GL.GetUniformLocation(material.shader, "projection");
        }

        public GameObject(Mesh mesh, Material material)
        {
            this.mesh = mesh;
            transform = new Transform3D();
            SetMaterial(material);
        }

        public void Render()
        {
            material.Bind(Program.GetWindow().camera.CameraMatrix());
            transform.Apply();
            Matrix4 projectionMatrix = Program.GetWindow()!.camera.GetProjectionMatrix();
            GL.UniformMatrix4(projectionUniform, false, ref projectionMatrix);

            GL.BindVertexArray(mesh.vao);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.ibo);
            GL.DrawElements(PrimitiveType.Triangles, mesh.ibo.GetIndices().Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
