using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathGL.RenderObjects;
using MathGL.Import;
using MathGL.InputManager;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using MathGL.Generation;
using MathGL.Renderables;
using MathGL.Shaders;
using MathGL.UI;
using MathGL.CameraTypes;

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
        private List<GameObject> graphs = new List<GameObject>();
        private RiemannSurface riemannSurfaceMaterial;
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<UIObject> uiObjects = new List<UIObject>();
        private Slider slider;
        public float timeSinceStart;
        public int sliderSteps = 8;

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            CreateProjectionMatrix();
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
            camera = new OrbitCamera(new Vector3(0.5f, 0.75f, -2.5f), new Vector3(0, 0, -5f));
            //camera = new FreeCamera(new Vector3(0.5f, 0.75f, -2.5f), new Vector3(0, 0, 0f));
            CreateProjectionMatrix();

            //Setup inputs
            Input.Instantiate();

            //Enable depth and backface culling
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            //Generate font and texture
            FontLoader.AddFont(@"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\src\testCMU_0.png",
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\src\testCMU.fnt");
            int textTexture = FontLibrary.fonts["CMU Sans Serif"].textureHandle;

            //First Plot GameObject
            Mesh riemannPlaneMesh = MeshGeneration.GeneratePlane(501, 501, false);
            riemannSurfaceMaterial = new RiemannSurface(0);
            riemannSurfaceMaterial.cullFaces = false;
            graphs.Add(new GameObject(riemannPlaneMesh, riemannSurfaceMaterial));
            graphs[graphs.Count - 1].transform.position = new Vector3(0, 0, -3);

            //Text GameObject
            Mesh textMesh = TextGeneration.GenerateMesh("f(z) = z^2 + 0.2 \nPos: (Re(z), Im(z), |z|) \nHSV: (Arg(z), 1, 1)", "CMU Sans Serif");
            Material textMaterial = new TexturedMaterial(textTexture);
            gameObjects.Add(new GameObject(textMesh, textMaterial));
            gameObjects[gameObjects.Count - 1].transform.position = new Vector3(0, 3, -2);

            //Quad GameObject
            Mesh quadMesh = MeshGeneration.GenerateQuad();
            Material graphAxis = new DefaultShader(
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Custom\2DGraphAxes\graph_vert.glsl",
                @"C:\Users\noah0\source\repos\OpenGL Math\OpenGL Math\Shaders\Custom\2DGraphAxes\graph_frag.glsl");
            gameObjects.Add(new GameObject(quadMesh, graphAxis));
            gameObjects[gameObjects.Count - 1].transform.position = new Vector3(0, 0, -3);

            //X-Axis Name GameObject
            Mesh xMesh = TextGeneration.GenerateMesh("Re(z)", "CMU Sans Serif");
            gameObjects.Add(new GameObject(xMesh, textMaterial));
            gameObjects[gameObjects.Count - 1].transform.position = new Vector3(0.5f, 0, -2);
            gameObjects[gameObjects.Count - 1].transform.rotation = new Vector3(-3.14f / 2, 0, 0);

            //Y-Axis Name GameObject
            Mesh yMesh = TextGeneration.GenerateMesh("Im(z)", "CMU Sans Serif");
            gameObjects.Add(new GameObject(yMesh, textMaterial));
            gameObjects[gameObjects.Count - 1].transform.position = new Vector3(1, 0, -2.5f);
            gameObjects[gameObjects.Count - 1].transform.rotation = new Vector3(-3.14f / 2, 0, 3.14f / 2);

            Material redMaterial = new UnlitMaterial(Color4.Red);
            Material greenMaterial = new UnlitMaterial(Color4.Green);
            Material blackMaterial = new UnlitMaterial(Color4.Black);
            Material colorWheelMaterial = new ColorWheelMaterial();
            UIObject sliderBehind = new UIObject(quadMesh, blackMaterial).Positioned(new Vector3(0.125f, -0.1f, 0))
                 .Scaled(new Vector3(0.25f, 0.1f, 1));

            UIObject header = new UIObject(quadMesh, greenMaterial).Positioned(new Vector3(0f, 0f, 0))
                .Scaled(new Vector3(0.1f, 0.1f, 1)).AddComponent(new Draggable(0.5f, 0.6f));
            uiObjects.Add(header);
            UIObject redWindow = new UIObject(quadMesh, redMaterial).Positioned(new Vector3(0, -0.5f, 0))
                .Scaled(new Vector3(0.5f, 0.5f, 1));
            redWindow.transform.parent = header.transform;
            sliderBehind.transform.parent = header.transform;

            this.slider = new(0.15f, 1f / sliderSteps);
            UIObject slider = new UIObject(quadMesh, greenMaterial).Positioned(new Vector3(0.125f, -0.1f, 0))
                .Scaled(new Vector3(0.1f, 0.1f, 1)).AddComponent(this.slider);
            slider.transform.parent = header.transform;

            UIObject colorWheel = new UIObject(quadMesh, colorWheelMaterial).Positioned(new Vector3(0.25f, -0.5f, 0))
                .Scaled(new Vector3(0.1f, 0.1f, 1));
            colorWheel.transform.parent = header.transform;
            uiObjects.Add(colorWheel);
            uiObjects.Add(slider);
            uiObjects.Add(sliderBehind);
            uiObjects.Add(redWindow);


            //Quad GameObject
            Material riemannSphereMaterial = new RiemannSphere();
            Mesh cubeMesh = MeshGeneration.GenerateCube();
            riemannSphereMaterial.cullFaces = false;
            gameObjects.Add(new GameObject(cubeMesh, riemannSphereMaterial));
            gameObjects[gameObjects.Count - 1].transform.position = new Vector3(0, 0, 0);

            //Quad GameObject
            Material contourMapMaterial = new ContourMap();
            riemannSphereMaterial.cullFaces = false;
            gameObjects.Add(new GameObject(quadMesh, contourMapMaterial));
            gameObjects[gameObjects.Count - 1].transform.position = new Vector3(0, 0, -1.5f);

            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            timeSinceStart += (float)args.Time;
            riemannSurfaceMaterial.branch = 0;

            //Clear background
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Render();
            }

            //Use instanced rendering to draw multiple branches of a graph
            foreach (GameObject gameObject in graphs)
            {
                gameObject.PrepareRender();
                #pragma warning disable CS0618 // Type or member is obsolete
                GL.DrawElementsInstanced(BeginMode.Triangles, gameObject.mesh.ibo.GetIndices().Length, 
                    DrawElementsType.UnsignedInt, IntPtr.Zero, 1);
                #pragma warning restore CS0618 // Type or member is obsolete IntPtr.Zero is obsolete, but 0 doesn't work! :(
            }
            /*
            foreach (UIObject uiObject in uiObjects)
            {
                uiObject.Render();
            }*/

            //Swap buffers
            Context.SwapBuffers();

            base.OnRenderFrame(args);
        }
    }
}
