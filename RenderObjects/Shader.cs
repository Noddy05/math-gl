using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace MathGL.RenderObjects
{
    class Shader : IDisposable
    {
        private int shaderHandle;

        private bool disposed;

        public int GetHandle() => shaderHandle;

        /// <summary>
        /// Creates a new <see cref="Shader"/> object. Used by renderer.<br/>
        /// Parameters:<br/>
        /// - <paramref name="vertexShaderLocation"/>; a string with the location of your vertex shader glsl file.<br/>
        /// - <paramref name="fragmentShaderLocation"/>; a string with the location of your fragment shader glsl file.<br/>
        /// </summary>
        /// <param name="vertexShaderLocation"></param>
        /// <param name="fragmentShaderLocation"></param>
        public Shader(string vertexShaderLocation, string fragmentShaderLocation)
        {
            GenerateShader(vertexShaderLocation, fragmentShaderLocation);
        }

        private static int LoadShader(string location, ShaderType type)
        {
            int shader = GL.CreateShader(type);

            //Parsing shader (Searching for #include FILE_LOCATION)
            string[] lines = File.ReadAllLines(location);
            string shaderFileParsed = "";
            for(int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == ' '
                        || line[j] == '\t')
                        continue;

                    string searchText = "#include";
                    if (j + searchText.Length >= line.Length)
                        break;
                    if(line.Substring(j, searchText.Length) == searchText)
                    {
                        Console.WriteLine("search found!");
                        if(line[j + searchText.Length] == ' ')
                        {
                            Console.WriteLine("space found!");
                            line = File.ReadAllText(line.Substring(j + searchText.Length
                                + 1, line.Length - (j + searchText.Length + 1)));
                        }
                    }
                }
                shaderFileParsed += line + '\n';
            }

            GL.ShaderSource(shader, shaderFileParsed);
            GL.CompileShader(shader);

            string log = GL.GetShaderInfoLog(shader);
            if (string.IsNullOrEmpty(log))
                return shader;

            throw new Exception(log);
        }
        private void GenerateShader(string vertexShaderLocation, string fragmentShaderLocation)
        {
            shaderHandle = GL.CreateProgram();

            int vertexShaderHandle = LoadShader(vertexShaderLocation, ShaderType.VertexShader);
            int fragmentShaderHandle = LoadShader(fragmentShaderLocation, ShaderType.FragmentShader);

            GL.AttachShader(shaderHandle, vertexShaderHandle);
            GL.AttachShader(shaderHandle, fragmentShaderHandle);
            GL.LinkProgram(shaderHandle);

            GL.DetachShader(shaderHandle, vertexShaderHandle);
            GL.DetachShader(shaderHandle, fragmentShaderHandle);

            GL.DeleteShader(vertexShaderHandle);
            GL.DeleteShader(fragmentShaderHandle);

            string log = GL.GetProgramInfoLog(shaderHandle);

            if (string.IsNullOrEmpty(log))
                return;

            throw new Exception(log);
        }

        /// <summary>
        /// Disposes the <see cref="Shader"/> object.<br/>
        /// Should be done for all instantiated <see cref="Shader"/>s
        /// </summary>
        public void Dispose()
        {
            if (disposed)
                return;

            GL.UseProgram(0);
            GL.DeleteProgram(shaderHandle);

            disposed = true;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implicitly converts from <see cref="Shader"/> to an <see cref="int"/> representing its handle.
        /// </summary>
        public static implicit operator int(Shader shader) => shader.GetHandle();
    }
}
