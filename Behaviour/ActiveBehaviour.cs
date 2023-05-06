using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Behaviour
{
    class ActiveBehaviour
    {
        public ActiveBehaviour()
        {
            Start();
            Window window = Program.GetWindow();
            window.RenderFrame += Update;
        }

        public virtual void Start()
        {

        }
        public virtual void Update(FrameEventArgs e)
        {

        }
    }
}
