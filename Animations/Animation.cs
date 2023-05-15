using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.Animations
{

    class Animation
    {
        struct Function
        {
            public float time;
            public float startValue;
            public float endValue;
            public VariableVoid function;
        }

        private Function function;
        public virtual void Play() {
            Program.GetWindow().RenderFrame += OnRender;
        }

        public delegate void VariableVoid(float t);
        public delegate float InterpolationMethod(float t);
        protected void LinearLerp(VariableVoid function, float startValue, float endValue, float time)
        {

        }

        private float timeSinceStart = 0;
        private void OnRender(FrameEventArgs args)
        {
            timeSinceStart += (float)args.Time;
            if (timeSinceStart > function.time)
                Program.GetWindow().RenderFrame -= OnRender;

            Frame(timeSinceStart);
        }

        public virtual void Frame(float time)
        {

        }
    }
}
