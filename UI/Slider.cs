using MathGL.Renderables;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using System;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGL.UI
{
    class Slider : Button
    {
        private float width;
        private float startX;
        public float stepSize;
        private float previousPositionStepped;
        private float sliderNonSteppedPosition;

        public Slider(float width, float stepSize = 0)
            :base()
        {
            this.width = width;
            this.stepSize = stepSize;

            Program.GetWindow().MouseMove += Move;
            Program.GetWindow().MouseUp += StepSlider;
        }

        protected override void NewComponentParent()
        {
            startX = parent.transform.position.X;
        }

        public float Value()
        {
            return (parent.transform.position.X - startX) / width;
        }

        private void Move(MouseMoveEventArgs e)
        {
            Vector2 delta = e.Delta;
            if (buttonPressed)
            {
                sliderNonSteppedPosition += delta.X * 2 / Program.GetWindow().Size.X;

                if (sliderNonSteppedPosition - startX < 0)
                    sliderNonSteppedPosition = 0 + startX;

                if (sliderNonSteppedPosition - startX > width)
                    sliderNonSteppedPosition = width + startX;

                parent.transform.position.X =
                    MathF.Round((sliderNonSteppedPosition - startX) / width / stepSize) * stepSize * width + startX;
            }
        }
        private void StepSlider(MouseButtonEventArgs e)
        {
            if(e.Button == MouseButton.Left)
                sliderNonSteppedPosition = MathF.Round((sliderNonSteppedPosition - startX) / width / stepSize) * stepSize * width + startX;
        }
    }
}
