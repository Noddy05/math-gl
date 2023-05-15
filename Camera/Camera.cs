using MathGL.Behaviour;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace MathGL.CameraTypes
{
    class Camera : ActiveBehaviour
    {
        protected Vector3 position = new Vector3();
        protected Vector3 rotation = new Vector3();
        protected float fov = 60;
        protected float cameraSensitivity = 1 / 800f;
        protected Matrix4 projectionMatrix;
        protected Vector2 rotationConstraints = new Vector2(-MathF.PI / 2, MathF.PI / 2);

        public Camera(Vector3 startingPosition, Vector3 startingRotation, float fov = 60, float cameraSensitivity = 1 / 800f)
        {
            position = startingPosition;
            rotation = startingRotation;
            this.fov = fov;
            this.cameraSensitivity = cameraSensitivity;
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(fov / 180f * MathF.PI,
                (float)Program.GetWindow()!.Size.X / Program.GetWindow()!.Size.Y, 0.1f, 1000f);
            Program.GetWindow().MouseMove += OnMouseMove;
            Program.GetWindow().MouseUp += OnMouseButton;
            Program.GetWindow().MouseDown += OnMouseButton;
        }

        public Camera()
        {
            position = new Vector3();
            rotation = new Vector3();
            fov = 60;
            cameraSensitivity = 1 / 800f;
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(fov / 180f * MathF.PI,
                (float)Program.GetWindow()!.Size.X / Program.GetWindow()!.Size.Y, 0.1f, 1000f);
            Program.GetWindow().MouseMove += OnMouseMove;
            Program.GetWindow().MouseUp += OnMouseButton;
            Program.GetWindow().MouseDown += OnMouseButton;
        }

        public override void Update(FrameEventArgs e)
        {
        }

        public virtual void OnMouseMove(MouseMoveEventArgs e)
        {
        }

        private bool holdingLeft = false;
        public virtual void OnMouseButton(MouseButtonEventArgs e)
        {
        }

        public Matrix4 GetProjectionMatrix()
            => projectionMatrix;
        public void SetProjectionMatrix(Matrix4 projectionMatrix)
            => this.projectionMatrix = projectionMatrix;

        public void Move(Vector3 movement)
        {
            position += movement;
        }

        public void Rotate(Vector3 rotation)
        {
            this.rotation += rotation * cameraSensitivity;
            this.rotation.X = Math.Clamp(this.rotation.X, rotationConstraints.X, rotationConstraints.Y);
        }

        public Vector3 Position() => position;

        public Vector3 Up() => (Vector4.UnitY * CameraMatrix().Inverted()).Xyz;
        public Vector3 Down() => -Up();
        public Vector3 Forward() => (Vector4.UnitZ * CameraMatrix().Inverted()).Xyz;
        public Vector3 Backward() => -Forward();
        public Vector3 Right() => -(Vector4.UnitX * CameraMatrix().Inverted()).Xyz;
        public Vector3 Left() => -Right();

        public float GetFOV() => angleToRad(fov);
        public float angleToRad(float angle) => angle * MathF.PI / 180;
        public float radToAngle(float rad) => rad / MathF.PI * 180;

        public virtual Matrix4 CameraMatrix()
            => Matrix4.CreateTranslation(position)
                * Matrix4.CreateFromQuaternion(Quaternion.FromEulerAngles(rotation));

        public Matrix4 CameraRotationMatrix()
            => Matrix4.CreateFromQuaternion(Quaternion.FromEulerAngles(rotation));
    }
}
