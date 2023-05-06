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
    class Transform
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;

        protected int objectUniform;

        public Transform()
        {
            position = new Vector3();
            rotation = new Vector3();
            scale = new Vector3();
        }
        public Transform(Vector3 position)
        {
            this.position = position;
            rotation = new Vector3();
            scale = new Vector3();
        }
        public Transform(Vector3 position, Vector3 rotation)
        {
            this.position = position;
            this.rotation = rotation;
            scale = new Vector3();
        }
        public Transform(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }

        public void ChangeMaterial(Material material)
        {
            objectUniform = GL.GetUniformLocation(material.shader, "object");
        }

        public virtual void Apply() { }
    }
}
