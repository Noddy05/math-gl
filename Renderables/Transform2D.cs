using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Renderables
{
    class Transform2D : Transform
    {
        public Transform2D(){}

        public Transform2D(Vector3 position) : base(position){}

        public Transform2D(Vector3 position, Vector3 rotation) : base(position, rotation){}

        public Transform2D(Vector3 position, Vector3 rotation, Vector3 scale) : base(position, rotation, scale){}

        public override Matrix4 TransformationMatrix()
        {
            Matrix4 parentMatrix = Matrix4.Identity;
            if (parent != null)
                parentMatrix = parent.TransformationMatrix();

            return Matrix4.CreateRotationZ(rotation.Z)
                 
                * Matrix4.CreateTranslation(new Vector3(position.X, position.Y, 0)) * parentMatrix;
        }

        public override void Apply()
        {
            Matrix4 transformationMatrix = Matrix4.Identity;
            transformationMatrix *= Matrix4.CreateFromQuaternion(Quaternion.FromEulerAngles(
                new Vector3(0, MathF.PI / 2, MathF.PI / 2))) * Matrix4.CreateScale(
                    scale.X / Program.GetWindow().Size.X, scale.Y / Program.GetWindow().Size.Y, 1) 
                * TransformationMatrix() 
                * Matrix4.CreateTranslation(new Vector3(0, 0, -1)) * Matrix4.CreateScale(new Vector3(0.5f,
                (float)Program.GetWindow().Size.Y / Program.GetWindow().Size.X / 2f, 1));

            GL.UniformMatrix4(objectUniform, false, ref transformationMatrix);
        }
    }
}
