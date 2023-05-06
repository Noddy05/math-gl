using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.RenderObjects
{
    class VAO : IDisposable
    {
        private int handle;
        private bool disposed;

        public VAO()
        {
            handle = GL.GenVertexArray();
        }

        public void BindVertexAttribPointer(int index, int size, VertexAttribPointerType pointerType, int stride, int offset)
        {
            GL.VertexAttribPointer(index, size, pointerType, false, stride * sizeof(float), offset * sizeof(float));
            GL.EnableVertexAttribArray(index);
        }

        public int GetHandle() => handle;


        /// <summary>
        /// Disposes the <see cref="VAO"/> object.<br/>
        /// Should be done for all instantiated <see cref="VAO"/>s
        /// </summary>
        public void Dispose()
        {
            if (disposed)
                return;

            GL.BindVertexArray(0);
            GL.DeleteVertexArray(handle);

            disposed = true;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implicitly converts from <see cref="VAO"/> to an <see cref="int"/> representing its handle.
        /// </summary>
        public static implicit operator int(VAO vao) => vao.GetHandle();
    }
}