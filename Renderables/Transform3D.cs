using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Renderables
{
    class Transform3D : Transform
    {
        

        public Transform3D(){}

        public Transform3D(Vector3 position) : base(position){}

        public Transform3D(Vector3 position, Vector3 rotation) : base(position, rotation){}

        public Transform3D(Vector3 position, Vector3 rotation, Vector3 scale) : base(position, rotation, scale){}

        public override void Apply()
        {
            Matrix4 transformationMatrix = Matrix4.CreateFromQuaternion(Quaternion.FromEulerAngles(rotation)) 
                * Matrix4.CreateTranslation(position);
            GL.UniformMatrix4(objectUniform, false, ref transformationMatrix);
        }
    }
}
