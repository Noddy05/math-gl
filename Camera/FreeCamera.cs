using MathGL.Behaviour;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using OpenTK.Windowing.GraphicsLibraryFramework;
using MathGL.InputManager;

namespace MathGL.CameraTypes
{
    class FreeCamera : Camera
    {
        public FreeCamera(Vector3 startingPosition, Vector3 startingRotation, float fov = 60, float cameraSensitivity = 1 / 800f)
            : base(startingPosition, startingRotation, fov, cameraSensitivity)
        {
        }

        public FreeCamera()
            : base()
        {
        }

        public override void Update(FrameEventArgs e)
        {
            position += (Forward() * Input.Vertical() + Right() * Input.Horizontal()) * (float)e.Time;

            if (Input.KeyDown("Q"))
                position += Vector3.UnitY * (float)e.Time;
            if (Input.KeyDown("E"))
                position -= Vector3.UnitY * (float)e.Time;
        }

        public override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (!Input.ButtonDownByIndex(2))
                return;

            rotation += new Vector3(e.DeltaY, e.DeltaX, 0) * cameraSensitivity;
            rotation.X = Math.Clamp(rotation.X, rotationConstraints.X, rotationConstraints.Y);
        }

        private bool holdingLeft = false;
        public override void OnMouseButton(MouseButtonEventArgs e)
        {
            if (e.Button != MouseButton.Right)
                return;

            holdingLeft = e.IsPressed;
            Program.GetWindow()!.CursorState = holdingLeft ? CursorState.Grabbed : CursorState.Normal;
        }

        public override Matrix4 CameraMatrix()
            => Matrix4.CreateTranslation(position)
                * Matrix4.CreateFromQuaternion(Quaternion.FromEulerAngles(rotation));
    }
}
