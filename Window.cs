using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathGL.RenderObjects;
using MathGL.Import;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using MathGL.Generation;
using MathGL.Renderables;
using MathGL.Shaders;

namespace MathGL
{
    class Window : GameWindow
    {
        public Window()
            :base(GameWindowSettings.Default, new NativeWindowSettings()
            {
                Title = "Math Window",
                Size = new(1920, 1080),
                StartVisible = false,
                APIVersion = new(3, 3)
            })
        {

        }
        
        public Camera camera;
        private GameObject plane;
        private GameObject text;

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
            CreateProjectionMatrix();
            base.OnResize(e);
        }

        private void CreateProjectionMatrix()
        {
            camera.SetProjectionMatrix(Matrix4.CreatePerspectiveFieldOfView(80 / 180f * MathF.PI, Size.X / (float)Size.Y, 0.001f, 1000f));
        }

        protected override void OnLoad()
        {
            //Setup
            IsVisible = true;
            GL.ClearColor(new Color4(255, 255, 255, 255));
            camera = new Camera();
            CreateProjectionMatrix();
            Input.Instantiate();
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            //Generate font and texture
            FontLoader.AddFont(@"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\src\testCMU_0.png",
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\src\testCMU.fnt");
            int textTexture = FontLibrary.fonts["CMU Sans Serif"].textureHandle;

            //Plane GameObject
            Mesh planeMesh = MeshGeneration.GeneratePlane(121, 121);
            Material texturedMaterial = new Simple2DGraph();
            plane = new GameObject(planeMesh, texturedMaterial);
            plane.transform.position = new Vector3(0, -1, -3);
            //plane.transform.rotation = new Vector3(3.14f / 2, 0, 0);

            //Text GameObject
            Mesh textMesh = TextGeneration.GenerateMesh("f(z) = z^2 + 0.2 \nPos: (Re(z), Im(z), |z|) \nHSV: (Arg(z), 1, 1)", "CMU Sans Serif");
            Material textMaterial = new TexturedMaterial(textTexture);
            text = new GameObject(textMesh, textMaterial);
            text.transform.position += new Vector3(0, 0, -2);

            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            //Clear background
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //Render GameObjects
            text.Render();
            plane.Render();
            
            //Swap buffers
            Context.SwapBuffers();

            base.OnRenderFrame(args);
        }
    }
}
