using MathGL.RenderObjects;
using MathGL.Shaders;
using MathGL.UI;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Renderables
{
    class UIObject
    {
        public Mesh mesh;
        private Material material;
        public readonly Transform2D transform;

        private List<UIComponent> components = new();

        private Matrix4 projectionMatrix;
        private int projectionUniform;

        public void SetMaterial(Material material)
        {
            this.material = material;
            transform.ChangeMaterial(material);
            projectionUniform = GL.GetUniformLocation(material.shader, "projection");
        }

        private void SetProjectionMatrix(ResizeEventArgs e)
        {
            projectionMatrix = Matrix4.CreateOrthographic(1, (float)Program.GetWindow().Size.Y / Program.GetWindow().Size.X,
                0.01f, 1000f);
        }

        public UIObject(Mesh mesh, Material material)
        {
            this.mesh = mesh;
            transform = new Transform2D();
            SetMaterial(material);
            Program.GetWindow().Resize += SetProjectionMatrix;
            SetProjectionMatrix(new ResizeEventArgs());
        }

        public UIObject AddComponent(UIComponent component)
        {
            component.SetComponentParent(this);
            components.Add(component);
            return this;
        }

        public UIObject Scaled(Vector3 scale)
        {
            transform.scale = scale;
            return this;
        }
        public UIObject Positioned(Vector3 pos)
        {
            transform.position = pos;
            return this;
        }
        public UIObject Rotated(Vector3 rot)
        {
            transform.rotation = rot;
            return this;
        }

        public bool MouseHovering()
        {
            Vector2 mousePosition = Program.GetWindow().MousePosition / Program.GetWindow().Size;
            mousePosition.Y = 1 - mousePosition.Y;
            return mousePosition.X >= PositionToPixel().X && mousePosition.X <= EndPositionToPixel().X
                && mousePosition.Y >= PositionToPixel().Y && mousePosition.Y <= EndPositionToPixel().Y;
        }
        public Vector2 ParentOffset()
        {
            Vector3 offset = new Vector3();
            Transform parent = transform.parent;

            while(parent != null)
            {
                offset += parent.position;
                parent = parent.parent;
            }

            return new Vector2(offset.X, offset.Y);
        }
        public Vector2 PositionToPixel()
        {
            Vector2 offset = ParentOffset();

            return new Vector2(Map((transform.position.X + offset.X), -1, 1, 0, 1),
                Map((transform.position.Y + offset.Y), -1, 1, 0, 1));
        }
        public Vector2 EndPositionToPixel()
        {
            Vector2 offset = ParentOffset();

            return new Vector2(transform.position.X + transform.scale.X + offset.X, 
                transform.position.Y + transform.scale.Y + offset.Y);
        }

        public float Map(float t, float tMin, float tMax, float mappedMin, float mappedMax)
        {
            return mappedMin + (mappedMax - mappedMin) * (t - tMin) / (tMax - tMin);
        }

        public void Render()
        {
            Vector2 size = Program.GetWindow().Size;
            float widthToHeightRatio = size.X / size.Y;
            material.Bind(Matrix4.Identity);
            transform.Apply();
            projectionMatrix = Matrix4.CreateOrthographic(1, size.Y / size.X,
                0.01f, 1000f);
            GL.UniformMatrix4(projectionUniform, false, ref projectionMatrix);

            GL.BindVertexArray(mesh.vao);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.ibo);
            GL.DrawElements(PrimitiveType.Triangles, mesh.ibo.GetIndices().Length, DrawElementsType.UnsignedInt, 0);
        }
    }
}
