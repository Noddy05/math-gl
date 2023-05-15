using MathGL.Behaviour;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using OpenTK.Windowing.GraphicsLibraryFramework;
using MathGL.InputManager;

namespace MathGL.CameraTypes
{
    class OrbitCamera : Camera
    {
        public Vector3 offset;
        public Vector3 center;

        public OrbitCamera(Vector3 center, Vector3 offset, Vector3 startingPosition, Vector3 startingRotation, float fov = 60, float cameraSensitivity = 1 / 800f)
            : base(startingPosition, startingRotation, fov, cameraSensitivity)
        {
            this.center = center;
            this.offset = offset;
        }

        public OrbitCamera(Vector3 center, Vector3 offset)
            : base()
        {
            this.center = center;
            this.offset = offset;
            Program.GetWindow().MouseWheel += OnMouseWheel;
        }
        public override void Update(FrameEventArgs e)
        {
            if (Input.KeyDown("Q"))
                center -= Vector3.UnitY * (float)e.Time;
            if (Input.KeyDown("E"))
                center += Vector3.UnitY * (float)e.Time;
        }

        public override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (!Input.ButtonDownByIndex(2))
                return;

            rotation += new Vector3(e.DeltaY, e.DeltaX, 0) * cameraSensitivity;
            rotation.X = Math.Clamp(rotation.X, rotationConstraints.X, rotationConstraints.Y);
        }

        public void OnMouseWheel(MouseWheelEventArgs e)
        {
            if(e.OffsetY < 0)
            {
                offset.Z *= 1.15f;
            }
            else
            {
                offset.Z /= 1.15f;
            }
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
            => Matrix4.CreateTranslation(position - center)
                * Matrix4.CreateFromQuaternion(Quaternion.FromEulerAngles(rotation))
                * Matrix4.CreateTranslation(-(position - offset));
    }
}