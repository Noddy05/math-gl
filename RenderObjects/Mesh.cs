using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.RenderObjects
{
    struct Mesh
    {
        public VAO vao; 
        public IBO ibo;
        public Mesh(VAO vao, IBO ibo)
        {
            this.vao = vao;
            this.ibo = ibo;
        }
    }
}
